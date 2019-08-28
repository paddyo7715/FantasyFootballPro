Imports System.Data.SQLite
Imports System.IO

Public Class League_Services
    Public Sub CreateNewLeague(ByVal nl As Leaguemdl)
        Dim ts As New Team_Services()

        'Check if Fantasy Football Pro Directory exists under My Documents.  If not then create it.
        Dim DIRPath As String = System.IO.Path.Combine(My.Computer.FileSystem.SpecialDirectories.MyDocuments, App_Constants.LEAGUE_DB_FOLDER)
        Dim DIRPath_League As String = My.Computer.FileSystem.SpecialDirectories.MyDocuments & "\" & App_Constants.LEAGUE_DB_FOLDER & "\" & nl.Short_Name
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

            'Create the stadium image League folder
            My.Computer.FileSystem.CreateDirectory(My.Computer.FileSystem.SpecialDirectories.MyDocuments & "\" & App_Constants.LEAGUE_DB_FOLDER & "\" & nl.Short_Name & "\" & App_Constants.LEAGUE_STADIUM_SUBFOLDER)

            'Copy and Create the league database file
            My.Computer.FileSystem.CopyFile(App_Constants.BLANK_DB_FOLDER & "\" & App_Constants.BLANK_DB, DIRPath_League & "\" & New_League_File)

            'Copy the league image files to the league folder
            My.Computer.FileSystem.CopyFile(nl.Trophy_filepath, DIRPath_League & "\" & Path.GetFileName(nl.Trophy_filepath))

            'Copy team image files to league folder
            For Each t In nl.Teams
                My.Computer.FileSystem.CopyFile(t.Helmet_img_path, DIRPath_League & "\" & Path.GetFileName(t.Helmet_img_path))
                My.Computer.FileSystem.CopyFile(t.Stadium.Stadium_Img_Path, DIRPath_League & "\" & Path.GetFileName(t.Stadium.Stadium_Img_Path))
            Next

            'Create players for each team
            For Each t In nl.Teams
                Dim Roster As List(Of PlayerMdl) = ts.Roll_Players("")
                Roster = Roster.OrderBy(Function(x) x.Pos).
                ThenByDescending(Function(x) x.Ratings.OverAll).ToList
                t.setPlayers(Roster)
            Next

            'Creating schedule
            nl.setSchedule(create_schedule(nl.Short_Name, nl.Num_Teams, nl.Divisions.Count, nl.Conferences.Count, nl.Number_of_Games, nl.Number_of_weeks))

            'Write the league records to the database
            LeagueDAO.Create_New_League(League_con_string)

        Catch ex As Exception
            My.Computer.FileSystem.DeleteDirectory(DIRPath_League & "/" & nl.Short_Name, FileIO.DeleteDirectoryOption.DeleteAllContents)
            Throw New Exception("Error creating file error:" & ex.Message)
        End Try

    End Sub
    Public Function create_schedule(ByVal Short_Name As String, ByVal Num_Teams As Integer, ByVal Num_Divisions As Integer,
                                    ByVal Num_Conferences As Integer, ByVal Number_of_Games As Integer, ByVal Number_of_weeks As Integer) As List(Of String)

        Try
            Dim ls As New Schedule(Short_Name, Num_Teams, Num_Teams \ Num_Divisions, Num_Conferences, Number_of_Games, Number_of_weeks - Number_of_Games)

            Dim s As List(Of String) = Nothing
            s = ls.Generate_Regular_Schedule()
            Dim val_sched As New Validate_Sched(Short_Name, Num_Teams, Num_Teams \ Num_Divisions, Num_Conferences, Number_of_Games, Number_of_weeks - Number_of_Games)

            Return s
        Catch e As Exception
            Throw New Exception("Error attempting to create schedule")
        End Try


    End Function

End Class
