Imports log4net

Public Class Administration_Services
    Private Shared logger As ILog = LogManager.GetLogger("RollingFile")

    'This service is called when the user elects to create new potential name(s) by going into the
    'Administration function and entering a first and/or last name or selecting a name file to create
    'new potential names for new players.
    Public Function AddPlayerNames(ByVal FirstName As String, ByVal LastName As String, ByVal sFile As String) As Integer
        Dim i As Integer = 0
        Dim pnDAO As Player_NamesDAO = New Player_NamesDAO()
        Dim bFirst = True
        Dim srFileReader As System.IO.StreamReader = Nothing
        Dim sInputLine As String = ""
        Dim bFileOpen As Boolean = False
        Dim FirstLastName As String = ""

        logger.Info("Adding Player Names.")

        If Not CommonUtils.isBlank(FirstName) And FirstName.Trim.Length > 1 Then i += pnDAO.AddFirstName(CommonUtils.CapitalizeFirstLetter(FirstName.Trim))
        If Not CommonUtils.isBlank(LastName) And LastName.Trim.Length > 1 Then i += pnDAO.AddLastName(CommonUtils.CapitalizeFirstLetter(LastName.Trim))

        If Not CommonUtils.isBlank(sFile) Then
            srFileReader = System.IO.File.OpenText(sFile)
            bFileOpen = True
            sInputLine = srFileReader.ReadLine()
            If sInputLine = "{FirstNames}" Then
                FirstLastName = "F"
            ElseIf sInputLine = "{LastNames}" Then
                FirstLastName = "L"
            End If
            Do Until sInputLine Is Nothing
                sInputLine = srFileReader.ReadLine()
                If CommonUtils.isBlank(sInputLine) OrElse sInputLine.Trim.Length = 0 Then Continue Do
                Select Case FirstLastName
                    Case "F"
                        i += pnDAO.AddFirstName(CommonUtils.CapitalizeFirstLetter(sInputLine.Trim))
                    Case "L"
                        i += pnDAO.AddLastName(CommonUtils.CapitalizeFirstLetter(sInputLine.Trim))
                    Case Else
                        Throw New Exception("No First or Last Name header in player names file.")
                End Select
            Loop

        End If

        Return i

    End Function
    'This service is called to get the total number of potential first and last names for new players.
    Public Function getPlayerNameTotals() As Long()
        Dim r(2) As Long
        Dim pnDAO As Player_NamesDAO = New Player_NamesDAO()

        r(0) = pnDAO.getTotalFirstNames
        r(1) = pnDAO.getTotalLastNames

        Return r
    End Function


End Class
