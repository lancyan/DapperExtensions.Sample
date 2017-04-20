using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using WebApi.OutputCache.Core.Cache;

namespace Test.Cache
{
    public class CacheHelper
    {
        private static int hostType = System.Configuration.ConfigurationManager.AppSettings["cacheType"] == null ? 0 : int.Parse(System.Configuration.ConfigurationManager.AppSettings["cacheType"]);

        private static IApiOutputCache _instance = null;

        private static readonly object SynObject = new object();

        CacheHelper()
        {
        }

        public static IApiOutputCache Instance
        {
            get
            {
                if (null == _instance)
                {
                    lock (SynObject)
                    {
                        if (null == _instance)
                        {
                            switch (hostType)
                            {
                                case 0:
                                    _instance = new MemoryCacheDefault();
                                    break;
                                case 1:
                                    _instance = new RedisProvider();
                                    break;
                                case 2:
                                    _instance = new MemCachedProvider();
                                    break;
                            }
                        }
                    }
                }
                return _instance;
            }
        }
    }
}
