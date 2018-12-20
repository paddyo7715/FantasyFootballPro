Imports System.IO
Imports System.Windows
Imports Microsoft.Win32

Public Class NewTeam
    Property winMainMenu As MainWindow
    Property NewLeague_Teams As NewLeague_Teams
    Property team_ind As Integer
    Property Roster As List(Of PlayerMdl) = Nothing
    Property New_League As Leaguemdl = Nothing
    Property Uniform_Img As Uniform_Image

    Property Event_from_Code As Boolean = False

    Public Sub New(ByVal winMainMenu As MainWindow, ByVal NewLeague_Teams As NewLeague_Teams,
                   ByVal team_ind As Integer, ByVal New_League As Leaguemdl)

        ' This call is required by the designer.
        InitializeComponent()

        Me.NewLeague_Teams = NewLeague_Teams
        Me.team_ind = team_ind
        Me.winMainMenu = winMainMenu
        Me.New_League = New_League


        '        Uniform_Img = New Uniform_Image(My.Application.Info.DirectoryPath + "/Images/blankUniform.png")
        Uniform_Img = New Uniform_Image("../../Images/blankUniform.png")
        Uniform_Img.Flip_All_Colors(True,
           App_Constants.STOCK_GREY_COLOR, App_Constants.STOCK_GREY_COLOR,
           App_Constants.STOCK_GREY_COLOR, App_Constants.STOCK_GREY_COLOR,
           App_Constants.STOCK_GREY_COLOR, App_Constants.STOCK_GREY_COLOR,
           App_Constants.STOCK_GREY_COLOR, App_Constants.STOCK_GREY_COLOR,
           App_Constants.STOCK_GREY_COLOR, App_Constants.STOCK_GREY_COLOR,
           App_Constants.STOCK_GREY_COLOR, App_Constants.STOCK_GREY_COLOR,
           App_Constants.STOCK_GREY_COLOR, App_Constants.STOCK_GREY_COLOR,
           App_Constants.STOCK_GREY_COLOR, App_Constants.STOCK_GREY_COLOR,
           App_Constants.STOCK_GREY_COLOR, App_Constants.STOCK_GREY_COLOR,
           App_Constants.STOCK_GREY_COLOR, App_Constants.STOCK_GREY_COLOR)

        Uniform_Img.Flip_All_Colors(False,
           App_Constants.STOCK_GREY_COLOR, App_Constants.STOCK_GREY_COLOR,
           App_Constants.STOCK_GREY_COLOR, App_Constants.STOCK_GREY_COLOR,
           App_Constants.STOCK_GREY_COLOR, App_Constants.STOCK_GREY_COLOR,
           App_Constants.STOCK_GREY_COLOR, App_Constants.STOCK_GREY_COLOR,
           App_Constants.STOCK_GREY_COLOR, App_Constants.STOCK_GREY_COLOR,
           App_Constants.STOCK_GREY_COLOR, App_Constants.STOCK_GREY_COLOR,
           App_Constants.STOCK_GREY_COLOR, App_Constants.STOCK_GREY_COLOR,
           App_Constants.STOCK_GREY_COLOR, App_Constants.STOCK_GREY_COLOR,
           App_Constants.STOCK_GREY_COLOR, App_Constants.STOCK_GREY_COLOR,
           App_Constants.STOCK_GREY_COLOR, App_Constants.STOCK_GREY_COLOR)

        newtHomeUniform.Source = Uniform_Img.getHomeUniform_Image
        newtAwayUniform.Source = Uniform_Img.getAwayUniform_Image

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

        If Not CommonUtils.isBlank(League_Teams(team_ind).Helmet_img_path) Then
            newtHelmetImgPath.Text = League_Teams(team_ind).Helmet_img_path
            Dim helmetIMG_source As BitmapImage = New BitmapImage(New Uri("pack://application:,,,/Resources/" & League_Teams(team_ind).Helmet_img_path))
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

        If Event_from_Code Then Return

        Dim mc As System.Windows.Media.Color = New SolidColorBrush(newtHelmentLogoColor.SelectedColor).Color
        Uniform_Img.Flip_One_Color(True, App_Constants.STOCK_HEL_LOGO_COLOR, System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B))
        Uniform_Img.Flip_One_Color(False, App_Constants.STOCK_HEL_LOGO_COLOR, System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B))

        newtHomeUniform.Source = Uniform_Img.getHomeUniform_Image
        newtAwayUniform.Source = Uniform_Img.getAwayUniform_Image

    End Sub
    Private Sub newtFacemaskColor_SelectedColorChanged(sender As Object, e As RoutedPropertyChangedEventArgs(Of Windows.Media.Color?))

        If Event_from_Code Then Return

        Dim mc As System.Windows.Media.Color = New SolidColorBrush(newtFacemaskColor.SelectedColor).Color
        Uniform_Img.Flip_One_Color(True, App_Constants.STOCK_FACEMASK, System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B))
        Uniform_Img.Flip_One_Color(False, App_Constants.STOCK_FACEMASK, System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B))

        newtHomeUniform.Source = Uniform_Img.getHomeUniform_Image
        newtAwayUniform.Source = Uniform_Img.getAwayUniform_Image

    End Sub
    Private Sub newtSockColor_SelectedColorChanged(sender As Object, e As RoutedPropertyChangedEventArgs(Of Windows.Media.Color?))

        If Event_from_Code Then Return

        Dim mc As System.Windows.Media.Color = New SolidColorBrush(newtSockColor.SelectedColor).Color
        Uniform_Img.Flip_One_Color(True, App_Constants.STOCK_SOCKS_COLOR, System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B))
        Uniform_Img.Flip_One_Color(False, App_Constants.STOCK_SOCKS_COLOR, System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B))

        newtHomeUniform.Source = Uniform_Img.getHomeUniform_Image
        newtAwayUniform.Source = Uniform_Img.getAwayUniform_Image
    End Sub
    Private Sub newtCleatsColor_SelectedColorChanged(sender As Object, e As RoutedPropertyChangedEventArgs(Of Windows.Media.Color?))

        If Event_from_Code Then Return

        Dim mc As System.Windows.Media.Color = New SolidColorBrush(newtCleatsColor.SelectedColor).Color
        Uniform_Img.Flip_One_Color(True, App_Constants.STOCK_CLEATES_COLOR, System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B))
        Uniform_Img.Flip_One_Color(False, App_Constants.STOCK_CLEATES_COLOR, System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B))

        newtHomeUniform.Source = Uniform_Img.getHomeUniform_Image
        newtAwayUniform.Source = Uniform_Img.getAwayUniform_Image
    End Sub
    Private Sub newtHelmentColor_SelectedColorChanged(sender As Object, e As RoutedPropertyChangedEventArgs(Of Windows.Media.Color?))

        If Event_from_Code Then Return

        Dim mc As System.Windows.Media.Color = New SolidColorBrush(newtHelmentColor.SelectedColor).Color
        Uniform_Img.Flip_One_Color(True, App_Constants.STOCK_HELMET_COLOR, System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B))
        Uniform_Img.Flip_One_Color(False, App_Constants.STOCK_HELMET_COLOR, System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B))

        newtHomeUniform.Source = Uniform_Img.getHomeUniform_Image
        newtAwayUniform.Source = Uniform_Img.getAwayUniform_Image
    End Sub
    Private Sub newtHomeJerseyColor_SelectedColorChanged(sender As Object, e As RoutedPropertyChangedEventArgs(Of Color?))

        If Event_from_Code Then Return

        Dim mc As System.Windows.Media.Color = New SolidColorBrush(newtHomeJerseyColor.SelectedColor).Color
        Uniform_Img.Flip_One_Color(True, App_Constants.STOCK_JERSEY_COLOR, System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B))

        Event_from_Code = True
        newtHomeSleeveColor.SelectedColor = newtHomeJerseyColor.SelectedColor
        newtHomeShoulderStripeColor.SelectedColor = newtHomeJerseyColor.SelectedColor
        newtHomeUniform.Source = Uniform_Img.getHomeUniform_Image
        Event_from_Code = False

    End Sub
    Private Sub newtHomeShoulderStripeColor_SelectedColorChanged(sender As Object, e As RoutedPropertyChangedEventArgs(Of Color?))

        If Event_from_Code Then Return

        Dim mc As System.Windows.Media.Color = New SolidColorBrush(newtHomeShoulderStripeColor.SelectedColor).Color
        Uniform_Img.Flip_One_Color(True, App_Constants.STOCK_SHOULDER_STRIPE_COLOR, System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B))

        newtHomeUniform.Source = Uniform_Img.getHomeUniform_Image
    End Sub

    Private Sub newtHomeNumberOutlineColor_SelectedColorChanged(sender As Object, e As RoutedPropertyChangedEventArgs(Of Color?))

        If Event_from_Code Then Return

        Dim mc As System.Windows.Media.Color = New SolidColorBrush(newtHomeNumberOutlineColor.SelectedColor).Color
        Uniform_Img.Flip_One_Color(True, App_Constants.STOCK_NUM_OUTLINE_COLOR, System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B))

        newtHomeUniform.Source = Uniform_Img.getHomeUniform_Image

    End Sub

    Private Sub newtHomeJerseyNumberColor_SelectedColorChanged(sender As Object, e As RoutedPropertyChangedEventArgs(Of Color?))

        If Event_from_Code Then Return

        Dim mc As System.Windows.Media.Color = New SolidColorBrush(newtHomeJerseyNumberColor.SelectedColor).Color
        Uniform_Img.Flip_One_Color(True, App_Constants.STOCK_NUMBER_COLOR, System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B))

        newtHomeUniform.Source = Uniform_Img.getHomeUniform_Image
    End Sub
    Private Sub newtHomeSleeveColor_SelectedColorChanged(sender As Object, e As RoutedPropertyChangedEventArgs(Of Windows.Media.Color?))

        If Event_from_Code Then Return

        Dim mc As System.Windows.Media.Color = New SolidColorBrush(newtHomeSleeveColor.SelectedColor).Color
        Uniform_Img.Flip_One_Color(True, App_Constants.STOCK_SLEEVE_COLOR, System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B))

        Event_from_Code = True
        newtHomeJerseySleeve1Color.SelectedColor = newtHomeSleeveColor.SelectedColor
        newtHomeJerseySleeve2Color.SelectedColor = newtHomeSleeveColor.SelectedColor
        newtHomeJerseySleeve3Color.SelectedColor = newtHomeSleeveColor.SelectedColor
        newtHomeJerseySleeve4Color.SelectedColor = newtHomeSleeveColor.SelectedColor
        newtHomeJerseySleeve5Color.SelectedColor = newtHomeSleeveColor.SelectedColor
        newtHomeJerseySleeve6Color.SelectedColor = newtHomeSleeveColor.SelectedColor

        newtHomeUniform.Source = Uniform_Img.getHomeUniform_Image
        Event_from_Code = False

    End Sub
    Private Sub newtHomeJerseySleeve1Color_SelectedColorChanged(sender As Object, e As RoutedPropertyChangedEventArgs(Of Color?))

        If Event_from_Code Then Return

        Dim mc As System.Windows.Media.Color = New SolidColorBrush(newtHomeJerseySleeve1Color.SelectedColor).Color
        Uniform_Img.Flip_One_Color(True, App_Constants.STOCK_SLEEVE_STRIPE_1_COLOR, System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B))

        newtHomeUniform.Source = Uniform_Img.getHomeUniform_Image

    End Sub

    Private Sub newtHomeJerseySleeve2Color_SelectedColorChanged(sender As Object, e As RoutedPropertyChangedEventArgs(Of Color?))

        If Event_from_Code Then Return

        Dim mc As System.Windows.Media.Color = New SolidColorBrush(newtHomeJerseySleeve2Color.SelectedColor).Color
        Uniform_Img.Flip_One_Color(True, App_Constants.STOCK_SLEEVE_STRIPE_2_COLOR, System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B))

        newtHomeUniform.Source = Uniform_Img.getHomeUniform_Image

    End Sub

    Private Sub newtHomeJerseySleeve3Color_SelectedColorChanged(sender As Object, e As RoutedPropertyChangedEventArgs(Of Color?))

        If Event_from_Code Then Return

        Dim mc As System.Windows.Media.Color = New SolidColorBrush(newtHomeJerseySleeve3Color.SelectedColor).Color
        Uniform_Img.Flip_One_Color(True, App_Constants.STOCK_SLEEVE_STRIPE_3_COLOR, System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B))

        newtHomeUniform.Source = Uniform_Img.getHomeUniform_Image

    End Sub

    Private Sub newtHomeJerseySleeve4Color_SelectedColorChanged(sender As Object, e As RoutedPropertyChangedEventArgs(Of Color?))

        If Event_from_Code Then Return

        Dim mc As System.Windows.Media.Color = New SolidColorBrush(newtHomeJerseySleeve4Color.SelectedColor).Color
        Uniform_Img.Flip_One_Color(True, App_Constants.STOCK_SLEEVE_STRIPE_4_COLOR, System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B))

        newtHomeUniform.Source = Uniform_Img.getHomeUniform_Image

    End Sub

    Private Sub newtHomeJerseySleeve5Color_SelectedColorChanged(sender As Object, e As RoutedPropertyChangedEventArgs(Of Color?))

        If Event_from_Code Then Return

        Dim mc As System.Windows.Media.Color = New SolidColorBrush(newtHomeJerseySleeve5Color.SelectedColor).Color
        Uniform_Img.Flip_One_Color(True, App_Constants.STOCK_SLEEVE_STRIPE_5_COLOR, System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B))

        newtHomeUniform.Source = Uniform_Img.getHomeUniform_Image
    End Sub

    Private Sub newtHomeJerseySleeve6Color_SelectedColorChanged(sender As Object, e As RoutedPropertyChangedEventArgs(Of Color?))

        If Event_from_Code Then Return

        Dim mc As System.Windows.Media.Color = New SolidColorBrush(newtHomeJerseySleeve6Color.SelectedColor).Color
        Uniform_Img.Flip_One_Color(True, App_Constants.STOCK_SLEEVE_STRIPE_6_COLOR, System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B))

        newtHomeUniform.Source = Uniform_Img.getHomeUniform_Image
    End Sub
    Private Sub newtHomePantsColor_SelectedColorChanged(sender As Object, e As RoutedPropertyChangedEventArgs(Of Windows.Media.Color?))

        If Event_from_Code Then Return

        Dim mc As System.Windows.Media.Color = New SolidColorBrush(newtHomePantsColor.SelectedColor).Color
        Uniform_Img.Flip_One_Color(True, App_Constants.STOCK_PANTS_COLOR, System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B))

        Event_from_Code = True
        newtHomePantsStripe1Color.SelectedColor = newtHomePantsColor.SelectedColor
        newtHomePantsStripe2Color.SelectedColor = newtHomePantsColor.SelectedColor
        newtHomePantsStripe3Color.SelectedColor = newtHomePantsColor.SelectedColor

        newtHomeUniform.Source = Uniform_Img.getHomeUniform_Image
        Event_from_Code = False
    End Sub
    Private Sub newtHomePantsStripe1Color_SelectedColorChanged(sender As Object, e As RoutedPropertyChangedEventArgs(Of Windows.Media.Color?))

        If Event_from_Code Then Return

        Dim mc As System.Windows.Media.Color = New SolidColorBrush(newtHomePantsStripe1Color.SelectedColor).Color
        Uniform_Img.Flip_One_Color(True, App_Constants.STOCK_PANTS_STRIPE_1_COLOR, System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B))

        newtHomeUniform.Source = Uniform_Img.getHomeUniform_Image
    End Sub
    Private Sub newtHomePantsStripe2Color_SelectedColorChanged(sender As Object, e As RoutedPropertyChangedEventArgs(Of Windows.Media.Color?))

        If Event_from_Code Then Return

        Dim mc As System.Windows.Media.Color = New SolidColorBrush(newtHomePantsStripe2Color.SelectedColor).Color
        Uniform_Img.Flip_One_Color(True, App_Constants.STOCK_PANTS_STRIPE_2_COLOR, System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B))

        newtHomeUniform.Source = Uniform_Img.getHomeUniform_Image
    End Sub
    Private Sub newtHomePantsStripe3Color_SelectedColorChanged(sender As Object, e As RoutedPropertyChangedEventArgs(Of Windows.Media.Color?))

        If Event_from_Code Then Return

        Dim mc As System.Windows.Media.Color = New SolidColorBrush(newtHomePantsStripe3Color.SelectedColor).Color
        Uniform_Img.Flip_One_Color(True, App_Constants.STOCK_PANTS_STRIPE_3_COLOR, System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B))

        newtHomeUniform.Source = Uniform_Img.getHomeUniform_Image
    End Sub


    Private Sub newtAwayJerseyColor_SelectedColorChanged(sender As Object, e As RoutedPropertyChangedEventArgs(Of Color?))

        If Event_from_Code Then Return

        Dim mc As System.Windows.Media.Color = New SolidColorBrush(newtAwayJerseyColor.SelectedColor).Color
        Uniform_Img.Flip_One_Color(False, App_Constants.STOCK_JERSEY_COLOR, System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B))

        Event_from_Code = True
        newtAwaySleeveColor.SelectedColor = newtAwayJerseyColor.SelectedColor
        newtAwayShoulderStripeColor.SelectedColor = newtAwayJerseyColor.SelectedColor

        newtAwayUniform.Source = Uniform_Img.getAwayUniform_Image
        Event_from_Code = False

    End Sub
    Private Sub newtAwayShoulderStripeColor_SelectedColorChanged(sender As Object, e As RoutedPropertyChangedEventArgs(Of Color?))

        If Event_from_Code Then Return

        Dim mc As System.Windows.Media.Color = New SolidColorBrush(newtAwayShoulderStripeColor.SelectedColor).Color
        Uniform_Img.Flip_One_Color(False, App_Constants.STOCK_SHOULDER_STRIPE_COLOR, System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B))

        newtAwayUniform.Source = Uniform_Img.getAwayUniform_Image
    End Sub

    Private Sub newtAwayNumberOutlineColor_SelectedColorChanged(sender As Object, e As RoutedPropertyChangedEventArgs(Of Color?))

        If Event_from_Code Then Return

        Dim mc As System.Windows.Media.Color = New SolidColorBrush(newtAwayNumberOutlineColor.SelectedColor).Color
        Uniform_Img.Flip_One_Color(False, App_Constants.STOCK_NUM_OUTLINE_COLOR, System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B))

        newtAwayUniform.Source = Uniform_Img.getAwayUniform_Image

    End Sub

    Private Sub newtAwayJerseyNumberColor_SelectedColorChanged(sender As Object, e As RoutedPropertyChangedEventArgs(Of Color?))

        If Event_from_Code Then Return

        Dim mc As System.Windows.Media.Color = New SolidColorBrush(newtAwayJerseyNumberColor.SelectedColor).Color
        Uniform_Img.Flip_One_Color(False, App_Constants.STOCK_NUMBER_COLOR, System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B))

        newtAwayUniform.Source = Uniform_Img.getAwayUniform_Image
    End Sub
    Private Sub newtAwaySleeveColor_SelectedColorChanged(sender As Object, e As RoutedPropertyChangedEventArgs(Of Windows.Media.Color?))

        If Event_from_Code Then Return

        Dim mc As System.Windows.Media.Color = New SolidColorBrush(newtAwaySleeveColor.SelectedColor).Color
        Uniform_Img.Flip_One_Color(False, App_Constants.STOCK_SLEEVE_COLOR, System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B))

        Event_from_Code = True
        newtAwayJerseySleeve1Color.SelectedColor = newtAwaySleeveColor.SelectedColor
        newtAwayJerseySleeve2Color.SelectedColor = newtAwaySleeveColor.SelectedColor
        newtAwayJerseySleeve3Color.SelectedColor = newtAwaySleeveColor.SelectedColor
        newtAwayJerseySleeve4Color.SelectedColor = newtAwaySleeveColor.SelectedColor
        newtAwayJerseySleeve5Color.SelectedColor = newtAwaySleeveColor.SelectedColor
        newtAwayJerseySleeve6Color.SelectedColor = newtAwaySleeveColor.SelectedColor

        newtAwayUniform.Source = Uniform_Img.getAwayUniform_Image
        Event_from_Code = False

    End Sub
    Private Sub newtAwayJerseySleeve1Color_SelectedColorChanged(sender As Object, e As RoutedPropertyChangedEventArgs(Of Color?))

        If Event_from_Code Then Return

        Dim mc As System.Windows.Media.Color = New SolidColorBrush(newtAwayJerseySleeve1Color.SelectedColor).Color
        Uniform_Img.Flip_One_Color(False, App_Constants.STOCK_SLEEVE_STRIPE_1_COLOR, System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B))

        newtAwayUniform.Source = Uniform_Img.getAwayUniform_Image

    End Sub

    Private Sub newtAwayJerseySleeve2Color_SelectedColorChanged(sender As Object, e As RoutedPropertyChangedEventArgs(Of Color?))

        If Event_from_Code Then Return

        Dim mc As System.Windows.Media.Color = New SolidColorBrush(newtAwayJerseySleeve2Color.SelectedColor).Color
        Uniform_Img.Flip_One_Color(False, App_Constants.STOCK_SLEEVE_STRIPE_2_COLOR, System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B))

        newtAwayUniform.Source = Uniform_Img.getAwayUniform_Image

    End Sub

    Private Sub newtAwayJerseySleeve3Color_SelectedColorChanged(sender As Object, e As RoutedPropertyChangedEventArgs(Of Color?))

        If Event_from_Code Then Return

        Dim mc As System.Windows.Media.Color = New SolidColorBrush(newtAwayJerseySleeve3Color.SelectedColor).Color
        Uniform_Img.Flip_One_Color(False, App_Constants.STOCK_SLEEVE_STRIPE_3_COLOR, System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B))

        newtAwayUniform.Source = Uniform_Img.getAwayUniform_Image

    End Sub

    Private Sub newtAwayJerseySleeve4Color_SelectedColorChanged(sender As Object, e As RoutedPropertyChangedEventArgs(Of Color?))

        If Event_from_Code Then Return

        Dim mc As System.Windows.Media.Color = New SolidColorBrush(newtAwayJerseySleeve4Color.SelectedColor).Color
        Uniform_Img.Flip_One_Color(False, App_Constants.STOCK_SLEEVE_STRIPE_4_COLOR, System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B))

        newtAwayUniform.Source = Uniform_Img.getAwayUniform_Image

    End Sub

    Private Sub newtAwayJerseySleeve5Color_SelectedColorChanged(sender As Object, e As RoutedPropertyChangedEventArgs(Of Color?))

        If Event_from_Code Then Return

        Dim mc As System.Windows.Media.Color = New SolidColorBrush(newtAwayJerseySleeve5Color.SelectedColor).Color
        Uniform_Img.Flip_One_Color(False, App_Constants.STOCK_SLEEVE_STRIPE_5_COLOR, System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B))

        newtAwayUniform.Source = Uniform_Img.getAwayUniform_Image
    End Sub

    Private Sub newtAwayJerseySleeve6Color_SelectedColorChanged(sender As Object, e As RoutedPropertyChangedEventArgs(Of Color?))

        If Event_from_Code Then Return

        Dim mc As System.Windows.Media.Color = New SolidColorBrush(newtAwayJerseySleeve6Color.SelectedColor).Color
        Uniform_Img.Flip_One_Color(False, App_Constants.STOCK_SLEEVE_STRIPE_6_COLOR, System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B))

        newtAwayUniform.Source = Uniform_Img.getAwayUniform_Image
    End Sub
    Private Sub newtAwayPantsColor_SelectedColorChanged(sender As Object, e As RoutedPropertyChangedEventArgs(Of Windows.Media.Color?))

        If Event_from_Code Then Return

        Dim mc As System.Windows.Media.Color = New SolidColorBrush(newtAwayPantsColor.SelectedColor).Color
        Uniform_Img.Flip_One_Color(False, App_Constants.STOCK_PANTS_COLOR, System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B))

        Event_from_Code = True
        newtAwayPantsStripe1Color.SelectedColor = newtAwayPantsColor.SelectedColor
        newtAwayPantsStripe2Color.SelectedColor = newtAwayPantsColor.SelectedColor
        newtAwayPantsStripe3Color.SelectedColor = newtAwayPantsColor.SelectedColor

        newtAwayUniform.Source = Uniform_Img.getAwayUniform_Image
        Event_from_Code = False
    End Sub
    Private Sub newtAwayPantsStripe1Color_SelectedColorChanged(sender As Object, e As RoutedPropertyChangedEventArgs(Of Windows.Media.Color?))

        If Event_from_Code Then Return

        Dim mc As System.Windows.Media.Color = New SolidColorBrush(newtAwayPantsStripe1Color.SelectedColor).Color
        Uniform_Img.Flip_One_Color(False, App_Constants.STOCK_PANTS_STRIPE_1_COLOR, System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B))

        newtAwayUniform.Source = Uniform_Img.getAwayUniform_Image
    End Sub
    Private Sub newtAwayPantsStripe2Color_SelectedColorChanged(sender As Object, e As RoutedPropertyChangedEventArgs(Of Windows.Media.Color?))

        If Event_from_Code Then Return

        Dim mc As System.Windows.Media.Color = New SolidColorBrush(newtAwayPantsStripe2Color.SelectedColor).Color
        Uniform_Img.Flip_One_Color(False, App_Constants.STOCK_PANTS_STRIPE_2_COLOR, System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B))

        newtAwayUniform.Source = Uniform_Img.getAwayUniform_Image
    End Sub
    Private Sub newtAwayPantsStripe3Color_SelectedColorChanged(sender As Object, e As RoutedPropertyChangedEventArgs(Of Windows.Media.Color?))

        If Event_from_Code Then Return

        Dim mc As System.Windows.Media.Color = New SolidColorBrush(newtAwayPantsStripe3Color.SelectedColor).Color
        Uniform_Img.Flip_One_Color(False, App_Constants.STOCK_PANTS_STRIPE_3_COLOR, System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B))

        newtAwayUniform.Source = Uniform_Img.getAwayUniform_Image
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

            Dim City_Abr As String = newtCityAbb.Background.GetHashCode
            Dim City As String = newtCity.Background.GetHashCode
            Dim Nickname As String = newtNickname.Background.GetHashCode

            Dim socks_color As String = newtSockColor.Background.GetHashCode
            Dim cleats_color As String = newtCleatsColor.Background.GetHashCode
            Dim helmet_color As String = newtHelmentColor.Background.GetHashCode
            Dim helmet_logo_color As String = newtHelmentLogoColor.Background.GetHashCode
            Dim helmet_facemask_color As String = newtFacemaskColor.Background.GetHashCode

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

            Dim away_Pants_Color As String = newtAwayPantsColor.Background.GetHashCode
            Dim away_Pants_Stripe_1_Color As String = newtAwayPantsStripe1Color.Background.GetHashCode
            Dim away_Pants_Stripe_2_Color As String = newtAwayPantsStripe2Color.Background.GetHashCode
            Dim away_Pants_Stripe_3_Color As String = newtAwayPantsStripe3Color.Background.GetHashCode


            stadium = New StadiumMdl(newtStadium.Text, newtStadiumLocation.Text,
                                    newtStadiumCapacity.Text, newtStadiumPath.Text)
            Footwear = New FootwearMdl(socks_color, cleats_color)
            Helmet = New HelmetMdl(helmet_color, helmet_logo_color,
                                helmet_facemask_color)
            Home_Jersey = New JerseyMdl(Home_Jersey_Color, Home_Sleeve_Color, Home_Shoulder_Stripe_Color,
                                Home_Jersey_Number_Color, Home_Jersey_Outline_Number_Color,
                                Home_Jersey_Sleeve_1_Color, Home_Jersey_Sleeve_2_Color,
                                Home_Jersey_Sleeve_3_Color, Home_Jersey_Sleeve_4_Color,
                                Home_Jersey_Sleeve_5_Color, Home_Jersey_Sleeve_6_Color)

            Home_Pants = New PantsMdl(Home_Pants_Color, Home_Pants_Stripe_1_Color,
                                Home_Pants_Stripe_2_Color, Home_Pants_Stripe_3_Color)

            Away_Jersey = New JerseyMdl(away_Jersey_Color, away_Sleeve_Color, away_Shoulder_Stripe_Color,
                                away_Jersey_Number_Color, away_Jersey_Outline_Number_Color,
                                away_Jersey_Sleeve_1_Color, away_Jersey_Sleeve_2_Color,
                                away_Jersey_Sleeve_3_Color, away_Jersey_Sleeve_4_Color,
                                away_Jersey_Sleeve_5_Color, away_Jersey_Sleeve_6_Color)

            Away_Pants = New PantsMdl(away_Pants_Color, away_Pants_Stripe_1_Color,
                                away_Pants_Stripe_2_Color, away_Pants_Stripe_3_Color)
            Uniform = New UniformMdl(Helmet, Home_Jersey, Away_Jersey, Home_Pants, Away_Pants, Footwear)

            new_t.setFields(City_Abr, City, Nickname, stadium, Uniform, newtHelmetImgPath.Text, Roster)

        Catch ex As Exception
            MessageBox.Show(CommonUtils.substr(ex.Message, 0, 100), "Error", MessageBoxButton.OK, MessageBoxImage.Error)
        End Try
    End Sub



End Class