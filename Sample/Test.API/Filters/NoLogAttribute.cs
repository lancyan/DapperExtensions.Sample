using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Test.API.Filters
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true)]
    public class NoLogAttribute : Attribute
    {
    }
}