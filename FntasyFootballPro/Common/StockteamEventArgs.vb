Public Class StockteamEventArgs
    Inherits EventArgs

    Public team_ind As Integer

    Public Sub New(ByVal team_ind As Integer)
        Me.team_ind = team_ind
    End Sub
End Class
