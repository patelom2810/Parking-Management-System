Imports System.Data.SqlClient
Public Class _6_vehicalsearch
    ' Connection string to the database
    Dim connectionString As String = "Data Source=.\SQLEXPRESS;AttachDbFilename=D:\PMS\PARKING_OM\PMSF\PMSF\PMSF.mdf;Integrated Security=True;User Instance=True"
    Dim connection As SqlConnection
    Private Sub Button8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button8.Click
        ' Validate if the vehicle number is entered
        If txtVehicleNumber.Text.Trim() = "" Then
            MessageBox.Show("Please enter a vehicle number.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        Try
            connection = New SqlConnection(connectionString)
            connection.Open()

            ' Query to fetch data based on VehicleNumber
            Dim query As String = "SELECT * FROM EntryExit WHERE VehicleNumber = @VehicleNumber"
            Dim command As New SqlCommand(query, connection)
            command.Parameters.AddWithValue("@VehicleNumber", txtVehicleNumber.Text.Trim())

            Dim reader As SqlDataReader = command.ExecuteReader()
            If reader.Read() Then
                ' Display fetched data, handling DBNull
                lblVehicleNumber.Text = txtVehicleNumber.Text.Trim()
                lblName.Text = If(reader.IsDBNull(reader.GetOrdinal("Name")), "N/A", reader("Name").ToString())
                lblContact.Text = If(reader.IsDBNull(reader.GetOrdinal("Contactno")), "N/A", reader("Contactno").ToString())
                lblDate.Text = If(reader.IsDBNull(reader.GetOrdinal("EntryDate")), "N/A", Convert.ToDateTime(reader("EntryDate")).ToString("dd/MM/yyyy"))
                lblTime.Text = If(reader.IsDBNull(reader.GetOrdinal("EntryTime")), "N/A", reader("EntryTime").ToString())
                lblFloor.Text = If(reader.IsDBNull(reader.GetOrdinal("FloorNo")), "N/A", reader("FloorNo").ToString())
                lblVehicleType.Text = If(reader.IsDBNull(reader.GetOrdinal("VehicleType")), "N/A", reader("VehicleType").ToString())
                lblvexitdate.Text = If(reader.IsDBNull(reader.GetOrdinal("ExitDate")), "N/A", Convert.ToDateTime(reader("ExitDate")).ToString("dd/MM/yyyy"))
                ' Exit Time Handling
                Dim exitTime As Object = reader("ExitTime")
                If IsDBNull(exitTime) OrElse String.IsNullOrWhiteSpace(exitTime.ToString()) OrElse exitTime.ToString() = "00:00:00" Then
                    lblExitTime.Text = "N/A"
                    lblParkedStatus.Text = "Vehicle is currently parked."
                    lblParkedStatus.Visible = True
                Else
                    Dim validExitTime As DateTime
                    If DateTime.TryParse(exitTime.ToString(), validExitTime) Then
                        lblExitTime.Text = validExitTime.ToString("HH:mm:ss")
                        lblParkedStatus.Visible = False
                    Else
                        lblExitTime.Text = "Invalid Time"
                        lblParkedStatus.Text = "Vehicle has invalid exit time."
                        lblParkedStatus.Visible = True
                    End If
                End If

                ' Amount Calculation
                Dim rawAmount As Object = reader("pay")
                lblPay.Text = If(IsDBNull(rawAmount), "0", Math.Ceiling(Convert.ToDouble(rawAmount)).ToString())
            Else
                MessageBox.Show("No data found for the entered vehicle number.", "No Data", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

            reader.Close()
        Catch ex As SqlException
            MessageBox.Show("Database error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            MessageBox.Show("An error occurred: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If connection IsNot Nothing AndAlso connection.State = ConnectionState.Open Then
                connection.Close()
            End If
        End Try
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        ' Clear all textboxes and labels
        txtVehicleNumber.Text = "" ' Clear the TextBox
        lblVehicleNumber.Text = "" ' Clear the Vehicle Number label
        lblName.Text = ""         ' Clear all Labels
        lblContact.Text = ""
        lblDate.Text = ""
        lblTime.Text = ""
        lblExitTime.Text = ""
        lblPay.Text = ""
        lblFloor.Text = ""
        lblVehicleType.Text = ""
        lblvexitdate.Text = ""
        lblParkedStatus.Visible = False ' Hide the parked status on refresh
        txtVehicleNumber.Focus() ' Focus back to the Vehicle Number textbox
    End Sub

    Private Sub Panel1_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Panel1.Paint

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

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.Close() ' Closes the current form
    End Sub

    Private Sub _6_vehicalsearch_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub
End Class