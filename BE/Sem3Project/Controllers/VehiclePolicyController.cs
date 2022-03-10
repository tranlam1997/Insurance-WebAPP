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
    public class VehiclePolicyController : ControllerBase
    {
        private readonly IVehiclePolicyRepository _vehiclePolicyRepository;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private IConfiguration _config;

        public VehiclePolicyController(
            IVehiclePolicyRepository vehiclePolicyRepository,
            IMapper mapper,
            IUserRepository userRepository,
            IConfiguration config
        )
        {
            _vehiclePolicyRepository = vehiclePolicyRepository;
            _mapper = mapper;
            _userRepository = userRepository;
            _config = config;
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public IActionResult CreateVehiclePolicy(
            [FromBody] VehiclePolicyCreateDto vehiclePolicyCreateDto
        )
        {
            try
            {
                var currentUser = GetCurrentUser();
                var result = _vehiclePolicyRepository.CreateVehiclePolicy(
                    vehiclePolicyCreateDto,
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

        //Option 1:
        //[Route("~/api/[controller]/non-logged")]
        //[HttpGet]
        //Option 2:
        [HttpGet("non-logged")]
        [AllowAnonymous]
        public IActionResult GetVehiclePolicies([FromQuery] PaginationFilter paginationFilter)
        {
            try
            {
                var vehiclePolicies = _vehiclePolicyRepository.GetVehiclePolicies(paginationFilter);

                var metadata = new
                {
                    vehiclePolicies.TotalCount,
                    vehiclePolicies.PageSize,
                    vehiclePolicies.CurrentPage,
                    vehiclePolicies.HasNext,
                    vehiclePolicies.HasPrevious,
                };

                Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

                var vehiclePolicyDtos = new List<VehiclePolicyDto>();
                foreach (var vehiclePolicy in vehiclePolicies)
                {
                    VehiclePolicyDto data = _mapper.Map<VehiclePolicyDto>(vehiclePolicy);

                    if (vehiclePolicy.CreatedBy != null)
                    {
                        var user = _userRepository.GetUser(vehiclePolicy.CreatedBy);
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

                    if (vehiclePolicy.ModifiedBy != null)
                    {
                        var user = _userRepository.GetUser(vehiclePolicy.ModifiedBy);
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

                    vehiclePolicyDtos.Add(data);
                }

                return Ok(new { Data = vehiclePolicyDtos, metadata = metadata });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpGet]
        [Authorize(Roles = "Administrator,Staff")]
        public IActionResult GetVehiclePoliciesForAdmin(
            [FromQuery] PaginationFilter paginationFilter,
            [FromQuery] VehiclePolicyFilter vehiclePolicyFilter
        )
        {
            try
            {
                var vehiclePolicies = _vehiclePolicyRepository.GetVehiclePoliciesForAdmin(
                    paginationFilter,
                    vehiclePolicyFilter
                );
                var metadata = new
                {
                    vehiclePolicies.TotalCount,
                    vehiclePolicies.PageSize,
                    vehiclePolicies.CurrentPage,
                    vehiclePolicies.HasNext,
                    vehiclePolicies.HasPrevious,
                };

                Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

                var vehiclePolicyDtos = new List<VehiclePolicyDto>();

                foreach (var vehiclePolicy in vehiclePolicies)
                {
                    VehiclePolicyDto data = _mapper.Map<VehiclePolicyDto>(vehiclePolicy);

                    if (vehiclePolicy.CreatedBy != null)
                    {
                        var user = _userRepository.GetUser(vehiclePolicy.CreatedBy);

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

                    if (vehiclePolicy.ModifiedBy != null)
                    {
                        var user = _userRepository.GetUser(vehiclePolicy.ModifiedBy);

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

                    vehiclePolicyDtos.Add(data);
                }

                return Ok(new { Data = vehiclePolicyDtos, metadata = metadata });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        //Option 1:
        //[Route("~/api/[controller]/{id}/non-logged")]
        //[HttpGet]
        //Option 2:
        [HttpGet("{id}/non-logged")]
        [AllowAnonymous]
        public IActionResult GetVehiclePolicy(string id)
        {
            try
            {
                var vehiclePolicy = _vehiclePolicyRepository.GetVehiclePolicy(id);

                if (vehiclePolicy == null)
                {
                    return NotFound(new { message = "Vehicle policy not found" });
                }
                else
                {
                    var vehiclePolicyDto = _mapper.Map<VehiclePolicyDto>(vehiclePolicy);

                    if (vehiclePolicy.CreatedBy != null)
                    {
                        var user = _userRepository.GetUser(vehiclePolicy.CreatedBy);

                        if (user != null)
                        {
                            vehiclePolicyDto.CreatedByInfo = new ModifierInfoDto
                            {
                                Id = user.Id,
                                Email = user.Email,
                                FirtsName = user.FirtsName,
                                LastName = user.LastName,
                                Role = user.Role,
                            };
                        }
                    }

                    if (vehiclePolicy.ModifiedBy != null)
                    {
                        var user = _userRepository.GetUser(vehiclePolicy.ModifiedBy);

                        if (user != null)
                        {
                            vehiclePolicyDto.CreatedByInfo = new ModifierInfoDto
                            {
                                Id = user.Id,
                                Email = user.Email,
                                FirtsName = user.FirtsName,
                                LastName = user.LastName,
                                Role = user.Role,
                            };
                        }
                    }

                    return Ok(new { Data = vehiclePolicyDto });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Administrator,Staff")]
        public IActionResult GetVehiclePolicyForAdmin(string id)
        {
            try
            {
                var vehiclePolicy = _vehiclePolicyRepository.GetVehiclePolicyForAdmin(id);

                if (vehiclePolicy == null)
                {
                    return NotFound(new { message = "Vehicle policy not found" });
                }
                else
                {
                    var vehiclePolicyDto = _mapper.Map<VehiclePolicyDto>(vehiclePolicy);

                    if (vehiclePolicy.CreatedBy != null)
                    {
                        var user = _userRepository.GetUser(vehiclePolicy.CreatedBy);

                        if (user != null)
                        {
                            vehiclePolicyDto.CreatedByInfo = new ModifierInfoDto
                            {
                                Id = user.Id,
                                Email = user.Email,
                                FirtsName = user.FirtsName,
                                LastName = user.LastName,
                                Role = user.Role,
                            };
                        }
                    }

                    if (vehiclePolicy.ModifiedBy != null)
                    {
                        var user = _userRepository.GetUser(vehiclePolicy.ModifiedBy);

                        if (user != null)
                        {
                            vehiclePolicyDto.CreatedByInfo = new ModifierInfoDto
                            {
                                Id = user.Id,
                                Email = user.Email,
                                FirtsName = user.FirtsName,
                                LastName = user.LastName,
                                Role = user.Role,
                            };
                        }
                    }

                    return Ok(new { Data = vehiclePolicyDto });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Administrator")]
        public IActionResult UpdateVehiclePolicy(
            [FromBody] VehiclePolicyUpdateDto vehiclePolicyUpdateDto,
            string id
        )
        {
            try
            {
                var currentUser = GetCurrentUser();
                var result = _vehiclePolicyRepository.UpdateVehiclePolicy(
                    vehiclePolicyUpdateDto,
                    id,
                    currentUser.Id
                );

                if (result == true)
                {
                    return Ok(new { message = "Update success" });
                }
                else
                {
                    return NotFound(new { message = "Vehicle policy not found" });
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
