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
        Dim hBitmap As IntPtr = Away_Uniform_Image.GetHbitmap()
        Dim retval As BitmapImage = New BitmapImage()

        Dim bitmapSource = Imaging.CreateBitmapSourceFromHBitmap(hBitmap, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions())

        Dim encoder = New BmpBitmapEncoder()
        Dim memoryStream = New MemoryStream()

        encoder.Frames.Add(BitmapFrame.Create(bitmapSource))
        encoder.Save(memoryStream)

        retval.BeginInit()
        retval.StreamSource = New MemoryStream(memoryStream.ToArray())
        retval.EndInit()

        memoryStream.Close()

        Return retval
    End Function
    Public Sub GreyOutUniform(ByVal bHome As Boolean)

        Dim x As Integer
        Dim y As Integer
        Dim red As Byte
        Dim green As Byte
        Dim blue As Byte

        Dim grey_red As Byte = 40
        Dim grey_green As Byte = 40
        Dim grey_blue As Byte = 40
        Dim grey_color As Color = Color.FromArgb(grey_red, grey_green, grey_blue)

        Dim img As Bitmap = Nothing
        If bHome Then
            img = Home_Uniform_image
        Else
            img = Away_Uniform_Image
        End If

        For x = 0 To img.Width - 1
            For y = 0 To img.Height - 1
                red = img.GetPixel(x, y).R
                green = img.GetPixel(x, y).G
                blue = img.GetPixel(x, y).B

                If red = 128 And blue = 0 And green = 0 Then
                    img.SetPixel(x, y, grey_color)
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

        Dim grey_red As Byte = 40
        Dim grey_green As Byte = 40
        Dim grey_blue As Byte = 40
        Dim grey_color As Color = Color.FromArgb(grey_red, grey_green, grey_blue)

        Dim img As Bitmap = Nothing
        If bHome Then
            img = Home_Uniform_image
        Else
            img = Away_Uniform_Image
        End If

        For x = 0 To img.Width - 1
            For y = 0 To img.Height - 1
                red = img.GetPixel(x, y).R
                green = img.GetPixel(x, y).G
                blue = img.GetPixel(x, y).B

                If red = 128 And blue = 0 And green = 0 Then
                    img.SetPixel(x, y, grey_color)
                End If
            Next
        Next

    End Sub

End Class
