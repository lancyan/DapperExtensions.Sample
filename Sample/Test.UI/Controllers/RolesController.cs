using Test.UI.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Test.Entity.SYS;
using Test.Utility;

namespace Test.UI.Controllers
{
    [MyAuth]
    public class RolesController : BaseController
    {
        //
        // GET: /Roles/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Edit()
        {
            return View("Edit");
        }

        //string controllerName = "roles";


        public async Task<ActionResult> GetList(string where, int pageIndex, int pageSize, string orderBy)
        {
            string base64Where = Common.StringToBase64(where);
            string query = string.Format("?pageIndex={0}&pageSize={1}&orderBy={2}&where={3}", pageIndex, pageSize, orderBy, base64Where);
            var listStr = await HttpClientHelper.GetAsyncString(folderName, controllerName, "GetList", query);
            return Content(listStr, "text/json");
        }


        public async Task<ActionResult> GetCount(string where)
        {
            string base64Where = Common.StringToBase64(where); 
            string query = string.Format("?where={0}", base64Where);
            var count = await HttpClientHelper.GetAsyncString(folderName, controllerName, "GetCount", query);
            return Content(count);
        }

        //
        // GET: /PageInfo/Details/5
        public async Task<ActionResult> GetRole(int id)
        {
            string query = "?id=" + id;
            var json = await HttpClientHelper.GetAsyncString(folderName, controllerName, "Get", query);
            return Content(json, "text/json");
        }

        public async Task<ActionResult> GetUserRoles(int userId)
        {
            string query = "?userId=" + userId;
            var json = await HttpClientHelper.GetAsyncString(folderName, controllerName, "GetUserRoles", query);
            return Content(json, "text/json");
        }

        public async Task<ActionResult> GetRoles()
        {
            var json = await HttpClientHelper.GetAsyncString(folderName, controllerName, "GetRoles");
            return Content(json, "text/json");
        }

        //
        // GET: /PageInfo/Details/5
        [HttpPost]
        public async Task<ActionResult> EditRole(Roles model, string menuIds)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            dict.Add("model", model);
            dict.Add("menuIds", menuIds);
            var result = await HttpClientHelper.PostAsync(folderName, controllerName, dict);
            return Content(result, "text/json");
        }

        public async Task<ActionResult> Delete(int id)
        {
            var count = await HttpClientHelper.DeleteAsync(folderName, controllerName, id);
            return Content(count);
        }

  

    }
}
