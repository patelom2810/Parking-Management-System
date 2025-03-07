Imports System.Drawing
Imports System.Drawing.Printing

Public Class _5_Receip
    Private panelImage As Bitmap
    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub _5_Receip_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub
 Public Sub SetPayslipData(ByVal floorID As Integer, ByVal comboBoxValue As String, ByVal vehicleNumber As String, ByVal dateField As Date, ByVal timeField As String, ByVal exitDate As Date, ByVal exitTime As String, ByVal payment As String)
        lblFloorID.Text = floorID.ToString()
        lblComboBoxValue.Text = comboBoxValue
        lblVehicleNumber.Text = vehicleNumber
        lblDateField.Text = dateField.ToString("d") ' Display date only
        lblTimeField.Text = timeField
        lblExitTime.Text = exitTime
        lblPayment.Text = payment
        lblExitDate.Text = If(exitDate = Date.MinValue, "N/A", exitDate.ToString("d")) ' Display exit date with check for null
    End Sub

    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        ' Print process
        Try
            ' Ensure Panel3 is not Nothing before drawing
            If Panel3 IsNot Nothing Then
                ' Create a bitmap with the size of the panel
                panelImage = New Bitmap(Panel3.Width, Panel3.Height)

                ' Draw the panel's content to the bitmap
                Panel3.DrawToBitmap(panelImage, New Rectangle(0, 0, Panel3.Width, Panel3.Height))

                ' Initialize PrintDocument
                Dim printDoc As New PrintDocument()
                AddHandler printDoc.PrintPage, AddressOf Me.PrintDocument_PrintPage

                ' Trigger Print Dialog
                Dim printDialog As New PrintDialog()
                printDialog.Document = printDoc

                If printDialog.ShowDialog() = DialogResult.OK Then
                    printDoc.Print()
                End If
            Else
                MessageBox.Show("Panel3 is not initialized.")
            End If
        Catch ex As Exception
            MessageBox.Show("Error during print operation: " & ex.Message)
        End Try
    End Sub

    ' PrintDocument PrintPage Event
    Private Sub PrintDocument_PrintPage(ByVal sender As Object, ByVal e As PrintPageEventArgs)
        Try
            ' Draw the captured panel image on the print page
            If panelImage IsNot Nothing Then
                e.Graphics.DrawImage(panelImage, 0, 0)
            Else
                MessageBox.Show("Panel image is not available.")
            End If
        Catch ex As Exception
            MessageBox.Show("Error while rendering the page: " & ex.Message)
        End Try
    End Sub
End Class