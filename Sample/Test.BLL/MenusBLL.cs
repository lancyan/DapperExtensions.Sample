using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.DAL;
using Test.Entity.SYS;

namespace Test.BLL
{
    public class MenusBLL
    {
        MenusDAL dal = new MenusDAL();


        # region 构造函数
        /// <summary>
        ///MenusBLL Menus实体
        /// </summary>
        public MenusBLL()
        {
        }
        #endregion

        #region 添加
        public int Insert(Menus model)
        {
            return dal.Insert(model);
        }
        #endregion

        #region 修改
        public bool Update(Menus model)
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
        public bool Save(Menus entity)
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
        public Menus Get(Int32 id)
        {
            return dal.Get(id);
        }

        #endregion

        #region 查询
        public List<Menus> GetList(string where = "", string orderBy = "", string items = "*")
        {
            return dal.Where(where, orderBy).ToList();
        }
        #endregion

        #region 分页查询
        public List<Menus> GetList(string where, string orderBy, int pageIndex, int pageSize)
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



        public bool BatchAdd(int parentId, string parentName, string code, dynamic actions)
        {
            return dal.BatchAdd(parentId, parentName, code, actions);
        }
    }
}
