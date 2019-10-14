Imports log4net

Public Class StockTeams_Services
    Private Shared logger As ILog = LogManager.GetLogger("RollingFile")

    Public Function getAllStockTeams() As List(Of TeamMdl)
        Dim r As List(Of TeamMdl) = New List(Of TeamMdl)
        Dim StockTeamDAO = New Stock_TeamsDAO()
        r = StockTeamDAO.getAllStockTeams()
        Return r
    End Function
    Public Sub AddStockTeam(ByVal Team As TeamMdl)
        logger.Info("Adding new stock team " & Team.Nickname)
        Dim StockTeamDAO = New Stock_TeamsDAO()
        StockTeamDAO.AddStockTeam(Team)
    End Sub
    Public Sub DeleteStockTeam(ByVal t_id As Integer)
        logger.info("Deleting stock team with id " & t_id)
        Dim StockTeamDAO = New Stock_TeamsDAO()
        StockTeamDAO.DeleteStockTeam(t_id)
    End Sub
    Public Sub UpdateStockTeam(ByVal Team As TeamMdl)
        logger.Info("Updating stock team " & Team.Nickname)
        Dim StockTeamDAO = New Stock_TeamsDAO()
        StockTeamDAO.UpdateStockTeam(Team)
    End Sub
    Public Function DoesTeamAlreadyExist_ID(ByVal City As String, ByVal Nickname As String, ByVal original_City As String, ByVal original_Nickname As String) As Boolean

        Return New Stock_TeamsDAO().DoesTeamAlreadyExist_ID(City, Nickname, original_City, original_Nickname)

    End Function
    Public Function DoesTeamAlreadyExist(ByVal City As String, ByVal Nickname As String) As Boolean

        Return New Stock_TeamsDAO().DoesTeamAlreadyExist(City, Nickname)

    End Function

End Class
