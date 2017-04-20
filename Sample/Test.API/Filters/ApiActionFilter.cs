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

    public class ApiActionFilter : System.Web.Http.Filters.ActionFilterAttribute
    {
        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            base.OnActionExecuted(actionExecutedContext);

            ApiResult result = new ApiResult();

            if (actionExecutedContext.Response != null)
            {
                HttpStatusCode statusCode = actionExecutedContext.ActionContext.Response.StatusCode;
                result.Message = "成功";
                // 取得由 API 返回的状态代码
                result.Status = (int)statusCode;
                // 取得由 API 返回的资料
                result.Data = actionExecutedContext.ActionContext.Response.Content.ReadAsAsync<object>().Result;
                // 重新封装回传格式
                actionExecutedContext.Response = actionExecutedContext.Request.CreateResponse(statusCode, result);
            }
            else
            {
                //result.Status = HttpStatusCode.InternalServerError;
                //result.Data = null;
                //result.ErrorMessage = actionExecutedContext.Exception.Message;
            }
        }
    }

}