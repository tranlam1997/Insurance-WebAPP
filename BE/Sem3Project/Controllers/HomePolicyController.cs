using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Sem3Project.Filters;
using Sem3Project.Models;
using Sem3Project.Models.Dtos;
using Sem3Project.Repositories;
using Sem3Project.Repositories.IRepository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using PayPal.Api;

namespace Sem3Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomePolicyController : ControllerBase
    {
        private readonly IHomePolicyRepository _homePolicyRepository;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private IConfiguration _config;

        public HomePolicyController(
            IHomePolicyRepository homePolicyRepository,
            IMapper mapper,
            IUserRepository userRepository,
            IConfiguration config
        )
        {
            _homePolicyRepository = homePolicyRepository;
            _mapper = mapper;
            _userRepository = userRepository;
            _config = config;
        }

        [HttpPost]
        //[ClaimRequirement(ClaimTypes.Role, "Administrator,Staff")]
        [Authorize(Roles = "Administrator")]
        public IActionResult CreateHomePolicy([FromBody] HomePolicyCreateDto homePolicyCreateDto)
        {
            try
            {
                var currentUser = GetCurrentUser();
                var result = _homePolicyRepository.CreateHomePolicy(
                    homePolicyCreateDto,
                    currentUser.Id
                );
                return Ok(new { message = "Create policy success" });
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

        [HttpGet("non-logged")]
        [AllowAnonymous]
        public IActionResult GetHomePolicies([FromQuery] PaginationFilter paginationFilter)
        {
            try
            {
                var homePolicies = _homePolicyRepository.GetHomePolicies(paginationFilter);
                var metadata = new
                {
                    homePolicies.TotalCount,
                    homePolicies.PageSize,
                    homePolicies.CurrentPage,
                    homePolicies.HasNext,
                    homePolicies.HasPrevious,
                };

                Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

                var homePolicyDtos = new List<HomePolicyDto>();
                foreach (var homePolicy in homePolicies)
                {
                    HomePolicyDto data = _mapper.Map<HomePolicyDto>(homePolicy);

                    if (homePolicy.CreatedBy != null)
                    {
                        var user = _userRepository.GetUser(homePolicy.CreatedBy);
                        if (user != null)
                        {
                            data.CreatedByInfo = new ModifierInfoDto
                            {
                                Id = user.Id,
                                Email = user.Email,
                                FirtsName = user.FirtsName,
                                LastName = user.LastName,
                                Role = user.Role,
                            };
                        }
                    }

                    if (homePolicy.ModifiedBy != null)
                    {
                        var user = _userRepository.GetUser(homePolicy.ModifiedBy);
                        if (user != null)
                        {
                            data.CreatedByInfo = new ModifierInfoDto
                            {
                                Id = user.Id,
                                Email = user.Email,
                                FirtsName = user.FirtsName,
                                LastName = user.LastName,
                                Role = user.Role,
                            };
                        }
                    }

                    homePolicyDtos.Add(data);
                }

                return Ok(new { Data = homePolicyDtos, metadata = metadata });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpGet]
        [Authorize(Roles = "Administrator,Staff")]
        public IActionResult GetHomePoliciesForAdmin(
            [FromQuery] PaginationFilter paginationFilter,
            [FromQuery] HomePolicyFilter homePolicyFilter
        )
        {
            try
            {
                var homePolicies = _homePolicyRepository.GetHomePoliciesForAdmin(
                    paginationFilter,
                    homePolicyFilter
                );
                var metadata = new
                {
                    homePolicies.TotalCount,
                    homePolicies.PageSize,
                    homePolicies.CurrentPage,
                    homePolicies.HasNext,
                    homePolicies.HasPrevious,
                };

                Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

                var homePolicyDtos = new List<HomePolicyDto>();
                foreach (var homePolicy in homePolicies)
                {
                    HomePolicyDto data = _mapper.Map<HomePolicyDto>(homePolicy);

                    if (homePolicy.CreatedBy != null)
                    {
                        var user = _userRepository.GetUser(homePolicy.CreatedBy);
                        if (user != null)
                        {
                            data.CreatedByInfo = new ModifierInfoDto
                            {
                                Id = user.Id,
                                Email = user.Email,
                                FirtsName = user.FirtsName,
                                LastName = user.LastName,
                                Role = user.Role,
                            };
                        }
                    }

                    if (homePolicy.ModifiedBy != null)
                    {
                        var user = _userRepository.GetUser(homePolicy.ModifiedBy);
                        if (user != null)
                        {
                            data.CreatedByInfo = new ModifierInfoDto
                            {
                                Id = user.Id,
                                Email = user.Email,
                                FirtsName = user.FirtsName,
                                LastName = user.LastName,
                                Role = user.Role,
                            };
                        }
                    }

                    homePolicyDtos.Add(data);
                }

                return Ok(new { Data = homePolicyDtos, metadata = metadata });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpGet("{id}/non-logged")]
        [AllowAnonymous]
        public IActionResult GetHomePolicy(string id)
        {
            try
            {
                var homePolicy = _homePolicyRepository.GetHomePolicy(id);
                if (homePolicy == null)
                {
                    return NotFound(new { message = "Home policy not found" });
                }
                else
                {
                    var homePolicyDto = _mapper.Map<HomePolicyDto>(homePolicy);

                    if (homePolicy.CreatedBy != null)
                    {
                        var user = _userRepository.GetUser(homePolicy.CreatedBy);
                        if (user != null)
                        {
                            homePolicyDto.CreatedByInfo = new ModifierInfoDto
                            {
                                Id = user.Id,
                                Email = user.Email,
                                FirtsName = user.FirtsName,
                                LastName = user.LastName,
                                Role = user.Role,
                            };
                        }
                    }

                    if (homePolicy.ModifiedBy != null)
                    {
                        var user = _userRepository.GetUser(homePolicy.ModifiedBy);
                        if (user != null)
                        {
                            homePolicyDto.ModifiedByInfo = new ModifierInfoDto
                            {
                                Id = user.Id,
                                Email = user.Email,
                                FirtsName = user.FirtsName,
                                LastName = user.LastName,
                                Role = user.Role,
                            };
                        }
                    }

                    return Ok(new { Data = homePolicyDto });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Administrator,Staff")]
        public IActionResult GetHomePolicyForAdmin(string id)
        {
            try
            {
                var homePolicy = _homePolicyRepository.GetHomePolicyForAdmin(id);

                if (homePolicy == null)
                {
                    return NotFound(new { message = "Home policy not found" });
                }
                else
                {
                    var homePolicyDto = _mapper.Map<HomePolicyDto>(homePolicy);

                    if (homePolicy.CreatedBy != null)
                    {
                        var user = _userRepository.GetUser(homePolicy.CreatedBy);

                        if (user != null)
                        {
                            homePolicyDto.CreatedByInfo = new ModifierInfoDto
                            {
                                Id = user.Id,
                                Email = user.Email,
                                FirtsName = user.FirtsName,
                                LastName = user.LastName,
                                Role = user.Role,
                            };
                        }
                    }

                    if (homePolicy.ModifiedBy != null)
                    {
                        var user = _userRepository.GetUser(homePolicy.ModifiedBy);

                        if (user != null)
                        {
                            homePolicyDto.ModifiedByInfo = new ModifierInfoDto
                            {
                                Id = user.Id,
                                Email = user.Email,
                                FirtsName = user.FirtsName,
                                LastName = user.LastName,
                                Role = user.Role,
                            };
                        }
                    }

                    return Ok(new { Data = homePolicyDto });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Administrator")]
        public IActionResult UpdateHomePolicy(
            [FromBody] HomePolicyUpdateDto homePolicyUpdateDto,
            string id
        )
        {
            try
            {
                var currentUser = GetCurrentUser();
                var result = _homePolicyRepository.UpdateHomePolicy(
                    homePolicyUpdateDto,
                    id,
                    currentUser.Id
                );

                if (result == true)
                {
                    return Ok(new { message = "Update success" });
                }
                else
                {
                    return NotFound(new { message = "Home policy not found" });
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
