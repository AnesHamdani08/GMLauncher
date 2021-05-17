Imports System.ComponentModel
Imports DiscordRPC.Message
Imports GMLauncher

Class MainWindow
    Public Enum OsdPlace
        Games
        Status
        Settings
        Home
    End Enum
    Dim _GamesFocusedItem As Integer = 0
    Property GamesFocusedItem As Integer
        Get
            Return _GamesFocusedItem
        End Get
        Set(value As Integer)
            _GamesFocusedItem = value
            Main_Games.Children.Item(value).Focus()
            'Dim loc As Point = Main_Games.Children.Item(value).TransformToAncestor(Main_Games).Transform(New Point(0, 0)) 'As New Thickness((value * 200) + 5, 106, 0, 0) 
            'Home_BigGameLogo.Margin = New Thickness(loc.X, 106, 0, 0)
            'Dim currentPoint As Point = Main_Games.Children.Item(value).TransformToAncestor(Main_Games).Transform(New Point(0, 0))
            'currentPoint = Main_Games.TransformToAncestor(sc).Transform(New Point(0, 0))
            'currentPoint = sc.TransformToAncestor(Me).Transform(currentPoint)
            'Home_BigGameLogo.Margin = New Thickness(currentPoint.X + 10 + (5 * (GamesFocusedItem + 1)), currentPoint.Y, 0, 0)
        End Set
    End Property
    Dim _StatusFocusedItem As Integer = 0
    Property StatusFocusedItem As Integer
        Get
            Return _StatusFocusedItem
        End Get
        Set(value As Integer)
            _StatusFocusedItem = value
            Main_Toolbar.Children(value).Focus()
        End Set
    End Property
    Dim _SettingsFocusedItem As Integer = 0
    Property SettingsFocusedItem As Integer
        Get
            Return _SettingsFocusedItem
        End Get
        Set(value As Integer)
            _SettingsFocusedItem = value
            Home_Settings_WPanel.Children(value).Focus()
        End Set
    End Property
    Public CaptureFocus As Boolean = True
    Private _OsdPlacement As OsdPlace = OsdPlace.Games
    Property OsdPlacement As OsdPlace
        Get
            Return _OsdPlacement
        End Get
        Set(value As OsdPlace)
            _OsdPlacement = value
            Select Case value
                Case OsdPlace.Games
                    If DiscordRPCClient.IsInitialized Then DiscordRPCClient.SetPresence(New DiscordRPC.RichPresence With {.Details = "Home", .State = "Browsing Games", .Assets = New DiscordRPC.Assets With {.LargeImageKey = "home", .LargeImageText = "Home", .SmallImageKey = "idling", .SmallImageText = "Idling"}})
                Case OsdPlace.Settings
                    If DiscordRPCClient.IsInitialized Then DiscordRPCClient.SetPresence(New DiscordRPC.RichPresence With {.Details = "Settings", .State = "Tweaking Settings", .Assets = New DiscordRPC.Assets With {.LargeImageKey = "settings", .LargeImageText = "Settings", .SmallImageKey = "idling", .SmallImageText = "Idling"}})
            End Select
        End Set
    End Property
    Dim WithEvents UI_Manager As New Forms.Timer With {.Enabled = True, .Interval = 1000}
    Public ParticlesManager As ParticleManager = Nothing
    Public WithEvents MainLibrary As Library = Nothing
    Public BGM_Player As New Player(Me, AddressOf BGM_Player_MediaEnded) With {.AutoPlay = True, .FadeAudio = False}
    Public WithEvents DiscordRPCClient As New DiscordRPC.DiscordRpcClient(841670098477645884)
    Dim WithEvents NotifcationManager As New NotificationManager
    Private Sub DiscordRPCClient_OnConnectionEstablished(sender As Object, args As ConnectionEstablishedMessage) Handles DiscordRPCClient.OnConnectionEstablished
        NotifcationManager.SendNotification(New NotificationManager.NotificationItem("Discord", "Connected To Discord RPC", Utils.OSD_DISCORD, args.TimeCreated.TimeOfDay))
        DiscordRPCClient.SetPresence(New DiscordRPC.RichPresence With {.Details = "Home", .State = "Browsing games", .Assets = New DiscordRPC.Assets With {.LargeImageKey = "home", .LargeImageText = "Home", .SmallImageKey = "idling", .SmallImageText = "Idling"}})
    End Sub

    Private Sub DiscordRPCClient_OnConnectionFailed(sender As Object, args As ConnectionFailedMessage) Handles DiscordRPCClient.OnConnectionFailed
        NotifcationManager.SendNotification(New NotificationManager.NotificationItem("Discord", "Couldn't Connect To Discord RPC", Utils.OSD_DISCORD, args.TimeCreated.TimeOfDay))
    End Sub
    Private Sub BGM_Player_MediaEnded()
    End Sub
    Private Sub NotifcationManager_OnNotificationRecieved(Notification As NotificationManager.NotificationItem, Count As Integer) Handles NotifcationManager.OnNotificationRecieved
        Dispatcher.Invoke(Sub()
                              Select Case Count
                                  Case 0
                                      CType(Main_Toolbar.Children.Item(0), StatusItem).Main_Icon.Source = Utils.OSD_NOTIFY
                                  Case 1
                                      CType(Main_Toolbar.Children.Item(0), StatusItem).Main_Icon.Source = Utils.OSD_NOTIFY1
                                  Case 2
                                      CType(Main_Toolbar.Children.Item(0), StatusItem).Main_Icon.Source = Utils.OSD_NOTIFY2
                                  Case 3
                                      CType(Main_Toolbar.Children.Item(0), StatusItem).Main_Icon.Source = Utils.OSD_NOTIFY3
                                  Case > 3
                                      CType(Main_Toolbar.Children.Item(0), StatusItem).Main_Icon.Source = Utils.OSD_NOTIFY4
                              End Select
                          End Sub)
    End Sub
    Private Sub NotificationManager_OnNotificationMarkedAsRead(Notification As NotificationManager.NotificationItem, Index As Integer, Rest As Integer) Handles NotifcationManager.OnNotificationMarkedAsRead

    End Sub
    Private Sub UI_Manager_Tick() Handles UI_Manager.Tick
        Home_Clock.Text = Now.ToString(My.Settings.TimeFormat)
    End Sub
    Public Async Sub AnimateWave()
        Home_Background.BeginAnimation(MarginProperty, New Animation.ThicknessAnimation(New Thickness(-200, -200, -200, -200), New Duration(TimeSpan.FromMilliseconds(500))))
        Home_Background.BeginAnimation(OpacityProperty, New Animation.DoubleAnimation(0, New Duration(TimeSpan.FromMilliseconds(500))))
        Await Task.Delay(500)
        Home_Background.BeginAnimation(MarginProperty, New Animation.ThicknessAnimation(New Thickness(0, 0, 0, 0), New Duration(TimeSpan.FromMilliseconds(500))))
        Home_Background.BeginAnimation(OpacityProperty, New Animation.DoubleAnimation(1, New Duration(TimeSpan.FromMilliseconds(500))))
    End Sub
    Public Sub TriggerShutdown()

    End Sub
    Private LoadingLoop As Boolean = False
    Private LoadingSync As Integer = 0
    Public Async Sub TriggerLoading(Show As Boolean)
        If Show Then
            If Loading_Triangle.Opacity = 0 Then
                Home_Loading.BeginAnimation(OpacityProperty, Utils.DAnimOneOneHundred)
                Loading_Triangle.BeginAnimation(OpacityProperty, Utils.DAnimOneOneHundred)
                Await Task.Delay(100)
                Loading_Circle.BeginAnimation(OpacityProperty, Utils.DAnimOneOneHundred)
                Await Task.Delay(100)
                Loading_Cross.BeginAnimation(OpacityProperty, Utils.DAnimOneOneHundred)
                Await Task.Delay(100)
                Loading_Square.BeginAnimation(OpacityProperty, Utils.DAnimOneOneHundred)
                Await Task.Delay(100)
                LoadingLoop = True
                Do While LoadingLoop
                    Await Task.Delay(1000)
                    Dim t_tri As Thickness = Loading_Triangle.Margin
                    Dim t_cir As Thickness = Loading_Circle.Margin
                    Dim t_cro As Thickness = Loading_Cross.Margin
                    Dim t_squ As Thickness = Loading_Square.Margin
                    If LoadingSync = 4 Then
                        'Loading_Cross.Margin = New Thickness(75, 75, 0, 0)
                        'Loading_Circle.Margin = New Thickness(75, -75, 0, 0)
                        'Loading_Square.Margin = New Thickness(-75, 75, 0, 0)
                        'Loading_Triangle.Margin = New Thickness(-75, -75, 0, 0)
                        t_tri = New Thickness(-75, -75, 0, 0)
                        t_cir = New Thickness(75, -75, 0, 0)
                        t_cro = New Thickness(75, 75, 0, 0)
                        t_squ = New Thickness(-75, 75, 0, 0)
                        Loading_Triangle.BeginAnimation(MarginProperty, New Animation.ThicknessAnimation(New Thickness(t_cir.Left, t_cir.Top, 0, 0), New Duration(TimeSpan.FromMilliseconds(200))))
                        Loading_Circle.BeginAnimation(MarginProperty, New Animation.ThicknessAnimation(New Thickness(t_cro.Left, t_cro.Top, 0, 0), New Duration(TimeSpan.FromMilliseconds(200))))
                        Loading_Cross.BeginAnimation(MarginProperty, New Animation.ThicknessAnimation(New Thickness(t_squ.Left, t_squ.Top, 0, 0), New Duration(TimeSpan.FromMilliseconds(200))))
                        Loading_Square.BeginAnimation(MarginProperty, New Animation.ThicknessAnimation(New Thickness(t_tri.Left, t_tri.Top, 0, 0), New Duration(TimeSpan.FromMilliseconds(200))))
                        LoadingSync = 1
                    Else
                        Loading_Triangle.BeginAnimation(MarginProperty, New Animation.ThicknessAnimation(New Thickness(t_cir.Left, t_cir.Top, 0, 0), New Duration(TimeSpan.FromMilliseconds(200))))
                        Loading_Circle.BeginAnimation(MarginProperty, New Animation.ThicknessAnimation(New Thickness(t_cro.Left, t_cro.Top, 0, 0), New Duration(TimeSpan.FromMilliseconds(200))))
                        Loading_Cross.BeginAnimation(MarginProperty, New Animation.ThicknessAnimation(New Thickness(t_squ.Left, t_squ.Top, 0, 0), New Duration(TimeSpan.FromMilliseconds(200))))
                        Loading_Square.BeginAnimation(MarginProperty, New Animation.ThicknessAnimation(New Thickness(t_tri.Left, t_tri.Top, 0, 0), New Duration(TimeSpan.FromMilliseconds(200))))
                        LoadingSync += 1
                    End If
                Loop
            End If
        Else
            LoadingLoop = False
            Loading_Triangle.BeginAnimation(OpacityProperty, Utils.DAnimZeroFifty)
            Await Task.Delay(25)
            Loading_Circle.BeginAnimation(OpacityProperty, Utils.DAnimZeroFifty)
            Await Task.Delay(25)
            Loading_Cross.BeginAnimation(OpacityProperty, Utils.DAnimZeroFifty)
            Await Task.Delay(25)
            Loading_Square.BeginAnimation(OpacityProperty, Utils.DAnimZeroFifty)
            Home_Loading.BeginAnimation(OpacityProperty, New Animation.DoubleAnimation(0, New Duration(TimeSpan.FromMilliseconds(100))))
        End If
    End Sub
    Public Async Sub TriggerError(Message As String, Show As Boolean, AutoClose As Boolean, Optional Delay As Integer = 1000)
        If Show Then
            CaptureFocus = False
            Utils.S_Error_Player.Play()
            Home_Error_Message.Text = Message
            Home_Error.BeginAnimation(OpacityProperty, Utils.DAnimOneOneHundred)
            Home_Error.BeginAnimation(MarginProperty, New Animation.ThicknessAnimation(New Thickness(0, 0, 0, 0), New Duration(TimeSpan.FromMilliseconds(200))))
            If AutoClose Then
                Await Task.Delay(Delay)
                Home_Error.BeginAnimation(OpacityProperty, Utils.DAnimZeroOneHundred)
                Home_Error.BeginAnimation(MarginProperty, New Animation.ThicknessAnimation(New Thickness(-100, -100, -100, -100), New Duration(TimeSpan.FromMilliseconds(200))))
            End If
            Home_Error_Btn.Focus()
        Else
            Home_Error.BeginAnimation(OpacityProperty, Utils.DAnimZeroOneHundred)
            Home_Error.BeginAnimation(MarginProperty, New Animation.ThicknessAnimation(New Thickness(-100, -100, -100, -100), New Duration(TimeSpan.FromMilliseconds(200))))
            CaptureFocus = True
            UpdateFocus()
        End If

    End Sub
    Private Sub Home_Error_Btn_Click()
        TriggerError(Nothing, False, False)
    End Sub
    Public Sub TriggerReload()
        TriggerLoading(True)
        MainLibrary.Reload()
        Main_Games.Children.Clear()
        For Each game In MainLibrary.LibGames
            Main_Games.Children.Add(New GameItem(game.Name, game.Icon, game.Path, game.BigIcon) With {.VerticalAlignment = VerticalAlignment.Top})
        Next
        TriggerLoading(False)
    End Sub
    Private Sub UpdateFocus()
        Select Case OsdPlacement
            Case OsdPlace.Games
                Main_Games.Children.Item(GamesFocusedItem).Focus()
            Case OsdPlace.Status
                Main_Toolbar.Children.Item(StatusFocusedItem).Focus()
            Case OsdPlace.Settings
                Home_Settings_WPanel.Children.Item(SettingsFocusedItem).Focus()
        End Select
    End Sub
    Public Sub MainLibrary_Reloaded() Handles MainLibrary.Reloaded
        TriggerLoading(True)
        Main_Games.Children.Clear()
        For Each game In MainLibrary.LibGames
            Main_Games.Children.Add(New GameItem(game.Name, game.Icon, game.Path, game.BigIcon) With {.VerticalAlignment = VerticalAlignment.Top})
        Next
        TriggerLoading(False)
    End Sub
    Private Sub MainWindow_PreviewKeyDown(sender As Object, e As KeyEventArgs) Handles Me.PreviewKeyDown
        If CaptureFocus Then
            If e.Key = Key.Right Then
                Select Case OsdPlacement
                    Case OsdPlace.Games
                        If GamesFocusedItem <> Main_Games.Children.Count - 1 Then
                            GamesFocusedItem += 1
                        Else
                            GamesFocusedItem = 0
                        End If
                        Utils.S_Navigation_Player.Play()
                    Case OsdPlace.Status
                        If StatusFocusedItem <> Main_Toolbar.Children.Count - 1 Then
                            StatusFocusedItem += 1
                        Else
                            StatusFocusedItem = 0
                        End If
                        Utils.S_Navigation_Player.Play()
                End Select
            ElseIf e.Key = Key.Left Then
                Select Case OsdPlacement
                    Case OsdPlace.Games
                        If GamesFocusedItem <> 0 Then
                            GamesFocusedItem -= 1
                        Else
                            GamesFocusedItem = Main_Games.Children.Count - 1
                        End If
                        Utils.S_Navigation_Player.Play()
                    Case OsdPlace.Status
                        If StatusFocusedItem <> 0 Then
                            StatusFocusedItem -= 1
                        Else
                            StatusFocusedItem = Main_Toolbar.Children.Count - 1
                        End If
                        Utils.S_Navigation_Player.Play()
                    Case OsdPlace.Settings
                        Home_Settings.BeginAnimation(OpacityProperty, Utils.DAnimZeroOneHundred)
                        OsdPlacement = OsdPlace.Status
                        UpdateFocus()
                End Select
            ElseIf e.Key = Key.Down Then
                Select Case OsdPlacement
                    Case OsdPlace.Status
                        OsdPlacement = OsdPlace.Games
                        Main_Games.Children.Item(GamesFocusedItem).Focus()
                        Utils.S_Back_Player.Play()
                        Main_Toolbar.BeginAnimation(MarginProperty, New Animation.ThicknessAnimation(New Thickness(10, -120, 0, 0), New Duration(TimeSpan.FromMilliseconds(200))))
                        sc.BeginAnimation(MarginProperty, New Animation.ThicknessAnimation(New Thickness(10, 130, 0, 0), New Duration(TimeSpan.FromMilliseconds(200))))
                    Case OsdPlace.Settings
                        If SettingsFocusedItem <> Home_Settings_WPanel.Children.Count - 1 Then
                            SettingsFocusedItem += 1
                        Else
                            SettingsFocusedItem = 1
                        End If
                        Utils.S_Navigation_Player.Play()
                End Select
            ElseIf e.Key = Key.Up Then
                Select Case OsdPlacement
                    Case OsdPlace.Games
                        OsdPlacement = OsdPlace.Status
                        Main_Toolbar.Children.Item(StatusFocusedItem).Focus()
                        Utils.S_Select_Player.Play()
                        Main_Toolbar.BeginAnimation(MarginProperty, New Animation.ThicknessAnimation(New Thickness(10, 130, 0, 0), New Duration(TimeSpan.FromMilliseconds(200))))
                        sc.BeginAnimation(MarginProperty, New Animation.ThicknessAnimation(New Thickness(10, 250, 0, 0), New Duration(TimeSpan.FromMilliseconds(200))))
                    Case OsdPlace.Settings
                        If SettingsFocusedItem > 1 Then
                            SettingsFocusedItem -= 1
                        Else
                            SettingsFocusedItem = Home_Settings_WPanel.Children.Count - 1
                        End If
                        Utils.S_Navigation_Player.Play()
                End Select
            End If
            e.Handled = True
        End If
    End Sub

    Private Async Sub Background_Startup_Video_MediaEnded(sender As Object, e As RoutedEventArgs) Handles Background_Startup_Video.MediaEnded
        Background_Startup_Video.Opacity = 0
        Utils.S_LogOn_Player.Play()
        Main_Toolbar.Children.Add(New StatusItem("Notifications", Utils.OSD_NOTIFY, Nothing) With {.VerticalAlignment = VerticalAlignment.Top})
        Main_Toolbar.Children.Add(New StatusItem("Settings", Utils.OSD_SETTINGS, Sub()
                                                                                     Home_Settings.BeginAnimation(OpacityProperty, Utils.DAnimOneOneHundred)
                                                                                     OsdPlacement = OsdPlace.Settings
                                                                                 End Sub) With {.VerticalAlignment = VerticalAlignment.Top})
        Main_Toolbar.Children.Add(New StatusItem("Shutdown", Utils.OSD_SHUTDOWN, Sub()
                                                                                     Close()
                                                                                 End Sub) With {.VerticalAlignment = VerticalAlignment.Top})
        If Not String.IsNullOrEmpty(My.Settings.LibraryPath) Then
            MainLibrary = New Library(My.Settings.LibraryPath) '"C:\Users\pc\AppData\Roaming\GMLauncher\Library\library.xml")
        Else
            My.Settings.LibraryPath = Await Library.MakeLibrary()
            My.Settings.Save()
            MainLibrary = New Library(My.Settings.LibraryPath)
        End If
        For Each game In MainLibrary.LibGames
            Main_Games.Children.Add(New GameItem(game.Name, game.Icon, game.Path, game.BigIcon) With {.VerticalAlignment = VerticalAlignment.Top})
        Next
        If Main_Games.Children.Count = 0 Then Main_Games.Children.Add(New GameItem("Welcome", Utils.OSD_UPDATE, Nothing, Utils.OSD_GMLAUNCHER))
        'Settings
        Home_Background.Visibility = My.Settings.Wave
        Home_Background.Fill = TryCast(My.Settings.WaveColor, Brush)
        sc.BeginAnimation(MarginProperty, New Animation.ThicknessAnimation(New Thickness(10, 130, 0, 0), New Duration(TimeSpan.FromMilliseconds(500))))
        sc.BeginAnimation(OpacityProperty, New Animation.DoubleAnimation(1, New Duration(TimeSpan.FromMilliseconds(500))))
        Home_Settings_WPanel.Children.Add(New RectItem(Sub()
                                                           Home_Settings_Header.Text = "Wave"
                                                           Utils.S_Select_Player.Play()
                                                           Home_Settings_WPanel.BeginAnimation(OpacityProperty, New Animation.DoubleAnimation(0, New Duration(TimeSpan.FromMilliseconds(200))))
                                                           CaptureFocus = False
                                                           Home_Settings_SPanel.Children.Clear()
                                                           Home_Settings_SPanel.Children.Add(New HomeBackgroundChanger With {.Margin = Utils.DefaultMargin, .OnBack = Sub()
                                                                                                                                                                          Home_Settings_Header.Text = "Settings"
                                                                                                                                                                          .BeginAnimation(OpacityProperty, New Animation.DoubleAnimation(0, New Duration(TimeSpan.FromMilliseconds(200))))
                                                                                                                                                                          Home_Settings_WPanel.BeginAnimation(OpacityProperty, New Animation.DoubleAnimation(1, New Duration(TimeSpan.FromMilliseconds(200))))
                                                                                                                                                                          CaptureFocus = True
                                                                                                                                                                          UpdateFocus()
                                                                                                                                                                      End Sub})
                                                           Home_Settings_SPanel.BeginAnimation(OpacityProperty, New Animation.DoubleAnimation(1, New Duration(TimeSpan.FromMilliseconds(500))))
                                                           Home_Settings_SPanel.Children.Item(0).Focus()
                                                       End Sub) With {.Width = Home_Settings_WPanel.Width, .Margin = Utils.DefaultMargin, .Message = "Wave", .Icon = Utils.OSD_BACKGROUND})
        Home_Settings_WPanel.Children.Add(New RectItem(Sub()
                                                           Home_Settings_Header.Text = "Particles"
                                                           Utils.S_Select_Player.Play()
                                                           Home_Settings_WPanel.BeginAnimation(OpacityProperty, New Animation.DoubleAnimation(0, New Duration(TimeSpan.FromMilliseconds(200))))
                                                           CaptureFocus = False
                                                           Home_Settings_SPanel.Children.Clear()
                                                           Home_Settings_SPanel.Children.Add(New ParticlesChanger With {.Margin = Utils.DefaultMargin, .OnBack = Sub()
                                                                                                                                                                     Home_Settings_Header.Text = "Settings"
                                                                                                                                                                     .BeginAnimation(OpacityProperty, New Animation.DoubleAnimation(0, New Duration(TimeSpan.FromMilliseconds(200))))
                                                                                                                                                                     Home_Settings_WPanel.BeginAnimation(OpacityProperty, New Animation.DoubleAnimation(1, New Duration(TimeSpan.FromMilliseconds(200))))
                                                                                                                                                                     CaptureFocus = True
                                                                                                                                                                     UpdateFocus()
                                                                                                                                                                 End Sub})
                                                           Home_Settings_SPanel.BeginAnimation(OpacityProperty, New Animation.DoubleAnimation(1, New Duration(TimeSpan.FromMilliseconds(500))))
                                                           Home_Settings_SPanel.Children.Item(0).Focus()
                                                       End Sub) With {.Width = Home_Settings_WPanel.Width, .Margin = Utils.DefaultMargin, .Message = "Particles", .Icon = Utils.OSD_BACKGROUND})
        Home_Settings_WPanel.Children.Add(New RectItem(Sub()
                                                           Home_Settings_Header.Text = "Games Managment"
                                                           Utils.S_Select_Player.Play()
                                                           Home_Settings_WPanel.BeginAnimation(OpacityProperty, New Animation.DoubleAnimation(0, New Duration(TimeSpan.FromMilliseconds(200))))
                                                           CaptureFocus = False
                                                           Home_Settings_SPanel.Children.Clear()
                                                           Home_Settings_SPanel.Children.Add(New GameLibraryChanger With {.Margin = Utils.DefaultMargin, .OnBack = Sub()
                                                                                                                                                                       Home_Settings_Header.Text = "Settings"
                                                                                                                                                                       .BeginAnimation(OpacityProperty, New Animation.DoubleAnimation(0, New Duration(TimeSpan.FromMilliseconds(200))))
                                                                                                                                                                       Home_Settings_WPanel.BeginAnimation(OpacityProperty, New Animation.DoubleAnimation(1, New Duration(TimeSpan.FromMilliseconds(200))))
                                                                                                                                                                       CaptureFocus = True
                                                                                                                                                                       UpdateFocus()
                                                                                                                                                                   End Sub})
                                                           Home_Settings_SPanel.BeginAnimation(OpacityProperty, New Animation.DoubleAnimation(1, New Duration(TimeSpan.FromMilliseconds(500))))
                                                           Home_Settings_SPanel.Children.Item(0).Focus()
                                                       End Sub) With {.Width = Home_Settings_WPanel.Width, .Margin = Utils.DefaultMargin, .Message = "Games Managment", .Icon = Utils.OSD_GAMESSETTINGS})
        Home_Settings_WPanel.Children.Add(New RectItem(Sub()
                                                           Home_Settings_Header.Text = "Discord Rich Presence"
                                                           Utils.S_Select_Player.Play()
                                                           Home_Settings_WPanel.BeginAnimation(OpacityProperty, New Animation.DoubleAnimation(0, New Duration(TimeSpan.FromMilliseconds(200))))
                                                           CaptureFocus = False
                                                           Home_Settings_SPanel.Children.Clear()
                                                           Home_Settings_SPanel.Children.Add(New DiscordRPCChanger With {.Margin = Utils.DefaultMargin, .OnBack = Sub()
                                                                                                                                                                      Home_Settings_Header.Text = "Settings"
                                                                                                                                                                      .BeginAnimation(OpacityProperty, New Animation.DoubleAnimation(0, New Duration(TimeSpan.FromMilliseconds(200))))
                                                                                                                                                                      Home_Settings_WPanel.BeginAnimation(OpacityProperty, New Animation.DoubleAnimation(1, New Duration(TimeSpan.FromMilliseconds(200))))
                                                                                                                                                                      CaptureFocus = True
                                                                                                                                                                  End Sub})
                                                           Home_Settings_SPanel.BeginAnimation(OpacityProperty, New Animation.DoubleAnimation(1, New Duration(TimeSpan.FromMilliseconds(500))))
                                                           Home_Settings_SPanel.Children.Item(0).Focus()
                                                       End Sub) With {.Width = Home_Settings_WPanel.Width, .Margin = Utils.DefaultMargin, .Message = "Discord Rich Presence", .Icon = Utils.OSD_DISCORD})
        Home_Settings_WPanel.Children.Add(New RectItem(Sub()
                                                           Home_Settings_Header.Text = "Updates"
                                                           Utils.S_Select_Player.Play()
                                                           Home_Settings_WPanel.BeginAnimation(OpacityProperty, New Animation.DoubleAnimation(0, New Duration(TimeSpan.FromMilliseconds(200))))
                                                           CaptureFocus = False
                                                           Home_Settings_SPanel.Children.Clear()
                                                           Home_Settings_SPanel.Children.Add(New ParticlesChanger With {.Margin = Utils.DefaultMargin, .OnBack = Sub()
                                                                                                                                                                     Home_Settings_Header.Text = "Settings"
                                                                                                                                                                     .BeginAnimation(OpacityProperty, New Animation.DoubleAnimation(0, New Duration(TimeSpan.FromMilliseconds(200))))
                                                                                                                                                                     Home_Settings_WPanel.BeginAnimation(OpacityProperty, New Animation.DoubleAnimation(1, New Duration(TimeSpan.FromMilliseconds(200))))
                                                                                                                                                                     CaptureFocus = True
                                                                                                                                                                 End Sub})
                                                           Home_Settings_SPanel.BeginAnimation(OpacityProperty, New Animation.DoubleAnimation(1, New Duration(TimeSpan.FromMilliseconds(500))))
                                                           Home_Settings_SPanel.Children.Item(0).Focus()
                                                       End Sub) With {.Width = Home_Settings_WPanel.Width, .Margin = Utils.DefaultMargin, .Message = "Updates", .Icon = Utils.OSD_UPDATE})
        Home_Settings_WPanel.Children.Add(New RectItem(Sub()
                                                           Home_Settings_Header.Text = "Date and Time"
                                                           Utils.S_Select_Player.Play()
                                                           Home_Settings_WPanel.BeginAnimation(OpacityProperty, New Animation.DoubleAnimation(0, New Duration(TimeSpan.FromMilliseconds(200))))
                                                           CaptureFocus = False
                                                           Home_Settings_SPanel.Children.Clear()
                                                           Home_Settings_SPanel.Children.Add(New DateTimeChanger With {.Margin = Utils.DefaultMargin, .OnBack = Sub()
                                                                                                                                                                    Home_Settings_Header.Text = "Settings"
                                                                                                                                                                    .BeginAnimation(OpacityProperty, New Animation.DoubleAnimation(0, New Duration(TimeSpan.FromMilliseconds(200))))
                                                                                                                                                                    Home_Settings_WPanel.BeginAnimation(OpacityProperty, New Animation.DoubleAnimation(1, New Duration(TimeSpan.FromMilliseconds(200))))
                                                                                                                                                                    CaptureFocus = True
                                                                                                                                                                End Sub})
                                                           Home_Settings_SPanel.BeginAnimation(OpacityProperty, New Animation.DoubleAnimation(1, New Duration(TimeSpan.FromMilliseconds(500))))
                                                           Home_Settings_SPanel.Children.Item(0).Focus()
                                                       End Sub) With {.Width = Home_Settings_WPanel.Width, .Margin = Utils.DefaultMargin, .Message = "Date and Time", .Icon = Utils.OSD_DATETIMESETTINGS})
        Home_Settings_WPanel.Children.Add(New RectItem(Sub()
                                                           Home_Settings_Header.Text = "Sound Settings"
                                                           Utils.S_Select_Player.Play()
                                                           Home_Settings_WPanel.BeginAnimation(OpacityProperty, New Animation.DoubleAnimation(0, New Duration(TimeSpan.FromMilliseconds(200))))
                                                           CaptureFocus = False
                                                           Home_Settings_SPanel.Children.Clear()
                                                           Home_Settings_SPanel.Children.Add(New SoundChanger With {.Margin = Utils.DefaultMargin, .OnBack = Sub()
                                                                                                                                                                 Home_Settings_Header.Text = "Settings"
                                                                                                                                                                 .BeginAnimation(OpacityProperty, New Animation.DoubleAnimation(0, New Duration(TimeSpan.FromMilliseconds(200))))
                                                                                                                                                                 Home_Settings_WPanel.BeginAnimation(OpacityProperty, New Animation.DoubleAnimation(1, New Duration(TimeSpan.FromMilliseconds(200))))
                                                                                                                                                                 CaptureFocus = True
                                                                                                                                                             End Sub})
                                                           Home_Settings_SPanel.BeginAnimation(OpacityProperty, New Animation.DoubleAnimation(1, New Duration(TimeSpan.FromMilliseconds(500))))
                                                           Home_Settings_SPanel.Children.Item(0).Focus()
                                                       End Sub) With {.Width = Home_Settings_WPanel.Width, .Margin = Utils.DefaultMargin, .Message = "Sound Settings", .Icon = Utils.OSD_SOUNDSETTINGS})
        If My.Settings.Particles = Visibility.Visible Then
            ParticlesManager = New ParticleManager(Particles_Host, Me, 150, 10) With {.Blurry = My.Settings.ParticlesBlur, .Color = My.Settings.ParticlesColor}
        End If
        If My.Settings.BGMPath = "%default%" Then
            Dim bgmpath = IO.Path.GetTempFileName
            Dim file As New IO.FileStream(bgmpath, IO.FileMode.Create, IO.FileAccess.Write)
            Utils.S_BGM.CopyTo(file)
            file.Close()
            Utils.S_BGM.Close()
            BGM_Player.RepeateType = Player.RepeateBehaviour.RepeatOne
            BGM_Player.LoadSong(bgmpath, Nothing, False)
            BGM_Player.StreamPlay()
        Else
            BGM_Player.RepeateType = Player.RepeateBehaviour.RepeatOne
            BGM_Player.LoadSong(My.Settings.BGMPath, Nothing, False)
            BGM_Player.StreamPlay()
        End If
        If My.Settings.DiscordRPC Then
            DiscordRPCClient = New DiscordRPC.DiscordRpcClient(841670098477645884)
            DiscordRPCClient.Initialize()
        End If
    End Sub
    Private Sub MainWindow_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        Select Case My.Settings.Startup
            Case Utils.StartUpType.Wave
                Background_Startup_Video.Opacity = 0
                Background_Video.Opacity = 0
                Background_Startup_Video.Source = Nothing
            Case Utils.StartUpType.Particles
                Background_Startup_Video.Opacity = 0
                Background_Video.Opacity = 0
                Background_Startup_Video.Source = Nothing
            Case Utils.StartUpType.PS1
                Background_Startup_Video.Source = New Uri(IO.Path.Combine(My.Application.Info.DirectoryPath, "Res", "SUP_PS1_XMB_STARTUP.mp4"))
            Case Utils.StartUpType.PS2
                Background_Startup_Video.Source = New Uri(IO.Path.Combine(My.Application.Info.DirectoryPath, "Res", "SUP_PS2_XMB_STARTUP.mp4"))
                Background_Video_Timeline.Source = New Uri(IO.Path.Combine(My.Application.Info.DirectoryPath, "Res", "SUP_PS2_XMB.mp4"))
            Case Utils.StartUpType.PS3
                Background_Startup_Video.Opacity = 0
                Background_Video.Opacity = 0
                Background_Startup_Video.Source = New Uri(IO.Path.Combine(My.Application.Info.DirectoryPath, "Res", "SUP_PS3_XMB_STARTUP.mp4"))
                Background_Video_Timeline.Source = New Uri(IO.Path.Combine(My.Application.Info.DirectoryPath, "Res", "SUP_PS3_XMB_Blue.mp4"))
                Background_Video.BeginAnimation(OpacityProperty, New Animation.DoubleAnimation(1, New Duration(TimeSpan.FromSeconds(6))))
            Case Utils.StartUpType.Xbox
                Background_Startup_Video.Source = New Uri(IO.Path.Combine(My.Application.Info.DirectoryPath, "Res", "SUP_XBOX_Original_Startup.mp4"))
            Case Utils.StartUpType.Xbox360Old
                Background_Startup_Video.Source = New Uri(IO.Path.Combine(My.Application.Info.DirectoryPath, "Res", "SUP_XBOX_360_Startup.mp4"))
            Case Utils.StartUpType.Xbox360New
                Background_Startup_Video.Source = New Uri(IO.Path.Combine(My.Application.Info.DirectoryPath, "Res", "SUP_Xbox_360_2_Startup.mp4"))
            Case Utils.StartUpType.XboxOneX
                Background_Startup_Video.Source = New Uri(IO.Path.Combine(My.Application.Info.DirectoryPath, "Res", "SUP_XBOX_One_X_Startup.mp4"))
            Case Utils.StartUpType.CyberPunk
                Background_Startup_Video.Source = Nothing
                Background_Startup_Video_MediaEnded(Nothing, Nothing)
                Background_Video_Timeline.Source = New Uri(IO.Path.Combine(My.Application.Info.DirectoryPath, "Res", "SUP_CyberPunk_Startup.mp4"))
        End Select
    End Sub
    Private Sub MainWindow_Closed(sender As Object, e As EventArgs) Handles Me.Closed
        If DiscordRPCClient.IsInitialized Then
            DiscordRPCClient.ClearPresence()
            DiscordRPCClient.Deinitialize()
        End If
    End Sub
    Private Async Sub sc_PreviewKeyUp(sender As Object, e As KeyEventArgs) Handles sc.PreviewKeyUp
        If e.Key = Key.Enter Then
            Utils.S_Select_Player.Play()
            If TryCast(Main_Games.Children.Item(GamesFocusedItem), GameItem).BigIcon IsNot Nothing Then
                Home_BigGameLogo.Source = TryCast(Main_Games.Children.Item(GamesFocusedItem), GameItem).BigIcon
            Else
                Home_BigGameLogo.Source = TryCast(Main_Games.Children.Item(GamesFocusedItem), GameItem).Icon
            End If
            Home_MainGrid.BeginAnimation(MarginProperty, New Animation.ThicknessAnimation(New Thickness(200, 200, 200, 200), New Duration(TimeSpan.FromMilliseconds(200))))
                Home_MainGrid.BeginAnimation(OpacityProperty, New Animation.DoubleAnimation(0, New Duration(TimeSpan.FromMilliseconds(200))))
                Home_BigGameLogo.BeginAnimation(OpacityProperty, New Animation.DoubleAnimation(1, New Duration(TimeSpan.FromMilliseconds(200))))
                Home_BigGameLogo.BeginAnimation(MarginProperty, New Animation.ThicknessAnimation(New Thickness(0, 0, 0, 0), New Duration(TimeSpan.FromMilliseconds(200))))
            Await Task.Delay(500)
            If IO.File.Exists(CType(Main_Games.Children(GamesFocusedItem), GameItem).Source) Then
                Dim prcs = Process.Start(CType(Main_Games.Children(GamesFocusedItem), GameItem).Source)
                Await BGM_Player.FadeVol(0)
                BGM_Player.StreamPause()
                If DiscordRPCClient.IsInitialized Then
                    DiscordRPCClient.SetPresence(New DiscordRPC.RichPresence With {.Details = "In Game", .State = CType(Main_Games.Children(GamesFocusedItem), GameItem).Title, .Timestamps = New DiscordRPC.Timestamps(DateTime.UtcNow), .Assets = New DiscordRPC.Assets With {.LargeImageKey = "ingame", .LargeImageText = "Playing", .SmallImageKey = "dnd", .SmallImageText = "Do Not Disturb"}})
                End If
                prcs.WaitForExit()
                BGM_Player.StreamPlay()
                BGM_Player.FadeVol(1)
            End If
            If DiscordRPCClient.IsInitialized Then
                DiscordRPCClient.SetPresence(New DiscordRPC.RichPresence With {.Details = "Home", .State = "Browsing games", .Assets = New DiscordRPC.Assets With {.LargeImageKey = "home", .LargeImageText = "Home", .SmallImageKey = "idling", .SmallImageText = "Idling"}})
            End If
            Home_BigGameLogo.BeginAnimation(MarginProperty, New Animation.ThicknessAnimation(New Thickness(100, 100, 100, 100), New Duration(TimeSpan.FromMilliseconds(200))))
                Home_BigGameLogo.BeginAnimation(OpacityProperty, New Animation.DoubleAnimation(0, New Duration(TimeSpan.FromMilliseconds(200))))
                Home_MainGrid.BeginAnimation(OpacityProperty, New Animation.DoubleAnimation(1, New Duration(TimeSpan.FromMilliseconds(200))))
                Home_MainGrid.BeginAnimation(MarginProperty, New Animation.ThicknessAnimation(New Thickness(0, 0, 0, 0), New Duration(TimeSpan.FromMilliseconds(200))))
            End If
    End Sub
End Class
