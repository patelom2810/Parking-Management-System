Imports System.Data.SqlClient
Imports System.Text.RegularExpressions

Public Class _9_changepass
    Private LoggedInUsername As String

    ' Constructor to pass the logged-in username
    Public Sub New(ByVal username As String)
        InitializeComponent()
        LoggedInUsername = username ' Set the username of the logged-in user
    End Sub
    ' Connection string to the database
    Dim connectionString As String = "Data Source=.\SQLEXPRESS;AttachDbFilename=D:\PMS\PARKING_OM\PMSF\PMSF\PMSF.mdf;Integrated Security=True;User Instance=True"
    ' Function to validate password strength
    Private Function IsValidPassword(ByVal password As String) As Boolean
        Dim pattern As String = "^(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).{8,16}$"
        Return Regex.IsMatch(password, pattern)
    End Function
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click

        Dim currentPassword As String = txtCurrentPassword.Text.Trim()
        Dim newPassword As String = txtNewPassword.Text.Trim()
        Dim confirmPassword As String = txtConfirmPassword.Text.Trim()
        ' Validate password criteria

        If Not IsValidPassword(newPassword) Then
            MessageBox.Show("Password must be 8-16 characters long, contain at least one uppercase letter, one number, and one special character.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        ' Check if any field is empty
        If currentPassword = "" Or newPassword = "" Or confirmPassword = "" Then
            MessageBox.Show("Please fill in all fields.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        ' Check if the new password matches the confirm password
        If newPassword <> confirmPassword Then
            MessageBox.Show("New Password and Confirm Password do not match.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        ' Connection string to the database
        Dim connString As String = "Data Source=.\SQLEXPRESS;AttachDbFilename=D:\KASHAK\coms\coms\coms.mdf;Integrated Security=True;User Instance=True"
        Dim queryCheck As String = "SELECT COUNT(*) FROM Users WHERE Username = @username AND Password = @password"
        Dim queryUpdate As String = "UPDATE Users SET Password = @newPassword WHERE Username = @username"

        ' Create connection and commands
        Using conn As New SqlConnection(connString)
            Using cmdCheck As New SqlCommand(queryCheck, conn)
                cmdCheck.Parameters.AddWithValue("@username", LoggedInUsername) ' Use logged-in username
                cmdCheck.Parameters.AddWithValue("@password", currentPassword) ' Current password entered by user

                Try
                    conn.Open()

                    ' Check if the current password matches the username
                    Dim result As Integer = Convert.ToInt32(cmdCheck.ExecuteScalar())

                    If result = 1 Then
                        ' If current password is correct, update to the new password
                        Using cmdUpdate As New SqlCommand(queryUpdate, conn)
                            cmdUpdate.Parameters.AddWithValue("@username", LoggedInUsername) ' Use logged-in username
                            cmdUpdate.Parameters.AddWithValue("@newPassword", newPassword) ' New password entered by user

                            cmdUpdate.ExecuteNonQuery()
                            MessageBox.Show("Password reset successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

                            ' Clear text fields after reset
                            txtCurrentPassword.Clear()
                            txtNewPassword.Clear()
                            txtConfirmPassword.Clear()


                            ' Close the current form (Dashboard)
                            Me.Hide()
                            For Each frm As Form In Application.OpenForms
                                If frm.Name = "dashboard" Then
                                    frm.Close()
                                    Exit For
                                End If
                            Next

                            ' Create and show the login form
                            Dim loginForm As New Form1()
                            loginForm.Show()

                        End Using
                    Else
                        MessageBox.Show("Current password is incorrect.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End If
                Catch ex As Exception
                    MessageBox.Show("An error occurred: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Finally
                    conn.Close()
                End Try
            End Using
        End Using
    End Sub

    Private Sub ClearFields()
        ' Clear the input fields
        txtConfirmPassword.Clear()
        txtNewPassword.Clear()
        txtCurrentPassword.Clear()
    End Sub

    Private Sub btnCANCLE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCANCLE.Click
        Me.Close() ' Closes the current form
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.Close() ' Closes the current form
    End Sub

    Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button7.Click
        ' Check if the Dashboard form is already open
        For Each frm As Form In Application.OpenForms
            If TypeOf frm Is _2_Dashboard Then
                ' Bring the existing Dashboard form to the front if it's open
                frm.BringToFront()
                frm.Focus()
                Exit Sub ' Exit the sub once the dashboard form is found
            End If
        Next
    End Sub

    Private Sub _9_changepass_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub
End Class