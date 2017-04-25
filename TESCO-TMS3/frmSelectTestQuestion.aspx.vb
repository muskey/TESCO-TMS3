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

    Public ReadOnly Property test_title As String
        Get
            Return Page.Request.QueryString("title") & ""
        End Get
    End Property
#End Region

#Region "Initail"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then

            Me.lblTitle.Text = "<h3>&nbsp>&nbsp<a href=""frmSelectTestCourse.aspx""><font color=""#019b79"">" + test_title + "&nbsp&nbsp</font></a></h3>"
            Me.ckbA.Attributes.Add("onclick", "onConfirmCheck(0)")
            Me.ckbB.Attributes.Add("onclick", "onConfirmCheck(1)")
            Me.ckbC.Attributes.Add("onclick", "onConfirmCheck(2)")
            Me.ckbD.Attributes.Add("onclick", "onConfirmCheck(3)")

            'Me.txtQuestion_no.Style.Add("display", "none")
            'Me.txtQuestion_Count.Style.Add("display", "none")
            'Me.txtCourse_id.Style.Add("display", "none")
            SetStartTest()
            SetTestQuestionCount()
            SetTestQuestion(1)
        End If

    End Sub
    Private Sub SetStartTest()
        UserData.TestSubject.DefaultView.RowFilter = "id='" & test_id & "'"
        If UserData.TestSubject.DefaultView.Count > 0 Then
            Me.txtCourse_id.Text = UserData.TestSubject.DefaultView.Table.Rows(0)("course_id")
        End If
        Session("teststarttime") = Date.Now

        For i As Integer = 0 To UserData.TestQuestion.Rows.Count - 1
            UserData.TestQuestion.Rows(i)("answer_id") = DBNull.Value
            UserData.TestQuestion.Rows(i)("answer_result") = DBNull.Value
            UserData.TestQuestion.Rows(i)("time_spent") = DBNull.Value
            UserData.TestQuestion.Rows(i)("answer_choice") = DBNull.Value
        Next
        Session("UserData") = UserData
    End Sub
    Private Sub SetTestQuestionCount()

        UserData.TestQuestion.DefaultView.RowFilter = "tb_test_id='" & test_id & "'"
        If UserData.TestQuestion.DefaultView.Count > 0 Then
            Me.txtQuestion_Count.Text = UserData.TestQuestion.DefaultView.Count
        End If


    End Sub
    Private Sub SetTestQuestion(no As Integer)
        Try
            ' Dim test_id_test As String = "1"
            Me.txtQuestion_no.Text = no.ToString
            ' Dim sql As String = " select * from TB_TESTING_QUESTION where test_id=" + test_id + " and question_no=" + no.ToString
            UserData.TestQuestion.DefaultView.RowFilter = "tb_test_id='" & test_id & "' and question_no=" + no.ToString

            Dim dt As DataTable = UserData.TestQuestion.DefaultView.ToTable 'GetSqlDataTable(sql)

            Dim str As String = ""
            If no = 1 Then
                If (dt.Rows.Count > 0) Then
                    Me.lblQNumber.Text = "ข้อ " + no.ToString + "/" + Me.txtQuestion_Count.Text
                    Me.lblQDetail.Text = dt.Rows(0)("question") & ""
                    If dt.Rows(0)("icon_url") & "" <> "" Then
                        Me.imgQ.Src = dt.Rows(0)("icon_url") & ""
                    End If

                    Dim tmpAnswer() As String = Split(dt.Rows(0)("answer"), "##")
                    Dim tmpChoice() As String = Split(dt.Rows(0)("choice"), "##")

                    If tmpChoice.Length = 4 And tmpAnswer.Length = 4 Then
                        lblA.InnerText = "ก. " + tmpChoice(0)
                        lblB.InnerText = "ข. " + tmpChoice(1)
                        lblC.InnerText = "ค. " + tmpChoice(2)
                        lblD.InnerText = "ง. " + tmpChoice(3)
                    End If
                End If
            Else
                SetTestQuestionBefor(no - 1)

                Me.lblQNumber2.Text = "ข้อ " + no.ToString + "/" + Me.txtQuestion_Count.Text
                Me.lblQDetail2.Text = dt.Rows(0)("question") & ""
                If dt.Rows(0)("icon_url") & "" <> "" Then
                    ' Me.imgQ.Src = dt.Rows(0)("icon_url") & ""
                    lblImage2.Text = dt.Rows(0)("icon_url") & ""
                End If
                Dim tmpAnswer2() As String = Split(dt.Rows(0)("answer"), "##")
                Dim tmpChoice2() As String = Split(dt.Rows(0)("choice"), "##")
                If tmpChoice2.Length = 4 And tmpAnswer2.Length = 4 Then
                    lblA2.InnerText = "ก. " + tmpChoice2(0)
                    lblB2.InnerText = "ข. " + tmpChoice2(1)
                    lblC2.InnerText = "ค. " + tmpChoice2(2)
                    lblD2.InnerText = "ง. " + tmpChoice2(3)
                End If
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub SetTestQuestionBefor(no As Integer)
        UserData.TestQuestion.DefaultView.RowFilter = "tb_test_id='" & test_id & "' and question_no=" + no.ToString

        Dim dt As DataTable = UserData.TestQuestion.DefaultView.ToTable 'GetSqlDataTable(sql)
        If (dt.Rows.Count > 0) Then
            Me.lblQNumber.Text = "ข้อ " + no.ToString + "/" + Me.txtQuestion_Count.Text
            Me.lblQDetail.Text = dt.Rows(0)("question") & ""
            If dt.Rows(0)("icon_url") & "" <> "" Then
                Me.imgQ.Src = dt.Rows(0)("icon_url") & ""
            End If

            Dim tmpAnswer() As String = Split(dt.Rows(0)("answer"), "##")
            Dim tmpChoice() As String = Split(dt.Rows(0)("choice"), "##")

            If tmpChoice.Length = 4 And tmpAnswer.Length = 4 Then
                lblA.InnerText = "ก. " + tmpChoice(0)
                lblB.InnerText = "ข. " + tmpChoice(1)
                lblC.InnerText = "ค. " + tmpChoice(2)
                lblD.InnerText = "ง. " + tmpChoice(3)
            End If

            Dim retchoice As Integer = Val(dt.Rows(0)("answer_choice") & "")

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
    End Sub

    Private Sub btnOK_Click(sender As Object, e As EventArgs) Handles btnOK.ServerClick


        If ckbA.Checked = False And ckbB.Checked = False And ckbC.Checked = False And ckbD.Checked = False Then
            ScriptManager.RegisterStartupScript(Me, Page.GetType, "Script", "onCheckSelect();", True)
            Exit Sub
        End If



        'Dim test_id_test As String = "1"
        If Me.txtQuestion_no.Text <> "" Then
            UserData.TestQuestion.DefaultView.RowFilter = "tb_test_id='" & test_id & "' and question_no=" + Me.txtQuestion_no.Text
            Dim dt As DataTable = UserData.TestQuestion.DefaultView.ToTable 'GetSqlDataTable(sql)
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
                        retTrue = "ก. " + tmpChoice(0)
                    ElseIf tmpAnswer(1) = "True" Then
                        retTrue = "ข. " + tmpChoice(1)
                    ElseIf tmpAnswer(2) = "True" Then
                        retTrue = "ค. " + tmpChoice(2)
                    ElseIf tmpAnswer(3) = "True" Then
                        retTrue = "ง. " + tmpChoice(3)
                    End If
                End If

                Dim StartTime As DateTime = Session("teststarttime")
                For i As Integer = 0 To UserData.TestQuestion.Rows.Count - 1
                    If UserData.TestQuestion.Rows(i)("question_no") = Me.txtQuestion_no.Text And UserData.TestQuestion.Rows(i)("tb_test_id") = test_id Then
                        UserData.TestQuestion.Rows(i)("answer_id") = retId
                        UserData.TestQuestion.Rows(i)("answer_result") = ret.ToString.ToLower
                        UserData.TestQuestion.Rows(i)("time_spent") = DateDiff(DateInterval.Second, StartTime, DateTime.Now)
                        UserData.TestQuestion.Rows(i)("answer_choice") = retChoice
                    End If
                Next

                Session("UserData") = UserData

                'ScriptManager.RegisterStartupScript(Me, Page.GetType, "Script", "onConfirmTest('" + ret.ToString + "','" & retTrue & "','" & Me.txtQuestion_no.Text & "');", True)

                'ckbA.Checked = False
                'ckbB.Checked = False
                'ckbC.Checked = False
                'ckbD.Checked = False

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

                ScriptManager.RegisterStartupScript(Me, Page.GetType, "Script", "onConfirmTest('" + ret.ToString + "','" & retTrue & "','" & Me.txtQuestion_no.Text & "','" & lastchoice.ToString & "');", True)

            End If
        End If
    End Sub

    'Private Sub btnAnswer_Click(sender As Object, e As EventArgs) Handles btnAnswer.Click
    '    If ckbA.Checked = False And ckbB.Checked = False And ckbC.Checked = False And ckbD.Checked = False Then
    '        ScriptManager.RegisterStartupScript(Me, Page.GetType, "Script", "onCheckSelect();", True)
    '        Exit Sub
    '    End If

    '    'Dim test_id_test As String = "1"
    '    If Me.txtQuestion_no.Text <> "" Then
    '        UserData.TestQuestion.DefaultView.RowFilter = "tb_test_id='" & test_id & "' and question_no=" + Me.txtQuestion_no.Text
    '        Dim dt As DataTable = UserData.TestQuestion.DefaultView.ToTable 'GetSqlDataTable(sql)
    '        If dt.Rows.Count > 0 Then
    '            Dim ret As Boolean = False
    '            Dim retTrue As String = ""
    '            Dim retId As String = ""
    '            Dim tmpAnswer() As String = Split(dt.Rows(0)("answer"), "##")
    '            retId = dt.Rows(0)("id")
    '            If tmpAnswer.Length = 4 Then
    '                If ckbA.Checked Then
    '                    If tmpAnswer(0) = "True" Then
    '                        ret = True
    '                    End If
    '                ElseIf ckbB.Checked Then
    '                    If tmpAnswer(1) = "True" Then
    '                        ret = True
    '                    End If
    '                ElseIf ckbC.Checked Then
    '                    If tmpAnswer(2) = "True" Then
    '                        ret = True
    '                    End If
    '                ElseIf ckbD.Checked Then
    '                    If tmpAnswer(3) = "True" Then
    '                        ret = True
    '                    End If
    '                End If

    '                If tmpAnswer(0) = "True" Then
    '                    retTrue = lblA.InnerText
    '                ElseIf tmpAnswer(1) = "True" Then
    '                    retTrue = lblB.InnerText
    '                ElseIf tmpAnswer(2) = "True" Then
    '                    retTrue = lblC.InnerText
    '                ElseIf tmpAnswer(3) = "True" Then
    '                    retTrue = lblD.InnerText
    '                End If
    '            End If

    '            Dim StartTime As DateTime = Session("teststarttime")
    '            For i As Integer = 0 To UserData.TestQuestion.Rows.Count - 1
    '                If UserData.TestQuestion.Rows(i)("question_no") = Me.txtQuestion_no.Text And UserData.TestQuestion.Rows(i)("tb_test_id") = test_id Then
    '                    UserData.TestQuestion.Rows(i)("answer_id") = retId
    '                    UserData.TestQuestion.Rows(i)("answer_result") = ret.ToString.ToLower
    '                    UserData.TestQuestion.Rows(i)("time_spent") = DateDiff(DateInterval.Second, StartTime, DateTime.Now)
    '                End If
    '            Next

    '            Session("UserData") = UserData


    '            ckbA.Checked = False
    '            ckbB.Checked = False
    '            ckbC.Checked = False
    '            ckbD.Checked = False

    '            Me.txtQuestion_no.Text = Val(Me.txtQuestion_no.Text) + 1




    '            If Val(Me.txtQuestion_no.Text) <= Val(Me.txtQuestion_Count.Text) Then
    '                SetTestQuestion(Val(Me.txtQuestion_no.Text))
    '            Else
    '                btnOK.Visible = False
    '                btnSummary.Visible = True
    '            End If

    '            ScriptManager.RegisterStartupScript(Me, Page.GetType, "Script", "onConfirmTest('" + ret.ToString + "','" & retTrue & "');", True)

    '        End If
    '    End If
    'End Sub
    Private Sub Sumnary()
        'To Summary Screen
        Dim _TargetPercent As Integer = 0
        UserData.TestSubject.DefaultView.RowFilter = "id='" & test_id & "'"
        If UserData.TestSubject.DefaultView.Count > 0 Then
            _TargetPercent = UserData.TestSubject.DefaultView.Table.Rows(0)("target_percentage")
        End If

        Dim countall As Integer
        UserData.TestQuestion.DefaultView.RowFilter = "tb_test_id='" & test_id & "'"
        countall = UserData.TestQuestion.DefaultView.Count

        Dim countScore As Integer
        UserData.TestQuestion.DefaultView.RowFilter = "tb_test_id='" & test_id & "' and answer_result='true'"
        countScore = UserData.TestQuestion.DefaultView.Count



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

        UserData.TestQuestion.DefaultView.RowFilter = "tb_test_id='" & test_id & "'"
        Dim dtAPI As DataTable = UserData.TestQuestion.DefaultView.ToTable 'GetSqlDataTable(sql)

        For Each qDr As DataRow In dtAPI.Rows
            If qDr("question_no") > 1 Then
                AnswerData += ", "
            End If
            AnswerData += "{"
            AnswerData += Chr(34) + "question_id" + Chr(34) & ":" & qDr("id") & ", " & Environment.NewLine
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
            txtQuestion_Dialog.Text = strdialogid
        End If
    End Sub
    Private Sub btnSummary_Click(sender As Object, e As EventArgs) Handles btnSummary.ServerClick

        If Me.txtQuestion_no.Text <> "" Then

            'To Summary Screen
            Dim _TargetPercent As Integer = 0
            UserData.TestSubject.DefaultView.RowFilter = "id='" & test_id & "'"
            If UserData.TestSubject.DefaultView.Count > 0 Then
                _TargetPercent = UserData.TestSubject.DefaultView.Table.Rows(0)("target_percentage")
            End If

            Dim countall As Integer
            UserData.TestQuestion.DefaultView.RowFilter = "tb_test_id='" & test_id & "'"
            countall = UserData.TestQuestion.DefaultView.Count

            Dim countScore As Integer
            UserData.TestQuestion.DefaultView.RowFilter = "tb_test_id='" & test_id & "' and answer_result='true'"
            countScore = UserData.TestQuestion.DefaultView.Count



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

            UserData.TestQuestion.DefaultView.RowFilter = "tb_test_id='" & test_id & "'"
            Dim dtAPI As DataTable = UserData.TestQuestion.DefaultView.ToTable 'GetSqlDataTable(sql)

            For Each qDr As DataRow In dtAPI.Rows
                If qDr("question_no") > 1 Then
                    AnswerData += ", "
                End If
                AnswerData += "{"
                AnswerData += Chr(34) + "question_id" + Chr(34) & ":" & qDr("id") & ", " & Environment.NewLine
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