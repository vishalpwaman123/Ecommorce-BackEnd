using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Common.Exceptions
{
    public class CustomExceptions : Exception
    {
        private ExceptionType _exceptionType;

        public enum ExceptionType{
            NULL_EXCEPTION,
            EMPTY_STRING_EXCEPTION
        }


        public CustomExceptions(CustomExceptions.ExceptionType exceptionType, string message) : base(message)
        {
            _exceptionType = exceptionType;
        }
    }
}
