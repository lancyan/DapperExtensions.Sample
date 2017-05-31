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
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using Test.Entity;
using Test.DAL;
using Test.Entity.SYS;
namespace Test.BLL
{
    public class UserRolesBLL
    {
        UserRolesDAL dal = new UserRolesDAL();

        # region 构造函数
        /// <summary>
        ///UserRolesBLL UserRoles实体
        /// </summary>
        public UserRolesBLL()
        {
        }
        #endregion

        #region 添加
        public int Insert(UserRoles model)
        {
            return dal.Insert(model);
        }
        #endregion

        #region 修改
        public bool Update(UserRoles model)
        {
            return dal.Update(model);
        }
        #endregion

        #region 删除
        public bool Delete(Int32 userId, Int32 roleId)
        {
            return dal.Delete(p => p.UserId == userId && p.RoleId == roleId);
        }
        #endregion

        #region 查询一条记录
        public UserRoles Get(Int32 userId, Int32 roleId)
        {
            return dal.Where(p => p.UserId == userId && p.RoleId == roleId).FirstOrDefault();
        }

        #endregion

        #region 查询
        public IEnumerable<UserRoles> GetList(string where = "", string orderBy = "")
        {
            return dal.Where(where, orderBy);
        }
        #endregion

        #region 分页查询
        public IEnumerable<UserRoles> GetList(string where, string orderBy, int pageIndex, int pageSize)
        {
            return dal.Where(where, orderBy, pageIndex, pageSize);
        }
        #endregion

        #region 记录数
        public int GetCount(string where = "")
        {
            return dal.Count(where);
        }
        #endregion

        public bool UpdateRole(int userId, string userName, int[] roleIds)
        {
            return dal.UpdateRole(userId, userName, roleIds);
        }

    }
}
