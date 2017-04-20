using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace Test.Utility
{
    public class ApiResultModel
    {
        public int Code { get; set; }
        public HttpStatusCode Status { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }

        

    }

}