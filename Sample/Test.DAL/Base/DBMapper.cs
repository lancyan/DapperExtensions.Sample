using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DapperExtensions;
using DapperExtensions.Mapper;
using DapperExtensions.Sql;
using System.Reflection;
using System.Data.SqlClient;

namespace Test.DAL.Base
{
    public class DBMapper
    {
        //private volatile static DBMapper _instance = null;
        //private static readonly object lockHelper = new object();
        //private DBMapper()
        //{
        //}
        //public static DBMapper CreateInstance()
        //{
        //    if (_instance == null)
        //    {
        //        lock (lockHelper)
        //        {
        //            if (_instance == null)
        //            {
        //                _instance = new DBMapper();
        //            }
        //        }
        //    }
        //    return _instance;
        //}

        public static ConcurrentDictionary<string, List<Database>> dict = new ConcurrentDictionary<string, List<Database>>();

        public static void InitDB(string connGroupName, ref List<Database> readDB, ref List<Database> writeDB, ref List<Database> readWriteDB)
        {
            if (string.IsNullOrEmpty(connGroupName))
            {
                connGroupName = ConnEnum.lists[0].GroupName;
            }

            var list = ConnEnum.lists.Where(p => p.GroupName == connGroupName);

            InitDB(connGroupName, list, 0, ref readDB);
            InitDB(connGroupName, list, 1, ref writeDB);
            InitDB(connGroupName, list, 2, ref readWriteDB);

        }

        private static void InitDB(string connGroupName, IEnumerable<ConnectionItem> list, int state, ref List<Database> db)
        {
            string key = connGroupName + "_" + state;
            if (dict.ContainsKey(key))
            {
                db = dict[key];
            }
            else
            {
                var tlist = list.Where(p => p.State == state);
                try
                {
                    foreach (var item in tlist)
                    {
                        var cfg = new DapperExtensionsConfiguration(typeof(AutoClassMapper<>), new List<Assembly>(), item.dBase);
                        var tdb = new Database(item.Connection, new SqlGeneratorImpl(cfg));
                        db.Add(tdb);
                    }
                }
                catch (SqlException ex)
                {

                }
                if (db.Count > 0)
                {
                    dict.TryAdd(key, db);
                }
            }
        }
    }
}
