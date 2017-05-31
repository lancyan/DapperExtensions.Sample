using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Test.UI.Filter
{
    /// <summary> 
    /// 错误日志（Controller发生异常时会执行这里） 
    /// </summary> 
    public class ErrorAttribute : ActionFilterAttribute, IExceptionFilter
    {
        /// <summary> 
        /// 异常 
        /// </summary> 
        /// <param name="filterContext"></param> 
        public void OnException(ExceptionContext filterContext)
        {
            Exception Error = filterContext.Exception;
            string message = Error.Message; //错误信息 
            string controller = filterContext.RouteData.Values["controller"].ToString(); 
            string url = HttpContext.Current.Request.RawUrl; //错误发生地址 
            filterContext.ExceptionHandled = true;
            filterContext.Result = new RedirectResult("/Error/Index"); //跳转至错误提示页面 
        }
    } 
}