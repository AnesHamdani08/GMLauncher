Imports System.ComponentModel
Imports System.Runtime.CompilerServices

Public Class ListGameItem
    Implements INotifyPropertyChanged

    Private _num As Integer
    Private _title As String
    Private _source As String
    Public Sub New(ByVal pnum As Integer, ByVal ptitle As String, ByVal psource As String, <CallerMemberName> ByVal Optional caller As String = Nothing)
        Num = pnum
        Title = ptitle
        _source = psource
    End Sub

    Public Property Num As Integer
        Get
            Return _num
        End Get
        Set(ByVal value As Integer)
            If _num = value Then Return
            _num = value
            OnPropertyChanged()
        End Set
    End Property

    Public Property Title As String
        Get
            Return _title
        End Get
        Set(ByVal value As String)
            If _title = value Then Return
            _title = value
            OnPropertyChanged()
        End Set
    End Property

    Public Property Source As String
        Get
            Return _source
        End Get
        Set(ByVal value As String)
            If _source = value Then Return
            _source = value
            OnPropertyChanged()
        End Set
    End Property
    Public Property IconSource As String
    Public Property Icon As ImageSource
    Public Property BigIconSource As String
    Public Property BigIcon As ImageSource
    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

    Protected Overridable Sub OnPropertyChanged(<CallerMemberName> ByVal Optional propertyName As String = Nothing)
        RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propertyName))
    End Sub
End Class
