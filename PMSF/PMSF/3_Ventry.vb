Imports System.Data.SqlClient
Public Class _3_Ventry
    ' Connection string to the database
    Dim connectionString As String = "Data Source=.\SQLEXPRESS;AttachDbFilename=D:\PMS\PARKING_OM\PMSF\PMSF\PMSF.mdf;Integrated Security=True;User Instance=True"
    Private Sub button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles button1.Click
        ' Validate input fields
        If String.IsNullOrEmpty(txtName.Text) OrElse String.IsNullOrEmpty(txtCoNumber.Text) OrElse String.IsNullOrEmpty(txtVNumber.Text) Then
            MessageBox.Show("Please fill in all required fields.")
            Return
        End If
        If vtype.SelectedItem Is Nothing Then
            MessageBox.Show("Please select a vehicle type.")
            Return
        End If
        If pfloor.SelectedItem Is Nothing Then
            MessageBox.Show("Please select a floor.")
            Return
        End If

        ' Get input values
        Dim selectedDate As DateTime = dtpDate.Value
        Dim currentTime As String = DateTime.Now.ToString("HH:mm:ss")
        txtVEntryTime.Text = currentTime
        Dim floorID As String = pfloor.SelectedItem.ToString()

        ' Check parking capacity
        If CheckParkingCapacity(floorID) Then
            ' Save data if capacity is available
            SaveData(selectedDate, currentTime, txtName.Text, txtCoNumber.Text, txtVNumber.Text, vtype.SelectedItem.ToString(), floorID)
        Else
            MessageBox.Show("Parking full on the selected floor.")
        End If
    End Sub

    Private Sub _3_Ventry_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ' Populate the pfloor ComboBox with floor IDs
        Dim floorIDs As List(Of String) = GetFloorNumbers()
        For Each floorID As String In floorIDs
            pfloor.Items.Add(floorID)
        Next

        ' Set the initial placeholder for Vehicle Number TextBox
        SetPlaceholder(txtVNumber, "XX XX XX XXXX")
    End Sub
    ' Method to check parking capacity
    Private Function CheckParkingCapacity(ByVal floorID As String) As Boolean
        Dim available As Boolean = False
        Try
            Using con As New SqlConnection(connectionString)
                con.Open()
                Dim query As String = "SELECT AvailableCapacity FROM Floors WHERE FloorNo = @FloorNo"
                Using cmd As New SqlCommand(query, con)
                    cmd.Parameters.AddWithValue("@FloorNo", floorID)
                    Using reader As SqlDataReader = cmd.ExecuteReader()
                        If reader.Read() Then
                            Dim availableCapacity As Integer = Convert.ToInt32(reader("AvailableCapacity"))
                            available = availableCapacity > 0
                        End If
                    End Using
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        End Try
        Return available
    End Function
    ' Add this in your form's code
    Private Sub VehicleNumberTextBox_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtVNumber.TextChanged
        ' Remove any existing spaces and non-alphanumeric characters
        Dim text As String = txtVNumber.Text.Replace(" ", "").ToUpper()

        ' Check if the text length is greater than 12 (for a valid vehicle number)
        If text.Length > 12 Then
            text = text.Substring(0, 12)
        End If

        ' Insert spaces after every 2 characters (for the format xx xx xx xxxx)
        If text.Length > 2 Then
            text = text.Insert(2, " ")
        End If
        If text.Length > 5 Then
            text = text.Insert(5, " ")
        End If
        If text.Length > 8 Then
            text = text.Insert(8, " ")
        End If

        ' Set the text back to the TextBox
        txtVNumber.Text = text

        ' Move the cursor to the end of the text
        txtVNumber.SelectionStart = txtVNumber.Text.Length
    End Sub

    ' SaveData method with validations
    Private Sub SaveData(ByVal selectedDate As DateTime, ByVal currentTime As String, ByVal name As String, ByVal contactNumber As String, ByVal vehicleNumber As String, ByVal comboValue As String, ByVal floor As String)
        Try
            ' Validation: Ensure all fields are provided
            If String.IsNullOrWhiteSpace(name) OrElse String.IsNullOrWhiteSpace(contactNumber) OrElse String.IsNullOrWhiteSpace(vehicleNumber) OrElse String.IsNullOrWhiteSpace(comboValue) OrElse String.IsNullOrWhiteSpace(floor) Then
                MessageBox.Show("All fields are required!")
                Return
            End If

            ' Validation: Ensure the contact number is exactly 10 digits and is not '1234567890' or '0000000000'
            If contactNumber.Length <> 10 OrElse Not IsNumeric(contactNumber) OrElse contactNumber = "1234567890" OrElse contactNumber = "0000000000" Then
                MessageBox.Show("Please enter a valid 10-digit contact number.")
                Return
            End If

            ' Validate if the field is not empty
            If String.IsNullOrWhiteSpace(vehicleNumber) Then
                MessageBox.Show("Vehicle number is required.")
                Return
            End If

            ' Database operations
            Using con As New SqlConnection(connectionString)
                con.Open()

                ' Query to get the highest SR.NO
                Dim srNoQuery As String = "SELECT ISNULL(MAX([SR.NO]), 0) + 1 FROM EntryExit"
                Dim srNo As Integer
                Using srNoCmd As New SqlCommand(srNoQuery, con)
                    srNo = Convert.ToInt32(srNoCmd.ExecuteScalar()) ' Get the next SR.NO
                End Using

                ' Insert into VehicleTable, now with the generated SR.NO
                Dim vehicleQuery As String = "INSERT INTO EntryExit ([SR.NO], EntryDate, EntryTime, Name, Contactno, VehicleNumber, VehicleType, FloorNo, ExitTime) " &
                                             "VALUES (@SRNo, @EntryDate, @EntryTime, @Name, @Contactno, @VehicleNumber, @VehicleType, @FloorNo, @ExitTime)"
                Using cmd As New SqlCommand(vehicleQuery, con)
                    cmd.Parameters.AddWithValue("@SRNo", srNo)
                    cmd.Parameters.AddWithValue("@EntryDate", selectedDate.Date)
                    cmd.Parameters.AddWithValue("@EntryTime", currentTime)
                    cmd.Parameters.AddWithValue("@Name", name)
                    cmd.Parameters.AddWithValue("@Contactno", contactNumber)
                    cmd.Parameters.AddWithValue("@VehicleNumber", vehicleNumber)
                    cmd.Parameters.AddWithValue("@VehicleType", comboValue)
                    cmd.Parameters.AddWithValue("@FloorNo", floor)
                    cmd.Parameters.AddWithValue("@ExitTime", "00:00:00") ' Default value for etimefield
                    cmd.ExecuteNonQuery()
                End Using

                ' Update available capacity in Floors table
                Dim updateQuery As String = "UPDATE Floors SET AvailableCapacity = AvailableCapacity - 1 WHERE FloorNo = @FloorNo"
                Using updateCmd As New SqlCommand(updateQuery, con)
                    updateCmd.Parameters.AddWithValue("@FloorNo", floor)
                    updateCmd.ExecuteNonQuery()
                End Using

                MessageBox.Show("Vehicle entry recorded successfully!")
            End Using
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        End Try
    End Sub



    ' Method to retrieve FloorIDs
    Private Function GetFloorNumbers() As List(Of String)
        Dim floorIDs As New List(Of String)
        Try
            Using con As New SqlConnection(connectionString)
                con.Open()
                Dim query As String = "SELECT FloorNo FROM Floors"
                Using cmd As New SqlCommand(query, con)
                    Using reader As SqlDataReader = cmd.ExecuteReader()
                        While reader.Read()
                            floorIDs.Add(reader("FloorNo").ToString())
                        End While
                    End Using
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        End Try
        Return floorIDs
    End Function

    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        ' for get current time get button
        txtVEntryTime.Text = DateTime.Now.ToString("HH:mm:ss")
    End Sub

    Private Sub Button9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button9.Click
        ' Get the entered vehicle number
        Dim enteredVehicleNumber As String = txtVNumber.Text

        ' Check if the VehicleNumberField is empty
        If String.IsNullOrEmpty(enteredVehicleNumber) Then
            MessageBox.Show("Please enter a Vehicle Number.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        ' SQL query to fetch required data
        Dim query As String = "SELECT FloorNo, VehicleNumber, EntryDate, EntryTime " &
                              "FROM EntryExit " &
                              "WHERE VehicleNumber = @VehicleNumber"

        ' Database connection and data fetching
        Using connection As New SqlConnection(connectionString)
            Using command As New SqlCommand(query, connection)
                command.Parameters.AddWithValue("@VehicleNumber", enteredVehicleNumber)

                connection.Open()
                Using reader As SqlDataReader = command.ExecuteReader()
                    If reader.Read() Then
                        ' Fetch data from the database
                        Dim floorID As Integer = Convert.ToInt32(reader("FloorNo"))
                        Dim vehicleNumber As String = reader("VehicleNumber").ToString()
                        Dim dateField As Date = Convert.ToDateTime(reader("EntryDate"))
                        Dim timeField As String = reader("EntryTime").ToString()

                        ' Pass the data to the Payslip Form
                        Dim payslipForm As New _10_entrytoken()
                        payslipForm.SetPayslipData(floorID, vehicleNumber, dateField, timeField)
                        payslipForm.Show()
                        ClearFields()
                    Else
                        MessageBox.Show("No record found for the entered vehicle number.", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If
                End Using
            End Using
        End Using
    End Sub

    Private Sub dtpDate_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtpDate.ValueChanged

    End Sub
    Private Sub label5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles label5.Click

    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub
    Private Sub ClearFields()
        ' Clear TextBox fields
        txtName.Clear()
        txtCoNumber.Clear()
        txtVNumber.Clear()
        txtVEntryTime.Clear()
        ' Reset ComboBox (set to default or first item)
        vtype.SelectedIndex = -1  ' or ComboBoxField.SelectedIndex = 0 for default item

        ' Clear DateTimePicker fields
        dtpDate.Value = DateTime.Now  ' or set to a default date

        ' Reset other fields, e.g., if you have a FloorID field, set it back to default
        pfloor.SelectedIndex = -1   ' Or set to some default value

        ' You can also reset other fields if necessary, like RadioButtons, CheckBoxes, etc.
        ' RadioButtonField.Checked = False
        ' CheckBoxField.Checked = False
    End Sub
    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        ClearFields()
    End Sub

    Private Sub Button8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

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
    ' Event when Vehicle Number TextBox gains focus
    Private Sub txtVNumber_GotFocus(ByVal sender As Object, ByVal e As EventArgs) Handles txtVNumber.GotFocus
        RemovePlaceholder(txtVNumber, "XX XX XX XXXX")
    End Sub

    ' Event when Vehicle Number TextBox loses focus
    Private Sub txtVNumber_LostFocus(ByVal sender As Object, ByVal e As EventArgs) Handles txtVNumber.LostFocus
        SetPlaceholder(txtVNumber, "XX XX XX XXXX")
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