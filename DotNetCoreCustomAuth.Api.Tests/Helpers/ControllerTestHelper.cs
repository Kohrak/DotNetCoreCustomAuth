using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xunit;

namespace DotNetCoreCustomAuth.Api.Tests.Helpers
{
    /// <summary>
    /// This class defines a base for testing common response codes in web apis 
    /// </summary>
    public class ControllerTestHelper
    {
        protected ITestServerFixture _server;

        public ControllerTestHelper(ITestServerFixture server)
        {
            _server = server;
        }

        public async Task GetReturns401(string queryString)
        {
            _server.DeAuthenticate();
            var response = await _server.Client.GetAsync(queryString);
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);

            //cleanup
            _server.Authenticate();

        }

        public async Task GetReturns403(string queryString)
        {
            var response = await _server.Client.GetAsync(queryString);
            Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
        }

        public async Task GetReturnsOk(string queryString)
        {
            var response = await _server.Client.GetAsync(queryString);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        public async Task GetReturnsNoContent(string queryString)
        {
            var response = await _server.Client.GetAsync(queryString);
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }

        public async Task GetReturnsListOfType<T>(string queryString)
        {
            var response = await _server.Client.GetAsync(queryString);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var content = await response.Content.ReadAsStringAsync();
            var list = JsonConvert.DeserializeObject(content, typeof(List<T>)) as List<T>;

            Assert.NotNull(list);
            Assert.NotEmpty(list);
        }

        public async Task PostReturnsOk(object requestObject, string queryString)
        {
            var response = await ExecutePost(requestObject, queryString);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        public async Task PostReturnsNoContent(object requestObject, string queryString)
        {
            var response = await ExecutePost(requestObject, queryString);
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }

        public async Task PostReturns401(object requestObject, string queryString)
        {
            _server.DeAuthenticate();
            var response = await ExecutePost(requestObject, queryString);
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);

            //cleanup
            _server.Authenticate();
        }

        public async Task PostReturnsLisOfType<T>(object requestObject, string queryString)
        {
            var response = await ExecutePost(requestObject, queryString);
            var content = await response.Content.ReadAsStringAsync();
            var list = JsonConvert.DeserializeObject(content, typeof(List<T>)) as List<T>;

            Assert.NotNull(list);
            Assert.NotEmpty(list);
        }

        public async Task<T> PostReturnsType<T>(object requestObject, string queryString)
        {
            var response = await ExecutePost(requestObject, queryString);
            var content = await response.Content.ReadAsStringAsync();

            var obj = JsonConvert.DeserializeObject(content, typeof(T));

            Assert.NotNull(obj);
            return (T)obj;
        }

        public async Task PostReturns403(object requestObject, string queryString)
        {
            var response = await ExecutePost(requestObject, queryString);
            Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
        }

        private async Task<HttpResponseMessage> ExecutePost(object requestObject, string queryString)
        {
            var json = JsonConvert.SerializeObject(requestObject);
            var j = new StringContent(json, Encoding.UTF8, "application/json");
            return await _server.Client.PostAsync(queryString, j);
        }
    }
}
