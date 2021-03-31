using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YuktiSolutions.SmartERP.Areas.Sales.Models;
using YuktiSolutions.SmartERP.Areas.Sales.Models.Database;
using YuktiSolutions.SmartERP.Areas.Sales.Models.UI;

namespace YuktiSolutions.SmartERP.Areas.Sales.Controllers
{
    public class HomeController : Controller
    {
       public SalesDbContext context = new SalesDbContext();
        // GET: Sales/Home
        public ActionResult Index( ActivityModel activity)
        {
            var record = context.LogingActivities.ToList();
            return View(record);
         }


        public ActionResult Task_Read([DataSourceRequest] DataSourceRequest request)
        {
            
            return Json(ActivityManager.GetActivities().ToDataSourceResult(request));
 
        }
        public ActionResult Related_Event_Read([DataSourceRequest] DataSourceRequest request)
        {
           
                return Json(ActivityManager.GetRelatedAccounts().ToDataSourceResult(request));
           
        }
        public ActionResult Event_Read([DataSourceRequest] DataSourceRequest request)
        {
            return Json(ActivityManager.GetActivities().ToDataSourceResult(request));

        }
        public ActionResult Email_Read([DataSourceRequest] DataSourceRequest request)
        {
            return Json(ActivityManager.GetActivities().ToDataSourceResult(request));

        }

        public ActionResult Recent_Contect_Read([DataSourceRequest] DataSourceRequest request, ActivityModel activity)
        
            {
                return Json(ActivityManager.GetActivities().ToDataSourceResult(request));
            }

        

        public ActionResult Call_Read([DataSourceRequest] DataSourceRequest request)
        {
            return Json(ActivityManager.GetActivities().ToDataSourceResult(request));

        }

        public ActionResult AccountsByCountries_Read()
        {
            return Json(AccountManager.GetAccountsByCountries());
        }
       
       
    }
}