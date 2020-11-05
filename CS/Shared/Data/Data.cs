using System;
using System.Data.Common;
using System.Data.Entity;
using System.Data.SQLite;

namespace DXSample.Data {
    public class AppointmentEntity {
        public int Id { get; set; }
        public bool AllDay { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public DateTime QueryStart { get; set; }
        public DateTime QueryEnd { get; set; }
        public int AppointmentType { get; set; }
        public string RecurrenceInfo { get; set; }
        public string ReminderInfo { get; set; }
        public int? ResourceId { get; set; }
        public int Label { get; set; }
        public int Status { get; set; }
    }
    public class ResourceEntity {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Group { get; set; }
    }
    public class SchedulingContext : DbContext {
        const string FileName = @"..\..\..\scheduling.db";
        public SchedulingContext() : base(CreateConnection(), true) { }
        static DbConnection CreateConnection() {
            var connection = DbProviderFactories.GetFactory("System.Data.SQLite.EF6").CreateConnection();
            connection.ConnectionString = new SQLiteConnectionStringBuilder { DataSource = FileName }.ConnectionString;
            return connection;
        }
        public DbSet<AppointmentEntity> AppointmentEntities { get; set; }
        public DbSet<ResourceEntity> ResourceEntities { get; set; }
    }

    public class AppointmentEntityHelper {
        public static void CopyProperties(AppointmentEntity source, AppointmentEntity target) {
            target.AllDay = source.AllDay;
            target.AppointmentType = source.AppointmentType;
            target.Description = source.Description;
            target.End = source.End;
            target.Label = source.Label;
            target.Location = source.Location;
            target.QueryEnd = source.QueryEnd;
            target.QueryStart = source.QueryStart;
            target.RecurrenceInfo = source.RecurrenceInfo;
            target.ReminderInfo = source.ReminderInfo;
            target.ResourceId = source.ResourceId;
            target.Start = source.Start;
            target.Status = source.Status;
            target.Subject = source.Subject;
        }
    }
}
