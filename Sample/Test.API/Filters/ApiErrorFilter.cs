using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Filters;
using Test.Entity;
using Test.API.Models;

namespace Test.API.Filters
{
    public class ApiErrorFilter : System.Web.Http.Filters.ExceptionFilterAttribute
    {
        public override void OnException(System.Web.Http.Filters.HttpActionExecutedContext actionExecutedContext)
        {
            base.OnException(actionExecutedContext);

            // 取得发生例外时的错误讯息
            var errorMessage = actionExecutedContext.Exception.Message;

            var response = actionExecutedContext.Response;
            int statusCode;
            if (response == null)
            {
                statusCode = 404;
            }
            else
            {
                statusCode = (int)actionExecutedContext.Response.StatusCode;
            }
            var result = new ApiResult()
            {
                Status = statusCode,
                Message = errorMessage
            };

            // 重新打包回传的讯息
            actionExecutedContext.Response = actionExecutedContext.Request.CreateResponse((HttpStatusCode)statusCode, result);
        }
      
    }
}