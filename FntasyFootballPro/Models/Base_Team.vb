Public MustInherit Class Base_Team

    Public Const PLAYERS_PER_TEAM = 18

    Property City_Abr As String
    Property City As String
    Property Nickname As String
    Property Stadium As Stadium
    Property Uniform As Uniform

    Property Players As List(Of New_Player)

    Public Sub setFields(ByVal City_Abr As String, ByVal City As String, ByVal Nickname As String,
                       ByVal Stadium As Stadium, ByVal uniform As Uniform)

        Me.City_Abr = City_Abr
        Me.City = City
        Me.Nickname = Nickname
        Me.Stadium = Stadium
        Me.Uniform = uniform

    End Sub



End Class
