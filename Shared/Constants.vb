Option Strict On
Public Class Constants
    Private Shared m_ServiceBaseAddress As String = "http://localhost:12345"
    Public GalleryBaseDirectory As String = "C:\Temp\BIAS"
    Public Shared ReadOnly Property BindingMaxReceivedMessageSize As Integer
        Get
            Return Integer.MaxValue
        End Get
    End Property

    Public Shared ReadOnly Property BindingMaxBufferSize As Integer
        Get
            Return Integer.MaxValue
        End Get
    End Property


    Public Shared ReadOnly Property ServiceBaseAddress As String
        Get
            Return m_ServiceBaseAddress
        End Get
    End Property
End Class
