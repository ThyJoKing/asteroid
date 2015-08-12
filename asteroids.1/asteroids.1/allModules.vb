Imports System.Math
Imports System.Drawing.Drawing2D
Imports System.Drawing.Text
Imports System.Runtime.InteropServices

Module debug
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

    Public spriteArray As New List(Of Generic.List(Of Object)) From {New List(Of Object), New List(Of Object), New List(Of Object), New List(Of Object)}
    'sprites in order
    '0. asteroids
    '1. player ship
    '2. enemy ship
    '3. bullets
    Public explosionArray As New List(Of explosion) From {}

    Public gamestate As String = "menu" 'The current Gamestate
    Public endTimer As Integer = 0

    Public Declare Function GetAsyncKeyState Lib "user32.dll" (ByVal vKey As Int32) As UShort 'The keycheck function

    Public normalFont As PrivateFontCollection = New PrivateFontCollection 'The Hyperspace Font
    Public Sub fontInit()
        Dim fontMemPointer As IntPtr = Marshal.AllocCoTaskMem(My.Resources.Hyperspace.Length)
        Marshal.Copy(My.Resources.Hyperspace, 0, fontMemPointer, My.Resources.Hyperspace.Length)
        normalFont.AddMemoryFont(fontMemPointer, My.Resources.Hyperspace.Length)
        Marshal.FreeCoTaskMem(fontMemPointer)
    End Sub 'Declaring the hyperspace font
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
    End Sub 'Declaring the initial hotkeys
    Public Sub labelInit()
        'Title Screen
        menu.title.Font = New Font(normalFont.Families(0), 100, FontStyle.Italic) : menu.title.Location = New Point(menu.Width / 2 - menu.title.Width / 2, menu.Height / 5)
        menu.playButton.Font = New Font(normalFont.Families(0), 60) : menu.playButton.Location = New Point(menu.Width / 2 - menu.playButton.Width / 2, menu.Height / 2)
        menu.optionsButton.Font = New Font(normalFont.Families(0), 60) : menu.optionsButton.Location = New Point(menu.Width / 2 - menu.optionsButton.Width / 2, 2 * menu.Height / 3)
        menu.Coins.Font = New Font(normalFont.Families(0), 20, FontStyle.Bold) : menu.Coins.Location = New Point(menu.Width / 2 - menu.Coins.Width / 2, menu.Height - 50)

        'Player score and title
        menu.player1Title.Font = New Font(normalFont.Families(0), 30, FontStyle.Underline) : menu.player1Title.Location = New System.Drawing.Point(8, 10)
        menu.player1Score.Font = New Font(normalFont.Families(0), 30, FontStyle.Underline) : menu.player1Score.Location = New System.Drawing.Point(-20, 70)
        menu.player2Title.Location = New System.Drawing.Point(menu.Width - 200, 10) : menu.player2Title.Font = New Font(normalFont.Families(0), 30, FontStyle.Underline)
        menu.player2Score.Location = New System.Drawing.Point(menu.Width - 390, 70) : menu.player2Score.Font = New Font(normalFont.Families(0), 30, FontStyle.Underline)

        'Pause Menu
        menu.pauseResume.Font = New Font(normalFont.Families(0), 40, FontStyle.Italic) : menu.pauseResume.Location = New Point(menu.Width / 2 - menu.pauseResume.Width / 2, menu.Height / 2 + 75)
        menu.pauseRestart.Font = New Font(normalFont.Families(0), 40, FontStyle.Italic) : menu.pauseRestart.Location = New Point(menu.Width / 2 - menu.pauseRestart.Width / 2, menu.Height / 2)
        menu.pauseExit.Font = New Font(normalFont.Families(0), 40, FontStyle.Italic) : menu.pauseExit.Location = New Point(menu.Width / 2 - menu.pauseExit.Width / 2, menu.Height / 2 - 75)
        
    End Sub 'Declaring the font of all labels and their positions
End Module

Module collisionTests
    Public Sub collisionThreads()
        'To do: threading here
        'Thread set 1:
        collision(0, 1) 'Asteroids with Player
        collision(3, 2) 'Bullets with Enemy ships

        'thread set 2:
        collision(0, 2) 'Asteroids with Enemy ships
        collision(1, 3) 'Player with Bullets

        'thread set 3:
        collision(3, 0) 'Bullets with asteroids
        collision(1, 2) 'Player with Enemy

    End Sub                                          'Setting every collision : MAYBE THREADS LATER?
    Public Sub collision(firstObject As Integer, secondObject As Integer)
        Dim firstCount As Integer = 0
        While firstCount < spriteArray(firstObject).Count
            Dim current1 = spriteArray(firstObject)(firstCount)
            Dim collide1 = False
            Dim secondCount As Integer = 0
            While secondCount < spriteArray(secondObject).Count() And Not collide1
                Dim current2 = spriteArray(secondObject)(secondCount)
                If intersects(current1, current2) Then
                    If TypeOf (current1) Is ship Then                                           'Player with Enemy ship or Bullets
                        If TypeOf current2 Is bullet Then
                            If current2.shooter = 3 Then
                                If current1.lives <> 0 Then
                                    current1.lives -= 1
                                Else
                                    gamestate = "gameOver"
                                End If
                                explosionArray.Add(New explosion(current1))
                                current1.spawn()
                                collide1 = True
                            End If
                        ElseIf TypeOf (current2) Is enemyShip Then
                            current1.score += current2.level * 500
                            If current1.lives <> 0 Then
                                current1.lives -= 1
                            Else
                                gamestate = "gameOver"
                            End If
                            explosionArray.Add(New explosion(current1))
                            explosionArray.Add(New explosion(current2))
                            current1.spawn()
                            collide1 = True
                            spriteArray(secondObject).RemoveAt(secondCount)
                        End If
                    ElseIf TypeOf (current1) Is asteroid Then                                   'Asteroid with anything
                        Dim score As Integer
                        If current1.level = 1 Then score = 20 Else If current1.level = 2 Then score = 50 Else score = 100
                        If TypeOf (current2) Is ship Then
                            If Not current2.invincible Then
                                current2.score += score
                                If current2.lives <> 0 Then
                                    current2.lives -= 1
                                Else
                                    current2.shootenable = False
                                    gamestate = "gameOver"
                                End If
                                explosionArray.Add(New explosion(current2))
                                explosionArray.Add(New explosion(current1))
                                current2.spawn()
                            ElseIf TypeOf (current2) Is enemyShip Then
                                spriteArray(secondObject).RemoveAt(secondCount)
                            End If
                            Dim temp As asteroid = current1
                            spriteArray(firstObject).RemoveAt(firstCount)
                            collide1 = True
                            If temp.level <> 3 Then
                                spriteArray(firstObject).Add(New asteroid(temp.level + 1, current1))
                                spriteArray(firstObject).Add(New asteroid(temp.level + 1, current1))
                            End If
                        End If
                    ElseIf TypeOf (current1) Is bullet Then                                     'Bullet with Enemy
                        If TypeOf current2 Is asteroid Then
                            Dim score As Integer
                            explosionArray.Add(New explosion(current2))
                            If current2.level = 1 Then score = 20 Else If current2.level = 2 Then score = 50 Else score = 100
                            If current1.shooter < 3 Then
                                spriteArray(1)(current1.shooter - 1).score += score
                            End If
                            spriteArray(secondObject).RemoveAt(secondCount)
                            secondCount += 1
                            spriteArray(firstObject).RemoveAt(firstCount)
                            collide1 = True
                            If current2.level <> 3 Then
                                spriteArray(secondObject).Add(New asteroid(current2.level + 1, current2))
                                spriteArray(secondObject).Add(New asteroid(current2.level + 1, current2))
                            End If
                        ElseIf TypeOf current2 Is enemyShip Then
                            explosionArray.Add(New explosion(current2))
                            explosionArray.Add(New explosion(current1))
                            If current1.shooter <> 3 Then
                                spriteArray(1)(current1.shooter - 1).score += current2.level * 500
                                spriteArray(secondObject).RemoveAt(secondCount)
                            End If
                            spriteArray(firstObject).RemoveAt(firstCount)
                            collide1 = True
                        End If
                        End If
                End If
                secondCount += 1
            End While
            If Not collide1 Then
                firstCount += 1
            End If
        End While
    End Sub               'Collision between polygons and points/other polygons
    Public Function intersects(list1, list2)
        For Each p In list1.drawPoints
            If PointInPolygon(list2.drawPoints, p.x, p.Y) Then
                Return True
            End If
        Next
        Return False
    End Function                                                               'Collision Check
End Module

Module shipActions
    Public Sub shoot(ship)
        Dim bulletNum As Integer = 0
        For Each bul As bullet In spriteArray(3) 'Check number of bullets
            If bul.shooter = ship.player Then bulletNum += 1
        Next
        If bulletNum < bulLimit Then
            spriteArray(3).Add(New bullet(ship))
            'If sound Then My.Computer.Audio.Play(My.Resources.fire, AudioPlayMode.Background) 'Shoot sound
        End If
    End Sub     'Every ship shooting
    Public Sub thrust(ship As ship)
        ship.xVelocity += Sin(2 * Math.PI * (ship.Angle / 360)) * 0.9
        ship.yVelocity -= Cos(2 * Math.PI * (ship.Angle / 360)) * 0.9
        ship.Image = My.Resources.shipThrust
    End Sub    'Ship thrusting
End Module

Module drawing
    Public Sub livesDraw(e As PaintEventArgs)
        Dim num As Integer = 1
        While num < spriteArray(1)(0).lives
            e.Graphics.DrawImage(My.Resources.shipLife, num * 30 + 220, 15)
            num += 1
        End While
        If coop Then
            num = 1
            While num < spriteArray(1)(1).lives
                e.Graphics.DrawImage(My.Resources.shipLife, menu.Width - (num * 30 + 240), 15)
                num += 1
            End While
        End If
    End Sub  'Draw both ship's lives
    Public Sub spriteDraw(e As PaintEventArgs)
        For Each arr As Object In spriteArray
            For Each obj As Object In arr : obj.draw(e) : Next
        Next
    End Sub 'Draw every sprite (player, asteroids, bullets, enemies)
    Public Sub explosionsDraw(e As PaintEventArgs)
        For Each obj As Object In explosionArray
            obj.draw(e)
        Next
    End Sub
    Public Sub drawRotateImage(image As Image, angle As Double, locationx As Double, locationy As Double, e As PaintEventArgs)
        e.Graphics.TranslateTransform(locationx, locationy)
        E.Graphics.RotateTransform(angle)
        E.Graphics.DrawImage(image, CInt(-image.Width / 2), CInt(-image.Height / 2), image.Width, image.Height)
        E.Graphics.ResetTransform()
    End Sub 'Draw rotated image
End Module

Module keyChecking
    Public Sub keyChecks(ship As ship)
        If Not ship.inHyperspace And ship.invincibleTimer > 0 Then
            If ship.player = 1 Then
                If GetAsyncKeyState(Convert.ToInt32(hotKeys("player1Left"))) Then ship.angle -= sensitivity
                If GetAsyncKeyState(Convert.ToInt32(hotKeys("player1Right"))) Then ship.angle += sensitivity
                If GetAsyncKeyState(Convert.ToInt32(hotKeys("player1Forward"))) Then thrust(ship) Else ship.Image = My.Resources.ship

                If GetAsyncKeyState(Convert.ToInt32(hotKeys("player1Shoot"))) Then
                    If Not ship.bulletCool Then
                        shoot(ship)
                        ship.bulletCool = True
                    End If
                Else
                    ship.bulletCool = False
                End If
                If GetAsyncKeyState(Convert.ToInt32(hotKeys("player1Hyperspace"))) Then ship.hyperspaceStart()
            Else
                If GetAsyncKeyState(Convert.ToInt32(hotKeys("player2Left"))) Then ship.angle -= sensitivity
                If GetAsyncKeyState(Convert.ToInt32(hotKeys("player2Right"))) Then ship.angle += sensitivity
                If GetAsyncKeyState(Convert.ToInt32(hotKeys("player2Forward"))) Then thrust(ship) Else ship.Image = My.Resources.ship

                If GetAsyncKeyState(Convert.ToInt32(hotKeys("player2Shoot"))) Then
                    If Not ship.bulletCool Then
                        shoot(ship)
                        ship.bulletCool = True
                    End If
                Else
                    ship.bulletCool = False
                End If
                If GetAsyncKeyState(Convert.ToInt32(hotKeys("player2Hyperspace"))) Then ship.hyperspaceStart()
            End If
        ElseIf ship.inHyperspace Then
            ship.hyperspaceCounter += 1
            If ship.hyperspaceCounter = 20 Then ship.hyperspace()
        End If
    End Sub 'Check the keys of each player ship
    Public Sub keyReset()
        For Each pair As KeyValuePair(Of String, Keys) In hotKeys
            GetAsyncKeyState(pair.Value)
        Next
    End Sub              'Reset each key check
End Module

Module other
    Public Sub bulletCheck()
        Dim bulletNum As Integer = 0
        While bulletNum < spriteArray(3).Count
            If spriteArray(3)(bulletNum).bulletTime = bulTime Then
                spriteArray(3).RemoveAt(bulletNum)
            Else
                bulletNum += 1
            End If
        End While
        While bulletNum < spriteArray(4).Count
            If spriteArray(4)(bulletNum).bulletTime = bulTime Then
                spriteArray(4).RemoveAt(bulletNum)
            Else
                bulletNum += 1
            End If
        End While
    End Sub           'Check if bullets are expired
    Public Sub explosionCheck()
        Dim num = 0
        While num < explosionArray.Count
            If explosionArray(num).timer > exploTime Then
                explosionArray.RemoveAt(num)
            Else
                num += 1
            End If
        End While
    End Sub

    Public Sub moveEverything()
        For Each arr As Object In spriteArray
            For Each obj As Object In arr
                obj.move()
            Next
        Next
        For Each obj As Object In explosionArray
            obj.move()
        Next
    End Sub        'Move everything according to their velocity 

    Public Sub setCursor(image)
        Dim bm As New Bitmap(80, 80)
        Dim g As Graphics = Graphics.FromImage(bm)
        g.Clear(Color.Transparent)
        g.DrawImage(image, 0, 0)
        g.Dispose()
        menu.Cursor = New Cursor(bm.GetHicon)
    End Sub        'Cursor change
End Module