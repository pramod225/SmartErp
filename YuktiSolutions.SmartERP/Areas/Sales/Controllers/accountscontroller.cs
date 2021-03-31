using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YuktiSolutions.SmartERP.Areas.Sales.Models.Database;
using YuktiSolutions.SmartERP.Areas.Sales.Models.UI;
using YuktiSolutions.SmartERP.Areas.Sales.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

namespace YuktiSolutions.SmartERP.Areas.Sales.Controllers
{
    [Authorize]
    public class AccountsController : Controller
    {
        // GET: Sales/Accounts
        public ActionResult Index()
        {
            return View(new AccountModel()
            {
                ID = Guid.NewGuid()
            });
        }


        [HttpPost]
        public ActionResult Accounts_Read([DataSourceRequest] DataSourceRequest request, string searchText)
        {

            if (string.IsNullOrEmpty(searchText))
            {
                return Json(AccountManager.GetAccounts().ToDataSourceResult(request));
            }
            else
            {
                var userId = Guid.Parse(User.Identity.GetUserId());
                if (User.IsInRole("Admin"))
                {
                    return Json(AccountManager.GetAccounts().Where(x => x.AccountName.Contains(searchText)).Select(x => new AccountListItem
                    {
                        AccountName = x.AccountName,
                        PhoneNumber = x.PhoneNumber,
                        Country = x.Country,
                        Industry = x.Industry,
                        Website = x.Website,
                        FaxNumber = x.FaxNumber,
                        CompanySize = x.CompanySize,
                        BillingAddressLine1 = x.BillingAddressLine1,
                        ID = x.ID
                    }).ToDataSourceResult(request));
                }
                else
                {
                    return Json(AccountManager.GetAccounts().Where(x => x.AccountName.Contains(searchText) && (x.CreatedBy == userId || x.AssignedTo == userId)).Select(x => new AccountListItem
                    {
                        AccountName = x.AccountName,
                        PhoneNumber = x.PhoneNumber,
                        Country = x.Country,
                        Industry = x.Industry,
                        Website = x.Website,
                        FaxNumber = x.FaxNumber,
                        CompanySize = x.CompanySize,
                        BillingAddressLine1 = x.BillingAddressLine1,
                        ID = x.ID
                    }).ToDataSourceResult(request));
                }
            }
        }
           
        public ActionResult SaveAccount(AccountModel account)
        {
            try
            {
                account.CreatedBy = Guid.Parse(User.Identity.GetUserId());
                account.ModifiedBy = Guid.Parse(User.Identity.GetUserId());

                account.Save();

                return Json(new AjaxResponse { Success = true, Message = "" });
            }
            catch (Exception ex)
            {
                return Json(new AjaxResponse { Success = false, Message = ex.InnerException == null ? ex.Message : ex.InnerException.Message });
            }
        }
        public ActionResult View(Guid id)
        {
            //SalesDbContext context = new SalesDbContext();
            return View (AccountManager.GetAccounts().Where(x => x.ID == id).FirstOrDefault());
        }

        /// <summary>
        /// Assign the multiple accounts to user.
        /// </summary>
        /// <param name="IDs">Semicolon separated Ids of the accounts to be assigned.</param>
        /// <param name="Id">Id of the user to which the accounts to be assigned.</param>
        public ActionResult Accounts_Assign(string IDs, Guid Id)
        {
            try
            {
                AccountManager.AssignAccounts(IDs, Id);

                return Json(new AjaxResponse { Success = true, Message = "" });
            }
            catch (Exception ex)
            {
                return Json(new AjaxResponse { Success = false, Message = ex.InnerException == null ? ex.Message : ex.InnerException.Message });
            }
        }

        /// <summary>
        /// Remove the assigned user from multiple accounts.
        /// </summary>
        /// <param name="IDs">Semicolon separated Ids of the accounts.</param>
        public ActionResult Accounts_ResetAssign(string IDs)
        {
            try
            {
                AccountManager.ResetAssignAccounts(IDs);

                return Json(new AjaxResponse { Success = true, Message = "" });
            }
            catch (Exception ex)
            {
                return Json(new AjaxResponse { Success = false, Message = ex.InnerException == null ? ex.Message : ex.InnerException.Message });
            }
        }
        public ActionResult Accounts_Delete(string IDs)
        {
            try
            {

                AccountManager.DeleteAccounts(IDs);

                return Json(new AjaxResponse { Success = true, Message = "" });
            }
            catch (Exception ex)
            {
                return Json(new AjaxResponse { Success = false, Message = ex.InnerException == null ? ex.Message : ex.InnerException.Message });
            }
        }

        

    }
}