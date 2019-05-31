Imports System.IO
Imports FntasyFootballPro.Leaguemdl
Imports Microsoft.Win32

Public Class NewLeagueUC
    'pw is the parent window mainwindow
    Private pw As MainWindow

    Public Event Show_MainMenu As EventHandler
    Public Event Show_NewTeam As EventHandler

    Public Sub New(ByVal pw As MainWindow)

        ' This call is required by the designer.
        InitializeComponent()

        Me.pw = pw
        Me.pw.League = New Leaguemdl()

        Dim icurrentyear As Integer = Date.Today.Year

        For i As Integer = icurrentyear - 100 To icurrentyear + 100
            newl1StartingYear.Items.Add(i.ToString)
        Next

        newl1StartingYear.Text = icurrentyear.ToString
        newl1Structure.SelectedIndex = 0

    End Sub

    Private Sub validate()

        Dim DIRPath As String
        DIRPath = System.IO.Path.Combine(My.Computer.FileSystem.SpecialDirectories.MyDocuments, App_Constants.LEAGUE_DB_FOLDER & "/" & newl1shortname.Text)

        If CommonUtils.isBlank(newl1shortname.Text) Then Throw New Exception("League Short Name must be supplied!")
        If CommonUtils.isBlank(newl1longname.Text) Then Throw New Exception("League Long Name must be supplied!")
        If CommonUtils.isBlank(newl1championshipgame.Text) Then Throw New Exception("Championship Game must be supplied!")
        If CommonUtils.isBlank(newl1TrophyPath.Text) Then Throw New Exception("League trophy image must be supplied!")

        If CommonUtils.isBlank(newlnumweeks.Text) OrElse Not IsNumeric(newlnumweeks.Text) Then Throw New Exception("Invalid Value for Number of Weeks!")
        If CommonUtils.isBlank(newlnumgames.Text) OrElse Not IsNumeric(newlnumgames.Text) Then Throw New Exception("Invalid Value for Number of Games!")
        If CommonUtils.isBlank(newlnumdivisions.Text) OrElse Not IsNumeric(newlnumdivisions.Text) Then Throw New Exception("Invalid Value for Number of Divisions!")
        If CommonUtils.isBlank(newlnumteams.Text) OrElse Not IsNumeric(newlnumteams.Text) Then Throw New Exception("Invalid Value for Number of Teams!")

        If CommonUtils.isBlank(newlnumplayoffteams.Text) OrElse Not IsNumeric(newlnumplayoffteams.Text) Then Throw New Exception("Invalid Value for Number of Playoff Teams")

        For i As Integer = 1 To CInt(newlnumconferences.Text)
            Dim conftxtname = "newlConf" & i.ToString
            Dim conftxtbox As TextBox = Me.FindName(conftxtname)

            If IsNothing(conftxtname) OrElse conftxtbox.Text = "" Then
                Throw New Exception("Conference name " & i.ToString & " must be supplied!")
            End If
        Next

        For i As Integer = 1 To CInt(newlnumdivisions.Text)
            Dim divtxtname = "newldiv" & i.ToString
            Dim divtxtbox As TextBox = Me.FindName(divtxtname)

            If IsNothing(divtxtbox) OrElse divtxtbox.Text.Trim.Length = 0 Then
                Throw New Exception("A name for division " & i.ToString & " must be supplied!")
            End If
        Next

        For i As Integer = 1 To CInt(newlnumdivisions.Text)
            Dim teamlabel = "newllblTeam" & i.ToString
            Dim tlabel As Label = Me.FindName(teamlabel)
            If tlabel.Content = App_Constants.EMPTY_TEAM_SLOT Then
                Throw New Exception("All Empty Team Slots must be filled with a team!")
            End If
        Next

        If Directory.Exists(DIRPath) Then
            Throw New Exception("League " & newl1shortname.Text & " already exists!")
        End If


    End Sub

    Private Sub newl1Structure_SelectionChanged(sender As Object, e As RoutedEventArgs) Handles newl1Structure.SelectionChanged

        Dim cbitem As ComboBoxItem = CType(newl1Structure.SelectedItem, ComboBoxItem)
        Dim v As Integer() = CommonUtils.getLeagueStructure(cbitem.Content)
        Dim num_weeks As Integer
        Dim num_games As Integer
        Dim num_divs As Integer
        Dim num_teams As Integer
        Dim num_confs As Integer
        Dim num_playoff_teams As Integer
        Dim last_div_first_group As Integer
        Dim teams_per_division As Integer
        Dim j As Integer
        Dim Largelblstyle As Style = Application.Current.FindResource("Largelbltyle")
        Dim GroupBoxstyle As Style = Application.Current.FindResource("GroupBoxstyle")
        Dim Largetxttyle As Style = Application.Current.FindResource("Largetxttyle")
        Dim Conflbltyle As Style = Application.Current.FindResource("Conflbltyle")

        num_weeks = v(0)
        num_games = v(1)
        num_divs = v(2)
        num_teams = v(3)
        num_confs = v(4)
        num_playoff_teams = v(5)
        teams_per_division = num_teams \ num_divs

        newlnumweeks.Text = num_weeks.ToString
        newlnumgames.Text = num_games.ToString
        newlnumdivisions.Text = num_divs.ToString
        newlnumteams.Text = num_teams.ToString
        newlnumconferences.Text = num_confs.ToString
        newlnumplayoffteams.Text = num_playoff_teams.ToString

        'Clear previous division selections
        spDivisions.Children.Clear()
        sp1.Children.Clear()
        Me.unregisterControl("newlConf1")
        Me.unregisterControl("newlConf2")
        Me.unregisterControl("newllblConf1")
        Me.unregisterControl("newllblConf2")

        For I As Integer = 1 To CInt(num_teams)
            Me.unregisterControl("newldiv" & I.ToString)
            Me.unregisterControl("newldiv_team" & I.ToString)
            Me.unregisterControl("newlimgTeam" & I.ToString)
            Me.unregisterControl("newllblTeam" & I.ToString)
        Next

        If num_confs = 2 Then
            Dim v_sp1 As StackPanel = New StackPanel()
            v_sp1.Orientation = Orientation.Vertical
            v_sp1.VerticalAlignment = VerticalAlignment.Top
            v_sp1.HorizontalAlignment = HorizontalAlignment.Center

            Dim conf_panel_1_sq As StackPanel = New StackPanel()
            conf_panel_1_sq.Name = "conference_panel_1"
            conf_panel_1_sq.Orientation = Orientation.Horizontal

            Dim conf_1_label As Label = New Label()
            conf_1_label.Content = "Conference 1:"
            conf_1_label.Style = Largelblstyle

            Dim txtConf1 As New TextBox()
            txtConf1.Name = "newlConf1"
            txtConf1.Width = 150
            txtConf1.Style = Largetxttyle
            txtConf1.MaxLength = 60
            txtConf1.AddHandler(TextBox.LostFocusEvent, New RoutedEventHandler(AddressOf confTextbox_LostFocus))

            conf_panel_1_sq.Children.Add(conf_1_label)
            conf_panel_1_sq.Children.Add(txtConf1)

            v_sp1.Children.Add(conf_panel_1_sq)

            Dim gb_conf1 As GroupBox = New GroupBox()
            gb_conf1.Name = "gb_conf1"
            gb_conf1.Margin = New Thickness(10, 10, 10, 10)
            gb_conf1.FontSize = 18
            gb_conf1.Header = "Divisions:"
            gb_conf1.Style = GroupBoxstyle

            v_sp1.Children.Add(gb_conf1)

            'register the dynamically added control so that it can be looked up later.
            Me.RegisterName(txtConf1.Name, txtConf1)

            Dim v_sp2 As StackPanel = New StackPanel()
            v_sp2.Orientation = Orientation.Vertical
            v_sp2.VerticalAlignment = VerticalAlignment.Top
            v_sp2.HorizontalAlignment = HorizontalAlignment.Center

            Dim conf_panel_2_sq As StackPanel = New StackPanel()
            conf_panel_2_sq.Name = "conference_panel_2"
            conf_panel_2_sq.Orientation = Orientation.Horizontal

            Dim conf_2_label As Label = New Label()
            conf_2_label.Content = "Conference 2:"
            conf_2_label.Style = Largelblstyle

            Dim txtConf2 As New TextBox()
            txtConf2.Name = "newlConf2"
            txtConf2.Width = 150
            txtConf2.Style = Largetxttyle
            txtConf2.MaxLength = 60
            txtConf2.AddHandler(TextBox.LostFocusEvent, New RoutedEventHandler(AddressOf confTextbox_LostFocus))

            conf_panel_2_sq.Children.Add(conf_2_label)
            conf_panel_2_sq.Children.Add(txtConf2)

            v_sp2.Children.Add(conf_panel_2_sq)

            Dim gb_conf2 As GroupBox = New GroupBox()
            gb_conf2.Name = "gb_conf2"
            gb_conf2.Margin = New Thickness(10, 10, 10, 10)
            gb_conf2.FontSize = 18
            gb_conf2.Header = "Divisions:"
            gb_conf2.Style = GroupBoxstyle

            v_sp2.Children.Add(gb_conf2)

            'register the dynamically added control so that it can be looked up later.
            Me.RegisterName(txtConf2.Name, txtConf2)

            Dim st_v_gb1 As StackPanel = New StackPanel()
            st_v_gb1.Orientation = Orientation.Vertical
            st_v_gb1.HorizontalAlignment = HorizontalAlignment.Center
            st_v_gb1.Margin = New Thickness(5, 5, 10, 10)

            Dim st_v_gb2 As StackPanel = New StackPanel()
            st_v_gb2.Orientation = Orientation.Vertical
            st_v_gb2.HorizontalAlignment = HorizontalAlignment.Center
            st_v_gb2.Margin = New Thickness(5, 5, 10, 10)

            last_div_first_group = num_divs \ 2

            'set the labels font text colors ext.
            For i As Integer = 1 To last_div_first_group
                j = i + last_div_first_group
                Dim sp1 As StackPanel = New StackPanel()
                sp1.Orientation = Orientation.Horizontal
                sp1.Margin = New Thickness(0, 0, 0, 2)
                sp1.Name = "div1_staack"

                Dim div_1_label As Label = New Label()
                div_1_label.Content = "Division " & i.ToString
                div_1_label.Style = Largelblstyle

                Dim txtDivision1 As New TextBox()
                txtDivision1.Name = "newldiv" & i.ToString
                txtDivision1.Width = 150
                txtDivision1.Style = Largetxttyle
                txtDivision1.AddHandler(TextBox.LostFocusEvent, New RoutedEventHandler(AddressOf divTextbox_LostFocus))

                'register the dynamically added control so that it can be looked up later.
                Me.RegisterName(txtDivision1.Name, txtDivision1)

                sp1.Children.Add(div_1_label)
                sp1.Children.Add(txtDivision1)

                st_v_gb1.Children.Add(sp1)

                Dim sp2 As StackPanel = New StackPanel()
                sp2.Orientation = Orientation.Horizontal
                sp2.Margin = New Thickness(0, 0, 0, 2)
                sp1.Name = "div2_staack"

                Dim div_2_label As Label = New Label()
                div_2_label.Content = "Division " & j.ToString
                div_2_label.Style = Largelblstyle

                Dim txtDivision2 As New TextBox()
                txtDivision2.Name = "newldiv" & j.ToString
                txtDivision2.Width = 150
                txtDivision2.Style = Largetxttyle
                txtDivision2.AddHandler(TextBox.LostFocusEvent, New RoutedEventHandler(AddressOf divTextbox_LostFocus))

                sp2.Children.Add(div_2_label)
                sp2.Children.Add(txtDivision2)

                st_v_gb2.Children.Add(sp2)

                'register the dynamically added control so that it can be looked up later.
                Me.RegisterName(txtDivision2.Name, txtDivision2)

            Next
            gb_conf1.Content = st_v_gb1
            gb_conf2.Content = st_v_gb2

            spDivisions.Children.Add(v_sp1)
            spDivisions.Children.Add(v_sp2)

        Else 'No conferences only divisions
            Dim v_sp As StackPanel = New StackPanel()
            v_sp.Orientation = Orientation.Vertical
            v_sp.VerticalAlignment = VerticalAlignment.Top
            v_sp.HorizontalAlignment = HorizontalAlignment.Center

            Dim gb_conf1 As GroupBox = New GroupBox()
            gb_conf1.Name = "gb_conf1"
            gb_conf1.Margin = New Thickness(10, 10, 10, 10)
            gb_conf1.FontSize = 18
            gb_conf1.Header = "Divisions:"
            gb_conf1.Style = GroupBoxstyle

            v_sp.Children.Add(gb_conf1)
            Dim st_v_gb As StackPanel = New StackPanel()
            st_v_gb.Orientation = Orientation.Vertical
            st_v_gb.HorizontalAlignment = HorizontalAlignment.Center
            st_v_gb.Margin = New Thickness(5, 5, 10, 10)

            'set the labels font text colors ext.
            For i As Integer = 1 To num_divs
                Dim sp1 As StackPanel = New StackPanel()
                sp1.Orientation = Orientation.Horizontal
                sp1.Margin = New Thickness(0, 0, 0, 2)
                sp1.Name = "div1_staack"

                Dim div_1_label As Label = New Label()
                div_1_label.Content = "Division " & i.ToString
                div_1_label.Style = Largelblstyle

                Dim txtDivision1 As New TextBox()
                txtDivision1.Name = "newldiv" & i.ToString
                txtDivision1.Width = 150
                txtDivision1.Style = Largetxttyle
                txtDivision1.AddHandler(TextBox.LostFocusEvent, New RoutedEventHandler(AddressOf divTextbox_LostFocus))

                sp1.Children.Add(div_1_label)
                sp1.Children.Add(txtDivision1)

                st_v_gb.Children.Add(sp1)
                gb_conf1.Content = st_v_gb

                'register the dynamically added control so that it can be looked up later.
                Me.RegisterName(txtDivision1.Name, txtDivision1)

            Next

            spDivisions.Children.Add(v_sp)
        End If

        'setting division from new_teams on teams tab
        Dim Teamlbltyle As Style = Application.Current.FindResource("Teamlbltyle")
        '            Dim Conflbltyle As Style = Application.Current.FindResource("Conflbltyle")

        Dim t_id = 1
        Dim num_divs_per_conf As Integer
        If num_confs = 0 Then
            num_divs_per_conf = num_divs
        Else
            num_divs_per_conf = num_divs \ num_confs
        End If

        If num_confs = 2 Then
            Dim v_sp1 As StackPanel = New StackPanel()
            v_sp1.Orientation = Orientation.Vertical
            v_sp1.VerticalAlignment = VerticalAlignment.Top
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
                gb_hdr_label.Name = "newldiv_team" & i.ToString
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
            v_sp2.VerticalAlignment = VerticalAlignment.Top
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

            For i As Integer = num_divs_per_conf + 1 To num_divs
                Dim gb_hdr_label As Label = New Label()
                gb_hdr_label.Name = "newldiv_team" & i.ToString
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
            Dim v2_sp As StackPanel = New StackPanel()
            v2_sp.Orientation = Orientation.Vertical
            v2_sp.HorizontalAlignment = HorizontalAlignment.Center

            For i As Integer = 1 To num_divs
                Dim gb_hdr_label As Label = New Label()
                gb_hdr_label.Name = "newldiv_team" & i.ToString
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
                v2_sp.Children.Add(gb_div)
            Next

            sp1.Children.Add(v2_sp)
        End If

        pw.League.setOrganization(num_weeks, num_games, num_teams, num_playoff_teams)
        setTeamsLabels()

    End Sub
    Private Sub newl1btnTrophyPath_Click(sender As Object, e As RoutedEventArgs) Handles newl1btnTrophyPath.Click
        Dim OpenFileDialog As OpenFileDialog = New OpenFileDialog()
        Dim init_folder As String = CommonUtils.getAppPath
        init_folder += "\Images\Trophies"

        OpenFileDialog.InitialDirectory = init_folder
        OpenFileDialog.Multiselect = False
        OpenFileDialog.Filter = "Image Files|*.jpg;*.gif;*.png;*.bmp"
        If (OpenFileDialog.ShowDialog() = True) Then
            Dim filepath As String = OpenFileDialog.FileName
            newl1TrophyPath.Text = filepath
            newl1TrophyImage.Source = New BitmapImage(New Uri(filepath))
        End If
    End Sub
    Private Sub newl1Cancel_Click(sender As Object, e As RoutedEventArgs) Handles newl1Cancel.Click

        RaiseEvent Show_MainMenu(Me, New EventArgs)

    End Sub
    Private Sub unregisterControl(ByVal s As String)
        Try
            Me.UnregisterName(s)
        Catch IG As Exception
        End Try
    End Sub
    Private Sub newl1Next_Click(sender As Object, e As RoutedEventArgs) Handles newl1Next.Click
        Dim Conferences_list As List(Of String) = New List(Of String)
        Dim Divisions_list As List(Of String) = New List(Of String)

        Try
            validate()

            If CInt(newlnumconferences.Text) = 2 Then
                Conferences_list.Add(CType(Me.FindName("newlConf1"), TextBox).Text)
                Conferences_list.Add(CType(Me.FindName("newlConf2"), TextBox).Text)
            End If

            For i As Integer = 1 To CInt(newlnumdivisions.Text)
                Divisions_list.Add(CType(Me.FindName("newldiv" & i.ToString), TextBox).Text)
            Next

            Dim lyears As List(Of Integer) = New List(Of Integer)(New Integer() {CInt(newl1StartingYear.Text)})

            pw.League.setBasicInfo(newl1shortname.Text, newl1longname.Text, CInt(newl1StartingYear.Text),
                        newl1championshipgame.Text, newl1TrophyPath.Text, Conferences_list, Divisions_list,
                        lyears, League_State.Regular_Season)

            '            Dim NL_Teams As NewLeague_Teams = New NewLeague_Teams(winMainMenu, Me, nl)
            '           NL_Teams.setFields()
            '          NL_Teams.Show()

        Catch ex As Exception
            MessageBox.Show(CommonUtils.substr(ex.Message, 0, 100), "Error", MessageBoxButton.OK, MessageBoxImage.Error)
        End Try

    End Sub
    'This event handler is called when the division name textbox loses focus, so that
    'the division names can be set on the tab of new teams
    Private Sub divTextbox_LostFocus(sender As Object, e As RoutedEventArgs)
        Dim l As TextBox = e.Source
        Dim n As Integer = CommonUtils.ExtractDivNumber(l.Name)
        Dim divLabel As Label = Me.FindName("newldiv_team" & n.ToString)
        divLabel.Content = l.Text
        '        MessageBox.Show(l.Name & " " & n.ToString)

    End Sub
    'This method is fired when either conference textbox loses focus so that either conference
    'label can be set on the teams tab
    Private Sub confTextbox_LostFocus(sender As Object, e As RoutedEventArgs)
        Dim l As TextBox = e.Source
        Dim confLabel As Label = Nothing
        If l.Name.EndsWith("1") Then
            confLabel = Me.FindName("newllblConf1")
        Else
            confLabel = Me.FindName("newllblConf2")
        End If
        confLabel.Content = l.Text
    End Sub
    Public Sub setTeamsLabels()
        Dim Teamlbltyle As Style = Application.Current.FindResource("Teamlbltyle")

        For i As Integer = 1 To CInt(newlnumteams.Text)
            Dim teamLabel = "newllblTeam" & i.ToString
            Dim teamImage = "newlimgTeam" & i.ToString

            Dim teamLbl As Label = Me.FindName(teamLabel)
            Dim teamImg As Image = Me.FindName(teamImage)
            teamLbl.Style = Teamlbltyle

            Dim st As TeamMdl = pw.League.Teams(i - 1)
            If st.City <> App_Constants.EMPTY_TEAM_SLOT Then
                Dim Color_Percents_List As List(Of Uniform_Color_percents) = Nothing
                Color_Percents_List = Uniform.getColorList(st.Uniform)

                teamLbl.Foreground = New SolidColorBrush(CommonUtils.getColorfromHex(Color_Percents_List(0).color_string))
                teamLbl.Content = st.City & " " & st.Nickname
                teamLbl.VerticalContentAlignment = VerticalContentAlignment.Center

                If Color_Percents_List.Count > 2 Then
                    Dim BackBrush As LinearGradientBrush = New LinearGradientBrush()
                    BackBrush.StartPoint = New Point(0, 0)
                    BackBrush.EndPoint = New Point(1, 1)

                    Dim running_value As Single = 0
                    For ii As Integer = 1 To Color_Percents_List.Count - 1
                        BackBrush.GradientStops.Add(New GradientStop(
                        CommonUtils.getColorfromHex(Color_Percents_List(ii).color_string), running_value))

                        running_value = Color_Percents_List(ii).value

                        BackBrush.GradientStops.Add(New GradientStop(
                        CommonUtils.getColorfromHex(Color_Percents_List(ii).color_string), running_value))
                    Next
                    teamLbl.Background = BackBrush
                Else
                    teamLbl.Background = New SolidColorBrush(CommonUtils.getColorfromHex(Color_Percents_List(1).color_string))
                End If
            Else
                teamLbl.Content = App_Constants.EMPTY_TEAM_SLOT
            End If

            Dim img_path = pw.League.Teams(i - 1).Helmet_img_path
            If Not IsNothing(img_path) AndAlso img_path.Length > 0 Then
                '                Dim helmetIMG_source As BitmapImage = New BitmapImage(New Uri("pack://application:,,,/Resources/" & img_path))
                Dim helmetIMG_source As BitmapImage = New BitmapImage(New Uri(img_path))
                teamImg.Source = helmetIMG_source
            End If
        Next
    End Sub
    Private Sub TeamLabel_MouseDown(sender As Object, e As RoutedEventArgs)

        Dim l As Label = e.Source
        Dim n As Integer = CommonUtils.ExtractTeamNumber(l.Name)
        RaiseEvent Show_NewTeam(Me, New teamEventArgs(n))

    End Sub


End Class
