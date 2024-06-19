Imports System.Net

Public Class CreateSubmissionForm
    Private stopwatch As New Stopwatch()
    Private timer As New Timer()

    Private Sub CreateSubmissionForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Text = "John Doe, Slidely Task 2 - Create Submission"

        ' Setup input fields and buttons
        ' Name
        Dim lblName As New Label()
        lblName.Text = "Name"
        lblName.Location = New Point(30, 30)
        Me.Controls.Add(lblName)

        Dim txtName As New TextBox()
        txtName.Name = "txtName"
        txtName.Location = New Point(150, 30)
        Me.Controls.Add(txtName)

        ' Email
        Dim lblEmail As New Label()
        lblEmail.Text = "Email"
        lblEmail.Location = New Point(30, 60)
        Me.Controls.Add(lblEmail)

        Dim txtEmail As New TextBox()
        txtEmail.Name = "txtEmail"
        txtEmail.Location = New Point(150, 60)
        Me.Controls.Add(txtEmail)

        ' Phone Number
        Dim lblPhone As New Label()
        lblPhone.Text = "Phone Num"
        lblPhone.Location = New Point(30, 90)
        Me.Controls.Add(lblPhone)

        Dim txtPhone As New TextBox()
        txtPhone.Name = "txtPhone"
        txtPhone.Location = New Point(150, 90)
        Me.Controls.Add(txtPhone)

        ' GitHub Link
        Dim lblGitHub As New Label()
        lblGitHub.Text = "GitHub Link For Task 2"
        lblGitHub.Location = New Point(30, 120)
        Me.Controls.Add(lblGitHub)

        Dim txtGitHub As New TextBox()
        txtGitHub.Name = "txtGitHub"
        txtGitHub.Size = New Size(200, 20)
        txtGitHub.Location = New Point(150, 120)
        Me.Controls.Add(txtGitHub)

        ' Stopwatch
        Dim btnToggleStopwatch As New Button()
        btnToggleStopwatch.Text = "TOGGLE STOPWATCH (CTRL + T)"
        btnToggleStopwatch.Location = New Point(30, 150)
        AddHandler btnToggleStopwatch.Click, AddressOf BtnToggleStopwatch_Click
        Me.Controls.Add(btnToggleStopwatch)

        Dim lblStopwatch As New Label()
        lblStopwatch.Name = "lblStopwatch"
        lblStopwatch.Text = "00:00:00"
        lblStopwatch.Location = New Point(250, 150)
        Me.Controls.Add(lblStopwatch)

        ' Submit Button
        Dim btnSubmit As New Button()
        btnSubmit.Text = "SUBMIT (CTRL + S)"
        btnSubmit.Location = New Point(30, 180)
        AddHandler btnSubmit.Click, AddressOf BtnSubmit_Click
        Me.Controls.Add(btnSubmit)

        ' Timer setup
        timer.Interval = 1000
        AddHandler timer.Tick, AddressOf Timer_Tick
    End Sub

    Private Sub BtnToggleStopwatch_Click(sender As Object, e As EventArgs)
        If stopwatch.IsRunning Then
            stopwatch.Stop()
            timer.Stop()
        Else
            stopwatch.Start()
            timer.Start()
        End If
    End Sub

    Private Sub Timer_Tick(sender As Object, e As EventArgs)
        Dim lblStopwatch As Label = Me.Controls("lblStopwatch")
        lblStopwatch.Text = stopwatch.Elapsed.ToString("hh\:mm\:ss")
    End Sub

    Private Sub BtnSubmit_Click(sender As Object, e As EventArgs)
        Try
            Dim name As String = Me.Controls("txtName").Text
            Dim email As String = Me.Controls("txtEmail").Text
            Dim phone As String = Me.Controls("txtPhone").Text
            Dim githubLink As String = Me.Controls("txtGitHub").Text
            Dim stopwatchTime As String = Me.Controls("lblStopwatch").Text

            Dim client As New WebClient()
            client.Headers(HttpRequestHeader.ContentType) = "application/json"
            Dim data As String = $"{{""name"":""{name}"",""email"":""{email}"",""phone"":""{phone}"",""github_link"":""{githubLink}"",""stopwatch_time"":""{stopwatchTime}""}}"
            client.UploadString("http://localhost:3000/submit", "POST", data)

            MessageBox.Show("Submission successful!")
            ResetForm()

        Catch ex As WebException
            MessageBox.Show($"Error submitting data: {ex.Message}")
            ' Log the exception details for further investigation
            Console.WriteLine($"WebException in BtnSubmit_Click: {ex}")
        Catch ex As Exception
            MessageBox.Show($"Unexpected error: {ex.Message}")
            ' Log the exception details for further investigation
            Console.WriteLine($"Exception in BtnSubmit_Click: {ex}")
        End Try
    End Sub

    Private Sub ResetForm()
        Me.Controls("txtName").Text = String.Empty
        Me.Controls("txtEmail").Text = String.Empty
        Me.Controls("txtPhone").Text = String.Empty
        Me.Controls("txtGitHub").Text = String.Empty
        Me.Controls("lblStopwatch").Text = "00:00:00"

        ' Reset stopwatch and timer
        stopwatch.Reset()
        timer.Stop()
    End Sub
End Class
