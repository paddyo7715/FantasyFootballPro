Public Class StadiumMdl
    Property Stadium_Name As String = ""
    Property Stadium_Location As String = ""
    Property Field_Type As Integer '1 grass, 2 artificial
    Property Field_Color As String
    Property Capacity As String = ""
    Property Stadium_Img_Path As String = ""


    Public Sub New(ByVal Stadium_Name As String, ByVal Stadium_Location As String,
                   ByVal Field_Type As Integer, ByVal Field_Color As String,
                   ByVal Capacity As String, ByVal Stadium_Img_Path As String)

        Me.Stadium_Img_Path = Stadium_Img_Path
        Me.Stadium_Location = Stadium_Location
        Me.Field_Type = Field_Type
        Me.Field_Color = Field_Color
        Me.Capacity = Capacity
        Me.Stadium_Name = Stadium_Name

    End Sub

End Class
