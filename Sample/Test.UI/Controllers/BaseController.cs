using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using Test.Entity.SYS;
using Test.Utility;

namespace Test.UI.Controllers
{
    public class BaseController : Controller
    {
        //
        // GET: /Base/
        protected string folderName = "test";

        public string controllerName
        {
            get
            {
                return RouteData.Values["controller"].ToString();
            }
        }
  
        public UserDatas CurrentUser
        {
            get
            {
                if (Session["User"] != null)
                {
                    return Session["User"] as UserDatas;
                }
                else
                {
                    var cookie = Request.Cookies[FormsAuthentication.FormsCookieName];
                    if (cookie == null || string.IsNullOrEmpty(cookie.Value))
                        return null;
                    var ticket = FormsAuthentication.Decrypt(cookie.Value);
                    if (ticket != null && !string.IsNullOrWhiteSpace(ticket.UserData))
                    {
                        return Common.Deserialize<UserDatas>(ticket.UserData);
                    }
                }
                return null;
            }
        }
    }
}
