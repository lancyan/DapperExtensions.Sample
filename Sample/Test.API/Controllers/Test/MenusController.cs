using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using Test.API.Filters;
using Test.BLL;
using Test.Entity;
using Test.Entity.SYS;
using Test.Utility;

namespace Test.API.Controllers.Test
{
    public class MenusController : ApiController
    {
        //[CacheOutput(ClientTimeSpan = 60, ServerTimeSpan = 60)]
        //[HttpGet]
        /// <summary>
        /// 获取菜单列表
        /// </summary>
        /// <param name="user">用户对象</param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public IEnumerable<Menus> GetList(string where, string orderBy, int pageIndex, int pageSize)
        {
            MenusBLL bll = new MenusBLL();
            string decodeWhere = Common.Base64ToString(where);
            string newWhere = Common.Where2Query<Menus>(decodeWhere);
            return bll.GetList(newWhere, orderBy, pageIndex, pageSize);
        }



        /// <summary>
        /// 通过角色获取菜单
        /// </summary>
        /// <param name="roleId">角色编号</param>
        /// <returns></returns>
        public HttpResponseMessage GetMenus(string roleId)
        {
            StringBuilder sb = new StringBuilder();
            List<Menus> listMenu = new MenusBLL().GetList();
            List<RoleMenus> listRoleRights = new RoleMenusBLL().GetList(string.Format("roleId in({0})", roleId.Trim()));
            sb.Append("[");
            LoopMenu(listMenu, listMenu.FindAll(p => p.ParentId == null || p.ParentId == 0), sb, listRoleRights);
            sb.Append("]");
            return new HttpResponseMessage { Content = new StringContent(sb.ToString(), System.Text.Encoding.UTF8, "application/json") };
        }


        private void LoopMenu(List<Menus> listAll, List<Menus> listChild, StringBuilder sb, List<RoleMenus> listRoleRights)
        {
            for (int i = 0, len = listChild.Count; i < len; i++)
            {
                Menus m = listChild[i];
                RoleMenus rr = listRoleRights.Find(p => p.MenuId == m.Id);
                if (rr != null)
                {
                    sb.Append(string.Format("{{\"id\":\"{0}\",\"text\":\"{1}\",\"checked\":{2},\"iconCls\":\"{3}\",\"attributes\":{{\"url\":\"{4}\",\"code\":\"{5}\"}}", m.Id, m.Name, "true", m.Ico, m.Url, m.Code));
                }
                else
                {
                    sb.Append(string.Format("{{\"id\":\"{0}\",\"text\":\"{1}\",\"checked\":{2},\"iconCls\":\"{3}\",\"attributes\":{{\"url\":\"{4}\",\"code\":\"{5}\"}}", m.Id, m.Name, "false", m.Ico, m.Url, m.Code));
                }
                var listTemp = listAll.FindAll(p => p.ParentId == m.Id);
                if (listTemp.Count > 0)
                {
                    sb.Append(",\"children\": [");
                    LoopMenu(listAll, listTemp, sb, listRoleRights);
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
        /// 通过角色编号获取左菜单
        /// </summary>
        /// <param name="roleId">角色编号</param>
        /// <returns></returns>
        public HttpResponseMessage GetLeftMenus(string roleId)
        {
            StringBuilder sb = new StringBuilder();
            //可见菜单
            List<Menus> listMenu = new MenusBLL().GetList("Status=1");
            List<RoleMenus> listRoleRights = new RoleMenusBLL().GetList(string.Format("roleId in({0})", roleId.Trim()));
            //linq
            List<int> listMenuIds = (from s in listRoleRights select new { s.MenuId }).Select(s => s.MenuId).ToList();

            listMenu = listMenu.FindAll(p => listMenuIds.Contains(p.Id));
            sb.Append("[");
            LoopLeftMenu(listMenu, listMenu.FindAll(p => p.ParentId == null || p.ParentId == 0), sb);
            sb.Append("]");
            return new HttpResponseMessage { Content = new StringContent(sb.ToString(), System.Text.Encoding.UTF8, "application/json") };
        }



        private void LoopLeftMenu(List<Menus> listAll, List<Menus> listChild, StringBuilder sb)
        {
            for (int i = 0, len = listChild.Count; i < len; i++)
            {
                Menus m = listChild[i];
                sb.Append(string.Format("{{\"id\":\"{0}\",\"name\":\"{1}\",\"url\":\"{2}\",\"icon\":\"{3}\"", m.Id, m.Name, m.Url, m.Ico));
                var listTemp = listAll.FindAll(p => p.ParentId == m.Id);
                if (listTemp.Count > 0)
                {
                    sb.Append(",\"children\": [");
                    LoopLeftMenu(listAll, listTemp, sb);
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
        /// 获取菜单
        /// </summary>
        /// <param name="id">菜单编号</param>
        /// <returns></returns>
        public Menus Get(int id)
        {
            return new MenusBLL().Get(id);
        }

        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="id">菜单编号</param>
        /// <returns></returns>
        [HttpDelete]
        public bool Delete(int id)
        {
            return new RoleMenusBLL().DeleteMenus(id);
        }

        /// <summary>
        /// Post提交一个Menus对象
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        //[ApiActionFilter]
        public bool Post([FromBody]Menus entity)
        {
            // {
            //    "id": 1,
            //    "serial": "aa",
            //    "name": "tom",
            //    "price": 100,
            //    "uid": 10
            //}
            MenusBLL bll = new MenusBLL();
            if (entity.Id == 0)
            {
                int pid = entity.ParentId ?? 0;
                Menus p = bll.Get(pid);
                if (entity.Type == p.Type)
                {
                    return false;
                }
                return bll.Insert(entity) > 0;
            }
            else
            {
                return bll.Update(entity);
            }
        }

        [HttpPost]
        public bool BatchAdd(dynamic obj)
        {
            int parentId = obj.parentId;
            string parentName = obj.parentName;
            string code = obj.code;
            dynamic actions = obj.actions;
            MenusBLL bll = new MenusBLL();
            return bll.BatchAdd(parentId, parentName, code, actions);
        }
    }
}
