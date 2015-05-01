<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Run
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
        Me.components = New System.ComponentModel.Container()
        Me.cmdRun = New System.Windows.Forms.Button()
        Me.cmdCancel = New System.Windows.Forms.Button()
        Me.cmdBrowse = New System.Windows.Forms.Button()
        Me.CBAdmin = New System.Windows.Forms.CheckBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.OFDRun = New System.Windows.Forms.OpenFileDialog()
        Me.CBRun = New System.Windows.Forms.ComboBox()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'cmdRun
        '
        Me.cmdRun.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdRun.Location = New System.Drawing.Point(17, 6)
        Me.cmdRun.Name = "cmdRun"
        Me.cmdRun.Size = New System.Drawing.Size(88, 34)
        Me.cmdRun.TabIndex = 1
        Me.cmdRun.Text = "Run"
        Me.cmdRun.UseVisualStyleBackColor = True
        '
        'cmdCancel
        '
        Me.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdCancel.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdCancel.Location = New System.Drawing.Point(219, 6)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.Size = New System.Drawing.Size(88, 34)
        Me.cmdCancel.TabIndex = 2
        Me.cmdCancel.Text = "Cancel"
        Me.cmdCancel.UseVisualStyleBackColor = True
        '
        'cmdBrowse
        '
        Me.cmdBrowse.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdBrowse.Location = New System.Drawing.Point(115, 6)
        Me.cmdBrowse.Name = "cmdBrowse"
        Me.cmdBrowse.Size = New System.Drawing.Size(88, 34)
        Me.cmdBrowse.TabIndex = 3
        Me.cmdBrowse.Text = "Browse .."
        Me.cmdBrowse.UseVisualStyleBackColor = True
        '
        'CBAdmin
        '
        Me.CBAdmin.AutoSize = True
        Me.CBAdmin.Checked = True
        Me.CBAdmin.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CBAdmin.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.CBAdmin.ForeColor = System.Drawing.Color.White
        Me.CBAdmin.Location = New System.Drawing.Point(70, 46)
        Me.CBAdmin.Name = "CBAdmin"
        Me.CBAdmin.Size = New System.Drawing.Size(205, 19)
        Me.CBAdmin.TabIndex = 4
        Me.CBAdmin.Text = "Run with Administrator Privilages"
        Me.CBAdmin.UseVisualStyleBackColor = True
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.DimGray
        Me.Panel1.Controls.Add(Me.cmdRun)
        Me.Panel1.Controls.Add(Me.cmdCancel)
        Me.Panel1.Controls.Add(Me.cmdBrowse)
        Me.Panel1.Location = New System.Drawing.Point(-8, 73)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(328, 56)
        Me.Panel1.TabIndex = 5
        '
        'OFDRun
        '
        Me.OFDRun.Filter = "Executables|*.exe"
        Me.OFDRun.Title = "Open Exe"
        '
        'CBRun
        '
        Me.CBRun.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append
        Me.CBRun.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CBRun.FormattingEnabled = True
        Me.CBRun.Items.AddRange(New Object() {"cmd", "Explorer", "Regedit", "dxdiag"})
        Me.CBRun.Location = New System.Drawing.Point(12, 12)
        Me.CBRun.Name = "CBRun"
        Me.CBRun.Size = New System.Drawing.Size(285, 21)
        Me.CBRun.TabIndex = 4
        '
        'Timer1
        '
        Me.Timer1.Interval = 50
        '
        'Run
        '
        Me.AcceptButton = Me.cmdRun
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Black
        Me.CancelButton = Me.cmdCancel
        Me.ClientSize = New System.Drawing.Size(309, 117)
        Me.Controls.Add(Me.CBRun)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.CBAdmin)
        Me.ForeColor = System.Drawing.Color.Black
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Run"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Run"
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents cmdRun As System.Windows.Forms.Button
    Friend WithEvents cmdCancel As System.Windows.Forms.Button
    Friend WithEvents cmdBrowse As System.Windows.Forms.Button
    Friend WithEvents CBAdmin As System.Windows.Forms.CheckBox
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents OFDRun As System.Windows.Forms.OpenFileDialog
    Friend WithEvents CBRun As System.Windows.Forms.ComboBox
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
End Class
