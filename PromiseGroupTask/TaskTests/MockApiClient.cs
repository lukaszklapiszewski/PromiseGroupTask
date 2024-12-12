using ApiClient.ApiHandler.Interface;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TaskTests
{
    public class MockApiClient : IApiClient
    {
        private readonly HttpClient httpClient;

        public MockApiClient(MockJsonHandler jsonHandler)
        {
            httpClient = new HttpClient(jsonHandler);
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
