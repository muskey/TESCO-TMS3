Imports System.Data.SqlClient
Imports LinqDB.ConnectDB
Imports LinqDB.TABLE
Imports Newtonsoft.Json.Linq

Public Class frmSelectTestCourse
    Inherits System.Web.UI.Page

#Region "Declare & Valiable"
    ' Public myUser As User
    Public ReadOnly Property UserData As UserProfileData
        Get
            Return Session("UserData")
        End Get
    End Property
#End Region

#Region "Initail"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            If GetDatableTableFromTesting.IsSuccess = True Then
                Me.txtFormatID.Style.Add("display", "none")
                Me.txtFormatTitle.Style.Add("display", "none")
                SetTestSubject()
                SetStatistict()
            End If
        End If

    End Sub

    Public Function GetDatableTableFromTesting() As ExecuteDataInfo
        Dim ret As New ExecuteDataInfo
        Try
            Dim UserData As UserProfileData = Session("UserData")

            Dim info As String = ""
            info = GetStringDataFromURL(GetWebServiceURL() & "api/testing/get", UserData.Token & "&user_id=" & UserData.UserID)
            If info.Trim = "" Then Return New ExecuteDataInfo

            Dim json As String = info
            Dim ser As JObject = JObject.Parse(json)
            Dim data As List(Of JToken) = ser.Children().ToList
            Dim output As String = ""

            For Each item As JProperty In data
                item.CreateReader()
                Select Case item.Name
                    Case "testing"
                        Dim trans As New TransactionDB
                        Dim sql As String = " delete from TB_TESTING_ANSWER "
                        sql += " where tb_testing_id in (select id from TB_TESTING where tb_user_session_id=@_USER_SESSION_ID) "
                        Dim p(1) As SqlParameter
                        p(0) = SqlDB.SetBigInt("@_USER_SESSION_ID", UserData.UserSessionID)

                        ret = SqlDB.ExecuteNonQuery(sql, p)
                        If ret.IsSuccess = True Then
                            sql = "delete from TB_TESTING_QUESTION "
                            sql += " where tb_testing_id in (select id from TB_TESTING where tb_user_session_id=@_USER_SESSION_ID) "
                            ReDim p(1)
                            p(0) = SqlDB.SetBigInt("@_USER_SESSION_ID", UserData.UserSessionID)

                            ret = SqlDB.ExecuteNonQuery(sql, p)
                            If ret.IsSuccess = True Then
                                sql = "delete from TB_TESTING where tb_user_session_id=@_USER_SESSION_ID"
                                ReDim p(1)
                                p(0) = SqlDB.SetBigInt("@_USER_SESSION_ID", UserData.UserSessionID)

                                ret = SqlDB.ExecuteNonQuery(sql, p)
                                If ret.IsSuccess = False Then
                                    trans.RollbackTransaction()
                                    Return ret
                                End If
                            Else
                                trans.RollbackTransaction()
                                Return ret
                            End If
                        Else
                            trans.RollbackTransaction()
                            Return ret
                        End If

                        For Each comment As JObject In item.Values
                            Dim lnq As New TbTestingLinqDB
                            lnq.TEST_ID = comment("id")
                            lnq.TEST_TITLE = comment("title").ToString
                            lnq.TEST_DESC = comment("description").ToString
                            lnq.TARGET_PERCENTAGE = Convert.ToInt32(comment("target_percentage"))
                            lnq.COURSE_ID = Convert.ToInt32(comment("course_id"))
                            lnq.TB_USER_SESSION_ID = UserData.UserSessionID
                            lnq.QUESTION_QTY = 0

                            ret = lnq.InsertData(UserData.UserName, trans.Trans)
                            If ret.IsSuccess = True Then
                                Dim question_qty As Integer = 0
                                Dim question_txt As String = "{""question"":" & comment("question").ToString & "}"
                                Dim question_ser As JObject = JObject.Parse(question_txt)
                                Dim question_data As List(Of JToken) = question_ser.Children().ToList
                                For Each question_item As JProperty In question_data
                                    For Each question_comment As JObject In question_item.Values

                                        question_qty = question_qty + 1

                                        question_item.CreateReader()

                                        Dim qLnq As New TbTestingQuestionLinqDB
                                        qLnq.TB_TESTING_ID = lnq.ID
                                        qLnq.TEST_ID = lnq.TEST_ID
                                        qLnq.QUIZ_ID = question_comment("quiz_id").ToString
                                        If question_comment("title") IsNot Nothing Then qLnq.QUESTION_TITLE = question_comment("title").ToString
                                        qLnq.ICON_URL = question_comment("cover").ToString
                                        qLnq.QUESTION_NO = question_qty
                                        qLnq.WEIGHT = question_comment("weight").ToString
                                        qLnq.STATUS = question_comment("status").ToString
                                        qLnq.QUESTION_TYPE = question_comment("type").ToString

                                        Select Case question_comment("type").ToString.ToLower
                                            Case "abcd"
                                                Dim vChoice As String = ""
                                                Dim vAnswer As String = ""

                                                Dim answer_txt As String = "{""answer"":" & question_comment("answer").ToString & "}"
                                                Dim answer_ser As JObject = JObject.Parse(answer_txt)
                                                Dim answer_data As List(Of JToken) = answer_ser.Children().ToList
                                                For Each answer_item As JProperty In answer_data
                                                    Dim PreAlphabet As Integer = Asc("ก")

                                                    For Each answer_comment As JObject In answer_item.Values
                                                        answer_item.CreateReader()

                                                        If vChoice = "" Then
                                                            vChoice = Chr(PreAlphabet) & ". " & answer_comment("text").ToString
                                                        Else
                                                            vChoice += "##" & Chr(PreAlphabet) & ". " & answer_comment("text").ToString
                                                        End If

                                                        If vAnswer = "" Then
                                                            vAnswer = answer_comment("is_correct").ToString
                                                        Else
                                                            vAnswer += "##" + answer_comment("is_correct").ToString
                                                        End If

                                                        PreAlphabet += 1
                                                        If PreAlphabet = 163 Then   '163=ตัว ฃ ขวด
                                                            PreAlphabet += 1
                                                        ElseIf PreAlphabet = 165 Then  ' 165= ฅ คน
                                                            PreAlphabet += 2
                                                        End If
                                                    Next
                                                Next

                                                qLnq.CHOICE = vChoice
                                                qLnq.ANSWER = vAnswer

                                            Case "yes/no"
                                                qLnq.YESNO_CORRECT_ANSWER = question_comment("correct_answer").ToString
                                            Case "writing"

                                            Case "matching"
                                                qLnq.MATCHING_LEFTTEXT = question_comment("leftText").ToString
                                                qLnq.MATCHING_RIGHTTEXT = question_comment("rightText").ToString
                                                qLnq.MATCHING_CORRECT_ANSWER = question_comment("correct_answer_id_list").ToString.Replace("[", "").Replace("]", "").Trim
                                            Case "picture"
                                                qLnq.PICTURE_TEXT = question_comment("text").ToString
                                                qLnq.MATCHING_CORRECT_ANSWER = question_comment("correct_answer_id_list").ToString.Replace("[", "").Replace("]", "").Trim
                                        End Select

                                        ret = qLnq.InsertData(UserData.UserName, trans.Trans)
                                        If ret.IsSuccess = False Then
                                            Exit For
                                        End If
                                    Next
                                    If ret.IsSuccess = False Then
                                        Exit For
                                    End If
                                Next

                                If ret.IsSuccess = False Then
                                    Exit For
                                End If

                                'จำนวนคำถามในแบบทดสอบ
                                lnq.QUESTION_QTY = question_qty
                                ret = lnq.UpdateData(UserData.UserName, trans.Trans)
                                If ret.IsSuccess = False Then
                                    Exit For
                                End If
                            Else
                                Exit For
                            End If
                        Next

                        If ret.IsSuccess = True Then
                            trans.CommitTransaction()
                        Else
                            trans.RollbackTransaction()
                        End If
                    Case "course_data"
                        For Each comment As JProperty In item.Values
                            Select Case comment.Name
                                Case "complete"
                                    UserData.CourseComplete = comment.First
                                Case "total"
                                    UserData.CourseTotal = comment.Last
                            End Select
                        Next
                    Case "testing_data"
                        For Each comment As JProperty In item.Values
                            Select Case comment.Name
                                Case "attempt"
                                    UserData.TestingAttempt = comment.First
                                Case "complete"
                                    UserData.TestingComplete = comment.First
                                Case "total"
                                    UserData.TestingTotal = comment.Last
                            End Select
                        Next
                End Select
            Next

            Session("UserData") = UserData
        Catch ex As Exception
            ret.IsSuccess = False
            ret.ErrorMessage = ex.Message
        End Try
        Return ret
    End Function

    Private Sub SetStatistict()
        Try
            Dim str As String = ""
            str += " <div class=""row-fluid"">"
            str += "    <span class=""text-center"" style=""font-size:28px;"">คุณเรียนจบหลักสูตรแล้วทั้งหมด <span style=""color:#019b79"">" + UserData.CourseComplete.ToString + "</span> หลักสูตร</span>"
            str += " </div>"
            str += " <div class=""row-fluid"">"
            str += "    <span class=""text-center"" style=""font-size:22px;"">จากทั้งหมด <span style=""color:#019b79"">" + UserData.CourseTotal.ToString + "</span> หลักสูตร</span>"
            str += " </div>"

            str += " <div class=""row-fluid"">&nbsp;</div>"
            str += " <div class=""row-fluid"">"
            str += "    <span class=""text-center"" style=""font-size:28px;"">คุณได้ทำบททดสอบแล้วทั้งหมด <span style=""color:#019b79"">" + UserData.TestingAttempt.ToString + "</span> หลักสูตร</span>"
            str += " </div>"
            str += " <div class=""row-fluid"">"
            str += "    <span class=""text-center"" style=""font-size:22px;"">จากทั้งหมด <span style=""color:#019b79"">" + UserData.TestingTotal.ToString + "</span> หลักสูตร</span>"
            str += " </div>"

            str += " <div class=""row-fluid"">&nbsp;</div>"
            str += " <div class=""row-fluid"">"
            str += "    <span class=""text-center"" style=""font-size:28px;"">คุณได้ทำบททดสอบผ่านแล้วทั้งหมด <span style=""color:#019b79"">" + UserData.TestingComplete.ToString + "</span> หลักสูตร</span>"
            str += " </div>"
            str += " <div class=""row-fluid"">"
            str += "    <span class=""text-center"" style=""font-size:22px;"">จากทั้งหมด <span style=""color:#019b79"">" + UserData.TestingTotal.ToString + "</span> หลักสูตร</span>"
            str += " </div>"
            lblNEWS.Text = str
        Catch ex As Exception

        End Try
    End Sub

    Private Sub SetTestSubject()
        Try

            Dim dt As DataTable = GetTesting(UserData.UserSessionID)
            Dim str As String = ""
            If (dt.Rows.Count > 0) Then
                For i As Int32 = 0 To dt.Rows.Count - 1
                    Dim dr As DataRow = dt.Rows(i)
                    str += " <p> <button class=""btn-block btn btn-larges"" id=" + dr("id").ToString + " onclick=""onConfirmTest('" + dr("id").ToString + "','" + dr("test_title").ToString + "','" + dr("test_desc").ToString + "','" + dr("target_percentage").ToString + "','" + dr("question_qty").ToString + "');return false;"" >" + dr("test_title").ToString + "</button></p>"
                Next
            End If
            lblBotton.Text = str
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnFormat_Click(sender As Object, e As EventArgs) Handles btnFormat.Click
        If Me.txtFormatID.Text <> "" Then
            Response.Redirect("frmSelectFunction.aspx?rnd=" & DateTime.Now.Millisecond & "&format_id=" & Me.txtFormatID.Text & "&formar_title=" + Me.txtFormatTitle.Text)
        End If
    End Sub
#End Region

End Class