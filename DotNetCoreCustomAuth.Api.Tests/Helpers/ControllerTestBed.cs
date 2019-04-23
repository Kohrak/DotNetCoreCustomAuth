using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace DotNetCoreCustomAuth.Api.Tests.Helpers
{
    //This class is needed to make the dependency injection of the test fixture using xUnit and isolate the controller test helper
    //Like this, the controller test helper can be use across different projects with different implementations of the test server fixture
    public class ControllerTestBed : ControllerTestHelper, IClassFixture<TestServerFixture>
    {
        public ControllerTestBed(TestServerFixture server) : base(server)
        {
        }
    }
}
