Imports System.Data.SqlClient
Public Class _4_vexit
    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        HandleVehicleExit()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        ' Get the entered vehicle number
        Dim enteredVehicleNumber As String = txtVehicleNumber.Text.Trim()

        If String.IsNullOrEmpty(enteredVehicleNumber) Then
            MessageBox.Show("Please enter a vehicle number.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        ' Database connection string (update with your database details)
        Dim connectionString As String = "Data Source=.\SQLEXPRESS;AttachDbFilename=D:\PMS\PARKING_OM\PMSF\PMSF\PMSF.mdf;Integrated Security=True;User Instance=True"
        Dim query As String = "SELECT FloorNo, VehicleType, VehicleNumber, EntryDate, EntryTime, ExitTime, ExitDate, pay " & _
                              "FROM EntryExit " & _
                              "WHERE VehicleNumber = @VehicleNumber"

        Try
            Using connection As New SqlConnection(connectionString)
                Using command As New SqlCommand(query, connection)
                    command.Parameters.AddWithValue("@VehicleNumber", enteredVehicleNumber)

                    connection.Open()

                    Using reader As SqlDataReader = command.ExecuteReader()
                        If reader.Read() Then
                            ' Fetch data from the database
                            Dim floorID As Integer = Convert.ToInt32(reader("FloorNo"))
                            Dim comboBoxValue As String = reader("VehicleType").ToString()
                            Dim vehicleNumber As String = reader("VehicleNumber").ToString()
                            Dim dateField As Date = Convert.ToDateTime(reader("EntryDate"))
                            Dim timeField As String = reader("EntryTime").ToString()
                            Dim exitTime As String = If(IsDBNull(reader("ExitTime")), String.Empty, reader("ExitTime").ToString())
                            Dim exitDate As Date = If(IsDBNull(reader("ExitDate")), Date.MinValue, Convert.ToDateTime(reader("ExitDate")))

                            ' If exitTime is already set, print receipt only, prevent further exit
                            If Not String.IsNullOrEmpty(exitTime) Then
                                MessageBox.Show("Vehicle has already exited. Printing receipt.", "Vehicle Exited", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                ' Open the new form to display data
                                Dim payslipForm As New _5_Receip()
                                payslipForm.SetPayslipData(floorID, comboBoxValue, vehicleNumber, dateField, timeField, exitDate, exitTime, reader("pay").ToString())
                                payslipForm.Show()
                                ClearFields()
                                Exit Sub
                            End If

                            ' If exitTime is empty, proceed with exit handling
                            HandleVehicleExit()
                        Else
                            MessageBox.Show("No record found for the entered vehicle number.", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        End If
                    End Using
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("An error occurred: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub HandleVehicleExit()
        Dim conn As SqlConnection = Nothing ' Declare conn at the start of the method
        Try
            ' Fetch vehicle number from the textbox
            Dim vehicleNumber As String = txtVehicleNumber.Text.Trim()
            If String.IsNullOrEmpty(vehicleNumber) Then
                MessageBox.Show("Please enter a valid vehicle number.")
                Exit Sub
            End If

            ' Initialize variables
            Dim entryDate As DateTime
            Dim entryTime As TimeSpan
            Dim exitDateTime As DateTime
            Dim payment As Decimal = 0
            Dim vehicleType As String = String.Empty
            Dim floorID As String = String.Empty ' To store the FloorID

            ' Check if the DateTimePicker has a valid value
            If Not DateTimePickerExitDate.Checked Then
                MessageBox.Show("Please select a valid exit date and time.")
                Exit Sub
            Else
                exitDateTime = DateTimePickerExitDate.Value
            End If

            ' SQL query to fetch data for the given vehicle number
            Dim fetchQuery As String = "SELECT EntryDate, EntryTime, VehicleType, FloorNo, ExitTime, ExitDate, pay FROM EntryExit WHERE VehicleNumber = @VehicleNumber"

            ' Open the connection inside the Try block
            conn = New SqlConnection("Data Source=.\SQLEXPRESS;AttachDbFilename=D:\PMS\PARKING_OM\PMSF\PMSF\PMSF.mdf;Integrated Security=True;User Instance=True")
            conn.Open()

            ' Fetch entry date, time, vehicle type, floor ID, and other necessary fields
            Using fetchCmd As New SqlCommand(fetchQuery, conn)
                fetchCmd.Parameters.AddWithValue("@VehicleNumber", vehicleNumber)

                Dim reader As SqlDataReader = fetchCmd.ExecuteReader()
                If reader.HasRows Then
                    reader.Read()

                    ' Check if the vehicle has already exited
                    If Not String.IsNullOrEmpty(reader("ExitDate").ToString()) AndAlso Not String.IsNullOrEmpty(reader("ExitTime").ToString()) AndAlso Not String.IsNullOrEmpty(reader("pay").ToString()) Then
                        ' Vehicle has already exited, display the result
                        MessageBox.Show("Vehicle has already exited. Printing receipt.", "Vehicle Exited", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        ' Open the new form to display the receipt
                        Dim payslipForm As New _5_Receip()
                        payslipForm.SetPayslipData(Convert.ToInt32(reader("FloorNo")), reader("VehicleType").ToString(), vehicleNumber, Convert.ToDateTime(reader("EntryDate")), reader("EntryTime").ToString(), reader("ExitTime").ToString(), reader("pay").ToString(), Convert.ToDateTime(reader("ExitDate")))
                        payslipForm.Show()

                        ' Populate the textbox with exit time and payment values
                        txtExitTime.Text = reader("ExitTime").ToString() ' Assuming you have a textbox for Exit Time
                        txtPayment.Text = reader("pay").ToString() ' Assuming you have a textbox for Pay

                        ClearFields()
                        Exit Sub
                    Else
                        ' Vehicle hasn't exited yet, continue with exit process
                        entryDate = Convert.ToDateTime(reader("EntryDate"))
                        entryTime = TimeSpan.Parse(reader("EntryTime").ToString())
                        vehicleType = reader("VehicleType").ToString()
                        floorID = reader("FloorNo").ToString() ' Fetch the FloorID
                    End If
                    reader.Close()
                Else
                    MessageBox.Show("Vehicle number not found!")
                    Exit Sub
                End If
            End Using

            ' Combine entry date and time
            Dim entryDateTime As DateTime = entryDate.Add(entryTime)

            ' Calculate duration
            Dim duration As TimeSpan = exitDateTime - entryDateTime
            Dim totalMinutes As Decimal = Convert.ToDecimal(duration.TotalMinutes)

            If totalMinutes < 0 Then
                MessageBox.Show("Error: Exit time is earlier than entry time. Please check the data.")
                Exit Sub
            End If

            ' Determine rate and calculate payment
            Select Case vehicleType.Trim().ToUpper()
                Case "TWO-WHEELER"
                    payment = totalMinutes * (20 / 60D) ' ₹20 per hour
                Case "THREE-WHEELER"
                    payment = totalMinutes * (30 / 60D) ' ₹30 per hour
                Case "FOUR-WHEELER"
                    payment = totalMinutes * (50 / 60D) ' ₹50 per hour
                Case "HEAVY VEHICLE"
                    payment = totalMinutes * (100 / 60D) ' ₹100 per hour
                Case Else
                    MessageBox.Show("Unknown vehicle type!")
                    Exit Sub
            End Select

            ' Round payment to the nearest integer (e.g., 1.25 -> 2, 1.50 -> 2, 1.75 -> 2)
            payment = Math.Ceiling(payment)

            ' Populate the textbox with calculated exit time and payment values
            txtExitTime.Text = exitDateTime.ToString("HH:mm:ss") ' Assuming you have a textbox for Exit Time
            txtPayment.Text = payment.ToString() ' Assuming you have a textbox for Pay

            ' SQL query to update the database with exit details and payment
            Dim updateQuery As String = "UPDATE EntryExit SET ExitDate = @ExitDate, ExitTime = @ExitTime, pay = @Payment WHERE VehicleNumber = @VehicleNumberField"

            Using updateCmd As New SqlCommand(updateQuery, conn)
                ' Update the parameters to match the corrected column names
                updateCmd.Parameters.AddWithValue("@ExitDate", exitDateTime.ToString("yyyy-MM-dd")) ' Adjusted for ExitDate column
                updateCmd.Parameters.AddWithValue("@ExitTime", exitDateTime.ToString("HH:mm:ss"))  ' Adjusted for etimefield column
                updateCmd.Parameters.AddWithValue("@Payment", payment)                             ' Adjusted for pay column
                updateCmd.Parameters.AddWithValue("@VehicleNumberField", vehicleNumber)            ' Ensure VehicleNumberField matches your table

                If updateCmd.ExecuteNonQuery() > 0 Then
                    MessageBox.Show("Exit date, time, and payment updated successfully.")
                Else
                    MessageBox.Show("Failed to update the record.")
                End If
            End Using

            ' Update the available capacity in the Floors table
            Dim updateCapacityQuery As String = "UPDATE Floors SET AvailableCapacity = AvailableCapacity + 1 WHERE FloorNo = @FloorNo"

            Using updateCapacityCmd As New SqlCommand(updateCapacityQuery, conn)
                updateCapacityCmd.Parameters.AddWithValue("@FloorNo", floorID)
                updateCapacityCmd.ExecuteNonQuery()
            End Using

        Catch ex As Exception
            MessageBox.Show("An error occurred: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If conn IsNot Nothing AndAlso conn.State = ConnectionState.Open Then
                conn.Close()
            End If
        End Try
    End Sub
    Private Sub ClearFields()
        ' Clear TextBox fields
        txtVehicleNumber.Clear()
        txtExitTime.Clear()
        txtPayment.Clear()
        ' Clear DateTimePicker fields
        DateTimePickerExitDate.Value = DateTime.Now  ' or set to a default date

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

    Private Sub _4_vexit_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ' Set the initial placeholder for Vehicle Number TextBox
        SetPlaceholder(txtVehicleNumber, "XX XX XX XXXX")
    End Sub
    ' Event when Vehicle Number TextBox gains focus
    Private Sub txtVNumber_GotFocus(ByVal sender As Object, ByVal e As EventArgs) Handles txtVehicleNumber.GotFocus
        RemovePlaceholder(txtVehicleNumber, "XX XX XX XXXX")
    End Sub

    ' Event when Vehicle Number TextBox loses focus
    Private Sub txtVNumber_LostFocus(ByVal sender As Object, ByVal e As EventArgs) Handles txtVehicleNumber.LostFocus
        SetPlaceholder(txtVehicleNumber, "XX XX XX XXXX")
    End Sub

    ' Function to set placeholder
    Private Sub SetPlaceholder(ByVal txt As TextBox, ByVal placeholder As String)
        If String.IsNullOrWhiteSpace(txt.Text) Then
            txt.Text = placeholder
            txt.ForeColor = Color.Gray
        End If
    End Sub

    ' Function to remove placeholder
    Private Sub RemovePlaceholder(ByVal txt As TextBox, ByVal placeholder As String)
        If txt.Text = placeholder Then
            txt.Text = ""
            txt.ForeColor = Color.Black
        End If
    End Sub

End Class