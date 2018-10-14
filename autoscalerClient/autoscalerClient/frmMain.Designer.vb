<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMain
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMain))
        Me.btnStart = New System.Windows.Forms.Button()
        Me.pictureBox = New System.Windows.Forms.PictureBox()
        Me.tmrRequests = New System.Windows.Forms.Timer(Me.components)
        Me.lblCounter = New System.Windows.Forms.Label()
        Me.txtLog = New System.Windows.Forms.TextBox()
        Me.lblBananaSeconds = New System.Windows.Forms.Label()
        Me.tmrStats = New System.Windows.Forms.Timer(Me.components)
        CType(Me.pictureBox, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnStart
        '
        Me.btnStart.Location = New System.Drawing.Point(12, 292)
        Me.btnStart.Name = "btnStart"
        Me.btnStart.Size = New System.Drawing.Size(260, 39)
        Me.btnStart.TabIndex = 1
        Me.btnStart.Text = "Go!"
        Me.btnStart.UseVisualStyleBackColor = True
        '
        'pictureBox
        '
        Me.pictureBox.Image = Global.autoscalerClient.My.Resources.Resources.stopped
        Me.pictureBox.Location = New System.Drawing.Point(12, 12)
        Me.pictureBox.Name = "pictureBox"
        Me.pictureBox.Size = New System.Drawing.Size(260, 271)
        Me.pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.pictureBox.TabIndex = 0
        Me.pictureBox.TabStop = False
        '
        'tmrRequests
        '
        '
        'lblCounter
        '
        Me.lblCounter.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.lblCounter.BackColor = System.Drawing.Color.Transparent
        Me.lblCounter.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.30189!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCounter.Location = New System.Drawing.Point(198, 206)
        Me.lblCounter.Name = "lblCounter"
        Me.lblCounter.Size = New System.Drawing.Size(86, 38)
        Me.lblCounter.TabIndex = 2
        Me.lblCounter.Text = "0"
        Me.lblCounter.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtLog
        '
        Me.txtLog.Location = New System.Drawing.Point(15, 339)
        Me.txtLog.Multiline = True
        Me.txtLog.Name = "txtLog"
        Me.txtLog.Size = New System.Drawing.Size(256, 136)
        Me.txtLog.TabIndex = 3
        '
        'lblBananaSeconds
        '
        Me.lblBananaSeconds.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.lblBananaSeconds.BackColor = System.Drawing.Color.Transparent
        Me.lblBananaSeconds.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.86792!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBananaSeconds.Location = New System.Drawing.Point(215, 244)
        Me.lblBananaSeconds.Name = "lblBananaSeconds"
        Me.lblBananaSeconds.Size = New System.Drawing.Size(69, 39)
        Me.lblBananaSeconds.TabIndex = 4
        Me.lblBananaSeconds.Text = "0 b/m"
        Me.lblBananaSeconds.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'tmrStats
        '
        Me.tmrStats.Enabled = True
        Me.tmrStats.Interval = 10000
        '
        'frmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(284, 483)
        Me.Controls.Add(Me.lblBananaSeconds)
        Me.Controls.Add(Me.txtLog)
        Me.Controls.Add(Me.lblCounter)
        Me.Controls.Add(Me.btnStart)
        Me.Controls.Add(Me.pictureBox)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "frmMain"
        Me.Text = "Kong wants bananà!"
        CType(Me.pictureBox, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents pictureBox As PictureBox
    Friend WithEvents btnStart As Button
    Friend WithEvents tmrRequests As Timer
    Friend WithEvents lblCounter As Label
    Friend WithEvents txtLog As TextBox
    Friend WithEvents lblBananaSeconds As Label
    Friend WithEvents tmrStats As Timer
End Class
