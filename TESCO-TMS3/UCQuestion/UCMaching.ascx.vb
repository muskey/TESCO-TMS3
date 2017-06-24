Imports LinqDB.ConnectDB
Public Class UCMaching
    Inherits System.Web.UI.UserControl

    Public ReadOnly Property UserData As UserProfileData
        Get
            Return Session("UserData")
        End Get
    End Property
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Public Sub SetTestQuestionMatching(test_id As String, question_no As Integer, QuestionCount As Integer, ShowAnswer As String)
        txtTestID.Text = test_id
        txtShowAnswer.Text = ShowAnswer
        pnlQuestionMatching.Visible = True
        Me.txtQuestion_no.Text = question_no

        Dim dt As DataTable = GetTestQuestion(test_id, question_no)
        Me.lblQNumber.Text = "ข้อ " + question_no.ToString + "/" + QuestionCount.ToString
        Me.lblQDetail.Text = dt.Rows(0)("question_title") & ""
        txtQuestionID.Text = dt.Rows(0)("id")
        If dt.Rows(0)("icon_url") & "" <> "" Then
            img1.Src = dt.Rows(0)("icon_url")
            lblImage2.Text = dt.Rows(0)("icon_url") & ""
            likImage1.HRef = dt.Rows(0)("icon_url")
        Else
            likExtras.Attributes.Add("style", "display:none")
        End If

        Dim tmpAnswer() As String = Split(dt.Rows(0)("matching_lefttext"), "###")
        Dim sumans As Double = tmpAnswer.Length
        Dim dtans As New DataTable
        dtans.Columns.Add("ans")
        dtans.Columns.Add("seq")

        Dim dr As DataRow
        For i As Integer = 0 To sumans - 1
            dr = dtans.NewRow()
            dr("ans") = tmpAnswer(i).ToString
            dr("seq") = (i + 1)

            dtans.Rows.Add(dr)
        Next
        rptQuestionMatching.DataSource = dtans
        rptQuestionMatching.DataBind()

        Dim tmpChoice() As String = Split(dt.Rows(0)("matching_righttext"), "###")
        Dim tmpCorrectAns() As String = Split(dt.Rows(0)("matching_correct_answer"), ",")
        Dim Choice As Double = tmpChoice.Length
        Dim dtChoice As New DataTable
        dtChoice.Columns.Add("seq")
        dtChoice.Columns.Add("choice")
        dtChoice.Columns.Add("correct_answer")
        Dim drc As DataRow
        For i As Integer = 0 To Choice - 1
            drc = dtChoice.NewRow()
            drc("choice") = tmpChoice(i).ToString
            drc("seq") = Chr(65 + i)
            drc("correct_answer") = tmpCorrectAns(i).Replace(Chr(34), "").Trim
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
        Dim lblCorrectAnswer As Label = e.Item.FindControl("lblCorrectAnswer")
        Dim txtAnswer As TextBox = e.Item.FindControl("txtAnswer")
        SetTextIntKeypress(txtAnswer)

        lblAnswer.Text = e.Item.DataItem("choice")
        No.Text = e.Item.DataItem("seq")
        lblCorrectAnswer.Text = e.Item.DataItem("correct_answer")
    End Sub

    Private Sub btnAns_Click(sender As Object, e As EventArgs) Handles btnAns.Click
        'Response.Redirect("frmSelectQuestionTest.aspx?id=" & txtTestID.Text & "&q_id=" & (Convert.ToInt16(txtQuestion_no.Text) + 1))
        LogFileBL.LogTrans(UserData.LoginHistoryID, "คลิกปุ่มตอบ")
        If ValidateData() = True Then
            lblDialogHead.Text = "เฉลยคำตอบ"

            Dim TimeSpen As Integer = DateDiff(DateInterval.Second, Convert.ToDateTime(Session("teststarttime")), DateTime.Now)
            Dim trans As New TransactionDB
            Dim ret As New ExecuteDataInfo
            For Each itm As RepeaterItem In rptAnswerMatching.Items
                Dim lblCorrectAnswer As Label = itm.FindControl("lblCorrectAnswer")
                Dim txtAnswer As TextBox = itm.FindControl("txtAnswer")
                Dim lblAnswer As Label = itm.FindControl("lblAnswer")

                Dim AnswerResult As String = IIf(lblCorrectAnswer.Text = Convert.ToInt16(txtAnswer.Text) - 1, "Y", "N")

                LogFileBL.LogTrans(UserData.LoginHistoryID, "ตอบคำถาม " & lblAnswer.Text & ":" & txtAnswer.Text & "  " & IIf(AnswerResult = "Y", "ตอบถูก", "ตอบผิด"))


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
        End If

    End Sub

    Private Function ValidateData() As Boolean
        For Each itm As RepeaterItem In rptAnswerMatching.Items
            Dim txtAnswer As TextBox = itm.FindControl("txtAnswer")
            If txtAnswer.Text.Trim = "" Then
                'onValidate
                ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "Alert", "alert('กรุณาตอบให้ครบทุกข้อ');", True)
                Return False
            End If
        Next

        Return True
    End Function

End Class