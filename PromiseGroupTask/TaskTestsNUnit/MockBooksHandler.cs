using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TaskTestsNUnit
{
    public class MockBooksHandler(string json) : HttpMessageHandler
    {
        private readonly string json = json;

        sealed protected override Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request, CancellationToken cancellationToken)
        {
            HttpResponseMessage response;

            switch (request.Method.ToString())
            {
                case "GET":
                    response = new HttpResponseMessage()
                    {
                        StatusCode = HttpStatusCode.OK,
                        Content = new StringContent(json, Encoding.UTF8, "application/json"),
                    };
                    break;

                case "POST":
                    response = new HttpResponseMessage()
                    {
                        StatusCode = HttpStatusCode.Created,
                    };
                    break;
                default:
                    response = new HttpResponseMessage()
                    {
                        StatusCode = HttpStatusCode.NotFound,
                    };
                    break;
            }

            return Task.FromResult(response);
        }
    }
}
