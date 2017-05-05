Imports System.Data.SqlClient
Imports LinqDB.ConnectDB
Imports LinqDB.TABLE
Public Class frmSelectTestQuestion
    Inherits System.Web.UI.Page

#Region "Declare & Valiable"
    ' Public myUser As User
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

    'Public ReadOnly Property test_title As String
    '    Get
    '        Return Page.Request.QueryString("title") & ""
    '    End Get
    'End Property
#End Region

#Region "Initail"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Dim dt As DataTable = GetTesting(UserData.UserSessionID)
            dt.DefaultView.RowFilter = "id=" & test_id

            If dt.DefaultView.Count > 0 Then
                Me.lblTitle.Text = "<h3>&nbsp>&nbsp<a href=""frmSelectTestCourse.aspx""><font color=""#019b79"">" + dt.DefaultView(0)("test_title").ToString + "&nbsp&nbsp</font></a></h3>"
                Me.ckbA.Attributes.Add("onclick", "onConfirmCheck(0)")
                Me.ckbB.Attributes.Add("onclick", "onConfirmCheck(1)")
                Me.ckbC.Attributes.Add("onclick", "onConfirmCheck(2)")
                Me.ckbD.Attributes.Add("onclick", "onConfirmCheck(3)")

                'Me.txtQuestion_no.Style.Add("display", "none")
                'Me.txtQuestion_Count.Style.Add("display", "none")
                'Me.txtCourse_id.Style.Add("display", "none")
                SetStartTest()
                'SetTestQuestionCount()
                SetTestQuestion(1)
            End If
            dt.DefaultView.RowFilter = ""
        End If
    End Sub
    Private Sub SetStartTest()
        Dim dt As DataTable = GetTesting(UserData.UserSessionID)
        dt.DefaultView.RowFilter = "id='" & test_id & "'"
        If dt.DefaultView.Count > 0 Then
            Me.txtCourse_id.Text = dt.DefaultView.Table.Rows(0)("course_id")
            Me.txtQuestion_Count.Text = dt.DefaultView(0)("question_qty")
        End If
        dt.DefaultView.RowFilter = ""
        Session("teststarttime") = DateTime.Now

        Dim trans As New TransactionDB
        Dim sql As String = " delete from TB_TESTING_ANSWER "
        sql += " where tb_testing_id in (select id from TB_TESTING where tb_user_session_id=@_USER_SESSION_ID) "
        Dim p(1) As SqlParameter
        p(0) = SqlDB.SetBigInt("@_USER_SESSION_ID", UserData.UserSessionID)

        Dim ret As ExecuteDataInfo = SqlDB.ExecuteNonQuery(sql, p)
        If ret.IsSuccess = True Then
            trans.CommitTransaction()
        Else
            trans.RollbackTransaction()
        End If

        'For i As Integer = 0 To UserData.TestQuestion.Rows.Count - 1
        '    UserData.TestQuestion.Rows(i)("answer_id") = DBNull.Value
        '    UserData.TestQuestion.Rows(i)("answer_result") = DBNull.Value
        '    UserData.TestQuestion.Rows(i)("time_spent") = DBNull.Value
        '    UserData.TestQuestion.Rows(i)("answer_choice") = DBNull.Value
        'Next
        'Session("UserData") = UserData
    End Sub
    'Private Sub SetTestQuestionCount()

    '    UserData.TestQuestion.DefaultView.RowFilter = "tb_test_id='" & test_id & "'"
    '    If UserData.TestQuestion.DefaultView.Count > 0 Then
    '        Me.txtQuestion_Count.Text = UserData.TestQuestion.DefaultView.Count
    '    End If
    'End Sub
    Private Sub SetTestQuestion(no As Integer)
        Try
            Me.txtQuestion_no.Text = no.ToString
            Dim dt As DataTable = GetTestQuestion(test_id, no) 'UserData.TestQuestion.DefaultView.ToTable 'GetSqlDataTable(sql)

            Dim str As String = ""
            If no = 1 Then
                If (dt.Rows.Count > 0) Then
                    Me.lblQNumber.Text = "ข้อ " + no.ToString + "/" + Me.txtQuestion_Count.Text
                    Me.lblQDetail.Text = dt.Rows(0)("question_title") & ""
                    If dt.Rows(0)("icon_url") & "" <> "" Then
                        Me.imgQ.Src = dt.Rows(0)("icon_url") & ""
                    End If

                    Dim tmpAnswer() As String = Split(dt.Rows(0)("answer"), "##")
                    Dim tmpChoice() As String = Split(dt.Rows(0)("choice"), "##")

                    If tmpChoice.Length = 4 And tmpAnswer.Length = 4 Then
                        lblA.InnerText = tmpChoice(0)
                        lblB.InnerText = tmpChoice(1)
                        lblC.InnerText = tmpChoice(2)
                        lblD.InnerText = tmpChoice(3)
                    End If
                End If
            Else
                SetTestQuestionBefor(no - 1)

                Me.lblQNumber2.Text = "ข้อ " + no.ToString + "/" + Me.txtQuestion_Count.Text
                Me.lblQDetail2.Text = dt.Rows(0)("question_title") & ""
                If dt.Rows(0)("icon_url") & "" <> "" Then
                    lblImage2.Text = dt.Rows(0)("icon_url") & ""
                End If
                Dim tmpAnswer2() As String = Split(dt.Rows(0)("answer"), "##")
                Dim tmpChoice2() As String = Split(dt.Rows(0)("choice"), "##")
                If tmpChoice2.Length = 4 And tmpAnswer2.Length = 4 Then
                    lblA2.InnerText = tmpChoice2(0)
                    lblB2.InnerText = tmpChoice2(1)
                    lblC2.InnerText = tmpChoice2(2)
                    lblD2.InnerText = tmpChoice2(3)
                End If
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub SetTestQuestionBefor(no As Integer)
        Dim dt As DataTable = GetTestQuestion(test_id, no)
        If dt.Rows.Count > 0 Then
            Me.lblQNumber.Text = "ข้อ " + no.ToString + "/" + Me.txtQuestion_Count.Text
            Me.lblQDetail.Text = dt.Rows(0)("question_title") & ""
            If dt.Rows(0)("icon_url") & "" <> "" Then
                Me.imgQ.Src = dt.Rows(0)("icon_url") & ""
            End If

            Dim tmpAnswer() As String = Split(dt.Rows(0)("answer"), "##")
            Dim tmpChoice() As String = Split(dt.Rows(0)("choice"), "##")

            If tmpChoice.Length = 4 And tmpAnswer.Length = 4 Then
                lblA.InnerText = tmpChoice(0)
                lblB.InnerText = tmpChoice(1)
                lblC.InnerText = tmpChoice(2)
                lblD.InnerText = tmpChoice(3)
            End If


            Dim sql As String = "select answer_choice "
            sql += " From TB_TESTING_ANSWER ta "
            sql += " inner join TB_TESTING_QUESTION tq On tq.id=ta.tb_testing_question_id "
            sql += " where ta.tb_testing_id=@_TESTING_ID and tq.question_no=@_QUESTION_NO "
            Dim p(2) As SqlParameter
            p(0) = SqlDB.SetBigInt("@_TESTING_ID", test_id)
            p(1) = SqlDB.SetInt("@_QUESTION_NO", no)

            Dim aDt As DataTable = SqlDB.ExecuteTable(sql, p)
            If aDt.Rows.Count > 0 Then
                Dim retchoice As Integer = Val(aDt.Rows(0)("answer_choice") & "")

                If retchoice = 0 Then
                    Me.ckbA.Checked = True
                    Me.ckbB.Checked = False
                    Me.ckbC.Checked = False
                    Me.ckbD.Checked = False
                ElseIf retchoice = 1 Then
                    Me.ckbA.Checked = False
                    Me.ckbB.Checked = True
                    Me.ckbC.Checked = False
                    Me.ckbD.Checked = False
                ElseIf retchoice = 2 Then
                    Me.ckbA.Checked = False
                    Me.ckbB.Checked = False
                    Me.ckbC.Checked = True
                    Me.ckbD.Checked = False
                ElseIf retchoice = 3 Then
                    Me.ckbA.Checked = False
                    Me.ckbB.Checked = False
                    Me.ckbC.Checked = False
                    Me.ckbD.Checked = True
                End If
            End If
            aDt.Dispose()
        End If
    End Sub

    Private Sub btnOK_Click(sender As Object, e As EventArgs) Handles btnOK.ServerClick
        If ckbA.Checked = False And ckbB.Checked = False And ckbC.Checked = False And ckbD.Checked = False Then
            ScriptManager.RegisterStartupScript(Me, Page.GetType, "Script", "onCheckSelect();", True)
            Exit Sub
        End If

        If Me.txtQuestion_no.Text <> "" Then

            Dim dt As DataTable = GetTestQuestion(test_id, txtQuestion_no.Text)
            If dt.Rows.Count > 0 Then
                Dim ret As Boolean = False
                Dim retTrue As String = ""
                Dim retId As String = ""
                Dim retChoice As Integer = 0
                Dim tmpAnswer() As String = Split(dt.Rows(0)("answer"), "##")
                Dim tmpChoice() As String = Split(dt.Rows(0)("choice"), "##")
                retId = dt.Rows(0)("id")
                If tmpAnswer.Length = 4 And tmpChoice.Length = 4 Then
                    If Me.txtQuestion_Choice.Text = 0 Then
                        retChoice = 0
                        If tmpAnswer(0) = "True" Then
                            ret = True
                        End If
                    ElseIf Me.txtQuestion_Choice.Text = 1 Then
                        retChoice = 1
                        If tmpAnswer(1) = "True" Then
                            ret = True
                        End If
                    ElseIf Me.txtQuestion_Choice.Text = 2 Then
                        retChoice = 2
                        If tmpAnswer(2) = "True" Then
                            ret = True
                        End If
                    ElseIf Me.txtQuestion_Choice.Text = 3 Then
                        retChoice = 3
                        If tmpAnswer(3) = "True" Then
                            ret = True
                        End If
                    End If

                    If tmpAnswer(0) = "True" Then
                        retTrue = tmpChoice(0)
                    ElseIf tmpAnswer(1) = "True" Then
                        retTrue = tmpChoice(1)
                    ElseIf tmpAnswer(2) = "True" Then
                        retTrue = tmpChoice(2)
                    ElseIf tmpAnswer(3) = "True" Then
                        retTrue = tmpChoice(3)
                    End If
                End If

                Dim StartTime As DateTime = Session("teststarttime")

                Dim aLnq As New TbTestingAnswerLinqDB
                aLnq.TB_TESTING_ID = test_id
                aLnq.TB_TESTING_QUESTION_ID = Convert.ToInt64(dt.Rows(0)("id"))
                aLnq.TIME_SPENT = DateDiff(DateInterval.Second, StartTime, DateTime.Now)
                aLnq.ANSWER_RESULT = IIf(ret = True, "Y", "N")
                aLnq.ANSWER_CHOICE = retChoice

                Dim trans As New TransactionDB
                If aLnq.InsertData(UserData.UserName, trans.Trans).IsSuccess = True Then
                    trans.CommitTransaction()
                Else
                    trans.RollbackTransaction()
                End If

                Me.txtQuestion_no.Text = Val(Me.txtQuestion_no.Text) + 1
                Dim lastchoice As Integer = 0
                If Val(Me.txtQuestion_no.Text) <= Val(Me.txtQuestion_Count.Text) Then
                    SetTestQuestion(Val(Me.txtQuestion_no.Text))
                Else
                    Sumnary()
                    SetTestQuestionBefor(Val(Me.txtQuestion_Count.Text))
                    btnOK.Visible = False
                    btnSummary.Visible = True
                    lastchoice = 1
                End If

                ScriptManager.RegisterStartupScript(Me, Page.GetType, "Script", "onConfirmTest('" + ret.ToString + "','" & retTrue & "','" & (Val(Me.txtQuestion_no.Text) - 1) & "','" & lastchoice.ToString & "');", True)
            End If
        End If
    End Sub

    Private Sub Sumnary()
        'To Summary Screen
        Dim dt As DataTable = GetTesting(UserData.UserSessionID)
        Dim _TargetPercent As Integer = 0
        dt.DefaultView.RowFilter = "id='" & test_id & "'"
        If dt.DefaultView.Count > 0 Then
            _TargetPercent = dt.DefaultView.Table.Rows(0)("target_percentage")
        End If
        dt.DefaultView.RowFilter = ""

        Dim countall As Integer
        Dim qDT As DataTable = GetTestQuestion(test_id)
        countall = qDT.Rows.Count

        Dim countScore As Integer
        Dim sql As String = "select ta.id, ta.answer_result, ta.answer_choice, ta.tb_testing_question_id,ta.time_spent "
        sql += " From TB_TESTING_ANSWER ta "
        sql += " where ta.tb_testing_id=@_TESTING_ID "
        Dim p(1) As SqlParameter
        p(0) = SqlDB.SetBigInt("@_TESTING_ID", test_id)

        Dim aDt As DataTable = SqlDB.ExecuteTable(sql, p)
        aDt.DefaultView.RowFilter = "answer_result='Y'"
        countScore = aDt.DefaultView.Count   'จำนวนข้อที่ตอบถูก
        aDt.DefaultView.RowFilter = ""

        Dim _ScorePercent As Integer = (countScore * 100) / countall
        Dim strdialogid As String = ""

        If _ScorePercent >= _TargetPercent Then
            Me.lblQ2T.Text = countScore
            Me.lblQ2A.Text = countall
            Me.lblQ2P.Text = _TargetPercent.ToString & "%"
            strdialogid = "dialogtrue"
        Else
            'strmsg += "เสียใจด้วยคุณยังสอบไม่ผ่านวิชานี้"

            Me.lblQ1T.Text = countScore
            Me.lblQ1A.Text = countall
            Me.lblQ1P.Text = _TargetPercent.ToString & "%"
            strdialogid = "dialogfalse"
        End If

        Dim AnswerData As String = ""
        AnswerData = "{" & Chr(34) & "testing_id" & Chr(34) & ":" & test_id & ","
        AnswerData += Chr(34) & "user_id" & Chr(34) & ":" & UserData.UserID & ","
        AnswerData += Chr(34) & "course_id" & Chr(34) & ":" & txtCourse_id.Text & ", "
        AnswerData += Chr(34) & "is_success" & Chr(34) & ":" & Chr(34) & "true" & Chr(34) & ", "
        AnswerData += Chr(34) & "target_percentage" & Chr(34) & ":" & _TargetPercent & ", "
        AnswerData += Chr(34) + "answer_data" + Chr(34) & ":[" & Environment.NewLine

        'dtTestQuestionResult.DefaultView.RowFilter = "test_id='" & test_id & "'"
        'Dim dtAPI As DataTable = dtTestQuestionResult.DefaultView.ToTable 'GetSqlDataTable(sql)

        Dim i As Integer = 0
        For Each qDr As DataRow In aDt.Rows
            If i > 0 Then
                AnswerData += ", "
            End If
            AnswerData += "{"
            AnswerData += Chr(34) + "question_id" + Chr(34) & ":" & qDr("tb_testing_question_id") & ", " & Environment.NewLine
            AnswerData += Chr(34) + "answer_id" + Chr(34) & ":" & qDr("answer_choice") & ", " & Environment.NewLine
            AnswerData += Chr(34) + "is_correct" + Chr(34) & ":" & Chr(34) & IIf(qDr("answer_result").ToString = "Y", "true", "false") & Chr(34) & ", " & Environment.NewLine
            AnswerData += Chr(34) + "time_spent" + Chr(34) & ":" & qDr("time_spent") & Environment.NewLine
            AnswerData += "}"

            i += 1
        Next
        AnswerData += "]"
        AnswerData += "}"

        Dim info As String = ""
        Dim strAction As String = "complete"
        Dim strModule As String = "testing"
        info = GetStringDataFromURL(GetWebServiceURL() & "api/log", UserData.Token & "&action=" & strAction & "&module=" & strModule & "&data=" & AnswerData)
        If info.Trim <> "" Then
            txtQuestion_Dialog.Text = strdialogid
        End If
    End Sub
    Private Sub btnSummary_Click(sender As Object, e As EventArgs) Handles btnSummary.ServerClick

        If Me.txtQuestion_no.Text <> "" Then

            'To Summary Screen
            Dim dt As DataTable = GetTesting(UserData.UserSessionID)
            Dim _TargetPercent As Integer = 0
            dt.DefaultView.RowFilter = "id='" & test_id & "'"
            If dt.DefaultView.Count > 0 Then
                _TargetPercent = dt.DefaultView.Table.Rows(0)("target_percentage")
            End If
            dt.DefaultView.RowFilter = ""

            Dim countall As Integer
            Dim qDt As DataTable = GetTestQuestion(test_id)
            countall = qDt.DefaultView.Count

            Dim countScore As Integer
            Dim dtTestQuestionResult As DataTable = Session("TestQuestionResult")
            dtTestQuestionResult.DefaultView.RowFilter = "test_id='" & test_id & "' and answer_result='true'"
            countScore = dtTestQuestionResult.DefaultView.Count



            Dim _ScorePercent As Integer = (countScore * 100) / countall

            Dim strdialogid As String = ""

            If _ScorePercent >= _TargetPercent Then
                Me.lblQ2T.Text = countScore
                Me.lblQ2A.Text = countall
                Me.lblQ2P.Text = _TargetPercent.ToString & "%"
                strdialogid = "dialogtrue"
            Else
                'strmsg += "เสียใจด้วยคุณยังสอบไม่ผ่านวิชานี้"

                Me.lblQ1T.Text = countScore
                Me.lblQ1A.Text = countall
                Me.lblQ1P.Text = _TargetPercent.ToString & "%"
                strdialogid = "dialogfalse"
            End If

            Dim AnswerData As String = ""
            AnswerData = "{" & Chr(34) & "testing_id" & Chr(34) & ":" & test_id & ","
            AnswerData += Chr(34) & "user_id" & Chr(34) & ":" & UserData.UserID & ","
            AnswerData += Chr(34) & "course_id" & Chr(34) & ":" & txtCourse_id.Text & ", "
            AnswerData += Chr(34) & "is_success" & Chr(34) & ":" & Chr(34) & "true" & Chr(34) & ", "
            AnswerData += Chr(34) & "target_percentage" & Chr(34) & ":" & _TargetPercent & ", "
            AnswerData += Chr(34) + "answer_data" + Chr(34) & ":[" & Environment.NewLine

            dtTestQuestionResult.DefaultView.RowFilter = "test_id='" & test_id & "'"
            Dim dtAPI As DataTable = dtTestQuestionResult.DefaultView.ToTable 'GetSqlDataTable(sql)

            For Each qDr As DataRow In dtAPI.Rows
                If qDr("question_no") > 1 Then
                    AnswerData += ", "
                End If
                AnswerData += "{"
                AnswerData += Chr(34) + "question_id" + Chr(34) & ":" & qDr("question_id") & ", " & Environment.NewLine
                AnswerData += Chr(34) + "answer_id" + Chr(34) & ":" & qDr("answer_id") & ", " & Environment.NewLine
                AnswerData += Chr(34) + "is_correct" + Chr(34) & ":" & Chr(34) & qDr("answer_result") & Chr(34) & ", " & Environment.NewLine
                AnswerData += Chr(34) + "time_spent" + Chr(34) & ":" & qDr("time_spent") & Environment.NewLine
                AnswerData += "}"
            Next
            AnswerData += "]"
            AnswerData += "}"

            'ScriptManager.RegisterStartupScript(Me, Page.GetType, "Script", "onSummary('" + strdialogid + "');", True)

            Dim info As String = ""
            Dim strAction As String = "complete"
            Dim strModule As String = "testing"
            info = GetStringDataFromURL(GetWebServiceURL() & "api/log", UserData.Token & "&client_id=" & 13 & "&action=" & strAction & "&module=" & strModule & "&data=" & AnswerData)
            If info.Trim <> "" Then
                ScriptManager.RegisterStartupScript(Me, Page.GetType, "Script", "onSummary('" + strdialogid + "');", True)
            End If

        End If
    End Sub

    Private Sub btnTest_Click(sender As Object, e As EventArgs) Handles btnTest.ServerClick


        ScriptManager.RegisterStartupScript(Me, Page.GetType, "Script", "onSummaryTestfalse();", True)



    End Sub

#End Region

End Class