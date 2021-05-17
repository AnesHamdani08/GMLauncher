Public Class DiscordRPCChanger
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
                                                 My.Settings.DiscordRPC = True
                                                 My.Settings.Save()
                                                 CType(Application.Current.MainWindow, MainWindow).DiscordRPCClient = New DiscordRPC.DiscordRpcClient(841670098477645884)
                                                 CType(Application.Current.MainWindow, MainWindow).DiscordRPCClient.Initialize()
                                             End Sub) With {.Margin = New Thickness(10, 10, 10, 10), .Message = "Discord RPC: On ", .Icon = Utils.OSD_SETTINGS})
        Main_Panel.Children.Add(New RectItem(Sub()
                                                 My.Settings.DiscordRPC = False
                                                 My.Settings.Save()
                                                 If CType(Application.Current.MainWindow, MainWindow).DiscordRPCClient.IsInitialized Then
                                                     CType(Application.Current.MainWindow, MainWindow).DiscordRPCClient.Deinitialize()
                                                 End If
                                             End Sub) With {.Margin = New Thickness(10, 10, 10, 10), .Message = "Discord RPC: Off", .Icon = Utils.OSD_SETTINGS})
    End Sub
    Private Sub DateTimeChanger_PreviewKeyDown(sender As Object, e As KeyEventArgs) Handles Me.PreviewKeyDown
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
End Class
