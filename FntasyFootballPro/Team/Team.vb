Public Class Team


    Public Shared Function get_team_from_name(ByVal team_city_name As String, ByVal t_list As List(Of TeamMdl)) As TeamMdl
        Dim r As TeamMdl = Nothing

        For Each t In t_list
            Dim t_city_name As String = t.City & " " & t.Nickname
            If t_city_name.Trim = team_city_name.Trim Then
                r = t
                Exit For
            End If
        Next

        Return r

    End Function


End Class
