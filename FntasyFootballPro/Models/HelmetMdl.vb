Public Class HelmetMdl
    Property Helmet_Color As String = ""
    Property Helmet_Logo_Color As String = ""
    Property Helmet_Facemask_Color As String = ""

    Public Sub New(ByVal Helmet_Color As String,
                 ByVal Helmet_Logo_Color As String, ByVal Helmet_Facemask_Color As String)

        Me.Helmet_Color = Helmet_Color
        Me.Helmet_Logo_Color = Helmet_Logo_Color
        Me.Helmet_Facemask_Color = Helmet_Facemask_Color

        validate()
    End Sub

    Private Sub validate()

        If CommonUtils.isBlank(Helmet_Color) Then
            Throw New Exception("Helmet color must have a value")
        End If

        If CommonUtils.isBlank(Helmet_Logo_Color) Then
            Throw New Exception("Helmet logo color must have a value")
        End If

        If CommonUtils.isBlank(Helmet_Facemask_Color) Then
            Throw New Exception("Helmet facemask color must have a value")
        End If

    End Sub

End Class
