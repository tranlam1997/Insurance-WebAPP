using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Sem3Project.Filters;
using Sem3Project.Models;
using Sem3Project.Models.Dtos;
using Sem3Project.Repositories;
using Sem3Project.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Sem3Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private IConfiguration _config;
        private IMailService _mailService;

        public UserController(
            IUserRepository UserRepository,
            IMapper mapper,
            IConfiguration config,
            IMailService mailService
        )
        {
            _userRepository = UserRepository;
            _mapper = mapper;
            _config = config;
            _mailService = mailService;
        }

        [HttpGet]
        [Authorize(Roles = "Administrator,Staff")]
        public IActionResult GetUsers([FromQuery] PaginationFilter paginationFilter, string search)
        {
            try
            {
                var users = _userRepository.GetUsers(paginationFilter, search);

                var metadata = new
                {
                    users.TotalCount,
                    users.PageSize,
                    users.CurrentPage,
                    users.HasNext,
                    users.HasPrevious,
                };

                Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

                var userDtos = new List<UserDto>();
                foreach (var user in users)
                {
                    userDtos.Add(_mapper.Map<UserDto>(user));
                }

                return Ok(new { Data = userDtos, Metadata = metadata, });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public IActionResult Register([FromBody] UserRegisterDto userRegisterDto)
        {
            try
            {
                var result = _userRepository.CreateUser(userRegisterDto);
                return Ok(new { message = "Register success" });
            }
            catch (ValidationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public IActionResult Login([FromBody] UserLoginDto userLoginDto)
        {
            try
            {
                var user = _userRepository.Login(userLoginDto);
                if (user == null)
                {
                    return BadRequest(new { message = "Wrong email or password" });
                }
                else
                {
                    var token = Generate(user);
                    return Ok(new { token });
                }
            }
            catch (ValidationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpPut]
        public IActionResult UpdateUser([FromBody] UserUpdateDto userUpdateDto)
        {
            try
            {
                var currentUser = GetCurrentUser();
                var result = _userRepository.UpdateUser(userUpdateDto, currentUser.Id);

                if (result == true)
                {
                    return Ok(new { message = "Update success" });
                }
                else
                {
                    return NotFound(new { message = "User not found" });
                }
            }
            catch (ValidationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpGet("me")]
        [Authorize]
        public IActionResult GetMe()
        {
            try
            {
                var currentUser = GetCurrentUser();
                var userInfo = _userRepository.GetUser(currentUser.Id);

                if (userInfo == null)
                {
                    return NotFound(new { message = "User not found" });
                }
                else
                {
                    var userDto = _mapper.Map<UserDto>(userInfo);
                    return Ok(new { Data = userDto });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Administrator,Staff")]
        public IActionResult GetUser(string id)
        {
            try
            {
                var user = _userRepository.GetUser(id);

                if (user == null)
                {
                    return NotFound(new { message = "User not found" });
                }
                else
                {
                    var userDto = _mapper.Map<UserDto>(user);
                    return Ok(new { Data = userDto });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }

            //var result = _userRepository.GetUser(id);
            //if (result == null)
            //{
            //    return NotFound("User not found");
            //}
            //return Ok(new
            //{
            //    Data = result
            //});
        }

        [HttpPost("change-password")]
        [Authorize]
        public IActionResult ChangePassword([FromBody] ChangePasswordDto changePasswordDto)
        {
            try
            {
                var currentUser = GetCurrentUser();
                var result = _userRepository.ChangePassword(changePasswordDto, currentUser.Id);

                if (result == false)
                {
                    return BadRequest(new { message = "Password not match" });
                }
                else
                {
                    return Ok(new { message = "Change password success" });
                }
            }
            catch (ValidationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        // Test mail send grid
        //[HttpPost("test-send-mail")]
        //public async Task<IActionResult> SendMailTest()
        //{
        //    await _mailService.SendMailAsync("foet1997@gmail.com", "Test Subject", "Test Content");
        //    return Ok(new { message = "foet1997@gmail.com" });
        //}

        private string Generate(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Role, user.Role),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            };

            var token = new JwtSecurityToken(
                _config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(3600),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private Identifier GetCurrentUser()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            if (identity != null)
            {
                var userClaims = identity.Claims;
                return new Identifier
                {
                    Id = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.NameIdentifier)?.Value,
                    Role = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Role)?.Value,
                };
            }

            return null;
        }
    }
}
