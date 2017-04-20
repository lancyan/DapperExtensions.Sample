using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Test.Cache
{
    public class MemCachedExtensions
    {
        public static List<string> GetAllKeys(string ipString = "127.0.0.1", int port = 11211)
        {
            List<string> allKeys = new List<string>();

            using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
            {
                socket.Connect(new IPEndPoint(IPAddress.Parse(ipString), port));
                var slabIdIter = QuerySlabId(socket);
                var keyIter = QueryKeys(socket, slabIdIter);

                foreach (String key in keyIter)
                {
                    if (!allKeys.Contains(key))
                        allKeys.Add(key);
                }
            }
            return allKeys;
        }


        /// <summary> 
        /// 查询slabId 
        /// </summary> 
        /// <param name="socket"> 套接字 </param> 
        /// <returns> slabId遍历器 </returns> 
        static IEnumerable<String> QuerySlabId(Socket socket)
        {
            var command = " stats items STAT items:0:number 0 \r\n ";
            var contentAsString = ExecuteScalarAsString(socket, command);
            return ParseStatsItems(contentAsString);
        }
        /// <summary> 
        /// 执行返回字符串标量 
        /// </summary> 
        /// <param name="socket"> 套接字 </param> 
        /// <param name="command"> 命令 </param> 
        /// <returns> 执行结果 </returns> 
        static String ExecuteScalarAsString(Socket socket, String command)
        {
            var sendNumOfBytes = socket.Send(Encoding.UTF8.GetBytes(command));
            var bufferSize = 0x1000;
            var buffer = new Byte[bufferSize];
            var readNumOfBytes = 0;
            var sb = new StringBuilder();

            while (true)
            {
                readNumOfBytes = socket.Receive(buffer);
                sb.Append(Encoding.UTF8.GetString(buffer));

                if (readNumOfBytes < bufferSize)
                    break;
            }

            return sb.ToString();
        }
        /// <summary> 
        /// 查询键 
        /// </summary> 
        /// <param name="socket"> 套接字 </param> 
        /// <param name="slabIdIter"> 被查询slabId </param> 
        /// <returns> 键遍历器 </returns> 
        static IEnumerable<String> QueryKeys(Socket socket, IEnumerable<String> slabIdIter)
        {
            var keys = new List<String>();
            var cmdFmt = " stats cachedump {0} 0 \r\n ";
            var contentAsString = String.Empty;

            foreach (String slabId in slabIdIter)
            {
                contentAsString = ExecuteScalarAsString(socket, String.Format(cmdFmt, slabId));
                keys.AddRange(ParseKeys(contentAsString));
            }

            return keys;
        }
        /// <summary> 
        /// 解析stats cachedump返回键 
        /// </summary> 
        /// <param name="contentAsString"> 解析内容 </param> 
        /// <returns> 键遍历器 </returns> 
        static IEnumerable<String> ParseKeys(String contentAsString)
        {
            var keys = new List<String>();
            var separator = new string[] { "\r\n" };
            var separator2 = new char[] { ' ' };
            var prefix = "ITEM";
            var items = contentAsString.Split(separator, StringSplitOptions.RemoveEmptyEntries);

            foreach (var item in items)
            {
                var itemParts = item.Split(separator2, StringSplitOptions.RemoveEmptyEntries);
                if ((itemParts.Length < 3) || !String.Equals(itemParts.FirstOrDefault(), prefix, StringComparison.OrdinalIgnoreCase))
                    continue;

                keys.Add(itemParts[1]);
            }

            return keys;
        }
        /// <summary> 
        /// 解析STAT items返回slabId 
        /// </summary> 
        /// <param name="contentAsString"> 解析内容 </param> 
        /// <returns> slabId遍历器 </returns> 
        static IEnumerable<String> ParseStatsItems(String contentAsString)
        {
            var slabIds = new List<String>();
            var separator = new string[] { "\r\n" };
            var separator2 = new char[] { ':' };
            var items = contentAsString.Split(separator, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0, len = items.Length; i < len; i += 4)
            {
                var itemParts = items[i].Split(separator2, StringSplitOptions.RemoveEmptyEntries);
                if (itemParts.Length < 3)
                    continue;
                slabIds.Add(itemParts[1]);
            }

            return slabIds;
        }
    }
}
