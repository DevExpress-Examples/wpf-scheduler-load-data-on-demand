Imports System
Imports System.Data.Common
Imports System.Data.Entity
Imports System.Data.SQLite

Namespace DXSample.Data
	Public Class AppointmentEntity
		Public Property Id() As Integer
		Public Property AllDay() As Boolean
		Public Property Subject() As String
		Public Property Description() As String
		Public Property Location() As String
		Public Property Start() As DateTime
		Public Property [End]() As DateTime
		Public Property QueryStart() As DateTime
		Public Property QueryEnd() As DateTime
		Public Property AppointmentType() As Integer
		Public Property RecurrenceInfo() As String
		Public Property ReminderInfo() As String
		Public Property ResourceId() As Integer?
		Public Property Label() As Integer
		Public Property Status() As Integer
	End Class
	Public Class ResourceEntity
		Public Property Id() As Integer
		Public Property Description() As String
		Public Property Group() As String
	End Class
	Public Class SchedulingContext
		Inherits DbContext

		Private Const FileName As String = "..\..\..\scheduling.db"
		Public Sub New()
			MyBase.New(CreateConnection(), True)
		End Sub
		Private Shared Function CreateConnection() As DbConnection
			Dim connection = DbProviderFactories.GetFactory("System.Data.SQLite.EF6").CreateConnection()
			connection.ConnectionString = New SQLiteConnectionStringBuilder With {.DataSource = FileName}.ConnectionString
			Return connection
		End Function
		Public Property AppointmentEntities() As DbSet(Of AppointmentEntity)
		Public Property ResourceEntities() As DbSet(Of ResourceEntity)
	End Class

	Public Class AppointmentEntityHelper
		Public Shared Sub CopyProperties(ByVal source As AppointmentEntity, ByVal target As AppointmentEntity)
			target.AllDay = source.AllDay
			target.AppointmentType = source.AppointmentType
			target.Description = source.Description
			target.End = source.End
			target.Label = source.Label
			target.Location = source.Location
			target.QueryEnd = source.QueryEnd
			target.QueryStart = source.QueryStart
			target.RecurrenceInfo = source.RecurrenceInfo
			target.ReminderInfo = source.ReminderInfo
			target.ResourceId = source.ResourceId
			target.Start = source.Start
			target.Status = source.Status
			target.Subject = source.Subject
		End Sub
	End Class
End Namespace
