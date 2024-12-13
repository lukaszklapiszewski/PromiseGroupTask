using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiClient.ApiHandler
{
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
