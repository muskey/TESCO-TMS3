Imports System.Data.SqlClient
Imports LinqDB.ConnectDB
Imports LinqDB.TABLE
Public Class UCyesno

    Inherits System.Web.UI.UserControl

    '    Public Event btnAnsYESNOclick(sender As Object, question_no As String)
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.chkAnsYes.Attributes.Item("onclick") = "onConfirmCheck(0)"
        Me.chkAnsNo.Attributes.Item("onclick") = "onConfirmCheck(1)"
    End Sub

    Public Sub SetTestQuestionYESNO(test_id As Integer, question_no As Integer, QuestionQty As Integer, ShowAnswer As String)
        txtTestID.Text = test_id
        txtShowAnswer.Text = ShowAnswer
        pnlQuestionYesNo.Visible = True
        Me.txtQuestion_no.Text = question_no
        Dim dt As DataTable = GetTestQuestion(test_id)
        Me.lblQNumber.Text = "ข้อ " + question_no.ToString + "/" + QuestionQty.ToString
        Me.lblQDetail.Text = dt.Rows(0)("question_title") & ""

        txtCorrectAnswer.Text = dt.Rows(0)("yesno_correct_answer")
        txtCorrectChoice.Text = IIf(dt.Rows(0)("yesno_correct_answer") = 1, "ใช่", "ไม่ใช่")
    End Sub

    Private Sub btnAns_ServerClick(sender As Object, e As EventArgs) Handles btnAns.ServerClick

        Dim isCorrect As Boolean = False
        If chkAnsYes.Checked = True Then
            If txtCorrectAnswer.Text = 1 Then
                isCorrect = True
            End If
        ElseIf chkAnsNo.Checked Then
            If txtCorrectAnswer.Text = 0 Then
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
        pnlAnsResult.Visible = True
    End Sub

    Private Sub btnNext_Click(sender As Object, e As EventArgs) Handles btnNext.Click
        'RaiseEvent btnAnsYESNOclick(sender, txtQuestion_no.Text)
        Response.Redirect("frmSelectQuestionTest.aspx?id=" & txtTestID.Text & "&q_id=" & (Convert.ToInt16(txtQuestion_no.Text) + 1))
    End Sub

    Private Sub btnCloseDialog_Click(sender As Object, e As EventArgs) Handles btnCloseDialog.Click
        pnlAnsResult.Visible = False
    End Sub
End Class