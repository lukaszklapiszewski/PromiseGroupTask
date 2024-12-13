using ApiClient.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ApiClient.ApiHandler.Interface
{
    public interface IApiClient
    {
        Task<Response<HttpStatusCode>> LoginAsync(string endpoint, User user);
        Task<Response<T>> GetAsync<T>(string endpoint);
        Task<Response<HttpStatusCode>> PostAsync<T>(string endpoint, T payload);
    }
}
