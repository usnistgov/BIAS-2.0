Imports System
Imports System.Windows.Forms
Imports System.Drawing
Imports System.Drawing.Image
Imports OASIS.BIAS.V2
Imports System.IO
Imports System.Diagnostics
Imports Nist.Bcl.Wsbd.Streaming
Imports System.Drawing.Imaging


Public Class MainForm

    Dim client As New BIAS_v2Client()

    'Dim m_ActiveCamera As WebCamera = New WebCamera("Microsoft LifeCam Cinema", picbx1_Enroll)
    'Dim m_ActiveCamera As WebCamera

    Private Sub MainForm_Load(sender As Object, e As EventArgs) Handles Me.Load
        'm_ActiveCamera = New WebCamera("Microsoft LifeCam Cinema", picbx1_Enroll) 'what if not on enrol but on identify tab? send feed to identify picbox?
        'Dim WebCameraPool As StreamPool
        'WebCameraPool = New StreamPool(m_ActiveCamera)
        'm_ActiveCamera.RegisterTargetPool(WebCameraPool)
    End Sub

    Private Sub btnClear_Enroll_Click(sender As Object, e As EventArgs) Handles ClearButton_Enroll.Click
        MessageBox.Show("Clear button was clicked")
        GivenNameTextBox_Enroll.Text = ""
        FamilyNameTextBox_Enroll.Text = ""
        SexComboBox_Enroll.SelectedIndex = -1
        CitizenshipComboBox_Enroll.SelectedIndex = -1
        DoBDateTimePicker_Enroll.Text = DateTime.Now


    End Sub

    Dim imgFrmfile As System.Drawing.Image
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

        'Create image
        userBiomRecord.BIR = New BaseBIRType
        'Dim testImage As System.Drawing.Image = System.Drawing.Image.FromFile("C:\Users\karenm.XE\Documents\BIAS_Client\BIAS-2.0\Client\Resources\subject01.gif")
        'Dim imgString As String = ImageToBase64String(testImage, ImageFormat.Gif)
        Dim enrolImg As System.Drawing.Image
        Dim webImage As System.Drawing.Image = webFrm.SnapshotImg
        If webImage IsNot Nothing Then
            enrolImg = webImage
        ElseIf imgFrmfile IsNot Nothing Then
            enrolImg = imgFrmfile
        Else
            MessageBox.Show("Pls provide an image to enroll")
            Return
        End If
        Dim imgString As String = ImageToBase64String(enrolImg, ImageFormat.Gif)
        userBiomRecord.BIR.biometricImage = imgString       'will soon come from a webcam
        userBiomRecord.BIR.biometricImageType = "Front"

        userBiomRecord.FormatOwner = 1
        userBiomRecord.FormatType = 1
        userIdentity.BiometricData.BIRList.Add(userBiomRecord)

        request.InputData = userbiographicalInput
        request.Identity = userIdentity

        Try
            response = client.Enroll(request)
        Catch ex As Exception
            MessageBox.Show("exception: " & ex.Message.ToString & "return value: " & response.ResponseStatus.Return.ToString)
        End Try

        'If response.ResponseStatus.Return = 0 Then
        '    MessageBox.Show("success:" & response.ResponseStatus.Message)
        'Else
        '    MessageBox.Show("failed:" & response.ResponseStatus.Message)
        'End If

        ResultsTextBox_Enroll.Text = response.ResponseStatus.Message
        If response.ResponseStatus.Return = 0 Then
            NewIDTextBox_Enroll.Text = response.Identity.SubjectID

            'use method instead
            'EnrolCompletePictureBox_enrol.Image = Base64StringToImage(response.Identity.BiometricData.BIRList.Item(0).BIR.biometricImage)
        Else
            NewIDTextBox_Enroll.Text = "N/A"
        End If


    End Sub

    'take in a string return an image
    Private Function Base64StringToImage(ByVal strdata As String) As System.Drawing.Image
        'Dim responseString As String = strdata
        Dim byteArray() As Byte = Convert.FromBase64String(strdata)
        Dim MS As MemoryStream = New MemoryStream
        MS = New MemoryStream(byteArray)
        Dim result As System.Drawing.Image
        result = System.Drawing.Image.FromStream(MS)

        Return result
    End Function


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

        'this code is for choosing an image from a local directory. keep off while acquiring a snapshot from a webcam feed.

        Dim OpenEnrolImage As New OpenFileDialog()
        OpenEnrolImage.InitialDirectory = "C:/Temp/Samples"
        OpenEnrolImage.Filter = "All files (*.*)|*.*"
        OpenEnrolImage.RestoreDirectory = True
        'OpenEnrolImage.FilterIndex = 1
        If OpenEnrolImage.ShowDialog() = Windows.Forms.DialogResult.OK Then
            Try
                txtbxBioImage_Enroll.Text = OpenEnrolImage.FileName.ToString
                picbx1_Enroll.Image = Drawing.Image.FromFile(OpenEnrolImage.FileName.ToString)
                imgFrmfile = Drawing.Image.FromFile(OpenEnrolImage.FileName.ToString) 'should this be set back to nothing somewhere? or made into a property?
            Catch ex As Exception
                MessageBox.Show("Error opening Enrol Biometric image")

            End Try
        End If

        'Dim imageString As String = "c:/temp/IMG_5306.JPG"
        'PictureBox1.Image = Drawing.Image.FromFile(imageString)
        'TextBox7.Text = imageString
    End Sub


    Private Sub btnIdentify_Identify_Click(sender As Object, e As EventArgs) Handles btnIdentify_Identify.Click
        Debug.Print("identify button clicked")
        Dim Identifyrequest As New IdentifyRequest()
        Dim Identifyresponse As New IdentifyResponsePackage()
        Identifyrequest.GalleryID = "1"
        Identifyrequest.MaxListSize = 1

        Dim iDprocOptn As New ProcessingOptionsType
        'Dim opt As New OptionType
        'opt.Key = "complete"
        'opt.Value = "partial"
        'procOptn.Add(opt)
        Identifyrequest.ProcessingOptions = iDprocOptn

        Identifyrequest.InputData = New OASIS.BIAS.V2.InformationType
        Identifyrequest.InputData.Images = New OASIS.BIAS.V2.InformationType.ImagesType

        Dim identifyImg As New OASIS.BIAS.V2.Image 'this is needed in order to get to load data into imagedata field.
        Identifyrequest.InputData.Images.Add(identifyImg)


        Dim mybytearray As Byte()
        If iDimgFrmfile IsNot Nothing Then
            Dim ms As System.IO.MemoryStream = New MemoryStream
            iDimgFrmfile.Save(ms, System.Drawing.Imaging.ImageFormat.Gif)
            mybytearray = ms.ToArray()
            Identifyrequest.InputData.Images(0).ImageData = mybytearray
        End If
        Identifyresponse = client.Identify(Identifyrequest)
        Try
            Identifyresponse = client.Identify(Identifyrequest)
        Catch ex As Exception
            MessageBox.Show("error making identify request call to server. exception msg: " & ex.Message)
        End Try
        MessageBox.Show("ResponseStatus: " & Identifyresponse.ResponseStatus.Message)

        'returned should be candidateList-candidateListType
        'CandidateListType - is a list Of CandidateType items-each consists of a BIASIdentity, rank, score, and biographicData-biographicDataType

        For Each ob As Object In Identifyresponse.CandidateList   'this not implemented yet on the service side.
            lstbx_CapabilitiesList.Items.Add(ob)
        Next
        'make it so when user click on one it shows in picbox

    End Sub


    Private Sub btnRetrieveInformation_RetrieveInformation_Click(sender As Object, e As EventArgs) Handles btnRetrieveInformation_RetrieveInformation.Click
        Dim client As New BIAS_v2Client()
        Dim getDataRequest As New RetrieveDataRequest()
        Dim getDataResponse As New RetrieveDataResponsePackage()

        Dim procOptn As New ProcessingOptionsType
        Dim newOption As New OptionType
        newOption.Key = "biographicData" 'choices: biometricData+images or full, allData+basic or full, biographicData+basic or full
        newOption.Value = "full"
        'newOption.Key = "biometricData"
        'newOption.Value = "image"
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

        txtbxGiven_RetrieveInformation.Text = getDataResponse.ReturnData.GivenName
        txtbxFamily_RetrieveInformation.Text = getDataResponse.ReturnData.FamilyName
        txtbxDOB_RetrieveInformation.Text = getDataResponse.ReturnData.DateOfBirth
        txtbxSex_RetrieveInformation.Text = getDataResponse.ReturnData.Sex
        txtbxCitizenship_RetrieveInformation.Text = getDataResponse.ReturnData.Citizenship


        'Waiting for Paul on this....
        'below will be attempted when biometric key/value is implemented. 
        'i converted a byte array (in response) to an image for display in the client window.

        'Dim resultImg As System.Drawing.Image   1)
        'If getDataResponse.ReturnData.Images.Count = 1 Then ======disregard this line.
        'resultImg = Base64StringToImage(getDataResponse.ReturnData.Images(0).ImageData) ======disregard this line.
        'Dim ms As System.IO.MemoryStream = New System.IO.MemoryStream(getDataResponse.ReturnData.Images(0).ImageData) 'not working yet. Paul is working on implementing this. 2)
        'resultImg = System.Drawing.Image.FromStream(ms)   3)

        'this might be deleted.
        'If newOption.Key Is "biometricData" Then
        'picboxResult_RetrieveInformation.Image = resultImg
        'End If


        'note imagedata is returned as 1 dimentional byte array. i will have to convert it to base64string -then to an image (in our function)
        'or i will have to convert the byte array to an image. (than load to picboxResult )

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

    'Converts a biometric image to base64string
    Private Function ImageToBase64String(ByVal image As System.Drawing.Image, ByVal imageFormat As ImageFormat)
        Using memStream As New MemoryStream
            image.Save(memStream, imageFormat)
            Dim result As String = Convert.ToBase64String(memStream.ToArray())
            memStream.Close()
            Return result
        End Using
    End Function


    Dim webFrm As New webCamForm
    Private Sub webCamPictureButton_Click(sender As Object, e As EventArgs) Handles webCamPictureButton.Click
        webFrm.ShowDialog()
        picbx1_Enroll.Image = webFrm.SnapshotImg
    End Sub

    Dim iDimgFrmfile As System.Drawing.Image
    Private Sub txtbxBioImage_Identify_MouseClick(sender As Object, e As EventArgs) Handles txtbxBioImage_Identify.MouseClick
        Dim OpenIdentifyImage As New OpenFileDialog()
        OpenIdentifyImage.InitialDirectory = "C:/Temp/Samples"
        OpenIdentifyImage.Filter = "All files (*.*)|*.*"
        OpenIdentifyImage.RestoreDirectory = True

        If OpenIdentifyImage.ShowDialog() = Windows.Forms.DialogResult.OK Then
            Try
                txtbxBioImage_Identify.Text = OpenIdentifyImage.FileName.ToString
                picbx1_Identify.Image = Drawing.Image.FromFile(OpenIdentifyImage.FileName.ToString)
                iDimgFrmfile = Drawing.Image.FromFile(OpenIdentifyImage.FileName.ToString) 'should this be set back to nothing somewhere? or made into a property?

            Catch ex As Exception
                MessageBox.Show("Error opening Identify Biometric image")
            End Try

        End If
    End Sub

    

End Class