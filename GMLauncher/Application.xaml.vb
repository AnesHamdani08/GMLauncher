Imports System.Windows.Threading

Class Application
    Private Sub Application_DispatcherUnhandledException(sender As Object, e As DispatcherUnhandledExceptionEventArgs) Handles Me.DispatcherUnhandledException
        TryCast(Application.Current.MainWindow, MainWindow).TriggerError(e.Exception.Message, True, False)
        e.Handled = True
    End Sub

    Private Sub Application_Exit(sender As Object, e As ExitEventArgs) Handles Me.Exit

    End Sub

    ' Application-level events, such as Startup, Exit, and DispatcherUnhandledException
    ' can be handled in this file.

End Class
