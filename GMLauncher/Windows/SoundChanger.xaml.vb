Public Class SoundChanger
    Dim _FocusedItem As Integer = 1
    Dim MainWindow As MainWindow = TryCast(Application.Current.MainWindow, MainWindow)
    Public Property OnBack As Action = Nothing
    Dim ListGameItemSource As New ObjectModel.ObservableCollection(Of ListGameItem)
    Property FocusedItem As Integer
        Get
            Return _FocusedItem
        End Get
        Set(value As Integer)
            _FocusedItem = value
            Main_Panel.Children(value).Focus()
        End Set
    End Property
    Private Sub GameLibraryChanger_Initialized(sender As Object, e As EventArgs) Handles Me.Initialized
        IAppsView.ItemsSource = ListGameItemSource
        Main_Panel.Children.Add(New RectItem(Async Sub()
                                                 Dim OFD As New Forms.OpenFileDialog With {.CheckFileExists = True, .Title = "Select a background music file...", .FileName = Utils.FILTER_AUDIO} '.Filter = "Supported Files|*.mp3;*.m4a;*.wav;*.aiff;*.mp2;*.mp1;*.ogg;*.wma;*.alac;*.webm|All files|*.*"}
                                                 If OFD.ShowDialog <> Forms.DialogResult.Cancel Then
                                                     My.Settings.BGMPath = OFD.FileName
                                                     My.Settings.Save()
                                                     Await MainWindow.BGM_Player.FadeVol(0)
                                                     MainWindow.BGM_Player.LoadSong(OFD.FileName, Nothing, False)
                                                 End If
                                             End Sub) With {.Margin = New Thickness(10, 10, 10, 10), .Message = "From File", .Icon = Utils.OSD_SETTINGS})
        Main_Panel.Children.Add(New RectItem(Async Sub()
                                                 My.Settings.BGMPath = "%default%"
                                                 My.Settings.Save()
                                                 Await MainWindow.BGM_Player.FadeVol(0)
                                                 Dim bgmpath = IO.Path.GetTempFileName
                                                 Dim file As New IO.FileStream(bgmpath, IO.FileMode.Create, IO.FileAccess.Write)
                                                 Dim s_bgm = System.Reflection.Assembly.GetExecutingAssembly.GetManifestResourceStream("GMLauncher.SOUND.mp3")
                                                 s_bgm.CopyTo(file)
                                                 file.Close()
                                                 s_bgm.Close()
                                                 MainWindow.BGM_Player.LoadSong(bgmpath, Nothing, False)
                                                 MainWindow.BGM_Player.StreamPlay()
                                             End Sub) With {.Margin = New Thickness(10, 10, 10, 10), .Message = "Default", .Icon = Utils.OSD_SETTINGS})
    End Sub
    Private Async Sub GameLibraryChanger_PreviewKeyDown(sender As Object, e As KeyEventArgs) Handles Me.PreviewKeyDown
        MainWindow.CaptureFocus = False
        If IAppsHost.Opacity = 0 Then
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
        Else
            If e.Key = Key.Left Then
                IAppsView.Focusable = False
                IAppsHost.BeginAnimation(OpacityProperty, Utils.DAnimZeroOneHundred)
                Main_Panel.BeginAnimation(OpacityProperty, Utils.DAnimOneOneHundred)
                Await Task.Delay(100)
                IAppsHost.Visibility = Visibility.Hidden
                Main_Panel.Children(FocusedItem).Focus()
            End If
        End If
    End Sub
End Class
