// See https://aka.ms/new-console-template for more information
using ApiClient.ApiHandler;
using ApiClient.ApiHandler.Interface;
using ApiClient.Controller;
using ApiClient.Exceptions;
using ApiClient.Model;
using System.Net;

IApiClient apiClient = new CommonApiClient();

BookController bookController = new BookController(apiClient);
try
{
    List<Book>? books = await bookController.GetBooks();
    Console.WriteLine(string.Format("Boooks count: {0}", books != null ? books.Count : "empty list"));
    if (books != null)
    {
        foreach (Book bookItem in books)
        {
            Console.WriteLine(string.Format("Tite: {0} Price {1} Bookstand {2} Shelf {3}", bookItem.Title, Math.Round(bookItem.Price, 2), bookItem.Bookstand, bookItem.Shelf));
        }
    }
}catch(ApiException apiException)
{
    Console.WriteLine(apiException.Message);
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

try
{
    HttpStatusCode httpStatusCode = await bookController.AddBook(book);
    if (httpStatusCode == HttpStatusCode.Created)
    {
        Console.WriteLine("Book created!");
    }
    else
    {
        Console.WriteLine("Error while creating book");
    }
}
catch (ApiException apiException)
{
    Console.WriteLine(apiException.Message);
}

try
{
    OrderController orderController = new OrderController(apiClient);
    List<Order>? orders = await orderController.GetOrders(3, 10);
    Console.WriteLine(string.Format("Orders count: {0}", orders != null ? orders.Count : "empty list"));
    if (orders != null)
    {
        foreach (Order order in orders)
        {
            Console.WriteLine(string.Format("Order: {0} Order lines count {1}", order.OrderId, order.OrderLines.Count));
        }
    }
}
catch (ApiException apiException)
{
    Console.WriteLine(apiException.Message);
}

Console.ReadKey();