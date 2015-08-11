<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class menu
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
        Me.gameTimer = New System.Windows.Forms.Timer(Me.components)
        Me.soundTimer = New System.Windows.Forms.Timer(Me.components)
        Me.playButton = New System.Windows.Forms.Label()
        Me.optionsButton = New System.Windows.Forms.Label()
        Me.title = New System.Windows.Forms.Label()
        Me.player1Score = New System.Windows.Forms.Label()
        Me.player1Title = New System.Windows.Forms.Label()
        Me.player2Title = New System.Windows.Forms.Label()
        Me.player2Score = New System.Windows.Forms.Label()
        Me.pauseResume = New System.Windows.Forms.Label()
        Me.pauseExit = New System.Windows.Forms.Label()
        Me.Coins = New System.Windows.Forms.Label()
        Me.pauseRestart = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'gameTimer
        '
        Me.gameTimer.Interval = 20
        '
        'soundTimer
        '
        Me.soundTimer.Interval = 10
        '
        'playButton
        '
        Me.playButton.AutoSize = True
        Me.playButton.BackColor = System.Drawing.Color.Transparent
        Me.playButton.ForeColor = System.Drawing.Color.White
        Me.playButton.Location = New System.Drawing.Point(421, 281)
        Me.playButton.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.playButton.Name = "playButton"
        Me.playButton.Size = New System.Drawing.Size(38, 24)
        Me.playButton.TabIndex = 9
        Me.playButton.Text = "Play"
        Me.playButton.UseCompatibleTextRendering = True
        '
        'optionsButton
        '
        Me.optionsButton.AutoSize = True
        Me.optionsButton.BackColor = System.Drawing.Color.Transparent
        Me.optionsButton.ForeColor = System.Drawing.Color.White
        Me.optionsButton.Location = New System.Drawing.Point(410, 305)
        Me.optionsButton.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.optionsButton.Name = "optionsButton"
        Me.optionsButton.Size = New System.Drawing.Size(63, 24)
        Me.optionsButton.TabIndex = 11
        Me.optionsButton.Text = "Options"
        Me.optionsButton.UseCompatibleTextRendering = True
        '
        'title
        '
        Me.title.AutoSize = True
        Me.title.BackColor = System.Drawing.Color.Transparent
        Me.title.ForeColor = System.Drawing.Color.White
        Me.title.Location = New System.Drawing.Point(403, 239)
        Me.title.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.title.Name = "title"
        Me.title.Size = New System.Drawing.Size(75, 24)
        Me.title.TabIndex = 8
        Me.title.Text = "Asteroids"
        Me.title.UseCompatibleTextRendering = True
        '
        'player1Score
        '
        Me.player1Score.AutoSize = True
        Me.player1Score.BackColor = System.Drawing.Color.Transparent
        Me.player1Score.ForeColor = System.Drawing.Color.White
        Me.player1Score.Location = New System.Drawing.Point(13, 33)
        Me.player1Score.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.player1Score.Name = "player1Score"
        Me.player1Score.Size = New System.Drawing.Size(46, 24)
        Me.player1Score.TabIndex = 18
        Me.player1Score.Text = "score"
        Me.player1Score.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.player1Score.UseCompatibleTextRendering = True
        '
        'player1Title
        '
        Me.player1Title.AutoSize = True
        Me.player1Title.BackColor = System.Drawing.Color.Transparent
        Me.player1Title.ForeColor = System.Drawing.Color.White
        Me.player1Title.Location = New System.Drawing.Point(13, 9)
        Me.player1Title.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.player1Title.Name = "player1Title"
        Me.player1Title.Size = New System.Drawing.Size(64, 24)
        Me.player1Title.TabIndex = 19
        Me.player1Title.Text = "player 1"
        Me.player1Title.UseCompatibleTextRendering = True
        '
        'player2Title
        '
        Me.player2Title.AutoSize = True
        Me.player2Title.BackColor = System.Drawing.Color.Transparent
        Me.player2Title.ForeColor = System.Drawing.Color.White
        Me.player2Title.Location = New System.Drawing.Point(823, 9)
        Me.player2Title.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.player2Title.Name = "player2Title"
        Me.player2Title.Size = New System.Drawing.Size(64, 24)
        Me.player2Title.TabIndex = 20
        Me.player2Title.Text = "player 2"
        Me.player2Title.UseCompatibleTextRendering = True
        '
        'player2Score
        '
        Me.player2Score.BackColor = System.Drawing.Color.Transparent
        Me.player2Score.ForeColor = System.Drawing.Color.White
        Me.player2Score.Location = New System.Drawing.Point(392, 33)
        Me.player2Score.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.player2Score.Name = "player2Score"
        Me.player2Score.Size = New System.Drawing.Size(495, 110)
        Me.player2Score.TabIndex = 21
        Me.player2Score.Text = "score"
        Me.player2Score.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.player2Score.UseCompatibleTextRendering = True
        '
        'pauseResume
        '
        Me.pauseResume.AutoSize = True
        Me.pauseResume.BackColor = System.Drawing.Color.Transparent
        Me.pauseResume.ForeColor = System.Drawing.Color.White
        Me.pauseResume.Location = New System.Drawing.Point(410, 435)
        Me.pauseResume.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.pauseResume.Name = "pauseResume"
        Me.pauseResume.Size = New System.Drawing.Size(67, 24)
        Me.pauseResume.TabIndex = 22
        Me.pauseResume.Text = "Resume"
        Me.pauseResume.UseCompatibleTextRendering = True
        '
        'pauseExit
        '
        Me.pauseExit.AutoSize = True
        Me.pauseExit.BackColor = System.Drawing.Color.Transparent
        Me.pauseExit.ForeColor = System.Drawing.Color.White
        Me.pauseExit.Location = New System.Drawing.Point(421, 476)
        Me.pauseExit.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.pauseExit.Name = "pauseExit"
        Me.pauseExit.Size = New System.Drawing.Size(33, 24)
        Me.pauseExit.TabIndex = 23
        Me.pauseExit.Text = "Exit"
        Me.pauseExit.UseCompatibleTextRendering = True
        '
        'Coins
        '
        Me.Coins.AutoSize = True
        Me.Coins.BackColor = System.Drawing.Color.Transparent
        Me.Coins.ForeColor = System.Drawing.Color.White
        Me.Coins.Location = New System.Drawing.Point(629, 798)
        Me.Coins.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Coins.Name = "Coins"
        Me.Coins.Size = New System.Drawing.Size(85, 24)
        Me.Coins.TabIndex = 24
        Me.Coins.Text = "Coins(s): 0"
        Me.Coins.UseCompatibleTextRendering = True
        '
        'pauseRestart
        '
        Me.pauseRestart.AutoSize = True
        Me.pauseRestart.BackColor = System.Drawing.Color.Transparent
        Me.pauseRestart.ForeColor = System.Drawing.Color.White
        Me.pauseRestart.Location = New System.Drawing.Point(410, 392)
        Me.pauseRestart.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.pauseRestart.Name = "pauseRestart"
        Me.pauseRestart.Size = New System.Drawing.Size(59, 24)
        Me.pauseRestart.TabIndex = 25
        Me.pauseRestart.Text = "Restart"
        Me.pauseRestart.UseCompatibleTextRendering = True
        '
        'menu
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.BackColor = System.Drawing.Color.Black
        Me.ClientSize = New System.Drawing.Size(900, 900)
        Me.Controls.Add(Me.pauseRestart)
        Me.Controls.Add(Me.Coins)
        Me.Controls.Add(Me.pauseExit)
        Me.Controls.Add(Me.pauseResume)
        Me.Controls.Add(Me.player2Score)
        Me.Controls.Add(Me.player2Title)
        Me.Controls.Add(Me.player1Title)
        Me.Controls.Add(Me.player1Score)
        Me.Controls.Add(Me.optionsButton)
        Me.Controls.Add(Me.playButton)
        Me.Controls.Add(Me.title)
        Me.Cursor = System.Windows.Forms.Cursors.Arrow
        Me.ForeColor = System.Drawing.Color.Black
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Name = "menu"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Form1"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents gameTimer As System.Windows.Forms.Timer
    Friend WithEvents soundTimer As System.Windows.Forms.Timer
    Friend WithEvents playButton As System.Windows.Forms.Label
    Friend WithEvents optionsButton As System.Windows.Forms.Label
    Friend WithEvents title As System.Windows.Forms.Label
    Friend WithEvents player1Score As System.Windows.Forms.Label
    Friend WithEvents player1Title As System.Windows.Forms.Label
    Friend WithEvents player2Title As System.Windows.Forms.Label
    Friend WithEvents player2Score As System.Windows.Forms.Label
    Friend WithEvents pauseResume As System.Windows.Forms.Label
    Friend WithEvents pauseExit As System.Windows.Forms.Label
    Friend WithEvents Coins As System.Windows.Forms.Label
    Friend WithEvents pauseRestart As System.Windows.Forms.Label

End Class
