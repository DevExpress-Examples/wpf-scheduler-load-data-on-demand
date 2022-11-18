using System;
using System.Collections.Specialized;
using System.Windows;
using System.Windows.Controls;
using DevExpress.Mvvm;
using DevExpress.Mvvm.Native;
using DevExpress.Xpf.Scheduling;

namespace DXSample.Views {
    public partial class SchedulingView : UserControl {
        public SchedulerControl Scheduler { get { return this.scheduler; } }
        public SchedulingView() {
            InitializeComponent();
            scheduler.ResourceItems.CollectionChanged += OnResourceItemsCollectionChanged;
            scheduler.AppointmentAdded += (d, e) => e.Appointments.ForEach(x => LogChange(x, "added"));
            scheduler.AppointmentEdited += (d, e) => e.Appointments.ForEach(x => LogChange(x, "edited"));
            scheduler.AppointmentRemoved += (d, e) => e.Appointments.ForEach(x => LogChange(x, "removed"));
            scheduler.AppointmentRestored += (d, e) => e.Appointments.ForEach(x => LogChange(x, "restored"));
            scheduler.DataSource.FetchAppointments += (d, e) => LogFetch(e);
            refreshData.Click += OnRefreshDataClick;
        }
        async void LogFetch(FetchDataEventArgs e) {
            if(e.AsyncResult != null)
                await e.AsyncResult;
            int apptCount = e.Result != null ? e.Result.Length : e.AsyncResult.Result.Length;
            var intervalStr = e.Interval.ToString("({0:d})-({1:d})", null);
            var format = "{0}: {1} appointments fetched";
            logTextEdit.Text += string.Format(format, intervalStr, apptCount) + Environment.NewLine;
        }
        void LogChange(AppointmentItem appt, string action) {
            logTextEdit.Text += string.Format("Appointment '{0}' {1}; changes saved to database", appt.Subject, action) + Environment.NewLine;
        }
        void OnRefreshDataClick(object sender, RoutedEventArgs e) {
            scheduler.RefreshData(RefreshDataKind.Appointments);
        }
        void ClearLog(object sender, RoutedEventArgs e) {
            logTextEdit.Text = string.Empty;
        }
        void OnResourceItemsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e) {
            for(int i = 0; i < scheduler.ResourceItems.Count; i++)
                scheduler.ResourceItems[i].Visible = i < 2;
            scheduler.ResourceItems.CollectionChanged -= OnResourceItemsCollectionChanged;
        }
    }
}
