Imports System.ComponentModel

Public Class MainMenuUC

    Public Event Shutdown_App As EventHandler
    Public Event Show_NewLeague As EventHandler

    Private Sub mmNew_Click(sender As Object, e As RoutedEventArgs) Handles mmNew.Click
        RaiseEvent Show_NewLeague(Me, New EventArgs)
    End Sub
    Private Sub mmExit_Click(sender As Object, e As RoutedEventArgs) Handles mmExit.Click
        Dim parent As MainWindow = Application.Current.MainWindow
        parent.Close()
    End Sub
    Private Sub mmAdmin_Click(sender As Object, e As RoutedEventArgs) Handles mmAdmin.Click
        '        Dim Adm As Administration = New Administration(Me)
        '        Adm.clearpage()
        '       Adm.Show()
    End Sub

End Class
