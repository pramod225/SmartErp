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
    [Table("sales_countries")]
    public class CountryModel
    {
        [Key]
        public Guid ID { get; set; }
        [Display(Name = " Country Code")]
        [Required(ErrorMessage = "Enter Country Code")]
        public string CountryCode { get; set; }

        [Display(Name = " Country Name")]
        [Required(ErrorMessage = "Enter Country Name")]
        public string CountryName { get; set; }

        public void Save()
        {
            CountryManager.SaveCountry(this);
           
        }
    }
    public static class CountryManager
    {

        public static IEnumerable<CountryModel> GetCountries()
        {
            SalesDbContext context = new SalesDbContext();

            return context.Countries;

        }


        public static void DeleteCountry(CountryModel countryModel)
        {
            using (SalesDbContext context = new SalesDbContext())
            {
                try
                {
                    var record = context.Countries.Where(x => x.ID == countryModel.ID).FirstOrDefault();
                    context.Countries.Remove(record);
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }


        public static void SaveCountry(CountryModel countryModel, SalesDbContext context)
        {
            if (context.Countries.Any(x => x.CountryName.Equals(countryModel.CountryName, StringComparison.CurrentCultureIgnoreCase)
                 && x.ID != countryModel.ID
            ))
            {
                throw new ApplicationException("Country name must be unique.");
            }

            var record = context.Countries.FirstOrDefault(x => x.ID == countryModel.ID);

            if (record == null)
            {
                record = context.Countries.Add(countryModel);
            }

            record.CountryCode = countryModel.CountryCode;
            record.CountryName = countryModel.CountryName;
        }

        public static void SaveCountry(CountryModel countryModel)
        {
            using (SalesDbContext context = new SalesDbContext())
            {
                SaveCountry(countryModel, context);
                context.SaveChanges();
            }
        }

    }
}