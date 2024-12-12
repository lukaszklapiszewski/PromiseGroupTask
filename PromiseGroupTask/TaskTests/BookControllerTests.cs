using ApiClient.ApiHandler.Interface;
using ApiClient.Controller;
using ApiClient.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace TaskTests
{
    public sealed class BookControllerTests
    {
        private BookController bookController;

        public void Setup()
        {
            List<Book> books = new List<Book>()
            { 
                new Book()
                {
                    Title = "Test title",
                    Bookstand = 1,
                    Shelf = 2,
                    Authors = new List<Author> ()
                    {
                        new Author()
                        {
                            FirstName = "John",
                            LastName = "Doe"
                        }
                    }
                },
                new Book()
                {
                    Title = "Test title 2",
                    Bookstand = 2,
                    Shelf = 3,
                    Authors = new List<Author> ()
                    {
                        new Author()
                        {
                            FirstName = "John",
                            LastName = "Doe"
                        },
                        new Author()
                        {
                            FirstName = "Julia",
                            LastName = "Doe"
                        }
                    }
                }
            };

            string bookJson = JsonConvert.SerializeObject(books);

            IApiClient apiClient = new MockApiClient(new MockJsonHandler(bookJson));
            bookController = new BookController(apiClient);
        }

        [TestMethod]
        public async Task Test_GetBooks()
        {
            List<Book>? books = await bookController.GetBooks();
          //  NUnit.Framework.Assert.That(books, Is.Not.Null);
           // NUnit.Framework.Assert.That(books!.Any(), Is.True);
        }
    }
}
