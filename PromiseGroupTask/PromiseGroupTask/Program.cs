// See https://aka.ms/new-console-template for more information
using ApiClient.Controller;
using ApiClient.Exceptions;
using ApiClient.Model;
using System.Net;

BookController bookController = new BookController();
try
{
    List<Book>? books = await bookController.GetBooks();
    Console.WriteLine(string.Format("Boooks count: {0}", books != null ? books.Count : "empty list"));
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
    if (httpStatusCode == HttpStatusCode.OK)
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
    OrderController orderController = new OrderController();
    List<Order>? orders = await orderController.GetOrders(0, 10);
    Console.WriteLine(string.Format("Orders count: {0}", orders != null ? orders.Count : "empty list"));
}
catch (ApiException apiException)
{
    Console.WriteLine(apiException.Message);
}


Console.ReadKey();