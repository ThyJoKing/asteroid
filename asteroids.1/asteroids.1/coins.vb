Public Class coins
    Dim coinDrag As Boolean = False

    Private Sub coins_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Top = My.Computer.Screen.Bounds.Height / 2 - 450 + Me.Height / 2
        Me.Left = My.Computer.Screen.Bounds.Width / 2 - 450 - Me.Width
    End Sub

    Private Sub coinslot_Click(sender As Object, e As EventArgs) Handles coinslot.Click
        If MsgBox("Are you sure you want to quit?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            End
        End If
    End Sub

    Private Sub coinTimer_Tick(sender As Object, e As EventArgs) Handles coinTimer.Tick
        If coinDrag Then

        End If
    End Sub

    Private Sub coinPile_Click(sender As Object, e As EventArgs) Handles coinPile.MouseDown
        coinDrag = True
        'actualCoin.Visible = True
    End Sub

    Private Sub coins_MouseUp(sender As Object, e As MouseEventArgs) Handles coinPile.MouseUp
        If MousePosition.X > coinslot.Left And MousePosition.X < coinslot.Right And MousePosition.Y > coinslot.Top And MousePosition.Y < coinslot.Bottom Then
            coinDrag = False
            'actualCoin.Visible = False
        End If
    End Sub
End Class