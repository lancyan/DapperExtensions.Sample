using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Test.Utility;

namespace Test.API.Filters
{
    public class AntiSQLInjectAttribute : FilterAttribute, IActionFilter
    {
        public static string[] specialArr = { "'", "\"", ">", "<", "=", "||", "|", "&", "#", "%", "/", "?", " or ", "select", "update", "insert", "delete", "declare", "exec", "drop", "create", "--" };

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            Type tn = typeof(string);

            var actionParameters = filterContext.ActionDescriptor.GetParameters();
            foreach (var p in actionParameters)
            {
                Type type = p.ParameterType;
                string pn = p.ParameterName;
                if (type.Equals(tn))
                {
                    if (filterContext.ActionParameters[pn] != null)
                    {
                        string s = filterContext.ActionParameters[pn].ToString().Trim().ToLower();
                        for (int i = 0, len = specialArr.Length; i < len; i++)
                        {
                            s = s.Replace(specialArr[i], "");
                        }
                        filterContext.ActionParameters[pn] = s;
                    }
                }
            }
        }
    }
}