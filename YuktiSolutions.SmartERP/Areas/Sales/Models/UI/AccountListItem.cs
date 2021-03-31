using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace YuktiSolutions.SmartERP.Areas.Sales.Models.UI
{
    public class AccountListItem
    {
        public Guid ID { get; set; }

        [Display(Name = "Account")]
        public String AccountName { get; set; }
        public String PhoneNumber { get; set; }

        public Guid Country { get; set; }

        public Guid AssignedTo { get; set; }

        [Display(Name = "Assigned To")]
        public string Assigned_To { get; set; }

        [Display(Name = "Country")]
        public String CountryName { get; set; }

        public Guid Industry { get; set; }

        [Display(Name = "Industry")]
        public String IndustryName { get; set; }


        public String Website { get; set; }
        public String FaxNumber { get; set; }
        public Guid CompanySize { get; set; }

        [Display(Name = "Company Size")]
        public string CompanySizeName { get; set; }
        public String GSTNumber { get; set; }
        public String BillingAddressLine1 { get; set; }
        public String BillingAddressLine2 { get; set; }
        public String BillingCityState { get; set; }
        public String ShippingAddressLine1 { get; set; }
        public String ShippingAddressLine2 { get; set; }
        public String ShippingCityState { get; set; }
        public String ZipCode { get; set; }
        public String ShippingZipCode { get; set; }
        public Guid ShippingCountry { get; set; }
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