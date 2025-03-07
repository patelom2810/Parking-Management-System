Imports System.Data.SqlClient
Public Class _7_Recordofvehical
    ' Connection string including table name
    Private connectionString As String = "Data Source=.\SQLEXPRESS;AttachDbFilename=D:\PMS\PARKING_OM\PMSF\PMSF\PMSF.mdf;Integrated Security=True;User Instance=True"

    Private Sub _7_Recordofvehical_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        LoadData()
    End Sub
    ' Method to load all data from the table into the DataGridView
    Private Sub LoadData()
        Try
            Using conn As New SqlConnection(connectionString)
                Dim query As String = "SELECT * FROM EntryExit" ' Correctly declaring query within the method
                Dim adapter As New SqlDataAdapter(query, conn)
                Dim table As New DataTable()
                adapter.Fill(table)
                DataGridView1.DataSource = table
            End Using
        Catch ex As Exception
            MessageBox.Show("Error loading data: {ex.Message}")
        End Try
    End Sub
    Private Sub FilterDataByDate(ByVal selectedDate As Date)
        Try
            Using conn As New SqlConnection(connectionString)
                Dim query As String = "SELECT * FROM EntryExit WHERE CAST([EntryDate] AS DATE) = @SelectedDate"
                Dim command As New SqlCommand(query, conn)
                command.Parameters.AddWithValue("@SelectedDate", selectedDate.Date)
                Dim adapter As New SqlDataAdapter(command)
                Dim table As New DataTable()
                adapter.Fill(table)
                DataGridView1.DataSource = table
            End Using
        Catch ex As Exception
            MessageBox.Show("Error filtering data: {ex.Message}")
        End Try
    End Sub



    Private Sub DateTimePicker1_ValueChanged_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DateTimePicker1.ValueChanged
        FilterDataByDate(DateTimePicker1.Value)
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        LoadData()
    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        ' Get the vehicle number from the textbox
        Dim vehicleNumber As String = txtVNumber.Text

        ' Check if the vehicle number is not empty
        If Not String.IsNullOrEmpty(vehicleNumber) Then
            Try
                ' Create a SQL query to delete the entry based on the vehicle number
                Dim query As String = "DELETE FROM EntryExit WHERE VehicleNumber = @VehicleNumber"

                ' Create a connection to the database (update connection string)
                Using conn As New SqlConnection(connectionString)
                    ' Open the connection
                    conn.Open()

                    ' Create a SQL command object
                    Using cmd As New SqlCommand(query, conn)
                        ' Add the vehicle number parameter to the SQL query
                        cmd.Parameters.AddWithValue("@VehicleNumber", vehicleNumber)

                        ' Execute the query
                        Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

                        ' Check if the deletion was successful
                        If rowsAffected > 0 Then
                            MessageBox.Show("Data deleted successfully.")
                        Else
                            MessageBox.Show("No data found for the given vehicle number.")
                        End If
                    End Using
                End Using
            Catch ex As Exception
                ' Show any errors
                MessageBox.Show("Error: " & ex.Message)
            End Try
        Else
            MessageBox.Show("Please enter a vehicle number.")
        End If
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
End Class