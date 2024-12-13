using ApiClient.Config;
using ApiClient.Controller.Interface;

namespace ApiClient.Controller
{
    /// <summary>
    /// Base controller class
    /// </summary>
    public class BaseController
    {
        /// <summary>
        /// Get preconfigured UriBuilder
        /// </summary>
        /// <returns>UriBuilder to use with HTTP methods</returns>
        protected UriBuilder GetUriBuilder()
        {
            UriBuilder uriBuilder = new UriBuilder();
            uriBuilder.Scheme = "https";
            uriBuilder.Host = Endpoint.ApiEndpoint;

            return uriBuilder;
        }
    }
}
