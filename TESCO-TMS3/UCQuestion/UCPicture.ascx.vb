Imports LinqDB.ConnectDB
Public Class UCPicture
    Inherits System.Web.UI.UserControl

    Public ReadOnly Property UserData As UserProfileData
        Get
            Return Session("UserData")
        End Get
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub


    Public Sub SetTestQuestionPicture(test_id As Integer, question_no As Integer, QuestionQty As Integer, ShowAnswer As String)
        txtTestID.Text = test_id
        txtShowAnswer.Text =
        pnlQuestionPicture.Visible = True
        Me.txtQuestion_no.Text = question_no.ToString

        Dim dt As DataTable = GetTestQuestion(test_id, question_no)
        Me.lblQNumber.Text = "ข้อ " + question_no.ToString + "/" + QuestionQty.ToString
        Me.lblQDetail.Text = dt.Rows(0)("question_title") & ""
        txtQuestionID.Text = dt.Rows(0)("id")

        If dt.Rows(0)("icon_url") & "" <> "" Then
            lblImage2.Text = dt.Rows(0)("icon_url") & ""
            img2.Src = dt.Rows(0)("icon_url")
        End If
        Dim question() As String = Split(dt.Rows(0)("picture_text"), "###")
        Dim CorrectAnswer() As String = Split(dt.Rows(0)("picture_correct_answer"), ",")
        Dim Choice As Double = question.Length
        Dim dtChoice As New DataTable
        dtChoice.Columns.Add("abc")
        dtChoice.Columns.Add("choice")
        dtChoice.Columns.Add("correct_answer")
        Dim drc As DataRow
        For i As Integer = 0 To Choice - 1
            drc = dtChoice.NewRow()
            drc("choice") = question(i).ToString
            drc("abc") = Chr(65 + i)
            drc("correct_answer") = CorrectAnswer(i).Replace(Chr(34), "").Trim
            dtChoice.Rows.Add(drc)
        Next
        rptQuestionPicture.DataSource = dtChoice
        rptQuestionPicture.DataBind()
    End Sub

    Private Sub btnAns_ServerClick(sender As Object, e As EventArgs) Handles btnAns.ServerClick
        If ValidateData() = True Then

            Dim TimeSpen As Integer = DateDiff(DateInterval.Second, Convert.ToDateTime(Session("teststarttime")), DateTime.Now)
            Dim trans As New TransactionDB
            Dim ret As New ExecuteDataInfo

            For Each itm As RepeaterItem In rptQuestionPicture.Items
                Dim lblCorrectAnswer As Label = itm.FindControl("lblCorrectAnswer")
                Dim txtAnswer As TextBox = itm.FindControl("txtAnswer")

                Dim AnswerResult As String = IIf(lblCorrectAnswer.Text = Convert.ToInt16(txtAnswer.Text) - 1, "Y", "N")
                ret = SaveTestAnswer(UserData.UserName, trans, txtTestID.Text, txtQuestionID.Text, TimeSpen, Convert.ToInt16(txtAnswer.Text) - 1, AnswerResult)
                If ret.IsSuccess = False Then
                    Exit For
                End If
            Next

            If ret.IsSuccess = True Then
                trans.CommitTransaction()
            Else
                trans.RollbackTransaction()
            End If

            Response.Redirect("frmSelectQuestionTest.aspx?id=" & txtTestID.Text & "&q_id=" & (Convert.ToInt16(txtQuestion_no.Text) + 1))
            'If txtShowAnswer.Text = "Y" Then
            '    pnlAnsResult.Visible = True
            'Else
            '    Response.Redirect("frmSelectQuestionTest.aspx?id=" & txtTestID.Text & "&q_id=" & (Convert.ToInt16(txtQuestion_no.Text) + 1))
            'End If
        End If
    End Sub

    Private Function ValidateData()
        For Each itm As RepeaterItem In rptQuestionPicture.Items
            Dim txtAnswer As TextBox = itm.FindControl("txtAnswer")
            If txtAnswer.Text = "" Then
                ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "Alert", "alert('กรุณาตอบให้ครบทุกข้อ');", True)
                Return False
            End If
        Next

        Return True
    End Function

    Private Sub rptQuestionPicture_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles rptQuestionPicture.ItemDataBound
        If e.Item.ItemType <> ListItemType.Item And e.Item.ItemType <> ListItemType.AlternatingItem Then Exit Sub

        Dim lblQuestion As Label = e.Item.FindControl("lblQuestion")
        Dim lblAbc As Label = e.Item.FindControl("lblAbc")
        Dim lblCorrectAnswer As Label = e.Item.FindControl("lblCorrectAnswer")
        Dim txtAnswer As TextBox = e.Item.FindControl("txtAnswer")
        SetTextIntKeypress(txtAnswer)


        lblQuestion.Text = e.Item.DataItem("choice")
        lblAbc.Text = e.Item.DataItem("abc")
        lblCorrectAnswer.Text = e.Item.DataItem("correct_answer")

    End Sub
End Class