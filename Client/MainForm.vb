Imports System
Imports System.Windows.Forms
Imports System.Drawing
Imports System.Drawing.Image
Imports OASIS.BIAS.V2
Imports System.IO
Imports System.Diagnostics


Public Class MainForm

    Dim client As New BIAS_v2Client()


    Private Sub btnClear_Enroll_Click(sender As Object, e As EventArgs) Handles ClearButton_Enroll.Click
        MessageBox.Show("Clear button was clicked")
        GivenNameTextBox_Enroll.Text = ""
        FamilyNameTextBox_Enroll.Text = ""
        SexComboBox_Enroll.SelectedIndex = -1
        CitizenshipComboBox_Enroll.SelectedIndex = -1
        DoBDateTimePicker_Enroll.Text = DateTime.Now
    End Sub

    Private Sub btnEnroll_Click(sender As Object, e As EventArgs) Handles EnrollButton_Enroll.Click
        Debug.Print("enroll button clicked")
        Dim request As New EnrollRequest()
        Dim response As New EnrollResponsePackage()
        Dim procOptn As New ProcessingOptionsType
        'Dim opt As New OptionType
        'opt.Key = "test"
        'opt.Value = "test"
        'procOptn.Add(opt)
        request.ProcessingOptions = procOptn

        '<request>
        '<p1>Karen</p1>
        '<ProcessingOptions></ProcessingOptions>  'example (in XML) of -null OptionType but -empty ProcessingOptions 
        '</request>

        Dim userInput As InformationType = New InformationType
        'userInput.GUID = Guid.NewGuid.ToString
        userInput.GivenName = GivenNameTextBox_Enroll.Text
        userInput.FamilyName = FamilyNameTextBox_Enroll.Text
        userInput.DateOfBirth = DoBDateTimePicker_Enroll.Text
        userInput.Sex = SexComboBox_Enroll.SelectedItem
        userInput.Citizenship = CitizenshipComboBox_Enroll.SelectedItem
        request.InputData = userInput

        'will have to take enrollment image..1-convert to byte array, load into enroll request,inoutdata.images
        'Dim img As OASIS.BIAS.V2.Image = New OASIS.BIAS.V2.Image
        'Dim byteArray As Byte() = Nothing
        'img.ImageData = subject01.gif  'convert to byte array
        'request.InputData.Images.Add(img)
        'request.InputData.Images.Item(0).ImageData() = Byte()
        'request.InputData.Images(0) = personImage

        Try
            response = client.Enroll(request)
        Catch ex As Exception
            MessageBox.Show("exception: " & ex.Message.ToString & "return value-") '& response.ResponseStatus.Return.ToString)
        End Try

        'If response.ResponseStatus.Return = 0 Then
        '    MessageBox.Show("success:" & response.ResponseStatus.Message)
        'Else
        '    MessageBox.Show("failed:" & response.ResponseStatus.Message)
        'End If

        ResultsTextBox_Enroll.Text = response.ResponseStatus.Message
        If response.ResponseStatus.Return = 0 Then
            NewIDTextBox_Enroll.Text = response.Identity.SubjectID
        Else
            NewIDTextBox_Enroll.Text = "N/A"
        End If


    End Sub

    Private Sub QueryCapabilities_Click(sender As Object, e As EventArgs) Handles QueryCapabilitiesButton_QueryCapabilities.Click
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
        'client = New BIAS_v2Client()
        'Dim identifyResponse As New IdentifyResponsePackage()
        ' Dim identifyRequest As New IdentifyRequest()
        ' i need to identify an image.  identifyRequest.inputData.binary = image
        'i need identifyRequest.inputData.processingOption = ??
        'identifyRequest.InputData.galleryID = ??
        'identifyRequest.InputData.maxlistsize = 1

        'identifyResponse = client.Identify(identifyRequest)
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


    Private Sub VerifyButton_Verify_Click(sender As Object, e As EventArgs) Handles VerifyButton_Verify.Click

        Debug.Print("verify button clicked")
        Dim request As New VerifyRequest()
        Dim response As New VerifyResponsePackage()
        Dim procOptn As New ProcessingOptionsType
        request.ProcessingOptions = procOptn
        request.GalleryID = "1"

        'A51DU0R6()
        Dim strg As BIASIdentity = New BIASIdentity
        strg.IdentityClaim = "A51DU0R6"

        Dim vUserInput As InformationType = New InformationType
        request.Identity = strg
        'request.Identity.IdentityClaim = SubjectIDTextbox_Verify.Text
        vUserInput.GivenName = GivenNameTextbox_Verify.Text
        vUserInput.FamilyName = FamilyNameTextbox_Verify.Text
        vUserInput.DateOfBirth = DoBDateTimePicker_Verify.Text
        vUserInput.Sex = SexComboBox_Verify.SelectedItem
        vUserInput.Citizenship = CitizenshipComboBox_Verify.SelectedItem

        'reference an image subject01.png
        'convert it to a byte array
        'load it into vUserInput.Images(0).imagedata

        Dim myImage As System.Drawing.Bitmap = New System.Drawing.Bitmap(My.Resources.subject01)
        Dim myImgByteArray As Byte() = Nothing
        Dim myImgMemStream As System.IO.MemoryStream = New System.IO.MemoryStream
        myImage.Save(myImgMemStream, System.Drawing.Imaging.ImageFormat.Jpeg)
        myImgByteArray = myImgMemStream.GetBuffer()

        vUserInput.Images = New InformationType.ImagesType
        Dim clientImg As OASIS.BIAS.V2.Image = New OASIS.BIAS.V2.Image
        clientImg.ImageData = myImgMemStream.GetBuffer()
        vUserInput.Images.Add(clientImg)

        request.InputData = vUserInput
        Try
            response = client.Verify(request)

        Catch ex As Exception
            MessageBox.Show("generic exception: " & ex.Message)
            MessageBox.Show("service exception: " & response.ResponseStatus.Return & response.ResponseStatus.Message)
        End Try

        'If response.ResponseStatus.Return = 0 Then
        '    MessageBox.Show("success")
        'Else
        '    MessageBox.Show("exception: ")

        'End If
    End Sub

    Private Sub ClearButton_Verify_Click(sender As Object, e As EventArgs) Handles ClearButton_Verify.Click
        MessageBox.Show("Clear button was clicked")
        SubjectIDTextbox_Verify.Text = ""
        GivenNameTextbox_Verify.Text = ""
        FamilyNameTextbox_Verify.Text = ""
        SexComboBox_Verify.SelectedIndex = -1
        CitizenshipComboBox_Verify.SelectedIndex = -1
        DoBDateTimePicker_Verify.Text = DateTime.Now
    End Sub

    

    

    


    
    
End Class