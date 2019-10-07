Public Class Team_Services
    'This method is called when the user clicks Roll Players on the new team window
    Public Function Roll_Players(ByVal league_DB_Connecdtion As String) As List(Of PlayerMdl)
        Dim r As List(Of PlayerMdl) = New List(Of PlayerMdl)
        Dim p As Player = New Player()
        Dim i As Integer = 0

        'offense
        For i = 1 To App_Constants.QB_PER_TEAM
            r.Add(p.CreatePlayer(PlayerMdl.Position.QB, r, 0, ""))
        Next

        For i = 1 To App_Constants.RB_PER_TEAM
            r.Add(p.CreatePlayer(PlayerMdl.Position.RB, r, 0, ""))
        Next

        For i = 1 To App_Constants.WR_PER_TEAM
            r.Add(p.CreatePlayer(PlayerMdl.Position.WR, r, 0, ""))
        Next

        For i = 1 To App_Constants.TE_PER_TEAM
            r.Add(p.CreatePlayer(PlayerMdl.Position.TE, r, 0, ""))
        Next

        For i = 1 To App_Constants.OL_PER_TEAM
            r.Add(p.CreatePlayer(PlayerMdl.Position.OL, r, 0, ""))
        Next

        'Defense
        For i = 1 To App_Constants.DL_PER_TEAM
            r.Add(p.CreatePlayer(PlayerMdl.Position.DL, r, 0, ""))
        Next

        For i = 1 To App_Constants.LB_PER_TEAM
            r.Add(p.CreatePlayer(PlayerMdl.Position.LB, r, 0, ""))
        Next

        For i = 1 To App_Constants.DB_PER_TEAM
            r.Add(p.CreatePlayer(PlayerMdl.Position.DB, r, 0, ""))
        Next

        'Special Teams
        For i = 1 To App_Constants.K_PER_TEAM
            r.Add(p.CreatePlayer(PlayerMdl.Position.K, r, 0, ""))
        Next

        For i = 1 To App_Constants.P_PER_TEAM
            r.Add(p.CreatePlayer(PlayerMdl.Position.P, r, 0, ""))
        Next

        Return r
    End Function

    Public Function FirstDuplicateTeam(ByVal Team_List As List(Of TeamMdl)) As String
        Dim r As String = Nothing
        Dim hs As HashSet(Of String) = New HashSet(Of String)

        For Each t In Team_List
            Dim s As String = t.City.ToUpper.Trim & " " & t.Nickname.ToUpper.Trim
            If hs.Contains(s) Then
                r = s
                Exit For
            Else
                hs.Add(s)
            End If
        Next

        Return r

    End Function

End Class
