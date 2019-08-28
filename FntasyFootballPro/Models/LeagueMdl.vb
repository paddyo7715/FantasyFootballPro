Public Class Leaguemdl
    Public Enum League_State
        Regular_Season
        Playoffs
        Season_Complete
        Prev_Season
    End Enum

    Property Short_Name As String = ""
    Property Long_Name As String = ""
    Property Starting_Year As Integer
    Property Number_of_weeks As Integer
    Property Number_of_Games As Integer
    Property Championship_Game_Name As String = ""
    Property Trophy_filepath As String = ""
    Property Num_Teams As Integer
    Property Num_Playoff_Teams As Integer

    Property Years As List(Of Integer)

    Property State As League_State = Nothing

    Property Conferences As List(Of String) = New List(Of String)
    Property Divisions As List(Of String) = New List(Of String)

    Property Teams As List(Of TeamMdl) = New List(Of TeamMdl)

    Property Schedule As List(Of String) = Nothing
    Public Sub setOrganization(ByVal Number_of_weeks As Integer, ByVal Number_of_Games As Integer,
                               ByVal Num_Teams As Integer, ByVal Num_Playoff_Teams As Integer)
        Me.Number_of_weeks = Number_of_weeks
        Me.Number_of_Games = Number_of_Games
        Me.Num_Teams = Num_Teams
        Me.Num_Playoff_Teams = Num_Playoff_Teams

        Teams = New List(Of TeamMdl)

        For i As Integer = 1 To Me.Num_Teams
            Teams.Add(New TeamMdl(i, App_Constants.EMPTY_TEAM_SLOT))
        Next
    End Sub
    Public Sub setBasicInfo(ByVal Short_Name As String,
            ByVal Long_Name As String, ByVal Starting_Year As Integer,
            ByVal Championship_Game_Name As String, ByVal Trophy_Filepath As String,
            ByVal Conferences As List(Of String),
            ByVal Divisions As List(Of String),
            ByVal Years As List(Of Integer),
            ByVal State As League_State)

        Me.Short_Name = Short_Name
        Me.Long_Name = Long_Name
        Me.Starting_Year = Starting_Year
        Me.Championship_Game_Name = Championship_Game_Name
        Me.Trophy_filepath = Trophy_Filepath

        Me.Conferences = Conferences
        Me.Divisions = Divisions

        Me.Years = Years
        Me.State = State

    End Sub
    Public Sub setSchedule(ByVal Schedule As List(Of String))
        Me.Schedule = Schedule
    End Sub

End Class
