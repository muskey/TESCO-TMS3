﻿Public Class UCPicture
    Inherits System.Web.UI.UserControl

    'Public Event btnAnsPICTUREclick(sender As Object, question_no As String)
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub


    Public Sub SetTestQuestionPicture(test_id As Integer, question_no As Integer, QuestionQty As Integer, ShowAnswer As String)
        txtTestID.Text = test_id
        txtShowAnswer.Text =
        pnlQuestionPicture.Visible = True
        Me.txtQuestion_no.Text = question_no.ToString

        Dim dt As DataTable = GetTestQuestion(test_id)

        Me.lblQNumber.Text = "ข้อ " + question_no.ToString + "/" + QuestionQty.ToString
        Me.lblQDetail.Text = dt.Rows(0)("question_title") & ""
        If dt.Rows(0)("icon_url") & "" <> "" Then
            lblImage2.Text = dt.Rows(0)("icon_url") & ""
            img2.Src = dt.Rows(0)("icon_url")
        End If
        Dim question() As String = Split(dt.Rows(0)("picture_text"), "###")
        Dim Choice As Double = question.Length
        Dim dtChoice As New DataTable
        dtChoice.Columns.Add("seq")
        dtChoice.Columns.Add("choice")
        Dim drc As DataRow
        For i As Integer = 0 To Choice - 1
            drc = dtChoice.NewRow()
            drc("choice") = question(i).ToString
            drc("seq") = i + 1
            dtChoice.Rows.Add(drc)
        Next
        rptQuestionPicture.DataSource = dtChoice
        rptQuestionPicture.DataBind()
    End Sub

    Private Sub btnAns_ServerClick(sender As Object, e As EventArgs) Handles btnAns.ServerClick
        'Response.Redirect("frmSelectQuestionTest.aspx?id=" & txtTestID.Text & "&q_id=" & (Convert.ToInt16(txtQuestion_no.Text) + 1))
        If txtShowAnswer.Text = "Y" Then
            pnlAnsResult.Visible = True
        Else
            Response.Redirect("frmSelectQuestionTest.aspx?id=" & txtTestID.Text & "&q_id=" & (Convert.ToInt16(txtQuestion_no.Text) + 1))
        End If
    End Sub

    Private Sub rptQuestionPicture_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles rptQuestionPicture.ItemDataBound
        If e.Item.ItemType <> ListItemType.Item And e.Item.ItemType <> ListItemType.AlternatingItem Then Exit Sub

        Dim lblQuestion As Label = e.Item.FindControl("lblQuestion")
        Dim lblNo As Label = e.Item.FindControl("lblNo")

        lblQuestion.Text = e.Item.DataItem("choice")
        lblNo.Text = e.Item.DataItem("seq")

    End Sub
End Class