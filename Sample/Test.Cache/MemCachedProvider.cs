using Enyim.Caching;
using Enyim.Caching.Configuration;
using Enyim.Caching.Memcached;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WebApi.OutputCache.Core.Cache;

namespace Test.Cache
{
    public class MemCachedProvider : IApiOutputCache, IDisposable
    {
        private static MemcachedClient client;

        private string[] readHosts = System.Configuration.ConfigurationManager.AppSettings["cacheReadHosts"].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
        private string[] writeHosts = System.Configuration.ConfigurationManager.AppSettings["cacheWriteHosts"].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
        public void Dispose()
        {
            if (client != null)
            {
                client.Dispose();
            }
            GC.Collect();
        }

        public MemCachedProvider(MemcachedProtocol protocol = MemcachedProtocol.Binary)
        {
            MemcachedClientConfiguration config = new MemcachedClientConfiguration() { Protocol = protocol };
          
            foreach (string s in readHosts)
            {
                config.AddServer(s);
            }
            foreach (string s in writeHosts)
            {
                config.AddServer(s);
            }
            client = new MemcachedClient(config);
        }

        public void Add(string key, object o, DateTimeOffset expiration, string dependsOnKey = null)
        {
            TimeSpan ts = expiration.Subtract(DateTimeOffset.Now);
            client.Store(StoreMode.Set, key, o, ts);
        }

        public IEnumerable<string> AllKeys
        {
            get
            {
                var len = readHosts.Length;
                var tasks = new Task[len];
                var listKeys = new List<string>();
                for (int i = 0; i < len; i++)
                {
                    string host = readHosts[i];
                    var arr = host.Split(':');
                    string ip = arr[0];
                    int port = Int32.Parse(arr[1]);
                    listKeys.AddRange(MemCachedExtensions.GetAllKeys(ip, port));
                }

                return listKeys;
            }
        }

        public bool Contains(string key)
        {
            return client.Get(key) != null;
        }

        public object Get(string key)
        {
            return client.Get(key);
        }
        public T Get<T>(string key)   where T : class
        {
            object data = client.Get(key);
            if (data is T) return (T)data;
            else return default(T);
        }

        public void Remove(string key)
        {
            client.Remove(key);
        }

        public void RemoveStartsWith(string key)
        {
            client.Remove(key);
        }
    }
}
