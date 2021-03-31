using System.Web.Mvc;

namespace YuktiSolutions.SmartERP.Areas.Sales
{
    public class SalesAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Sales";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Sales_default",
                "Sales/{controller}/{action}/{id}",
                new {controller="Home", action = "Index", id = UrlParameter.Optional },
                namespaces:new string[] { "YuktiSolutions.SmartERP.Areas.Sales.Controllers" }
            );
        }
    }
}