Public Class Schedule
    Private League As String
    Private Teams As Integer
    Private TeamsperDiv As Integer
    Private Conferences As Integer
    Private Weeks As Integer
    Private byes As Integer
    Private nonDivConfGames As Integer
    Private nonConfgames As Integer
    Private sched As List(Of String) = New List(Of String)
    Private rha As Random = New Random()

    Public Sub New(ByVal Short_Name As String, ByVal Number_of_Teams As Integer, ByVal Num_Teams_Per_Division As Integer,
                   ByVal Conferences As Integer, ByVal weeks As Integer, ByVal byes As Integer)

        Me.League = Short_Name
        Me.Teams = Number_of_Teams
        Me.TeamsperDiv = Num_Teams_Per_Division
        Me.Conferences = Conferences
        Me.Weeks = weeks
        Me.byes = byes
    End Sub

    Private Function getDivision(ByVal Team As Integer) As Integer
        Return ((Team - 1) \ TeamsperDiv) + 1
    End Function

    Private Function getConference(ByVal Team As Integer) As Integer

        If Conferences = 0 Then
            Return 1
        Else
            Return (Team - 1) \ (Teams \ Conferences) + 1
        End If

    End Function

    Private Function game_type_fianl_sched(ByVal v As List(Of String), ByVal T As Integer) As Integer()
        Dim home_games As Integer = 0
        Dim away_games As Integer = 0
        Dim nondiv_conf_games As Integer = 0
        Dim nonconf_games As Integer = 0

        For Each lv In v
            Dim g As String = lv
            Dim m() As String = g.Split(",")
            If m(1) = T.ToString Then
                home_games = (home_games + 1)
                If getConference(m(1).ToString) = getConference(m(2).ToString) _
                            AndAlso getDivision(m(1).ToString) <> getDivision(m(2).ToString) Then
                    nondiv_conf_games = (nondiv_conf_games + 1)
                End If

                If getConference(m(1).ToString) <> getConference(m(2).ToString) Then
                    nonconf_games = (nonconf_games + 1)
                End If

            End If

            If m(2) = T.ToString Then
                away_games = (away_games + 1)
                If getConference(m(2)) = getConference(m(1)) _
                            AndAlso getDivision(m(2)) <> getDivision(m(1)) Then
                    nondiv_conf_games = (nondiv_conf_games + 1)
                End If

                If getConference(m(2)) <> getConference(m(1)) Then
                    nonconf_games = (nonconf_games + 1)
                End If

            End If

        Next

        Return New Integer() {home_games, away_games, nondiv_conf_games, nonconf_games}
    End Function

    Private Function game_types(ByVal T As Integer) As Integer()
        Dim home_games As Integer = 0
        Dim away_games As Integer = 0
        Dim nondiv_conf_games As Integer = 0
        Dim nonconf_games As Integer = 0

        For Each lv In sched
            Dim g As String = lv
            Dim m() As String = g.Split(",")
            If m(0) = T.ToString Then
                home_games = (home_games + 1)
                If getConference(m(0).ToString) = getConference(m(1).ToString) _
                            AndAlso getDivision(m(0).ToString) <> getDivision(m(1).ToString) Then
                    nondiv_conf_games = (nondiv_conf_games + 1)
                End If

                If getConference(m(0).ToString) <> getConference(m(1).ToString) Then
                    nonconf_games = (nonconf_games + 1)
                End If

            End If

            If m(1) = T.ToString Then
                away_games = (away_games + 1)
                If getConference(m(1).ToString) = getConference(m(0).ToString) _
                            AndAlso getDivision(m(1).ToString) <> getDivision(m(0).ToString) Then
                    nondiv_conf_games = (nondiv_conf_games + 1)
                End If

                If getConference(m(1).ToString) <> getConference(m(0).ToString) Then
                    nonconf_games = (nonconf_games + 1)
                End If

            End If

        Next

        Return New Integer() {home_games, away_games, nondiv_conf_games, nonconf_games}
    End Function

    Public Function DupeGame(ByVal Home As Integer, ByVal Away As Integer) As Boolean
        Dim r As Boolean = False

        For Each lv In sched
            Dim g As String = lv
            Dim m() As String = g.Split(",")
            If ((m(0) = Home.ToString) OrElse m(0) = Away.ToString) _
                        AndAlso (m(1) = Home.ToString OrElse m(1) = Away.ToString) Then
                r = True
                Exit For
            End If

        Next

        Return r
    End Function

    Public Function Generate_Regular_Schedule() As List(Of String)

        Dim max_weeks As Integer
        Dim Weekly_sched As List(Of String)
        Dim expected_home_games As Integer
        Dim expected_away_games As Integer
        Dim i As Integer
        Dim tries As Integer

START_METHOD:
        Dim gt() As Integer = Nothing
        max_weeks = 100
        Weekly_sched = New List(Of String)
        Weekly_sched.Add("Week Number,Home Team ID,Away Team ID")
        expected_home_games = (Me.Weeks \ 2)
        expected_away_games = expected_home_games
        i = 1
        tries = 0

        'Create division games
        Do While (i <= Me.Teams)
            Dim cur_div As Integer = Me.getDivision(i)
            Dim beg_div As Integer = ((cur_div * Me.TeamsperDiv) + (1 - Me.TeamsperDiv))
            Dim cur_conf As Integer = Me.getConference(i)
            Dim x As Integer = beg_div - 1
            Do While (x < ((beg_div + TeamsperDiv) - 1))
                x += 1
                If (i = x) Then
                    Continue Do
                End If

                If Me.DupeGame(i, x) Then
                    Continue Do
                End If

                sched.Add((i.ToString + ("," + x.ToString)))
                sched.Add((x.ToString + ("," + i.ToString)))
            Loop

            i += 1
        Loop

        '        file.WriteLine("Finished scheduling div games")

        'Create all other games        
        '
        Dim num_of_weeks = (((Teams / 2) * Weeks) - (Teams * (TeamsperDiv - 1))) / (Teams / 2)
        Dim wg_count As Integer = 0
        Do While (wg_count < num_of_weeks)
            Dim t As Integer
            Dim ii As Integer
            Dim wgcount As Integer = (Me.Teams \ 2)
            Dim wgames() As String = New String((wgcount) - 1) {}
            Dim wg_now As Integer = 0
            'clear the weekly games           
            For Each s As String In wgames
                s = Nothing
            Next
            tries = 0
            '            Dim rha As Random = New Random()
            While True
                Dim home As Integer = 0
                Dim away As Integer = 0
                ii = (rha.Next(Me.Teams) + 1)
                t = (rha.Next(Me.Teams) + 1)

                tries += 1

                If (tries = 2000) Then
                    wgames = New String((wgcount) - 1) {}
                    wg_now = 0
                    tries = 0
                    '                    file.WriteLine("tries exceed If (tries = 2000) starting over")
                End If

                'if teams are equal then pick new teams.
                If (ii = t) Then
                    Continue While
                End If

                'if same division then pick new teams.
                If (getDivision(ii) = getDivision(t)) Then
                    Continue While
                End If

                gt = Me.game_types(ii)
                Dim a As Integer = (gt(0) - gt(1))
                gt = Me.game_types(t)
                Dim b As Integer = (gt(0) - gt(1))
                If (a < b) Then
                    home = ii
                    away = t
                Else
                    home = t
                    away = ii
                End If

                Dim pgame As String = home.ToString + "," + away.ToString
                If Not Me.dupe_weekly_game(wgames, pgame) Then
                    wgames(wg_now) = pgame
                    wg_now += 1
                End If

                If (wg_now = wgcount) Then
                    Exit While
                End If

            End While

            sched.AddRange(wgames)
            '           file.WriteLine(("Number of Games Scheduled " + sched.Count.ToString))
            wg_count += 1
        Loop

        'Make sure that all teams have an even number of home and away games.
        tries = 0
EVEN_HOME_AND_AWAY:

        Dim g As Integer = 0
        For i = 1 To Me.Teams
            Do While 1 = 1
                Dim Even_non_Div As List(Of Integer) = New List(Of Integer)
                Dim need_to_flip_even_other As Boolean = True

                If (tries >= 2000) Then
                    '                    file.WriteLine("trying to even out teams failed, starting completely over.")
                    GoTo START_METHOD
                End If

                tries += 1

                Dim g1() As Integer = Me.game_types(i)
                Dim ha_diff_i As Integer = g1(0) - g1(1)

                'if the team has an too many home games then continue.
                If ha_diff_i <= 0 Then
                    need_to_flip_even_other = False
                    Exit Do
                End If

                'go thru the games and try to find a game for this team where they play another team that has 
                'the opposite home/away defecit
                g = 0
                Do While g < sched.Count() - 1

                    Dim other_team As Integer = 0
                    Dim p_game() As String = sched(g).Split(",")
                    Dim h As Integer = p_game(0)
                    Dim a As Integer = p_game(1)

                    If h = i Then
                        other_team = CInt(p_game(1))
                    ElseIf a = i Then
                        other_team = CInt(p_game(0))
                    End If

                    'if the team i is not either the home or away store the game to possibly 
                    'shift if no other actual candidate is found then check the next game
                    If other_team = 0 Then
                        g += 1
                        Continue Do
                    End If

                    'if the two teams are in the same division then the game can not be flipped.
                    If getDivision(i) = getDivision(other_team) Then
                        g += 1
                        Continue Do
                    End If

                    Dim other_team_totals() As Integer = Me.game_types(other_team)

                    Dim other_diff As Integer = other_team_totals(0) - other_team_totals(1)

                    'if the other team has an even number of home and away then write to list of
                    'possible even games to home and away flip with this game if a better candidate
                    'is not found.
                    If other_diff = 0 And h = i Then
                        Even_non_Div.Add(g)
                    End If

                    'if the other team has an imbalace of home and away games in the other direction then
                    'swap the home and away game.
                    If (h = i And ha_diff_i > 0 And other_diff < 0) Then
                        need_to_flip_even_other = False
                        sched(g) = a.ToString & "," & h.ToString
                        '                        file.WriteLine("home and away switched to " & a.ToString & "," & h.ToString)
                        Exit Do
                    End If
                    g += 1
                Loop 'Do While g < sched.Count() - 1

                'If this team (i) had two many home games but couldn't find a game with a team with too many 
                'away games to flip then flip a random game with an opponent that has an even number of home
                'and away games and then start the home/away evening out all over.

                If need_to_flip_even_other Then

                    If Even_non_Div.Count = 0 Then
                        '                        file.WriteLine("no even team to flip as last resort.  Starting whole process again")
                        GoTo START_METHOD
                    End If

                    Dim rand_g As Integer = CommonUtils.getRandomNum(0, Even_non_Div.Count - 1)
                    Dim p_game() As String = sched(Even_non_Div(rand_g)).Split(",")
                    Dim h As Integer = p_game(0)
                    Dim a As Integer = p_game(1)
                    sched(Even_non_Div(rand_g)) = a.ToString & "," & h.ToString
                    '                    file.WriteLine("game " & rand_g & " changed to " & a.ToString & "," & h.ToString)
                    GoTo EVEN_HOME_AND_AWAY
                End If

            Loop 'Do While 1 = 1
        Next

        '       file.WriteLine(("Finished scheduling all other games sched size " + sched.Count.ToString))
        'Print out game totals for each team
        '       file.WriteLine("Game totals:")
        Dim yyu As Integer = 1
        Do While (yyu <= Teams)
            Dim zt() As Integer = Me.game_types(yyu)
            '            file.WriteLine(yyu.ToString + " " + zt(0).ToString + " " + zt(1).ToString + " " + zt(2).ToString + " " + zt(3).ToString)
            yyu += 1
        Loop

        'schedule weekly games            
        Dim w As Integer = 1
        Do While (w <= (Me.Weeks + Me.byes))
            If (sched.Count = 0) Then
                Exit Do
            End If

            '            file.WriteLine(("Week " + w.ToString))
            Dim games As Integer = Teams \ 2
            Dim wi As Integer = 0
            Dim week_arr() As String = New String((games) - 1) {}
            Dim pg As String = Nothing
            tries = 0

            While True
                tries += 1
                If (tries >= 50000) Then
                    tries = 0
                    '                    file.WriteLine("tries exceed If (tries >= 50000) starting over")
                    Exit While
                End If

                Dim gg As Integer = (rha.Next(sched.Count) + 1)

                gg -= 1

                pg = CType(sched(gg), String)

                If Not Me.dupe_weekly_game(week_arr, pg) Then
                    sched.RemoveAt(gg)
                    week_arr(wi) = pg
                    wi = (wi + 1)
                    tries = 0
                End If

                If ((wi = games) OrElse (Me.sched.Count = 0)) Then
                    Exit While
                End If


            End While

            'Move week to overall sched vector
            For Each wkq As String In week_arr
                If (Not (wkq) Is Nothing) Then
                    Weekly_sched.Add(w.ToString + "," + wkq)
                End If

            Next
            w = (w + 1)
        Loop

        '        file.WriteLine("Scheduling left over games")
        tries = 0
        While (sched.Count > 0)
            '           file.WriteLine(("Left over games: " + sched.Count.ToString))
            Dim r2 As Random = New Random
            Dim lo_rand As Integer = r2.Next(sched.Count)
            Dim lo_game As String = Nothing

            tries += 1
            If (tries >= 200000) Then
                '                file.WriteLine("exceeded tries >= 200000) goto outer ")
                GoTo OUTER_DO
            End If

            Dim w3 As Integer = 1
            Do While (w3 <= (Weeks + byes))
                lo_game = CType(sched(lo_rand), String)

                If (sched_games_for_week(w3, Weekly_sched) _
                            = (Me.Teams \ 2)) Then
                    w3 += 1
                    Continue Do
                End If

                Dim t2() As String = lo_game.Split(",")
                Dim h As String = Me.sched_weekly_team_game(w3, t2(0), Weekly_sched, True)
                Dim a As String = Me.sched_weekly_team_game(w3, t2(1), Weekly_sched, False)
                If ((h Is Nothing) OrElse (a Is Nothing)) Then
                    Dim bw As Integer = (r2.Next(2) + 1)
                    If (bw = 2) Then
                        w3 += 1
                        Continue Do
                    End If

                    'to fix the issue of the null pointer exception caused by both h and a being nothing
                    If IsNothing(h) And IsNothing(a) Then
                        w3 += 1
                        Continue Do
                    End If

                    Dim ng As String = Nothing
                    If Not IsNothing(h) Then
                        ng = h
                    Else
                        ng = a
                    End If

                    'bpo debug
                    If IsNothing(ng) Then
                        Dim zzz = 5
                    End If

                    Dim n_index As Integer = sched_game_index(ng, Weekly_sched)
                    swap_game(w3, lo_game, ng, sched, lo_rand, Weekly_sched, n_index)
                    Dim www As Integer = (Weeks + byes)
                    Do While (www > 0)
                        Dim yy() As String = ng.Split(",")
                        Dim hh As String = Me.sched_weekly_team_game(www, yy(1), Weekly_sched, True)
                        Dim aa As String = Me.sched_weekly_team_game(www, yy(2), Weekly_sched, False)
                        If ((hh Is Nothing) AndAlso (aa Is Nothing)) Then
                            add_game_to_weekly_sched(Weekly_sched, www, ng)
                            sched.RemoveAt(lo_rand)
                            GoTo OUTER_DO
                        End If

                        www -= 1
                    Loop

                End If

                w3 += 1
            Loop
OUTER_DO:

        End While

        Return Weekly_sched

    End Function

    Private Sub add_game_to_weekly_sched(ByVal v As List(Of String), ByVal w As Integer, ByVal game As String)
        Dim u() As String = game.Split(",")
        Dim qei As Integer = 0
        Do While (qei < v.Count)
            Dim q As String = CType(v(qei), String)
            Dim gt() As String = q.Split(",")
            If gt(0) = w.ToString Then
                v.Insert(qei, gt(0).ToString + "," + u(1) + "," + u(2))
                Exit Do
            End If

            qei = (qei + 1)
        Loop

    End Sub

    Private Sub swap_game(ByVal week As Integer, ByVal lef_over_game As String, ByVal scheded_game As String, ByVal sched As List(Of String), ByVal sched_index As Integer, ByVal weekly_sched As List(Of String), ByVal ws_index As Integer)
        Dim gt() As String = scheded_game.Split(",")
        sched(sched_index) = gt(1) + "," + gt(2)
        weekly_sched(ws_index) = week.ToString + "," + lef_over_game
    End Sub

    Private Function sched_game_index(ByVal g As String, ByVal v As List(Of String)) As Integer
        Dim r As Integer = -1
        Dim qei As Integer = 0
        Do While (qei < v.Count)
            Dim q As String = CType(v(qei), String)
            If g = q Then
                r = qei
                Exit Do
            End If

            qei += 1
        Loop

        Return r
    End Function

    Private Function sched_weekly_team_game(ByVal x As Integer, ByVal t As String, ByVal v As List(Of String), ByVal home As Boolean) As String
        Dim r As String = Nothing
        Dim qei As Integer = 0
        Do While (qei < v.Count)
            Dim q As String = CType(v(qei), String)
            Dim qt() As String = q.Split(",")
            If qt(0).Equals("Week Number") Then
                qei += 1
                Continue Do
            End If

            If (qt(0) = x.ToString) AndAlso (qt(1) = t OrElse qt(2) = t) Then
                r = q
                Exit Do
            End If

            qei += 1
        Loop

        Return r
    End Function

    Private Function sched_games_for_week(ByVal x As Integer, ByVal v As List(Of String)) As Integer
        Dim r As Integer = 0
        Dim qei As Integer = 0
        Do While (qei < v.Count)
            Dim q As String = CType(v(qei), String)
            Dim qt() As String = q.Split(",")
            If qt(0).Equals("Week Number") Then
                qei += 1
                Continue Do
            End If

            If (qt(0) = x.ToString) Then
                r += 1
            End If

            qei += 1
        Loop

        Return r
    End Function

    Private Function dupe_weekly_game(ByVal w() As String, ByVal g As String) As Boolean
        Dim r As Boolean = False
        Dim m() As String = g.Split(",")
        Dim g1 As Integer = CType(m(0), Integer)
        Dim g2 As Integer = CType(m(1), Integer)
        Dim i As Integer = 0
        Do While (i < w.Length)
            If (w(i) Is Nothing) Then
                Exit Do
            End If

            Dim m2() As String = w(i).Split(",")
            Dim w1 As Integer = CType(m2(0), Integer)
            Dim w2 As Integer = CType(m2(1), Integer)
            If ((g1 = w1) OrElse (g1 = w2)) Then
                r = True
                Exit Do
            End If

            If ((g2 = w1) _
                        OrElse (g2 = w2)) Then
                r = True
                Exit Do
            End If

            i = (i + 1)
        Loop

        Return r
    End Function

End Class

