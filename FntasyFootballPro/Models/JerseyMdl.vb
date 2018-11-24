Public Class JerseyMdl
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
    Property Sleeve_Stripe7 As String = ""
    Property Sleeve_Stripe8 As String = ""
    Property Sleeve_Stripe9 As String = ""


    Public Sub New(ByVal Jersey_Color As String, ByVal Sleeve_Color As String, ByVal Shoulder_Stripe_Color As String,
        ByVal Number_Color As String, ByVal Number_Outline_Color As String,
        ByVal Sleeve_Stripe1 As String, ByVal Sleeve_Stripe2 As String,
        ByVal Sleeve_Stripe3 As String, ByVal Sleeve_Stripe4 As String,
        ByVal Sleeve_Stripe5 As String, ByVal Sleeve_Stripe6 As String,
        ByVal Sleeve_Stripe7 As String, ByVal Sleeve_Stripe8 As String,
        ByVal Sleeve_Stripe9 As String)

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
        Me.Sleeve_Stripe7 = Sleeve_Stripe7
        Me.Sleeve_Stripe8 = Sleeve_Stripe8
        Me.Sleeve_Stripe9 = Sleeve_Stripe9

        validate()

    End Sub

    Private Sub validate()

        If CommonUtils.isBlank(Jersey_Color) Then
            Throw New Exception("Jersey color must have a value")
        End If

        If CommonUtils.isBlank(Sleeve_Color) Then
            Throw New Exception("Jersey sleeve color must have a value")
        End If

        If CommonUtils.isBlank(Shoulder_Stripe_Color) Then
            Throw New Exception("Jersey shoulder stripe color must have a value")
        End If

        If CommonUtils.isBlank(Number_Color) Then
            Throw New Exception("Jersey numbering color must have a value")
        End If

        If CommonUtils.isBlank(Number_Outline_Color) Then
            Throw New Exception("Jersey number outline color must have a value")
        End If

        If CommonUtils.isBlank(Sleeve_Stripe1) Then
            Throw New Exception("Jersey sleeve stripe 1 color must have a value")
        End If

        If CommonUtils.isBlank(Sleeve_Stripe2) Then
            Throw New Exception("Jersey sleeve stripe 2 color must have a value")
        End If

        If CommonUtils.isBlank(Sleeve_Stripe3) Then
            Throw New Exception("Jersey sleeve stripe 3 color must have a value")
        End If

        If CommonUtils.isBlank(Sleeve_Stripe4) Then
            Throw New Exception("Jersey sleeve stripe 4 color must have a value")
        End If

        If CommonUtils.isBlank(Sleeve_Stripe5) Then
            Throw New Exception("Jersey sleeve stripe 5 color must have a value")
        End If

        If CommonUtils.isBlank(Sleeve_Stripe6) Then
            Throw New Exception("Jersey sleeve stripe 6 color must have a value")
        End If

        If CommonUtils.isBlank(Sleeve_Stripe7) Then
            Throw New Exception("Jersey sleeve stripe 7 color must have a value")
        End If

        If CommonUtils.isBlank(Sleeve_Stripe8) Then
            Throw New Exception("Jersey sleeve stripe 8 color must have a value")
        End If

        If CommonUtils.isBlank(Sleeve_Stripe9) Then
            Throw New Exception("Jersey sleeve stripe 9 color must have a value")
        End If



    End Sub

End Class
