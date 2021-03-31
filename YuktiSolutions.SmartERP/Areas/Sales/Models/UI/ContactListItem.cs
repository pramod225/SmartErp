using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace YuktiSolutions.SmartERP.Areas.Sales.Models.UI
{
    public class ContactListItem
    {
        [Key]
        public Guid Id { get; set; }
        public Guid Title { get; set; }
        public string TitleName { get; set; }
        public string Name { get { return this.TitleName +" " + this.FirstName + " " + this.LastName; } }
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string EmailId { get; set; }

        public string MobileNo { get; set; }

        public string PhoneNo { get; set; }
        public string Extension { get; set; }
        public string SkypeId { get; set; }
        public string WhatsApp { get; set; }
        public Guid Account { get; set; }

        [Display(Name = "Account")]
        public string AccountName { get; set; }
        public string Designation { get; set; }
        [Display(Name = "LinkedIn Profile")]
        public string LinkdinProfile { get; set; }
        public Guid AssignedTo { get; set; }
        [Display(Name = "Assigned To")]
        public string Assigned_To { get; set; }

        public Guid CreatedBy { get; set; }

        [Display(Name = "Created By ")]
        public string CreatedByName { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy,hh: mm:ss tt}")]
        public DateTime CreatedOn { get; set; }
        
        public Guid ModifiedBy { get; set; }

        [Display(Name = "Modified By ")]
        public string ModifiedByName { get; set; }

        
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy,hh: mm:ss tt}")]
        public DateTime ModifiedOn { get; set; }



    }
}