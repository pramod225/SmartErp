using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using YuktiSolutions.SmartERP.Areas.Sales.Models;
using YuktiSolutions.SmartERP.Areas.Sales.Models.Database;

namespace YuktiSolutions.SmartERP.Areas.Sales.Models.Database
{
    [Table("sales_masterentries")]
    public class MasterEntryModel
    {
        [Key]
        public Guid ID { get; set; }
        [Display(Name = "Name")]
        [Required]
        public string Name { get; set; }

        public string ApiKey { get; set; }

        public MasterEntryType MasterEntryType { get; set; }

        public void Save()
        {
            MasterEntriesManager.SaveMasterEntries(this);
        }
    }
    public enum MasterEntryType
    {
        LeadSources,
        CompanySize,
        Title,
        Industry,
        Ratings,
        Stage,
        LossReason,
        CampaignSource,
        OpportunityType,
        
    }
    public static class MasterEntriesManager
    {
        public static IEnumerable<MasterEntryModel> GetMasterEntries()
        {
            SalesDbContext context = new SalesDbContext();

            return context.MasterEntries;

        }

        public static IEnumerable<MasterEntryModel> GetMasterEntries(MasterEntryType masterEntryType)
        {
            SalesDbContext context = new SalesDbContext();

            var query = context.MasterEntries.Where(x => x.MasterEntryType == masterEntryType);

            if (query.Any())
            {
                return query.OrderBy(x => x.Name);
            }
            else
            {
                return new List<MasterEntryModel>();
            }
        }

        public static void MasterEntriesDelete(MasterEntryModel masterEntryModel)
        {
            using (SalesDbContext context = new SalesDbContext())
            {
                try
                {
                    var record = context.MasterEntries.Where(x => x.ID == masterEntryModel.ID).FirstOrDefault();
                    context.MasterEntries.Remove(record);
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public static void SaveMasterEntries(MasterEntryModel masterEntryModel, SalesDbContext context)
        {
            if (context.MasterEntries.Any(x => x.Name.Equals(masterEntryModel.Name, StringComparison.CurrentCultureIgnoreCase)
                 && x.ID != masterEntryModel.ID  && x.MasterEntryType == masterEntryModel.MasterEntryType
            ))

            {
                throw new ApplicationException("Name must be unique.");
            }
            var record = context.MasterEntries.FirstOrDefault(x => x.ID == masterEntryModel.ID);

            if (record == null)
            {
                record = context.MasterEntries.Add(masterEntryModel);
            }

            record.Name = masterEntryModel.Name;
            record.ApiKey = masterEntryModel.ApiKey;

        }


        public static void SaveMasterEntries(MasterEntryModel masterEntryModel)
        {
            using (SalesDbContext context = new SalesDbContext())
            {
                SaveMasterEntries(masterEntryModel, context);
                context.SaveChanges();
            }
        }
    }
}
