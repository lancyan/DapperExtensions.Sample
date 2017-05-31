using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using Test.BLL;
using Test.Entity.SYS;
using Test.Utility;
using WebApi.OutputCache.V2;

namespace Test.API.Controllers.Test
{
    public class RolesController : ApiController
    {

        /// <summary>
        /// 获取菜单列表
        /// </summary>
        /// <param name="user">用户对象</param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public IEnumerable<Roles> GetList(string where, string orderBy, int pageIndex, int pageSize)
        {
            RolesBLL bll = new RolesBLL();
            string decodeWhere = Common.Base64ToString(where);
            string newWhere = Common.Where2Query<Roles>(decodeWhere);
            var list = bll.GetList(newWhere, orderBy, pageIndex, pageSize);
            return list;
        }


        /// <summary>
        /// 获取数量
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public int GetCount(string where)
        {
            UsersBLL bll = new UsersBLL();
            string decodeWhere = Common.Base64ToString(where);
            string newWhere = Common.Where2Query<Roles>(decodeWhere);
            return bll.Count(newWhere);
        }

        /// <summary>
        /// 获取角色
        /// </summary>
        /// <param name="id">角色编号</param>
        /// <returns></returns>
        public Roles Get(int id)
        {
            RolesBLL bll = new RolesBLL();
            return bll.Get(id);
        }

        /// <summary>
        /// 获取用户的角色
        /// </summary>
        /// <param name="userId">userId</param>
        /// <returns></returns>
        public string GetUserRoles(string userId)
        {
            UserRolesBLL bll = new UserRolesBLL();
            var list = bll.GetList(string.Format("UserId={0}", userId));

            var result = string.Join(",", list.Select(p => p.RoleId).ToArray());
            return result;
        }

        /// <summary>
        /// 获取角色
        /// </summary>
        /// <returns></returns>
        public string GetRoles()
        {
            StringBuilder sb = new StringBuilder();
            //string[] userRoles = UserData.Roles;
            //int maxRole = userRoles.Min();

            List<Roles> listMenu = new RolesBLL().GetList().ToList();

            sb.Append("[");
            LoopRole(listMenu, listMenu.FindAll(p => p.ParentId == 0 || p.ParentId == null), sb);
            sb.Append("]");
            return sb.ToString();
        }


        private void LoopRole(List<Roles> listAll, List<Roles> listChild, StringBuilder sb)
        {
            for (int i = 0, len = listChild.Count; i < len; i++)
            {
                Roles m = listChild[i];
                sb.Append(string.Format("{{\"id\":\"{0}\",\"text\":\"{1}\",\"checked\":{2},\"iconCls\":\"\"", m.Id, m.Name, "false"));
                var listTemp = listAll.FindAll(p => p.ParentId == m.Id);
                if (listTemp.Count > 0)
                {
                    sb.Append(",\"children\": [");
                    LoopRole(listAll, listTemp, sb);
                    sb.Append("]");
                }

                if (i < len - 1)
                {
                    sb.Append("},");
                }
                else
                {
                    sb.Append("}");
                }
            }
        }


        /// <summary>
        /// Post提交一个Roles对象
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool Post([FromBody]dynamic obj)
        {
            // {
            //    "id": 1,
            //    "serial": "aa",
            //    "name": "tom",
            //    "price": 100,
            //    "uid": 10
            //}
            Roles entity = (obj.model as JObject).ToObject<Roles>();
            string menuIds = obj.menuIds;
            RolesBLL bll = new RolesBLL();
            List<int> listMenuId = string.IsNullOrWhiteSpace(menuIds) ? new List<int>() : menuIds.Split(',').Select(x => int.Parse(x)).ToList();

            return bll.Save(entity, listMenuId);
        }

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="id">角色编号</param>
        /// <returns></returns>
        [HttpDelete]
        public bool Delete(int id)
        {
            RolesBLL bll = new RolesBLL();
            return bll.Delete(id);
        }

        /// <summary>
        /// 判断权限
        /// </summary>
        /// <param name="roleId">roleId</param>
        /// <param name="controllerName">controllerName</param>
        /// <param name="actionName">actionName</param>
        /// <returns></returns>
        [CacheOutput(ClientTimeSpan = 60, ServerTimeSpan = 60)]
        public bool IsPermission(dynamic obj)
        {
            string r = obj.r;
            string c = obj.c;
            string a = obj.a;
            RolesBLL bll = new RolesBLL();
            return bll.IsPermission(r, c, a);
        }


    }
}
