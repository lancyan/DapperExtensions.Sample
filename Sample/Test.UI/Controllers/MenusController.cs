using Test.UI.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Test.Entity.SYS;
using Test.Utility;
using System.Reflection;

namespace Test.UI.Controllers
{
    [MyAuth]
    public class MenusController : BaseController
    {
        //
        // GET: /Menus/

        public ActionResult Index()
        {
            return View();
        }



        //string controllerName = "Menus";
 

        public async Task<ActionResult> GetMenus()
        {
            if (CurrentUser != null)
            {
                string query = string.Format("?roleId={0}", string.Join(",", CurrentUser.Roles));
                var listStr = await HttpClientHelper.GetAsyncString(folderName, controllerName, "GetMenus", query);
                return Content(listStr, "text/json");
            }
            else
            {
                return Content("{}", "text/json");
            }
        }

        public async Task<ActionResult> GetLeftMenus()
        {
            if (CurrentUser != null)
            {
                string query = string.Format("?roleId={0}", string.Join(",", CurrentUser.Roles));
                var listStr = await HttpClientHelper.GetAsyncString(folderName, controllerName, "GetLeftMenus", query);
                return Content(listStr, "text/json");
            }
            else
            {
                return Content("{}", "text/json");
            }
        }

        public async Task<ActionResult> DeleteMenu(int id)
        {
            var flag = await HttpClientHelper.DeleteAsync(folderName, controllerName, id);
            return Content(flag);
        }

        public async Task<ActionResult> BatchAdd(int parentId, string parentName, string code)
        {
            //
            //int parentId = obj.parentId;
            //string parentName = obj.parentName;
            //string code = obj.code;

            Dictionary<string, object> dict = new Dictionary<string, object>();
            dict.Add("parentId", parentId);
            dict.Add("parentName", parentName);
            dict.Add("code", code);
            dict.Add("actions", GetMethods(code));
            var flag = await HttpClientHelper.PostAsync(folderName, controllerName, dict, "BatchAdd");
            return Content(flag);
        }

        public ActionResult Edit()
        {
            return View("Edit");
        }

        public async Task<ActionResult> GetMenu(int id)
        {
            string query = "?id=" + id;
            var json = await HttpClientHelper.GetAsyncString(folderName, controllerName, "Get", query);
            return Content(json, "text/json");
        }

        public ActionResult GetIcos()
        {
            StringBuilder json = new StringBuilder();
            json.Append("[");
            json.Append("{\"id\":1,\"text\":\"icon-sys\",\"ico\":\"icon-sys\"},");
            json.Append("{\"id\":2,\"text\":\"icon-set\",\"ico\":\"icon-set\"},");
            json.Append("{\"id\":3,\"text\":\"icon-add\",\"ico\":\"icon-add\"},");
            json.Append("{\"id\":4,\"text\":\"icon-nav\",\"ico\":\"icon-nav\"},");
            json.Append("{\"id\":5,\"text\":\"icon-users\",\"ico\":\"icon-users\"},");
            json.Append("{\"id\":6,\"text\":\"icon-role\",\"ico\":\"icon-role\"},");
            json.Append("{\"id\":7,\"text\":\"icon-log\",\"ico\":\"icon-log\"},");
            json.Append("{\"id\":8,\"text\":\"icon-delete\",\"ico\":\"icon-delete\"},");
            json.Append("{\"id\":9,\"text\":\"icon-edit\",\"ico\":\"icon-edit\"},");
            json.Append("{\"id\":10,\"text\":\"icon-magic\",\"ico\":\"icon-magic\"},");
            json.Append("{\"id\":11,\"text\":\"icon-database\",\"ico\":\"icon-database\"},");
            json.Append("{\"id\":12,\"text\":\"icon-expand\",\"ico\":\"icon-expand\"},");
            json.Append("{\"id\":13,\"text\":\"icon-collapse\",\"ico\":\"icon-collapse\"},");
            json.Append("{\"id\":14,\"text\":\"icon-smile\",\"ico\":\"icon-smile\"}");
            json.Append("{\"id\":15,\"text\":\"icon-cry\",\"ico\":\"icon-cry\"}");
            json.Append("]");

            return Content(json.ToString(), "text/json");
        }

        [OutputCache(Duration = 120)]
        public ActionResult GetControllers()
        {
            Type type = typeof(BaseController);
            Assembly assembly = Assembly.GetExecutingAssembly();
            Type[] typeArr = assembly.GetTypes();
            Dictionary<string, List<string>> list1 = new Dictionary<string, List<string>>();
            object objs = Activator.CreateInstance(type);
            string ns = MethodBase.GetCurrentMethod().DeclaringType.Namespace;
            foreach (var ta in typeArr)
            {
                if (ta.BaseType == type)
                {
                    string controllerName = ta.Name.Replace("Controller", "");
                    string cname = controllerName.Trim('/');
                    string tn = ns + "." + cname + "Controller";
                    object obj = assembly.CreateInstance(tn, true);
                    List<string> list2 = new List<string>();
                    if (obj != null)
                    {
                        var ms = obj.GetType().GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
                        foreach (MethodInfo mi in ms)
                        {
                            list2.Add(mi.Name);
                        }
                    }
                    list1.Add(controllerName, list2);
                }
            }

            return Json(list1, JsonRequestBehavior.AllowGet);
        }


        private List<string> GetMethods(string controllerName = null)
        {
            List<string> list = new List<string>();
            if (!string.IsNullOrEmpty(controllerName))
            {
                string cname = controllerName.Trim('/');
                string ns = MethodBase.GetCurrentMethod().DeclaringType.Namespace;
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
            return list;
        }


        public async Task<ActionResult> EditMenu(Menus menu)
        {
            var flag = await HttpClientHelper.PostAsync(folderName, controllerName, menu);
            return Content(flag);
        }

    }
}
