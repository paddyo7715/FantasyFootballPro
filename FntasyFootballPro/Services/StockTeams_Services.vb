﻿Public Class StockTeams_Services
    Public Function getAllStockTeams() As List(Of TeamMdl)
        Dim r As List(Of TeamMdl) = New List(Of TeamMdl)
        Dim StockTeamDAO = New Stock_TeamsDAO()
        r = StockTeamDAO.getAllStockTeams()
        Return r
    End Function
    Public Sub AddStockTeam(ByVal Team As TeamMdl)
        Dim StockTeamDAO = New Stock_TeamsDAO()
        StockTeamDAO.AddStockTeam(Team)
    End Sub
    Public Sub DeleteStockTeam(ByVal t_id As Integer)

    End Sub

End Class