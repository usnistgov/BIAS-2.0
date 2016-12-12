Imports OASIS.BIAS.V2
Imports Microsoft.VisualBasic
Imports System.Text
Imports System.IO
Imports System.Data
'Imports System.Windows.Forms
Imports System.Configuration
Imports System.ServiceModel.Web
Imports Microsoft.VisualBasic.Interaction
Imports System.Collections.Generic
Imports System.Linq
Imports System.Reflection
Imports System.Windows.Forms
Imports System.Drawing
Imports Emgu.CV
Imports Emgu.Util
Imports Emgu.CV.Structure
Imports Emgu.CV.Util
Imports System.Drawing.Imaging

Module mainModule
    Sub testRetrieve()
        Dim bias1 As New BIAS_v2Client()

        Dim retrieveData As New RetrieveDataRequest
        retrieveData.Identity = New BIASIdentity
        retrieveData.Identity.SubjectID = "TTPC5TMG"
        retrieveData.ProcessingOptions = New ProcessingOptionsType
        Dim testOption As New OptionType
        testOption.Key = "biometricData"
        testOption.Value = "basic"
        retrieveData.ProcessingOptions.Add(testOption)
        Dim a As RetrieveDataResponsePackage = bias1.RetrieveData(retrieveData)

    End Sub

    Sub testIdentification()
        Dim bias1 As New BIAS_v2Client()
        Console.WriteLine("Hello")
        'bias1.facialRecMain()
        ''Dim query As New QueryCapabilitiesRequest
        ''bias1.QueryCapabilities(query)
    End Sub

    Sub testRetrieveBiomData()
        Dim bias1 As New BIAS_v2Client()
        Dim retrieveBiomData As New RetrieveBiometricDataRequest
        retrieveBiomData.Identity = New BIASIdentity
        retrieveBiomData.Identity.SubjectID = "QKLJZBOU"
        retrieveBiomData.GalleryID = "1"
        Dim a As RetrieveBiometricDataResponsePackage = bias1.RetrieveBiometricData(retrieveBiomData)
    End Sub

    Sub testEnroll()
        Dim bias1 As New BIAS_v2Client()
        MessageBox.Show("Hello")

        Dim retrieveBiomData As New RetrieveBiometricDataRequest
        retrieveBiomData.Identity = New BIASIdentity
        retrieveBiomData.Identity.SubjectID = "QKLJZBOU"
        retrieveBiomData.GalleryID = "1"
        'Dim a As RetrieveBiometricDataResponsePackage = bias1.RetrieveBiometricData(retrieveBiomData)

        Dim sampleInfo As New InformationType
        sampleInfo.GUID = "Tester"
        sampleInfo.GivenName = "John"
        sampleInfo.FamilyName = "Doe"
        sampleInfo.DateOfBirth = "09/27/1990"
        sampleInfo.Sex = "M"
        sampleInfo.Citizenship = "United States of America"

        Dim testIdentity As New BIASIdentity
        testIdentity.BiometricData = New BIASBiometricDataType
        testIdentity.BiometricData.BIRList = New CBEFF_BIR_ListType

        Dim biomRecord = New CBEFF_BIR_Type

        'Create BIR_Info
        Dim sampleBIR As New BIRInfoType
        sampleBIR.Creator = "testCreator"
        sampleBIR.Index = "testIndex"
        sampleBIR.Integrity = True
        Dim payArray = New Byte() {0}
        sampleBIR.Payload = payArray
        Dim currentDate1 As Date = Date.Now
        sampleBIR.CreationDate = currentDate1
        sampleBIR.NotValidBefore = currentDate1
        sampleBIR.NotValidAfter = currentDate1

        'Create BDB_Info
        'Private PurposeField As OASIS.BIAS.V2.PurposeType
        'Private QualityField As OASIS.BIAS.V2.QualityType
        Dim sampleBDB As New BDBInfoType
        'sampleBDB.ChallengeResponseField = 
        sampleBDB.Index = "testIndex"
        Dim testFormat = New RegistryIDType
        testFormat.Organization = "testOrganization"
        testFormat.Type = "testType"
        sampleBDB.Format = testFormat
        sampleBDB.Encryption = False
        Dim currentDate2 As Date = Date.Now
        sampleBDB.CreationDate = currentDate2
        sampleBDB.NotValidBefore = currentDate2
        sampleBDB.NotValidAfter = currentDate2
        'sampleBDB.Type = 
        'sampleBDB.Subtype = 
        sampleBDB.Level = 0
        sampleBDB.Product = testFormat
        sampleBDB.CaptureDevice = testFormat
        sampleBDB.FeatureExtractionAlgorithm = testFormat
        sampleBDB.ComparisonAlgorithm = testFormat
        sampleBDB.CompressionAlgorithm = testFormat
        sampleBDB.Purpose = 2
        'sampleBDB.Quality =

        'Create SB_Info
        'Private extensionDataField As System.Runtime.Serialization.ExtensionDataObject
        'Private FormatField As OASIS.BIAS.V2.RegistryIDType
        Dim sampleSB As New SBInfoType
        sampleSB.Format = testFormat

        biomRecord.BIR_Information = New CBEFF_BIR_Type.BIR_InformationType
        biomRecord.BIR_Information.BIR_Info = sampleBIR
        biomRecord.BIR_Information.BDB_Info = sampleBDB
        biomRecord.BIR_Information.SB_Info = sampleSB
        testIdentity.BiometricData.BIRList.Add(biomRecord)

        Dim enrol1 As New EnrollRequest()
        enrol1.InputData = sampleInfo
        enrol1.Identity = testIdentity
        Dim es As EnrollResponsePackage = bias1.Enroll(enrol1)
    End Sub

    Sub testDeleteBiom()
        Dim bias1 As New BIAS_v2Client()
        Dim deleteRequest = New DeleteBiometricDataRequest
        deleteRequest.GalleryID = "1"
        deleteRequest.Identity = New BIASIdentity
        deleteRequest.Identity.SubjectID = "QJ1QOHQZ"

        Dim testReturn = bias1.DeleteBiometricData(deleteRequest)

    End Sub

    Sub testListBiom()

        Dim bias1 As New BIAS_v2Client()
        Dim listRequest = New ListBiometricDataRequest
        listRequest.Identity = New BIASIdentity
        listRequest.Identity.SubjectID = "QJ1QOHQZ"

        Dim testReturn = bias1.ListBiometricData(listRequest)

    End Sub

    Sub testUpdateBiom()


    End Sub

    Sub testFacial()
        Dim bias1 As New BIAS_v2Client()
        bias1.facialRecMain()
    End Sub
    Sub Main()

        'testEnroll()
        'testUpdateBiom()
        testFacial()
        'testEnroll()

    End Sub
End Module


<ServiceModel.ServiceBehavior(IncludeExceptionDetailInFaults:=True, Namespace:="http://docs.oasis-open.org/bias/ns/bias-2.0/")>
Public Class BIAS_v2Client
    Implements BIAS_v2

    Public Function getImagePaths(path As DirectoryInfo)
        Dim imagePathList As List(Of String) = New List(Of String)

        For Each currentFile In path.EnumerateFiles("*.*", SearchOption.AllDirectories)
            Dim fileName As String = currentFile.Name
            If Not fileName.EndsWith(".txt") And Not fileName.EndsWith(".jpg") Then
                Console.WriteLine(currentFile.FullName)
                imagePathList.Add(currentFile.FullName)
            End If
        Next

        Return imagePathList
    End Function

    Public Function getTestImagePaths(path As DirectoryInfo)
        Dim image_paths = New List(Of String)
        For Each currentFile In path.EnumerateFiles("*.*", SearchOption.AllDirectories)
            Dim fileName As String = currentFile.Name
            If fileName.EndsWith(".jpg") Then
                image_paths.Add(currentFile.FullName)
            End If
        Next
        Return image_paths
    End Function

    Public Function prediction(test_paths As List(Of String), recognizer As Face.LBPHFaceRecognizer, faceCascade As CascadeClassifier)

        For Each Path In test_paths

            'so now we have a list of all image filepaths needing identification
            'get image as variable
            Dim originalImage As New Image(Of Gray, Byte)(Path)

            'get the face area as a rectangle and create a new rectangle using the dimensions of the face rectangle
            Dim faceRegion As Rectangle() = faceCascade.DetectMultiScale(originalImage)
            Dim CropRect As New Rectangle(faceRegion(0).X, faceRegion(0).Y, faceRegion(0).Width, faceRegion(0).Height)
            'get the image from path, save in a new image variable. Also create a bitmap to save the cropped image in.
            Dim originalImage2 = System.Drawing.Image.FromFile(Path)
            Dim CropImage = New Bitmap(CropRect.Width, CropRect.Height)

            'take the original image and crop it, using the CropRect dimensions.
            Using grp = Graphics.FromImage(CropImage)
                grp.DrawImage(originalImage2, New Rectangle(0, 0, CropRect.Width, CropRect.Height), CropRect, GraphicsUnit.Pixel)
            End Using

            Dim croppedImage As New Image(Of Gray, Byte)(CropImage)

            'save it as the same filepath, but with 'cropped' added to it. 
            Dim croppedPath = Path.Substring(0, Path.LastIndexOf(".")) & "crop.jpg"
            croppedImage.Save(croppedPath)

            'then the below executes, doing the same thing, but now with a cropped, grayscale version of the original image. 
            'read in each image we want to identify, then convert it to grayscale
            Dim predict_image_pilC As Mat = CvInvoke.Imread(croppedPath, CvEnum.LoadImageType.Color)
            Dim predict_image_pilG As New Mat()
            CvInvoke.CvtColor(predict_image_pilC, predict_image_pilG, CvEnum.ColorConversion.Bgr2Gray)

            'Detect the face in the image
            Dim nbr_predicted = recognizer.Predict(predict_image_pilG)
            CvInvoke.Imshow("test", predict_image_pilG)

            Dim nbr_actual = Path.Substring(Path.LastIndexOf("\") + 1, 6)

            Dim confirmString = nbr_actual & " identified as Subject " & nbr_predicted.Label
            MessageBox.Show(nbr_predicted.Distance)
            MessageBox.Show(nbr_predicted.Label)
        Next

    End Function

    Public Function testTrain(imagePathList As List(Of String), faceCascade As CascadeClassifier)

        'need to return array of images and array of integers
        Dim images = New Emgu.CV.Image(Of Gray, Byte)() {}
        Dim labels = New Integer() {}

        For Each Path In imagePathList
            'Get image from the path
            Dim img1 As New Image(Of Gray, Byte)(Path)

            'get the face area as a rectangle and create a new rectangle using the dimensions of the face rectangle
            Dim faceRegion As Rectangle() = faceCascade.DetectMultiScale(img1)
            Dim CropRect As New Rectangle(faceRegion(0).X, faceRegion(0).Y, faceRegion(0).Width, faceRegion(0).Height)
            'get the image from path, save in a new image variable. Also create a bitmap to save the cropped image in.
            Dim OriginalImage = System.Drawing.Image.FromFile(Path)
            Dim CropImage = New Bitmap(CropRect.Width, CropRect.Height)

            'take the original image and crop it, using the CropRect dimensions.
            Using grp = Graphics.FromImage(CropImage)
                grp.DrawImage(OriginalImage, New Rectangle(0, 0, CropRect.Width, CropRect.Height), CropRect, GraphicsUnit.Pixel)
            End Using

            Dim img2 As New Image(Of Gray, Byte)(CropImage)
            'add cropped image to the array of images. 
            Array.Resize(images, images.Length + 1)
            images(images.Length - 1) = img2

            'Get the label/subjectID of the image
            Dim subjectID = Convert.ToInt32(Path.Substring(Path.LastIndexOf("\") + 1, 6))
            Array.Resize(labels, labels.Length + 1)
            labels(labels.Length - 1) = subjectID
        Next

        Return New Tuple(Of Emgu.CV.Image(Of Gray, Byte)(), Integer())(images, labels)
    End Function

    'for face registration and recognition
    Public Function facialRecMain()

        'load the classifier file and create the Local Binary Patterns Histograms Face Recognizer
        Dim faceCascade = New CascadeClassifier("C:\Users\pyl\Documents\NIST\BIAS Web Service Project\BIAS-2.0-master\Service\haarcascade_frontalface_default.xml")
        Dim recognizer = New Face.LBPHFaceRecognizer

        'Append all the absolute image paths in imagePathList
        Dim path As New IO.DirectoryInfo("C:\Users\pyl\Documents\NIST\BIAS Web Service Project\BIAS-2.0-master\Service\MasterDB\Subject Records")
        Dim imagePathList As List(Of String) = getImagePaths(path)

        'get list of images and subject IDs
        Dim imageAndSubjectIDs As Tuple(Of Emgu.CV.Image(Of Gray, Byte)(), Integer()) = testTrain(imagePathList, faceCascade)
        Dim images = imageAndSubjectIDs.Item1
        Dim subjectIDs = imageAndSubjectIDs.Item2

        'Perform training of the recognizer
        recognizer.Train(images, subjectIDs)

        'Append the images we want to recognize to image_path. Currently just .jgps instead of .pngs, need to come up with a naming system later. 
        Dim identifyImageFolder As New IO.DirectoryInfo("C:\Users\pyl\Documents\NIST\BIAS Web Service Project\BIAS-2.0-master\Service\MasterDB\New folder")
        Dim test_paths = getTestImagePaths(identifyImageFolder)

        'Perform prediction. Currently has messageboxes containing matching information. Not doing anything with the result right now.
        prediction(test_paths, recognizer, faceCascade)

        Return 1
    End Function

    'creates a new Subject ID
    Public Function generateRandomID() As String
        Dim characters As String = "0123456789"
        Dim rand As New Random
        Dim sb As New StringBuilder
        For charPos As Integer = 1 To 8
            Dim idx As Integer = rand.Next(0, 10)
            sb.Append(characters.Substring(idx, 1))
        Next
        Return sb.ToString()
    End Function

    'Used in deleteBiographicData
    Function CheckForAlphaCharacters(ByVal StringToCheck As String)
        For i = 0 To StringToCheck.Length - 1
            If Char.IsLetter(StringToCheck.Chars(i)) Then
                Return True
            End If
        Next
        Return False
    End Function

    'Used to convert 64byte items back to biometric images
    Function ImageFromBase64String(ByVal base64 As String)
        Using memStream As New MemoryStream(System.Convert.FromBase64String(base64))
            Dim result As System.Drawing.Image = System.Drawing.Image.FromStream(memStream)
            memStream.Close()
            Return result
        End Using
    End Function

    'Converts a biometric image to base64
    Private Function ImageToBase64String(ByVal image As System.Drawing.Image, ByVal imageFormat As ImageFormat)
        Using memStream As New MemoryStream
            image.Save(memStream, imageFormat)
            Dim result As String = Convert.ToBase64String(memStream.ToArray())
            memStream.Close()
            Return result
        End Using
    End Function

    Public Function AddSubjectToGallery(AddSubjectToGalleryRequest As AddSubjectToGalleryRequest) As AddSubjectToGalleryResponsePackage Implements BIAS_v2.AddSubjectToGallery

        Console.WriteLine("In AddSubjectToGallery")

        Dim galleryResponse As New AddSubjectToGalleryResponsePackage()
        galleryResponse.ResponseStatus = New ResponseStatus
        Dim GalleryID As String
        Dim SubjectID As String
        Dim IdentityClaim As String = AddSubjectToGalleryRequest.Identity.SubjectID
        Dim EncounterID As String = "1" 'temporarily

        'Check to see if all inputs are valid
        If (AddSubjectToGalleryRequest.GalleryID IsNot Nothing) Then
            GalleryID = AddSubjectToGalleryRequest.GalleryID
        Else
            galleryResponse.ResponseStatus.Return = 3
            galleryResponse.ResponseStatus.Message = "The gallery field for the AddSubjectToGalleryRequest is empty"
            Return galleryResponse
        End If
        If (AddSubjectToGalleryRequest.Identity.SubjectID IsNot Nothing) Then
            SubjectID = AddSubjectToGalleryRequest.Identity.SubjectID
        Else
            galleryResponse.ResponseStatus.Return = 9
            galleryResponse.ResponseStatus.Message = "The input subject ID is empty or in an invalid format."
            Return galleryResponse
        End If
        If (AddSubjectToGalleryRequest.Identity.IdentityClaim IsNot Nothing) Then
            IdentityClaim = AddSubjectToGalleryRequest.Identity.IdentityClaim
        ElseIf (AddSubjectToGalleryRequest.Identity.SubjectID IsNot Nothing) Then
            IdentityClaim = AddSubjectToGalleryRequest.Identity.SubjectID
        Else
            galleryResponse.ResponseStatus.Return = 3
            galleryResponse.ResponseStatus.Message = "There is no IdentityClaim or SubjectID for this AddSubjectToGalleryRequest"
            Return galleryResponse
        End If
        If (AddSubjectToGalleryRequest.Identity.EncounterID IsNot Nothing) Then
            EncounterID = AddSubjectToGalleryRequest.Identity.EncounterID
        End If

        'Check that the gallery Exists
        Dim galleriesPath = Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).ToString).ToString & "\MasterDB\Galleries"
        Dim subjectPath = Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).ToString).ToString & "\MasterDB\Subject Records\" & SubjectID
        Dim subjectFile As String = subjectPath & "\" & SubjectID & ".txt"
        If (Directory.Exists(galleriesPath & "\" & GalleryID)) Then

            'Check that the subject file exists
            If (File.Exists(subjectFile)) Then

                'readinalllines
                'if galleryinfo doesn't exist, it hasn't been added to a gallery before and we can add the four lines to the top, and copy the entire contents to the existing subject file
                'if it does, we need to re-write the gallery line, save that to the gallery file.
                'then we need to save the original read in text, but replace the existing galleryID lien with it + the new galleryID. Then save that over the existing subject record folder subject file.

                Dim readText As List(Of String) = System.IO.File.ReadAllLines(subjectFile).ToList
                If readText(1).Contains("GalleryID:") Then
                    Dim galleryText = readText(1)

                    'create the string
                    Dim galleryIDString = galleryText.Substring(0, galleryText.Length) & "," & GalleryID
                    'replace it
                    readText(1) = galleryIDString

                    Dim overwriteSubjectFilePath = Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).ToString).ToString _
                                                       & "\MasterDB\Subject Records\" & SubjectID & "\" & SubjectID & ".txt"
                    Dim readTextArray = readText.ToArray()

                    'rewrite subject record file to include the updated galleryID
                    System.IO.File.WriteAllText(overwriteSubjectFilePath, "")
                    System.IO.File.WriteAllLines(overwriteSubjectFilePath, readTextArray)

                    'now create the new file in the specified gallery (with just the one value in galleryID:)
                    Dim galleryIDStringSingle = "GalleryID:" & GalleryID

                    readText(1) = galleryIDStringSingle
                    Dim readTextArray2 = readText.ToArray()
                    System.IO.Directory.CreateDirectory(Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).ToString).ToString & "\MasterDB\Galleries\" & GalleryID _
                                                        & "\" & SubjectID)
                    Dim subjectFileGalPath As String = Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).ToString).ToString _
                                                       & "\MasterDB\Galleries\" & GalleryID & "\" & SubjectID & "\" & SubjectID & ".txt"
                    'create the file and writes to it.
                    System.IO.File.WriteAllLines(subjectFileGalPath, readTextArray2)

                Else
                    'create the strings
                    Dim galleryIDString = "GalleryID:" & GalleryID

                    'Add lines to the beginning of the string
                    readText.Insert(2, galleryIDString)
                    Dim readTextArray2() = readText.ToArray()

                    'Create new directory for subjectID
                    System.IO.Directory.CreateDirectory(Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).ToString).ToString & "\MasterDB\Galleries\" & GalleryID _
                                                        & "\" & SubjectID)

                    'create new file in gallery
                    Dim subjectFileGalPath As String = Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).ToString).ToString _
                                                       & "\MasterDB\Galleries\" & GalleryID & "\" & SubjectID & "\" & SubjectID & ".txt"
                    Dim overwriteSubjectFilePath As String = Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).ToString).ToString _
                                                       & "\MasterDB\Subject Records\" & SubjectID & "\" & SubjectID & ".txt"

                    System.IO.File.WriteAllLines(subjectFileGalPath, readTextArray2)
                    System.IO.File.WriteAllText(overwriteSubjectFilePath, "")
                    System.IO.File.WriteAllLines(overwriteSubjectFilePath, readTextArray2)
                End If

            Else
                galleryResponse.ResponseStatus.Return = 1
                galleryResponse.ResponseStatus.Message = "The subject record does not exist within the subject records directory."
                Return galleryResponse
            End If
        Else
            galleryResponse.ResponseStatus.Return = 11
            galleryResponse.ResponseStatus.Message = "The gallery referenced by the input gallery ID does not exist."
            Return galleryResponse
        End If

        'Biometric Data

        'Check if biometric images in the subject folder
        Dim imageList = Directory.GetFiles(subjectPath, "*.png")
        'put images into a list
        Dim biomImageList As New List(Of System.Drawing.Image)
        For Each biomImage In imageList
            Dim testImage As System.Drawing.Image = System.Drawing.Image.FromFile(biomImage)
            biomImageList.Add(testImage)
        Next

        Dim subjectFileGalPath2 As String = Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).ToString).ToString _
                                                       & "\MasterDB\Galleries\" & GalleryID & "\" & SubjectID & "\"
        Dim numImages = Directory.GetFiles(subjectFileGalPath2).Length - 1

        For Each biomImage In biomImageList
            biomImage.Save(subjectFileGalPath2 & SubjectID & "photo" & numImages + 1 & ".png")
            numImages = numImages + 1
        Next



        Return galleryResponse
    End Function

    Public Function CheckQuality(CheckQualityRequest As CheckQualityRequest) As CheckQualityResponsePackage Implements BIAS_v2.CheckQuality

        Console.WriteLine("In CheckQuality")

        Dim AlgorithmVersion = "1"
        Dim QualityScore As QualityType 'none yet
        Dim [Return] 'value indicating success or a specific error condition. 0 = Unsucesseful, 1 = Sucessful
        Dim responseMessage As String = ""


        Dim bir As CBEFF_BIR_Type
        bir = CheckQualityRequest.BiometricData.BIR

        If bir Is Nothing Then
            Dim SubjectID As String
            SubjectID = CheckQualityRequest.Identity.SubjectID
            If SubjectID Is Nothing Then
                [Return] = 0
                responseMessage = "Quality Check Unsuccessful. Both BIR and SubjectID were not provided"
                QualityScore = Nothing
            Else
                [Return] = 1
                responseMessage = "Quality Check successful based on SubjectID. The quality score is the previously held quality score within the CheckQualityRequest"
                QualityScore = CheckQualityRequest.QualityInfo.QualityScore
            End If
        Else
            [Return] = 1
            responseMessage = "Quality Check successful based on BiometricData.BIR. The quality score is the previously held quality score within the CheckQualityRequest"
            QualityScore = CheckQualityRequest.QualityInfo.QualityScore
        End If

        Dim qualityResponse As New CheckQualityResponsePackage()

        qualityResponse.ResponseStatus.Return = [Return]
        qualityResponse.ResponseStatus.Message = responseMessage
        qualityResponse.QualityInfo.QualityScore = QualityScore
        qualityResponse.QualityInfo.AlgorithmVersion = AlgorithmVersion

        Return qualityResponse
    End Function

    Public Function ClassifyBiometricData(ClassifyBiometricDataRequest As ClassifyBiometricDataRequest) As ClassifyBiometricDataResponsePackage Implements BIAS_v2.ClassifyBiometricData

        Console.WriteLine("In ClassifyBiometricData")

        'Hard coded to just return "Face"
        'Previously could be used to classify fingerprints, but with not necessary with faces.
        Dim classifyBioDataResponse As New ClassifyBiometricDataResponsePackage()
        classifyBioDataResponse.ResponseStatus = New ResponseStatus
        classifyBioDataResponse.ResponseStatus.Message = "Face"
        classifyBioDataResponse.ResponseStatus.Return = 0

        Return classifyBioDataResponse
    End Function

    Public Function CreateSubject(CreateSubjectRequest As CreateSubjectRequest) As CreateSubjectResponsePackage Implements BIAS_v2.CreateSubject

        Console.WriteLine("In CreateSubject")

        'Creates a model-neutral subject record
        Dim createSubjectResponse As New CreateSubjectResponsePackage()
        createSubjectResponse.Identity = New BIASIdentity
        createSubjectResponse.ResponseStatus = New ResponseStatus

        '1. Generate a 8 digit ID (A-Z,0-9) - 2.25 Trillion Unique IDs - 
        Dim generatedID As String = generateRandomID()
        '2. Open text file with IDs currently in use.
        Try

            Dim sr As New StreamReader(Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).ToString).ToString & "\MasterDB\SubjectIDDB.txt")
            Dim line As String
            line = sr.ReadToEnd()
            '3. Search for generated ID
            Dim doesIDExist = line.IndexOf(generatedID)
            '4. If found, generate another.
            While doesIDExist > -1
                generatedID = generateRandomID()
                doesIDExist = line.IndexOf(generatedID)
            End While
            sr.Close()

            '5. Once a unique ID is generated, save it in the text file.
            line = line & generatedID & Environment.NewLine
            Dim path = Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).ToString).ToString & "\MasterDB\SubjectIDDB.txt"
            System.IO.File.WriteAllText(path, "")
            System.IO.File.WriteAllText(path, line)

        Catch e As Exception
            Console.WriteLine(e.Message)
        End Try

        'create new folder for the subjectID
        System.IO.Directory.CreateDirectory(Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).ToString).ToString & "\MasterDB\Subject Records\" & generatedID)

        '6. Create a subject record (text file)
        Dim f As FileStream = File.Create(Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).ToString).ToString & "\MasterDB\Subject Records\" & generatedID & "\" & generatedID & ".txt")
        f.Close()

        '7. Write subjectID and the biographicData+biometricData headers to file
        Dim subjectRecordPath = Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).ToString).ToString & "\MasterDB\Subject Records\" & generatedID & "\" & generatedID & ".txt"
        Dim objWriter As New System.IO.StreamWriter(subjectRecordPath)
        objWriter.Write("SubjectID:" & generatedID & Environment.NewLine)
        objWriter.Write("IdentityClaim:" & generatedID & Environment.NewLine)
        objWriter.Write("BiographicData:" & Environment.NewLine)
        objWriter.Write("BiometricData:" & Environment.NewLine)
        objWriter.Close()

        '8.Create Response
        createSubjectResponse.Identity.SubjectID = generatedID
        createSubjectResponse.ResponseStatus.Return = 0
        createSubjectResponse.ResponseStatus.Message = "Success!"

        Return createSubjectResponse

    End Function

    Public Function DeleteBiographicData(DeleteBiographicDataRequest As DeleteBiographicDataRequest) As DeleteBiographicDataResponsePackage Implements BIAS_v2.DeleteBiographicData

        Console.WriteLine("In DeleteBiographicData")

        Dim SubjectID = DeleteBiographicDataRequest.Identity.SubjectID
        Dim GalleryID = DeleteBiographicDataRequest.GalleryID
        Dim deleteBiogDataResponse As New DeleteBiographicDataResponsePackage()
        deleteBiogDataResponse.ResponseStatus = New ResponseStatus()


        'Check to see if all inputs are valid
        If (DeleteBiographicDataRequest.GalleryID Is Nothing) Then
            deleteBiogDataResponse.ResponseStatus.Return = 3
            deleteBiogDataResponse.ResponseStatus.Message = "The gallery field for the AddSubjectToGalleryRequest is empty"
            Return deleteBiogDataResponse
        End If
        If (DeleteBiographicDataRequest.Identity.SubjectID Is Nothing) Then
            deleteBiogDataResponse.ResponseStatus.Return = 9
            deleteBiogDataResponse.ResponseStatus.Message = "The input subject ID is empty or in an invalid format."
            Return deleteBiogDataResponse
        End If

        'If encounter ID...
        'else

        Dim subjectFile = Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).ToString).ToString & "\MasterDB\Subject Records" & "\" & SubjectID & "\" & SubjectID & ".txt"

        'Read in text file, find the line # that has biographic data
        If (File.Exists(subjectFile)) Then
            Dim readText As List(Of String) = System.IO.File.ReadAllLines(subjectFile).ToList

            Dim biographicLineNum = readText.IndexOf("BiographicData:")
            Dim endBiographicLineNum = readText.IndexOf("BiometricData:")
            Dim curIndex = biographicLineNum + 1
            For counter = biographicLineNum + 1 To endBiographicLineNum - 1
                readText.RemoveAt(curIndex)
            Next

            System.IO.File.WriteAllLines(subjectFile, readText)

            'rewrite gallery copy with the same text
            Dim galleriesPath = Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).ToString).ToString & "\MasterDB\Galleries"
            If (Directory.Exists(galleriesPath & "\" & GalleryID)) Then
                Dim subjectFilePath = galleriesPath & "\" & GalleryID & "\" & SubjectID & ".txt"
                System.IO.File.WriteAllLines(subjectFilePath, readText)
                deleteBiogDataResponse.ResponseStatus.Return = 0
                deleteBiogDataResponse.ResponseStatus.Message = "Biographic data sucessfully deleted."
                Return deleteBiogDataResponse
            Else
                deleteBiogDataResponse.ResponseStatus.Return = 11
                deleteBiogDataResponse.ResponseStatus.Message = "The gallery referenced by the input gallery ID does not exist."
                Return deleteBiogDataResponse
            End If
        Else
            deleteBiogDataResponse.ResponseStatus.Return = 10
            deleteBiogDataResponse.ResponseStatus.Message = "The subject referenced by the input subject ID does not exist."
            Return deleteBiogDataResponse
        End If

    End Function

    Public Function DeleteBiometricData(DeleteBiometricDataRequest As DeleteBiometricDataRequest) As DeleteBiometricDataResponsePackage Implements BIAS_v2.DeleteBiometricData

        Console.WriteLine("In DeleteBiometricData")

        Dim deleteBiomDataResponse As New DeleteBiometricDataResponsePackage()
        Dim GalleryID = ""
        Dim SubjectID = ""
        Dim opt = DeleteBiometricDataRequest.BiometricType

        'Check to see if all inputs are valid
        If (DeleteBiometricDataRequest.GalleryID IsNot Nothing) Then
            GalleryID = DeleteBiometricDataRequest.GalleryID
        End If
        If (DeleteBiometricDataRequest.Identity.SubjectID IsNot Nothing) Then
            SubjectID = DeleteBiometricDataRequest.Identity.SubjectID
        Else
            deleteBiomDataResponse.ResponseStatus.Return = 9
            deleteBiomDataResponse.ResponseStatus.Message = "The input subject ID is empty or in an invalid format."
            Return deleteBiomDataResponse
        End If

        'Define file paths
        Dim subjectPath = Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).ToString).ToString & "\MasterDB\Subject Records\" & SubjectID
        Dim subjectFile As String = subjectPath & "\" & SubjectID & ".txt"

        'Check if there are biometricType inputs
        If opt Is Nothing Then
            'Delete all images and metadata
            'If no GalleryID, delete from all galleries and subject record
            If GalleryID = "" Then
                Dim galleryString = ""
                Dim listGalleries = New List(Of String)
                'Open file, read galleries, put in list
                Dim readText As List(Of String) = System.IO.File.ReadAllLines(subjectFile).ToList
                For Each line In readText
                    If line.IndexOf("GalleryID") = 0 Then
                        galleryString = line.Substring(10)
                        listGalleries = galleryString.Split(","c).ToList()
                    End If
                Next

                'Delete text from text file
                Dim biometricLineNum = readText.IndexOf("BiometricData:")
                Dim curIndex = biometricLineNum + 1
                For counter = biometricLineNum + 1 To readText.Count - 1
                    readText.RemoveAt(curIndex)
                Next
                System.IO.File.WriteAllLines(subjectFile, readText)

                'Delete images from subject record
                Dim iList = Directory.GetFiles(subjectPath, "*.png")
                For Each imagePath In iList
                    File.Delete(imagePath)
                Next

                'Delete both from all galleries in listGalleries
                For Each Gallery In listGalleries
                    Dim subjectGalFilepath = Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).ToString).ToString & "\MasterDB\Galleries\" & Gallery & "\" & SubjectID
                    Dim subjectGalFile = subjectGalFilepath & "\" & SubjectID & ".txt"

                    'Delete text from text file
                    System.IO.File.WriteAllLines(subjectGalFile, readText)

                    'Delete Image
                    Dim imGalList = Directory.GetFiles(subjectGalFilepath, "*.png")
                    For Each imagePath In imGalList
                        File.Delete(imagePath)
                    Next
                Next

            Else 'Delete just from the gallery provided
                Dim subjectGalFilepath = Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).ToString).ToString & "\MasterDB\Galleries\" & GalleryID & "\" & SubjectID
                Dim subjectGalFile = subjectGalFilepath & "\" & SubjectID & ".txt"

                'Delete text from file
                Dim readText As List(Of String) = System.IO.File.ReadAllLines(subjectGalFile).ToList
                Dim biometricLineNum = readText.IndexOf("BiometricData:")
                Dim curIndex = biometricLineNum + 1
                For counter = biometricLineNum + 1 To readText.Count - 1
                    readText.RemoveAt(curIndex)
                Next
                System.IO.File.WriteAllLines(subjectGalFile, readText)

                'Delete Image
                Dim imGalList = Directory.GetFiles(subjectGalFilepath, "*.png")
                For Each imagePath In imGalList
                    File.Delete(imagePath)
                Next

            End If

        Else
            MessageBox.Show("In DeleteBiometricData, with a BiometricType. Still need to implement.")
        End If

        MessageBox.Show("Stop")
        Return deleteBiomDataResponse
    End Function

    Public Function DeleteSubject(DeleteSubjectRequest As DeleteSubjectRequest) As DeleteSubjectResponsePackage Implements BIAS_v2.DeleteSubject
        Dim deleteSubjectResponse As New DeleteSubjectResponsePackage()
        deleteSubjectResponse.ResponseStatus = New ResponseStatus
        Dim subjectID As String

        'Check that SubjectID is not null
        If (DeleteSubjectRequest.Identity.SubjectID IsNot Nothing) Then
            subjectID = DeleteSubjectRequest.Identity.SubjectID
        Else
            deleteSubjectResponse.ResponseStatus.Return = 9
            deleteSubjectResponse.ResponseStatus.Message = "The input subject ID is empty or in an invalid format."
            Return deleteSubjectResponse
        End If

        'get the gallery that the subject is potentially in.
        Dim GalleryID As String = ""
        Dim subjectFile As String = Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).ToString).ToString & "\MasterDB\Subject Records\" _
                                    & subjectID & "\" & subjectID & ".txt"
        If (File.Exists(subjectFile)) Then
            Dim readText As List(Of String) = System.IO.File.ReadAllLines(subjectFile).ToList

            'read file contents, find galleryID, read in the value
            For index As Integer = 0 To readText.Count() - 1
                Dim currentLine As String = readText(index)
                Dim galleryIDIndex As Integer = currentLine.IndexOf("GalleryID:")
                If (galleryIDIndex = 0) Then
                    GalleryID = currentLine.Substring(10)
                End If
            Next

            'delete the folder+contents
            Dim subjectRecordFolder = Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).ToString).ToString & "\MasterDB\Subject Records\" _
                                    & subjectID
            System.IO.Directory.Delete(subjectRecordFolder, True)

        Else 'if the subject record doesn't exist
            deleteSubjectResponse.ResponseStatus.Return = 10
            deleteSubjectResponse.ResponseStatus.Message = "The subject referenced by the input subject ID does not exist."
            Return deleteSubjectResponse
        End If


        'Go to gallery and delete the folder+contents
        Dim recordInGalleryPath = Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).ToString).ToString & "\MasterDB\Galleries\" & GalleryID & "\" & subjectID

        System.IO.Directory.Delete(recordInGalleryPath, True)

        deleteSubjectResponse.ResponseStatus.Return = 0
        deleteSubjectResponse.ResponseStatus.Message = "Sucessfully deleted."

        Return deleteSubjectResponse
    End Function

    Public Function DeleteSubjectFromGallery(DeleteSubjectFromGalleryRequest As DeleteSubjectFromGalleryRequest) As DeleteSubjectFromGalleryResponsePackage Implements BIAS_v2.DeleteSubjectFromGallery
        Dim deleteSubjectGalleryResponse As New DeleteSubjectFromGalleryResponsePackage()
        deleteSubjectGalleryResponse.ResponseStatus = New ResponseStatus

        Dim GalleryID As String = DeleteSubjectFromGalleryRequest.GalleryID
        Dim SubjectID As String = DeleteSubjectFromGalleryRequest.Identity.SubjectID
        Dim IdentityClaim As String = DeleteSubjectFromGalleryRequest.Identity.IdentityClaim
        Dim idOrClaim As String = ""

        'Check to see if all inputs are valid
        If (DeleteSubjectFromGalleryRequest.GalleryID Is Nothing) Then
            deleteSubjectGalleryResponse.ResponseStatus.Return = 3
            deleteSubjectGalleryResponse.ResponseStatus.Message = "The gallery field for the DeleteSubjectFromGalleryRequest is empty"
            Return deleteSubjectGalleryResponse
        End If

        If (DeleteSubjectFromGalleryRequest.Identity.SubjectID Is Nothing And DeleteSubjectFromGalleryRequest.Identity.IdentityClaim Is Nothing) Then
            deleteSubjectGalleryResponse.ResponseStatus.Return = 9
            deleteSubjectGalleryResponse.ResponseStatus.Message = "Both the input subjectID and the IdentityClaim are empty."
            Return deleteSubjectGalleryResponse
        End If
        If (DeleteSubjectFromGalleryRequest.Identity.SubjectID IsNot Nothing) Then
            idOrClaim = "id"
        ElseIf (DeleteSubjectFromGalleryRequest.Identity.IdentityClaim IsNot Nothing) Then
            idOrClaim = "claim"
        End If

        'to avoid duplicate code, standardize the conditional variable used.
        If (idOrClaim = "claim") Then
            SubjectID = IdentityClaim
        End If

        'Go to gallery and delete the folder+contents
        Dim recordInGalleryPath = Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).ToString).ToString & "\MasterDB\Galleries\" & GalleryID & "\" & SubjectID
        System.IO.Directory.Delete(recordInGalleryPath, True)

        deleteSubjectGalleryResponse.ResponseStatus.Return = 0
        deleteSubjectGalleryResponse.ResponseStatus.Message = "Subject Record sucessfully deleted from gallery."
        Return deleteSubjectGalleryResponse
    End Function

    Public Function Enroll(EnrollRequest As EnrollRequest) As EnrollResponsePackage Implements BIAS_v2.Enroll

        Console.WriteLine("In Enroll")

        'Dim sent64Byte As String = EnrollRequest.Identity.BiometricData.BIRList(0).BIR.biometricImage
        If EnrollRequest.Identity.BiometricData.BIRList(0).BIR_Information.BIR_Info.Creator Is Nothing Then
            MessageBox.Show("The creator is dead Jim.")
        Else
            MessageBox.Show("The creator looks good")
        End If
        If EnrollRequest.Identity.BiometricData.BIRList(0).BIR.biometricImageType Is Nothing Then
            MessageBox.Show("The image type is dead Jim.")
        Else
            MessageBox.Show("The image type looks good")
        End If
        If EnrollRequest.Identity.BiometricData.BIRList(0).BIR.biometricImage Is Nothing Then
            MessageBox.Show("The image is dead Jim.")
        Else
            MessageBox.Show("The image looks good")
        End If
        'ImageFromBase64String(sent64Byte)
        'Dim convertedImage = ImageFromBase64String(sent64Byte)



        Dim enrollResponse As New EnrollResponsePackage()
        enrollResponse.ResponseStatus = New ResponseStatus

        Dim bias As New BIAS_v2Client()

        Dim processingOptions As New ProcessingOptionsType
        processingOptions = EnrollRequest.ProcessingOptions

        Dim inputData As New InformationType
        inputData = EnrollRequest.InputData

        'Assuming Identify Subject comes out with false

        'call createSubject
        Dim createSub As New CreateSubjectRequest
        Dim subjectID As String = ""
        subjectID = bias.CreateSubject(createSub).Identity.SubjectID

        'create the identity to be used across all setup/enroll functions
        Dim newIdentity As New BIASIdentity
        newIdentity.SubjectID = subjectID
        newIdentity.BiometricData = EnrollRequest.Identity.BiometricData

        'set biographic data
        Dim setBiog As New SetBiographicDataRequest
        setBiog.Identity = newIdentity
        setBiog.Identity.BiographicData = New BiographicDataType
        setBiog.Identity.BiographicData.FirstName = "John"
        setBiog.Identity.BiographicData.LastName = "Doe"
        setBiog.Identity.BiographicData.BiographicDataItemList = New BiographicDataItemListType 'filled with biographicDataItemTypes, which consist of Name, Type, and Value Strings.

        Dim GUIDField = New BiographicDataItemType
        GUIDField.Name = "GUID"
        GUIDField.Type = "String"
        GUIDField.Value = inputData.GUID
        Dim GivenNameField = New BiographicDataItemType
        GivenNameField.Name = "GivenName"
        GivenNameField.Type = "String"
        GivenNameField.Value = inputData.GivenName
        Dim FamilyNameField = New BiographicDataItemType
        FamilyNameField.Name = "FamilyName"
        FamilyNameField.Type = "String"
        FamilyNameField.Value = inputData.FamilyName
        Dim DateOfBirthField = New BiographicDataItemType
        DateOfBirthField.Name = "DateOfBirth"
        DateOfBirthField.Type = "String"
        DateOfBirthField.Value = inputData.DateOfBirth
        Dim SexField = New BiographicDataItemType
        SexField.Name = "Sex"
        SexField.Type = "String"
        SexField.Value = inputData.Sex
        Dim CitizenshipField = New BiographicDataItemType
        CitizenshipField.Name = "Citizenship"
        CitizenshipField.Type = "String"
        CitizenshipField.Value = inputData.Citizenship

        setBiog.Identity.BiographicData.BiographicDataItemList.Add(GUIDField)
        setBiog.Identity.BiographicData.BiographicDataItemList.Add(GivenNameField)
        setBiog.Identity.BiographicData.BiographicDataItemList.Add(FamilyNameField)
        setBiog.Identity.BiographicData.BiographicDataItemList.Add(DateOfBirthField)
        setBiog.Identity.BiographicData.BiographicDataItemList.Add(SexField)
        setBiog.Identity.BiographicData.BiographicDataItemList.Add(CitizenshipField)
        bias.SetBiographicData(setBiog)

        'set biometric data
        Dim setBiom As New SetBiometricDataRequest
        setBiom.Identity = EnrollRequest.Identity 'used to be newIdentity, but this was erasing the image within BIR for some reason. Not sure why?

        bias.SetBiometricData(setBiom)

        'add subject to gallery
        Dim addToGallery As New AddSubjectToGalleryRequest
        addToGallery.GalleryID = "1"
        addToGallery.Identity = newIdentity
        bias.AddSubjectToGallery(addToGallery)

        'Check if the given subject is already known to the system using Identify
        'If not, use any or all of createSubject, SetBiographicData, SetBiometricData, and AddSubjectToGallery to make it known.

        'If it is known, then update bio/biog data in a person centric model, or initiate set Biog/Biom data in an encounter-centric model

        enrollResponse.Identity = newIdentity
        'MessageBox.Show(enrollResponse.Identity.SubjectID)
        enrollResponse.ResponseStatus.Return = 0
        enrollResponse.ResponseStatus.Message = "Participant Enrolled"
        Return enrollResponse
    End Function

    Public Function GetEnrollResults(GetEnrollResultsRequest As GetEnrollResultsRequest) As GetEnrollResultsResponsePackage Implements BIAS_v2.GetEnrollResults
        Dim enrollResultsResponse As New GetEnrollResultsResponsePackage()
        Return enrollResultsResponse
    End Function

    Public Function GetIdentifyResults(GetIdentifyResultsRequest As GetIdentifyResultsRequest) As GetIdentifyResultsResponsePackage Implements BIAS_v2.GetIdentifyResults
        Dim identityResultsResponse As New GetIdentifyResultsResponsePackage()
        Return identityResultsResponse
    End Function

    Public Function GetIdentifySubjectResults(GetIdentifySubjectResultsRequest As GetIdentifySubjectResultsRequest) As GetIdentifySubjectResultsResponsePackage Implements BIAS_v2.GetIdentifySubjectResults
        Dim identitySubjectResultsResponse As New GetIdentifySubjectResultsResponsePackage()
        Return identitySubjectResultsResponse
    End Function

    Public Function GetVerifyResults(GetVerifyResultsRequest As GetVerifyResultsRequest) As GetVerifyResultsResponsePackage Implements BIAS_v2.GetVerifyResults
        Dim Result As New GetVerifyResultsResponsePackage
        Return Result
    End Function

    Public Function Identify(IdentifyRequest As IdentifyRequest) As IdentifyResponsePackage Implements BIAS_v2.Identify
        'Dim d1 = IdentifyRequest.InputData.ExtensionData.
        Dim identifyResponse As New IdentifyResponsePackage()
        identifyResponse.ResponseStatus = New ResponseStatus
        identifyResponse.ResponseStatus.Return = 99
        identifyResponse.ResponseStatus.Message = "Not implemented yet"
        Return identifyResponse
    End Function

    Public Function IdentifySubject(IdentifySubjectRequest As IdentifySubjectRequest) As IdentifySubjectResponsePackage Implements BIAS_v2.IdentifySubject
        Dim identifySubjectResponse As New IdentifySubjectResponsePackage()
        identifySubjectResponse.ResponseStatus = New ResponseStatus
        identifySubjectResponse.ResponseStatus.Return = 99
        identifySubjectResponse.ResponseStatus.Message = "Not implemented yet"
        Return identifySubjectResponse
    End Function

    Public Function ListBiographicData(ListBiographicDataRequest As ListBiographicDataRequest) As ListBiographicDataResponsePackage Implements BIAS_v2.ListBiographicData


        Dim subjectID As String = ListBiographicDataRequest.Identity.SubjectID
        Dim encounterID As String = ListBiographicDataRequest.Identity.EncounterID
        Dim encounterType As String = ListBiographicDataRequest.EncounterType

        Dim listBiogDataResponse As New ListBiographicDataResponsePackage()
        listBiogDataResponse.ResponseStatus = New ResponseStatus()
        listBiogDataResponse.Identity = ListBiographicDataRequest.Identity

        If (ListBiographicDataRequest.Identity.SubjectID Is Nothing) Then
            listBiogDataResponse.ResponseStatus.Return = 9
            listBiogDataResponse.ResponseStatus.Message = "The input subject ID is empty or in an invalid format."
            Return listBiogDataResponse
        End If

        'open subject record file
        Dim subjectFile = Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).ToString).ToString & "\MasterDB\Subject Records" & "\" & subjectID & "\" & subjectID & ".txt"

        'Read in text file, find the line # that has biographic data
        If (File.Exists(subjectFile)) Then

            Dim readText As List(Of String) = System.IO.File.ReadAllLines(subjectFile).ToList
            Dim biographicLineNum = readText.IndexOf("BiographicData:")
            Dim endBiographicLineNum = readText.IndexOf("BiometricData:")

            Dim biographicData As New BiographicDataType
            biographicData.BiographicDataItemList = New BiographicDataItemListType

            'Loop through readText starting at biographic Line Num, create new biographicdataitems and add them to the lst
            For counter = biographicLineNum + 1 To endBiographicLineNum - 1
                Dim data = New BiographicDataItemType
                data.Name = readText(counter).Substring(0, readText(counter).IndexOf(":"))
                biographicData.BiographicDataItemList.Add(data)
            Next

            listBiogDataResponse.ResponseStatus.Return = 0
            listBiogDataResponse.ResponseStatus.Message = "List of biographic data types sucessfully returned."
            listBiogDataResponse.Identity.BiographicData = biographicData
            Return listBiogDataResponse
        Else
            listBiogDataResponse.ResponseStatus.Return = 10
            listBiogDataResponse.ResponseStatus.Message = "The subject referenced by the input subject ID does not exist."
            Return listBiogDataResponse
        End If

    End Function

    Public Function ListBiometricData(ListBiometricDataRequest As ListBiometricDataRequest) As ListBiometricDataResponsePackage Implements BIAS_v2.ListBiometricData
        Dim subjectID As String = ListBiometricDataRequest.Identity.SubjectID

        Dim listBiomDataResponse As New ListBiometricDataResponsePackage()
        listBiomDataResponse.ResponseStatus = New ResponseStatus()

        If (ListBiometricDataRequest.Identity.SubjectID Is Nothing) Then
            listBiomDataResponse.ResponseStatus.Return = 9
            listBiomDataResponse.ResponseStatus.Message = "The input subject ID is empty or in an invalid format."
            Return listBiomDataResponse
        End If

        'Go into subject record
        Dim subjectFile = Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).ToString).ToString & "\MasterDB\Subject Records" & "\" & subjectID & "\" & subjectID & ".txt"
        If (File.Exists(subjectFile)) Then

            Dim readText As List(Of String) = System.IO.File.ReadAllLines(subjectFile).ToList
            'Go to each biometric image
            Dim imageNameIndexes = New List(Of Integer)
            Dim lineNum = 0
            For Each line In readText
                If line.IndexOf("ImageName") = 0 Then
                    imageNameIndexes.Add(lineNum)
                End If
                lineNum = lineNum + 1
            Next

            'Create biometricData types and add them to the biometricdatatypelist list
            Dim bioDataTypeList = New List(Of BiometricDataType)
            For Each index In imageNameIndexes
                Dim bioType = New BiometricDataType
                bioType.BiometricType.Nodes.SetValue("Face", 0)
                bioType.BDBFormatOwner = 257 'format owner and type are always these values
                bioType.BDBFormatType = 1
                bioDataTypeList.Add(bioType)
            Next

            listBiomDataResponse.Identity = ListBiometricDataRequest.Identity
            listBiomDataResponse.Identity.BiometricData = New BIASBiometricDataType
            listBiomDataResponse.Identity.BiometricData.BiometricDataList = bioDataTypeList
            listBiomDataResponse.ResponseStatus.Return = 0
            listBiomDataResponse.ResponseStatus.Message = "Biometric data types sucessfully returned."
            Return listBiomDataResponse
        Else
            listBiomDataResponse.ResponseStatus.Return = 10
            listBiomDataResponse.ResponseStatus.Message = "The subject referenced by the input subject ID does not exist."
            Return listBiomDataResponse
        End If

    End Function

    Public Function PerformFusion(PerformFusionRequest As PerformFusionRequest) As PerformFusionResponsePackage Implements BIAS_v2.PerformFusion
        Dim performFusionResponse As New PerformFusionResponsePackage()
        Return performFusionResponse
    End Function

    Public Function QueryCapabilities(QueryCapabilitiesRequest As QueryCapabilitiesRequest) As QueryCapabilitiesResponsePackage Implements BIAS_v2.QueryCapabilities

        Dim capabilityList As List(Of CapabilityType) = New CapabilityListType

        Dim AggregateInputDataOptional As New CapabilityType
        AggregateInputDataOptional.CapabilityName = 0
        AggregateInputDataOptional.CapabilityID = "0"
        AggregateInputDataOptional.CapabilityDescription = "AggregateInputDataOptional: Names of the data elements returned by aggregate services. This implementation does not use aggregate services, so the capability value and supporting value will be an empty string"
        AggregateInputDataOptional.CapabilityValue = ""
        AggregateInputDataOptional.CapabilitySupportingValue = ""
        capabilityList.Add(AggregateInputDataOptional)

        Dim AggregateInputDataRequired As New CapabilityType
        AggregateInputDataRequired.CapabilityName = 1
        AggregateInputDataRequired.CapabilityID = "1"
        AggregateInputDataRequired.CapabilityDescription = "AggregateInputDataRequired: Names of the input data elements required bf the aggregate services. This implementation does not use aggregate services, so the capability value and supporting value will be an empty string"
        AggregateInputDataRequired.CapabilityValue = ""
        AggregateInputDataRequired.CapabilitySupportingValue = ""
        capabilityList.Add(AggregateInputDataRequired)

        Dim AggregateProcessingOption As New CapabilityType
        AggregateProcessingOption.CapabilityName = 2
        AggregateProcessingOption.CapabilityID = "2"
        AggregateProcessingOption.CapabilityDescription = "AggregateProcessingOption: Description of the key/value pairs in the Processing Option parameter in the aggregate services. This implementation does not use aggregate services, so the capability value and supporting value will be an empty string"
        AggregateProcessingOption.CapabilityValue = ""
        AggregateProcessingOption.CapabilitySupportingValue = ""
        capabilityList.Add(AggregateProcessingOption)

        Dim AggregateReturnData As New CapabilityType
        AggregateReturnData.CapabilityName = 3
        AggregateReturnData.CapabilityID = "3"
        AggregateReturnData.CapabilityDescription = "AggregateReturnData: Names of the data element returned by the aggregate services. This implementation does not use aggregate services, so the capability value and supporting value will be an empty string"
        AggregateReturnData.CapabilityValue = ""
        AggregateReturnData.CapabilitySupportingValue = ""
        capabilityList.Add(AggregateReturnData)

        Dim AggregateServiceDescription As New CapabilityType
        AggregateServiceDescription.CapabilityName = 4
        AggregateServiceDescription.CapabilityID = "4"
        AggregateServiceDescription.CapabilityDescription = "AggregateServiceDescription: Description of the processing logic of the aggregate services. This implementation does not use aggregate services, so the capability value and supporting value will be an empty string"
        AggregateServiceDescription.CapabilityValue = ""
        AggregateServiceDescription.CapabilitySupportingValue = ""
        capabilityList.Add(AggregateServiceDescription)

        Dim biographicDataSet As New CapabilityType
        biographicDataSet.CapabilityName = 5
        biographicDataSet.CapabilityID = "5"
        biographicDataSet.CapabilityDescription = "BiographicDataSet: Information about the biographic data sets supported by the system"
        biographicDataSet.CapabilityValue = "EFTS"
        biographicDataSet.CapabilitySupportingValue = "Unknown"
        biographicDataSet.CapabilityAdditionalInfo = "ASCII"
        capabilityList.Add(biographicDataSet)

        Dim cbeffPatronFormat As New CapabilityType
        cbeffPatronFormat.CapabilityName = 6
        cbeffPatronFormat.CapabilityID = "6"
        cbeffPatronFormat.CapabilityDescription = "CBEFFPatronFormat: Information about the patron format used for biometric data"
        cbeffPatronFormat.CapabilityValue = "ISO/IEC"
        cbeffPatronFormat.CapabilitySupportingValue = ""
        capabilityList.Add(cbeffPatronFormat)

        Dim classificationAlgorithm As New CapabilityType
        classificationAlgorithm.CapabilityName = 7
        classificationAlgorithm.CapabilityID = "7"
        classificationAlgorithm.CapabilityDescription = "ClassificationAlgorithmType: Classifying facial data is not done. Instead, biometric classification defaults to 'face' "
        classificationAlgorithm.CapabilityValue = "None"
        capabilityList.Add(classificationAlgorithm)

        Dim conformanceClass As New CapabilityType
        conformanceClass.CapabilityName = 8
        conformanceClass.CapabilityID = "8"
        conformanceClass.CapabilityDescription = "Class Conformance"
        conformanceClass.CapabilityValue = "1"
        capabilityList.Add(conformanceClass)

        Dim Gallery As New CapabilityType
        Gallery.CapabilityName = 9
        Gallery.CapabilityID = "9"
        Gallery.CapabilityDescription = "Gallery: Galleries supported by the implementing system"
        Dim galleryDir As String = Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).ToString).ToString & "\MasterDB\Galleries"
        Dim galleries = ""
        For Each Dir As String In System.IO.Directory.GetDirectories(galleryDir)
            Dim dirInfo As New System.IO.DirectoryInfo(Dir)
            galleries = galleries & dirInfo.Name & ", "
        Next
        Gallery.CapabilityValue = galleries
        capabilityList.Add(Gallery)

        Dim identityModel As New CapabilityType
        identityModel.CapabilityName = 10
        identityModel.CapabilityID = "10"
        identityModel.CapabilityDescription = "IdentityModel: Identification of person-centric or encounter-centric model."
        identityModel.CapabilityValue = "person"
        capabilityList.Add(identityModel)

        Dim comparisonAlgorithm As New CapabilityType 'Relevant to fusion, which isn't implemented yet.
        comparisonAlgorithm.CapabilityName = 11
        comparisonAlgorithm.CapabilityID = "11"
        comparisonAlgorithm.CapabilityDescription = "ComparisonAlgorithm"
        comparisonAlgorithm.CapabilityValue = ""
        comparisonAlgorithm.CapabilitySupportingValue = ""
        capabilityList.Add(comparisonAlgorithm)

        Dim comparisonScore As New CapabilityType
        comparisonScore.CapabilityName = 12
        comparisonScore.CapabilityID = "12"
        comparisonScore.CapabilityDescription = "ComparisonScore: Score threshold for successful identification"
        comparisonScore.CapabilityValue = "(10:100)"
        comparisonScore.CapabilitySupportingValue = "(0:9.999)"
        capabilityList.Add(comparisonScore)

        Dim qualityAlgorithm As New CapabilityType
        qualityAlgorithm.CapabilityName = 13
        qualityAlgorithm.CapabilityID = "13"
        qualityAlgorithm.CapabilityDescription = "QualityAlgorithm: No software version. Check Quality always returns 1, unless subjectID is not found."
        qualityAlgorithm.CapabilityValue = "None"
        qualityAlgorithm.CapabilitySupportingValue = "None"
        qualityAlgorithm.CapabilityAdditionalInfo = "None"
        capabilityList.Add(qualityAlgorithm)

        Dim supportedBiometric As New CapabilityType
        supportedBiometric.CapabilityName = 14
        supportedBiometric.CapabilityID = "14"
        supportedBiometric.CapabilityDescription = "SupportedBiometric: Information about supported biometric capabilities. In this model, identification and verification are supported."
        supportedBiometric.CapabilityValue = "face"
        supportedBiometric.CapabilitySupportingValue = "3" 'Identification + verification supported
        capabilityList.Add(supportedBiometric)

        Dim transformOperation As New CapabilityType
        transformOperation.CapabilityName = 15
        transformOperation.CapabilityID = "15"
        transformOperation.CapabilityDescription = "TransformOperation: Supported Transform Biometric Data Operations"
        transformOperation.CapabilityValue = "{1,2,3}"
        transformOperation.CapabilitySupportingValue = "{1,2,3}" 'Feature extraction, centring/cropping, biometric data format conversion
        capabilityList.Add(transformOperation)


        Dim queryCapabilitiesResponse As New QueryCapabilitiesResponsePackage()
        queryCapabilitiesResponse.ResponseStatus = New ResponseStatus
        queryCapabilitiesResponse.ResponseStatus.Return = "0"
        queryCapabilitiesResponse.ResponseStatus.Message = "Capability list returned."
        queryCapabilitiesResponse.CapabilityList = capabilityList
        Return queryCapabilitiesResponse

    End Function

    Public Function RetrieveBiographicData(RetrieveBiographicDataRequest As RetrieveBiographicDataRequest) As RetrieveBiographicDataResponsePackage Implements BIAS_v2.RetrieveBiographicData

        Dim retrieveBiogDataResponse As New RetrieveBiographicDataResponsePackage()
        retrieveBiogDataResponse.Identity = New BIASIdentity
        retrieveBiogDataResponse.ResponseStatus = New ResponseStatus
        Dim subjectID As String = RetrieveBiographicDataRequest.Identity.SubjectID
        Dim galleryID As String = RetrieveBiographicDataRequest.GalleryID

        If (RetrieveBiographicDataRequest.Identity.SubjectID Is Nothing) Then
            retrieveBiogDataResponse.ResponseStatus.Return = 9
            retrieveBiogDataResponse.ResponseStatus.Message = "The input subject ID is empty or in an invalid format."
            Return retrieveBiogDataResponse
        End If

        'if galleryid specified, then get the biog data from the subjectID held in that gallery
        Dim biographicData As New BiographicDataType
        biographicData.BiographicDataItemList = New BiographicDataItemListType

        If galleryID IsNot Nothing Then
            Dim subjectFileGalPath As String = Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).ToString).ToString _
                                                       & "\MasterDB\Galleries\" & galleryID & "\" & subjectID & "\" & subjectID & ".txt"

            Dim readText As List(Of String) = System.IO.File.ReadAllLines(subjectFileGalPath).ToList
            Dim biographicLineNum = readText.IndexOf("BiographicData:")
            Dim endBiographicLineNum = readText.IndexOf("BiometricData:")

            'create the biographic data items using the lines between the biographic and biometric data markers.
            For counter = biographicLineNum + 1 To endBiographicLineNum - 1
                Dim data = New BiographicDataItemType
                data.Name = readText(counter).Substring(0, readText(counter).IndexOf(":"))
                data.Value = readText(counter).Substring(readText(counter).IndexOf(":") + 1)
                biographicData.BiographicDataItemList.Add(data)
            Next
        Else 'get all biog data for the subjectID in all galleries available
            For Each Dir As String In Directory.GetDirectories(Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).ToString).ToString _
                                                       & "\MasterDB\Galleries\")
                galleryID = Dir
                If Directory.Exists(Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).ToString).ToString _
                                                       & "\MasterDB\Galleries\" & galleryID & "\" & subjectID) Then 'check if the subject record exists in the gallery

                    Dim subjectFileGalPath As String = Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).ToString).ToString _
                                                       & "\MasterDB\Galleries\" & galleryID & "\" & subjectID & "\" & subjectID & ".txt"

                    Dim readText As List(Of String) = System.IO.File.ReadAllLines(subjectFileGalPath).ToList
                    Dim biographicLineNum = readText.IndexOf("BiographicData:")
                    Dim endBiographicLineNum = readText.IndexOf("BiometricData:")

                    For counter = biographicLineNum + 1 To endBiographicLineNum - 1
                        Dim data = New BiographicDataItemType
                        data.Name = readText(counter).Substring(0, readText(counter).IndexOf(":"))
                        data.Value = readText(counter).Substring(readText(counter).IndexOf(":") + 1)
                        biographicData.BiographicDataItemList.Add(data)
                    Next
                End If
            Next
        End If


        retrieveBiogDataResponse.Identity.BiographicData = biographicData
        retrieveBiogDataResponse.ResponseStatus.Return = 0
        retrieveBiogDataResponse.ResponseStatus.Message = "Biographic data sucessfully returned."
        Return retrieveBiogDataResponse

    End Function

    Public Function RetrieveBiometricData(RetrieveBiometricDataRequest As RetrieveBiometricDataRequest) As RetrieveBiometricDataResponsePackage Implements BIAS_v2.RetrieveBiometricData
        Dim retrieveBiomDataResponse As New RetrieveBiometricDataResponsePackage()

        'Error Codes
        If (RetrieveBiometricDataRequest.Identity.SubjectID Is Nothing) Then
            retrieveBiomDataResponse.ResponseStatus.Return = 9
            retrieveBiomDataResponse.ResponseStatus.Message = "The input subject ID is empty or in an invalid format."
            Return retrieveBiomDataResponse
        End If

        Dim GalleryID = RetrieveBiometricDataRequest.GalleryID
        Dim SubjectID = RetrieveBiometricDataRequest.Identity.SubjectID
        Dim BiometricType = RetrieveBiometricDataRequest.BiometricType ' not implemented/used yet.

        retrieveBiomDataResponse.Identity = New BIASIdentity
        retrieveBiomDataResponse.Identity.BiometricData = New BIASBiometricDataType
        retrieveBiomDataResponse.Identity.BiometricData.BIRList = New CBEFF_BIR_ListType

        'read lines from text file
        Dim subjectFileGalPath As String = Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).ToString).ToString _
                                                       & "\MasterDB\Galleries\" & GalleryID & "\" & SubjectID & "\" & SubjectID & ".txt"
        Dim readText As List(Of String) = System.IO.File.ReadAllLines(subjectFileGalPath).ToList

        'Go through list, find the line # of ImageName instances
        Dim imageNameIndexes = New List(Of Integer)
        Dim lineNum = 0
        For Each line In readText
            If line.IndexOf("ImageName") = 0 Then
                imageNameIndexes.Add(lineNum)
            End If
            lineNum = lineNum + 1
        Next

        'Create CBEFF_BIR types based off the start of each image grouping in readText
        For Each index In imageNameIndexes

            Dim bir = New CBEFF_BIR_Type
            bir.BIR = New BaseBIRType
            'Image and Format
            Dim imageName = readText(index).Substring(10)
            Dim subjectGalleryFolderPath As String = Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).ToString).ToString _
                                                       & "\MasterDB\Galleries\" & GalleryID & "\" & SubjectID & "\"

            Dim biomImage = System.Drawing.Image.FromFile(subjectGalleryFolderPath & imageName)
            bir.BIR.biometricImage = ImageToBase64String(biomImage, ImageFormat.Png)
            bir.FormatOwner = readText(index + 1).Substring(12)
            bir.FormatType = readText(index + 2).Substring(11)
            'BIR DB
            bir.BIR_Information = New CBEFF_BIR_Type.BIR_InformationType
            bir.BIR_Information.BIR_Info = New BIRInfoType
            bir.BIR_Information.BIR_Info.Creator = readText(index + 3).Substring(8)
            bir.BIR_Information.BIR_Info.Index = readText(index + 4).Substring(10)
            Dim checkIntegrity As Boolean = readText(index + 5).Substring(10)
            bir.BIR_Information.BIR_Info.Integrity = checkIntegrity
            bir.BIR_Information.BIR_Info.Payload = New Byte() {readText(index + 6).Substring(8)}
            bir.BIR_Information.BIR_Info.CreationDate = readText(index + 7).Substring(13)
            bir.BIR_Information.BIR_Info.NotValidBefore = readText(index + 8).Substring(15)
            bir.BIR_Information.BIR_Info.NotValidAfter = readText(index + 9).Substring(14)
            'BDB DB
            bir.BIR_Information.BDB_Info = New BDBInfoType
            bir.BIR_Information.BDB_Info.Index = readText(index + 10).Substring(10)
            bir.BIR_Information.BDB_Info.Format = New RegistryIDType
            bir.BIR_Information.BDB_Info.Format.Organization = readText(index + 11).Substring(23)
            bir.BIR_Information.BDB_Info.Format.Type = readText(index + 12).Substring(15)
            bir.BIR_Information.BDB_Info.Encryption = readText(index + 13).Substring(11)
            bir.BIR_Information.BDB_Info.CreationDate = readText(index + 14).Substring(17)
            bir.BIR_Information.BDB_Info.NotValidBefore = readText(index + 15).Substring(19)
            bir.BIR_Information.BDB_Info.NotValidAfter = readText(index + 16).Substring(18)
            bir.BIR_Information.BDB_Info.Level = readText(index + 17).Substring(6)
            bir.BIR_Information.BDB_Info.Product = New RegistryIDType
            bir.BIR_Information.BDB_Info.Product.Organization = readText(index + 18).Substring(20)
            bir.BIR_Information.BDB_Info.Product.Type = readText(index + 19).Substring(12)
            bir.BIR_Information.BDB_Info.CaptureDevice = New RegistryIDType
            bir.BIR_Information.BDB_Info.CaptureDevice.Organization = readText(index + 20).Substring(26)
            bir.BIR_Information.BDB_Info.CaptureDevice.Type = readText(index + 21).Substring(18)
            bir.BIR_Information.BDB_Info.FeatureExtractionAlgorithm = New RegistryIDType
            bir.BIR_Information.BDB_Info.FeatureExtractionAlgorithm.Organization = readText(index + 22).Substring(39)
            bir.BIR_Information.BDB_Info.FeatureExtractionAlgorithm.Type = readText(index + 23).Substring(31)
            bir.BIR_Information.BDB_Info.ComparisonAlgorithm = New RegistryIDType
            bir.BIR_Information.BDB_Info.ComparisonAlgorithm.Organization = readText(index + 24).Substring(32)
            bir.BIR_Information.BDB_Info.ComparisonAlgorithm.Type = readText(index + 25).Substring(24)
            bir.BIR_Information.BDB_Info.CompressionAlgorithm = New RegistryIDType
            bir.BIR_Information.BDB_Info.CompressionAlgorithm.Organization = readText(index + 26).Substring(33)
            bir.BIR_Information.BDB_Info.CompressionAlgorithm.Type = readText(index + 27).Substring(25)
            bir.BIR_Information.BDB_Info.Purpose = readText(index + 28).Substring(8)
            'SB DB
            bir.BIR_Information.SB_Info = New SBInfoType
            bir.BIR_Information.SB_Info.Format = New RegistryIDType
            bir.BIR_Information.SB_Info.Format.Organization = readText(index + 29).Substring(22)
            bir.BIR_Information.SB_Info.Format.Type = readText(index + 30).Substring(14)

            'Add newly created CBEFF_BIR to CBEFF_BIR_LIST
            retrieveBiomDataResponse.Identity.BiometricData.BIRList.Add(bir)
        Next

        retrieveBiomDataResponse.ResponseStatus.Return = 0
        retrieveBiomDataResponse.ResponseStatus.Message = "The biometric records have been sucessfully retrieved."
        Return retrieveBiomDataResponse
    End Function

    Public Function RetrieveData(RetrieveDataRequest As RetrieveDataRequest) As RetrieveDataResponsePackage Implements BIAS_v2.RetrieveData

        Dim subjectID As String = RetrieveDataRequest.Identity.SubjectID
        Dim retrieveDataResponse As New RetrieveDataResponsePackage()
        Dim processingOptions As ProcessingOptionsType = RetrieveDataRequest.ProcessingOptions 'list of optionType, which are key/val pairs

        If (RetrieveDataRequest.Identity.SubjectID Is Nothing) Then
            retrieveDataResponse.ResponseStatus.Return = 9
            retrieveDataResponse.ResponseStatus.Message = "The input subject ID is empty or in an invalid format."
            Return retrieveDataResponse
        End If

        'Pull out all relavent information from the subject record file, to be used as needed.
        Dim subjectFilePath As String = Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).ToString).ToString & "\MasterDB\Subject Records\" & subjectID & "\" & subjectID & ".txt"
        Dim readText As List(Of String) = System.IO.File.ReadAllLines(subjectFilePath).ToList


        'possible options - [biographicData, basic] [biographicData, full] [biographicData count] [biometric data, number] [biometric data, full] [biometric data, count] [allData, basic] [allData, full]
        '[biographicData, basic] - GUID = SubjectID/IdentityClaim, GivenName + FamilyName = FirstName+LastNAme
        Dim dataType As String = processingOptions(0).Key
        Dim dataSelection As String = processingOptions(0).Value
        Dim returnInfoType As New InformationType
        If dataType = "biographicData" Then
            Dim identityClaimIndex, givenNameIndex, familyNameIndex, dateOfBirthIndex, sexIndex, citizenshipIndex As Integer

            'find the indexes of all non-biometric information
            For Each line In readText
                If line.StartsWith("IdentityClaim:") Then identityClaimIndex = readText.IndexOf(line)
                If line.StartsWith("GivenName:") Then givenNameIndex = readText.IndexOf(line)
                If line.StartsWith("FamilyName:") Then familyNameIndex = readText.IndexOf(line)
                If line.StartsWith("DateOfBirth:") Then dateOfBirthIndex = readText.IndexOf(line)
                If line.StartsWith("Sex:") Then sexIndex = readText.IndexOf(line)
                If line.StartsWith("Citizenship:") Then citizenshipIndex = readText.IndexOf(line)
            Next

            Dim GUID As String = readText(identityClaimIndex)
            Dim givenName As String = readText(givenNameIndex)
            Dim familyName As String = readText(familyNameIndex)
            Dim dateOfBirth As String = readText(dateOfBirthIndex)
            Dim sex As String = readText(sexIndex)
            Dim citizenship As String = readText(citizenshipIndex)

            'Options
            If dataSelection = "basic" Then
                returnInfoType.GUID = GUID
                returnInfoType.GivenName = givenName
                returnInfoType.FamilyName = familyName
            End If
            If dataSelection = "full" Then
                returnInfoType.GUID = GUID
                returnInfoType.GivenName = givenName
                returnInfoType.FamilyName = familyName
                returnInfoType.DateOfBirth = dateOfBirth
                returnInfoType.Sex = sex
                returnInfoType.Citizenship = citizenship
            End If
        End If
        If dataType = "biometricData" Then 'implement later
            If dataSelection = "Images" Then
                'return list of image names

                'Go through list, find the line # of ImageName instances
                Dim imageNameIndexes = New List(Of Integer)
                Dim lineNum = 0
                For Each line In readText
                    If line.IndexOf("ImageName") = 0 Then
                        imageNameIndexes.Add(lineNum)
                    End If
                    lineNum = lineNum + 1
                Next

                Dim imageList = New List(Of String)
                For Each index In imageNameIndexes
                    Dim imageName = readText(index).Substring(10)
                    imageList.Add(imageName)
                Next
            End If
            If dataSelection = "full" Then
                'return list of cbeff constructed items


            End If
        End If
        If dataType = "allData" Then 'implement later
        End If


        retrieveDataResponse.ReturnData = returnInfoType
        retrieveDataResponse.ResponseStatus = New ResponseStatus
        retrieveDataResponse.ResponseStatus.Return = 0
        retrieveDataResponse.ResponseStatus.Message = "Data sucessfully retrieved."
        Return retrieveDataResponse
    End Function

    Public Function SetBiographicData(SetBiographicDataRequest As SetBiographicDataRequest) As SetBiographicDataResponsePackage Implements BIAS_v2.SetBiographicData

        Console.WriteLine("In SetBiographicData")

        Dim setBiogDataResponse As New SetBiographicDataResponsePackage()
        setBiogDataResponse.ResponseStatus = New ResponseStatus

        'ADD OTHER RETURN CODES HERE LATER
        If (SetBiographicDataRequest.Identity.SubjectID Is Nothing) Then
            setBiogDataResponse.ResponseStatus.Return = 9
            setBiogDataResponse.ResponseStatus.Message = "The input subject ID is empty or in an invalid format."
            Return setBiogDataResponse
        End If
        If (SetBiographicDataRequest.Identity.BiographicData.BiographicDataItemList Is Nothing) Then
            setBiogDataResponse.ResponseStatus.Return = 13
            setBiogDataResponse.ResponseStatus.Message = "The biographic data format is either not known, not supported, or empty."
            Return setBiogDataResponse
        End If

        'Used to set the first set of biog data
        Dim dataList = New BiographicDataItemListType
        dataList = SetBiographicDataRequest.Identity.BiographicData.BiographicDataItemList

        Dim subjectID As String = SetBiographicDataRequest.Identity.SubjectID
        Dim firstName As String = SetBiographicDataRequest.Identity.BiographicData.FirstName
        Dim lastName As String = SetBiographicDataRequest.Identity.BiographicData.LastName

        'create new dictionary
        Dim biogDataDictionary As New Dictionary(Of String, String)
        biogDataDictionary.Add("FirstName:", firstName)
        biogDataDictionary.Add("LastName:", lastName)
        For Each dataItem In dataList
            biogDataDictionary.Add(dataItem.Name & ":", dataItem.Value)
        Next

        'read in file contents and append lines
        Dim subjectFile = Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).ToString).ToString & "\MasterDB\Subject Records\" & subjectID & "\" & subjectID & ".txt"
        Dim readText As List(Of String) = System.IO.File.ReadAllLines(subjectFile).ToList
        Dim biographicLineNum = readText.IndexOf("BiographicData:")
        Dim counter As Integer = 1
        For Each Item As KeyValuePair(Of String, String) In biogDataDictionary
            Dim dictionaryField As String = (Item.Key & Item.Value)
            readText.Insert(biographicLineNum + counter, dictionaryField)
            counter = counter + 1
        Next

        'delete current contents, then write new line text list to subject record
        System.IO.File.WriteAllText(subjectFile, "")
        Dim objWriter As New System.IO.StreamWriter(subjectFile)
        For Each line In readText
            objWriter.Write(line & Environment.NewLine)
        Next
        objWriter.Close()

        setBiogDataResponse.ResponseStatus.Return = 0
        setBiogDataResponse.ResponseStatus.Message = "Biographic Data sucessfully set."
        Return (setBiogDataResponse)
    End Function

    Public Function SetBiometricData(SetBiometricDataRequest As SetBiometricDataRequest) As SetBiometricDataResponsePackage Implements BIAS_v2.SetBiometricData

        Dim setBiomDataResponse As New SetBiometricDataResponsePackage()
        setBiomDataResponse.ResponseStatus = New ResponseStatus

        'ErrorCodeReturns
        'If no identity
        If (SetBiometricDataRequest.Identity.SubjectID Is Nothing) Then
            setBiomDataResponse.ResponseStatus.Return = 9
            setBiomDataResponse.ResponseStatus.Message = "The input subject ID is empty or in an invalid format."
            Return setBiomDataResponse
        End If


        Console.WriteLine("In SetBiometricData")

        'Used to set the first set of biom data
        Dim SubjectID = SetBiometricDataRequest.Identity.SubjectID
        Dim biomList = SetBiometricDataRequest.Identity.BiometricData.BIRList
        Dim subjectDirectory = Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).ToString).ToString & "\MasterDB\Subject Records\" & SubjectID & "\"
        For Each record In biomList

            Dim biomImage = ImageFromBase64String(record.BIR.biometricImage) 'convert base64 image string to reg image
            Dim biomImageType = record.BIR.biometricImageType

            'save this image in the subjectID record
            biomImage.Save(subjectDirectory & SubjectID & biomImageType & ".png")
            Dim imageName = SubjectID & biomImageType & ".png"

            'Open the text file
            Dim subjectFile = Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).ToString).ToString & "\MasterDB\Subject Records\" & SubjectID & "\" & SubjectID & ".txt"
            Dim readText As List(Of String) = System.IO.File.ReadAllLines(subjectFile).ToList
            Dim startLine = readText.Count + 1

            'Create CBEFF_BIR_TYPE attributes
            Dim formatOwner = 257
            Dim formatType = 1

            'Create dictionary from the 3 information DBs + formatOwner and formatType
            Dim biomDataDictionary As New Dictionary(Of String, String)
            biomDataDictionary.Add("ImageName:", imageName)
            biomDataDictionary.Add("FormatOwner:", formatOwner)
            biomDataDictionary.Add("FormatType:", formatType)
            biomDataDictionary.Add("Creator:", record.BIR_Information.BIR_Info.Creator)
            biomDataDictionary.Add("BIR_Index:", record.BIR_Information.BIR_Info.Index)
            biomDataDictionary.Add("Integrity:", record.BIR_Information.BIR_Info.Integrity)
            biomDataDictionary.Add("Payload:", record.BIR_Information.BIR_Info.Payload(0))
            biomDataDictionary.Add("CreationDate:", record.BIR_Information.BIR_Info.CreationDate)
            biomDataDictionary.Add("NotValidBefore:", record.BIR_Information.BIR_Info.NotValidBefore)
            biomDataDictionary.Add("NotValidAfter:", record.BIR_Information.BIR_Info.NotValidAfter)
            biomDataDictionary.Add("BDB_Index:", record.BIR_Information.BDB_Info.Index)
            biomDataDictionary.Add("BDB_FormatOrganization:", record.BIR_Information.BDB_Info.Format.Organization)
            biomDataDictionary.Add("BDB_FormatType:", record.BIR_Information.BDB_Info.Format.Type)
            biomDataDictionary.Add("Encryption:", record.BIR_Information.BDB_Info.Encryption)
            biomDataDictionary.Add("BDB_CreationDate:", record.BIR_Information.BDB_Info.CreationDate)
            biomDataDictionary.Add("BDB_NotValidBefore:", record.BIR_Information.BDB_Info.NotValidBefore)
            biomDataDictionary.Add("BDB_NotValidAfter:", record.BIR_Information.BDB_Info.NotValidAfter)
            biomDataDictionary.Add("Level:", record.BIR_Information.BDB_Info.Level)
            biomDataDictionary.Add("ProductOrganization:", record.BIR_Information.BDB_Info.Product.Organization)
            biomDataDictionary.Add("ProductType:", record.BIR_Information.BDB_Info.Product.Type)
            biomDataDictionary.Add("CaptureDeviceOrganization:", record.BIR_Information.BDB_Info.CaptureDevice.Organization)
            biomDataDictionary.Add("CaptureDeviceType:", record.BIR_Information.BDB_Info.CaptureDevice.Type)
            biomDataDictionary.Add("FeatureExtractionAlgorithmOrganization:", record.BIR_Information.BDB_Info.FeatureExtractionAlgorithm.Organization)
            biomDataDictionary.Add("FeatureExtractionAlgorithmType:", record.BIR_Information.BDB_Info.FeatureExtractionAlgorithm.Type)
            biomDataDictionary.Add("ComparisonAlgorithmOrganization:", record.BIR_Information.BDB_Info.ComparisonAlgorithm.Organization)
            biomDataDictionary.Add("ComparisonAlgorithmType:", record.BIR_Information.BDB_Info.ComparisonAlgorithm.Type)
            biomDataDictionary.Add("CompressionAlgorithmOrganization:", record.BIR_Information.BDB_Info.ComparisonAlgorithm.Organization)
            biomDataDictionary.Add("CompressionAlgorithmType:", record.BIR_Information.BDB_Info.ComparisonAlgorithm.Type)
            biomDataDictionary.Add("Purpose:", record.BIR_Information.BDB_Info.Purpose)
            biomDataDictionary.Add("SB_FormatOrganization:", record.BIR_Information.SB_Info.Format.Organization)
            biomDataDictionary.Add("SB_FormatType:", record.BIR_Information.SB_Info.Format.Type)
            biomDataDictionary.Add("", "")

            'Add dictionary contents to the readText list
            For Each Item As KeyValuePair(Of String, String) In biomDataDictionary
                Dim dictionaryField As String = (Item.Key & Item.Value)
                readText.Add(dictionaryField)
            Next

            'delete current contents, then write new line text list to subject record
            System.IO.File.WriteAllText(subjectFile, "")
            Dim objWriter As New System.IO.StreamWriter(subjectFile)
            For Each line In readText
                objWriter.Write(line & Environment.NewLine)
            Next
            objWriter.Close()

        Next

        setBiomDataResponse.ResponseStatus.Return = 0
        setBiomDataResponse.ResponseStatus.Message = "Biometric Data sucessfully set."
        Return setBiomDataResponse
    End Function

    Public Function TransformBiometricData(TransformBiometricDataRequest As TransformBiometricDataRequest) As TransformBiometricDataResponsePackage Implements BIAS_v2.TransformBiometricData

        Dim transformBiomDataResponse As New TransformBiometricDataResponsePackage()
        transformBiomDataResponse.ResponseStatus = New ResponseStatus

        Dim inputBiometricData As CBEFF_BIR_Type = TransformBiometricDataRequest.InputBIR
        Dim transformOperation = TransformBiometricDataRequest.TransformOperation
        Dim transformationControl As String = TransformBiometricDataRequest.TransformControl
        Dim outputBIR As New CBEFF_BIR_Type

        If transformOperation = 1 Then 'feature extraction

        End If
        If transformOperation = 2 Then 'centering images

        End If
        If transformOperation = 3 Then 'cropping image

        End If
        If transformOperation = 4 Then 'standard biometric data format conversion
        End If

        transformBiomDataResponse.OutputBIR = outputBIR
        transformBiomDataResponse.ResponseStatus.Return = 0
        Return transformBiomDataResponse
    End Function

    Public Function UpdateBiographicData(UpdateBiographicDataRequest As UpdateBiographicDataRequest) As UpdateBiographicDataResponsePackage Implements BIAS_v2.UpdateBiographicData
        'Used in person centric models
        Dim subjectID = UpdateBiographicDataRequest.Identity.SubjectID
        Dim biographicData = UpdateBiographicDataRequest.Identity.BiographicData.BiographicDataItemList 'list of updated key/value pairs

        'pull updated biographic data information out input identity
        Dim GUID As String = ""
        Dim givenName As String = ""
        Dim familyName As String = ""
        Dim dateOfBirth As String = ""
        Dim sex As String = ""
        Dim citizenship As String = ""

        For Each item In biographicData
            If item.Name = "GUID" Then
                GUID = item.Value
            ElseIf item.Name = "GivenName" Then
                givenName = item.Value
            ElseIf item.Name = "FamilyName" Then
                familyName = item.Value
            ElseIf item.Name = "DateOfBirth" Then
                dateOfBirth = item.Value
            ElseIf item.Name = "Sex" Then
                sex = item.Value
            ElseIf item.Name = "Citizenship" Then
                citizenship = item.Value
            End If
        Next

        'go through each line and replace the value with the new one if the new one is not nothing 
        Dim subjectPath = Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).ToString).ToString & "\MasterDB\Subject Records\" & subjectID & "\" & subjectID & ".txt"
        Dim readText As List(Of String) = System.IO.File.ReadAllLines(subjectPath).ToList
        For x As Integer = 0 To readText.Count() - 1
            Dim line As String = readText(x)
            If line.Contains("GUID:") Then
                If GUID IsNot Nothing And GUID IsNot "" Then
                    readText(x) = "GUID:" & GUID
                End If
            ElseIf line.Contains("GivenName:") Then
                If givenName IsNot Nothing And givenName IsNot "" Then
                    readText(x) = "GivenName:" & givenName
                End If
            ElseIf line.Contains("FamilyName:") Then
                If familyName IsNot Nothing And familyName IsNot "" Then
                    readText(x) = "FamilyName:" & familyName
                End If
            ElseIf line.Contains("DateOfBirth:") Then
                If dateOfBirth IsNot Nothing And dateOfBirth IsNot "" Then
                    readText(x) = "DateOfBirth:" & dateOfBirth
                End If
            ElseIf line.Contains("Sex:") Then
                If sex IsNot Nothing And sex IsNot "" Then
                    readText(x) = "Sex:" & sex
                End If
            ElseIf line.Contains("Citizenship:") Then
                If citizenship IsNot Nothing And citizenship IsNot "" Then
                    readText(x) = "Citizenship:" & citizenship
                End If
            End If
        Next

        'update subject record
        System.IO.File.WriteAllText(subjectPath, "")
        System.IO.File.WriteAllLines(subjectPath, readText)

        'update record in the various galleries
        'make a list of the galleries the file is in
        Dim galleryList As New List(Of String)
        For Each line In readText
            If line.Contains("GalleryID:") Then
                Dim contents As String = line.Substring(line.IndexOf(":") + 1)
                galleryList = contents.Split(",").ToList
            End If
        Next

        Dim readText2 As New List(Of String)
        'copy over the subject record, delete non-relevant gallery entries
        For Each Gallery In galleryList
            Dim subjectGalPath = Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).ToString).ToString & "\MasterDB\Galleries\" & Gallery & "\" & subjectID & "\" & subjectID & ".txt"
            File.Copy(subjectPath, subjectGalPath, True)
            readText2 = System.IO.File.ReadAllLines(subjectGalPath).ToList
            For x As Integer = 0 To readText2.Count() - 1
                Dim line = readText(x)
                If line.Contains("GalleryID:") Then
                    readText2(x) = "GalleryID:" & Gallery
                End If
            Next
            System.IO.File.WriteAllText(subjectGalPath, "")
            System.IO.File.WriteAllLines(subjectGalPath, readText2)
        Next

        Dim updateBiogDataResponse As New UpdateBiographicDataResponsePackage()
        updateBiogDataResponse.ResponseStatus = New ResponseStatus
        updateBiogDataResponse.ResponseStatus.Return = 0
        updateBiogDataResponse.ResponseStatus.Message = "Biographic data updated sucessfully."
        Return updateBiogDataResponse
    End Function

    Public Function UpdateBiometricData(UpdateBiometricDataRequest As UpdateBiometricDataRequest) As UpdateBiometricDataResponsePackage Implements BIAS_v2.UpdateBiometricData
        'Used in person centric models
        'Used in person centric models
        Dim updateBiomDataResponse As New UpdateBiometricDataResponsePackage()

        Dim subjectID = UpdateBiometricDataRequest.Identity.SubjectID
        Dim record = UpdateBiometricDataRequest.Identity.BiometricData.BIR
        Dim biomImage = ImageFromBase64String(record.BIR.biometricImage)
        Dim biomImageType = record.BIR.biometricImageType

        'Go into subject record
        Dim subjectDirectory = Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).ToString).ToString & "\MasterDB\Subject Records\" & subjectID
        Dim subjectFile = subjectDirectory & "\" & subjectID & ".txt"
        'replace image
        biomImage.Save(subjectDirectory & "\" & subjectID & biomImageType & ".png")
        Dim imageName = subjectID & biomImageType & ".png"

        'Create CBEFF_BIR_TYPE attributes
        Dim formatOwner = 257
        Dim formatType = 1

        'Make updated lines
        Dim biomDataDictionary As New Dictionary(Of String, String)
        biomDataDictionary.Add("FormatOwner:", formatOwner)
        biomDataDictionary.Add("FormatType:", formatType)
        biomDataDictionary.Add("Creator:", record.BIR_Information.BIR_Info.Creator)
        biomDataDictionary.Add("BIR_Index:", record.BIR_Information.BIR_Info.Index)
        biomDataDictionary.Add("Integrity:", record.BIR_Information.BIR_Info.Integrity)
        biomDataDictionary.Add("Payload:", record.BIR_Information.BIR_Info.Payload(0))
        biomDataDictionary.Add("CreationDate:", record.BIR_Information.BIR_Info.CreationDate)
        biomDataDictionary.Add("NotValidBefore:", record.BIR_Information.BIR_Info.NotValidBefore)
        biomDataDictionary.Add("NotValidAfter:", record.BIR_Information.BIR_Info.NotValidAfter)
        biomDataDictionary.Add("BDB_Index:", record.BIR_Information.BDB_Info.Index)
        biomDataDictionary.Add("BDB_FormatOrganization:", record.BIR_Information.BDB_Info.Format.Organization)
        biomDataDictionary.Add("BDB_FormatType:", record.BIR_Information.BDB_Info.Format.Type)
        biomDataDictionary.Add("Encryption:", record.BIR_Information.BDB_Info.Encryption)
        biomDataDictionary.Add("BDB_CreationDate:", record.BIR_Information.BDB_Info.CreationDate)
        biomDataDictionary.Add("BDB_NotValidBefore:", record.BIR_Information.BDB_Info.NotValidBefore)
        biomDataDictionary.Add("BDB_NotValidAfter:", record.BIR_Information.BDB_Info.NotValidAfter)
        biomDataDictionary.Add("Level:", record.BIR_Information.BDB_Info.Level)
        biomDataDictionary.Add("ProductOrganization:", record.BIR_Information.BDB_Info.Product.Organization)
        biomDataDictionary.Add("ProductType:", record.BIR_Information.BDB_Info.Product.Type)
        biomDataDictionary.Add("CaptureDeviceOrganization:", record.BIR_Information.BDB_Info.CaptureDevice.Organization)
        biomDataDictionary.Add("CaptureDeviceType:", record.BIR_Information.BDB_Info.CaptureDevice.Type)
        biomDataDictionary.Add("FeatureExtractionAlgorithmOrganization:", record.BIR_Information.BDB_Info.FeatureExtractionAlgorithm.Organization)
        biomDataDictionary.Add("FeatureExtractionAlgorithmType:", record.BIR_Information.BDB_Info.FeatureExtractionAlgorithm.Type)
        biomDataDictionary.Add("ComparisonAlgorithmOrganization:", record.BIR_Information.BDB_Info.ComparisonAlgorithm.Organization)
        biomDataDictionary.Add("ComparisonAlgorithmType:", record.BIR_Information.BDB_Info.ComparisonAlgorithm.Type)
        biomDataDictionary.Add("CompressionAlgorithmOrganization:", record.BIR_Information.BDB_Info.ComparisonAlgorithm.Organization)
        biomDataDictionary.Add("CompressionAlgorithmType:", record.BIR_Information.BDB_Info.ComparisonAlgorithm.Type)
        biomDataDictionary.Add("Purpose:", record.BIR_Information.BDB_Info.Purpose)
        biomDataDictionary.Add("SB_FormatOrganization:", record.BIR_Information.SB_Info.Format.Organization)
        biomDataDictionary.Add("SB_FormatType:", record.BIR_Information.SB_Info.Format.Type)

        'look for the biomImageType
        Dim readText As List(Of String) = System.IO.File.ReadAllLines(subjectFile).ToList
        Dim lineIndex = 0
        Dim startLineIndex
        For Each line In readText
            Dim imageLine = line.IndexOf(biomImageType)
            If imageLine > -1 Then
                startLineIndex = lineIndex + 1
            End If
            lineIndex = lineIndex + 1
        Next

        lineIndex = startLineIndex
        If startLineIndex IsNot Nothing Then
            For Each Item As KeyValuePair(Of String, String) In biomDataDictionary
                Dim dictionaryField As String = (Item.Key & Item.Value)
                readText(lineIndex) = dictionaryField
                lineIndex = lineIndex + 1
            Next
        End If
        System.IO.File.WriteAllLines(subjectFile, readText)

        'get the galleries
        Dim listGalleries = New List(Of String)
        Dim galleryString = ""
        For Each line In readText
            If line.IndexOf("GalleryID") = 0 Then
                galleryString = line.Substring(10)
                listGalleries = galleryString.Split(","c).ToList()
            End If
        Next

        'go into the galleries
        For Each Gallery In listGalleries
            Dim subjectGalFilepath = Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).ToString).ToString & "\MasterDB\Galleries\" & Gallery & "\" & subjectID
            Dim subjectGalFile = subjectGalFilepath & "\" & subjectID & ".txt"

            'Update text from text file
            System.IO.File.WriteAllLines(subjectGalFile, readText)

            'Update Image
            biomImage.Save(subjectGalFilepath & "\" & subjectID & biomImageType & ".png")

        Next


        updateBiomDataResponse.ResponseStatus = New ResponseStatus
        updateBiomDataResponse.ResponseStatus.Message = "Biometric Data Updated"
        updateBiomDataResponse.ResponseStatus.Return = 0
        Return updateBiomDataResponse
    End Function

    Public Function Verify(VerifyRequest As VerifyRequest) As VerifyResponsePackage Implements BIAS_v2.Verify
        Dim verifyResponse As New VerifyResponsePackage()
        Return verifyResponse
    End Function

    Public Function VerifySubject(VerifySubjectRequest As VerifySubjectRequest) As VerifySubjectResponsePackage Implements BIAS_v2.VerifySubject
        Dim verifySubjectResponse As New VerifySubjectResponsePackage()
        Return verifySubjectResponse
    End Function


    'Public Function GetPolicy() As IO.Stream Implements IPolicyRetriever.GetPolicy
    '    '<cross-domain-policy xsi:noNamespaceSchemaLocation="http://www.adobe.com/xml/schemas/PolicyFile.xsd">
    '    '<allow-access-from domain="twitter.com"/>
    '    '<allow-access-from domain="api.twitter.com"/>
    '    '<allow-access-from domain="search.twitter.com"/>
    '    '<allow-access-from domain="static.twitter.com"/>
    '    '<site-control permitted-cross-domain-policies="master-only"/>
    '    '<allow-http-request-headers-from domain="*.twitter.com" headers="*" secure="true"/>
    '    '</cross-domain-policy>

    '    Dim policy = "<cross-domain-policy>" &
    '                 "  <allow-access-from domain='*' />" &
    '                 "  <site-control permitted-cross-domain-policies='all' />" &
    '                 "  <allow-http-request-headers-from domain='*' headers='*' />" &
    '                 "</cross-domain-policy>"

    '    WebOperationContext.Current.OutgoingResponse.ContentType = "application/xml"
    '    Return New MemoryStream(Encoding.UTF8.GetBytes(policy))
    'End Function
End Class
