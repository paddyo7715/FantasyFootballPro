Public Class TeamUpdatedEventArgs
    Inherits EventArgs

    Public team_upd As Boolean

    Public Sub New(ByVal team_upd As Boolean)
        Me.team_upd = team_upd
    End Sub
End Class
