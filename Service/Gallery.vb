Option Strict On

Imports System.Runtime.Serialization
Imports System.IO
Imports System.Collections.Generic

Module Gallery
    Public Subjects As SortedDictionary(Of String, Subject) = Nothing

    Public Sub Load()
        Subjects = New SortedDictionary(Of String, Subject)

        Dim BaseDirectory As New DirectoryInfo("C:\Users\pyl\Documents\NIST\BIAS Web Service Project\BIAS Tester\Service\MasterDB\Subject Records")

        If Not BaseDirectory.Exists Then
            BaseDirectory.Create()
        End If

        Dim Files As FileInfo() = BaseDirectory.GetFiles("????????-????-????-????-????????????.???")

        For Each File As FileInfo In Files
            Dim ID As String = File.Name.Substring(0, File.Name.LastIndexOf("."))
            Dim CurrentSubject As Subject = LoadSubject(ID)

            If CurrentSubject IsNot Nothing Then
                Subjects.Add(ID, CurrentSubject)
            End If
        Next File
    End Sub

    Public Sub Save()
        For Each ID As String In Subjects.Keys
            UpdateSubject(ID, Subjects(ID))
        Next
    End Sub

    Public Function LoadSubject(ByVal ID As String) As Subject
        Dim Result As Subject

        Using SubjectFile As FileStream = New IO.FileStream(GetFilename(ID), FileMode.OpenOrCreate, FileAccess.Read)
            Dim dcs As New DataContractSerializer(GetType(Subject))
            Result = CType(dcs.ReadObject(SubjectFile), Subject)
            SubjectFile.Close()
        End Using

        Return Result
    End Function

    Public Sub UpdateSubject(ByVal ID As String, ByVal s As Subject)
        Subjects(ID) = s

        Dim fm As FileMode = FileMode.OpenOrCreate

        'MsgBox(GetFilename(ID))

        Using SubjectFile As FileStream = New FileStream(GetFilename(ID), FileMode.OpenOrCreate, FileAccess.Write)
            Dim dcs As New DataContractSerializer(GetType(Subject))
            dcs.WriteObject(SubjectFile, s)
        End Using
    End Sub

    Public Sub UpdateSubject(ByVal s As Subject)
        UpdateSubject(s.GUID, s)
    End Sub

    Private Function GetFilename(ByVal ID As String) As String
        Return String.Format("{0}\{1}.txt", "C:\Users\pyl\Documents\NIST\BIAS Web Service Project\BIAS Tester\Service\MasterDB\Subject Records", ID)
    End Function
End Module