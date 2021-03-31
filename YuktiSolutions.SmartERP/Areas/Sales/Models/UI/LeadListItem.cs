using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace YuktiSolutions.SmartERP.Areas.Sales.Models.UI
{
    public class LeadListItem
    {
        public Guid ID { get; set; }
        public string TitleName { get; set; }
        public Guid? Title { get; set; }
        public string Name { get { return this.TitleName + " "  + this.FirstName + " " + this.LastName ; } }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        [Display(Name = "Company ")]
        public string CompanyName { get; set; }

        public string Designation { get; set; }

        public string MobileNumber { get; set; }
        public string Extension { get; set; }

        public string Email { get; set; }

        [Display(Name = "Lead Source")]
        public string LeadSourceName { get; set; }
        public Guid? LeadSource { get; set; }

        [Display(Name = "Rating")]
        public string RatingName { get; set; }

        public Guid? Rating { get; set; }
       
        public Guid AssignedTo { get; set; }

        [Display(Name="Assigned To")]
        public string Assigned_To { get; set; }


        public string Street { get; set; }


        public string City { get; set; }

        [Display(Name = "Country")]
        public string CountryName { get; set; }

        public Guid? Country { get; set; }
        
        public int ZipCode { get; set; }


        public string StateProvince { get; set; }


        public string Skype { get; set; }



        public string PhoneNumber { get; set; }

        [Display(Name = "No Of Employees")]
        public int? NoOfEmployees { get; set; }

        [Display(Name = "Industry ")]
        public string IndustryName { get; set; }
        public Guid? Industry { get; set; }

        public string Owner { get; set; }

        public Guid CreatedBy { get; set; }

        [Display(Name = "Created By")]
        public string CreatedByName { get; set; }

        [Display(Name = "Modified By")]
        public string ModifiedByName { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy,hh: mm:ss tt}")]
        public DateTime CreatedOn { get; set; }
        public string  Messege { get; set; }

        public Guid ModifiedBy { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy,hh: mm:ss tt}")]
        public DateTime ModifiedOn { get; set; }



    }
}