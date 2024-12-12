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
        Task<T?> GetAsync<T>(string endpoint);
        Task<HttpStatusCode> PostAsync<T>(string endpoint, T payload);
    }
}
