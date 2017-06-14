Public Class UCWriting
    Inherits System.Web.UI.UserControl

    Public Event btnAnswritingclick(sender As Object, question_no As String)
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Public Sub SetTestQuestionWriting(no As Integer, question_number As Double, qDt As DataTable)
        pnlQuestionWriting.Visible = True
        Me.txtQuestion_no.Text = question_number.ToString

        Dim tbc As DataTable = GetTestQuestion(qDt.Rows(0)("tb_testing_id"))
        txtQuestion_Count.Text = tbc.Rows.Count
        If no = 1 Then

        Else

            Me.lblQNumber.Text = "ข้อ " + question_number.ToString + "/" + Me.txtQuestion_Count.Text
            Me.lblQDetail.Text = qDt.Rows(0)("question_title") & ""
            If qDt.Rows(0)("icon_url") & "" <> "" Then
                lblImage2.Text = qDt.Rows(0)("icon_url") & ""
                img1.Src = qDt.Rows(0)("icon_url")
            End If
            Dim tmpAnswer2() As String = Split(qDt.Rows(0)("answer"), "##")
            Dim tmpChoice2() As String = Split(qDt.Rows(0)("choice"), "##")
            If tmpChoice2.Length = 4 And tmpAnswer2.Length = 4 Then
                lblA2.InnerText = tmpChoice2(0)
                lblB2.InnerText = tmpChoice2(1)
                lblC2.InnerText = tmpChoice2(2)
                lblD2.InnerText = tmpChoice2(3)
            End If
        End If
    End Sub

    Private Sub btnAns_ServerClick(sender As Object, e As EventArgs) Handles btnAns.ServerClick
        Dim question_no As Double = txtQuestion_no.Text
        RaiseEvent btnAnswritingclick(sender, question_no)
    End Sub

End Class