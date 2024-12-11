using ApiClient.ApiHandler;
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
        /// <summary>
        /// Fetch books list from API
        /// </summary>
        /// <returns>List of books or null</returns>
        public async Task<List<Book>?> GetBooks()
        {
            UriBuilder uriBuilder = GetUriBuilder();
            uriBuilder.Path = "books";
            return await CommonApiClient.Get<List<Book>>(uriBuilder.Uri.AbsoluteUri);
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
            return await CommonApiClient.Post(uriBuilder.Uri.AbsoluteUri, book);
        }
    }
}
