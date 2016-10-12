Imports System
Imports System.Windows.Forms
Imports System.Drawing
Imports System.Drawing.Image
Imports OASIS.BIAS.V2




Public Class MainForm

    Dim client As New BIAS_v2Client()

    Private Sub btnClear_Enroll_Click(sender As Object, e As EventArgs) Handles ClearButton_Enroll.Click
        MessageBox.Show("Clear button was clicked")
    End Sub

    Private Sub btnEnroll_Click(sender As Object, e As EventArgs) Handles EnrollButton_Enroll.Click
        'MessageBox.Show("Enroll button was clicked")
        Dim request As New EnrollRequest()
        Dim response As New EnrollResponsePackage()
        'Dim genericReq As New GenericRequestParameters

        Dim procOptn As New ProcessingOptionsType
        Dim opt As New OptionType
        opt.Key = "test"
        opt.Value = "test"
        procOptn.Add(opt)
        request.ProcessingOptions = procOptn


        Dim userInput As InformationType = New InformationType
        userInput.GUID = Guid.NewGuid.ToString
        userInput.GivenName = GivenNameTextBox_Enroll.Text
        userInput.FamilyName = FamilyNameTextBox_Enroll.Text
        userInput.DateOfBirth = DOBDateTimePicker_Enroll.Text
        userInput.Sex = SexTextBox_Enroll.Text
        userInput.Citizenship = CitizenshipTextBox_Enroll.Text
        request.InputData = userInput

        'Dim s As String
        's = "f30fab8d-8b0a-47fb-ba72-d088e93ca414"
        'request.InputData.GivenName = GivenNameTextBox_Enroll.Text
        'request.InputData.FamilyName = FamilyNameTextBox_Enroll.Text
        'request.InputData.DateOfBirth = DOBDateTimePicker_Enroll.Text
        'request.InputData.Sex = SexTextBox_Enroll.Text
        'request.InputData.Citizenship = CitizenshipTextBox_Enroll.Text

        'Dim img As OASIS.BIAS.V2.Image = New OASIS.BIAS.V2.Image
        'Dim byteArray As Byte() = Nothing

        'img.ImageData = subject01.gif  'convert to byte array
        'request.InputData.Images.Add(img)

        'request.InputData.Images.Item(0).ImageData() = Byte()
        'request.InputData.Images(0) = personImage

        'Dim response As New EnrollResponsePackage()
        If (request IsNot Nothing) Then
            Console.WriteLine("object is not null")
        End If
        Try

            response = client.Enroll(request)

        Catch ex As Exception
            MessageBox.Show("exception: " & ex.Message.ToString & "return value-")'& response.ResponseStatus.Return.ToString)
        End Try
        

    End Sub

    Private Sub QueryCapabilities_Click(sender As Object, e As EventArgs) Handles QueryCapabilitiesBtn.Click
        client = New BIAS_v2Client()
        Dim queryCapabilitiesResponse As QueryCapabilitiesResponsePackage
        Dim queryCapabilitiesRequest As New QueryCapabilitiesRequest()
        'MessageBox.Show("QueryCapabilities button was clicked")
        lstbx_CapabilitiesList.Items.Clear()

        queryCapabilitiesResponse = client.QueryCapabilities(queryCapabilitiesRequest)
        For Each ob As Object In queryCapabilitiesResponse.CapabilityList
            lstbx_CapabilitiesList.Items.Add(ob)
        Next
        Return
    End Sub

    Private Sub updateCapabilityAttributes(ByVal aCapabilty As Object)
        txtbxName_QueryCap.Text = aCapabilty.CapabilityName.ToString
        txtbxId_QueryCap.Text = aCapabilty.CapabilityID.ToString
        txtbxDesc_QueryCap.Text = aCapabilty.CapabilityDescription.ToString
        'txtbxValue_QueryCap.Text = aCapabilty.CapabilityValue.ToString
        'txtbxSupportingValue_QueryCap.Text = aCapabilty.CapabilitySupportingValue.ToString
        'txtbxAdditionalInfo_QueryCap.Text = aCapabilty.CapabilityAdditionalInfo.ToString
    End Sub

    Private Sub CapabilityClicked_MouseClick(sender As Object, e As MouseEventArgs) Handles lstbx_CapabilitiesList.MouseClick
        'MessageBox.Show("mouse click in capabilities text box")
        If lstbx_CapabilitiesList.SelectedIndex = -1 Then
            Return
        End If
        updateCapabilityAttributes(lstbx_CapabilitiesList.SelectedItem)
    End Sub

    Private Sub lstbx_CapabilitiesList_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstbx_CapabilitiesList.SelectedIndexChanged
        If lstbx_CapabilitiesList.SelectedIndex = -1 Then
            Return
        End If
        updateCapabilityAttributes(lstbx_CapabilitiesList.SelectedItem)
    End Sub

    Private Sub BioImageTxtBx_MouseClick(sender As Object, e As EventArgs) Handles txtbxBioImage_Enroll.MouseClick
        Dim OpenEnrolImage As New OpenFileDialog()

        OpenEnrolImage.InitialDirectory = "C:/Temp/Samples"
        OpenEnrolImage.Filter = "All files (*.*)|*.*"
        OpenEnrolImage.RestoreDirectory = True
        'OpenEnrolImage.FilterIndex = 1
        If OpenEnrolImage.ShowDialog() = Windows.Forms.DialogResult.OK Then
            Try
                txtbxBioImage_Enroll.Text = OpenEnrolImage.FileName.ToString
                picbx1_Enroll.Image = Drawing.Image.FromFile(OpenEnrolImage.FileName.ToString)
            Catch ex As Exception
                MessageBox.Show("Error opening Enrol Biometric image")

            End Try
        End If



        'Dim imageString As String = "c:/temp/IMG_5306.JPG"
        'PictureBox1.Image = Drawing.Image.FromFile(imageString)
        'TextBox7.Text = imageString
    End Sub


    Private Sub btnIdentify_Identify_Click(sender As Object, e As EventArgs) Handles btnIdentify_Identify.Click
        client = New BIAS_v2Client()
        Dim identifyResponse As New IdentifyResponsePackage()
        Dim identifyRequest As New IdentifyRequest()
        ' i need to identify an image.  identifyRequest.inputData.binary = image
        'i need identifyRequest.inputData.processingOption = ??
        'identifyRequest.InputData.galleryID = ??
        'identifyRequest.InputData.maxlistsize = 1

        identifyResponse = client.Identify(identifyRequest)
        'identifyResponse.ReturnData.

    End Sub

    
    Private Sub btnRetrieveInformation_RetrieveInformation_Click(sender As Object, e As EventArgs) Handles btnRetrieveInformation_RetrieveInformation.Click
        Dim client As New BIAS_v2Client()
        Dim getDataRequest As New RetrieveDataRequest()
        Dim getDataResponse As New RetrieveDataResponsePackage()
        Dim first As String
        first = "01"


        getDataRequest.Identity.SubjectID = first
        getDataResponse = client.RetrieveData(getDataRequest)

        txtbxGiven_RetrieveInformation.Text = getDataResponse.ReturnData.GivenName
        txtbxFamily_RetrieveInformation.Text = getDataResponse.ReturnData.FamilyName
        txtbxDOB_RetrieveInformation.Text = getDataResponse.ReturnData.DateOfBirth
        txtbxSex_RetrieveInformation.Text = getDataResponse.ReturnData.Sex
        txtbxCitizenship_RetrieveInformation.Text = getDataResponse.ReturnData.Citizenship





    End Sub
End Class