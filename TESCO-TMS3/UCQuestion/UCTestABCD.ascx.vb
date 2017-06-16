Imports System.Data.SqlClient
Imports LinqDB.ConnectDB
Imports LinqDB.TABLE
Public Class UCTestABCD
    Inherits System.Web.UI.UserControl
    'Public Event btnAnsABCDclick(sender As Object, question_no As String)

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
        Dim isCorrect As Boolean = False
        If ckbA.Checked = True Then
            If txtCorrectAnswer.Text = 0 Then
                isCorrect = True
            End If
        ElseIf ckbB.Checked Then
            If txtCorrectAnswer.Text = 1 Then
                isCorrect = True
            End If
        ElseIf ckbC.Checked = True Then
            If txtCorrectAnswer.Text = 2 Then
                isCorrect = True
            End If
        ElseIf ckbD.Checked = True Then
            If txtCorrectAnswer.Text = 3 Then
                isCorrect = True
            End If
        End If

        If isCorrect = True Then
            lblDialogHead.Text = "ยินดีด้วย"
            litAnsDetail.Text = "<h2>คุณตอบถูก</h2>"
        Else
            lblDialogHead.Text = "คุณตอบผิด"
            divHeader.Attributes.Remove("style")
            divHeader.Attributes.Add("style", "background:red")

            litAnsDetail.Text = "<font color='#019b79'><h4>คำตอบที่ถูกคือ<h4></font>"
            litAnsDetail.Text += "<font color='#019b79'><h4>" + txtCorrectChoice.Text + "</h4></font>"
        End If

        If txtShowAnswer.Text = "Y" Then
            pnlAnsResult.Visible = True
        Else
            Response.Redirect("frmSelectQuestionTest.aspx?id=" & txtTestID.Text & "&q_id=" & (Convert.ToInt16(txtQuestion_no.Text) + 1))
        End If

    End Sub

    Private Sub btnCloseDialog_Click(sender As Object, e As EventArgs) Handles btnCloseDialog.Click
        pnlAnsResult.Visible = False
    End Sub

    Private Sub btnNext_Click(sender As Object, e As EventArgs) Handles btnNext.Click
        'RaiseEvent btnAnsABCDclick(sender, txtQuestion_no.Text)

        Response.Redirect("frmSelectQuestionTest.aspx?id=" & txtTestID.Text & "&q_id=" & (Convert.ToInt16(txtQuestion_no.Text) + 1))
    End Sub
End Class