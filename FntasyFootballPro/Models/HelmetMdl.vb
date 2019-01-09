Public Class HelmetMdl
    Property Helmet_Color As String = ""
    Property Helmet_Logo_Color As String = ""
    Property Helmet_Facemask_Color As String = ""

    Public Sub New(ByVal Helmet_Color As String,
                 ByVal Helmet_Logo_Color As String, ByVal Helmet_Facemask_Color As String)

        Me.Helmet_Color = Helmet_Color
        Me.Helmet_Logo_Color = Helmet_Logo_Color
        Me.Helmet_Facemask_Color = Helmet_Facemask_Color


    End Sub

End Class
