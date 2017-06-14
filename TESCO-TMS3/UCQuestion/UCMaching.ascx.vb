Public Class UCMaching
    Inherits System.Web.UI.UserControl

    Public Event btnAnsMACHINGclick(sender As Object, question_no As String)
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub



    Public Sub SetTestQuestionMatching(no As Integer, qDt As DataTable)
        pnlQuestionMatching.Visible = True
        Me.txtQuestion_no.Text = no.ToString

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

            Me.lblQNumber2.Text = "ข้อ " + no.ToString + "/" + Me.txtQuestion_Count.Text
            Me.lblQDetail2.Text = qDt.Rows(0)("question_title") & ""
            If qDt.Rows(0)("icon_url") & "" <> "" Then
                lblImage2.Text = qDt.Rows(0)("icon_url") & ""
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

    Private Sub rptQuestion2_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles rptQuestionMatching.ItemDataBound
        If e.Item.ItemType <> ListItemType.Item And e.Item.ItemType <> ListItemType.AlternatingItem Then Exit Sub
        Dim txtAnswer As TextBox = e.Item.FindControl("txtAnswer")
        Dim lblQuestion As Label = e.Item.FindControl("lblQuestion")
        Dim lblAnswer As Label = e.Item.FindControl("lblAnswer")

        lblQuestion.Text = e.Item.DataItem("question_title")
        lblAnswer.Text = e.Item.DataItem("answer_text")
    End Sub

    Private Sub btnAns_ServerClick(sender As Object, e As EventArgs) Handles btnAns.ServerClick
        Dim question_no As Double = txtQuestion_no.Text
        RaiseEvent btnAnsMACHINGclick(sender, question_no)
    End Sub

End Class