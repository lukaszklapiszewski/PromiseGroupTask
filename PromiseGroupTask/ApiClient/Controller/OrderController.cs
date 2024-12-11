using ApiClient.ApiHandler;
using ApiClient.Config;
using ApiClient.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiClient.Controller
{
    /// <summary>
    /// Order controller class
    /// </summary>
    public class OrderController : BaseController
    {
        /// <summary>
        /// Fetch books list from API
        /// </summary>
        /// <param name="pageNumber">Page number for data amount reduction</param>
        /// <param name="itemsPerPage">Defines maximum amount of items per page</param>
        /// <returns>List of orders or null</returns>
        public async Task<List<Order>?> GetOrders(int pageNumber, int itemsPerPage)
        {
            UriBuilder uriBuilder = GetUriBuilder();
            uriBuilder.Path = "orders";
            uriBuilder.Query = string.Format("pageNumber={0}&itemsPerPage={1}", pageNumber, itemsPerPage);

            return await CommonApiClient.Get<List<Order>>(uriBuilder.Uri.AbsoluteUri);
        }
    }
}