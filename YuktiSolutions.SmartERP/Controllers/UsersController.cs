using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using YuktiSolutions.SmartERP.Models;
using System.Web;
using Microsoft.AspNet.Identity.Owin;
using YuktiSolutions.SmartERP.Controllers;
using System;
using System.Collections.Generic;
using YuktiSolutions.SmartERP.Areas.Sales.Models.UI;
using System.Security.Cryptography;

namespace YuktiSolutions.SmartERP.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        // GET: Users

        /// <summary>
        /// Returns the user grid.
        /// </summary>
        public ActionResult Users()
        {
            return View();

        }

        /// <summary>
        /// Saves the users.
        /// </summary>
        /// <param name="userViewModel">Contains all properties of UserViewModel to be saved</param>
        public ActionResult StoreUser(UserViewModel userViewModel)
        {
            try
            {
                
                userViewModel.Save();

                return Json(new AjaxResponse { Success = true, Message = "" });
            }
            catch (Exception ex)
            {
                return Json(new AjaxResponse { Success = false, Message = ex.InnerException == null ? ex.Message : ex.InnerException.Message });
            }
        }

        [HttpPost]
        /// <summary>
        /// Reads the user from database.
        /// </summary>
        /// <param name="request">Ajax Request</param>
        /// <param name="searchText">string which username must contain</param>
        public ActionResult Users_Read([DataSourceRequest] DataSourceRequest request, string searchText)
        {

            ApplicationDbContext context = new ApplicationDbContext();
            if (string.IsNullOrEmpty(searchText))
            {

                return Json(context.Users.Select(x => new IdentityUser
                {
                    Id = x.Id,
                    UserName = x.UserName,
                    PhoneNumber = x.PhoneNumber,
                    Email = x.Email,


                }).ToDataSourceResult(request)); ;
            }
            else
            {
                return Json(context.Users.Where(x => x.UserName.Contains(searchText)).Select(x => new IdentityUser
                {
                    Id = x.Id,
                    UserName = x.UserName,
                    PhoneNumber = x.PhoneNumber,
                    Email = x.Email,


                }).ToDataSourceResult(request)); ;
            }
        }

        /// <summary>
        /// Deletes multiple users.
        /// </summary>
        /// <param name="ids">Semicolon separated Ids of the users to be deleted.</param>
        public static void DeleteUsers(string ids)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                try
                {
                    String[] IDs = ids.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);


                    var records = context.Users.Where(x => IDs.Contains(x.Id));

                    foreach (var record in records)
                    {
                        context.Users.Remove(record);
                    }

                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        /// <summary>
        /// Deletes multiple users.
        /// </summary>
        /// <param name="ids">Semicolon separated Ids of the users to be deleted.</param>
        public ActionResult UsersDelete(string IDs)
        {
            try
            {

                DeleteUsers(IDs);

                return Json(new AjaxResponse { Success = true, Message = "" });
            }
            catch (Exception ex)
            {
                return Json(new AjaxResponse { Success = false, Message = ex.InnerException == null ? ex.Message : ex.InnerException.Message });
            }
        }
    }
}