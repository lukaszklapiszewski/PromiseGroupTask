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
            List<Order>? orders = await orderController.GetOrders(1, int.MaxValue);
            Assert.That(orders, Is.Not.Null);
            Assert.That(orders!.Count, Is.EqualTo(100));
        }

        [Test]
        public async Task GetDifferentPages()
        {
            List<Order>? ordersPage1 = await orderController.GetOrders(1, 3);
            Assert.That(ordersPage1, Is.Not.Null);
            Assert.That(ordersPage1!.Count, Is.EqualTo(3));

            List<Order>? ordersPage2 = await orderController.GetOrders(2, 3);
            Assert.That(ordersPage2, Is.Not.Null);
            Assert.That(ordersPage2!.Count, Is.EqualTo(3));

            foreach (var order in ordersPage1)
            {
                foreach(var order2 in ordersPage2)
                {
                    Assert.That(order.OrderId, Is.Not.EqualTo(order2.OrderId));
                }
            }
        }

        [Test]
        public async Task GetSamePages()
        {
            List<Order>? ordersPage1 = await orderController.GetOrders(2, 3);
            Assert.That(ordersPage1, Is.Not.Null);
            Assert.That(ordersPage1!.Count, Is.EqualTo(3));

            List<Order>? ordersPage2 = await orderController.GetOrders(2, 3);
            Assert.That(ordersPage2, Is.Not.Null);
            Assert.That(ordersPage2!.Count, Is.EqualTo(3));

            for(int i = 0; i < 3; i++)
            {
                {
                    Assert.That(ordersPage1[i].OrderId, Is.EqualTo(ordersPage2[i].OrderId));
                }
            }
        }

        [Test]
        public async Task GetLastPage()
        {
            List<Order>? ordersPage1 = await orderController.GetOrders(34, 3);
            Assert.That(ordersPage1, Is.Not.Null);
            Assert.That(ordersPage1!.Count, Is.EqualTo(1));
        }

        [Test]
        public async Task GetOutOfRangePage()
        {
            List<Order>? ordersPage1 = await orderController.GetOrders(35, 3);
            Assert.That(ordersPage1, Is.Not.Null);
            Assert.That(ordersPage1!.Count, Is.EqualTo(0));
        }
    }
}
