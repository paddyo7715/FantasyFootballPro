Public Class TeamMdl

    Public Const PLAYERS_PER_TEAM = 18

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


End Class
