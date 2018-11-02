Public Class Helmet
    Property Helmet_img_path As String
    Property Helmet_Color As String
    Property Helmet_Logo_Color As String
    Property Helmet_Facemask_Color As String
    Property Helmet_Stripe1_Color As String
    Property Helmet_Stripe2_Color As String
    Property Helmet_Stripe3_Color As String

    Public Sub New(ByVal Helmet_img_path As String, ByVal Helmet_Color As String,
                 ByVal Helmet_Logo_Color As String, ByVal Helmet_Facemask_Color As String,
                 ByVal Helmet_Stripe1_Color As String, ByVal Helmet_Stripe2_Color As String,
                 ByVal Helmet_Stripe3_Color As String)

        Me.Helmet_img_path = Helmet_img_path
        Me.Helmet_Color = Helmet_Color
        Me.Helmet_Logo_Color = Helmet_Logo_Color
        Me.Helmet_Facemask_Color = Helmet_Facemask_Color
        Me.Helmet_Stripe1_Color = Helmet_Stripe1_Color
        Me.Helmet_Stripe2_Color = Helmet_Stripe1_Color
        Me.Helmet_Stripe3_Color = Helmet_Stripe1_Color

    End Sub

End Class
