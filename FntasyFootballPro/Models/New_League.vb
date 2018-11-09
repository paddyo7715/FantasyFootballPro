Public Class New_League
    Inherits Base_League


    Sub New(ByVal Short_Name As String, ByVal Long_Name As String, ByVal Starting_Year As Integer,
            ByVal Number_of_weeks As Integer, ByVal Number_of_Games As Integer,
            ByVal Championship_Game_Name As String, ByVal Num_Divisions As Integer,
            ByVal Num_Teams_Per_Division As Integer, ByVal Division1 As String, ByVal Division2 As String,
            ByVal Division3 As String, ByVal Division4 As String,
            ByVal Division5 As String, ByVal Division6 As String,
            ByVal Division7 As String, ByVal Division8 As String,
            ByVal Conference1 As String, ByVal Conference2 As String)

        Me.Short_Name = Short_Name
        Me.Long_Name = Long_Name
        Me.Starting_Year = Starting_Year
        Me.Number_of_weeks = Number_of_weeks
        Me.Number_of_Games = Number_of_Games
        Me.Championship_Game_Name = Championship_Game_Name
        Me.Num_Divisions = Num_Divisions
        Me.Num_Teams_Per_Division = Num_Teams_Per_Division

        Divisions.Add(Division1)
        Divisions.Add(Division2)
        Divisions.Add(Division3)
        Divisions.Add(Division4)
        Divisions.Add(Division5)
        Divisions.Add(Division6)
        Divisions.Add(Division7)
        Divisions.Add(Division8)

        Conferences.Add(Conference1)
        Conferences.Add(Conference2)

        For i As Integer = 0 To Num_Divisions * Num_Teams_Per_Division - 1
            Teams.Add(New TeamMdl(i + 1, "New Team"))
        Next



    End Sub

End Class
