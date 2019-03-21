Imports System.Windows.Controls

Public Class NewLeague_Teams

    Property winMainMenu As MainWindow
    Property NewLeague_Settings As NewLeague_Settings
    Property New_League As Leaguemdl = Nothing

    Public Sub New(ByVal winMainMenu As MainWindow, ByVal NewLeague_Settings As NewLeague_Settings,
                   ByVal New_League As Leaguemdl)

        ' This call is required by the designer.
        InitializeComponent()

        Me.winMainMenu = winMainMenu
        Me.NewLeague_Settings = NewLeague_Settings
        Me.New_League = New_League

        Dim Teamlbltyle As Style = Application.Current.FindResource("Teamlbltyle")
        Dim Conflbltyle As Style = Application.Current.FindResource("Conflbltyle")

        Dim t_id = 1
        Dim num_conferences As Integer = CInt(NewLeague_Settings.newlnumconferences.Text)
        Dim num_teams As Integer = CInt(NewLeague_Settings.newlnumteams.Text)
        Dim num_divisions As Integer = CInt(NewLeague_Settings.newlnumdivisions.Text)
        Dim num_divs_per_conf As Integer
        If num_conferences = 0 Then
            num_divs_per_conf = num_divisions
        Else
            num_divs_per_conf = num_divisions \ num_conferences
        End If
        Dim teams_per_division As Integer = CInt(NewLeague_Settings.newlnumteams.Text) \ CInt(NewLeague_Settings.newlnumdivisions.Text)

        If num_conferences = 2 Then
            Dim v_sp1 As StackPanel = New StackPanel()
            v_sp1.Orientation = Orientation.Vertical
            v_sp1.VerticalAlignment = HorizontalAlignment.Center
            v_sp1.HorizontalAlignment = HorizontalAlignment.Center

            Dim conf1_sp As StackPanel = New StackPanel()
            conf1_sp.Orientation = Orientation.Horizontal
            conf1_sp.HorizontalAlignment = HorizontalAlignment.Center

            Dim conf1_label As Label = New Label()
            conf1_label.Name = "newllblConf1"
            conf1_label.Width = 150
            conf1_label.Style = Conflbltyle

            conf1_sp.Children.Add(conf1_label)
            v_sp1.Children.Add(conf1_sp)

            Me.RegisterName(conf1_label.Name, conf1_label)

            For i As Integer = 1 To num_divs_per_conf
                Dim gb_hdr_label As Label = New Label()
                gb_hdr_label.Name = "newldiv" & i.ToString
                gb_hdr_label.Foreground = Brushes.White

                Dim gb_div As GroupBox = New GroupBox()
                gb_div.Margin = New Thickness(1, 1, 1, 1)
                gb_div.FontSize = 14
                gb_div.Header = gb_hdr_label

                Dim v_sp_in_groupbox As StackPanel = New StackPanel()
                v_sp_in_groupbox.Orientation = Orientation.Vertical
                v_sp_in_groupbox.Width = 350

                gb_div.Content = v_sp_in_groupbox

                Me.RegisterName(gb_hdr_label.Name, gb_hdr_label)

                For z As Integer = 1 To teams_per_division
                    Dim sp_team As StackPanel = New StackPanel()
                    sp_team.Orientation = Orientation.Horizontal

                    Dim helmet_img As Image = New Image()
                    helmet_img.Name = "newlimgTeam" & t_id.ToString
                    helmet_img.Width = 20
                    helmet_img.Height = 20

                    Dim team_label As Label = New Label()
                    team_label.Name = "newllblTeam" & t_id.ToString
                    team_label.Padding = New Thickness(10, 0, 0, 0)
                    team_label.Style = Teamlbltyle
                    team_label.AddHandler(Label.MouseDownEvent, New RoutedEventHandler(AddressOf TeamLabel_MouseDown))

                    sp_team.Children.Add(helmet_img)
                    sp_team.Children.Add(team_label)

                    v_sp_in_groupbox.Children.Add(sp_team)

                    Me.RegisterName(helmet_img.Name, helmet_img)
                    Me.RegisterName(team_label.Name, team_label)

                    t_id += 1
                Next
                v_sp1.Children.Add(gb_div)
            Next

            Dim v_sp2 As StackPanel = New StackPanel()
            v_sp2.Orientation = Orientation.Vertical
            v_sp2.HorizontalAlignment = HorizontalAlignment.Center

            Dim conf2_sp As StackPanel = New StackPanel()
            conf2_sp.Orientation = Orientation.Horizontal
            conf2_sp.HorizontalAlignment = HorizontalAlignment.Center

            Dim conf2_label As Label = New Label()
            conf2_label.Name = "newllblConf2"
            conf2_label.Width = 150
            conf2_label.Style = Conflbltyle

            conf2_sp.Children.Add(conf2_label)
            v_sp2.Children.Add(conf2_sp)

            Me.RegisterName(conf2_label.Name, conf2_label)

            For i As Integer = num_divs_per_conf + 1 To num_divisions
                Dim gb_hdr_label As Label = New Label()
                gb_hdr_label.Name = "newldiv" & i.ToString
                gb_hdr_label.Foreground = Brushes.White

                Dim gb_div As GroupBox = New GroupBox()
                gb_div.Margin = New Thickness(1, 1, 1, 1)
                gb_div.FontSize = 14
                gb_div.Header = gb_hdr_label

                Dim v_sp_in_groupbox As StackPanel = New StackPanel()
                v_sp_in_groupbox.Orientation = Orientation.Vertical
                v_sp_in_groupbox.Width = 350

                gb_div.Content = v_sp_in_groupbox

                Me.RegisterName(gb_hdr_label.Name, gb_hdr_label)

                For z As Integer = 1 To teams_per_division
                    Dim sp_team As StackPanel = New StackPanel()
                    sp_team.Orientation = Orientation.Horizontal

                    Dim helmet_img As Image = New Image()
                    helmet_img.Name = "newlimgTeam" & t_id.ToString
                    helmet_img.Width = 20
                    helmet_img.Height = 20

                    Dim team_label As Label = New Label()
                    team_label.Name = "newllblTeam" & t_id.ToString
                    team_label.Padding = New Thickness(10, 0, 0, 0)
                    team_label.Style = Teamlbltyle
                    team_label.AddHandler(Label.MouseDownEvent, New RoutedEventHandler(AddressOf TeamLabel_MouseDown))

                    sp_team.Children.Add(helmet_img)
                    sp_team.Children.Add(team_label)

                    v_sp_in_groupbox.Children.Add(sp_team)

                    Me.RegisterName(helmet_img.Name, helmet_img)
                    Me.RegisterName(team_label.Name, team_label)

                    t_id += 1
                Next
                v_sp2.Children.Add(gb_div)
            Next

            sp1.Children.Add(v_sp1)
            sp1.Children.Add(v_sp2)
        Else 'No conferences
            Dim v_sp As StackPanel = New StackPanel()
            v_sp.Orientation = Orientation.Vertical
            v_sp.HorizontalAlignment = HorizontalAlignment.Center


            For i As Integer = 1 To num_divisions
                Dim gb_hdr_label As Label = New Label()
                gb_hdr_label.Name = "newldiv" & i.ToString
                gb_hdr_label.Foreground = Brushes.White

                Dim gb_div As GroupBox = New GroupBox()
                gb_div.Margin = New Thickness(1, 1, 1, 1)
                gb_div.FontSize = 14
                gb_div.Header = gb_hdr_label

                Dim v_sp_in_groupbox As StackPanel = New StackPanel()
                v_sp_in_groupbox.Orientation = Orientation.Vertical
                v_sp_in_groupbox.Width = 350

                gb_div.Content = v_sp_in_groupbox
                Me.RegisterName(gb_hdr_label.Name, gb_hdr_label)

                For z As Integer = 1 To teams_per_division
                    Dim sp_team As StackPanel = New StackPanel()
                    sp_team.Orientation = Orientation.Horizontal

                    Dim helmet_img As Image = New Image()
                    helmet_img.Name = "newlimgTeam" & t_id.ToString
                    helmet_img.Width = 20
                    helmet_img.Height = 20

                    Dim team_label As Label = New Label()
                    team_label.Name = "newllblTeam" & t_id.ToString
                    team_label.Padding = New Thickness(10, 0, 0, 0)
                    team_label.Style = Teamlbltyle
                    team_label.AddHandler(Label.MouseDownEvent, New RoutedEventHandler(AddressOf TeamLabel_MouseDown))

                    sp_team.Children.Add(helmet_img)
                    sp_team.Children.Add(team_label)

                    v_sp_in_groupbox.Children.Add(sp_team)

                    Me.RegisterName(helmet_img.Name, helmet_img)
                    Me.RegisterName(team_label.Name, team_label)

                    t_id += 1
                Next
                v_sp.Children.Add(gb_div)
                Next

            sp1.Children.Add(v_sp)
        End If

    End Sub
    Public Sub setFields()

        With NewLeague_Settings
            LeageName.Content = .newl1shortname.Text

            'set conferences
            For i As Integer = 1 To CInt(.newlnumconferences.Text)
                Dim confLabel = "newllblConf" & i.ToString
                Dim confLbl As Label = Me.FindName(confLabel)
                confLbl.Content = New_League.Conferences(i - 1)
            Next

            For i As Integer = 1 To CInt(.newlnumdivisions.Text)
                Dim divLabel As String = "newldiv" & i.ToString
                Dim divLbl As Label = Me.FindName(divLabel)
                divLbl.Content = New_League.Divisions(i - 1)
            Next

            For i As Integer = 1 To CInt(.newlnumteams.Text)
                Dim teamLabel = "newllblTeam" & i.ToString
                Dim teamImage = "newlimgTeam" & i.ToString

                Dim teamLbl As Label = Me.FindName(teamLabel)
                Dim teamImg As Image = Me.FindName(teamImage)
                teamLbl.Content = New_League.Teams(i - 1).City
                Dim img_path As String = Nothing
                If Not IsNothing(New_League.Teams(i - 1).Uniform) Then img_path = New_League.Teams(i - 1).Helmet_img_path
                If Not IsNothing(img_path) Then
                    Dim helmetIMG_source As BitmapImage = New BitmapImage(New Uri(img_path))
                    teamImg.Source = helmetIMG_source
                End If
            Next
        End With

    End Sub
    Private Sub TeamLabel_MouseDown(sender As Object, e As RoutedEventArgs)
        Dim l As Label = e.Source
        Dim n As Integer = CommonUtils.ExtractTeamNumber(l.Name)
        '        MessageBox.Show(l.Name & " " & n.ToString)
        Dim NL_Team As NewTeam = New NewTeam(winMainMenu, Me, n - 1, New_League)
        NL_Team.setfields()
        NL_Team.Show()
    End Sub

    Private Sub newl2Cancel_Click(sender As Object, e As RoutedEventArgs) Handles newl2Cancel.Click
        Me.Close()
        NewLeague_Settings.Show()
    End Sub

    Private Sub newl2Submit_Click(sender As Object, e As RoutedEventArgs) Handles newl2Submit.Click

        Try
            validate()
            Mouse.OverrideCursor = Cursors.Wait
            Dim ls As League_Services = New League_Services()
            ls.CreateNewLeague(New_League)
            Mouse.OverrideCursor = Nothing
            NewLeague_Settings.Close()
            Me.Close()
            winMainMenu.Show()
        Catch ex As Exception
            Mouse.OverrideCursor = Nothing
            MessageBox.Show(CommonUtils.substr(ex.Message, 0, 100), "Error", MessageBoxButton.OK, MessageBoxImage.Error)

        End Try

    End Sub

    Private Sub validate()

        For Each t In New_League.Teams
            If t.City = "New Team" Then
                Throw New Exception("Not all teams have been created!")
            End If
        Next

    End Sub

End Class

