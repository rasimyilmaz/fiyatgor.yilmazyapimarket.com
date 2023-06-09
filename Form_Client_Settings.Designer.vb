<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form_Client_Settings
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
        Me.TextBox_url = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Button_Save = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TextBox_apikey = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.TextBox_username = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.TextBox_password = New System.Windows.Forms.TextBox()
        Me.Label_IdAndName = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'TextBox_url
        '
        Me.TextBox_url.Location = New System.Drawing.Point(12, 86)
        Me.TextBox_url.Multiline = True
        Me.TextBox_url.Name = "TextBox_url"
        Me.TextBox_url.Size = New System.Drawing.Size(262, 23)
        Me.TextBox_url.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(162, Byte))
        Me.Label1.Location = New System.Drawing.Point(12, 60)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(23, 15)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Url"
        '
        'Button_Save
        '
        Me.Button_Save.Location = New System.Drawing.Point(96, 411)
        Me.Button_Save.Name = "Button_Save"
        Me.Button_Save.Size = New System.Drawing.Size(75, 23)
        Me.Button_Save.TabIndex = 2
        Me.Button_Save.Text = "Kaydet"
        Me.Button_Save.UseVisualStyleBackColor = True
        Me.Button_Save.Visible = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(162, Byte))
        Me.Label2.Location = New System.Drawing.Point(12, 126)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(47, 15)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "Api Key"
        '
        'TextBox_apikey
        '
        Me.TextBox_apikey.Location = New System.Drawing.Point(12, 157)
        Me.TextBox_apikey.Multiline = True
        Me.TextBox_apikey.Name = "TextBox_apikey"
        Me.TextBox_apikey.Size = New System.Drawing.Size(262, 39)
        Me.TextBox_apikey.TabIndex = 3
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(162, Byte))
        Me.Label3.Location = New System.Drawing.Point(12, 215)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(65, 15)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "Username"
        '
        'TextBox_username
        '
        Me.TextBox_username.Location = New System.Drawing.Point(15, 246)
        Me.TextBox_username.Multiline = True
        Me.TextBox_username.Name = "TextBox_username"
        Me.TextBox_username.Size = New System.Drawing.Size(262, 31)
        Me.TextBox_username.TabIndex = 5
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(162, Byte))
        Me.Label4.Location = New System.Drawing.Point(12, 297)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(61, 15)
        Me.Label4.TabIndex = 8
        Me.Label4.Text = "Password"
        '
        'TextBox_password
        '
        Me.TextBox_password.Location = New System.Drawing.Point(15, 328)
        Me.TextBox_password.Multiline = True
        Me.TextBox_password.Name = "TextBox_password"
        Me.TextBox_password.Size = New System.Drawing.Size(262, 31)
        Me.TextBox_password.TabIndex = 7
        '
        'Label_IdAndName
        '
        Me.Label_IdAndName.AutoSize = True
        Me.Label_IdAndName.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(162, Byte))
        Me.Label_IdAndName.Location = New System.Drawing.Point(93, 22)
        Me.Label_IdAndName.Name = "Label_IdAndName"
        Me.Label_IdAndName.Size = New System.Drawing.Size(70, 15)
        Me.Label_IdAndName.TabIndex = 9
        Me.Label_IdAndName.Text = "Id - Name"
        '
        'Form_Client_Settings
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(286, 446)
        Me.Controls.Add(Me.Label_IdAndName)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.TextBox_password)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.TextBox_username)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.TextBox_apikey)
        Me.Controls.Add(Me.Button_Save)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TextBox_url)
        Me.Location = New System.Drawing.Point(200, 200)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Form_Client_Settings"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Bağlantı Ayarı"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TextBox_url As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Button_Save As System.Windows.Forms.Button
    Friend WithEvents Label2 As Label
    Friend WithEvents TextBox_apikey As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents TextBox_username As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents TextBox_password As TextBox
    Friend WithEvents Label_IdAndName As Label
End Class
