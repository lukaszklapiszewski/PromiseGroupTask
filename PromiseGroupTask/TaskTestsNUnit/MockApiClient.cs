using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using ApiClient.ApiHandler.Interface;

namespace TaskTestsNUnit
{
    public class MockApiClient : IApiClient
    {
        private readonly HttpClient httpClient;

        public MockApiClient(HttpMessageHandler handler)
        {
            httpClient = new HttpClient(handler);
        }

        public async Task<T?> GetAsync<T>(string endpoint)
        {
            T? deserialized = await httpClient.GetFromJsonAsync<T>(endpoint);

            return deserialized;
        }

        public async Task<HttpStatusCode> PostAsync<T>(string endpoint, T payload)
        {
            HttpResponseMessage httpResponseMessage = await httpClient.PostAsJsonAsync(endpoint, payload);
            return httpResponseMessage.StatusCode;
        }
    }
}
