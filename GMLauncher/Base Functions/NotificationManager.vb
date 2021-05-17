Public Class NotificationManager
    Public Class NotificationItem
        Public Property Title As String
        Public Property Message As String
        Public Property Icon As ImageSource
        Public Property TimeCreated As TimeSpan
        Public Sub New(_Title As String, _Message As String, _Icon As ImageSource, Optional _TimeCreated As TimeSpan = Nothing)
            Title = _Title
            Message = _Message
            Icon = _Icon
            If _TimeCreated = Nothing Then
                TimeCreated = Now.TimeOfDay
            Else
                TimeCreated = _TimeCreated
            End If
        End Sub
    End Class
    Public Event OnNotificationRecieved(Notification As NotificationItem, Count As Integer)
    Public Event OnNotificationMarkedAsRead(Notification As NotificationItem, Index As Integer, Rest As Integer)
    Private Property _Notifications As New List(Of NotificationItem)
    Public ReadOnly Property Notifications
        Get
            Return _Notifications
        End Get
    End Property
    Public Property ReadNotifications As New List(Of NotificationItem)
    Public Property UnReadNotifications As New List(Of NotificationItem)
    Public Sub SendNotification(Notification As NotificationItem)
        _Notifications.Add(Notification)
        UnReadNotifications.Add(Notification)
        RaiseEvent OnNotificationRecieved(Notification, UnReadNotifications.Count)
    End Sub
    Public Sub MarkAsRead(Index As Integer)
        Dim Notification = UnReadNotifications.Item(Index)
        UnReadNotifications.RemoveAt(Index)
        ReadNotifications.Add(Notification)
        RaiseEvent OnNotificationMarkedAsRead(Notification, Index, UnReadNotifications.Count)
    End Sub
End Class
