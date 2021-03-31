using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using YuktiSolutions.SmartERP.Areas.Sales.Models;
using YuktiSolutions.SmartERP.Areas.Sales.Models.UI;


namespace YuktiSolutions.SmartERP.Areas.Sales.Models.Database
{
    [Table("sales_activities")]
    public class ActivityModel
    {
        [Key]
        public Guid Id { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        public Guid? RelatedAccounts { get; set; }
        public string Status { get; set; }
        public string Location { get; set; }

        /// <summary>
        ///  Entity type is related to contact or lead.
        /// </summary>
        public RelatedEntityType? RelatedEntityType { get; set; }


        public DateTime DueDate { get; set; }
        public Guid? RelatedEntity { get; set; }
        public Guid? AssignTo { get; set; }
        public DateTime StartDateTime { get; set; }
        public ActivityType? ActivityType { get; set; }
        public DateTime EndDateTime { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public Guid? CreatedBy { get; set; }
        public Guid? ModifiedBy { get; set; }
        public void Save()
        {
            ActivityManager.SaveActivity(this);
        }
    }

    public enum ActivityType
    {
        Call,
        Email,
        Task,
        Event
    }

    public enum RelatedEntityType
    {
        Contact,
        Lead
    }


    public static class ActivityManager
    {
        public static IEnumerable<EventActivityListItem> GetActivities()
        {
            SalesDbContext context = new SalesDbContext();

            //return context.LogingActivities;

            return context.Database.SqlQuery<EventActivityListItem>("select  s.*, i.[UserName] CreatedByName, j.[UserName] ModifiedByName ,h.[EmailId] as Assign_To, c.AccountName RelatedAccount " +
                         "from sales_activities s left join sales_contacts t on s.AssignTo = t.id left join sales_accounts c on t.Account = c.ID " +
                         "left join sales_contacts h on s.AssignTo = h.Id left join AspNetUsers i on s.CreatedBy = i.Id " +
                         "left join AspNetUsers j on s.ModifiedBy = j.Id  ");

        }
        public static IEnumerable<EventActivityListItem> GetRelatedAccounts()
        {
            SalesDbContext context = new SalesDbContext();

            //return context.LogingActivities;

            return context.Database.SqlQuery<EventActivityListItem>("select max( s.StartDateTime) StartDateTime ,c.AccountName RelatedAccount" +
                               "from sales_activities s left join sales_contacts t on s.AssignTo = t.id left join sales_accounts c" +
                               " on t.Account = c.ID where EndDateTime < DATEADD(MONTH, DATEDIFF(MONTH, -1, GETDATE()) - 1, -1)"+
                               " group by c.AccountName");

        }

        public static IEnumerable<ActivityModel> GetActivities(ActivityType activityType)
        {
            SalesDbContext context = new SalesDbContext();

            var query = context.LogingActivities.Where(x => x.ActivityType == activityType);

            if (query.Any())
            {

                return query.OrderBy(x => x.Subject);
            }
            else
            {
                return new List<ActivityModel>();
            }
        }
        public static void SaveActivity(ActivityModel activityModel, SalesDbContext context)
        {
            var record = context.LogingActivities.FirstOrDefault(x => x.Id == activityModel.Id);

            DateTime temp = new DateTime(0001, 01, 01);

            if (record == null)
            {

                activityModel.EndDateTime = temp == activityModel.EndDateTime ? DateTime.Now : activityModel.EndDateTime;
                activityModel.StartDateTime = temp == activityModel.StartDateTime ? DateTime.Now : activityModel.StartDateTime;
                activityModel.DueDate = temp == activityModel.DueDate ? DateTime.Now : activityModel.DueDate;
                record = context.LogingActivities.Add(activityModel);
            }
            record.Subject = activityModel.Subject;
            record.StartDateTime = activityModel.StartDateTime;
            record.RelatedAccounts = activityModel.RelatedAccounts;
            record.RelatedEntity = activityModel.RelatedEntity;
            record.RelatedEntityType = activityModel.RelatedEntityType;
            record.ModifiedOn = activityModel.ModifiedOn;
            record.CreatedOn = activityModel.CreatedOn;
            record.StartDateTime = temp == activityModel.StartDateTime ? record.StartDateTime : activityModel.StartDateTime;
            record.EndDateTime = temp == activityModel.EndDateTime ? record.EndDateTime : activityModel.EndDateTime;
            record.DueDate = temp == activityModel.DueDate ? record.DueDate : activityModel.DueDate;
            record.Description = activityModel.Description;
            record.Status = activityModel.Status;
            record.ModifiedBy = activityModel.ModifiedBy;
            record.Location = activityModel.Location;
        }

        public static void AssignActivity(string ids, Guid userId)
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

                    var records = context.LogingActivities.Where(x => selectedIds.Contains(x.Id));

                    foreach (var record in records)
                    {
                        record.AssignTo = userId;

                    }

                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }


        public static void SaveActivity(ActivityModel activityModel)
        {
            using (SalesDbContext context = new SalesDbContext())
            {
                SaveActivity(activityModel, context);
                context.SaveChanges();
            }
        }
    }

}
 