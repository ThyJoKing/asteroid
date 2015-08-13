Imports System.Drawing.Text
Imports System.Runtime.InteropServices
Imports System.IO
Imports System.Text
Imports Microsoft.VisualBasic.ApplicationServices

Module debug
    Public debugging As Boolean = True
    Public hitbox As Boolean = True         'Hitbox show
    Public bulLimit As Integer = 4          'The number of bullets onscreen per player
    Public bulTime As Integer = 25          'The amount of time the bullet stays on screen
    Public start As Boolean = False         'To change the number and size of asteroids
    Public howMany As Integer = 100         'How many asteroids
    Public whatSize As Integer = 3          'What size
    Public velocityNo As Boolean = False    'Whether the asteroid is moving
    Public invincibleLength As Integer = 250 'The length of the invincibility period

    'asteroid specifics
    Public Property minRadius As Integer = 20   'The minimum size of asteroid
    Public Property maxRadius As Integer = 35   'The maximum size
    Public Property gran As Integer = 15        'The amount of spiky bits
    Public Property minVary As Integer = 10     'How minimum amount of variation
    Public Property maxVary As Integer = 30     'The maximum amount

    Public Property exploTime As Integer = 150  'The length of the explosion
    Public Property exploMove As Integer = 6    'The speed the explosion can spread
    Public Property exploPercent As Integer = 2 'The velocity the explosion inherits from its object

    Public Property endTime As Integer = 50

    Public Property highFirst As Boolean = False 'Skips straight to highscore

    Public Property bulletSpeed As Integer = 30

    Public Property colour As Pen = Pens.White

    Public Property fadeArray As New List(Of Pen) From {Pens.White, Pens.LightGray, Pens.DarkGray, Pens.Gray, Pens.DimGray, Pens.Black}
    'Ordered from white to black (oddly enough, darkgray is lighter than gray)
End Module

Module initialise
    'Sound Variables
    Public soundCounter As Integer = 0      'Counter for time between high and low sound
    Public soundLimit As Integer = 70       'Interval between high and low
    Public highSound As Boolean = True      'Whether it is high sound's turn
    Public level As Integer = 1             'NOTE: RESET WHEN GAMELOAD

    'Option Variables
    Public coop As Boolean = False          'For player 1 and player 2
    Public hotKeys As New Dictionary(Of String, Keys) 'The keys to control the character
    Public sensitivity As Integer = 8       'The speed at which the ship rotates
    Public shipBorders As Integer = 300     'The safezone spawn radius around the ship
    Public sound As Boolean = False         'Mute or not

    'Label reference
    Public highscoreLabels As New List(Of Label) From {menu.highscore1, menu.highscore2, menu.highscore3, menu.highscore4, menu.highscore5}
    Public roundLabels As New List(Of Label) From {menu.round1, menu.round2, menu.round3, menu.round4, menu.round5}
    Public nameLabels As New List(Of Label) From {menu.name1, menu.name2, menu.name3, menu.name4, menu.name5}

    Public spriteArray As New List(Of Generic.List(Of Object)) From {New List(Of Object), New List(Of Object), New List(Of Object), New List(Of Object)}
    'sprites in order
    '0. asteroids
    '1. player ship
    '2. enemy ship
    '3. bullets
    Public explosionArray As New List(Of explosion) From {}

    Public gamestate As String = "menu" 'The current Gamestate
    Public endTimer As Integer = 0
    Public endScore1 As Integer = -1
    Public endScore2 As Integer = -1
    Public onboard As Boolean = False
    Public endPlace1 As Integer
    Public endPlace2 As Integer
    Public letterPlace1 As Integer
    Public letterPlace2 As Integer
    Public letters1 As List(Of Integer)
    Public letters2 As List(Of Integer)
    Public letterCool1 As Boolean
    Public letterCool2 As Boolean

    Public allLetters = New List(Of Char) From {"A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z"}

    Public Declare Function GetAsyncKeyState Lib "user32.dll" (ByVal vKey As Int32) As UShort 'The keycheck function

    Public hyperspaceFont As PrivateFontCollection = New PrivateFontCollection 'The Hyperspace Font
    Public Sub fontInit()
        Dim fontMemPointer As IntPtr = Marshal.AllocCoTaskMem(My.Resources.Hyperspace.Length)
        Marshal.Copy(My.Resources.Hyperspace, 0, fontMemPointer, My.Resources.Hyperspace.Length)
        hyperspaceFont.AddMemoryFont(fontMemPointer, My.Resources.Hyperspace.Length)
        Marshal.FreeCoTaskMem(fontMemPointer)
    End Sub     'Declaring the hyperspace font
    Public Sub hotKeysInit()
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
    End Sub  'Declaring the initial hotkeys
    Public Sub labelInit()
        'Title Screen
        menu.title.Font = New Font(hyperspaceFont.Families(0), 100, FontStyle.Italic) : menu.title.Location = New Point(menu.Width / 2 - menu.title.Width / 2, menu.Height / 5)
        menu.playButton.Font = New Font(hyperspaceFont.Families(0), 60) : menu.playButton.Location = New Point(menu.Width / 2 - menu.playButton.Width / 2, menu.Height / 2)
        menu.optionsButton.Font = New Font(hyperspaceFont.Families(0), 60) : menu.optionsButton.Location = New Point(menu.Width / 2 - menu.optionsButton.Width / 2, 2 * menu.Height / 3)
        menu.Coins.Font = New Font(hyperspaceFont.Families(0), 20, FontStyle.Bold) : menu.Coins.Location = New Point(menu.Width / 2 - menu.Coins.Width / 2, menu.Height - 50)

        'Player score and title
        menu.player1Title.Font = New Font(hyperspaceFont.Families(0), 30, FontStyle.Underline) : menu.player1Title.Location = New System.Drawing.Point(8, 10)
        menu.player1Score.Font = New Font(hyperspaceFont.Families(0), 30, FontStyle.Underline) : menu.player1Score.Location = New System.Drawing.Point(-20, 70)
        menu.player2Title.Location = New System.Drawing.Point(menu.Width - 200, 10) : menu.player2Title.Font = New Font(hyperspaceFont.Families(0), 30, FontStyle.Underline)
        menu.player2Score.Location = New System.Drawing.Point(menu.Width - 390, 70) : menu.player2Score.Font = New Font(hyperspaceFont.Families(0), 30, FontStyle.Underline)

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

    End Sub    'Declaring the font of all labels and their positions
    Public Sub screenInit()
        menu.Size = New Size(900, 900)
        menu.Top = My.Computer.Screen.Bounds.Height / 2 - menu.Height / 2
        menu.Left = My.Computer.Screen.Bounds.Width / 2 - menu.Width / 2
        setCursor(My.Resources.shipLife)
    End Sub   'Declare the screen specifics
End Module
