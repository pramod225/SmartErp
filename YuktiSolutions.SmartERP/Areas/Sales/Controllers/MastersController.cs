using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YuktiSolutions.SmartERP.Areas.Sales.Models.Database;
using YuktiSolutions.SmartERP.Areas.Sales.Models.UI;

namespace YuktiSolutions.SmartERP.Areas.Sales.Controllers
{
    [Authorize]
    public partial class MastersController : Controller
    {
        // GET: Sales/Masters
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult MasterEntries_Read([DataSourceRequest] DataSourceRequest request, MasterEntryType masterEntryType)
        {
            return Json(MasterEntriesManager.GetMasterEntries().Where(c => c.MasterEntryType == masterEntryType).Select(x => new MasterEntriesListItem
            {
                Name = x.Name,
                ID= x.ID,
                ApiKey=x.ApiKey,
                MasterEntryType = x.MasterEntryType

            }).ToDataSourceResult(request));
        }
        [HttpPost]
        public ActionResult Countries_Read([DataSourceRequest] DataSourceRequest request)
        {
            return Json(CountryManager.GetCountries().Select(x => new CountryListItem
            {
                CountryCode = x.CountryCode,
                CountryName = x.CountryName,
                ID = x.ID
            }).ToDataSourceResult(request));
        }


        [HttpPost]
        public ActionResult Countries_Save(CountryListItem countryListItem)
        {
            try
            {
                CountryModel countryModel = new CountryModel
                {
                    ID = countryListItem.ID,
                    CountryCode = countryListItem.CountryCode,
                    CountryName = countryListItem.CountryName
                };

                countryModel.Save();

                return Json(new AjaxResponse { Success = true, Message = "" });
            }
            catch (Exception ex)
            {
                return Json(new AjaxResponse { Success = false, Message = ex.InnerException == null ? ex.Message : ex.InnerException.Message });
            }
        }

        [HttpPost]
        public ActionResult Countries_Update(CountryListItem countryListItem)
        {
            try
            {
                CountryModel countryModel = new CountryModel
                {
                    ID = countryListItem.ID,
                    CountryCode = countryListItem.CountryCode,
                    CountryName = countryListItem.CountryName
                };

                countryModel.Save();

                return Json(new AjaxResponse { Success = true, Message = "" });
            }
            catch (Exception ex)
            {
                return Json(new AjaxResponse { Success = false, Message = ex.InnerException == null ? ex.Message : ex.InnerException.Message });
            }
        }

        [HttpPost]
        public ActionResult Countries_Destroy([DataSourceRequest] DataSourceRequest request, CountryListItem countryListItem)
        {
            ModelState.Clear();
            try
            {
                CountryManager.DeleteCountry(new CountryModel
                {
                    ID = countryListItem.ID,
                    CountryCode = countryListItem.CountryCode,
                    CountryName = countryListItem.CountryName
                });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error", (ex.InnerException == null ? ex.Message : ex.InnerException.Message));
            }

            return Json(new[] { countryListItem }.ToDataSourceResult(request, ModelState));

        }



        [HttpPost]
        public ActionResult MasterEntries_Save(MasterEntriesListItem masterEntriesListItem)
        {

            try
            {
                MasterEntryModel masterEntryModel = new MasterEntryModel
                {
                    ID = masterEntriesListItem.ID,
                    Name = masterEntriesListItem.Name,
                    ApiKey= masterEntriesListItem.ApiKey,
                    MasterEntryType = masterEntriesListItem.MasterEntryType

                };

                masterEntryModel.Save();

                return Json(new AjaxResponse { Success = true, Message = "" });
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    foreach (var ve in eve.ValidationErrors)
                    {

                        return Json(new AjaxResponse { Success = false, Message = ve.ErrorMessage });
                    }
                }
                throw;
            }
            catch (Exception ex)
            {
                return Json(new AjaxResponse { Success = false, Message = ex.InnerException == null ? ex.Message : ex.InnerException.Message });
            }
        }


        [HttpPost]
        public ActionResult MasterEntries_Update(MasterEntriesListItem masterEntriesListItem)
        {
            try
            {
                MasterEntryModel masterEntryModel = new MasterEntryModel
                {
                    ID = masterEntriesListItem.ID,
                    Name = masterEntriesListItem.Name,
                    ApiKey= masterEntriesListItem.ApiKey,

                    MasterEntryType = masterEntriesListItem.MasterEntryType
                };

                masterEntryModel.Save();

                return Json(new AjaxResponse { Success = true, Message = "" });
            }
            catch (Exception ex)
            {
                return Json(new AjaxResponse { Success = false, Message = ex.InnerException == null ? ex.Message : ex.InnerException.Message });
            }
        }

        [HttpPost]
        public ActionResult MasterEntries_Destroy([DataSourceRequest] DataSourceRequest request, MasterEntriesListItem masterEntriesListItem)
        {
            ModelState.Clear();
            try
            {
                MasterEntriesManager.MasterEntriesDelete(new MasterEntryModel
                {
                    ID = masterEntriesListItem.ID,
                    Name = masterEntriesListItem.Name,
                    ApiKey =masterEntriesListItem.ApiKey,
                    MasterEntryType = masterEntriesListItem.MasterEntryType
                });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error", (ex.InnerException == null ? ex.Message : ex.InnerException.Message));
            }

            return Json(new[] { masterEntriesListItem }.ToDataSourceResult(request, ModelState));

        }
    }
}