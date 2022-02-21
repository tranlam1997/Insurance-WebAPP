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
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;

namespace Sem3Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LifePolicyController : ControllerBase
    {
        private readonly ILifePolicyRepository _lifePolicyRepository;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private IConfiguration _config;

        public LifePolicyController(
            ILifePolicyRepository lifePolicyRepository,
            IMapper mapper,
            IUserRepository userRepository,
            IConfiguration config
        ) {
            _lifePolicyRepository = lifePolicyRepository;
            _mapper = mapper;
            _userRepository = userRepository;
            _config = config;
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public IActionResult CreateLifePolicy([FromBody] LifePolicyCreateDto lifePolicyCreateDto)
        {
            try
            {
                var currentUser = GetCurrentUser();
                var result = _lifePolicyRepository.CreateLifePolicy(lifePolicyCreateDto, currentUser.Id);
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
        public IActionResult GetLifePolicies([FromQuery] PaginationFilter paginationFilter)
        {
            try
            {
                var lifePolicies = _lifePolicyRepository.GetLifePolicies(paginationFilter);
                var metadata = new
                {
                    lifePolicies.TotalCount,
                    lifePolicies.PageSize,
                    lifePolicies.CurrentPage,
                    lifePolicies.HasNext,
                    lifePolicies.HasPrevious,
                };

                Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

                var lifePolicyDtos = new List<LifePolicyDto>();
                foreach (var lifePolicy in lifePolicies)
                {
                    LifePolicyDto data = _mapper.Map<LifePolicyDto>(lifePolicy);
                    if (lifePolicy.CreatedBy != null)
                    {
                        var user = _userRepository.GetUser(lifePolicy.CreatedBy);

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

                    if (lifePolicy.ModifiedBy != null)
                    {
                        var user = _userRepository.GetUser(lifePolicy.ModifiedBy);

                        if (user != null)
                        {
                            data.ModifiedByInfo = new ModifierInfoDto
                            {
                                Id = user.Id,
                                Email = user.Email,
                                FirtsName = user.FirtsName,
                                LastName = user.LastName,
                                Role = user.Role,
                            };
                        }
                    }

                    lifePolicyDtos.Add(data);
                }

                return Ok(new
                {
                    Data = lifePolicyDtos,
                    metadata = metadata
                });

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpGet]
        [Authorize(Roles = "Administrator,Staff")]
        public IActionResult GetLifePoliciesForAdmin(
            [FromQuery] PaginationFilter paginationFilter,
            [FromQuery] LifePolicyFilter lifePolicyFilter
        ) {
            try
            {
                var lifePolicies = _lifePolicyRepository.GetLifePoliciesForAdmin(
                    paginationFilter,
                    lifePolicyFilter
                );
                var metadata = new
                {
                    lifePolicies.TotalCount,
                    lifePolicies.PageSize,
                    lifePolicies.CurrentPage,
                    lifePolicies.HasNext,
                    lifePolicies.HasPrevious,
                };

                Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

                var lifePolicyDtos = new List<LifePolicyDto>();

                foreach (var lifePolicy in lifePolicies)
                {
                    LifePolicyDto data = _mapper.Map<LifePolicyDto>(lifePolicy);

                    if (lifePolicy.CreatedBy != null)
                    {
                        var user = _userRepository.GetUser(lifePolicy.CreatedBy);

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

                    if (lifePolicy.ModifiedBy != null)
                    {
                        var user = _userRepository.GetUser(lifePolicy.ModifiedBy);

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

                    lifePolicyDtos.Add(data);
                }

                return Ok(new
                {
                    Data = lifePolicyDtos,
                    metadata = metadata
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpGet("{id}/non-logged")]
        [AllowAnonymous]
        public IActionResult GetLifePolicy(string id)
        {
            try
            {
                var lifePolicy = _lifePolicyRepository.GetLifePolicy(id);

                if (lifePolicy == null)
                {
                    return NotFound(new { message = "Vehicle policy not found" });
                }
                else
                {
                    var lifePolicyDto = _mapper.Map<LifePolicyDto>(lifePolicy);

                    if (lifePolicy.CreatedBy != null)
                    {
                        var user = _userRepository.GetUser(lifePolicy.CreatedBy);

                        if (user != null)
                        {
                            lifePolicyDto.CreatedByInfo = new ModifierInfoDto
                            {
                                Id = user.Id,
                                Email = user.Email,
                                FirtsName = user.FirtsName,
                                LastName = user.LastName,
                                Role = user.Role,
                            };
                        }
                    }

                    if (lifePolicy.ModifiedBy != null)
                    {
                        var user = _userRepository.GetUser(lifePolicy.ModifiedBy);

                        if (user != null)
                        {
                            lifePolicyDto.ModifiedByInfo = new ModifierInfoDto
                            {
                                Id = user.Id,
                                Email = user.Email,
                                FirtsName = user.FirtsName,
                                LastName = user.LastName,
                                Role = user.Role,
                            };
                        }
                    }

                    return Ok(new { Data = lifePolicyDto });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpGet("{id}")]
        [Authorize(Roles ="Administrator,Staff")]
        public IActionResult GetLifePolicyForAdmin(string id) 
        {
            try
            {
                var lifePolicy = _lifePolicyRepository.GetLifePolicyForAdmin(id);
                if (lifePolicy == null)
                {
                    return NotFound(new { message = "Vehicle policy not found" });
                }
                else
                {
                    var lifePolicyDto = _mapper.Map<LifePolicyDto>(lifePolicy);

                    if (lifePolicy.CreatedBy != null)
                    {
                        var user = _userRepository.GetUser(lifePolicy.CreatedBy);

                        if (user != null)
                        {
                            lifePolicyDto.CreatedByInfo = new ModifierInfoDto
                            {
                                Id = user.Id,
                                Email = user.Email,
                                FirtsName = user.FirtsName,
                                LastName = user.LastName,
                                Role = user.Role,
                            };
                        }
                    }

                    if (lifePolicy.ModifiedBy != null)
                    {
                        var user = _userRepository.GetUser(lifePolicy.ModifiedBy);

                        if (user != null)
                        {
                            lifePolicyDto.ModifiedByInfo = new ModifierInfoDto
                            {
                                Id = user.Id,
                                Email = user.Email,
                                FirtsName = user.FirtsName,
                                LastName = user.LastName,
                                Role = user.Role,
                            };
                        }
                    }

                    return Ok(new { Data = lifePolicyDto });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Administrator")]
        public IActionResult UpdateLifePolicy([FromBody] LifePolicyUpdateDto lifePolicyUpdateDto, string id)
        {
            try
            {
                var currentUser = GetCurrentUser();
                var result = _lifePolicyRepository.UpdateLifePolicy(
                    lifePolicyUpdateDto,
                    id,
                    currentUser.Id
                );

                if (result == true)
                {
                    return Ok(new { message = "Update success" });
                }
                else
                {
                    return NotFound(new { message = "Life policy not found" });
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
