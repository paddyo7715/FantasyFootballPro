Imports System
Imports System.Drawing
Imports System.Drawing.Imaging
Imports System.IO
Imports System.Windows
Imports System.Windows.Interop
Imports System.Windows.Media.Imaging

Public Class Uniform_Image
    Private stock_image As Bitmap = Nothing
    Private Home_Uniform_image As Bitmap = Nothing
    Private Away_Uniform_Image As Bitmap = Nothing

    Public Sub New(ByVal stock_image_file_path As String)
        stock_image = New Bitmap(stock_image_file_path)

        Home_Uniform_image = stock_image.Clone
        Away_Uniform_Image = stock_image.Clone


    End Sub
    '    Public Function getHomeUniform_Image() As ImageSource
    Public Function getHomeUniform_Image() As BitmapImage

        Dim stream As MemoryStream = New MemoryStream()

        Home_Uniform_image.Save(stream, ImageFormat.Png) ' Was .Bmp, but this did Not show a transparent background.

        stream.Position = 0
        Dim result As BitmapImage = New BitmapImage()
        result.BeginInit()
        ' According to MSDN, "The default OnDemand cache option retains access to the stream until the image is needed."
        ' Force the bitmap to load right now so we can dispose the stream.
        result.CacheOption = BitmapCacheOption.OnLoad
        result.StreamSource = stream
        result.EndInit()
        result.Freeze()
        Return result

    End Function
    Public Function getAwayUniform_Image() As BitmapImage

        Dim stream As MemoryStream = New MemoryStream()

        Away_Uniform_Image.Save(stream, ImageFormat.Png) ' Was .Bmp, but this did Not show a transparent background.

        stream.Position = 0
        Dim result As BitmapImage = New BitmapImage()
        result.BeginInit()
        ' According to MSDN, "The default OnDemand cache option retains access to the stream until the image is needed."
        ' Force the bitmap to load right now so we can dispose the stream.
        result.CacheOption = BitmapCacheOption.OnLoad
        result.StreamSource = stream
        result.EndInit()
        result.Freeze()
        Return result

    End Function
    Public Sub Flip_All_Colors(ByVal bHome As Boolean,
                               ByVal helmet_color As Color, ByVal fasemask_color As Color,
                               ByVal hel_logo_color As Color, ByVal jersey_color As Color,
                               ByVal number_color As Color, ByVal num_outline_color As Color,
                               ByVal sleeve_color As Color, ByVal shoulder_stripe_color As Color,
                               ByVal sleeve_stripe_1_color As Color, ByVal sleeve_stripe_2_color As Color,
                               ByVal sleeve_stripe_3_color As Color, ByVal sleeve_stripe_4_color As Color,
                               ByVal sleeve_stripe_5_color As Color, ByVal sleeve_stripe_6_color As Color,
                               ByVal pants_color As Color, ByVal pants_stripe_1_color As Color,
                               ByVal pants_stripe_2_color As Color, ByVal pants_stripe_3_color As Color,
                               ByVal socks_color As Color, ByVal cleats_color As Color)

        Dim x As Integer
        Dim y As Integer
        Dim red As Byte
        Dim green As Byte
        Dim blue As Byte



        Dim img As Bitmap = Nothing

        If bHome Then
            img = Home_Uniform_image
        Else
            img = Away_Uniform_Image
        End If

        For x = 0 To stock_image.Width - 1
            For y = 0 To stock_image.Height - 1
                red = stock_image.GetPixel(x, y).R
                green = stock_image.GetPixel(x, y).G
                blue = stock_image.GetPixel(x, y).B

                Dim the_color As Color = Color.FromArgb(red, green, blue)

                If the_color.ToArgb = App_Constants.STOCK_HELMET_COLOR.ToArgb Then
                    img.SetPixel(x, y, helmet_color)
                End If

                If the_color.ToArgb = App_Constants.STOCK_FACEMASK.ToArgb Then
                    img.SetPixel(x, y, fasemask_color)
                End If

                If the_color.ToArgb = App_Constants.STOCK_HEL_LOGO_COLOR.ToArgb Then
                    img.SetPixel(x, y, hel_logo_color)
                End If

                If the_color.ToArgb = App_Constants.STOCK_JERSEY_COLOR.ToArgb Then
                    img.SetPixel(x, y, jersey_color)
                End If

                If the_color.ToArgb = App_Constants.STOCK_NUMBER_COLOR.ToArgb Then
                    img.SetPixel(x, y, number_color)
                End If

                If the_color.ToArgb = App_Constants.STOCK_NUM_OUTLINE_COLOR.ToArgb Then
                    img.SetPixel(x, y, num_outline_color)
                End If

                If the_color.ToArgb = App_Constants.STOCK_SLEEVE_COLOR.ToArgb Then
                    img.SetPixel(x, y, sleeve_color)
                End If

                If the_color.ToArgb = App_Constants.STOCK_SHOULDER_STRIPE_COLOR.ToArgb Then
                    img.SetPixel(x, y, shoulder_stripe_color)
                End If

                If the_color.ToArgb = App_Constants.STOCK_SLEEVE_STRIPE_1_COLOR.ToArgb Then
                    img.SetPixel(x, y, sleeve_stripe_1_color)
                End If

                If the_color.ToArgb = App_Constants.STOCK_SLEEVE_STRIPE_2_COLOR.ToArgb Then
                    img.SetPixel(x, y, sleeve_stripe_2_color)
                End If

                If the_color.ToArgb = App_Constants.STOCK_SLEEVE_STRIPE_3_COLOR.ToArgb Then
                    img.SetPixel(x, y, sleeve_stripe_3_color)
                End If

                If the_color.ToArgb = App_Constants.STOCK_SLEEVE_STRIPE_4_COLOR.ToArgb Then
                    img.SetPixel(x, y, sleeve_stripe_4_color)
                End If

                If the_color.ToArgb = App_Constants.STOCK_SLEEVE_STRIPE_5_COLOR.ToArgb Then
                    img.SetPixel(x, y, sleeve_stripe_5_color)
                End If

                If the_color.ToArgb = App_Constants.STOCK_SLEEVE_STRIPE_6_COLOR.ToArgb Then
                    img.SetPixel(x, y, sleeve_stripe_6_color)
                End If

                If the_color.ToArgb = App_Constants.STOCK_PANTS_COLOR.ToArgb Then
                    img.SetPixel(x, y, pants_color)
                End If

                If the_color.ToArgb = App_Constants.STOCK_PANTS_STRIPE_1_COLOR.ToArgb Then
                    img.SetPixel(x, y, pants_stripe_1_color)
                End If

                If the_color.ToArgb = App_Constants.STOCK_PANTS_STRIPE_2_COLOR.ToArgb Then
                    img.SetPixel(x, y, pants_stripe_2_color)
                End If

                If the_color.ToArgb = App_Constants.STOCK_PANTS_STRIPE_3_COLOR.ToArgb Then
                    img.SetPixel(x, y, pants_stripe_3_color)
                End If

                If the_color.ToArgb = App_Constants.STOCK_SOCKS_COLOR.ToArgb Then
                    img.SetPixel(x, y, socks_color)
                End If

                If the_color.ToArgb = App_Constants.STOCK_CLEATES_COLOR.ToArgb Then
                    img.SetPixel(x, y, cleats_color)
                End If

            Next
        Next

    End Sub
    Public Sub Flip_One_Color(ByVal bHome As Boolean, ByVal old_color As Color, ByVal new_color As Color)

        Dim x As Integer
        Dim y As Integer
        Dim red As Byte
        Dim green As Byte
        Dim blue As Byte

        Dim img As Bitmap = Nothing
        If bHome Then
            img = Home_Uniform_image
        Else
            img = Away_Uniform_Image
        End If

        For x = 0 To stock_image.Width - 1
            For y = 0 To stock_image.Height - 1
                red = stock_image.GetPixel(x, y).R
                green = stock_image.GetPixel(x, y).G
                blue = stock_image.GetPixel(x, y).B

                Dim the_color As Color = Color.FromArgb(red, green, blue)

                If the_color.ToArgb = old_color.ToArgb Then
                    img.SetPixel(x, y, new_color)
                End If

            Next
        Next

    End Sub

End Class
