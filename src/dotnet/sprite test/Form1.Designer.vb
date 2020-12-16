<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
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
        Me.components = New System.ComponentModel.Container
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.badGuy = New System.Windows.Forms.PictureBox
        Me.dude = New System.Windows.Forms.PictureBox
        Me.badGuyAI = New System.Windows.Forms.Timer(Me.components)
        Me.Panel1.SuspendLayout()
        CType(Me.badGuy, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dude, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.badGuy)
        Me.Panel1.Controls.Add(Me.dude)
        Me.Panel1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(200, 175)
        Me.Panel1.TabIndex = 0
        '
        'badGuy
        '
        Me.badGuy.Location = New System.Drawing.Point(12, 68)
        Me.badGuy.Name = "badGuy"
        Me.badGuy.Size = New System.Drawing.Size(100, 50)
        Me.badGuy.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.badGuy.TabIndex = 1
        Me.badGuy.TabStop = False
        '
        'dude
        '
        Me.dude.Location = New System.Drawing.Point(12, 12)
        Me.dude.Name = "dude"
        Me.dude.Size = New System.Drawing.Size(100, 50)
        Me.dude.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.dude.TabIndex = 0
        Me.dude.TabStop = False
        '
        'badGuyAI
        '
        Me.badGuyAI.Interval = 300
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(200, 175)
        Me.Controls.Add(Me.Panel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Name = "Form1"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Sprite Test"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.badGuy, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dude, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents dude As System.Windows.Forms.PictureBox
    Friend WithEvents badGuy As System.Windows.Forms.PictureBox
    Friend WithEvents badGuyAI As System.Windows.Forms.Timer

End Class
