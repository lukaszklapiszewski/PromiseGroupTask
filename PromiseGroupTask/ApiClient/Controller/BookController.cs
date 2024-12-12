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
        private IApiClient apiClient;

        public BookController(IApiClient apiClient)
        {
            this.apiClient = apiClient;
        }

        /// <summary>
        /// Fetch books list from API
        /// </summary>
        /// <returns>List of books or null</returns>
        public async Task<List<Book>?> GetBooks()
        {
            UriBuilder uriBuilder = GetUriBuilder();
            uriBuilder.Path = "books";
            List<Book>? books = await apiClient.GetAsync<List<Book>>(uriBuilder.Uri.AbsoluteUri);

            return books != null ? books : new List<Book>();
        }

        /// <summary>
        /// Creates new book in database
        /// </summary>
        /// <param name="book">Book to be created</param>
        /// <returns>HttpStatusCode of request</returns>
        public async Task<HttpStatusCode> AddBook(Book book)
        {
            UriBuilder uriBuilder = GetUriBuilder();
            uriBuilder.Path = "books";
            return await apiClient.PostAsync(uriBuilder.Uri.AbsoluteUri, book);
        }
    }
}