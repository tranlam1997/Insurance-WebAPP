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
using Sem3Project.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using PayPal.Api;

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
        private readonly IReceiptRepository _receiptRepository;
        private IConfiguration _config;
        private IMailService _mailService;

        public VehicleInsuranceController(
            IVehicleInsuranceRepository vehicleInsuranceRepository,
            IMapper mapper,
            IUserRepository userRepository,
            IVehiclePolicyRepository vehiclePolicyRepository,
            IReceiptRepository receiptRepository,
            IConfiguration config,
            IMailService mailService
        )
        {
            _vehicleInsuranceRepository = vehicleInsuranceRepository;
            _mapper = mapper;
            _userRepository = userRepository;
            _vehiclePolicyRepository = vehiclePolicyRepository;
            _receiptRepository = receiptRepository;
            _config = config;
            _mailService = mailService;
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> CreateVehicleInsurance(
            [FromBody] VehicleInsuranceCreateDto vehicleInsuranceCreateDto
        )
        {
            try
            {
                var currentUser = GetCurrentUser();
                var existVehiclePolicy = _vehiclePolicyRepository.GetVehiclePolicyForAdmin(
                    vehicleInsuranceCreateDto.VehiclePolicyId
                );
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

        [HttpGet("verify")]
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

        [HttpGet("logged")]
        [Authorize]
        public IActionResult GetInsurances([FromQuery] PaginationFilter paginationFilter)
        {
            try
            {
                var currentUser = GetCurrentUser();
                var vehicleInsurances = _vehicleInsuranceRepository.GetVehicleInsurances(
                    paginationFilter,
                    currentUser.Id
                );
                var metadata = new
                {
                    vehicleInsurances.TotalCount,
                    vehicleInsurances.PageSize,
                    vehicleInsurances.CurrentPage,
                    vehicleInsurances.HasNext,
                    vehicleInsurances.HasPrevious,
                };

                Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

                var vehicleInsuranceDtos = new List<VehicleInsuranceDto>();
                foreach (var vehicleInsurance in vehicleInsurances)
                {
                    VehicleInsuranceDto data = _mapper.Map<VehicleInsuranceDto>(vehicleInsurance);
                    VehiclePolicy vehiclePolicy = _mapper.Map<VehiclePolicy>(
                        vehicleInsurance.VehiclePolicy
                    );
                    data.VehiclePolicy = vehiclePolicy;

                    vehicleInsuranceDtos.Add(data);
                }

                return Ok(new { Data = vehicleInsuranceDtos, metadata = metadata });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpGet]
        [Authorize(Roles = "Administrator,Staff")]
        public IActionResult GetInsurancesForAdmin(
            [FromQuery] PaginationFilter paginationFilter,
            [FromQuery] VehicleInsuranceFilter vehicleInsuranceFilter
        )
        {
            try
            {
                var vehicleInsurances = _vehicleInsuranceRepository.GetVehicleInsurancesForAdmin(
                    paginationFilter,
                    vehicleInsuranceFilter
                );
                var metadata = new
                {
                    vehicleInsurances.TotalCount,
                    vehicleInsurances.PageSize,
                    vehicleInsurances.CurrentPage,
                    vehicleInsurances.HasNext,
                    vehicleInsurances.HasPrevious,
                };

                Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

                var vehicleInsuranceForAdminDtos = new List<VehicleInsuranceForAdminDto>();
                foreach (var vehicleInsurance in vehicleInsurances)
                {
                    VehicleInsuranceForAdminDto data = _mapper.Map<VehicleInsuranceForAdminDto>(
                        vehicleInsurance
                    );
                    VehiclePolicy vehiclePolicy = _mapper.Map<VehiclePolicy>(
                        vehicleInsurance.VehiclePolicy
                    );
                    data.VehiclePolicy = vehiclePolicy;

                    //Do user có liên kết tới bảng khác nên sẽ bị lỗi JSON
                    UserForAdminDto user = _mapper.Map<UserForAdminDto>(vehicleInsurance.User);
                    data.UserForAdminDto = user;

                    //Viết thế này thì các trường trống sẽ mặc định là null
                    //data.User = new User()
                    //{
                    //    Id = vehicleInsurance.User.Id,
                    //    Email = vehicleInsurance.User.Email,
                    //    FirtsName = vehicleInsurance.User.FirtsName,
                    //    LastName = vehicleInsurance.User.LastName,
                    //    PhoneNumber = vehicleInsurance.User.PhoneNumber,
                    //    Role = vehicleInsurance.User.Role,
                    //};

                    vehicleInsuranceForAdminDtos.Add(data);
                }
                return Ok(new { Data = vehicleInsuranceForAdminDtos, metadata = metadata });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpGet("{id}/logged")]
        [Authorize]
        public IActionResult GetInsurance(string id)
        {
            try
            {
                var currentUser = GetCurrentUser();
                var vehicleInsurance = _vehicleInsuranceRepository.GetVehicleInsurance(
                    id,
                    currentUser.Id
                );

                return Ok(vehicleInsurance);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpPost("{id}/pay")]
        [Authorize]
        public IActionResult Pay(string id, [FromBody] PaymentMethodDto paymentMethodDto)
        {
            try
            {
                var currentUser = GetCurrentUser();
                var vehicleInsurance = _vehicleInsuranceRepository.GetVehicleInsurance(
                    id,
                    currentUser.Id
                );

                if (vehicleInsurance == null)
                {
                    return NotFound(new { message = "Vehicle insurance not found" });
                }

                switch (paymentMethodDto.Method)
                {
                    case "paypal":
                    {
                        var accessToken = new OAuthTokenCredential(
                            _config["Paypal:clientId"],
                            _config["Paypal:secret"]
                        ).GetAccessToken();
                        var apiContext = new APIContext(accessToken);

                        var payment = Payment.Create(
                            apiContext,
                            new Payment
                            {
                                intent = "sale",
                                payer = new Payer { payment_method = "paypal" },
                                transactions = new List<Transaction>
                                {
                                    new Transaction
                                    {
                                        description = "Transaction description.",
                                        amount = new Amount
                                        {
                                            currency = "USD",
                                            total = "100.00",
                                            details = new Details
                                            {
                                                tax = "15",
                                                shipping = "10",
                                                subtotal = "75"
                                            }
                                        },
                                        item_list = new ItemList
                                        {
                                            items = new List<Item>
                                            {
                                                new Item
                                                {
                                                    name = "Item Name",
                                                    currency = "USD",
                                                    price = "15",
                                                    quantity = "5",
                                                    sku = "sku",
                                                }
                                            }
                                        }
                                    }
                                },
                                redirect_urls = new RedirectUrls
                                {
                                    return_url = "https://localhost:44312/api/VehicleInsurance/check",
                                    cancel_url = "https://localhost:44312/swagger/index.html"
                                }
                            }
                        );

                        ReceiptCreateDto receiptCreateDto = new ReceiptCreateDto()
                        {
                            MinimumPayment = Convert.ToInt32(
                                double.Parse(payment.transactions[0].amount.total)
                            ),
                            Balance = Convert.ToInt32(
                                double.Parse(payment.transactions[0].amount.total)
                            ),
                            PaymentType = "paypal",
                            UserId = currentUser.Id,
                            InsuranceId = id.ToLower(),
                            InsuranceType = Enum.InsuranceType.Vehicle,
                            PaymentId = payment.id,
                        };

                        _receiptRepository.CreateReceipt(receiptCreateDto);

                        return Ok(new { accessToken, payment, receiptCreateDto });
                    }
                    default:
                    {
                        return NotFound(new { message = "Not found supplied payment method" });
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpGet("check")]
        public IActionResult Check([FromQuery] string paymentId, [FromQuery] string payerId)
        {
            try
            {
                var accessToken = new OAuthTokenCredential(
                    _config["Paypal:clientId"],
                    _config["Paypal:secret"]
                ).GetAccessToken();
                var apiContext = new APIContext(accessToken);
                var paymentExecution = new PaymentExecution()
                {
                    payer_id = payerId,
                };
                Payment payment = new Payment() { id = paymentId };
                payment.Execute(apiContext, paymentExecution);
                return Ok(new { message = "Successful payment" });
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
