Imports System.ComponentModel
Imports System.IO
Imports FntasyFootballPro.Leaguemdl
Imports log4net
Imports Microsoft.Win32

Public Class NewLeagueUC
    Private Shared logger As ILog = LogManager.GetLogger("RollingFile")
    'pw is the parent window mainwindow
    Private pw As MainWindow
    Private st_list As List(Of TeamMdl)
    Private dragSource As ListBox = Nothing
    Private dragSource_team As StackPanel = Nothing
    Private drag_data As StackPanel = Nothing
    Private drag_from As String = Nothing
    '    Private dragSource_team As StackPanel = Nothing
    Private UnselNewTeamSP As Style = Application.Current.FindResource("UnselNewTeamSP")
    Private DragEnt_NewTeamSP As Style = Application.Current.FindResource("DragEnt_NewTeamSP")
    Public Property startPoint As Point

    Public Event Show_MainMenu As EventHandler
    Public Event Show_NewTeam As EventHandler

    Public bw As BackgroundWorker = Nothing
    Public pop As Progress_Dialog = New Progress_Dialog()

    Public Sub New(ByVal pw As MainWindow, ByVal st_list As List(Of TeamMdl))

        ' This call is required by the designer.
        InitializeComponent()

        Me.pw = pw
        Me.pw.League = New Leaguemdl()
        Me.st_list = st_list

        Dim icurrentyear As Integer = Date.Today.Year

        For i As Integer = icurrentyear - 100 To icurrentyear + 100
            newl1StartingYear.Items.Add(i.ToString)
        Next

        newl1StartingYear.Text = icurrentyear.ToString
        newl1Structure.SelectedIndex = 0

        setStockTeams()

    End Sub
    Private Sub setStockTeams()

        StockTeamsGrid.Items.Clear()

        For Each st In st_list

            Dim h_sp As StackPanel = New StackPanel
            h_sp.Orientation = Orientation.Horizontal

            Dim BitmapImage As BitmapImage = New BitmapImage(New Uri(CommonUtils.getAppPath & App_Constants.APP_HELMET_FOLDER & st.Helmet_img_path))
            Dim helmet_img As Image = New Image()
            helmet_img.Width = 25
            helmet_img.Height = 25
            helmet_img.Source = BitmapImage

            Dim Color_Percents_List As List(Of Uniform_Color_percents) = Nothing
            Color_Percents_List = Uniform.getColorList(st.Uniform)

            Dim team_label As Label = New Label()
            team_label.Foreground = New SolidColorBrush(CommonUtils.getColorfromHex(Color_Percents_List(0).color_string))
            team_label.Content = st.City & " " & st.Nickname
            team_label.Height = 25
            team_label.Width = 180
            team_label.VerticalContentAlignment = VerticalContentAlignment.Center

            If Color_Percents_List.Count > 2 Then
                Dim BackBrush As LinearGradientBrush = New LinearGradientBrush()
                BackBrush.StartPoint = New Point(0, 0)
                BackBrush.EndPoint = New Point(1, 1)

                Dim running_value As Single = 0
                For i As Integer = 1 To Color_Percents_List.Count - 1
                    BackBrush.GradientStops.Add(New GradientStop(
                    CommonUtils.getColorfromHex(Color_Percents_List(i).color_string), running_value))

                    running_value = Color_Percents_List(i).value

                    BackBrush.GradientStops.Add(New GradientStop(
                    CommonUtils.getColorfromHex(Color_Percents_List(i).color_string), running_value))
                Next
                team_label.Background = BackBrush
            Else
                team_label.Background = New SolidColorBrush(CommonUtils.getColorfromHex(Color_Percents_List(1).color_string))
            End If
            team_label.FontFamily = New FontFamily("Times New Roman")
            team_label.FontSize = 12

            h_sp.Children.Add(helmet_img)
            h_sp.Children.Add(team_label)
            'h_sp.Children.Add(std_img)
            h_sp.Margin = New System.Windows.Thickness(5)

            StockTeamsGrid.Items.Add(h_sp)
        Next

    End Sub


    Private Sub validate()

        Dim DIRPath As String
        DIRPath = System.IO.Path.Combine(My.Computer.FileSystem.SpecialDirectories.MyDocuments, App_Constants.GAME_DOC_FOLDER & Path.DirectorySeparatorChar & newl1shortname.Text)

        If CommonUtils.isBlank(newl1shortname.Text) Then Throw New Exception("League Short Name must be supplied!")
        If Not CommonUtils.isAlpha(newl1shortname.Text, False) Then Throw New Exception("Invalid character in League Short Name!")

        If CommonUtils.isBlank(newl1longname.Text) Then Throw New Exception("League Long Name must be supplied!")
        If Not CommonUtils.isAlpha(newl1longname.Text, True) Then Throw New Exception("Invalid character in League Long Name!")

        If CommonUtils.isBlank(newl1championshipgame.Text) Then Throw New Exception("Championship Game must be supplied!")

        If CommonUtils.isBlank(newlnumweeks.Text) OrElse Not IsNumeric(newlnumweeks.Text) Then Throw New Exception("Invalid Value for Number of Weeks!")
        If CommonUtils.isBlank(newlnumgames.Text) OrElse Not IsNumeric(newlnumgames.Text) Then Throw New Exception("Invalid Value for Number of Games!")
        If CommonUtils.isBlank(newlnumdivisions.Text) OrElse Not IsNumeric(newlnumdivisions.Text) Then Throw New Exception("Invalid Value for Number of Divisions!")
        If CommonUtils.isBlank(newlnumteams.Text) OrElse Not IsNumeric(newlnumteams.Text) Then Throw New Exception("Invalid Value for Number of Teams!")

        If CommonUtils.isBlank(newlnumplayoffteams.Text) OrElse Not IsNumeric(newlnumplayoffteams.Text) Then Throw New Exception("Invalid Value for Number of PGAME_DOC_FOLDERlayoff Teams")

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

        Dim ts As Team_Services = New Team_Services()
        Dim first_dup_team = ts.FirstDuplicateTeam(pw.League.Teams)
        If Not IsNothing(first_dup_team) Then
            Throw New Exception("Duplicate team " & first_dup_team & " found in league!  Leagues can not have duplicate teams!")
        End If

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
        Dim UnselNewTeamSP As Style = Application.Current.FindResource("UnselNewTeamSP")
        Dim DragEnt_NewTeamSP As Style = Application.Current.FindResource("DragEnt_NewTeamSP")

        num_weeks = v(0)
        num_games = v(1)
        num_divs = v(2)
        num_teams = v(3)
        num_confs = v(4)
        num_playoff_teams = v(5)
        teams_per_division = num_teams \ num_divs

        logger.info("League structure changed to " &
            "num_weeks " & num_weeks &
            "num_games " & num_games &
            "num_divs " & num_divs &
            "num_teams " & num_teams &
            "num_confs " & num_confs &
            "num_playoff_teams " & num_playoff_teams &
            "teams_per_division " & teams_per_division)

        newlnumweeks.Text = num_weeks.ToString
        newlnumgames.Text = num_games.ToString
        newlnumdivisions.Text = num_divs.ToString
        newlnumteams.Text = num_teams.ToString
        newlnumconferences.Text = num_confs.ToString
        newlnumplayoffteams.Text = num_playoff_teams.ToString

        logger.Debug("setOrganization")
        pw.League.setOrganization(num_weeks, num_games, num_teams, num_playoff_teams)

        'Clear previous division selections
        logger.Debug("Clear previous division selections")
        spDivisions.Children.Clear()
        sp1.Children.Clear()
        Me.unregisterControl("newlConf1")
        Me.unregisterControl("newlConf2")
        Me.unregisterControl("newllblConf1")
        Me.unregisterControl("newllblConf2")

        logger.Debug("Unregister all team controls")
        For I As Integer = 1 To CInt(num_teams)
            Me.unregisterControl("newldiv" & I.ToString)
            Me.unregisterControl("newldiv_team" & I.ToString)
            Me.unregisterControl("newlimgTeam" & I.ToString)
            Me.unregisterControl("newllblTeam" & I.ToString)
        Next

        If num_confs = 2 Then
            logger.Debug("Num conferences = 2")
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

            logger.Debug("Conference 1 controls created.")

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

            logger.Debug("Conference 2 controls created.")

            Dim st_v_gb1 As StackPanel = New StackPanel()
            st_v_gb1.Orientation = Orientation.Vertical
            st_v_gb1.HorizontalAlignment = HorizontalAlignment.Center
            st_v_gb1.Margin = New Thickness(5, 5, 10, 10)

            Dim st_v_gb2 As StackPanel = New StackPanel()
            st_v_gb2.Orientation = Orientation.Vertical
            st_v_gb2.HorizontalAlignment = HorizontalAlignment.Center
            st_v_gb2.Margin = New Thickness(5, 5, 10, 10)

            last_div_first_group = num_divs \ 2

            logger.Debug("last_div_first_group " & last_div_first_group)

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

                logger.Debug("Division " & i & " created")

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

                logger.Debug("Division " & j & " created")
            Next
            gb_conf1.Content = st_v_gb1
            gb_conf2.Content = st_v_gb2

            spDivisions.Children.Add(v_sp1)
            spDivisions.Children.Add(v_sp2)

        Else 'No conferences only divisions
            logger.Debug("0 divisions")
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

                logger.Debug("Division " & i & " created")
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
                    sp_team.Style = UnselNewTeamSP

                    Dim helmet_img As Image = New Image()
                    helmet_img.Name = "newlimgTeam" & t_id.ToString
                    helmet_img.Width = 20
                    helmet_img.Height = 20

                    Dim team_label As Label = New Label()
                    team_label.Name = "newllblTeam" & t_id.ToString
                    team_label.Padding = New Thickness(10, 0, 0, 0)
                    team_label.Width = 250
                    team_label.Style = Teamlbltyle


                    sp_team.Children.Add(helmet_img)
                    sp_team.Children.Add(team_label)
                    sp_team.AllowDrop = True

                    sp_team.AddHandler(StackPanel.MouseDownEvent, New RoutedEventHandler(AddressOf sp_team_MouseDown))
                    sp_team.AddHandler(StackPanel.MouseMoveEvent, New RoutedEventHandler(AddressOf sp_team_MouseMove))
                    sp_team.AddHandler(StackPanel.DragEnterEvent, New DragEventHandler(AddressOf sp_team_dragenter))
                    sp_team.AddHandler(StackPanel.DragLeaveEvent, New DragEventHandler(AddressOf sp_team_dragleave))
                    sp_team.AddHandler(StackPanel.DropEvent, New DragEventHandler(AddressOf sp_team_drop))

                    sp_team.Style = UnselNewTeamSP

                    v_sp_in_groupbox.Children.Add(sp_team)

                    Me.RegisterName(helmet_img.Name, helmet_img)
                    Me.RegisterName(team_label.Name, team_label)

                    logger.Debug("Team " & t_id & " control created")

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
                    team_label.Width = 250
                    team_label.Style = Teamlbltyle

                    sp_team.Children.Add(helmet_img)
                    sp_team.Children.Add(team_label)
                    sp_team.AllowDrop = True
                    sp_team.AddHandler(StackPanel.MouseDownEvent, New RoutedEventHandler(AddressOf sp_team_MouseDown))
                    sp_team.AddHandler(StackPanel.MouseMoveEvent, New RoutedEventHandler(AddressOf sp_team_MouseMove))
                    sp_team.AddHandler(StackPanel.DragEnterEvent, New DragEventHandler(AddressOf sp_team_dragenter))
                    sp_team.AddHandler(StackPanel.DragLeaveEvent, New DragEventHandler(AddressOf sp_team_dragleave))
                    sp_team.AddHandler(StackPanel.DropEvent, New DragEventHandler(AddressOf sp_team_drop))
                    sp_team.Style = UnselNewTeamSP

                    v_sp_in_groupbox.Children.Add(sp_team)

                    Me.RegisterName(helmet_img.Name, helmet_img)
                    Me.RegisterName(team_label.Name, team_label)

                    logger.Debug("Team " & t_id & " control created")

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
                    team_label.Width = 250
                    team_label.Style = Teamlbltyle

                    sp_team.Children.Add(helmet_img)
                    sp_team.Children.Add(team_label)
                    sp_team.AllowDrop = True
                    sp_team.AddHandler(StackPanel.MouseDownEvent, New RoutedEventHandler(AddressOf sp_team_MouseDown))
                    sp_team.AddHandler(StackPanel.MouseMoveEvent, New RoutedEventHandler(AddressOf sp_team_MouseMove))
                    sp_team.AddHandler(StackPanel.DragEnterEvent, New DragEventHandler(AddressOf sp_team_dragenter))
                    sp_team.AddHandler(StackPanel.DragLeaveEvent, New DragEventHandler(AddressOf sp_team_dragleave))
                    sp_team.AddHandler(StackPanel.DropEvent, New DragEventHandler(AddressOf sp_team_drop))
                    sp_team.Style = UnselNewTeamSP

                    v_sp_in_groupbox.Children.Add(sp_team)

                    Me.RegisterName(helmet_img.Name, helmet_img)
                    Me.RegisterName(team_label.Name, team_label)

                    logger.Debug("Team " & t_id & " control created")

                    t_id += 1
                Next
                v2_sp.Children.Add(gb_div)
            Next

            sp1.Children.Add(v2_sp)
        End If


        setTeamsLabels()

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
            logger.Info("Create new league clicked")
            validate()
            logger.Info("league validated!")

            If CInt(newlnumconferences.Text) = 2 Then
                Conferences_list.Add(CType(Me.FindName("newlConf1"), TextBox).Text.Trim)
                Conferences_list.Add(CType(Me.FindName("newlConf2"), TextBox).Text.Trim)
            End If

            For i As Integer = 1 To CInt(newlnumdivisions.Text)
                Divisions_list.Add(CType(Me.FindName("newldiv" & i.ToString), TextBox).Text.Trim)
            Next

            Dim lyears As List(Of Integer) = New List(Of Integer)(New Integer() {CInt(newl1StartingYear.Text)})

            pw.League.setBasicInfo(newl1shortname.Text.ToUpper.Trim, newl1longname.Text.Trim, CInt(newl1StartingYear.Text),
                        newl1championshipgame.Text.Trim, Conferences_list, Divisions_list,
                        lyears, League_State.Regular_Season)

            'Background Worker code for popup
            pop.btnclose.Visibility = Visibility.Hidden

            bw = New BackgroundWorker()

            ' Add any initialization after the InitializeComponent() call.
            AddHandler bw.DoWork, AddressOf bw_DoWork
            AddHandler bw.ProgressChanged, AddressOf bw_ProgressChanged
            AddHandler bw.RunWorkerCompleted, AddressOf bw_RunWorkerCompleted

            bw.WorkerReportsProgress = True
            bw.RunWorkerAsync(pw.League)

            'Progress Bar Window
            pop.ShowDialog()

        Catch ex As Exception
            logger.Error("Error creating new league")
            logger.Error(ex)
            MessageBox.Show(CommonUtils.substr(ex.Message, 0, 100), "Error", MessageBoxButton.OK, MessageBoxImage.Error)
        End Try

    End Sub
    Private Sub bw_DoWork(ByVal sender As Object, ByVal e As DoWorkEventArgs)

        Dim lg As Leaguemdl = e.Argument

        Dim s As League_Services = New League_Services()
        s.CreateNewLeague(lg, bw)

    End Sub
    Private Sub bw_RunWorkerCompleted(ByVal sender As Object, ByVal e As RunWorkerCompletedEventArgs)
        Dim lableError As Style = Application.Current.FindResource("LabelError")
        pop.btnclose.Visibility = Visibility.Visible

        'Progress Bar Window close
        If pop.prgTest.Foreground Is Brushes.Red Then

            pop.statuslbl.Style = lableError
            pop.statuslbl.Content = "Error!  League Not Create!"

        Else
            pop.statuslbl.Content = "League Created Successfully!"
        End If

    End Sub
    Private Sub bw_ProgressChanged(ByVal sender As Object, ByVal e As ProgressChangedEventArgs)
        Dim user_state_Struct As String = e.UserState
        Dim user_stats As String() = user_state_Struct.Split("|")

        pop.Title = user_stats(0)
        pop.prgTest.Value = e.ProgressPercentage
        pop.prglbl.Content = user_stats(1)

        If user_stats(0) = "Error" Then
            pop.prgTest.Foreground = Brushes.Red
        End If


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

        logger.Info("Setting team labels")

        For i As Integer = 1 To CInt(newlnumteams.Text)
            Dim teamLabel = "newllblTeam" & i.ToString
            Dim teamImage = "newlimgTeam" & i.ToString
            logger.Debug("setting teamlabel and teamimage " & " " & teamImage)

            Dim teamLbl As Label = Me.FindName(teamLabel)
            Dim teamImg As Image = Me.FindName(teamImage)
            logger.Debug("teamlabel and teamimg found")

            teamLbl.Style = Teamlbltyle

            Dim st As TeamMdl = pw.League.Teams(i - 1)
            If st.City <> App_Constants.EMPTY_TEAM_SLOT Then

                logger.Debug("Setting label for " & st.City)

                teamLbl.Content = st.City & " " & st.Nickname
                teamLbl.VerticalContentAlignment = VerticalContentAlignment.Center
            Else
                teamLbl.Content = App_Constants.EMPTY_TEAM_SLOT
            End If

            Dim img_path = pw.League.Teams(i - 1).Helmet_img_path

            logger.Debug("Helmet img_path: " & img_path)

            If Not IsNothing(img_path) AndAlso img_path.Length > 0 Then
                Dim helmetIMG_source As BitmapImage = New BitmapImage(New Uri(img_path))
                teamImg.Source = helmetIMG_source
            End If
        Next
    End Sub
    Private Sub sp_team_drop(sender As Object, e As DragEventArgs)

        If sender Is e.Source Then Return

        Dim new_sp As StackPanel = CType(sender, StackPanel)
        Dim new_image As Image = new_sp.Children.Item(0)
        Dim new_label As Label = new_sp.Children.Item(1)

        Dim drag_data_image As Image = Nothing
        Dim drag_data_label As Label = Nothing


        If drag_from = "stock" Then
            drag_data_image = drag_data.Children.Item(0)
            drag_data_label = drag_data.Children.Item(1)

            'Get the full stock team that was dragged
            Dim new_team As TeamMdl = Team.get_team_from_name(drag_data_label.Content, st_list)
            'get the imdex of the team label that the new team is to be dropped on
            Dim new_index As Integer = CommonUtils.ExtractTeamNumber(new_label.Name) - 1

            new_sp.Style = UnselNewTeamSP

            new_image.Source = CType(drag_data.Children.Item(0), Image).Source
            new_label.Content = CType(drag_data.Children.Item(1), Label).Content

            dragSource.Items.Remove(drag_data)
            Dim cloned_team As TeamMdl = New TeamMdl(new_team)
            cloned_team.setID(new_index)
            'Set prefix the helmet image path and stadium image path with the app folders for this
            'computer, because these stock teams were created on the developer's computer and
            'would not be correct.
            cloned_team.setStockImagePaths(CommonUtils.getAppPath & App_Constants.APP_HELMET_FOLDER & new_team.Helmet_img_path,
            CommonUtils.getAppPath & App_Constants.APP_STADIUM_FOLDER & new_team.Stadium.Stadium_Img_Path)
            pw.League.Teams(new_index) = cloned_team
        ElseIf drag_from = "league" Then
            drag_data_image = drag_data.Children.Item(0)
            drag_data_label = drag_data.Children.Item(1)

            'get the new team id from the label name
            Dim new_index As Integer = CommonUtils.ExtractTeamNumber(new_label.Name) - 1
            'get the old team id from the label name
            Dim old_index As Integer = CommonUtils.ExtractTeamNumber(drag_data_label.Name) - 1

            new_sp.Style = UnselNewTeamSP

            new_image.Source = CType(drag_data.Children.Item(0), Image).Source
            new_label.Content = CType(drag_data.Children.Item(1), Label).Content

            drag_data_image.Source = Nothing
            drag_data_label.Content = App_Constants.EMPTY_TEAM_SLOT

            'Set new team in league to the old team and change the id to the new slot
            pw.League.Teams(new_index) = pw.League.Teams(old_index)
            pw.League.Teams(new_index).setID(new_index)

            'Set old slot to a new blank team
            Dim blank_team As TeamMdl = New TeamMdl(old_index, App_Constants.EMPTY_TEAM_SLOT)
            pw.League.Teams(old_index) = blank_team

        End If

    End Sub
    Private Sub sp_team_MouseMove(sender As Object, e As MouseEventArgs)

        ' Get the current mouse position
        Dim mousePos As Point = e.GetPosition(Nothing)
        Dim diff As Vector = startPoint - mousePos

        If e.LeftButton = MouseButtonState.Pressed And
          (Math.Abs(diff.X) > SystemParameters.MinimumHorizontalDragDistance Or
          Math.Abs(diff.Y) > SystemParameters.MinimumVerticalDragDistance) Then
            drag_data = Nothing
            drag_from = "league"
            Dim parent As StackPanel = CType(sender, StackPanel)
            drag_data = parent
            If drag_data IsNot Nothing Then
                DragDrop.DoDragDrop(parent, drag_data, DragDropEffects.Move)
            End If
        End If
    End Sub

    Private Sub sp_team_MouseDown(sender As Object, e As MouseButtonEventArgs)

        startPoint = e.GetPosition(Nothing)

        If e.ClickCount = 2 Then
            Dim s As StackPanel = sender
            Dim l As Label = s.Children(1)
            Dim n As Integer = CommonUtils.ExtractTeamNumber(l.Name)
            RaiseEvent Show_NewTeam(Me, New teamEventArgs(n))
        End If

    End Sub

    Private Sub sp_team_dragenter(sender As Object, e As DragEventArgs)

        Dim tb As StackPanel = TryCast(sender, StackPanel)
        tb.Style = DragEnt_NewTeamSP

    End Sub

    Private Sub sp_team_dragleave(sender As Object, e As DragEventArgs)

        Dim tb As StackPanel = TryCast(sender, StackPanel)
        tb.Style = UnselNewTeamSP

    End Sub

    Private Sub StockTeamsGrid_PreviewMouseLeftButtonDown(sender As Object, e As MouseButtonEventArgs) Handles StockTeamsGrid.PreviewMouseLeftButtonDown

        drag_data = Nothing
        drag_from = "stock"
        Dim parent As ListBox = CType(sender, ListBox)
        dragSource = parent
        Dim data As Object = GetDataFromListBox(dragSource, e.GetPosition(parent))
        If data IsNot Nothing Then
            drag_data = CType(data, StackPanel)
            DragDrop.DoDragDrop(parent, data, DragDropEffects.Move)
        End If

    End Sub

    Private Shared Function GetDataFromListBox(ByVal source As ListBox, ByVal point As Point) As Object

        Dim element As UIElement = TryCast(source.InputHitTest(point), UIElement)

        If element IsNot Nothing Then
            Dim data As Object = DependencyProperty.UnsetValue

            While data Is DependencyProperty.UnsetValue
                data = source.ItemContainerGenerator.ItemFromContainer(element)

                If data Is DependencyProperty.UnsetValue Then
                    element = TryCast(VisualTreeHelper.GetParent(element), UIElement)
                End If

                If element Is source Then
                    Return Nothing
                End If
            End While

            If data IsNot DependencyProperty.UnsetValue Then
                Return data
            End If
        End If

        Return Nothing
    End Function

    Private Sub help_btn_Click(sender As Object, e As RoutedEventArgs) Handles help_btn.Click
        Dim hlp_form As Help_NewLeague = New Help_NewLeague()
        hlp_form.Top = (System.Windows.SystemParameters.PrimaryScreenHeight - hlp_form.Height) / 2
        hlp_form.Left = (System.Windows.SystemParameters.PrimaryScreenWidth - hlp_form.Width) / 2
        hlp_form.ShowDialog()
    End Sub
End Class
