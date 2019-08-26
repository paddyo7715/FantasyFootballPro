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
            addUpdate_Uniform(r, New Uniform_Color_percents(u.Home_Pants.Pants_Color, 50.0))
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

            running_value += r(i).value
            r(i).value = running_value / tot_values

        Next

        Return r

    End Function

    Public Shared Function getAllColorList(ByVal u As UniformMdl) As List(Of String)
        Dim r As List(Of String) = New List(Of String)

        Dim helmet_color = u.Helmet.Helmet_Color
        r.Add(helmet_color)

        Dim helmet_logo_color = u.Helmet.Helmet_Logo_Color
        If Not r.Contains(helmet_logo_color) Then r.Add(helmet_logo_color)

        Dim helmet_facemask_color = u.Helmet.Helmet_Facemask_Color
        If Not r.Contains(helmet_facemask_color) Then r.Add(helmet_facemask_color)

        Dim home_jersey_color = u.Home_Jersey.Jersey_Color
        If Not r.Contains(home_jersey_color) Then r.Add(home_jersey_color)

        Dim home_Sleeve_Color As String = u.Home_Jersey.Sleeve_Color
        If Not r.Contains(home_Sleeve_Color) Then r.Add(home_Sleeve_Color)

        Dim home_Shoulder_Stripe_Color As String = u.Home_Jersey.Shoulder_Stripe_Color
        If Not r.Contains(home_Shoulder_Stripe_Color) Then r.Add(home_Shoulder_Stripe_Color)

        Dim home_Number_Color As String = u.Home_Jersey.Number_Color
        If Not r.Contains(home_Number_Color) Then r.Add(home_Number_Color)

        Dim home_Number_Outline_Color As String = u.Home_Jersey.Number_Outline_Color
        If Not r.Contains(home_Number_Outline_Color) Then r.Add(home_Number_Outline_Color)

        Dim home_Sleeve_Stripe1 As String = u.Home_Jersey.Sleeve_Stripe1
        If Not r.Contains(home_Sleeve_Stripe1) Then r.Add(home_Sleeve_Stripe1)

        Dim home_Sleeve_Stripe2 As String = u.Home_Jersey.Sleeve_Stripe2
        If Not r.Contains(home_Sleeve_Stripe2) Then r.Add(home_Sleeve_Stripe2)

        Dim home_Sleeve_Stripe3 As String = u.Home_Jersey.Sleeve_Stripe3
        If Not r.Contains(home_Sleeve_Stripe3) Then r.Add(home_Sleeve_Stripe3)

        Dim home_Sleeve_Stripe4 As String = u.Home_Jersey.Sleeve_Stripe4
        If Not r.Contains(home_Sleeve_Stripe4) Then r.Add(home_Sleeve_Stripe4)

        Dim home_Sleeve_Stripe5 As String = u.Home_Jersey.Sleeve_Stripe5
        If Not r.Contains(home_Sleeve_Stripe5) Then r.Add(home_Sleeve_Stripe5)

        Dim home_Sleeve_Stripe6 As String = u.Home_Jersey.Sleeve_Stripe6
        If Not r.Contains(home_Sleeve_Stripe6) Then r.Add(home_Sleeve_Stripe6)

        Dim home_Pantshome_Color As String = u.Home_Pants.Pants_Color
        If Not r.Contains(home_Pantshome_Color) Then r.Add(home_Pantshome_Color)

        Dim home_Stripe_Color_1 As String = u.Home_Pants.Stripe_Color_1
        If Not r.Contains(home_Stripe_Color_1) Then r.Add(home_Stripe_Color_1)

        Dim home_Stripe_Color_2 As String = u.Home_Pants.Stripe_Color_2
        If Not r.Contains(home_Stripe_Color_2) Then r.Add(home_Stripe_Color_2)

        Dim home_Stripe_Color_3 As String = u.Home_Pants.Stripe_Color_3
        If Not r.Contains(home_Stripe_Color_3) Then r.Add(home_Stripe_Color_3)

        Dim away_jersey_color = u.Away_Jersey.Jersey_Color
        If Not r.Contains(away_jersey_color) Then r.Add(away_jersey_color)

        Dim away_Sleeve_Color As String = u.Away_Jersey.Sleeve_Color
        If Not r.Contains(away_Sleeve_Color) Then r.Add(away_Sleeve_Color)

        Dim away_Shoulder_Stripe_Color As String = u.Away_Jersey.Shoulder_Stripe_Color
        If Not r.Contains(away_Shoulder_Stripe_Color) Then r.Add(away_Shoulder_Stripe_Color)

        Dim away_Number_Color As String = u.Away_Jersey.Number_Color
        If Not r.Contains(away_Number_Color) Then r.Add(away_Number_Color)

        Dim away_Number_Outline_Color As String = u.Away_Jersey.Number_Outline_Color
        If Not r.Contains(away_Number_Outline_Color) Then r.Add(away_Number_Outline_Color)

        Dim away_Sleeve_Stripe1 As String = u.Away_Jersey.Sleeve_Stripe1
        If Not r.Contains(away_Sleeve_Stripe1) Then r.Add(away_Sleeve_Stripe1)

        Dim away_Sleeve_Stripe2 As String = u.Away_Jersey.Sleeve_Stripe2
        If Not r.Contains(away_Sleeve_Stripe2) Then r.Add(away_Sleeve_Stripe2)

        Dim away_Sleeve_Stripe3 As String = u.Away_Jersey.Sleeve_Stripe3
        If Not r.Contains(away_Sleeve_Stripe3) Then r.Add(away_Sleeve_Stripe3)

        Dim away_Sleeve_Stripe4 As String = u.Away_Jersey.Sleeve_Stripe4
        If Not r.Contains(away_Sleeve_Stripe4) Then r.Add(away_Sleeve_Stripe4)

        Dim away_Sleeve_Stripe5 As String = u.Away_Jersey.Sleeve_Stripe5
        If Not r.Contains(away_Sleeve_Stripe5) Then r.Add(away_Sleeve_Stripe5)

        Dim away_Sleeve_Stripe6 As String = u.Away_Jersey.Sleeve_Stripe6
        If Not r.Contains(away_Sleeve_Stripe6) Then r.Add(away_Sleeve_Stripe6)

        Dim away_Pants_Color As String = u.Away_Pants.Pants_Color
        If Not r.Contains(away_Pants_Color) Then r.Add(away_Pants_Color)

        Dim away_Stripe_Color_1 As String = u.Away_Pants.Stripe_Color_1
        If Not r.Contains(away_Stripe_Color_1) Then r.Add(away_Stripe_Color_1)

        Dim away_Stripe_Color_2 As String = u.Away_Pants.Stripe_Color_2
        If Not r.Contains(away_Stripe_Color_2) Then r.Add(away_Stripe_Color_2)

        Dim away_Stripe_Color_3 As String = u.Away_Pants.Stripe_Color_3
        If Not r.Contains(away_Stripe_Color_3) Then r.Add(away_Stripe_Color_3)

        Dim sock_color As String = u.Footwear.Socks_Color
        If Not r.Contains(sock_color) Then r.Add(sock_color)

        Dim cleat_color As String = u.Footwear.Cleats_Color
        If Not r.Contains(cleat_color) Then r.Add(cleat_color)

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
