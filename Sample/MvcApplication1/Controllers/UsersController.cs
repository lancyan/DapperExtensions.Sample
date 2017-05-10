using MvcApplication1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Test.Entity;
using Test.Utility;

namespace MvcApplication1.Controllers
{
    public class UsersController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        private string folderName = "test";


        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

       

        public async Task<ActionResult> GetList(Users model, int pageIndex, int pageSize)
        {
            string query = string.Format("?pageIndex={0}&pageSize={1}&", pageIndex, pageSize);
            query += Common.GetQueryString(model);

            //if (model.UserName != null)
            //{
            //    query += "userName=" + model.UserName;
            //}
            var list = await HttpClientHelper.GetAsyncString(folderName, "users", "GetList", query);
            //return Content(Common.Serialize(list));//"text/json"
            //return Content(Common.Serialize(list, "text/json"));
            return View(list);

        }
       
        //
        // GET: /PageInfo/Details/5
        public async Task<ActionResult> Edit(int id)
        {
            UserModels editModel = null;

            string query = "?id=" + id;

            var user = await HttpClientHelper.GetAsyncObject<Users>(folderName, "users", "Get", query);

            ViewBag.UserName = user.UserName;
            ViewBag.Sex = user.Sex;
            ViewBag.Birthday = user.Birthday;
            //editModel = new UserModels { UserName = user.UserName, Birthday = user.Birthday, Mobile = user.Mobile, NickName = user.NickName, Sex = user.Sex };

            return View("Edit");
            //return View(user);
        }

        public async Task<ActionResult> GetCount(Users model)
        {
            string query = "?";
            //query += Common.GetQueryString(model);
            if (model.UserName != null)
            {
                query += "userName=" + model.UserName;
            }
            var count = await HttpClientHelper.GetAsyncString(folderName, "users", "GetCount", query);
            return Content(count);
        }

        public async Task<ActionResult> GetTest(int id)
        {
            //var count = await HttpClientHelper.GetAsyncString(folderName, "users", "GetTime", "?id=" + id);
            var count = await HttpClientHelper.PutAsync<Users>(folderName, "users", 1, new Users { NickName = "AAA", UserName = "AAA", CreateTime = DateTime.Now, UpdateTime = DateTime.Now });
            //var count = await HttpClientHelper.PostAsync<Users>(folderName, "users", new Users { NickName = "CCC", UserName = "CCCCCC", Mobile = "13580098899", CreateTime = DateTime.Now, Status = 1, UpdateTime = DateTime.Now });
            //var count = await HttpClientHelper.DeleteAsync(folderName, "users", 1);

            return Content(count);
        }

        public async Task<ActionResult> GetTime(int id)
        {
            var time = await HttpClientHelper.GetAsyncString(folderName, "users", "GetTime", "?id=" + id);

            return Content(time);
        }

    }
}
