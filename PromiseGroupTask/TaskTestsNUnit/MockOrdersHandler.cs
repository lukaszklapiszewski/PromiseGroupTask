using ApiClient.Model;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using System.Web;

namespace TaskTestsNUnit
{
    public class MockOrdersHandler(List<Order> orders) : HttpMessageHandler
    {
        private readonly List<Order> orders = orders;

        sealed protected override Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request, CancellationToken cancellationToken)
        {
            HttpResponseMessage response;

            switch (request.Method.ToString())
            {
                case "GET":
                    var query = HttpUtility.ParseQueryString(request.RequestUri.Query);
                    if (int.TryParse(query["page"], out int page) && int.TryParse(query["limit"], out int limit))
                    {
                        List<Order> pagedOrders = orders.Take(new Range(new Index((page - 1) * limit), (page - 1) * limit + limit)).ToList();

                        response = new HttpResponseMessage()
                        {
                            StatusCode = HttpStatusCode.OK,
                            Content = new StringContent(JsonConvert.SerializeObject(pagedOrders), Encoding.UTF8, "application/json"),
                        };
                    }
                    else
                    {
                        response = new HttpResponseMessage()
                        {
                            StatusCode = HttpStatusCode.BadRequest,
                        };
                    }
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