Imports System.ComponentModel
Imports System.Data.SQLite
Imports System.IO
Imports log4net

Public Class League_Services
    Private Shared logger As ILog = LogManager.GetLogger("RollingFile")

    Public Sub CreateNewLeague(ByVal nl As Leaguemdl, ByVal bw As BackgroundWorker)

        logger.Info("CreateNewLeague Started")

        Dim ts As New Team_Services()

        'Create the league folder
        Dim DIRPath As String = System.IO.Path.Combine(My.Computer.FileSystem.SpecialDirectories.MyDocuments, App_Constants.GAME_DOC_FOLDER)
        Dim DIRPath_League As String = My.Computer.FileSystem.SpecialDirectories.MyDocuments & Path.DirectorySeparatorChar & App_Constants.GAME_DOC_FOLDER & Path.DirectorySeparatorChar & nl.Short_Name
        Dim New_League_File As String = nl.Short_Name & "." & App_Constants.DB_FILE_EXT
        Dim League_con_string As String = My.Computer.FileSystem.SpecialDirectories.MyDocuments & Path.DirectorySeparatorChar & App_Constants.GAME_DOC_FOLDER & Path.DirectorySeparatorChar & nl.Short_Name & Path.DirectorySeparatorChar & nl.Short_Name & Path.DirectorySeparatorChar & App_Constants.DB_FILE_EXT
        Dim LeagueDAO = New LeagueDAO(nl)
        Dim process_state As String = "Processing..."
        Dim state_struct As String = Nothing
        Dim i As Integer

        Try
            'Update the progress bar
            i = 2
            process_state = "Creating League Folder Strucuture 1 of 4"
            state_struct = "Processing..." & "|" & process_state & "|" & ""
            bw.ReportProgress(i, state_struct)

            'Create the League Folder
            logger.Info("Creating league folder")
            My.Computer.FileSystem.CreateDirectory(My.Computer.FileSystem.SpecialDirectories.MyDocuments & Path.DirectorySeparatorChar & App_Constants.GAME_DOC_FOLDER & Path.DirectorySeparatorChar & nl.Short_Name)

            'Create the helmet image League Folder
            logger.Info("Creating league helmet folder")
            My.Computer.FileSystem.CreateDirectory(My.Computer.FileSystem.SpecialDirectories.MyDocuments & Path.DirectorySeparatorChar & App_Constants.GAME_DOC_FOLDER & Path.DirectorySeparatorChar & nl.Short_Name & Path.DirectorySeparatorChar & App_Constants.LEAGUE_HELMETS_SUBFOLDER)

            'Create the stadium image League folder
            logger.Info("Creating league image folder")
            My.Computer.FileSystem.CreateDirectory(My.Computer.FileSystem.SpecialDirectories.MyDocuments & Path.DirectorySeparatorChar & App_Constants.GAME_DOC_FOLDER & Path.DirectorySeparatorChar & nl.Short_Name & Path.DirectorySeparatorChar & App_Constants.LEAGUE_STADIUM_SUBFOLDER)

            'Copy and Create the league database file
            logger.Info("Starting league database creation and copy")
            My.Computer.FileSystem.CopyFile(CommonUtils.getAppPath & Path.DirectorySeparatorChar & App_Constants.BLANK_DB_FOLDER & Path.DirectorySeparatorChar & App_Constants.BLANK_DB, DIRPath_League & Path.DirectorySeparatorChar & New_League_File)

            'Copy team image files to league folder
            logger.Info("Helmet and stadium files copy starting")
            For Each t In nl.Teams
                logger.Debug("Copying " & t.Helmet_img_path)
                My.Computer.FileSystem.CopyFile(t.Helmet_img_path, DIRPath_League & Path.DirectorySeparatorChar & App_Constants.LEAGUE_HELMETS_SUBFOLDER & Path.DirectorySeparatorChar & Path.GetFileName(t.Helmet_img_path))
                logger.Debug("Copying " & t.Stadium.Stadium_Img_Path)
                My.Computer.FileSystem.CopyFile(t.Stadium.Stadium_Img_Path, DIRPath_League & Path.DirectorySeparatorChar & App_Constants.LEAGUE_STADIUM_SUBFOLDER & Path.DirectorySeparatorChar & Path.GetFileName(t.Stadium.Stadium_Img_Path))
            Next

            'Update the progress bar
            i = 25
            process_state = "Create Players 2 of 4"
            state_struct = "Processing..." & "|" & process_state & "|" & ""
            bw.ReportProgress(i, state_struct)

            'Create players for each team
            logger.Info("Creating players for new league.")
            For Each t In nl.Teams
                logger.Debug("Creating players for team " & t.Nickname)
                Dim Roster As List(Of PlayerMdl) = ts.Roll_Players("")
                Roster = Roster.OrderBy(Function(x) x.Pos).
                ThenByDescending(Function(x) x.Ratings.OverAll).ToList
                t.setPlayers(Roster)
            Next

            'Update the progress bar
            i = 50
            process_state = "Creating Schedule 3 of 4"
            state_struct = "Processing..." & "|" & process_state & "|" & ""
            bw.ReportProgress(i, state_struct)

            'Creating schedule
            logger.Info("Creating schedule")
            nl.setSchedule(create_schedule(nl.Short_Name, nl.Num_Teams, nl.Divisions.Count, nl.Conferences.Count, nl.Number_of_Games, nl.Number_of_weeks))

            'Update the progress bar
            i = 75
            process_state = "Saving League to Database 4 of 4"
            state_struct = "Processing..." & "|" & process_state & "|" & ""
            bw.ReportProgress(i, state_struct)

            'Write the league records to the database
            logger.Info("Saving new league to database")
            LeagueDAO.Create_New_League(League_con_string)

            'Update the progress bar
            i = 100
            process_state = ""
            state_struct = "Complete" & "|" & process_state & "|" & "League Completed Successfully!"
            bw.ReportProgress(i, state_struct)

        Catch ex As Exception
            state_struct = "Error" & "|" & process_state & "|" & "Failed to Create New League"
            bw.ReportProgress(i, state_struct)

            My.Computer.FileSystem.DeleteDirectory(DIRPath_League, FileIO.DeleteDirectoryOption.DeleteAllContents)

            logger.Error("Create league service failed")
            logger.Error(ex)
        End Try

    End Sub
    Public Function create_schedule(ByVal Short_Name As String, ByVal Num_Teams As Integer, ByVal Num_Divisions As Integer,
                                    ByVal Num_Conferences As Integer, ByVal Number_of_Games As Integer, ByVal Number_of_weeks As Integer) As List(Of String)


        Dim ls As New Schedule(Short_Name, Num_Teams, Num_Teams \ Num_Divisions, Num_Conferences, Number_of_Games, Number_of_weeks - Number_of_Games)

            Dim s As List(Of String) = Nothing

        'allow up to 5 tries if the schedule validation fails.
        For i As Integer = 0 To 6
            s = ls.Generate_Regular_Schedule()
            logger.Debug("Possible League Schedule Created")
            Dim val_sched As New Validate_Sched(Short_Name, Num_Teams, Num_Teams \ Num_Divisions, Num_Conferences, Number_of_Games, Number_of_weeks - Number_of_Games)
            Dim r As String = val_sched.Validate(s)
            If Not IsNothing(r) Then
                logger.Debug("Schedule validated")
                Exit For
            End If
            If i >= 5 Then Throw New Exception("More than 6 tries to validate schedule failed.")
        Next i

        Return s

    End Function

End Class
