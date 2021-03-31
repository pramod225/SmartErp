using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNet.Identity;
using YuktiSolutions.SmartERP.Areas.Sales.Models;
using YuktiSolutions.SmartERP.Areas.Sales.Models.Database;
using YuktiSolutions.SmartERP.Areas.Sales.Models.UI;

namespace YuktiSolutions.SmartERP.Areas.Sales.Controllers
{
    [Authorize]
    public class OpportunityController : Controller
    {
        // GET: Sales/Opportunity
        public ActionResult Index()
        {
            return View(new OpportunityModel() {
                ID = Guid.NewGuid()
            });
        }
       
        public ActionResult Opportunity_Read([DataSourceRequest] DataSourceRequest request, string searchText)
        {
            if (String.IsNullOrEmpty(searchText))
            {
                return Json(OpportunityManager.GetOpportunities().ToDataSourceResult(request));
                
            }
            else
            {
                var userId = Guid.Parse(User.Identity.GetUserId());
                if (User.IsInRole("Admin"))
                {
                    return Json(OpportunityManager.GetOpportunities().Where(x => x.OpportunityName.Contains(searchText)).Select(x => new OpportunityListItem
                    {
                        OpportunityName = x.OpportunityName,
                        AccountName = x.AccountName,
                        Stage = x.Stage,
                        CloseDate = x.CloseDate,
                        LossReason = x.LossReason,
                        NextStep = x.NextStep,
                        Owner = x.Owner,
                        CreatedOn = x.CreatedOn,
                        ID = x.ID,
                        OpportunityType = x.OpportunityType
                    }).ToDataSourceResult(request));
                }
                else
                {
                    return Json(OpportunityManager.GetOpportunities().Where(x => x.OpportunityName.Contains(searchText) && (x.CreatedBy==userId || x.AssignedTo==userId)).Select(x => new OpportunityListItem
                    {
                        OpportunityName = x.OpportunityName,
                        AccountName = x.AccountName,
                        Stage = x.Stage,
                        CloseDate = x.CloseDate,
                        LossReason = x.LossReason,
                        NextStep = x.NextStep,
                        Owner = x.Owner,
                        CreatedOn = x.CreatedOn,
                        ID = x.ID,
                        OpportunityType = x.OpportunityType
                    }).ToDataSourceResult(request));
                }

               
            }

       }
      

        public ActionResult SaveOpportunity(OpportunityModel opportunity)
        {
            try
            {
                opportunity.CreatedBy = Guid.Parse(User.Identity.GetUserId());
                opportunity.ModifiedBy = Guid.Parse(User.Identity.GetUserId());
                opportunity.Save();
                return Json(new AjaxResponse {  Success=true, Message=""});
            }
            catch (Exception ex)
            {

                return Json(new AjaxResponse { Success = false, Message = ex.InnerException==null?ex.Message: ex.InnerException.Message});
               
            }
        }
        public ActionResult View(Guid id)
        {
            return View(OpportunityManager.GetOpportunities().Where(x => x.ID == id).FirstOrDefault());


        }
        public ActionResult Opportunities_Delete(string IDs)
        {
            try
            {

                OpportunityManager.DeleteOpportunities(IDs);

                return Json(new AjaxResponse { Success = true, Message = "" });
            }
            catch (Exception ex)
            {
                return Json(new AjaxResponse { Success = false, Message = ex.InnerException == null ? ex.Message : ex.InnerException.Message });
            }
        }

        public ActionResult Opportunity_Assign(string IDs, Guid Id)
        {
            try
            {
                OpportunityManager.AssignOpportunity(IDs, Id);

                return Json(new AjaxResponse { Success = true, Message = "" });
            }
            catch (Exception ex)
            {
                return Json(new AjaxResponse { Success = false, Message = ex.InnerException == null ? ex.Message : ex.InnerException.Message });
            }
        }



    }
}

