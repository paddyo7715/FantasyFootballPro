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

        validate()

    End Sub

    Private Sub validate()

        If CommonUtils.isBlank(Stadium_Name) Then
            Throw New Exception("Stadium Name must have a value")
        End If

        If CommonUtils.isBlank(Stadium_Location) Then
            Throw New Exception("Stadium Location must have a value")
        End If

        If CommonUtils.isBlank(Capacity) Then
            Throw New Exception("Stadium Capacity must be supplied and numeric")
        End If

        If CommonUtils.isBlank(Stadium_Img_Path) Then
            Throw New Exception("Image of team stadium must be supplied")
        End If

    End Sub

End Class
