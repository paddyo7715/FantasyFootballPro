Public Class Team_Services
    'This method is called when the user clicks Roll Players on the new team window
    Public Function Roll_Players(ByVal League_Teams As List(Of TeamMdl),
                                 ByVal league_DB_Connecdtion As String) As List(Of PlayerMdl)
        Dim r As List(Of PlayerMdl) = New List(Of PlayerMdl)
        Dim p As Player = New Player()

        'offense
        r.Add(p.CreatePlayer(PlayerMdl.Position.QB, League_Teams, r, 0, ""))
        r.Add(p.CreatePlayer(PlayerMdl.Position.RB, League_Teams, r, 0, ""))
        r.Add(p.CreatePlayer(PlayerMdl.Position.RB, League_Teams, r, 0, ""))
        r.Add(p.CreatePlayer(PlayerMdl.Position.WR, League_Teams, r, 0, ""))
        r.Add(p.CreatePlayer(PlayerMdl.Position.WR, League_Teams, r, 0, ""))
        r.Add(p.CreatePlayer(PlayerMdl.Position.OL, League_Teams, r, 0, ""))
        r.Add(p.CreatePlayer(PlayerMdl.Position.OL, League_Teams, r, 0, ""))
        r.Add(p.CreatePlayer(PlayerMdl.Position.OL, League_Teams, r, 0, ""))
        r.Add(p.CreatePlayer(PlayerMdl.Position.OL, League_Teams, r, 0, ""))
        r.Add(p.CreatePlayer(PlayerMdl.Position.OL, League_Teams, r, 0, ""))
        r.Add(p.CreatePlayer(PlayerMdl.Position.K, League_Teams, r, 0, ""))
        r.Add(p.CreatePlayer(PlayerMdl.Position.P, League_Teams, r, 0, ""))

        'Defense
        r.Add(p.CreatePlayer(PlayerMdl.Position.DL, League_Teams, r, 0, ""))
        r.Add(p.CreatePlayer(PlayerMdl.Position.DL, League_Teams, r, 0, ""))
        r.Add(p.CreatePlayer(PlayerMdl.Position.DL, League_Teams, r, 0, ""))
        r.Add(p.CreatePlayer(PlayerMdl.Position.DL, League_Teams, r, 0, ""))
        r.Add(p.CreatePlayer(PlayerMdl.Position.DL, League_Teams, r, 0, ""))
        r.Add(p.CreatePlayer(PlayerMdl.Position.LB, League_Teams, r, 0, ""))
        r.Add(p.CreatePlayer(PlayerMdl.Position.CB, League_Teams, r, 0, ""))
        r.Add(p.CreatePlayer(PlayerMdl.Position.CB, League_Teams, r, 0, ""))

        Return r
    End Function
End Class
