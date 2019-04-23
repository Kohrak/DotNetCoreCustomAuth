using System.Net.Http;

namespace DotNetCoreCustomAuth.Api.Tests.Helpers
{
    public interface ITestServerFixture
    {
        HttpClient Client { get; }

        void Authenticate();
        void DeAuthenticate();
    }
}
