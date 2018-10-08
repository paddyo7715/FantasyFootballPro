Imports System.Data.SQLite
Imports System.IO

Public Class League_Services
    Public Sub CreateNewLeague(ByVal nl As New_League)

        'Check if Fantasy Football Pro Directory exists under My Documents.  If not then create it.
        Dim DIRPath As String = System.IO.Path.Combine(My.Computer.FileSystem.SpecialDirectories.MyDocuments, App_Constants.LEAGUE_DB_FOLDER)
        Dim DIRPath_League As String = My.Computer.FileSystem.SpecialDirectories.MyDocuments & "\" & App_Constants.LEAGUE_DB_FOLDER & "\" & nl.Short_Name & "\" & App_Constants.LEAGUE_HELMETS_SUBFOLDER
        Dim DIRPath_League_Helmets As String = My.Computer.FileSystem.SpecialDirectories.MyDocuments & "\" & App_Constants.LEAGUE_DB_FOLDER & "\" & nl.Short_Name & "\" & App_Constants.LEAGUE_HELMETS_SUBFOLDER
        Dim New_League_File As String = nl.Short_Name & "." & App_Constants.DB_FILE_EXT
        Dim League_con_string As String = My.Computer.FileSystem.SpecialDirectories.MyDocuments & "\" & App_Constants.LEAGUE_DB_FOLDER & "\" & nl.Short_Name & "\" & nl.Short_Name & "\" & App_Constants.DB_FILE_EXT
        Dim LeagueDAO = New LeagueDAO(nl)

        Try
            If Not Directory.Exists(DIRPath) Then
                Directory.CreateDirectory(DIRPath)
            End If

            'Create the League Folder
            My.Computer.FileSystem.CreateDirectory(My.Computer.FileSystem.SpecialDirectories.MyDocuments & "\" & App_Constants.LEAGUE_DB_FOLDER & "\" & nl.Short_Name)

            'Create the helmet image League Folder
            My.Computer.FileSystem.CreateDirectory(My.Computer.FileSystem.SpecialDirectories.MyDocuments & "\" & App_Constants.LEAGUE_DB_FOLDER & "\" & nl.Short_Name & "\" & App_Constants.LEAGUE_HELMETS_SUBFOLDER)

            'Copy and Create the league database file
            My.Computer.FileSystem.CopyFile(App_Constants.BLANK_DB_FOLDER & "\" & App_Constants.BLANK_DB, DIRPath_League & "\" & New_League_File)

            'Write the league records to the database
            LeagueDAO.Create_New_League(League_con_string)

        Catch ex As Exception
            My.Computer.FileSystem.DeleteDirectory(DIRPath_League & "/" & nl.Short_Name, FileIO.DeleteDirectoryOption.DeleteAllContents)
            Throw New Exception("Error creating file error:" & ex.Message)
        End Try

    End Sub

End Class
