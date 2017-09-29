Imports System.Drawing
Public Class RenderCaptcha
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim objBmp As New Bitmap(50, 20)
        Dim objGraphics As Graphics = Graphics.FromImage(objBmp)
        objGraphics.Clear(Color.White)

        objGraphics.TextRenderingHint = Text.TextRenderingHint.AntiAlias
        Dim objFont As New Font("Arial", 9, FontStyle.Bold)
        Dim randomStr As String = ""

        Dim autoRand = New Random
        For x As Integer = 0 To 3
            randomStr &= Convert.ToInt32(autoRand.Next(0, 9))
        Next

        Session("randomStr") = randomStr

        DrawNetLine(objGraphics)
        objGraphics.DrawString(randomStr, objFont, Brushes.Black, 3, 3)
        Response.ContentType = "image/GIF"

        objBmp.Save(Response.OutputStream, Imaging.ImageFormat.Gif)

        objFont.Dispose()
        objGraphics.Dispose()
        objBmp.Dispose()
    End Sub


    Private Sub DrawNetLine(objGraphics As Graphics)
        'ทำเส้นเป็นตาข่ายด้านหลังตัวเลข
        Dim autoRand = New Random
        Dim LineQty As Integer = autoRand.Next(3, 8)

        For i As Integer = 0 To LineQty - 1
            Dim grayPen As New Pen(Color.Gray, 1)
            Dim StartPoint As Integer = autoRand.Next(3, 18)
            Dim EndPoint As Integer = autoRand.Next(3, 18)

            objGraphics.DrawLine(grayPen, New Point(3, StartPoint), New Point(45, EndPoint))
        Next

    End Sub
End Class