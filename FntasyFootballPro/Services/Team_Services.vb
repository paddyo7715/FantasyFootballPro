Public Class Team_Services
    'This method is called when the user clicks Roll Players on the new team window
    Public Function Roll_Players(ByVal League_Teams As List(Of TeamMdl),
                                 ByVal league_DB_Connecdtion As String) As List(Of PlayerMdl)
        Dim r As List(Of PlayerMdl) = New List(Of PlayerMdl)
        Dim p As Player = New Player()
        Dim i As Integer = 0

        'offense
        For i = 1 To App_Constants.QB_PER_TEAM
            r.Add(p.CreatePlayer(PlayerMdl.Position.QB, League_Teams, r, 0, ""))
        Next

        For i = 1 To App_Constants.RB_PER_TEAM
            r.Add(p.CreatePlayer(PlayerMdl.Position.RB, League_Teams, r, 0, ""))
        Next

        For i = 1 To App_Constants.WR_PER_TEAM
            r.Add(p.CreatePlayer(PlayerMdl.Position.WR, League_Teams, r, 0, ""))
        Next

        For i = 1 To App_Constants.OL_PER_TEAM
            r.Add(p.CreatePlayer(PlayerMdl.Position.OL, League_Teams, r, 0, ""))
        Next

        'Defense
        For i = 1 To App_Constants.DL_PER_TEAM
            r.Add(p.CreatePlayer(PlayerMdl.Position.DL, League_Teams, r, 0, ""))
        Next

        For i = 1 To App_Constants.LB_PER_TEAM
            r.Add(p.CreatePlayer(PlayerMdl.Position.LB, League_Teams, r, 0, ""))
        Next

        For i = 1 To App_Constants.DB_PER_TEAM
            r.Add(p.CreatePlayer(PlayerMdl.Position.DB, League_Teams, r, 0, ""))
        Next

        'Special Teams
        For i = 1 To App_Constants.K_PER_TEAM
            r.Add(p.CreatePlayer(PlayerMdl.Position.K, League_Teams, r, 0, ""))
        Next

        For i = 1 To App_Constants.P_PER_TEAM
            r.Add(p.CreatePlayer(PlayerMdl.Position.P, League_Teams, r, 0, ""))
        Next

        Return r
    End Function
End Class
