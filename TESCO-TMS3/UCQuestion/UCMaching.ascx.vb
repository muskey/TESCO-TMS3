Public Class UCMaching
    Inherits System.Web.UI.UserControl

    Public Event btnAnsMACHINGclick(sender As Object, question_no As String)
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub



    Public Sub SetTestQuestionMatching(no As Integer, question_number As Double, qDt As DataTable)
        pnlQuestionMatching.Visible = True
        Me.txtQuestion_no.Text = question_number.ToString

        Dim tbc As DataTable = GetTestQuestion(qDt.Rows(0)("tb_testing_id"))
        txtQuestion_Count.Text = tbc.Rows.Count
        If no = 1 Then
            Dim dt As New DataTable
            dt.Columns.Add("question_title")
            dt.Columns.Add("answer_text")

            For i As Integer = 0 To 9
                Dim dr As DataRow = dt.NewRow
                dr("question_title") = (i + 1) & ". XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX"
                dr("answer_text") = Chr(65 + i) & ". xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx"

                dt.Rows.Add(dr)
            Next

            rptQuestionMatching.DataSource = dt
            rptQuestionMatching.DataBind()
        Else
            '    SetTestQuestionBefor1(no - 1)

            Me.lblQNumber.Text = "ข้อ " + question_number.ToString + "/" + Me.txtQuestion_Count.Text
            Me.lblQDetail.Text = qDt.Rows(0)("question_title") & ""
            If qDt.Rows(0)("icon_url") & "" <> "" Then
                img1.Src = qDt.Rows(0)("icon_url")
                lblImage2.Text = qDt.Rows(0)("icon_url") & ""
            End If

            Dim tmpAnswer2() As String = Split(qDt.Rows(0)("matching_lefttext"), "###")
            Dim sumans As Double = tmpAnswer2.Length
            Dim dtans As New DataTable
            dtans.Columns.Add("ans")
            dtans.Columns.Add("seq")
            Dim dr As DataRow
            For i As Integer = 0 To sumans - 1
                dr = dtans.NewRow()
                dr("ans") = tmpAnswer2(i).ToString
                dr("seq") = Chr(65 + i)
                dtans.Rows.Add(dr)
            Next
            rptQuestionMatching.DataSource = dtans
            rptQuestionMatching.DataBind()

            Dim tmpChoice2() As String = Split(qDt.Rows(0)("matching_righttext"), "###")
            Dim Choice As Double = tmpChoice2.Length
            Dim dtChoice As New DataTable
            dtChoice.Columns.Add("seq")
            dtChoice.Columns.Add("choice")
            Dim drc As DataRow
            For i As Integer = 0 To Choice - 1
                drc = dtChoice.NewRow()
                drc("choice") = tmpChoice2(i).ToString
                drc("seq") = i + 1
                dtChoice.Rows.Add(drc)
            Next
            rptAnswerMatching.DataSource = dtChoice
            rptAnswerMatching.DataBind()


        End If


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
        Dim question_no As Double = txtQuestion_no.Text
        RaiseEvent btnAnsMACHINGclick(sender, question_no)
    End Sub

End Class