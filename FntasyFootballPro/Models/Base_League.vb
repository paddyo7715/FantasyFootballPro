Public Class Base_League
    Property Short_Name As String
    Property Long_Name As String
    Property Starting_Year As Integer
    Property Number_of_weeks As Integer
    Property Number_of_Games As Integer
    Property Championship_Game_Name As String
    Property Num_Divisions As Integer
    Property Num_Teams_Per_Division As Integer

    Property Conferences As List(Of String) = New List(Of String)
    Property Divisions As List(Of String) = New List(Of String)

    Property Teams As List(Of TeamMdl) = New List(Of TeamMdl)
End Class
