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

namespace Sem3Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicalPolicyController : ControllerBase
    {
        private readonly IMedicalPolicyRepository _medicalPolicyRepository;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private IConfiguration _config;

        public MedicalPolicyController(
            IMedicalPolicyRepository medicalPolicyRepository,
            IMapper mapper,
            IUserRepository userRepository,
            IConfiguration config
        )
        {
            _medicalPolicyRepository = medicalPolicyRepository;
            _mapper = mapper;
            _userRepository = userRepository;
            _config = config;
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public IActionResult CreateMedicalPolicy(
            [FromBody] MedicalPolicyCreateDto medicalPolicyCreateDto
        )
        {
            try
            {
                var currentUser = GetCurrentUser();
                var result = _medicalPolicyRepository.CreateMedicalPolicy(
                    medicalPolicyCreateDto,
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
        public IActionResult GetMedicalPolicies([FromQuery] PaginationFilter paginationFilter)
        {
            try
            {
                var medicalPolicies = _medicalPolicyRepository.GetMedicalPolicies(paginationFilter);
                var metadata = new
                {
                    medicalPolicies.TotalCount,
                    medicalPolicies.PageSize,
                    medicalPolicies.CurrentPage,
                    medicalPolicies.HasNext,
                    medicalPolicies.HasPrevious,
                };

                Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

                var medicalPolicyDtos = new List<MedicalPolicyDto>();
                foreach (var medicalPolicy in medicalPolicies)
                {
                    MedicalPolicyDto data = _mapper.Map<MedicalPolicyDto>(medicalPolicy);

                    if (medicalPolicy.CreatedBy != null)
                    {
                        var user = _userRepository.GetUser(medicalPolicy.CreatedBy);
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

                    if (medicalPolicy.ModifiedBy != null)
                    {
                        var user = _userRepository.GetUser(medicalPolicy.ModifiedBy);
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

                    medicalPolicyDtos.Add(data);
                }

                return Ok(new { Data = medicalPolicyDtos, metadata = metadata });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpGet]
        [Authorize(Roles = "Administrator,Staff")]
        public IActionResult GetMedicalPoliciesForAdmin(
            [FromQuery] PaginationFilter paginationFilter,
            [FromQuery] MedicalPolicyFilter medicalPolicyFilter
        )
        {
            try
            {
                var medicalPolicies = _medicalPolicyRepository.GetMedicalPoliciesForAdmin(
                    paginationFilter,
                    medicalPolicyFilter
                );
                var metadata = new
                {
                    medicalPolicies.TotalCount,
                    medicalPolicies.PageSize,
                    medicalPolicies.CurrentPage,
                    medicalPolicies.HasNext,
                    medicalPolicies.HasPrevious,
                };

                Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

                var medicalPolicyDtos = new List<MedicalPolicyDto>();
                foreach (var medicalPolicy in medicalPolicies)
                {
                    MedicalPolicyDto data = _mapper.Map<MedicalPolicyDto>(medicalPolicy);

                    if (medicalPolicy.CreatedBy != null)
                    {
                        var user = _userRepository.GetUser(medicalPolicy.CreatedBy);
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

                    if (medicalPolicy.ModifiedBy != null)
                    {
                        var user = _userRepository.GetUser(medicalPolicy.ModifiedBy);
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

                    medicalPolicyDtos.Add(data);
                }

                return Ok(new { Data = medicalPolicyDtos, metadata = metadata });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpGet("{id}/non-logged")]
        [AllowAnonymous]
        public IActionResult GeMedicalPolicy(string id)
        {
            try
            {
                var medicalPolicy = _medicalPolicyRepository.GetMedicalPolicy(id);
                if (medicalPolicy == null)
                {
                    return NotFound(new { message = "Medical policy not found" });
                }
                else
                {
                    var medicalPolicyDto = _mapper.Map<MedicalPolicyDto>(medicalPolicy);

                    if (medicalPolicy.CreatedBy != null)
                    {
                        var user = _userRepository.GetUser(medicalPolicy.CreatedBy);
                        if (user != null)
                        {
                            medicalPolicyDto.CreatedByInfo = new ModifierInfoDto
                            {
                                Id = user.Id,
                                Email = user.Email,
                                FirtsName = user.FirtsName,
                                LastName = user.LastName,
                                Role = user.Role,
                            };
                        }
                    }

                    if (medicalPolicy.ModifiedBy != null)
                    {
                        var user = _userRepository.GetUser(medicalPolicy.ModifiedBy);
                        if (user != null)
                        {
                            medicalPolicyDto.ModifiedByInfo = new ModifierInfoDto
                            {
                                Id = user.Id,
                                Email = user.Email,
                                FirtsName = user.FirtsName,
                                LastName = user.LastName,
                                Role = user.Role,
                            };
                        }
                    }

                    return Ok(new { Data = medicalPolicyDto });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Administrator,Staff")]
        public IActionResult GetMedicalPolicyForAdmin(string id)
        {
            try
            {
                var medicalPolicy = _medicalPolicyRepository.GetMedicalPolicyForAdmin(id);
                if (medicalPolicy == null)
                {
                    return NotFound(new { message = "Medical policy not found" });
                }
                else
                {
                    var medicalPolicyDto = _mapper.Map<MedicalPolicyDto>(medicalPolicy);

                    if (medicalPolicy.CreatedBy != null)
                    {
                        var user = _userRepository.GetUser(medicalPolicy.CreatedBy);

                        if (user != null)
                        {
                            medicalPolicyDto.CreatedByInfo = new ModifierInfoDto
                            {
                                Id = user.Id,
                                Email = user.Email,
                                FirtsName = user.FirtsName,
                                LastName = user.LastName,
                                Role = user.Role,
                            };
                        }
                    }

                    if (medicalPolicy.ModifiedBy != null)
                    {
                        var user = _userRepository.GetUser(medicalPolicy.ModifiedBy);

                        if (user != null)
                        {
                            medicalPolicyDto.ModifiedByInfo = new ModifierInfoDto
                            {
                                Id = user.Id,
                                Email = user.Email,
                                FirtsName = user.FirtsName,
                                LastName = user.LastName,
                                Role = user.Role,
                            };
                        }
                    }

                    return Ok(new { Data = medicalPolicyDto });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Administrator")]
        public IActionResult UpdateMedicalPolicy(
            [FromBody] MedicalPolicyUpdateDto medicalPolicyUpdateDto,
            string id
        )
        {
            try
            {
                var currentUser = GetCurrentUser();
                var result = _medicalPolicyRepository.UpdateMedicalPolicy(
                    medicalPolicyUpdateDto,
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
