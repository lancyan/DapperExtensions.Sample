using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Test.Utility;

namespace Test.UI.Controllers
{
    public class RightsController : BaseController
    {
        //
        // GET: /Rights/

        public ActionResult Index()
        {
            ViewData["Roles"] = CurrentUser == null ? "" : string.Join(",", CurrentUser.Roles);
            return View();
        }

        //string controllerName = "rights";


        public async Task<ActionResult> GetRights(string roleId)
        {
            if (CurrentUser != null)
            {
                string query = string.Format("?roleId={0}", roleId.Trim());
                var listStr = await HttpClientHelper.GetAsyncString(folderName, controllerName, "GetRights", query);
                //var ja = JArray.Parse(listStr);
                //for (int i = 0; i < ja.Count; i++)
                //{
                //    var jo = (JObject)ja[i];
                //}
                return Content(listStr, "text/json");
            }
            else
            {
                return Content("{}", "text/json");
            }
        }

        public async Task<ActionResult> GetRights2(string roleId)
        {
            if (CurrentUser != null)
            {
                string query = string.Format("?roleId={0}", roleId.Trim());
                var listStr = await HttpClientHelper.GetAsyncString(folderName, controllerName, "GetRights", query);
                return Content(listStr, "text/json");
            }
            else
            {
                return Content("{}", "text/json");
            }
        }




        public ActionResult GetMethods(string c = null)
        {
            List<string> list = new List<string>();
            string cname = "";
            if (!string.IsNullOrEmpty(c))
            {
                cname = c.Trim('/');
                string ns = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Namespace;
                string tn = ns + "." + cname + "Controller";
                Assembly assembly = Assembly.GetExecutingAssembly();
                object obj = assembly.CreateInstance(tn, true);
                if (obj != null)
                {
                    var ms = obj.GetType().GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
                    foreach (MethodInfo mi in ms)
                    {
                        list.Add(mi.Name);
                    }
                }
            }
            return Json(new { name = cname, list = list }, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> EditRight(string roleId, string menuCodeStr, string rightCodeStr)
        {
            if (CurrentUser != null)
            {
                string query = string.Format("?roleId={0}", roleId.Trim());
                Dictionary<string, object> dict = new Dictionary<string, object>();
                dict.Add("roleId", roleId);
                dict.Add("menuCode", menuCodeStr);
                dict.Add("rightCode", rightCodeStr);

                var listStr = await HttpClientHelper.PostAsync(folderName, controllerName, dict, "EditRight");
                return Content(listStr, "text/json");
            }
            else
            {
                return Content("{}", "text/json");
            }
        }

    }
}
