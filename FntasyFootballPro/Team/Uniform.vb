Public Class Uniform
    Property Helmet As Helmet
    Property Home_Jersey As Jersey
    Property Away_Jersey As Jersey
    Property Home_Pants As Pants
    Property Away_Pants As Pants
    Property Footwear As Footwear

    Public Sub New(ByVal Helmet As Helmet, ByVal Home_Jersey As Jersey,
        ByVal Away_Jersey As Jersey, ByVal Home_Pants As Pants,
        ByVal Away_Pants As Pants, ByVal Footwear As Footwear)

        Me.Helmet = Helmet
        Me.Home_Jersey = Home_Jersey
        Me.Away_Jersey = Away_Jersey
        Me.Home_Pants = Home_Pants
        Me.Away_Pants = Away_Pants
        Me.Footwear = Footwear

    End Sub
End Class
