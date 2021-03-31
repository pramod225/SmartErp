using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using YuktiSolutions.SmartERP.Areas.Sales.Models.Database;

namespace YuktiSolutions.SmartERP.Areas.Sales.Models.UI
{
    public class CallActivityListItem
    {
        [Key]
        public Guid Id { get; set; }

        [Display(Name = "Subject")]
        [Required(ErrorMessage = "Subject name is required")]
        public string Subject { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime DueDate { get; set; }

        public Guid AssignTo { get; set; }

        [Display(Name = "Assign_To")]
        public string Assign_To { get; set; }
        public Guid CreatedBy { get; set; }

        [Display(Name = "Created By ")]
        public string CreatedByName { get; set; }

        public Guid ModifiedBy { get; set; }

        [Display(Name = "Modified By ")]
        public string ModifiedByName { get; set; }
        public RelatedEntityType RelatedEntityType { get; set; }
        public Guid RelatedEntityID { get; set; }
    }
}