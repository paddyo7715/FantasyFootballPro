﻿Public Class PlayerMdl
    Public Enum Position
        QB
        RB
        WR
        TE
        OL
        DL
        DB
        LB
        K
        P
    End Enum


    Property First_Name As String = ""
    Property Last_Name As String = ""
    Property Age As Integer = 0
    Property Jersey_Number As Integer
    Property Active As Boolean
    Property Pos As Position

    Property Ratings As Player_Abilities

End Class



