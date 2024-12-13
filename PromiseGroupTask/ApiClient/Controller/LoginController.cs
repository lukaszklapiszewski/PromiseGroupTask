using ApiClient.ApiHandler.Interface;
using ApiClient.ApiHandler;
using ApiClient.Model;
using System.Net;

namespace ApiClient.Controller
{
    /// <summary>
    /// Login controller class
    /// </summary>
    public class LoginController : BaseController
    {
        private const string Endpoint = "login";

        private IApiClient apiClient;

        public LoginController(IApiClient apiClient)
        {
            this.apiClient = apiClient;
        }

        /// <summary>
        /// Login user and fetch bearer token
        /// </summary>
        /// <returns>HttpStatusCode of login process</returns>
        public async Task<Response<HttpStatusCode>> Login(User user)
        {
            UriBuilder uriBuilder = GetUriBuilder();
            uriBuilder.Path = Endpoint;
            return await apiClient.LoginAsync(uriBuilder.Uri.AbsoluteUri, user);
        }
    }
}
