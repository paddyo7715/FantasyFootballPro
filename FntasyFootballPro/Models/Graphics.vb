Public Class Graphics
    Property Helmet_img_path As String = ""

    Public Sub New(ByVal Helmet_img_path As String)
        Me.Helmet_img_path = Helmet_img_path

        validate()
    End Sub

    Private Sub validate()

        If CommonUtils.isBlank(Helmet_img_path) Then
            Throw New Exception("Helmet image path must have a value")
        End If
    End Sub

End Class
