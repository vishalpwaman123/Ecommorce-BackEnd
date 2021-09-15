using Ecommerce.Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Repository.Interface
{
    public interface IEcommerceServiceRL
    {
        Task<SignUpResponse> SignUp(SignUpRequest request);
        Task<LoginResponse> Login(LoginRequest request);
    }
}
