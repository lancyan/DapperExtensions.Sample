using Test.UI.Filter;
using System.Web;
using System.Web.Mvc;

namespace Test.UI
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new ErrorAttribute());
        }
    }
}