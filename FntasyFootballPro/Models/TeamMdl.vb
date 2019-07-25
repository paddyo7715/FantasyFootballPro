Public Class TeamMdl

    Property id As Integer
    Property Owner As String
    Property City_Abr As String = ""
    Property City As String = ""
    Property Nickname As String = ""
    Property Stadium As StadiumMdl
    Property Uniform As UniformMdl
    Property Helmet_img_path As String = ""

    Property Players As List(Of PlayerMdl)
    Public Sub New(ByVal id As Integer, ByVal City As String)
        Me.id = id
        Me.City = City
    End Sub
    'Clone the team model
    Public Sub New(ByVal tc As TeamMdl)
        Me.id = tc.id
        Me.Owner = tc.Owner
        Me.City = tc.City
        Me.Nickname = tc.Nickname
        Me.City_Abr = tc.City_Abr

        Dim std As StadiumMdl = New StadiumMdl(tc.Stadium.Stadium_Name, tc.Stadium.Stadium_Location, tc.Stadium.Field_Type,
            tc.Stadium.Field_Color, tc.Stadium.Capacity, tc.Stadium.Stadium_Img_Path)
        Me.Stadium = std

        Dim helmt As HelmetMdl = New HelmetMdl(tc.Uniform.Helmet.Helmet_Color, tc.Uniform.Helmet.Helmet_Logo_Color,
                                               tc.Uniform.Helmet.Helmet_Facemask_Color)
        Dim h_jers As JerseyMdl = New JerseyMdl(tc.Uniform.Home_Jersey.Jersey_Color, tc.Uniform.Home_Jersey.Sleeve_Color,
            tc.Uniform.Home_Jersey.Shoulder_Stripe_Color, tc.Uniform.Home_Jersey.Number_Color, tc.Uniform.Home_Jersey.Number_Outline_Color,
            tc.Uniform.Home_Jersey.Sleeve_Stripe1, tc.Uniform.Home_Jersey.Sleeve_Stripe2,
            tc.Uniform.Home_Jersey.Sleeve_Stripe3, tc.Uniform.Home_Jersey.Sleeve_Stripe4,
            tc.Uniform.Home_Jersey.Sleeve_Stripe5, tc.Uniform.Home_Jersey.Sleeve_Stripe6)
        Dim h_pants As PantsMdl = New PantsMdl(tc.Uniform.Home_Pants.Pants_Color,
            tc.Uniform.Home_Pants.Stripe_Color_1,
            tc.Uniform.Home_Pants.Stripe_Color_2,
            tc.Uniform.Home_Pants.Stripe_Color_3)

        Dim a_jers As JerseyMdl = New JerseyMdl(tc.Uniform.Away_Jersey.Jersey_Color, tc.Uniform.Away_Jersey.Sleeve_Color,
            tc.Uniform.Away_Jersey.Shoulder_Stripe_Color, tc.Uniform.Away_Jersey.Number_Color, tc.Uniform.Away_Jersey.Number_Outline_Color,
            tc.Uniform.Away_Jersey.Sleeve_Stripe1, tc.Uniform.Away_Jersey.Sleeve_Stripe2,
            tc.Uniform.Away_Jersey.Sleeve_Stripe3, tc.Uniform.Away_Jersey.Sleeve_Stripe4,
            tc.Uniform.Away_Jersey.Sleeve_Stripe5, tc.Uniform.Away_Jersey.Sleeve_Stripe6)
        Dim a_pants As PantsMdl = New PantsMdl(tc.Uniform.Away_Pants.Pants_Color,
            tc.Uniform.Away_Pants.Stripe_Color_1,
            tc.Uniform.Away_Pants.Stripe_Color_2,
            tc.Uniform.Away_Pants.Stripe_Color_3)
        Dim footw As FootwearMdl = New FootwearMdl(tc.Uniform.Footwear.Socks_Color,
            tc.Uniform.Footwear.Cleats_Color)

        Dim uni As UniformMdl = New UniformMdl(helmt, h_jers, a_jers, h_pants, a_pants, footw)

        Me.Uniform = uni
        Me.Helmet_img_path = tc.Helmet_img_path


    End Sub
    Public Sub setStockImagePaths(ByVal helmet_folder As String, ByVal stadium_folder As String)
        Helmet_img_path = helmet_folder
        Stadium.Stadium_Img_Path = stadium_folder
    End Sub


    Public Sub setFields(ByVal Owner As String, ByVal City_Abr As String, ByVal City As String, ByVal Nickname As String,
                       ByVal Stadium As StadiumMdl, ByVal uniform As UniformMdl, ByVal Helmet_img_path As String, ByVal Players As List(Of PlayerMdl))

        Me.Owner = Owner
        Me.City_Abr = City_Abr
        Me.City = City
        Me.Nickname = Nickname
        Me.Stadium = Stadium
        Me.Uniform = uniform
        Me.Helmet_img_path = Helmet_img_path

        Me.Players = Players

    End Sub
    Public Sub setID(ByVal id As Integer)
        Me.id = id
    End Sub


End Class
