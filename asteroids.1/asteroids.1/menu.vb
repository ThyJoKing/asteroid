Imports System.Math
Imports System.Drawing.Drawing2D
Imports System.Drawing.Text

'   things to do:
'       do threads
'       enemy ships - IN PROGRESS
'       highscores
'       TO REMEMBER:
'       application.startup path or something for high score record
'       etc.

Public Class menu
    Public Sub baseLoad(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Size = New Size(900, 900)
        Me.Top = My.Computer.Screen.Bounds.Height / 2 - Me.Height / 2
        Me.Left = My.Computer.Screen.Bounds.Width / 2 - Me.Width / 2
        DoubleBuffered = True
        hotKeysInit()
        fontInit()
        labelInit()
        Randomize()
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
        If gamestate = "play" Or gamestate = "over" Then
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
    End Sub   'The main game timer
    Public Sub painting(sender As Object, e As PaintEventArgs) Handles Me.Paint
        spriteDraw(e)
        explosionsDraw(e)
        If gamestate = "play" Then livesDraw(e)
    End Sub              'The sprite draw

    Public Sub menuLoad()
        gamestate = "menu"
        setCursor(My.Resources.shipThrust)

        player1Score.Visible = False : player1Title.Visible = False
        player2Score.Visible = False : player2Title.Visible = False
        pauseExit.Visible = False : pauseResume.Visible = False : pauseRestart.Visible = False

        title.Visible = True : optionsButton.Visible = True : playButton.Visible = True
        spriteArray = New List(Of Generic.List(Of Object)) From {New List(Of Object), New List(Of Object), New List(Of Object), New List(Of Object), New List(Of Object), New List(Of Object), New List(Of Object)}
        endTimer = 0
        For ast As Integer = 1 To 3
            spriteArray(0).Add(New asteroid(ast, Nothing)) : spriteArray(0).Add(New asteroid(ast, Nothing)) : spriteArray(0).Add(New asteroid(ast, Nothing))
        Next
    End Sub     'Menu 
    Public Sub optionsLoad()
        title.Visible = False
        playButton.Visible = False
        optionsButton.Visible = False
        gamestate = "options"
    End Sub  'Options
    Public Sub gameLoad()
        Cursor.Hide()
        keyReset()
        spriteArray = New List(Of Generic.List(Of Object)) From {New List(Of Object), New List(Of Object), New List(Of Object), New List(Of Object), New List(Of Object), New List(Of Object), New List(Of Object)}
        explosionArray = New List(Of explosion)
        gamestate = "play"

        title.Visible = False : playButton.Visible = False : optionsButton.Visible = False
        pauseExit.Visible = False : pauseResume.Visible = False : pauseRestart.Visible = False
        player1Score.Visible = True : player1Title.Visible = True

        spriteArray(1).Add(New ship(coop, 1))
        If coop Then
            spriteArray(1).Add(New ship(coop, 2))
            player2Score.Visible = True : player2Title.Visible = True
        End If

        'spriteArray(2).Add(New enemyShip(2))

        level = 1
        levelLoad()
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
    End Sub    'Level
    Public Sub pauseLoad()
        Cursor.Show()
        gameTimer.Enabled = False
        setCursor(My.Resources.shipLife)
        pauseExit.Visible = True : pauseResume.Visible = True : pauseRestart.Visible = True
    End Sub    'Pause
    Public Sub highLoad()
        gamestate = "highscore"
        Cursor.Show()

        endScore1 = 0 : endScore2 = 0
        If coop Then endScore2 = spriteArray(1)(1).score
        endScore1 = spriteArray(1)(0).score

        player1Score.Visible = False : player1Title.Visible = False
        player2Score.Visible = False : player2Title.Visible = False
        pauseExit.Visible = False : pauseResume.Visible = False : pauseRestart.Visible = False
        title.Visible = False : optionsButton.Visible = False : playButton.Visible = False

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
        pauseExit.Visible = False : pauseResume.Visible = False : pauseRestart.Visible = False
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
End Class
