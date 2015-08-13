Imports System.Math
Imports System.Drawing.Drawing2D
Imports System.Drawing.Text
Imports Microsoft.VisualBasic.ApplicationServices

'   things to do:
'       do threads
'       enemy ships - IN PROGRESS
'       highscores
'       TO REMEMBER:
'       application.startup path or something for high score record
'       etc.

Public Class menu
    Public Event Shutdown As ShutdownEventHandler
    Public Sub baseLoad(sender As Object, e As EventArgs) Handles MyBase.Load
        Randomize()
        DoubleBuffered = True
        highscoreInit()
        screenInit()
        hotKeysInit()
        fontInit()
        labelInit()
        menuLoad()
        gameTimer.Enabled = True
        soundTimer.Enabled = sound
        If Not debugging Then
            state.Visible = False
        End If
    End Sub
    Private Sub menu_LostFocus(sender As Object, e As EventArgs) Handles Me.LostFocus
        If gamestate = "play" Then
            pauseLoad()
        End If
    End Sub
    Private Sub MyApplication_Shutdown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shutdown
        If onboard Then
            highScoreRecord()
        End If
    End Sub

    Public Sub Timer2_Tick(sender As Object, e As EventArgs) Handles soundTimer.Tick
        soundCounter += 1
        If soundCounter > soundLimit Then
            If highSound Then
                highSound = False
                'My.Computer.Audio.Play(My.Resources.thumphi, AudioPlayMode.Background)
            Else
                'My.Computer.Audio.Play(My.Resources.thumplo, AudioPlayMode.Background)
                highSound = True
            End If
            soundCounter = 0
        End If
    End Sub         'The sound timer
    Public Sub Timer1_Tick(sender As Object, e As System.EventArgs) Handles gameTimer.Tick
        Me.Invalidate()
        If gamestate = "play" Or gamestate = "over" And endTimer <= endTime Then
            collisionThreads()
            player1Score.Text = Str(spriteArray(1)(0).score)
            If coop Then player2Score.Text = Str(spriteArray(1)(1).score)
            If gamestate = "over" Then endTimer += 1
            If endTimer > endTime Then highLoad()
            gameOverCheck()
        End If
        If spriteArray(1).Count <> 0 Then
            For Each obj As Object In spriteArray(1) : keyChecks(obj) : Next
        End If
        moveEverything()
        bulletCheck()
        explosionCheck()
        If spriteArray(0).Count = 0 Then : levelLoad() : level += 1 : End If
        If GetAsyncKeyState(Convert.ToInt32(hotKeys("pause"))) And gamestate = "play" Then pauseLoad()
        state.Text = gamestate
        If gamestate = "over" Then
            If onboard Then scoreRecord()
        End If
    End Sub   'The main game timer
    Public Sub painting(sender As Object, e As PaintEventArgs) Handles Me.Paint
        spriteDraw(e)
        explosionsDraw(e)
        If gamestate = "play" Then livesDraw(e)
    End Sub              'The sprite draw

    Public Sub menuLoad()
        gamestate = "menu"

        menuVisible(True)
        pauseVisible(False)
        scoreVisible(False, False)
        highscoreVisible(False)
        highscoreVisible(False)

        spriteArray = New List(Of Generic.List(Of Object)) From {New List(Of Object), New List(Of Object), New List(Of Object), New List(Of Object), New List(Of Object), New List(Of Object), New List(Of Object)}
        endTimer = 0
        For ast As Integer = 1 To 3
            spriteArray(0).Add(New asteroid(ast, Nothing)) : spriteArray(0).Add(New asteroid(ast, Nothing)) : spriteArray(0).Add(New asteroid(ast, Nothing))
        Next
    End Sub     'Menu 
    Public Sub optionsLoad()
        End 'Temp
    End Sub  'Options
    Public Sub gameLoad()
        Cursor.Hide()
        keyReset()
        spriteArray = New List(Of Generic.List(Of Object)) From {New List(Of Object), New List(Of Object), New List(Of Object), New List(Of Object), New List(Of Object), New List(Of Object), New List(Of Object)}
        explosionArray = New List(Of explosion)
        gamestate = "play"

        menuVisible(False)
        pauseVisible(False)
        highscoreVisible(False)

        spriteArray(1).Add(New ship(coop, 1))
        scoreVisible(True, False)
        If coop Then
            spriteArray(1).Add(New ship(coop, 2))
            scoreVisible(True, True)
        End If
        level = 1
        levelLoad()
        If highFirst Then highLoad()
    End Sub     'Game
    Public Sub levelLoad()
        If Not start Then
            Dim num As Integer = 0
            Do Until num > level + 1 Or num > 11
                spriteArray(0).Add(New asteroid(1, Nothing))
                num += 1
            Loop
        Else 'debugging
            Dim num As Integer
            Do Until num > howMany
                spriteArray(0).Add(New asteroid(whatSize, Nothing))
                num += 1
            Loop
        End If
        'spriteArray(2).Add(New enemyShip(2))
    End Sub    'Level
    Public Sub pauseLoad()
        Cursor.Show()
        gameTimer.Enabled = False
        setCursor(My.Resources.shipLife)
        pauseVisible(True)
    End Sub    'Pause
    Public Sub highLoad()
        gamestate = "highscore"
        Cursor.Show()

        endScore1 = 0 : endScore2 = 0
        If coop Then endScore2 = spriteArray(1)(1).score
        endScore1 = spriteArray(1)(0).score

        menuVisible(False)
        pauseVisible(False)
        highscoreVisible(True)
        scoreVisible(False, False)
        highscoreInit()
    End Sub

    Public Sub playButton_Click(sender As Object, e As EventArgs) Handles playButton.Click
        gameLoad()
    End Sub         'Play Button
    Public Sub optionsButton_Click(sender As Object, e As EventArgs) Handles optionsButton.Click
        optionsLoad()
    End Sub   'Options Button

    Private Sub pauseResume_Click(sender As Object, e As EventArgs) Handles pauseResume.Click
        Cursor.Hide()
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

    Private Sub highscoreBack_Click(sender As Object, e As EventArgs) Handles highscoreBack.Click
        highScoreRecord()
        menuLoad()
    End Sub  'Highscore Back button
End Class
