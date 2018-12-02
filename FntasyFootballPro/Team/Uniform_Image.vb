

Imports System.Drawing

Public Class Uniform_Image
    Private stock_image As Bitmap = Nothing
    Private Home_Uniform_image As Bitmap = Nothing
    Private Away_Uniform_Image As Bitmap = Nothing

    Public Sub New(ByVal stock_image_file_path As String)
        stock_image = New Bitmap(stock_image_file_path)
        Home_Uniform_image = stock_image.Clone
        Away_Uniform_Image = stock_image.Clone
    End Sub
    Public Function getHomeUniform_Image() As ImageSource
        Dim c As ImageSourceConverter = New ImageSourceConverter()
        Return CType(c.ConvertFrom(Home_Uniform_image), ImageSource)
    End Function
    Public Function getAwayUniform_Image() As ImageSource
        Dim c As ImageSourceConverter = New ImageSourceConverter()
        Return CType(c.ConvertFrom(Away_Uniform_Image), ImageSource)
    End Function
End Class
