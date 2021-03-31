using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace YuktiSolutions.SmartERP.Areas.Sales.Models.UI
{
    public class OpportunityListItem
    {
        
        public Guid ID { get; set; }

        [Display(Name = "Opportunity")]
        public string OpportunityName { get; set; }
       
        public Guid Account { get; set; }

        [Display(Name ="Account")]
        public string AccountName { get; set; }
       
        public Guid Stage { get; set; }

        [Display(Name = "Stage")]
        public string StageName { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public DateTime CloseDate { get; set; }

        [Display(Name = "Loss Reason")]
        public Guid LossReason { get; set; }

        [Display(Name = "Loss Reason")]
        public string LossReasonName { get; set; }

        [Display(Name = "Next Step")]
        public string NextStep { get; set; }
        public string Owner { get; set; }
        public Guid OpportunityType { get; set; }

        [Display(Name = "Primary Campaign Source")]
        public Guid PrimaryCampaignSource { get; set; }

        [Display(Name = "Primary Campaign Source")]
        public string PrimaryCampaignSourceName { get; set; }
        public bool BudgetConfirmed { get; set; }
        public bool DiscoveryCompleted { get; set; }
        public bool ROIAnalysisCompleted { get; set; }

        [Display(Name = "Probability(%)")]
        public float Probability { get; set; }

        [Display(Name = "Lead Source")]
        public Guid LeadSource { get; set; }

        [Display(Name = "Lead Source")]
        public string LeadSourceName { get; set; }
        public int Amount { get; set; }
        public string Description { get; set; }

        public Guid AssignedTo { get; set; }

        [Display(Name ="Assigned To")]

        public string Assigned_To { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy,hh: mm:ss tt}")]
        public DateTime CreatedOn { get; set; }
        public Guid CreatedBy { get; set; }
        
        [Display(Name = "Created By ")]
        public string CreatedByName { get; set; }

        public Guid ModifiedBy { get; set; }

        [Display(Name = "Modified By ")]
        public string ModifiedByName { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy,hh: mm:ss tt}")]
        public DateTime ModifiedOn { get; set; }
    }
}