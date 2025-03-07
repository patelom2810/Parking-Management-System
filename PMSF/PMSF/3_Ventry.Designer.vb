<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class _3_Ventry
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(_3_Ventry))
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.panel2 = New System.Windows.Forms.Panel()
        Me.Button9 = New System.Windows.Forms.Button()
        Me.Button6 = New System.Windows.Forms.Button()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.pfloor = New System.Windows.Forms.ComboBox()
        Me.vtype = New System.Windows.Forms.ComboBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.dtpDate = New System.Windows.Forms.DateTimePicker()
        Me.button1 = New System.Windows.Forms.Button()
        Me.txtVEntryTime = New System.Windows.Forms.TextBox()
        Me.label6 = New System.Windows.Forms.Label()
        Me.label5 = New System.Windows.Forms.Label()
        Me.txtVNumber = New System.Windows.Forms.TextBox()
        Me.label4 = New System.Windows.Forms.Label()
        Me.label3 = New System.Windows.Forms.Label()
        Me.txtCoNumber = New System.Windows.Forms.TextBox()
        Me.txtName = New System.Windows.Forms.TextBox()
        Me.label2 = New System.Windows.Forms.Label()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Button7 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.label1 = New System.Windows.Forms.Label()
        Me.Panel1.SuspendLayout()
        Me.panel2.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.panel2)
        Me.Panel1.Controls.Add(Me.Panel3)
        Me.Panel1.Location = New System.Drawing.Point(1, 3)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(881, 528)
        Me.Panel1.TabIndex = 0
        '
        'panel2
        '
        Me.panel2.BackColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(227, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.panel2.Controls.Add(Me.Button9)
        Me.panel2.Controls.Add(Me.Button6)
        Me.panel2.Controls.Add(Me.Button4)
        Me.panel2.Controls.Add(Me.pfloor)
        Me.panel2.Controls.Add(Me.vtype)
        Me.panel2.Controls.Add(Me.Label8)
        Me.panel2.Controls.Add(Me.Label7)
        Me.panel2.Controls.Add(Me.dtpDate)
        Me.panel2.Controls.Add(Me.button1)
        Me.panel2.Controls.Add(Me.txtVEntryTime)
        Me.panel2.Controls.Add(Me.label6)
        Me.panel2.Controls.Add(Me.label5)
        Me.panel2.Controls.Add(Me.txtVNumber)
        Me.panel2.Controls.Add(Me.label4)
        Me.panel2.Controls.Add(Me.label3)
        Me.panel2.Controls.Add(Me.txtCoNumber)
        Me.panel2.Controls.Add(Me.txtName)
        Me.panel2.Controls.Add(Me.label2)
        Me.panel2.Location = New System.Drawing.Point(3, 67)
        Me.panel2.Name = "panel2"
        Me.panel2.Size = New System.Drawing.Size(874, 459)
        Me.panel2.TabIndex = 10
        '
        'Button9
        '
        Me.Button9.BackColor = System.Drawing.Color.MidnightBlue
        Me.Button9.Font = New System.Drawing.Font("Century Gothic", 12.0!, System.Drawing.FontStyle.Bold)
        Me.Button9.ForeColor = System.Drawing.Color.White
        Me.Button9.Location = New System.Drawing.Point(518, 395)
        Me.Button9.Name = "Button9"
        Me.Button9.Size = New System.Drawing.Size(163, 42)
        Me.Button9.TabIndex = 44
        Me.Button9.Text = "PRINT TOKEN"
        Me.Button9.UseVisualStyleBackColor = False
        '
        'Button6
        '
        Me.Button6.BackColor = System.Drawing.Color.Orange
        Me.Button6.Font = New System.Drawing.Font("Century Gothic", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button6.ForeColor = System.Drawing.Color.White
        Me.Button6.Location = New System.Drawing.Point(426, 301)
        Me.Button6.Name = "Button6"
        Me.Button6.Size = New System.Drawing.Size(201, 31)
        Me.Button6.TabIndex = 22
        Me.Button6.Text = "GET CURRENT TIME"
        Me.Button6.UseVisualStyleBackColor = False
        '
        'Button4
        '
        Me.Button4.BackColor = System.Drawing.Color.MidnightBlue
        Me.Button4.Font = New System.Drawing.Font("Century Gothic", 12.0!, System.Drawing.FontStyle.Bold)
        Me.Button4.ForeColor = System.Drawing.Color.White
        Me.Button4.Location = New System.Drawing.Point(351, 395)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(132, 42)
        Me.Button4.TabIndex = 20
        Me.Button4.Text = "CLEAR"
        Me.Button4.UseVisualStyleBackColor = False
        '
        'pfloor
        '
        Me.pfloor.Font = New System.Drawing.Font("Century Gothic", 10.8!, System.Drawing.FontStyle.Bold)
        Me.pfloor.FormattingEnabled = True
        Me.pfloor.Location = New System.Drawing.Point(198, 80)
        Me.pfloor.Name = "pfloor"
        Me.pfloor.Size = New System.Drawing.Size(115, 30)
        Me.pfloor.TabIndex = 18
        '
        'vtype
        '
        Me.vtype.Font = New System.Drawing.Font("Century Gothic", 10.8!, System.Drawing.FontStyle.Bold)
        Me.vtype.FormattingEnabled = True
        Me.vtype.Items.AddRange(New Object() {"TWO-WHEELER", "THREE-WHEELER", "FOUR-WHEELER", "HEAVY VEHICLE"})
        Me.vtype.Location = New System.Drawing.Point(601, 229)
        Me.vtype.Name = "vtype"
        Me.vtype.Size = New System.Drawing.Size(248, 30)
        Me.vtype.TabIndex = 17
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Century Gothic", 12.0!, System.Drawing.FontStyle.Bold)
        Me.Label8.Location = New System.Drawing.Point(436, 230)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(154, 23)
        Me.Label8.TabIndex = 16
        Me.Label8.Text = "VEHICLE TYPE : "
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Century Gothic", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(5, 83)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(187, 23)
        Me.Label7.TabIndex = 14
        Me.Label7.Text = "PARKING FLOOR : "
        '
        'dtpDate
        '
        Me.dtpDate.Font = New System.Drawing.Font("Century Gothic", 12.0!, System.Drawing.FontStyle.Bold)
        Me.dtpDate.Location = New System.Drawing.Point(606, 82)
        Me.dtpDate.Name = "dtpDate"
        Me.dtpDate.Size = New System.Drawing.Size(251, 32)
        Me.dtpDate.TabIndex = 13
        '
        'button1
        '
        Me.button1.BackColor = System.Drawing.Color.MidnightBlue
        Me.button1.Font = New System.Drawing.Font("Century Gothic", 12.0!, System.Drawing.FontStyle.Bold)
        Me.button1.ForeColor = System.Drawing.Color.White
        Me.button1.Location = New System.Drawing.Point(181, 395)
        Me.button1.Name = "button1"
        Me.button1.Size = New System.Drawing.Size(132, 42)
        Me.button1.TabIndex = 12
        Me.button1.Text = "SUBMIT"
        Me.button1.UseVisualStyleBackColor = False
        '
        'txtVEntryTime
        '
        Me.txtVEntryTime.Font = New System.Drawing.Font("Century Gothic", 10.8!, System.Drawing.FontStyle.Bold)
        Me.txtVEntryTime.Location = New System.Drawing.Point(228, 298)
        Me.txtVEntryTime.Name = "txtVEntryTime"
        Me.txtVEntryTime.Size = New System.Drawing.Size(192, 30)
        Me.txtVEntryTime.TabIndex = 11
        '
        'label6
        '
        Me.label6.AutoSize = True
        Me.label6.Font = New System.Drawing.Font("Century Gothic", 12.0!, System.Drawing.FontStyle.Bold)
        Me.label6.Location = New System.Drawing.Point(5, 301)
        Me.label6.Name = "label6"
        Me.label6.Size = New System.Drawing.Size(218, 23)
        Me.label6.TabIndex = 10
        Me.label6.Text = "VEHICLE ENTRY TIME  :"
        '
        'label5
        '
        Me.label5.AutoSize = True
        Me.label5.Font = New System.Drawing.Font("Century Gothic", 12.0!, System.Drawing.FontStyle.Bold)
        Me.label5.Location = New System.Drawing.Point(526, 89)
        Me.label5.Name = "label5"
        Me.label5.Size = New System.Drawing.Size(69, 23)
        Me.label5.TabIndex = 8
        Me.label5.Text = "DATE :"
        '
        'txtVNumber
        '
        Me.txtVNumber.Font = New System.Drawing.Font("Century Gothic", 10.8!, System.Drawing.FontStyle.Bold)
        Me.txtVNumber.Location = New System.Drawing.Point(199, 224)
        Me.txtVNumber.Name = "txtVNumber"
        Me.txtVNumber.Size = New System.Drawing.Size(221, 30)
        Me.txtVNumber.TabIndex = 7
        '
        'label4
        '
        Me.label4.AutoSize = True
        Me.label4.Font = New System.Drawing.Font("Century Gothic", 12.0!, System.Drawing.FontStyle.Bold)
        Me.label4.Location = New System.Drawing.Point(5, 229)
        Me.label4.Name = "label4"
        Me.label4.Size = New System.Drawing.Size(187, 23)
        Me.label4.TabIndex = 6
        Me.label4.Text = "VEHICLE NUMBER :"
        '
        'label3
        '
        Me.label3.AutoSize = True
        Me.label3.Font = New System.Drawing.Font("Century Gothic", 12.0!, System.Drawing.FontStyle.Bold)
        Me.label3.Location = New System.Drawing.Point(434, 153)
        Me.label3.Name = "label3"
        Me.label3.Size = New System.Drawing.Size(161, 23)
        Me.label3.TabIndex = 4
        Me.label3.Text = "CONTACT NO. :"
        '
        'txtCoNumber
        '
        Me.txtCoNumber.Font = New System.Drawing.Font("Century Gothic", 10.8!, System.Drawing.FontStyle.Bold)
        Me.txtCoNumber.Location = New System.Drawing.Point(601, 146)
        Me.txtCoNumber.Name = "txtCoNumber"
        Me.txtCoNumber.Size = New System.Drawing.Size(256, 30)
        Me.txtCoNumber.TabIndex = 5
        '
        'txtName
        '
        Me.txtName.Font = New System.Drawing.Font("Century Gothic", 10.8!, System.Drawing.FontStyle.Bold)
        Me.txtName.Location = New System.Drawing.Point(97, 153)
        Me.txtName.Name = "txtName"
        Me.txtName.Size = New System.Drawing.Size(323, 30)
        Me.txtName.TabIndex = 3
        '
        'label2
        '
        Me.label2.AutoSize = True
        Me.label2.Font = New System.Drawing.Font("Century Gothic", 12.0!, System.Drawing.FontStyle.Bold)
        Me.label2.Location = New System.Drawing.Point(5, 153)
        Me.label2.Name = "label2"
        Me.label2.Size = New System.Drawing.Size(86, 23)
        Me.label2.TabIndex = 2
        Me.label2.Text = "NAME : "
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.MidnightBlue
        Me.Panel3.Controls.Add(Me.Button7)
        Me.Panel3.Controls.Add(Me.Button2)
        Me.Panel3.Controls.Add(Me.label1)
        Me.Panel3.Location = New System.Drawing.Point(3, 2)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(874, 63)
        Me.Panel3.TabIndex = 9
        '
        'Button7
        '
        Me.Button7.BackgroundImage = CType(resources.GetObject("Button7.BackgroundImage"), System.Drawing.Image)
        Me.Button7.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Button7.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button7.Location = New System.Drawing.Point(5, 20)
        Me.Button7.Name = "Button7"
        Me.Button7.Size = New System.Drawing.Size(37, 28)
        Me.Button7.TabIndex = 41
        Me.Button7.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.FlatAppearance.BorderSize = 0
        Me.Button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button2.Font = New System.Drawing.Font("Verdana", 13.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button2.ForeColor = System.Drawing.Color.SeaShell
        Me.Button2.Location = New System.Drawing.Point(822, 10)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(40, 40)
        Me.Button2.TabIndex = 2
        Me.Button2.Text = "X"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'label1
        '
        Me.label1.AutoSize = True
        Me.label1.Font = New System.Drawing.Font("Century Gothic", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label1.ForeColor = System.Drawing.Color.White
        Me.label1.Location = New System.Drawing.Point(48, 13)
        Me.label1.Name = "label1"
        Me.label1.Size = New System.Drawing.Size(213, 37)
        Me.label1.TabIndex = 0
        Me.label1.Text = "Vehicle Entry "
        '
        '_3_Ventry
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(884, 531)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "_3_Ventry"
        Me.Text = "3_Ventry"
        Me.Panel1.ResumeLayout(False)
        Me.panel2.ResumeLayout(False)
        Me.panel2.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Private WithEvents panel2 As System.Windows.Forms.Panel
    Private WithEvents Button6 As System.Windows.Forms.Button
    Private WithEvents Button4 As System.Windows.Forms.Button
    Friend WithEvents pfloor As System.Windows.Forms.ComboBox
    Friend WithEvents vtype As System.Windows.Forms.ComboBox
    Private WithEvents Label8 As System.Windows.Forms.Label
    Private WithEvents Label7 As System.Windows.Forms.Label
    Private WithEvents dtpDate As System.Windows.Forms.DateTimePicker
    Private WithEvents button1 As System.Windows.Forms.Button
    Private WithEvents txtVEntryTime As System.Windows.Forms.TextBox
    Private WithEvents label6 As System.Windows.Forms.Label
    Private WithEvents label5 As System.Windows.Forms.Label
    Private WithEvents txtVNumber As System.Windows.Forms.TextBox
    Private WithEvents label4 As System.Windows.Forms.Label
    Private WithEvents label3 As System.Windows.Forms.Label
    Private WithEvents txtCoNumber As System.Windows.Forms.TextBox
    Private WithEvents txtName As System.Windows.Forms.TextBox
    Private WithEvents label2 As System.Windows.Forms.Label
    Private WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents Button7 As System.Windows.Forms.Button
    Private WithEvents Button2 As System.Windows.Forms.Button
    Private WithEvents label1 As System.Windows.Forms.Label
    Private WithEvents Button9 As System.Windows.Forms.Button
End Class
