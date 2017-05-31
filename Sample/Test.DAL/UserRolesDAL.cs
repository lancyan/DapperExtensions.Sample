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
	public partial class UserRolesDAL : BaseDAL<UserRoles>
	{
		public UserRolesDAL()
            : base(ConnEnum.connGroupName1)
		{
		}

        public bool UpdateRole(int userId, string userName, int[] roleIds)
        {
            var db = GetDB(1);
            try
            {
                db.BeginTransaction();
                db.Delete<UserRoles>(p => p.UserId == userId);
                for (int i = 0, len = roleIds.Length; i < len; i++)
                {
                    db.Insert<UserRoles>(new UserRoles() { UserId = userId, RoleId = roleIds[i], UserName = userName });
                }
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

