Option Strict On

Imports System.IO
Imports System.ServiceModel
Imports System.ServiceModel.Web

<ServiceContract()>
Public Interface IPolicyRetriever

    <OperationContract(), WebGet(UriTemplate:="/crossdomain.xml")>
    Function GetPolicy() As Stream

    '    Dim policy = "<?xml version='1.0'?><!DOCTYPE cross-domain-policy SYSTEM 'http://www.macromedia.com/xml/dtds/cross-domain-policy.dtd'> <cross-domain-policy><allow-access-from domain='*' /></cross-domain-policy>"
    '   WebOperationContext.Current.OutgoingResponse.ContentType = "application/xml"
    '  Return New MemoryStream(Encoding.UTF8.GetBytes(policy))
    'End Function
End Interface