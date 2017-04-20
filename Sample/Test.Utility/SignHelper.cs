
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Test.Utility
{
    public static class SignHelper
    {
        public const string PlatformId = "platformId";
        public const string Token = "token";
        public const string Sign = "sign";


        public static bool IsPassVerify(NameValueCollection dict1, Dictionary<string, object> dict2)
        {
            bool re = false;
            string platformId = GetSign(SignHelper.PlatformId, dict1, dict2);
            string token = GetSign(SignHelper.Token, dict1, dict2);
            //string platformValue = GetSign(SignHelper.PlatformId, nvc, dict);
            string token2 = "";
            switch (platformId)
            {
                case "1":
                    token2 = EncryptHelper.Md5(Platform.Find(platformId) + "!#%&(" + DateTime.Now.ToString("yyyy/M/d"), 32, System.Text.Encoding.UTF8);
                    re = token2.Equals(token, StringComparison.OrdinalIgnoreCase) && !string.IsNullOrEmpty(token);
                    break;
                case "2":
                    token2 = EncryptHelper.Md5(Platform.Find(platformId) + "@$^*" + DateTime.Now.ToString("yyyy/M/d"), 32, System.Text.Encoding.UTF8);
                    re = token2.Equals(token, StringComparison.OrdinalIgnoreCase) && !string.IsNullOrEmpty(token);
                    break;

            }
            return re;
        }

        private static string GetSign(string key, NameValueCollection dict1, Dictionary<string, object> dict2)
        {
            string val = "";

            if (dict1.Count > 0)
            {
                if (dict1[key] != null)
                {
                    val = dict1[key];
                    return val;
                }

            }
            if (dict2.Count > 0)
            {
                if (dict2[key] != null)
                {
                    val = (dict2[key]).ToString();
                    return val;
                }
            }
            return val;
        }

    }


    public class Platform
    {
        public static Dictionary<string, string> dict = new Dictionary<string, string>();


        static Platform()
        {
            dict.Add("1", "D24E0AEF6ED947359EE92E068E38F518");
            dict.Add("2", "4AF86EBAAA3D420ABB48B4103B1C3531");
        }

        public static string Find(string id)
        {
            return dict[id];
        }

    }

    //public class PlatformItem
    //{
    //    public string id;
    //    public string hash;
    //}
}
