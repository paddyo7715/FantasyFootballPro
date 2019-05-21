Public Class StockteamEventArgs
    Inherits EventArgs

    Public team As TeamMdl

    Public Sub New(ByVal team As TeamMdl)
        Me.team = team
    End Sub
End Class
