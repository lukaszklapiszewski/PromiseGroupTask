using ApiClient.ApiHandler;
using ApiClient.ApiHandler.Interface;
using ApiClient.Model;

namespace ApiClient.Controller
{
    /// <summary>
    /// Order controller class
    /// </summary>
    public class OrderController : BaseController
    {
        private const string Endpoint = "orders";

        private IApiClient apiClient;

        public OrderController(IApiClient apiClient)
        {
            this.apiClient = apiClient;
        }

        /// <summary>
        /// Fetch books list from API
        /// </summary>
        /// <param name="pageNumber">Page number for data amount reduction</param>
        /// <param name="itemsPerPage">Defines maximum amount of items per page</param>
        /// <returns>Api response with List of orders or with error message if something went wrong</returns>
        public async Task<Response<List<Order>>> GetOrders(int pageNumber, int itemsPerPage)
        {
            UriBuilder uriBuilder = GetUriBuilder();
            uriBuilder.Path = Endpoint;
            uriBuilder.Query = string.Format("page={0}&limit={1}", pageNumber, itemsPerPage);

            return await apiClient.GetAsync<List<Order>>(uriBuilder.Uri.AbsoluteUri);
        }
    }
}