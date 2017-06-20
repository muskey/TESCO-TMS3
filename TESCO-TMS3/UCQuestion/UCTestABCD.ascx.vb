Imports System.Data.SqlClient
Imports LinqDB.ConnectDB
Imports LinqDB.TABLE
Public Class UCTestABCD
    Inherits System.Web.UI.UserControl
    Public ReadOnly Property UserData As UserProfileData
        Get
            Return Session("UserData")
        End Get
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.ckbA.Attributes.Item("onclick") = "onConfirmCheck(0)"
        Me.ckbB.Attributes.Item("onclick") = "onConfirmCheck(1)"
        Me.ckbC.Attributes.Item("onclick") = "onConfirmCheck(2)"
        Me.ckbD.Attributes.Item("onclick") = "onConfirmCheck(3)"
    End Sub


    Public Sub setTestQuestionABCD(test_id As String, question_no As Double, QuestionCount As Integer, ShowAnswer As String)
        txtTestID.Text = test_id
        txtShowAnswer.Text = ShowAnswer
        Try
            Me.txtQuestion_no.Text = question_no.ToString
            Dim dt As DataTable = GetTestQuestion(test_id, question_no)

            txtQuestionID.Text = dt.Rows(0)("id")
            pnlQuestionABCD.Visible = True
            Dim str As String = ""
            If (dt.Rows.Count > 0) Then
                Me.lblQNumber.Text = "ข้อ " + question_no.ToString + "/" + QuestionCount.ToString
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

                    Dim i As Integer = 0
                    For Each ans As String In tmpAnswer
                        If ans.ToLower = "true" Then
                            txtCorrectAnswer.Text = i
                            txtCorrectChoice.Text = tmpChoice(i)
                            i += 1
                        End If
                    Next
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnAns_ServerClick(sender As Object, e As EventArgs) Handles btnAns.ServerClick
        LogFileBL.LogTrans(UserData.LoginHistoryID, "คลิกปุ่มตอบ")

        If ValidateData() = True Then
            Dim isCorrect As Boolean = False
            Dim AnswerChoice As Integer = -1
            If ckbA.Checked = True Then
                AnswerChoice = 0
                If txtCorrectAnswer.Text = 0 Then
                    isCorrect = True
                End If
            ElseIf ckbB.Checked Then
                AnswerChoice = 1
                If txtCorrectAnswer.Text = 1 Then
                    isCorrect = True
                End If
            ElseIf ckbC.Checked = True Then
                AnswerChoice = 2
                If txtCorrectAnswer.Text = 2 Then
                    isCorrect = True
                End If
            ElseIf ckbD.Checked = True Then
                AnswerChoice = 3
                If txtCorrectAnswer.Text = 3 Then
                    isCorrect = True
                End If
            End If

            Dim TimeSpen As Integer = DateDiff(DateInterval.Second, Convert.ToDateTime(Session("teststarttime")), DateTime.Now)
            Dim trans As New TransactionDB
            Dim ret As ExecuteDataInfo = SaveTestAnswer(UserData.UserName, trans, txtTestID.Text, txtQuestionID.Text, TimeSpen, AnswerChoice, IIf(isCorrect = True, "Y", "N"))
            If ret.IsSuccess = True Then
                trans.CommitTransaction()
            Else
                trans.RollbackTransaction()
            End If

            Dim LogMsg As String = "ตอบคำถาม " & lblQNumber.Text & " " & lblQDetail.Text
            If isCorrect = True Then
                lblDialogHead.Text = "ยินดีด้วย"
                litAnsDetail.Text = "<h2>คุณตอบถูก</h2>"
                LogMsg += " ตอบถูก"
            Else
                lblDialogHead.Text = "คุณตอบผิด"
                divHeader.Attributes.Remove("style")
                divHeader.Attributes.Add("style", "background:red")

                litAnsDetail.Text = "<font color='#019b79'><h4>คำตอบที่ถูกคือ<h4></font>"
                litAnsDetail.Text += "<font color='#019b79'><h4>" + txtCorrectChoice.Text + "</h4></font>"
                LogMsg += " ตอบผิด"
            End If

            LogFileBL.LogTrans(UserData.LoginHistoryID, LogMsg)
            If txtShowAnswer.Text = "Y" Then
                pnlAnsResult.Visible = True
            Else
                Response.Redirect("frmSelectQuestionTest.aspx?id=" & txtTestID.Text & "&q_id=" & (Convert.ToInt16(txtQuestion_no.Text) + 1))
            End If
        End If
    End Sub

    Private Function ValidateData() As Boolean
        If ckbA.Checked = False And ckbB.Checked = False And ckbC.Checked = False And ckbD.Checked = False Then
            ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "Alert", "alert('กรุณาเลือกคำตอบ');", True)
            Return False
        End If

        Return True
    End Function

    Private Sub btnCloseDialog_Click(sender As Object, e As EventArgs) Handles btnCloseDialog.Click
        LogFileBL.LogTrans(UserData.LoginHistoryID, "คลิกปุ่ม ปิด")
        pnlAnsResult.Visible = False
    End Sub

    Private Sub btnNext_Click(sender As Object, e As EventArgs) Handles btnNext.Click
        LogFileBL.LogTrans(UserData.LoginHistoryID, "คลิกปุ่ม ต่อไป")
        Response.Redirect("frmSelectQuestionTest.aspx?id=" & txtTestID.Text & "&q_id=" & (Convert.ToInt16(txtQuestion_no.Text) + 1))
    End Sub
End Class