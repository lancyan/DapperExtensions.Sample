using Test.UI.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Test.Utility;

namespace Test.UI.Controllers
{
    [MyAuth]
    public class MainController : BaseController
    {
        //
        // GET: /Main/

        public ActionResult Index()
        {
            ViewBag.UserName = CurrentUser == null ? "" : CurrentUser.UserName;
            return View();
        }

        [HttpPost]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Response.Cookies.Remove(FormsAuthentication.FormsCookieName);
            Response.Cache.SetExpires(DateTime.Now.AddSeconds(-1));
            HttpCookie cookie = Request.Cookies[FormsAuthentication.FormsCookieName];
            if (cookie != null)
            {
                cookie.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(cookie);
            }
            Session["User"] = null;
            return Content("1");
        }

        public async Task<ActionResult> UpdatePwd(string pwd1, string pwd2)
        {
            int id = CurrentUser == null ? 0 : CurrentUser.UserId;
            if (id == 0)
            {
                return Content("0");
            }
            else
            {
                Dictionary<string, object> dict = new Dictionary<string, object>();
                dict.Add("id", id);
                dict.Add("pwd1", EncryptHelper.Md5(EncryptHelper.AESEncrypt(pwd1)));
                dict.Add("pwd2", EncryptHelper.Md5(EncryptHelper.AESEncrypt(pwd2)));
                var result = await HttpClientHelper.PostAsync(folderName, "users", dict, "UpdatePwd");
                return Content(result, "text/json");
            }
        }

    }
}
