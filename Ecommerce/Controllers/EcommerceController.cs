using Ecommerce.Common.Model;
using Ecommerce.Service.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EcommerceController : ControllerBase
    {
        public readonly ILogger<EcommerceController> _logger;
        public readonly IEcommerceDataAccessSL _ecommerceDataAccessSL;
        public readonly IEcommerceServiceSL _ecommerceServiceSL;

        public EcommerceController(ILogger<EcommerceController> logger, IEcommerceDataAccessSL ecommerceDataAccessSL, IEcommerceServiceSL ecommerceServiceSL)
        {
            _logger = logger;
            _ecommerceDataAccessSL = ecommerceDataAccessSL;
            _ecommerceServiceSL = ecommerceServiceSL;
        }

        [HttpPost]
        [Route("SignUp")]
        public async Task<IActionResult> SignUp(SignUpRequest request)
        {
            SignUpResponse response = null;
            try
            {
                response = await _ecommerceServiceSL.SignUp(request);
            }catch(Exception ex)
            {
                _logger.LogError($" Login Error => {ex}");
            }
            return Ok(response);
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            LoginResponse response = null;
            try
            {
                response = await _ecommerceServiceSL.Login(request);
            }catch(Exception ex)
            {
                _logger.LogError($" Login Error => {ex}");
            }
            return Ok(response);
        }
    }
}
