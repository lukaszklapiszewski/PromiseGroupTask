namespace ApiClient.Exceptions
{
    /// <summary>
    /// Simpe APi exception class
    /// </summary>
    public class ApiException : Exception
    {
        public ApiException():base() { }
        public ApiException(string message) : base(message) { }
    }
}
