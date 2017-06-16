Imports System.Data.SqlClient
Imports LinqDB.ConnectDB
Imports LinqDB.TABLE
Public Class frmSelectQuestionTest
    Inherits System.Web.UI.Page

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
    Public ReadOnly Property q_id As String
        Get
            Return Page.Request.QueryString("q_id")
        End Get
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Dim lnq As TbTestingLinqDB = GetTestingData(test_id)
            If lnq.ID > 0 Then
                Me.lblTestTitle.Text = "<h3>&nbsp>&nbsp<a href=""frmSelectTestCourse.aspx""><font color=""#019b79"">" + lnq.TEST_TITLE + "&nbsp&nbsp</font></a></h3>"
                lblIsShowAnswer.Text = lnq.IS_SHOW_ANSWER
                lblQuestionQty.Text = lnq.QUESTION_QTY
                SetStartTest()
            End If
        End If
    End Sub

    Private Sub SetStartTest()
        'Dim dt As DataTable = GetTesting(UserData.UserSessionID)
        'dt.DefaultView.RowFilter = "id='" & test_id & "'"

        If q_id = 1 Then
            'ถ้าเป็นข้อแรก ให้ลบคำตอบที่เคยตอบแล้ว เพื่อเริ่มทำข้อสอบใหม่
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
        End If

        Session("teststarttime") = DateTime.Now
        Dim qDt As DataTable = GetTestQuestion(test_id, q_id)
        If qDt.Rows.Count > 0 Then
            UCabcd1.Visible = False
            UCyesno.Visible = False
            UCMaching.Visible = False
            UCWriting.Visible = False
            UCPicture.Visible = False

            Select Case qDt.Rows(0)("question_type").ToString.ToLower
                Case "abcd"
                    Dim question_number As Double = qDt.Rows(0)("question_no").ToString()
                    Me.UCabcd1.setTestQuestionABCD(test_id, question_number, lblQuestionQty.Text, lblIsShowAnswer.Text)
                    UCabcd1.Visible = True
                Case "yes/no"
                    Dim question_number As Double = qDt.Rows(0)("question_no").ToString()
                    Me.UCyesno.SetTestQuestionYESNO(test_id, question_number, lblQuestionQty.Text, lblIsShowAnswer.Text)
                    UCyesno.Visible = True
                Case "matching"
                    Dim question_number As Double = qDt.Rows(0)("question_no").ToString()
                    Me.UCMaching.SetTestQuestionMatching(test_id, question_number, lblQuestionQty.Text, lblIsShowAnswer.Text)
                    UCMaching.Visible = True
                Case "writing"
                    Dim question_number As Double = qDt.Rows(0)("question_no").ToString()
                    Me.UCWriting.SetTestQuestionWriting(test_id, question_number, lblQuestionQty.Text, lblIsShowAnswer.Text)
                    UCWriting.Visible = True
                Case "picture"
                    Dim question_number As Double = qDt.Rows(0)("question_no").ToString()
                    Me.UCPicture.SetTestQuestionPicture(test_id, question_number, lblQuestionQty.Text, lblIsShowAnswer.Text)
                    UCPicture.Visible = True
            End Select
        End If
    End Sub

End Class