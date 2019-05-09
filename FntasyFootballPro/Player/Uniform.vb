Public Class Uniform_Color_percents
    Property color_string As String
    Property value As Single

    Public Sub New(ByVal color_string As String, ByVal axis_start As Single)
        Me.color_string = color_string
        Me.value = axis_start
    End Sub
End Class

Public Class Uniform
    Public Shared Function getColorList(ByVal u As UniformMdl) As List(Of Uniform_Color_percents)
        Dim r As List(Of Uniform_Color_percents) = New List(Of Uniform_Color_percents)

        Dim letter_color As String = u.Home_Jersey.Number_Color

        Dim letter_uni_percent As Uniform_Color_percents = New Uniform_Color_percents(letter_color, 0.0)
        r.Add(letter_uni_percent)

        r.Add(New Uniform_Color_percents(u.Home_Jersey.Jersey_Color, 40.0))

        If u.Helmet.Helmet_Color <> letter_color Then
            addUpdate_Uniform(r, New Uniform_Color_percents(u.Helmet.Helmet_Color, 25.0))
        End If

        If u.Home_Pants.Pants_Color <> letter_color Then
            addUpdate_Uniform(r, New Uniform_Color_percents(u.Home_Pants.Pants_Color, 25.0))
        End If

        If u.Home_Jersey.Sleeve_Color <> letter_color Then
            addUpdate_Uniform(r, New Uniform_Color_percents(u.Home_Jersey.Sleeve_Color, 10.0))
        End If

        If u.Home_Jersey.Shoulder_Stripe_Color <> letter_color Then
            addUpdate_Uniform(r, New Uniform_Color_percents(u.Home_Jersey.Shoulder_Stripe_Color, 2.0))
        End If

        If u.Home_Jersey.Sleeve_Stripe1 <> letter_color Then
            addUpdate_Uniform(r, New Uniform_Color_percents(u.Home_Jersey.Sleeve_Stripe1, 1.0))
        End If

        If u.Home_Jersey.Sleeve_Stripe2 <> letter_color Then
            addUpdate_Uniform(r, New Uniform_Color_percents(u.Home_Jersey.Sleeve_Stripe2, 1.0))
        End If

        If u.Home_Jersey.Sleeve_Stripe3 <> letter_color Then
            addUpdate_Uniform(r, New Uniform_Color_percents(u.Home_Jersey.Sleeve_Stripe3, 1.0))
        End If

        If u.Home_Jersey.Sleeve_Stripe4 <> letter_color Then
            addUpdate_Uniform(r, New Uniform_Color_percents(u.Home_Jersey.Sleeve_Stripe4, 1.0))
        End If

        If u.Home_Jersey.Sleeve_Stripe5 <> letter_color Then
            addUpdate_Uniform(r, New Uniform_Color_percents(u.Home_Jersey.Sleeve_Stripe5, 1.0))
        End If

        If u.Home_Jersey.Sleeve_Stripe6 <> letter_color Then
            addUpdate_Uniform(r, New Uniform_Color_percents(u.Home_Jersey.Sleeve_Stripe6, 1.0))
        End If

        If u.Home_Pants.Stripe_Color_1 <> letter_color Then
            addUpdate_Uniform(r, New Uniform_Color_percents(u.Home_Pants.Stripe_Color_1, 1.0))
        End If

        If u.Home_Pants.Stripe_Color_2 <> letter_color Then
            addUpdate_Uniform(r, New Uniform_Color_percents(u.Home_Pants.Stripe_Color_2, 1.0))
        End If

        If u.Home_Pants.Stripe_Color_3 <> letter_color Then
            addUpdate_Uniform(r, New Uniform_Color_percents(u.Home_Pants.Stripe_Color_3, 1.0))
        End If

        Dim tot_values As Single
        For Each w In r
            tot_values += w.value
        Next

        Dim bFirst As Boolean = True
        Dim running_value As Single = 0.0
        For i As Integer = 1 To r.Count - 1
            If bFirst Then
                running_value = 0.0
                bFirst = False
            End If

            r(i).value = running_value / tot_values
            running_value += r(i).value

        Next

        Return r

    End Function
    Public Shared Sub addUpdate_Uniform(ByVal lup_l As List(Of Uniform_Color_percents),
                                        ByVal lup As Uniform_Color_percents)
        Dim bfound = False

        For Each p In lup_l
            If lup.color_string = p.color_string Then
                p.value += lup.value
                bfound = True
                Exit For
            End If
        Next

        If Not bfound Then
            lup_l.Add(lup)
        End If


    End Sub

End Class
