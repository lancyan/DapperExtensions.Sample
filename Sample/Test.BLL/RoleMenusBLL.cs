/******************************************
*
* 模块名称：
* 当前版本：1.0
* 开发人员：lancyan
* 完成时间：2017/5/17
* 版本历史：
* 
******************************************/

using System;
using System.Text;
using System.Data;
using System.Linq;
using System.Data.SqlClient;
using System.Collections.Generic;
using Test.Entity;
using Test.DAL;
using Test.Entity.SYS;
namespace Test.BLL
{
    public class RoleMenusBLL
    {
        RoleMenusDAL dal = new RoleMenusDAL();

        # region 构造函数
        /// <summary>
        ///RoleMenusBLL RoleMenus实体
        /// </summary>
        public RoleMenusBLL()
        {
        }
        #endregion

        #region 添加
        public int Insert(RoleMenus model)
        {
            return dal.Insert(model);
        }
        #endregion

        #region 修改
        public bool Update(RoleMenus model)
        {
            return dal.Update(model);
        }
        #endregion

        #region 删除
        public bool Delete(Int32 roleId, Int32 menuId)
        {
            return dal.Delete(p => p.RoleId == roleId && p.MenuId == menuId);
        }
        #endregion

        #region 查询一条记录
        public RoleMenus Get(Int32 roleId, Int32 menuId)
        {
            return dal.Where(p => p.RoleId == roleId && p.MenuId == menuId).FirstOrDefault();
        }

        #endregion

        #region 查询
        public List<RoleMenus> GetList(string where = "", string orderBy = "")
        {
            return dal.Where(where, orderBy).ToList();
        }
      
        #endregion

        #region 分页查询
        public List<RoleMenus> GetList(string where, string orderBy, int pageIndex, int pageSize)
        {
            return dal.Where(where, orderBy, pageIndex, pageSize).ToList();
        }
        #endregion

        #region 记录数
        public int GetCount(string where = "")
        {
            return dal.Count(where);
        }
        #endregion


        public bool DeleteMenus(int menuId)
        {
            return dal.DeleteMenus(menuId);
        }
    }
}
