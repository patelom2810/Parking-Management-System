Imports System.Data.SqlClient
Public Class _2_Dashboard
    Public LoggedInUsername As String
    ' Constructor that sets the logged-in username
    Public Sub New(ByVal username As String)
        InitializeComponent()
        LoggedInUsername = username ' Set the username of the logged-in user
    End Sub
    ' Connection string to the database
    Dim connectionString As String = "Data Source=.\SQLEXPRESS;AttachDbFilename=D:\PMS\PARKING_OM\PMSF\PMSF\PMSF.mdf;Integrated Security=True;User Instance=True"
    Private Sub btnVehicalEntry_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnVehicalEntry.Click
        ' Create an instance of VehicleEntryForm
        Dim vehicleEntry As New _3_Ventry() ' Replace VehicleEntryForm with your actual form class name

        ' Show the Vehicle Entry Form
        vehicleEntry.Show()
    End Sub

    Private Sub btnVehicalexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnVehicalexit.Click
        ' Create an instance of VehicleEntryForm
        Dim vehicleExit As New _4_vexit() ' Replace VehicleEntryForm with your actual form class name

        ' Show the Vehicle Entry Form
        vehicleExit.Show()
    End Sub

    Private Sub btnpayslip_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub btnrecord_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnrecord.Click

        ' Create an instance of VehicleEntryForm
        Dim Searchvehical As New _6_vehicalsearch() ' Replace VehicleEntryForm with your actual form class name

        ' Show the Vehicle Entry Form
        Searchvehical.Show()
    End Sub

    Private Sub btnvehicalsearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnvehicalsearch.Click
        ' Create an instance of VehicleEntryForm
        Dim recordofv As New _7_Recordofvehical() ' Replace VehicleEntryForm with your actual form class name

        ' Show the Vehicle Entry Form
        recordofv.Show()

    End Sub

    Private Sub btnfloormgt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnfloormgt.Click
        ' Create an instance of VehicleEntryForm
        Dim fm As New _8_floormanage() ' Replace VehicleEntryForm with your actual form class name

        ' Show the Vehicle Entry Form
        fm.Show()
    End Sub

    Private Sub btnchangepass_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnchangepass.Click
        Dim changePasswordForm As New _9_changepass(LoggedInUsername)
        ' Show Form11 as a dialog (modal form)
        changePasswordForm.ShowDialog()
    End Sub

    Private Sub btnlogout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnlogout.Click
        ' Ask for confirmation before logging out
        Dim result As DialogResult = MessageBox.Show("Are you sure you want to log out?", "Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        If result = DialogResult.Yes Then
            ' Close the current form (Dashboard)
            Me.Close()

            ' Optionally, if you want to clear data before logging out, you can do it here:
            ' For example: Clear user session data, username, etc.

            ' Show the login form again
            Dim loginForm As New Form1()
            loginForm.Show()

            ' Optionally, if you want to close the main form after logout, use Application.Exit() to stop the application:
            ' Application.Exit()
        End If
    End Sub
    Private Sub PopulateFloorDropdown()
        Try
            Using conn As New SqlConnection(connectionString)
                Dim query As String = "SELECT FloorNo FROM Floors ORDER BY FloorNo"
                Dim adapter As New SqlDataAdapter(query, conn)
                Dim table As New DataTable()
                adapter.Fill(table)

                ' Temporarily disable the event handler
                RemoveHandler ComboBoxFloor.SelectedIndexChanged, AddressOf ComboBoxFloor_SelectedIndexChanged

                ' Bind Floor IDs to ComboBox
                ComboBoxFloor.DataSource = table
                ComboBoxFloor.DisplayMember = "FloorNo"
                ComboBoxFloor.ValueMember = "FloorNo"
                ComboBoxFloor.SelectedIndex = -1 ' Ensure no default selection

                ' Re-enable the event handler
                AddHandler ComboBoxFloor.SelectedIndexChanged, AddressOf ComboBoxFloor_SelectedIndexChanged
            End Using
        Catch ex As Exception
            MessageBox.Show("Error populating dropdown: " & ex.Message)
        End Try
    End Sub

    Private Sub _2_Dashboard_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ShowAllFloorDetails()
        PopulateFloorDropdown()
        ' Start the timer for auto-refresh
        Timer1.Interval = 1000 ' 2 seconds
        Timer1.Enabled = True

        ' Start the timer on form load
        Timer1.Interval = 1000 ' Set interval to 1 second
        Timer1.Start()
    End Sub
    Private Sub ShowAllFloorDetails()
        Try
            Using conn As New SqlConnection(connectionString)
                Dim query As String = "SELECT FloorNo, TotalCapacity, AvailableCapacity FROM Floors ORDER BY FloorNo"
                Dim command As New SqlCommand(query, conn)
                conn.Open()

                ' Execute the query
                Dim reader As SqlDataReader = command.ExecuteReader()
                Dim floorIndex As Integer = 1 ' To track label sets for each floor

                While reader.Read()
                    ' Dynamically update labels for each floor
                    Select Case reader("FloorNo")
                        Case 1
                            LabelFloorID1.Text = reader("FloorNo").ToString()
                            LabelCapacity1.Text = reader("TotalCapacity").ToString()
                            LabelAvailableCapacity1.Text = reader("AvailableCapacity").ToString()
                        Case 2
                            LabelFloorID2.Text = reader("FloorNo").ToString()
                            LabelCapacity2.Text = reader("TotalCapacity").ToString()
                            LabelAvailableCapacity2.Text = reader("AvailableCapacity").ToString()
                        Case 3
                            LabelFloorID3.Text = reader("FloorNo").ToString()
                            LabelCapacity3.Text = reader("TotalCapacity").ToString()
                            LabelAvailableCapacity3.Text = reader("AvailableCapacity").ToString()
                        Case 4
                            LabelFloorID4.Text = reader("FloorNo").ToString()
                            LabelCapacity4.Text = reader("TotalCapacity").ToString()
                            LabelAvailableCapacity4.Text = reader("AvailableCapacity").ToString()
                        Case 5
                            LabelFloorID5.Text = reader("FloorNo").ToString()
                            LabelCapacity5.Text = reader("TotalCapacity").ToString()
                            LabelAvailableCapacity5.Text = reader("AvailableCapacity").ToString()
                    End Select

                    floorIndex += 1
                End While

                reader.Close()
            End Using
        Catch ex As Exception
            MessageBox.Show("Error retrieving data: " & ex.Message)
        End Try
    End Sub

    Private Sub ComboBoxFloor_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBoxFloor.SelectedIndexChanged
        If ComboBoxFloor.SelectedIndex <> -1 Then
            Dim selectedFloorID As Integer
            If Integer.TryParse(ComboBoxFloor.SelectedValue.ToString(), selectedFloorID) Then
                DisplayFloorDetails(selectedFloorID)
            Else
                ' Clear the details if the value is invalid
                LabelSelectedCapacity.Text = "Total Capacity: N/A"
                LabelSelectedAvailableCapacity.Text = "Available Capacity: N/A"
            End If
        Else
            ' Clear the details if no item is selected
            LabelSelectedCapacity.Text = "Total Capacity: N/A"
            LabelSelectedAvailableCapacity.Text = "Available Capacity: N/A"
        End If
    End Sub
    Private Sub DisplayFloorDetails(ByVal floorID As Integer)
        Try
            Using conn As New SqlConnection(connectionString)
                Dim query As String = "SELECT TotalCapacity, AvailableCapacity FROM Floors WHERE FloorNo = @FloorNo"
                Dim command As New SqlCommand(query, conn)
                command.Parameters.AddWithValue("@FloorNo", floorID)
                conn.Open()

                Dim reader As SqlDataReader = command.ExecuteReader()
                If reader.Read() Then
                    LabelSelectedCapacity.Text = reader("TotalCapacity").ToString()
                    LabelSelectedAvailableCapacity.Text = reader("AvailableCapacity").ToString()
                End If
                reader.Close()
            End Using
        Catch ex As Exception
            MessageBox.Show("Error displaying floor details: {ex.Message}")
        End Try
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        ShowAllFloorDetails()
        ' Update the label with the current date and time every second
        lblDateTime.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
    End Sub
    ' Method to enable or disable admin features
    Public Sub ShowAdminFeatures(ByVal isAdmin As Boolean)
        Button2.Visible = isAdmin
        PictureBox1.Visible = isAdmin
    End Sub

    ' Method to set the logged-in user
    Public Sub SetLoggedInUser(ByVal username As String)
        lblwel.Text = "Welcom " & username
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click

        ' Get username and password from InputBox
        Dim username As String = InputBox("Enter username:", "Login")
        Dim password As String = InputBox("Enter password:", "Login")

        ' Check if username and password are empty
        If username = "" Or password = "" Then
            MessageBox.Show("Please enter both username and password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        ' Check if the username is 'admin'
        If username.ToLower() = "admin" Then
            ' Proceed with admin login
            Dim connString As String = "Data Source=.\SQLEXPRESS;AttachDbFilename=D:\PMS\PARKING_OM\PMSF\PMSF\PMSF.mdf;Integrated Security=True;User Instance=True"

            Dim query As String = "SELECT COUNT(*) FROM Users WHERE username = @username AND password = @password"

            ' Create connection and command
            Using conn As New SqlConnection(connString)
                Using cmd As New SqlCommand(query, conn)
                    ' Add parameters to avoid SQL injection
                    cmd.Parameters.AddWithValue("@username", username)
                    cmd.Parameters.AddWithValue("@password", password)

                    Try
                        ' Open connection
                        conn.Open()

                        ' Execute query to check if credentials are correct
                        Dim result As Integer = Convert.ToInt32(cmd.ExecuteScalar())

                        ' If result is 1, credentials are correct
                        If result = 1 Then
                            ' Open Form2 for admin
                            _11_adduser.Show()
                            'Me.Hide() ' Hide current form (optional)
                        Else
                            MessageBox.Show("Invalid username or password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        End If
                    Catch ex As Exception
                        MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End Try
                End Using
            End Using
        Else
            ' If username is not 'admin', deny access
            MessageBox.Show("Access Denied. Only 'admin' can log in.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Application.Exit()
    End Sub
End Class