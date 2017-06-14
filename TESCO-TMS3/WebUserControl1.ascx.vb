Imports System.Data.SqlClient
Imports LinqDB.ConnectDB
Imports LinqDB.TABLE
Public Class WebUserControl1
    Inherits System.Web.UI.UserControl
    Public Event btnAnsABCDclick(sender As Object, question_no As String)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub


    Public Sub setCol_rpt(test_id As String, question_no As Double, dt As DataTable)
        Dim dt_col As String = test_id
        Try
            Me.txtQuestion_no.Text = question_no.ToString
            Dim tbc As DataTable = GetTestQuestion(dt.Rows(0)("tb_testing_id"))
            txtQuestion_Count.Text = tbc.Rows.Count
            pnlQuestionABCD.Visible = True
            Dim str As String = ""
            If question_no = 1 Then
                If (dt.Rows.Count > 0) Then
                    Me.lblQNumber.Text = "ข้อ " + question_no.ToString + "/" + Me.txtQuestion_Count.Text
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
                SetTestQuestionBefor1(test_id, question_no)

                Me.lblQNumber2.Text = "ข้อ " + question_no.ToString + "/" + Me.txtQuestion_Count.Text
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

    Private Sub SetTestQuestionBefor1(test_id As Integer, no As Integer)
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

    Private Sub btnAns_ServerClick(sender As Object, e As EventArgs) Handles btnAns.ServerClick
        Dim question_no As Double = txtQuestion_no.Text
        RaiseEvent btnAnsABCDclick(sender, question_no)
    End Sub
End Class