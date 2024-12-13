using ApiClient.ApiHandler;
using ApiClient.ApiHandler.Interface;
using ApiClient.Controller;
using ApiClient.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskTestsNUnit
{
    public sealed class OrderControllerTest
    {
        private Random random = new Random(DateTime.Now.Second);

        private OrderController orderController;

        [SetUp]
        public void Setup()
        {
            List<Order> orders = new List<Order>();

            for (int i = 0; i < 100; i++)
            {
                orders.Add(new Order()
                {
                    OrderId = Guid.NewGuid(),
                    OrderLines = new List<OrderLine>()
                    {
                        new OrderLine()
                        {
                           BookId = i,
                           Quantity = random.Next()
                        }
                    }
                });
            }

            IApiClient apiClient = new MockApiClient(new MockOrdersHandler(orders));
            orderController = new OrderController(apiClient);
        }

        [Test]
        public async Task GetAll()
        {
            Response<List<Order>> ordersResponse = await orderController.GetOrders(1, int.MaxValue);
            Assert.That(ordersResponse.Result, Is.Not.Null);
            Assert.That(ordersResponse.Result.Count, Is.EqualTo(100));
        }

        [Test]
        public async Task GetDifferentPages()
        {
            Response<List<Order>> ordersPage1Response = await orderController.GetOrders(1, 3);
            Assert.That(ordersPage1Response, Is.Not.Null);
            Assert.That(ordersPage1Response.Result, Is.Not.Null);
            Assert.That(ordersPage1Response.Result.Count, Is.EqualTo(3));

            Response<List<Order>> ordersPage2Response = await orderController.GetOrders(2, 3);
            Assert.That(ordersPage2Response, Is.Not.Null);
            Assert.That(ordersPage2Response.Result, Is.Not.Null);
            Assert.That(ordersPage2Response.Result.Count, Is.EqualTo(3));

            foreach (var order in ordersPage1Response.Result)
            {
                foreach(var order2 in ordersPage2Response.Result)
                {
                    Assert.That(order.OrderId, Is.Not.EqualTo(order2.OrderId));
                }
            }
        }

        [Test]
        public async Task GetSamePages()
        {
            Response<List<Order>> ordersPage1Response = await orderController.GetOrders(2, 3);
            Assert.That(ordersPage1Response, Is.Not.Null);
            Assert.That(ordersPage1Response.Result, Is.Not.Null);
            Assert.That(ordersPage1Response.Result.Count, Is.EqualTo(3));

            Response<List<Order>> ordersPage2Response = await orderController.GetOrders(2, 3);
            Assert.That(ordersPage2Response, Is.Not.Null);
            Assert.That(ordersPage2Response.Result, Is.Not.Null);
            Assert.That(ordersPage2Response.Result.Count, Is.EqualTo(3));

            for(int i = 0; i < 3; i++)
            {
                {
                    Assert.That(ordersPage1Response.Result[i].OrderId, Is.EqualTo(ordersPage2Response.Result[i].OrderId));
                }
            }
        }

        [Test]
        public async Task GetLastPage()
        {
            Response<List<Order>> ordersPage1Response = await orderController.GetOrders(34, 3);
            Assert.That(ordersPage1Response, Is.Not.Null);
            Assert.That(ordersPage1Response.Result, Is.Not.Null);
            Assert.That(ordersPage1Response.Result.Count, Is.EqualTo(1));
        }

        [Test]
        public async Task GetOutOfRangePage()
        {
            Response<List<Order>> ordersPage1Response = await orderController.GetOrders(35, 3);
            Assert.That(ordersPage1Response, Is.Not.Null);
            Assert.That(ordersPage1Response.Result, Is.Not.Null);
            Assert.That(ordersPage1Response.Result.Count, Is.EqualTo(0));
        }
    }
}
