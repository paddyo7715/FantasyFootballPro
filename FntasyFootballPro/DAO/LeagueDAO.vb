Imports System.Data
Imports System.Data.SQLite

Public Class LeagueDAO

    Property League_con As SQLiteConnection = Nothing
    Property nl As New_League

    Public Sub New(ByVal nl As New_League)
        Me.nl = nl
    End Sub

    Public Function getLeagueConnection(ByVal sConnectString As String) As SQLiteConnection
        Dim c As SQLiteConnection = New SQLiteConnection()
        c.ConnectionString = sConnectString
        Return c
    End Function
    Public Function createTransaction(ByVal c As SQLiteConnection) As SQLiteTransaction
        Return c.BeginTransaction
    End Function
    Public Sub closeConnection(ByVal c As SQLiteConnection)
        If c.State = ConnectionState.Open Then
            c.Close()
        End If
    End Sub
    Public Sub Create_New_League(ByVal con_string As String)
        Dim tr As SQLiteTransaction = Nothing
        Dim cmdLeague As SQLiteCommand = Nothing
        Dim cmdConf As SQLiteCommand = Nothing
        Dim cmdDiv As SQLiteCommand = Nothing
        Dim cmdTeam As SQLiteCommand = Nothing
        Dim cmdPlayers As SQLiteCommand = Nothing
        Dim cmdGames As SQLiteCommand = Nothing
        Dim strStage = Nothing
        Dim sSQL As String = Nothing

        Try
            strStage = "Getting connection"
            League_con = getLeagueConnection(con_string)
            League_con.Open()

            strStage = "Beginning transaction"
            tr = League_con.BeginTransaction

            strStage = "Inserting League Record"
            sSQL = "INSERT INTO LEAGUE (Short_Name, Long_Name, Starting_Year, Number_of_weeks,Number_of_Games, Champtionship_Game_Name, Num_Divisions, Num_Teams_Per_Division) VALUES(@Short_Name, @Long_Name, @Starting_Year, @Number_of_weeks,@Number_of_Games, @Champtionship_Game_Name, @Num_Divisions, @Num_Teams_Per_Division)"

            cmdLeague = New SQLiteCommand(League_con)
            cmdLeague.CommandText = sSQL
            cmdLeague.Parameters.Add("@Short_Name", Data.DbType.String).Value = nl.Short_Name
            cmdLeague.Parameters.Add("@Long_Name", Data.DbType.String).Value = nl.Long_Name
            cmdLeague.Parameters.Add("@Starting_Year", Data.DbType.Int16).Value = nl.Starting_Year
            cmdLeague.Parameters.Add("@Number_of_weeks", Data.DbType.Int16).Value = nl.Number_of_weeks
            cmdLeague.Parameters.Add("@Number_of_Games", Data.DbType.Int16).Value = nl.Number_of_Games
            cmdLeague.Parameters.Add("@Champtionship_Game_Name", Data.DbType.String).Value = nl.Championship_Game_Name
            cmdLeague.Parameters.Add("@Num_Divisions", Data.DbType.Int16).Value = nl.Num_Divisions
            cmdLeague.Parameters.Add("@Num_Teams_Per_Division", Data.DbType.Int16).Value = nl.Num_Teams_Per_Division
            cmdLeague.ExecuteNonQuery()

            strStage = "Inserting Conferences"
            cmdConf = New SQLiteCommand(League_con)
            Dim c_id As Integer = 0
            For Each c In nl.Conferences
                c_id += 1
                sSQL = "INSERT INTO CONFERENCE (ID,Name) VALUES(@ID, @Conf_Name)"
                cmdConf.CommandText = sSQL
                cmdConf.Parameters.Add("@ID", Data.DbType.Int16).Value = c_id + 1
                cmdConf.Parameters.Add("@Conf_Name", Data.DbType.String).Value = c
                cmdConf.ExecuteNonQuery()

                strStage = "Inserting Divisions"
                cmdDiv = New SQLiteCommand(League_con)
                Dim delta1 As Integer = ((nl.Divisions.Count / nl.Conferences.Count) * (c_id - 1))
                Dim delta2 As Integer = delta1 + (nl.Divisions.Count / nl.Conferences.Count)

                For i As Integer = delta1 To delta2
                    Dim d As String = nl.Divisions(i)
                    sSQL = "INSERT INTO DIVISION (ID, Name) VALUES(@ID, @Name)"
                    cmdDiv.CommandText = sSQL
                    cmdDiv.Parameters.Add("@ID", Data.DbType.Int16).Value = i + 1
                    cmdDiv.Parameters.Add("@Name", Data.DbType.String).Value = d
                    cmdDiv.ExecuteNonQuery()
                Next
            Next

            strStage = "Inserting Team"
            Dim t_count As Integer = 0
            For Each t In nl.Teams
                Dim t_id As Integer
                Dim d_num As Integer = CommonUtils.getDivisionNum_from_Team_Number(nl.Num_Teams_Per_Division, t_count)
                sSQL = "INSERT INTO TEAMS (ID, Division_ID, City_Abr, City, Nickname, Helmet_img_path,
                        Helmet_Color, Home_jersey_Color, Home_Pants_Color, Away_jersey_Color, Away_Pants_Color) 
                        VALUES(@ID, @Division_ID, @City_Abr, @City, @Nickname, @Helmet_img_path,
                        @Helmet_Color, @Home_jersey_Color, @Home_Pants_Color, @Away_jersey_Color, @Away_Pants_Color)"
                cmdTeam.CommandText = sSQL
                cmdTeam.Parameters.Add("@ID", Data.DbType.Int16).Value = t_id
                cmdTeam.Parameters.Add("@Division_ID", Data.DbType.Int16).Value = CommonUtils.getDivisionNum_from_Team_Number(nl.Teams.Count - nl.Num_Teams_Per_Division, t_id)
                cmdTeam.Parameters.Add("@City_Abr", Data.DbType.String).Value = t.City_Abr
                cmdTeam.Parameters.Add("@City", Data.DbType.String).Value = t.City
                cmdTeam.Parameters.Add("@Nickname", Data.DbType.String).Value = t.Nickname
                cmdTeam.Parameters.Add("@Helmet_img_path", Data.DbType.String).Value = t.Helmet_img_path
                cmdTeam.Parameters.Add("@Helmet_Color", Data.DbType.String).Value = t.Helmet_Color
                cmdTeam.Parameters.Add("@Home_jersey_Color", Data.DbType.String).Value = t.Home_jersey_Color
                cmdTeam.Parameters.Add("@Home_Pants_Color", Data.DbType.String).Value = t.Home_Pants_Color
                cmdTeam.Parameters.Add("@Away_jersey_Color", Data.DbType.String).Value = t.Away_jersey_Color
                cmdTeam.Parameters.Add("@Away_Pants_Color", Data.DbType.String).Value = t.Away_Pants_Color
                cmdTeam.ExecuteNonQuery()

                strStage = "Inserting Players"
                For Each P In t.Players
                    sSQL = "INSERT INTO PLAYERS (Team_ID,Age,Jersey_Number,First_Name,Last_Name,Active,Position,Fumble_Rating,Accuracy_Rating,Decision_Making,Arm_Strength_Rating,Pass_Block_Rating,Run_Block_Rating,Running_Power_Rating,Speed_Rating,Agility_Rating,Hands_Rating,Pass_Attack,Run_Attack,Tackle_Rating,Kicker_Leg_Power,Kicker_Leg_Accuracy) 
                        VALUES(@Team_ID,@Age,@Jersey_Number,@First_Name,@Last_Name,@Active,@Position,@Fumble_Rating,
@Accuracy_Rating,@Decision_Making,@Arm_Strength_Rating,@Pass_Block_Rating,@Run_Block_Rating,@Running_Power_Rating,@Speed_Rating,
@Agility_Rating,@Hands_Rating,@Pass_Attack,@Run_Attack,@Tackle_Rating)"
                    cmdPlayers.CommandText = sSQL
                    cmdPlayers.Parameters.Add("@Team_ID", Data.DbType.Int16).Value = t_id
                    cmdPlayers.Parameters.Add("@Age", Data.DbType.Int16).Value = P.Age
                    cmdPlayers.Parameters.Add("@Jersey_Number", Data.DbType.Int16).Value = P.Jersey_Number
                    cmdPlayers.Parameters.Add("@First_Name", Data.DbType.String).Value = P.First_Name
                    cmdPlayers.Parameters.Add("@Last_Name", Data.DbType.String).Value = P.Last_Name
                    cmdPlayers.Parameters.Add("@Active", Data.DbType.Int16).Value = 1
                    cmdPlayers.Parameters.Add("@Position", Data.DbType.Int16).Value = CInt(P.Pos)
                    cmdPlayers.Parameters.Add("@Fumble_Rating", Data.DbType.Int16).Value = P.Ratings.Fumble_Rating
                    cmdPlayers.Parameters.Add("@Accuracy_Rating", Data.DbType.Int16).Value = P.Ratings.Accuracy_Rating
                    cmdPlayers.Parameters.Add("@Decision_Making", Data.DbType.Int16).Value = P.Ratings.Decision_Making
                    cmdPlayers.Parameters.Add("@Arm_Strength", Data.DbType.Int16).Value = P.Ratings.Arm_Strength_Rating
                    cmdPlayers.Parameters.Add("@Pass_Block_Rating", Data.DbType.Int16).Value = P.Ratings.Pass_Block_Rating
                    cmdPlayers.Parameters.Add("@Run_Block_Rating", Data.DbType.Int16).Value = P.Ratings.Run_Block_Rating
                    cmdPlayers.Parameters.Add("@Running_Power_Rating", Data.DbType.Int16).Value = P.Ratings.Running_Power_Rating
                    cmdPlayers.Parameters.Add("@Speed_Rating", Data.DbType.Int16).Value = P.Ratings.Speed_Rating
                    cmdPlayers.Parameters.Add("@Agility_Rating", Data.DbType.Int16).Value = P.Ratings.Agilty_Rating
                    cmdPlayers.Parameters.Add("@Hands_Rating", Data.DbType.Int16).Value = P.Ratings.Hands_Rating
                    cmdPlayers.Parameters.Add("@Pass_Attack", Data.DbType.Int16).Value = P.Ratings.Pass_Attack
                    cmdPlayers.Parameters.Add("@Run_Attack", Data.DbType.Int16).Value = P.Ratings.Run_Attack
                    cmdPlayers.Parameters.Add("@Tackle_Rating", Data.DbType.Int16).Value = P.Ratings.Tackle_Rating
                    cmdPlayers.Parameters.Add("@Kicker_Leg_Power", Data.DbType.Int16).Value = P.Ratings.Leg_Strength
                    cmdPlayers.Parameters.Add("@Kicker_Leg_Accuracy", Data.DbType.Int16).Value = P.Ratings.Kicking_Accuracy
                    cmdPlayers.ExecuteNonQuery()
                Next
                t_count += 1

            Next

            strStage = "Creating schedule"
            '            Dim ls As New Schedule("APFL", 40, 5, 2, 18, 2)
            Dim ls As New Schedule(nl.Short_Name, nl.Teams.Count, nl.Num_Teams_Per_Division, nl.Conferences.Count, nl.Number_of_Games, nl.Number_of_weeks - nl.Number_of_Games)

            Dim s As List(Of String) = Nothing
            s = ls.Generate_Regular_Schedule()
            Dim val_sched As New Validate_Sched(nl.Short_Name, nl.Teams.Count, nl.Num_Teams_Per_Division, nl.Conferences.Count, nl.Number_of_Games, nl.Number_of_weeks - nl.Number_of_Games)

            strStage = "Inserting schedule into database"
            Dim w As String = Nothing
            Dim h As String = Nothing
            Dim a As String = Nothing

            For Each g In s
                Dim m() As String = g.Split(",")

                w = m(0)
                h = m(1)
                a = m(2)

                sSQL = "INSERT INTO Game (Year,Week,Home_Team_ID,Away_Team_ID 
                        VALUES(@Year,@Week,@Home_Team_ID,@Away_Team_ID)"
                cmdGames.CommandText = sSQL
                cmdGames.Parameters.Add("@Year", Data.DbType.Int16).Value = nl.Starting_Year
                cmdGames.Parameters.Add("@Week", Data.DbType.Int16).Value = w
                cmdGames.Parameters.Add("@Home_Team_ID", Data.DbType.Int16).Value = h
                cmdGames.Parameters.Add("@Away_Team_ID", Data.DbType.String).Value = a
                cmdGames.ExecuteNonQuery()
            Next

            tr.Commit()
        Catch ex As Exception
            tr.Rollback()
            Throw New Exception("Error at stage " & strStage & " writing records to database:" & ex.Message)
        Finally
            closeConnection(League_con)
        End Try

    End Sub

End Class
