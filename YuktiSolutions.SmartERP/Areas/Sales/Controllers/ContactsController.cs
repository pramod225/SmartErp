using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNet.Identity;
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
    [Authorize]
    public class ContactsController : Controller
    {
        // GET: Sales/Contacts
        public ActionResult Index()
        {
            return View(new ContactModel()
            {
                Id = Guid.NewGuid()
            });
        }
        [HttpPost]
        public ActionResult Contacts_Read([DataSourceRequest] DataSourceRequest request, string searchText)
        {
            if (string.IsNullOrEmpty(searchText))
            {
                return Json(ContactManager.GetContacts().ToDataSourceResult(request));
            }
            else
            {
                return Json(ContactManager.GetContacts().Where(x => x.FirstName.Contains(searchText)).Select(x => new ContactListItem
                {
                    TitleName = x.TitleName,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    EmailId = x.EmailId,
                    MobileNo = x.MobileNo,
                    PhoneNo = x.PhoneNo,
                    Extension = x.Extension,
                    SkypeId = x.SkypeId,
                    WhatsApp = x.WhatsApp,
                    Account = x.Account,
                    Designation = x.Designation,
                    Id = x.Id
                }).ToDataSourceResult(request)); 
            }
        }
        public ActionResult View(Guid id)
        {
             return View(ContactManager.GetContacts().Where(x=>x.Id==id).FirstOrDefault());
        }

        [HttpPost]
        public ActionResult ContactsByAccount_Read([DataSourceRequest] DataSourceRequest request, Guid accountId)
        {
            /*TODO: When contacts section is complete and account is associated with the contact through
             * an ID, instead of Account Name, we will put where clause here to filter the contacts as per the selected Account*/
            return Json(ContactManager.GetContactsByAccount(accountId).Select(x => new ContactListItem
                {
                    Title = x.Title,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    EmailId = x.EmailId,
                    MobileNo = x.MobileNo,
                    PhoneNo = x.PhoneNo,
                    Extension = x.Extension,
                    SkypeId = x.SkypeId,
                    WhatsApp = x.WhatsApp,
                    Account = x.Account,
                    Designation = x.Designation,
                    LinkdinProfile=x.LinkdinProfile,
                    Id = x.Id
                }).ToDataSourceResult(request));
        }
        public ActionResult Contacts_Assign(string IDs, Guid Id)
        {
            try
            {

                ContactManager.AssignContacts(IDs, Id);

                return Json(new AjaxResponse { Success = true, Message = "" });
            }
            catch (Exception ex)
            {
                return Json(new AjaxResponse { Success = false, Message = ex.InnerException == null ? ex.Message : ex.InnerException.Message });
            }
        }

        public ActionResult SaveContact(ContactModel contact)
        {

            try
            {
                contact.CreatedBy = Guid.Parse(User.Identity.GetUserId());
                contact.ModifiedBy = Guid.Parse(User.Identity.GetUserId());
                contact.Save();
                return Json(new AjaxResponse { Success = true, Message = "" });
            }
            catch (Exception ex)
            {
                return Json(new AjaxResponse { Success = false, Message = ex.InnerException == null ? ex.Message : ex.InnerException.Message });
            }
        }
        public ActionResult Contacts_Delete(string IDs)
        {
            try
            {

                ContactManager.DeleteContacts(IDs);

                return Json(new AjaxResponse { Success = true, Message = "" });
            }
            catch (Exception ex)
            {
                return Json(new AjaxResponse { Success = false, Message = ex.InnerException == null ? ex.Message : ex.InnerException.Message });
            }
        }

    }
}