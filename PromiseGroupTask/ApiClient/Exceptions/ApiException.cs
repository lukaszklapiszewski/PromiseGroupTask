﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
