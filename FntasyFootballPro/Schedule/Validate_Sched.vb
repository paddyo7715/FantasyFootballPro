Public Class Validate_Sched
    Private League As String
    Private Teams As Integer
    Private TeamsperDiv As Integer
    Private Conferences As Integer
    Private Weeks As Integer
    Private byes As Integer
    Private nonDivConfGames As Integer
    Private nonConfgames As Integer

    Public Sub New(ByVal Short_Name As String, ByVal Number_of_Teams As Integer, ByVal Num_Teams_Per_Division As Integer,
                   ByVal Conferences As Integer, ByVal weeks As Integer, ByVal byes As Integer)

        Me.League = Short_Name
        Me.Teams = Number_of_Teams
        Me.TeamsperDiv = Num_Teams_Per_Division
        Me.Conferences = Conferences
        Me.Weeks = weeks
        Me.byes = byes
    End Sub

    Public Sub Validate(ByVal sched As List(Of String))

        Dim expected_games As Integer = ((Teams \ 2) * Weeks)
        Dim Actual_Games As Integer = sched.Count - 1
        If Actual_Games <> expected_games Then
            Throw New Exception("Invalid schedule created:  Incorrect number of league games scheduled." &
                                expected_games.ToString & " games expected, but " & Actual_Games &
                                " games were scheduled")
        End If

        For i As Integer = 1 To Teams
            Dim total_games As Integer
            Dim div_games As Integer
            total_games = 0
            div_games = 0

            For Each g In sched
                Dim m() As String = g.Split(",")
                Dim sWeek As String = m(0)
                Dim ht = m(1)
                Dim at = m(2)

                If sWeek.StartsWith("Week") Then Continue For

                If ht = i.ToString Or at = i.ToString Then
                    total_games += 1

                    If getDivision(ht.ToString) = getDivision(at.ToString) Then
                        div_games += 1
                    End If
                End If
            Next

            If total_games <> Weeks Then
                Throw New Exception("Schedule Error: Invalid number of games for team " & i.ToString)
            End If

            If div_games < (TeamsperDiv - 1) * 2 Then
                Throw New Exception("Schedule Error: Invalid number of divisional games for team " & i.ToString)
            End If

            For w = 1 To Weeks + byes
                If sched_weekly_team_game(w, i.ToString, sched) > 1 Then
                    Throw New Exception("Schedule Error: Team " & i.ToString & " is scheduled to play more than 1 game in week " & w.ToString)
                End If
            Next
        Next

    End Sub
    Private Function games_this_week(ByVal week As Integer, ByVal t As Integer, ByVal sched As List(Of String)) As Integer
        Dim r As Integer = 0

        For Each g In sched
            Dim m() As String = g.Split(",")
            If m(0) = week.ToString Then
                If m(1) = t.ToString Or m(2) = t.ToString Then r += 1
            End If
        Next

        Return r

    End Function

    Private Function getDivision(ByVal Team As Integer) As Integer
        Return (((Team - 1) _
                    \ TeamsperDiv) _
                    + 1)
    End Function

    Private Function sched_weekly_team_game(ByVal x As Integer, ByVal t As String, ByVal v As List(Of String)) As Integer
        Dim r As Integer = 0
        Dim qei As Integer = 0
        Do While (qei < v.Count)
            Dim q As String = CType(v(qei), String)
            Dim qt() As String = q.Split(",")
            If qt(0) = "Week Number" Then
                qei += 1
                Continue Do
            End If

            If (qt(0) = x.ToString) AndAlso (qt(1) = t OrElse qt(2) = t) Then
                r += 1
            End If

            qei += 1
        Loop

        Return r
    End Function
End Class