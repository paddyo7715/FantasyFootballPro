'This eventArgs is used to pass the team number/id when someone clicks on a team label/name
Public Class teamEventArgs
    Inherits EventArgs

    Public team_num As Integer

    Public Sub New(ByVal team_num As Integer)
        Me.team_num = team_num
    End Sub
End Class
