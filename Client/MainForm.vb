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
        'opt.Key = "complete"
        'opt.Value = "partial"
        'procOptn.Add(opt)
        request.ProcessingOptions = procOptn

        '<request>
        '<p1>Karen</p1>
        '<ProcessingOptions></ProcessingOptions>  'example (in XML) of -null OptionType but -empty ProcessingOptions 
        '</request>

        Dim userbiographicalInput As InformationType = New InformationType
        'userInput.GUID = Guid.NewGuid.ToString
        userbiographicalInput.GivenName = GivenNameTextBox_Enroll.Text
        userbiographicalInput.FamilyName = FamilyNameTextBox_Enroll.Text
        userbiographicalInput.DateOfBirth = DoBDateTimePicker_Enroll.Text
        userbiographicalInput.Sex = SexComboBox_Enroll.SelectedItem
        userbiographicalInput.Citizenship = CitizenshipComboBox_Enroll.SelectedItem

        Dim userBiomRecord As New CBEFF_BIR_Type
        'Create image
        userBiomRecord.BIR = New BaseBIRType
        Dim testImage As System.Drawing.Image = System.Drawing.Image.FromFile("C:\Users\karenm.XE\Documents\BIAS_Client\BIAS-2.0\Client\Resources\subject01.gif")
        userBiomRecord.BIR.biometricImage = testImage 'will soon come from a webcam
        userBiomRecord.BIR.biometricImageType = "Front"


        '*********************************
        'Dim img1 As New OASIS.BIAS.V2.Image 'add this to list(of T) - infomationType.ImagesType
        ''convert the image to byte array
        'Dim bmpImg As System.Drawing.Bitmap = My.Resources.subject01
        'Dim ms As New MemoryStream
        'Dim byteArray As Byte()
        ''save image to memorystream, then to bytearray
        'bmpImg.Save(ms, bmpImg.RawFormat)
        'byteArray = ms.ToArray
        'img1.ImageData = byteArray
        ''img1.BridgeWidth = "bridgWidth"
        ''img1.ContentType = "gif"
        ''img1.EyebrowDistance
        'Dim ImgsList As New InformationType.ImagesType
        'ImgsList.Add(img1)
        'userbiographicalInput.Images = ImgsList
        '*************

        Dim userIdentity As New BIASIdentity
        userIdentity.BiometricData = New BIASBiometricDataType
        userIdentity.BiometricData.BIRList = New CBEFF_BIR_ListType

        'Create BIR_Info
        Dim userBIR As New BIRInfoType
        userBIR.Creator = "userTestCreator"
        userBIR.Index = "userTestIndex"
        userBIR.Integrity = True
        Dim payArray = New Byte() {0}
        userBIR.Payload = payArray
        Dim currentDate1 As Date = Date.Now
        userBIR.CreationDate = currentDate1
        userBIR.NotValidBefore = currentDate1
        userBIR.NotValidAfter = currentDate1

        'Create BDB_Info
        Dim userBDB As New BDBInfoType
        'sampleBDB.ChallengeResponseField = 
        userBDB.Index = "userTestIndex"
        Dim userTestFormat = New RegistryIDType
        userTestFormat.Organization = "userTestOrganization"
        userTestFormat.Type = "userTestType"
        userBDB.Format = userTestFormat
        userBDB.Encryption = False
        Dim currentDate2 As Date = Date.Now
        userBDB.CreationDate = currentDate2
        userBDB.NotValidBefore = currentDate2
        userBDB.NotValidAfter = currentDate2
        'sampleBDB.Type = 
        'sampleBDB.Subtype = 
        userBDB.Level = 0
        userBDB.Product = userTestFormat
        userBDB.CaptureDevice = userTestFormat
        userBDB.FeatureExtractionAlgorithm = userTestFormat
        userBDB.ComparisonAlgorithm = userTestFormat
        userBDB.CompressionAlgorithm = userTestFormat
        userBDB.Purpose = 2
        'sampleBDB.Quality =

        'Create SB_Info
        Dim userSB As New SBInfoType
        userSB.Format = userTestFormat

        userBiomRecord.BIR_Information = New CBEFF_BIR_Type.BIR_InformationType
        userBiomRecord.BIR_Information.BIR_Info = userBIR
        userBiomRecord.BIR_Information.BDB_Info = userBDB
        userBiomRecord.BIR_Information.SB_Info = userSB

        userBiomRecord.FormatOwner = 1
        userBiomRecord.FormatType = 1
        userIdentity.BiometricData.BIRList.Add(userBiomRecord)

        request.InputData = userbiographicalInput
        request.Identity = userIdentity
        response = client.Enroll(request)

        'Try
        '    response = client.Enroll(request)
        'Catch ex As Exception
        '    MessageBox.Show("exception: " & ex.Message.ToString & "return value-" & response.ResponseStatus.Return.ToString)
        'End Try

        If response.ResponseStatus.Return = 0 Then
            MessageBox.Show("success:" & response.ResponseStatus.Message)
        Else
            MessageBox.Show("failed:" & response.ResponseStatus.Message)
        End If

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

        Dim procOptn As New ProcessingOptionsType
        Dim newOption As New OptionType
        newOption.Key = "biographic"
        newOption.Value = "full"
        procOptn.Add(newOption)
        getDataRequest.ProcessingOptions = procOptn

        getDataRequest.Identity = New BIASIdentity()
        getDataRequest.Identity.SubjectID = txtbxGUID_RetrieveInformation.Text


        'A51DU0R6
        Try
            getDataResponse = client.RetrieveData(getDataRequest)
        Catch ex As Exception
            Console.WriteLine("error: " & ex.Message)
        End Try
        MessageBox.Show("RetrieveData ResponseStatus-" & getDataResponse.ResponseStatus.Return & vbTab & getDataResponse.ResponseStatus.Message)

        'txtbxGiven_RetrieveInformation.Text = getDataResponse.ReturnData.GivenName
        'txtbxFamily_RetrieveInformation.Text = getDataResponse.ReturnData.FamilyName
        'txtbxDOB_RetrieveInformation.Text = getDataResponse.ReturnData.DateOfBirth
        'txtbxSex_RetrieveInformation.Text = getDataResponse.ReturnData.Sex
        'txtbxCitizenship_RetrieveInformation.Text = getDataResponse.ReturnData.Citizenship


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
        'myImage.Save(myImgMemStream, System.Drawing.Imaging.ImageFormat.Jpeg)
        myImage.Save(myImgMemStream, myImage.RawFormat)
        'myImgByteArray = myImgMemStream.GetBuffer()
        myImgByteArray = myImgMemStream.ToArray


        vUserInput.Images = New InformationType.ImagesType
        Dim clientImg As OASIS.BIAS.V2.Image = New OASIS.BIAS.V2.Image
        'clientImg.ImageData = myImgMemStream.GetBuffer()
        clientImg.ImageData = myImgMemStream.ToArray
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