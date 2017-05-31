using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Test.Entity.SYS;
using Test.Utility;

namespace Test.UI.Controllers
{
    public class HomeController : BaseController
    {

        //
        // GET: /Home/

        public ActionResult Index()
        {
            if (CurrentUser != null)
            {
                //if (new SYS_UserBLL().Exists(userName))
                //{
                //    Response.Redirect(FormsAuthentication.GetRedirectUrl(userName, false));
                //}
                return RedirectToAction("Index", "Main");

            }
            return View();
        }


        [HttpPost]
        public async Task<ActionResult> Login(string un, string pwd, string validateCode)
        {
            int re = 0;
            if (string.IsNullOrEmpty(un))
            {
                re = 1;//"用户名不能为空~";
            }
            else if (string.IsNullOrEmpty(pwd))
            {
                re = 2;//"密码不能为空~";
            }
            //else if (string.IsNullOrEmpty(validateCode))
            //{
            //    re = 3;//"验证码不能为空~";
            //}
            //else if (Session["CheckCode"] == null || !validateCode.Equals(Session["CheckCode"].ToString(), StringComparison.OrdinalIgnoreCase))
            //{
            //    //ValidatedCode v = new ValidatedCode(16);
            //    //string code = v.CreateVerifyCode();
            //    //v.CreateImageOnPage(code, HttpContext.Current);
            //    //HttpContext.Current.Session["CheckCode"] = code;
            //    re = 4;//"验证码不正确~";
            //}
            else
            {
                Session["CheckCode"] = null;
                //string query = string.Format("?pageIndex={0}&pageSize={1}&orderBy={2}&where={3}", pageIndex, pageSize, orderBy, base64Where);

                string encodePwd = EncryptHelper.Md5(EncryptHelper.AESEncrypt(pwd));
                Dictionary<string, object> dict = new Dictionary<string, object>();
                dict.Add("userName", un);
                dict.Add("passWord", encodePwd);

                var listStr = await HttpClientHelper.PostAsync(folderName, "users", dict, "UserLogin");
                if (listStr != null && listStr != "null")
                {
                    var userDatas = Common.Deserialize<UserDatas>(listStr);
                    FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, un, DateTime.Now, DateTime.Now.AddMinutes(30), false, listStr, "/");
                    HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(ticket));
                    cookie.HttpOnly = true;
                    cookie.Expires = ticket.Expiration;
                    FormsAuthentication.SetAuthCookie(un, false);
                    //FormsAuthentication.RedirectFromLoginPage(un, true);
                    Session["User"] = userDatas;
                    Response.Cookies.Add(cookie);
                }
                else
                {
                    re = 5;// "用户名或密码不正确";
                }
            }

            return Content(re.ToString(), "text/json");
        }


      

        [HttpGet]
        public ActionResult GetValidateCode()
        {
            ValidatedCode v = new ValidatedCode(16);
            string code = v.CreateVerifyCode();
            byte[] buffer = v.CreateImageOnPage(code);
            Session["CheckCode"] = code;
            return File(buffer.ToArray(), "image/jpeg");
        }


   
    }
}
