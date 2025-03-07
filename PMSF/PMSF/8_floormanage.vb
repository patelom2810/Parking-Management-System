Imports System.Data.SqlClient
Public Class _8_floormanage
    Dim con As SqlConnection
    Dim cmd As SqlCommand
    Dim reader As SqlDataReader
    Dim query As String

    ' Connection string to the database
    Dim connectionString As String = "Data Source=.\SQLEXPRESS;AttachDbFilename=D:\PMS\PARKING_OM\PMSF\PMSF\PMSF.mdf;Integrated Security=True;User Instance=True"
    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click

        ' Validate inputs
        If String.IsNullOrWhiteSpace(txtFloorNumber.Text) Then
            MessageBox.Show("Floor No is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtFloorNumber.Focus()
            Exit Sub
        End If

        If String.IsNullOrWhiteSpace(txtFloorName.Text) Then
            MessageBox.Show("Floor Name is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtFloorName.Focus()
            Exit Sub
        End If

        If String.IsNullOrWhiteSpace(txtFloorCapacity.Text) Then
            MessageBox.Show("Total Capacity is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtFloorCapacity.Focus()
            Exit Sub
        End If

        Dim totalCapacity As Integer
        If Not Integer.TryParse(txtFloorCapacity.Text, totalCapacity) OrElse totalCapacity <= 0 Then
            MessageBox.Show("Total Capacity must be a valid positive number.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtFloorCapacity.Focus()
            Exit Sub
        End If

        con = New SqlConnection(connectionString)
        Try
            con.Open()

            ' Check if FloorID already exists
            query = "SELECT COUNT(*) FROM Floors WHERE FloorNo = @FloorNo"
            cmd = New SqlCommand(query, con)
            cmd.Parameters.AddWithValue("@FloorNo", txtFloorNumber.Text.Trim())
            Dim recordCount As Integer = Convert.ToInt32(cmd.ExecuteScalar())

            If recordCount > 0 Then
                MessageBox.Show("A floor with this no already exists. Please enter a unique Floor No.", "Duplicate Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If

            ' Insert new floor
            query = "INSERT INTO Floors (FloorNo, FloorName, TotalCapacity, AvailableCapacity) VALUES (@FloorNo, @FloorName, @TotalCapacity, @AvailableCapacity)"
            cmd = New SqlCommand(query, con)
            cmd.Parameters.AddWithValue("@FloorNo", txtFloorNumber.Text.Trim())
            cmd.Parameters.AddWithValue("@FloorName", txtFloorName.Text.Trim())
            cmd.Parameters.AddWithValue("@TotalCapacity", totalCapacity)
            cmd.Parameters.AddWithValue("@AvailableCapacity", totalCapacity) ' Initially AvailableCapacity = TotalCapacity
            cmd.ExecuteNonQuery()

            MessageBox.Show("Floor added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            LoadFloors()
            ClearFields()
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            con.Close()
        End Try
    End Sub


    ' Method to Load Floors into DataGridView
    Private Sub LoadFloors()
        con = New SqlConnection(connectionString)
        Try
            con.Open()
            query = "SELECT * FROM Floors"
            cmd = New SqlCommand(query, con)
            Dim adapter As New SqlDataAdapter(cmd)
            Dim table As New DataTable()
            adapter.Fill(table)
            DataGridView1.DataSource = table
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        Finally
            con.Close()
        End Try
    End Sub
    ' Method to fetch FloorNumber list from database
    Public Function GetFloorNumbers() As List(Of String)
        Dim floorNumbers As New List(Of String)
        con = New SqlConnection(connectionString)
        Try
            con.Open()
            query = "SELECT FloorNo FROM Floors"
            cmd = New SqlCommand(query, con)
            reader = cmd.ExecuteReader()

            ' Populate the list with FloorNumber values
            While reader.Read()
                floorNumbers.Add(reader("FloorNo").ToString())
            End While
            reader.Close()
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        Finally
            con.Close()
        End Try
        Return floorNumbers
    End Function

    Private Sub btnUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdate.Click

        ' Validate inputs
        If String.IsNullOrWhiteSpace(txtFloorNumber.Text) Then
            MessageBox.Show("Floor No is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtFloorNumber.Focus()
            Exit Sub
        End If

        If String.IsNullOrWhiteSpace(txtFloorName.Text) Then
            MessageBox.Show("Floor Name is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtFloorName.Focus()
            Exit Sub
        End If

        If String.IsNullOrWhiteSpace(txtFloorCapacity.Text) Then
            MessageBox.Show("Total Capacity is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtFloorCapacity.Focus()
            Exit Sub
        End If

        Dim totalCapacity As Integer
        If Not Integer.TryParse(txtFloorCapacity.Text, totalCapacity) OrElse totalCapacity < 0 Then
            MessageBox.Show("Total Capacity must be a valid positive number.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtFloorCapacity.Focus()
            Exit Sub
        End If

        ' Database connection and update operation
        con = New SqlConnection(connectionString)
        Try
            con.Open()
            query = "UPDATE Floors SET FloorName = @FloorName, TotalCapacity = @TotalCapacity, AvailableCapacity = @TotalCapacity WHERE FloorNo = @FloorNo"
            cmd = New SqlCommand(query, con)
            cmd.Parameters.AddWithValue("@FloorNo", txtFloorNumber.Text.Trim())
            cmd.Parameters.AddWithValue("@FloorName", txtFloorName.Text.Trim())
            cmd.Parameters.AddWithValue("@TotalCapacity", totalCapacity)
            cmd.ExecuteNonQuery()

            MessageBox.Show("Floor updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            LoadFloors()
            ClearFields()
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            con.Close()
        End Try
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click

        ' Validate Floor ID
        If String.IsNullOrWhiteSpace(txtFloorNumber.Text) Then
            MessageBox.Show("Floor No is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtFloorNumber.Focus()
            Exit Sub
        End If

        ' Confirm deletion
        Dim confirm As DialogResult = MessageBox.Show("Are you sure you want to delete this floor?", "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If confirm <> DialogResult.Yes Then Exit Sub

        ' Database connection and delete operation
        con = New SqlConnection(connectionString)
        Try
            con.Open()

            ' Check if FloorID exists before deletion
            query = "SELECT COUNT(*) FROM Floors WHERE FloorNo = @FloorNo"
            cmd = New SqlCommand(query, con)
            cmd.Parameters.AddWithValue("@FloorNo", txtFloorNumber.Text.Trim())
            Dim recordCount As Integer = Convert.ToInt32(cmd.ExecuteScalar())

            If recordCount = 0 Then
                MessageBox.Show("Floor ID not found.", "Deletion Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If

            ' Proceed with deletion
            query = "DELETE FROM Floors WHERE FloorNo = @FloorNo"
            cmd = New SqlCommand(query, con)
            cmd.Parameters.AddWithValue("@FloorNo", txtFloorNumber.Text.Trim())
            cmd.ExecuteNonQuery()

            MessageBox.Show("Floor deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            LoadFloors()
            ClearFields()
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            con.Close()
        End Try
    End Sub

    Private Sub DataGridView1_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        If e.RowIndex >= 0 Then
            Dim row As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
            txtFloorNumber.Text = row.Cells(0).Value.ToString()
            txtFloorName.Text = row.Cells(1).Value.ToString()
            txtFloorCapacity.Text = row.Cells(2).Value.ToString()
        End If
    End Sub

    Private Sub _8_floormanage_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        LoadFloors()
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
    Private Sub ClearFields()
        txtFloorNumber.Clear()
        txtFloorName.Clear()
        txtFloorCapacity.Clear()
    End Sub
    Private Sub Panel1_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Panel1.Paint

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        ClearFields()
    End Sub
End Class