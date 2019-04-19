Imports System.ComponentModel

Class MainWindow
    Public League As Leaguemdl = Nothing

    Private MainMenuUC As MainMenuUC = Nothing
    Private NewLeagueUC As NewLeagueUC = Nothing
    Private NewTeamUC As NewTeamUC = Nothing

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


End Class
