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

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Dim dt As DataTable = GetTesting(UserData.UserSessionID)
            dt.DefaultView.RowFilter = "id=" & test_id

            If dt.DefaultView.Count > 0 Then
                Me.lblTestTitle.Text = "<h3>&nbsp>&nbsp<a href=""frmSelectTestCourse.aspx""><font color=""#019b79"">" + dt.DefaultView(0)("test_title").ToString + "&nbsp&nbsp</font></a></h3>"

                SetStartTest()
            End If
            dt.DefaultView.RowFilter = ""
        End If
    End Sub

    Private Sub SetStartTest()
        Dim dt As DataTable = GetTesting(UserData.UserSessionID)
        dt.DefaultView.RowFilter = "id='" & test_id & "'"

        Session("teststarttime") = DateTime.Now

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

        Dim qDt As DataTable = GetTestQuestion(test_id, 1)

        If qDt.Rows.Count > 0 Then

            Select Case qDt.Rows(0)("question_type").ToString.ToLower
                Case "abcd"
                    Dim question_number As Double = qDt.Rows(0)("question_no").ToString()
                    Me.UCabcd1.setCol_rpt(test_id, question_number, qDt)
                    UCabcd1.Visible = True
                    UCyesno.Visible = False
                    UCMaching.Visible = False
                    UCWriting.Visible = False
                    UCPicture.Visible = False
                Case "yes/no"
                    Dim question_number As Double = qDt.Rows(0)("question_no").ToString()
                    Me.UCyesno.SetTestQuestionYESNO(test_id, question_number, qDt)
                    UCabcd1.Visible = False
                    UCyesno.Visible = True
                    UCMaching.Visible = False
                    UCWriting.Visible = False
                    UCPicture.Visible = False
                Case "matching"
                    Dim question_number As Double = qDt.Rows(0)("question_no").ToString()
                    Me.UCMaching.SetTestQuestionMatching(test_id, question_number, qDt)
                    UCabcd1.Visible = False
                    UCyesno.Visible = False
                    UCMaching.Visible = True
                    UCWriting.Visible = False
                    UCPicture.Visible = False
                Case "writing"
                    Dim question_number As Double = qDt.Rows(0)("question_no").ToString()
                    Me.UCWriting.SetTestQuestionWriting(test_id, question_number, qDt)
                    UCabcd1.Visible = False
                    UCyesno.Visible = False
                    UCMaching.Visible = False
                    UCWriting.Visible = True
                    UCPicture.Visible = False
                Case "picture"
                    Dim question_number As Double = qDt.Rows(0)("question_no").ToString()
                    Me.UCPicture.SetTestQuestionPicture(test_id, question_number, qDt)
                    UCabcd1.Visible = False
                    UCyesno.Visible = False
                    UCMaching.Visible = False
                    UCWriting.Visible = False
                    UCPicture.Visible = True
            End Select
        End If
    End Sub

    Private Sub WebUserControl1_btnAnsABCDclick(sender As Object, question_no As Double) Handles UCabcd1.btnAnsABCDclick

        Dim lastchoice As Integer = 0
        Dim qDt As DataTable = GetTestQuestion(test_id, question_no + 1)

        If qDt.Rows.Count > 0 Then

            Select Case qDt.Rows(0)("question_type").ToString.ToLower
                Case "abcd"
                    Dim question_number As Double = qDt.Rows(0)("question_no").ToString()
                    Me.UCabcd1.setCol_rpt(test_id, question_number, qDt)
                    UCabcd1.Visible = True
                    UCyesno.Visible = False
                    UCMaching.Visible = False
                    UCWriting.Visible = False
                    UCPicture.Visible = False
                Case "yes/no"
                    Dim question_number As Double = qDt.Rows(0)("question_no").ToString()
                    Me.UCyesno.SetTestQuestionYESNO(test_id, question_number, qDt)
                    UCabcd1.Visible = False
                    UCyesno.Visible = True
                    UCMaching.Visible = False
                    UCWriting.Visible = False
                    UCPicture.Visible = False
                Case "matching"
                    Dim question_number As Double = qDt.Rows(0)("question_no").ToString()
                    Me.UCMaching.SetTestQuestionMatching(test_id, question_number, qDt)
                    UCabcd1.Visible = False
                    UCyesno.Visible = False
                    UCMaching.Visible = True
                    UCWriting.Visible = False
                    UCPicture.Visible = False
                Case "writing"
                    Dim question_number As Double = qDt.Rows(0)("question_no").ToString()
                    Me.UCWriting.SetTestQuestionWriting(test_id, question_number, qDt)
                    UCabcd1.Visible = False
                    UCyesno.Visible = False
                    UCMaching.Visible = False
                    UCWriting.Visible = True
                    UCPicture.Visible = False
                Case "picture"
                    Dim question_number As Double = qDt.Rows(0)("question_no").ToString()
                    Me.UCPicture.SetTestQuestionPicture(test_id, question_number, qDt)
                    UCabcd1.Visible = False
                    UCyesno.Visible = False
                    UCMaching.Visible = False
                    UCWriting.Visible = False
                    UCPicture.Visible = True
            End Select
        End If
    End Sub

    Private Sub UCMaching_btnAnsMACHINGclick(sender As Object, question_no As Double) Handles UCMaching.btnAnsMACHINGclick

        Dim lastchoice As Integer = 0
        Dim qDt As DataTable = GetTestQuestion(test_id, question_no + 1)

        If qDt.Rows.Count > 0 Then

            Select Case qDt.Rows(0)("question_type").ToString.ToLower
                Case "abcd"
                    Dim question_number As Double = qDt.Rows(0)("question_no").ToString()
                    Me.UCabcd1.setCol_rpt(test_id, question_number, qDt)
                    UCabcd1.Visible = True
                    UCyesno.Visible = False
                    UCMaching.Visible = False
                    UCWriting.Visible = False
                    UCPicture.Visible = False
                Case "yes/no"
                    Dim question_number As Double = qDt.Rows(0)("question_no").ToString()
                    Me.UCyesno.SetTestQuestionYESNO(test_id, question_number, qDt)
                    UCabcd1.Visible = False
                    UCyesno.Visible = True
                    UCMaching.Visible = False
                    UCWriting.Visible = False
                    UCPicture.Visible = False
                Case "matching"
                    Dim question_number As Double = qDt.Rows(0)("question_no").ToString()
                    Me.UCMaching.SetTestQuestionMatching(test_id, question_number, qDt)
                    UCabcd1.Visible = False
                    UCyesno.Visible = False
                    UCMaching.Visible = True
                    UCWriting.Visible = False
                    UCPicture.Visible = False
                Case "writing"
                    Dim question_number As Double = qDt.Rows(0)("question_no").ToString()
                    Me.UCWriting.SetTestQuestionWriting(test_id, question_number, qDt)
                    UCabcd1.Visible = False
                    UCyesno.Visible = False
                    UCMaching.Visible = False
                    UCWriting.Visible = True
                    UCPicture.Visible = False
                Case "picture"
                    Dim question_number As Double = qDt.Rows(0)("question_no").ToString()
                    Me.UCPicture.SetTestQuestionPicture(test_id, question_number, qDt)
                    UCabcd1.Visible = False
                    UCyesno.Visible = False
                    UCMaching.Visible = False
                    UCWriting.Visible = False
                    UCPicture.Visible = True
            End Select
        End If
    End Sub

    Private Sub UCPicture_btnAnsPICTUREclick(sender As Object, question_no As Double) Handles UCPicture.btnAnsPICTUREclick

        Dim lastchoice As Integer = 0
        Dim qDt As DataTable = GetTestQuestion(test_id, question_no + 1)

        If qDt.Rows.Count > 0 Then

            Select Case qDt.Rows(0)("question_type").ToString.ToLower
                Case "abcd"
                    Dim question_number As Double = qDt.Rows(0)("question_no").ToString()
                    Me.UCabcd1.setCol_rpt(test_id, question_number, qDt)
                    UCabcd1.Visible = True
                    UCyesno.Visible = False
                    UCMaching.Visible = False
                    UCWriting.Visible = False
                    UCPicture.Visible = False
                Case "yes/no"
                    Dim question_number As Double = qDt.Rows(0)("question_no").ToString()
                    Me.UCyesno.SetTestQuestionYESNO(test_id, question_number, qDt)
                    UCabcd1.Visible = False
                    UCyesno.Visible = True
                    UCMaching.Visible = False
                    UCWriting.Visible = False
                    UCPicture.Visible = False
                Case "matching"
                    Dim question_number As Double = qDt.Rows(0)("question_no").ToString()
                    Me.UCMaching.SetTestQuestionMatching(test_id, question_number, qDt)
                    UCabcd1.Visible = False
                    UCyesno.Visible = False
                    UCMaching.Visible = True
                    UCWriting.Visible = False
                    UCPicture.Visible = False
                Case "writing"
                    Dim question_number As Double = qDt.Rows(0)("question_no").ToString()
                    Me.UCWriting.SetTestQuestionWriting(test_id, question_number, qDt)
                    UCabcd1.Visible = False
                    UCyesno.Visible = False
                    UCMaching.Visible = False
                    UCWriting.Visible = True
                    UCPicture.Visible = False
                Case "picture"
                    Dim question_number As Double = qDt.Rows(0)("question_no").ToString()
                    Me.UCPicture.SetTestQuestionPicture(test_id, question_number, qDt)
                    UCabcd1.Visible = False
                    UCyesno.Visible = False
                    UCMaching.Visible = False
                    UCWriting.Visible = False
                    UCPicture.Visible = True
            End Select
        End If
    End Sub

    Private Sub UCyesno_btnAnsYESNOclick(sender As Object, question_no As Double) Handles UCyesno.btnAnsYESNOclick

        Dim lastchoice As Integer = 0
        Dim qDt As DataTable = GetTestQuestion(test_id, question_no + 1)

        If qDt.Rows.Count > 0 Then

            Select Case qDt.Rows(0)("question_type").ToString.ToLower
                Case "abcd"
                    Dim question_number As Double = qDt.Rows(0)("question_no").ToString()
                    Me.UCabcd1.setCol_rpt(test_id, question_number, qDt)
                    UCabcd1.Visible = True
                    UCyesno.Visible = False
                    UCMaching.Visible = False
                    UCWriting.Visible = False
                    UCPicture.Visible = False
                Case "yes/no"
                    Dim question_number As Double = qDt.Rows(0)("question_no").ToString()
                    Me.UCyesno.SetTestQuestionYESNO(test_id, question_number, qDt)
                    UCabcd1.Visible = False
                    UCyesno.Visible = True
                    UCMaching.Visible = False
                    UCWriting.Visible = False
                    UCPicture.Visible = False
                Case "matching"
                    Dim question_number As Double = qDt.Rows(0)("question_no").ToString()
                    Me.UCMaching.SetTestQuestionMatching(test_id, question_number, qDt)
                    UCabcd1.Visible = False
                    UCyesno.Visible = False
                    UCMaching.Visible = True
                    UCWriting.Visible = False
                    UCPicture.Visible = False
                Case "writing"
                    Dim question_number As Double = qDt.Rows(0)("question_no").ToString()
                    Me.UCWriting.SetTestQuestionWriting(test_id, question_number, qDt)
                    UCabcd1.Visible = False
                    UCyesno.Visible = False
                    UCMaching.Visible = False
                    UCWriting.Visible = True
                    UCPicture.Visible = False
                Case "picture"
                    Dim question_number As Double = qDt.Rows(0)("question_no").ToString()
                    Me.UCPicture.SetTestQuestionPicture(test_id, question_number, qDt)
                    UCabcd1.Visible = False
                    UCyesno.Visible = False
                    UCMaching.Visible = False
                    UCWriting.Visible = False
                    UCPicture.Visible = True
            End Select
        End If
    End Sub

    Private Sub UCyesno_btnAnsWritingclick(sender As Object, question_no As Double) Handles UCWriting.btnAnswritingclick

        Dim lastchoice As Integer = 0
        Dim qDt As DataTable = GetTestQuestion(test_id, question_no + 1)

        If qDt.Rows.Count > 0 Then

            Select Case qDt.Rows(0)("question_type").ToString.ToLower
                Case "abcd"
                    Dim question_number As Double = qDt.Rows(0)("question_no").ToString()
                    Me.UCabcd1.setCol_rpt(test_id, question_number, qDt)
                    UCabcd1.Visible = True
                    UCyesno.Visible = False
                    UCMaching.Visible = False
                    UCWriting.Visible = False
                    UCPicture.Visible = False
                Case "yes/no"
                    Dim question_number As Double = qDt.Rows(0)("question_no").ToString()
                    Me.UCyesno.SetTestQuestionYESNO(test_id, question_number, qDt)
                    UCabcd1.Visible = False
                    UCyesno.Visible = True
                    UCMaching.Visible = False
                    UCWriting.Visible = False
                    UCPicture.Visible = False
                Case "matching"
                    Dim question_number As Double = qDt.Rows(0)("question_no").ToString()
                    Me.UCMaching.SetTestQuestionMatching(test_id, question_number, qDt)
                    UCabcd1.Visible = False
                    UCyesno.Visible = False
                    UCMaching.Visible = True
                    UCWriting.Visible = False
                    UCPicture.Visible = False
                Case "writing"
                    Dim question_number As Double = qDt.Rows(0)("question_no").ToString()
                    Me.UCWriting.SetTestQuestionWriting(test_id, question_number, qDt)
                    UCabcd1.Visible = False
                    UCyesno.Visible = False
                    UCMaching.Visible = False
                    UCWriting.Visible = True
                    UCPicture.Visible = False
                Case "picture"
                    Dim question_number As Double = qDt.Rows(0)("question_no").ToString()
                    Me.UCPicture.SetTestQuestionPicture(test_id, question_number, qDt)
                    UCabcd1.Visible = False
                    UCyesno.Visible = False
                    UCMaching.Visible = False
                    UCWriting.Visible = False
                    UCPicture.Visible = True
            End Select
        End If
    End Sub

End Class