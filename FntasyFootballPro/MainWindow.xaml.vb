Imports System.ComponentModel
Imports log4net

Class MainWindow
    Public League As Leaguemdl = Nothing

    Private MainMenuUC As MainMenuUC = Nothing
    Private NewLeagueUC As NewLeagueUC = Nothing
    Private NewTeamUC As NewTeamUC = Nothing
    Private PlayerNamesUC As PlayerNamesUC = Nothing
    Private Stock_teamsUC As StockTeamsUC = Nothing

    Private Shared logger As ILog = LogManager.GetLogger("RollingFile")

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        MainMenuUC = New MainMenuUC()

        sp_uc.Children.Add(MainMenuUC)

        'If run from Visual Studio using the debugger then make the admin menu item visible
        If Not Debugger.IsAttached() Then
            Dim AdminMenuItem As MenuItem
            AdminMenuItem = DirectCast(Main_menu_top.Items(Main_menu_top.Items.Count - 1), MenuItem)
            AdminMenuItem.Visibility = Windows.Visibility.Collapsed
        End If

        AddHandler MainMenuUC.Shutdown_App, AddressOf Me.MainWindow_Closing
        AddHandler MainMenuUC.Show_NewLeague, AddressOf Me.Show_NewLeague


    End Sub

    Private Sub MainWindow_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing

        Dim response = MessageBox.Show("Do you really want to exit?", "Exiting...",
                                        MessageBoxButton.YesNo, MessageBoxImage.Exclamation)
        If (response = MessageBoxResult.No) Then
            e.Cancel = True
        Else
            CloseApplication()
        End If

    End Sub
    Private Sub mmTopExit_Click(sender As Object, e As RoutedEventArgs)
        Me.Close()
    End Sub

    Private Sub CloseApplication()
        Application.Current.Shutdown()
    End Sub
    Private Sub Show_MainMenu(sender As Object, e As EventArgs)

        sp_uc.Children.Clear()
        sp_uc.Children.Add(MainMenuUC)
        NewLeagueUC = Nothing
        NewTeamUC = Nothing
    End Sub
    Private Sub Show_NewLeague(sender As Object, e As EventArgs)

        Try
            Mouse.OverrideCursor = Cursors.Wait
            logger.Info("Entering Create new league")
            Dim sts As StockTeams_Services = New StockTeams_Services()
            Dim st_list As List(Of TeamMdl) = sts.getAllStockTeams
            NewLeagueUC = New NewLeagueUC(Me, st_list)

            AddHandler NewLeagueUC.Show_MainMenu, AddressOf Me.Show_MainMenu
            AddHandler NewLeagueUC.Show_NewTeam, AddressOf Me.Show_NewTeamDetail

            sp_uc.Children.Clear()
            sp_uc.Children.Add(NewLeagueUC)
            Mouse.OverrideCursor = Nothing
        Catch ex As Exception
            Mouse.OverrideCursor = Nothing
            MessageBox.Show(CommonUtils.substr(ex.Message, 0, 100), "Error", MessageBoxButton.OK, MessageBoxImage.Error)
        End Try

    End Sub
    Private Sub Back_NewLeague(sender As Object, e As TeamUpdatedEventArgs)

        'Only if the team is updated, update the team labels
        If e.team_upd = True Then
            NewLeagueUC.setTeamsLabels()
        End If

        sp_uc.Children.Clear()
        sp_uc.Children.Add(NewLeagueUC)

    End Sub
    Private Sub Show_NewTeamDetail(sender As Object, e As teamEventArgs)

        Mouse.OverrideCursor = Cursors.Wait

        Dim team_ind As Integer = e.team_num - 1
        NewTeamUC = New NewTeamUC(League.Teams(team_ind), "New_League")
        NewTeamUC.setBaseUniform()
        NewTeamUC.setfields()
        AddHandler NewTeamUC.backtoNewLeague, AddressOf Me.Back_NewLeague

        sp_uc.Children.Clear()
        sp_uc.Children.Add(NewTeamUC)

        Mouse.OverrideCursor = Nothing

    End Sub
    Private Sub Show_PlayerNames(sender As Object, e As EventArgs)

        PlayerNamesUC = New PlayerNamesUC()
        PlayerNamesUC.clearpage()
        AddHandler PlayerNamesUC.Show_MainMenu, AddressOf Me.Show_MainMenu

        sp_uc.Children.Clear()
        sp_uc.Children.Add(PlayerNamesUC)

    End Sub
    Private Sub Show_StockTeams(sender As Object, e As EventArgs)

        Try
            Mouse.OverrideCursor = Cursors.Wait
            League = Nothing
            Dim sts As StockTeams_Services = New StockTeams_Services()
            Dim st_list As List(Of TeamMdl) = sts.getAllStockTeams
            Stock_teamsUC = New StockTeamsUC(st_list)
            AddHandler Stock_teamsUC.Show_MainMenu, AddressOf Me.Show_MainMenu
            AddHandler Stock_teamsUC.Show_NewStockTeam, AddressOf Me.Show_NewStockTeam
            AddHandler Stock_teamsUC.Show_UpdateStockTeam, AddressOf Me.Show_UpdateStockTeam
            sp_uc.Children.Clear()
            sp_uc.Children.Add(Stock_teamsUC)
            Mouse.OverrideCursor = Nothing
        Catch ex As Exception
            Mouse.OverrideCursor = Nothing
            MessageBox.Show(CommonUtils.substr(ex.Message, 0, 100), "Error", MessageBoxButton.OK, MessageBoxImage.Error)
        End Try

    End Sub
    Private Sub Show_NewStockTeam(sender As Object, e As EventArgs)

        Dim stock_team As TeamMdl = New TeamMdl(0, "")
        NewTeamUC = New NewTeamUC(stock_team, "New_Stock_Team")
        NewTeamUC.setBaseUniform()
        NewTeamUC.setfields()
        AddHandler NewTeamUC.backtoStockTeams, AddressOf Me.Show_StockTeams

        sp_uc.Children.Clear()
        sp_uc.Children.Add(NewTeamUC)

    End Sub
    Private Sub Show_UpdateStockTeam(sender As Object, e As StockteamEventArgs)

        Try
            Mouse.OverrideCursor = Cursors.Wait
            Dim stock_team As TeamMdl = e.team
            NewTeamUC = New NewTeamUC(stock_team, "Update_Stock_Team")
            NewTeamUC.setBaseUniform()
            NewTeamUC.setfields()
            AddHandler NewTeamUC.backtoStockTeams, AddressOf Me.Show_StockTeams

            sp_uc.Children.Clear()
            sp_uc.Children.Add(NewTeamUC)
            Mouse.OverrideCursor = Nothing
        Catch ex As Exception
            Mouse.OverrideCursor = Nothing
            MessageBox.Show(CommonUtils.substr(ex.Message, 0, 100), "Error", MessageBoxButton.OK, MessageBoxImage.Error)
        End Try


    End Sub

End Class
