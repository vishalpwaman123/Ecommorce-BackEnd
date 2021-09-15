using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Common.Model
{
    public class LoginRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class LoginResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public Login login { get; set; }
    }

    public class Login
    {

        public int UserID { get; set; }
        public string EmailID { get; set; }
        public string UserName { get; set; }

    }
}
