using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.DAL.Base;
using Test.Entity;
using Test.Entity.SYS;

namespace Test.DAL
{
	public partial class RoleMenusDAL : BaseDAL<RoleMenus>
	{
		public RoleMenusDAL()
		 : base(ConnEnum.connGroupName1)
		{
		}



        public bool DeleteMenus(int menuId)
        {
            var db = GetDB(1);
            try
            {
                db.BeginTransaction();

                db.Delete<RoleMenus>(p => p.MenuId == menuId);

                db.DeleteById<Menus>(menuId);

                db.Commit();
                return true;
            }
            catch
            {
                db.Rollback();
            }
            finally
            {
                db.Dispose();
            }
            return false;
        }
    }
}

