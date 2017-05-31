using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.DAL;
using Test.Entity.SYS;

namespace Test.BLL
{
    public class RolesBLL
    {

        RolesDAL dal = new RolesDAL();


        # region 构造函数
        /// <summary>
        ///RolesBLL Roles实体
        /// </summary>
        public RolesBLL()
        {
        }
        #endregion

        #region 添加
        public int Insert(Roles model)
        {
            return dal.Insert(model);
        }
        #endregion

        #region 修改
        public bool Update(Roles model)
        {
            return dal.Update(model);
        }
        #endregion

        #region 删除
        public bool Delete(Int32 id)
        {
            return dal.DeleteById(id);
        }
        #endregion

        #region 批量删除
        public bool Deletes(string idlist)
        {
            return dal.Deletes(idlist);
        }
        #endregion

        #region 保存
        public bool Save(Roles entity)
        {
            if (entity.Id == 0)
            {
                return dal.Insert(entity) > 0;
            }
            else
            {
                return dal.Update(entity);
            }
        }
        #endregion

        #region 查询一条记录
        public Roles Get(Int32 id)
        {
            return dal.Get(id);
        }
  
        #endregion

        #region 查询
        public IEnumerable<Roles> GetList(string where = "", string orderBy = "")
        {
            return dal.Where(where, orderBy);
        }
        #endregion

        #region 分页查询
        public IEnumerable<Roles> GetList( string where , string orderBy,int pageIndex, int pageSize)
        {
            return dal.Where(where, orderBy,pageIndex, pageSize);
        }
        #endregion

        #region 记录数
        public int GetCount(string where = "")
        {
            return dal.Count(where);
        }
        #endregion

        public bool Save(Roles model,List<int> menuIds)
        {
            return dal.Save(model, menuIds);
        }


        public bool IsPermission(string roleIds, string controllerName, string actionName)
        {
            string sql = string.Format("select count(*) from roleMenus where roleId in({0}) and menuId in(select id from menus where code='{1}' and action='{2}')", roleIds, controllerName, actionName);
            ///有待优化
            return dal.Query(sql).Count() > 0;
        }
    }
}
