using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Sem3Project.Models;
using Sem3Project.Models.Dtos;
using Sem3Project.Repositories;
using Sem3Project.Repositories.IRepository;
using Sem3Project.Services;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Sem3Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleInsuranceController : ControllerBase
    {
        private readonly IVehicleInsuranceRepository _vehicleInsuranceRepository;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IVehiclePolicyRepository _vehiclePolicyRepository;
        private IConfiguration _config;
        private IMailService _mailService;

        public VehicleInsuranceController(
            IVehicleInsuranceRepository vehicleInsuranceRepository,
            IMapper mapper,
            IUserRepository userRepository,
            IVehiclePolicyRepository vehiclePolicyRepository,
            IConfiguration config,
            IMailService mailService
        ) {
            _vehicleInsuranceRepository = vehicleInsuranceRepository;
            _mapper = mapper;
            _userRepository = userRepository;
            _vehiclePolicyRepository = vehiclePolicyRepository;
            _config = config;
            _mailService = mailService;
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> CreateVehicleInsurance([FromBody] VehicleInsuranceCreateDto vehicleInsuranceCreateDto) {
            try
            {
                var currentUser = GetCurrentUser();
                var existVehiclePolicy = _vehiclePolicyRepository.GetVehiclePolicyForAdmin(vehicleInsuranceCreateDto.VehiclePolicyId);
                var existUser = _userRepository.GetUser(vehicleInsuranceCreateDto.UserId);

                if (existVehiclePolicy == null)
                {
                    return NotFound(new { message = "Not found vehicle policy" });
                }

                if (existVehiclePolicy.IsReleased == false)
                {
                    return BadRequest(new { message = "Vehicle policy is not released" });
                }

                if (existUser == null)
                {
                    return NotFound(new { message = "Not found user" });
                }

                var result = _vehicleInsuranceRepository.CreateVehicleInsurance(
                    vehicleInsuranceCreateDto,
                    existUser,
                    existVehiclePolicy,
                    currentUser.Id
                );

                var data = new
                {
                    plateNumber = result.PlateNumber,
                    engineNumber = result.EngineNumber,
                    chassisNumber = result.ChassisNumber,
                    type = result.Type,
                    effectiveDate = result.EffectiveDate.ToString("dd/mm/yyyy"),
                    expireDate = result.ExpireDate.ToString("dd/mm/yyyy"),
                    vehiclePolicy = vehicleInsuranceCreateDto.VehiclePolicyId,
                    url = _config["Jwt:Issuer"] + "?token=" + result.Token
                };

                await _mailService.SendMailAsyncWithTemplate(
                    existUser.Email,
                    data,
                    _config["SendGridTemplate:VerifyInsurance"]
                );

                return Ok(new { message = "Create vehicle insurance success" });
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

        [HttpGet]
        [AllowAnonymous]
        public IActionResult VerifyInsurance([FromQuery] string token)
        {
            var result = _vehicleInsuranceRepository.VerifyInsurance(token);
            
            if (result == false)
            {
                return BadRequest(new { message = "Invalid token" });
            }
            else
            {
                return Ok(new { message = "Verify insurance success" });
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
