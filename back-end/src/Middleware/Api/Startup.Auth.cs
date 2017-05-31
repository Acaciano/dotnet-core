using System;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Application.Interface.Application;
using Domain.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.IdentityModel.Tokens;
using Middleware.Api.TokenProvider;

namespace Api
{
    public partial class Startup
    {
        private static readonly string secretKey = "d71a5f83-8132-4c50-acd0-96ed6ebbf61d";

        private void ConfigureAuth(IApplicationBuilder app)
        {
            var signingKey = new SymmetricSecurityKey(ASCIIEncoding.ASCII.GetBytes(secretKey));

            var tokenValidationParameters = new TokenValidationParameters
            {
                // The signing key must match!
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = signingKey,

                // Validate the JWT Issuer (iss) claim
                ValidateIssuer = true,
                ValidIssuer = "Issuer",

                // Validate the JWT Audience (aud) claim
                ValidateAudience = true,
                ValidAudience = "Audience",

                // Validate the token expiry
                ValidateLifetime = true,
                
                // If you want to allow a certain amount of clock drift, set that here:
                ClockSkew = TimeSpan.Zero
            };

            app.UseSimpleTokenProvider(new TokenProviderOptions
            {
                Path = "/token",
                RefreshPath = "/refresh-token",
                Audience = "Audience",
                Issuer = "Issuer",
                SigningCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256),
                IdentityResolver = GetIdentity
            }, tokenValidationParameters);

            app.UseJwtBearerAuthentication(new JwtBearerOptions
            {
                AutomaticAuthenticate = true,
                AutomaticChallenge = true,
                TokenValidationParameters = tokenValidationParameters
            });

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AutomaticAuthenticate = true,
                AutomaticChallenge = true,
                AuthenticationScheme = "Cookie",
                CookieName = "access_token",
                TicketDataFormat = new CustomJwtDataFormat(
                    SecurityAlgorithms.HmacSha256,
                    tokenValidationParameters)
            });
        }

        private Task<User> GetIdentity(IUserApplication userApplication, string username, string password)
        {
            User user = userApplication.Authenticate(username, password);

            if (user == null)
                return Task.FromResult<User>(null);

            return Task.FromResult(user);
        }
    }
}
