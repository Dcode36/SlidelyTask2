Public Class Form1
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Text = "John Doe, Slidely Task 2 - Slidely Form App"
        ' Setup buttons
        Dim btnView As New Button()
        btnView.Text = "VIEW SUBMISSIONS (CTRL + V)"
        btnView.Location = New Point(30, 30)
        btnView.Size = New Size(200, 40)
        AddHandler btnView.Click, AddressOf BtnView_Click
        Me.Controls.Add(btnView)

        Dim btnCreate As New Button()
        btnCreate.Text = "CREATE NEW SUBMISSION (CTRL + N)"
        btnCreate.Location = New Point(30, 80)
        btnCreate.Size = New Size(200, 40)
        AddHandler btnCreate.Click, AddressOf BtnCreate_Click
        Me.Controls.Add(btnCreate)
    End Sub

    Private Sub BtnView_Click(sender As Object, e As EventArgs)
        Dim viewForm As New ViewSubmissionsForm()
        viewForm.Show()
    End Sub

    Private Sub BtnCreate_Click(sender As Object, e As EventArgs)
        Dim createForm As New CreateSubmissionForm()
        createForm.Show()
    End Sub
End Class