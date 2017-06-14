Imports System.Data.SqlClient
Imports LinqDB.ConnectDB
Imports LinqDB.TABLE
Public Class UCyesno

    Inherits System.Web.UI.UserControl

    Public Event btnAnsYESNOclick(sender As Object, question_no As String)
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Public Sub SetTestQuestionYESNO(no As Integer, dt As DataTable)
        pnlQuestionYesNo.Visible = True
        Me.txtQuestion_no.Text = no.ToString
        If no = 1 Then
            Me.lblQNumber.Text = "ข้อ " + no.ToString + "/" + Me.txtQuestion_Count.Text
            Me.lblQDetail.Text = dt.Rows(0)("question_title") & ""
            If dt.Rows(0)("icon_url") & "" <> "" Then
                Me.imgQ.Src = dt.Rows(0)("icon_url") & ""
            End If
        Else

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
    End Sub

    Private Sub btnAns_ServerClick(sender As Object, e As EventArgs) Handles btnAns.ServerClick
        Dim question_no As Double = txtQuestion_no.Text
        RaiseEvent btnAnsYESNOclick(sender, question_no)
    End Sub

End Class