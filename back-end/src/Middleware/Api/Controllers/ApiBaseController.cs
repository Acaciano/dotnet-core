using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace Middleware.Api.Controllers
{
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