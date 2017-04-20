using Test.DAL.Base;
using Test.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.DAL
{
    public partial class UsersDAL : BaseDAL<Users>
    {
        public UsersDAL(string connGroupName = "Test1")
            : base(connGroupName)
        {

        }

        /// <summary>
        /// 自定义事件，针对部分特殊事件,比如需要事物处理
        /// </summary>
        /// <returns></returns>
        public bool Deletes(string ids)
        {
            var db = GetDB(1);
            try
            {
                db.BeginTransaction();
                string[] idArr = ids.Split(',');
                for (int i = 0, len = idArr.Length; i < len; i++)
                {
                    db.DeleteById<Users>(idArr[i]);
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

