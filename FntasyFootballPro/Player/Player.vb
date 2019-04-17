Public Class Player
    Public Function CreatePlayer(ByVal pos As PlayerMdl.Position, ByVal Players As List(Of PlayerMdl),
                             ByVal team_ind As Integer, ByVal league_DB_Connecdtion As String) As PlayerMdl

        Dim r As PlayerMdl = New PlayerMdl()

        Dim PlayerName As String() = Nothing
        Dim player_number As Integer = 0

        Dim pnDAO As Player_NamesDAO = New Player_NamesDAO()

        'Create a player name and only leave the while loop when it is unique
        While True
            PlayerName = pnDAO.CreatePlayerName

            'If first and last name are the same then pick a new name
            If UCase(PlayerName(0)) = UCase(PlayerName(1)) Then
                Continue While
            End If

            Exit While
        End While

        While True
            player_number = getPlayerNumber(pos)

            If Not isPlayerNumber_Unique_Team_Memoory(player_number, Players) Then
                Continue While
            End If

            If Not IsNothing(league_DB_Connecdtion) And league_DB_Connecdtion.Length > 0 Then
                If Not Player_DAO.isPlayerNumber_Unigue_DB(player_number, league_DB_Connecdtion, team_ind) Then
                    Continue While
                End If
            End If
            Exit While
        End While

        Dim age As Integer = generate_Player_Age(IsNothing(league_DB_Connecdtion))

        Dim ratings As Player_Abilities = Create_Player_Abilities(pos)

        r.First_Name = PlayerName(0)
        r.Last_Name = PlayerName(1)

        r.Age = age
        r.Pos = pos
        r.Jersey_Number = player_number
        r.Ratings = ratings
        r.Active = True

        Return r

    End Function
    Public Function getPlayerNumber(ByVal pos As PlayerMdl.Position) As Integer

        Dim r As Integer
        Dim iLower As Integer = 0
        Dim iUpper As Integer = 0

        Select Case pos
            Case PlayerMdl.Position.QB
                r = CommonUtils.getRandomNum(App_Constants.QBLOWNUM, App_Constants.QBHIGHNUM)
            Case PlayerMdl.Position.RB
                r = CommonUtils.getRandomNum(App_Constants.RBLOWNUM, App_Constants.RBHIGHNUM)
            Case PlayerMdl.Position.WR
                r = CommonUtils.getRandomNum(App_Constants.WRLOWNUM, App_Constants.WRHIGHNUM)
            Case PlayerMdl.Position.TE
                While True
                    r = CommonUtils.getRandomNum(App_Constants.TELOWNUM, App_Constants.TEHIGHNUM)
                    If r >= 50 And r <= 79 Then
                        Exit While
                    End If
                End While
            Case PlayerMdl.Position.OL
                r = CommonUtils.getRandomNum(App_Constants.OLLOWNUM, App_Constants.OLHIGHNUM)
            Case PlayerMdl.Position.DL
                While True
                    r = CommonUtils.getRandomNum(App_Constants.DLLOWNUM, App_Constants.DLHIGHNUM)
                    If r < 80 Or r > 89 Then
                        Exit While
                    End If
                End While
            Case PlayerMdl.Position.LB
                r = CommonUtils.getRandomNum(App_Constants.LBLOWNUM, App_Constants.LBHIGHNUM)
            Case PlayerMdl.Position.DB
                r = CommonUtils.getRandomNum(App_Constants.DBLOWNUM, App_Constants.DBHIGHNUM)
            Case PlayerMdl.Position.K, PlayerMdl.Position.P
                r = CommonUtils.getRandomNum(App_Constants.KLOWNUM, App_Constants.KHIGHNUM)
        End Select

        Return r

    End Function
    Public Function isPlayerNumber_Unique_Team_Memoory(ByVal number As String, ByVal Players As List(Of PlayerMdl)) As Boolean
        Dim r As Boolean = True

        If Not IsNothing(Players) Then
            For Each p In Players
                If p.Jersey_Number.ToString = number.Trim Then
                    r = False
                    Exit For
                End If
            Next
        End If

        Return r

    End Function
    Public Function generate_Player_Age(ByVal newleage As Boolean) As Integer
        Dim r As Integer = App_Constants.STARTING_ROOKIE_AGE

        If Not newleage Then
            r = CommonUtils.getRandomNum(App_Constants.NEWLEAGE_LOW_AGE, App_Constants.NEWLEAGE_HIGH_AGE)
        End If

        Return r
    End Function

    Public Function Create_Player_Abilities(ByVal pos As PlayerMdl.Position)
        Dim r As Player_Abilities = New Player_Abilities

        Select Case pos
            Case PlayerMdl.Position.QB
                r.Accuracy_Rating = CommonUtils.getRandomNum(App_Constants.PRIMARY_ABILITY_LOW_RATING, App_Constants.PRIMARY_ABILITY_HIGH_RATING)
                r.Decision_Making = CommonUtils.getRandomNum(App_Constants.PRIMARY_ABILITY_LOW_RATING, App_Constants.PRIMARY_ABILITY_HIGH_RATING)
                r.Arm_Strength_Rating = CommonUtils.getRandomNum(App_Constants.PRIMARY_ABILITY_LOW_RATING, App_Constants.PRIMARY_ABILITY_HIGH_RATING)
                r.Fumble_Rating = CommonUtils.getRandomNum(App_Constants.PRIMARY_ABILITY_LOW_RATING, App_Constants.PRIMARY_ABILITY_HIGH_RATING)

                r.Agilty_Rating = CommonUtils.getRandomNum(App_Constants.SECONDARY_3_ABILITY_LOW_RATING, App_Constants.SECONDARY_3_ABILITY_HIGH_RATING)
                r.Speed_Rating = CommonUtils.getRandomNum(App_Constants.SECONDARY_3_ABILITY_LOW_RATING, App_Constants.SECONDARY_3_ABILITY_HIGH_RATING)

                r.Hands_Rating = CommonUtils.getRandomNum(App_Constants.TERTIARY_ABILITY_LOW_RATING, App_Constants.TERTIARY_ABILITY_HIGH_RATING)
                r.Kicking_Accuracy = CommonUtils.getRandomNum(App_Constants.TERTIARY_ABILITY_LOW_RATING, App_Constants.TERTIARY_ABILITY_HIGH_RATING)
                r.Leg_Strength = CommonUtils.getRandomNum(App_Constants.TERTIARY_ABILITY_LOW_RATING, App_Constants.TERTIARY_ABILITY_HIGH_RATING)
                r.Pass_Attack = CommonUtils.getRandomNum(App_Constants.TERTIARY_ABILITY_LOW_RATING, App_Constants.TERTIARY_ABILITY_HIGH_RATING)
                r.Pass_Block_Rating = CommonUtils.getRandomNum(App_Constants.TERTIARY_ABILITY_LOW_RATING, App_Constants.TERTIARY_ABILITY_HIGH_RATING)
                r.Running_Power_Rating = CommonUtils.getRandomNum(App_Constants.TERTIARY_ABILITY_LOW_RATING, App_Constants.TERTIARY_ABILITY_HIGH_RATING)
                r.Run_Attack = CommonUtils.getRandomNum(App_Constants.TERTIARY_ABILITY_LOW_RATING, App_Constants.TERTIARY_ABILITY_HIGH_RATING)
                r.Run_Block_Rating = CommonUtils.getRandomNum(App_Constants.TERTIARY_ABILITY_LOW_RATING, App_Constants.TERTIARY_ABILITY_HIGH_RATING)
                r.Tackle_Rating = CommonUtils.getRandomNum(App_Constants.TERTIARY_ABILITY_LOW_RATING, App_Constants.TERTIARY_ABILITY_HIGH_RATING)

                r.OverAll = (r.Fumble_Rating * App_Constants.QB_FUMBLE_PERCENT) +
                           (r.Arm_Strength_Rating * App_Constants.QB_ARMSTRENGTH_PERCENT) +
                           (r.Accuracy_Rating * App_Constants.QB_ACCURACY_RATING) +
                           (r.Decision_Making * App_Constants.QB_DESISION_RATING)

            Case PlayerMdl.Position.RB
                r.Running_Power_Rating = CommonUtils.getRandomNum(App_Constants.PRIMARY_ABILITY_LOW_RATING, App_Constants.PRIMARY_ABILITY_HIGH_RATING)
                r.Speed_Rating = CommonUtils.getRandomNum(App_Constants.PRIMARY_ABILITY_LOW_RATING, App_Constants.PRIMARY_ABILITY_HIGH_RATING)
                r.Agilty_Rating = CommonUtils.getRandomNum(App_Constants.PRIMARY_ABILITY_LOW_RATING, App_Constants.PRIMARY_ABILITY_HIGH_RATING)
                r.Fumble_Rating = CommonUtils.getRandomNum(App_Constants.PRIMARY_ABILITY_LOW_RATING, App_Constants.PRIMARY_ABILITY_HIGH_RATING)

                r.Hands_Rating = CommonUtils.getRandomNum(App_Constants.SECONDARY_1_ABILITY_LOW_RATING, App_Constants.SECONDARY_1_ABILITY_HIGH_RATING)
                r.Tackle_Rating = CommonUtils.getRandomNum(App_Constants.SECONDARY_3_ABILITY_LOW_RATING, App_Constants.SECONDARY_3_ABILITY_HIGH_RATING)

                r.Accuracy_Rating = CommonUtils.getRandomNum(App_Constants.TERTIARY_ABILITY_LOW_RATING, App_Constants.TERTIARY_ABILITY_HIGH_RATING)
                r.Decision_Making = CommonUtils.getRandomNum(App_Constants.TERTIARY_ABILITY_LOW_RATING, App_Constants.TERTIARY_ABILITY_HIGH_RATING)
                r.Arm_Strength_Rating = CommonUtils.getRandomNum(App_Constants.TERTIARY_ABILITY_LOW_RATING, App_Constants.TERTIARY_ABILITY_HIGH_RATING)
                r.Kicking_Accuracy = CommonUtils.getRandomNum(App_Constants.TERTIARY_ABILITY_LOW_RATING, App_Constants.TERTIARY_ABILITY_HIGH_RATING)
                r.Leg_Strength = CommonUtils.getRandomNum(App_Constants.TERTIARY_ABILITY_LOW_RATING, App_Constants.TERTIARY_ABILITY_HIGH_RATING)
                r.Pass_Attack = CommonUtils.getRandomNum(App_Constants.TERTIARY_ABILITY_LOW_RATING, App_Constants.TERTIARY_ABILITY_HIGH_RATING)
                r.Pass_Block_Rating = CommonUtils.getRandomNum(App_Constants.TERTIARY_ABILITY_LOW_RATING, App_Constants.TERTIARY_ABILITY_HIGH_RATING)
                r.Run_Attack = CommonUtils.getRandomNum(App_Constants.TERTIARY_ABILITY_LOW_RATING, App_Constants.TERTIARY_ABILITY_HIGH_RATING)
                r.Run_Block_Rating = CommonUtils.getRandomNum(App_Constants.TERTIARY_ABILITY_LOW_RATING, App_Constants.TERTIARY_ABILITY_HIGH_RATING)

                r.OverAll = (r.Fumble_Rating * App_Constants.RB_FUMBLE_PERCENT) +
                            (r.Running_Power_Rating * App_Constants.RB_RUNNING_PWER_PERCENT) +
                            (r.Speed_Rating * App_Constants.RB_SPEED_PERCENT) +
                            ((r.Hands_Rating + 100 - App_Constants.SECONDARY_1_ABILITY_HIGH_RATING) * App_Constants.RB_HANDS_PERCENT) +
                            (r.Agilty_Rating * App_Constants.RB_AGILITY_PERCENT)

            Case PlayerMdl.Position.WR
                r.Speed_Rating = CommonUtils.getRandomNum(App_Constants.PRIMARY_ABILITY_LOW_RATING, App_Constants.PRIMARY_ABILITY_HIGH_RATING)
                r.Agilty_Rating = CommonUtils.getRandomNum(App_Constants.PRIMARY_ABILITY_LOW_RATING, App_Constants.PRIMARY_ABILITY_HIGH_RATING)
                r.Hands_Rating = CommonUtils.getRandomNum(App_Constants.PRIMARY_ABILITY_LOW_RATING, App_Constants.PRIMARY_ABILITY_HIGH_RATING)
                r.Fumble_Rating = CommonUtils.getRandomNum(App_Constants.PRIMARY_ABILITY_LOW_RATING, App_Constants.PRIMARY_ABILITY_HIGH_RATING)

                r.Tackle_Rating = CommonUtils.getRandomNum(App_Constants.SECONDARY_2_ABILITY_LOW_RATING, App_Constants.SECONDARY_2_ABILITY_HIGH_RATING)

                r.Running_Power_Rating = CommonUtils.getRandomNum(App_Constants.TERTIARY_ABILITY_LOW_RATING, App_Constants.TERTIARY_ABILITY_HIGH_RATING)
                r.Accuracy_Rating = CommonUtils.getRandomNum(App_Constants.TERTIARY_ABILITY_LOW_RATING, App_Constants.TERTIARY_ABILITY_HIGH_RATING)
                r.Decision_Making = CommonUtils.getRandomNum(App_Constants.TERTIARY_ABILITY_LOW_RATING, App_Constants.TERTIARY_ABILITY_HIGH_RATING)
                r.Arm_Strength_Rating = CommonUtils.getRandomNum(App_Constants.TERTIARY_ABILITY_LOW_RATING, App_Constants.TERTIARY_ABILITY_HIGH_RATING)
                r.Kicking_Accuracy = CommonUtils.getRandomNum(App_Constants.TERTIARY_ABILITY_LOW_RATING, App_Constants.TERTIARY_ABILITY_HIGH_RATING)
                r.Leg_Strength = CommonUtils.getRandomNum(App_Constants.TERTIARY_ABILITY_LOW_RATING, App_Constants.TERTIARY_ABILITY_HIGH_RATING)
                r.Pass_Attack = CommonUtils.getRandomNum(App_Constants.TERTIARY_ABILITY_LOW_RATING, App_Constants.TERTIARY_ABILITY_HIGH_RATING)
                r.Pass_Block_Rating = CommonUtils.getRandomNum(App_Constants.TERTIARY_ABILITY_LOW_RATING, App_Constants.TERTIARY_ABILITY_HIGH_RATING)
                r.Run_Attack = CommonUtils.getRandomNum(App_Constants.TERTIARY_ABILITY_LOW_RATING, App_Constants.TERTIARY_ABILITY_HIGH_RATING)
                r.Run_Block_Rating = CommonUtils.getRandomNum(App_Constants.TERTIARY_ABILITY_LOW_RATING, App_Constants.TERTIARY_ABILITY_HIGH_RATING)

                r.OverAll = (r.Fumble_Rating * App_Constants.WR_FUMBLE_PERCENT) +
                            (r.Speed_Rating * App_Constants.WR_SPEED_PERCENT) +
                            (r.Hands_Rating * App_Constants.WR_HANDS_PERCENT) +
                            (r.Agilty_Rating * App_Constants.WR_AGILITY_PERCENT)

            Case PlayerMdl.Position.TE
                r.Speed_Rating = CommonUtils.getRandomNum(App_Constants.SECONDARY_1_ABILITY_LOW_RATING, App_Constants.SECONDARY_1_ABILITY_HIGH_RATING)
                r.Agilty_Rating = CommonUtils.getRandomNum(App_Constants.SECONDARY_1_ABILITY_LOW_RATING, App_Constants.SECONDARY_1_ABILITY_HIGH_RATING)
                r.Hands_Rating = CommonUtils.getRandomNum(App_Constants.PRIMARY_ABILITY_LOW_RATING, App_Constants.PRIMARY_ABILITY_HIGH_RATING)
                r.Fumble_Rating = CommonUtils.getRandomNum(App_Constants.PRIMARY_ABILITY_LOW_RATING, App_Constants.PRIMARY_ABILITY_HIGH_RATING)
                r.Pass_Block_Rating = CommonUtils.getRandomNum(App_Constants.SECONDARY_1_ABILITY_LOW_RATING, App_Constants.SECONDARY_1_ABILITY_HIGH_RATING)
                r.Run_Block_Rating = CommonUtils.getRandomNum(App_Constants.SECONDARY_1_ABILITY_LOW_RATING, App_Constants.SECONDARY_1_ABILITY_HIGH_RATING)

                r.Tackle_Rating = CommonUtils.getRandomNum(App_Constants.SECONDARY_1_ABILITY_LOW_RATING, App_Constants.SECONDARY_1_ABILITY_HIGH_RATING)

                r.Running_Power_Rating = CommonUtils.getRandomNum(App_Constants.TERTIARY_ABILITY_LOW_RATING, App_Constants.TERTIARY_ABILITY_HIGH_RATING)
                r.Accuracy_Rating = CommonUtils.getRandomNum(App_Constants.TERTIARY_ABILITY_LOW_RATING, App_Constants.TERTIARY_ABILITY_HIGH_RATING)
                r.Decision_Making = CommonUtils.getRandomNum(App_Constants.TERTIARY_ABILITY_LOW_RATING, App_Constants.TERTIARY_ABILITY_HIGH_RATING)
                r.Arm_Strength_Rating = CommonUtils.getRandomNum(App_Constants.TERTIARY_ABILITY_LOW_RATING, App_Constants.TERTIARY_ABILITY_HIGH_RATING)
                r.Kicking_Accuracy = CommonUtils.getRandomNum(App_Constants.TERTIARY_ABILITY_LOW_RATING, App_Constants.TERTIARY_ABILITY_HIGH_RATING)
                r.Leg_Strength = CommonUtils.getRandomNum(App_Constants.TERTIARY_ABILITY_LOW_RATING, App_Constants.TERTIARY_ABILITY_HIGH_RATING)
                r.Pass_Attack = CommonUtils.getRandomNum(App_Constants.TERTIARY_ABILITY_LOW_RATING, App_Constants.TERTIARY_ABILITY_HIGH_RATING)
                r.Run_Attack = CommonUtils.getRandomNum(App_Constants.TERTIARY_ABILITY_LOW_RATING, App_Constants.TERTIARY_ABILITY_HIGH_RATING)

                r.OverAll = (r.Fumble_Rating * App_Constants.TE_FUMBLE_PERCENT) +
                            ((r.Speed_Rating + 100 - App_Constants.SECONDARY_1_ABILITY_HIGH_RATING) * App_Constants.TE_SPEED_PERCENT) +
                            (r.Hands_Rating * App_Constants.TE_HANDS_PERCENT) +
                            ((r.Agilty_Rating + 100 - App_Constants.SECONDARY_1_ABILITY_HIGH_RATING) * App_Constants.TE_AGILITY_PERCENT) +
                            ((r.Pass_Block_Rating + 100 - App_Constants.SECONDARY_1_ABILITY_HIGH_RATING) * App_Constants.TE_PASS_BLOCK_PERCENT) +
                            ((r.Run_Block_Rating + 100 - App_Constants.SECONDARY_1_ABILITY_HIGH_RATING) * App_Constants.TE_RUN_BLOCK_PERCENT)

            Case PlayerMdl.Position.OL
                r.Pass_Block_Rating = CommonUtils.getRandomNum(App_Constants.PRIMARY_ABILITY_LOW_RATING, App_Constants.PRIMARY_ABILITY_HIGH_RATING)
                r.Run_Block_Rating = CommonUtils.getRandomNum(Math.Max(App_Constants.PRIMARY_ABILITY_LOW_RATING, r.Pass_Block_Rating - App_Constants.OL_RUN_PASS_BLOCK_DELTA),
                                     Math.Min(App_Constants.PRIMARY_ABILITY_HIGH_RATING, r.Pass_Block_Rating + App_Constants.OL_RUN_PASS_BLOCK_DELTA))

                r.Tackle_Rating = CommonUtils.getRandomNum(App_Constants.SECONDARY_1_ABILITY_LOW_RATING, App_Constants.SECONDARY_1_ABILITY_HIGH_RATING)
                r.Speed_Rating = CommonUtils.getRandomNum(App_Constants.SECONDARY_3_ABILITY_LOW_RATING, App_Constants.SECONDARY_3_ABILITY_HIGH_RATING)
                r.Agilty_Rating = CommonUtils.getRandomNum(App_Constants.SECONDARY_3_ABILITY_LOW_RATING, App_Constants.SECONDARY_3_ABILITY_HIGH_RATING)

                r.Accuracy_Rating = CommonUtils.getRandomNum(App_Constants.TERTIARY_ABILITY_LOW_RATING, App_Constants.TERTIARY_ABILITY_HIGH_RATING)
                r.Decision_Making = CommonUtils.getRandomNum(App_Constants.TERTIARY_ABILITY_LOW_RATING, App_Constants.TERTIARY_ABILITY_HIGH_RATING)
                r.Arm_Strength_Rating = CommonUtils.getRandomNum(App_Constants.TERTIARY_ABILITY_LOW_RATING, App_Constants.TERTIARY_ABILITY_HIGH_RATING)
                r.Fumble_Rating = CommonUtils.getRandomNum(App_Constants.TERTIARY_ABILITY_LOW_RATING, App_Constants.TERTIARY_ABILITY_HIGH_RATING)
                r.Hands_Rating = CommonUtils.getRandomNum(App_Constants.TERTIARY_ABILITY_LOW_RATING, App_Constants.TERTIARY_ABILITY_HIGH_RATING)
                r.Kicking_Accuracy = CommonUtils.getRandomNum(App_Constants.TERTIARY_ABILITY_LOW_RATING, App_Constants.TERTIARY_ABILITY_HIGH_RATING)
                r.Leg_Strength = CommonUtils.getRandomNum(App_Constants.TERTIARY_ABILITY_LOW_RATING, App_Constants.TERTIARY_ABILITY_HIGH_RATING)
                r.Pass_Attack = CommonUtils.getRandomNum(App_Constants.TERTIARY_ABILITY_LOW_RATING, App_Constants.TERTIARY_ABILITY_HIGH_RATING)
                r.Running_Power_Rating = CommonUtils.getRandomNum(App_Constants.TERTIARY_ABILITY_LOW_RATING, App_Constants.TERTIARY_ABILITY_HIGH_RATING)
                r.Run_Attack = CommonUtils.getRandomNum(App_Constants.TERTIARY_ABILITY_LOW_RATING, App_Constants.TERTIARY_ABILITY_HIGH_RATING)

                r.OverAll = (r.Pass_Block_Rating * App_Constants.OL_PASS_BLOCK_PERCENT) +
                            (r.Run_Block_Rating * App_Constants.OL_RUN_BLOCK_PERCENT) +
                            ((r.Agilty_Rating + 100 - App_Constants.SECONDARY_3_ABILITY_HIGH_RATING) * App_Constants.OL_AGILITY_PERCENT)

            Case PlayerMdl.Position.DL
                r.Pass_Attack = CommonUtils.getRandomNum(App_Constants.PRIMARY_ABILITY_LOW_RATING, App_Constants.PRIMARY_ABILITY_HIGH_RATING)
                r.Run_Attack = CommonUtils.getRandomNum(Math.Max(App_Constants.PRIMARY_ABILITY_LOW_RATING, r.Pass_Attack - App_Constants.OL_RUN_PASS_BLOCK_DELTA),
                                     Math.Min(App_Constants.PRIMARY_ABILITY_HIGH_RATING, r.Pass_Attack + App_Constants.OL_RUN_PASS_BLOCK_DELTA))
                r.Tackle_Rating = CommonUtils.getRandomNum(App_Constants.PRIMARY_ABILITY_LOW_RATING, App_Constants.PRIMARY_ABILITY_HIGH_RATING)

                r.Speed_Rating = CommonUtils.getRandomNum(App_Constants.SECONDARY_3_ABILITY_LOW_RATING, App_Constants.SECONDARY_3_ABILITY_HIGH_RATING)
                r.Agilty_Rating = CommonUtils.getRandomNum(App_Constants.SECONDARY_3_ABILITY_LOW_RATING, App_Constants.SECONDARY_3_ABILITY_HIGH_RATING)

                r.Pass_Block_Rating = CommonUtils.getRandomNum(App_Constants.TERTIARY_ABILITY_LOW_RATING, App_Constants.TERTIARY_ABILITY_HIGH_RATING)
                r.Run_Block_Rating = CommonUtils.getRandomNum(Math.Max(App_Constants.TERTIARY_ABILITY_LOW_RATING, r.Pass_Block_Rating - App_Constants.OL_RUN_PASS_BLOCK_DELTA),
                                     Math.Min(App_Constants.TERTIARY_ABILITY_HIGH_RATING, r.Pass_Block_Rating + App_Constants.OL_RUN_PASS_BLOCK_DELTA))
                r.Accuracy_Rating = CommonUtils.getRandomNum(App_Constants.TERTIARY_ABILITY_LOW_RATING, App_Constants.TERTIARY_ABILITY_HIGH_RATING)
                r.Decision_Making = CommonUtils.getRandomNum(App_Constants.TERTIARY_ABILITY_LOW_RATING, App_Constants.TERTIARY_ABILITY_HIGH_RATING)
                r.Arm_Strength_Rating = CommonUtils.getRandomNum(App_Constants.TERTIARY_ABILITY_LOW_RATING, App_Constants.TERTIARY_ABILITY_HIGH_RATING)
                r.Fumble_Rating = CommonUtils.getRandomNum(App_Constants.TERTIARY_ABILITY_LOW_RATING, App_Constants.TERTIARY_ABILITY_HIGH_RATING)
                r.Hands_Rating = CommonUtils.getRandomNum(App_Constants.TERTIARY_ABILITY_LOW_RATING, App_Constants.TERTIARY_ABILITY_HIGH_RATING)
                r.Kicking_Accuracy = CommonUtils.getRandomNum(App_Constants.TERTIARY_ABILITY_LOW_RATING, App_Constants.TERTIARY_ABILITY_HIGH_RATING)
                r.Leg_Strength = CommonUtils.getRandomNum(App_Constants.TERTIARY_ABILITY_LOW_RATING, App_Constants.TERTIARY_ABILITY_HIGH_RATING)
                r.Running_Power_Rating = CommonUtils.getRandomNum(App_Constants.TERTIARY_ABILITY_LOW_RATING, App_Constants.TERTIARY_ABILITY_HIGH_RATING)

                r.OverAll = (r.Pass_Attack * App_Constants.DL_PASS_ATTACK_PERCENT) +
                            (r.Run_Attack * App_Constants.DL_RUN_ATTACK_PERCENT) +
                            (r.Tackle_Rating * App_Constants.DL_TACKLE_PERCENT) +
                            ((r.Agilty_Rating + 100 - App_Constants.SECONDARY_3_ABILITY_HIGH_RATING) * App_Constants.DL_AGILITY_PERCENT) +
                            ((r.Speed_Rating + 100 - App_Constants.SECONDARY_3_ABILITY_HIGH_RATING) * App_Constants.DL_SPEED_PERCENT)

            Case PlayerMdl.Position.DB
                r.Speed_Rating = CommonUtils.getRandomNum(App_Constants.PRIMARY_ABILITY_LOW_RATING, App_Constants.PRIMARY_ABILITY_HIGH_RATING)
                r.Hands_Rating = CommonUtils.getRandomNum(App_Constants.PRIMARY_ABILITY_LOW_RATING, App_Constants.PRIMARY_ABILITY_HIGH_RATING)
                r.Tackle_Rating = CommonUtils.getRandomNum(App_Constants.PRIMARY_ABILITY_LOW_RATING, App_Constants.PRIMARY_ABILITY_HIGH_RATING)
                r.Agilty_Rating = CommonUtils.getRandomNum(App_Constants.PRIMARY_ABILITY_LOW_RATING, App_Constants.PRIMARY_ABILITY_HIGH_RATING)

                r.Pass_Block_Rating = CommonUtils.getRandomNum(App_Constants.TERTIARY_ABILITY_LOW_RATING, App_Constants.TERTIARY_ABILITY_HIGH_RATING)
                r.Run_Block_Rating = CommonUtils.getRandomNum(Math.Max(App_Constants.TERTIARY_ABILITY_LOW_RATING, r.Pass_Block_Rating - App_Constants.OL_RUN_PASS_BLOCK_DELTA),
                                     Math.Min(App_Constants.TERTIARY_ABILITY_HIGH_RATING, r.Pass_Block_Rating + App_Constants.OL_RUN_PASS_BLOCK_DELTA))
                r.Accuracy_Rating = CommonUtils.getRandomNum(App_Constants.TERTIARY_ABILITY_LOW_RATING, App_Constants.TERTIARY_ABILITY_HIGH_RATING)
                r.Decision_Making = CommonUtils.getRandomNum(App_Constants.TERTIARY_ABILITY_LOW_RATING, App_Constants.TERTIARY_ABILITY_HIGH_RATING)
                r.Arm_Strength_Rating = CommonUtils.getRandomNum(App_Constants.TERTIARY_ABILITY_LOW_RATING, App_Constants.TERTIARY_ABILITY_HIGH_RATING)
                r.Fumble_Rating = CommonUtils.getRandomNum(App_Constants.TERTIARY_ABILITY_LOW_RATING, App_Constants.TERTIARY_ABILITY_HIGH_RATING)
                r.Kicking_Accuracy = CommonUtils.getRandomNum(App_Constants.TERTIARY_ABILITY_LOW_RATING, App_Constants.TERTIARY_ABILITY_HIGH_RATING)
                r.Leg_Strength = CommonUtils.getRandomNum(App_Constants.TERTIARY_ABILITY_LOW_RATING, App_Constants.TERTIARY_ABILITY_HIGH_RATING)
                r.Pass_Attack = CommonUtils.getRandomNum(App_Constants.TERTIARY_ABILITY_LOW_RATING, App_Constants.TERTIARY_ABILITY_HIGH_RATING)
                r.Running_Power_Rating = CommonUtils.getRandomNum(App_Constants.TERTIARY_ABILITY_LOW_RATING, App_Constants.TERTIARY_ABILITY_HIGH_RATING)
                r.Run_Attack = CommonUtils.getRandomNum(App_Constants.TERTIARY_ABILITY_LOW_RATING, App_Constants.TERTIARY_ABILITY_HIGH_RATING)

                r.OverAll = (r.Speed_Rating * App_Constants.DB_SPEED_PERCENT) +
                            (r.Hands_Rating * App_Constants.DB_HANDS_PERCENT) +
                            (r.Tackle_Rating * App_Constants.DB_TACKLING_PERCENT) +
                            (r.Agilty_Rating * App_Constants.DB_AGILITY_PERCENT)

            Case PlayerMdl.Position.LB
                r.Tackle_Rating = CommonUtils.getRandomNum(App_Constants.PRIMARY_ABILITY_LOW_RATING, App_Constants.PRIMARY_ABILITY_HIGH_RATING)
                r.Pass_Attack = CommonUtils.getRandomNum(App_Constants.PRIMARY_ABILITY_LOW_RATING, App_Constants.PRIMARY_ABILITY_HIGH_RATING)
                r.Run_Attack = CommonUtils.getRandomNum(Math.Max(App_Constants.PRIMARY_ABILITY_LOW_RATING, r.Pass_Attack - App_Constants.OL_RUN_PASS_BLOCK_DELTA),
                                     Math.Min(App_Constants.PRIMARY_ABILITY_HIGH_RATING, r.Pass_Attack + App_Constants.OL_RUN_PASS_BLOCK_DELTA))

                r.Speed_Rating = CommonUtils.getRandomNum(App_Constants.SECONDARY_1_ABILITY_LOW_RATING, App_Constants.SECONDARY_1_ABILITY_HIGH_RATING)
                r.Agilty_Rating = CommonUtils.getRandomNum(App_Constants.SECONDARY_1_ABILITY_LOW_RATING, App_Constants.SECONDARY_1_ABILITY_HIGH_RATING)
                r.Hands_Rating = CommonUtils.getRandomNum(App_Constants.SECONDARY_2_ABILITY_LOW_RATING, App_Constants.SECONDARY_2_ABILITY_HIGH_RATING)

                r.Pass_Block_Rating = CommonUtils.getRandomNum(App_Constants.TERTIARY_ABILITY_LOW_RATING, App_Constants.TERTIARY_ABILITY_HIGH_RATING)
                r.Run_Block_Rating = CommonUtils.getRandomNum(Math.Max(App_Constants.TERTIARY_ABILITY_LOW_RATING, r.Pass_Block_Rating - App_Constants.OL_RUN_PASS_BLOCK_DELTA),
                                     Math.Min(App_Constants.TERTIARY_ABILITY_HIGH_RATING, r.Pass_Block_Rating + App_Constants.OL_RUN_PASS_BLOCK_DELTA))
                r.Accuracy_Rating = CommonUtils.getRandomNum(App_Constants.TERTIARY_ABILITY_LOW_RATING, App_Constants.TERTIARY_ABILITY_HIGH_RATING)
                r.Decision_Making = CommonUtils.getRandomNum(App_Constants.TERTIARY_ABILITY_LOW_RATING, App_Constants.TERTIARY_ABILITY_HIGH_RATING)
                r.Arm_Strength_Rating = CommonUtils.getRandomNum(App_Constants.TERTIARY_ABILITY_LOW_RATING, App_Constants.TERTIARY_ABILITY_HIGH_RATING)
                r.Fumble_Rating = CommonUtils.getRandomNum(App_Constants.TERTIARY_ABILITY_LOW_RATING, App_Constants.TERTIARY_ABILITY_HIGH_RATING)
                r.Kicking_Accuracy = CommonUtils.getRandomNum(App_Constants.TERTIARY_ABILITY_LOW_RATING, App_Constants.TERTIARY_ABILITY_HIGH_RATING)
                r.Leg_Strength = CommonUtils.getRandomNum(App_Constants.TERTIARY_ABILITY_LOW_RATING, App_Constants.TERTIARY_ABILITY_HIGH_RATING)
                r.Running_Power_Rating = CommonUtils.getRandomNum(App_Constants.TERTIARY_ABILITY_LOW_RATING, App_Constants.TERTIARY_ABILITY_HIGH_RATING)

                r.OverAll = (r.Pass_Attack * App_Constants.LB_PASS_ATTACK_PERCENT) +
                            (r.Run_Attack * App_Constants.LB_RUN_ATTACK_PERCENT) +
                            (r.Tackle_Rating * App_Constants.LB_TACKLE_PERCENT) +
                            ((r.Hands_Rating + 100 - App_Constants.SECONDARY_2_ABILITY_HIGH_RATING) * App_Constants.LB_HANDS_PERCENT) +
                            ((r.Speed_Rating + 100 - App_Constants.SECONDARY_1_ABILITY_HIGH_RATING) * App_Constants.LB_SPEED_PERCENT) +
                            ((r.Agilty_Rating + 100 - App_Constants.SECONDARY_1_ABILITY_HIGH_RATING) * App_Constants.LB_AGILITY_PERCENT)

            Case PlayerMdl.Position.K, PlayerMdl.Position.P
                r.Kicking_Accuracy = CommonUtils.getRandomNum(App_Constants.PRIMARY_ABILITY_LOW_RATING, App_Constants.PRIMARY_ABILITY_HIGH_RATING)
                r.Leg_Strength = CommonUtils.getRandomNum(App_Constants.PRIMARY_ABILITY_LOW_RATING, App_Constants.PRIMARY_ABILITY_HIGH_RATING)

                r.Pass_Block_Rating = CommonUtils.getRandomNum(App_Constants.TERTIARY_ABILITY_LOW_RATING, App_Constants.TERTIARY_ABILITY_HIGH_RATING)
                r.Run_Block_Rating = CommonUtils.getRandomNum(Math.Max(App_Constants.TERTIARY_ABILITY_LOW_RATING, r.Pass_Block_Rating - App_Constants.OL_RUN_PASS_BLOCK_DELTA),
                                     Math.Min(App_Constants.TERTIARY_ABILITY_HIGH_RATING, r.Pass_Block_Rating + App_Constants.OL_RUN_PASS_BLOCK_DELTA))
                r.Accuracy_Rating = CommonUtils.getRandomNum(App_Constants.TERTIARY_ABILITY_LOW_RATING, App_Constants.TERTIARY_ABILITY_HIGH_RATING)
                r.Decision_Making = CommonUtils.getRandomNum(App_Constants.TERTIARY_ABILITY_LOW_RATING, App_Constants.TERTIARY_ABILITY_HIGH_RATING)
                r.Arm_Strength_Rating = CommonUtils.getRandomNum(App_Constants.TERTIARY_ABILITY_LOW_RATING, App_Constants.TERTIARY_ABILITY_HIGH_RATING)
                r.Fumble_Rating = CommonUtils.getRandomNum(App_Constants.TERTIARY_ABILITY_LOW_RATING, App_Constants.TERTIARY_ABILITY_HIGH_RATING)
                r.Agilty_Rating = CommonUtils.getRandomNum(App_Constants.TERTIARY_ABILITY_LOW_RATING, App_Constants.TERTIARY_ABILITY_HIGH_RATING)
                r.Hands_Rating = CommonUtils.getRandomNum(App_Constants.TERTIARY_ABILITY_LOW_RATING, App_Constants.TERTIARY_ABILITY_HIGH_RATING)
                r.Pass_Attack = CommonUtils.getRandomNum(App_Constants.TERTIARY_ABILITY_LOW_RATING, App_Constants.TERTIARY_ABILITY_HIGH_RATING)
                r.Running_Power_Rating = CommonUtils.getRandomNum(App_Constants.TERTIARY_ABILITY_LOW_RATING, App_Constants.TERTIARY_ABILITY_HIGH_RATING)
                r.Run_Attack = CommonUtils.getRandomNum(App_Constants.TERTIARY_ABILITY_LOW_RATING, App_Constants.TERTIARY_ABILITY_HIGH_RATING)
                r.Speed_Rating = CommonUtils.getRandomNum(App_Constants.TERTIARY_ABILITY_LOW_RATING, App_Constants.TERTIARY_ABILITY_HIGH_RATING)
                r.Run_Attack = CommonUtils.getRandomNum(App_Constants.TERTIARY_ABILITY_LOW_RATING, App_Constants.TERTIARY_ABILITY_HIGH_RATING)
                r.Tackle_Rating = CommonUtils.getRandomNum(App_Constants.TERTIARY_ABILITY_LOW_RATING, App_Constants.TERTIARY_ABILITY_HIGH_RATING)

                r.OverAll = (r.Kicking_Accuracy * App_Constants.K_KICK_ACC) +
                            (r.Leg_Strength * App_Constants.K_LEG_STRENGTH)

        End Select

        Return r
    End Function

End Class
