Public Class TeamMdl

    Public Const PLAYERS_PER_TEAM = 18

    Property id As Integer
    Property City_Abr As String
    Property City As String
    Property Nickname As String
    Property Stadium As StadiumMdl
    Property Uniform As Uniform

    Property Players As List(Of PlayerMdl)
    Public Sub New(ByVal id As Integer, ByVal Nickname As String)
        Me.id = id
        Me.Nickname = Nickname
    End Sub

    Public Sub setFields(ByVal City_Abr As String, ByVal City As String, ByVal Nickname As String,
                       ByVal Stadium As StadiumMdl, ByVal uniform As Uniform)

        Me.City_Abr = City_Abr
        Me.City = City
        Me.Nickname = Nickname
        Me.Stadium = Stadium
        Me.Uniform = uniform

    End Sub



End Class
