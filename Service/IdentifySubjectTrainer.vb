Imports System.IO
Imports Emgu.CV
Imports System.Collections.Generic
Imports Emgu.CV.Structure
Imports System.Drawing
Imports OASIS.BIAS.V2
Imports System.Windows.Forms

''' <summary>
''' Class for creating instances of trainers using LBPHFaceRecognizer
''' Trainers are used for subject image identification and verification in IdentifySubject()
''' </summary>
Public Class IdentifySubjectTrainer

    Public gallery As CandidateListType

    Public Sub setGallery(ByVal inputGallery As CandidateListType)
        gallery = inputGallery
    End Sub

    Public Function createTrainer() As Emgu.CV.Face.LBPHFaceRecognizer

        'load the classifier file and create the Local Binary Patterns Histograms Face Recognizer
        Dim classifierFileDirectory = Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).ToString).ToString & "\haarcascade_frontalface_default.xml"
        Dim faceCascade = New CascadeClassifier(classifierFileDirectory)
        Dim trainer = New Face.LBPHFaceRecognizer

        'extract and convert biometric images into bitmaps.
        Dim imageAndSubjectIDs As Tuple(Of Emgu.CV.Image(Of Gray, Byte)(), Integer()) = testTrain(gallery, faceCascade)
        Dim images = imageAndSubjectIDs.Item1
        Dim subjectIDs = imageAndSubjectIDs.Item2
        'Perform training of the recognizer
        trainer.Train(images, subjectIDs)

        Return trainer
    End Function

    'returns a list of Image(Of Gray, Byte)
    Public Function getImagesFromCL(gallery As CandidateListType) As List(Of Bitmap)

        Dim bitmapList As List(Of Bitmap)

        For Each sample In gallery
            Dim sampleBIR As Example_BIR = sample.Identity.BiometricData.BIR.BIR
            Dim biomImage As System.Drawing.Image = ImageFromBase64String(sampleBIR.BiometricSample)
            Dim biomBitmap As New Bitmap(biomImage)
            bitmapList.Add(biomBitmap)
        Next

        Return bitmapList

    End Function

    ''' <summary>
    ''' Detects the face in each image, crops around that region, adds it to a list of image
    ''' Detects the subjectID of each image, sends back a tuple of the paired images and subject IDs.
    ''' </summary>
    ''' <param name="imagePathList">List of image paths</param>
    ''' <param name="faceCascade">Used to detect face locations</param>
    ''' <returns>Tuple(Of Emgu.CV.Image(Of Gray, Byte)(), Integer())(images, labels) - Tuple containing cropped face images and the corresponding subjectIDs</returns>
    Public Function testTrain(imageList As CandidateListType, faceCascade As CascadeClassifier) As Tuple(Of Emgu.CV.Image(Of Gray, Byte)(), Integer())

        'need to return array of images and array of integers
        Dim images = New Emgu.CV.Image(Of Gray, Byte)() {}
        Dim labels = New Integer() {}

        For Each sample In gallery
            For Each btest In sample.Identity.BiometricData.BIRList
                'convert to bitmap, then to CV.Image
                Dim sampleBIR As Example_BIR = btest.BIR
                Dim biomImage As System.Drawing.Image = ImageFromBase64String(sampleBIR.BiometricSample)

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

                Dim img2 As New Image(Of Gray, Byte)(CropImage)
                'add cropped image to the array of images. 
                Array.Resize(images, images.Length + 1)
                images(images.Length - 1) = img2

                'Get the label/subjectID of the image
                Dim subjectID = "No subjectID associated with the sample given."
                subjectID = sample.Identity.SubjectID
                Array.Resize(labels, labels.Length + 1)
                labels(labels.Length - 1) = subjectID
            Next
        Next

        Return New Tuple(Of Emgu.CV.Image(Of Gray, Byte)(), Integer())(images, labels)
    End Function

    ''' <summary>
    ''' Converts base64 strings back to images
    ''' </summary>
    ''' <returns>result - The transformed biometric sample image</returns>
    Function ImageFromBase64String(ByVal base64 As String) As System.Drawing.Image
        Using memStream As New MemoryStream(System.Convert.FromBase64String(base64))
            Dim result As System.Drawing.Image = System.Drawing.Image.FromStream(memStream)
            memStream.Close()
            Return result
        End Using
    End Function

End Class
