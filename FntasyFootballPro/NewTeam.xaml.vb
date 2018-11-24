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

        If Not IsNothing(League_Teams(team_ind).Stadium) Then
            newtStadium.Text = League_Teams(team_ind).Stadium.Stadium_Name
            newtStadiumLocation.Text = League_Teams(team_ind).Stadium.Stadium_Location
            newtStadiumPath.Text = League_Teams(team_ind).Stadium.Stadium_Img_Path
            newtStadiumCapacity.Text = League_Teams(team_ind).Stadium.Capacity
        End If

        If Not IsNothing(League_Teams(team_ind).Graphics) Then
            newtHelmetImgPath.Text = League_Teams(team_ind).Graphics.Helmet_img_path
            Dim helmetIMG_source As BitmapImage = New BitmapImage(New Uri("pack://application:,,,/Resources/" & League_Teams(team_ind).Graphics.Helmet_img_path))
        End If

        If Not IsNothing(League_Teams(team_ind).Uniform) Then
            Dim hel_color As String = League_Teams(team_ind).Uniform.Helmet.Helmet_Color
            '            Helmet_color.Background = New SolidColorBrush(CType(ColorConverter.ConvertFromString(hel_color), Color))
            newtHelmentColor.SelectedColor = CType(ColorConverter.ConvertFromString(hel_color), Color)

            Dim home_jersey_c As String = League_Teams(team_ind).Uniform.Home_Jersey.Jersey_Color
            '            home_jersey_color.Background = New SolidColorBrush(CType(ColorConverter.ConvertFromString(home_jersey_c), Color))
            newtHomeJerseyColor.SelectedColor = CType(ColorConverter.ConvertFromString(home_jersey_c), Color)

            Dim away_jersey_c As String = League_Teams(team_ind).Uniform.Away_Jersey.Jersey_Color
            '            away_jersey_color.Background = New SolidColorBrush(CType(ColorConverter.ConvertFromString(away_jersey_c), Color))
            newtAwayJerseyColor.SelectedColor = CType(ColorConverter.ConvertFromString(away_jersey_c), Color)

            Dim home_pants_c As String = League_Teams(team_ind).Uniform.Home_Pants.Pants_Color
            '            home_pants_color.Background = New SolidColorBrush(CType(ColorConverter.ConvertFromString(home_pants_c), Color))
            newtHomePantsColor.SelectedColor = CType(ColorConverter.ConvertFromString(home_pants_c), Color)

            Dim away_pants_c As String = League_Teams(team_ind).Uniform.Away_Pants.Pants_Color
            '           away_pants_color.Background = New SolidColorBrush(CType(ColorConverter.ConvertFromString(away_pants_c), Color))
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
        '        Helmet_color.Background = New SolidColorBrush(newtHelmentColor.SelectedColor)
    End Sub
    Private Sub HomeJerseyColor_SelectedColorChanged(sender As Object, e As RoutedPropertyChangedEventArgs(Of Windows.Media.Color?))
        '        home_jersey_color.Background = New SolidColorBrush(newtHomeJerseyColor.SelectedColor)
    End Sub
    Private Sub AwayJerseyColor_SelectedColorChanged(sender As Object, e As RoutedPropertyChangedEventArgs(Of Windows.Media.Color?))
        '        away_jersey_color.Background = New SolidColorBrush(newtAwayJerseyColor.SelectedColor)
    End Sub
    Private Sub HomePantsColor_SelectedColorChanged(sender As Object, e As RoutedPropertyChangedEventArgs(Of Windows.Media.Color?))
        '        home_pants_color.Background = New SolidColorBrush(newtHomePantsColor.SelectedColor)
    End Sub
    Private Sub AwayPantsColor_SelectedColorChanged(sender As Object, e As RoutedPropertyChangedEventArgs(Of Windows.Media.Color?))
        '        away_pants_color.Background = New SolidColorBrush(newtAwayPantsColor.SelectedColor)
    End Sub
    Private Sub newtAwaySleeveColor_SelectedColorChanged(sender As Object, e As RoutedPropertyChangedEventArgs(Of Windows.Media.Color?))

    End Sub
    Private Sub newtAwayNumberOutlineColor_SelectedColorChanged(sender As Object, e As RoutedPropertyChangedEventArgs(Of Windows.Media.Color?))

    End Sub
    Private Sub newtAwayJerseySleeve1Color_SelectedColorChanged(sender As Object, e As RoutedPropertyChangedEventArgs(Of Windows.Media.Color?))

    End Sub
    Private Sub newtAwayJerseySleeve2Color_SelectedColorChanged(sender As Object, e As RoutedPropertyChangedEventArgs(Of Windows.Media.Color?))

    End Sub
    Private Sub newtAwayJerseySleeve3Color_SelectedColorChanged(sender As Object, e As RoutedPropertyChangedEventArgs(Of Windows.Media.Color?))

    End Sub
    Private Sub newtAwayJerseySleeve4Color_SelectedColorChanged(sender As Object, e As RoutedPropertyChangedEventArgs(Of Windows.Media.Color?))

    End Sub
    Private Sub newtAwayJerseySleeve5Color_SelectedColorChanged(sender As Object, e As RoutedPropertyChangedEventArgs(Of Windows.Media.Color?))

    End Sub
    Private Sub newtAwayJerseySleeve6Color_SelectedColorChanged(sender As Object, e As RoutedPropertyChangedEventArgs(Of Windows.Media.Color?))

    End Sub
    Private Sub newtAwayJerseySleeve7Color_SelectedColorChanged(sender As Object, e As RoutedPropertyChangedEventArgs(Of Windows.Media.Color?))

    End Sub
    Private Sub newtAwayJerseySleeve8Color_SelectedColorChanged(sender As Object, e As RoutedPropertyChangedEventArgs(Of Windows.Media.Color?))

    End Sub
    Private Sub newtAwayJerseySleeve9Color_SelectedColorChanged(sender As Object, e As RoutedPropertyChangedEventArgs(Of Windows.Media.Color?))

    End Sub
    Private Sub newtAwayJerseyNumberColor_SelectedColorChanged(sender As Object, e As RoutedPropertyChangedEventArgs(Of Windows.Media.Color?))

    End Sub
    Private Sub newtAwayJerseyColor_SelectedColorChanged(sender As Object, e As RoutedPropertyChangedEventArgs(Of Windows.Media.Color?))

    End Sub
    Private Sub newtAwayShoulderStripeColor_SelectedColorChanged(sender As Object, e As RoutedPropertyChangedEventArgs(Of Windows.Media.Color?))

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
    Private Sub newtbtnStadiumPath_Click(sender As Object, e As RoutedEventArgs) Handles newtbtnStadiumPath.Click
        Dim OpenFileDialog As OpenFileDialog = New OpenFileDialog()
        If (OpenFileDialog.ShowDialog() = True) Then
            Dim filepath As String = OpenFileDialog.FileName
            newtStadiumPath.Text = filepath
            Stadium_image.Source = New BitmapImage(New Uri(filepath))
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

    Private Sub newt1Add_Click(sender As Object, e As RoutedEventArgs) Handles newt1Add.Click
        Try
            Dim new_t As TeamMdl = New_League.Teams(team_ind)
            Dim stadium As StadiumMdl = Nothing
            Dim Footwear As FootwearMdl = Nothing
            Dim Helmet As HelmetMdl = Nothing
            Dim Home_Jersey As JerseyMdl = Nothing
            Dim Home_Pants As PantsMdl = Nothing
            Dim Away_Jersey As JerseyMdl = Nothing
            Dim Away_Pants As PantsMdl = Nothing
            Dim Uniform As UniformMdl = Nothing
            Dim Graphics As Graphics = Nothing

            Dim City_Abr As String = newtCityAbb.Background.GetHashCode
            Dim City As String = newtCity.Background.GetHashCode
            Dim Nickname As String = newtNickname.Background.GetHashCode

            Dim socks_color As String = newtSockColor.Background.GetHashCode
            Dim cleats_color As String = newtCleatsColor.Background.GetHashCode
            Dim helmet_color As String = newtHelmentColor.Background.GetHashCode
            Dim helmet_logo_color As String = newtHelmentLogoColor.Background.GetHashCode
            Dim helmet_facemask_color As String = newtFacemaskColor.Background.GetHashCode
            Dim helmet_stripe_1 As String = newtHelmentStripe1Color.Background.GetHashCode
            Dim helmet_stripe_2 As String = newtHelmentStripe2Color.Background.GetHashCode
            Dim helmet_stripe_3 As String = newtHelmentStripe3Color.Background.GetHashCode

            Dim Home_Jersey_Color As String = newtHomeJerseyColor.Background.GetHashCode
            Dim Home_Sleeve_Color As String = newtHomeSleeveColor.Background.GetHashCode
            Dim Home_Shoulder_Stripe_Color As String = newtHomeShoulderStripeColor.Background.GetHashCode
            Dim Home_Jersey_Number_Color As String = newtHomeJerseyNumberColor.Background.GetHashCode
            Dim Home_Jersey_Outline_Number_Color As String = newtHomeNumberOutlineColor.Background.GetHashCode
            Dim Home_Jersey_Sleeve_1_Color As String = newtHomeJerseySleeve1Color.Background.GetHashCode
            Dim Home_Jersey_Sleeve_2_Color As String = newtHomeJerseySleeve2Color.Background.GetHashCode
            Dim Home_Jersey_Sleeve_3_Color As String = newtHomeJerseySleeve3Color.Background.GetHashCode
            Dim Home_Jersey_Sleeve_4_Color As String = newtHomeJerseySleeve4Color.Background.GetHashCode
            Dim Home_Jersey_Sleeve_5_Color As String = newtHomeJerseySleeve5Color.Background.GetHashCode
            Dim Home_Jersey_Sleeve_6_Color As String = newtHomeJerseySleeve6Color.Background.GetHashCode
            Dim Home_Jersey_Sleeve_7_Color As String = newtHomeJerseySleeve7Color.Background.GetHashCode
            Dim Home_Jersey_Sleeve_8_Color As String = newtHomeJerseySleeve8Color.Background.GetHashCode
            Dim Home_Jersey_Sleeve_9_Color As String = newtHomeJerseySleeve9Color.Background.GetHashCode

            Dim Home_Pants_Color As String = newtHomePantsColor.Background.GetHashCode
            Dim Home_Pants_Stripe_1_Color As String = newtHomePantsStripe1Color.Background.GetHashCode
            Dim Home_Pants_Stripe_2_Color As String = newtHomePantsStripe2Color.Background.GetHashCode
            Dim Home_Pants_Stripe_3_Color As String = newtHomePantsStripe3Color.Background.GetHashCode

            Dim away_Jersey_Color As String = newtAwayJerseyColor.Background.GetHashCode
            Dim away_Sleeve_Color As String = newtAwaySleeveColor.Background.GetHashCode
            Dim away_Shoulder_Stripe_Color As String = newtAwayShoulderStripeColor.Background.GetHashCode
            Dim away_Jersey_Number_Color As String = newtAwayJerseyNumberColor.Background.GetHashCode
            Dim away_Jersey_Outline_Number_Color As String = newtAwayNumberOutlineColor.Background.GetHashCode
            Dim away_Jersey_Sleeve_1_Color As String = newtAwayJerseySleeve1Color.Background.GetHashCode
            Dim away_Jersey_Sleeve_2_Color As String = newtAwayJerseySleeve2Color.Background.GetHashCode
            Dim away_Jersey_Sleeve_3_Color As String = newtAwayJerseySleeve3Color.Background.GetHashCode
            Dim away_Jersey_Sleeve_4_Color As String = newtAwayJerseySleeve4Color.Background.GetHashCode
            Dim away_Jersey_Sleeve_5_Color As String = newtAwayJerseySleeve5Color.Background.GetHashCode
            Dim away_Jersey_Sleeve_6_Color As String = newtAwayJerseySleeve6Color.Background.GetHashCode
            Dim away_Jersey_Sleeve_7_Color As String = newtAwayJerseySleeve7Color.Background.GetHashCode
            Dim away_Jersey_Sleeve_8_Color As String = newtAwayJerseySleeve8Color.Background.GetHashCode
            Dim away_Jersey_Sleeve_9_Color As String = newtAwayJerseySleeve9Color.Background.GetHashCode

            Dim away_Pants_Color As String = newtAwayPantsColor.Background.GetHashCode
            Dim away_Pants_Stripe_1_Color As String = newtAwayPantsStripe1Color.Background.GetHashCode
            Dim away_Pants_Stripe_2_Color As String = newtAwayPantsStripe2Color.Background.GetHashCode
            Dim away_Pants_Stripe_3_Color As String = newtAwayPantsStripe3Color.Background.GetHashCode


            stadium = New StadiumMdl(newtStadium.Text, newtStadiumLocation.Text,
                                    newtStadiumCapacity.Text, newtStadiumPath.Text)
            Footwear = New FootwearMdl(socks_color, cleats_color)
            Helmet = New HelmetMdl(helmet_color, helmet_logo_color,
                                helmet_facemask_color, helmet_stripe_1, helmet_stripe_2, helmet_stripe_3)
            Home_Jersey = New JerseyMdl(Home_Jersey_Color, Home_Sleeve_Color, Home_Shoulder_Stripe_Color,
                                Home_Jersey_Number_Color, Home_Jersey_Outline_Number_Color,
                                Home_Jersey_Sleeve_1_Color, Home_Jersey_Sleeve_2_Color,
                                Home_Jersey_Sleeve_3_Color, Home_Jersey_Sleeve_4_Color,
                                Home_Jersey_Sleeve_5_Color, Home_Jersey_Sleeve_6_Color,
                                Home_Jersey_Sleeve_7_Color, Home_Jersey_Sleeve_8_Color,
                                Home_Jersey_Sleeve_9_Color)
            Home_Pants = New PantsMdl(Home_Pants_Color, Home_Pants_Stripe_1_Color,
                                Home_Pants_Stripe_2_Color, Home_Pants_Stripe_3_Color)

            Away_Jersey = New JerseyMdl(away_Jersey_Color, away_Sleeve_Color, away_Shoulder_Stripe_Color,
                                away_Jersey_Number_Color, away_Jersey_Outline_Number_Color,
                                away_Jersey_Sleeve_1_Color, away_Jersey_Sleeve_2_Color,
                                away_Jersey_Sleeve_3_Color, away_Jersey_Sleeve_4_Color,
                                away_Jersey_Sleeve_5_Color, away_Jersey_Sleeve_6_Color,
                                away_Jersey_Sleeve_7_Color, away_Jersey_Sleeve_8_Color,
                                away_Jersey_Sleeve_9_Color)
            Away_Pants = New PantsMdl(away_Pants_Color, away_Pants_Stripe_1_Color,
                                away_Pants_Stripe_2_Color, away_Pants_Stripe_3_Color)
            Graphics = New Graphics(newtHelmetImgPath.Text)
            Uniform = New UniformMdl(Helmet, Home_Jersey, Away_Jersey, Home_Pants, Away_Pants, Footwear)

            new_t.setFields(City_Abr, City, Nickname, stadium, Uniform, Graphics, Roster)

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
