namespace TaskTestsNUnit
{
    public class LoginHttpMessageHandler : HttpMessageHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            HttpResponseMessage response = new HttpResponseMessage();

            return Task.FromResult(response);
        }
    }
}
