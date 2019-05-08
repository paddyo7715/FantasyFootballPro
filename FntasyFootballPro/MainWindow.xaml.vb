Imports System.ComponentModel

Class MainWindow
    Public League As Leaguemdl = Nothing

    Private MainMenuUC As MainMenuUC = Nothing
    Private NewLeagueUC As NewLeagueUC = Nothing
    Private NewTeamUC As NewTeamUC = Nothing
    Private PlayerNamesUC As PlayerNamesUC = Nothing
    Private Stock_teams As StockTeamsUC = Nothing

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

        NewLeagueUC = New NewLeagueUC(Me)

        AddHandler NewLeagueUC.Show_MainMenu, AddressOf Me.Show_MainMenu
        AddHandler NewLeagueUC.Show_NewTeam, AddressOf Me.Show_NewTeamDetail

        sp_uc.Children.Clear()
        sp_uc.Children.Add(NewLeagueUC)

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

        Dim team_ind As Integer = e.team_num - 1
        NewTeamUC = New NewTeamUC(League.Teams(team_ind), "New_League")
        NewTeamUC.setTeamDetail()
        NewTeamUC.setfields()
        AddHandler NewTeamUC.backtoNewLeague, AddressOf Me.Back_NewLeague

        sp_uc.Children.Clear()
        sp_uc.Children.Add(NewTeamUC)

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
            Dim ts As Team_Services = New Team_Services()
            Dim st_list As List(Of TeamMdl) = ts.getAllStockTeams
            Stock_teams = New StockTeamsUC(st_list)
            AddHandler Stock_teams.Show_MainMenu, AddressOf Me.Show_MainMenu
            AddHandler Stock_teams.Show_NewStockTeam, AddressOf Me.Show_NewStockTeam
            sp_uc.Children.Clear()
            sp_uc.Children.Add(Stock_teams)
            Mouse.OverrideCursor = Nothing
        Catch ex As Exception
            Mouse.OverrideCursor = Nothing
            MessageBox.Show(CommonUtils.substr(ex.Message, 0, 100), "Error", MessageBoxButton.OK, MessageBoxImage.Error)
        End Try

    End Sub
    Private Sub Show_NewStockTeam(sender As Object, e As EventArgs)

        Dim stock_team As TeamMdl = New TeamMdl(0, "")
        NewTeamUC = New NewTeamUC(stock_team, "New_Stock_Team")
        NewTeamUC.setTeamDetail()
        NewTeamUC.setfields()
        AddHandler NewTeamUC.backtoStockTeams, AddressOf Me.Show_StockTeams

        sp_uc.Children.Clear()
        sp_uc.Children.Add(NewTeamUC)

    End Sub

End Class
