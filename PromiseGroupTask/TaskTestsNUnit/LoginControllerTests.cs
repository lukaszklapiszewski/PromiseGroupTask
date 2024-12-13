using ApiClient.ApiHandler;
using ApiClient.ApiHandler.Interface;
using ApiClient.Controller;
using ApiClient.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TaskTestsNUnit
{
    public class LoginControllerTests
    {
        private LoginController loginController;

        [SetUp]
        public void Setup()
        {
            IApiClient apiClient = new MockApiClient(new LoginHttpMessageHandler());
            loginController = new LoginController(apiClient);
        }

        [Test]
        public async Task TestCorrectLogin()
        {
            User user = new User()
            {
                Login = "testlogin@test.eu",
                Password = "testPassword"
            };

            Response<HttpStatusCode> loginResponse = await loginController.Login(user);

            Assert.That(loginResponse, Is.Not.Null);
            Assert.That(loginResponse.Result, Is.EqualTo(HttpStatusCode.OK));
        }

        [Test]
        public async Task TestInCorrectLogin()
        {
            User user = new User()
            {
                Password = "testPassword"
            };

            Response<HttpStatusCode> loginResponse = await loginController.Login(user);

            Assert.That(loginResponse, Is.Not.Null);
            Assert.That(loginResponse.Result, Is.Not.EqualTo(HttpStatusCode.OK));

            user = new User()
            {
                Login = "wrongLogin@test.eu",
                Password = "testPassword"
            };

            loginResponse = await loginController.Login(user);

            Assert.That(loginResponse, Is.Not.Null);
            Assert.That(loginResponse.Result, Is.Not.EqualTo(HttpStatusCode.OK));
        }
    }
}
