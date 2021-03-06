using System.Web;
using System.Web.Mvc;

namespace YuktiSolutions.SmartERP
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());

#if !DEBUG
            filters.Add(new RequireHttpsAttribute());
#endif

        }
    }
}
