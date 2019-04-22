using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;

namespace Auth
{
    public static class CustomTokenAuth
    {
        public static string CustomTokenAuthHeader = "TokenAuth";

        public static AuthenticationBuilder AddCustomTokenAuth(this AuthenticationBuilder builder)
        {
            return builder.AddScheme<TokenAuthHandlerOptions, TokenAuthHandler>("CustomTokenAuth", null);
        }

        public static void AddTokenAuthServices(this IServiceCollection services)
        {
            services.AddTransient<ITokenService, SampleTokenService>();
        }
    }
}
