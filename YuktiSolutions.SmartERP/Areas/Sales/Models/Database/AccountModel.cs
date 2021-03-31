using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using YuktiSolutions.SmartERP.Areas.Sales.Models.UI;

namespace YuktiSolutions.SmartERP.Areas.Sales.Models.Database
{
    [Table("sales_accounts")]
    public class AccountModel
    {
        public Guid ID { get; set; }
          
        /// <summary>
        /// Name of the account.
        /// </summary>
        [Required(ErrorMessage ="Account name is required")]
        public String AccountName { get; set; }
        
        [Display(Name = "Phone Number")]
        public String PhoneNumber { get; set; }
        
        [Required(ErrorMessage = "Select country")]
        public Guid Country { get; set; }
        
        /// <summary>
        /// To whom this account has been assigned (Foreign Key).
        /// </summary>
        [Display(Name = "Assigned To")]
        public Guid AssignedTo { get; set; }

        /// <summary>
        /// Industry for this account.
        /// </summary>
        [Required(ErrorMessage ="Select industry")]
        public Guid Industry { get; set; }
        
        
        [RegularExpression(@"^http(s?)\:\/\/[0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*(:(0-9)*)*(\/?)([a-zA-Z0-9\-\.\?\,\'\/\\\+&amp;%\$#_]*)?$", ErrorMessage = "Entered website format is not valid")]
        public String Website { get; set; }

       
        [Display(Name = "Fax Number")]
        public String FaxNumber { get; set; }

        
        [Display(Name = "Company Size")]
        [Required(ErrorMessage = "Select company size")]

        public Guid CompanySize { get; set; }

        public String GSTNumber { get; set; }

        public String BillingAddressLine1 { get; set; }

        public String BillingAddressLine2 { get; set; }


        [RegularExpression(@"^[a-zA-Z, ]+$", ErrorMessage = "Use letters only")]
        public String BillingCityState { get; set; }

        public String ShippingAddressLine1 { get; set; }

        public String ShippingAddressLine2 { get; set; }


        [RegularExpression(@"^[a-zA-Z, ]+$", ErrorMessage = "Use letters only")]
        public String ShippingCityState { get; set; }


        [StringLength(8)]
        public string ZipCode { get; set; }


        [DataType(DataType.PhoneNumber)]
        [StringLength(8)]
        public String ShippingZipCode { get; set; }


        [Required(ErrorMessage = "Select shipping country")]
        [Display(Name = "Shipping Country")]

        public Guid ShippingCountry { get; set; }

        public Guid CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public Guid ModifiedBy { get; set; }

        public DateTime ModifiedOn { get; set; }
        public int Percentage { get; set; }

        public void Save()
        {
            AccountManager.SaveAccount(this);
        }
    }
    public static class AccountManager
    {
        public static IEnumerable<AccountListItem> GetAccounts()
        {
            SalesDbContext context = new SalesDbContext();

            return context.Database.SqlQuery<AccountListItem>("Select a.*,b.CountryName, c.[Name] as IndustryName ,f.[UserName] CreatedByName,g.[UserName] ModifiedByName,d.[Name] CompanySizeName,e.[UserName] Assigned_To From sales_accounts a " +
            "left outer join sales_countries b on a.Country = b.ID " +
            "left outer join sales_masterentries c on a.Industry = c.ID and c.MasterEntryType = 3 " +
            "left outer join sales_masterentries d on a.CompanySize = d.ID and d.MasterEntryType = 1 " +
            "left join AspNetUsers e on a.AssignedTo = e.Id " +
            "left join AspNetUsers f on a.CreatedBy = f.Id " +
            "left join AspNetUsers g on a.ModifiedBy = g.Id ");
            
        }

        public static IEnumerable<CountryWiseAccounts> GetAccountsByCountries()
        {
            SalesDbContext context = new SalesDbContext();

            return context.Database.SqlQuery<CountryWiseAccounts> ("select CountryName, noofaccounts from(Select country, COUNT(*) as noofaccounts from sales_accounts group by Country)K Left join sales_countries b on k.Country = b.ID");

        }

        /// <summary>
        /// Deletes multiple accounts.
        /// </summary>
        /// <param name="ids">Semicolon separated Ids of the accounts to be deleted.</param>
        public static void DeleteAccounts(string ids)
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

                    var records = context.Accounts.Where(x => selectedIds.Contains(x.ID));

                    foreach (var record in records)
                    {
                        context.Accounts.Remove(record);
                    }
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }


        /// <summary>
        /// Assign the multiple accounts to user.
        /// </summary>
        /// <param name="ids">Semicolon separated Ids of the accounts to be assigned.</param>
        /// <param name="userId">Id of the user to which the accounts to be assigned.</param>
        public static void AssignAccounts(string ids, Guid userId)
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

                    var records = context.Accounts.Where(x => selectedIds.Contains(x.ID));

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

        /// <summary>
        /// Remove the assigned user from multiple accounts.
        /// </summary>
        /// <param name="IDs">Semicolon separated Ids of the accounts.</param>
        public static void ResetAssignAccounts(string ids)
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

                    var records = context.Accounts.Where(x => selectedIds.Contains(x.ID));

                    foreach (var record in records)
                    {
                        record.AssignedTo =Guid.Empty ;

                    }

                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public static void SaveAccount(AccountModel accountModel, SalesDbContext context)
        {
            if (context.Accounts.Any(x => x.AccountName.Equals(accountModel.AccountName, StringComparison.CurrentCultureIgnoreCase)
                 && x.ID != accountModel.ID
            ))
            {
                throw new ApplicationException("Account name must be unique.");
            }

            var record = context.Accounts.FirstOrDefault(x => x.ID == accountModel.ID);

            if (record == null)
            {
                accountModel.CreatedOn = DateTime.Now;
                record = context.Accounts.Add(accountModel);
            }

            record.AccountName = accountModel.AccountName;
            record.PhoneNumber = accountModel.PhoneNumber;
            record.Country = accountModel.Country;
            record.Industry = accountModel.Industry;
            record.Website = accountModel.Website;
            record.FaxNumber = accountModel.FaxNumber;
            record.CompanySize = accountModel.CompanySize;
            record.GSTNumber = accountModel.GSTNumber;
            record.BillingAddressLine1 = accountModel.BillingAddressLine1;
            record.BillingAddressLine2 = accountModel.BillingAddressLine2;
            record.BillingCityState = accountModel.BillingCityState;
            record.ShippingAddressLine1 = accountModel.ShippingAddressLine1;
            record.ShippingAddressLine2 = accountModel.ShippingAddressLine2;
            record.ShippingCityState = accountModel.ShippingCityState;
            record.ZipCode = accountModel.ZipCode;
            record.ShippingZipCode = accountModel.ShippingZipCode;
            record.ShippingCountry = accountModel.ShippingCountry;
            record.ModifiedBy = accountModel.ModifiedBy;
            record.ModifiedOn = DateTime.Now;


        }
        public static void SaveAccount(AccountModel accountModel)
        {
            using (SalesDbContext context = new SalesDbContext())
            {
                SaveAccount(accountModel, context);
                context.SaveChanges();
            }
        }

    }
}