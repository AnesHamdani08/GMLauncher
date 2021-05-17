Public Class HomeBackgroundChanger
    Dim _FocusedItem As Integer = 1
    Dim MainWindow As MainWindow = TryCast(Application.Current.MainWindow, MainWindow)
    Public Property OnBack As Action = Nothing
    Property FocusedItem As Integer
        Get
            Return _FocusedItem
        End Get
        Set(value As Integer)
            _FocusedItem = value
            Main_Panel.Children(value).Focus()
        End Set
    End Property
    Private Sub HomeBackgroundChanger_Initialized(sender As Object, e As EventArgs) Handles Me.Initialized
        Main_Panel.Children.Add(New RectItem(Sub()
                                                 MainWindow.Home_Background.Visibility = Visibility.Visible
                                             End Sub) With {.Margin = New Thickness(10, 10, 10, 10), .Message = "Wave : On", .Icon = Utils.OSD_SETTINGS})
        Main_Panel.Children.Add(New RectItem(Sub()
                                                 MainWindow.Home_Background.Visibility = Visibility.Hidden
                                             End Sub) With {.Margin = New Thickness(10, 10, 10, 10), .Message = "Wave : Off", .Icon = Utils.OSD_SETTINGS})
        Main_Panel.Children.Add(New RectItem(Sub()
                                                 MainWindow.Home_Background.Fill = Brushes.Red
                                             End Sub) With {.Margin = New Thickness(10, 10, 10, 10), .Message = "Wave : Red", .Icon = Utils.OSD_SETTINGS})
        Main_Panel.Children.Add(New RectItem(Sub()
                                                 MainWindow.Home_Background.Fill = Brushes.Green
                                             End Sub) With {.Margin = New Thickness(10, 10, 10, 10), .Message = "Wave : Green", .Icon = Utils.OSD_SETTINGS})
        Main_Panel.Children.Add(New RectItem(Sub()
                                                 MainWindow.Home_Background.Fill = Brushes.Yellow
                                             End Sub) With {.Margin = New Thickness(10, 10, 10, 10), .Message = "Wave : Yellow", .Icon = Utils.OSD_SETTINGS})
        Main_Panel.Children.Add(New RectItem(Sub()
                                                 MainWindow.Home_Background.Fill = Brushes.Violet
                                             End Sub) With {.Margin = New Thickness(10, 10, 10, 10), .Message = "Wave : Violet", .Icon = Utils.OSD_SETTINGS})
        Main_Panel.Children.Add(New RectItem(Sub()
                                                 MainWindow.Home_Background.Fill = Brushes.Gold
                                             End Sub) With {.Margin = New Thickness(10, 10, 10, 10), .Message = "Particles : Gold", .Icon = Utils.OSD_SETTINGS})
        Main_Panel.Children.Add(New RectItem(Sub()
                                                 MainWindow.Home_Background.Fill = Brushes.Blue
                                             End Sub) With {.Margin = New Thickness(10, 10, 10, 10), .Message = "Particles : Blue", .Icon = Utils.OSD_SETTINGS})
        Main_Panel.Children.Add(New RectItem(Sub()
                                                 MainWindow.Home_Background.Fill = Brushes.Black
                                             End Sub) With {.Margin = New Thickness(10, 10, 10, 10), .Message = "Particles : Black", .Icon = Utils.OSD_SETTINGS})
        Main_Panel.Children.Add(New RectItem(Sub()
                                                 MainWindow.Home_Background.Fill = Brushes.White
                                             End Sub) With {.Margin = New Thickness(10, 10, 10, 10), .Message = "Particles : White", .Icon = Utils.OSD_SETTINGS})
    End Sub
    Private Sub HomeBackgroundChanger_PreviewKeyDown(sender As Object, e As KeyEventArgs) Handles Me.PreviewKeyDown
        MainWindow.CaptureFocus = False
        If e.Key = Key.Down Then
            If FocusedItem <> Main_Panel.Children.Count - 1 Then
                FocusedItem += 1
            Else
                FocusedItem = 1
            End If
            Utils.S_Navigation_Player.Play()
        ElseIf e.Key = Key.Up Then
            If FocusedItem > 1 Then
                FocusedItem -= 1
            Else
                FocusedItem = Main_Panel.Children.Count - 1
            End If
            Utils.S_Navigation_Player.Play()
        ElseIf e.Key = Key.Left Then
            If OnBack IsNot Nothing Then
                OnBack.Invoke
                Utils.S_Back_Player.Play()
            End If
        End If
    End Sub

    Private Sub Sc_PreviewKeyDown(sender As Object, e As KeyEventArgs) Handles Sc.PreviewKeyDown
        e.Handled = True
    End Sub
End Class
