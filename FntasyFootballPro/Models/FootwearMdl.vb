Public Class FootwearMdl
    Property Socks_Color As String
    Property Cleats_Color As String

    Public Sub New(ByVal Socks_Color As String, ByVal Cleats_Color As String)
        Me.Socks_Color = Socks_Color
        Me.Cleats_Color = Cleats_Color

        validate()
    End Sub

    Private Sub validate()

        If CommonUtils.isBlank(Socks_Color) Then
            Throw New Exception("Uniform sock color must have a value")
        End If

        If CommonUtils.isBlank(Cleats_Color) Then
            Throw New Exception("Uniform cleats color must have a value")
        End If
    End Sub
End Class
