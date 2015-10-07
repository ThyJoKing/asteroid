Imports System.IO

Public Class soundSettings
    Dim playing As Boolean
    Private brahmsSong As AudioFile
    Private hydeSong As AudioFile
    Private ruinsSong As AudioFile
    Private abbaSong As AudioFile

    Private Sub soundScreen_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then
            End
        End If
    End Sub

    Private Sub soundScreen_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        brahmsSong = New AudioFile(Path.Combine(initialPath, "Brahmas.mp3"))
        hydeSong = New AudioFile(Path.Combine(initialPath, "Hyde.mp3"))
        ruinsSong = New AudioFile(Path.Combine(initialPath, "Ruins.mp3"))
        abbaSong = New AudioFile(Path.Combine(initialPath, "Abba.mp3"))

        lblBack.Font = New Font(hyper, 27)
        lblselectedSong.Font = New Font(hyper, 27)
        lblSong1.Font = New Font(hyper, 27)
        lblSong2.Font = New Font(hyper, 27)
        lblsong3.Font = New Font(hyper, 27)
        lblsong4.Font = New Font(hyper, 27)
    End Sub

    Private Sub lblBack_MouseClick(sender As Object, e As MouseEventArgs) Handles lblBack.MouseClick
        Me.Hide()
        settingsMenu.Show()
    End Sub

    Private Sub lblBack_MouseHover(sender As Object, e As EventArgs) Handles lblBack.MouseHover
        lblBack.Font = New Font(hyper, 30)
    End Sub
    Private Sub lblBack_MouseLeave(sender As Object, e As EventArgs) Handles lblBack.MouseLeave
        lblBack.Font = New Font(hyper, 27)
    End Sub

    Private Sub lblSong1_Click(sender As Object, e As EventArgs) Handles lblSong1.Click
        lblselectedSong.Text = lblSong1.Text
    End Sub

    Private Sub lblSong2_Click(sender As Object, e As EventArgs) Handles lblSong2.Click
        lblselectedSong.Text = lblSong2.Text
    End Sub

    Private Sub lblsong3_Click(sender As Object, e As EventArgs) Handles lblsong3.Click
        lblselectedSong.Text = lblsong3.Text
    End Sub

    Private Sub lblsong4_Click(sender As Object, e As EventArgs) Handles lblsong4.Click
        lblselectedSong.Text = lblsong4.Text
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        brahmsSong.Close()
        hydeSong.Close()
        ruinsSong.Close()
        abbaSong.Close()
        If lblselectedSong.Text = "Brahms" Then
            brahmsSong.Play()
        ElseIf lblselectedSong.Text = "Hyde" Then
            hydeSong.Play()
        ElseIf lblselectedSong.Text = "Glorious Ruins" Then
            ruinsSong.Play()
        ElseIf lblselectedSong.Text = "Abba" Then
            abbaSong.Play()
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If lblselectedSong.Text = "Brahms" Then
            brahmsSong.Stop()
            brahmsSong.Close()
        ElseIf lblselectedSong.Text = "Hyde" Then
            hydeSong.Stop()
            hydeSong.Close()
        ElseIf lblselectedSong.Text = "Glorious Ruins" Then
            ruinsSong.Stop()
            ruinsSong.Close()
        ElseIf lblselectedSong.Text = "Abba" Then
            abbaSong.Stop()
            abbaSong.Close()
        End If
    End Sub

    Private Sub lblSong1_MouseHover(sender As Object, e As EventArgs) Handles lblSong1.MouseHover
        lblSong1.Font = New Font(hyper, 27, FontStyle.Underline)
    End Sub
    Private Sub lblSong2_MouseHover(sender As Object, e As EventArgs) Handles lblSong2.MouseHover
        lblSong2.Font = New Font(hyper, 27, FontStyle.Underline)
    End Sub
    Private Sub lblsong3_MouseHover(sender As Object, e As EventArgs) Handles lblsong3.MouseHover
        lblsong3.Font = New Font(hyper, 27, FontStyle.Underline)
    End Sub
    Private Sub lblsong4_MouseHover(sender As Object, e As EventArgs) Handles lblsong4.MouseHover
        lblsong4.Font = New Font(hyper, 27, FontStyle.Underline)
    End Sub


    Private Sub lblSong1_MouseLeave(sender As Object, e As EventArgs) Handles lblSong1.MouseLeave
        lblSong1.Font = New Font(hyper, 27)
    End Sub
    Private Sub lblSong2_MouseLeave(sender As Object, e As EventArgs) Handles lblSong2.MouseLeave
        lblSong2.Font = New Font(hyper, 27)
    End Sub
    Private Sub lblSong3_MouseLeave(sender As Object, e As EventArgs) Handles lblsong3.MouseLeave
        lblsong3.Font = New Font(hyper, 27)
    End Sub
    Private Sub lblSong4_MouseLeave(sender As Object, e As EventArgs) Handles lblsong4.MouseLeave
        lblsong4.Font = New Font(hyper, 27)
    End Sub
End Class