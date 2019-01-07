Public Class CommonUtils

    Private Shared random As New Random()

    Public Shared Function ExtractTeamNumber(ByVal s As String) As Integer
        Dim i = s.IndexOf("Team")
        Dim r = s.Substring(i + 4)
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

        r = random.Next(iLow, iHigh)

        Return r.ToString
    End Function
    Public Shared Function getDivisionNum_from_Team_Number(ByVal num_divs As Integer, ByVal t_num As Integer) As Integer

        Return ((t_num - 1) Mod num_divs) + 1

    End Function
    Public Shared Function getHexfromColor(ByVal c As System.Windows.Media.Color) As String
        Dim r As String = Nothing

        r = "#" + c.R.ToString("X2") + c.G.ToString("X2") + c.B.ToString("X2")

        Return r
    End Function
    Public Shared Function getColorfromHex(ByVal s As String) As System.Windows.Media.Color
        Dim r As System.Windows.Media.Color = Nothing
        Dim red As Integer = 0
        Dim green As Integer = 0
        Dim blue As Integer = 0

        red = Convert.ToInt32(s.Substring(1, 2), 16)
        green = Convert.ToInt32(s.Substring(3, 2), 16)
        blue = Convert.ToInt32(s.Substring(5, 2), 16)


    End Function

End Class
