Public MustInherit Class Base_Team

    Public Const PLAYERS_PER_TEAM = 18

    Property City_Abr As String
    Property City As String
    Property Nickname As String
    Property Helmet_img_path As String
    Property Helmet_Color As String
    Property Home_jersey_Color As String
    Property Home_Pants_Color As String
    Property Away_jersey_Color As String
    Property Away_Pants_Color As String

    Property Players As List(Of New_Player)

    Public Sub setFields(ByVal City_Abr As String, ByVal City As String, ByVal Nickname As String,
                       ByVal Helmet_img_path As String, ByVal Helmet_Color As String,
                       ByVal Home_jersey_Color As String, ByVal Home_Pants_Color As String,
                       ByVal Away_jersey_Color As String, ByVal Away_Pants_Color As String)

        Me.City_Abr = City_Abr
        Me.City = City
        Me.Nickname = Nickname
        Me.Helmet_img_path = Helmet_img_path
        Me.Helmet_Color = Helmet_Color
        Me.Home_jersey_Color = Home_jersey_Color
        Me.Home_Pants_Color = Home_Pants_Color
        Me.Away_jersey_Color = Away_jersey_Color
        Me.Away_Pants_Color = Away_Pants_Color

    End Sub



End Class
