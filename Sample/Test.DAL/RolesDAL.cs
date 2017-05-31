using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.DAL.Base;
using Test.Entity.SYS;

namespace Test.DAL
{

    public partial class RolesDAL : BaseDAL<Roles>
    {
        public RolesDAL()
            : base(ConnEnum.connGroupName1)
        {

        }



        public bool Save(Roles model, List<int> listMenuId)
        {
            var db = GetDB(0);
            try
            {
                db.BeginTransaction();
                int roleId = 0;
                if (model.Id == 0)
                {
                    object obj = db.Insert(model);
                    if (obj != null)
                    {
                        roleId = Convert.ToInt32(obj);
                    }
                }
                else
                {
                    if (db.Update(model))
                    {
                        roleId = model.Id;
                    }
                }

                db.Delete<RoleMenus>(p => p.RoleId == roleId);

                foreach (int menuId in listMenuId)
                {
                    db.Insert<RoleMenus>(new RoleMenus() { RoleId = roleId, MenuId = menuId, CreateTime = DateTime.Now });
                }

                db.Commit();
                return true;
            }
            catch (Exception ex)
            {
                db.Rollback();
                throw ex;
            }
            finally
            {
                db.Dispose();
            }
        }
    }
}
