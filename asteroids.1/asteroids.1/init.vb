Imports System.Drawing.Text
Imports System.Runtime.InteropServices
Imports System.IO
Imports System.Text
Imports Microsoft.VisualBasic.ApplicationServices

Module debug
    Public Const debugging As Boolean = False
    Public Const bulLimit As Integer = 4          'The number of bullets onscreen per player
    Public Const bulTime As Integer = 25          'The amount of time the bullet stays on screen
    Public Const start As Boolean = False         'To change the number and size of asteroids
    Public Const howMany As Integer = 100         'How many asteroids
    Public Const whatSize As Integer = 3          'What size
    Public Const velocityNo As Boolean = False    'Whether the asteroid is moving
    Public Const invincibleLength As Integer = 250 'The length of the invincibility period
    Public Const shipBorders As Integer = 300     'The safezone spawn radius around the ship

    'asteroid specifics
   
    Public Const exploTime As Integer = 150  'The length of the explosion
    Public Const exploMove As Integer = 6    'The speed the explosion can spread
    Public Const exploPercent As Integer = 2 'The velocity the explosion inherits from its object

    Public Const endTime As Integer = 50

    Public Const highFirst As Boolean = False 'Skips straight to highscore

    Public Const bulletSpeed As Integer = 30

    Public colour As Pen = Pens.White

    Public fadeArray As New List(Of Pen) From {Pens.White, Pens.LightGray, Pens.DarkGray, Pens.Gray, Pens.DimGray, Pens.Black}
    'Ordered from white to black (oddly enough, darkgray is lighter than gray)
End Module

Module initialise
    'Option Variables
    Public coop As Boolean = False          'For player 1 and player 2
    Public sensitivity As Integer = 8       'The speed at which the ship rotates
    Public mute As Boolean = True         'Mute or not

    Public spriteArray As New List(Of Generic.List(Of Object)) From {New List(Of Object), New List(Of Object), New List(Of Object), New List(Of Object)}
    'sprites in order
    '0. asteroids
    '1. player ship
    '2. enemy ship
    '3. bullets
    Public explosionArray As New List(Of explosion) From {}
    Public gamestate As String = "menu" 'The current Gamestate

    Public Sub allInit()
        highscoreInit()
        screenInit()
        hotKeysInit()
        fontInit()
        labelInit()
        lifeImageSet()
    End Sub                         'Initialises everything

    'Fonts
    Public hyperspaceFont As PrivateFontCollection = New PrivateFontCollection 'The Hyperspace Font
    Private Sub fontInit()
        Dim fontMemPointer As IntPtr = Marshal.AllocCoTaskMem(My.Resources.Hyperspace.Length)
        Marshal.Copy(My.Resources.Hyperspace, 0, fontMemPointer, My.Resources.Hyperspace.Length)
        hyperspaceFont.AddMemoryFont(fontMemPointer, My.Resources.Hyperspace.Length)
        Marshal.FreeCoTaskMem(fontMemPointer)
    End Sub                        'Declaring the hyperspace font

    'Hotkeys
    Public Declare Function GetAsyncKeyState Lib "user32.dll" (ByVal vKey As Int32) As UShort 'The keycheck function
    Public hotKeys As New Dictionary(Of String, Keys) 'The keys to control the character
    Private Sub hotKeysInit()
        'Player 1
        hotKeys.Add("player1Left", Keys.A)
        hotKeys.Add("player1Right", Keys.D)
        hotKeys.Add("player1Forward", Keys.W)
        hotKeys.Add("player1Shoot", Keys.Space)
        hotKeys.Add("player1Hyperspace", Keys.LShiftKey)

        'Player 2
        hotKeys.Add("player2Left", Keys.Left)
        hotKeys.Add("player2Right", Keys.Right)
        hotKeys.Add("player2Forward", Keys.Up)
        hotKeys.Add("player2Shoot", Keys.Enter)
        hotKeys.Add("player2Hyperspace", Keys.RShiftKey)

        hotKeys.Add("pause", Keys.Escape)
    End Sub                     'Declaring the initial hotkeys

    'Labels
    Public highscoreLabels As New List(Of Label) From {menu.highscore1, menu.highscore2, menu.highscore3, menu.highscore4, menu.highscore5}
    Private roundLabels As New List(Of Label) From {menu.round1, menu.round2, menu.round3, menu.round4, menu.round5}
    Public nameLabels As New List(Of Label) From {menu.name1, menu.name2, menu.name3, menu.name4, menu.name5}
    Private Sub labelInit()
        'Title Screen
        menu.title.Font = New Font(hyperspaceFont.Families(0), 100, FontStyle.Italic) : menu.title.Location = New Point(menu.Width / 2 - menu.title.Width / 2, 180)
        menu.playButton.Font = New Font(hyperspaceFont.Families(0), 60) : menu.playButton.Location = New Point(menu.Width / 2 - menu.playButton.Width / 2, 450)
        menu.optionsButton.Font = New Font(hyperspaceFont.Families(0), 60) : menu.optionsButton.Location = New Point(menu.Width / 2 - menu.optionsButton.Width / 2, 650)
        menu.Coins.Font = New Font(hyperspaceFont.Families(0), 20, FontStyle.Bold) : menu.Coins.Location = New Point(menu.Width / 2 - menu.Coins.Width / 2, 850)
        menu.highscores.Font = New Font(hyperspaceFont.Families(0), 60) : menu.highscores.Location = New Point(menu.Width / 2 - menu.highscores.Width / 2, 550)


        'Player score and title
        menu.player1Title.Font = New Font(hyperspaceFont.Families(0), 30, FontStyle.Underline) : menu.player1Title.Location = New System.Drawing.Point(8, 10)
        menu.player1Score.Font = New Font(hyperspaceFont.Families(0), 30, FontStyle.Underline) : menu.player1Score.Location = New System.Drawing.Point(-20, 70)
        menu.player2Title.Location = New System.Drawing.Point(menu.Width - 200, 10) : menu.player2Title.Font = New Font(hyperspaceFont.Families(0), 30, FontStyle.Underline)
        menu.player2Score.Location = New System.Drawing.Point(menu.Width - 400, 70) : menu.player2Score.Font = New Font(hyperspaceFont.Families(0), 30, FontStyle.Underline)

        'Pause Menu
        menu.pauseResume.Font = New Font(hyperspaceFont.Families(0), 40, FontStyle.Italic) : menu.pauseResume.Location = New Point(menu.Width / 2 - menu.pauseResume.Width / 2, menu.Height / 2 + 75)
        menu.pauseRestart.Font = New Font(hyperspaceFont.Families(0), 40, FontStyle.Italic) : menu.pauseRestart.Location = New Point(menu.Width / 2 - menu.pauseRestart.Width / 2, menu.Height / 2)
        menu.pauseExit.Font = New Font(hyperspaceFont.Families(0), 40, FontStyle.Italic) : menu.pauseExit.Location = New Point(menu.Width / 2 - menu.pauseExit.Width / 2, menu.Height / 2 - 75)

        'Highscore Menu
        menu.highScoreTitle.Font = New Font(hyperspaceFont.Families(0), 50, FontStyle.Italic) : menu.highScoreTitle.Location = New Point(menu.Width / 2 - menu.highScoreTitle.Width / 2, 100)
        menu.highscoreBack.Font = New Font(hyperspaceFont.Families(0), 40) : menu.highscoreBack.Location = New Point(menu.Width / 2 - menu.highscoreBack.Width / 2, menu.Height - 150)
        Dim num As Integer = 230
        Dim tem As Integer = 0
        While tem < 5
            nameLabels(tem).Font = New Font(hyperspaceFont.Families(0), 40, FontStyle.Regular)
            nameLabels(tem).Location = New Point(menu.Width / 2 + 100, num)

            roundLabels(tem).Font = New Font(hyperspaceFont.Families(0), 40, FontStyle.Regular)
            roundLabels(tem).Location = New Point(menu.Width / 2 - roundLabels(tem).Width / 2 - 200, num)

            highscoreLabels(tem).Size = New Size(250, 100)
            highscoreLabels(tem).Font = New Font(hyperspaceFont.Families(0), 40, FontStyle.Regular)
            highscoreLabels(tem).Location = New Point(menu.Width / 2 - 170, num)
            num += 100
            tem += 1
        End While

    End Sub                       'Declaring the font of all labels and their positions

    'Screen
    Public cursorVis As Boolean = True
    Private Sub screenInit()
        menu.Size = New Size(900, 900)
        menu.Top = My.Computer.Screen.Bounds.Height / 2 - menu.Height / 2
        menu.Left = My.Computer.Screen.Bounds.Width / 2 - menu.Width / 2
        cursorInit(My.Resources.shipThrust)
    End Sub                      'Declare the screen specifics
    Private Sub cursorInit(image As Image)
        Dim bm As Bitmap = New Bitmap(image, New Size(image.Width * 2, image.Height + 10))
        Dim g As Graphics = Graphics.FromImage(bm)
        g.Clear(Color.Transparent)
        g.RotateTransform(340)
        g.DrawImage(image, 0, 22)
        g.Dispose()
        menu.Cursor = New Cursor(bm.GetHicon)
    End Sub        'Cursor set

    Public lifeImage As Image
    Private Sub lifeImageSet()
        lifeImage = ResizeImage(My.Resources.ship, New Size(2 * My.Resources.ship.Width / 3, 2 * My.Resources.ship.Height / 3))
    End Sub
End Module

Module loading
    Public Sub menuLoad()
        If Not cursorVis Then
            Cursor.Show()
            cursorVis = True
        End If
        Cursor.Position = New Point(My.Computer.Screen.WorkingArea.Width / 2, My.Computer.Screen.WorkingArea.Height / 2 + 100)
        gamestate = "menu"
        menuVisible(True)
        pauseVisible(False)
        scoreVisible(False, False)
        highscoreVisible(False)
        highscoreVisible(False)
        endTimer = 0
        spriteArray = New List(Of Generic.List(Of Object)) From {New List(Of Object), New List(Of Object), New List(Of Object), New List(Of Object), New List(Of Object), New List(Of Object), New List(Of Object)}

        For ast As Integer = 1 To 3
            spriteArray(0).Add(New asteroid(ast, Nothing)) : spriteArray(0).Add(New asteroid(ast, Nothing)) : spriteArray(0).Add(New asteroid(ast, Nothing))
        Next
    End Sub     'Loads Menu 
    Public Sub optionsLoad()
        End 'Temp
    End Sub  'Loads Options
    Public Sub gameLoad()
        If cursorVis Then
            Cursor.Hide()
            cursorVis = False
        End If
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
    End Sub     'Loads Game
    Public Sub levelLoad()
        spawnInterval = 5000 / level
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
    End Sub    'Loads Level
    Public Sub pauseLoad()
        Cursor.Position = New Point(My.Computer.Screen.WorkingArea.Width / 2, My.Computer.Screen.WorkingArea.Height / 2 + 150)
        If Not cursorVis Then
            Cursor.Show()
            cursorVis = True
        End If
        menu.gameTimer.Enabled = False
        pauseVisible(True)
    End Sub    'Loads Pause
    Public Sub highLoad()
        If Not cursorVis Then
            Cursor.Show()
            cursorVis = True
        End If
        Cursor.Position = New Point(My.Computer.Screen.WorkingArea.Width / 2, My.Computer.Screen.WorkingArea.Height / 2 + 370)

        endScore1 = -1 : endScore2 = -1
        If gamestate <> "menu" Then
            If coop Then endScore2 = spriteArray(1)(1).score
            endScore1 = spriteArray(1)(0).score
        End If

        gamestate = "highscore"
        menuVisible(False)
        pauseVisible(False)
        highscoreVisible(True)
        scoreVisible(False, False)
        highscoreInit()
    End Sub     'Loads Highscores
End Module
