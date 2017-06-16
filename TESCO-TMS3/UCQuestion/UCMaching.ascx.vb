Public Class UCMaching
    Inherits System.Web.UI.UserControl

    'Public Event btnAnsMACHINGclick(sender As Object, question_no As String)
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Public Sub SetTestQuestionMatching(test_id As String, question_no As Integer, QuestionCount As Integer, ShowAnswer As String)
        txtTestID.Text = test_id
        txtShowAnswer.Text = ShowAnswer
        pnlQuestionMatching.Visible = True
        Me.txtQuestion_no.Text = question_no

        Dim dt As DataTable = GetTestQuestion(test_id)
        Me.lblQNumber.Text = "ข้อ " + question_no.ToString + "/" + QuestionCount.ToString
        Me.lblQDetail.Text = dt.Rows(0)("question_title") & ""
        If dt.Rows(0)("icon_url") & "" <> "" Then
            img1.Src = dt.Rows(0)("icon_url")
            lblImage2.Text = dt.Rows(0)("icon_url") & ""
        End If

        Dim tmpAnswer2() As String = Split(dt.Rows(0)("matching_lefttext"), "###")
        Dim sumans As Double = tmpAnswer2.Length
        Dim dtans As New DataTable
        dtans.Columns.Add("ans")
        dtans.Columns.Add("seq")
        Dim dr As DataRow
        For i As Integer = 0 To sumans - 1
            dr = dtans.NewRow()
            dr("ans") = tmpAnswer2(i).ToString
            dr("seq") = (i + 1)
            dtans.Rows.Add(dr)
        Next
        rptQuestionMatching.DataSource = dtans
        rptQuestionMatching.DataBind()

        Dim tmpChoice2() As String = Split(dt.Rows(0)("matching_righttext"), "###")
        Dim Choice As Double = tmpChoice2.Length
        Dim dtChoice As New DataTable
        dtChoice.Columns.Add("seq")
        dtChoice.Columns.Add("choice")
        Dim drc As DataRow
        For i As Integer = 0 To Choice - 1
            drc = dtChoice.NewRow()
            drc("choice") = tmpChoice2(i).ToString
            drc("seq") = Chr(65 + i)
            dtChoice.Rows.Add(drc)
        Next
        rptAnswerMatching.DataSource = dtChoice
        rptAnswerMatching.DataBind()
    End Sub

    Private Sub rptQuestion2_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles rptQuestionMatching.ItemDataBound
        If e.Item.ItemType <> ListItemType.Item And e.Item.ItemType <> ListItemType.AlternatingItem Then Exit Sub

        Dim abc As Label = e.Item.FindControl("abc")
        Dim lblQuestion As Label = e.Item.FindControl("lblQuestion")

        lblQuestion.Text = e.Item.DataItem("ans")
        abc.Text = e.Item.DataItem("seq")
    End Sub

    Private Sub rptAnswerMatching_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles rptAnswerMatching.ItemDataBound
        If e.Item.ItemType <> ListItemType.Item And e.Item.ItemType <> ListItemType.AlternatingItem Then Exit Sub

        Dim No As Label = e.Item.FindControl("No")
        Dim lblAnswer As Label = e.Item.FindControl("lblAnswer")

        lblAnswer.Text = e.Item.DataItem("choice")
        No.Text = e.Item.DataItem("seq")
    End Sub

    Private Sub btnAns_ServerClick(sender As Object, e As EventArgs) Handles btnAns.ServerClick
        'Response.Redirect("frmSelectQuestionTest.aspx?id=" & txtTestID.Text & "&q_id=" & (Convert.ToInt16(txtQuestion_no.Text) + 1))
        If txtShowAnswer.Text = "Y" Then
            pnlAnsResult.Visible = True
        Else
            Response.Redirect("frmSelectQuestionTest.aspx?id=" & txtTestID.Text & "&q_id=" & (Convert.ToInt16(txtQuestion_no.Text) + 1))
        End If
    End Sub

End Class