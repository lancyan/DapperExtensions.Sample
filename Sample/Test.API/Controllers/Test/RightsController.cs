using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using Test.BLL;
using Test.Entity.SYS;

namespace Test.API.Controllers.Test
{
    public class RightsController : ApiController
    {
        /// <summary>
        /// 获取角色下的所有权限
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public HttpResponseMessage GetRights(string roleId)
        {
            StringBuilder sb = new StringBuilder();
            List<Menus> listMenu = new MenusBLL().GetList();
            List<RoleMenus> listRoleRights = new RoleMenusBLL().GetList(string.Format("roleId in({0})", roleId.Trim()));

            sb.Append("[");
            LoopMenu(listMenu, listMenu.FindAll(p => p.ParentId == null || p.ParentId == 0), sb, listRoleRights);
            sb.Append("]");

            return new HttpResponseMessage { Content = new StringContent(sb.ToString(), System.Text.Encoding.UTF8, "application/json") };
        }

        /// <summary>
        /// 编辑权限
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public string EditRight(dynamic obj)
        {
            string roleId = obj.roleId;
            string menuCode = obj.menuCode;
            string rightCode = obj.rightCode;

            return "";
        }


        private void LoopMenu(List<Menus> listAll, List<Menus> listChild, StringBuilder sb, List<RoleMenus> listRoleRights)
        {
            for (int i = 0, len = listChild.Count; i < len; i++)
            {
                Menus m = listChild[i];
                RoleMenus rr = listRoleRights.Find(p => p.MenuId == m.Id);
                string closed = "";
                //if (m.Type == 1)
                //{
                //    closed = "\"state\":\"closed\",";
                //}
                if (rr != null)
                {
                    sb.Append(string.Format("{{\"id\":\"{0}\",\"text\":\"{1}\",\"checked\":{2},\"iconCls\":\"{3}\",{4}\"attributes\":{{\"url\":\"{5}\",\"action\":\"{6}\"}}", m.Id, m.Name, "true", m.Ico, closed, m.Url, m.Action));
                }
                else
                {
                    sb.Append(string.Format("{{\"id\":\"{0}\",\"text\":\"{1}\",\"checked\":{2},\"iconCls\":\"{3}\",{4}\"attributes\":{{\"url\":\"{5}\",\"action\":\"{6}\"}}", m.Id, m.Name, "false", m.Ico, closed, m.Url, m.Action));
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
    }
}
