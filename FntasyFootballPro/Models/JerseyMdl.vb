﻿Public Class JerseyMdl
    Property Jersey_Color As String = ""
    Property Sleeve_Color As String = ""
    Property Shoulder_Stripe_Color As String = ""
    Property Number_Color As String = ""
    Property Number_Outline_Color As String = ""
    Property Sleeve_Stripe1 As String = ""
    Property Sleeve_Stripe2 As String = ""
    Property Sleeve_Stripe3 As String = ""
    Property Sleeve_Stripe4 As String = ""
    Property Sleeve_Stripe5 As String = ""
    Property Sleeve_Stripe6 As String = ""


    Public Sub New(ByVal Jersey_Color As String, ByVal Sleeve_Color As String, ByVal Shoulder_Stripe_Color As String,
        ByVal Number_Color As String, ByVal Number_Outline_Color As String,
        ByVal Sleeve_Stripe1 As String, ByVal Sleeve_Stripe2 As String,
        ByVal Sleeve_Stripe3 As String, ByVal Sleeve_Stripe4 As String,
        ByVal Sleeve_Stripe5 As String, ByVal Sleeve_Stripe6 As String)

        Me.Jersey_Color = Jersey_Color
        Me.Sleeve_Color = Sleeve_Color
        Me.Shoulder_Stripe_Color = Shoulder_Stripe_Color
        Me.Number_Color = Number_Color
        Me.Number_Outline_Color = Number_Outline_Color
        Me.Sleeve_Stripe1 = Sleeve_Stripe1
        Me.Sleeve_Stripe2 = Sleeve_Stripe2
        Me.Sleeve_Stripe3 = Sleeve_Stripe3
        Me.Sleeve_Stripe4 = Sleeve_Stripe4
        Me.Sleeve_Stripe5 = Sleeve_Stripe5
        Me.Sleeve_Stripe6 = Sleeve_Stripe6

    End Sub


End Class
