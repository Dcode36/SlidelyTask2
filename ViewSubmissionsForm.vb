Imports System.Net
Imports Newtonsoft.Json.Linq

Public Class ViewSubmissionsForm
    Private currentIndex As Integer = 0
    Private submissions As JArray

    ' Declare text boxes for displaying submission details with WithEvents
    Private WithEvents txtName As TextBox
    Private WithEvents txtEmail As TextBox
    Private WithEvents txtPhone As TextBox
    Private WithEvents txtGitHub As TextBox
    Private WithEvents txtStopwatch As TextBox

    Private Sub ViewSubmissionsForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Text = "John Doe, Slidely Task 2 - View Submissions"

        ' Initialize and add controls
        InitializeControls()

        ' Load submissions from the server
        LoadSubmissions()

        ' Display the current submission
        DisplaySubmission(currentIndex)
    End Sub

    Private Sub InitializeControls()
        ' Initialize and add labels (unchanged from your original code)
        ' ...

        ' Initialize and add text boxes with WithEvents (unchanged from your original code)
        ' ...

        ' Initialize and add buttons (unchanged from your original code)
        ' ...
    End Sub

    Private Sub LoadSubmissions()
        Try
            Dim client As New WebClient()
            Dim response As String = client.DownloadString($"http://localhost:3000/read?index={currentIndex}")

            ' Check if the response indicates an error (status 400)
            If response.StartsWith("{""error"":") Then
                Dim errorObject As JObject = JObject.Parse(response)
                Dim errorMessage As String = errorObject("error").ToString()
                MessageBox.Show($"Error loading submissions: {errorMessage}")
            Else
                ' Parse the response as a JArray
                submissions = JArray.Parse(response)
            End If

        Catch ex As WebException
            MessageBox.Show($"Error loading submissions: {ex.Message}")
            ' Log the exception details for further investigation
            Console.WriteLine($"WebException in LoadSubmissions: {ex}")
        Catch ex As Exception
            MessageBox.Show($"Unexpected error: {ex.Message}")
            ' Log the exception details for further investigation
            Console.WriteLine($"Exception in LoadSubmissions: {ex}")
        End Try
    End Sub

    Private Sub DisplaySubmission(index As Integer)
        If submissions Is Nothing OrElse submissions.Count = 0 Then
            MessageBox.Show("No submissions found.")
            Return
        End If

        If index < 0 OrElse index >= submissions.Count Then
            MessageBox.Show("No more submissions.")
            Return
        End If

        Dim submission As JObject = submissions(index)
        txtName.Text = submission("name").ToString()
        txtEmail.Text = submission("email").ToString()
        txtPhone.Text = submission("phone").ToString()
        txtGitHub.Text = submission("github_link").ToString()
        txtStopwatch.Text = submission("stopwatch_time").ToString()
    End Sub

    Private Sub btnPrevious_Click(sender As Object, e As EventArgs)
        If currentIndex > 0 Then
            currentIndex -= 1
            LoadSubmissions() ' Reload submissions after index change
            DisplaySubmission(currentIndex)
        Else
            MessageBox.Show("No previous submissions.")
        End If
    End Sub

    Private Sub btnNext_Click(sender As Object, e As EventArgs)
        If currentIndex < submissions.Count - 1 Then
            currentIndex += 1
            LoadSubmissions() ' Reload submissions after index change
            DisplaySubmission(currentIndex)
        Else
            MessageBox.Show("No more submissions.")
        End If
    End Sub
End Class
