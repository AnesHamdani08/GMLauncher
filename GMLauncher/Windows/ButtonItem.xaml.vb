Public Class ButtonItem
    Public Property Title As String
    Public Property Icon As ImageSource
    Public Property Source As String
    Dim IsFocusBoxFull As Boolean = False
    Dim WithEvents BrdTmr As New Forms.Timer With {.Interval = 1000}
    Public Property OnClick As Action = Nothing
    Private Sub GameItem_Initialized(sender As Object, e As EventArgs) Handles Me.Initialized
        Dim s As Style = New Style()
        s.Setters.Add(New Setter(FocusVisualStyleProperty, Nothing))
        FocusVisualStyle = s
    End Sub
    Private Sub RectItem_GotFocus(sender As Object, e As RoutedEventArgs) Handles Me.GotFocus
        Main_Border.BeginAnimation(BorderThicknessProperty, New Animation.ThicknessAnimation(New Thickness(1, 1, 1, 1), New Duration(TimeSpan.FromMilliseconds(200))))
        BrdTmr.Start()
    End Sub

    Private Sub RectItem_LostFocus(sender As Object, e As RoutedEventArgs) Handles Me.LostFocus
        BrdTmr.Stop()
        Main_Border.BeginAnimation(BorderThicknessProperty, New Animation.ThicknessAnimation(New Thickness(0, 0, 0, 0), New Duration(TimeSpan.FromMilliseconds(200))))
    End Sub

    Private Sub BrdTmr_Tick(sender As Object, e As EventArgs) Handles BrdTmr.Tick
        If IsFocusBoxFull Then
            Main_Border.BeginAnimation(BorderThicknessProperty, New Animation.ThicknessAnimation(New Thickness(1, 1, 1, 1), New Duration(TimeSpan.FromSeconds(1))))
            IsFocusBoxFull = False
        Else
            Main_Border.BeginAnimation(BorderThicknessProperty, New Animation.ThicknessAnimation(New Thickness(3, 3, 3, 3), New Duration(TimeSpan.FromSeconds(1))))
            IsFocusBoxFull = True
        End If
    End Sub

    Private Sub ButtonItem_PreviewKeyUp(sender As Object, e As KeyEventArgs) Handles Me.PreviewKeyUp
        If e.Key = Key.Enter Then
            If OnClick IsNot Nothing Then
                OnClick.Invoke
            End If
        End If
    End Sub
End Class
