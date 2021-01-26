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
				var updatedAppts = args.UpdateInSource.Select(x => (AppointmentEntity)x.SourceObject);
				var updatedApptIds = updatedAppts.Select(x => x.Id).ToArray();
				var apptsToUpdate = dbContext.AppointmentEntities.Where(x => updatedApptIds.Contains(x.Id));
				foreach(var appt in updatedAppts)
					AppointmentEntityHelper.CopyProperties(appt, apptsToUpdate.First(x => x.Id == appt.Id));
				var deletedApptIds = args.DeleteFromSource.Select(x => ((AppointmentEntity)x.SourceObject).Id).ToArray();
				var apptsToDelete = dbContext.AppointmentEntities.Where(x => deletedApptIds.Contains(x.Id)).ToArray();
				dbContext.AppointmentEntities.RemoveRange(apptsToDelete);
				dbContext.SaveChanges();
			}
		}
    }
}
