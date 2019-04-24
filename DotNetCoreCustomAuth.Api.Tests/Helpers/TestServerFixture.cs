using System.Net.Http;
using Auth;
using Auth.AuthScheme;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;

namespace DotNetCoreCustomAuth.Api.Tests.Helpers
{
    /// <summary>
    /// Defines a test server based on the configuration of the application under test
    /// </summary>
    public class TestServerFixture : ITestServerFixture
    {
        public HttpClient Client { get; }
        private readonly TestServer _testServer;

        public TestServerFixture()
        {
            var builder = new WebHostBuilder().UseEnvironment("Development").UseStartup<Startup>();
            _testServer = new TestServer(builder);
            Client = _testServer.CreateClient();
            Authenticate();
        }

        public void Authenticate()
        {
            Client.DefaultRequestHeaders.Add(CustomTokenAuth.CustomTokenAuthHeader, "rxZyPugoPIz29hHy");
        }

        public void DeAuthenticate()
        {
            Client.DefaultRequestHeaders.Remove(CustomTokenAuth.CustomTokenAuthHeader);
        }

        public void Dispose()
        {
            Client.Dispose();
            _testServer.Dispose();
        }
    }
}
