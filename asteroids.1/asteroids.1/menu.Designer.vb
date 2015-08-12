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
        Me.state = New System.Windows.Forms.Label()
        Me.highScoreTitle = New System.Windows.Forms.Label()
        Me.highscore1 = New System.Windows.Forms.Label()
        Me.highscore2 = New System.Windows.Forms.Label()
        Me.highscore3 = New System.Windows.Forms.Label()
        Me.highscore5 = New System.Windows.Forms.Label()
        Me.highscore4 = New System.Windows.Forms.Label()
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
        Me.playButton.Location = New System.Drawing.Point(498, 586)
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
        Me.optionsButton.Location = New System.Drawing.Point(487, 610)
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
        Me.title.Location = New System.Drawing.Point(480, 544)
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
        Me.player1Score.Location = New System.Drawing.Point(373, 568)
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
        Me.player1Title.Location = New System.Drawing.Point(373, 544)
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
        Me.player2Title.Location = New System.Drawing.Point(373, 610)
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
        Me.player2Score.Location = New System.Drawing.Point(30, 634)
        Me.player2Score.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.player2Score.Name = "player2Score"
        Me.player2Score.Size = New System.Drawing.Size(407, 110)
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
        Me.pauseResume.Location = New System.Drawing.Point(590, 587)
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
        Me.pauseExit.Location = New System.Drawing.Point(601, 628)
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
        Me.pauseRestart.Location = New System.Drawing.Point(590, 544)
        Me.pauseRestart.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.pauseRestart.Name = "pauseRestart"
        Me.pauseRestart.Size = New System.Drawing.Size(59, 24)
        Me.pauseRestart.TabIndex = 25
        Me.pauseRestart.Text = "Restart"
        Me.pauseRestart.UseCompatibleTextRendering = True
        '
        'state
        '
        Me.state.AutoSize = True
        Me.state.BackColor = System.Drawing.Color.Transparent
        Me.state.ForeColor = System.Drawing.Color.White
        Me.state.Location = New System.Drawing.Point(13, 628)
        Me.state.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.state.Name = "state"
        Me.state.Size = New System.Drawing.Size(83, 24)
        Me.state.TabIndex = 26
        Me.state.Text = "gamestate"
        Me.state.UseCompatibleTextRendering = True
        '
        'highScoreTitle
        '
        Me.highScoreTitle.AutoSize = True
        Me.highScoreTitle.BackColor = System.Drawing.Color.Transparent
        Me.highScoreTitle.ForeColor = System.Drawing.Color.White
        Me.highScoreTitle.Location = New System.Drawing.Point(781, 479)
        Me.highScoreTitle.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.highScoreTitle.Name = "highScoreTitle"
        Me.highScoreTitle.Size = New System.Drawing.Size(88, 24)
        Me.highScoreTitle.TabIndex = 27
        Me.highScoreTitle.Text = "Highscores"
        Me.highScoreTitle.UseCompatibleTextRendering = True
        '
        'highscore1
        '
        Me.highscore1.AutoSize = True
        Me.highscore1.BackColor = System.Drawing.Color.Transparent
        Me.highscore1.ForeColor = System.Drawing.Color.White
        Me.highscore1.Location = New System.Drawing.Point(741, 520)
        Me.highscore1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.highscore1.Name = "highscore1"
        Me.highscore1.Size = New System.Drawing.Size(89, 24)
        Me.highscore1.TabIndex = 28
        Me.highscore1.Text = "Highscore1"
        Me.highscore1.UseCompatibleTextRendering = True
        '
        'highscore2
        '
        Me.highscore2.AutoSize = True
        Me.highscore2.BackColor = System.Drawing.Color.Transparent
        Me.highscore2.ForeColor = System.Drawing.Color.White
        Me.highscore2.Location = New System.Drawing.Point(740, 544)
        Me.highscore2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.highscore2.Name = "highscore2"
        Me.highscore2.Size = New System.Drawing.Size(89, 24)
        Me.highscore2.TabIndex = 29
        Me.highscore2.Text = "Highscore2"
        Me.highscore2.UseCompatibleTextRendering = True
        '
        'highscore3
        '
        Me.highscore3.AutoSize = True
        Me.highscore3.BackColor = System.Drawing.Color.Transparent
        Me.highscore3.ForeColor = System.Drawing.Color.White
        Me.highscore3.Location = New System.Drawing.Point(739, 568)
        Me.highscore3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.highscore3.Name = "highscore3"
        Me.highscore3.Size = New System.Drawing.Size(89, 24)
        Me.highscore3.TabIndex = 30
        Me.highscore3.Text = "Highscore3"
        Me.highscore3.UseCompatibleTextRendering = True
        '
        'highscore5
        '
        Me.highscore5.AutoSize = True
        Me.highscore5.BackColor = System.Drawing.Color.Transparent
        Me.highscore5.ForeColor = System.Drawing.Color.White
        Me.highscore5.Location = New System.Drawing.Point(740, 616)
        Me.highscore5.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.highscore5.Name = "highscore5"
        Me.highscore5.Size = New System.Drawing.Size(89, 24)
        Me.highscore5.TabIndex = 32
        Me.highscore5.Text = "Highscore5"
        Me.highscore5.UseCompatibleTextRendering = True
        '
        'highscore4
        '
        Me.highscore4.AutoSize = True
        Me.highscore4.BackColor = System.Drawing.Color.Transparent
        Me.highscore4.ForeColor = System.Drawing.Color.White
        Me.highscore4.Location = New System.Drawing.Point(739, 592)
        Me.highscore4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.highscore4.Name = "highscore4"
        Me.highscore4.Size = New System.Drawing.Size(89, 24)
        Me.highscore4.TabIndex = 31
        Me.highscore4.Text = "Highscore4"
        Me.highscore4.UseCompatibleTextRendering = True
        '
        'menu
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.BackColor = System.Drawing.Color.Black
        Me.ClientSize = New System.Drawing.Size(900, 900)
        Me.Controls.Add(Me.highscore5)
        Me.Controls.Add(Me.highscore4)
        Me.Controls.Add(Me.highscore3)
        Me.Controls.Add(Me.highscore2)
        Me.Controls.Add(Me.highscore1)
        Me.Controls.Add(Me.highScoreTitle)
        Me.Controls.Add(Me.state)
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
    Friend WithEvents state As System.Windows.Forms.Label
    Friend WithEvents highScoreTitle As System.Windows.Forms.Label
    Friend WithEvents highscore1 As System.Windows.Forms.Label
    Friend WithEvents highscore2 As System.Windows.Forms.Label
    Friend WithEvents highscore3 As System.Windows.Forms.Label
    Friend WithEvents highscore5 As System.Windows.Forms.Label
    Friend WithEvents highscore4 As System.Windows.Forms.Label

End Class
