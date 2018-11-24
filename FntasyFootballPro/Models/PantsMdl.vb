Public Class PantsMdl
    Property Pants_Color As String = ""
    Property Stripe_Color_1 As String = ""
    Property Stripe_Color_2 As String = ""
    Property Stripe_Color_3 As String = ""

    Public Sub New(ByVal Pants_Color As String, ByVal Stripe_Color_1 As String,
                   ByVal Stripe_Color_2 As String, ByVal Stripe_Color_3 As String)

        Me.Pants_Color = Pants_Color
        Me.Stripe_Color_1 = Stripe_Color_1
        Me.Stripe_Color_2 = Stripe_Color_2
        Me.Stripe_Color_3 = Stripe_Color_3

        validate()
    End Sub

    Private Sub validate()

        If CommonUtils.isBlank(Pants_Color) Then
            Throw New Exception("Home pants color must have a value")
        End If

        If CommonUtils.isBlank(Stripe_Color_1) Then
            Throw New Exception("Home pants stripe 1 color must have a value")
        End If

        If CommonUtils.isBlank(Stripe_Color_2) Then
            Throw New Exception("Home pants stripe 2 color must have a value")
        End If

        If CommonUtils.isBlank(Stripe_Color_3) Then
            Throw New Exception("Home pants stripe 3 color must have a value")
        End If

    End Sub

End Class
