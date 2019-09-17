Imports System.IO
Imports log4net

Class Application

    Private Shared logger As ILog = LogManager.GetLogger("RollingFile")

    Public Sub Application_Startup(ByVal sender As Object, ByVal e As StartupEventArgs)
        Dim DIRPath As String = System.IO.Path.Combine(My.Computer.FileSystem.SpecialDirectories.MyDocuments, App_Constants.GAME_DOC_FOLDER)
        Dim Log_folder As String = App_Constants.LOG_FOLDER

        'If it doesn't exist then create the Game folder/log folder under my documents.  This
        'should only be necessary the first time that the game is run
        If Not Directory.Exists(DIRPath) Then
            Directory.CreateDirectory(DIRPath)
            Directory.CreateDirectory(DIRPath & "\" & Log_folder)
        End If

        'Set the folder for the logs
        log4net.GlobalContext.Properties("logFolder") = DIRPath & "\" & Log_folder
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


End Class
