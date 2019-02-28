Imports System.IO
Imports Microsoft.Win32

Public Class NewLeague_Settings

    Property winMainMenu As MainWindow


    Public Sub New(ByVal winMainMenu As MainWindow)

        ' This call is required by the designer.
        InitializeComponent()

        Me.winMainMenu = winMainMenu

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
        If CommonUtils.isBlank(newl1LogoPath.Text) Then Throw New Exception("League logo path must be supplied!")
        If CommonUtils.isBlank(newl1championshipgame.Text) Then Throw New Exception("Championship Game must be supplied!")
        If CommonUtils.isBlank(newl1LogoPath.Text) Then Throw New Exception("League logo image must be supplied!")
        If CommonUtils.isBlank(newl1TrophyPath.Text) Then Throw New Exception("League trophy image must be supplied!")


        If CommonUtils.isBlank(newlnumweeks.Text) OrElse Not IsNumeric(newlnumweeks.Text) Then Throw New Exception("Invalid Value for Number of Weeks!")
        If CommonUtils.isBlank(newlnumgames.Text) OrElse Not IsNumeric(newlnumgames.Text) Then Throw New Exception("Invalid Value for Number of Games!")
        If CommonUtils.isBlank(newlnumdivisions.Text) OrElse Not IsNumeric(newlnumdivisions.Text) Then Throw New Exception("Invalid Value for Number of Divisions!")
        If CommonUtils.isBlank(newlnumteams.Text) OrElse Not IsNumeric(newlnumteams.Text) Then Throw New Exception("Invalid Value for Number of Teams!")

        If CommonUtils.isBlank(newlnumplayoffteams.Text) OrElse Not IsNumeric(newlnumplayoffteams.Text) Then Throw New Exception("Invalid Value for Number of Playoff Teams")

        If newl1Structure.SelectedIndex <> -1 Then
            Throw New Exception("You must select a league structure!")
        End If

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
        Dim j As Integer

        num_weeks = v(0)
        num_games = v(1)
        num_divs = v(2)
        num_teams = v(3)
        num_confs = v(4)
        num_playoff_teams = v(5)

        newlnumweeks.Text = num_weeks.ToString
        newlnumgames.Text = num_games.ToString
        newlnumdivisions.Text = num_divs.ToString
        newlnumteams.Text = num_confs.ToString
        newlnumconferences.Text = num_confs.ToString
        newlnumplayoffteams.Text = num_playoff_teams.ToString

        Dim v_sp As StackPanel = New StackPanel()
        v_sp.Orientation = "Vertical"

        If num_confs = 2 Then
            Dim conf_panel_1_sq As StackPanel = New StackPanel()
            conf_panel_1_sq.Name = "conference_panel_1"
            conf_panel_1_sq.Orientation = "Horizontal"

            Dim conf_1_label As Label = New Label()
            conf_1_label.Content = "Conference 1:"
            conf_1_label.Width = 150
            conf_1_label.FontSize = 18
            conf_1_label.Foreground = Brushes.White

            Dim txtConf1 As New TextBox()
            txtConf1.Name = "newlConf1"
            txtConf1.Width = 150
            txtConf1.FontSize = 18
            txtConf1.Height = 30
            txtConf1.Foreground = Brushes.Black
            txtConf1.Background = Brushes.White
            txtConf1.MaxLength = 60

            conf_panel_1_sq.Children.Add(conf_1_label)
            conf_panel_1_sq.Children.Add(txtConf1)

            v_sp.Children.Add(conf_panel_1_sq)

            Dim gb_conf1 As GroupBox = New GroupBox()
            gb_conf1.Name = "gb_conf1"
            gb_conf1.Margin = New Thickness(10, 10, 10, 10)
            gb_conf1.FontSize = 18
            gb_conf1.Header = "Conference 1:"

            v_sp.Children.Add(gb_conf1)

            Dim conf_panel_2_sq As StackPanel = New StackPanel()
            conf_panel_2_sq.Name = "conference_panel_2"
            conf_panel_2_sq.Orientation = "Horizontal"

            Dim conf_2_label As Label = New Label()
            conf_2_label.Content = "Conference 2:"
            conf_2_label.Width = 150
            conf_2_label.FontSize = 18
            conf_2_label.Foreground = Brushes.White

            Dim txtConf2 As New TextBox()
            txtConf2.Name = "newlConf2"
            txtConf2.Width = 150
            txtConf2.FontSize = 18
            txtConf2.Height = 30
            txtConf2.Foreground = Brushes.Black
            txtConf2.Background = Brushes.White
            txtConf2.MaxLength = 60

            conf_panel_2_sq.Children.Add(conf_2_label)
            conf_panel_2_sq.Children.Add(txtConf2)

            v_sp.Children.Add(conf_panel_2_sq)

            Dim gb_conf2 As GroupBox = New GroupBox()
            gb_conf2.Name = "gb_conf2"
            gb_conf2.Margin = New Thickness(10, 10, 10, 10)
            gb_conf2.FontSize = 18
            gb_conf2.Header = "Conference 2:"

            v_sp.Children.Add(gb_conf2)

            last_div_first_group = num_confs \ 2

            'set the labels font text colors ext.
            For i As Integer = 1 To last_div_first_group
                j = i + last_div_first_group
                Dim sp1 As StackPanel = New StackPanel()
                sp1.Orientation = "Horizontal"
                sp1.Name = "div1_staack"

                Dim div_1_label As Label = New Label()
                div_1_label.Content = "Division " & i.ToString
                div_1_label.Width = 150
                div_1_label.FontSize = 18
                div_1_label.Foreground = Brushes.White

                Dim txtDivision1 As New TextBox()
                txtDivision1.Name = "newldiv" & i.ToString
                txtDivision1.Width = 150
                txtDivision1.FontSize = 18
                txtDivision1.Height = 30
                txtDivision1.Foreground = Brushes.Black
                txtDivision1.Background = Brushes.LightGray
                txtDivision1.IsReadOnly = True

                sp1.Children.Add(div_1_label)
                sp1.Children.Add(txtDivision1)

                gb_conf1.Content = sp1

                Dim sp2 As StackPanel = New StackPanel()
                sp2.Orientation = "Horizontal"
                sp1.Name = "div2_staack"

                Dim div_2_label As Label = New Label()
                div_2_label.Content = "Division " & j.ToString
                div_2_label.Width = 150
                div_2_label.FontSize = 18
                div_2_label.Foreground = Brushes.White

                Dim txtDivision2 As New TextBox()
                txtDivision2.Name = "newldiv" & j.ToString
                txtDivision2.Width = 150
                txtDivision2.FontSize = 18
                txtDivision2.Height = 30
                txtDivision2.Foreground = Brushes.Black
                txtDivision2.Background = Brushes.LightGray
                txtDivision2.IsReadOnly = True

                sp2.Children.Add(div_2_label)
                sp2.Children.Add(txtDivision2)

                gb_conf2.Content = sp2
            Next
        Else 'No conferences only divisions
            Dim gb_conf1 As GroupBox = New GroupBox()
            gb_conf1.Name = "gb_conf1"
            gb_conf1.Margin = New Thickness(10, 10, 10, 10)
            gb_conf1.FontSize = 18
            gb_conf1.Header = "Divisions:"

            v_sp.Children.Add(gb_conf1)

            'set the labels font text colors ext.
            For i As Integer = 1 To num_divs
                Dim sp1 As StackPanel = New StackPanel()
                sp1.Orientation = "Horizontal"
                sp1.Name = "div1_staack"

                Dim div_1_label As Label = New Label()
                div_1_label.Content = "Division " & i.ToString
                div_1_label.Width = 150
                div_1_label.FontSize = 18
                div_1_label.Foreground = Brushes.White

                Dim txtDivision1 As New TextBox()
                txtDivision1.Name = "newldiv" & i.ToString
                txtDivision1.Width = 150
                txtDivision1.FontSize = 18
                txtDivision1.Height = 30
                txtDivision1.Foreground = Brushes.Black
                txtDivision1.Background = Brushes.LightGray
                txtDivision1.IsReadOnly = True

                sp1.Children.Add(div_1_label)
                sp1.Children.Add(txtDivision1)

                gb_conf1.Content = sp1
            Next

        End If

    End Sub


    Private Sub newl1btnLogoPath_Click(sender As Object, e As RoutedEventArgs) Handles newl1btnLogoPath.Click
        Dim OpenFileDialog As OpenFileDialog = New OpenFileDialog()
        If (OpenFileDialog.ShowDialog() = True) Then
            Dim filepath As String = OpenFileDialog.FileName
            newl1LogoPath.Text = filepath
            newl1LogoImage.Source = New BitmapImage(New Uri(filepath))
        End If
    End Sub
    Private Sub newl1btnTrophyPath_Click(sender As Object, e As RoutedEventArgs) Handles newl1btnTrophyPath.Click
        Dim OpenFileDialog As OpenFileDialog = New OpenFileDialog()
        If (OpenFileDialog.ShowDialog() = True) Then
            Dim filepath As String = OpenFileDialog.FileName
            newl1TrophyPath.Text = filepath
            newl1TrophyImage.Source = New BitmapImage(New Uri(filepath))
        End If
    End Sub
    Private Sub newl1Cancel_Click(sender As Object, e As RoutedEventArgs) Handles newl1Cancel.Click
        Me.Close()
        winMainMenu.Show()
    End Sub
    Private Sub newl1Next_Click(sender As Object, e As RoutedEventArgs) Handles newl1Next.Click
        Dim Conferences_list As List(Of String) = New List(Of String)
        Dim Divisions_list As List(Of String) = New List(Of String)

        Try
            validate()

            If CInt(newlnumconferences.Text) = 2 Then
                Conferences_list.Add(Me.FindName("newlConf1"))
                Conferences_list.Add(Me.FindName("newlConf2"))
            End If

            For i As Integer = 1 To CInt(newlnumdivisions.Text)
                Divisions_list.Add(Me.FindName("newldiv" & i.ToString))
            Next

            Dim nl As Leaguemdl = New Leaguemdl(newl1LogoPath.Text, newl1shortname.Text, newl1longname.Text, CInt(newl1StartingYear.Text),
            CInt(newlnumweeks.Text), CInt(newlnumgames.Text), newl1championshipgame.Text, newl1TrophyPath.Text,
            CInt(newlnumteams.Text), CInt(newlnumplayoffteams.Text), Conferences_list, Divisions_list)

            Dim NL_Teams As NewLeague_Teams = New NewLeague_Teams(winMainMenu, Me, nl)
            NL_Teams.setFields()
            NL_Teams.Show()

        Catch ex As Exception
            MessageBox.Show(CommonUtils.substr(ex.Message, 0, 100), "Error", MessageBoxButton.OK, MessageBoxImage.Error)
        End Try


    End Sub


End Class
