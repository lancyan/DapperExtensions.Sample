using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Test.Entity.SYS;
using Test.Utility;

namespace Test.UI.Filter
{
    /// <summary> 
    /// 权限验证 
    /// </summary> 
    public class MyAuthAttribute : AuthorizeAttribute
    {
        /// <summary> 
        /// 角色名称 
        /// </summary> 
        public string controllerName { get; set; }
        public string actionName { get; set; }
        
        /// <summary> 
        /// 验证权限（action执行前会先执行这里） 
        /// </summary> 
        /// <param name="filterContext"></param> 
        //public override void OnActionExecuting(ActionExecutingContext filterContext)
        //{
        //    // 获取域名
        //    string domainName = filterContext.HttpContext.Request.Url.Authority;
        //    //获取模块名称
        //    // module = filterContext.HttpContext.Request.Url.Segments[1].Replace('/', ' ').Trim();
        //    //获取 controllerName 名称
        //    string controllerName = filterContext.RouteData.Values["controller"].ToString();
        //    //获取ACTION 名称
        //    string actionName = filterContext.RouteData.Values["action"].ToString();

        //    //如果存在身份信息 
        //    if (!HttpContext.Current.User.Identity.IsAuthenticated)
        //    {
        //        ContentResult Content = new ContentResult();
        //        Content.Content = string.Format("<script type='text/javascript'>alert('请先登录！');window.location.href='{0}';</script>", FormsAuthentication.LoginUrl);
        //        filterContext.Result = Content;
        //    }
        //    else
        //    {
        //        HttpCookie authCookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];//获取cookie 
        //        FormsAuthenticationTicket Ticket = FormsAuthentication.Decrypt(authCookie.Value);//解密 
        //        UserData userData = Common.Deserialize<UserData>(Ticket.UserData);//反序列化 
        //        string[] Role = userData.Roles.Split(','); //获取所有角色 
        //        if (!Role.Contains(Code)) //验证权限 
        //        {
        //            //验证不通过 
        //            ContentResult Content = new ContentResult();
        //            Content.Content = "<script type='text/javascript'>alert('权限验证不通过！');history.go(-1);</script>";
        //            filterContext.Result = Content;
        //        }
        //    }
        //}

        //string adminCode = System.Configuration.ConfigurationManager.AppSettings["adminCode"].ToString();

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext == null)
            {
                throw new ArgumentNullException("httpContext");
            }
            if (!httpContext.User.Identity.IsAuthenticated)
            {
                return false;
            }

            //string filePath = httpContext.Request.CurrentExecutionFilePath;

            var cookie = httpContext.Request.Cookies[FormsAuthentication.FormsCookieName];
            if (cookie == null || string.IsNullOrEmpty(cookie.Value))
                return false;
            var ticket = FormsAuthentication.Decrypt(cookie.Value);
            
            if (ticket != null && !string.IsNullOrWhiteSpace(ticket.UserData))
            {
                UserDatas obj = Common.Deserialize<UserDatas>(ticket.UserData);
                Dictionary<string, object> dict = new Dictionary<string, object>();
                dict.Add("r", string.Join(",", obj.Roles));
                dict.Add("c", controllerName);
                dict.Add("a", actionName);
                var result = HttpClientHelper.Post("test", "roles", dict, "IsPermission");

                return true;
                //return result == "true";
                //string httpUrl = httpContext.Request.FilePath.TrimEnd('/');
                //if (new UserBLL().IsHasRight(httpUrl, obj.RoleCode))
                //    result = true;

                //return true;
            }
            else
            {
                //FormsIdentity fi = (HttpContext.Current.User.Identity as FormsIdentity);
                //if (!result)
                //{
                //    httpContext.Response.StatusCode = 403;
                //}
                return false;
            }
           
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            actionName = filterContext.ActionDescriptor.ActionName;
            //string roles = GetRoles.GetActionRoles(actionName, controllerName);
            //if (!string.IsNullOrWhiteSpace(roles))
            //{
                //this.Roles = roles.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            //}
            base.OnAuthorization(filterContext);
        }

        //如果AuthorizeCore返回false才会执行HandleUnauthorizedRequest
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            base.HandleUnauthorizedRequest(filterContext);
            if (filterContext == null)
            {
                throw new ArgumentNullException("filterContext");
            }
            else
            {
                filterContext.HttpContext.Response.Redirect("/home/index");
            }
        }


    }
}