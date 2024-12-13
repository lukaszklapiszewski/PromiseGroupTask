using ApiClient.ApiHandler;
using ApiClient.ApiHandler.Interface;
using ApiClient.Controller;
using ApiClient.Model;
using System.Net;

IApiClient apiClient = new CommonApiClient();

LoginController loginController = new LoginController(apiClient);

User user = new User()
{
    Login = "testlogin@test.eu",
    Password = "testPassword"
};

Response<HttpStatusCode> loginResponse = await loginController.Login(user);

if(loginResponse.Result != HttpStatusCode.OK)
{
    Console.WriteLine(loginResponse.ErrorMessage);
    Console.ReadKey();
    return;
}

BookController bookController = new BookController(apiClient);

Response<List<Book>> booksResponse = await bookController.GetBooks();

if (string.IsNullOrEmpty(booksResponse.ErrorMessage))
{

    Console.WriteLine(string.Format("Boooks count: {0}", booksResponse.Result.Count));
    if (booksResponse != null)
    {
        foreach (Book bookItem in booksResponse.Result)
        {
            Console.WriteLine(string.Format("Tite: {0} Price {1} Bookstand {2} Shelf {3}", bookItem.Title, Math.Round(bookItem.Price, 2), bookItem.Bookstand, bookItem.Shelf));
        }
    }
}
else
{
    Console.WriteLine(booksResponse.ErrorMessage);
}

Random random = new Random();

Book book = new Book
{
    Authors = new List<Author> {
        new Author()
        {
            FirstName = "John",
            LastName = "Doe"
        }
    },
    Bookstand = random.Next(20),
    Id = random.Next(1000),
    Price = (float)random.NextDouble(),//casting cuts precision here
    Shelf = random.Next(20),
    Title = string.Format("Title {0}", random.Next(20))
};

Response<HttpStatusCode> addBookResponse = await bookController.AddBook(book);

if (string.IsNullOrEmpty(booksResponse.ErrorMessage))
{
    if (addBookResponse.Result == HttpStatusCode.Created)
    {
        Console.WriteLine("Book created!");
    }
    else
    {
        Console.WriteLine("Error while creating book");
    }
}
else
{
    Console.WriteLine(booksResponse.ErrorMessage);
}

OrderController orderController = new OrderController(apiClient);
Response<List<Order>> ordersResponse = await orderController.GetOrders(3, 10);

if (string.IsNullOrEmpty(booksResponse.ErrorMessage))
{
    Console.WriteLine(string.Format("Orders count: {0}", ordersResponse.Result.Count));

    if (ordersResponse != null)
    {
        foreach (Order order in ordersResponse.Result)
        {
            Console.WriteLine(string.Format("Order: {0} Order lines count {1}", order.OrderId, order.OrderLines.Count));
        }
    }
}
else
{
    Console.WriteLine(booksResponse.ErrorMessage);
}

Console.ReadKey();