Public Class GameLibraryChanger
    Dim _FocusedItem As Integer = 1
    Dim MainWindow As MainWindow = TryCast(Application.Current.MainWindow, MainWindow)
    Public Property OnBack As Action = Nothing
    Public Property IsOnEdit As Boolean = False
    Dim ListGameItemSource As New ObjectModel.ObservableCollection(Of ListGameItem)
    Property EditFocusedItem As Integer = 0
    Private SkipThatOne As Boolean = False
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
                                                 Dim OFD As New Forms.OpenFileDialog With {.CheckFileExists = True, .Multiselect = True, .Title = "Select a game or app..."}
                                                 If OFD.ShowDialog <> Forms.DialogResult.Cancel Then
                                                     MainWindow.TriggerLoading(True)
                                                     Await MainWindow.MainLibrary.AddGamesToLibraryAsync(OFD.FileNames)
                                                     MainWindow.TriggerLoading(False)
                                                 End If
                                             End Sub) With {.Margin = New Thickness(10, 10, 10, 10), .Message = "Add From File", .Icon = Utils.OSD_SETTINGS})
        Main_Panel.Children.Add(New RectItem(Sub()
                                                 Dim installs As New List(Of Utils.AppItem)
                                                 Dim keys As New List(Of String)
                                                 keys.Add("SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall")
                                                 keys.Add("SOFTWARE\WOW6432Node\Microsoft\Windows\CurrentVersion\Uninstall")
                                                 Utils.FindInstalls(Microsoft.Win32.RegistryKey.OpenBaseKey(Microsoft.Win32.RegistryHive.LocalMachine, Microsoft.Win32.RegistryView.Registry64), keys, installs)
                                                 Utils.FindInstalls(Microsoft.Win32.RegistryKey.OpenBaseKey(Microsoft.Win32.RegistryHive.CurrentUser, Microsoft.Win32.RegistryView.Registry64), keys, installs)
                                                 installs = installs.Where(Function(s) Not String.IsNullOrWhiteSpace(s.DisplayName)).Distinct().ToList()
                                                 ListGameItemSource.Clear()
                                                 For Each app In installs
                                                     ListGameItemSource.Add(New ListGameItem(ListGameItemSource.Count + 1, app.DisplayName, app.InstallLocation))
                                                 Next
                                                 IAppsView.Tag = "1"
                                                 IAppsView.Focusable = True
                                                 IAppsView.Focus()
                                                 Main_Panel.BeginAnimation(OpacityProperty, Utils.DAnimZeroOneHundred)
                                                 IAppsHost.Visibility = Visibility.Visible
                                                 IAppsHost.BeginAnimation(OpacityProperty, Utils.DAnimOneOneHundred)
                                             End Sub) With {.Margin = New Thickness(10, 10, 10, 10), .Message = "Add From Installed Apps", .Icon = Utils.OSD_SETTINGS})
        Main_Panel.Children.Add(New RectItem(Sub()
                                                 ListGameItemSource.Clear()
                                                 For Each app In MainWindow.MainLibrary.LibGames
                                                     ListGameItemSource.Add(New ListGameItem(ListGameItemSource.Count + 1, app.Name, app.Path) With {.IconSource = app.IconPath, .Icon = app.Icon})
                                                 Next
                                                 IAppsView.Tag = "3"
                                                 IAppsView.Focusable = True
                                                 IAppsView.Focus()
                                                 Main_Panel.BeginAnimation(OpacityProperty, Utils.DAnimZeroOneHundred)
                                                 IAppsHost.Visibility = Visibility.Visible
                                                 IAppsHost.BeginAnimation(OpacityProperty, Utils.DAnimOneOneHundred)
                                             End Sub) With {.Margin = New Thickness(10, 10, 10, 10), .Message = "Edit", .Icon = Utils.OSD_SETTINGS})
        Main_Panel.Children.Add(New RectItem(Sub()
                                                 ListGameItemSource.Clear()
                                                 For Each app In MainWindow.MainLibrary.LibGames
                                                     ListGameItemSource.Add(New ListGameItem(ListGameItemSource.Count + 1, app.Name, app.Path))
                                                 Next
                                                 IAppsView.Tag = "2"
                                                 IAppsView.Focusable = True
                                                 IAppsView.Focus()
                                                 Main_Panel.BeginAnimation(OpacityProperty, Utils.DAnimZeroOneHundred)
                                                 IAppsHost.Visibility = Visibility.Visible
                                                 IAppsHost.BeginAnimation(OpacityProperty, Utils.DAnimOneOneHundred)
                                             End Sub) With {.Margin = New Thickness(10, 10, 10, 10), .Message = "Remove", .Icon = Utils.OSD_SETTINGS})
        Main_Panel.Children.Add(New RectItem(Sub()
                                                 MainWindow.MainLibrary.ScanLibrary()
                                             End Sub) With {.Margin = New Thickness(10, 10, 10, 10), .Message = "Scan", .Icon = Utils.OSD_SETTINGS})
    End Sub
    Private Async Sub GameLibraryChanger_PreviewKeyDown(sender As Object, e As KeyEventArgs) Handles Me.PreviewKeyDown
        MainWindow.CaptureFocus = False
        If IsOnEdit = True Then
            Select Case e.Key
                Case Key.Down
                    EditFocusedItem += 1
                    If EditFocusedItem = 4 Then
                        EditFocusedItem = 0
                    End If
                    Select Case EditFocusedItem
                        Case 0 'Title
                            LibEditHost_TB_Title.Focus()
                        Case 1 'Source
                            LibEditHost_BTN_Source.Focus()
                        Case 2 'Icon Source
                            LibEditHost_BTN_IconSource.Focus()
                        Case 3 'I'M DONE!
                            LibEditHost_BTN_Done.Focus()
                    End Select
                Case Key.Up
                    EditFocusedItem -= 1
                    If EditFocusedItem = -1 Then
                        EditFocusedItem = 3
                    End If
                    Select Case EditFocusedItem
                        Case 0 'Title
                            LibEditHost_TB_Title.Focus()
                        Case 1 'Source
                            LibEditHost_BTN_Source.Focus()
                        Case 2 'Icon Source
                            LibEditHost_BTN_IconSource.Focus()
                        Case 3 'I'M DONE!
                            LibEditHost_BTN_Done.Focus()
                    End Select
            End Select
        Else
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
        End If
    End Sub
    Private Async Sub IAppsView_PreviewKeyUp(sender As Object, e As KeyEventArgs) Handles IAppsView.PreviewKeyUp
        If SkipThatOne Then SkipThatOne = False : Exit Sub
        If e.Key = Key.Enter AndAlso IAppsView.SelectedIndex < ListGameItemSource.Count Then
            If IAppsView.Tag = "1" Then
                Dim ofd As New Forms.OpenFileDialog With {.CheckFileExists = True, .Filter = Utils.FILTER_EXE, .InitialDirectory = ListGameItemSource.Item(IAppsView.SelectedIndex).Source}
                If ofd.ShowDialog <> Forms.DialogResult.Cancel Then
                    MainWindow.TriggerLoading(True)
                    Await MainWindow.MainLibrary.AddGamesToLibraryAsync({ofd.FileName})
                    MainWindow.TriggerLoading(False)
                    IAppsView.Focusable = False
                    IAppsHost.BeginAnimation(OpacityProperty, Utils.DAnimZeroOneHundred)
                    Main_Panel.BeginAnimation(OpacityProperty, Utils.DAnimOneOneHundred)
                    Await Task.Delay(100)
                    IAppsHost.Visibility = Visibility.Hidden
                    Main_Panel.Children(FocusedItem).Focus()
                End If
            ElseIf IAppsView.Tag = "2" Then
                    MainWindow.MainLibrary.RemoveFromLibrary(ListGameItemSource.Item(IAppsView.SelectedIndex).Source)
                    IAppsView.Focusable = False
                    IAppsHost.BeginAnimation(OpacityProperty, Utils.DAnimZeroOneHundred)
                    Main_Panel.BeginAnimation(OpacityProperty, Utils.DAnimOneOneHundred)
                    Await Task.Delay(100)
                    IAppsHost.Visibility = Visibility.Hidden
                    Main_Panel.Children(FocusedItem).Focus()
                ElseIf IAppsView.Tag = "3" Then
                    IsOnEdit = True
                Dim item = ListGameItemSource.Item(IAppsView.SelectedIndex)
                If item.Icon IsNot Nothing Then
                    LibEditHost_IM_Icon.Source = item.Icon
                Else
                    LibEditHost_IM_Icon.Source = Utils.OSD_GAME
                End If
                LibEditHost_TB_Title.Text = item.Title
                LibEditHost_BTN_Source.Tag = item.Source
                LibEditHost_BTN_IconSource.Tag = item.IconSource
                LibEditHost_TB_Title.Focusable = True
                LibEditHost_BTN_IconSource.Focusable = True
                LibEditHost_BTN_BigIconSource.Focusable = True
                LibEditHost_BTN_Source.Focusable = True
                LibEditHost_BTN_Done.Focusable = True
                LibEditHost_BTN_Cancel.Focusable = True
                IAppsView.Focusable = False
                IAppsHost.BeginAnimation(OpacityProperty, Utils.DAnimZeroOneHundred)
                LibEditHost.BeginAnimation(OpacityProperty, Utils.DAnimOneOneHundred)
            End If
        End If
        e.Handled = True
    End Sub

    Private Sub LibEditHost_BTN_Source_Click(sender As Object, e As RoutedEventArgs) Handles LibEditHost_BTN_Source.Click
        Dim OFD As New Forms.OpenFileDialog With {.CheckFileExists = True, .Filter = Utils.FILTER_EXE}
        If OFD.ShowDialog <> Forms.DialogResult.Cancel Then
            ListGameItemSource.Item(IAppsView.SelectedIndex).Source = OFD.FileName
        End If
    End Sub

    Private Sub LibEditHost_BTN_IconSource_Click(sender As Object, e As RoutedEventArgs) Handles LibEditHost_BTN_IconSource.Click
        Dim OFD As New Forms.OpenFileDialog With {.CheckFileExists = True, .Filter = Utils.FILTER_IMAGE}
        If OFD.ShowDialog <> Forms.DialogResult.Cancel Then
            ListGameItemSource.Item(IAppsView.SelectedIndex).IconSource = OFD.FileName
        End If
        LibEditHost_IM_Icon.Source = New BitmapImage(New Uri(OFD.FileName, UriKind.Absolute))
    End Sub

    Private Sub LibEditHost_BTN_BigIconSource_Click(sender As Object, e As RoutedEventArgs) Handles LibEditHost_BTN_BigIconSource.Click
        Dim OFD As New Forms.OpenFileDialog With {.CheckFileExists = True, .Filter = Utils.FILTER_IMAGE}
        If OFD.ShowDialog <> Forms.DialogResult.Cancel Then
            ListGameItemSource.Item(IAppsView.SelectedIndex).BigIconSource = OFD.FileName
        End If
    End Sub
    Private Async Sub LibEditHost_BTN_Done_Click(sender As Object, e As RoutedEventArgs) Handles LibEditHost_BTN_Done.Click
        SkipThatOne = True
        Await MainWindow.MainLibrary.UpdateLibraryAsync(New Library.LibGameItem(LibEditHost_TB_Title.Text, ListGameItemSource.Item(IAppsView.SelectedIndex).IconSource, ListGameItemSource.Item(IAppsView.SelectedIndex).Source, ListGameItemSource.Item(IAppsView.SelectedIndex).BigIconSource), IAppsView.SelectedIndex)
        LibEditHost_TB_Title.Focusable = False
        LibEditHost_BTN_IconSource.Focusable = False
        LibEditHost_BTN_BigIconSource.Focusable = False
        LibEditHost_BTN_Source.Focusable = False
        LibEditHost_BTN_Done.Focusable = False
        LibEditHost_BTN_Cancel.Focusable = False
        LibEditHost.BeginAnimation(OpacityProperty, Utils.DAnimZeroOneHundred)
        Await Task.Delay(100)
        ListGameItemSource.Clear()
        For Each app In MainWindow.MainLibrary.LibGames
            ListGameItemSource.Add(New ListGameItem(ListGameItemSource.Count + 1, app.Name, app.Path) With {.IconSource = app.IconPath, .Icon = app.Icon})
        Next
        IAppsView.Focusable = True
        IAppsHost.BeginAnimation(OpacityProperty, Utils.DAnimOneOneHundred)
        IAppsView.Focus()
        IsOnEdit = False
    End Sub
    Private Async Sub LibEditHost_BTN_Cancel_Click(sender As Object, e As RoutedEventArgs) Handles LibEditHost_BTN_Cancel.Click
        LibEditHost_TB_Title.Focusable = False
        LibEditHost_BTN_IconSource.Focusable = False
        LibEditHost_BTN_BigIconSource.Focusable = False
        LibEditHost_BTN_Source.Focusable = False
        LibEditHost_BTN_Done.Focusable = False
        LibEditHost_BTN_Cancel.Focusable = False
        LibEditHost.BeginAnimation(OpacityProperty, Utils.DAnimZeroOneHundred)
        Await Task.Delay(100)
        IAppsView.Focusable = True
        IAppsHost.BeginAnimation(OpacityProperty, Utils.DAnimOneOneHundred)
        IAppsView.Focus()
        IsOnEdit = False
    End Sub
    Private Sub LibEditHost_TB_Title_GotFocus(sender As Object, e As RoutedEventArgs) Handles LibEditHost_TB_Title.GotFocus
        LibEditHost_TB_Title.BeginAnimation(BorderThicknessProperty, New Animation.ThicknessAnimation(New Thickness(3, 3, 3, 3), New Duration(TimeSpan.FromMilliseconds(100))))
    End Sub

    Private Sub LibEditHost_TB_Title_LostFocus(sender As Object, e As RoutedEventArgs) Handles LibEditHost_TB_Title.LostFocus
        LibEditHost_TB_Title.BeginAnimation(BorderThicknessProperty, New Animation.ThicknessAnimation(New Thickness(1, 1, 1, 1), New Duration(TimeSpan.FromMilliseconds(100))))
    End Sub

    Private Sub LibEditHost_BTN_Source_GotFocus(sender As Object, e As RoutedEventArgs) Handles LibEditHost_BTN_Source.GotFocus
        LibEditHost_BTN_Source.BeginAnimation(BorderThicknessProperty, New Animation.ThicknessAnimation(New Thickness(3, 3, 3, 3), New Duration(TimeSpan.FromMilliseconds(100))))
    End Sub

    Private Sub LibEditHost_BTN_Source_LostFocus(sender As Object, e As RoutedEventArgs) Handles LibEditHost_BTN_Source.LostFocus
        LibEditHost_BTN_Source.BeginAnimation(BorderThicknessProperty, New Animation.ThicknessAnimation(New Thickness(1, 1, 1, 1), New Duration(TimeSpan.FromMilliseconds(100))))
    End Sub

    Private Sub LibEditHost_BTN_IconSource_GotFocus(sender As Object, e As RoutedEventArgs) Handles LibEditHost_BTN_IconSource.GotFocus
        LibEditHost_BTN_IconSource.BeginAnimation(BorderThicknessProperty, New Animation.ThicknessAnimation(New Thickness(3, 3, 3, 3), New Duration(TimeSpan.FromMilliseconds(100))))
    End Sub

    Private Sub LibEditHost_BTN_IconSource_LostFocus(sender As Object, e As RoutedEventArgs) Handles LibEditHost_BTN_IconSource.LostFocus
        LibEditHost_BTN_IconSource.BeginAnimation(BorderThicknessProperty, New Animation.ThicknessAnimation(New Thickness(1, 1, 1, 1), New Duration(TimeSpan.FromMilliseconds(100))))
    End Sub

    Private Sub LibEditHost_BTN_Done_GotFocus(sender As Object, e As RoutedEventArgs) Handles LibEditHost_BTN_Done.GotFocus
        LibEditHost_BTN_Done.BeginAnimation(BorderThicknessProperty, New Animation.ThicknessAnimation(New Thickness(3, 3, 3, 3), New Duration(TimeSpan.FromMilliseconds(100))))
    End Sub

    Private Sub LibEditHost_BTN_Done_LostFocus(sender As Object, e As RoutedEventArgs) Handles LibEditHost_BTN_Done.LostFocus
        LibEditHost_BTN_Done.BeginAnimation(BorderThicknessProperty, New Animation.ThicknessAnimation(New Thickness(1, 1, 1, 1), New Duration(TimeSpan.FromMilliseconds(100))))
    End Sub

    Private Sub LibEditHost_BTN_Cancel_GotFocus(sender As Object, e As RoutedEventArgs) Handles LibEditHost_BTN_Cancel.GotFocus
        LibEditHost_BTN_Cancel.BeginAnimation(BorderThicknessProperty, New Animation.ThicknessAnimation(New Thickness(3, 3, 3, 3), New Duration(TimeSpan.FromMilliseconds(100))))
    End Sub

    Private Sub LibEditHost_BTN_Cancel_LostFocus(sender As Object, e As RoutedEventArgs) Handles LibEditHost_BTN_Cancel.LostFocus
        LibEditHost_BTN_Cancel.BeginAnimation(BorderThicknessProperty, New Animation.ThicknessAnimation(New Thickness(1, 1, 1, 1), New Duration(TimeSpan.FromMilliseconds(100))))
    End Sub

    Private Sub LibEditHost_BTN_BigIconSource_GotFocus(sender As Object, e As RoutedEventArgs) Handles LibEditHost_BTN_BigIconSource.GotFocus
        LibEditHost_BTN_BigIconSource.BeginAnimation(BorderThicknessProperty, New Animation.ThicknessAnimation(New Thickness(3, 3, 3, 3), New Duration(TimeSpan.FromMilliseconds(100))))
    End Sub

    Private Sub LibEditHost_BTN_BigIconSource_LostFocus(sender As Object, e As RoutedEventArgs) Handles LibEditHost_BTN_BigIconSource.LostFocus
        LibEditHost_BTN_BigIconSource.BeginAnimation(BorderThicknessProperty, New Animation.ThicknessAnimation(New Thickness(1, 1, 1, 1), New Duration(TimeSpan.FromMilliseconds(100))))
    End Sub
End Class
