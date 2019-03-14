Public Class Leaguemdl
    Property Logo_Filepath As String = ""
    Property Short_Name As String = ""
    Property Long_Name As String = ""
    Property Starting_Year As Integer
    Property Number_of_weeks As Integer
    Property Number_of_Games As Integer
    Property Championship_Game_Name As String = ""
    Property Trophy_filepath As String = ""
    Property Num_Teams As Integer
    Property Num_Playoff_Teams As Integer


    Property Conferences As List(Of String) = New List(Of String)
    Property Divisions As List(Of String) = New List(Of String)

    Property Teams As List(Of TeamMdl) = New List(Of TeamMdl)

    Sub New(ByVal Logo_Filepath As String, ByVal Short_Name As String,
            ByVal Long_Name As String, ByVal Starting_Year As Integer,
            ByVal Number_of_weeks As Integer, ByVal Number_of_Games As Integer,
            ByVal Championship_Game_Name As String, ByVal Trophy_Filepath As String,
            ByVal Num_Teams As Integer, ByVal Num_Playoff_Teams As Integer,
            ByVal Conferences As List(Of String),
            ByVal Divisions As List(Of String))

        Me.Logo_Filepath = Logo_Filepath
        Me.Short_Name = Short_Name
        Me.Long_Name = Long_Name
        Me.Starting_Year = Starting_Year
        Me.Number_of_weeks = Number_of_weeks
        Me.Number_of_Games = Number_of_Games
        Me.Championship_Game_Name = Championship_Game_Name
        Me.Trophy_filepath = Trophy_Filepath
        Me.Num_Teams = Num_Teams
        Me.Num_Playoff_Teams = Num_Playoff_Teams

        Me.Conferences = Conferences
        Me.Divisions = Divisions

        For i As Integer = 1 To Me.Num_Teams
            Teams.Add(New TeamMdl(i, "New Team"))
        Next
    End Sub

End Class
