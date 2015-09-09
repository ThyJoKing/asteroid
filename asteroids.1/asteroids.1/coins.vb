Public Class coins
    Private coinDrag As Boolean = False
    Private coinsNum As Integer = 0
    Private smallCoin As Image

    Private Sub coins_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Top = My.Computer.Screen.Bounds.Height / 2 - 450 + Me.Height / 2
        Me.Left = My.Computer.Screen.Bounds.Width / 2 - 450 - Me.Width
        smallCoin = ResizeImage(My.Resources.coin, New Size(100, 100))
    End Sub

    Private Sub coinslot_Click(sender As Object, e As EventArgs) Handles coinslot.Click
        If MsgBox("Are you sure you want to quit?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            End
        End If
    End Sub

    Private Sub coinTimer_Tick(sender As Object, e As EventArgs) Handles coinTimer.Tick
        Me.Invalidate()
    End Sub

    Private Sub coinPile_Click(sender As Object, e As EventArgs) Handles coinPile.MouseDown
        coinDrag = True
    End Sub

    Private Sub coins_MouseUp(sender As Object, e As MouseEventArgs) Handles coinPile.MouseUp
        If MousePosition.X > coinslot.Left + Me.Left And MousePosition.X < coinslot.Right + Me.Left And MousePosition.Y < coinslot.Bottom + Me.Top And MousePosition.Y > coinslot.Top + Me.Top Then
            coinsNum += 1
            mainWindow.coinLabel.Text = "Coin(s):" + Str(coinsNum)
        End If
        coinDrag = False
    End Sub

    Private Sub coins_Paint(sender As Object, e As PaintEventArgs) Handles Me.Paint
        If coinDrag Then
            drawRotateImage(smallCoin, 90, MousePosition.X - Me.Left, MousePosition.Y - Me.Top + smallCoin.Height / 2, e)
        End If
    End Sub
End Class