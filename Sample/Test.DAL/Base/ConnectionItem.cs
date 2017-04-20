using DapperExtensions.Sql;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Test.DAL.Base
{
    public class ConnectionItem
    {
        public string GroupName { get; set; }

        public string Name { get; set; }

        /// <summary>
        /// 0 SQLServer 1 MySQL
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// 0 读库 1写库 2读写库
        /// </summary>
        public int State { get; set; }

        public SqlDialectBase dBase
        {
            get
            {
                SqlDialectBase mybase = null;
                switch (Type)
                {
                    case 0:
                        //SQL Server
                        mybase = new SqlServerDialect();
                        break;
                    case 1:
                        //My SQL
                        mybase = new MySqlDialect();
                        break;
                }
                return mybase;
            }
        }

        public DbConnection Connection
        {
            get
            {
                var cs = ConfigurationManager.ConnectionStrings[Name].ConnectionString;
                DbConnection conn = null;
                switch (Type)
                {
                    case 0:
                        conn = new SqlConnection(cs);
                        break;
                    case 1:
                        conn = new MySqlConnection(cs);
                        break;

                }
                return conn;
            }
        }
    }
}
