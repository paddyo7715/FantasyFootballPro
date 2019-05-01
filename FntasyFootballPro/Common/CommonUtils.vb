Imports System.Drawing

Public Class CommonUtils

    Private Shared random As New Random()

    Public Shared Function ExtractTeamNumber(ByVal s As String) As Integer
        Dim i = s.IndexOf("Team")
        Dim r = s.Substring(i + 4)
        Return CInt(r)
    End Function
    Public Shared Function ExtractDivNumber(ByVal s As String) As Integer
        Dim i = s.IndexOf("newldiv")
        Dim r = s.Substring(i + 7)
        Return CInt(r)
    End Function
    Public Shared Function isBlank(ByVal s As String) As Boolean
        If IsNothing(s) OrElse s.Trim.Length = 0 Then
            Return True
        Else
            Return False
        End If
    End Function
    Public Shared Function CapitalizeFirstLetter(ByVal s As String) As String
        Return s.Substring(0, 1).ToUpper + substr(s, 1, s.Length)
    End Function
    Public Shared Function substr(ByVal s As String, ByVal f As Integer, ByVal l As Integer) As String
        Dim r As String
        If IsNothing(s) Then
            r = ""
        ElseIf s.Trim = "" Then
            r = ""
        ElseIf f > s.Length - 1 Then
            r = ""
        Else
            r = s.Substring(f, Math.Min(l, s.Length - f))
        End If

        Return r
    End Function
    Public Shared Function getRandomNum(ByVal iLow As Integer, ByVal iHigh As Integer) As Integer
        Dim r As Integer

        r = random.Next(iLow, iHigh + 1)

        Return r.ToString
    End Function
    Public Shared Function getDivisionNum_from_Team_Number(ByVal num_divs As Integer, ByVal t_num As Integer) As Integer

        Return ((t_num - 1) Mod num_divs) + 1

    End Function
    Public Shared Function getLeagueStructure(ByVal s As String) As Integer()

        Dim i As Integer = 0
        Dim Teams As Integer
        Dim Weeks_in_Season As Integer
        Dim Games_in_Season As Integer
        Dim Num_Divisions As Integer
        Dim Conferences As Integer
        Dim PlayoffTeams As Integer

        Dim ls As String() = s.Split(" ")
        For Each l In ls
            Select Case l
                Case "Teams"
                    Teams = ls(i + 1)
                Case "Games"
                    Games_in_Season = ls(i + 1)
                Case "Weeks"
                    Weeks_in_Season = ls(i + 1)
                Case "Conferences"
                    Conferences = ls(i + 1)
                Case "Divisions"
                    Num_Divisions = ls(i + 1)
                Case "PlayoffTeams"
                    PlayoffTeams = ls(i + 1)
            End Select

            i += 1
        Next

        Return New Integer() {Weeks_in_Season, Games_in_Season, Num_Divisions, Teams, Conferences, PlayoffTeams}


    End Function

    Public Shared Function getConferenceNum_from_Team_Number(ByVal num_confs As Integer, ByVal t_num As Integer) As Integer

        Dim r As Integer

        If num_confs = 0 Then
            r = 0
        ElseIf num_confs > t_num Then
            r = 2
        Else
            r = 1
        End If

        Return r

    End Function

    Public Shared Function getHexfromColor(ByVal c As System.Windows.Media.Color) As String
        Dim r As String = Nothing

        r = "#" + c.R.ToString("X2") + c.G.ToString("X2") + c.B.ToString("X2")

        Return r
    End Function
    Public Shared Function getColorfromHex(ByVal s As String) As System.Windows.Media.Color
        Dim r As System.Windows.Media.Color = New System.Windows.Media.Color()
        Dim red As Byte = 0
        Dim green As Byte = 0
        Dim blue As Byte = 0

        red = CType(Convert.ToInt32(s.Substring(1, 2), 16), Byte)
        green = CType(Convert.ToInt32(s.Substring(3, 2), 16), Byte)
        blue = CType(Convert.ToInt32(s.Substring(5, 2), 16), Byte)

        r = System.Windows.Media.Color.FromRgb(red, green, blue)
        Return r

    End Function

End Class
