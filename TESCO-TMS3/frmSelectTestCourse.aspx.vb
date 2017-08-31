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
        If Session("UserData") Is Nothing Then
            Response.Redirect("Default.aspx")
        End If

        If Not Page.IsPostBack Then
            If GetTestStatistic.IsSuccess = True Then
                Me.txtFormatID.Style.Add("display", "none")
                Me.txtFormatTitle.Style.Add("display", "none")
                SetTestSubject()
                SetStatistict()
            End If
        End If

    End Sub

    Public Function GetTestStatistic() As ExecuteDataInfo
        Dim ret As New ExecuteDataInfo
        Try
            Dim UserData As UserProfileData = Session("UserData")
            LogFileBL.LogTrans(UserData.LoginHistoryID, "ดึงข้อมูลสถิติจาก Backend")

            Dim info As String = ""
            info = GetStringDataFromURL(Me, Me.GetType, UserData.LoginHistoryID, GetWebServiceURL() & "api/testing/get", UserData.Token & "&user_id=" & UserData.UserID)
            If info.Trim = "" Then
                LogFileBL.LogError(UserData, "ดึงข้อมูลสถิติไม่สำเร็จ")
                Return New ExecuteDataInfo
            End If

            Dim json As String = info
            Dim ser As JObject = JObject.Parse(json)
            Dim data As List(Of JToken) = ser.Children().ToList
            Dim output As String = ""

            For Each item As JProperty In data
                item.CreateReader()
                Select Case item.Name
                    Case "course_data"
                        For Each comment As JProperty In item.Values
                            Select Case comment.Name
                                Case "complete"
                                    UserData.CourseComplete = comment.First
                                Case "total"
                                    UserData.CourseTotal = comment.Last
                            End Select
                        Next
                        LogFileBL.LogTrans(UserData.LoginHistoryID, "ดึงข้อมูลสถิติบทการเรียนจาก Backend")

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

                        LogFileBL.LogTrans(UserData.LoginHistoryID, "ดึงข้อมูลสถิติการสอบจาก Backend")
                End Select
            Next

            Session("UserData") = UserData
            ret.IsSuccess = True
        Catch ex As Exception
            ret.IsSuccess = False
            ret.ErrorMessage = ex.Message
            LogFileBL.LogException(UserData, ex.Message, ex.StackTrace)
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