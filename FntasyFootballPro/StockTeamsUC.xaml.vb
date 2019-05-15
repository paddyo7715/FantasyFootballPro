Imports System.Data

Public Class StockTeamsUC
    Private st_list As List(Of TeamMdl) = Nothing

    Public Event Show_MainMenu As EventHandler
    Public Event Show_NewStockTeam As EventHandler

    Public Sub New(ByVal st_list As List(Of TeamMdl))

        ' This call is required by the designer.
        InitializeComponent()

        Me.st_list = st_list

        setStockTeams()

    End Sub
    Private Sub setStockTeams()

        StockTeamsGrid.Items.Clear()

        For Each st In st_list

            Dim h_sp As StackPanel = New StackPanel
            h_sp.Orientation = Orientation.Horizontal

            Dim BitmapImage As BitmapImage = New BitmapImage(New Uri(CommonUtils.getAppPath & "/Images/Helmets/" & st.Helmet_img_path))
            Dim helmet_img As Image = New Image()
            helmet_img.Width = 50
            helmet_img.Height = 50
            helmet_img.Source = BitmapImage

            Dim Color_Percents_List As List(Of Uniform_Color_percents) = Nothing
            Color_Percents_List = Uniform.getColorList(st.Uniform)

            Dim team_label As Label = New Label()
            team_label.Foreground = New SolidColorBrush(CommonUtils.getColorfromHex(Color_Percents_List(0).color_string))
            team_label.Content = st.City & " " & st.Nickname
            team_label.Height = 50
            team_label.Width = 360
            team_label.VerticalContentAlignment = VerticalContentAlignment.Center

            If Color_Percents_List.Count > 2 Then
                Dim BackBrush As LinearGradientBrush = New LinearGradientBrush()
                BackBrush.StartPoint = New Point(0.5, 0)
                BackBrush.EndPoint = New Point(0.5, 1)

                Dim running_value As Single = 0
                For i As Integer = 1 To Color_Percents_List.Count - 1
                    BackBrush.GradientStops.Add(New GradientStop(
                    CommonUtils.getColorfromHex(Color_Percents_List(i).color_string), running_value))

                    running_value += Color_Percents_List(i).value

                    BackBrush.GradientStops.Add(New GradientStop(
                    CommonUtils.getColorfromHex(Color_Percents_List(i).color_string), running_value))
                Next
                team_label.Background = BackBrush
            Else
                team_label.Background = New SolidColorBrush(CommonUtils.getColorfromHex(Color_Percents_List(1).color_string))
            End If
            team_label.FontFamily = New FontFamily("Times New Roman")
            team_label.FontSize = 24


            Dim BitmapImageST As BitmapImage = New BitmapImage(New Uri(CommonUtils.getAppPath & "/Images/Stadiums/" & st.Stadium.Stadium_Img_Path))
            Dim std_img As Image = New Image()
            std_img.Width = 80
            std_img.Height = 50

            std_img.Source = BitmapImageST

            h_sp.Children.Add(helmet_img)
            h_sp.Children.Add(team_label)
            h_sp.Children.Add(std_img)
            h_sp.Margin = New System.Windows.Thickness(5)

            StockTeamsGrid.Items.Add(h_sp)
        Next

    End Sub
    Private Sub canstockT_Click(sender As Object, e As RoutedEventArgs) Handles canstockT.Click

        RaiseEvent Show_MainMenu(Me, New EventArgs)

    End Sub
    Private Sub AddstockT_Click(sender As Object, e As RoutedEventArgs) Handles AddstockT.Click

        RaiseEvent Show_NewStockTeam(Me, New EventArgs)

    End Sub

    Private Sub DelstockT_Click(sender As Object, e As RoutedEventArgs) Handles DelstockT.Click
        Dim sts As StockTeams_Services = Nothing
        Dim id As Integer = StockTeamsGrid.SelectedIndex
        Dim drow As DataRowView = Nothing

        If id <> -1 Then

            Dim response = MessageBox.Show("Do you really want to delete ", "Delete?",
                       MessageBoxButton.YesNo, MessageBoxImage.Exclamation)
            If (response = MessageBoxResult.Yes) Then
                Try
                    Mouse.OverrideCursor = Cursors.Wait
                    sts = New StockTeams_Services()
                    sts.DeleteStockTeam(id)
                    setStockTeams()
                    Mouse.OverrideCursor = Nothing
                Catch ex As Exception
                    Mouse.OverrideCursor = Nothing
                    MessageBox.Show("Error deleting stock team", "Error", MessageBoxButton.OK, MessageBoxImage.Error)
                End Try
            End If
        Else
            MessageBox.Show("You must first select a stock team to delete ", "",
                                MessageBoxButton.OK, MessageBoxImage.Information)
        End If


    End Sub
End Class
