<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class main
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.addendsList = New System.Windows.Forms.TextBox
        Me.TwoDice = New System.Windows.Forms.CheckBox
        Me.SuspendLayout()
        '
        'addendsList
        '
        Me.addendsList.Dock = System.Windows.Forms.DockStyle.Top
        Me.addendsList.Enabled = False
        Me.addendsList.Location = New System.Drawing.Point(0, 0)
        Me.addendsList.Name = "addendsList"
        Me.addendsList.Size = New System.Drawing.Size(520, 20)
        Me.addendsList.TabIndex = 0
        Me.addendsList.Visible = False
        '
        'TwoDice
        '
        Me.TwoDice.AutoSize = True
        Me.TwoDice.Checked = True
        Me.TwoDice.CheckState = System.Windows.Forms.CheckState.Checked
        Me.TwoDice.Location = New System.Drawing.Point(12, 237)
        Me.TwoDice.Name = "TwoDice"
        Me.TwoDice.Size = New System.Drawing.Size(69, 17)
        Me.TwoDice.TabIndex = 1
        Me.TwoDice.Text = "TwoDice"
        Me.TwoDice.UseVisualStyleBackColor = True
        Me.TwoDice.Visible = False
        '
        'main
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSize = True
        Me.ClientSize = New System.Drawing.Size(520, 266)
        Me.Controls.Add(Me.TwoDice)
        Me.Controls.Add(Me.addendsList)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "main"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Form1"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents addendsList As System.Windows.Forms.TextBox
    Friend WithEvents TwoDice As System.Windows.Forms.CheckBox

End Class
