using System;
using System.Collections.Generic;
using System.Text;
using DotNetCoreCustomAuth.Api.Tests.Helpers;
using Xunit;

namespace DotNetCoreCustomAuth.Api.Tests.IntegrationTests
{
    public class ValuesControllerTests : ControllerTestBed
    {
        public string QueryString = "api/Values";
        public ValuesControllerTests(TestServerFixture server) : base(server)
        {
        }

        [Fact]
        public async void Get_WhenNoTokenGiven_Returns401()
        {
            await GetReturns401(QueryString);
        }

        [Fact]
        public async void Get_WhenKeyGiven_ReturnsOk()
        {
            await GetReturnsOk(QueryString);
        }
    }
}
