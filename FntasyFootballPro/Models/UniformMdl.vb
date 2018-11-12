Public Class UniformMdl
    Property Helmet As HelmetMdl
    Property Home_Jersey As JerseyMdl
    Property Away_Jersey As JerseyMdl
    Property Home_Pants As PantsMdl
    Property Away_Pants As PantsMdl
    Property Footwear As FootwearMdl

    Public Sub New(ByVal Helmet As HelmetMdl, ByVal Home_Jersey As JerseyMdl,
        ByVal Away_Jersey As JerseyMdl, ByVal Home_Pants As PantsMdl,
        ByVal Away_Pants As PantsMdl, ByVal Footwear As FootwearMdl)

        Me.Helmet = Helmet
        Me.Home_Jersey = Home_Jersey
        Me.Away_Jersey = Away_Jersey
        Me.Home_Pants = Home_Pants
        Me.Away_Pants = Away_Pants
        Me.Footwear = Footwear

    End Sub
End Class
