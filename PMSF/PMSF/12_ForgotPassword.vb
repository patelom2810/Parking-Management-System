Imports System.Data.SqlClient


Public Class _12_ForgotPassword
    ' Database connection
    Dim conn As New SqlConnection("Data Source=.\SQLEXPRESS;AttachDbFilename=D:\PMS\PARKING_OM\PMSF\PMSF\PMSF.mdf;Integrated Security=True;User Instance=True")

    ' Track if the security question is shown
    Dim isQuestionShown As Boolean = False


    Private Sub btnForgotResetPassword_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnForgotResetPassword.Click
        ' Validate password fields
        If String.IsNullOrWhiteSpace(txtForgotNewPassword.Text) OrElse String.IsNullOrWhiteSpace(txtForgotConfirmPassword.Text) Then
            MessageBox.Show("Please enter and confirm your new password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        ' Check if passwords match
        If txtForgotNewPassword.Text <> txtForgotConfirmPassword.Text Then
            MessageBox.Show("Passwords do not match. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        ' Validate password strength
        Dim password As String = txtForgotNewPassword.Text
        Dim hasUpperCase As Boolean = password.Any(Function(c) Char.IsUpper(c))
        Dim hasLowerCase As Boolean = password.Any(Function(c) Char.IsLower(c))
        Dim hasDigit As Boolean = password.Any(Function(c) Char.IsDigit(c))
        Dim hasSpecialChar As Boolean = password.IndexOfAny("!@#$%^&*()_+={}[]|:;<>,.?/~`".ToCharArray()) <> -1

        If password.Length < 8 OrElse Not hasUpperCase OrElse Not hasLowerCase OrElse Not hasDigit OrElse Not hasSpecialChar Then
            MessageBox.Show("Password must be at least 8 characters long and include at least one uppercase letter, one lowercase letter, one digit, and one special character.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        ' Update the password in the database
        Dim cmd As New SqlCommand("UPDATE Users SET Password = @NewPassword WHERE Username = @Username", conn)
        cmd.Parameters.AddWithValue("@NewPassword", txtForgotNewPassword.Text)
        cmd.Parameters.AddWithValue("@Username", txtForgotUsername.Text)

        If conn.State = ConnectionState.Closed Then conn.Open()
        Dim rowsAffected As Integer = cmd.ExecuteNonQuery()
        conn.Close()

        ' Check if password was updated successfully
        If rowsAffected > 0 Then
            MessageBox.Show("Password reset successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            'Application.Restart() 
            Me.Close() ' Close form after successful reset
        Else
            MessageBox.Show("Error resetting password. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If

    End Sub

    Private Sub btnForgotVerifyAnswer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnForgotVerifyAnswer.Click
        ' Step 1: If security question is not shown, fetch it
        If Not isQuestionShown Then
            ' Validate username
            If String.IsNullOrWhiteSpace(txtForgotUsername.Text) Then
                MessageBox.Show("Please enter your username.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End If

            ' Fetch security question
            Dim cmd As New SqlCommand("SELECT SecurityQuestion FROM Users WHERE Username = @Username", conn)
            cmd.Parameters.AddWithValue("@Username", txtForgotUsername.Text)

            If conn.State = ConnectionState.Closed Then conn.Open()
            Dim questionResult As Object = cmd.ExecuteScalar()
            conn.Close()

            ' If username not found
            If questionResult Is Nothing Then
                MessageBox.Show("Username not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End If

            ' Show security question and answer fields
            lblForgotSecurityQuestion.Text = questionResult.ToString()
            lblForgotSecurityQuestion.Visible = True
            txtForgotSecurityAnswer.Visible = True

            ' Set flag to true so next click will verify answer
            isQuestionShown = True
            btnForgotVerifyAnswer.Text = "Verify Answer" ' Change button text
        Else
            ' Step 2: Validate security answer
            If String.IsNullOrWhiteSpace(txtForgotSecurityAnswer.Text) Then
                MessageBox.Show("Please enter the security answer.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End If

            ' Check if the answer is correct
            Dim answerCmd As New SqlCommand("SELECT COUNT(*) FROM Users WHERE Username = @Username AND SecurityAnswer = @SecurityAnswer", conn)
            answerCmd.Parameters.AddWithValue("@Username", txtForgotUsername.Text)
            answerCmd.Parameters.AddWithValue("@SecurityAnswer", txtForgotSecurityAnswer.Text)

            If conn.State = ConnectionState.Closed Then conn.Open()
            Dim answerResult As Integer = Convert.ToInt32(answerCmd.ExecuteScalar())
            conn.Close()

            ' If answer is correct, show password reset fields
            If answerResult > 0 Then
                txtForgotNewPassword.Visible = True
                txtForgotConfirmPassword.Visible = True
                btnForgotResetPassword.Visible = True
                lblenp.Visible = True
                lblcp.Visible = True
                MessageBox.Show("Answer verified successfully. You can now reset your password.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                MessageBox.Show("Incorrect security answer.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        End If
    End Sub

    Private Sub _12_ForgotPassword_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ' Hide security and reset password fields initially
        lblForgotSecurityQuestion.Visible = False
        txtForgotSecurityAnswer.Visible = False
        txtForgotNewPassword.Visible = False
        txtForgotConfirmPassword.Visible = False
        btnForgotResetPassword.Visible = False
        lblenp.Visible = False
        lblcp.Visible = False
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.Close()
    End Sub

    Private Sub LinkLabel1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LinkLabel1.Click

    End Sub

    Private Sub LinkLabel1_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        Me.Close()
    End Sub
End Class