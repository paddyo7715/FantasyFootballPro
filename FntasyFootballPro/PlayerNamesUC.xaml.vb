Imports System.IO
Imports System.Windows
Imports Microsoft.Win32

Public Class PlayerNamesUC
    Public Event Show_MainMenu As EventHandler

    Private Sub admUbtnSelectFile_Click(sender As Object, e As RoutedEventArgs) Handles admUbtnSelectFile.Click
        Dim OpenFileDialog As OpenFileDialog = New OpenFileDialog()
        If (OpenFileDialog.ShowDialog() = True) Then
            admtxtSelectFile.Text = OpenFileDialog.FileName
        End If
    End Sub

    Private Sub admBack_Click(sender As Object, e As RoutedEventArgs) Handles admBack.Click
        RaiseEvent Show_MainMenu(Me, New EventArgs)
    End Sub
    Public Sub clearpage()
        Dim adm_service As Administration_Services = New Administration_Services
        Dim r As Long()

        Try
            admFirstName.Text = ""
            admLastName.Text = ""
            admtxtSelectFile.Text = ""
            r = adm_service.getPlayerNameTotals
            admtotFirstName.Content = Format(r(0), "###,###,###,##0")
            admtotLastName.Content = Format(r(1), "###,###,###,##0")

        Catch ex As Exception
            MessageBox.Show("An error occured while retrieving player names. " + CommonUtils.substr(ex.Message, 0, 100), "Error", MessageBoxButton.OK, MessageBoxImage.Error)
        End Try

    End Sub

    Private Sub admSubmit_Click(sender As Object, e As RoutedEventArgs) Handles admSubmit.Click
        Dim adm_service As Administration_Services = New Administration_Services
        Dim r As Long

        Try
            Mouse.OverrideCursor = Cursors.Wait
            r = adm_service.AddPlayerNames(admFirstName.Text, admLastName.Text, admtxtSelectFile.Text)
            Mouse.OverrideCursor = Nothing
            MessageBox.Show(Format(r, "###,###,###,##0") + " Player Names Added.", "", MessageBoxButton.OK, MessageBoxImage.Information)
            clearpage()
        Catch ex As Exception
            Mouse.OverrideCursor = Nothing
            MessageBox.Show("An error occured while trying to add the new names " + CommonUtils.substr(ex.Message, 0, 100), "Error", MessageBoxButton.OK, MessageBoxImage.Error)
        End Try
    End Sub


End Class
