using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using YuktiSolutions.SmartERP.Areas.Sales.Models;
using YuktiSolutions.SmartERP.Areas.Sales.Models.Database;
using YuktiSolutions.SmartERP.Areas.Sales.Models.UI;


namespace YuktiSolutions.SmartERP.Areas.Sales.Controllers
{
    [Authorize]
    public class LeadsController : Controller
    {
        // GET: Sales/Leads
        public ActionResult Index()
        {
            return View(new LeadModel()
            {

                ID = Guid.NewGuid()
            });

        }

        public static bool Email(string content, string fromMailId, string toMailId, string subject, string host, int port, string password)
        {
            try
            {
                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress(fromMailId);
                    mail.To.Add(toMailId);
                    mail.Subject = subject;
                    mail.Body = content;
                    mail.IsBodyHtml = true;
                    //mail.Attachments.Add(new Attachment("C:\\file.zip"));

                    using (SmtpClient smtp = new SmtpClient(host, port))
                    {
                        smtp.Credentials = new NetworkCredential(fromMailId, password);
                        smtp.EnableSsl = true;
                        smtp.Send(mail);
                    }
                }
                return true;
            }
            catch (Exception ex) { return false; }
        }

        [HttpPost]
        public ActionResult Leads_Read([DataSourceRequest] DataSourceRequest request ,string searchText)
        {
            if (string.IsNullOrEmpty(searchText))
            {
                return Json(LeadManager.GetLeads().ToDataSourceResult(request));

            }
            else
            {
                return Json(LeadManager.GetLeads().Where(x => x.FirstName.Contains(searchText)).Select(x => new LeadListItem
                {
                    TitleName = x.TitleName,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    LeadSource =x.LeadSource,
                    CountryName= x.CountryName,
                    Rating= x.Rating,
                    CompanyName =x.CompanyName,
                    Designation =x.Designation,
                    PhoneNumber =x.PhoneNumber,
                    Email=x.Email,
                    StateProvince=x.StateProvince,
                    Owner=x.Owner,
                    CreatedOn=x.CreatedOn,
                    Messege = x.Messege



                }).ToDataSourceResult(request));
            }

        }
        public ActionResult View(Guid id)
        {
           
          return View(LeadManager.GetLeads().Where(x => x.ID == id).FirstOrDefault());
           

        }
        public ActionResult SaveLead(LeadModel lead)
        {
            try
            {
                lead.CreatedBy = Guid.Parse(User.Identity.GetUserId());
                lead.ModifiedBy = Guid.Parse(User.Identity.GetUserId());
                lead.Save();
                string content = "<b>New Entry Filled </b>";
                bool mailSend = Email(content, "info@yuktisolutions.com", "info@yuktisolutions.com", "New Entry Filled", "smtp.gmail.com", 587, "Winner2020@");
                return Json(new AjaxResponse { Success = true, Message = "" });
            }
            catch (Exception ex)
            {
                return Json(new AjaxResponse { Success = false, Message = ex.InnerException == null ? ex.Message : ex.InnerException.Message });
            }
        }
        public ActionResult Leads_Delete(string IDs)
        {
            try
            {
                LeadManager.DeleteLeads(IDs);

                return Json(new AjaxResponse { Success = true, Message = "" });
            }
            catch (Exception ex)
            {
                return Json(new AjaxResponse { Success = false, Message = ex.InnerException == null ? ex.Message : ex.InnerException.Message });
            }
        }
        public ActionResult Leads_Assign(string IDs, Guid Id)
        {
            try
            {

                LeadManager.AssignLeads(IDs, Id);

                return Json(new AjaxResponse { Success = true, Message = "" });
            }
            catch (Exception ex)
            {
                return Json(new AjaxResponse { Success = false, Message = ex.InnerException == null ? ex.Message : ex.InnerException.Message });
            }
        }

    }
}