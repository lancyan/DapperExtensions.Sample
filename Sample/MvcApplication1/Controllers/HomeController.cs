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
    public class HomeController : Controller
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
            var list = await HttpClientHelper.GetAsyncObject<dynamic>(folderName, "users", "GetList", query);
            return Content(Common.Serialize(list), "text/json");

        }
        public async Task<ActionResult> GetCount(Users model)
        {
            string query = "?";
            query += Common.GetQueryString(model);
            var count = await HttpClientHelper.GetAsyncString(folderName, "users", "GetCount", query);
            return Content(count);
        }
        public async Task<ActionResult> GetTest(int id)
        {

            //var count = await HttpClientHelper.GetAsyncString(folderName, "users", "GetTime", "?id=" + id);

            var count = await HttpClientHelper.PutAsync<Users>(folderName, "users", 1, new Users { NickName = "AAA", UserName = "AAA", CreateTime = DateTime.Now, UpdateTime = DateTime.Now });


            //var count = await HttpClientHelper.PostAsync<Users>(folderName, "users", new Users { NickName = "CCC", UserName = "CCCCCC", Mobile = "13580098899", CreateTime = DateTime.Now, Status = 1, UpdateTime = DateTime.Now });

           // var count = await HttpClientHelper.DeleteAsync(folderName, "users", 1);

            return Content(count);
        }
     
    }
}
