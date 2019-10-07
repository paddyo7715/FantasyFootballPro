Imports System.IO
Imports System.Management.dll
Imports log4net

Class Application

    Private Shared logger As ILog = LogManager.GetLogger("RollingFile")

    Public Sub Application_Startup(ByVal sender As Object, ByVal e As StartupEventArgs)
        Dim DIRPath As String = System.IO.Path.Combine(My.Computer.FileSystem.SpecialDirectories.MyDocuments, App_Constants.GAME_DOC_FOLDER)
        Dim Log_folder As String = App_Constants.LOG_FOLDER
        Dim Drive_letter As String = DIRPath.Split(":")(0)

        If AlreadyRunning() Then
            logger.Error("Application already running.  Can not run more than one instance.")
            MessageBox.Show("Application already running.  Can not run more than one instance.", "Error", MessageBoxButton.OK, MessageBoxImage.Error)
            End
        End If

        'Check that the disk drive that holds the team databases and loggs has enough disk space
        Dim free_disk_space As Long = (Convert.ToInt64(My.Computer.FileSystem.GetDriveInfo(Drive_letter & ":").AvailableFreeSpace.ToString) / 1024) / 1024
        If free_disk_space < App_Constants.MIN_FREE_DISK_SPACE Then
            logger.Error("Only " & free_disk_space.ToString & " free disk space available.  Ending program")
            MessageBox.Show("Insufficient free space available.  Only " & free_disk_space.ToString & " free disk space available.  Ending program")
            End
        End If

        'If it doesn't exist then create the Game folder/log folder under my documents.  This
        'should only be necessary the first time that the game is run
        If Not Directory.Exists(DIRPath) Then
            Directory.CreateDirectory(DIRPath)
            Directory.CreateDirectory(DIRPath & Path.DirectorySeparatorChar & Log_folder)
        End If

        'Set the folder for the logs
        log4net.GlobalContext.Properties("logFolder") = DIRPath & Path.DirectorySeparatorChar & Log_folder
        log4net.Config.XmlConfigurator.Configure(New System.IO.FileInfo("Log4Net.Config.xml"))

        logger.Info("=================== Start Program ========================")
        logger.Info("Hostname: " & Environment.MachineName)
        logger.Info("Operating System: " & My.Computer.Info.OSFullName & "  Version:  " & My.Computer.Info.OSVersion & "  Platform:  " & My.Computer.Info.OSPlatform)
        logger.Info(".NET version: " & System.Environment.Version.ToString)

        Dim win As MainWindow = New MainWindow()
        MainWindow = win
        win.Show()

        'e.Args.Count
        '       Shutdown()

    End Sub

    Public Sub Application_Exit(ByVal sender As Object, ByVal e As ExitEventArgs)
        logger.Info("=================== End Program =========================")
    End Sub

    ' Return True if another instance
    ' of this program is already running.
    Private Function AlreadyRunning() As Boolean
        ' Get our process name.
        Dim my_proc As Process = Process.GetCurrentProcess
        Dim my_name As String = my_proc.ProcessName

        ' Get information about processes with this name.
        Dim procs() As Process =
            Process.GetProcessesByName(my_name)

        ' If there is only one, it's us.
        If procs.Length = 1 Then Return False

        ' If there is more than one process,
        ' see if one has a StartTime before ours.
        Dim i As Integer
        For i = 0 To procs.Length - 1
            If procs(i).StartTime < my_proc.StartTime Then _
                Return True
        Next i

        ' If we get here, we were first.
        Return False
    End Function


End Class
