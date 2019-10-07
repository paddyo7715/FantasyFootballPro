Imports System.Collections.ObjectModel
Imports System.IO
Imports System.Windows.Controls
Imports Microsoft.Win32
Imports log4net

Public Class NewTeamUC
    Public Enum form_func
        New_Team
        Stock_Team_New
        Stock_Team_Edit
        Update_Team
    End Enum
    'pw is the parent window mainwindow
    Private this_team As TeamMdl = Nothing

    Public Event backtoNewLeague As EventHandler
    Public Event backtoStockTeams As EventHandler

    '    Property Roster As List(Of PlayerMdl) = Nothing
    Property Uniform_Img As Uniform_Image

    Public Recent_ColorList = New ObservableCollection(Of Xceed.Wpf.Toolkit.ColorItem)()
    Public Standard_ColorList = New ObservableCollection(Of Xceed.Wpf.Toolkit.ColorItem)()

    Property Form_Function As form_func = Nothing

    Property Event_from_Code As Boolean = False
    Private Shared logger As ILog = LogManager.GetLogger("RollingFile")

    Public Sub New(ByVal this_team As TeamMdl, ByVal func As String)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Me.this_team = this_team

        Dim all_uniform_colors As List(Of String) = New List(Of String)

        Select Case func
            Case "New_League"
                Form_Function = form_func.New_Team
                lblTitle.Content = "New Team"
                newt1Add.Content = "Add"
            Case "New_Stock_Team"
                Form_Function = form_func.Stock_Team_New
                lblTitle.Content = "New Stock Team"
                newt1Add.Content = "Add"
            Case "Update_Stock_Team"
                Form_Function = form_func.Stock_Team_Edit
                lblTitle.Content = "Update Stock Team"
                newt1Add.Content = "Save"
                all_uniform_colors = Uniform.getAllColorList(this_team.Uniform)
        End Select

        'if we are editing a team then load the uniform colors in the recent colors
        If all_uniform_colors.Count > 0 Then
            For Each c In all_uniform_colors
                Dim color_val As System.Windows.Media.Color = CommonUtils.getColorfromHex(c)
                Dim color_name As String = color_val.ToString
                Dim possible_color_name As String = getColorName(color_name, newtHelmentColor.AvailableColors, newtHelmentColor.StandardColors)
                If Not IsNothing(possible_color_name) Then color_name = possible_color_name
                Recent_ColorList.add(New Xceed.Wpf.Toolkit.ColorItem(CommonUtils.getColorfromHex(c), color_name))
            Next
        End If

        'Create the standard color list and exclude the transparent color, since is causes problems
        For Each sc In newtHelmentColor.StandardColors
            If sc.Name = "Transparent" Then
                Continue For
            End If
            Standard_ColorList.add(New Xceed.Wpf.Toolkit.ColorItem(sc.Color, sc.Name))
        Next

        'Set the transparent less coloritem array to the stand color for each color picker
        newtHelmentColor.StandardColors = Standard_ColorList
        newtHelmentLogoColor.StandardColors = Standard_ColorList
        newtFacemaskColor.StandardColors = Standard_ColorList
        newtSockColor.StandardColors = Standard_ColorList
        newtCleatsColor.StandardColors = Standard_ColorList

        newtHomeJerseyColor.StandardColors = Standard_ColorList
        newtHomeSleeveColor.StandardColors = Standard_ColorList

        newtHomeJerseyNumberColor.StandardColors = Standard_ColorList
        newtHomeNumberOutlineColor.StandardColors = Standard_ColorList

        newtHomeShoulderStripeColor.StandardColors = Standard_ColorList

        newtHomeJerseySleeve1Color.StandardColors = Standard_ColorList
        newtHomeJerseySleeve2Color.StandardColors = Standard_ColorList
        newtHomeJerseySleeve3Color.StandardColors = Standard_ColorList
        newtHomeJerseySleeve4Color.StandardColors = Standard_ColorList
        newtHomeJerseySleeve5Color.StandardColors = Standard_ColorList
        newtHomeJerseySleeve6Color.StandardColors = Standard_ColorList

        newtHomePantsColor.StandardColors = Standard_ColorList
        newtHomePantsStripe1Color.StandardColors = Standard_ColorList
        newtHomePantsStripe2Color.StandardColors = Standard_ColorList
        newtHomePantsStripe3Color.StandardColors = Standard_ColorList

        newtAwayJerseyColor.StandardColors = Standard_ColorList
        newtAwaySleeveColor.StandardColors = Standard_ColorList

        newtAwayJerseyNumberColor.StandardColors = Standard_ColorList
        newtAwayNumberOutlineColor.StandardColors = Standard_ColorList

        newtAwayShoulderStripeColor.StandardColors = Standard_ColorList

        newtAwayJerseySleeve1Color.StandardColors = Standard_ColorList
        newtAwayJerseySleeve2Color.StandardColors = Standard_ColorList
        newtAwayJerseySleeve3Color.StandardColors = Standard_ColorList
        newtAwayJerseySleeve4Color.StandardColors = Standard_ColorList
        newtAwayJerseySleeve5Color.StandardColors = Standard_ColorList
        newtAwayJerseySleeve6Color.StandardColors = Standard_ColorList

        newtAwayPantsColor.StandardColors = Standard_ColorList
        newtAwayPantsStripe1Color.StandardColors = Standard_ColorList
        newtAwayPantsStripe2Color.StandardColors = Standard_ColorList
        newtAwayPantsStripe3Color.StandardColors = Standard_ColorList

        'For some reason, I couldn't set the recentcolors in xaml
        newtHelmentColor.RecentColors = Recent_ColorList
        newtHelmentLogoColor.RecentColors = Recent_ColorList
        newtFacemaskColor.RecentColors = Recent_ColorList
        newtSockColor.RecentColors = Recent_ColorList
        newtCleatsColor.RecentColors = Recent_ColorList

        newtHomeJerseyColor.RecentColors = Recent_ColorList
        newtHomeSleeveColor.RecentColors = Recent_ColorList

        newtHomeJerseyNumberColor.RecentColors = Recent_ColorList
        newtHomeNumberOutlineColor.RecentColors = Recent_ColorList

        newtHomeShoulderStripeColor.RecentColors = Recent_ColorList

        newtHomeJerseySleeve1Color.RecentColors = Recent_ColorList
        newtHomeJerseySleeve2Color.RecentColors = Recent_ColorList
        newtHomeJerseySleeve3Color.RecentColors = Recent_ColorList
        newtHomeJerseySleeve4Color.RecentColors = Recent_ColorList
        newtHomeJerseySleeve5Color.RecentColors = Recent_ColorList
        newtHomeJerseySleeve6Color.RecentColors = Recent_ColorList

        newtHomePantsColor.RecentColors = Recent_ColorList
        newtHomePantsStripe1Color.RecentColors = Recent_ColorList
        newtHomePantsStripe2Color.RecentColors = Recent_ColorList
        newtHomePantsStripe3Color.RecentColors = Recent_ColorList

        newtAwayJerseyColor.RecentColors = Recent_ColorList
        newtAwaySleeveColor.RecentColors = Recent_ColorList

        newtAwayJerseyNumberColor.RecentColors = Recent_ColorList
        newtAwayNumberOutlineColor.RecentColors = Recent_ColorList

        newtAwayShoulderStripeColor.RecentColors = Recent_ColorList

        newtAwayJerseySleeve1Color.RecentColors = Recent_ColorList
        newtAwayJerseySleeve2Color.RecentColors = Recent_ColorList
        newtAwayJerseySleeve3Color.RecentColors = Recent_ColorList
        newtAwayJerseySleeve4Color.RecentColors = Recent_ColorList
        newtAwayJerseySleeve5Color.RecentColors = Recent_ColorList
        newtAwayJerseySleeve6Color.RecentColors = Recent_ColorList

        newtAwayPantsColor.RecentColors = Recent_ColorList
        newtAwayPantsStripe1Color.RecentColors = Recent_ColorList
        newtAwayPantsStripe2Color.RecentColors = Recent_ColorList
        newtAwayPantsStripe3Color.RecentColors = Recent_ColorList

    End Sub
    Private Function getColorName(ByVal c As String,
                                  ByVal availableColors As ObservableCollection(Of Xceed.Wpf.Toolkit.ColorItem),
                                  ByVal standardColors As ObservableCollection(Of Xceed.Wpf.Toolkit.ColorItem)) As String
        Dim r As String = Nothing
        Dim bfound As Boolean = False

        For Each a In availableColors
            If a.Color.ToString = c Then
                r = a.Name
                bfound = True
                Exit For
            End If
        Next

        If Not bfound Then
            For Each s In standardColors
                If s.Color.ToString = c Then
                    r = s.Name
                    bfound = True
                    Exit For
                End If
            Next
        End If

        Return r

    End Function

    Public Sub setBaseUniform()

        Uniform_Img = New Uniform_Image(CommonUtils.getAppPath & Path.DirectorySeparatorChar & "Images" & Path.DirectorySeparatorChar & "blankUniform.png")

        'If you are editing a team then there is no need to set the images to grey
        If Me.Form_Function = form_func.New_Team Or Me.Form_Function = form_func.Stock_Team_New Then
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
        End If
        newtHomeUniform.Source = Uniform_Img.getHomeUniform_Image
        newtAwayUniform.Source = Uniform_Img.getAwayUniform_Image

    End Sub
    Public Sub setfields()

        Dim colorConverter As ColorConverter = New ColorConverter()
        Dim bc As BrushConverter = New BrushConverter()

        Dim mc As System.Windows.Media.Color = Nothing
        Dim helmetColor As System.Drawing.Color = Nothing
        Dim helmetLogoColor As System.Drawing.Color = Nothing
        Dim helmetFacemaskColor As System.Drawing.Color = Nothing
        Dim SocksColor As System.Drawing.Color = Nothing
        Dim CleatsColor As System.Drawing.Color = Nothing
        Dim HomeJerseyColor As System.Drawing.Color = Nothing
        Dim HomeJerseySleeveColor As System.Drawing.Color = Nothing
        Dim HomeJerseyShoulderLoopColor As System.Drawing.Color = Nothing
        Dim HomeJerseyNumberColor As System.Drawing.Color = Nothing
        Dim HomeJerseyNumberOutlineColor As System.Drawing.Color = Nothing
        Dim HomeJerseyStripe_1 As System.Drawing.Color = Nothing
        Dim HomeJerseyStripe_2 As System.Drawing.Color = Nothing
        Dim HomeJerseyStripe_3 As System.Drawing.Color = Nothing
        Dim HomeJerseyStripe_4 As System.Drawing.Color = Nothing
        Dim HomeJerseyStripe_5 As System.Drawing.Color = Nothing
        Dim HomeJerseyStripe_6 As System.Drawing.Color = Nothing
        Dim HomePantsColor As System.Drawing.Color = Nothing
        Dim HomePants_Stripe_1 As System.Drawing.Color = Nothing
        Dim HomePants_Stripe_2 As System.Drawing.Color = Nothing
        Dim HomePants_Stripe_3 As System.Drawing.Color = Nothing

        Dim AwayJerseyColor As System.Drawing.Color = Nothing
        Dim AwayJerseySleeveColor As System.Drawing.Color = Nothing
        Dim AwayJerseyShoulderLoopColor As System.Drawing.Color = Nothing
        Dim AwayJerseyNumberColor As System.Drawing.Color = Nothing
        Dim AwayJerseyNumberOutlineColor As System.Drawing.Color = Nothing
        Dim AwayJerseyStripe_1 As System.Drawing.Color = Nothing
        Dim AwayJerseyStripe_2 As System.Drawing.Color = Nothing
        Dim AwayJerseyStripe_3 As System.Drawing.Color = Nothing
        Dim AwayJerseyStripe_4 As System.Drawing.Color = Nothing
        Dim AwayJerseyStripe_5 As System.Drawing.Color = Nothing
        Dim AwayJerseyStripe_6 As System.Drawing.Color = Nothing
        Dim AwayPantsColor As System.Drawing.Color = Nothing
        Dim AwayPants_Stripe_1 As System.Drawing.Color = Nothing
        Dim AwayPants_Stripe_2 As System.Drawing.Color = Nothing
        Dim AwayPants_Stripe_3 As System.Drawing.Color = Nothing

        Dim stadium_img_path As String = ""
        Dim helmet_img_path As String = ""

        logger.Debug("Basic team info Controls set")

        newtCityAbb.Text = this_team.City_Abr
        newtCity.Text = this_team.City
        newtNickname.Text = this_team.Nickname
        newtHelmetImgPath.Text = helmet_img_path

        If Not IsNothing(this_team.Stadium) Then
            If this_team.Stadium.Stadium_Img_Path = Path.GetFileName(this_team.Stadium.Stadium_Img_Path) Then
                Dim init_folder As String = CommonUtils.getAppPath
                init_folder += Path.DirectorySeparatorChar & "Images" & Path.DirectorySeparatorChar & "Stadiums"
                stadium_img_path = init_folder + Path.DirectorySeparatorChar + Path.GetFileName(this_team.Stadium.Stadium_Img_Path)
            Else
                stadium_img_path = this_team.Stadium.Stadium_Img_Path
            End If

            logger.Debug("Setting stadium image control " & stadium_img_path)

            newtStadium.Text = this_team.Stadium.Stadium_Name
            newtStadiumLocation.Text = this_team.Stadium.Stadium_Location
            newtStadiumPath.Text = stadium_img_path
            Stadium_image.Source = New BitmapImage(New Uri(stadium_img_path))
            newtStadiumCapacity.Text = this_team.Stadium.Capacity
            newl1FieldType.SelectedIndex = this_team.Stadium.Field_Type - 1
            newl1FieldColor.SelectedColor = CommonUtils.getColorfromHex(this_team.Stadium.Field_Color)
        End If

        If Not IsNothing(this_team.Helmet_img_path) AndAlso this_team.Helmet_img_path.Length > 0 Then
            If this_team.Helmet_img_path = Path.GetFileName(this_team.Helmet_img_path) Then
                Dim init_folder As String = CommonUtils.getAppPath
                init_folder += Path.DirectorySeparatorChar & "Images" & Path.DirectorySeparatorChar & "Helmets"
                helmet_img_path = init_folder + Path.DirectorySeparatorChar + Path.GetFileName(this_team.Helmet_img_path)
            Else
                helmet_img_path = this_team.Helmet_img_path
            End If

            logger.Debug("Setting helmet image control " & helmet_img_path)

            newtHelmetImgPath.Text = helmet_img_path
            Helmet_image.Source = New BitmapImage(New Uri(helmet_img_path))
        End If

        If Not IsNothing(this_team.Uniform) Then
            logger.Debug("Setting uniform image controls")

            newtHelmentColor.SelectedColor = CommonUtils.getColorfromHex(this_team.Uniform.Helmet.Helmet_Color)
            newtHelmentLogoColor.SelectedColor = CommonUtils.getColorfromHex(this_team.Uniform.Helmet.Helmet_Logo_Color)
            newtFacemaskColor.SelectedColor = CommonUtils.getColorfromHex(this_team.Uniform.Helmet.Helmet_Facemask_Color)

            newtSockColor.SelectedColor = CommonUtils.getColorfromHex(this_team.Uniform.Footwear.Socks_Color)
            newtCleatsColor.SelectedColor = CommonUtils.getColorfromHex(this_team.Uniform.Footwear.Cleats_Color)

            newtHomeJerseyColor.SelectedColor = CommonUtils.getColorfromHex(this_team.Uniform.Home_Jersey.Jersey_Color)
            newtHomeSleeveColor.SelectedColor = CommonUtils.getColorfromHex(this_team.Uniform.Home_Jersey.Sleeve_Color)
            newtHomeShoulderStripeColor.SelectedColor = CommonUtils.getColorfromHex(this_team.Uniform.Home_Jersey.Shoulder_Stripe_Color)
            newtHomeJerseyNumberColor.SelectedColor = CommonUtils.getColorfromHex(this_team.Uniform.Home_Jersey.Number_Color)
            newtHomeNumberOutlineColor.SelectedColor = CommonUtils.getColorfromHex(this_team.Uniform.Home_Jersey.Number_Outline_Color)
            newtHomeJerseySleeve1Color.SelectedColor = CommonUtils.getColorfromHex(this_team.Uniform.Home_Jersey.Sleeve_Stripe1)
            newtHomeJerseySleeve2Color.SelectedColor = CommonUtils.getColorfromHex(this_team.Uniform.Home_Jersey.Sleeve_Stripe2)
            newtHomeJerseySleeve3Color.SelectedColor = CommonUtils.getColorfromHex(this_team.Uniform.Home_Jersey.Sleeve_Stripe3)
            newtHomeJerseySleeve4Color.SelectedColor = CommonUtils.getColorfromHex(this_team.Uniform.Home_Jersey.Sleeve_Stripe4)
            newtHomeJerseySleeve5Color.SelectedColor = CommonUtils.getColorfromHex(this_team.Uniform.Home_Jersey.Sleeve_Stripe5)
            newtHomeJerseySleeve6Color.SelectedColor = CommonUtils.getColorfromHex(this_team.Uniform.Home_Jersey.Sleeve_Stripe6)

            newtHomePantsColor.SelectedColor = CommonUtils.getColorfromHex(this_team.Uniform.Home_Pants.Pants_Color)
            newtHomePantsStripe1Color.SelectedColor = CommonUtils.getColorfromHex(this_team.Uniform.Home_Pants.Stripe_Color_1)
            newtHomePantsStripe2Color.SelectedColor = CommonUtils.getColorfromHex(this_team.Uniform.Home_Pants.Stripe_Color_2)
            newtHomePantsStripe3Color.SelectedColor = CommonUtils.getColorfromHex(this_team.Uniform.Home_Pants.Stripe_Color_3)

            newtAwayJerseyColor.SelectedColor = CommonUtils.getColorfromHex(this_team.Uniform.Away_Jersey.Jersey_Color)
            newtAwaySleeveColor.SelectedColor = CommonUtils.getColorfromHex(this_team.Uniform.Away_Jersey.Sleeve_Color)
            newtAwayShoulderStripeColor.SelectedColor = CommonUtils.getColorfromHex(this_team.Uniform.Away_Jersey.Shoulder_Stripe_Color)
            newtAwayJerseyNumberColor.SelectedColor = CommonUtils.getColorfromHex(this_team.Uniform.Away_Jersey.Number_Color)
            newtAwayNumberOutlineColor.SelectedColor = CommonUtils.getColorfromHex(this_team.Uniform.Away_Jersey.Number_Outline_Color)
            newtAwayJerseySleeve1Color.SelectedColor = CommonUtils.getColorfromHex(this_team.Uniform.Away_Jersey.Sleeve_Stripe1)
            newtAwayJerseySleeve2Color.SelectedColor = CommonUtils.getColorfromHex(this_team.Uniform.Away_Jersey.Sleeve_Stripe2)
            newtAwayJerseySleeve3Color.SelectedColor = CommonUtils.getColorfromHex(this_team.Uniform.Away_Jersey.Sleeve_Stripe3)
            newtAwayJerseySleeve4Color.SelectedColor = CommonUtils.getColorfromHex(this_team.Uniform.Away_Jersey.Sleeve_Stripe4)
            newtAwayJerseySleeve5Color.SelectedColor = CommonUtils.getColorfromHex(this_team.Uniform.Away_Jersey.Sleeve_Stripe5)
            newtAwayJerseySleeve6Color.SelectedColor = CommonUtils.getColorfromHex(this_team.Uniform.Away_Jersey.Sleeve_Stripe6)

            newtAwayPantsColor.SelectedColor = CommonUtils.getColorfromHex(this_team.Uniform.Away_Pants.Pants_Color)
            newtAwayPantsStripe1Color.SelectedColor = CommonUtils.getColorfromHex(this_team.Uniform.Away_Pants.Stripe_Color_1)
            newtAwayPantsStripe2Color.SelectedColor = CommonUtils.getColorfromHex(this_team.Uniform.Away_Pants.Stripe_Color_2)
            newtAwayPantsStripe3Color.SelectedColor = CommonUtils.getColorfromHex(this_team.Uniform.Away_Pants.Stripe_Color_3)

            mc = New SolidColorBrush(newtHelmentColor.SelectedColor).Color
            helmetColor = System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B)

            mc = New SolidColorBrush(newtHelmentLogoColor.SelectedColor).Color
            helmetLogoColor = System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B)

            mc = New SolidColorBrush(newtFacemaskColor.SelectedColor).Color
            helmetFacemaskColor = System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B)

            mc = New SolidColorBrush(newtSockColor.SelectedColor).Color
            SocksColor = System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B)

            mc = New SolidColorBrush(newtCleatsColor.SelectedColor).Color
            CleatsColor = System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B)

            mc = New SolidColorBrush(newtHomeJerseyColor.SelectedColor).Color
            HomeJerseyColor = System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B)

            mc = New SolidColorBrush(newtHomeSleeveColor.SelectedColor).Color
            HomeJerseySleeveColor = System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B)

            mc = New SolidColorBrush(newtHomeShoulderStripeColor.SelectedColor).Color
            HomeJerseyShoulderLoopColor = System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B)

            mc = New SolidColorBrush(newtHomeJerseyNumberColor.SelectedColor).Color
            HomeJerseyNumberColor = System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B)

            mc = New SolidColorBrush(newtHomeNumberOutlineColor.SelectedColor).Color
            HomeJerseyNumberOutlineColor = System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B)

            mc = New SolidColorBrush(newtHomeJerseySleeve1Color.SelectedColor).Color
            HomeJerseyStripe_1 = System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B)

            mc = New SolidColorBrush(newtHomeJerseySleeve2Color.SelectedColor).Color
            HomeJerseyStripe_2 = System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B)

            mc = New SolidColorBrush(newtHomeJerseySleeve3Color.SelectedColor).Color
            HomeJerseyStripe_3 = System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B)

            mc = New SolidColorBrush(newtHomeJerseySleeve4Color.SelectedColor).Color
            HomeJerseyStripe_4 = System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B)

            mc = New SolidColorBrush(newtHomeJerseySleeve5Color.SelectedColor).Color
            HomeJerseyStripe_5 = System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B)

            mc = New SolidColorBrush(newtHomeJerseySleeve6Color.SelectedColor).Color
            HomeJerseyStripe_6 = System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B)

            mc = New SolidColorBrush(newtHomePantsColor.SelectedColor).Color
            HomePantsColor = System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B)

            mc = New SolidColorBrush(newtHomePantsStripe1Color.SelectedColor).Color
            HomePants_Stripe_1 = System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B)

            mc = New SolidColorBrush(newtHomePantsStripe2Color.SelectedColor).Color
            HomePants_Stripe_2 = System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B)

            mc = New SolidColorBrush(newtHomePantsStripe3Color.SelectedColor).Color
            HomePants_Stripe_3 = System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B)

            Uniform_Img.Flip_All_Colors(True, helmetColor, helmetFacemaskColor, helmetLogoColor, HomeJerseyColor,
            HomeJerseyNumberColor, HomeJerseyNumberOutlineColor,
            HomeJerseySleeveColor, HomeJerseyShoulderLoopColor,
            HomeJerseyStripe_1, HomeJerseyStripe_2,
            HomeJerseyStripe_3, HomeJerseyStripe_4,
            HomeJerseyStripe_5, HomeJerseyStripe_6,
            HomePantsColor,
            HomePants_Stripe_1,
            HomePants_Stripe_2,
            HomePants_Stripe_3,
            SocksColor, CleatsColor)

            newtHomeUniform.Source = Uniform_Img.getHomeUniform_Image

            mc = New SolidColorBrush(newtAwayJerseyColor.SelectedColor).Color
            AwayJerseyColor = System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B)

            mc = New SolidColorBrush(newtAwaySleeveColor.SelectedColor).Color
            AwayJerseySleeveColor = System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B)

            mc = New SolidColorBrush(newtAwayShoulderStripeColor.SelectedColor).Color
            AwayJerseyShoulderLoopColor = System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B)

            mc = New SolidColorBrush(newtAwayJerseyNumberColor.SelectedColor).Color
            AwayJerseyNumberColor = System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B)

            mc = New SolidColorBrush(newtAwayNumberOutlineColor.SelectedColor).Color
            AwayJerseyNumberOutlineColor = System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B)

            mc = New SolidColorBrush(newtAwayJerseySleeve1Color.SelectedColor).Color
            AwayJerseyStripe_1 = System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B)

            mc = New SolidColorBrush(newtAwayJerseySleeve2Color.SelectedColor).Color
            AwayJerseyStripe_2 = System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B)

            mc = New SolidColorBrush(newtAwayJerseySleeve3Color.SelectedColor).Color
            AwayJerseyStripe_3 = System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B)

            mc = New SolidColorBrush(newtAwayJerseySleeve4Color.SelectedColor).Color
            AwayJerseyStripe_4 = System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B)

            mc = New SolidColorBrush(newtAwayJerseySleeve5Color.SelectedColor).Color
            AwayJerseyStripe_5 = System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B)

            mc = New SolidColorBrush(newtAwayJerseySleeve6Color.SelectedColor).Color
            AwayJerseyStripe_6 = System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B)

            mc = New SolidColorBrush(newtAwayPantsColor.SelectedColor).Color
            AwayPantsColor = System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B)

            mc = New SolidColorBrush(newtAwayPantsStripe1Color.SelectedColor).Color
            AwayPants_Stripe_1 = System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B)

            mc = New SolidColorBrush(newtAwayPantsStripe2Color.SelectedColor).Color
            AwayPants_Stripe_2 = System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B)

            mc = New SolidColorBrush(newtAwayPantsStripe3Color.SelectedColor).Color
            AwayPants_Stripe_3 = System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B)

            Uniform_Img.Flip_All_Colors(True, helmetColor, helmetFacemaskColor, helmetLogoColor, AwayJerseyColor,
            AwayJerseyNumberColor, AwayJerseyNumberOutlineColor,
            AwayJerseySleeveColor, AwayJerseyShoulderLoopColor,
            AwayJerseyStripe_1, AwayJerseyStripe_2,
            AwayJerseyStripe_3, AwayJerseyStripe_4,
            AwayJerseyStripe_5, AwayJerseyStripe_6,
            AwayPantsColor,
            AwayPants_Stripe_1,
            AwayPants_Stripe_2,
            AwayPants_Stripe_3,
            SocksColor, CleatsColor)

            newtAwayUniform.Source = Uniform_Img.getAwayUniform_Image
        End If

    End Sub
    Private Sub newtHelmentLogoColor_SelectedColorChanged(sender As Object, e As RoutedPropertyChangedEventArgs(Of Windows.Media.Color?))

        If Event_from_Code Then Return

        setHomeUniform()
        setAwayUniform()

    End Sub
    Private Sub newtFacemaskColor_SelectedColorChanged(sender As Object, e As RoutedPropertyChangedEventArgs(Of Windows.Media.Color?))

        If Event_from_Code Then Return

        setHomeUniform()
        setAwayUniform()

    End Sub
    Private Sub newtSockColor_SelectedColorChanged(sender As Object, e As RoutedPropertyChangedEventArgs(Of Windows.Media.Color?))

        If Event_from_Code Then Return

        setHomeUniform()
        setAwayUniform()

    End Sub
    Private Sub newtCleatsColor_SelectedColorChanged(sender As Object, e As RoutedPropertyChangedEventArgs(Of Windows.Media.Color?))

        If Event_from_Code Then Return

        setHomeUniform()
        setAwayUniform()

    End Sub
    Private Sub newtHelmentColor_SelectedColorChanged(sender As Object, e As RoutedPropertyChangedEventArgs(Of Windows.Media.Color?))

        If Event_from_Code Then Return

        setHomeUniform()
        setAwayUniform()

    End Sub
    Private Sub newtHomeJerseyColor_SelectedColorChanged(sender As Object, e As RoutedPropertyChangedEventArgs(Of Color?))

        If Event_from_Code Then Return

        Event_from_Code = True
        newtHomeSleeveColor.SelectedColor = newtHomeJerseyColor.SelectedColor
        newtHomeShoulderStripeColor.SelectedColor = newtHomeJerseyColor.SelectedColor
        newtHomeJerseySleeve1Color.SelectedColor = newtHomeSleeveColor.SelectedColor
        newtHomeJerseySleeve2Color.SelectedColor = newtHomeSleeveColor.SelectedColor
        newtHomeJerseySleeve3Color.SelectedColor = newtHomeSleeveColor.SelectedColor
        newtHomeJerseySleeve4Color.SelectedColor = newtHomeSleeveColor.SelectedColor
        newtHomeJerseySleeve5Color.SelectedColor = newtHomeSleeveColor.SelectedColor
        newtHomeJerseySleeve6Color.SelectedColor = newtHomeSleeveColor.SelectedColor

        setHomeUniform()

        Event_from_Code = False

    End Sub
    Private Sub newtHomeShoulderStripeColor_SelectedColorChanged(sender As Object, e As RoutedPropertyChangedEventArgs(Of Color?))

        If Event_from_Code Then Return

        setHomeUniform()

    End Sub

    Private Sub newtHomeNumberOutlineColor_SelectedColorChanged(sender As Object, e As RoutedPropertyChangedEventArgs(Of Color?))

        If Event_from_Code Then Return

        setHomeUniform()

    End Sub

    Private Sub newtHomeJerseyNumberColor_SelectedColorChanged(sender As Object, e As RoutedPropertyChangedEventArgs(Of Color?))

        If Event_from_Code Then Return

        setHomeUniform()

    End Sub
    Private Sub newtHomeSleeveColor_SelectedColorChanged(sender As Object, e As RoutedPropertyChangedEventArgs(Of Windows.Media.Color?))

        If Event_from_Code Then Return

        Event_from_Code = True
        newtHomeJerseySleeve1Color.SelectedColor = newtHomeSleeveColor.SelectedColor
        newtHomeJerseySleeve2Color.SelectedColor = newtHomeSleeveColor.SelectedColor
        newtHomeJerseySleeve3Color.SelectedColor = newtHomeSleeveColor.SelectedColor
        newtHomeJerseySleeve4Color.SelectedColor = newtHomeSleeveColor.SelectedColor
        newtHomeJerseySleeve5Color.SelectedColor = newtHomeSleeveColor.SelectedColor
        newtHomeJerseySleeve6Color.SelectedColor = newtHomeSleeveColor.SelectedColor

        setHomeUniform()

        Event_from_Code = False

    End Sub
    Private Sub newtHomeJerseySleeve1Color_SelectedColorChanged(sender As Object, e As RoutedPropertyChangedEventArgs(Of Color?))

        If Event_from_Code Then Return

        setHomeUniform()

    End Sub

    Private Sub newtHomeJerseySleeve2Color_SelectedColorChanged(sender As Object, e As RoutedPropertyChangedEventArgs(Of Color?))

        If Event_from_Code Then Return

        setHomeUniform()

    End Sub

    Private Sub newtHomeJerseySleeve3Color_SelectedColorChanged(sender As Object, e As RoutedPropertyChangedEventArgs(Of Color?))

        If Event_from_Code Then Return

        setHomeUniform()

    End Sub

    Private Sub newtHomeJerseySleeve4Color_SelectedColorChanged(sender As Object, e As RoutedPropertyChangedEventArgs(Of Color?))

        If Event_from_Code Then Return

        setHomeUniform()

    End Sub

    Private Sub newtHomeJerseySleeve5Color_SelectedColorChanged(sender As Object, e As RoutedPropertyChangedEventArgs(Of Color?))

        If Event_from_Code Then Return

        setHomeUniform()

    End Sub

    Private Sub newtHomeJerseySleeve6Color_SelectedColorChanged(sender As Object, e As RoutedPropertyChangedEventArgs(Of Color?))

        If Event_from_Code Then Return

        setHomeUniform()

    End Sub
    Private Sub newtHomePantsColor_SelectedColorChanged(sender As Object, e As RoutedPropertyChangedEventArgs(Of Windows.Media.Color?))

        If Event_from_Code Then Return

        Event_from_Code = True
        newtHomePantsStripe1Color.SelectedColor = newtHomePantsColor.SelectedColor
        newtHomePantsStripe2Color.SelectedColor = newtHomePantsColor.SelectedColor
        newtHomePantsStripe3Color.SelectedColor = newtHomePantsColor.SelectedColor

        setHomeUniform()

        Event_from_Code = False
    End Sub
    Private Sub newtHomePantsStripe1Color_SelectedColorChanged(sender As Object, e As RoutedPropertyChangedEventArgs(Of Windows.Media.Color?))

        If Event_from_Code Then Return

        setHomeUniform()

    End Sub
    Private Sub newtHomePantsStripe2Color_SelectedColorChanged(sender As Object, e As RoutedPropertyChangedEventArgs(Of Windows.Media.Color?))

        If Event_from_Code Then Return

        setHomeUniform()

    End Sub
    Private Sub newtHomePantsStripe3Color_SelectedColorChanged(sender As Object, e As RoutedPropertyChangedEventArgs(Of Windows.Media.Color?))

        If Event_from_Code Then Return

        setHomeUniform()

    End Sub
    Private Sub newtAwayJerseyColor_SelectedColorChanged(sender As Object, e As RoutedPropertyChangedEventArgs(Of Color?))

        If Event_from_Code Then Return

        Event_from_Code = True
        newtAwaySleeveColor.SelectedColor = newtAwayJerseyColor.SelectedColor
        newtAwayShoulderStripeColor.SelectedColor = newtAwayJerseyColor.SelectedColor
        newtAwayJerseySleeve1Color.SelectedColor = newtAwaySleeveColor.SelectedColor
        newtAwayJerseySleeve2Color.SelectedColor = newtAwaySleeveColor.SelectedColor
        newtAwayJerseySleeve3Color.SelectedColor = newtAwaySleeveColor.SelectedColor
        newtAwayJerseySleeve4Color.SelectedColor = newtAwaySleeveColor.SelectedColor
        newtAwayJerseySleeve5Color.SelectedColor = newtAwaySleeveColor.SelectedColor
        newtAwayJerseySleeve6Color.SelectedColor = newtAwaySleeveColor.SelectedColor

        setAwayUniform()
        Event_from_Code = False

    End Sub
    Private Sub newtAwayShoulderStripeColor_SelectedColorChanged(sender As Object, e As RoutedPropertyChangedEventArgs(Of Color?))

        If Event_from_Code Then Return

        setAwayUniform()
    End Sub

    Private Sub newtAwayNumberOutlineColor_SelectedColorChanged(sender As Object, e As RoutedPropertyChangedEventArgs(Of Color?))

        If Event_from_Code Then Return

        setAwayUniform()

    End Sub

    Private Sub newtAwayJerseyNumberColor_SelectedColorChanged(sender As Object, e As RoutedPropertyChangedEventArgs(Of Color?))

        If Event_from_Code Then Return

        setAwayUniform()
    End Sub
    Private Sub newtAwaySleeveColor_SelectedColorChanged(sender As Object, e As RoutedPropertyChangedEventArgs(Of Windows.Media.Color?))

        If Event_from_Code Then Return

        Event_from_Code = True
        newtAwayJerseySleeve1Color.SelectedColor = newtAwaySleeveColor.SelectedColor
        newtAwayJerseySleeve2Color.SelectedColor = newtAwaySleeveColor.SelectedColor
        newtAwayJerseySleeve3Color.SelectedColor = newtAwaySleeveColor.SelectedColor
        newtAwayJerseySleeve4Color.SelectedColor = newtAwaySleeveColor.SelectedColor
        newtAwayJerseySleeve5Color.SelectedColor = newtAwaySleeveColor.SelectedColor
        newtAwayJerseySleeve6Color.SelectedColor = newtAwaySleeveColor.SelectedColor

        setAwayUniform()
        Event_from_Code = False

    End Sub
    Private Sub newtAwayJerseySleeve1Color_SelectedColorChanged(sender As Object, e As RoutedPropertyChangedEventArgs(Of Color?))

        If Event_from_Code Then Return

        setAwayUniform()

    End Sub

    Private Sub newtAwayJerseySleeve2Color_SelectedColorChanged(sender As Object, e As RoutedPropertyChangedEventArgs(Of Color?))

        If Event_from_Code Then Return

        setAwayUniform()

    End Sub

    Private Sub newtAwayJerseySleeve3Color_SelectedColorChanged(sender As Object, e As RoutedPropertyChangedEventArgs(Of Color?))

        If Event_from_Code Then Return

        setAwayUniform()

    End Sub

    Private Sub newtAwayJerseySleeve4Color_SelectedColorChanged(sender As Object, e As RoutedPropertyChangedEventArgs(Of Color?))

        If Event_from_Code Then Return

        setAwayUniform()

    End Sub

    Private Sub newtAwayJerseySleeve5Color_SelectedColorChanged(sender As Object, e As RoutedPropertyChangedEventArgs(Of Color?))

        If Event_from_Code Then Return

        setAwayUniform()
    End Sub

    Private Sub newtAwayJerseySleeve6Color_SelectedColorChanged(sender As Object, e As RoutedPropertyChangedEventArgs(Of Color?))

        If Event_from_Code Then Return

        setAwayUniform()
    End Sub
    Private Sub newtAwayPantsColor_SelectedColorChanged(sender As Object, e As RoutedPropertyChangedEventArgs(Of Windows.Media.Color?))

        If Event_from_Code Then Return

        Event_from_Code = True
        newtAwayPantsStripe1Color.SelectedColor = newtAwayPantsColor.SelectedColor
        newtAwayPantsStripe2Color.SelectedColor = newtAwayPantsColor.SelectedColor
        newtAwayPantsStripe3Color.SelectedColor = newtAwayPantsColor.SelectedColor

        setAwayUniform()
        Event_from_Code = False
    End Sub
    Private Sub newtAwayPantsStripe1Color_SelectedColorChanged(sender As Object, e As RoutedPropertyChangedEventArgs(Of Windows.Media.Color?))

        If Event_from_Code Then Return

        setAwayUniform()

    End Sub
    Private Sub newtAwayPantsStripe2Color_SelectedColorChanged(sender As Object, e As RoutedPropertyChangedEventArgs(Of Windows.Media.Color?))

        If Event_from_Code Then Return

        setAwayUniform()
    End Sub
    Private Sub newtAwayPantsStripe3Color_SelectedColorChanged(sender As Object, e As RoutedPropertyChangedEventArgs(Of Windows.Media.Color?))

        If Event_from_Code Then Return

        setAwayUniform()

    End Sub
    Private Sub newt1Cancel_Click(sender As Object, e As RoutedEventArgs) Handles newt1Cancel.Click

        Select Case Form_Function
            Case form_func.New_Team
                RaiseEvent backtoNewLeague(Me, New TeamUpdatedEventArgs(False))
            Case form_func.Stock_Team_New
                RaiseEvent backtoStockTeams(Me, New EventArgs)
            Case form_func.Stock_Team_Edit
                RaiseEvent backtoStockTeams(Me, New EventArgs)

        End Select

    End Sub

    Private Sub newtbtnHelmetImgPath_Click(sender As Object, e As RoutedEventArgs) Handles newtbtnHelmetImgPath.Click
        Dim OpenFileDialog As OpenFileDialog = New OpenFileDialog()
        Dim init_folder As String = CommonUtils.getAppPath
        init_folder += Path.DirectorySeparatorChar & "Images" & Path.DirectorySeparatorChar & "Helmets"

        OpenFileDialog.InitialDirectory = init_folder
        OpenFileDialog.Multiselect = False
        OpenFileDialog.Filter = "Image Files|*.jpg;*.gif;*.png;*.bmp"

        If (OpenFileDialog.ShowDialog() = True) Then
            Dim filepath As String = OpenFileDialog.FileName
            newtHelmetImgPath.Text = filepath
            Helmet_image.Source = New BitmapImage(New Uri(filepath))
        End If
    End Sub
    Private Sub newtbtnStadiumPath_Click(sender As Object, e As RoutedEventArgs) Handles newtbtnStadiumPath.Click
        Dim OpenFileDialog As OpenFileDialog = New OpenFileDialog()
        Dim init_folder As String = CommonUtils.getAppPath
        init_folder += Path.DirectorySeparatorChar & "Images" & Path.DirectorySeparatorChar & "Stadiums"

        OpenFileDialog.InitialDirectory = init_folder
        OpenFileDialog.Multiselect = False
        OpenFileDialog.Filter = "Image Files|*.jpg;*.gif;*.png;*.bmp"

        If (OpenFileDialog.ShowDialog() = True) Then
            Dim filepath As String = OpenFileDialog.FileName
            newtStadiumPath.Text = filepath
            Stadium_image.Source = New BitmapImage(New Uri(filepath))
        End If
    End Sub


    Private Sub newt1Add_Click(sender As Object, e As RoutedEventArgs) Handles newt1Add.Click
        Try
            Dim stadium As StadiumMdl = Nothing
            Dim Footwear As FootwearMdl = Nothing
            Dim Helmet As HelmetMdl = Nothing
            Dim Home_Jersey As JerseyMdl = Nothing
            Dim Home_Pants As PantsMdl = Nothing
            Dim Away_Jersey As JerseyMdl = Nothing
            Dim Away_Pants As PantsMdl = Nothing
            Dim Uniform As UniformMdl = Nothing
            Dim Stadium_Field_Color As String = Nothing
            Dim socks_color As String = Nothing
            Dim cleats_color As String = Nothing
            Dim helmet_color As String = Nothing
            Dim helmet_logo_color As String = Nothing
            Dim helmet_facemask_color As String = Nothing
            Dim Home_Jersey_Color As String = Nothing
            Dim Home_Sleeve_Color As String = Nothing
            Dim Home_Shoulder_Stripe_Color As String = Nothing
            Dim Home_Jersey_Number_Color As String = Nothing
            Dim Home_Jersey_Outline_Number_Color As String = Nothing
            Dim Home_Jersey_Sleeve_1_Color As String = Nothing
            Dim Home_Jersey_Sleeve_2_Color As String = Nothing
            Dim Home_Jersey_Sleeve_3_Color As String = Nothing
            Dim Home_Jersey_Sleeve_4_Color As String = Nothing
            Dim Home_Jersey_Sleeve_5_Color As String = Nothing
            Dim Home_Jersey_Sleeve_6_Color As String = Nothing
            Dim Home_Pants_Color As String = Nothing
            Dim Home_Pants_Stripe_1_Color As String = Nothing
            Dim Home_Pants_Stripe_2_Color As String = Nothing
            Dim Home_Pants_Stripe_3_Color As String = Nothing

            Dim Away_Jersey_Color As String = Nothing
            Dim Away_Sleeve_Color As String = Nothing
            Dim Away_Shoulder_Stripe_Color As String = Nothing
            Dim Away_Jersey_Number_Color As String = Nothing
            Dim Away_Jersey_Outline_Number_Color As String = Nothing
            Dim Away_Jersey_Sleeve_1_Color As String = Nothing
            Dim Away_Jersey_Sleeve_2_Color As String = Nothing
            Dim Away_Jersey_Sleeve_3_Color As String = Nothing
            Dim Away_Jersey_Sleeve_4_Color As String = Nothing
            Dim Away_Jersey_Sleeve_5_Color As String = Nothing
            Dim Away_Jersey_Sleeve_6_Color As String = Nothing
            Dim Away_Pants_Color As String = Nothing
            Dim Away_Pants_Stripe_1_Color As String = Nothing
            Dim Away_Pants_Stripe_2_Color As String = Nothing
            Dim Away_Pants_Stripe_3_Color As String = Nothing

            Dim City_Abr As String = newtCityAbb.Text.Trim
            Dim City As String = newtCity.Text.Trim
            Dim Nickname As String = newtNickname.Text.Trim

            logger.Debug("Just before team validation")
            Validate()
            logger.Debug("Team validated")

            socks_color = CommonUtils.getHexfromColor(New SolidColorBrush(newtSockColor.SelectedColor).Color)
            cleats_color = CommonUtils.getHexfromColor(New SolidColorBrush(newtCleatsColor.SelectedColor).Color)

            helmet_color = CommonUtils.getHexfromColor(New SolidColorBrush(newtHelmentColor.SelectedColor).Color)
            helmet_logo_color = CommonUtils.getHexfromColor(New SolidColorBrush(newtHelmentLogoColor.SelectedColor).Color)
            helmet_facemask_color = CommonUtils.getHexfromColor(New SolidColorBrush(newtFacemaskColor.SelectedColor).Color)

            Home_Jersey_Color = CommonUtils.getHexfromColor(New SolidColorBrush(newtHomeJerseyColor.SelectedColor).Color)
            Home_Sleeve_Color = CommonUtils.getHexfromColor(New SolidColorBrush(newtHomeSleeveColor.SelectedColor).Color)
            Home_Shoulder_Stripe_Color = CommonUtils.getHexfromColor(New SolidColorBrush(newtHomeShoulderStripeColor.SelectedColor).Color)
            Home_Jersey_Number_Color = CommonUtils.getHexfromColor(New SolidColorBrush(newtHomeJerseyNumberColor.SelectedColor).Color)
            Home_Jersey_Outline_Number_Color = CommonUtils.getHexfromColor(New SolidColorBrush(newtHomeNumberOutlineColor.SelectedColor).Color)
            Home_Jersey_Sleeve_1_Color = CommonUtils.getHexfromColor(New SolidColorBrush(newtHomeJerseySleeve1Color.SelectedColor).Color)
            Home_Jersey_Sleeve_2_Color = CommonUtils.getHexfromColor(New SolidColorBrush(newtHomeJerseySleeve2Color.SelectedColor).Color)
            Home_Jersey_Sleeve_3_Color = CommonUtils.getHexfromColor(New SolidColorBrush(newtHomeJerseySleeve3Color.SelectedColor).Color)
            Home_Jersey_Sleeve_4_Color = CommonUtils.getHexfromColor(New SolidColorBrush(newtHomeJerseySleeve4Color.SelectedColor).Color)
            Home_Jersey_Sleeve_5_Color = CommonUtils.getHexfromColor(New SolidColorBrush(newtHomeJerseySleeve5Color.SelectedColor).Color)
            Home_Jersey_Sleeve_6_Color = CommonUtils.getHexfromColor(New SolidColorBrush(newtHomeJerseySleeve6Color.SelectedColor).Color)
            Home_Pants_Color = CommonUtils.getHexfromColor(New SolidColorBrush(newtHomePantsColor.SelectedColor).Color)
            Home_Pants_Stripe_1_Color = CommonUtils.getHexfromColor(New SolidColorBrush(newtHomePantsStripe1Color.SelectedColor).Color)
            Home_Pants_Stripe_2_Color = CommonUtils.getHexfromColor(New SolidColorBrush(newtHomePantsStripe2Color.SelectedColor).Color)
            Home_Pants_Stripe_3_Color = CommonUtils.getHexfromColor(New SolidColorBrush(newtHomePantsStripe3Color.SelectedColor).Color)

            Away_Jersey_Color = CommonUtils.getHexfromColor(New SolidColorBrush(newtAwayJerseyColor.SelectedColor).Color)
            Away_Sleeve_Color = CommonUtils.getHexfromColor(New SolidColorBrush(newtAwaySleeveColor.SelectedColor).Color)
            Away_Shoulder_Stripe_Color = CommonUtils.getHexfromColor(New SolidColorBrush(newtAwayShoulderStripeColor.SelectedColor).Color)
            Away_Jersey_Number_Color = CommonUtils.getHexfromColor(New SolidColorBrush(newtAwayJerseyNumberColor.SelectedColor).Color)
            Away_Jersey_Outline_Number_Color = CommonUtils.getHexfromColor(New SolidColorBrush(newtAwayNumberOutlineColor.SelectedColor).Color)
            Away_Jersey_Sleeve_1_Color = CommonUtils.getHexfromColor(New SolidColorBrush(newtAwayJerseySleeve1Color.SelectedColor).Color)
            Away_Jersey_Sleeve_2_Color = CommonUtils.getHexfromColor(New SolidColorBrush(newtAwayJerseySleeve2Color.SelectedColor).Color)
            Away_Jersey_Sleeve_3_Color = CommonUtils.getHexfromColor(New SolidColorBrush(newtAwayJerseySleeve3Color.SelectedColor).Color)
            Away_Jersey_Sleeve_4_Color = CommonUtils.getHexfromColor(New SolidColorBrush(newtAwayJerseySleeve4Color.SelectedColor).Color)
            Away_Jersey_Sleeve_5_Color = CommonUtils.getHexfromColor(New SolidColorBrush(newtAwayJerseySleeve5Color.SelectedColor).Color)
            Away_Jersey_Sleeve_6_Color = CommonUtils.getHexfromColor(New SolidColorBrush(newtAwayJerseySleeve6Color.SelectedColor).Color)
            Away_Pants_Color = CommonUtils.getHexfromColor(New SolidColorBrush(newtAwayPantsColor.SelectedColor).Color)
            Away_Pants_Stripe_1_Color = CommonUtils.getHexfromColor(New SolidColorBrush(newtAwayPantsStripe1Color.SelectedColor).Color)
            Away_Pants_Stripe_2_Color = CommonUtils.getHexfromColor(New SolidColorBrush(newtAwayPantsStripe2Color.SelectedColor).Color)
            Away_Pants_Stripe_3_Color = CommonUtils.getHexfromColor(New SolidColorBrush(newtAwayPantsStripe3Color.SelectedColor).Color)


            Dim Field_Type_Int As Int16
            If newl1FieldType.Text = "Grass" Then
                Field_Type_Int = 1
            ElseIf newl1FieldType.Text = "Artificial" Then
                Field_Type_Int = 2
            End If

            Stadium_Field_Color = CommonUtils.getHexfromColor(New SolidColorBrush(newl1FieldColor.SelectedColor).Color)

            stadium = New StadiumMdl(newtStadium.Text.Trim, newtStadiumLocation.Text.Trim,
                                     Field_Type_Int, Stadium_Field_Color,
                                    newtStadiumCapacity.Text.Trim, newtStadiumPath.Text.Trim)
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

            Away_Jersey = New JerseyMdl(Away_Jersey_Color, Away_Sleeve_Color, Away_Shoulder_Stripe_Color,
                                Away_Jersey_Number_Color, Away_Jersey_Outline_Number_Color,
                                Away_Jersey_Sleeve_1_Color, Away_Jersey_Sleeve_2_Color,
                                Away_Jersey_Sleeve_3_Color, Away_Jersey_Sleeve_4_Color,
                                Away_Jersey_Sleeve_5_Color, Away_Jersey_Sleeve_6_Color)

            Away_Pants = New PantsMdl(Away_Pants_Color, Away_Pants_Stripe_1_Color,
                                Away_Pants_Stripe_2_Color, Away_Pants_Stripe_3_Color)
            Uniform = New UniformMdl(Helmet, Home_Jersey, Away_Jersey, Home_Pants, Away_Pants, Footwear)

            this_team.setFields("C", City_Abr, City, Nickname, stadium, Uniform, newtHelmetImgPath.Text.Trim)

            Select Case Form_Function
                Case form_func.New_Team
                    RaiseEvent backtoNewLeague(Me, New TeamUpdatedEventArgs(True))
                Case form_func.Stock_Team_New
                    Dim sts As StockTeams_Services = New StockTeams_Services()
                    sts.AddStockTeam(this_team)
                    RaiseEvent backtoStockTeams(Me, New EventArgs)
                Case form_func.Stock_Team_Edit
                    Dim sts As StockTeams_Services = New StockTeams_Services()
                    sts.UpdateStockTeam(this_team)
                    RaiseEvent backtoStockTeams(Me, New EventArgs)
            End Select

        Catch ex As Exception
            logger.Error("Error saving team error")
            logger.Error(ex)
            MessageBox.Show(CommonUtils.substr(ex.Message, 0, 100), "Error", MessageBoxButton.OK, MessageBoxImage.Error)
        End Try
    End Sub
    Public Sub setHomeUniform()
        Dim mc As System.Windows.Media.Color = Nothing
        Dim helmetColor As System.Drawing.Color = Nothing
        Dim helmetLogoColor As System.Drawing.Color = Nothing
        Dim helmetFacemaskColor As System.Drawing.Color = Nothing
        Dim SocksColor As System.Drawing.Color = Nothing
        Dim CleatsColor As System.Drawing.Color = Nothing

        Dim HomeJerseyColor As System.Drawing.Color = Nothing
        Dim HomeJerseySleeveColor As System.Drawing.Color = Nothing
        Dim HomeJerseyShoulderLoopColor As System.Drawing.Color = Nothing
        Dim HomeJerseyNumberColor As System.Drawing.Color = Nothing
        Dim HomeJerseyNumberOutlineColor As System.Drawing.Color = Nothing
        Dim HomeJerseyStripe_1 As System.Drawing.Color = Nothing
        Dim HomeJerseyStripe_2 As System.Drawing.Color = Nothing
        Dim HomeJerseyStripe_3 As System.Drawing.Color = Nothing
        Dim HomeJerseyStripe_4 As System.Drawing.Color = Nothing
        Dim HomeJerseyStripe_5 As System.Drawing.Color = Nothing
        Dim HomeJerseyStripe_6 As System.Drawing.Color = Nothing
        Dim HomePantsColor As System.Drawing.Color = Nothing
        Dim HomePants_Stripe_1 As System.Drawing.Color = Nothing
        Dim HomePants_Stripe_2 As System.Drawing.Color = Nothing
        Dim HomePants_Stripe_3 As System.Drawing.Color = Nothing

        If Not IsNothing(newtHelmentColor.SelectedColor) Then
            mc = New SolidColorBrush(newtHelmentColor.SelectedColor).Color
            helmetColor = System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B)
        Else
            helmetColor = App_Constants.STOCK_GREY_COLOR
        End If

        If Not IsNothing(newtHelmentLogoColor.SelectedColor) Then
            mc = New SolidColorBrush(newtHelmentLogoColor.SelectedColor).Color
            helmetLogoColor = System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B)
        Else
            helmetLogoColor = App_Constants.STOCK_GREY_COLOR
        End If

        If Not IsNothing(newtFacemaskColor.SelectedColor) Then
            mc = New SolidColorBrush(newtFacemaskColor.SelectedColor).Color
            helmetFacemaskColor = System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B)
        Else
            helmetFacemaskColor = App_Constants.STOCK_GREY_COLOR
        End If

        If Not IsNothing(newtSockColor.SelectedColor) Then
            mc = New SolidColorBrush(newtSockColor.SelectedColor).Color
            SocksColor = System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B)
        Else
            SocksColor = App_Constants.STOCK_GREY_COLOR
        End If

        If Not IsNothing(newtCleatsColor.SelectedColor) Then
            mc = New SolidColorBrush(newtCleatsColor.SelectedColor).Color
            CleatsColor = System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B)
        Else
            CleatsColor = App_Constants.STOCK_GREY_COLOR
        End If

        If Not IsNothing(newtHomeJerseyColor.SelectedColor) Then
            mc = New SolidColorBrush(newtHomeJerseyColor.SelectedColor).Color
            HomeJerseyColor = System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B)
        Else
            HomeJerseyColor = App_Constants.STOCK_GREY_COLOR
        End If

        If Not IsNothing(newtHomeSleeveColor.SelectedColor) Then
            mc = New SolidColorBrush(newtHomeSleeveColor.SelectedColor).Color
            HomeJerseySleeveColor = System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B)
        Else
            HomeJerseySleeveColor = App_Constants.STOCK_GREY_COLOR
        End If

        If Not IsNothing(newtHomeShoulderStripeColor.SelectedColor) Then
            mc = New SolidColorBrush(newtHomeShoulderStripeColor.SelectedColor).Color
            HomeJerseyShoulderLoopColor = System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B)
        Else
            HomeJerseyShoulderLoopColor = App_Constants.STOCK_GREY_COLOR
        End If

        If Not IsNothing(newtHomeJerseyNumberColor.SelectedColor) Then
            mc = New SolidColorBrush(newtHomeJerseyNumberColor.SelectedColor).Color
            HomeJerseyNumberColor = System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B)
        Else
            HomeJerseyNumberColor = App_Constants.STOCK_GREY_COLOR
        End If

        If Not IsNothing(newtHomeNumberOutlineColor.SelectedColor) Then
            mc = New SolidColorBrush(newtHomeNumberOutlineColor.SelectedColor).Color
            HomeJerseyNumberOutlineColor = System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B)
        Else
            HomeJerseyNumberOutlineColor = App_Constants.STOCK_GREY_COLOR
        End If

        If Not IsNothing(newtHomeJerseySleeve1Color.SelectedColor) Then
            mc = New SolidColorBrush(newtHomeJerseySleeve1Color.SelectedColor).Color
            HomeJerseyStripe_1 = System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B)
        Else
            HomeJerseyStripe_1 = App_Constants.STOCK_GREY_COLOR
        End If

        If Not IsNothing(newtHomeJerseySleeve2Color.SelectedColor) Then
            mc = New SolidColorBrush(newtHomeJerseySleeve2Color.SelectedColor).Color
            HomeJerseyStripe_2 = System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B)
        Else
            HomeJerseyStripe_2 = App_Constants.STOCK_GREY_COLOR
        End If

        If Not IsNothing(newtHomeJerseySleeve3Color.SelectedColor) Then
            mc = New SolidColorBrush(newtHomeJerseySleeve3Color.SelectedColor).Color
            HomeJerseyStripe_3 = System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B)
        Else
            HomeJerseyStripe_3 = App_Constants.STOCK_GREY_COLOR
        End If

        If Not IsNothing(newtHomeJerseySleeve4Color.SelectedColor) Then
            mc = New SolidColorBrush(newtHomeJerseySleeve4Color.SelectedColor).Color
            HomeJerseyStripe_4 = System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B)
        Else
            HomeJerseyStripe_4 = App_Constants.STOCK_GREY_COLOR
        End If

        If Not IsNothing(newtHomeJerseySleeve5Color.SelectedColor) Then
            mc = New SolidColorBrush(newtHomeJerseySleeve5Color.SelectedColor).Color
            HomeJerseyStripe_5 = System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B)
        Else
            HomeJerseyStripe_5 = App_Constants.STOCK_GREY_COLOR
        End If

        If Not IsNothing(newtHomeJerseySleeve6Color.SelectedColor) Then
            mc = New SolidColorBrush(newtHomeJerseySleeve6Color.SelectedColor).Color
            HomeJerseyStripe_6 = System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B)
        Else
            HomeJerseyStripe_6 = App_Constants.STOCK_GREY_COLOR
        End If

        If Not IsNothing(newtHomePantsColor.SelectedColor) Then
            mc = New SolidColorBrush(newtHomePantsColor.SelectedColor).Color
            HomePantsColor = System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B)
        Else
            HomePantsColor = App_Constants.STOCK_GREY_COLOR
        End If

        If Not IsNothing(newtHomePantsStripe1Color.SelectedColor) Then
            mc = New SolidColorBrush(newtHomePantsStripe1Color.SelectedColor).Color
            HomePants_Stripe_1 = System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B)
        Else
            HomePants_Stripe_1 = App_Constants.STOCK_GREY_COLOR
        End If

        If Not IsNothing(newtHomePantsStripe2Color.SelectedColor) Then
            mc = New SolidColorBrush(newtHomePantsStripe2Color.SelectedColor).Color
            HomePants_Stripe_2 = System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B)
        Else
            HomePants_Stripe_2 = App_Constants.STOCK_GREY_COLOR
        End If

        If Not IsNothing(newtHomePantsStripe3Color.SelectedColor) Then
            mc = New SolidColorBrush(newtHomePantsStripe3Color.SelectedColor).Color
            HomePants_Stripe_3 = System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B)
        Else
            HomePants_Stripe_3 = App_Constants.STOCK_GREY_COLOR
        End If

        Uniform_Img.Flip_All_Colors(True, helmetColor, helmetFacemaskColor, helmetLogoColor, HomeJerseyColor,
        HomeJerseyNumberColor, HomeJerseyNumberOutlineColor,
        HomeJerseySleeveColor, HomeJerseyShoulderLoopColor,
        HomeJerseyStripe_1, HomeJerseyStripe_2,
        HomeJerseyStripe_3, HomeJerseyStripe_4,
        HomeJerseyStripe_5, HomeJerseyStripe_6,
        HomePantsColor,
        HomePants_Stripe_1,
        HomePants_Stripe_2,
        HomePants_Stripe_3,
        SocksColor, CleatsColor)

        newtHomeUniform.Source = Uniform_Img.getHomeUniform_Image

    End Sub

    Public Sub setAwayUniform()
        Dim mc As System.Windows.Media.Color = Nothing
        Dim helmetColor As System.Drawing.Color = Nothing
        Dim helmetLogoColor As System.Drawing.Color = Nothing
        Dim helmetFacemaskColor As System.Drawing.Color = Nothing
        Dim SocksColor As System.Drawing.Color = Nothing
        Dim CleatsColor As System.Drawing.Color = Nothing

        Dim AwayJerseyColor As System.Drawing.Color = Nothing
        Dim AwayJerseySleeveColor As System.Drawing.Color = Nothing
        Dim AwayJerseyShoulderLoopColor As System.Drawing.Color = Nothing
        Dim AwayJerseyNumberColor As System.Drawing.Color = Nothing
        Dim AwayJerseyNumberOutlineColor As System.Drawing.Color = Nothing
        Dim AwayJerseyStripe_1 As System.Drawing.Color = Nothing
        Dim AwayJerseyStripe_2 As System.Drawing.Color = Nothing
        Dim AwayJerseyStripe_3 As System.Drawing.Color = Nothing
        Dim AwayJerseyStripe_4 As System.Drawing.Color = Nothing
        Dim AwayJerseyStripe_5 As System.Drawing.Color = Nothing
        Dim AwayJerseyStripe_6 As System.Drawing.Color = Nothing
        Dim AwayPantsColor As System.Drawing.Color = Nothing
        Dim AwayPants_Stripe_1 As System.Drawing.Color = Nothing
        Dim AwayPants_Stripe_2 As System.Drawing.Color = Nothing
        Dim AwayPants_Stripe_3 As System.Drawing.Color = Nothing

        If Not IsNothing(newtHelmentColor.SelectedColor) Then
            mc = New SolidColorBrush(newtHelmentColor.SelectedColor).Color
            helmetColor = System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B)
        Else
            helmetColor = App_Constants.STOCK_GREY_COLOR
        End If

        If Not IsNothing(newtHelmentLogoColor.SelectedColor) Then
            mc = New SolidColorBrush(newtHelmentLogoColor.SelectedColor).Color
            helmetLogoColor = System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B)
        Else
            helmetLogoColor = App_Constants.STOCK_GREY_COLOR
        End If

        If Not IsNothing(newtFacemaskColor.SelectedColor) Then
            mc = New SolidColorBrush(newtFacemaskColor.SelectedColor).Color
            helmetFacemaskColor = System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B)
        Else
            helmetFacemaskColor = App_Constants.STOCK_GREY_COLOR
        End If

        If Not IsNothing(newtSockColor.SelectedColor) Then
            mc = New SolidColorBrush(newtSockColor.SelectedColor).Color
            SocksColor = System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B)
        Else
            SocksColor = App_Constants.STOCK_GREY_COLOR
        End If

        If Not IsNothing(newtCleatsColor.SelectedColor) Then
            mc = New SolidColorBrush(newtCleatsColor.SelectedColor).Color
            CleatsColor = System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B)
        Else
            CleatsColor = App_Constants.STOCK_GREY_COLOR
        End If

        If Not IsNothing(newtAwayJerseyColor.SelectedColor) Then
            mc = New SolidColorBrush(newtAwayJerseyColor.SelectedColor).Color
            AwayJerseyColor = System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B)
        Else
            AwayJerseyColor = App_Constants.STOCK_GREY_COLOR
        End If

        If Not IsNothing(newtAwaySleeveColor.SelectedColor) Then
            mc = New SolidColorBrush(newtAwaySleeveColor.SelectedColor).Color
            AwayJerseySleeveColor = System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B)
        Else
            AwayJerseySleeveColor = App_Constants.STOCK_GREY_COLOR
        End If

        If Not IsNothing(newtAwayShoulderStripeColor.SelectedColor) Then
            mc = New SolidColorBrush(newtAwayShoulderStripeColor.SelectedColor).Color
            AwayJerseyShoulderLoopColor = System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B)
        Else
            AwayJerseyShoulderLoopColor = App_Constants.STOCK_GREY_COLOR
        End If

        If Not IsNothing(newtAwayJerseyNumberColor.SelectedColor) Then
            mc = New SolidColorBrush(newtAwayJerseyNumberColor.SelectedColor).Color
            AwayJerseyNumberColor = System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B)
        Else
            AwayJerseyNumberColor = App_Constants.STOCK_GREY_COLOR
        End If

        If Not IsNothing(newtAwayNumberOutlineColor.SelectedColor) Then
            mc = New SolidColorBrush(newtAwayNumberOutlineColor.SelectedColor).Color
            AwayJerseyNumberOutlineColor = System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B)
        Else
            AwayJerseyNumberOutlineColor = App_Constants.STOCK_GREY_COLOR
        End If

        If Not IsNothing(newtAwayJerseySleeve1Color.SelectedColor) Then
            mc = New SolidColorBrush(newtAwayJerseySleeve1Color.SelectedColor).Color
            AwayJerseyStripe_1 = System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B)
        Else
            AwayJerseyStripe_1 = App_Constants.STOCK_GREY_COLOR
        End If

        If Not IsNothing(newtAwayJerseySleeve2Color.SelectedColor) Then
            mc = New SolidColorBrush(newtAwayJerseySleeve2Color.SelectedColor).Color
            AwayJerseyStripe_2 = System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B)
        Else
            AwayJerseyStripe_2 = App_Constants.STOCK_GREY_COLOR
        End If

        If Not IsNothing(newtAwayJerseySleeve3Color.SelectedColor) Then
            mc = New SolidColorBrush(newtAwayJerseySleeve3Color.SelectedColor).Color
            AwayJerseyStripe_3 = System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B)
        Else
            AwayJerseyStripe_3 = App_Constants.STOCK_GREY_COLOR
        End If

        If Not IsNothing(newtAwayJerseySleeve4Color.SelectedColor) Then
            mc = New SolidColorBrush(newtAwayJerseySleeve4Color.SelectedColor).Color
            AwayJerseyStripe_4 = System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B)
        Else
            AwayJerseyStripe_4 = App_Constants.STOCK_GREY_COLOR
        End If

        If Not IsNothing(newtAwayJerseySleeve5Color.SelectedColor) Then
            mc = New SolidColorBrush(newtAwayJerseySleeve5Color.SelectedColor).Color
            AwayJerseyStripe_5 = System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B)
        Else
            AwayJerseyStripe_5 = App_Constants.STOCK_GREY_COLOR
        End If

        If Not IsNothing(newtAwayJerseySleeve6Color.SelectedColor) Then
            mc = New SolidColorBrush(newtAwayJerseySleeve6Color.SelectedColor).Color
            AwayJerseyStripe_6 = System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B)
        Else
            AwayJerseyStripe_6 = App_Constants.STOCK_GREY_COLOR
        End If

        If Not IsNothing(newtAwayPantsColor.SelectedColor) Then
            mc = New SolidColorBrush(newtAwayPantsColor.SelectedColor).Color
            AwayPantsColor = System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B)
        Else
            AwayPantsColor = App_Constants.STOCK_GREY_COLOR
        End If

        If Not IsNothing(newtAwayPantsStripe1Color.SelectedColor) Then
            mc = New SolidColorBrush(newtAwayPantsStripe1Color.SelectedColor).Color
            AwayPants_Stripe_1 = System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B)
        Else
            AwayPants_Stripe_1 = App_Constants.STOCK_GREY_COLOR
        End If

        If Not IsNothing(newtAwayPantsStripe2Color.SelectedColor) Then
            mc = New SolidColorBrush(newtAwayPantsStripe2Color.SelectedColor).Color
            AwayPants_Stripe_2 = System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B)
        Else
            AwayPants_Stripe_2 = App_Constants.STOCK_GREY_COLOR
        End If

        If Not IsNothing(newtAwayPantsStripe3Color.SelectedColor) Then
            mc = New SolidColorBrush(newtAwayPantsStripe3Color.SelectedColor).Color
            AwayPants_Stripe_3 = System.Drawing.Color.FromArgb(mc.A, mc.R, mc.G, mc.B)
        Else
            AwayPants_Stripe_3 = App_Constants.STOCK_GREY_COLOR
        End If

        Uniform_Img.Flip_All_Colors(False, helmetColor, helmetFacemaskColor, helmetLogoColor, AwayJerseyColor,
        AwayJerseyNumberColor, AwayJerseyNumberOutlineColor,
        AwayJerseySleeveColor, AwayJerseyShoulderLoopColor,
        AwayJerseyStripe_1, AwayJerseyStripe_2,
        AwayJerseyStripe_3, AwayJerseyStripe_4,
        AwayJerseyStripe_5, AwayJerseyStripe_6,
        AwayPantsColor,
        AwayPants_Stripe_1,
        AwayPants_Stripe_2,
        AwayPants_Stripe_3,
        SocksColor, CleatsColor)

        newtAwayUniform.Source = Uniform_Img.getAwayUniform_Image

    End Sub

    Private Sub Validate()

        If CommonUtils.isBlank(newtCityAbb.Text) Then
            Throw New Exception("City Abbriviation must have a value")
        End If

        If CommonUtils.isBlank(newtCity.Text) Then
            Throw New Exception("City must have a value")
        End If

        If CommonUtils.isBlank(newtNickname.Text) Then
            Throw New Exception("Nickname must have a value")
        End If

        If CommonUtils.isBlank(newtStadium.Text) Then
            Throw New Exception("Stadium Name must have a value")
        End If

        If CommonUtils.isBlank(newtStadiumLocation.Text) Then
            Throw New Exception("Stadium Location must have a value")
        End If

        If IsNothing(newl1FieldColor.SelectedColor) Then
            Throw New Exception("Field color of team stadium must be supplied")
        End If

        If CommonUtils.isBlank(newtStadiumCapacity.Text) Then
            Throw New Exception("Stadium Capacity must be supplied and numeric")
        End If

        If CommonUtils.isBlank(newtStadiumPath.Text) Then
            Throw New Exception("Image of team stadium must be supplied")
        End If

        If IsNothing(newtHelmentColor.SelectedColor) Then
            Throw New Exception("A helmet color must be selected")
        End If

        If IsNothing(newtHelmentLogoColor.SelectedColor) Then
            Throw New Exception("A helmet Logo color must be selected")
        End If

        If IsNothing(newtFacemaskColor.SelectedColor) Then
            Throw New Exception("A helmet facemask must be selected")
        End If

        If CommonUtils.isBlank(newtHelmetImgPath.Text) Then
            Throw New Exception("Helmet image path must have a value")
        End If

        If IsNothing(newtSockColor.SelectedColor) Then
            Throw New Exception("Sock color must have a value")
        End If

        If IsNothing(newtCleatsColor.SelectedColor) Then
            Throw New Exception("Cleat color must have a value")
        End If

        If IsNothing(newtHomeJerseyColor.SelectedColor) Then
            Throw New Exception("Home jersey color must have a value")
        End If

        If IsNothing(newtHomeSleeveColor.SelectedColor) Then
            Throw New Exception("Home sleeve color must have a value")
        End If

        If IsNothing(newtHomeShoulderStripeColor.SelectedColor) Then
            Throw New Exception("Home shoulder loop color must have a value")
        End If

        If IsNothing(newtHomeJerseyNumberColor.SelectedColor) Then
            Throw New Exception("Home jersey number color must have a value")
        End If

        If IsNothing(newtHomeNumberOutlineColor.SelectedColor) Then
            Throw New Exception("Home jersey outline number color must have a value")
        End If

        If IsNothing(newtHomeJerseySleeve1Color.SelectedColor) Then
            Throw New Exception("Home jersey sleeve string 1 color must have a value")
        End If

        If IsNothing(newtHomeJerseySleeve2Color.SelectedColor) Then
            Throw New Exception("Home jersey sleeve string 2 color must have a value")
        End If

        If IsNothing(newtHomeJerseySleeve3Color.SelectedColor) Then
            Throw New Exception("Home jersey sleeve string 3 color must have a value")
        End If

        If IsNothing(newtHomeJerseySleeve4Color.SelectedColor) Then
            Throw New Exception("Home jersey sleeve string 4 color must have a value")
        End If

        If IsNothing(newtHomeJerseySleeve5Color.SelectedColor) Then
            Throw New Exception("Home jersey sleeve string 5 color must have a value")
        End If

        If IsNothing(newtHomeJerseySleeve6Color.SelectedColor) Then
            Throw New Exception("Home jersey sleeve string 6 color must have a value")
        End If

        If IsNothing(newtHomePantsColor.SelectedColor) Then
            Throw New Exception("Home pants color must have a value")
        End If

        If IsNothing(newtHomePantsStripe1Color.SelectedColor) Then
            Throw New Exception("Home pants stripe 1 color must have a value")
        End If

        If IsNothing(newtHomePantsStripe2Color.SelectedColor) Then
            Throw New Exception("Home pants stripe 2 color must have a value")
        End If

        If IsNothing(newtHomePantsStripe3Color.SelectedColor) Then
            Throw New Exception("Home pants stripe 3 color must have a value")
        End If

        If IsNothing(newtAwayJerseyColor.SelectedColor) Then
            Throw New Exception("Away jersey color must have a value")
        End If

        If IsNothing(newtAwaySleeveColor.SelectedColor) Then
            Throw New Exception("Away sleeve color must have a value")
        End If

        If IsNothing(newtAwayShoulderStripeColor.SelectedColor) Then
            Throw New Exception("Away shoulder loop color must have a value")
        End If

        If IsNothing(newtAwayJerseyNumberColor.SelectedColor) Then
            Throw New Exception("Away jersey number color must have a value")
        End If

        If IsNothing(newtAwayNumberOutlineColor.SelectedColor) Then
            Throw New Exception("Away jersey outline number color must have a value")
        End If

        If IsNothing(newtAwayJerseySleeve1Color.SelectedColor) Then
            Throw New Exception("Away jersey sleeve string 1 color must have a value")
        End If

        If IsNothing(newtAwayJerseySleeve2Color.SelectedColor) Then
            Throw New Exception("Away jersey sleeve string 2 color must have a value")
        End If

        If IsNothing(newtAwayJerseySleeve3Color.SelectedColor) Then
            Throw New Exception("Away jersey sleeve string 3 color must have a value")
        End If

        If IsNothing(newtAwayJerseySleeve4Color.SelectedColor) Then
            Throw New Exception("Away jersey sleeve string 4 color must have a value")
        End If

        If IsNothing(newtAwayJerseySleeve5Color.SelectedColor) Then
            Throw New Exception("Away jersey sleeve string 5 color must have a value")
        End If

        If IsNothing(newtAwayJerseySleeve6Color.SelectedColor) Then
            Throw New Exception("Away jersey sleeve string 6 color must have a value")
        End If

        If IsNothing(newtAwayPantsColor.SelectedColor) Then
            Throw New Exception("Away pants color must have a value")
        End If

        If IsNothing(newtAwayPantsStripe1Color.SelectedColor) Then
            Throw New Exception("Away pants stripe 1 color must have a value")
        End If

        If IsNothing(newtAwayPantsStripe2Color.SelectedColor) Then
            Throw New Exception("Away pants stripe 2 color must have a value")
        End If

        If IsNothing(newtAwayPantsStripe3Color.SelectedColor) Then
            Throw New Exception("Away pants stripe 3 color must have a value")
        End If

        'Only if stock team check for duplicate team here
        If Form_Function = form_func.Stock_Team_New Or Form_Function = form_func.Stock_Team_Edit Then
            Dim st_services = New StockTeams_Services()
            If st_services.DoesTeamAlreadyExist(newtCity.Text.Trim, newtNickname.Text.Trim) Then
                Throw New Exception("This Stock Team Already Exists!  No Duplicate Stock Teams Allowed!")
            End If
        End If

    End Sub
    Private Sub help_btn_Click(sender As Object, e As RoutedEventArgs) Handles help_btn.Click

        Select Case Form_Function
            Case form_func.New_Team
                Dim hlp_form As Help_NewTeam = New Help_NewTeam()
                hlp_form.Top = (System.Windows.SystemParameters.PrimaryScreenHeight - hlp_form.Height) / 2
                hlp_form.Left = (System.Windows.SystemParameters.PrimaryScreenWidth - hlp_form.Width) / 2
                hlp_form.ShowDialog()
        End Select

    End Sub


End Class
