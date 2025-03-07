Imports System.Data.SqlClient

Public Class Form1

    ' Connection string to the database
    Dim connectionString As String = "Data Source=.\SQLEXPRESS;AttachDbFilename=D:\PMS\PARKING_OM\PMSF\PMSF\PMSF.mdf;Integrated Security=True;User Instance=True"
    Private Sub btnlogin_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnlogin.Click
        Dim connection As New SqlConnection("Data Source=.\SQLEXPRESS;AttachDbFilename=D:\PMS\PARKING_OM\PMSF\PMSF\PMSF.mdf;Integrated Security=True;User Instance=True")
        Dim username As String = txtUsername.Text.Trim()
        Dim password As String = txtPassword.Text.Trim()

        ' Validate that username and password are not empty
        If String.IsNullOrEmpty(username) Then
            MessageBox.Show("Please enter a username.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtUsername.Focus()
            Return ' Exit the method to stop further execution
        End If

        If String.IsNullOrEmpty(password) Then
            MessageBox.Show("Please enter a password.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtPassword.Focus()
            Return ' Exit the method to stop further execution
        End If

        ' Query to check username and password with case-sensitive comparison
        Dim query As String = "SELECT Username FROM Users WHERE Username = @username AND Password = @password COLLATE Latin1_General_BIN"
        Dim command As New SqlCommand(query, connection)

        ' Parameters to prevent SQL Injection
        command.Parameters.AddWithValue("@username", username)
        command.Parameters.AddWithValue("@password", password)

        Try
            connection.Open()
            Dim result As Object = command.ExecuteScalar()

            If result IsNot Nothing Then
                ' Display success message
                MessageBox.Show("Login successful! Welcome, " & username & ".", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

                ' Specific logic based on username
                Dim dashboard As New _2_Dashboard(username) ' Pass the username here

                If username.ToLower() = "admin" Then
                    dashboard.ShowAdminFeatures(True)
                Else
                    dashboard.ShowAdminFeatures(False)
                End If

                dashboard.SetLoggedInUser(username)
                dashboard.Show()
                Me.Hide()
            Else
                MessageBox.Show("Invalid username or password!", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        Catch ex As Exception
            MessageBox.Show("An error occurred: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            connection.Close()
        End Try
    End Sub

    Private Sub pictureBoxShowHide_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pictureBoxShowHide.Click
        ' Toggle password visibility
        If txtPassword.PasswordChar = "*" Then
            ' Show the actual password
            txtPassword.PasswordChar = ""
        Else
            ' Hide the password with *
            txtPassword.PasswordChar = "*"
        End If
    End Sub


    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ' Hide the password when typing using *
        txtPassword.PasswordChar = "*"  ' This will hide the password by default



    End Sub


    Private Sub txtPassword_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPassword.TextChanged

    End Sub

    Private Sub LinkLabel1_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        Dim fp As New _12_ForgotPassword()
        fp.Show() ' Opens Form12
        
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        ' Closes the application
        Application.Exit()
    End Sub


End Class
