using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using DapperExtensions.Sql;
using System.Text.RegularExpressions;

namespace Test.DAL.Base
{
    public class ConfigHelper
    {
        public IEnumerable<ConnectionItem> ConnectItemList { get; private set; }

        //public SqlDialectBase sqlDialect { get; private set; }

        public ConfigHelper(string connGroupName = "")
        {
            ConnectItemList = GetConnectionList(connGroupName);
        }

        //private List<Database> readDB = new List<Database>();
        //private List<Database> writeDB = new List<Database>();
        //private List<Database> readWriteDB = new List<Database>();

        //protected Database db
        //{
        //    get
        //    {
        //        StackTrace st = new StackTrace();
        //        string mn = st.GetFrame(0).GetMethod().Name;
        //        return null;
        //    }
        //}


        //public static ConnectionStringSettings GetConnectionStringSettings(ConnEnum connNameEnum = 0)
        //{
        //    ConnectionStringSettingsCollection configStringCollention = ConfigurationManager.ConnectionStrings;
        //    if (configStringCollention == null || configStringCollention.Count <= 0)
        //    {
        //        throw new Exception("web.config 中无连接字符串!");
        //    }
        //    ConnectionStringSettings connSettings = null;
        //    string connName = Enum.GetName(typeof(ConnEnum), connNameEnum);
        //    if (!string.IsNullOrEmpty(connName))
        //    {
        //        connSettings = ConfigurationManager.ConnectionStrings[connName];
        //    }
        //    return connSettings;
        //}
        public static IEnumerable<ConnectionItem> GetConnectionList(string connGroupName)
        {
            IEnumerable<ConnectionItem> connList = ConnEnum.lists.Where(p => p.GroupName == connGroupName);
            //ConnectionStringSettingsCollection configStringCollention = ConfigurationManager.ConnectionStrings;
            //if (configStringCollention == null || configStringCollention.Count <= 0)
            //{
            //    throw new Exception("web.config 中无连接字符串!");
            //}
            return connList;
        }



        /// <summary>
        /// 获取Connection string
        /// </summary>
        /// <param name="connectionName"></param>
        /// <param name="isThrowExceptionIfNotExist"></param>
        /// <returns></returns>
        public static string GetConnectionString(string connectionName, bool isThrowExceptionIfNotExist = false)
        {
            string connectionString = ConfigurationManager.ConnectionStrings[connectionName].ConnectionString;
            if ((string.IsNullOrEmpty(connectionString) || connectionString.Trim().Length == 0) && isThrowExceptionIfNotExist)
            {
                throw new System.ApplicationException(string.Format("请在配置文件的connectionStrings中配置{0}的值！", connectionName));
            }
            return connectionString;
        }

        /// <summary>
        /// 获取app setting字符串
        /// </summary>
        /// <param name="keyName"></param>
        /// <param name="isThrowExceptionIfNotExist"></param>
        /// <returns></returns>
        public static string GetAppSettingValue(string keyName, bool isThrowExceptionIfNotExist = false)
        {
            string text = ConfigurationManager.AppSettings[keyName];
            if ((string.IsNullOrEmpty(text) || text.Trim().Length == 0) && isThrowExceptionIfNotExist)
            {
                throw new System.ApplicationException(string.Format("请在配置文件的appSettings中配置{0}的值！", keyName));
            }
            return text;
        }

        #region remark
        //private static readonly string ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
        //private static readonly string ProviderFactoryString = ConfigurationManager.AppSettings["DBProvider"].ToString();

        //private static DbProviderFactory df = null;
        ///// <summary>
        ///// 创建工厂提供器并且
        ///// </summary>
        //public static IDbConnection DbService()
        //{
        //    if (df == null)
        //        df = DbProviderFactories.GetFactory(ProviderFactoryString);
        //    var connection = df.CreateConnection();
        //    connection.ConnectionString = ConnectionString;
        //    connection.Open();
        //    return connection;
        //}

        ////连接池
        //private readonly static ConcurrentDictionary<ConnEnum, IDbConnection> connReadPool = new ConcurrentDictionary<ConnEnum, IDbConnection>();
        //private readonly static ConcurrentDictionary<ConnEnum, IDbConnection> connWritePool = new ConcurrentDictionary<ConnEnum, IDbConnection>();
        //ConcurrentDictionary<ConnEnum, IDbConnection> connPool = isReadOnly ? connReadPool : connWritePool;
        #endregion
    }
}
