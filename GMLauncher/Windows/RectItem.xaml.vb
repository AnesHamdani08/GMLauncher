Public Class RectItem
    Private _Message As String
    Dim IsFocusBoxFull As Boolean = False
    Dim WithEvents Tmr As New Forms.Timer With {.Interval = 1000}
    Public OnClickAction As Action = Nothing
    Dim CaptureNext As Boolean = True
    Public WriteOnly Property Icon As ImageSource
        Set(value As ImageSource)
            Rect_Icon.Source = value
        End Set
    End Property
    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub
    Public Sub New(OnClick As Action)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        OnClickAction = OnClick
    End Sub
    Private Sub RectItem_Initialized(sender As Object, e As EventArgs) Handles Me.Initialized
        Dim s As Style = New Style()
        s.Setters.Add(New Setter(FocusVisualStyleProperty, Nothing))
        FocusVisualStyle = s
    End Sub
    Public Property Message As String
        Get
            Return _Message
        End Get
        Set(value As String)
            _Message = value
            Message_TB.Text = value
        End Set
    End Property
    Private Sub RectItem_GotFocus(sender As Object, e As RoutedEventArgs) Handles Me.GotFocus
        Main_Border.BeginAnimation(BorderThicknessProperty, New Animation.ThicknessAnimation(New Thickness(1, 1, 1, 1), New Duration(TimeSpan.FromMilliseconds(200))))
        Tmr.Start()
    End Sub

    Private Sub RectItem_LostFocus(sender As Object, e As RoutedEventArgs) Handles Me.LostFocus
        Tmr.Stop()
        Main_Border.BeginAnimation(BorderThicknessProperty, New Animation.ThicknessAnimation(New Thickness(0, 0, 0, 0), New Duration(TimeSpan.FromMilliseconds(200))))
    End Sub

    Private Sub Tmr_Tick(sender As Object, e As EventArgs) Handles Tmr.Tick
        If IsFocusBoxFull Then
            Main_Border.BeginAnimation(BorderThicknessProperty, New Animation.ThicknessAnimation(New Thickness(1, 1, 1, 1), New Duration(TimeSpan.FromSeconds(1))))
            IsFocusBoxFull = False
        Else
            Main_Border.BeginAnimation(BorderThicknessProperty, New Animation.ThicknessAnimation(New Thickness(3, 3, 3, 3), New Duration(TimeSpan.FromSeconds(1))))
            IsFocusBoxFull = True
        End If
    End Sub

    Private Sub RectItem_PreviewKeyUp(sender As Object, e As KeyEventArgs) Handles Me.PreviewKeyUp
        If CaptureNext Then
            If e.Key = Key.Enter AndAlso OnClickAction IsNot Nothing Then
                CaptureNext = False
                OnClickAction.Invoke
            End If
        Else
            CaptureNext = True
        End If
    End Sub
End Class
