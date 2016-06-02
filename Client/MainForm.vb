Imports System
Imports System.Windows.Forms
Imports System.Drawing
Imports OASIS.BIAS.V2



Public Class MainForm

    Dim client As BIAS_v2Client



    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        'ClientHelper.StartService()

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        MessageBox.Show("Clear button was clicked")
    End Sub

    Private Sub Button2Click(sender As Object, e As EventArgs) Handles Button2.Click
        MessageBox.Show("Enroll button was clicked")
        Dim request As New EnrollRequest
        request.InputData.


        Dim response As EnrollResponsePackage
        'response = client.Enroll(request)


    End Sub


    Private Sub Button7Click(sender As Object, e As EventArgs) Handles Button7.Click
        client = New BIAS_v2Client()
        Dim response1 As QueryCapabilitiesResponsePackage
        Dim queryCapabilitiesRequest As New QueryCapabilitiesRequest()

        'response1 = client.QueryCapabilities(queryCapabilitiesRequest)

        MessageBox.Show("QueryCapabilities button was clicked")

        Return


    End Sub

    
    
End Class