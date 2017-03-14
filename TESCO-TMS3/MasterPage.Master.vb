Imports System.Data.SqlClient
Imports LinqDB.ConnectDB
Imports LinqDB.TABLE
Imports Newtonsoft.Json.Linq

Public Class MasterPage
    Inherits System.Web.UI.MasterPage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Private Sub btnTesting_Click(sender As Object, e As EventArgs) Handles btnTesting.Click
        If GetDatableTableFromTesting.IsSuccess = True Then
            Response.Redirect("frmTestingCourse.aspx?rnd=" & DateTime.Now.Millisecond)
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

                                        Dim vChoice As String = ""
                                        Dim vAnswer As String = ""
                                        Dim answer_txt As String = "{""answer"":" & question_comment("answer").ToString & "}"
                                        Dim answer_ser As JObject = JObject.Parse(answer_txt)
                                        Dim answer_data As List(Of JToken) = answer_ser.Children().ToList
                                        For Each answer_item As JProperty In answer_data
                                            For Each answer_comment As JObject In answer_item.Values
                                                answer_item.CreateReader()

                                                If vChoice = "" Then
                                                    vChoice = answer_comment("text").ToString
                                                Else
                                                    vChoice += "##" + answer_comment("text").ToString
                                                End If

                                                If vAnswer = "" Then
                                                    vAnswer = answer_comment("is_correct").ToString
                                                Else
                                                    vAnswer += "##" + answer_comment("is_correct").ToString
                                                End If
                                            Next
                                        Next

                                        Dim qLnq As New TbTestingQuestionLinqDB
                                        qLnq.TB_TESTING_ID = lnq.ID
                                        qLnq.TEST_ID = lnq.TEST_ID
                                        qLnq.QUESTION_TITLE = question_comment("description").ToString
                                        qLnq.ICON_URL = question_comment("cover").ToString
                                        qLnq.CHOICE = vChoice
                                        qLnq.ANSWER = vAnswer
                                        qLnq.QUESTION_NO = question_qty

                                        ret = qLnq.InsertData(UserData.UserName, trans.Trans)
                                        If ret.IsSuccess = False Then
                                            Exit For
                                        End If
                                    Next
                                Next

                                'จำนวนคำถามในแบบทดสอบ
                                lnq.QUESTION_QTY = question_qty
                                ret = lnq.UpdateData(UserData.UserName, trans.Trans)
                                If ret.IsSuccess = False Then
                                    Exit For
                                End If
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
        Catch ex As Exception
            ret.IsSuccess = False
            ret.ErrorMessage = ex.Message
        End Try
        Return ret
    End Function
End Class