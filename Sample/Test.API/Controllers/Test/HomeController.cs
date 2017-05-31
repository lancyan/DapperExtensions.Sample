using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Test.Utility;

namespace Test.API.Controllers.Test
{
    public class HomeController : ApiController
    {
        /// <summary>
        /// 获取token
        /// </summary>
        /// <param name="appId">appId</param>
        /// <returns>token</returns>
        public string GetToken(string appId)
        {
            string token = "";
            for (int i = 0; i < SignHelper.keyArr.Length; i++)
            {
                if (SignHelper.keyArr[i].Equals(appId, StringComparison.OrdinalIgnoreCase))
                {
                    return SignHelper.valueArr[i];
                }
            }
            return token;
        }
    }
}
