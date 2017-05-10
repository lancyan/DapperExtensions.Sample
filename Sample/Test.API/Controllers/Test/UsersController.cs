
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
    /// <summary>
    /// 用户api列表
    /// </summary>
    public class UsersController : ApiController
    {
        //注意post参数不能是多个对象参数
        //http://www.cnblogs.com/Juvy/p/3903974.html
        //[ApiActionFilter]
        /// <summary>
        /// 获取一个用户通过id
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>User对象</returns>
        public Users Get(int id)
        {
            UsersBLL bll = new UsersBLL();
            return bll.Get(id);
        }

        /// <summary>
        /// 获取用户名
        /// </summary>
        /// <returns></returns>
        public IEnumerable<string> GetUserName()
        {
            return new string[] { "Test1", "Test2" };
        }

       
        //[CacheOutput(ClientTimeSpan = 60, ServerTimeSpan = 60)]
         //[HttpGet]
        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <param name="user">用户对象</param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public IEnumerable<Users> GetList([FromUri]Users user, int pageIndex, int pageSize)
        {
            UsersBLL bll = new UsersBLL();

            return bll.Where(string.Format("userName='{0}'" , user.UserName), "id", pageIndex, pageSize);

        }

        //[HttpGet]
        //[CacheOutput(ClientTimeSpan = 60, ServerTimeSpan = 60)]
        /// <summary>
        /// 获取数量
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public int GetCount(string userName)
        {
            UsersBLL bll = new UsersBLL();

            return bll.Count(string.Format("userName='{0}'", userName));

        }


        /// <summary>
        /// 获取所有的用户信息
        /// </summary>
        /// <returns></returns>
        //[CacheOutput(ClientTimeSpan = 60, ServerTimeSpan = 60)]
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

        /// <summary>
        /// Post提交一个User对象
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
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
        /// <summary>
        /// 删除一个User通过id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public bool Delete(int id)
        {
            UsersBLL bll = new UsersBLL();
            return bll.Delete(id);
        }

        
        //[IgnoreCacheOutput]
        //[CacheOutput(ClientTimeSpan = 60, ServerTimeSpan = 60, AnonymousOnly = true)]
        /// <summary>
        /// 获取时间
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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
