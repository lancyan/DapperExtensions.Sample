using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.DAL.Base;
using Test.Entity.SYS;

namespace Test.DAL
{
    public partial class MenusDAL : BaseDAL<Menus>
    {
        public MenusDAL()
            : base(ConnEnum.connGroupName1)
        {

        }


        public bool BatchAdd(int parentId, string parentName, string code, dynamic actions)
        {
            var db = GetDB(1);
            try
            {
                var trans = db.BeginTransaction();
                var alllist = db.Where<Menus>(p => p.Code == code, "id");
                var list = new List<Menus>();
                for (int i = 0, len = actions.Count; i < len; i++)
                {
                    string action = actions[i];
                    if (alllist.Count(p => action.Equals(p.Action)) == 0)
                    {
                        list.Add(new Menus() { ParentId = parentId, ParentName = parentName, Type = 1, Action = action, Code = code, Name = action, Status = 0, Url = "/" + code + "/" + action, Ico = "icon-sys" });
                    }
                }
                db.Inserts(list);
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
