<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Dash
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.Button5 = New System.Windows.Forms.Button()
        Me.Button6 = New System.Windows.Forms.Button()
        Me.DirectorySearcher1 = New System.DirectoryServices.DirectorySearcher()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.Panel7 = New System.Windows.Forms.Panel()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.BackgroundWorker1 = New System.ComponentModel.BackgroundWorker()
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.Panel3.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel4.SuspendLayout()
        Me.Panel5.SuspendLayout()
        Me.Panel6.SuspendLayout()
        Me.Panel7.SuspendLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.Panel2)
        Me.Panel1.Location = New System.Drawing.Point(12, 12)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(200, 426)
        Me.Panel1.TabIndex = 0
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.Button6)
        Me.Panel2.Controls.Add(Me.Button5)
        Me.Panel2.Controls.Add(Me.Button4)
        Me.Panel2.Controls.Add(Me.Button3)
        Me.Panel2.Controls.Add(Me.Button2)
        Me.Panel2.Controls.Add(Me.Button1)
        Me.Panel2.Location = New System.Drawing.Point(0, 96)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(200, 226)
        Me.Panel2.TabIndex = 1
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(0, 33)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(200, 23)
        Me.Button1.TabIndex = 0
        Me.Button1.Text = "Dashboard"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(0, 62)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(200, 23)
        Me.Button2.TabIndex = 1
        Me.Button2.Text = "Point of Sales"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Button3
        '
        Me.Button3.Location = New System.Drawing.Point(0, 91)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(200, 23)
        Me.Button3.TabIndex = 2
        Me.Button3.Text = "Inventory"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'Button4
        '
        Me.Button4.Location = New System.Drawing.Point(0, 120)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(200, 23)
        Me.Button4.TabIndex = 3
        Me.Button4.Text = "Sales Report"
        Me.Button4.UseVisualStyleBackColor = True
        '
        'Button5
        '
        Me.Button5.Location = New System.Drawing.Point(0, 149)
        Me.Button5.Name = "Button5"
        Me.Button5.Size = New System.Drawing.Size(200, 23)
        Me.Button5.TabIndex = 4
        Me.Button5.Text = "Settings"
        Me.Button5.UseVisualStyleBackColor = True
        '
        'Button6
        '
        Me.Button6.Location = New System.Drawing.Point(0, 178)
        Me.Button6.Name = "Button6"
        Me.Button6.Size = New System.Drawing.Size(200, 23)
        Me.Button6.TabIndex = 5
        Me.Button6.Text = "Log Out"
        Me.Button6.UseVisualStyleBackColor = True
        '
        'DirectorySearcher1
        '
        Me.DirectorySearcher1.ClientTimeout = System.TimeSpan.Parse("-00:00:01")
        Me.DirectorySearcher1.ServerPageTimeLimit = System.TimeSpan.Parse("-00:00:01")
        Me.DirectorySearcher1.ServerTimeLimit = System.TimeSpan.Parse("-00:00:01")
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.White
        Me.Panel3.Controls.Add(Me.DataGridView1)
        Me.Panel3.Controls.Add(Me.Panel7)
        Me.Panel3.Controls.Add(Me.Panel6)
        Me.Panel3.Controls.Add(Me.Panel5)
        Me.Panel3.Controls.Add(Me.Panel4)
        Me.Panel3.Controls.Add(Me.Label2)
        Me.Panel3.Controls.Add(Me.PictureBox2)
        Me.Panel3.Controls.Add(Me.TextBox1)
        Me.Panel3.Controls.Add(Me.Label1)
        Me.Panel3.Controls.Add(Me.PictureBox1)
        Me.Panel3.Location = New System.Drawing.Point(218, 12)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(570, 426)
        Me.Panel3.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(3, 3)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(131, 29)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "Dashboard"
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(139, 9)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(150, 20)
        Me.TextBox1.TabIndex = 4
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = Global.PharmacyandMedicine.My.Resources.Resources.profile
        Me.PictureBox1.Location = New System.Drawing.Point(429, 3)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(46, 44)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox1.TabIndex = 2
        Me.PictureBox1.TabStop = False
        '
        'PictureBox2
        '
        Me.PictureBox2.Image = Global.PharmacyandMedicine.My.Resources.Resources.notification
        Me.PictureBox2.Location = New System.Drawing.Point(380, 6)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(37, 38)
        Me.PictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox2.TabIndex = 5
        Me.PictureBox2.TabStop = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(487, 9)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(60, 29)
        Me.Label2.TabIndex = 6
        Me.Label2.Text = "user"
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.DarkRed
        Me.Panel4.Controls.Add(Me.Label3)
        Me.Panel4.Location = New System.Drawing.Point(8, 52)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(134, 100)
        Me.Panel4.TabIndex = 7
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.Color.Gainsboro
        Me.Panel5.Controls.Add(Me.Label4)
        Me.Panel5.Location = New System.Drawing.Point(148, 52)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(134, 100)
        Me.Panel5.TabIndex = 8
        '
        'Panel6
        '
        Me.Panel6.BackColor = System.Drawing.Color.DarkRed
        Me.Panel6.Controls.Add(Me.Label5)
        Me.Panel6.Location = New System.Drawing.Point(288, 52)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(134, 100)
        Me.Panel6.TabIndex = 9
        '
        'Panel7
        '
        Me.Panel7.BackColor = System.Drawing.Color.Gainsboro
        Me.Panel7.Controls.Add(Me.Label6)
        Me.Panel7.Location = New System.Drawing.Point(428, 52)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(134, 100)
        Me.Panel7.TabIndex = 9
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.ForeColor = System.Drawing.Color.White
        Me.Label3.Location = New System.Drawing.Point(8, 10)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(96, 13)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "Statistics Revenue"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.ForeColor = System.Drawing.Color.Black
        Me.Label4.Location = New System.Drawing.Point(8, 9)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(78, 13)
        Me.Label4.TabIndex = 1
        Me.Label4.Text = "Statistics Sales"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.ForeColor = System.Drawing.Color.White
        Me.Label5.Location = New System.Drawing.Point(7, 9)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(102, 13)
        Me.Label5.TabIndex = 1
        Me.Label5.Text = "Statistics Purchases"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.ForeColor = System.Drawing.Color.Black
        Me.Label6.Location = New System.Drawing.Point(8, 8)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(87, 13)
        Me.Label6.TabIndex = 2
        Me.Label6.Text = "Statistics Income"
        '
        'DataGridView1
        '
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Location = New System.Drawing.Point(8, 158)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.Size = New System.Drawing.Size(554, 164)
        Me.DataGridView1.TabIndex = 10
        '
        'Dash
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.DarkRed
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "Dash"
        Me.Text = "Dashboard"
        Me.Panel1.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        Me.Panel5.ResumeLayout(False)
        Me.Panel5.PerformLayout()
        Me.Panel6.ResumeLayout(False)
        Me.Panel6.PerformLayout()
        Me.Panel7.ResumeLayout(False)
        Me.Panel7.PerformLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Button6 As Button
    Friend WithEvents Button5 As Button
    Friend WithEvents Button4 As Button
    Friend WithEvents Button3 As Button
    Friend WithEvents Button2 As Button
    Friend WithEvents Button1 As Button
    Friend WithEvents DirectorySearcher1 As DirectoryServices.DirectorySearcher
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents Panel3 As Panel
    Friend WithEvents Label1 As Label
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents PictureBox2 As PictureBox
    Friend WithEvents Panel7 As Panel
    Friend WithEvents Panel6 As Panel
    Friend WithEvents Panel5 As Panel
    Friend WithEvents Panel4 As Panel
    Friend WithEvents Label4 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents DataGridView1 As DataGridView
    Friend WithEvents Label6 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents BackgroundWorker1 As System.ComponentModel.BackgroundWorker
End Class
