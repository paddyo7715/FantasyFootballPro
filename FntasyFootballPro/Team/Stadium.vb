Public Class Stadium
    Property Stadium_Name As String
    Property Stadium_Location As String
    Property Stadium_Img_Path As String


    Public Sub New(ByVal Stadium_Name As String, ByVal Stadium_Location As String,
                   ByVal Stadium_Img_Path As String)

        Me.Stadium_Img_Path = Stadium_Img_Path
        Me.Stadium_Location = Stadium_Location
        Me.Stadium_Name = Stadium_Name

    End Sub
End Class
