using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Everaldo.Cardoso.C19BR.Framework.HttpTransaction
{
    public class HttpRequestResponse
    {
        public HttpRequestResponse()
        {
            Success = false;
            HttpResult = null;
            Exception = null;
        }
        public bool Success { get; set; }
        public HttpResponseMessage HttpResult { get; set; }
        public Exception Exception { get; set; }
    }
}
