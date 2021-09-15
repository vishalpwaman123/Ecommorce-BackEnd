using Ecommerce.Common.Model;
using Ecommerce.Repository.Interface;
using Ecommerce.Service.Interface;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Ecommerce.Service.Services
{
    public class EcommerceServiceSL : IEcommerceServiceSL
    {
        public readonly ILogger<EcommerceServiceSL> _logger;
        public readonly IEcommerceServiceRL _ecommerceServiceRL;
        private readonly string strRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
         @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
         @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
        
        private readonly string MobileNumber = @"^[0-9]{10}$";

        public EcommerceServiceSL(ILogger<EcommerceServiceSL> logger, IEcommerceServiceRL ecommerceServiceRL)
        {
            _ecommerceServiceRL = ecommerceServiceRL;
            _logger = logger;
        }

        public async Task<SignUpResponse> SignUp(SignUpRequest request)
        {

            SignUpResponse response = new SignUpResponse();
            try
            {
                //Check Email Validation
                Regex EmailIDRegrex = new Regex(strRegex);
                if (!(EmailIDRegrex.IsMatch(request.EmailID)))
                {
                    response.IsSuccess = false;
                    response.Message = "Invalid Email Form.";
                    return response;
                }

                //Check Mobile Number Validation
                Regex MobileNumberRegrex = new Regex(MobileNumber);
                if (!(MobileNumberRegrex.IsMatch(request.MobileNo)){
                    response.IsSuccess = false;
                    response.Message = "Invalid Mobile Number Form.";
                    return response;
                }
                
                response = await _ecommerceServiceRL.SignUp(request);
            }catch(Exception ex)
            {
                _logger.LogError($"Sign Up Service Error => {ex}");
            }
            return response;
        }

        public async Task<LoginResponse> Login(LoginRequest request)
        {
            LoginResponse response = null;
            try
            {
                
                response = await _ecommerceServiceRL.Login(request);

            }catch(Exception ex)
            {
                _logger.LogError($" Login Service Error => {ex}");
            }
            return response;

        }

    }
}
