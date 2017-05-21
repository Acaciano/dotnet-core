using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.CrossCutting.Helper
{
    public class ErrorResponse
    {
        public ErrorResponse()
        {
            this.Errors = new List<CustomError>();
        }
        public List<CustomError> Errors { get; set; }        
    }

    public class SuccessResponse
    {
        public string Message { get; set; }
    }

    public class CustomError
    {
        public CustomError(string code, string message)
        {
            this.Code = code;
            this.ErrorMessage = message;
        }
        public string Code { get; set; }
        public string ErrorMessage { get; set; }
    }
}
