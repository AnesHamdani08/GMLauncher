Public Class DateTimeChanger
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
    Private Sub DateTimeChanger_Initialized(sender As Object, e As EventArgs) Handles Me.Initialized
        Main_Panel.Children.Add(New RectItem(Sub()
                                                 CType(Main_Panel.Children.Item(1), RectItem).Message = "Date and Time: " & Now.ToString(My.Settings.TimeFormat) & " " & Now.ToString(My.Settings.DateFormat)
                                             End Sub) With {.Margin = New Thickness(10, 10, 10, 10), .Message = "Date and Time: " & Now.ToString(My.Settings.TimeFormat) & " " & Now.ToString(My.Settings.DateFormat), .Icon = Utils.OSD_SETTINGS})
        Main_Panel.Children.Add(New RectItem(Sub()
                                                 Message.Text = "Time Format"
                                                 InputBox.BeginAnimation(OpacityProperty, Utils.DAnimOneOneHundred)
                                                 Input.Focusable = True
                                                 Input.Tag = "1"
                                                 Input.Focus()
                                             End Sub) With {.Margin = New Thickness(10, 10, 10, 10), .Message = "Date Format: " & My.Settings.DateFormat & " " & Now.ToString(My.Settings.DateFormat), .Icon = Utils.OSD_SETTINGS})
        Main_Panel.Children.Add(New RectItem(Sub()
                                                 Message.Text = "Date Format"
                                                 InputBox.BeginAnimation(OpacityProperty, Utils.DAnimOneOneHundred)
                                                 Input.Focusable = True
                                                 Input.Tag = "2"
                                                 Input.Focus()
                                             End Sub) With {.Margin = New Thickness(10, 10, 10, 10), .Message = "Time Format: " & My.Settings.TimeFormat & " " & Now.ToString(My.Settings.TimeFormat), .Icon = Utils.OSD_SETTINGS})
    End Sub
    Private Sub Input_PreviewKeyUp(sender As Object, e As KeyEventArgs) Handles Input.PreviewKeyUp
        If e.Key = Key.Enter AndAlso Not String.IsNullOrEmpty(Input.Text.Trim(" ")) Then
            If Input.Tag = "1" Then
                My.Settings.DateFormat = Input.Text
                My.Settings.Save()
                CType(Main_Panel.Children.Item(2), RectItem).Message = "Date Format: " & My.Settings.DateFormat & " " & Now.ToString(My.Settings.DateFormat)
                CType(Main_Panel.Children.Item(1), RectItem).Message = "Date and Time: " & Now.ToString(My.Settings.TimeFormat) & " " & Now.ToString(My.Settings.DateFormat)
                Input.Focusable = False
                InputBox.BeginAnimation(OpacityProperty, Utils.DAnimZeroOneHundred)
                Main_Panel.Children.Item(FocusedItem).Focus()
            ElseIf Input.Tag = "2" Then
                My.Settings.TimeFormat = Input.Text
                My.Settings.Save()
                CType(Main_Panel.Children.Item(3), RectItem).Message = "Time Format: " & My.Settings.TimeFormat & " " & Now.ToString(My.Settings.TimeFormat)
                CType(Main_Panel.Children.Item(1), RectItem).Message = "Date and Time: " & Now.ToString(My.Settings.TimeFormat) & " " & Now.ToString(My.Settings.DateFormat)
                Input.Focusable = False
                InputBox.BeginAnimation(OpacityProperty, Utils.DAnimZeroOneHundred)
                Main_Panel.Children.Item(FocusedItem).Focus()
            End If
        End If
    End Sub
    Private Sub DateTimeChanger_PreviewKeyDown(sender As Object, e As KeyEventArgs) Handles Me.PreviewKeyDown
        MainWindow.CaptureFocus = False
        If Input.IsFocused Then Exit Sub
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
End Class
