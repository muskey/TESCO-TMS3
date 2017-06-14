Public Class UCPicture
    Inherits System.Web.UI.UserControl

    Public Event btnAnsPICTUREclick(sender As Object, question_no As String)
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub


    Public Sub SetTestQuestionPicture(no As Integer, question_number As Double, qDt As DataTable)
        pnlQuestionPicture.Visible = True
        Me.txtQuestion_no.Text = question_number.ToString

        Dim tbc As DataTable = GetTestQuestion(qDt.Rows(0)("tb_testing_id"))
        txtQuestion_Count.Text = tbc.Rows.Count
        If no = 1 Then
            Dim dt As New DataTable
            dt.Columns.Add("question_title")

            For i As Integer = 0 To 9
                Dim dr As DataRow = dt.NewRow
                dr("question_title") = (i + 1) & ". XXXXXX XXXXXXXX XXXXXXX XXXXXXX XXXXXXX"

                dt.Rows.Add(dr)
            Next

            rptQuestionPicture.DataSource = dt
            rptQuestionPicture.DataBind()
        Else

            Me.lblQNumber.Text = "ข้อ " + question_number.ToString + "/" + Me.txtQuestion_Count.Text
            Me.lblQDetail.Text = qDt.Rows(0)("question_title") & ""
            If qDt.Rows(0)("icon_url") & "" <> "" Then
                lblImage2.Text = qDt.Rows(0)("icon_url") & ""
                img2.Src = qDt.Rows(0)("icon_url")
            End If
            Dim question() As String = Split(qDt.Rows(0)("picture_text"), "###")
            Dim Choice As Double = question.Length
            Dim dtChoice As New DataTable
            dtChoice.Columns.Add("seq")
            dtChoice.Columns.Add("choice")
            dtChoice.Columns.Add("abc")
            Dim drc As DataRow
            For i As Integer = 0 To Choice - 1
                drc = dtChoice.NewRow()
                drc("choice") = question(i).ToString
                drc("seq") = i + 1
                drc("abc") = Chr(65 + i)
                dtChoice.Rows.Add(drc)
            Next
            rptQuestionPicture.DataSource = dtChoice
            rptQuestionPicture.DataBind()
        End If
    End Sub

    Private Sub btnAns_ServerClick(sender As Object, e As EventArgs) Handles btnAns.ServerClick
        Dim question_no As Double = txtQuestion_no.Text
        RaiseEvent btnAnsPICTUREclick(sender, question_no)
    End Sub

    Private Sub rptQuestionPicture_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles rptQuestionPicture.ItemDataBound
        If e.Item.ItemType <> ListItemType.Item And e.Item.ItemType <> ListItemType.AlternatingItem Then Exit Sub

        Dim lblQuestion As Label = e.Item.FindControl("lblQuestion")
        Dim No As Label = e.Item.FindControl("No")
        Dim abc As Label = e.Item.FindControl("abc")

        lblQuestion.Text = e.Item.DataItem("choice")
        No.Text = e.Item.DataItem("seq")
        abc.Text = e.Item.DataItem("abc")

    End Sub
End Class