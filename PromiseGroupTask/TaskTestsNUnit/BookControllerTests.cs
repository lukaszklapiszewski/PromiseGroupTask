using ApiClient.ApiHandler.Interface;
using ApiClient.Controller;
using ApiClient.Model;
using Newtonsoft.Json;
using System.Net;

namespace TaskTestsNUnit
{
    public sealed class BookControllerTests
    {
        private BookController bookController;
        private Random random = new Random (DateTime.Now.Second);

        [SetUp]
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

            IApiClient apiClient = new MockApiClient(new MockBooksHandler(bookJson));
            bookController = new BookController(apiClient);
        }

        [Test]
        public async Task Test_GetBooks()
        {
            List<Book>? books = await bookController.GetBooks();
            Assert.That(books, Is.Not.Null);
            Assert.That(books!.Any(), Is.True);
        }

        [Test]
        public async Task Test_AddBook()
        {
            Book book = new Book()
            {
                Title = "Test title " + random.Next(),
                Bookstand = random.Next(),
                Shelf = random.Next(),
                Authors = new List<Author>()
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
            };

            HttpStatusCode httpStatusCode = await bookController.AddBook(book);
            Assert.That(httpStatusCode, Is.EqualTo(HttpStatusCode.Created));
        }
    }
}