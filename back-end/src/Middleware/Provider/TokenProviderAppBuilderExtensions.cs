// Copyright (c) Nate Barbettini. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace TokenProvider
{
    
    public static class TokenProviderAppBuilderExtensions
    {
        public static IApplicationBuilder UseSimpleTokenProvider(this IApplicationBuilder app, TokenProviderOptions options, TokenValidationParameters tokenValidationParameters)
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }

            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            if (tokenValidationParameters == null)
            {
                throw new ArgumentNullException(nameof(tokenValidationParameters));
            }

            return app.UseMiddleware<TokenProviderMiddleware>(Options.Create(options), tokenValidationParameters);
        }
    }
}