﻿Imports System.Math
Imports System.Drawing.Drawing2D

'NOTES:
'Remove bounds after ranGen
'Remove cirle completely

Public Class ship
    Public Property Image As Image
    Public Property location As PointF
    Public Property locationx As Double : Public Property locationy As Double
    Public Property angle As Integer
    Public Property xVelocity As Double : Public Property yVelocity As Double
    Public Property inHyperspace As Boolean = False
    Public Property hyperspaceCounter As Integer
    Public Property hyperspaceEnable As Boolean = True
    Public Property shootEnable As Boolean = True
    Public Property coop As Boolean
    Public Property player As Integer
    Public Property bulletCool As Boolean = False
    Public Property points As List(Of PointF) = New List(Of PointF) From {}
    Public Property drawPoints As PointF()
    Public Property lives As Integer = 4
    Public Property score As Integer = 0
    Public Property extraLives As Integer = 10000

    Public Sub New(cop As Boolean, playa As Integer)
        coop = cop
        player = playa
        spawn()
    End Sub
    Public Sub spawn()
        If lives <> 0 Then
            If coop = True Then
                If player = 1 Then locationx = menu.Width / 2 - 50 Else locationx = menu.Width / 2 + 50
            Else
                locationx = menu.Width / 2
            End If
            locationy = menu.Height / 2
            Location = New Point(locationx, locationy)
            xVelocity = 0 : yVelocity = 0
            Angle = 0
            Image = My.Resources.ship
        Else
            Location = New Point(-100, -100)
        End If
    End Sub
    Public Sub move()
        If Not inHyperspace And lives <> 0 Then
            yVelocity *= 0.97 : xVelocity *= 0.97
            locationx += xVelocity : locationy += yVelocity
            If locationx < -Image.Width / 2 Then locationx = menu.Width + Image.Width / 2 - 1
            If locationx > menu.Width + Image.Width / 2 Then locationx = -Image.Width / 2
            If locationy < -Image.Width / 2 Then locationy = menu.Height + Image.Width / 2
            If locationy > menu.Height + Image.Width / 2 + 1 Then locationy = -Image.Width / 2
            Location = New Point(locationx, locationy)
        End If
        scoreCheck()
    End Sub
    Public Sub Draw(e As PaintEventArgs)
        If lives <> 0 Then
            e.Graphics.TranslateTransform(Location.X, Location.Y)
            e.Graphics.RotateTransform(Angle)
            e.Graphics.DrawImage(Image, CInt(-Image.Width / 2), CInt(-Image.Height / 2), Image.Width, Image.Height)
            e.Graphics.ResetTransform()
            points.Clear()
            points.Add(New PointF(Sin(2 * Math.PI * (angle / 360)) * 30 + location.X, -Cos(2 * Math.PI * (angle / 360)) * 30 + location.Y))
            points.Add(New PointF(Sin(2 * Math.PI * ((angle - 70) / 360)) * 10 + location.X, -Cos(2 * Math.PI * ((angle - 70) / 360)) * 10 + location.Y))
            points.Add(New PointF(Sin(2 * Math.PI * ((angle - 140) / 360)) * 35 + location.X, -Cos(2 * Math.PI * ((angle - 140) / 360)) * 35 + location.Y))
            points.Add(New PointF(Sin(2 * Math.PI * ((angle + 140) / 360)) * 35 + location.X, -Cos(2 * Math.PI * ((angle + 140) / 360)) * 35 + location.Y))
            points.Add(New PointF(Sin(2 * Math.PI * ((angle + 70) / 360)) * 10 + location.X, -Cos(2 * Math.PI * ((angle + 70) / 360)) * 10 + location.Y))

            drawPoints = points.ToArray
        Else
            drawPoints = {New Point(-100, -100)}
        End If
    End Sub
    Public Sub hyperspaceStart()
        If hyperspaceEnable And lives <> 0 Then
            inHyperspace = True
            hyperspaceCounter = 0
            Location = New Point(-200, -200)
            xVelocity = 0 : yVelocity = 0
        End If
    End Sub
    Public Sub hyperspace()
        If hyperspaceEnable Then
            locationx = Rnd() * menu.Width : locationy = Rnd() * menu.Height
            Location = New Point(locationx, locationy)
            inHyperspace = False
            hyperspaceCounter = 0
        End If
    End Sub
    Public Sub scoreCheck()
        If score > extraLives Then
            lives += 1
            extraLives += 10000
        End If
    End Sub
    Public ReadOnly Property Bounds As List(Of PointF)
        Get
            Return New List(Of PointF) From {New PointF(Sin(2 * Math.PI * (Angle / 360)) * 30 + Location.X, -Cos(2 * Math.PI * (Angle / 360)) * 30 + Location.Y),
                New PointF(Sin(2 * Math.PI * ((Angle - 140) / 360)) * 35 + Location.X, -Cos(2 * Math.PI * ((Angle - 140) / 360)) * 35 + Location.Y),
                New PointF(Sin(2 * Math.PI * ((Angle + 140) / 360)) * 35 + Location.X, -Cos(2 * Math.PI * ((Angle + 140) / 360)) * 35 + Location.Y)}
        End Get
    End Property
End Class

Public Class asteroid
    Public Property location As PointF
    Public Property locationx As Double : Public Property locationy As Double
    Public Property level As Integer
    Public Property xVelocity As Double : Public Property yvelocity As Double
    Public Property radius As Integer
    Public Property tempList As List(Of PointF) = New List(Of PointF) From {}
    Public Property points As List(Of PointF) = New List(Of PointF) From {}
    Public Property drawPoints As PointF()

    Public Sub New(size As Integer, ast As asteroid)
        level = size
        randomGenerate(size)
        radius = minRadius
        If ast Is Nothing Then spawn() Else split(ast)
        For Each P As PointF In tempList
            points.Add(New Point(P.X + location.X, P.Y + location.Y))
        Next
        drawPoints = points.ToArray()
    End Sub
    Public Sub spawn()
        Dim done As Boolean = False
        Do Until done
            locationx = Rnd() * menu.Width : locationy = Rnd() * menu.Height
            done = True
            If gamestate = "play" Then
                Dim shi1 As ship = spriteArray(1)(0)
                done = Not ((locationx > shi1.locationx - shipBorders And locationx < shi1.locationx + shipBorders) And _
                (locationy > shi1.locationy - shipBorders And locationy < shi1.locationy + shipBorders))
                If coop And done Then
                    Dim shi2 As ship = spriteArray(1)(1)
                    done = Not ((locationx > shi2.locationx - shipBorders And locationx < shi2.locationx + shipBorders) And _
                    (locationy > shi2.locationy - shipBorders And locationy < shi2.locationy + shipBorders))
                End If
            End If
        Loop
        location = New PointF(locationx, locationy)
        xVelocity = 0 : yvelocity = 0
        While Not (xVelocity <> 0 And yvelocity <> 0)
            xVelocity = Round(Rnd() * 10 - 5) : yvelocity = Round(Rnd() * 10 - 5)
        End While
        xVelocity *= level : yvelocity *= level
        If velocityNo Then
            xVelocity = 0 : yvelocity = 0
        End If
    End Sub
    Public Sub split(ast As asteroid)
        locationx = ast.locationx + Rnd() * 2 * ast.radius - ast.radius : locationy = ast.locationy + Rnd() * ast.radius * 2 - ast.radius
        location = New Point(locationx, locationy)
        xVelocity = 0 : yvelocity = 0
        While Not (xVelocity > 2 Or xVelocity < -2 And yvelocity > 2 Or yvelocity < -2)
            xVelocity = ast.xVelocity + (Rnd() * 5 - 2) : yvelocity = ast.yvelocity + (Rnd() * 5 - 2)
        End While
        If velocityNo Then
            xVelocity = 0 : yvelocity = 0
        End If
    End Sub
    Public Sub move()
        locationx += xVelocity : locationy += yvelocity
        If locationx < -radius Then locationx = menu.Width + radius - 1
        If locationx > menu.Width + radius Then locationx = -radius
        If locationy < -radius Then locationy = menu.Height + radius - 1
        If locationy > menu.Height + radius Then locationy = -radius
        location = New Point(locationx, locationy)
    End Sub
    Public Sub Draw(e As PaintEventArgs)
        points.Clear()
        For Each P As PointF In tempList
            points.Add(New Point(P.X + location.X, P.Y + location.Y))
        Next
        drawPoints = points.ToArray()
        e.Graphics.DrawPolygon(Pens.White, drawPoints)
    End Sub
    Public Sub randomGenerate(size)
        Dim ang As Double = 0
        Dim angVary As Integer
        Dim angRadians As Double
        Dim angFinal As Double
        Dim ranRadius As Integer
        Dim tempX As Double
        Dim tempY As Double

        While ang < 2 * PI
            angVary = Rnd() * (maxVary - minVary) + minVary
            angRadians = (2 * PI / gran) * angVary / 100
            angFinal = ang + angRadians - PI / gran
            ranRadius = Rnd() * (maxRadius * (4 - size) - minRadius * (4 - size)) + minRadius * (4 - size)
            tempX = Sin(angFinal) * ranRadius
            tempY = -Cos(angFinal) * ranRadius
            tempList.Add(New Point(tempX, tempY))
            ang += 2 * PI / gran
        End While
        For Each P As PointF In tempList
            points.Add(New Point(P.X + location.X, P.Y + location.Y))
        Next
    End Sub
End Class

Public Class bullet
    Public Property image As Image
    Public Property location As PointF
    Public Property xVelocity As Integer
    Public Property yVelocity As Integer
    Public Property bulletTime As Integer
    Public Property shooter As Integer
    Public Property drawPoints As PointF()

    Public Sub move()
        location = New Point(location.X + Xvelocity, location.Y + Yvelocity)
        If location.X < -20 Then location = New Point(menu.Width + 15, location.Y)
        If location.X > menu.Width + 15 Then location = New Point(-20, location.Y)
        If location.Y < -20 Then location = New Point(location.X, menu.Height + 15)
        If location.Y > menu.Height + 15 Then location = New Point(location.X, -20)
        bulletTime += 1
        drawPoints = {New Point(location.X, location.Y)}
    End Sub
    Public Sub Draw(e As PaintEventArgs)
        e.Graphics.DrawImage(image, location)
    End Sub
    Public ReadOnly Property Bounds As List(Of PointF)
        Get
            Return New List(Of PointF) From {location}
        End Get
    End Property
End Class

Public Class enemyShip
    Public Property image As Image
    Public Property location As PointF
    Public Property locationx As Double : Public Property locationy As Double
    Public Property xVelocity As Double : Public Property yvelocity As Double
    Public Property angle As Integer
    Public Property drawPoints As PointF()
    Public Property level As Integer
    Public Property player As Integer = 3

    Public Sub New(size As Integer)
        level = size
        image = My.Resources.enemyShip1
        image = ResizeImage(image, New Size((1 / level) * image.Width, (1 / level) * image.Height))
        locationx = (Rnd() * 2 - 1) * menu.Width : locationy = Rnd() * menu.Height
        angle = Rnd() * 178 + 1
        If locationx = 0 Then
            angle += 180
        End If
        xVelocity = Sin(2 * Math.PI * (angle / 360)) * 5 : yvelocity = -Cos(2 * Math.PI * (angle / 360)) * 5
    End Sub
    Public Sub move()
        locationx += xVelocity : locationy += yvelocity
        If locationx < -image.Width / 2 Then locationx = menu.Width + image.Width / 2 - 1
        If locationx > menu.Width + image.Width / 2 Then locationx = -image.Width / 2
        If locationy < -image.Width / 2 Then locationy = menu.Height + image.Width / 2
        If locationy > menu.Height + image.Width / 2 + 1 Then locationy = -image.Width / 2
        location = New Point(locationx, locationy)
    End Sub
    Public Sub Draw(e As PaintEventArgs)
        e.Graphics.DrawImage(image, location)
        If hitbox Then
            drawPoints = {New Point(locationx + 60 * (1 / level), locationy + 20 * (1 / level)),
                      New Point(locationx + 20 * (1 / level), locationy + 60 * (1 / level)),
                      New Point(locationx + 50 * (1 / level), locationy + 80 * (1 / level)),
                      New Point(locationx + 100 * (1 / level), locationy + 80 * (1 / level)),
                      New Point(locationx + 130 * (1 / level), locationy + 60 * (1 / level)),
                      New Point(locationx + 85 * (1 / level), locationy + 20 * (1 / level))}
            e.Graphics.DrawPolygon(Pens.Red, drawPoints)
        End If
    End Sub
End Class
