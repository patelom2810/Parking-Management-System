Imports System.Data.SqlClient
Imports System.Text.RegularExpressions
Public Class _11_adduser
    Dim conn As New SqlConnection("Data Source=.\SQLEXPRESS;AttachDbFilename=D:\PMS\PARKING_OM\PMSF\PMSF\PMSF.mdf;Integrated Security=True;User Instance=True")
  Private Sub btnAddUser_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddUser.Click

        ' Check if any field is empty
        If txtSecurityAnswer.Text = "" Or txtUsername.Text = "" Or txtPassword.Text = "" Or cmbSecurityQuestion.SelectedIndex = -1 Then
            MessageBox.Show("Please fill in all fields.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If
        ' Validate password criteria
        Dim password As String = txtPassword.Text
        If Not IsValidPassword(password) Then
            MessageBox.Show("Password must be 8-16 characters long, contain at least one uppercase letter, one number, and one special character.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        ' Validate password and confirm password
        If password <> txtConfirmPassword.Text Then
            MessageBox.Show("Password and Confirm Password do not match.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        ' Validate that Security Question and Answer are selected/entered
        If cmbSecurityQuestion.SelectedIndex = -1 Then
            MessageBox.Show("Please select a Security Question.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        If String.IsNullOrWhiteSpace(txtSecurityAnswer.Text) Then
            MessageBox.Show("Please enter the Security Answer.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        ' Get the next UserID if not using Identity column
        Dim newUserID As Integer = GetNextUserID()

        ' Declare the SqlCommand variable before using it
        Dim cmd As New SqlCommand("INSERT INTO Users (UserID, Username, Password, SecurityQuestion, SecurityAnswer) " &
                                  "VALUES (@UserID, @Username, @Password, @SecurityQuestion, @SecurityAnswer)", conn)

        ' Add parameters to avoid SQL injection
        cmd.Parameters.AddWithValue("@UserID", newUserID) ' For non-Identity fields
        cmd.Parameters.AddWithValue("@Username", txtUsername.Text)
        cmd.Parameters.AddWithValue("@Password", txtPassword.Text) ' Store plain-text password (should be hashed)
        cmd.Parameters.AddWithValue("@SecurityQuestion", cmbSecurityQuestion.SelectedItem.ToString()) ' Selected Security Question
        cmd.Parameters.AddWithValue("@SecurityAnswer", txtSecurityAnswer.Text)

        ' Open connection, execute the query, and close the connection
        Try
            conn.Open()
            cmd.ExecuteNonQuery()
            MessageBox.Show("User added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            ClearFields()
            Me.Close()
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            conn.Close()
            Me.Close()
        End Try
    End Sub
    ' Method to generate the next UserID (if not using Identity)
    Private Function GetNextUserID() As Integer
        Dim cmd As New SqlCommand("SELECT MAX(UserID) FROM Users", conn)
        conn.Open()

        ' If no users, return 1 as the first UserID
        Dim result As Object = cmd.ExecuteScalar()
        conn.Close()

        If result Is DBNull.Value Then
            Return 1
        Else
            Return Convert.ToInt32(result) + 1 ' Increment the max UserID by 1
        End If
    End Function
    ' Function to clear all input fields
    Private Sub ClearFields()
        txtUsername.Clear()
        txtPassword.Clear()
        txtConfirmPassword.Clear()
        cmbSecurityQuestion.SelectedIndex = -1 ' Reset dropdown selection
        txtSecurityAnswer.Clear()
    End Sub

    ' Function to validate password criteria
    Private Function IsValidPassword(ByVal password As String) As Boolean
        Dim pattern As String = "^(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).{8,16}$"
        Return Regex.IsMatch(password, pattern)
    End Function

    Private Sub Button2_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.Close()
    End Sub

    Private Sub _11_adduser_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ' Add some sample security questions to the ComboBox
        cmbSecurityQuestion.Items.Add("What is your mother's maiden name?")
        cmbSecurityQuestion.Items.Add("What was the name of your first pet?")
        cmbSecurityQuestion.Items.Add("What is your favorite color?")
        cmbSecurityQuestion.Items.Add("What is your childhood nickname?")
        cmbSecurityQuestion.Items.Add("Where did you go to high school?")
    End Sub
End Class