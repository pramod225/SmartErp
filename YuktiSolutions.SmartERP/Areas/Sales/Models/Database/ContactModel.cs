using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using YuktiSolutions.SmartERP.Areas.Sales.Models.UI;

namespace YuktiSolutions.SmartERP.Areas.Sales.Models.Database
{
   [Table("sales_contacts")]

    public class ContactModel
    {
        [Key]
        public Guid Id { get; set; }

        [Display(Name = "Title*")]
        [Required(ErrorMessage ="Title must be selected")]
        public Guid Title { get; set; }

        [Display(Name = "First Name")]
        [Required(ErrorMessage = "Enter first name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Enter last name")]
        public string LastName { get; set; }

        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$",
        ErrorMessage = "Please enter eorrect email address")]
        public string EmailId { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Mobile No")]
        public string MobileNo { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone No")]
        public string PhoneNo { get; set; }

        [Display(Name = "Extension")]
         [Range(1,1000,ErrorMessage ="Extension number must be between 1....1000 ")]
        [RegularExpression("([1-9][0-9]*)")]
        public string Extension { get; set; }

        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$", ErrorMessage = "Please Enter Correct skype id")]
        [Display(Name = "Skype ID")]
        public string SkypeId { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Display(Name = "WhatsApp")]
        public string WhatsApp { get; set; }

        [Required(ErrorMessage ="Select account name ")]
        [Display(Name ="Account Name*")]
        public Guid Account { get; set; }
       
        [Display(Name = "Designation")]
        public string Designation { get; set; }

        [DataType(DataType.EmailAddress)]
        [Display(Name = "LinkedIn Profile")]
        public string LinkdinProfile { get; set; }

        public Guid AssignedTo { get; set; }

        public Guid CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public Guid ModifiedBy { get; set; }

        public DateTime ModifiedOn { get; set; }


        public void Save()
        {
            ContactManager.SaveContact(this);
        }

    }

    public static class ContactManager
    {
        public static IEnumerable<ContactListItem> GetContacts()
        {
            SalesDbContext context = new SalesDbContext();

           return context.Database.SqlQuery<ContactListItem>("Select a.*,c.AccountName, b.[Name] as TitleName,h.[UserName] Assigned_To, d.[UserName] CreatedByName,g.[UserName] ModifiedByName  From sales_contacts a " +
                                                               "left join AspNetUsers h on a.AssignedTo = h.Id " + 
                                                               " left outer join sales_masterentries b on a.Title = b.ID and b.MasterEntryType = 2 " +
                                                               "left join AspNetUsers d on a.CreatedBy = d.Id " +
                                                                "left join AspNetUsers g on a.ModifiedBy = g.Id " +
                                                                " left outer join sales_accounts c on a.Account = c.ID ");

        }

        public static IEnumerable<ContactListItem> GetContactsByAccount(Guid AccountID)
        {
            SalesDbContext context = new SalesDbContext();

            return context.Database.SqlQuery<ContactListItem>("Select a.*,c.AccountName, b.[Name] as TitleName From sales_contacts a " +
                                                                " left outer join sales_masterentries b on a.Title = b.ID and b.MasterEntryType = 2 " +
                                                                 " left outer join sales_accounts c on a.Account = c.ID where a.Account=@p0 ", AccountID);

        }

        public static void DeleteContacts(string ids)
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

                    var records = context.Contacts.Where(x => selectedIds.Contains(x.Id));

                    foreach (var record in records)
                    {
                        context.Contacts.Remove(record);
                    }

                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        public static void AssignContacts(string ids, Guid userId)
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

                    var records = context.Contacts.Where(x => selectedIds.Contains(x.Id));

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

        public static void SaveContact(ContactModel contactModel, SalesDbContext context)
        {
            var record = context.Contacts.FirstOrDefault(x => x.Id == contactModel.Id);

            if (record == null)
            {
                contactModel.CreatedOn = DateTime.Now;
                record = context.Contacts.Add(contactModel);
            }
            record.Title = contactModel.Title;
            record.FirstName = contactModel.FirstName;
            record.LastName = contactModel.LastName;
            record.EmailId = contactModel.EmailId;
            record.MobileNo = contactModel.MobileNo;
            record.PhoneNo = contactModel.PhoneNo;
            record.Extension = contactModel.Extension;
            record.SkypeId = contactModel.SkypeId;
            record.WhatsApp = contactModel.WhatsApp;
            record.Account = contactModel.Account;
            record.LinkdinProfile = contactModel.LinkdinProfile;
            record.Designation = contactModel.Designation;
            record.ModifiedBy = contactModel.ModifiedBy;
            record.ModifiedOn = DateTime.Now;
        }

        public static void SaveContact(ContactModel contactModel)
        {
                using (SalesDbContext context = new SalesDbContext())
                {
                    SaveContact(contactModel, context);
                    context.SaveChanges();
                }
        }

    }
}

