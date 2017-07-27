Imports System.Data.SqlClient
Imports LinqDB.ConnectDB
Imports LinqDB.TABLE
Public Class frmSelectQuestionTest
    Inherits System.Web.UI.Page

    Public ReadOnly Property UserData As UserProfileData
        Get
            Return Session("UserData")
        End Get
    End Property
    Public ReadOnly Property test_id As String
        Get
            Return Page.Request.QueryString("id") & ""
        End Get
    End Property
    Public ReadOnly Property q_id As String
        Get
            Return Page.Request.QueryString("q_id")
        End Get
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("UserData") Is Nothing Then
            Response.Redirect("frmLogin.aspx")
        End If

        If Not Page.IsPostBack Then
            Dim lnq As TbTestingLinqDB = GetTestingData(test_id)
            If lnq.ID > 0 Then
                Me.lblTestTitle.Text = "<h3>&nbsp>&nbsp<a href=""frmSelectTestCourse.aspx""><font color=""#019b79"">" + lnq.TEST_TITLE + "&nbsp&nbsp</font></a></h3>"
                lblIsShowAnswer.Text = lnq.IS_SHOW_ANSWER
                lblQuestionQty.Text = lnq.QUESTION_QTY
                lblCourseID.Text = lnq.COURSE_ID
                lblTargetPercent.Text = lnq.TARGET_PERCENTAGE

                If q_id <= Convert.ToInt32(lblQuestionQty.Text) Then
                    SetStartTest(lnq)
                Else
                    'ถ้าเป็นคำถามสุดท้ายแล้วให้ Update Log ไปยัง Backend และแสดงหน้าจอสรุป
                    pnlTestQuestion.Visible = False


                    Dim aDt As DataTable = GetAnswerDT()
                    SendTestingLog(aDt)
                    ShowTestSummary(aDt)
                End If
            End If
        End If
    End Sub
    Private Sub SetStartTest(TestLnq As TbTestingLinqDB)
        'Dim dt As DataTable = GetTesting(UserData.UserSessionID)
        'dt.DefaultView.RowFilter = "id='" & test_id & "'"

        If q_id = 1 Then
            'ถ้าเป็นข้อแรก ให้ลบคำตอบที่เคยตอบแล้ว เพื่อเริ่มทำข้อสอบใหม่
            Dim trans As New TransactionDB
            Dim sql As String = " delete from TB_TESTING_ANSWER "
            sql += " where tb_testing_id in (select id from TB_TESTING where tb_user_session_id=@_USER_SESSION_ID) "
            Dim p(1) As SqlParameter
            p(0) = SqlDB.SetBigInt("@_USER_SESSION_ID", UserData.UserSessionID)

            Dim ret As ExecuteDataInfo = SqlDB.ExecuteNonQuery(sql, p)
            If ret.IsSuccess = True Then

                'Update TestHistory
                Dim tLnq As New TbTestingHisLinqDB
                tLnq.ChkDataByTEST_ID_USER_ID(TestLnq.TEST_ID, UserData.UserID, trans.Trans)

                tLnq.USER_ID = UserData.UserID
                tLnq.USERNAME = UserData.UserName
                tLnq.TEST_ID = TestLnq.TEST_ID
                tLnq.TEST_TITLE = TestLnq.TEST_TITLE
                tLnq.TEST_DESC = TestLnq.TEST_DESC
                tLnq.TARGET_PERCENTAGE = TestLnq.TARGET_PERCENTAGE
                tLnq.COURSE_ID = TestLnq.COURSE_ID
                tLnq.QUESTION_QTY = TestLnq.QUESTION_QTY

                If tLnq.ID = 0 Then
                    ret = tLnq.InsertData(UserData.UserName, trans.Trans)
                Else
                    ret = tLnq.UpdateData(UserData.UserName, trans.Trans)
                End If

                If ret.IsSuccess = True Then
                    trans.CommitTransaction()
                Else
                    trans.RollbackTransaction()
                End If
                tLnq = Nothing
            Else
                trans.RollbackTransaction()
            End If
        End If

        Session("teststarttime") = DateTime.Now
        Dim qDt As DataTable = GetTestQuestion(test_id, q_id)
        If qDt.Rows.Count > 0 Then
            UCabcd1.Visible = False
            UCyesno.Visible = False
            UCMaching.Visible = False
            UCWriting.Visible = False
            UCPicture.Visible = False

            LogFileBL.LogTrans(UserData.LoginHistoryID, "แสดงรายละเอียดคำถามที่ " & q_id & "/" & lblQuestionQty.Text & " ประเภทคำถาม " & qDt.Rows(0)("question_type").ToString)

            Select Case qDt.Rows(0)("question_type").ToString.ToLower
                Case "abcd"
                    Dim question_number As Double = qDt.Rows(0)("question_no").ToString()
                    Me.UCabcd1.setTestQuestionABCD(test_id, question_number, lblQuestionQty.Text, lblIsShowAnswer.Text)
                    UCabcd1.Visible = True
                Case "yes/no"
                    Dim question_number As Double = qDt.Rows(0)("question_no").ToString()
                    Me.UCyesno.SetTestQuestionYESNO(test_id, question_number, lblQuestionQty.Text, lblIsShowAnswer.Text)
                    UCyesno.Visible = True
                Case "matching"
                    Dim question_number As Double = qDt.Rows(0)("question_no").ToString()
                    Me.UCMaching.SetTestQuestionMatching(test_id, question_number, lblQuestionQty.Text, lblIsShowAnswer.Text)
                    UCMaching.Visible = True
                Case "writing"
                    Dim question_number As Double = qDt.Rows(0)("question_no").ToString()
                    Me.UCWriting.SetTestQuestionWriting(test_id, question_number, lblQuestionQty.Text, lblIsShowAnswer.Text)
                    UCWriting.Visible = True
                Case "picture"
                    Dim question_number As Double = qDt.Rows(0)("question_no").ToString()
                    Me.UCPicture.SetTestQuestionPicture(test_id, question_number, lblQuestionQty.Text, lblIsShowAnswer.Text)
                    UCPicture.Visible = True
            End Select
        End If
    End Sub

    Private Function GetAnswerDT() As DataTable
        Dim sql As String = "select ta.id, t.id test_id, ta.answer_result, ta.answer_choice,tq.question_no, ta.tb_testing_question_id,ta.time_spent, "
        sql += " tq.question_type, tq.weight"
        sql += " From TB_TESTING_ANSWER ta "
        sql += " inner join TB_TESTING_QUESTION tq on tq.id=ta.tb_testing_question_id "
        sql += " inner join TB_TESTING t on t.id=tq.tb_testing_id"
        sql += " where ta.tb_testing_id=@_TESTING_ID "
        Dim p(1) As SqlParameter
        p(0) = SqlDB.SetBigInt("@_TESTING_ID", test_id)

        Dim aDt As DataTable = SqlDB.ExecuteTable(sql, p)
        Return aDt
    End Function

    Private Sub SendTestingLog(AnswerDt As DataTable)
        If AnswerDt.Rows.Count > 0 Then
            Dim AnswerData As String = ""
            AnswerData = "{" & Chr(34) & "testing_id" & Chr(34) & ":" & AnswerDt.DefaultView(0)("test_id") & ","
            AnswerData += Chr(34) + "is_scorm" + Chr(34) & ":" & Chr(34) & "false" & Chr(34) & ", "
            AnswerData += Chr(34) & "user_id" & Chr(34) & ":" & UserData.UserID & ","
            AnswerData += Chr(34) & "course_id" & Chr(34) & ":" & lblCourseID.Text & ", "
            AnswerData += Chr(34) & "is_success" & Chr(34) & ":" & Chr(34) & "true" & Chr(34) & ", "
            AnswerData += Chr(34) & "target_percentage" & Chr(34) & ":" & lblTargetPercent.Text & ", "
            AnswerData += Chr(34) + "answer_data" + Chr(34) & ":[" & Environment.NewLine

            Dim HaveAns As Boolean = False
#Region "Answer ABCD, YESNO"
            AnswerDt.DefaultView.RowFilter = "question_type in ('abcd','yes/no')"
            If AnswerDt.DefaultView.Count > 0 Then
                For Each qDr As DataRowView In AnswerDt.DefaultView
                    If HaveAns = True Then
                        AnswerData += ", "
                    End If
                    AnswerData += "{"
                    AnswerData += Chr(34) + "question_id" + Chr(34) & ":" & qDr("question_no") & ", " & Environment.NewLine

                    AnswerData += Chr(34) + "type" + Chr(34) + ":" & qDr("question_type") & ", " & Environment.NewLine
                    AnswerData += Chr(34) + "weight" + Chr(34) + ":" & qDr("weight") & "," & Environment.NewLine
                    AnswerData += Chr(34) + "answer_id" + Chr(34) & ":" & qDr("answer_choice") & ", " & Environment.NewLine
                    AnswerData += Chr(34) + "is_correct" + Chr(34) & ":" & Chr(34) & IIf(qDr("answer_result").ToString = "Y", "true", "false") & Chr(34) & ", " & Environment.NewLine
                    AnswerData += Chr(34) + "time_spent" + Chr(34) & ":" & qDr("time_spent") & Environment.NewLine
                    AnswerData += "}"

                    HaveAns = True
                Next
            End If
            AnswerDt.DefaultView.RowFilter = ""
#End Region

#Region "Answer MATCHING, PICTURE"
            AnswerDt.DefaultView.RowFilter = "question_type in ('matching','picture')"
            If AnswerDt.DefaultView.Count > 0 Then
                Dim qDt As New DataTable   'จำนวนคำถามของ maching, picture
                qDt = AnswerDt.DefaultView.ToTable(True, "test_id", "question_no", "question_type", "weight", "time_spent")
                If qDt.Rows.Count > 0 Then

                    For Each qDr As DataRow In qDt.Rows
                        'เอาเฉพาะข้อที่ตอบถูกเพื่อหา Percent
                        AnswerDt.DefaultView.RowFilter = "question_type in ('matching','picture') and question_no='" & qDr("question_no") & "' "
                        Dim AllAnswer As Integer = AnswerDt.DefaultView.Count  'จำนวนข้อทั้งหมด

                        AnswerDt.DefaultView.RowFilter = "question_type in ('matching','picture') and question_no='" & qDr("question_no") & "' and answer_result='Y'"
                        Dim CorrectAnswer As Integer = AnswerDt.DefaultView.Count  'จำนวนข้อที่ตอบถูก

                        Dim CorrectPercent As Integer = (CorrectAnswer * 100) / AllAnswer

                        If HaveAns = True Then
                            AnswerData += ", "
                        End If
                        AnswerData += "{"
                        AnswerData += Chr(34) + "question_id" + Chr(34) & ":" & qDr("question_no") & ", " & Environment.NewLine
                        AnswerData += Chr(34) + "type" + Chr(34) + ":" & Chr(34) & qDr("question_type") & Chr(34) & ", " & Environment.NewLine
                        AnswerData += Chr(34) + "weight" + Chr(34) + ":" & qDr("weight") & "," & Environment.NewLine
                        AnswerData += Chr(34) + "correct_percentage" + Chr(34) & ":" & CorrectPercent & ", " & Environment.NewLine
                        AnswerData += Chr(34) + "time_spent" + Chr(34) & ":" & qDr("time_spent") & Environment.NewLine
                        AnswerData += "}"

                        HaveAns = True
                    Next
                End If
                qDt.Dispose()
            End If
            AnswerDt.DefaultView.RowFilter = ""
#End Region

#Region "WRITING"
            Dim dt As DataTable = GetTestWritingAnswer(test_id)
            If dt.Rows.Count > 0 Then
                For Each qDr As DataRow In dt.Rows
                    If HaveAns = True Then
                        AnswerData += ", "
                    End If
                    AnswerData += "{"
                    AnswerData += Chr(34) + "question_id" + Chr(34) & ":" & qDr("question_no") & ", " & Environment.NewLine
                    AnswerData += Chr(34) + "type" + Chr(34) + ":" & Chr(34) & qDr("question_type") & Chr(34) & ", " & Environment.NewLine
                    AnswerData += Chr(34) + "weight" + Chr(34) + ":" & qDr("weight") & "," & Environment.NewLine
                    AnswerData += Chr(34) + "answer_text" + Chr(34) & ":" & Chr(34) & qDr("answer_text") & Chr(34) & ", " & Environment.NewLine
                    AnswerData += Chr(34) + "time_spent" + Chr(34) & ":" & qDr("time_spent") & Environment.NewLine
                    AnswerData += "}"
                    HaveAns = True
                Next
            End If
            dt.Dispose()
#End Region

            AnswerData += "]"
            AnswerData += "}"

            Dim info As String = ""
            info = GetStringDataFromURL(Me, Me.GetType, UserData.LoginHistoryID, GetWebServiceURL() & "api/log", UserData.Token & "&action=complete&module=testing&data=" & AnswerData)
            If info.Trim <> "" Then
                'txtQuestion_Dialog.Text = strdialogid
            End If
        End If

    End Sub

    Private Sub ShowTestSummary(AnswerDt As DataTable)
        Dim TotalWeight As Integer = 0
        Dim CorrectScore As Double = 0
        Dim ResultPercent As Double = 0
        Dim Target As Integer = lblTargetPercent.Text
        Dim qDt As DataTable = AnswerDt.DefaultView.ToTable(True, "tb_testing_question_id", "weight")
        If qDt.Rows.Count > 0 Then
            TotalWeight = Convert.ToInt64(qDt.Compute("sum(weight)", ""))   '100%
            For i As Integer = 0 To qDt.Rows.Count - 1
                Dim TestQuestionID As Long = qDt.Rows(i)("tb_testing_question_id")
                Dim Weight As Integer = qDt.Rows(i)("weight")

                AnswerDt.DefaultView.RowFilter = "tb_testing_question_id=" & TestQuestionID & " and answer_result='Y'"
                Dim CorrectQty As Integer = AnswerDt.DefaultView.Count

                AnswerDt.DefaultView.RowFilter = "tb_testing_question_id=" & TestQuestionID
                Dim AnsQty As Integer = AnswerDt.DefaultView.Count

                CorrectScore += ((CorrectQty * Weight) / AnsQty)

                AnswerDt.DefaultView.RowFilter = ""
            Next

            ResultPercent = (CorrectScore * 100) / TotalWeight
        End If

        If ResultPercent < Target Then
            pnlSummaryFalse.Visible = True
            lblCorrectScoreFalse.Text = CorrectScore.ToString("0")
            lblTotalScoreFalse.Text = TotalWeight
            lblTotalQuestionFalse.Text = lblQuestionQty.Text
            lblResultPercentFalse.Text = Target & "%"
            lblCorrectPercentFalse.Text = ResultPercent & "%"
        Else
            pnlSummaryTrue.Visible = True

            lblCorrectScoreTrue.Text = CorrectScore.ToString("0")
            lblTotalScoreTrue.Text = TotalWeight
            lblTotalQuestionTrue.Text = lblQuestionQty.Text
            lblResultPercentTrue.Text = Target & "%"
            lblCorrectPercentTrue.Text = ResultPercent & "%"
        End If
    End Sub

    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBackFalse.Click, btnBackTrue.Click
        LogFileBL.LogTrans(UserData.LoginHistoryID, "คลิกปุ่ม กลับหน้าแรก")
        Response.Redirect("frmSelectTestCourse.aspx?rnd=" & DateTime.Now.Millisecond)
    End Sub
End Class