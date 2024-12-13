using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TaskTests
{
    public class MockJsonHandler(string json) : HttpMessageHandler
    {
        private readonly string json = json;

        sealed protected override Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var response = new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(json, Encoding.UTF8, "application/json"),
            };

            return Task.FromResult(response);
        }
    }
}
