using ServiceStack.Redis;
using ServiceStack.Redis.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.OutputCache.Core.Cache;

namespace Test.Cache
{
    public class RedisProvider : IApiOutputCache, IDisposable
    {
        public static RedisClient client;
        private string[] readHosts = System.Configuration.ConfigurationManager.AppSettings["cacheReadHosts"].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
        private string[] writeHosts = System.Configuration.ConfigurationManager.AppSettings["cacheWriteHosts"].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

        //默认缓存过期时间单位秒
        public int secondsTimeOut = 30 * 60;


        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="OpenPooledRedis">是否开启缓冲池</param>
        public RedisProvider(bool OpenPooledRedis = true)
        {
            PooledRedisClientManager prcm = new PooledRedisClientManager(writeHosts, readHosts,
                new RedisClientManagerConfig
                {
                    MaxWritePoolSize = writeHosts.Length * 5,
                    MaxReadPoolSize = readHosts.Length * 5,
                    AutoStart = true
                });
            client = prcm.GetClient() as RedisClient;
        }

        #region Key/Value存储
        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">缓存建</param>
        /// <param name="t">缓存值</param>
        /// <param name="timeout">过期时间，单位秒,-1：不过期，0：默认过期时间</param>
        /// <returns></returns>
        public bool Set<T>(string key, T t, int timeout = 0)
        {
            if (timeout >= 0)
            {
                if (timeout > 0)
                {
                    secondsTimeOut = timeout;
                }
                client.Expire(key, secondsTimeOut);
            }

            return client.Add<T>(key, t);
        }

        /// <summary>
        /// 显示调用接口
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        T IApiOutputCache.Get<T>(string key)
        {
            return client.Get<T>(key);
        }

        public object Get(string key)
        {
            return client.Get(key);
        }
        public void Add(string key, object o, DateTimeOffset expiration, string dependsOnKey = null)
        {
            TimeSpan ts = expiration.Subtract(DateTimeOffset.Now);
            client.Add(key, o, ts);
        }
        public bool Add<T>(string key, T t, int timeout)
        {
            if (timeout >= 0)
            {
                if (timeout > 0)
                {
                    secondsTimeOut = timeout;
                }
                client.Expire(key, secondsTimeOut);
            }
            return client.Add<T>(key, t);
        }
        #endregion

        #region 链表操作
        /// <summary>
        /// 根据IEnumerable数据添加链表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="listId"></param>
        /// <param name="values"></param>
        /// <param name="timeout"></param>
        public void AddList<T>(string listId, IList<T> values, int timeout = 0)
        {
            client.Expire(listId, 60);
            IRedisTypedClient<T> iredisClient = client.As<T>();
            if (timeout >= 0)
            {
                if (timeout > 0)
                {
                    secondsTimeOut = timeout;
                }
                client.Expire(listId, secondsTimeOut);
            }
            var redisList = iredisClient.Lists[listId];
            redisList.AddRange(values);
            iredisClient.Save();
        }
        /// <summary>
        /// 添加单个实体到链表中
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="listId"></param>
        /// <param name="Item"></param>
        /// <param name="timeout"></param>
        public void AddEntityToList<T>(string listId, T Item, int timeout = 0)
        {
            IRedisTypedClient<T> iredisClient = client.As<T>();
            if (timeout >= 0)
            {
                if (timeout > 0)
                {
                    secondsTimeOut = timeout;
                }
                client.Expire(listId, secondsTimeOut);
            }
            var redisList = iredisClient.Lists[listId];
            redisList.Add(Item);
            iredisClient.Save();
        }
        /// <summary>
        /// 获取链表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="listId"></param>
        /// <returns></returns>
        public IList<T> GetList<T>(string listId)
        {
            IRedisTypedClient<T> iredisClient = client.As<T>();
            return iredisClient.Lists[listId];
        }
        /// <summary>
        /// 在链表中删除单个实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="listId"></param>
        /// <param name="t"></param>
        public void RemoveEntityFromList<T>(string listId, T t)
        {
            IRedisTypedClient<T> iredisClient = client.As<T>();
            var redisList = iredisClient.Lists[listId];
            redisList.RemoveValue(t);
            iredisClient.Save();
        }
        /// <summary>
        /// 根据lambada表达式删除符合条件的实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="listId"></param>
        /// <param name="func"></param>
        public void RemoveEntityFromList<T>(string listId, Func<T, bool> func)
        {
            IRedisTypedClient<T> iredisClient = client.As<T>();

            var redisList = iredisClient.Lists[listId];
            T value = redisList.Where(func).FirstOrDefault();
            redisList.RemoveValue(value);
            iredisClient.Save();
        }
        #endregion

        //释放资源
        public void Dispose()
        {
            if (client != null)
            {
                client.Dispose();
            }
            GC.Collect();
        }

        public void RemoveStartsWith(string key)
        {
            client.Remove(key);
        }



        public void Remove(string key)
        {
            client.Remove(key);
        }

        public bool Contains(string key)
        {
            return client.ContainsKey(key);
        }



        public IEnumerable<string> AllKeys
        {
            get { return client.GetAllKeys(); }
        }
    }
}
