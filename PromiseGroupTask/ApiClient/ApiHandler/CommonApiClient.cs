using ApiClient.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Net.Http.Json;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace ApiClient.ApiHandler
{
    /// <summary>
    /// Apli client class used to make requests
    /// </summary>
    public class CommonApiClient
    {
        static readonly HttpClient client = new HttpClient();

        /// <summary>
        /// Generates response from given endpoint
        /// </summary>
        /// <typeparam name="T">Type to be returned from endpoint</typeparam>
        /// <param name="endpoint">Endpoint to be used to reach data</param>
        /// <returns>Parsed data from server or null</returns>
        public static async Task<T?> Get<T>(string endpoint)
        {
            try
            {
                AddAuthorization();
                return await client.GetFromJsonAsync<T>(endpoint);
            }
            catch (HttpRequestException e)
            {
                throw new ApiException("An exception occured while processing request", e);
            }
        }

        /// <summary>
        /// Sends HTTP POST to given endpoint
        /// </summary>
        /// <typeparam name="T">Type of data in payload</typeparam>
        /// <param name="endpoint">Endpoint to be used to post data</param>
        /// <param name="payload">Data payoad</param>
        /// <returns>HttpStatusCode from request</returns>
        public static async Task<HttpStatusCode> Post<T>(string endpoint, T payload)
        {
            try
            {
                AddAuthorization();
                HttpResponseMessage httpResponseMessage = await client.PostAsJsonAsync(endpoint, payload);
                return httpResponseMessage.StatusCode;
            }
            catch (HttpRequestException e)
            {
                throw new ApiException("An exception occured while processing request", e);
            }
        }

        /// <summary>
        /// Adds authorization header to client
        /// </summary>
        private static void AddAuthorization()
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "SomeFakeAuthorizationToken");
        }
    }
}
