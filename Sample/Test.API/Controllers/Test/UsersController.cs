
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
using Test.Utility;
using Test.Entity.SYS;
//using Newtonsoft.Json;

namespace Test.API.Controllers.Test
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
         [HttpPost]
        public UserDatas UserLogin(dynamic obj)
        {
            string userName = obj.userName;
            string passWord = obj.passWord;
            UsersBLL bll = new UsersBLL();
            return bll.UserLogin(userName, passWord);
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
        public IEnumerable<Users> GetList(string where, int pageIndex, int pageSize, string orderBy)
        {
            UsersBLL bll = new UsersBLL();
            string decodeWhere = Common.Base64ToString(where);
            string newWhere = Common.Where2Query<Users>(decodeWhere);
            return bll.Where(newWhere, orderBy, pageIndex, pageSize);
        }

        //[HttpGet]
        //[CacheOutput(ClientTimeSpan = 60, ServerTimeSpan = 60)]
        /// <summary>
        /// 获取数量
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public int GetCount(string where)
        {
            UsersBLL bll = new UsersBLL();
            string decodeWhere = Common.Base64ToString(where);
            string newWhere = Common.Where2Query<Users>(decodeWhere);
            return bll.Count(newWhere);
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
            if (entity.ID == 0)
            {
                return bll.Insert(entity) > 0;
            }
            else
            {
                return bll.Update(entity);
            }
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="id">用户id</param>
        /// <param name="pwd1">原密码</param>
        /// <param name="pwd2">修改后的密码</param>
        /// <returns>是否成功</returns>
        [HttpPost]
        public bool UpdatePwd(dynamic obj)
        {
            int id = obj.id;
            string pwd1 = obj.pwd1;
            string pwd2 = obj.pwd2;
            UsersBLL bll = new UsersBLL();
            return bll.Update(new { password = pwd2 }, new { id = id, password = pwd1 });
        }

        public bool UpdateRole(dynamic obj)
        {
            int userId = obj.userId;
            string userName = obj.userName;
            int[] roleIds = obj.roleIds;
            UserRolesBLL bll = new UserRolesBLL();
            return bll.UpdateRole(userId, userName, roleIds);

        }

        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <param name="id">用户编号</param>
        /// <param name="entity">用户实体类</param>
        /// <returns>是否成功</returns>
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
