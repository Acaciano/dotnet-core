using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Infrastructure.CrossCutting.Helper;

namespace Middleware.Api.Filters
{
    public class CustomExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            var statusCode = (int)HttpStatusCode.InternalServerError;
            var exception = context.Exception;
            var error = new ErrorResponse();
            statusCode = GetErrorStatusCode(statusCode, exception, error);
            context.HttpContext.Response.StatusCode = statusCode;
            context.Result = new JsonResult(error);
        }

        private static int GetErrorStatusCode(int statusCode, Exception exception, ErrorResponse error)
        {
            if (exception is BussinessException)
            {
                var ex = (BussinessException)exception;
                statusCode = (int)HttpStatusCode.BadRequest;
                error.Errors.Add(new CustomError(ex.Code, ex.Message));
            }
            else if (exception is AggregateException)
            {
                foreach (var ex in ((AggregateException)exception).InnerExceptions)                
                    GetErrorStatusCode(statusCode, ex, error);                
            }
            else
            {
                statusCode = (int)HttpStatusCode.InternalServerError;
                error.Errors.Add(new CustomError("500", exception.Message));
            }

            return statusCode;
        }
    }
}
