Imports System.Web
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System
Imports System.IO
Imports System.ComponentModel
Imports Service.OASIS.BIAS.V2

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
' <System.Web.Script.Services.ScriptService()> _
<System.Web.Services.WebService(Namespace:="http://docs.oasis-open.org/bias/ns/bias-2.0/", _
   Description:="BIAS Tester Service")> _
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<ToolboxItem(False)> _
Public Class BIAS
    Inherits System.Web.Services.WebService

    Implements BIAS_v2
    <WebMethod()> _
    Public Function HelloWorld(GreetingMessage As String)
        Return GreetingMessage
    End Function

    <WebMethod()> _
    Public Function AddSubjectToGallery(AddSubjectToGalleryRequest As AddSubjectToGalleryRequest) As AddSubjectToGalleryResponsePackage Implements BIAS_v2.AddSubjectToGallery
        Dim galleryResponse As New AddSubjectToGalleryResponsePackage()
        Return galleryResponse
    End Function

    <WebMethod()> _
    Public Function CheckQuality(CheckQualityRequest As CheckQualityRequest) As CheckQualityResponsePackage Implements BIAS_v2.CheckQuality
        Dim qualityResponse As New CheckQualityResponsePackage()
        Return qualityResponse
    End Function

    <WebMethod()> _
    Public Function ClassifyBiometricData(ClassifyBiometricDataRequest As ClassifyBiometricDataRequest) As ClassifyBiometricDataResponsePackage Implements BIAS_v2.ClassifyBiometricData
        Dim classifyBioDataReponse As New ClassifyBiometricDataResponsePackage()
        Return classifyBioDataReponse
    End Function

    <WebMethod()> _
    Public Function CreateSubject(CreateSubjectRequest As CreateSubjectRequest) As CreateSubjectResponsePackage Implements BIAS_v2.CreateSubject
        Dim createSubjectResponse As New CreateSubjectResponsePackage()
        Return createSubjectResponse
    End Function

    <WebMethod()> _
    Public Function DeleteBiographicData(DeleteBiographicDataRequest As DeleteBiographicDataRequest) As DeleteBiographicDataResponsePackage Implements BIAS_v2.DeleteBiographicData
        Dim deleteBiogDataResponse As New DeleteBiographicDataResponsePackage()
        Return deleteBiogDataResponse
    End Function
    <WebMethod()> _
    Public Function DeleteBiometricData(DeleteBiometricDataRequest As DeleteBiometricDataRequest) As DeleteBiometricDataResponsePackage Implements BIAS_v2.DeleteBiometricData
        Dim deleteBiomDataResponse As New DeleteBiometricDataResponsePackage()
        Return deleteBiomDataResponse
    End Function
    <WebMethod()> _
    Public Function DeleteSubject(DeleteSubjectRequest As DeleteSubjectRequest) As DeleteSubjectResponsePackage Implements BIAS_v2.DeleteSubject
        Dim deleteSubjectResponse As New DeleteSubjectResponsePackage()
        Return deleteSubjectResponse
    End Function
    <WebMethod()> _
    Public Function DeleteSubjectFromGallery(DeleteSubjectFromGalleryRequest As DeleteSubjectFromGalleryRequest) As DeleteSubjectFromGalleryResponsePackage Implements BIAS_v2.DeleteSubjectFromGallery
        Dim deleteSubjectGalleryResponse As New DeleteSubjectFromGalleryResponsePackage()
        Return deleteSubjectGalleryResponse
    End Function
    <WebMethod()> _
    Public Function Enroll(EnrollRequest As EnrollRequest) As EnrollResponsePackage Implements BIAS_v2.Enroll
        Dim enrollResponse As New EnrollResponsePackage()
        Return enrollResponse
    End Function
    <WebMethod()> _
    Public Function GetEnrollResults(GetEnrollResultsRequest As GetEnrollResultsRequest) As GetEnrollResultsResponsePackage Implements BIAS_v2.GetEnrollResults
        Dim enrollResultsResponse As New GetEnrollResultsResponsePackage()
        Return enrollResultsResponse
    End Function
    <WebMethod()> _
    Public Function GetIdentifyResults(GetIdentifyResultsRequest As GetIdentifyResultsRequest) As GetIdentifyResultsResponsePackage Implements BIAS_v2.GetIdentifyResults
        Dim identityResultsResponse As New GetIdentifyResultsResponsePackage()
        Return identityResultsResponse
    End Function
    <WebMethod()> _
    Public Function GetIdentifySubjectResults(GetIdentifySubjectResultsRequest As GetIdentifySubjectResultsRequest) As GetIdentifySubjectResultsResponsePackage Implements BIAS_v2.GetIdentifySubjectResults
        Dim identitySubjectResultsResponse As New GetIdentifySubjectResultsResponsePackage()
        Return identitySubjectResultsResponse
    End Function
    <WebMethod()> _
    Public Function GetVerifyResults(GetVerifyResultsRequest As GetVerifyResultsRequest) As GetVerifyResultsResponsePackage Implements BIAS_v2.GetVerifyResults
        Dim verifyResultsResponse As New GetVerifyResultsResponsePackage()
        Return verifyResultsResponse
    End Function
    <WebMethod()> _
    Public Function Identify(IdentifyRequest As IdentifyRequest) As IdentifyResponsePackage Implements BIAS_v2.Identify
        Dim identifyResponse As New IdentifyResponsePackage()
        Return identifyResponse
    End Function
    <WebMethod()> _
    Public Function IdentifySubject(IdentifySubjectRequest As IdentifySubjectRequest) As IdentifySubjectResponsePackage Implements BIAS_v2.IdentifySubject
        Dim identifySubjectResponse As New IdentifySubjectResponsePackage()
        Return identifySubjectResponse
    End Function
    <WebMethod()> _
    Public Function ListBiographicData(ListBiographicDataRequest As ListBiographicDataRequest) As ListBiographicDataResponsePackage Implements BIAS_v2.ListBiographicData
        Dim listBiogDataResponse As New ListBiographicDataResponsePackage()
        Return listBiogDataResponse
    End Function
    <WebMethod()> _
    Public Function ListBiometricData(ListBiometricDataRequest As ListBiometricDataRequest) As ListBiometricDataResponsePackage Implements BIAS_v2.ListBiometricData
        Dim listBiomDataResponse As New ListBiometricDataResponsePackage()
        Return listBiomDataResponse
    End Function
    <WebMethod()> _
    Public Function PerformFusion(PerformFusionRequest As PerformFusionRequest) As PerformFusionResponsePackage Implements BIAS_v2.PerformFusion
        Dim performFusionResponse As New PerformFusionResponsePackage()
        Return performFusionResponse
    End Function
    <WebMethod()> _
    Public Function QueryCapabilities(QueryCapabilitiesRequest As QueryCapabilitiesRequest) As QueryCapabilitiesResponsePackage Implements BIAS_v2.QueryCapabilities
        Dim queryCapabilitiesResponse As New QueryCapabilitiesResponsePackage()
        Return queryCapabilitiesResponse
    End Function
    <WebMethod()> _
    Public Function RetrieveBiographicData(RetrieveBiographicDataRequest As RetrieveBiographicDataRequest) As RetrieveBiographicDataResponsePackage Implements BIAS_v2.RetrieveBiographicData
        Dim retrieveBiogDataResponse As New RetrieveBiographicDataResponsePackage()
        Return retrieveBiogDataResponse
    End Function
    <WebMethod()> _
    Public Function RetrieveBiometricData(RetrieveBiometricDataRequest As RetrieveBiometricDataRequest) As RetrieveBiometricDataResponsePackage Implements BIAS_v2.RetrieveBiometricData
        Dim retrieveBiomDataResponse As New RetrieveBiometricDataResponsePackage()
        Return retrieveBiomDataResponse
    End Function
    <WebMethod()> _
    Public Function RetrieveData(RetrieveDataRequest As RetrieveDataRequest) As RetrieveDataResponsePackage Implements BIAS_v2.RetrieveData
        Dim retrieveDataResponse As New RetrieveDataResponsePackage()
        Return retrieveDataResponse
    End Function
    <WebMethod()> _
    Public Function SetBiographicData(SetBiographicDataRequest As SetBiographicDataRequest) As SetBiographicDataResponsePackage Implements BIAS_v2.SetBiographicData
        Dim setBiogDataResponse As New SetBiographicDataResponsePackage()
        Return setBiogDataResponse
    End Function
    <WebMethod()> _
    Public Function SetBiometricData(SetBiometricDataRequest As SetBiometricDataRequest) As SetBiometricDataResponsePackage Implements BIAS_v2.SetBiometricData
        Dim setBiomDataResponse As New SetBiometricDataResponsePackage()
        Return setBiomDataResponse
    End Function
    <WebMethod()> _
    Public Function TransformBiometricData(TransformBiometricDataRequest As TransformBiometricDataRequest) As TransformBiometricDataResponsePackage Implements BIAS_v2.TransformBiometricData
        Dim transformBiomDataResponse As New TransformBiometricDataResponsePackage()
        Return transformBiomDataResponse
    End Function
    <WebMethod()> _
    Public Function UpdateBiographicData(UpdateBiographicDataRequest As UpdateBiographicDataRequest) As UpdateBiographicDataResponsePackage Implements BIAS_v2.UpdateBiographicData
        Dim updateBiogDataResponse As New UpdateBiographicDataResponsePackage()
        Return updateBiogDataResponse
    End Function
    <WebMethod()> _
    Public Function UpdateBiometricData(UpdateBiometricDataRequest As UpdateBiometricDataRequest) As UpdateBiometricDataResponsePackage Implements BIAS_v2.UpdateBiometricData
        Dim updateBiomDataResponse As New UpdateBiometricDataResponsePackage()
        Return updateBiomDataResponse
    End Function
    <WebMethod()> _
    Public Function Verify(VerifyRequest As VerifyRequest) As VerifyResponsePackage Implements BIAS_v2.Verify
        Dim verifyResponse As New VerifyResponsePackage()
        Return verifyResponse
    End Function
    <WebMethod()> _
    Public Function VerifySubject(VerifySubjectRequest As VerifySubjectRequest) As VerifySubjectResponsePackage Implements BIAS_v2.VerifySubject
        Dim verifySubjectResponse As New VerifySubjectResponsePackage()
        Return verifySubjectResponse
    End Function

End Class