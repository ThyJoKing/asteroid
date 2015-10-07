Public Class soundProperties
    Private properties As New List(Of Boolean)(New Boolean() {False, False})

    Public Sub change(number, flag)
        properties(number) = flag
    End Sub

    Function checking()
        MsgBox(properties)
        Return properties
    End Function
End Class
