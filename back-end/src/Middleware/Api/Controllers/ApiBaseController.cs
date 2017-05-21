using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Middleware.Api.Filters;

namespace Middleware.Api.Controllers
{
    [CustomExceptionFilter]
    public class ApiBaseController : Controller
    {
        protected Claim GetClaims(object claimType)
        {
            try
            {
                ClaimsIdentity claimsIdentity = (ClaimsIdentity)HttpContext.User.Identity;
                Claim claim = claimsIdentity.FindFirst((string) claimType);

                return claim;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}