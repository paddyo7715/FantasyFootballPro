Imports System.Data
Imports System.Data.SQLite

Public Class Player_DAO

    Public Shared Function isPlayerNumber_Unigue_DB(ByVal number As String, ByVal league_db_connection As String, ByVal team_ind As Integer) As Boolean
        Dim LeagueConnection As SQLiteConnection = New SQLiteConnection()
        LeagueConnection.ConnectionString = league_db_connection
        Dim cmd As SQLiteCommand = Nothing
        Dim r As Boolean = False

        Try
            LeagueConnection.Open()
            Dim sSQL = "select count(*) from Players where Jersey_Number = @JerseyNumber and Active = 1"
            cmd = LeagueConnection.CreateCommand
            cmd.CommandText = sSQL
            cmd.Parameters.Add("@JerseyNumber", Data.DbType.Int16).Value = CInt(number.Trim)

            Dim i As Integer = cmd.ExecuteNonQuery

            If i = 0 Then
                r = True
            End If

            Return r

        Catch ex As Exception
            Throw ex
        Finally
            If Not IsNothing(cmd) Then cmd.Dispose()
            If LeagueConnection.State = ConnectionState.Open Then
                LeagueConnection.Close()
            End If
        End Try

    End Function

End Class
