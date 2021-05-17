Public Class GameItemXbox360
    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub
    Public Sub New(Title As String, Icon As ImageSource)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Game_Title.Text = Title
        Game_Icon.Source = Icon
    End Sub

    Private Sub GameItemXbox360_GotFocus(sender As Object, e As RoutedEventArgs) Handles Me.GotFocus
        BeginAnimation(HeightProperty, New Animation.DoubleAnimation(360, New Duration(TimeSpan.FromMilliseconds(100))))
        BeginAnimation(WidthProperty, New Animation.DoubleAnimation(205, New Duration(TimeSpan.FromMilliseconds(100))))
    End Sub

    Private Sub GameItemXbox360_LostFocus(sender As Object, e As RoutedEventArgs) Handles Me.LostFocus
        BeginAnimation(HeightProperty, New Animation.DoubleAnimation(345, New Duration(TimeSpan.FromMilliseconds(100))))
        BeginAnimation(WidthProperty, New Animation.DoubleAnimation(190, New Duration(TimeSpan.FromMilliseconds(100))))
    End Sub
End Class
