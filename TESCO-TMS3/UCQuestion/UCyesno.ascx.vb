Imports System.Data.SqlClient
Imports LinqDB.ConnectDB
Imports LinqDB.TABLE
Public Class UCyesno

    Inherits System.Web.UI.UserControl

    Public Event btnAnsYESNOclick(sender As Object, question_no As String)
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Public Sub SetTestQuestionYESNO(no As Integer, question_number As Double, dt As DataTable)
        pnlQuestionYesNo.Visible = True
        Me.txtQuestion_no.Text = question_number.ToString
        Dim tbc As DataTable = GetTestQuestion(dt.Rows(0)("tb_testing_id"))
        txtQuestion_Count.Text = tbc.Rows.Count
        Me.lblQNumber.Text = "ข้อ " + question_number.ToString + "/" + Me.txtQuestion_Count.Text
            Me.lblQDetail.Text = dt.Rows(0)("question_title") & ""
        'If dt.Rows(0)("icon_url") & "" <> "" Then
        '    lblImage2.Text = dt.Rows(0)("icon_url") & ""
        'Else
        'End If
        'Dim tmpAnswer2() As String = Split(dt.Rows(0)("answer"), "##")
        'Dim tmpChoice2() As String = Split(dt.Rows(0)("choice"), "##")
        'If tmpChoice2.Length = 2 And tmpAnswer2.Length = 2 Then
        '    lblAnsYes.InnerText = tmpChoice2(0)
        '    lblAnsNo.InnerText = tmpChoice2(1)

        '    Dim i As Integer = 0
        '    For Each ans As String In tmpAnswer2
        '        If ans.ToLower = "true" Then

        '            i += 1
        '        End If
        '    Next
        'End If

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
        RaiseEvent btnAnsYESNOclick(sender, txtQuestion_no.Text)
    End Sub

    Private Sub btnCloseDialog_Click(sender As Object, e As EventArgs) Handles btnCloseDialog.Click
        pnlAnsResult.Visible = False
    End Sub
End Class