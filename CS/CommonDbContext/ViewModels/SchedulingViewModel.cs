using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DevExpress.Mvvm.Native;
using DevExpress.Xpf.Scheduling;
using DXSample.Data;

namespace DXSample.ViewModels {
    public class SchedulingViewModel {
        SchedulingContext dbContext;
        public SchedulingViewModel() {
            dbContext = new SchedulingContext();
            Resources = dbContext.ResourceEntities.ToList();
        }
        public virtual List<ResourceEntity> Resources { get; set; }
        public void CreateSourceObject(CreateSourceObjectEventArgs args) {
            if(args.ItemType == ItemType.AppointmentItem)
                args.Instance = new AppointmentEntity();
        }
        public void FetchAppointments(FetchDataEventArgs args) {
            args.AsyncResult = dbContext.AppointmentEntities.
                Where(args.GetFetchExpression<AppointmentEntity>()).
                ToArrayAsync<object>();
        }
        public void ProcessChanges(AppointmentCRUDEventArgs args) {
            dbContext.AppointmentEntities.AddRange(args.AddToSource.Select(x => (AppointmentEntity)x.SourceObject));
            dbContext.AppointmentEntities.RemoveRange(args.DeleteFromSource.Select(x => (AppointmentEntity)x.SourceObject));
            dbContext.SaveChanges();
        }
    }
}