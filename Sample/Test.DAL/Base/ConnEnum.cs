using DapperExtensions.Sql;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.DAL.Base
{
    /// <summary>
    /// 连接串枚举类，方便扩展多数据源
    /// </summary>
    public class ConnEnum
    {
        public static List<ConnectionItem> lists = new List<ConnectionItem>();

        /// <summary>
        /// State 0为读，1为写，2为读写
        /// Flag  0为SQLServer，1为MySQL，2为SQLite,若GroupName相同则可以进行读写分离访问的控制
        /// </summary>
        static ConnEnum()
        {
            lists.Add(new ConnectionItem() { GroupName = connGroupName1, Name = "SQLServerTest", State = 0, Type = 0 }); //读库 -SQLServer
            lists.Add(new ConnectionItem() { GroupName = connGroupName1, Name = "SQLServerTest", State = 1, Type = 0 }); //写库 -SQLServer

            //======================================================================================================================

            lists.Add(new ConnectionItem() { GroupName = connGroupName2, Name = "MySqlTest", State = 2, Type = 1 });  //读写库 -MySQL
            lists.Add(new ConnectionItem() { GroupName = connGroupName3, Name = "SQLiteTest", State = 2, Type = 2 });  //读写库 -SQLite
        }

        public static string connGroupName1 = "Test1";
        public static string connGroupName2 = "Test2";
        public static string connGroupName3 = "Test3";

    }

}
