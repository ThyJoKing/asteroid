Public Class coins
    Private Sub coins_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Top = My.Computer.Screen.Bounds.Height / 2 - 450 + Me.Height / 2
        Me.Left = My.Computer.Screen.Bounds.Width / 2 - 450 - Me.Width
    End Sub

    Private Sub coinslot_Click(sender As Object, e As EventArgs) Handles coinslot.Click
        If MsgBox("Are you sure you want to quit?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            End
        End If
    End Sub
End Class