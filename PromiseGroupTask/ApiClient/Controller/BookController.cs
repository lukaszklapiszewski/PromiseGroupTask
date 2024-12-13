using ApiClient.ApiHandler;
using ApiClient.ApiHandler.Interface;
using ApiClient.Config;
using ApiClient.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ApiClient.Controller
{
    /// <summary>
    /// Book controller class.
    /// </summary>
    public class BookController : BaseController
    {
        private const string Endpoint = "books";

        private IApiClient apiClient;        

        public BookController(IApiClient apiClient)
        {
            this.apiClient = apiClient;
        }

        /// <summary>
        /// Fetch books list from API
        /// </summary>
        /// <returns>Api response with List of books or with error message if something went wrong</returns>
        public async Task<Response<List<Book>>> GetBooks()
        {
            UriBuilder uriBuilder = GetUriBuilder();
            uriBuilder.Path = Endpoint;
            return await apiClient.GetAsync<List<Book>>(uriBuilder.Uri.AbsoluteUri);
        }

        /// <summary>
        /// Creates new book in database
        /// </summary>
        /// <param name="book">Book to be created</param>
        /// <returns>Api response with HttpStatusCode or with error message if something went wrong</returns>
        public async Task<Response<HttpStatusCode>> AddBook(Book book)
        {
            UriBuilder uriBuilder = GetUriBuilder();
            uriBuilder.Path = Endpoint;
            return await apiClient.PostAsync(uriBuilder.Uri.AbsoluteUri, book);
        }
    }
}