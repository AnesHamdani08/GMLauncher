Public Class GameItem
    Private WithEvents Tmr As New Forms.Timer With {.Interval = 2000, .Enabled = True}
    Public Property Title As String
    Public Property Icon As ImageSource
    Public Property BigIcon As ImageSource
    Public Property Source As String
    Dim IsFocusBoxFull As Boolean = False
    Dim WithEvents BrdTmr As New Forms.Timer With {.Interval = 1000}
    Public Sub New(_Title As String, _Icon As ImageSource, Src As String, _BigIcon As ImageSource)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Game_Title.Text = _Title
        Game_Logo.Source = _Icon
        Title = _Title
        Icon = _Icon
        BigIcon = _BigIcon
        Source = Src
    End Sub
    Private Sub GameItem_GotFocus(sender As Object, e As RoutedEventArgs) Handles Me.GotFocus
        Game_Title.Text = Title
        BeginAnimation(HeightProperty, New Animation.DoubleAnimation(200, New Duration(TimeSpan.FromMilliseconds(100))))
        Game_Title_OW.BeginAnimation(Grid.MarginProperty, New Animation.ThicknessAnimation(New Thickness(0, 0, 0, 0), New Duration(TimeSpan.FromMilliseconds(200))))
        Game_Logo.BeginAnimation(Image.MarginProperty, New Animation.ThicknessAnimation(New Thickness(-30, -30, -30, -30), New Duration(TimeSpan.FromSeconds(10))))
        Tmr.Start()
    End Sub

    Private Sub GameItem_LostFocus(sender As Object, e As RoutedEventArgs) Handles Me.LostFocus
        BeginAnimation(HeightProperty, New Animation.DoubleAnimation(180, New Duration(TimeSpan.FromMilliseconds(100))))
        Game_Title_OW.BeginAnimation(Grid.MarginProperty, New Animation.ThicknessAnimation(New Thickness(0, 0, 0, -30), New Duration(TimeSpan.FromMilliseconds(200))))
        Game_Logo.BeginAnimation(Image.MarginProperty, New Animation.ThicknessAnimation(New Thickness(0, 0, 0, 0), New Duration(TimeSpan.FromMilliseconds(100))))
        Tmr.Stop()
    End Sub

    Private Async Sub Tmr_Tick(sender As Object, e As EventArgs) Handles Tmr.Tick
        Game_Title.BeginAnimation(TextBlock.OpacityProperty, New Animation.DoubleAnimation(0, New Duration(TimeSpan.FromMilliseconds(200))))
        Await Task.Delay(200)
        Game_Title.Text = "Start"
        Game_Title.BeginAnimation(TextBlock.OpacityProperty, New Animation.DoubleAnimation(1, New Duration(TimeSpan.FromMilliseconds(200))))
    End Sub

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
End Class
