using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Auth.TokenService;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Auth.AuthScheme
{
    public class TokenAuthHandler : AuthenticationHandler<TokenAuthHandlerOptions>
    {
        private readonly ITokenService _tokenService;
        public TokenAuthHandler(
            IOptionsMonitor<TokenAuthHandlerOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            ITokenService tokenService)
            : base(options, logger, encoder, clock)
        {
            _tokenService = tokenService;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            var token = GetTokenFromHeaders(CustomTokenAuth.CustomTokenAuthHeader);
            var validationResult = _tokenService.IsTokenValid(token);
            if (validationResult)
            {
                var ticket = GetAuthTicket(null);
                return await Task.FromResult(AuthenticateResult.Success(ticket));
            }
            return await Task.FromResult(AuthenticateResult.Fail("Invalid Token"));
        }

        private string GetTokenFromHeaders(string tokenName)
        {
            return Request.Headers[tokenName];
        }

        private AuthenticationTicket GetAuthTicket(List<Claim> claims)
        {
            var claimList = claims != null && claims.Any() ? claims : new List<Claim>();
            var identity = new ClaimsIdentity(claimList, Scheme.Name);
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);
            return ticket;
        }
    }

    public class TokenAuthHandlerOptions : AuthenticationSchemeOptions
    {
        //No need for custom options at the moment
    }
}
