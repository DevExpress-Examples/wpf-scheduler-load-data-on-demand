Imports System
Imports System.Collections.Specialized
Imports System.Windows
Imports System.Windows.Controls
Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.Native
Imports DevExpress.Xpf.Scheduling

Namespace DXSample.Views
	Partial Public Class SchedulingView
		Inherits UserControl

		Public ReadOnly Property SchedulerRef() As SchedulerControl
			Get
				Return Me.scheduler
			End Get
		End Property
		Public Sub New()
			InitializeComponent()
			AddHandler scheduler.ResourceItems.CollectionChanged, AddressOf OnResourceItemsCollectionChanged
			AddHandler scheduler.AppointmentAdded, Sub(d, e) e.Appointments.ForEach(Sub(x) LogChange(x, "added"))
			AddHandler scheduler.AppointmentEdited, Sub(d, e) e.Appointments.ForEach(Sub(x) LogChange(x, "edited"))
			AddHandler scheduler.AppointmentRemoved, Sub(d, e) e.Appointments.ForEach(Sub(x) LogChange(x, "removed"))
			AddHandler scheduler.AppointmentRestored, Sub(d, e) e.Appointments.ForEach(Sub(x) LogChange(x, "restored"))
			AddHandler scheduler.DataSource.FetchAppointments, Sub(d, e) LogFetch(e.Interval, e.Result.Length)
			AddHandler refreshData.Click, AddressOf OnRefreshDataClick
		End Sub
		Private Sub LogFetch(ByVal interval As DateTimeRange, ByVal count As Integer)
			Dim intervalStr = interval.ToString("({0:d})-({1:d})", Nothing)
			Dim format = "{0}: {1} appointments fetched"
			logTextEdit.Text += String.Format(format, intervalStr, count) & Environment.NewLine
		End Sub
		Private Sub LogChange(ByVal appt As AppointmentItem, ByVal action As String)
			logTextEdit.Text += String.Format("Appointment '{0}' {1}; changes saved to database", appt.Subject, action) & Environment.NewLine
		End Sub
		Private Sub OnRefreshDataClick(ByVal sender As Object, ByVal e As RoutedEventArgs)
			scheduler.RefreshData(RefreshDataKind.Appointments)
		End Sub
		Private Sub ClearLog(ByVal sender As Object, ByVal e As RoutedEventArgs)
			logTextEdit.Text = String.Empty
		End Sub
		Private Sub OnResourceItemsCollectionChanged(ByVal sender As Object, ByVal e As NotifyCollectionChangedEventArgs)
			For i As Integer = 0 To scheduler.ResourceItems.Count - 1
				scheduler.ResourceItems(i).Visible = i < 2
			Next i
			RemoveHandler scheduler.ResourceItems.CollectionChanged, AddressOf OnResourceItemsCollectionChanged
		End Sub
	End Class
End Namespace
