using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using YuktiSolutions.SmartERP.Areas.Sales.Models.UI;
using System.Web;

namespace YuktiSolutions.SmartERP.Areas.Sales.Models.Database
{
    [Table("sales_leads")]
    public class LeadModel
    {
        [Key]
        public Guid ID { get; set; }

        [Display(Name = "Title*")]
   
        public Guid? Title { get; set; }

   
        [Display(Name = "First Name*")]
 
        public string FirstName { get; set; }


      
        [Display(Name = "Last Name*")]
      
        public string LastName { get; set; }

        [Display(Name = "Company Name*")]
       
        public string CompanyName { get; set; }

        [Display(Name = "Designation")]
        public string Designation { get; set; }

        [DataType(DataType.PhoneNumber)]
              
        [Display(Name = "Mobile Number")]
        public string MobileNumber { get; set; }


        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$",
        ErrorMessage = "Please enter correct email address")]
        public string Email { get; set; }

        [Display(Name = "Lead Source*")]
    
        public Guid? LeadSource { get; set; }

        [Display(Name = "Rating*")]
        
        public Guid? Rating { get; set; }


        [Display(Name = "Street")]
        public string Street { get; set; }


        [Display(Name = "City")]
        public string City { get; set; }


        [Display(Name = "Country*")]
        
        public Guid? Country { get; set; }

        [Display(Name = "Zip Code")]
        public int ZipCode { get; set; }
        

        [Display(Name = "State/Province")]
        public string StateProvince { get; set; }


        [Display(Name = "Skype")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$",
        ErrorMessage = "Please enter correct skype id ")]
        public string Skype { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }


        [Display(Name = "Assigned To")]
        public Guid AssignedTo { get; set; }


        [Display(Name = "No Of Employees")]
        public int NoOfEmployees { get; set; }
        

        [Display(Name = "Industry*")]
     
        public Guid? Industry { get; set; }


        [Display(Name = "Owner")]
        public string Owner { get; set; }


        [Display(Name = "Extension")]
        [Range(1, 1000, ErrorMessage = "Extension number must be between 1....1000 ")]
        [RegularExpression("([1-9][0-9]*)")]
        public string Extension { get; set; }


        [Display(Name = "Created On")]
        public DateTime CreatedOn { get; set; }


        public Guid CreatedBy { get; set; }


        public Guid ModifiedBy { get; set; }


        public DateTime ModifiedOn { get; set; }

        public string Messege { get; set; }

       


        public void Save()
        {
            LeadManager.SaveLead(this);
        }


    }
    public static class LeadManager
    {

        public static IEnumerable<LeadListItem> GetLeads()
        {
            SalesDbContext context = new SalesDbContext();

            return context.Database.SqlQuery<LeadListItem>("Select a.*,b.CountryName,r.[UserName] CreatedByName,g.[UserName] ModifiedByName, c.[Name] as TitleName,d.[Name] as LeadSourceName,e.[Name] as RatingName,f.[Name] as IndustryName,h.[UserName] Assigned_To From sales_leads a " +
"left outer join sales_countries b on a.Country = b.ID " +
"left outer join sales_masterentries c on a.Title = c.ID and c.MasterEntryType = 2 " +
"left outer join sales_masterentries d on a.LeadSource = d.ID and d.MasterEntryType = 0 " +
"left outer join sales_masterentries e on a.Rating = e.ID and e.MasterEntryType = 4 " +
"left join AspNetUsers h on a.AssignedTo = h.Id " +
"left join AspNetUsers r on a.CreatedBy = r.Id " +
"left join AspNetUsers g on a.ModifiedBy = g.Id " +
"left outer join sales_masterentries f on a.Industry = f.ID and f.MasterEntryType = 3 ");

            
        }
        public static void DeleteLeads(string ids)
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

                    var records = context.Leads.Where(x => selectedIds.Contains(x.ID));

                    foreach (var record in records)
                    {
                        context.Leads.Remove(record);
                    }

                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }


        public static void SaveLead(LeadModel leadModel, SalesDbContext context)
        {
           
            var record = context.Leads.FirstOrDefault(x => x.ID == leadModel.ID);

            if (record == null)
            {
                leadModel.CreatedOn = DateTime.Now;        
                record = context.Leads.Add(leadModel);
            }

            record.Title = leadModel.Title;
            record.FirstName = leadModel.FirstName;
            record.LastName = leadModel.LastName;
            record.CompanyName = leadModel.CompanyName;
            record.Designation = leadModel.Designation;
            record.Email = leadModel.Email;
            record.MobileNumber = leadModel.MobileNumber;
            record.LeadSource = leadModel.LeadSource;
            record.Rating = leadModel.Rating;
            record.Street = leadModel.Street;
            record.City = leadModel.City;
            record.Country = leadModel.Country;
            record.StateProvince = leadModel.StateProvince;
            record.ZipCode = leadModel.ZipCode;
            record.PhoneNumber = leadModel.PhoneNumber;
            record.Skype = leadModel.Skype;
            record.NoOfEmployees = leadModel.NoOfEmployees;
            record.Industry = leadModel.Industry;
            record.Messege = leadModel.Messege;
            record.ModifiedBy = leadModel.ModifiedBy;
            record.ModifiedOn = DateTime.Now;



        }
        public static void SaveLead(LeadModel leadModel)
        {
            using (SalesDbContext context = new SalesDbContext())
            {
                SaveLead(leadModel, context);
                context.SaveChanges();
            }
        }
        public static void AssignLeads(string ids, Guid userId)
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

                    var records = context.Leads.Where(x => selectedIds.Contains(x.ID));

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


    }
}