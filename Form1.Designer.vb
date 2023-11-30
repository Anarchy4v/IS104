<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
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
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtUser = New System.Windows.Forms.TextBox()
        Me.txtPass = New System.Windows.Forms.TextBox()
        Me.btnLogin = New System.Windows.Forms.Button()
        Me.btnAccount = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.Firebrick
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(338, 450)
        Me.Panel1.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(478, 91)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(57, 24)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Login"
        '
        'txtUser
        '
        Me.txtUser.ForeColor = System.Drawing.SystemColors.ScrollBar
        Me.txtUser.Location = New System.Drawing.Point(417, 150)
        Me.txtUser.Name = "txtUser"
        Me.txtUser.Size = New System.Drawing.Size(183, 20)
        Me.txtUser.TabIndex = 2
        Me.txtUser.Text = "Enter your email"
        '
        'txtPass
        '
        Me.txtPass.ForeColor = System.Drawing.SystemColors.ScrollBar
        Me.txtPass.Location = New System.Drawing.Point(417, 226)
        Me.txtPass.Name = "txtPass"
        Me.txtPass.Size = New System.Drawing.Size(183, 20)
        Me.txtPass.TabIndex = 3
        Me.txtPass.Text = "Enter your password"
        '
        'btnLogin
        '
        Me.btnLogin.Location = New System.Drawing.Point(482, 303)
        Me.btnLogin.Name = "btnLogin"
        Me.btnLogin.Size = New System.Drawing.Size(53, 23)
        Me.btnLogin.TabIndex = 4
        Me.btnLogin.Text = "LOGIN"
        Me.btnLogin.UseVisualStyleBackColor = True
        '
        'btnAccount
        '
        Me.btnAccount.Location = New System.Drawing.Point(440, 265)
        Me.btnAccount.Name = "btnAccount"
        Me.btnAccount.Size = New System.Drawing.Size(135, 23)
        Me.btnAccount.TabIndex = 5
        Me.btnAccount.Text = "Don't have an account?"
        Me.btnAccount.UseVisualStyleBackColor = True
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(674, 450)
        Me.Controls.Add(Me.btnAccount)
        Me.Controls.Add(Me.btnLogin)
        Me.Controls.Add(Me.txtPass)
        Me.Controls.Add(Me.txtUser)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Panel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "Form1"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents Label1 As Label
    Friend WithEvents txtUser As TextBox
    Friend WithEvents txtPass As TextBox
    Friend WithEvents btnLogin As Button
    Friend WithEvents btnAccount As Button
End Class
