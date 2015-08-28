Imports System.Math
Imports System.Drawing.Drawing2D
Imports System.Drawing.Text
Imports System.Runtime.InteropServices
Imports System.IO
Imports System.Text

Module collisionTests
    Public Sub collisionThreads()
        'To do: threading here
        'Thread set 1:
        collision(0, 1) 'Asteroids with Player
        'collision(3, 2) 'Bullets with Enemy ships

        'thread set 2:
        'collision(0, 2) 'Asteroids with Enemy ships
        collision(1, 3) 'Player with Bullets

        'thread set 3:
        collision(3, 0) 'Bullets with asteroids
        'collision(1, 2) 'Player with Enemy

    End Sub                                          'Setting every collision : MAYBE THREADS LATER?
    Public Sub collision(firstObject As Integer, secondObject As Integer)
        Dim firstCount As Integer = 0
        While firstCount < spriteArray(firstObject).Count
            Dim current1 = spriteArray(firstObject)(firstCount)
            Dim collide1 = False
            Dim secondCount As Integer = 0
            While secondCount < spriteArray(secondObject).Count() And Not collide1
                Dim current2 = spriteArray(secondObject)(secondCount)
                If dist(current1.location, current2.location) Then
                    If intersects(current1, current2) Then
                        If TypeOf current1 Is ship Then                                           'Player with Enemy ship or Bullets
                            If TypeOf current2 Is bullet Then
                                If current2.shooter = 3 Then
                                    If current1.lives <> 0 Then current1.lives -= 1
                                    explosionArray.Add(New explosion(current1))
                                    current1.spawn()
                                    collide1 = True
                                End If
                            ElseIf TypeOf current2 Is enemyShip Then
                                current1.score += current2.level * 500
                                If current1.lives <> 0 Then current1.lives -= 1
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
                                    If current2.lives <> 0 Then current2.lives -= 1
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
                End If
                secondCount += 1
            End While
            If Not collide1 Then
                firstCount += 1
            End If
        End While
    End Sub  'Collision between everything
    Public Function intersects(list1, list2)
        For Each p In list1.drawPoints
            If PointInPolygon(list2.drawPoints, p.x, p.Y) Then
                Return True
            End If
        Next
        Return False
    End Function                               'Collision Check
End Module

Module shipActions
    Public Sub shoot(ship)
        Dim bulletNum As Integer = 0
        For Each bul As bullet In spriteArray(3) 'Check number of bullets
            If bul.shooter = ship.player Then bulletNum += 1
        Next
        If bulletNum < bulLimit And ship.shootEnable Then
            spriteArray(3).Add(New bullet(ship))
            'If sound Then My.Computer.Audio.Play(My.Resources.fire, AudioPlayMode.Background) 'Shoot sound
        End If
    End Sub             'Ship shooting
    Public Sub thrust(ship As ship)
        ship.xVelocity += Sin(2 * Math.PI * (ship.angle / 360)) * 0.9
        ship.yVelocity -= Cos(2 * Math.PI * (ship.angle / 360)) * 0.9
        ship.Image = My.Resources.shipThrust
    End Sub    'Ship thrusting
End Module

Module drawing
    Public Sub livesDraw(e As PaintEventArgs)
        Dim num As Integer = 1
        While num < spriteArray(1)(0).lives
            e.Graphics.DrawImage(lifeImage, num * 35 + 180, 15)
            num += 1
        End While
        If coop Then
            num = 1
            While num < spriteArray(1)(1).lives
                e.Graphics.DrawImage(lifeImage, menu.Width - (num * 35 + 240), 15)
                num += 1
            End While
        End If
    End Sub                                 'Draw ship's lives
    Public Sub spriteDraw(e As PaintEventArgs)
        For Each arr As Object In spriteArray
            For Each obj As Object In arr : obj.draw(e) : Next
        Next
    End Sub                                'Draw every sprite (player, asteroids, bullets, enemies)
    Public Sub explosionsDraw(e As PaintEventArgs)
        For Each obj As Object In explosionArray
            obj.draw(e)
        Next
    End Sub                            'Draw explosions
    Public Sub drawRotateImage(image, angle, locationx, locationy, e)
        e.Graphics.TranslateTransform(locationx, locationy)
        e.Graphics.RotateTransform(angle)
        e.Graphics.DrawImage(image, CInt(-image.Width / 2), CInt(-image.Height / 2), image.Width, image.Height)
        e.Graphics.ResetTransform()
    End Sub         'Draw rotated image
End Module

Module checks
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
    End Sub        'Check if explosions are expired
    Public Sub gameOverCheck()
        If coop Then
            If spriteArray(1)(0).lives < 1 And spriteArray(1)(1).lives < 1 Then
                gamestate = "over"
                spriteArray(1)(0).shootEnable = False
                spriteArray(1)(1).shootEnable = False
            End If
        Else
            If spriteArray(1)(0).lives < 1 Then gamestate = "over" : spriteArray(1)(0).shootEnable = False
        End If
    End Sub         'Check if player is out of lives

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

    Private enemyTime As Integer = 0
    Private directionInterval As Integer
    Private shootInterval As Integer
    Public spawnInterval As Integer = 5000

    Public Sub enemyCheck()
        If spriteArray(2).Count <> 0 Then
            If enemyTime Mod directionInterval = 0 Then
                'change direction
            End If
            If enemyTime Mod shootInterval = 0 Then
                'shoot
            End If
        Else
            If enemyTime Mod spawnInterval = 0 Then
                'spawn
                enemyTime = 0
            End If
        End If
        enemyTime += 1
    End Sub
End Module

Module labelVisible
    Public Sub menuVisible(truFalse As Boolean)
        menu.title.Visible = truFalse
        menu.optionsButton.Visible = truFalse
        menu.playButton.Visible = truFalse
        menu.highscores.Visible = truFalse
    End Sub                         'Menu labels visible
    Public Sub pauseVisible(truFalse As Boolean)
        menu.pauseExit.Visible = truFalse
        menu.pauseResume.Visible = truFalse
        menu.pauseRestart.Visible = truFalse
    End Sub                        'Pause labels visible
    Public Sub scoreVisible(player1 As Boolean, player2 As Boolean)
        menu.player1Score.Visible = player1 : menu.player1Title.Visible = player1
        menu.player2Score.Visible = player2 : menu.player2Title.Visible = player2
    End Sub     'Score labels visible
    Public Sub highscoreVisible(truFalse As Boolean)
        menu.highScoreTitle.Visible = truFalse
        menu.highscoreBack.Visible = truFalse

        menu.highscore1.Visible = truFalse : menu.highscore2.Visible = truFalse
        menu.highscore3.Visible = truFalse : menu.highscore4.Visible = truFalse
        menu.highscore5.Visible = truFalse

        menu.round1.Visible = truFalse : menu.round2.Visible = truFalse
        menu.round3.Visible = truFalse : menu.round4.Visible = truFalse
        menu.round5.Visible = truFalse

        menu.name1.Visible = truFalse : menu.name2.Visible = truFalse
        menu.name3.Visible = truFalse : menu.name4.Visible = truFalse
        menu.name5.Visible = truFalse
    End Sub                    'Highscore labels visible
End Module

Module highscores
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

    Public allLetters As List(Of Char) = New List(Of Char) From {"A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "!", "?", "/", "#", "$"}

    Public Sub highscoreInit()
        Dim highscores As List(Of String) = New List(Of String) From {}
        Dim names As List(Of String) = New List(Of String) From {}
        Dim strPath As String = Path.GetDirectoryName(Environment.GetCommandLineArgs()(0))
        Dim fileName As String = "highscores.txt"
        Dim fullPath = Path.Combine(strPath, fileName)
        Try
            Dim lines() As String = File.ReadAllLines(fullPath)
        Catch ex As Exception
            Dim fs As FileStream = File.Create(fullPath)
            Dim tamp As Integer = 0
            While tamp < 5
                Dim info As Byte() = New UTF8Encoding(True).GetBytes("AAA 0" + vbNewLine)
                fs.Write(info, 0, info.Length)
                tamp += 1
            End While
            fs.Close()
        End Try
        Dim text() As String = File.ReadAllLines(fullPath)
        Dim temp As Integer = 0
        While temp < 5
            names.Add(text(temp).Split(" ")(0))
            highscores.Add(text(temp).Split(" ")(1))
            highscoreLabels(temp).Text = highscores(temp)
            nameLabels(temp).Text = names(temp)
            temp += 1
        End While

        temp = 0
        onboard = False
        endplace1 = 6
        endplace2 = 6
        While temp < highscores.Count
            If CInt(highscores(temp)) <= endScore1 Then
                If temp <> 0 Then
                    If CInt(highscores(temp - 1)) > endScore1 Then
                        onboard = True
                        highscores.Insert(temp, endScore1)
                        names.Insert(temp, "AAA")
                        endPlace1 = temp
                    End If
                Else
                    onboard = True
                    highscores.Insert(temp, endScore1)
                    names.Insert(temp, "AAA")
                    endPlace1 = temp
                End If
            End If
            temp += 1
        End While
        temp = 0
        If coop Then
            While temp < highscores.Count
                If CInt(highscores(temp)) <= endScore2 Then
                    If temp <> 0 Then
                        If CInt(highscores(temp - 1)) > endScore2 Then
                            onboard = True
                            highscores.Insert(temp, endScore2)
                            names.Insert(temp, "AAA")
                            endPlace2 = temp
                        End If
                    Else
                        onboard = True
                        highscores.Insert(temp, endScore2)
                        names.Insert(temp, "AAA")
                        endPlace2 = temp
                    End If
                End If
                temp += 1
            End While
            If endPlace1 = endPlace2 Then
                endPlace1 += 1
            End If
        End If
        temp = 0
        While temp < 5
            nameLabels(temp).Text = names(temp)
            highscoreLabels(temp).Text = highscores(temp)
            temp += 1
        End While
        letterPlace1 = 0
        letterPlace2 = 0
        letters1 = New List(Of Integer) From {0, 0, 0}
        letters2 = New List(Of Integer) From {0, 0, 0}
        letterCool1 = False
        letterCool2 = False
    End Sub

    Public Sub scoreRecord()
        If endPlace1 < 6 Then
            If GetAsyncKeyState(hotKeys("player1Shoot")) Then
                If Not letterCool1 Then letterPlace1 += 1
                letterCool1 = True
            ElseIf GetAsyncKeyState(hotKeys("player1Left")) Then
                If Not letterCool1 Then letters1(letterPlace1) -= 1
                letterCool1 = True
            ElseIf GetAsyncKeyState(hotKeys("player1Right")) Then
                If Not letterCool1 Then letters1(letterPlace1) += 1
                letterCool1 = True
            Else
                letterCool1 = False
            End If
            If letterPlace1 = 3 Then : endPlace1 = 6
            Else
                If letters1(letterPlace1) = -1 Then
                    letters1(letterPlace1) = allLetters.Count - 1
                ElseIf letters1(letterPlace1) = allLetters.count Then
                    letters1(letterPlace1) = 0
                End If
                nameLabels(endPlace1).Text = allLetters(letters1(0)) + allLetters(letters1(1)) + allLetters(letters1(2))

            End If
        End If
        If endPlace2 < 6 Then
            If GetAsyncKeyState(hotKeys("player2Shoot")) Then
                If Not letterCool2 Then letterPlace2 += 1
                letterCool2 = True
            ElseIf GetAsyncKeyState(hotKeys("player2Left")) Then
                If Not letterCool2 Then letters2(letterPlace2) -= 1
                letterCool2 = True
            ElseIf GetAsyncKeyState(hotKeys("player2Right")) Then
                If Not letterCool2 Then letters2(letterPlace2) += 1
                letterCool2 = True
            Else
                letterCool2 = False
            End If
            If letterPlace2 = 3 Then : endPlace2 = 6
            Else
                If letters2(letterPlace2) = -1 Then
                    letters2(letterPlace2) = allLetters.Count - 1
                ElseIf letters2(letterPlace2) = allLetters.Count Then
                    letters2(letterPlace2) = 0
                End If
                nameLabels(endPlace2).Text = allLetters(letters2(0)) + allLetters(letters2(1)) + allLetters(letters2(2))

            End If
        End If

    End Sub

    Public Sub highScoreRecord()
        Dim strPath As String = Path.GetDirectoryName(Environment.GetCommandLineArgs()(0))
        Dim fileName As String = "highscores.txt"
        Dim fullPath = Path.Combine(strPath, fileName)
        Dim fs As FileStream = File.Create(fullPath)
        Dim tamp As Integer = 0
        While tamp < 5
            Dim info As Byte() = New UTF8Encoding(True).GetBytes(nameLabels(tamp).Text + " " + highscoreLabels(tamp).Text + vbNewLine)
            fs.Write(info, 0, info.Length)
            tamp += 1
        End While
        fs.Close()
    End Sub
End Module

Module sound
    'Sound Variables
    Public soundCounter As Integer = 0      'Counter for time between high and low sound
    Public Const soundLimit As Integer = 70 'Interval between high and low
    Public highSound As Boolean = True      'Whether it is high sound's turn
    Public level As Integer = 1             'NOTE: RESET WHEN GAMELOAD

    Public Sub soundAll()
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
    End Sub
End Module

Module other
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
End Module
