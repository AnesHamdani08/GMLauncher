Public Class ParticlesChanger
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
    Private Sub ParticlesChanger_Initialized(sender As Object, e As EventArgs) Handles Me.Initialized
        Main_Panel.Children.Add(New RectItem(Sub()
                                                 MainWindow.ParticlesManager.Enable()
                                             End Sub) With {.Margin = New Thickness(10, 10, 10, 10), .Message = "Particles : On", .Icon = Utils.OSD_SETTINGS})
        Main_Panel.Children.Add(New RectItem(Sub()
                                                 MainWindow.ParticlesManager.Disable()
                                             End Sub) With {.Margin = New Thickness(10, 10, 10, 10), .Message = "Particles : Off", .Icon = Utils.OSD_SETTINGS})
        Main_Panel.Children.Add(New RectItem(Sub()
                                                 MainWindow.ParticlesManager.Blurry = True
                                                 MainWindow.ParticlesManager.OnBlurApply()
                                             End Sub) With {.Margin = New Thickness(10, 10, 10, 10), .Message = "Particles blur : On", .Icon = Utils.OSD_SETTINGS})
        Main_Panel.Children.Add(New RectItem(Sub()
                                                 MainWindow.ParticlesManager.Blurry = False
                                                 MainWindow.ParticlesManager.OnBlurApply()
                                             End Sub) With {.Margin = New Thickness(10, 10, 10, 10), .Message = "Particles blur : Off", .Icon = Utils.OSD_SETTINGS})
        Main_Panel.Children.Add(New RectItem(Sub()
                                                 MainWindow.ParticlesManager.Color = Brushes.Red.Clone
                                                 MainWindow.ParticlesManager.ApplyColor()
                                             End Sub) With {.Margin = New Thickness(10, 10, 10, 10), .Message = "Particles : Red", .Icon = Utils.OSD_SETTINGS})
        Main_Panel.Children.Add(New RectItem(Sub()
                                                 MainWindow.ParticlesManager.Color = Brushes.Green.Clone
                                                 MainWindow.ParticlesManager.ApplyColor()
                                             End Sub) With {.Margin = New Thickness(10, 10, 10, 10), .Message = "Particles : Green", .Icon = Utils.OSD_SETTINGS})
        Main_Panel.Children.Add(New RectItem(Sub()
                                                 MainWindow.ParticlesManager.Color = Brushes.Yellow.Clone
                                                 MainWindow.ParticlesManager.ApplyColor()
                                             End Sub) With {.Margin = New Thickness(10, 10, 10, 10), .Message = "Particles : Yellow", .Icon = Utils.OSD_SETTINGS})
        Main_Panel.Children.Add(New RectItem(Sub()
                                                 MainWindow.ParticlesManager.Color = Brushes.Violet.Clone
                                                 MainWindow.ParticlesManager.ApplyColor()
                                             End Sub) With {.Margin = New Thickness(10, 10, 10, 10), .Message = "Particles : Violet", .Icon = Utils.OSD_SETTINGS})
        Main_Panel.Children.Add(New RectItem(Sub()
                                                 MainWindow.ParticlesManager.Color = Brushes.Gold.Clone
                                                 MainWindow.ParticlesManager.ApplyColor()
                                             End Sub) With {.Margin = New Thickness(10, 10, 10, 10), .Message = "Particles : Gold", .Icon = Utils.OSD_SETTINGS})
        Main_Panel.Children.Add(New RectItem(Sub()
                                                 MainWindow.ParticlesManager.Color = Brushes.Blue.Clone
                                                 MainWindow.ParticlesManager.ApplyColor()
                                             End Sub) With {.Margin = New Thickness(10, 10, 10, 10), .Message = "Particles : Blue", .Icon = Utils.OSD_SETTINGS})
        Main_Panel.Children.Add(New RectItem(Sub()
                                                 MainWindow.ParticlesManager.Color = Brushes.Black.Clone
                                                 MainWindow.ParticlesManager.ApplyColor()
                                             End Sub) With {.Margin = New Thickness(10, 10, 10, 10), .Message = "Particles : Black", .Icon = Utils.OSD_SETTINGS})
        Main_Panel.Children.Add(New RectItem(Sub()
                                                 MainWindow.ParticlesManager.Color = Brushes.White.Clone
                                                 MainWindow.ParticlesManager.ApplyColor()
                                             End Sub) With {.Margin = New Thickness(10, 10, 10, 10), .Message = "Particles : White", .Icon = Utils.OSD_SETTINGS})
    End Sub
    Private Sub ParticlesChanger_PreviewKeyDown(sender As Object, e As KeyEventArgs) Handles Me.PreviewKeyDown
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
