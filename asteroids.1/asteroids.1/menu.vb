Imports System.Math
Imports System.Drawing.Drawing2D
Imports System.Drawing.Text
Imports Microsoft.VisualBasic.ApplicationServices

'   things to do:
'       threads
'       enemy ships - for wesley

Public Class menu
    Public Event Shutdown As ShutdownEventHandler
    Public Sub baseLoad(sender As Object, e As EventArgs) Handles MyBase.Load
        coins.Show()
        Randomize()
        DoubleBuffered = True
        allInit()
        menuLoad()
        gameTimer.Enabled = True
        soundTimer.Enabled = Not mute
        If Not debugging Then
            state.Visible = False
        End If
    End Sub                      'Initial Load
    Private Sub menu_LostFocus(sender As Object, e As EventArgs) Handles Me.LostFocus
        If gamestate = "play" Then
            pauseLoad()
        End If
    End Sub              'Automatic pause if alt-tab
    Private Sub MyApplication_Shutdown(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Shutdown
        If onboard Then
            highScoreRecord()
        End If
    End Sub       'Saves the highscore in the event of unexpected quit

    'Timers
    Public Sub soundTime(sender As Object, e As EventArgs) Handles soundTimer.Tick
        soundAll()
    End Sub                 'The sound timer
    Public Sub gameTime(sender As Object, e As EventArgs) Handles gameTimer.Tick
        Invalidate()
        If gamestate = "play" Or gamestate = "over" And endTimer <= endTime Then
            collisionThreads()
            If coop Then player2Score.Text = Str(spriteArray(1)(1).score)
            player1Score.Text = Str(spriteArray(1)(0).score)
            If gamestate = "over" Then endTimer += 1                        'This is for not immediately switching to highscores
            If endTimer > endTime Then highLoad()
            gameOverCheck()
        End If
        If spriteArray(1).Count <> 0 Then
            For Each obj As Object In spriteArray(1) : keyChecks(obj) : Next
        End If
        moveEverything()
        bulletCheck()
        explosionCheck()
        'enemyCheck()
        If spriteArray(0).Count = 0 Then : level += 1 : levelLoad() : End If
        If GetAsyncKeyState(Convert.ToInt32(hotKeys("pause"))) And gamestate = "play" Then pauseLoad()
        state.Text = gamestate
        If gamestate = "over" Then
            If onboard Then scoreRecord()
        End If
    End Sub                   'The main game timer
    Public Sub painting(sender As Object, e As PaintEventArgs) Handles Me.Paint
        spriteDraw(e)
        explosionsDraw(e)
        If gamestate = "play" Then livesDraw(e)
    End Sub                    'The sprite draw

    'Menu Buttons
    Public Sub playButton_Click(sender As Object, e As EventArgs) Handles playButton.Click
        gameLoad()
    End Sub         'Play Button
    Public Sub optionsButton_Click(sender As Object, e As EventArgs) Handles optionsButton.Click
        optionsLoad()
    End Sub   'Options Button
    Private Sub highscores_Click(sender As Object, e As EventArgs) Handles highscores.Click
        highLoad()
    End Sub        'Highscores Button

    'Pause Buttons
    Private Sub pauseResume_Click(sender As Object, e As EventArgs) Handles pauseResume.Click
        If cursorVis Then
            Cursor.Hide()
            cursorVis = False
        End If
        keyReset()
        pauseVisible(False)
        gameTimer.Enabled = True
    End Sub      'Pause Resume Button
    Private Sub pauseExit_Click(sender As Object, e As EventArgs) Handles pauseExit.Click
        menuLoad()
        gameTimer.Enabled = True
    End Sub          'Pause Exit Button
    Private Sub pauseRestart_Click(sender As Object, e As EventArgs) Handles pauseRestart.Click
        gameLoad()
        gameTimer.Enabled = True
    End Sub    'Pause Restart Button 

    'Highscore Buttons
    Private Sub highscoreBack_Click(sender As Object, e As EventArgs) Handles highscoreBack.Click
        highScoreRecord()
        menuLoad()
    End Sub  'Highscore Back button

    Private Sub label1_Click(sender As Object, e As EventArgs) Handles Label1.Click
        If coop Then
            coop = False
            Label1.Text = "Co-op: False"
        Else
            coop = True
            Label1.Text = "Co-op: True"
        End If
    End Sub
End Class
