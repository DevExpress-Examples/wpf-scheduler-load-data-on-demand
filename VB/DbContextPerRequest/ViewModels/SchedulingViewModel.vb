Imports System.Collections.Generic
Imports System.Linq
Imports DevExpress.Mvvm.Native
Imports DevExpress.Xpf.Scheduling
Imports DXSample.Data

Namespace DXSample.ViewModels
	Public Class SchedulingViewModel
		Public Sub New()
			Using dbContext = New SchedulingContext()
				Resources = dbContext.ResourceEntities.ToList()
			End Using
		End Sub
		Public Overridable Property Resources() As List(Of ResourceEntity)
		Public Sub CreateSourceObject(ByVal args As CreateSourceObjectEventArgs)
			If args.ItemType = ItemType.AppointmentItem Then
				args.Instance = New AppointmentEntity()
			End If
		End Sub
		Public Sub FetchAppointments(ByVal args As FetchDataEventArgs)
			Using dbContext = New SchedulingContext()
				args.Result = dbContext.AppointmentEntities.Where(args.GetFetchExpression(Of AppointmentEntity)()).ToArray()
			End Using
		End Sub
		Public Sub ProcessChanges(ByVal args As AppointmentCRUDEventArgs)
			Using dbContext = New SchedulingContext()
				dbContext.AppointmentEntities.AddRange(args.AddToSource.Select(Function(x) CType(x.SourceObject, AppointmentEntity)))
				For Each appt In args.UpdateInSource.Select(Function(x) CType(x.SourceObject, AppointmentEntity))
					AppointmentEntityHelper.CopyProperties(appt, dbContext.AppointmentEntities.Find(appt.Id))
				Next appt
				For Each appt In args.DeleteFromSource.Select(Function(x) CType(x.SourceObject, AppointmentEntity))
					dbContext.AppointmentEntities.Remove(dbContext.AppointmentEntities.Find(appt.Id))
				Next appt
				dbContext.SaveChanges()
			End Using
		End Sub
	End Class
End Namespace