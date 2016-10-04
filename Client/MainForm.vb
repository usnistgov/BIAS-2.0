Imports System
Imports System.Windows.Forms
Imports System.Drawing
Imports System.Drawing.Image
Imports OASIS.BIAS.V2




Public Class MainForm

    Dim client As BIAS_v2Client

    Private Sub btnClear_Enroll_Click(sender As Object, e As EventArgs) Handles btnClear_Enroll.Click
        MessageBox.Show("Clear button was clicked")
    End Sub

    Private Sub btnEnroll_Click(sender As Object, e As EventArgs) Handles btnEnroll_Enroll.Click
        MessageBox.Show("Enroll button was clicked")


        Dim request As New EnrollRequest()
        Dim params As New GenericRequestParameters()
        params.Application = "clientApp1"
        params.ApplicationUser = "user1"
        params.BIASOperationName = "enrol"
        'request.GenericRequestParameters = New GenericRequestParameters()
        request.GenericRequestParameters = params

        Dim procOptn As New ProcessingOptionsType
        Dim opt As New OptionType
        opt.Key = "null"
        opt.Value = "null"
        procOptn.Add(opt)
        request.ProcessingOptions = procOptn

        request.Identity.SubjectID = txtbxGUID_Enroll.Text
        request.Identity.BiographicData.FirstName = txtbxGiven_Enroll.Text
        request.Identity.BiographicData.LastName = txtbxFamily_Enroll.Text

        With request.Identity.BiographicData.BiographicDataItemList
            .Insert(0, New BiographicDataItemType())
            .Item(0).Name = "DOB"
            .Item(0).Type = DateString
            .Item(0).Value = DateOfBirthPicker_Enroll.Text
        End With


        'request.InputData = New InformationType()
        'request.InputData.GUID = txtbxGUID_Enroll.Text
        'request.InputData.GivenName = txtbxGiven_Enroll.Text
        'request.InputData.FamilyName = txtbxFamily_Enroll.Text
        'request.InputData.DateOfBirth = DateOfBirthPicker_Enroll.Text
        'request.InputData.Sex = txtbxSex_Enroll.Text
        'request.InputData.Citizenship = txtbxCitizenship_Enroll.Text

        Dim response As EnrollResponsePackage
        'response = client.Enroll(request)
        'Console.WriteLine(response.ResponseStatus.Return.ToString) 'did the enrol succeed or fail

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