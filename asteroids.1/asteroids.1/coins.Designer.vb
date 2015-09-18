<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class coins
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
        Me.components = New System.ComponentModel.Container()
        Me.coinTimer = New System.Windows.Forms.Timer(Me.components)
        Me.coinPile = New System.Windows.Forms.PictureBox()
        Me.coinslot = New System.Windows.Forms.PictureBox()
        CType(Me.coinPile, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.coinslot, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'coinTimer
        '
        Me.coinTimer.Enabled = True
        Me.coinTimer.Interval = 20
        '
        'coinPile
        '
        Me.coinPile.Image = Global.Asteroids.My.Resources.Resources.coin_piles
        Me.coinPile.Location = New System.Drawing.Point(12, 352)
        Me.coinPile.Name = "coinPile"
        Me.coinPile.Size = New System.Drawing.Size(196, 152)
        Me.coinPile.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.coinPile.TabIndex = 1
        Me.coinPile.TabStop = False
        '
        'coinslot
        '
        Me.coinslot.Cursor = System.Windows.Forms.Cursors.Hand
        Me.coinslot.Image = Global.Asteroids.My.Resources.Resources.coinslot
        Me.coinslot.Location = New System.Drawing.Point(50, 114)
        Me.coinslot.Name = "coinslot"
        Me.coinslot.Size = New System.Drawing.Size(123, 158)
        Me.coinslot.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.coinslot.TabIndex = 0
        Me.coinslot.TabStop = False
        '
        'coins
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.BackColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.ClientSize = New System.Drawing.Size(240, 549)
        Me.Controls.Add(Me.coinPile)
        Me.Controls.Add(Me.coinslot)
        Me.DoubleBuffered = True
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "coins"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "coins"
        CType(Me.coinPile, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.coinslot, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents coinslot As PictureBox
    Friend WithEvents coinPile As PictureBox
    Friend WithEvents coinTimer As Timer
End Class
