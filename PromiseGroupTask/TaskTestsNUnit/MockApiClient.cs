using System.Net.Http.Json;
using System.Net;
using ApiClient.ApiHandler.Interface;
using ApiClient.ApiHandler;
using ApiClient.Model;

namespace TaskTestsNUnit
{
    public class MockApiClient : IApiClient
    {
        private readonly HttpClient httpClient;
        private LoginResponse loginResponse;

        public MockApiClient(HttpMessageHandler handler)
        {
            httpClient = new HttpClient(handler);
        }

        public async Task<Response<T>> GetAsync<T>(string endpoint)
        {
            T? deserialized = await httpClient.GetFromJsonAsync<T>(endpoint);

            if (deserialized != null)
            {
                return new Response<T>(deserialized);
            }
            else
            {
                return new Response<T>("Error");
            }
        }

        public async Task<Response<HttpStatusCode>> PostAsync<T>(string endpoint, T payload)
        {
            HttpResponseMessage httpResponseMessage = await httpClient.PostAsJsonAsync(endpoint, payload);
            return new Response<HttpStatusCode>(httpResponseMessage.StatusCode);
        }

        /// <summary>
        /// Fake login function
        /// </summary>
        /// <param name="endpoint">Login endpoint</param>
        /// <param name="user">User to be logged in</param>
        /// <returns>Response with HttpStatusCode from request or with error message</returns>
        public async Task<Response<HttpStatusCode>> LoginAsync(string endpoint, User user)
        {
            if (string.IsNullOrEmpty(user.Password) || string.IsNullOrEmpty(user.Login))
            {
                return await Task.FromResult(new Response<HttpStatusCode>(HttpStatusCode.Unauthorized, "Insert login credentials"));
            }
            else if (user.Password != "testPassword" || user.Login != "testlogin@test.eu")
            {
                return await Task.FromResult(new Response<HttpStatusCode>(HttpStatusCode.Unauthorized, "Bad login credentials"));
            }
            else
            {
                return await Task.FromResult(new Response<HttpStatusCode>(HttpStatusCode.OK));
            }
        }
    }
}
