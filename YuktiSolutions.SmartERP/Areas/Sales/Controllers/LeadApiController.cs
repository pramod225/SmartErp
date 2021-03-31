using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using YuktiSolutions.SmartERP.Areas.Sales.Models.Database;
using YuktiSolutions.SmartERP.Areas.Sales.Models.UI;
using YuktiSolutions.SmartERP.Areas.Sales.Models;


namespace YuktiSolutions.SmartERP.Areas.Sales.Controllers
{ 
    
    public class LeadApiController : ApiController
    {
        private SalesDbContext context = new SalesDbContext();

        public IQueryable<LeadModel> GetLead()

        {
            return context.Leads;
        }
        


        // POST api/<controller>
        [ResponseType(typeof(LeadModel))]
        [HttpPost]
        public IHttpActionResult PostLead(LeadModel leadModel)
        {

                context.Leads.Add(leadModel);
                context.SaveChanges();

            
            return CreatedAtRoute("DefaultApi", new { id = leadModel.ID}, leadModel);
        }
            
    }
}