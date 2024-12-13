namespace ApiClient.ApiHandler
{
    /// <summary>
    /// Generic response class for passing data from api
    /// </summary>
    /// <typeparam name="T">Type of object to be returned by this class</typeparam>
    public class Response<T>
    {
        public T Result { get; set; }
        public string ErrorMessage { get; set; }

        public Response(T result)
        {
            Result = result;
        }

        public Response(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }

        public Response(T result, string errorMessage)
        {
            Result = result;
            ErrorMessage = errorMessage;
        }
    }
}
