Public Class NewLeague_Teams

    Property winMainMenu As MainWindow
    Property NewLeague_Settings As NewLeague_Settings
    Property New_League As New_League = Nothing



    Public Sub New(ByVal winMainMenu As MainWindow, ByVal NewLeague_Settings As NewLeague_Settings,
                   ByVal New_League As New_League)

        ' This call is required by the designer.
        InitializeComponent()

        Me.winMainMenu = winMainMenu
        Me.NewLeague_Settings = NewLeague_Settings
        Me.New_League = New_League

        Dim League_Teams As List(Of TeamMdl)

        League_Teams = New List(Of TeamMdl)
        With NewLeague_Settings
            For i As Integer = 0 To CInt(.newlnumdivisions.Text) * CInt(.newlnumteamsperdivisions.Text) - 1
                League_Teams.Add(New TeamMdl(i + 1, "New Team"))
            Next
        End With
        Me.New_League.Teams = League_Teams

    End Sub



    Public Sub setFields()

        newldiv1gh.Content = NewLeague_Settings.newldiv1.Text
        newldiv2gh.Content = NewLeague_Settings.newldiv2.Text
        newldiv3gh.Content = NewLeague_Settings.newldiv3.Text
        newldiv4gh.Content = NewLeague_Settings.newldiv4.Text

        newldiv5gh.Content = NewLeague_Settings.newldiv5.Text
        newldiv6gh.Content = NewLeague_Settings.newldiv6.Text
        newldiv7gh.Content = NewLeague_Settings.newldiv7.Text
        newldiv8gh.Content = NewLeague_Settings.newldiv8.Text


        With NewLeague_Settings
            For i As Integer = 1 To CInt(.newlnumdivisions.Text) * CInt(.newlnumteamsperdivisions.Text)
                Dim teamLabel = "newllblTeam" & i.ToString
                Dim teamImage = "newlimgTeam" & i.ToString

                Dim teamLbl As Label = Me.FindName(teamLabel)
                Dim teamImg As Image = Me.FindName(teamImage)
                teamLbl.Content = New_League.Teams(i - 1).Nickname
                Dim img_path = New_League.Teams(i - 1).Helmet_img_path
                If Not IsNothing(img_path) Then
                    Dim helmetIMG_source As BitmapImage = New BitmapImage(New Uri("pack://application:,,,/Resources/" & img_path))
                    teamImg.Source = helmetIMG_source
                End If
            Next
        End With

    End Sub
    Private Sub TeamLabel_MouseDown(sender As Object, e As RoutedEventArgs)
        Dim l As Label = e.Source
        Dim n As Integer = CommonUtils.ExtractTeamNumber(l.Name)
        '        MessageBox.Show(l.Name & " " & n.ToString)
        Dim NL_Team As NewTeam = New NewTeam(winMainMenu, Me, n, New_League)
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
            Dim ls As League_Services = New League_Services()
            ls.CreateNewLeague(New_League)
            NewLeague_Settings.Close()
            Me.Close()
            winMainMenu.Show()
        Catch ex As Exception
            MessageBox.Show(CommonUtils.substr(ex.Message, 0, 100), "Error", MessageBoxButton.OK, MessageBoxImage.Error)
        End Try

    End Sub

    Private Sub validate()

        For Each t In New_League.Teams
            If t.Nickname = "New Team" Then
                Throw New Exception("Not all teams have been created!")
            End If
        Next

    End Sub

End Class

