Imports System.ComponentModel

Class MainWindow
    Private Sub mmExit_Click(sender As Object, e As RoutedEventArgs) Handles mmExit.Click
        Me.Close()
    End Sub

    Private Sub mmAdmin_Click(sender As Object, e As RoutedEventArgs) Handles mmAdmin.Click
        Dim Adm As Administration = New Administration(Me)
        Adm.clearpage()
        Adm.Show()
    End Sub
    Private Sub MainWindow_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing

        Dim response = MessageBox.Show("Do you really want to exit?", "Exiting...",
                                        MessageBoxButton.YesNo, MessageBoxImage.Exclamation)
        If (response = MessageBoxResult.No) Then
            e.Cancel = True
        Else
            Application.Current.Shutdown()
        End If

    End Sub

    Private Sub mmNew_Click(sender As Object, e As RoutedEventArgs) Handles mmNew.Click
        Dim NewLge As NewLeague_Settings = New NewLeague_Settings(Me)
        NewLge.Show()
    End Sub
End Class
