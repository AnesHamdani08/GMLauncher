Public Class XboxUI
#Region "Home tiles Got/Lost focus"
    Private Sub Home_Tile1_GotFocus(sender As Object, e As RoutedEventArgs) Handles Home_Tile1.GotFocus
        Grid.SetZIndex(Home_Tile1, 1)
        Home_Tile1.BeginAnimation(MarginProperty, New Animation.ThicknessAnimation(New Thickness(Home_Tile1.Margin.Left - 5, Home_Tile1.Margin.Top - 5, 0, 0), New Duration(TimeSpan.FromMilliseconds(100))))
        Home_Tile1.BeginAnimation(WidthProperty, New Animation.DoubleAnimation(Home_Tile1.Width + 15, New Duration(TimeSpan.FromMilliseconds(100))))
        Home_Tile1.BeginAnimation(HeightProperty, New Animation.DoubleAnimation(Home_Tile1.Height + 15, New Duration(TimeSpan.FromMilliseconds(100))))
    End Sub

    Private Sub Home_Tile2_GotFocus(sender As Object, e As RoutedEventArgs) Handles Home_Tile2.GotFocus
        Grid.SetZIndex(Home_Tile2, 1)
        Home_Tile2.BeginAnimation(MarginProperty, New Animation.ThicknessAnimation(New Thickness(Home_Tile2.Margin.Left - 5, Home_Tile2.Margin.Top - 5, 0, 0), New Duration(TimeSpan.FromMilliseconds(100))))
        Home_Tile2.BeginAnimation(WidthProperty, New Animation.DoubleAnimation(Home_Tile2.Width + 15, New Duration(TimeSpan.FromMilliseconds(100))))
        Home_Tile2.BeginAnimation(HeightProperty, New Animation.DoubleAnimation(Home_Tile2.Height + 15, New Duration(TimeSpan.FromMilliseconds(100))))
    End Sub

    Private Sub Home_Tile3_GotFocus(sender As Object, e As RoutedEventArgs) Handles Home_Tile3.GotFocus
        Grid.SetZIndex(Home_Tile3, 1)
        Home_Tile3.BeginAnimation(MarginProperty, New Animation.ThicknessAnimation(New Thickness(Home_Tile3.Margin.Left - 5, Home_Tile3.Margin.Top - 5, 0, 0), New Duration(TimeSpan.FromMilliseconds(100))))
        Home_Tile3.BeginAnimation(WidthProperty, New Animation.DoubleAnimation(Home_Tile3.Width + 15, New Duration(TimeSpan.FromMilliseconds(100))))
        Home_Tile3.BeginAnimation(HeightProperty, New Animation.DoubleAnimation(Home_Tile3.Height + 15, New Duration(TimeSpan.FromMilliseconds(100))))
    End Sub

    Private Sub Home_Tile4_GotFocus(sender As Object, e As RoutedEventArgs) Handles Home_Tile4.GotFocus
        Grid.SetZIndex(Home_Tile4, 1)
        Home_Tile4.BeginAnimation(MarginProperty, New Animation.ThicknessAnimation(New Thickness(Home_Tile4.Margin.Left - 5, Home_Tile4.Margin.Top - 5, 0, 0), New Duration(TimeSpan.FromMilliseconds(100))))
        Home_Tile4.BeginAnimation(WidthProperty, New Animation.DoubleAnimation(Home_Tile4.Width + 15, New Duration(TimeSpan.FromMilliseconds(100))))
        Home_Tile4.BeginAnimation(HeightProperty, New Animation.DoubleAnimation(Home_Tile4.Height + 15, New Duration(TimeSpan.FromMilliseconds(100))))
    End Sub

    Private Sub Home_Tile5_GotFocus(sender As Object, e As RoutedEventArgs) Handles Home_Tile5.GotFocus
        Grid.SetZIndex(Home_Tile5, 1)
        Home_Tile5.BeginAnimation(MarginProperty, New Animation.ThicknessAnimation(New Thickness(Home_Tile5.Margin.Left - 5, Home_Tile5.Margin.Top - 5, 0, 0), New Duration(TimeSpan.FromMilliseconds(100))))
        Home_Tile5.BeginAnimation(WidthProperty, New Animation.DoubleAnimation(Home_Tile5.Width + 15, New Duration(TimeSpan.FromMilliseconds(100))))
        Home_Tile5.BeginAnimation(HeightProperty, New Animation.DoubleAnimation(Home_Tile5.Height + 15, New Duration(TimeSpan.FromMilliseconds(100))))
    End Sub

    Private Sub Home_Tile6_GotFocus(sender As Object, e As RoutedEventArgs) Handles Home_Tile6.GotFocus
        Grid.SetZIndex(Home_Tile6, 1)
        Home_Tile6.BeginAnimation(MarginProperty, New Animation.ThicknessAnimation(New Thickness(Home_Tile6.Margin.Left - 5, Home_Tile6.Margin.Top - 5, 0, 0), New Duration(TimeSpan.FromMilliseconds(100))))
        Home_Tile6.BeginAnimation(WidthProperty, New Animation.DoubleAnimation(Home_Tile6.Width + 15, New Duration(TimeSpan.FromMilliseconds(100))))
        Home_Tile6.BeginAnimation(HeightProperty, New Animation.DoubleAnimation(Home_Tile6.Height + 15, New Duration(TimeSpan.FromMilliseconds(100))))
    End Sub

    Private Sub Home_Tile7_GotFocus(sender As Object, e As RoutedEventArgs) Handles Home_Tile7.GotFocus
        Grid.SetZIndex(Home_Tile7, 1)
        Home_Tile7.BeginAnimation(MarginProperty, New Animation.ThicknessAnimation(New Thickness(Home_Tile7.Margin.Left - 5, Home_Tile7.Margin.Top - 5, 0, 0), New Duration(TimeSpan.FromMilliseconds(100))))
        Home_Tile7.BeginAnimation(WidthProperty, New Animation.DoubleAnimation(Home_Tile7.Width + 15, New Duration(TimeSpan.FromMilliseconds(100))))
        Home_Tile7.BeginAnimation(HeightProperty, New Animation.DoubleAnimation(Home_Tile7.Height + 15, New Duration(TimeSpan.FromMilliseconds(100))))
    End Sub

    Private Sub Home_Tile8_GotFocus(sender As Object, e As RoutedEventArgs) Handles Home_Tile8.GotFocus
        Grid.SetZIndex(Home_Tile8, 1)
        Home_Tile8.BeginAnimation(MarginProperty, New Animation.ThicknessAnimation(New Thickness(Home_Tile8.Margin.Left - 5, Home_Tile8.Margin.Top - 5, 0, 0), New Duration(TimeSpan.FromMilliseconds(100))))
        Home_Tile8.BeginAnimation(WidthProperty, New Animation.DoubleAnimation(Home_Tile8.Width + 15, New Duration(TimeSpan.FromMilliseconds(100))))
        Home_Tile8.BeginAnimation(HeightProperty, New Animation.DoubleAnimation(Home_Tile8.Height + 15, New Duration(TimeSpan.FromMilliseconds(100))))
    End Sub

    Private Sub Home_Tile9_GotFocus(sender As Object, e As RoutedEventArgs) Handles Home_Tile9.GotFocus
        Grid.SetZIndex(Home_Tile9, 1)
        Home_Tile9.BeginAnimation(MarginProperty, New Animation.ThicknessAnimation(New Thickness(Home_Tile9.Margin.Left - 5, Home_Tile9.Margin.Top - 5, 0, 0), New Duration(TimeSpan.FromMilliseconds(100))))
        Home_Tile9.BeginAnimation(WidthProperty, New Animation.DoubleAnimation(Home_Tile9.Width + 15, New Duration(TimeSpan.FromMilliseconds(100))))
        Home_Tile9.BeginAnimation(HeightProperty, New Animation.DoubleAnimation(Home_Tile9.Height + 15, New Duration(TimeSpan.FromMilliseconds(100))))
    End Sub
    '----------------------------------------------------------------------------------------------Lost Focus---------------------------------------------------------------------------------------------------------
    Private Sub Home_Tile1_LostFocus(sender As Object, e As RoutedEventArgs) Handles Home_Tile1.LostFocus
        Grid.SetZIndex(Home_Tile1, 0)
        Home_Tile1.BeginAnimation(MarginProperty, New Animation.ThicknessAnimation(New Thickness(Home_Tile1.Margin.Left + 5, Home_Tile1.Margin.Top + 5, 0, 0), New Duration(TimeSpan.FromMilliseconds(100))))
        Home_Tile1.BeginAnimation(WidthProperty, New Animation.DoubleAnimation(Home_Tile1.Width - 15, New Duration(TimeSpan.FromMilliseconds(100))))
        Home_Tile1.BeginAnimation(HeightProperty, New Animation.DoubleAnimation(Home_Tile1.Height - 15, New Duration(TimeSpan.FromMilliseconds(100))))
    End Sub

    Private Sub Home_Tile2_LostFocus(sender As Object, e As RoutedEventArgs) Handles Home_Tile2.LostFocus
        Grid.SetZIndex(Home_Tile2, 0)
        Home_Tile2.BeginAnimation(MarginProperty, New Animation.ThicknessAnimation(New Thickness(Home_Tile2.Margin.Left + 5, Home_Tile2.Margin.Top + 5, 0, 0), New Duration(TimeSpan.FromMilliseconds(100))))
        Home_Tile2.BeginAnimation(WidthProperty, New Animation.DoubleAnimation(Home_Tile2.Width - 15, New Duration(TimeSpan.FromMilliseconds(100))))
        Home_Tile2.BeginAnimation(HeightProperty, New Animation.DoubleAnimation(Home_Tile2.Height - 15, New Duration(TimeSpan.FromMilliseconds(100))))
    End Sub

    Private Sub Home_Tile3_LostFocus(sender As Object, e As RoutedEventArgs) Handles Home_Tile3.LostFocus
        Grid.SetZIndex(Home_Tile3, 0)
        Home_Tile3.BeginAnimation(MarginProperty, New Animation.ThicknessAnimation(New Thickness(Home_Tile3.Margin.Left + 5, Home_Tile3.Margin.Top + 5, 0, 0), New Duration(TimeSpan.FromMilliseconds(100))))
        Home_Tile3.BeginAnimation(WidthProperty, New Animation.DoubleAnimation(Home_Tile3.Width - 15, New Duration(TimeSpan.FromMilliseconds(100))))
        Home_Tile3.BeginAnimation(HeightProperty, New Animation.DoubleAnimation(Home_Tile3.Height - 15, New Duration(TimeSpan.FromMilliseconds(100))))
    End Sub

    Private Sub Home_Tile4_LostFocus(sender As Object, e As RoutedEventArgs) Handles Home_Tile4.LostFocus
        Grid.SetZIndex(Home_Tile4, 0)
        Home_Tile4.BeginAnimation(MarginProperty, New Animation.ThicknessAnimation(New Thickness(Home_Tile4.Margin.Left + 5, Home_Tile4.Margin.Top + 5, 0, 0), New Duration(TimeSpan.FromMilliseconds(100))))
        Home_Tile4.BeginAnimation(WidthProperty, New Animation.DoubleAnimation(Home_Tile4.Width - 15, New Duration(TimeSpan.FromMilliseconds(100))))
        Home_Tile4.BeginAnimation(HeightProperty, New Animation.DoubleAnimation(Home_Tile4.Height - 15, New Duration(TimeSpan.FromMilliseconds(100))))
    End Sub

    Private Sub Home_Tile5_LostFocus(sender As Object, e As RoutedEventArgs) Handles Home_Tile5.LostFocus
        Grid.SetZIndex(Home_Tile5, 0)
        Home_Tile5.BeginAnimation(MarginProperty, New Animation.ThicknessAnimation(New Thickness(Home_Tile5.Margin.Left + 5, Home_Tile5.Margin.Top + 5, 0, 0), New Duration(TimeSpan.FromMilliseconds(100))))
        Home_Tile5.BeginAnimation(WidthProperty, New Animation.DoubleAnimation(Home_Tile5.Width - 15, New Duration(TimeSpan.FromMilliseconds(100))))
        Home_Tile5.BeginAnimation(HeightProperty, New Animation.DoubleAnimation(Home_Tile5.Height - 15, New Duration(TimeSpan.FromMilliseconds(100))))
    End Sub

    Private Sub Home_Tile6_LostFocus(sender As Object, e As RoutedEventArgs) Handles Home_Tile6.LostFocus
        Grid.SetZIndex(Home_Tile6, 0)
        Home_Tile6.BeginAnimation(MarginProperty, New Animation.ThicknessAnimation(New Thickness(Home_Tile6.Margin.Left + 5, Home_Tile6.Margin.Top + 5, 0, 0), New Duration(TimeSpan.FromMilliseconds(100))))
        Home_Tile6.BeginAnimation(WidthProperty, New Animation.DoubleAnimation(Home_Tile6.Width - 15, New Duration(TimeSpan.FromMilliseconds(100))))
        Home_Tile6.BeginAnimation(HeightProperty, New Animation.DoubleAnimation(Home_Tile6.Height - 15, New Duration(TimeSpan.FromMilliseconds(100))))
    End Sub

    Private Sub Home_Tile7_LostFocus(sender As Object, e As RoutedEventArgs) Handles Home_Tile7.LostFocus
        Grid.SetZIndex(Home_Tile7, 0)
        Home_Tile7.BeginAnimation(MarginProperty, New Animation.ThicknessAnimation(New Thickness(Home_Tile7.Margin.Left + 5, Home_Tile7.Margin.Top + 5, 0, 0), New Duration(TimeSpan.FromMilliseconds(100))))
        Home_Tile7.BeginAnimation(WidthProperty, New Animation.DoubleAnimation(Home_Tile7.Width - 15, New Duration(TimeSpan.FromMilliseconds(100))))
        Home_Tile7.BeginAnimation(HeightProperty, New Animation.DoubleAnimation(Home_Tile7.Height - 15, New Duration(TimeSpan.FromMilliseconds(100))))
    End Sub

    Private Sub Home_Tile8_LostFocus(sender As Object, e As RoutedEventArgs) Handles Home_Tile8.LostFocus
        Grid.SetZIndex(Home_Tile8, 0)
        Home_Tile8.BeginAnimation(MarginProperty, New Animation.ThicknessAnimation(New Thickness(Home_Tile8.Margin.Left + 5, Home_Tile8.Margin.Top + 5, 0, 0), New Duration(TimeSpan.FromMilliseconds(100))))
        Home_Tile8.BeginAnimation(WidthProperty, New Animation.DoubleAnimation(Home_Tile8.Width - 15, New Duration(TimeSpan.FromMilliseconds(100))))
        Home_Tile8.BeginAnimation(HeightProperty, New Animation.DoubleAnimation(Home_Tile8.Height - 15, New Duration(TimeSpan.FromMilliseconds(100))))
    End Sub

    Private Sub Home_Tile9_LostFocus(sender As Object, e As RoutedEventArgs) Handles Home_Tile9.LostFocus
        Grid.SetZIndex(Home_Tile9, 0)
        Home_Tile9.BeginAnimation(MarginProperty, New Animation.ThicknessAnimation(New Thickness(Home_Tile9.Margin.Left + 5, Home_Tile9.Margin.Top + 5, 0, 0), New Duration(TimeSpan.FromMilliseconds(100))))
        Home_Tile9.BeginAnimation(WidthProperty, New Animation.DoubleAnimation(Home_Tile9.Width - 15, New Duration(TimeSpan.FromMilliseconds(100))))
        Home_Tile9.BeginAnimation(HeightProperty, New Animation.DoubleAnimation(Home_Tile9.Height - 15, New Duration(TimeSpan.FromMilliseconds(100))))
    End Sub

#End Region
    Public Property MainLibrary As New Library(My.Settings.LibraryPath)
    Public Property OsdPlacement As MainWindow.OsdPlace = MainWindow.OsdPlace.Home
    Private _Home_FocusedItem As Integer = 0
    Public Property Home_FocusedItem As Integer
        Get
            Return _Home_FocusedItem
        End Get
        Set(value As Integer)
            _Home_FocusedItem = value
            CType(CType(Main_TabControl.Items.Item(0), TabItem).Content, Grid).Children.Item(value).Focus()
        End Set
    End Property
    Private _Games_FocusedItem As Integer = 0
    Public Property Games_FocusedItem As Integer
        Get
            Return _Games_FocusedItem
        End Get
        Set(value As Integer)
            _Games_FocusedItem = value
            Games_WP.Children.Item(value).Focus()
        End Set
    End Property
    Private Sub XboxUI_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        Dim rnd As New Random
        Dim srnd = rnd.Next(0, MainLibrary.LibGames.Count - 1)
        With MainLibrary.LibGames.Item(srnd)
            Home_Tile4.Title = .Name
            CType(CType(Home_Tile4.Content, Grid).Children(0), Image).Source = .BigIcon
        End With
        srnd = rnd.Next(0, MainLibrary.LibGames.Count - 1)
        With MainLibrary.LibGames.Item(srnd)
            Home_Tile5.Title = .Name
            CType(CType(Home_Tile5.Content, Grid).Children(0), Image).Source = .Icon
        End With
        srnd = rnd.Next(0, MainLibrary.LibGames.Count - 1)
        With MainLibrary.LibGames.Item(srnd)
            Home_Tile6.Title = .Name
            CType(CType(Home_Tile6.Content, Grid).Children(0), Image).Source = .Icon
        End With
        srnd = rnd.Next(0, MainLibrary.LibGames.Count - 1)
        With MainLibrary.LibGames.Item(srnd)
            Home_Tile7.Title = .Name
            CType(CType(Home_Tile7.Content, Grid).Children(0), Image).Source = .Icon
        End With
        srnd = rnd.Next(0, MainLibrary.LibGames.Count - 1)
        With MainLibrary.LibGames.Item(srnd)
            Home_Tile8.Title = .Name
            CType(CType(Home_Tile8.Content, Grid).Children(0), Image).Source = .Icon
        End With
        srnd = rnd.Next(0, MainLibrary.LibGames.Count - 1)
        With MainLibrary.LibGames.Item(srnd)
            Home_Tile9.Title = .Name
            CType(CType(Home_Tile9.Content, Grid).Children(0), Image).Source = .Icon
        End With
        For Each game In MainLibrary.LibGames
            Games_WP.Children.Add(New GameItemXbox360(game.Name, game.Icon) With {.Margin = New Thickness(5, 0, 0, 0), .VerticalAlignment = VerticalAlignment.Top})
        Next
    End Sub
    Private Sub XboxUI_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        Select Case OsdPlacement
            Case MainWindow.OsdPlace.Home
                Select Case e.Key
                    Case Key.Left
                        Select Case Home_FocusedItem
                            Case 0 'S L T Tile
                            Case 1 'S L M Tile
                            Case 2 'S L B Tile
                            Case 3 'B C T Tile
                                Home_FocusedItem = 0
                            Case 4 'S C B Tile
                                Home_FocusedItem = 2
                            Case 5 'S C B Tile 2
                                Home_FocusedItem = 4
                            Case 6 'S R T Tile
                                Home_FocusedItem = 3
                            Case 7 'S R M Tile
                                Home_FocusedItem = 3
                            Case 8 'S R B Tile      
                                Home_FocusedItem = 5
                        End Select
                        Utils.S_Xbox360Nav_Player.Play()
                    Case Key.Right
                        Select Case Home_FocusedItem
                            Case 0 'S L T Tile
                                Home_FocusedItem = 3
                                Utils.S_Xbox360Nav_Player.Play()
                            Case 1 'S L M Tile
                                Home_FocusedItem = 3
                                Utils.S_Xbox360Nav_Player.Play()
                            Case 2 'S L B Tile
                                Home_FocusedItem = 4
                                Utils.S_Xbox360Nav_Player.Play()
                            Case 3 'B C T Tile
                                Home_FocusedItem = 6
                                Utils.S_Xbox360Nav_Player.Play()
                            Case 4 'S C B Tile
                                Home_FocusedItem = 5
                                Utils.S_Xbox360Nav_Player.Play()
                            Case 5 'S C B Tile 2
                                Home_FocusedItem = 8
                                Utils.S_Xbox360Nav_Player.Play()
                            Case 6 'S R T Tile      
                                Main_TabControl.SelectedIndex = 1
                                OsdPlacement = MainWindow.OsdPlace.Games
                                Utils.S_Xbox360Select_Player.Play()
                            Case 7 'S R M Tile
                                Main_TabControl.SelectedIndex = 1
                                OsdPlacement = MainWindow.OsdPlace.Games
                                Utils.S_Xbox360Select_Player.Play()
                            Case 8 'S R B Tile      
                                Main_TabControl.SelectedIndex = 1
                                OsdPlacement = MainWindow.OsdPlace.Games
                                Utils.S_Xbox360Select_Player.Play()
                        End Select
                    Case Key.Up
                        Select Case Home_FocusedItem
                            Case 0 'S L T Tile
                            Case 1 'S L M Tile
                                Home_FocusedItem = 0
                            Case 2 'S L B Tile
                                Home_FocusedItem = 1
                            Case 3 'B C T Tile                        
                            Case 4 'S C B Tile
                                Home_FocusedItem = 3
                            Case 5 'S C B Tile 2
                                Home_FocusedItem = 3
                            Case 6 'S R T Tile
                            Case 7 'S R M Tile
                                Home_FocusedItem = 6
                            Case 8 'S R B Tile      
                                Home_FocusedItem = 7
                        End Select

                        Utils.S_Xbox360Nav_Player.Play()
                    Case Key.Down
                        Select Case Home_FocusedItem
                            Case 0 'S L T Tile
                                Home_FocusedItem = 1
                            Case 1 'S L M Tile
                                Home_FocusedItem = 2
                            Case 2 'S L B Tile
                            Case 3 'B C T Tile
                                Home_FocusedItem = 4
                            Case 4 'S C B Tile
                            Case 5 'S C B Tile 2
                            Case 6 'S R T Tile
                                Home_FocusedItem = 7
                            Case 7 'S R M Tile
                                Home_FocusedItem = 8
                            Case 8 'S R B Tile      
                        End Select
                        Utils.S_Xbox360Nav_Player.Play()
                End Select
            Case MainWindow.OsdPlace.Games
                Select Case e.Key
                    Case Key.Right
                        If Games_FocusedItem + 1 = Games_WP.Children.Count Then
                            Games_FocusedItem = 0
                        Else
                            Games_FocusedItem += 1
                        End If
                    Case Key.Left
                        If Games_FocusedItem - 1 = 0 Then
                            Games_FocusedItem = 0
                            Main_TabControl.SelectedIndex = 0
                            OsdPlacement = MainWindow.OsdPlace.Home
                        Else
                            Games_FocusedItem -= 1
                        End If
                End Select
        End Select
        e.Handled = True
    End Sub

    Private Sub sc_PreviewKeyDown(sender As Object, e As KeyEventArgs) Handles sc.PreviewKeyDown
        Select Case e.Key
            Case Key.Right
                If Games_FocusedItem + 1 = Games_WP.Children.Count Then
                    Games_FocusedItem = 0
                Else
                    Games_FocusedItem += 1
                End If
            Case Key.Left
                If Games_FocusedItem = 0 Then
                    Games_FocusedItem = 0
                    Main_TabControl.SelectedIndex = 0
                    OsdPlacement = MainWindow.OsdPlace.Home
                Else
                    Games_FocusedItem -= 1
                End If
        End Select
        e.Handled = True
    End Sub
End Class
