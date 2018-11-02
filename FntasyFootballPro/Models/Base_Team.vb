Public MustInherit Class Base_Team

    Public Const PLAYERS_PER_TEAM = 18

    Property City_Abr As String
    Property City As String
    Property Nickname As String




    Property Players As List(Of New_Player)

    Public Sub setFields(ByVal City_Abr As String, ByVal City As String, ByVal Nickname As String,
                       ByVal Helmet_img_path As String, ByVal Helmet_Color As String,
                       ByVal Home_jersey_Color As String, ByVal Home_Pants_Color As String,
                       ByVal Away_jersey_Color As String, ByVal Away_Pants_Color As String,
                        ByVal Stadium As String, ByVal Stadium_location As String, ByVal Stadium_img_path As String,
                         ByVal Helmet_Logo_Color As String, ByVal Helmet_Facemask_Color As String,
                         ByVal Helmet_Stripe1_Color As String, ByVal Helmet_Stripe2_Color As String,
                         ByVal Helmet_Stripe3_Color As String, ByVal Socks_Color As String,
                         ByVal Cleats_Color As String, ByVal Home_Jersey_Should_Stripe_Color As String,
                         ByVal Home_Number_Color As String, ByVal Home_Number_Outline_Color As String,
                         ByVal Home_Sleeve_Stripe1 As String, ByVal Home_Sleeve_Stripe2 As String,
                         ByVal Home_Sleeve_Stripe3 As String, ByVal Home_Sleeve_Stripe4 As String,
                         ByVal Home_Sleeve_Stripe5 As String, ByVal Home_Sleeve_Stripe6 As String,
                         ByVal Home_Sleeve_Stripe7 As String, ByVal Home_Sleeve_Stripe8 As String,
                         ByVal Home_Sleeve_Stripe9 As String, ByVal Home_Sleeve_Stripe10 As String)

        Me.City_Abr = City_Abr
        Me.City = City
        Me.Nickname = Nickname
        Me.Helmet_img_path = Helmet_img_path
        Me.Helmet_Color = Helmet_Color
        Me.Home_jersey_Color = Home_jersey_Color
        Me.Home_Pants_Color = Home_Pants_Color
        Me.Away_jersey_Color = Away_jersey_Color
        Me.Away_Pants_Color = Away_Pants_Color
        Me.Stadium_Name = Stadium
        Me.Stadium_Location = Stadium_location
        Me.Stadium_Img_Path = Stadium_img_path

        Me.Helmet_Logo_Color = Helmet_Logo_Color
        Me.Helmet_Facemask_Color = Helmet_Facemask_Color
        Me.Helmet_Stripe1_Color = Helmet_Stripe1_Color
        Me.Helmet_Stripe2_Color = Helmet_Stripe2_Color
        Me.Helmet_Stripe3_Color = Helmet_Stripe3_Color
        Me.Socks_Color = Socks_Color
        Me.Cleats_Color = Cleats_Color
        Me.Home_Jersey_Should_Stripe_Color = Home_Jersey_Should_Stripe_Color
        Me.Home_Number_Color =
Me.Home_Number_Outline_Color =
Me.Home_Sleeve_Stripe1 =
Me.Home_Sleeve_Stripe2 =
Me.Home_Sleeve_Stripe3 =
Me.Home_Sleeve_Stripe4 =
Me.Home_Sleeve_Stripe5 =
Me.Home_Sleeve_Stripe6 =
Me.Home_Sleeve_Stripe7 =
Me.Home_Sleeve_Stripe8 =
Me.Home_Sleeve_Stripe9 =
Me.Home_Sleeve_Stripe10 =


    End Sub



End Class
