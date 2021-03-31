using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YuktiSolutions.SmartERP.Areas.Sales.Models.Database;
using YuktiSolutions.SmartERP.Areas.Sales.Models.UI;
using Microsoft.AspNet.Identity;
using System.Net.Mail;
using System.Net;

namespace YuktiSolutions.SmartERP.Areas.Sales.Controllers
{
    [Authorize]
    public class ActivitiesController : Controller
    {
        // GET: Sales/Activities

        public ActionResult Index()
        {
            return View(new ActivityModel()
            {
                Id = Guid.NewGuid()
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
        public ActionResult SaveActivity(ActivityModel activity,ContactModel contactModel)
        {
            try
            {
                activity.CreatedBy = Guid.Parse(User.Identity.GetUserId());
                activity.ModifiedBy = Guid.Parse(User.Identity.GetUserId());
                activity.Save();
                string content = "<b>New Entry Filled</b>";
                bool mailSend = Email(content, "navneetmishra2512@gmail.com", contactModel.EmailId, "New Entry Filled", "smtp.gmail.com", 587, "Navneet@#$123");
                return Json(new AjaxResponse { Success = true, Message = " " });
            }
            catch (Exception ex)
            {
                return Json(new AjaxResponse { Success = false, Message = ex.InnerException == null ? ex.Message : ex.InnerException.Message });
            }
        }

        public ActionResult View(Guid id)
        {
            return View(ActivityManager.GetActivities().Where(x => x.Id == id).FirstOrDefault());
        }

        public ActionResult Activity_assign(string IDs, Guid Id)
        {
            try
            {
                ActivityManager.AssignActivity(IDs, Id);

                return Json(new AjaxResponse { Success = true, Message = "" });
            }
            catch (Exception ex)
            {
                return Json(new AjaxResponse { Success = false, Message = ex.InnerException == null ? ex.Message : ex.InnerException.Message });
            }
        }

    }
}

 