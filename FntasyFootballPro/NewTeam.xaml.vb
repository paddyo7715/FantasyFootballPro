Imports System.IO
Imports System.Windows
Imports Microsoft.Win32

Public Class NewTeam
    Property winMainMenu As MainWindow
    Property NewLeague_Teams As NewLeague_Teams
    Property team_ind As Integer
    Property Roster As List(Of PlayerMdl) = Nothing
    Property New_League As Leaguemdl = Nothing


    Public Sub New(ByVal winMainMenu As MainWindow, ByVal NewLeague_Teams As NewLeague_Teams,
                   ByVal team_ind As Integer, ByVal New_League As Leaguemdl)

        ' This call is required by the designer.
        InitializeComponent()

        Me.NewLeague_Teams = NewLeague_Teams
        Me.team_ind = team_ind
        Me.winMainMenu = winMainMenu
        Me.New_League = New_League

    End Sub
    Public Sub setfields()

        Dim League_Teams As List(Of TeamMdl) = New_League.Teams
        Dim colorConverter As ColorConverter = New ColorConverter()
        Dim bc As BrushConverter = New BrushConverter()

        newtCityAbb.Text = League_Teams(team_ind).City_Abr
        newtCity.Text = League_Teams(team_ind).City
        newtNickname.Text = League_Teams(team_ind).Nickname

        If Not IsNothing(League_Teams(team_ind).Stadium_Name) Then
            newtStadium.Text = League_Teams(team_ind).Stadium_Name
        End If

        If Not IsNothing(League_Teams(team_ind).Stadium_Location) Then
            newtStadiumLocation.Text = League_Teams(team_ind).Stadium_Location
        End If

        If Not IsNothing(League_Teams(team_ind).Stadium_Img_Path) Then
            newtStadiumPath.Text = League_Teams(team_ind).Stadium_Img_Path
        End If

        If Not IsNothing(League_Teams(team_ind).Helmet_img_path) Then
            newtHelmetImgPath.Text = League_Teams(team_ind).Helmet_img_path
            Dim helmetIMG_source As BitmapImage = New BitmapImage(New Uri("pack://application:,,,/Resources/" & League_Teams(team_ind).Helmet_img_path))
        End If

        If Not IsNothing(League_Teams(team_ind).Helmet_Color) Then
            Dim hel_color As String = League_Teams(team_ind).Helmet_Color
            Helmet_color.Background = New SolidColorBrush(CType(ColorConverter.ConvertFromString(hel_color), Color))
            newtHelmentColor.SelectedColor = CType(ColorConverter.ConvertFromString(hel_color), Color)
        End If

        If Not IsNothing(League_Teams(team_ind).Home_jersey_Color) Then
            Dim home_jersey_c As String = League_Teams(team_ind).Home_jersey_Color
            home_jersey_color.Background = New SolidColorBrush(CType(ColorConverter.ConvertFromString(home_jersey_c), Color))
            newtHomeJerseyColor.SelectedColor = CType(ColorConverter.ConvertFromString(home_jersey_c), Color)
        End If

        If Not IsNothing(League_Teams(team_ind).Away_jersey_Color) Then
            Dim away_jersey_c As String = League_Teams(team_ind).Away_jersey_Color
            away_jersey_color.Background = New SolidColorBrush(CType(ColorConverter.ConvertFromString(away_jersey_c), Color))
            newtAwayJerseyColor.SelectedColor = CType(ColorConverter.ConvertFromString(away_jersey_c), Color)
        End If

        If Not IsNothing(League_Teams(team_ind).Home_Pants_Color) Then
            Dim home_pants_c As String = League_Teams(team_ind).Home_Pants_Color
            home_pants_color.Background = New SolidColorBrush(CType(ColorConverter.ConvertFromString(home_pants_c), Color))
            newtHomePantsColor.SelectedColor = CType(ColorConverter.ConvertFromString(home_pants_c), Color)
        End If

        If Not IsNothing(League_Teams(team_ind).Away_Pants_Color) Then
            Dim away_pants_c As String = League_Teams(team_ind).Away_Pants_Color
            away_pants_color.Background = New SolidColorBrush(CType(ColorConverter.ConvertFromString(away_pants_c), Color))
            newtAwayPantsColor.SelectedColor = CType(ColorConverter.ConvertFromString(away_pants_c), Color)
        End If

    End Sub
    Private Sub newtHelmentLogoColor_SelectedColorChanged(sender As Object, e As RoutedPropertyChangedEventArgs(Of Windows.Media.Color?))

    End Sub
    Private Sub newtFacemaskColor_SelectedColorChanged(sender As Object, e As RoutedPropertyChangedEventArgs(Of Windows.Media.Color?))

    End Sub
    Private Sub newtHelmentStripe1Color_SelectedColorChanged(sender As Object, e As RoutedPropertyChangedEventArgs(Of Windows.Media.Color?))

    End Sub
    Private Sub newtHelmentStripe2Color_SelectedColorChanged(sender As Object, e As RoutedPropertyChangedEventArgs(Of Windows.Media.Color?))

    End Sub
    Private Sub newtHelmentStripe3Color_SelectedColorChanged(sender As Object, e As RoutedPropertyChangedEventArgs(Of Windows.Media.Color?))

    End Sub
    Private Sub newtSockColor_SelectedColorChanged(sender As Object, e As RoutedPropertyChangedEventArgs(Of Windows.Media.Color?))

    End Sub
    Private Sub newtCleatsColor_SelectedColorChanged(sender As Object, e As RoutedPropertyChangedEventArgs(Of Windows.Media.Color?))

    End Sub
    Private Sub newtHelmentColor_SelectedColorChanged(sender As Object, e As RoutedPropertyChangedEventArgs(Of Windows.Media.Color?))
        Helmet_color.Background = New SolidColorBrush(newtHelmentColor.SelectedColor)
    End Sub
    Private Sub HomeJerseyColor_SelectedColorChanged(sender As Object, e As RoutedPropertyChangedEventArgs(Of Windows.Media.Color?))
        home_jersey_color.Background = New SolidColorBrush(newtHomeJerseyColor.SelectedColor)
    End Sub
    Private Sub AwayJerseyColor_SelectedColorChanged(sender As Object, e As RoutedPropertyChangedEventArgs(Of Windows.Media.Color?))
        away_jersey_color.Background = New SolidColorBrush(newtAwayJerseyColor.SelectedColor)
    End Sub
    Private Sub HomePantsColor_SelectedColorChanged(sender As Object, e As RoutedPropertyChangedEventArgs(Of Windows.Media.Color?))
        home_pants_color.Background = New SolidColorBrush(newtHomePantsColor.SelectedColor)
    End Sub
    Private Sub AwayPantsColor_SelectedColorChanged(sender As Object, e As RoutedPropertyChangedEventArgs(Of Windows.Media.Color?))
        away_pants_color.Background = New SolidColorBrush(newtAwayPantsColor.SelectedColor)
    End Sub

    Private Sub newt1Cancel_Click(sender As Object, e As RoutedEventArgs) Handles newt1Cancel.Click
        Me.Close()
        NewLeague_Teams.Show()
    End Sub

    Private Sub newtbtnHelmetImgPath_Click(sender As Object, e As RoutedEventArgs) Handles newtbtnHelmetImgPath.Click
        Dim OpenFileDialog As OpenFileDialog = New OpenFileDialog()
        If (OpenFileDialog.ShowDialog() = True) Then
            Dim filepath As String = OpenFileDialog.FileName
            newtHelmetImgPath.Text = filepath
            Helmet_image.Source = New BitmapImage(New Uri(filepath))
        End If
    End Sub


    Private Sub newtRollTeam_Click(sender As Object, e As RoutedEventArgs) Handles newtRollTeam.Click
        Dim ts As New Team_Services()

        Try
            Roster = ts.Roll_Players(New_League.Teams, "")
            newtPlayersGrid.ItemsSource = Roster

        Catch ex As Exception
            MessageBox.Show(CommonUtils.substr(ex.Message, 0, 100), "Error", MessageBoxButton.OK, MessageBoxImage.Error)
        End Try


    End Sub

    Private Sub newtPlayersGrid_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles newtPlayersGrid.SelectionChanged
        Try
            Dim new_t As TeamMdl = New_League.Teams(team_ind)
            Dim stadium As StadiumMdl = Nothing
            Dim hel_color As String = Helmet_color.Background.GetHashCode
            Dim hjersey_color As String = home_jersey_color.Background.GetHashCode
            Dim ajersey_color As String = away_jersey_color.Background.GetHashCode
            Dim hpants_color As String = home_pants_color.Background.GetHashCode
            Dim apants_color As String = away_pants_color.Background.GetHashCode

            'Create the Stadium Object for this team
            stadium = New StadiumMdl(team_ind, newtStadium.Text, newtStadiumLocation.Text,
                                    newtStadiumCapacity.Text, newtStadiumPath.Text)


            new_t.setFields(newtCityAbb.Text, newtCity.Text, newtNickname.Text, newtHelmetImgPath.Text,
                    hel_color, hjersey_color, hpants_color, ajersey_color, apants_color,
newtStadium.Text, newtStadiumLocation.Text, newtStadiumPath.Text)
            new_t.Players = Roster

        Catch ex As Exception
            MessageBox.Show(CommonUtils.substr(ex.Message, 0, 100), "Error", MessageBoxButton.OK, MessageBoxImage.Error)
        End Try
    End Sub

    Private Sub newtHomeJerseyColor_SelectedColorChanged(sender As Object, e As RoutedPropertyChangedEventArgs(Of Color?))

    End Sub

    Private Sub newtHomeShoulderStripeColor_SelectedColorChanged(sender As Object, e As RoutedPropertyChangedEventArgs(Of Color?))

    End Sub

    Private Sub newtHomeNumberOutlineColor_SelectedColorChanged(sender As Object, e As RoutedPropertyChangedEventArgs(Of Color?))

    End Sub

    Private Sub newtHomeJerseyNumberColor_SelectedColorChanged(sender As Object, e As RoutedPropertyChangedEventArgs(Of Color?))

    End Sub

    Private Sub newtHomeJerseySleeve1Color_SelectedColorChanged(sender As Object, e As RoutedPropertyChangedEventArgs(Of Color?))

    End Sub

    Private Sub newtHomeJerseySleeve2Color_SelectedColorChanged(sender As Object, e As RoutedPropertyChangedEventArgs(Of Color?))

    End Sub

    Private Sub newtHomeJerseySleeve3Color_SelectedColorChanged(sender As Object, e As RoutedPropertyChangedEventArgs(Of Color?))

    End Sub

    Private Sub newtHomeJerseySleeve4Color_SelectedColorChanged(sender As Object, e As RoutedPropertyChangedEventArgs(Of Color?))

    End Sub

    Private Sub newtHomeJerseySleeve5Color_SelectedColorChanged(sender As Object, e As RoutedPropertyChangedEventArgs(Of Color?))

    End Sub

    Private Sub newtHomeJerseySleeve6Color_SelectedColorChanged(sender As Object, e As RoutedPropertyChangedEventArgs(Of Color?))

    End Sub

    Private Sub newtHomeJerseySleeve7Color_SelectedColorChanged(sender As Object, e As RoutedPropertyChangedEventArgs(Of Color?))

    End Sub

    Private Sub newtHomeJerseySleeve8Color_SelectedColorChanged(sender As Object, e As RoutedPropertyChangedEventArgs(Of Color?))

    End Sub

    Private Sub newtHomeJerseySleeve9Color_SelectedColorChanged(sender As Object, e As RoutedPropertyChangedEventArgs(Of Color?))

    End Sub

    Private Sub newtHomeJerseySleeve10Color_SelectedColorChanged(sender As Object, e As RoutedPropertyChangedEventArgs(Of Color?))

    End Sub
End Class
