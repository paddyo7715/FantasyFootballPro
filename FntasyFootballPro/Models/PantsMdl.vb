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

    End Sub

End Class
