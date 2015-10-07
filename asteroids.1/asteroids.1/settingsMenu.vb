Imports System
Imports System.Windows.Forms
Imports System.Runtime.InteropServices

Public Class settingsMenu
    'coop - True/False
    'sounds - slider 0-100 + mute
    'hot keys - player movements, 1 and 2 and pause
    'sensitivity - slider 0-10
    Private Const VolUp As Integer = &HA0000
    Private Const VolDn As Integer = &H90000
    Private Const MsgNo As Integer = &H319
    Dim sliderMove As Boolean
    Dim j As Integer
    Dim mpx As Point
    Dim focusHotKey As String
    Dim hotKeysPrimitive As New Dictionary(Of String, String)
    Dim awaitingKey As Boolean
    Dim pressedKey As String
    Dim existingKeys As New List(Of Keys)
    Dim validKey As Boolean
    Dim whichPlayer As Integer
    Dim whichLabel As String
    Dim sliderMove2 As Boolean
    Dim oldSoundPercentage As Double
    Dim soundPercentage As Double
    Dim sensePercentage As Double
    Dim message As String



    Public Declare Function getasynckeystate Lib "user.32.dll" (ByVal vkey As Int32) As UShort '

    'Public Shared Function SendMessageW(ByVal hWnd As IntPtr, _
    '         ByVal Msg As Integer, ByVal wParam As IntPtr, _
    '         ByVal lParam As IntPtr) As IntPtr
    'End Function
    Declare Function SendMessageW Lib "user32" (ByVal hWnd As IntPtr,
                                                                 ByVal Msg As Integer,
                                                                 ByVal wParam As IntPtr,
                                                                 ByVal lParam As IntPtr) As IntPtr

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        soundPercentage = 0.5
        pbSlider.Top = 135
        tmrSlider.Enabled = True
        init()
        hotKeysInit2()
    End Sub
    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles pbSlider2.MouseDown
        sliderMove2 = True
    End Sub
    Private Sub pbSlider2_MouseUp(sender As Object, e As MouseEventArgs) Handles pbSlider2.MouseUp
        sliderMove2 = False
    End Sub

    Private Sub pbSlider_MouseDown(sender As Object, e As MouseEventArgs) Handles pbSlider.MouseDown
        sliderMove = True
    End Sub
    Private Sub pbSlider_MouseUp(sender As Object, e As MouseEventArgs) Handles pbSlider.MouseUp
        sliderMove = False

    End Sub

    Private Sub sysTimer_Tick(sender As Object, e As EventArgs) Handles tmrSlider.Tick
        'managing sliders for volume and sensitivity
        mpx = MousePosition
        If sliderMove = True Then
            'txtMousePos.Text = mpx.ToString
            If mpx.X < 216 Then
                pbSlider.Left = 216
            ElseIf mpx.X > 1131 Then
                pbSlider.Left = 1131
            Else
                pbSlider.Left = mpx.X
            End If
            oldSoundPercentage = soundPercentage
            soundPercentage = (pbSlider.Left - 216)
            soundPercentage = soundPercentage / 915
            soundPercentage = Math.Round(soundPercentage * 100)
            ' lblVolume.Text = Str("Volume " + Str(CInt(soundPercentage * 100)))
            If soundPercentage Mod 2 <> 0 Then
                soundPercentage += 1
            End If
            message = soundPercentage
            If message > oldSoundPercentage Then
                changeVolume(True)
            ElseIf message <> oldSoundPercentage
                changeVolume(False)
            End If
            lblVolume.Text = "Volume:" + message + "%"
            soundVolume = message


        ElseIf sliderMove2 = True Then
            If mpx.X < 216 Then
                pbSlider2.Left = 216
            ElseIf mpx.X > 1131 Then
                pbSlider2.Left = 1131
            Else
                pbSlider2.Left = mpx.X
            End If
            sensePercentage = (pbSlider2.Left - 216) / 915
            sensePercentage = CInt(sensePercentage * 100)
            lblSensitivity.Text = "Sensitivity:" + Str(sensePercentage) + "%"
            sensitivity = Math.Ceiling(sensePercentage * 0.16)
        End If

    End Sub

    Public Sub changeVolume(ByVal Up As Boolean)
        If Up Then
            For a = 0 To Math.Round(soundPercentage - oldSoundPercentage) / 2
                SendMessageW(Me.Handle, MsgNo, Me.Handle, New IntPtr(VolUp))
            Next
        Else
            For a = 0 To Math.Round(oldSoundPercentage - soundPercentage) / 2
                SendMessageW(Me.Handle, MsgNo, Me.Handle, New IntPtr(VolDn))
            Next
        End If

    End Sub

    Private Sub pbCoopToggle_Click(sender As Object, e As EventArgs) Handles pbCoopToggle.Click
        If coop = False Then
            coop = True
            pbCoopToggle.Left = 346
        Else
            coop = False
            pbCoopToggle.Left = 201
        End If
    End Sub

    Public Sub init()
        whichPlayer = 1
        coop = False
        soundVolume = 50
        'fonts
        lblPlayerSelected.Font = New Font(hyper, 30)
        lblCoop.Font = New Font(hyper, 20)
        lblOffCoop.Font = New Font(hyper, 30)
        lblOnCoop.Font = New Font(hyper, 30)
        lbl1.Font = New Font(hyper, 20)
        lbl2.Font = New Font(hyper, 20)
        lbl3.Font = New Font(hyper, 20)
        lbl4.Font = New Font(hyper, 20)
        lbl5.Font = New Font(hyper, 20)

        lblHotUp.Font = New Font(hyper, 20)
        lblHotHyperSpace.Font = New Font(hyper, 20)
        lblHotLeft.Font = New Font(hyper, 20)
        lblHotRight.Font = New Font(hyper, 20)
        lblHotShoot.Font = New Font(hyper, 20)

        lblVolume.Font = New Font(hyper, 30)
        lblSensitivity.Font = New Font(hyper, 30)
        lblSoundMenu.Font = New Font(hyper, 30)

        backButton.Font = New Font(hyper, 30)

    End Sub

    Private Sub lblHotUp_MouseClick(sender As Object, e As MouseEventArgs) Handles lblHotUp.MouseClick
        whichLabel = "up"
        If whichPlayer = 1 Then
            focusHotKey = "player1Forward"
        Else
            focusHotKey = "player2Forward"
        End If
        awaitingKey = True
        changeBack()
        lblHotUp.Text = "Please enter a key"
    End Sub
    Private Sub lblHotHyperSpace_MouseClick(sender As Object, e As MouseEventArgs) Handles lblHotHyperSpace.MouseClick
        whichLabel = "hyper"
        If whichPlayer = 1 Then
            focusHotKey = "player1Hyperspace"
        Else
            focusHotKey = "player2Hyperspace"
        End If
        awaitingKey = True
        changeBack()
        lblHotHyperSpace.Text = "Please enter a key"
    End Sub
    Private Sub lblHotLeft_MouseClick(sender As Object, e As MouseEventArgs) Handles lblHotLeft.MouseClick
        whichLabel = "left"
        If whichPlayer = 1 Then
            focusHotKey = "player1Left"
        Else
            focusHotKey = "player2Left"
        End If
        awaitingKey = True
        changeBack()
        lblHotLeft.Text = "Please enter a key"
    End Sub
    Private Sub lblHotRight_MouseClick(sender As Object, e As MouseEventArgs) Handles lblHotRight.MouseClick
        whichLabel = "right"
        If whichPlayer = 1 Then
            focusHotKey = "player1Right"
        Else
            focusHotKey = "player2Right"
        End If
        awaitingKey = True
        changeBack()
        lblHotRight.Text = "Please enter a key"
    End Sub
    Private Sub lblHotShoot_MouseClick(sender As Object, e As MouseEventArgs) Handles lblHotShoot.MouseClick
        whichLabel = "shoot"
        If whichPlayer = 1 Then
            focusHotKey = "player1Shoot"
        Else
            focusHotKey = "Player2Shoot"
        End If
        awaitingKey = True
        changeBack()
        lblHotShoot.Text = "Please enter a key"
    End Sub

    Private Sub changeBack()
        If whichPlayer = 1 Then
            lblHotRight.Text = hardCodedOtherKeys("player1Right") + "           "
            lblHotHyperSpace.Text = hardCodedOtherKeys("player1Hyperspace") + "           "
            lblHotLeft.Text = hardCodedOtherKeys("player1Left") + "           "
            lblHotShoot.Text = hardCodedOtherKeys("player1Shoot") + "           "
            lblHotUp.Text = hardCodedOtherKeys("player1Forward") + "           "
        ElseIf whichPlayer = 2 Then
            lblHotRight.Text = hardCodedOtherKeys("player2Right") + "           "
            lblHotHyperSpace.Text = hardCodedOtherKeys("player2Hyperspace") + "           "
            lblHotLeft.Text = hardCodedOtherKeys("player2Left") + "           "
            lblHotShoot.Text = hardCodedOtherKeys("player2Shoot") + "           "
            lblHotUp.Text = hardCodedOtherKeys("player2Forward") + "           "
        End If
    End Sub

    Private Function hardCodedOtherKeys(whichKey)
        Dim Showkeyplayer As String

        Select Case hotKeys(whichKey)
            Case Keys.Right
                Showkeyplayer = "right"
            Case Keys.Left
                Showkeyplayer = "left"
            Case Keys.Up
                Showkeyplayer = "Up"
            Case Keys.RShiftKey
                Showkeyplayer = "Right shift"
            Case Keys.LShiftKey
                Showkeyplayer = "Left shift"
            Case Keys.Space
                Showkeyplayer = "Space"
            Case Else
                Showkeyplayer = ChrW(hotKeys(whichKey))
        End Select
        Return Showkeyplayer

    End Function

    Private Sub Form1_KeyPress(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then
            End
        End If

        If awaitingKey = True Then
            pressedKey = e.KeyData
            checkHotKeyExist()
            If validKey = True Then
                '  MsgBox(focusHotKey + " " + pressedKey)
                hotKeys(focusHotKey) = pressedKey 'adding the pressedKey into the dictionary
                awaitingKey = False
                focusHotKey = "none"
            Else
                MsgBox("Sorry, that key is already used.")

            End If
            changeBack() 'setting the keys back to what they should be now 
        End If
    End Sub

    Private Sub checkHotKeyExist()
        existingKeys.Clear()
        For Each value In hotKeys.Values
            existingKeys.Add(value)
        Next
        If existingKeys.Contains(pressedKey) Then
            validKey = False
        Else
            validKey = True
        End If

    End Sub

    Private Sub hotKeysInit2()
        hotKeysPrimitive.Add("left1", "a")
        hotKeysPrimitive.Add("right1", "d")
        hotKeysPrimitive.Add("up1", "w")
        hotKeysPrimitive.Add("shoot", "space bar")
        hotKeysPrimitive.Add("hyper", "Lshift")
        hotKeysPrimitive.Add("left2", "r")
    End Sub

    Private Sub lblPlayerSelected_MouseDown(sender As Object, e As MouseEventArgs) Handles lblPlayerSelected.MouseDown

        If whichPlayer = 1 Then
            whichPlayer = 2
            lblPlayerSelected.Text = "player 2 selected"
        Else
            whichPlayer = 1
            lblPlayerSelected.Text = "player 1 selected"
        End If
        changeBack()
    End Sub

    Private Sub lblPlayerSelected_MouseHover(sender As Object, e As EventArgs) Handles lblPlayerSelected.MouseHover
        lblPlayerSelected.Font = New Font(hyper, 30, FontStyle.Underline)
    End Sub

    Private Sub lblPlayerSelected_MouseLeave(sender As Object, e As EventArgs) Handles lblPlayerSelected.MouseLeave
        lblPlayerSelected.Font = New Font(hyper, 30, FontStyle.Regular)
    End Sub

    Private Sub lblSoundMenu_Click(sender As Object, e As EventArgs) Handles lblSoundMenu.Click
        soundSettings.Show()
        Me.Hide()
    End Sub

    Private Sub lblSoundMenu_MouseHover(sender As Object, e As EventArgs) Handles lblSoundMenu.MouseHover
        lblSoundMenu.Font = New Font(hyper, 30, FontStyle.Underline)
    End Sub

    Private Sub lblSoundMenu_MouseLeave(sender As Object, e As EventArgs) Handles lblSoundMenu.MouseLeave
        lblSoundMenu.Font = New Font(hyper, 30)
    End Sub

    Private Sub backButton_Click(sender As Object, e As EventArgs) Handles backButton.Click
        Me.Hide()
        coins.Show()
        mainWindow.Show()
    End Sub
End Class
