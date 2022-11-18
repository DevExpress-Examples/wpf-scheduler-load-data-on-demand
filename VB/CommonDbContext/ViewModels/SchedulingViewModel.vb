Imports System.Collections.Generic
Imports System.Data.Entity
Imports System.Linq
Imports DevExpress.Mvvm.Native
Imports DevExpress.Xpf.Scheduling
Imports DXSample.Data

Namespace DXSample.ViewModels
	Public Class SchedulingViewModel
		Private dbContext As SchedulingContext
		Public Sub New()
			dbContext = New SchedulingContext()
			Resources = dbContext.ResourceEntities.ToList()
		End Sub
		Public Overridable Property Resources() As List(Of ResourceEntity)
		Public Sub CreateSourceObject(ByVal args As CreateSourceObjectEventArgs)
			If args.ItemType = ItemType.AppointmentItem Then
				args.Instance = New AppointmentEntity()
			End If
		End Sub
		Public Sub FetchAppointments(ByVal args As FetchDataEventArgs)
			Dim res = dbContext.AppointmentEntities.Where(args.GetFetchExpression(Of AppointmentEntity)())
			args.AsyncResult = QueryableExtensions.ToArrayAsync(Of Object)(res)
		End Sub
		Public Sub ProcessChanges(ByVal args As AppointmentCRUDEventArgs)
			dbContext.AppointmentEntities.AddRange(args.AddToSource.Select(Function(x) CType(x.SourceObject, AppointmentEntity)))
			dbContext.AppointmentEntities.RemoveRange(args.DeleteFromSource.Select(Function(x) CType(x.SourceObject, AppointmentEntity)))
			dbContext.SaveChanges()
		End Sub
	End Class
End Namespace