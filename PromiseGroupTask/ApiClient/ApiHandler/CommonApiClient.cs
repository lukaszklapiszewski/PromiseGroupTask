using ApiClient.Exceptions;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using ApiClient.ApiHandler.Interface;
using ApiClient.Model;

namespace ApiClient.ApiHandler
{
    /// <summary>
    /// Api client class used to make requests
    /// </summary>
    public class CommonApiClient : IApiClient
    {
        private static readonly HttpClient client = new HttpClient();
        private LoginResponse loginResponse;

        /// <summary>
        /// Generates async response from given endpoint
        /// </summary>
        /// <typeparam name="T">Type to be returned from endpoint</typeparam>
        /// <param name="endpoint">Endpoint to be used to reach data</param>
        /// <returns>Response with Parsed data from server with error message</returns>
        public async Task<Response<T>> GetAsync<T>(string endpoint)
        {
            try
            {
                AddAuthorization();
                T? result = await client.GetFromJsonAsync<T>(endpoint);
                if(result != null)
                {
                    return new Response<T>(result);
                }
                else
                {
                    throw new ApiException("Error, empty response returned");
                }
            }
            catch (ApiException ex)
            {
                return new Response<T>(ex.Message);
            }
            catch (HttpRequestException)
            {
                return new Response<T>("An HTTP exception occured while processing request");
            }
            catch (Exception)
            {
                return new Response<T>("General error occured while processing request");
            }
        }

        /// <summary>
        /// Sends async HTTP POST to given endpoint
        /// </summary>
        /// <typeparam name="T">Type of data in payload</typeparam>
        /// <param name="endpoint">Endpoint to be used to post data</param>
        /// <param name="payload">Data payoad</param>
        /// <returns>Response with HttpStatusCode from request or with error message</returns>
        public async Task<Response<HttpStatusCode>> PostAsync<T>(string endpoint, T payload)
        {
            try
            {
                AddAuthorization();
                HttpResponseMessage httpResponseMessage = await client.PostAsJsonAsync(endpoint, payload);
                return new Response<HttpStatusCode>(httpResponseMessage.StatusCode);
            }
            catch(ApiException ex)
            {
                return new Response<HttpStatusCode>(ex.Message);
            }
            catch (HttpRequestException)
            {
                return new Response<HttpStatusCode>("An exception occured while processing request");
            }
        }

        /// <summary>
        /// Adds authorization header to client
        /// </summary>
        private void AddAuthorization()
        {
            if (loginResponse == null || string.IsNullOrEmpty(loginResponse.BearerToken))
            {
                throw new ApiException("Not Authorized");
            }

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", loginResponse.BearerToken);
        }

        /// <summary>
        /// Fake login function
        /// </summary>
        /// <param name="endpoint">Login endpoint</param>
        /// <param name="user">User to be logged in</param>
        /// <returns>Response with HttpStatusCode from request or with error message</returns>
        public async Task<Response<HttpStatusCode>> LoginAsync(string endpoint, User user)
        {
            loginResponse = new LoginResponse()
            {
                BearerToken = "FakeToken"
            };

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
