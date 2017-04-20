
using Test.BLL;
using Test.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using WebApi.OutputCache.V2;
using Test.API.Filters;
using Newtonsoft.Json;
//using Newtonsoft.Json;

namespace API.Controllers
{
    //使缓存作废
    //[AutoInvalidateCacheOutput]
    //[CacheOutput(ClientTimeSpan = 60, ServerTimeSpan = 60)]
    public class UsersController : ApiController
    {
        //注意post参数不能是多个对象参数
        //http://www.cnblogs.com/Juvy/p/3903974.html
        [ApiActionFilter]
        public Users Get(int id)
        {
            UsersBLL bll = new UsersBLL();
            return bll.Get(id);
        }

        // GET api/values
        public IEnumerable<string> GetUserName()
        {
            return new string[] { "Test1", "Test2" };
        }


        /// <summary>
        /// 获取所有的用户信息
        /// </summary>
        /// <returns></returns>
        [CacheOutput(ClientTimeSpan = 60, ServerTimeSpan = 60)]
        [ApiActionFilter]
        public IEnumerable<Users> GetUsers()
        {
            UsersBLL bll = new UsersBLL();
            return bll.Where(p => p.Status != 0);
        }

        /// <summary>
        /// 获取用户购买的所有商品信息
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <returns></returns>
        [CacheOutput(ClientTimeSpan = 60, ServerTimeSpan = 60)]
        [ApiActionFilter]
        public IEnumerable<Goods> GetGoodsByUser([FromBody]string userId)
        {
            UsersBLL bll = new UsersBLL();
            return bll.GetGoodsByUser(userId);
        }

        /// <summary>
        /// 获取用户购买的所有商品信息和用户信息,返回一个自定义类型
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <returns></returns>
        [CacheOutput(ClientTimeSpan = 60, ServerTimeSpan = 60)]
        [ApiActionFilter]
        public IEnumerable<dynamic> GetBillsByUser([FromBody]string userId)
        {
            UsersBLL bll = new UsersBLL();
            return bll.GetBillsByUser(userId);
        }

        // POST api/values
        public bool Post([FromBody]Users entity)
        {
            // {
            //    "id": 1,
            //    "serial": "aa",
            //    "name": "tom",
            //    "price": 100,
            //    "uid": 10
            //}
            UsersBLL bll = new UsersBLL();
            return bll.Insert(entity) > 0;
        }

        [HttpPut]
        public bool Put(int id, [FromBody]Users entity)
        {
            UsersBLL bll = new UsersBLL();
            return bll.Update(entity, new { id = id });
        }

        //[HttpPost]
        //public bool Put(int id, [FromBody]string value)
        //{
        //    UsersBLL bll = new UsersBLL();
        //    var entity = JsonConvert.DeserializeObject<Users>(value);
        //    return bll.Update(entity, new { id = id });
        //}

        [HttpDelete]
        public bool Delete(int id)
        {
            UsersBLL bll = new UsersBLL();
            return bll.Delete(id);
        }

        //[IgnoreCacheOutput]
        [CacheOutput(ClientTimeSpan = 60, ServerTimeSpan = 60, AnonymousOnly = true)]
        [HttpGet]
        public DateTime GetTime(int id)
        {
            if (id == 1)
            {
                return DateTime.Now.AddMinutes(1);
            }
            else
            {
                return DateTime.Now;
            }
        }

    }
}
