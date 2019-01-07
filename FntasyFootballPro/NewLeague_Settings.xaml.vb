Imports System.IO
Imports Microsoft.Win32

Public Class NewLeague_Settings

    Property winMainMenu As MainWindow


    Public Sub New(ByVal winMainMenu As MainWindow)

        ' This call is required by the designer.
        InitializeComponent()

        Me.winMainMenu = winMainMenu

        Dim icurrentyear As Integer = Date.Today.Year

        For i As Integer = icurrentyear - 100 To icurrentyear + 100
            newl1StartingYear.Items.Add(i.ToString)
        Next
        newl1StartingYear.Text = icurrentyear.ToString

    End Sub


    Private Sub validate()

        Dim DIRPath As String
        DIRPath = System.IO.Path.Combine(My.Computer.FileSystem.SpecialDirectories.MyDocuments, App_Constants.LEAGUE_DB_FOLDER & "/" & newl1shortname.Text)

        If CommonUtils.isBlank(newl1shortname.Text) Then Throw New Exception("League Short Name must be supplied!")
        If CommonUtils.isBlank(newl1longname.Text) Then Throw New Exception("League Long Name must be supplied!")
        If CommonUtils.isBlank(newl1LogoPath.Text) Then Throw New Exception("League logo path must be supplied!")
        If CommonUtils.isBlank(newl1championshipgame.Text) Then Throw New Exception("Championship Game must be supplied!")
        If CommonUtils.isBlank(newl1LogoPath.Text) Then Throw New Exception("League logo image must be supplied!")
        If CommonUtils.isBlank(newl1TrophyPath.Text) Then Throw New Exception("League trophy image must be supplied!")


        If CommonUtils.isBlank(newlnumweeks.Text) OrElse Not IsNumeric(newlnumweeks.Text) Then Throw New Exception("Invalid Value for Number of Weeks!")
        If CommonUtils.isBlank(newlnumgames.Text) OrElse Not IsNumeric(newlnumgames.Text) Then Throw New Exception("Invalid Value for Number of Games!")
        If CommonUtils.isBlank(newlnumdivisions.Text) OrElse Not IsNumeric(newlnumdivisions.Text) Then Throw New Exception("Invalid Value for Number of Divisions!")
        If CommonUtils.isBlank(newlnumteamsperdivisions.Text) OrElse Not IsNumeric(newlnumteamsperdivisions.Text) Then Throw New Exception("Invalid Value for Number of Teams in Each Division!")

        If CommonUtils.isBlank(newlConf1.Text) Then Throw New Exception("Conference 1 Name Must be Supplied!")
        If CommonUtils.isBlank(newlConf2.Text) Then Throw New Exception("Conference 2 Name Must be Supplied!")

        If CommonUtils.isBlank(newldiv1.Text) Then Throw New Exception("Division 1 Name Must be Supplied!")
        If CommonUtils.isBlank(newldiv2.Text) Then Throw New Exception("Division 2 Name Must be Supplied!")
        If CommonUtils.isBlank(newldiv3.Text) Then Throw New Exception("Division 3 Name Must be Supplied!")
        If CommonUtils.isBlank(newldiv4.Text) Then Throw New Exception("Division 4 Name Must be Supplied!")

        If CommonUtils.isBlank(newldiv5.Text) Then Throw New Exception("Division 5 Name Must be Supplied!")
        If CommonUtils.isBlank(newldiv6.Text) Then Throw New Exception("Division 6 Name Must be Supplied!")
        If CommonUtils.isBlank(newldiv7.Text) Then Throw New Exception("Division 7 Name Must be Supplied!")
        If CommonUtils.isBlank(newldiv8.Text) Then Throw New Exception("Division 8 Name Must be Supplied!")


        If Directory.Exists(DIRPath) Then
            Throw New Exception("League " & newl1shortname.Text & " already exists!")
        End If

    End Sub
    Private Sub newl1btnLogoPath_Click(sender As Object, e As RoutedEventArgs) Handles newl1btnLogoPath.Click
        Dim OpenFileDialog As OpenFileDialog = New OpenFileDialog()
        If (OpenFileDialog.ShowDialog() = True) Then
            Dim filepath As String = OpenFileDialog.FileName
            newl1LogoPath.Text = filepath
            newl1LogoImage.Source = New BitmapImage(New Uri(filepath))
        End If
    End Sub
    Private Sub newl1btnTrophyPath_Click(sender As Object, e As RoutedEventArgs) Handles newl1btnTrophyPath.Click
        Dim OpenFileDialog As OpenFileDialog = New OpenFileDialog()
        If (OpenFileDialog.ShowDialog() = True) Then
            Dim filepath As String = OpenFileDialog.FileName
            newl1TrophyPath.Text = filepath
            newl1TrophyImage.Source = New BitmapImage(New Uri(filepath))
        End If
    End Sub
    Private Sub newl1Cancel_Click(sender As Object, e As RoutedEventArgs) Handles newl1Cancel.Click
        Me.Close()
        winMainMenu.Show()
    End Sub
    Private Sub newl1Next_Click(sender As Object, e As RoutedEventArgs) Handles newl1Next.Click

        Try
            validate()

            Dim nl As Leaguemdl = New Leaguemdl(newl1LogoPath.Text, newl1shortname.Text, newl1longname.Text, CInt(newl1StartingYear.Text),
            CInt(newlnumweeks.Text), CInt(newlnumgames.Text), newl1championshipgame.Text, newl1TrophyPath.Text, CInt(newlnumdivisions.Text),
            CInt(newlnumteamsperdivisions.Text), newldiv1.Text, newldiv2.Text, newldiv3.Text, newldiv4.Text, newldiv5.Text, newldiv6.Text, newldiv7.Text, newldiv8.Text, newlConf1.Text, newlConf2.Text)

            Dim NL_Teams As NewLeague_Teams = New NewLeague_Teams(winMainMenu, Me, nl)
            NL_Teams.setFields()
            NL_Teams.Show()

        Catch ex As Exception
            MessageBox.Show(CommonUtils.substr(ex.Message, 0, 100), "Error", MessageBoxButton.OK, MessageBoxImage.Error)
        End Try


    End Sub


End Class
