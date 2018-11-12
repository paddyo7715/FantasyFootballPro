Public Class HelmetMdl
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

        validate()
    End Sub

    Private Sub validate()

        If CommonUtils.isBlank(Helmet_img_path) Then
            Throw New Exception("Helmet image path must have a value")
        End If

        If CommonUtils.isBlank(Helmet_Color) Then
            Throw New Exception("Helmet color must have a value")
        End If

        If CommonUtils.isBlank(Helmet_Logo_Color) Then
            Throw New Exception("Helmet logo color must have a value")
        End If

        If CommonUtils.isBlank(Helmet_Facemask_Color) Then
            Throw New Exception("Helmet facemask color must have a value")
        End If

        If CommonUtils.isBlank(Helmet_Stripe1_Color) Then
            Throw New Exception("Helmet stripe 1 color must have a value")
        End If

        If CommonUtils.isBlank(Helmet_Stripe2_Color) Then
            Throw New Exception("Helmet stripe 2 color must have a value")
        End If

        If CommonUtils.isBlank(Helmet_Stripe3_Color) Then
            Throw New Exception("Helmet stripe 3 color must have a value")
        End If



    End Sub

End Class
