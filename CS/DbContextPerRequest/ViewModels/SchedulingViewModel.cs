using System.Collections.Generic;
using System.Linq;
using DevExpress.Mvvm.Native;
using DevExpress.Xpf.Scheduling;
using DXSample.Data;

namespace DXSample.ViewModels {
    public class SchedulingViewModel {
        public SchedulingViewModel() {
            using(var dbContext = new SchedulingContext()) {
                Resources = dbContext.ResourceEntities.ToList();
            }
        }
        public virtual List<ResourceEntity> Resources { get; set; }
        public void CreateSourceObject(CreateSourceObjectEventArgs args) {
            if(args.ItemType == ItemType.AppointmentItem)
                args.Instance = new AppointmentEntity();
        }
        public void FetchAppointments(FetchDataEventArgs args) {
            using(var dbContext = new SchedulingContext()) {
                args.Result = dbContext.AppointmentEntities.Where(args.GetFetchExpression<AppointmentEntity>()).ToArray();
            }
        }
        public void ProcessChanges(AppointmentCRUDEventArgs args) {
            using(var dbContext = new SchedulingContext()) {
                dbContext.AppointmentEntities.AddRange(args.AddToSource.Select(x => (AppointmentEntity)x.SourceObject));
                foreach(var appt in args.UpdateInSource.Select(x => (AppointmentEntity)x.SourceObject))
                    AppointmentEntityHelper.CopyProperties(appt, dbContext.AppointmentEntities.Find(appt.Id));
                foreach(var appt in args.DeleteFromSource.Select(x => (AppointmentEntity)x.SourceObject))
                    dbContext.AppointmentEntities.Remove(dbContext.AppointmentEntities.Find(appt.Id));
                dbContext.SaveChanges();
            }
        }
    }
}