using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using YuktiSolutions.SmartERP.Areas.Sales.Models.UI;
namespace YuktiSolutions.SmartERP.Areas.Sales.Models.Database
{
    [Table("sales_opportunities")]
    public class OpportunityModel
    {
       
        [Key]
        public Guid ID { get; set; }

        [Required(ErrorMessage ="Opportunity name is required")]
        [Display(Name = "Opportunity *")]
       
        public string OpportunityName { get; set; }

        [Required(ErrorMessage = "Account name is required")]
        public Guid Account { get; set; }

        [Required(ErrorMessage = "Stage is required")]
        [Display(Name = "Stage")]
        public Guid Stage { get; set; }
        
        [Required(ErrorMessage ="Date must be requires")]
        public DateTime CloseDate{ get; set; }

        [Required(ErrorMessage = "Loss reason is required")]
        [Display(Name = "Loss Reason")]
        public Guid LossReason { get; set; }

        [Display(Name = "Next Step")]
        public string NextStep { get; set; }
        public string Owner { get; set; }

        [Required(ErrorMessage = "Opportunity type is required")]
        [Display(Name = "Opportunity Type")]
       public Guid OpportunityType { get; set; }

        [Required(ErrorMessage = "Primary campaign source is required")]
        [Display(Name = "Primary Campaign Source")]
        public Guid PrimaryCampaignSource { get; set; }
        [Display(Name = "Budget Confirmed")]
        public bool BudgetConfirmed { get; set; }
        [Display(Name = "Discovery Completed")]
     
        public bool DiscoveryCompleted { get; set; }
        [Display(Name = "ROI Analyses Completed")]

        public bool ROIAnalysisCompleted { get; set; }

        [Display(Name = "Probability(%)")]

        [Range(0, 100, ErrorMessage = "Probability is required")]
        public float Probability { get; set; }

        [Required(ErrorMessage = "Lead source is required")]
        [Display(Name = "Lead Source")]
        public Guid LeadSource { get; set; }

        [Range(0,Int32.MaxValue, ErrorMessage = "Amount is required")]
        public int Amount { get; set; }
        public string Description { get; set; }

        [Display(Name = "AssignedTo")]
        public Guid AssignedTo { get; set; }

        public DateTime CreatedOn { get; set; }

        public Guid CreatedBy { get; set; }

       
        public DateTime ModifiedOn { get; set; }

        public Guid ModifiedBy { get; set; }

        public void Save()
        {
            OpportunityManager.SaveOpportunity(this);
        }
       

    }

    public static class OpportunityManager
    {

        public static IEnumerable<OpportunityListItem> GetOpportunities()
        {
            SalesDbContext context = new SalesDbContext();

            return context.Database.SqlQuery<OpportunityListItem>("Select a.*, b.AccountName,c.[Name] as LossReasonName,d.[Name] as PrimaryCampaignSourceName,e.[Name] as OpportunityType,f.[Name] as LeadSourceName,g.[Name]  as StageName,h.[UserName] as Assigned_To,i.[UserName] CreatedByName,j.[UserName] ModifiedByName From sales_opportunities a " +
"left outer join sales_masterentries c on a.LossReason = c.ID and c.MasterEntryType = 6 " +
"left outer join sales_masterentries d on a.PrimaryCampaignSource = d.ID and d.MasterEntryType = 7" +
"left outer join sales_masterentries e on a.OpportunityType = e.ID and e.MasterEntryType = 8 " +
"left outer join sales_masterentries f on a.LeadSource = f.ID and f.MasterEntryType = 0 " +
"left outer join sales_masterentries g on a.Stage = g.ID and g.MasterEntryType = 5 " +
" left outer join sales_accounts b on a.Account = b.ID " +
"left join AspNetUsers h on a.AssignedTo = h.Id " +
"left join AspNetUsers i on a.CreatedBy = i.Id " +
"left join AspNetUsers j on a.ModifiedBy = j.Id ");
        }
        public static void DeleteOpportunities(string ids)
        {
            using (SalesDbContext context = new SalesDbContext())
            {
                try
                {
                    String[] IDs = ids.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

                    List<Guid> selectedIds = new List<Guid>();

                    foreach (var id in IDs)
                    {
                        selectedIds.Add(Guid.Parse(id));
                    }

                    var records = context.Opportunities.Where(x => selectedIds.Contains(x.ID));

                    foreach (var record in records)
                    {
                        context.Opportunities.Remove(record);
                    }

                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public static void SaveOpportunity(OpportunityModel opportunityModel, SalesDbContext context)
        {
            if (context.Opportunities.Any(x => x.OpportunityName.Equals(opportunityModel.OpportunityName, StringComparison.CurrentCultureIgnoreCase)
                 && x.ID != opportunityModel.ID
            ))
            {
                throw new ApplicationException("Opportunity name must be unique.");
            }

            var record = context.Opportunities.FirstOrDefault(x => x.ID == opportunityModel.ID);

            DateTime temp = new DateTime(0001, 01, 01);

            if (record == null)
            {
                opportunityModel.CreatedOn = DateTime.Now;
                opportunityModel.CloseDate = temp == opportunityModel.CloseDate ? DateTime.Now : opportunityModel.CloseDate;
                record = context.Opportunities.Add(opportunityModel);
            }
            record.OpportunityName = opportunityModel.OpportunityName;
            record.Account = opportunityModel.Account;
            record.Stage = opportunityModel.Stage;
            record.CloseDate = temp == opportunityModel.CloseDate ? record.CloseDate : opportunityModel.CloseDate;
            record.LossReason = opportunityModel.LossReason;
            record.NextStep = opportunityModel.NextStep;
            record.OpportunityType = opportunityModel.OpportunityType;
            record.Description = opportunityModel.Description;
            record.Probability = opportunityModel.Probability;
            record.Amount = opportunityModel.Amount;
            record.BudgetConfirmed  = opportunityModel.BudgetConfirmed;
            record.DiscoveryCompleted = opportunityModel.DiscoveryCompleted;
            record.ROIAnalysisCompleted = opportunityModel.ROIAnalysisCompleted;
            record.ModifiedBy = opportunityModel.ModifiedBy;
            record.ModifiedOn = DateTime.Now;

        }
        public static void AssignOpportunity(string ids, Guid userId)
        {
            using (SalesDbContext context = new SalesDbContext())
            {
                try
                {
                    String[] IDs = ids.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

                    List<Guid> selectedIds = new List<Guid>();

                    foreach (var id in IDs)
                    {
                        selectedIds.Add(Guid.Parse(id));
                    }

                    var records = context.Opportunities.Where(x => selectedIds.Contains(x.ID));

                    foreach (var record in records)
                    {
                        record.AssignedTo = userId;

                    }

                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public static void SaveOpportunity(OpportunityModel opportunityModel)
        {
            using (SalesDbContext context = new SalesDbContext())
            {
                SaveOpportunity(opportunityModel, context);
                context.SaveChanges();
            }
        }
      
    }

}
