Imports LinqDB.ConnectDB
Imports LinqDB.TABLE

Public Class UCWriting
    Inherits System.Web.UI.UserControl

    Public ReadOnly Property UserData As UserProfileData
        Get
            Return Session("UserData")
        End Get
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Public Sub SetTestQuestionWriting(test_id As Integer, question_no As Integer, QuestionQty As Integer, ShowAnswer As String)
        txtTestID.Text = test_id
        pnlQuestionWriting.Visible = True
        Me.txtQuestion_no.Text = question_no.ToString

        Dim dt As DataTable = GetTestQuestion(test_id, question_no)

        Me.lblQNumber.Text = "ข้อ " + question_no.ToString + "/" + QuestionQty.ToString
        Me.lblQDetail.Text = dt.Rows(0)("question_title") & ""
        txtQuestionID.Text = dt.Rows(0)("id")
        If dt.Rows(0)("icon_url") & "" <> "" Then
            lblImage2.Text = dt.Rows(0)("icon_url") & ""

            links.InnerHtml = "<a href='" & dt.Rows(0)("icon_url") & "' title='' data-gallery='' >"
            links.InnerHtml += "    <img  src='" & dt.Rows(0)("icon_url") & "' style='width:200px;height:200px;' />"
            links.InnerHtml += "</a>"
            '    img1.Src = dt.Rows(0)("icon_url")
            '    likImage1.HRef = dt.Rows(0)("icon_url")
            'Else
            '    likExtras.Attributes.Add("style", "display:none")
        End If
    End Sub

    Private Sub btnAns_ServerClick(sender As Object, e As EventArgs) Handles btnAns.ServerClick
        'LogFileBL.LogTrans(UserData.LoginHistoryID, "คลิกปุ่มตอบ")
        If ValidateData() = True Then

            'LogFileBL.LogTrans(UserData.LoginHistoryID, "คำตอบคือ" & txtAnsQuestion4.Text)
            Dim TimeSpen As Integer = DateDiff(DateInterval.Second, Convert.ToDateTime(Session("teststarttime")), DateTime.Now)
            Dim trans As New TransactionDB
            Dim ret As ExecuteDataInfo = SaveTestAnswerWriting(UserData.UserName, trans, txtTestID.Text, txtQuestionID.Text, TimeSpen, txtAnsQuestion4.Text)
            If ret.IsSuccess = True Then
                trans.CommitTransaction()
            Else
                trans.RollbackTransaction()
            End If

            Response.Redirect("frmSelectQuestionTest.aspx?id=" & txtTestID.Text & "&q_id=" & (Convert.ToInt16(txtQuestion_no.Text) + 1))
        End If
    End Sub

    Private Function ValidateData()
        If txtAnsQuestion4.Text.Trim = "" Then
            ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "Alert", "alert('กรุณาเขียนคำตอบ');", True)
            Return False
        End If

        Return True
    End Function

End Class