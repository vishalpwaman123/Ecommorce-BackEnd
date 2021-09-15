using Ecommerce.Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Service.Interface
{
    public interface IEcommerceServiceSL
    {
        Task<SignUpResponse> SignUp(SignUpRequest request);
        Task<LoginResponse> Login(LoginRequest request);
    }
}
