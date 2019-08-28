Imports System.Data
Imports System.Data.SQLite
Imports System.Configuration
Imports System.IO


Public Class Stock_TeamsDAO
    Property SettingsConnection As SQLiteConnection = New SQLiteConnection()

    Public Sub New()
        Dim constr As String = ""
        constr = CommonUtils.getSettingsDBConnectionString
        SettingsConnection.ConnectionString = constr

    End Sub

    Public Function getAllStockTeams() As List(Of TeamMdl)
        Dim r As List(Of TeamMdl) = New List(Of TeamMdl)

        Dim cmd As SQLiteCommand = Nothing

        Dim ID As Integer
        Dim City_Abr As String
        Dim City As String
        Dim nickname As String
        Dim Stadium_Name As String
        Dim Stadium_Location As String
        Dim Stadium_Capacity As String
        Dim Stadium_Img_Path As String
        Dim Helmet_img_path As String
        Dim Helmet_Color As String
        Dim Helmet_Logo_Color As String
        Dim Helmet_Facemask_Color As String
        Dim Socks_Color As String
        Dim Cleats_Color As String
        Dim Home_jersey_Color As String
        Dim Home_Sleeve_Color As String
        Dim Home_Jersey_Number_Color As String
        Dim Home_Jersey_Number_Outline_Color As String
        Dim Home_Jersey_Shoulder_Stripe As String
        Dim Home_Jersey_Sleeve_Stripe_Color_1 As String
        Dim Home_Jersey_Sleeve_Stripe_Color_2 As String
        Dim Home_Jersey_Sleeve_Stripe_Color_3 As String
        Dim Home_Jersey_Sleeve_Stripe_Color_4 As String
        Dim Home_Jersey_Sleeve_Stripe_Color_5 As String
        Dim Home_Jersey_Sleeve_Stripe_Color_6 As String
        Dim Home_Pants_Color As String
        Dim Home_Pants_Stripe_Color_1 As String
        Dim Home_Pants_Stripe_Color_2 As String
        Dim Home_Pants_Stripe_Color_3 As String
        Dim Away_jersey_Color As String
        Dim Away_Sleeve_Color As String
        Dim Away_Jersey_Number_Color As String
        Dim Away_Jersey_Number_Outline_Color As String
        Dim Away_Jersey_Shoulder_Stripe As String
        Dim Away_Jersey_Sleeve_Stripe_Color_1 As String
        Dim Away_Jersey_Sleeve_Stripe_Color_2 As String
        Dim Away_Jersey_Sleeve_Stripe_Color_3 As String
        Dim Away_Jersey_Sleeve_Stripe_Color_4 As String
        Dim Away_Jersey_Sleeve_Stripe_Color_5 As String
        Dim Away_Jersey_Sleeve_Stripe_Color_6 As String
        Dim Away_Pants_Color As String
        Dim Away_Pants_Stripe_Color_1 As String
        Dim Away_Pants_Stripe_Color_2 As String
        Dim Away_Pants_Stripe_Color_3 As String
        Dim Stadium_Field_Type As String
        Dim Stadium_Field_Color As String

        Try
            SettingsConnection.Open()
            Dim sSQL = "SELECT 	ID, City_Abr, City,nickname,Stadium_Name, Stadium_Location,Stadium_Capacity,Stadium_Img_Path,
	        Helmet_img_path, Helmet_Color, Helmet_Logo_Color, Helmet_Facemask_Color, Socks_Color,
	        Cleats_Color, Home_jersey_Color, Home_Sleeve_Color, Home_Jersey_Number_Color, Home_Jersey_Number_Outline_Color,
	        Home_Jersey_Shoulder_Stripe,
	        Home_Jersey_Sleeve_Stripe_Color_1,
	        Home_Jersey_Sleeve_Stripe_Color_2,
	        Home_Jersey_Sleeve_Stripe_Color_3,
	        Home_Jersey_Sleeve_Stripe_Color_4,
	        Home_Jersey_Sleeve_Stripe_Color_5,
	        Home_Jersey_Sleeve_Stripe_Color_6,
	        Home_Pants_Color,
	        Home_Pants_Stripe_Color_1,
	        Home_Pants_Stripe_Color_2,
	        Home_Pants_Stripe_Color_3,
	        Away_jersey_Color,Away_Sleeve_Color,Away_Jersey_Number_Color, Away_Jersey_Number_Outline_Color,Away_Jersey_Shoulder_Stripe,
	        Away_Jersey_Sleeve_Stripe_Color_1,
	        Away_Jersey_Sleeve_Stripe_Color_2,
	        Away_Jersey_Sleeve_Stripe_Color_3,
	        Away_Jersey_Sleeve_Stripe_Color_4,
	        Away_Jersey_Sleeve_Stripe_Color_5,
	        Away_Jersey_Sleeve_Stripe_Color_6,
	        Away_Pants_Color,
	        Away_Pants_Stripe_Color_1,
	        Away_Pants_Stripe_Color_2,
	        Away_Pants_Stripe_Color_3,
	        Stadium_Field_Type, Stadium_Field_Color	
            FROM Stock_Teams order by City;"

            cmd = SettingsConnection.CreateCommand
            cmd.CommandText = sSQL

            Dim rdr As SQLiteDataReader = cmd.ExecuteReader()

            Using rdr
                While (rdr.Read())
                    ID = rdr.GetInt16(rdr.GetOrdinal("ID"))
                    City_Abr = rdr.GetString(rdr.GetOrdinal("City_Abr"))
                    City = rdr.GetString(rdr.GetOrdinal("City"))
                    nickname = rdr.GetString(rdr.GetOrdinal("nickname"))
                    Stadium_Name = rdr.GetString(rdr.GetOrdinal("Stadium_Name"))
                    Stadium_Location = rdr.GetString(rdr.GetOrdinal("Stadium_Location"))
                    Stadium_Capacity = rdr.GetString(rdr.GetOrdinal("Stadium_Capacity"))
                    Stadium_Img_Path = rdr.GetString(rdr.GetOrdinal("Stadium_Img_Path"))
                    Helmet_img_path = rdr.GetString(rdr.GetOrdinal("Helmet_img_path"))
                    Helmet_Color = rdr.GetString(rdr.GetOrdinal("Helmet_Color"))
                    Helmet_Logo_Color = rdr.GetString(rdr.GetOrdinal("Helmet_Logo_Color"))
                    Helmet_Facemask_Color = rdr.GetString(rdr.GetOrdinal("Helmet_Facemask_Color"))
                    Socks_Color = rdr.GetString(rdr.GetOrdinal("Socks_Color"))
                    Cleats_Color = rdr.GetString(rdr.GetOrdinal("Cleats_Color"))
                    Home_jersey_Color = rdr.GetString(rdr.GetOrdinal("Home_jersey_Color"))
                    Home_Sleeve_Color = rdr.GetString(rdr.GetOrdinal("Home_Sleeve_Color"))
                    Home_Jersey_Number_Color = rdr.GetString(rdr.GetOrdinal("Home_Jersey_Number_Color"))
                    Home_Jersey_Number_Outline_Color = rdr.GetString(rdr.GetOrdinal("Home_Jersey_Number_Outline_Color"))
                    Home_Jersey_Shoulder_Stripe = rdr.GetString(rdr.GetOrdinal("Home_Jersey_Shoulder_Stripe"))
                    Home_Jersey_Sleeve_Stripe_Color_1 = rdr.GetString(rdr.GetOrdinal("Home_Jersey_Sleeve_Stripe_Color_1"))
                    Home_Jersey_Sleeve_Stripe_Color_2 = rdr.GetString(rdr.GetOrdinal("Home_Jersey_Sleeve_Stripe_Color_2"))
                    Home_Jersey_Sleeve_Stripe_Color_3 = rdr.GetString(rdr.GetOrdinal("Home_Jersey_Sleeve_Stripe_Color_3"))
                    Home_Jersey_Sleeve_Stripe_Color_4 = rdr.GetString(rdr.GetOrdinal("Home_Jersey_Sleeve_Stripe_Color_4"))
                    Home_Jersey_Sleeve_Stripe_Color_5 = rdr.GetString(rdr.GetOrdinal("Home_Jersey_Sleeve_Stripe_Color_5"))
                    Home_Jersey_Sleeve_Stripe_Color_6 = rdr.GetString(rdr.GetOrdinal("Home_Jersey_Sleeve_Stripe_Color_6"))
                    Home_Pants_Color = rdr.GetString(rdr.GetOrdinal("Home_Pants_Color"))
                    Home_Pants_Stripe_Color_1 = rdr.GetString(rdr.GetOrdinal("Home_Pants_Stripe_Color_1"))
                    Home_Pants_Stripe_Color_2 = rdr.GetString(rdr.GetOrdinal("Home_Pants_Stripe_Color_2"))
                    Home_Pants_Stripe_Color_3 = rdr.GetString(rdr.GetOrdinal("Home_Pants_Stripe_Color_3"))
                    Away_jersey_Color = rdr.GetString(rdr.GetOrdinal("Away_jersey_Color"))
                    Away_Sleeve_Color = rdr.GetString(rdr.GetOrdinal("Away_Sleeve_Color"))
                    Away_Jersey_Number_Color = rdr.GetString(rdr.GetOrdinal("Away_Jersey_Number_Color"))
                    Away_Jersey_Number_Outline_Color = rdr.GetString(rdr.GetOrdinal("Away_Jersey_Number_Outline_Color"))
                    Away_Jersey_Shoulder_Stripe = rdr.GetString(rdr.GetOrdinal("Away_Jersey_Shoulder_Stripe"))
                    Away_Jersey_Sleeve_Stripe_Color_1 = rdr.GetString(rdr.GetOrdinal("Away_Jersey_Sleeve_Stripe_Color_1"))
                    Away_Jersey_Sleeve_Stripe_Color_2 = rdr.GetString(rdr.GetOrdinal("Away_Jersey_Sleeve_Stripe_Color_2"))
                    Away_Jersey_Sleeve_Stripe_Color_3 = rdr.GetString(rdr.GetOrdinal("Away_Jersey_Sleeve_Stripe_Color_3"))
                    Away_Jersey_Sleeve_Stripe_Color_4 = rdr.GetString(rdr.GetOrdinal("Away_Jersey_Sleeve_Stripe_Color_4"))
                    Away_Jersey_Sleeve_Stripe_Color_5 = rdr.GetString(rdr.GetOrdinal("Away_Jersey_Sleeve_Stripe_Color_5"))
                    Away_Jersey_Sleeve_Stripe_Color_6 = rdr.GetString(rdr.GetOrdinal("Away_Jersey_Sleeve_Stripe_Color_6"))
                    Away_Pants_Color = rdr.GetString(rdr.GetOrdinal("Away_Pants_Color"))
                    Away_Pants_Stripe_Color_1 = rdr.GetString(rdr.GetOrdinal("Away_Pants_Stripe_Color_1"))
                    Away_Pants_Stripe_Color_2 = rdr.GetString(rdr.GetOrdinal("Away_Pants_Stripe_Color_2"))
                    Away_Pants_Stripe_Color_3 = rdr.GetString(rdr.GetOrdinal("Away_Pants_Stripe_Color_3"))
                    Stadium_Field_Type = rdr.GetInt16(rdr.GetOrdinal("Stadium_Field_Type"))
                    Stadium_Field_Color = rdr.GetString(rdr.GetOrdinal("Stadium_Field_Color"))

                    Dim stadium = New StadiumMdl(Stadium_Name, Stadium_Location,
                         Stadium_Field_Type, Stadium_Field_Color,
                        Stadium_Capacity, Stadium_Img_Path)

                    Dim Footwear = New FootwearMdl(Socks_Color, Cleats_Color)

                    Dim Helmet = New HelmetMdl(Helmet_Color, Helmet_Logo_Color, Helmet_Facemask_Color)

                    Dim Home_Jersey = New JerseyMdl(Home_jersey_Color, Home_Sleeve_Color, Home_Jersey_Shoulder_Stripe,
                    Home_Jersey_Number_Color, Home_Jersey_Number_Outline_Color,
                    Home_Jersey_Sleeve_Stripe_Color_1, Home_Jersey_Sleeve_Stripe_Color_2,
                    Home_Jersey_Sleeve_Stripe_Color_3, Home_Jersey_Sleeve_Stripe_Color_4,
                    Home_Jersey_Sleeve_Stripe_Color_5, Home_Jersey_Sleeve_Stripe_Color_6)

                    Dim Home_Pants = New PantsMdl(Home_Pants_Color, Home_Pants_Stripe_Color_1,
                    Home_Pants_Stripe_Color_2, Home_Pants_Stripe_Color_3)

                    Dim Away_Jersey = New JerseyMdl(Away_jersey_Color, Away_Sleeve_Color, Away_Jersey_Shoulder_Stripe,
                    Away_Jersey_Number_Color, Away_Jersey_Number_Outline_Color,
                    Away_Jersey_Sleeve_Stripe_Color_1, Away_Jersey_Sleeve_Stripe_Color_2,
                    Away_Jersey_Sleeve_Stripe_Color_3, Away_Jersey_Sleeve_Stripe_Color_4,
                    Away_Jersey_Sleeve_Stripe_Color_5, Away_Jersey_Sleeve_Stripe_Color_6)

                    Dim Away_Pants = New PantsMdl(Away_Pants_Color, Away_Pants_Stripe_Color_1,
                    Away_Pants_Stripe_Color_2, Away_Pants_Stripe_Color_3)

                    Dim Uniform = New UniformMdl(Helmet, Home_Jersey, Away_Jersey, Home_Pants, Away_Pants, Footwear)

                    Dim Team As TeamMdl = New TeamMdl(ID, City)
                    Team.setFields("", City_Abr, City, nickname, stadium, Uniform, Helmet_img_path)
                    r.Add(Team)
                End While
            End Using

        Finally
            If Not IsNothing(cmd) Then cmd.Dispose()
            If SettingsConnection.State = ConnectionState.Open Then
                SettingsConnection.Close()
            End If
        End Try

        Return r

    End Function
    Public Sub AddStockTeam(ByVal t As TeamMdl)
        Dim sSQL As String = Nothing

        Dim cmdTeam As SQLiteCommand = Nothing

        Try
            SettingsConnection.Open()
            sSQL = "INSERT INTO Stock_Teams (City_Abr, City, Nickname, Helmet_img_path,
                        Helmet_Color, Helmet_Logo_Color, Helmet_Facemask_Color, Socks_Color, Cleats_Color,
                        Home_jersey_Color,Home_Sleeve_Color, Home_Jersey_Shoulder_Stripe, Home_Jersey_Number_Color, Home_Jersey_Number_Outline_Color,
                        Home_Jersey_Sleeve_Stripe_Color_1, Home_Jersey_Sleeve_Stripe_Color_2,
                        Home_Jersey_Sleeve_Stripe_Color_3, Home_Jersey_Sleeve_Stripe_Color_4,
                        Home_Jersey_Sleeve_Stripe_Color_5, Home_Jersey_Sleeve_Stripe_Color_6,                       
                        Home_Pants_Color, Home_Pants_Stripe_Color_1,  Home_Pants_Stripe_Color_2, Home_Pants_Stripe_Color_3,
                        Away_jersey_Color, Away_Sleeve_Color,Away_Jersey_Shoulder_Stripe, Away_Jersey_Number_Color, Away_Jersey_Number_Outline_Color,
                        Away_Jersey_Sleeve_Stripe_Color_1, Away_Jersey_Sleeve_Stripe_Color_2,
                        Away_Jersey_Sleeve_Stripe_Color_3, Away_Jersey_Sleeve_Stripe_Color_4,
                        Away_Jersey_Sleeve_Stripe_Color_5, Away_Jersey_Sleeve_Stripe_Color_6,   
                        Away_Pants_Color, Away_Pants_Stripe_Color_1,  Away_Pants_Stripe_Color_2, Away_Pants_Stripe_Color_3,
                        Stadium_Name,Stadium_Location,Stadium_Field_Type,Stadium_Field_Color,Stadium_Capacity,Stadium_Img_Path) 
                        VALUES(@City_Abr, @City, @Nickname, @Helmet_img_path,
                        @Helmet_Color, @Helmet_Logo_Color,@Helmet_Facemask_Color, @Socks_Color, @Cleats_Color,  
                        @Home_jersey_Color,@Home_Sleeve_Color, @Home_Jersey_Shoulder_Stripe, @Home_Jersey_Number_Color, @Home_Jersey_Number_Outline_Color,
                        @Home_Jersey_Sleeve_Stripe_Color_1, @Home_Jersey_Sleeve_Stripe_Color_2,
                        @Home_Jersey_Sleeve_Stripe_Color_3, @Home_Jersey_Sleeve_Stripe_Color_4,
                        @Home_Jersey_Sleeve_Stripe_Color_5, @Home_Jersey_Sleeve_Stripe_Color_6,
                        @Home_Pants_Color, @Home_Pants_Stripe_Color_1, @Home_Pants_Stripe_Color_2, @Home_Pants_Stripe_Color_3,
                        @Away_jersey_Color, @Away_Sleeve_Color, @Away_Jersey_Shoulder_Stripe, @Away_Jersey_Number_Color, @Away_Jersey_Number_Outline_Color,
                        @Away_Jersey_Sleeve_Stripe_Color_1, @Away_Jersey_Sleeve_Stripe_Color_2,
                        @Away_Jersey_Sleeve_Stripe_Color_3, @Away_Jersey_Sleeve_Stripe_Color_4,
                        @Away_Jersey_Sleeve_Stripe_Color_5, @Away_Jersey_Sleeve_Stripe_Color_6,                      
                        @Away_Pants_Color, @Away_Pants_Stripe_Color_1, @Away_Pants_Stripe_Color_2, @Away_Pants_Stripe_Color_3,
                        @Stadium_Name,@Stadium_Location,@Stadium_Field_Type,@Stadium_Field_Color,@Stadium_Capacity,@Stadium_Img_path)"
            cmdTeam = SettingsConnection.CreateCommand
            cmdTeam.CommandText = sSQL
            cmdTeam.Parameters.Add("@City_Abr", Data.DbType.String).Value = t.City_Abr
            cmdTeam.Parameters.Add("@City", Data.DbType.String).Value = t.City
            cmdTeam.Parameters.Add("@Nickname", Data.DbType.String).Value = t.Nickname
            cmdTeam.Parameters.Add("@Helmet_img_path", Data.DbType.String).Value = Path.GetFileName(t.Helmet_img_path)
            cmdTeam.Parameters.Add("@Helmet_Color", Data.DbType.String).Value = t.Uniform.Helmet.Helmet_Color
            cmdTeam.Parameters.Add("@Helmet_Logo_Color", Data.DbType.String).Value = t.Uniform.Helmet.Helmet_Logo_Color
            cmdTeam.Parameters.Add("@Helmet_Facemask_Color", Data.DbType.String).Value = t.Uniform.Helmet.Helmet_Facemask_Color
            cmdTeam.Parameters.Add("@Socks_Color", Data.DbType.String).Value = t.Uniform.Footwear.Socks_Color
            cmdTeam.Parameters.Add("@Cleats_Color", Data.DbType.String).Value = t.Uniform.Footwear.Cleats_Color
            cmdTeam.Parameters.Add("@Home_jersey_Color", Data.DbType.String).Value = t.Uniform.Home_Jersey.Jersey_Color
            cmdTeam.Parameters.Add("@Home_Sleeve_Color", Data.DbType.String).Value = t.Uniform.Home_Jersey.Sleeve_Color
            cmdTeam.Parameters.Add("@Home_jersey_Shoulder_Stripe", Data.DbType.String).Value = t.Uniform.Home_Jersey.Shoulder_Stripe_Color
            cmdTeam.Parameters.Add("@Home_Jersey_Number_Color", Data.DbType.String).Value = t.Uniform.Home_Jersey.Number_Color
            cmdTeam.Parameters.Add("@Home_Jersey_Number_Outline_Color", Data.DbType.String).Value = t.Uniform.Home_Jersey.Number_Outline_Color
            cmdTeam.Parameters.Add("@Home_Jersey_Sleeve_Stripe_Color_1", Data.DbType.String).Value = t.Uniform.Home_Jersey.Sleeve_Stripe1
            cmdTeam.Parameters.Add("@Home_Jersey_Sleeve_Stripe_Color_2", Data.DbType.String).Value = t.Uniform.Home_Jersey.Sleeve_Stripe2
            cmdTeam.Parameters.Add("@Home_Jersey_Sleeve_Stripe_Color_3", Data.DbType.String).Value = t.Uniform.Home_Jersey.Sleeve_Stripe3
            cmdTeam.Parameters.Add("@Home_Jersey_Sleeve_Stripe_Color_4", Data.DbType.String).Value = t.Uniform.Home_Jersey.Sleeve_Stripe4
            cmdTeam.Parameters.Add("@Home_Jersey_Sleeve_Stripe_Color_5", Data.DbType.String).Value = t.Uniform.Home_Jersey.Sleeve_Stripe5
            cmdTeam.Parameters.Add("@Home_Jersey_Sleeve_Stripe_Color_6", Data.DbType.String).Value = t.Uniform.Home_Jersey.Sleeve_Stripe6
            cmdTeam.Parameters.Add("@Home_Pants_Color", Data.DbType.String).Value = t.Uniform.Home_Pants.Pants_Color
            cmdTeam.Parameters.Add("@Home_Pants_Stripe_Color_1", Data.DbType.String).Value = t.Uniform.Home_Pants.Stripe_Color_1
            cmdTeam.Parameters.Add("@Home_Pants_Stripe_Color_2", Data.DbType.String).Value = t.Uniform.Home_Pants.Stripe_Color_2
            cmdTeam.Parameters.Add("@Home_Pants_Stripe_Color_3", Data.DbType.String).Value = t.Uniform.Home_Pants.Stripe_Color_3
            cmdTeam.Parameters.Add("@Away_jersey_Color", Data.DbType.String).Value = t.Uniform.Away_Jersey.Jersey_Color
            cmdTeam.Parameters.Add("@Away_Sleeve_Color", Data.DbType.String).Value = t.Uniform.Away_Jersey.Sleeve_Color
            cmdTeam.Parameters.Add("@Away_jersey_Shoulder_Stripe", Data.DbType.String).Value = t.Uniform.Away_Jersey.Shoulder_Stripe_Color
            cmdTeam.Parameters.Add("@Away_Jersey_Number_Color", Data.DbType.String).Value = t.Uniform.Away_Jersey.Number_Color
            cmdTeam.Parameters.Add("@Away_Jersey_Number_Outline_Color", Data.DbType.String).Value = t.Uniform.Away_Jersey.Number_Outline_Color
            cmdTeam.Parameters.Add("@Away_Jersey_Sleeve_Stripe_Color_1", Data.DbType.String).Value = t.Uniform.Away_Jersey.Sleeve_Stripe1
            cmdTeam.Parameters.Add("@Away_Jersey_Sleeve_Stripe_Color_2", Data.DbType.String).Value = t.Uniform.Away_Jersey.Sleeve_Stripe2
            cmdTeam.Parameters.Add("@Away_Jersey_Sleeve_Stripe_Color_3", Data.DbType.String).Value = t.Uniform.Away_Jersey.Sleeve_Stripe3
            cmdTeam.Parameters.Add("@Away_Jersey_Sleeve_Stripe_Color_4", Data.DbType.String).Value = t.Uniform.Away_Jersey.Sleeve_Stripe4
            cmdTeam.Parameters.Add("@Away_Jersey_Sleeve_Stripe_Color_5", Data.DbType.String).Value = t.Uniform.Away_Jersey.Sleeve_Stripe5
            cmdTeam.Parameters.Add("@Away_Jersey_Sleeve_Stripe_Color_6", Data.DbType.String).Value = t.Uniform.Away_Jersey.Sleeve_Stripe6
            cmdTeam.Parameters.Add("@Away_Pants_Color", Data.DbType.String).Value = t.Uniform.Away_Pants.Pants_Color
            cmdTeam.Parameters.Add("@Away_Pants_Stripe_Color_1", Data.DbType.String).Value = t.Uniform.Away_Pants.Stripe_Color_1
            cmdTeam.Parameters.Add("@Away_Pants_Stripe_Color_2", Data.DbType.String).Value = t.Uniform.Away_Pants.Stripe_Color_2
            cmdTeam.Parameters.Add("@Away_Pants_Stripe_Color_3", Data.DbType.String).Value = t.Uniform.Away_Pants.Stripe_Color_3
            cmdTeam.Parameters.Add("@Stadium_Name", Data.DbType.String).Value = t.Stadium.Stadium_Name
            cmdTeam.Parameters.Add("@Stadium_Location", Data.DbType.String).Value = t.Stadium.Stadium_Location
            cmdTeam.Parameters.Add("@Stadium_Field_Type", Data.DbType.Int16).Value = t.Stadium.Field_Type
            cmdTeam.Parameters.Add("@Stadium_Field_Color", Data.DbType.String).Value = t.Stadium.Field_Color
            cmdTeam.Parameters.Add("@Stadium_Capacity", Data.DbType.String).Value = t.Stadium.Capacity
            cmdTeam.Parameters.Add("@Stadium_Img_path", Data.DbType.String).Value = Path.GetFileName(t.Stadium.Stadium_Img_Path)
            Dim i As Integer = cmdTeam.ExecuteNonQuery()

            If i <> 1 Then
                Throw New Exception("Error inserting stock team " & i & " rows inserted")
            End If



        Finally
            If Not IsNothing(cmdTeam) Then cmdTeam.Dispose()
            If SettingsConnection.State = ConnectionState.Open Then
                SettingsConnection.Close()
            End If
        End Try

    End Sub
    Public Sub DeleteStockTeam(ByVal t_id As Integer)

        Dim sSQL As String = Nothing

        Dim cmdTeam As SQLiteCommand = Nothing

        Try
            SettingsConnection.Open()
            sSQL = "DELETE FROM STOCK_TEAMS WHERE ID = @ID"
            cmdTeam = SettingsConnection.CreateCommand
            cmdTeam.CommandText = sSQL
            cmdTeam.Parameters.Add("@ID", Data.DbType.Int32).Value = t_id
            Dim i As Integer = cmdTeam.ExecuteNonQuery()

            If i <> 1 Then
                Throw New Exception("Error deleting stock team ")
            End If

        Finally
            If Not IsNothing(cmdTeam) Then cmdTeam.Dispose()
            If SettingsConnection.State = ConnectionState.Open Then
                SettingsConnection.Close()
            End If
        End Try


    End Sub
    Public Sub UpdateStockTeam(ByVal t As TeamMdl)
        Dim sSQL As String = Nothing

        Dim cmdTeam As SQLiteCommand = Nothing


        Try
            SettingsConnection.Open()
            sSQL = "UPDATE Stock_Teams 
                    SET City_Abr =@City_Abr, 
                        City =@City,
                        Nickname =@Nickname,
                        Helmet_img_path =@Helmet_img_path,
                        Helmet_Color =@Helmet_Color,
                        Helmet_Logo_Color =@Helmet_Logo_Color,
                        Helmet_Facemask_Color =@Helmet_Facemask_Color,
                        Socks_Color =@Socks_Color,
                        Cleats_Color =@Cleats_Color,
                        Home_jersey_Color =@Home_jersey_Color,
                        Home_Sleeve_Color =@Home_Sleeve_Color,
                        Home_Jersey_Shoulder_Stripe =@Home_Jersey_Shoulder_Stripe,
                        Home_Jersey_Number_Color =@Home_Jersey_Number_Color,
                        Home_Jersey_Number_Outline_Color =@Home_Jersey_Number_Outline_Color,
                        Home_Jersey_Sleeve_Stripe_Color_1 =@Home_Jersey_Sleeve_Stripe_Color_1,
                        Home_Jersey_Sleeve_Stripe_Color_2 =@Home_Jersey_Sleeve_Stripe_Color_2,
                        Home_Jersey_Sleeve_Stripe_Color_3 =@Home_Jersey_Sleeve_Stripe_Color_3,
                        Home_Jersey_Sleeve_Stripe_Color_4 =@Home_Jersey_Sleeve_Stripe_Color_4,
                        Home_Jersey_Sleeve_Stripe_Color_5 =@Home_Jersey_Sleeve_Stripe_Color_5,
                        Home_Jersey_Sleeve_Stripe_Color_6 =@Home_Jersey_Sleeve_Stripe_Color_6,                       
                        Home_Pants_Color =@Home_Pants_Color,
                        Home_Pants_Stripe_Color_1 =@Home_Pants_Stripe_Color_1,
                        Home_Pants_Stripe_Color_2 =@Home_Pants_Stripe_Color_2,
                        Home_Pants_Stripe_Color_3 =@Home_Pants_Stripe_Color_3,
                        Away_jersey_Color = @Away_jersey_Color,
                        Away_Sleeve_Color = @Away_Sleeve_Color,
                        Away_Jersey_Shoulder_Stripe =@Away_Jersey_Shoulder_Stripe,
                        Away_Jersey_Number_Color =@Away_Jersey_Number_Color,
                        Away_Jersey_Number_Outline_Color =@Away_Jersey_Number_Outline_Color,
                        Away_Jersey_Sleeve_Stripe_Color_1 =@Away_Jersey_Sleeve_Stripe_Color_1,
                        Away_Jersey_Sleeve_Stripe_Color_2 =@Away_Jersey_Sleeve_Stripe_Color_2,
                        Away_Jersey_Sleeve_Stripe_Color_3 =@Away_Jersey_Sleeve_Stripe_Color_3,
                        Away_Jersey_Sleeve_Stripe_Color_4 =@Away_Jersey_Sleeve_Stripe_Color_4,
                        Away_Jersey_Sleeve_Stripe_Color_5 =@Away_Jersey_Sleeve_Stripe_Color_5,
                        Away_Jersey_Sleeve_Stripe_Color_6 =@Away_Jersey_Sleeve_Stripe_Color_6,   
                        Away_Pants_Color =@Away_Pants_Color,
                        Away_Pants_Stripe_Color_1 =@Away_Pants_Stripe_Color_1,
                        Away_Pants_Stripe_Color_2 =@Away_Pants_Stripe_Color_2,
                        Away_Pants_Stripe_Color_3 =@Away_Pants_Stripe_Color_3,
                        Stadium_Name =@Stadium_Name,
                        Stadium_Location = @Stadium_Location,
                        Stadium_Field_Type =@Stadium_Field_Type,
                        Stadium_Field_Color =@Stadium_Field_Color,
                        Stadium_Capacity =@Stadium_Capacity,
                        Stadium_Img_Path = @Stadium_Img_path
                        WHERE ID = @ID"
            cmdTeam = SettingsConnection.CreateCommand
            cmdTeam.CommandText = sSQL
            cmdTeam.Parameters.Add("@City_Abr", Data.DbType.String).Value = t.City_Abr
            cmdTeam.Parameters.Add("@City", Data.DbType.String).Value = t.City
            cmdTeam.Parameters.Add("@Nickname", Data.DbType.String).Value = t.Nickname
            cmdTeam.Parameters.Add("@Helmet_img_path", Data.DbType.String).Value = Path.GetFileName(t.Helmet_img_path)
            cmdTeam.Parameters.Add("@Helmet_Color", Data.DbType.String).Value = t.Uniform.Helmet.Helmet_Color
            cmdTeam.Parameters.Add("@Helmet_Logo_Color", Data.DbType.String).Value = t.Uniform.Helmet.Helmet_Logo_Color
            cmdTeam.Parameters.Add("@Helmet_Facemask_Color", Data.DbType.String).Value = t.Uniform.Helmet.Helmet_Facemask_Color
            cmdTeam.Parameters.Add("@Socks_Color", Data.DbType.String).Value = t.Uniform.Footwear.Socks_Color
            cmdTeam.Parameters.Add("@Cleats_Color", Data.DbType.String).Value = t.Uniform.Footwear.Cleats_Color
            cmdTeam.Parameters.Add("@Home_jersey_Color", Data.DbType.String).Value = t.Uniform.Home_Jersey.Jersey_Color
            cmdTeam.Parameters.Add("@Home_Sleeve_Color", Data.DbType.String).Value = t.Uniform.Home_Jersey.Sleeve_Color
            cmdTeam.Parameters.Add("@Home_jersey_Shoulder_Stripe", Data.DbType.String).Value = t.Uniform.Home_Jersey.Shoulder_Stripe_Color
            cmdTeam.Parameters.Add("@Home_Jersey_Number_Color", Data.DbType.String).Value = t.Uniform.Home_Jersey.Number_Color
            cmdTeam.Parameters.Add("@Home_Jersey_Number_Outline_Color", Data.DbType.String).Value = t.Uniform.Home_Jersey.Number_Outline_Color
            cmdTeam.Parameters.Add("@Home_Jersey_Sleeve_Stripe_Color_1", Data.DbType.String).Value = t.Uniform.Home_Jersey.Sleeve_Stripe1
            cmdTeam.Parameters.Add("@Home_Jersey_Sleeve_Stripe_Color_2", Data.DbType.String).Value = t.Uniform.Home_Jersey.Sleeve_Stripe2
            cmdTeam.Parameters.Add("@Home_Jersey_Sleeve_Stripe_Color_3", Data.DbType.String).Value = t.Uniform.Home_Jersey.Sleeve_Stripe3
            cmdTeam.Parameters.Add("@Home_Jersey_Sleeve_Stripe_Color_4", Data.DbType.String).Value = t.Uniform.Home_Jersey.Sleeve_Stripe4
            cmdTeam.Parameters.Add("@Home_Jersey_Sleeve_Stripe_Color_5", Data.DbType.String).Value = t.Uniform.Home_Jersey.Sleeve_Stripe5
            cmdTeam.Parameters.Add("@Home_Jersey_Sleeve_Stripe_Color_6", Data.DbType.String).Value = t.Uniform.Home_Jersey.Sleeve_Stripe6
            cmdTeam.Parameters.Add("@Home_Pants_Color", Data.DbType.String).Value = t.Uniform.Home_Pants.Pants_Color
            cmdTeam.Parameters.Add("@Home_Pants_Stripe_Color_1", Data.DbType.String).Value = t.Uniform.Home_Pants.Stripe_Color_1
            cmdTeam.Parameters.Add("@Home_Pants_Stripe_Color_2", Data.DbType.String).Value = t.Uniform.Home_Pants.Stripe_Color_2
            cmdTeam.Parameters.Add("@Home_Pants_Stripe_Color_3", Data.DbType.String).Value = t.Uniform.Home_Pants.Stripe_Color_3
            cmdTeam.Parameters.Add("@Away_jersey_Color", Data.DbType.String).Value = t.Uniform.Away_Jersey.Jersey_Color
            cmdTeam.Parameters.Add("@Away_Sleeve_Color", Data.DbType.String).Value = t.Uniform.Away_Jersey.Sleeve_Color
            cmdTeam.Parameters.Add("@Away_jersey_Shoulder_Stripe", Data.DbType.String).Value = t.Uniform.Away_Jersey.Shoulder_Stripe_Color
            cmdTeam.Parameters.Add("@Away_Jersey_Number_Color", Data.DbType.String).Value = t.Uniform.Away_Jersey.Number_Color
            cmdTeam.Parameters.Add("@Away_Jersey_Number_Outline_Color", Data.DbType.String).Value = t.Uniform.Away_Jersey.Number_Outline_Color
            cmdTeam.Parameters.Add("@Away_Jersey_Sleeve_Stripe_Color_1", Data.DbType.String).Value = t.Uniform.Away_Jersey.Sleeve_Stripe1
            cmdTeam.Parameters.Add("@Away_Jersey_Sleeve_Stripe_Color_2", Data.DbType.String).Value = t.Uniform.Away_Jersey.Sleeve_Stripe2
            cmdTeam.Parameters.Add("@Away_Jersey_Sleeve_Stripe_Color_3", Data.DbType.String).Value = t.Uniform.Away_Jersey.Sleeve_Stripe3
            cmdTeam.Parameters.Add("@Away_Jersey_Sleeve_Stripe_Color_4", Data.DbType.String).Value = t.Uniform.Away_Jersey.Sleeve_Stripe4
            cmdTeam.Parameters.Add("@Away_Jersey_Sleeve_Stripe_Color_5", Data.DbType.String).Value = t.Uniform.Away_Jersey.Sleeve_Stripe5
            cmdTeam.Parameters.Add("@Away_Jersey_Sleeve_Stripe_Color_6", Data.DbType.String).Value = t.Uniform.Away_Jersey.Sleeve_Stripe6
            cmdTeam.Parameters.Add("@Away_Pants_Color", Data.DbType.String).Value = t.Uniform.Away_Pants.Pants_Color
            cmdTeam.Parameters.Add("@Away_Pants_Stripe_Color_1", Data.DbType.String).Value = t.Uniform.Away_Pants.Stripe_Color_1
            cmdTeam.Parameters.Add("@Away_Pants_Stripe_Color_2", Data.DbType.String).Value = t.Uniform.Away_Pants.Stripe_Color_2
            cmdTeam.Parameters.Add("@Away_Pants_Stripe_Color_3", Data.DbType.String).Value = t.Uniform.Away_Pants.Stripe_Color_3
            cmdTeam.Parameters.Add("@Stadium_Name", Data.DbType.String).Value = t.Stadium.Stadium_Name
            cmdTeam.Parameters.Add("@Stadium_Location", Data.DbType.String).Value = t.Stadium.Stadium_Location
            cmdTeam.Parameters.Add("@Stadium_Field_Type", Data.DbType.Int16).Value = t.Stadium.Field_Type
            cmdTeam.Parameters.Add("@Stadium_Field_Color", Data.DbType.String).Value = t.Stadium.Field_Color
            cmdTeam.Parameters.Add("@Stadium_Capacity", Data.DbType.String).Value = t.Stadium.Capacity
            cmdTeam.Parameters.Add("@Stadium_Img_path", Data.DbType.String).Value = Path.GetFileName(t.Stadium.Stadium_Img_Path)
            cmdTeam.Parameters.Add("@ID", Data.DbType.Int16).Value = t.id
            Dim i As Integer = cmdTeam.ExecuteNonQuery()

            If i <> 1 Then
                Throw New Exception("Error editing stock team " & i & " rows edited")
            End If

        Finally
            If Not IsNothing(cmdTeam) Then cmdTeam.Dispose()
            If SettingsConnection.State = ConnectionState.Open Then
                SettingsConnection.Close()
            End If
        End Try

    End Sub



End Class
