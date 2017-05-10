using Test.Utility;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
//using Newtonsoft.Json.Linq;
using System.Text;
using System.Diagnostics;
using Newtonsoft.Json.Linq;

namespace Test.API.Filters
{
    /// <summary>  
    /// WebAPI防篡改签名验证抽象基类Attribute  
    /// </summary>  
    public class ApiAuthenticationFilter : ActionFilterAttribute
    {
        LogWriter log = new LogWriter(System.Configuration.ConfigurationManager.AppSettings["logPath"].ToString());
        StringBuilder sb = new StringBuilder();
        /// <summary>  
        /// Occurs before the action method is invoked.  
        /// </summary>  
        /// <param name="actionContext">The action context</param>  
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            GetTimer(actionContext, "action").Start();
            sb.Clear();
            var request = ((HttpContextWrapper)actionContext.Request.Properties["MS_HttpContext"]).Request;
            var qs = request.QueryString;
            string hm = request.HttpMethod;
            var dict = actionContext.ActionArguments;

            if (!String.IsNullOrWhiteSpace(request.Headers[SignHelper.PlatformId]))
            {
                dict.Add(SignHelper.PlatformId, request.Headers[SignHelper.PlatformId]);
            }
            if (!String.IsNullOrWhiteSpace(request.Headers[SignHelper.Token]))
            {
                dict.Add(SignHelper.Token, request.Headers[SignHelper.Token]);
            }

            sb.AppendLine(DateTime.Now.ToString() + "\r\n" + hm + "： " + request.Url.AbsoluteUri);
            if (qs == null || qs.Count == 0)
            {
                #region 让API调用支持Id=1&Name=test类似的POST提交格式
                string req = "";
                try
                {
                    if (request.ContentType.IndexOf("x-www-form-urlencoded") != -1)
                    {
                        req = HttpUtility.UrlDecode(request.Form.ToString()).Trim();
                    }
                    else
                    {
                        byte[] buffer = new byte[request.InputStream.Length];
                        request.InputStream.Read(buffer, 0, buffer.Length);
                        req = System.Text.Encoding.UTF8.GetString(buffer).Trim();
                    }
                    sb.AppendLine("参数： " + req);
                }
                catch (Exception ex)
                {
                    sb.AppendLine(DateTime.Now.ToString() + " URL： " + request.Url.AbsoluteUri + "  Exception: " + ex.Message);
                }

                #region JSON格式参数解析成nameValueCollection
                JObject jo = null;
                if (request.ContentType.IndexOf("application/json") != -1)
                {
                    try
                    {
                        if (req.StartsWith("{") && req.EndsWith("}"))
                        {
                            jo = JObject.Parse(req);
                        }
                    }
                    catch
                    {
                        sb.AppendLine("JSON格式非法！");
                    }
                }

                qs = (jo == null) ? HttpUtility.ParseQueryString(req) : jo.ToNameValueCollection();

                #endregion

                #region 给非JSON格式参数的请求赋值
                if (dict.Count > 0)
                {
                    var type = ((actionContext.ActionDescriptor).ActionBinding.ParameterBindings[0]).Descriptor.ParameterType;
                    for (int i = 0, count = dict.Count; i < count; i++)
                    {
                        var kvp = dict.ElementAt(i);
                        string key = kvp.Key;
                        if (kvp.Value == null)
                        {
                            var val = qs[key];
                            if (val == null && !Common.IsBaseType(type))
                            {
                                dict[key] = Common.NameValue2Object(qs, type);
                            }
                            else
                            {
                                dict[key] = Common.HackType(val, type);
                            }
                            break;
                        }
                    }
                }
                #endregion

                #endregion
            }
            else
            {
                sb.AppendLine("参数：" + request.QueryString.ToString());
            }

            base.OnActionExecuting(actionContext);
            return;

            if (SignHelper.IsPassVerify(qs, dict))
            {
                base.OnActionExecuting(actionContext);
            }
            else
            {
                ApiResultModel result = new ApiResultModel()
                {
                    Status = HttpStatusCode.Unauthorized,
                    Message = "错误的请求格式"
                };
                actionContext.Response = actionContext.Request.CreateResponse(result.Status, result);
            }

        }

        public override void OnActionExecuted(HttpActionExecutedContext filterContext)
        {
            var ac = filterContext.ActionContext;
            var actionTimer = GetTimer(ac, "action");
            actionTimer.Stop();
            sb.AppendLine(string.Format("时间：{0}ms", actionTimer.ElapsedMilliseconds));
    
            try
            {
                log.WriteLine(sb.ToString());
            }
            catch
            {
                //throw new Exception("文件写入异常");
            }

            base.OnActionExecuted(filterContext);
        }

        private Stopwatch GetTimer(HttpActionContext context, string name)
        {
            string key = "__timer__" + name;
            if (context.ActionArguments.ContainsKey(key))
            {
                return (Stopwatch)context.ActionArguments[key];
            }
            var result = new Stopwatch();
            context.ActionArguments[key] = result;
            return result;
        }
    }
}