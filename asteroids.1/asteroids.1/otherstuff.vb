Imports System.Drawing.Drawing2D
Imports System.Math

Module otherStuff
    Public Const boundary As Integer = 300
    Public Function dist(point1, point2)
        Return (point1.x - point2.x) ^ 2 + (point1.y - point2.y) ^ 2 < boundary ^ 2
    End Function
    Public Function ResizeImage(ByVal image As Image, ByVal size As Size, Optional ByVal preserveAspectRatio As Boolean = True) As Image
        Dim newWidth As Integer
        Dim newHeight As Integer
        If preserveAspectRatio Then
            Dim originalWidth As Integer = image.Width
            Dim originalHeight As Integer = image.Height
            Dim percentWidth As Single = CSng(size.Width) / CSng(originalWidth)
            Dim percentHeight As Single = CSng(size.Height) / CSng(originalHeight)
            Dim percent As Single = If(percentHeight < percentWidth, percentHeight, percentWidth)
            newWidth = CInt(originalWidth * percent)
            newHeight = CInt(originalHeight * percent)
        Else
            newWidth = size.Width
            newHeight = size.Height
        End If
        Dim newImage As Image = New Bitmap(newWidth, newHeight)
        Using graphicsHandle As Graphics = Graphics.FromImage(newImage)
            graphicsHandle.InterpolationMode = InterpolationMode.HighQualityBicubic
            graphicsHandle.DrawImage(image, 0, 0, newWidth, newHeight)
        End Using
        Return newImage
    End Function

    'Point in Polygon Math
    Public Function ATan2(ByVal opp As Single, ByVal adj As Single) As Single
        Dim angle As Single
        If Abs(adj) < 0.0001 Then
            angle = PI / 2
        Else
            angle = Abs(Atan(opp / adj))
        End If
        ' See if we are in quadrant 2 or 3.
        If adj < 0 Then
            ' angle > PI/2 or angle < -PI/2.
            angle = PI - angle
        End If
        ' See if we are in quadrant 3 or 4.
        If opp < 0 Then
            angle = -angle
        End If
        ' Return the result.
        ATan2 = angle
    End Function
    Public Function GetAngle(ByVal Ax As Single, ByVal Ay As Single, ByVal Bx As Single, ByVal By As Single, ByVal Cx As Single, ByVal Cy As Single) As Single
        Dim dot_product As Single
        Dim cross_product As Single

        ' Get the dot product and cross product.
        dot_product = DotProduct(Ax, Ay, Bx, By, Cx, Cy)
        cross_product = CrossProductLength(Ax, Ay, Bx, By, Cx, Cy)

        ' Calculate the angle.
        GetAngle = ATan2(cross_product, dot_product)
    End Function
    Public Function CrossProductLength(ByVal Ax As Single, ByVal Ay As Single, ByVal Bx As Single, ByVal By As Single, ByVal Cx As Single, ByVal Cy As Single) As Single
        Dim BAx As Single
        Dim BAy As Single
        Dim BCx As Single
        Dim BCy As Single

        ' Get the vectors' coordinates.
        BAx = Ax - Bx
        BAy = Ay - By
        BCx = Cx - Bx
        BCy = Cy - By

        ' Calculate the Z coordinate of the cross product.
        CrossProductLength = BAx * BCy - BAy * BCx
    End Function
    Public Function DotProduct(ByVal Ax As Single, ByVal Ay As Single, ByVal Bx As Single, ByVal By As Single, ByVal Cx As Single, ByVal Cy As Single) As Single
        Dim BAx As Single
        Dim BAy As Single
        Dim BCx As Single
        Dim BCy As Single

        ' Get the vectors' coordinates.
        BAx = Ax - Bx
        BAy = Ay - By
        BCx = Cx - Bx
        BCy = Cy - By

        ' Calculate the dot product.
        DotProduct = BAx * BCx + BAy * BCy
    End Function
    Public Function PointInPolygon(ByVal points() As PointF, ByVal X As Single, ByVal Y As Single) As Boolean
        ' Get the angle between the point and the
        ' first and last vertices.
        Dim max_point As Integer = points.Length - 1
        Dim total_angle As Single = GetAngle(points(max_point).X, points(max_point).Y, X, Y, points(0).X, points(0).Y)

        ' Add the angles from the point
        ' to each other pair of vertices.
        For i As Integer = 0 To max_point - 1
            total_angle += GetAngle(points(i).X, points(i).Y, X, Y, points(i + 1).X, points(i + 1).Y)
        Next i

        ' The total angle should be 2 * PI or -2 * PI if
        ' the point is in the polygon and close to zero
        ' if the point is outside the polygon.
        Return Math.Abs(total_angle) > 0.000001
    End Function
End Module
