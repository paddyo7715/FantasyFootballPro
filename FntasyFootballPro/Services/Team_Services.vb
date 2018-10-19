Public Class Team_Services
    'This method is called when the user clicks Roll Players on the new team window
    Public Function Roll_Players(ByVal League_Teams As List(Of New_Team),
                                 ByVal league_DB_Connecdtion As String) As List(Of New_Player)
        Dim r As List(Of New_Player) = New List(Of New_Player)
        Dim p As Player = New Player()

        'offense
        r.Add(p.CreatePlayer(Base_Player.Position.QB, League_Teams, r, 0, ""))
        r.Add(p.CreatePlayer(Base_Player.Position.RB, League_Teams, r, 0, ""))
        r.Add(p.CreatePlayer(Base_Player.Position.RB, League_Teams, r, 0, ""))
        r.Add(p.CreatePlayer(Base_Player.Position.WR, League_Teams, r, 0, ""))
        r.Add(p.CreatePlayer(Base_Player.Position.WR, League_Teams, r, 0, ""))
        r.Add(p.CreatePlayer(Base_Player.Position.OL, League_Teams, r, 0, ""))
        r.Add(p.CreatePlayer(Base_Player.Position.OL, League_Teams, r, 0, ""))
        r.Add(p.CreatePlayer(Base_Player.Position.OL, League_Teams, r, 0, ""))
        r.Add(p.CreatePlayer(Base_Player.Position.OL, League_Teams, r, 0, ""))
        r.Add(p.CreatePlayer(Base_Player.Position.OL, League_Teams, r, 0, ""))
        r.Add(p.CreatePlayer(Base_Player.Position.K, League_Teams, r, 0, ""))
        r.Add(p.CreatePlayer(Base_Player.Position.P, League_Teams, r, 0, ""))

        'Defense
        r.Add(p.CreatePlayer(Base_Player.Position.DL, League_Teams, r, 0, ""))
        r.Add(p.CreatePlayer(Base_Player.Position.DL, League_Teams, r, 0, ""))
        r.Add(p.CreatePlayer(Base_Player.Position.DL, League_Teams, r, 0, ""))
        r.Add(p.CreatePlayer(Base_Player.Position.DL, League_Teams, r, 0, ""))
        r.Add(p.CreatePlayer(Base_Player.Position.DL, League_Teams, r, 0, ""))
        r.Add(p.CreatePlayer(Base_Player.Position.LB, League_Teams, r, 0, ""))
        r.Add(p.CreatePlayer(Base_Player.Position.CB, League_Teams, r, 0, ""))
        r.Add(p.CreatePlayer(Base_Player.Position.CB, League_Teams, r, 0, ""))

        Return r
    End Function
End Class
