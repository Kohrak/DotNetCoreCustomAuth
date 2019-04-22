using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;

namespace Auth.Tests
{
    public class TokenAuthHandlerTests
    {

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public async void NoKeyGiven_AuthFails(string token)
        {

            var authResult = await AuthenticateWithToken(token, GetDefaultTokenServiceMock());
            Assert.NotNull(authResult);
            Assert.False(authResult.Succeeded);
            Assert.NotNull(authResult.Failure);
        }

        [Fact]
        public async void GivenInvalidKey_AuthFails()
        {
            var authResult = await AuthenticateWithToken("invalid", GetDefaultTokenServiceMock());
            Assert.NotNull(authResult);
            Assert.False(authResult.Succeeded);
            Assert.NotNull(authResult.Failure);
        }

        [Fact]
        public async void GivenValidKey_AuthenticationPasses()
        {
            var authResult = await AuthenticateWithToken("validKey", GetDefaultTokenServiceMock());
            Assert.True(authResult.Succeeded);
            Assert.True(authResult.Principal.Identity.IsAuthenticated);
        }

        private async Task<AuthenticateResult> AuthenticateWithToken(string token, ITokenService tokenService)
        {
            var context = new DefaultHttpContext();
            if (token != null)
            {
                context.Request.Headers[CustomTokenAuth.CustomTokenAuthHeader] = token;
            }

            var handler = await InitHandlerAsync(context, tokenService);
            return await handler.AuthenticateAsync();
        }

        private TokenAuthHandler GetHandler(ITokenService tokenService)
        {
            var options = new Mock<IOptionsMonitor<TokenAuthHandlerOptions>>();
            var loggerFactory = new Mock<ILoggerFactory>();
            var logger = new Mock<ILogger>();
            var encoder = new Mock<UrlEncoder>();
            var systemClock = new Mock<ISystemClock>();

            loggerFactory.Setup(x => x.CreateLogger(It.IsAny<string>())).Returns(logger.Object);

            return new TokenAuthHandler(options.Object, loggerFactory.Object, encoder.Object, systemClock.Object, tokenService);
        }

        private async Task<TokenAuthHandler> InitHandlerAsync(HttpContext context, ITokenService tokenService)
        {
            var handler = GetHandler(tokenService);
            await handler.InitializeAsync(
                new AuthenticationScheme("TokenAuthentication", "TokenAuthentication", handler.GetType()), context);
            return handler;
        }

        private ITokenService GetDefaultTokenServiceMock()
        {
            var moq = new Mock<ITokenService>();
            moq.Setup(x => x.IsTokenValid(It.Is<string>(n => n == "validKey"))).Returns(true);

            moq.Setup(x => x.IsTokenValid(It.Is<string>(n => n == null || n != "validKey"))).Returns(false);

            return moq.Object;
        }
    }
}
