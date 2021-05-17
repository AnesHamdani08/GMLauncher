Public Class StatusItem
    Public Property OnClick As Action = Nothing
    Public Sub New(Title As String, Icon As ImageSource, OnClickDo As Action)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Main_Icon.Source = Icon
        OnClick = OnClickDo
    End Sub
    Private Sub StatusItem_GotFocus(sender As Object, e As RoutedEventArgs) Handles Me.GotFocus
        BeginAnimation(HeightProperty, New Animation.DoubleAnimation(120, New Duration(TimeSpan.FromMilliseconds(100))))
    End Sub

    Private Sub StatusItem_Initialized(sender As Object, e As EventArgs) Handles Me.Initialized
        Dim s As Style = New Style()
        s.Setters.Add(New Setter(FocusVisualStyleProperty, Nothing))
        FocusVisualStyle = s
    End Sub

    Private Sub StatusItem_LostFocus(sender As Object, e As RoutedEventArgs) Handles Me.LostFocus
        BeginAnimation(HeightProperty, New Animation.DoubleAnimation(100, New Duration(TimeSpan.FromMilliseconds(100))))
    End Sub

    Private Sub StatusItem_PreviewKeyUp(sender As Object, e As KeyEventArgs) Handles Me.PreviewKeyUp
        If e.Key = Key.Enter AndAlso OnClick IsNot Nothing Then
            OnClick.Invoke
        End If
    End Sub
End Class
