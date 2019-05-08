Imports System.Data.SQLite
Imports System.Configuration
Imports System.Data

Public Class Player_NamesDAO
    Property SettingsConnection As SQLiteConnection = New SQLiteConnection()

    Public Sub New()
        Dim constr As String = ""
        constr = CommonUtils.getSettingsDBConnectionString
        SettingsConnection.ConnectionString = constr

    End Sub
    Public Function AddFirstName(ByVal FirstName As String) As Integer
        Dim r As Integer = 0
        Dim cmd As SQLiteCommand = Nothing
        Try
            SettingsConnection.Open()
            Dim sSQL = "INSERT INTO POTENTIAL_FIRST_NAMES VALUES(@FirstName)"

            cmd = SettingsConnection.CreateCommand
            cmd.CommandText = sSQL
            cmd.Parameters.Add("@FirstName", Data.DbType.String).Value = FirstName
            r = cmd.ExecuteNonQuery
        Catch ex As Exception
            Dim noop As Integer = 1
        Finally
            If Not IsNothing(cmd) Then cmd.Dispose()
            If SettingsConnection.State = ConnectionState.Open Then
                SettingsConnection.Close()
            End If
        End Try

        Return r
    End Function

    Public Function AddLastName(ByVal LastName As String) As Integer
        Dim r As Integer = 0
        Dim cmd As SQLiteCommand = Nothing
        Try
            SettingsConnection.Open()
            Dim sSQL = "INSERT INTO POTENTIAL_LAST_NAMES VALUES(@LastName)"

            cmd = SettingsConnection.CreateCommand
            cmd.CommandText = sSQL
            cmd.Parameters.Add("@LastName", Data.DbType.String).Value = LastName
            r = cmd.ExecuteNonQuery
        Catch ex As Exception
            'Do nothing
        Finally
            If Not IsNothing(cmd) Then cmd.Dispose()

            If SettingsConnection.State = ConnectionState.Open Then
                SettingsConnection.Close()
            End If
        End Try

        Return r
    End Function
    Public Function getTotalFirstNames() As Long
        Dim r As Long = 0
        Dim cmd As SQLiteCommand = Nothing
        Try
            SettingsConnection.Open()
            Dim sSQL = "SELECT COUNT(*) FROM POTENTIAL_FIRST_NAMES"
            cmd = SettingsConnection.CreateCommand
            cmd.CommandText = sSQL
            r = cmd.ExecuteScalar
        Finally
            If Not IsNothing(cmd) Then cmd.Dispose()

            If SettingsConnection.State = ConnectionState.Open Then
                SettingsConnection.Close()
            End If
        End Try

        Return r
    End Function

    Public Function getTotalLastNames() As Long
        Dim r As Long = 0
        Dim cmd As SQLiteCommand = Nothing
        Try
            SettingsConnection.Open()
            Dim sSQL = "SELECT COUNT(*) FROM POTENTIAL_LAST_NAMES"
            cmd = SettingsConnection.CreateCommand
            cmd.CommandText = sSQL
            r = cmd.ExecuteScalar

        Finally
            If Not IsNothing(cmd) Then cmd.Dispose()

            If SettingsConnection.State = ConnectionState.Open Then
                SettingsConnection.Close()
            End If
        End Try

        Return r
    End Function
    Public Function CreatePlayerName() As String()
        Dim sFirstName As String
        Dim sLastName As String
        Dim sSQL As String
        Dim cmd As SQLiteCommand = Nothing
        Try
            SettingsConnection.Open()

            sSQL = "SELECT FirstName FROM POTENTIAL_FIRST_NAMES ORDER BY RANDOM() LIMIT 1;"
            cmd = SettingsConnection.CreateCommand
            cmd.CommandText = sSQL
            sFirstName = cmd.ExecuteScalar

            sSQL = "SELECT LastName FROM POTENTIAL_LAST_NAMES ORDER BY RANDOM() LIMIT 1;"
            cmd = SettingsConnection.CreateCommand
            cmd.CommandText = sSQL
            sLastName = cmd.ExecuteScalar

        Catch ex As Exception
            Throw ex
        Finally
            If Not IsNothing(cmd) Then cmd.Dispose()

            If SettingsConnection.State = ConnectionState.Open Then
                SettingsConnection.Close()
            End If
        End Try

        Return New String() {sFirstName, sLastName}

    End Function

End Class
