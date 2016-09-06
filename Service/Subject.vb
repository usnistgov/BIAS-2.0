Option Strict On

'Imports Nbis
Imports System.Drawing
Imports System.Runtime.Serialization
Imports System.Collections

Public Class Subject

    <DataMember()>
    Public Property Name As String

    '<IgnoreDataMember()>
    'Public Property BiometricSamples As New SortedDictionary(Of String, Bitmap)

    <DataMember()>
    Public Property GUID As String

    <DataMember()>
    Public Property GivenName As String

    <DataMember()>
    Public Property FamilyName As String

    <DataMember()>
    Public Property DateOfBirth As String

    <DataMember()>
    Public Property Sex As String

    <DataMember()>
    Public Property Citizenship As String

    <DataMember()>
    Public Property MinutiaeTemplates As ArrayList
End Class

