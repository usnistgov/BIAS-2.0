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
Imports Emgu.CV.Face.LBPHFaceRecognizer
Imports Emgu.Util
Imports Emgu.CV.Structure
Imports Emgu.CV.Util
Imports System.Drawing.Imaging

Module mainModule
    Sub testRetrieve()
        Dim bias1 As New BIAS_v2Client()

        Dim retrieveData As New RetrieveDataRequest
        retrieveData.Identity = New BIASIdentity
        retrieveData.Identity.SubjectID = "66622315"
        retrieveData.ProcessingOptions = New ProcessingOptionsType
        Dim testOption As New OptionType
        testOption.Key = "allData"
        testOption.Value = "full"
        retrieveData.ProcessingOptions.Add(testOption)
        Dim a As RetrieveDataResponsePackage = bias1.RetrieveData(retrieveData)
        MessageBox.Show(a.ReturnData.Sex)

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

        Dim retrieveBiomData As New RetrieveBiometricDataRequest
        retrieveBiomData.Identity = New BIASIdentity
        retrieveBiomData.Identity.SubjectID = "QKLJZBOU"
        retrieveBiomData.GalleryID = "1"
        'Dim a As RetrieveBiometricDataResponsePackage = bias1.RetrieveBiometricData(retrieveBiomData)

        Dim sampleInfo As New InformationType
        sampleInfo.GUID = "Tester"
        sampleInfo.GivenName = "Paul"
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

        biomRecord.BIR = New BaseBIRType
        Dim inputImage = System.Drawing.Image.FromFile("C:\Users\pyl\Pictures\Obama.jpg")
        Dim inputImageString = ImageToBase64String(inputImage, ImageFormat.Jpeg)
        biomRecord.BIR.biometricImage = inputImageString
        biomRecord.BIR.biometricImageType = "Face"

        testIdentity.BiometricData.BIRList.Add(biomRecord)

        Dim enrol1 As New EnrollRequest()
        enrol1.InputData = sampleInfo
        enrol1.Identity = testIdentity
        Dim es As EnrollResponsePackage = bias1.Enroll(enrol1)
    End Sub

    Sub testDeleteBiom()
        Dim bias1 As New BIAS_v2Client()
        Dim deleteRequest = New DeleteBiometricDataRequest
        deleteRequest.GalleryID = "4"
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

    Private Function ImageToBase64String(ByVal image As System.Drawing.Image, ByVal imageFormat As ImageFormat)
        Using memStream As New MemoryStream
            image.Save(memStream, imageFormat)
            Dim result As String = Convert.ToBase64String(memStream.ToArray())
            memStream.Close()
            Return result
        End Using
    End Function

    Sub testIdentify()
        Dim bias1 = New BIAS_v2Client
        Dim IdentifyRequest As IdentifyRequest = New IdentifyRequest
        IdentifyRequest.GalleryID = "1"
        IdentifyRequest.MaxListSize = 2
        IdentifyRequest.InputData = New InformationType
        IdentifyRequest.InputData.Images = New InformationType.ImagesType
        Dim inputImage = System.Drawing.Image.FromFile("C:\Users\pyl\Pictures\Obama.jpg")

        'Turn image into byte
        Dim mybytearray As Byte()
        Dim ms As System.IO.MemoryStream = New System.IO.MemoryStream
        inputImage.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg)
        mybytearray = ms.ToArray()

        'create image data to go into list
        Dim oasisImage = New OASIS.BIAS.V2.Image
        oasisImage.ImageData = mybytearray

        IdentifyRequest.InputData.Images.Add(oasisImage)

        'MessageBox.Show(base64Image.Length)

        Dim IdentifyResponse = New IdentifyResponsePackage
        IdentifyResponse = bias1.Identify(IdentifyRequest)


    End Sub

    Sub Main()

        Dim bias1 = New BIAS_v2Client
        'MessageBox.Show("Hello")
        'testRetrieve()
        'testEnroll()
        'testUpdateBiom()
        'testFacial()
        testEnroll()
        'testIdentify()
        'Enroll someone
        'Identify request with a picture
        'Send the picture and the recognizer object
        'get them identified
        'testDeleteBiom()


    End Sub
End Module


<ServiceModel.ServiceBehavior(IncludeExceptionDetailInFaults:=True, Namespace:="http://docs.oasis-open.org/bias/ns/bias-2.0/")>
Public Class BIAS_v2Client
    Implements BIAS_v2

    ''' <summary>
    ''' Creates a new subjectID, made up of 8 numbers, from 1-9
    ''' Used within Enroll while creating a new subject record from an input identity.
    ''' </summary>
    ''' <returns>sb.ToString() - The generated subjectID</returns>
    Public Function generateRandomID() As String
        Dim characters As String = "123456789"
        Dim rand As New Random
        Dim sb As New StringBuilder
        For charPos As Integer = 1 To 8
            Dim idx As Integer = rand.Next(0, 9)
            sb.Append(characters.Substring(idx, 1))
        Next
        Return sb.ToString()
    End Function

    ''' <summary>
    ''' Converts base64 strings back to images
    ''' </summary>
    ''' <returns>result - The transformed biometric sample image</returns>
    Function ImageFromBase64String(ByVal base64 As String)
        Using memStream As New MemoryStream(System.Convert.FromBase64String(base64))
            Dim result As System.Drawing.Image = System.Drawing.Image.FromStream(memStream)
            memStream.Close()
            Return result
        End Using
    End Function

    ''' <summary>
    ''' Converts a biometric image to base64
    ''' </summary>
    ''' <returns>result - The transformed biometric sample string</returns>
    Private Function ImageToBase64String(ByVal image As System.Drawing.Image, ByVal imageFormat As ImageFormat)
        Using memStream As New MemoryStream
            image.Save(memStream, imageFormat)
            Dim result As String = Convert.ToBase64String(memStream.ToArray())
            memStream.Close()
            Return result
        End Using
    End Function

    ''' <summary>
    ''' Detects the face in each image, crops around that region, adds it to a list of image
    ''' Detects the subjectID of each image, sends back a tuple of the paired images and subject IDs.
    ''' </summary>
    ''' <param name="imagePathList">List of image paths</param>
    ''' <param name="faceCascade">Used to detect face locations</param>
    ''' <returns>Tuple(Of Emgu.CV.Image(Of Gray, Byte)(), Integer())(images, labels) - Tuple containing cropped face images and the corresponding subjectIDs</returns>
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
            Dim subjectID = Convert.ToInt32(Path.Substring(Path.LastIndexOf("\") + 1, 8))
            Array.Resize(labels, labels.Length + 1)
            labels(labels.Length - 1) = subjectID
        Next

        Return New Tuple(Of Emgu.CV.Image(Of Gray, Byte)(), Integer())(images, labels)
    End Function

    ''' <summary>
    ''' Takes an input image, crops it around the face, then compares it against the stored images in the trainer. Returns the match with the best distance score.
    ''' </summary>
    ''' <param name="trainer">The previously created/updated trainer, holding all subject record images and subjectIDs</param>
    ''' <param name="faceCascade">Used to detect face locations</param>
    ''' <param name="identificationImage">Image that needs to be identified. Sent over via a client request.</param>
    ''' <param name="maxListSize">The number of candiates to be retured. NOTE: LBPHFaceRecognizer does not natively support returning multiple canidate distance figures.
    '''                           prediction() will only return either 0 or 1 matches, with the 1 match being the match with the largest distance figure. Future implementations
    '''                           should use identification methods that support multiple candidate match figure returns.</param>
    ''' <returns>nbr_predicted - the most likely match, with attributes for the subjectID and the distance score.</returns>
    Public Function prediction(trainer As Face.LBPHFaceRecognizer, faceCascade As CascadeClassifier, identificationImage As System.Drawing.Image, maxListSize As Long) As Emgu.CV.Face.LBPHFaceRecognizer.PredictionResult

        Dim originalImage As New Image(Of Gray, Byte)(identificationImage)

        'get the face area as a rectangle and create a new rectangle using the dimensions of the face rectangle
        Dim faceRegion As Rectangle() = faceCascade.DetectMultiScale(originalImage)
        Dim CropRect As New Rectangle(faceRegion(0).X, faceRegion(0).Y, faceRegion(0).Width, faceRegion(0).Height)

        'get the image from path, save in a new image variable. Also create a bitmap to save the cropped image in.
        Dim originalImage2 = identificationImage
        Dim CropImage = New Bitmap(CropRect.Width, CropRect.Height)

        'take the original image and crop it, using the CropRect dimensions.
        Using grp = Graphics.FromImage(CropImage)
            grp.DrawImage(originalImage2, New Rectangle(0, 0, CropRect.Width, CropRect.Height), CropRect, GraphicsUnit.Pixel)
        End Using

        Dim croppedImage As New Image(Of Gray, Byte)(CropImage)

        'save the image
        Dim croppedPath = Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).ToString).ToString & "\MasterDB\Subject Images\" & "crop.jpg"
        croppedImage.Save(croppedPath)

        'then the below executes, doing the same thing, but now with a cropped, grayscale version of the original image. 
        'read in each image we want to identify, then convert it to grayscale
        Dim predict_image_pilC As Mat = CvInvoke.Imread(croppedPath, CvEnum.LoadImageType.Color)
        Dim predict_image_pilG As New Mat()
        CvInvoke.CvtColor(predict_image_pilC, predict_image_pilG, CvEnum.ColorConversion.Bgr2Gray)

        'Detect the face in the image
        Dim nbr_predicted = trainer.Predict(predict_image_pilG)
        CvInvoke.Imshow("test", predict_image_pilG)

        Return nbr_predicted

    End Function

    ''' <summary>
    ''' Iterates through all subject records and appends the path of each image found to imagePathList
    ''' </summary>
    ''' <param name="path">The path of the subject records folder</param>
    ''' <returns>imagePathList - the list of all image paths in the subject record folder</returns>
    Public Function getImagePaths(path As DirectoryInfo) As List(Of String)
        Dim imagePathList As List(Of String) = New List(Of String)

        For Each currentFile In path.EnumerateFiles("*.*", SearchOption.AllDirectories)
            Dim fileName As String = currentFile.Name
            If Not fileName.EndsWith(".txt") Then
                Console.WriteLine(currentFile.FullName)
                imagePathList.Add(currentFile.FullName)
            End If
        Next

        Return imagePathList
    End Function

    ''' <summary>
    ''' Updates the existing trainer with the subject record database in case of changes after the trainer is created. 
    ''' Used every time that identification/verification functions are called. 
    ''' </summary>
    ''' <param name="trainer">The previously created trainer that needs to be updated</param>
    ''' <returns>trainer - the up to date trainer</returns>
    Public Function updateTrainer(trainer As Emgu.CV.Face.LBPHFaceRecognizer) As Emgu.CV.Face.LBPHFaceRecognizer

        'load the classifier file and create the Local Binary Patterns Histograms Face Recognizer
        Dim classifierFileDirectory = Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).ToString).ToString & "\haarcascade_frontalface_default.xml"
        Dim faceCascade = New CascadeClassifier(classifierFileDirectory)

        Dim subjectRecordsPath = Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).ToString).ToString & "\MasterDB\Subject Records\"
        Dim path As New IO.DirectoryInfo(subjectRecordsPath)
        Dim imagePathList As List(Of String) = getImagePaths(path)

        'get list of images and subject IDs
        Dim imageAndSubjectIDs As Tuple(Of Emgu.CV.Image(Of Gray, Byte)(), Integer()) = testTrain(imagePathList, faceCascade)
        Dim images = imageAndSubjectIDs.Item1
        Dim subjectIDs = imageAndSubjectIDs.Item2

        trainer.Update(images, subjectIDs)

        Return trainer

    End Function

    ''' <summary>
    ''' Adds a subject record to a specific gallery.
    ''' </summary>
    ''' <param name="AddSubjectToGalleryRequest">Request parameter containing subject record and gallery ID information</param>
    ''' <returns name="galleryResponse">Contains a response status with a return code and message</returns>
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
                'then we need to save the original read in text, but replace the existing galleryID line with it + the new galleryID. Then save that over the existing subject record folder subject file.

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

                    'add the biometric image
                    Dim biomImageType = AddSubjectToGalleryRequest.Identity.BiometricData.BIRList(0).BIR.biometricImageType
                    Dim subjectGalFolder As String = Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).ToString).ToString _
                                                       & "\MasterDB\Galleries\" & GalleryID & "\" & SubjectID & "\"
                    Dim bytes As Byte() = Convert.FromBase64String(AddSubjectToGalleryRequest.Identity.BiometricData.BIRList(0).BIR.biometricImageType)
                    Dim biomImageJPG As System.Drawing.Image
                    Using ms As New MemoryStream(bytes)
                        biomImageJPG = System.Drawing.Image.FromStream(ms)
                        biomImageJPG.Save(subjectGalFolder & SubjectID & biomImageType & ".jpg", ImageFormat.Jpeg)
                    End Using

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

                    'add the biometric image
                    Dim biomImageType = AddSubjectToGalleryRequest.Identity.BiometricData.BIRList(0).BIR.biometricImageType
                    Dim subjectGalFolder As String = Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).ToString).ToString _
                                                       & "\MasterDB\Galleries\" & GalleryID & "\" & SubjectID & "\"
                    Dim bytes As Byte() = Convert.FromBase64String(AddSubjectToGalleryRequest.Identity.BiometricData.BIRList(0).BIR.biometricImage)
                    Dim biomImageJPG As System.Drawing.Image
                    Using ms As New MemoryStream(bytes)
                        biomImageJPG = System.Drawing.Image.FromStream(ms)
                        biomImageJPG.Save(subjectGalFolder & SubjectID & biomImageType & ".jpg", ImageFormat.Jpeg)
                    End Using

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

    ''' <summary>
    ''' Returns a quality score for a given biometric or specified subject.
    ''' NOTE: This implementation only provides basic structure and is not meant to be fully functional. At this point, it returns sucessful quality
    '''       checks if either a subjectID or a BIR is part of the CheckQualityRequest. Full implementation will look much different.
    ''' </summary>
    ''' <param name="CheckQualityRequest">Request parameter containing a subjectID or a BIR record</param>
    ''' <returns name="qualityResponse">Contains a response status with return value of 0 or 1 indicating sucess, a quality score, the algorithm version, and a response message.</returns>
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

    ''' <summary>
    ''' Classifies a biometric sample. For example, a fingerprint biometric sample may be classified as a whorl, loop, or arch.
    ''' NOTE: For this example implementation, this function always returns face due to the nature of our biometric samples.
    ''' </summary>
    ''' <param name="ClassifyBiometricDataRequest">Request parameter that contains a biometric sample</param>
    ''' <returns name="classifyBioDataResponse">Response that contains a return code and a message containing the type of biometric</returns>
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

    ''' <summary>
    ''' Creates a model-neutral subject record
    ''' </summary>
    ''' <param name="CreateSubjectRequest">Request parameter. Does not hold anything. </param>
    ''' <returns name="createSubjectResponse">Contains the generated subjectID, a status code, and a message pertaining to the status code.</returns>
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

    ''' <summary>
    ''' Erases all biographic data associated with a subject. If a galleryID is specified, only biographic data from the specified gallery for the subject will be erased.
    ''' </summary>
    ''' <param name="DeleteBiographicDataRequest">Request parameter that holds a subject ID. Can hold a gallery ID.</param>
    ''' <returns name="deleteBiogDataResponse">Contains a return code and a message that pertains to the return code.</returns>
    Public Function DeleteBiographicData(DeleteBiographicDataRequest As DeleteBiographicDataRequest) As DeleteBiographicDataResponsePackage Implements BIAS_v2.DeleteBiographicData

        Console.WriteLine("In DeleteBiographicData")

        Dim SubjectID = DeleteBiographicDataRequest.Identity.SubjectID
        Dim GalleryID = DeleteBiographicDataRequest.GalleryID
        Dim deleteBiogDataResponse As New DeleteBiographicDataResponsePackage()
        deleteBiogDataResponse.ResponseStatus = New ResponseStatus()


        'Check to see if inputs are valid
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

    ''' <summary>
    ''' Erases all biometric data associated with a subject. If a galleryID is specified, only biometric data from the specified gallery for the subject will be erased. Does not support
    ''' the biometric type specification functionality, as biometric data will be front facing facial images.
    ''' </summary>
    ''' <param name="DeleteBiometricDataRequest">Request parameter that holds a subject ID. Can hold a gallery ID.</param>
    ''' <returns name="deleteBiomDataResponse">Contains a return code and a message that pertains to the return code.</returns>
    Public Function DeleteBiometricData(DeleteBiometricDataRequest As DeleteBiometricDataRequest) As DeleteBiometricDataResponsePackage Implements BIAS_v2.DeleteBiometricData

        Console.WriteLine("In DeleteBiometricData")

        Dim deleteBiomDataResponse As New DeleteBiometricDataResponsePackage()
        Dim GalleryID = ""
        Dim SubjectID = ""
        Dim opt = DeleteBiometricDataRequest.BiometricType

        'Check to see if inputs exist.
        If (DeleteBiometricDataRequest.GalleryID IsNot Nothing) Then
            GalleryID = DeleteBiometricDataRequest.GalleryID
        End If
        If (DeleteBiometricDataRequest.Identity.SubjectID IsNot Nothing) Then
            SubjectID = DeleteBiometricDataRequest.Identity.SubjectID
        Else
            'INVALID_SUBJECT_ID=9
            deleteBiomDataResponse.ResponseStatus.Return = 9
            deleteBiomDataResponse.ResponseStatus.Message = "The input subject ID is empty or in an invalid format."
            Return deleteBiomDataResponse
        End If

        'Define file paths
        Dim subjectPath = Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).ToString).ToString & "\MasterDB\Subject Records\" & SubjectID
        Dim subjectFile As String = subjectPath & "\" & SubjectID & ".txt"

        'Check if the subject record exists
        If (Directory.Exists(subjectPath) = False) Then
            'UNKNOWN_SUBJECT=10
            deleteBiomDataResponse.ResponseStatus.Return = 9
            deleteBiomDataResponse.ResponseStatus.Message = "The subject referenced by the input subject ID does not exist."
            Return deleteBiomDataResponse
        End If


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
            'UNSUPPORTED_CAPABILITY=2
            deleteBiomDataResponse.ResponseStatus.Return = 24
            deleteBiomDataResponse.ResponseStatus.Message = "The biometric type capability is not supported by the service implementation, as all images are front facing facial images."
            Return deleteBiomDataResponse
        End If

        Return deleteBiomDataResponse
    End Function

    ''' <summary>
    ''' Deletes an existing subject record from the subject records folder, as well as all galleries.
    ''' </summary>
    ''' <param name="DeleteSubjectRequest">Request parameter that holds a subject ID.</param>
    ''' <returns name="deleteSubjectResponse">Contains a return code and a message that pertains to the return code.</returns>
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

    ''' <summary>
    ''' Deletes an existing subject record from a gallery using a subjectID or an identity claim. NOTE: In this implementation, subjectIDs and identity claims are the same.
    ''' </summary>
    ''' <param name="DeleteSubjectFromGalleryRequest">Request parameter that holds a subject ID or a identity claim, and a gallery ID.</param>
    ''' <returns name="deleteSubjectGalleryResponse">Contains a return code and a message that pertains to the return code.</returns>
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

    ''' <summary>
    ''' Enroll identifies the input subject, and if it exists, it updates the existing record. 
    ''' If the subject does not exist, it generates a new subject record using the attributes within the information type using various BIAS functions.
    ''' </summary>
    ''' <param name="EnrollRequest">Request parameter containing processing options and an information type. </param>
    ''' <returns name="enrollResponse">Response parameter holding the generated identity, a code indicating success, and a cooresponding message.</returns>
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
        setBiom.Identity = EnrollRequest.Identity
        setBiom.Identity.SubjectID = subjectID
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
        enrollResponse.ResponseStatus.Return = 0
        enrollResponse.ResponseStatus.Message = "Participant Enrolled"
        Return enrollResponse
    End Function

    ''' <summary>
    ''' Retrieves the enrolment results for the specified token. This service is used in conjunction with the Enroll aggregate service. If the Enrol aggregate service is
    ''' implemented as an asynchronous service, the implementing system returns a token, and the Get Enroll Results service is used to poll for the results of the original Enrol request.
    ''' NOTE: Because this example is implemented as a synchronous system, this service is not supported.
    ''' </summary>
    ''' <param name="GetEnrollResultsRequest">Request parameter that contains the token - the value used to retrieve the results of an Enroll request.</param>
    ''' <returns name="enrollResultsResponse">Return parameter that contains the code and message signifying that the service is not implemented.</returns>
    Public Function GetEnrollResults(GetEnrollResultsRequest As GetEnrollResultsRequest) As GetEnrollResultsResponsePackage Implements BIAS_v2.GetEnrollResults
        Dim enrollResultsResponse As New GetEnrollResultsResponsePackage()
        enrollResultsResponse.ResponseStatus = New ResponseStatus
        enrollResultsResponse.ResponseStatus.Return = 24
        enrollResultsResponse.ResponseStatus.Message = "The requested service/function is not implemented due to the synchronous implementation of the system."
        Return enrollResultsResponse
    End Function

    ''' <summary>
    ''' Retrieves the identification results for the specified token. This service is used in conjunction with the Identify aggregate service. 
    ''' If the Identify aggregate service is implemented as an asynchronous service, the implementing system returns a non-zero token, and the 
    ''' Get Identify Results service is used to poll for the results of the original Identify request.
    ''' NOTE: Because this example is implemented as a synchronous system, this service is not supported.
    ''' </summary>
    ''' <param name="GetIdentifyResultsRequest">Request parameter that contains the token - the value used to retrieve the results of an Identify request.</param>
    ''' <returns name="identityResultsResponse">Return parameter that contains the code and message signifying that the service is not implemented.</returns>
    Public Function GetIdentifyResults(GetIdentifyResultsRequest As GetIdentifyResultsRequest) As GetIdentifyResultsResponsePackage Implements BIAS_v2.GetIdentifyResults
        Dim identityResultsResponse As New GetIdentifyResultsResponsePackage()
        identityResultsResponse.ResponseStatus = New ResponseStatus
        identityResultsResponse.ResponseStatus.Return = 24
        identityResultsResponse.ResponseStatus.Message = "The requested service/function is not implemented due to the synchronous implementation of the system."
        Return identityResultsResponse
    End Function

    ''' <summary>
    ''' Retrieves the identification results for the specified token. This service is used in conjunction with the Identify Subject service. 
    ''' If the Identify Subject service is implemented as an asynchronous service, the implementing system returns a token, and the 
    ''' Get Identify Subject Results service is used to poll for the results of the original Identify Subject request.
    ''' NOTE: Because this example is implemented as a synchronous system, this service is not supported.
    ''' </summary>
    ''' <param name="GetIdentifySubjectResultsRequest">Request parameter that contains the token - the value used to retrieve the results of an Identify Subject request.</param>
    ''' <returns name="identitySubjectResultsResponse">Return parameter that contains the code and message signifying that the service is not implemented.</returns>
    Public Function GetIdentifySubjectResults(GetIdentifySubjectResultsRequest As GetIdentifySubjectResultsRequest) As GetIdentifySubjectResultsResponsePackage Implements BIAS_v2.GetIdentifySubjectResults
        Dim identitySubjectResultsResponse As New GetIdentifySubjectResultsResponsePackage()
        identitySubjectResultsResponse.ResponseStatus = New ResponseStatus
        identitySubjectResultsResponse.ResponseStatus.Return = 24
        identitySubjectResultsResponse.ResponseStatus.Message = "The requested service/function is not implemented due to the synchronous implementation of the system."
        Return identitySubjectResultsResponse
    End Function

    ''' <summary>
    ''' Retrieves the verification results for the specified token. This service is used in conjunction with the Verify aggregate service. If the Verify aggregate service is
    ''' implemented as an asynchronous service, the implementing system returns a non-zero token, and the Get Verify Results service is used to poll for the results of the original Verify request.
    ''' NOTE: Because this example is implemented as a synchronous system, this service is not supported.
    ''' </summary>
    ''' <param name="GetVerifyResultsRequest">Request parameter that contains the token - the value used to retrieve the results of an Verify request.</param>
    ''' <returns name="verifyResultsResponse">Return parameter that contains the code and message signifying that the service is not implemented.</returns>
    Public Function GetVerifyResults(GetVerifyResultsRequest As GetVerifyResultsRequest) As GetVerifyResultsResponsePackage Implements BIAS_v2.GetVerifyResults
        Dim verifyResultsResponse As New GetVerifyResultsResponsePackage
        verifyResultsResponse.ResponseStatus = New ResponseStatus
        verifyResultsResponse.ResponseStatus.Return = 24
        verifyResultsResponse.ResponseStatus.Message = "The requested service/function is not implemented due to the synchronous implementation of the system."
        Return verifyResultsResponse
    End Function

    ''' <summary>
    ''' Uses LBPHFaceRecognizer of OpenCV to do subject identification of an input biometric image. Returns a BIASIdentity of the most likely subject match.
    ''' NOTE: Due to the limitations of LBPHFaceRecognizer, specifying the number of candidates in the input maxListSize attribute is not supported. Instead,
    ''' only the most likely match is returned.
    ''' </summary>
    ''' <param name="ProcessingOptions"></param>
    ''' <param name="GalleryID">OPTIONAL - The identifier of the gallery which will be searched.</param>
    ''' <param name="maxListSize">The maximum size of the candidate list that should be returned. NOTE: Not supported due to limitations of LBPHFaceRecognizer.</param>
    ''' <param name="InputData">contains an input data record, which at a minimum must include biometric data</param>
    ''' <returns name="candidateList">A rank-ordered list of candidates(limited to 1 in this implementation) that have a likelihood of matchingthe input biometric sample</returns>
    ''' <returns name="Return">Holds a code signifying success (0) or one of 39 error codes. </returns>
    ''' <returns name="candidateList">Message cooresponding to the Return parameter</returns>
    Public Function Identify(IdentifyRequest As IdentifyRequest) As IdentifyResponsePackage Implements BIAS_v2.Identify

        Dim identifyResponse As New IdentifyResponsePackage()

        'added until Identify is finalized by Paul.
        Dim newIdentity As New BIASIdentity
        identifyResponse.Identity = newIdentity

        identifyResponse.ResponseStatus = New ResponseStatus

        'Since synchronous, limited processing options to specifying which galleries to look at. 0 = All galleries, Otherwise need the folder/gallery name
        'May cross over with GalleryID, check if GalleryID is nothing, if so, use processing options value as galleryID
        Dim galleryID As String = IdentifyRequest.GalleryID
        If galleryID Is Nothing Then
            galleryID = "1" 'default for demonstration purposes
        End If

        'check if gallery exists.
        Dim galleriesPath = Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).ToString).ToString & "\MasterDB\Galleries"
        If (Directory.Exists(galleriesPath & "\" & galleryID) = False) Then
            identifyResponse.ResponseStatus.Return = 11
            identifyResponse.ResponseStatus.Message = "The gallery referenced by the input gallery ID does not exist."
            Return identifyResponse
        End If

        'Create the trainer. Need to create each time Identify is done due to variability of galleryID

        Dim trainer = New IdentifyTrainer()

        trainer.setGalleryID(galleryID)

        Dim identifyTrainer = trainer.createTrainer()
        MessageBox.Show("12312")

        'In our case, inputData will only include the image. No other information will be used for identifcation
        'Load and convert byte() to image.
        Dim inputData As Byte() = IdentifyRequest.InputData.Images(0).ImageData

        Dim inputImage As System.Drawing.Image

        Dim ms As System.IO.MemoryStream = New System.IO.MemoryStream(inputData)
        inputImage = System.Drawing.Image.FromStream(ms)

        'create the faceCascade

        Dim cascadeFilePath = Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).ToString).ToString & "\haarcascade_frontalface_default.xml"

        Dim faceCascade = New CascadeClassifier(cascadeFilePath)

        'get the max list size
        Dim maxListSize As Long = IdentifyRequest.MaxListSize

        Dim predictionResult = prediction(identifyTrainer, faceCascade, inputImage, maxListSize)
        Dim predictSubjectID = predictionResult.Label
        Dim predictDistance = predictionResult.Distance

        Dim matchedIdentity As New BIASIdentity
        Dim candidateList = New CandidateListType
        'if the distance is above 5, probable match. If not, reject match, return empty identity
        If (predictDistance > 5) Then

            Dim candidate = New CandidateType
            candidate.Identity = New BIASIdentity
            candidate.Identity.SubjectID = predictSubjectID
            candidate.Rank = 1
            candidate.ScoreList = New CandidateType.ScoreListType
            Dim matchScore = New ScoreType
            matchScore.Value = predictDistance
            candidate.ScoreList.Score = matchScore
            candidateList.Add(candidate)

            Dim predictionString = "Input image is most likely Subject " & predictionResult.Label & ", with a distance score of " & predictionResult.Distance
            identifyResponse.ResponseStatus.Return = 0
            identifyResponse.ResponseStatus.Message = predictionString
        Else
            identifyResponse.ResponseStatus.Return = 34
            identifyResponse.ResponseStatus.Message = "Cannot perform a 1:N identification of the supplied and/or stored data."
        End If

        identifyResponse.CandidateList = candidateList
        Return identifyResponse
    End Function

    ''' <summary>
    ''' Uses LBPHFaceRecognizer of OpenCV to do subject identification of an input biometric image. Returns a BIASIdentity of the most likely subject match.
    ''' NOTE: Due to the limitations of LBPHFaceRecognizer, specifying the number of candidates in the input maxListSize attribute is not supported. Instead,
    ''' only the most likely match is returned.
    ''' </summary>
    ''' <param name="ProcessingOptions"></param>
    ''' <param name="GalleryID">OPTIONAL - The identifier of the gallery which will be searched.</param>
    ''' <param name="maxListSize">The maximum size of the candidate list that should be returned. NOTE: Not supported due to limitations of LBPHFaceRecognizer.</param>
    ''' <param name="biometricImage">The biometric image, stored as a base64 string.</param>
    ''' <returns name="candidateList">A rank-ordered list of candidates(limited to 1 in this implementation) that have a likelihood of matchingthe input biometric sample</returns>
    ''' <returns name="Return">Holds a code signifying success (0) or one of 39 error codes. </returns>
    ''' <returns name="candidateList">Message cooresponding to the Return parameter</returns>
    Public Function IdentifySubject(IdentifySubjectRequest As IdentifySubjectRequest) As IdentifySubjectResponsePackage Implements BIAS_v2.IdentifySubject
        Dim identifySubjectResponse = New IdentifySubjectResponsePackage
        identifySubjectResponse.ResponseStatus = New ResponseStatus

        'Since synchronous, limited processing options to specifying which galleries to look at. 0 = All galleries, Otherwise need the folder/gallery name
        'May cross over with GalleryID, check if GalleryID is nothing, if so, use processing options value as galleryID
        Dim galleryID As String = IdentifySubjectRequest.GalleryID
        If galleryID Is Nothing Then
            'Do identification against the input gallery.
            Dim inputGallery = IdentifySubjectRequest.Gallery

            Dim trainer = New IdentifySubjectTrainer()
            trainer.setGallery(inputGallery)
            Dim ISTrainer = trainer.createTrainer()

            'In our case, Identity will only hold the biometric image string. May be different in other implementations.
            'Load and convert base64 string to image.
            Dim inputData As String = IdentifySubjectRequest.Identity.BiometricData.BIR.BIR.biometricImage
            Dim inputImage As System.Drawing.Image = ImageFromBase64String(inputData)

            'create the faceCascade
            Dim cascadeFilePath = Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).ToString).ToString & "\haarcascade_frontalface_default.xml"
            Dim faceCascade = New CascadeClassifier(cascadeFilePath)

            Dim maxListSize As Long = IdentifySubjectRequest.MaxListSize

            Dim predictionResult = prediction(ISTrainer, faceCascade, inputImage, maxListSize)
            Dim predictSubjectID = predictionResult.Label
            Dim predictDistance = predictionResult.Distance

            Dim matchedIdentity As New BIASIdentity
            Dim candidateList = New CandidateListType

            'if the distance is above 5, probable match. If not, reject match, return empty identity
            If (predictDistance > 5) Then

                Dim candidate = New CandidateType
                candidate.Identity = New BIASIdentity
                candidate.Identity.SubjectID = predictSubjectID
                candidate.Rank = 1
                candidate.ScoreList = New CandidateType.ScoreListType
                Dim matchScore = New ScoreType
                matchScore.Value = predictDistance
                candidate.ScoreList.Score = matchScore
                candidateList.Add(candidate)

                Dim predictionString = "Input image is most likely Subject " & predictionResult.Label & ", with a distance score of " & predictionResult.Distance
                identifySubjectResponse.ResponseStatus.Return = 0
                identifySubjectResponse.ResponseStatus.Message = predictionString
            Else
                identifySubjectResponse.ResponseStatus.Return = 34
                identifySubjectResponse.ResponseStatus.Message = "Cannot perform a 1:N identification of the supplied and/or stored data."
            End If

            identifySubjectResponse.IdentifySubjectResult.CandidateList = candidateList
            Return identifySubjectResponse

        Else

            'Do normal identification against the galleryID provided.

            'check if gallery exists.
            Dim galleriesPath = Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).ToString).ToString & "\MasterDB\Galleries"
            If (Directory.Exists(galleriesPath & "\" & galleryID) = False) Then
                identifySubjectResponse.ResponseStatus.Return = 11
                identifySubjectResponse.ResponseStatus.Message = "The gallery referenced by the input gallery ID does not exist."
                Return identifySubjectResponse
            End If

            'Create the trainer. Need to create each time Identify is done due to variability of galleryID
            Dim globalTrainer = New IdentifyTrainer()
            globalTrainer.setGalleryID(galleryID)
            Dim trainer = globalTrainer.createTrainer()

            'In our case, Identity will only hold the biometric image string. May be different in other implementations.
            'Load and convert base64 string to image.
            Dim inputData As String = IdentifySubjectRequest.Identity.BiometricData.BIR.BIR.biometricImage
            Dim inputImage As System.Drawing.Image = ImageFromBase64String(inputData)

            'create the faceCascade
            Dim cascadeFilePath = Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).ToString).ToString & "\haarcascade_frontalface_default.xml"
            Dim faceCascade = New CascadeClassifier(cascadeFilePath)

            Dim maxListSize As Long = IdentifySubjectRequest.MaxListSize

            Dim predictionResult = prediction(trainer, faceCascade, inputImage, maxListSize)
            Dim predictSubjectID = predictionResult.Label
            Dim predictDistance = predictionResult.Distance

            Dim matchedIdentity As New BIASIdentity
            Dim candidateList = New CandidateListType
            'if the distance is above 5, probable match. If not, reject match, return empty identity
            If (predictDistance > 5) Then

                Dim candidate = New CandidateType
                candidate.Identity = New BIASIdentity
                candidate.Identity.SubjectID = predictSubjectID
                candidate.Rank = 1
                candidate.ScoreList = New CandidateType.ScoreListType
                Dim matchScore = New ScoreType
                matchScore.Value = predictDistance
                candidate.ScoreList.Score = matchScore
                candidateList.Add(candidate)

                Dim predictionString = "Input image is most likely Subject " & predictionResult.Label & ", with a distance score of " & predictionResult.Distance
                identifySubjectResponse.ResponseStatus.Return = 0
                identifySubjectResponse.ResponseStatus.Message = predictionString
            Else
                identifySubjectResponse.ResponseStatus.Return = 34
                identifySubjectResponse.ResponseStatus.Message = "Cannot perform a 1:N identification of the supplied and/or stored data."
            End If

            identifySubjectResponse.IdentifySubjectResult.CandidateList = candidateList
            Return identifySubjectResponse

        End If

    End Function

    ''' <summary>
    ''' Lists the biographic data elements stored for a subject using the Biographic Data Elements list. Does not return biographic data values, only the biographic data categories.
    ''' For example, "Family Name", "Sex", "Citizenship"
    ''' </summary>
    ''' <param name="SubjectID">The identifier of the subject</param>
    ''' <returns name="BiographicData">A list of biographic data item categories.</returns>
    ''' <returns name="Return">Holds a code signifying success (0) or one of 39 error codes. </returns>
    ''' <returns name="candidateList">Message cooresponding to the Return parameter</returns>
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

    ''' <summary>
    ''' Lists the biometric data elements stored for a subject using the Biographic Data Elements list. Does not return biometric data values.
    ''' NOTE: All biometric data in this implementation are facial images. Thus, it returns a list of elements cooresponding to the number of images in a subject record folder, with the values
    ''' for Type, Subtype, FormatOwner, and FormatType all being the same ("Face", "Front", 257, 1)
    ''' </summary>
    ''' <param name="SubjectID">The identifier of the subject</param>
    ''' <param name="ListFilter">An optional filter indicating what type of biometric information should be returned. Not supported in this implementation because of the singular nature of the biometric data.</param>
    ''' <returns name="BiometricDataList">A list of biometric data item labels.</returns>
    ''' <returns name="Return">Holds a code signifying success (0) or one of 39 error codes. </returns>
    ''' <returns name="Message">Message cooresponding to the Return parameter</returns>
    Public Function ListBiometricData(ListBiometricDataRequest As ListBiometricDataRequest) As ListBiometricDataResponsePackage Implements BIAS_v2.ListBiometricData
        Dim subjectID As String = ListBiometricDataRequest.Identity.SubjectID
        Dim listFilter = ListBiometricDataRequest.ListFilter

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
                bioType.BiometricSubType.Nodes.SetValue("Front", 0)
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

    ''' <summary>
    ''' Shall accept either comparison score or comparison decision information and create a fused comparison result.
    ''' <param name="FusionInput">Score or decision input information to the fusion method for each identity</param>
    ''' <returns name="Return">Holds a code signifying success (0) or one of 39 error codes. </returns>
    ''' <returns name="Match">Indicates the result of the fusion method </returns>
    ''' </summary>
    Public Function PerformFusion(PerformFusionRequest As PerformFusionRequest) As PerformFusionResponsePackage Implements BIAS_v2.PerformFusion
        Dim performFusionResponse As New PerformFusionResponsePackage()
        performFusionResponse.ResponseStatus = New ResponseStatus


        'Fusion input is a list of fusion identity lists
        'These fusion identity lists contain multiple fusion information types
        'These fusion information types are normally seperate, but with fusion, we compile them into the fusion identity list, creating a multiple source comparison result.

        For Each fusionIdentity In PerformFusionRequest.FusionInput
            For Each fusionInformation In fusionIdentity
                'Going to do Multi-Sample Fusion
                'fusion of multiple samples (images) acquired from the same source, such as multiple images of a single fingerprint, images of the same face, or recordings of a speaker
                'Within this, we're going to do score level fusion - which takes the similarity scores and combines them. 
                'Just average them together?
                'Or we could do sample level fusion


            Next
        Next

        Return performFusionResponse
    End Function

    ''' <summary>
    ''' Returns a list of the capabilities, options, galleries, etc. that are supported by the BIAS implementation.
    ''' NOTE: Reference Table 1 in ISO/IEC 30108-1 to view a list of capabilities.
    ''' <returns name="Return">Holds a code signifying success (0) or one of 39 error codes. </returns>
    ''' <returns name="CapabilityList">A list of capabilities supported by the BIAS implementation</returns>
    ''' </summary>
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

    ''' <summary>
    ''' Retrieve the list of biographic data associated with a subjectID. Differes from listBiographicData in that it returns the stored values as well.
    ''' If no galleryID is specified, a list of biographic data from all galleries shall be returned.
    ''' <param name="SubjectID">the identifier of the subject</param>
    ''' <param name="GalleryID">the identifier of the gallery or population group from which the biometric information will be retrieved</param>
    ''' <returns name="Return">Holds a code signifying success (0) or one of 39 error codes.</returns>
    ''' <returns name="Message">Message cooresponding to the Return parameter</returns>
    ''' <returns name="BiographicData">A list of biographic data associated with the subject</returns>
    ''' </summary>
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

    ''' <summary>
    ''' Retrieve the list of biometric data associated with a subjectID. Differes from listBiometricData in that it returns the stored values as well.
    ''' If no galleryID is specified, a list of biometric data from all galleries shall be returned.
    ''' <param name="SubjectID">the identifier of the subject</param>
    ''' <param name="GalleryID">the identifier of the gallery or population group from which the biometric information will be retrieved</param>
    ''' <returns name="Return">Holds a code signifying success (0) or one of 39 error codes.</returns>
    ''' <returns name="Message">Message cooresponding to the Return parameter</returns>
    ''' <returns name="BiometricData">A list of biometric data associated with the subject</returns>
    ''' </summary>
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


        If GalleryID IsNot Nothing Then

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
                Dim subjectRecordFolderPath As String = Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).ToString).ToString _
                                                           & "\MasterDB\Subject Records\" & SubjectID & "\"

                Dim biomImage = System.Drawing.Image.FromFile(subjectRecordFolderPath & imageName)
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
        Else 'get all biom data for the subjectID in all galleries available
            For Each Dir As String In Directory.GetDirectories(Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).ToString).ToString _
                                                       & "\MasterDB\Galleries\")
                GalleryID = Dir
                If Directory.Exists(Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).ToString).ToString _
                                                       & "\MasterDB\Galleries\" & GalleryID & "\" & SubjectID) Then 'check if the subject record exists in the gallery

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
                        Dim subjectRecordFolderPath As String = Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).ToString).ToString _
                                                                   & "\MasterDB\Subject Records\" & SubjectID & "\"

                        Dim biomImage = System.Drawing.Image.FromFile(subjectRecordFolderPath & imageName)
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
                End If
            Next
        End If




        retrieveBiomDataResponse.ResponseStatus = New ResponseStatus
        retrieveBiomDataResponse.ResponseStatus.Return = 0
        retrieveBiomDataResponse.ResponseStatus.Message = "The biometric records have been sucessfully retrieved."
        Return retrieveBiomDataResponse
    End Function

    ''' <summary>
    ''' Retrieve requested information about a subject. This aggregate service may be used to retrieve both biographic and biometric information for a subject record.
    ''' <param name="SubjectID">the identifier of the subject</param>
    ''' <param name="GalleryID">the identifier of the gallery or population group from which the biometric information will be retrieved</param>
    ''' <returns name="Return">Holds a code signifying success (0) or one of 39 error codes.</returns>
    ''' <returns name="Message">Message cooresponding to the Return parameter</returns>
    ''' <returns name="ReturnData">An InformationType that holds biographic/biometric/all data.</returns>
    ''' </summary>
    Public Function RetrieveData(RetrieveDataRequest As RetrieveDataRequest) As RetrieveDataResponsePackage Implements BIAS_v2.RetrieveData

        Dim subjectID As String = RetrieveDataRequest.Identity.SubjectID
        Dim retrieveDataResponse As New RetrieveDataResponsePackage()
        Dim processingOptions As ProcessingOptionsType = RetrieveDataRequest.ProcessingOptions 'list of optionType, which are key/val pairs

        MessageBox.Show("inside retrieve data....")
        If (RetrieveDataRequest.Identity.SubjectID Is Nothing) Then
            retrieveDataResponse.ResponseStatus.Return = 9
            retrieveDataResponse.ResponseStatus.Message = "The input subject ID is empty or in an invalid format."
            Return retrieveDataResponse
        End If
        MessageBox.Show("inside retrieve data....2")
        'Pull out all relavent information from the subject record file, to be used as needed.
        Dim subjectFilePath As String = Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).ToString).ToString & "\MasterDB\Subject Records\" & subjectID & "\" & subjectID & ".txt"
        Dim subjectRecordPath As String = Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).ToString).ToString & "\MasterDB\Subject Records\" & subjectID & "\"
        Dim readText As List(Of String) = System.IO.File.ReadAllLines(subjectFilePath).ToList
        MessageBox.Show("inside retrieve data....3")

        'possible options - [biographicData, basic] [biographicData, full] [biometricData, images], [allData, basic] [allData, full]
        '[biographicData, basic] - GUID = SubjectID/IdentityClaim, GivenName + FamilyName = FirstName+LastNAme
        Dim dataType As String = processingOptions(0).Key
        Dim dataSelection As String = processingOptions(0).Value
        Dim returnInfoType As New InformationType

        If dataType = "biographicData" Then
            Dim GUIDIndex, givenNameIndex, familyNameIndex, dateOfBirthIndex, sexIndex, citizenshipIndex As Integer

            'find the indexes of all non-biometric information
            For Each line In readText
                If line.StartsWith("GUID:") Then GUIDIndex = readText.IndexOf(line)
                If line.StartsWith("GivenName:") Then givenNameIndex = readText.IndexOf(line)
                If line.StartsWith("FamilyName:") Then familyNameIndex = readText.IndexOf(line)
                If line.StartsWith("DateOfBirth:") Then dateOfBirthIndex = readText.IndexOf(line)
                If line.StartsWith("Sex:") Then sexIndex = readText.IndexOf(line)
                If line.StartsWith("Citizenship:") Then citizenshipIndex = readText.IndexOf(line)
            Next

            Dim GUID As String = readText(GUIDIndex).Substring(5)
            Dim givenName As String = readText(givenNameIndex).Substring(10)
            Dim familyName As String = readText(familyNameIndex).Substring(11)
            Dim dateOfBirth As String = readText(dateOfBirthIndex).Substring(12)
            Dim sex As String = readText(sexIndex).Substring(4)
            Dim citizenship As String = readText(citizenshipIndex).Substring(12)

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

        If dataType = "biometricData" Then
            If dataSelection = "Images" Then 'return information type with images (list of image, with image being 64 byte strings), no biographic data
                'return list of image names
                Dim imageList = New InformationType.ImagesType

                Dim subjectDirectory = New DirectoryInfo(subjectRecordPath)
                Dim imagePathList = getImagePaths(subjectDirectory)

                For Each img In imagePathList

                    Dim subjectRawImage = System.Drawing.Image.FromFile(img)
                    Dim subjectImageByteArray As Byte()
                    Dim ms As System.IO.MemoryStream = New System.IO.MemoryStream
                    subjectRawImage.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg)
                    subjectImageByteArray = ms.ToArray()

                    Dim subjectImage = New OASIS.BIAS.V2.Image
                    subjectImage.ImageData = subjectImageByteArray

                    imageList.Add(subjectImage)
                Next

                returnInfoType.Images = imageList

            End If
        End If

        If dataType = "allData" Then 'returns a full information type

            'return all biographic data
            Dim GUIDIndex, givenNameIndex, familyNameIndex, dateOfBirthIndex, sexIndex, citizenshipIndex As Integer

            'find the indexes of all non-biometric information
            For Each line In readText
                If line.StartsWith("GUID") Then GUIDIndex = readText.IndexOf(line)
                If line.StartsWith("GivenName:") Then givenNameIndex = readText.IndexOf(line)
                If line.StartsWith("FamilyName:") Then familyNameIndex = readText.IndexOf(line)
                If line.StartsWith("DateOfBirth:") Then dateOfBirthIndex = readText.IndexOf(line)
                If line.StartsWith("Sex:") Then sexIndex = readText.IndexOf(line)
                If line.StartsWith("Citizenship:") Then citizenshipIndex = readText.IndexOf(line)
            Next

            Dim GUID As String = readText(GUIDIndex).Substring(5)
            Dim givenName As String = readText(givenNameIndex).Substring(10)
            Dim familyName As String = readText(familyNameIndex).Substring(11)
            Dim dateOfBirth As String = readText(dateOfBirthIndex).Substring(12)
            Dim sex As String = readText(sexIndex).Substring(4)
            Dim citizenship As String = readText(citizenshipIndex).Substring(12)

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

            'return all images 
            Dim imageList = New InformationType.ImagesType

            Dim subjectDirectory = New DirectoryInfo(subjectRecordPath)
            Dim imagePathList = getImagePaths(subjectDirectory)
            For Each img In imagePathList

                Dim subjectRawImage = System.Drawing.Image.FromFile(img)
                Dim subjectImageByteArray As Byte()
                Dim ms As System.IO.MemoryStream = New System.IO.MemoryStream
                subjectRawImage.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg)
                subjectImageByteArray = ms.ToArray()

                Dim subjectImage = New OASIS.BIAS.V2.Image
                subjectImage.ImageData = subjectImageByteArray

                imageList.Add(subjectImage)
            Next

            returnInfoType.Images = imageList
            MessageBox.Show("inside retrieve data....4 & ")
        End If


        retrieveDataResponse.ReturnData = returnInfoType
        retrieveDataResponse.ResponseStatus = New ResponseStatus
        retrieveDataResponse.ResponseStatus.Return = 0
        retrieveDataResponse.ResponseStatus.Message = "Data sucessfully retrieved."
        Return retrieveDataResponse
    End Function

    ''' <summary>
    ''' Associates biographic data to a given subject record. In a person centric model, the biographic information replaces any existing biographic information.
    ''' Note: This means that empty values for things like "FirstName" or "Sex" will be used to populate pre-existing information.
    ''' <param name="SubjectID">the identifier of the subject</param>
    ''' <param name="GalleryID">the identifier of the gallery or population group from which the biometric information will be retrieved</param>
    ''' <param name="BiographicData">a list of biographic data to associate with the subject</param>
    ''' <returns name="Return">Holds a code signifying success (0) or one of 39 error codes.</returns>
    ''' <returns name="Message">Message cooresponding to the Return parameter</returns>
    ''' </summary>
    Public Function SetBiographicData(SetBiographicDataRequest As SetBiographicDataRequest) As SetBiographicDataResponsePackage Implements BIAS_v2.SetBiographicData

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

    ''' <summary>
    ''' Associates biometric data to a given subject record. In a person centric model, the biometric information replaces any existing biometric information.
    ''' Note: This means that empty values for things like "FirstName" or "Sex" will be used to populate pre-existing information.
    ''' <param name="SubjectID">the identifier of the subject</param>
    ''' <param name="GalleryID">the identifier of the gallery or population group from which the biometric information will be retrieved</param>
    ''' <param name="BIR List">List containing populated into CBEFF_BIR types, which contain the new biometric samples(s)</param>
    ''' <returns name="Return">Holds a code signifying success (0) or one of 39 error codes.</returns>
    ''' <returns name="Message">Message cooresponding to the Return parameter</returns>
    ''' </summary>
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

        'Used to set the first set of biom data
        Dim SubjectID = SetBiometricDataRequest.Identity.SubjectID
        Dim biomList = SetBiometricDataRequest.Identity.BiometricData.BIRList
        Dim subjectDirectory = Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).ToString).ToString & "\MasterDB\Subject Records\" & SubjectID & "\"
        For Each record In biomList

            Dim biomImage As System.Drawing.Image = ImageFromBase64String(record.BIR.biometricImage) 'convert base64 image string to reg image
            Dim biomImageType = record.BIR.biometricImageType

            'save this image in the subjectID record
            Dim bytes As Byte() = Convert.FromBase64String(record.BIR.biometricImage)
            Dim biomImageJPG As System.Drawing.Image
            Using ms As New MemoryStream(bytes)
                biomImageJPG = System.Drawing.Image.FromStream(ms)
                biomImageJPG.Save(subjectDirectory & SubjectID & biomImageType & ".jpg", ImageFormat.Jpeg)
            End Using
            Dim imageName = SubjectID & biomImageType & ".jpg"

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

    ''' <summary>
    ''' Transforms or processes a given biometric in one format into a new target format. 
    ''' Examples of transformations include: Feature extraction, centering or cropping biometric images, data format conversion.
    ''' <param name="Transform Operation">Value indicating the type of transformation to perform</param>
    ''' <param name="Transform Control">Specifies controls for the requested transform operation</param>
    ''' <param name="Input BIRt">Data structure containing the biometric information to be transformed</param>
    ''' <returns name="Return">Holds a code signifying success (0) or one of 39 error codes.</returns>
    ''' <returns name="Message">Message cooresponding to the Return parameter</returns>
    ''' <returns name="Output BIR">Data structure containing the new, transformed biometric information</returns>
    ''' </summary>
    Public Function TransformBiometricData(TransformBiometricDataRequest As TransformBiometricDataRequest) As TransformBiometricDataResponsePackage Implements BIAS_v2.TransformBiometricData

        Dim transformBiomDataResponse As New TransformBiometricDataResponsePackage()
        transformBiomDataResponse.ResponseStatus = New ResponseStatus

        Dim inputBiometricData As CBEFF_BIR_Type = TransformBiometricDataRequest.InputBIR
        Dim transformOperation = TransformBiometricDataRequest.TransformOperation
        Dim transformationControl As String = TransformBiometricDataRequest.TransformControl
        Dim outputBIR As New CBEFF_BIR_Type

        If transformOperation = 1 Then 'cropping image

            'create the faceCascade
            Dim cascadeFilePath = Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).ToString).ToString & "\haarcascade_frontalface_default.xml"
            Dim faceCascade = New CascadeClassifier(cascadeFilePath)

            Dim biomImage As System.Drawing.Image = ImageFromBase64String(inputBiometricData.BIR.biometricImage)
            Dim biomBitmap As New Bitmap(biomImage)
            Dim img As Image(Of Gray, Byte) = New Image(Of Gray, Byte)(biomBitmap)

            'get the face area as a rectangle and create a new rectangle using the dimensions of the face rectangle
            Dim faceRegion As Rectangle() = faceCascade.DetectMultiScale(img)
            Dim CropRect As New Rectangle(faceRegion(0).X, faceRegion(0).Y, faceRegion(0).Width, faceRegion(0).Height)
            'get the image from path, save in a new image variable. Also create a bitmap to save the cropped image in.
            Dim OriginalImage = biomImage
            Dim CropImage = New Bitmap(CropRect.Width, CropRect.Height)

            'take the original image and crop it, using the CropRect dimensions.
            Using grp = Graphics.FromImage(CropImage)
                grp.DrawImage(OriginalImage, New Rectangle(0, 0, CropRect.Width, CropRect.Height), CropRect, GraphicsUnit.Pixel)
            End Using

            Dim croppedImage As System.Drawing.Image = DirectCast(CropImage, System.Drawing.Image)
            Dim croppedImageString = ImageToBase64String(croppedImage, ImageFormat.Jpeg)

            outputBIR.BIR.biometricImage = croppedImageString
            transformBiomDataResponse.OutputBIR = outputBIR
            transformBiomDataResponse.ResponseStatus.Return = 0
            transformBiomDataResponse.ResponseStatus.Message = "Biometric image sucessfully cropped."

        End If
        If transformOperation = 2 Then 'image format conversion
            'transform controls indicate what format to convert the image, i.e. PNG, JPEG
            'NOTE: Not supported in this implementation, as biometric images are stored as base64 strings, not as an Image
            transformBiomDataResponse.OutputBIR = TransformBiometricDataRequest.InputBIR
            transformBiomDataResponse.ResponseStatus.Return = 2
            transformBiomDataResponse.ResponseStatus.Message = "Not supported in this implementation, as biometric images are stored as base64 strings, not as an Image"
        End If

        Return transformBiomDataResponse
    End Function

    ''' <summary>
    ''' Updates the biographic data for an existing subject record. The service shall replace any existing biographic data with the new biographic data, within the subject record and in all galleries.
    ''' NOTE: Unlike SetBiographicData(), UpdateBiographicData does not overwrite data within an attribute if the input record does not have anything to replace it.
    ''' <param name="SubjectID">The identifier of the subject</param>
    ''' <param name="BiographicData">The list of updated biographic data elements</param>
    ''' <returns name="Return">Holds a code signifying success (0) or one of 39 error codes.</returns>
    ''' <returns name="Message">Message cooresponding to the Return parameter</returns>
    ''' </summary>
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

    ''' <summary>
    ''' Updates the biometric data for an existing subject record. The service shall replace any existing biometric data with the new biometric data, within the subject record and in all galleries.
    ''' NOTE: Unlike SetBiometricData(), UpdateBiometricData() does not overwrite data within an attribute if the input record does not have anything to replace it.
    ''' <param name="SubjectID">The identifier of the subject</param>
    ''' <param name="BIR">The list of updated biographic data elements</param>
    ''' <param name="Merge">Optional input that allows for the merging of two biometric samples. Functionality is implementation specific.</param>
    ''' <returns name="Return">Holds a code signifying success (0) or one of 39 error codes.</returns>
    ''' <returns name="Message">Message cooresponding to the Return parameter</returns>
    ''' </summary>
    Public Function UpdateBiometricData(UpdateBiometricDataRequest As UpdateBiometricDataRequest) As UpdateBiometricDataResponsePackage Implements BIAS_v2.UpdateBiometricData

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

    ''' <summary>
    ''' Performs a 1:1 verification function between a given biometric and either a claim of identity in a given gallery or another provided biometric and according to system requirements and/or resources.
    ''' Either the Identity Claim or Reference BIR input parameters are required.
    ''' <param name="Processing Options">Options that guide how the service request is processed, and may identify what type(s) of information should be returned</param>
    ''' <param name="Input Data">Contains an input data record, which at a minimum must include biometric data</param>
    ''' <param name="GalleryID">The identifier of the gallery or population group of which the subject must be a member - Optional</param>
    ''' <param name="Reference BIR">Data structure containing the biometric sample that will be compared to the Input BIR, required if no Identity Claim is provided - Conditional</param>
    ''' <param name="Identity Claim">The identifier by which the subject is known to the gallery, required if no Reference BIR is provided - Conditional</param>
    ''' <returns name="Return">Holds a code signifying success (0) or one of 39 error codes.</returns>
    ''' <returns name="Message">Message cooresponding to the Return parameter</returns>
    ''' <returns name="Match">indicates if the Input BIR matched either the biometric information associated with the Identity Claim or the Reference BIR</returns>
    ''' <returns name="Score">The comparison score, if the biometric information matched - Optional</returns>
    ''' <returns name="Return Data">contains a return data record</returns>
    ''' </summary>
    Public Function Verify(VerifyRequest As VerifyRequest) As VerifyResponsePackage Implements BIAS_v2.Verify
        Dim verifyResponse As New VerifyResponsePackage()
        verifyResponse.ResponseStatus = New ResponseStatus

        'check for either reference BIR or identity claim. One must exist
        If VerifyRequest.InputData.Images Is Nothing And VerifyRequest.IdentityClaim Is Nothing Then
            verifyResponse.Match = False
            verifyResponse.ResponseStatus.Return = 34
            verifyResponse.ResponseStatus.Message = "Cannot perform a 1:N identification of the supplied and/or stored data."
            Return verifyResponse
        End If

        'Processing options - specifying BIR source and return values.
        Dim options = VerifyRequest.ProcessingOptions
        Dim useReference = ""
        Dim returnScore = ""
        Dim returnRecord = ""
        For Each pOption In options
            Dim key = pOption.Key
            Dim val = pOption.Value
            If key = "Use" Then
                If val = "Reference" Then
                    useReference = "True"
                Else
                    useReference = "False"
                End If
            End If
            If key = "ReturnScore" Then
                If val = "True" Then
                    returnScore = "True"
                End If
            End If
            If key = "ReturnRecord" Then
                If val = "True" Then
                    returnRecord = "True"
                End If
            End If
        Next

        'Verify, using either the reference BIR or the identity claim - specified in processing options.
        If useReference = "True" Then

            'Need to create the referenceBIR candidate identity from the Inputdata

            Dim candidateList = New CandidateListType
            Dim candidate = New CandidateType
            candidate.Identity = New BIASIdentity
            candidate.Identity.BiometricData = New BIASBiometricDataType
            candidate.Identity.BiometricData.BIRList = New CBEFF_BIR_ListType
            'Create BIR list
            Dim imageList = VerifyRequest.InputData.Images
            For Each Image In imageList
                Dim imageString = Convert.ToBase64String(Image.ImageData)
                Dim cbeffBirType = New CBEFF_BIR_Type
                cbeffBirType.BIR = New BaseBIRType
                cbeffBirType.BIR.biometricImage = imageString
                candidate.Identity.BiometricData.BIRList.Add(cbeffBirType)
            Next

            candidateList.Add(candidate)

            Dim trainer = New IdentifySubjectTrainer()
            trainer.setGallery(candidateList)
            Dim verifyTrainer = trainer.createTrainer()

            'Load and convert base64 string to image.
            Dim inputData As String = VerifyRequest.Identity.BiometricData.BIR.BIR.biometricImage
            Dim inputImage As System.Drawing.Image = ImageFromBase64String(inputData)

            'create the faceCascade
            Dim cascadeFilePath = Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).ToString).ToString & "\haarcascade_frontalface_default.xml"
            Dim faceCascade = New CascadeClassifier(cascadeFilePath)

            'specified as 1 due to the singular nature of verify as opposed to the multiple match nature of identify
            Dim maxListSize As Long = 1

            Dim predictionResult = prediction(verifyTrainer, faceCascade, inputImage, maxListSize)
            Dim predictSubjectID = predictionResult.Label
            Dim predictDistance = predictionResult.Distance
            Dim matchScore = New ScoreType
            matchScore.Value = predictDistance

            'if the distance is above 5, probable match. If not, reject match, return empty identity
            If (predictDistance > 5) Then
                verifyResponse.ResponseStatus.Return = 0
                verifyResponse.Match = True
                verifyResponse.ResponseStatus.Message = "Match verified."
                If returnScore = "True" Then
                    verifyResponse.Score = matchScore
                End If
            Else
                verifyResponse.ResponseStatus.Return = 34
                verifyResponse.Match = False
                verifyResponse.ResponseStatus.Message = "Cannot perform a 1:N identification of the supplied and/or stored data."
                If returnScore = "True" Then
                    verifyResponse.Score = matchScore
                End If
            End If

        ElseIf useReference = "False" Then

            Dim identityClaim = VerifyRequest.Identity.IdentityClaim
            Dim candidateList = New CandidateListType
            Dim candidate = New CandidateType

            'use record in specified gallery if provided, otherwise use record from subject records folder.
            Dim galleryID As String = VerifyRequest.GalleryID
            If galleryID IsNot Nothing Then
                'check if gallery exists
                Dim galleriesPath = Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).ToString).ToString & "\MasterDB\Galleries"
                If (Directory.Exists(galleriesPath & "\" & galleryID) = False) Then
                    verifyResponse.ResponseStatus.Return = 11
                    verifyResponse.ResponseStatus.Message = "The gallery referenced by the input gallery ID does not exist."
                    Return verifyResponse
                End If

                'retrieve the biometric information from the subject record folder in the gallery
                Dim retrieveBiomData = New RetrieveBiometricDataRequest
                retrieveBiomData.GalleryID = galleryID
                retrieveBiomData.Identity.SubjectID = identityClaim
                Dim bias1 = New BIAS_v2Client
                Dim retrievedData = bias1.RetrieveBiometricData(retrieveBiomData)
                candidate.Identity = retrievedData.Identity

            ElseIf galleryID Is Nothing Then

                'retrieve the biometric information from the subject records folder
                Dim retrieveBiomData = New RetrieveBiometricDataRequest
                retrieveBiomData.Identity.SubjectID = identityClaim
                Dim bias1 = New BIAS_v2Client
                Dim retrievedData = bias1.RetrieveBiometricData(retrieveBiomData)
                candidate.Identity = retrievedData.Identity

            End If

            candidateList.Add(candidate)

            Dim trainer = New IdentifySubjectTrainer()
            trainer.setGallery(candidateList)
            Dim verifyTrainer = trainer.createTrainer()

            'Load and convert base64 string to image.
            Dim inputData As String = VerifyRequest.Identity.BiometricData.BIR.BIR.biometricImage
            Dim inputImage As System.Drawing.Image = ImageFromBase64String(inputData)

            'create the faceCascade
            Dim cascadeFilePath = Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).ToString).ToString & "\haarcascade_frontalface_default.xml"
            Dim faceCascade = New CascadeClassifier(cascadeFilePath)

            'specified as 1 due to the singular nature of verify as opposed to the multiple match nature of identify
            Dim maxListSize As Long = 1

            Dim predictionResult = prediction(verifyTrainer, faceCascade, inputImage, maxListSize)
            Dim predictSubjectID = predictionResult.Label
            Dim predictDistance = predictionResult.Distance
            Dim matchScore = New ScoreType
            matchScore.Value = predictDistance

            'if the distance is above 5, probable match. If not, reject match, return empty identity
            If (predictDistance > 5) Then
                Dim predictionString = "Input image is most likely Subject " & predictionResult.Label & ", with a distance score of " & predictionResult.Distance
                verifyResponse.ResponseStatus.Return = 0
                verifyResponse.Match = True
                verifyResponse.ResponseStatus.Message = predictionString
                If returnScore = "True" Then
                    verifyResponse.Score = matchScore
                End If
                If returnRecord = "True" Then
                    verifyResponse.ReturnData = New InformationType
                    verifyResponse.ReturnData.GivenName = "Implement"
                    verifyResponse.ReturnData.FamilyName = "Later"
                    'Dim subjectID = predictionResult.Label
                    'Dim retrieveBiogData = New RetrieveBiographicDataRequest
                    'If galleryID IsNot Nothing Then
                    '    retrieveBiogData.GalleryID = galleryID
                    'End If
                    'retrieveBiogData.Identity = New BIASIdentity
                    'retrieveBiogData.Identity.SubjectID = identityClaim
                    'Dim bias1 = New BIAS_v2Client
                    'Dim retrievedBiogData = bias1.RetrieveBiographicData(retrieveBiogData)
                    'Dim test = retrievedBiogData.Identity.BiographicData.BiographicDataItemList(0)
                End If
            Else
                verifyResponse.ResponseStatus.Return = 34
                verifyResponse.Match = False
                verifyResponse.ResponseStatus.Message = "Cannot perform a 1:N identification of the supplied and/or stored data."
                If returnScore = "True" Then
                    verifyResponse.Score = matchScore
                End If
                If returnRecord = "True" Then
                    'implement later
                    verifyResponse.ReturnData = New InformationType
                    verifyResponse.ReturnData.GivenName = "Implement"
                    verifyResponse.ReturnData.FamilyName = "Later"
                End If
            End If

        Else
            verifyResponse.ResponseStatus.Return = 33
            verifyResponse.ResponseStatus.Message = "No Reference BIR or Identity Claim provided."
        End If

        Return verifyResponse
    End Function

    ''' <summary>
    ''' Performs a 1:1 verification comparison between a given biometric and either a claim to identity in a given gallery or another provided biometric. 
    ''' As such, either the Identity Claim or Reference BIR input parameters are required.
    ''' <param name="SubjectID">The identifier of the subject</param>
    ''' <param name="GalleryID">The identifier of the gallery or population group of which the subject must be a member - Optional</param>
    ''' <param name="Input BIR">Data structure containing the biometric sample for the search</param>
    ''' <param name="Reference BIR">Data structure containing the biometric sample that will be compared to the Input BIR, required if no Identity Claim is provided - Conditional</param>
    ''' <param name="Identity Claim">The identifier by which the subject is known to the gallery, required if no Reference BIR is provided - Conditional</param>
    ''' <returns name="Return">Holds a code signifying success (0) or one of 39 error codes.</returns>
    ''' <returns name="Message">Message cooresponding to the Return parameter</returns>
    ''' <returns name="Match">indicates if the Input BIR matched either the biometric information associated with the Identity Claim or the Reference BIR</returns>
    ''' <returns name="Score">The comparison score, if the biometric information matched - Optional</returns>
    ''' </summary>
    Public Function VerifySubject(VerifySubjectRequest As VerifySubjectRequest) As VerifySubjectResponsePackage Implements BIAS_v2.VerifySubject
        Dim verifySubjectResponse As New VerifySubjectResponsePackage()
        verifySubjectResponse.ResponseStatus = New ResponseStatus

        'check for either reference BIR or identity claim. One must exist
        If VerifySubjectRequest.InputData.Images Is Nothing And VerifySubjectRequest.IdentityClaim Is Nothing Then
            verifySubjectResponse.Match = False
            verifySubjectResponse.ResponseStatus.Return = 34
            verifySubjectResponse.ResponseStatus.Message = "Cannot perform a 1:N identification of the supplied and/or stored data."
            Return verifySubjectResponse
        End If

        'Verify, using either the reference BIR or the identity claim - specified in processing options.
        If VerifySubjectRequest.InputData IsNot Nothing Then

            'Need to create the referenceBIR candidate identity from the Inputdata

            Dim candidateList = New CandidateListType
            Dim candidate = New CandidateType
            candidate.Identity = New BIASIdentity
            candidate.Identity.BiometricData = New BIASBiometricDataType
            candidate.Identity.BiometricData.BIRList = New CBEFF_BIR_ListType
            'Create BIR list
            Dim imageList = VerifySubjectRequest.InputData.Images
            For Each Image In imageList
                Dim imageString = Convert.ToBase64String(Image.ImageData)
                Dim cbeffBirType = New CBEFF_BIR_Type
                cbeffBirType.BIR = New BaseBIRType
                cbeffBirType.BIR.biometricImage = imageString
                candidate.Identity.BiometricData.BIRList.Add(cbeffBirType)
            Next

            candidateList.Add(candidate)

            Dim trainer = New IdentifySubjectTrainer()
            trainer.setGallery(candidateList)
            Dim verifyTrainer = trainer.createTrainer()

            'Load and convert base64 string to image.
            Dim inputData As String = VerifySubjectRequest.Identity.BiometricData.BIR.BIR.biometricImage
            Dim inputImage As System.Drawing.Image = ImageFromBase64String(inputData)

            'create the faceCascade
            Dim cascadeFilePath = Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).ToString).ToString & "\haarcascade_frontalface_default.xml"
            Dim faceCascade = New CascadeClassifier(cascadeFilePath)

            'specified as 1 due to the singular nature of verify as opposed to the multiple match nature of identify
            Dim maxListSize As Long = 1

            Dim predictionResult = prediction(verifyTrainer, faceCascade, inputImage, maxListSize)
            Dim predictSubjectID = predictionResult.Label
            Dim predictDistance = predictionResult.Distance
            Dim matchScore = New ScoreType
            matchScore.Value = predictDistance

            'if the distance is above 5, probable match. If not, reject match, return empty identity
            If (predictDistance > 5) Then
                verifySubjectResponse.ResponseStatus.Return = 0
                verifySubjectResponse.Match = True
                verifySubjectResponse.ResponseStatus.Message = "Match verified."
                verifySubjectResponse.Score = matchScore
            Else
                verifySubjectResponse.ResponseStatus.Return = 34
                verifySubjectResponse.Match = False
                verifySubjectResponse.ResponseStatus.Message = "Cannot perform a 1:N identification of the supplied and/or stored data."
            End If

        ElseIf VerifySubjectRequest.IdentityClaim IsNot Nothing Then

            Dim identityClaim = VerifySubjectRequest.Identity.IdentityClaim
            Dim candidateList = New CandidateListType
            Dim candidate = New CandidateType

            'use record in specified gallery if provided, otherwise use record from subject records folder.
            Dim galleryID As String = VerifySubjectRequest.GalleryID
            If galleryID IsNot Nothing Then
                'check if gallery exists
                Dim galleriesPath = Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).ToString).ToString & "\MasterDB\Galleries"
                If (Directory.Exists(galleriesPath & "\" & galleryID) = False) Then
                    verifySubjectResponse.ResponseStatus.Return = 11
                    verifySubjectResponse.ResponseStatus.Message = "The gallery referenced by the input gallery ID does not exist."
                    Return verifySubjectResponse
                End If

                'retrieve the biometric information from the subject record folder in the gallery
                Dim retrieveBiomData = New RetrieveBiometricDataRequest
                retrieveBiomData.GalleryID = galleryID
                retrieveBiomData.Identity.SubjectID = identityClaim
                Dim bias1 = New BIAS_v2Client
                Dim retrievedData = bias1.RetrieveBiometricData(retrieveBiomData)
                candidate.Identity = retrievedData.Identity

            ElseIf galleryID Is Nothing Then

                'retrieve the biometric information from the subject records folder
                Dim retrieveBiomData = New RetrieveBiometricDataRequest
                retrieveBiomData.Identity.SubjectID = identityClaim
                Dim bias1 = New BIAS_v2Client
                Dim retrievedData = bias1.RetrieveBiometricData(retrieveBiomData)
                candidate.Identity = retrievedData.Identity

            End If

            candidateList.Add(candidate)

            Dim trainer = New IdentifySubjectTrainer()
            trainer.setGallery(candidateList)
            Dim verifyTrainer = trainer.createTrainer()

            'Load and convert base64 string to image.
            Dim inputData As String = VerifySubjectRequest.Identity.BiometricData.BIR.BIR.biometricImage
            Dim inputImage As System.Drawing.Image = ImageFromBase64String(inputData)

            'create the faceCascade
            Dim cascadeFilePath = Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).ToString).ToString & "\haarcascade_frontalface_default.xml"
            Dim faceCascade = New CascadeClassifier(cascadeFilePath)

            'specified as 1 due to the singular nature of verify as opposed to the multiple match nature of identify
            Dim maxListSize As Long = 1

            Dim predictionResult = prediction(verifyTrainer, faceCascade, inputImage, maxListSize)
            Dim predictSubjectID = predictionResult.Label
            Dim predictDistance = predictionResult.Distance
            Dim matchScore = New ScoreType
            matchScore.Value = predictDistance

            'if the distance is above 5, probable match. If not, reject match, return empty identity
            If (predictDistance > 5) Then
                Dim predictionString = "Input image is most likely Subject " & predictionResult.Label & ", with a distance score of " & predictionResult.Distance
                verifySubjectResponse.ResponseStatus.Return = 0
                verifySubjectResponse.Match = True
                verifySubjectResponse.ResponseStatus.Message = predictionString
                verifySubjectResponse.Score = matchScore
            Else
                verifySubjectResponse.ResponseStatus.Return = 34
                verifySubjectResponse.Match = False
                verifySubjectResponse.ResponseStatus.Message = "Cannot perform a 1:N identification of the supplied and/or stored data."
                verifySubjectResponse.Score = matchScore
            End If

        Else
            verifySubjectResponse.ResponseStatus.Return = 33
            verifySubjectResponse.ResponseStatus.Message = "No Reference BIR or Identity Claim provided."
        End If
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
