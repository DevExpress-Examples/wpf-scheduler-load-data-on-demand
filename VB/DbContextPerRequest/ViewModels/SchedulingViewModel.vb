Imports System.Collections.Generic
Imports System.Data.Entity
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

        Public Overridable Property Resources As List(Of ResourceEntity)

        Public Sub CreateSourceObject(ByVal args As CreateSourceObjectEventArgs)
            If args.ItemType = ItemType.AppointmentItem Then args.Instance = New AppointmentEntity()
        End Sub

        Public Sub FetchAppointments(ByVal args As FetchDataEventArgs)
            Using dbContext = New SchedulingContext()
                Dim res = dbContext.AppointmentEntities.Where(args.GetFetchExpression(Of AppointmentEntity)())
                args.AsyncResult = QueryableExtensions.ToArrayAsync(Of Object)(res)
            End Using
        End Sub

        Public Sub ProcessChanges(ByVal args As AppointmentCRUDEventArgs)
            Using dbContext = New SchedulingContext()
                dbContext.AppointmentEntities.AddRange(args.AddToSource.[Select](Function(x) CType(x.SourceObject, AppointmentEntity)))
                Dim updatedAppts = args.UpdateInSource.[Select](Function(x) CType(x.SourceObject, AppointmentEntity))
                Dim updatedApptIds = updatedAppts.[Select](Function(x) x.Id).ToArray()
                Dim apptsToUpdate = dbContext.AppointmentEntities.Where(Function(x) updatedApptIds.Contains(x.Id))

                For Each appt In updatedAppts
                    AppointmentEntityHelper.CopyProperties(appt, apptsToUpdate.First(Function(x) x.Id = appt.Id))
                Next

                Dim deletedApptIds = args.DeleteFromSource.[Select](Function(x) (CType(x.SourceObject, AppointmentEntity)).Id).ToArray()
                Dim apptsToDelete = dbContext.AppointmentEntities.Where(Function(x) deletedApptIds.Contains(x.Id)).ToArray()
                dbContext.AppointmentEntities.RemoveRange(apptsToDelete)
                dbContext.SaveChanges()
            End Using
        End Sub
    End Class
End Namespace
