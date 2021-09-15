using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Common.Model
{
    public class SignUpRequest
    {
        public string UserName { get; set; }
        public string EmailID { get; set; }
        public string MobileNo { get; set; }
        public string Password { get; set; }
    }

    public class SignUpResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
}
