using Test.UI.Filter;
using Test.UI.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Test.Entity.SYS;
using Test.Utility;

namespace Test.UI.Controllers
{
    [MyAuth]
    public class UsersController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }

    

        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <param name="where"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        public async Task<ActionResult> GetList(string where, int pageIndex, int pageSize, string orderBy)
        {
            string encryptWhere = Common.StringToBase64(where);
            string query = string.Format("?pageIndex={0}&pageSize={1}&orderBy={2}&where={3}", pageIndex, pageSize, orderBy, encryptWhere);
            var listStr = await HttpClientHelper.GetAsyncString(folderName, controllerName, "GetList", query);
            return Content(listStr, "text/json");
        }

        /// <summary>
        /// 获取用户记录条数
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public async Task<ActionResult> GetCount(string where)
        {
            string base64Where = Common.StringToBase64(where);
            string query = string.Format("?where={0}", base64Where);
            var count = await HttpClientHelper.GetAsyncString(folderName, controllerName, "GetCount", query);
            return Content(count);
        }

         /// <summary>
         /// 得到用户通过id
         /// </summary>
         /// <param name="id"></param>
         /// <returns></returns>
        public async Task<ActionResult> GetUser(int id)
        {
            string query = "?id=" + id;
            var user = await HttpClientHelper.GetAsyncString(folderName, controllerName, "Get", query);
            return Content(user, "text/json");
        }

        /// <summary>
        /// 编辑用户
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> EditUser(Users model)
        {
            var result = await HttpClientHelper.PostAsync<Users>(folderName, controllerName, model);
            return Content(result, "text/json");
        }

        /// <summary>
        /// 编辑用户角色
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="userName"></param>
        /// <param name="roleIds"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> EditUserRole(int userId, string userName, string roleIds)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            dict.Add("userId", userId);
            dict.Add("userName", userName);
            dict.Add("roleIds", roleIds.Split(','));

            var result = await HttpClientHelper.PostAsync(folderName, controllerName, dict, "EditUserRole");
            return Content(result, "text/json");
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ActionResult> Delete(int id)
        {
            var count = await HttpClientHelper.DeleteAsync(folderName, controllerName, id);
            return Content(count);
        }


        /// <summary>
        /// 请求界面
        /// </summary>
        /// <returns></returns>
        public ActionResult Edit()
        {
            //CurrentUser.Roles

            return View("Edit");
        }



    }
}
