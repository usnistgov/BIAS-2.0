Imports System.IO
Imports Emgu.CV
Imports System.Collections.Generic
Imports Emgu.CV.Structure
Imports System.Drawing
Imports System.Windows.Forms


''' <summary>
''' Class for creating instances of trainers using LBPHFaceRecognizer
''' Trainers are used for subject image identification and verification in Identify()
''' </summary>
Public Class IdentifyTrainer

    Public galleryID As String

    Public Sub setGalleryID(ByVal ID As String)
        galleryID = ID
    End Sub

    ''' <summary>
    ''' Creates the trainer to be used using the external haarcascade_frontalface_default.xml file, and all the images and subjectIDs in the subject records folder.
    ''' </summary>
    Public Function createTrainer() As Emgu.CV.Face.LBPHFaceRecognizer

        'load the classifier file and create the Local Binary Patterns Histograms Face Recognizer
        Dim classifierFileDirectory = Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).ToString).ToString & "\haarcascade_frontalface_default.xml"

        Dim faceCascade = New CascadeClassifier(classifierFileDirectory)
        Dim trainer = New Face.LBPHFaceRecognizer

        'Append all the absolute image paths in imagePathList
        Dim subjectGalleryRecordsPath = Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).ToString).ToString & "\MasterDB\Galleries\" & galleryID
        Dim path As New IO.DirectoryInfo(subjectGalleryRecordsPath)
        Dim imagePathList As List(Of String) = getImagePaths(path)

        'get list of images and subject IDs
        Dim imageAndSubjectIDs As Tuple(Of Emgu.CV.Image(Of Gray, Byte)(), Integer()) = testTrain(imagePathList, faceCascade)
        Dim images = imageAndSubjectIDs.Item1
        Dim subjectIDs = imageAndSubjectIDs.Item2

        'Add code here to check that there is at least one sample.

        'Perform training of the recognizer
        trainer.Train(images, subjectIDs)

        Return trainer
    End Function

    ''' <summary>
    ''' Iterates through all subject records and appends the path of each image found to imagePathList
    ''' </summary>
    ''' <param name="path">The path of the subject records folder</param>
    ''' <returns>imagePathList - the list of all image paths in the subject record folder</returns>
    Public Function getImagePaths(path As DirectoryInfo)
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
            If faceRegion.Length = 1 Then
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
            Else
                MessageBox.Show("No face detected in image " & Path)
                Environment.Exit(0)
            End If

        Next

        Return New Tuple(Of Emgu.CV.Image(Of Gray, Byte)(), Integer())(images, labels)
    End Function
End Class
