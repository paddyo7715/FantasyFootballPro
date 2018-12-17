Public Class TeamMdl

    Public Const PLAYERS_PER_TEAM = 18

    Property id As Integer
    Property City_Abr As String = ""
    Property City As String = ""
    Property Nickname As String = ""
    Property Stadium As StadiumMdl
    Property Uniform As UniformMdl
    Property Helmet_img_path As String = ""

    Property Players As List(Of PlayerMdl)
    Public Sub New(ByVal id As Integer, ByVal Nickname As String)
        Me.id = id
        Me.Nickname = Nickname
    End Sub

    Public Sub setFields(ByVal City_Abr As String, ByVal City As String, ByVal Nickname As String,
                       ByVal Stadium As StadiumMdl, ByVal uniform As UniformMdl, ByVal Helmet_img_path As String, ByVal Players As List(Of PlayerMdl))

        Me.City_Abr = City_Abr
        Me.City = City
        Me.Nickname = Nickname
        Me.Stadium = Stadium
        Me.Uniform = uniform
        Me.Helmet_img_path = Helmet_img_path

        Me.Players = Players

        validate()
    End Sub

    Private Sub validate()

        If CommonUtils.isBlank(City_Abr) Then
            Throw New Exception("City Abbriviation must have a value")
        End If

        If CommonUtils.isBlank(City) Then
            Throw New Exception("City must have a value")
        End If

        If CommonUtils.isBlank(Nickname) Then
            Throw New Exception("Nickname must have a value")
        End If

        If CommonUtils.isBlank(Helmet_img_path) Then
            Throw New Exception("Helmet image path must have a value")
        End If

    End Sub



End Class
