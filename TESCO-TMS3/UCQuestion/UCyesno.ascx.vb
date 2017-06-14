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
        If dt.Rows(0)("icon_url") & "" <> "" Then
            lblImage2.Text = dt.Rows(0)("icon_url") & ""
        Else
        End If
        Dim tmpAnswer2() As String = Split(dt.Rows(0)("answer"), "##")
        Dim tmpChoice2() As String = Split(dt.Rows(0)("choice"), "##")
            If tmpChoice2.Length = 2 And tmpAnswer2.Length = 2 Then
                lblAnsYes.InnerText = tmpChoice2(0)
                lblAnsNo.InnerText = tmpChoice2(1)
            End If

    End Sub

    Private Sub btnAns_ServerClick(sender As Object, e As EventArgs) Handles btnAns.ServerClick
        Dim question_no As Double = txtQuestion_no.Text
        RaiseEvent btnAnsYESNOclick(sender, question_no)
    End Sub

End Class