Public Class UCPicture
    Inherits System.Web.UI.UserControl

    Public Event btnAnsPICTUREclick(sender As Object, question_no As String)
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub


    Public Sub SetTestQuestionPicture(no As Integer, qDt As DataTable)
        pnlQuestionPicture.Visible = True
        Me.txtQuestion_no.Text = no.ToString

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

    Private Sub btnAns_ServerClick(sender As Object, e As EventArgs) Handles btnAns.ServerClick
        Dim question_no As Double = txtQuestion_no.Text
        RaiseEvent btnAnsPICTUREclick(sender, question_no)
    End Sub

End Class