using System.Web;
using System.Web.Mvc;

namespace LightCore.Web.Mvc4.IntegrationSample
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}