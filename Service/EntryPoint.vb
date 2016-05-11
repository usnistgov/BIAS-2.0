﻿Option Strict On

Imports System.ServiceModel
Imports System.ServiceModel.Description
Imports System.ServiceModel.Web

Imports OASIS.BIAS.V2


Module EntryPoint
    Sub Main()

    End Sub

    Sub StartService()
        Console.WriteLine("BaseURI: {0}", Constants.ServiceBaseAddress)

        Dim host As New ServiceHost(GetType(BIASService), New Uri(Constants.ServiceBaseAddress))

        Dim binding = New BasicHttpBinding()
        binding.Namespace = "http://docs.oasis-open.org/bias/ns/bias-2.0/"
        binding.MaxReceivedMessageSize = Constants.BindingMaxReceivedMessageSize
        binding.MaxBufferSize = Constants.BindingMaxBufferSize
        binding.ReaderQuotas.MaxArrayLength = Integer.MaxValue
        binding.ReaderQuotas.MaxStringContentLength = Integer.MaxValue


        Dim b As New WebHttpBehavior()
        b.FaultExceptionEnabled = True

        host.AddServiceEndpoint(GetType(BIAS_v1), binding, "bias")
        'host.AddServiceEndpoint(GetType(IPolicyRetriever), New WebHttpBinding(), "").Behaviors.Add(b)


        Dim metadataBehavior As ServiceMetadataBehavior = If(host.Description.Behaviors.Find(Of ServiceMetadataBehavior)() Is Nothing, New ServiceMetadataBehavior(), host.Description.Behaviors.Find(Of ServiceMetadataBehavior)())
        metadataBehavior.HttpGetEnabled = True
        host.Description.Behaviors.Add(metadataBehavior)
        host.AddServiceEndpoint(ServiceMetadataBehavior.MexContractName, MetadataExchangeBindings.CreateMexHttpBinding(), "mex")



        For Each endpoint As ServiceEndpoint In host.Description.Endpoints()
            Console.WriteLine("Binding: {0}, address: {1}", endpoint.Binding.GetType().Name, endpoint.Address.Uri)
        Next



        host.Open()
        Console.ReadLine()
        host.Close()

        'C:\Program Files (x86)\Microsoft Visual Studio 10.0\Common7\IDE\WcfTestClient.exe
        'svcutil http://localhost:12345/BIAS (uses WS-MetadataExchange or DISCO)
    End Sub
End Module