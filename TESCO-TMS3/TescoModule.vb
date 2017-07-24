Imports System.Net
Imports System.IO
Imports System.Drawing
Imports LinqDB.ConnectDB
Imports LinqDB.TABLE
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports Newtonsoft.Json.Linq
Module TescoModule

    Public TempPath As String = "D:\" & "TMS"
    Public FolderCourseDocumentFile As String = TempPath & "\CourseDocumentFile"

    Public Function GetWebServiceURL() As String
        'Return "https://tescolotuslc.com/learningcenterdev/" 'Dev
        'Return "http://tescolotuslc.com/learningcenterstaging/"         '"Staging"
        'Return "https://tescolotuslc.com/learningcenterpreproduction/"  "ProProduction"
        'Return "https://tescolotuslc.com/learningcenter/                "Produciton"


        Dim cf As CfSysconfigLinqDB = GetSysconfig()
        Return cf.WEBSERVICE_URL
    End Function

    Private Function GetSysconfig() As CfSysconfigLinqDB
        Dim lnq As New CfSysconfigLinqDB
        Return lnq.GetDataByPK(1, Nothing)
    End Function

    Private Function CheckInternetConnection(URL As String) As Boolean
        Dim ret = False
        Dim req As WebRequest = WebRequest.Create(URL)
        Dim resp As WebResponse
        Try
            req.Proxy.Credentials = System.Net.CredentialCache.DefaultCredentials
            req.Timeout = 5000
            resp = req.GetResponse
            resp.Close()
            req = Nothing
            ret = True
        Catch ex As Exception
            ret = False
        End Try
        Return ret
    End Function



    Function GetStringDataFromURL(p As Page, pt As Type, LoginHisID As Long, ByVal URL As String, ByVal Parameter As String) As String
        Dim StartTime As DateTime = DateTime.Now
        Try

            Dim ret As String = ""
            'If CheckInternetConnection(GetWebServiceURL() & "api/welcome") = True Then
            Dim request As WebRequest
            request = WebRequest.Create(URL)
            Dim response As WebResponse
            Dim data As Byte() = Encoding.UTF8.GetBytes(Parameter)

            request.Proxy.Credentials = System.Net.CredentialCache.DefaultCredentials
            request.Method = "POST"
            request.ContentType = "application/x-www-form-urlencoded"
            request.ContentLength = data.Length

            Dim stream As Stream = request.GetRequestStream()
            stream.Write(data, 0, data.Length)
            stream.Close()

            response = request.GetResponse()
            Dim sr As New StreamReader(response.GetResponseStream())

            If LoginHisID > 0 Then
                LogFileBL.LogTrans(LoginHisID, "Call API URL:" & URL & "  Parameter:" & Parameter & "  Response Time :" & (DateTime.Now - StartTime).TotalMilliseconds)
            Else
                LogFileBL.LogTrans(Parameter, "Call API URL:" & URL & "  Parameter:" & Parameter & "  Response Time :" & (DateTime.Now - StartTime).TotalMilliseconds)
            End If

            Return sr.ReadToEnd()
            'Else
            '    ScriptManager.RegisterStartupScript(p, pt, Guid.NewGuid().ToString(), "alert('Unable to connect Back-End server " & vbCrLf & vbCrLf & "Please contract your support !!')", True)
            '    LogFileBL.LogError(LoginHisID, "Unable to connect Back-End server URL :" & URL)
            'End If
        Catch ex As Exception
            'MessageBox.Show(ex.Message & vbCrLf & ex.StackTrace, "Attention", MessageBoxButtons.OK, MessageBoxIcon.Information)
            LogFileBL.LogTrans(LoginHisID, "Call API FAIL URL:" & URL & "  Parameter:" & Parameter & "  Response Time :" & (DateTime.Now - StartTime).TotalMilliseconds & vbNewLine & " Exception Message : " & ex.Message)
            ScriptManager.RegisterStartupScript(p, pt, Guid.NewGuid().ToString(), "alert('Unable to connect Back-End server " & vbCrLf & vbCrLf & "Please contract your support !!')", True)
        End Try
        Return ""
    End Function

    Function GetStringDataFromURL(ByVal URL As String, Optional ByVal Parameter As String = "") As String
        Try
            Dim ret As String = ""
            'If CheckInternetConnection(GetWebServiceURL() & "api/welcome") = True Then
            Dim request As WebRequest
                request = WebRequest.Create(URL)
                Dim response As WebResponse
                Dim data As Byte() = Encoding.UTF8.GetBytes(Parameter)

                request.Proxy.Credentials = System.Net.CredentialCache.DefaultCredentials
                request.Method = "POST"
                request.ContentType = "application/x-www-form-urlencoded"
                request.ContentLength = data.Length

                Dim stream As Stream = request.GetRequestStream()
                stream.Write(data, 0, data.Length)
                stream.Close()

                response = request.GetResponse()
                Dim sr As New StreamReader(response.GetResponseStream())

                Return sr.ReadToEnd()
            'Else
            '    'LogFileBL.LogError()
            'End If
        Catch ex As Exception
            'MessageBox.Show(ex.Message & vbCrLf & ex.StackTrace, "Attention", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try
        Return ""
    End Function

    Function GetImageFromURL(ByVal URL As String) As Image
        Try
            Dim Client As WebClient = New WebClient
            Client.Proxy.Credentials = System.Net.CredentialCache.DefaultCredentials
            Dim Image As Bitmap = Bitmap.FromStream(New MemoryStream(Client.DownloadData(URL)))
            Return Image
        Catch ex As Exception : End Try
        Dim im As Bitmap
        Return im
    End Function

    Function GetFileFromURL(ByVal URL As String, OutputFile As String) As Boolean
        Dim ret As Boolean = False
        Try
            If File.Exists(OutputFile) = True Then
                File.SetAttributes(OutputFile, FileAttributes.Normal)
                File.Delete(OutputFile)
            End If

            Dim Client As New WebClient
            Client.Proxy.Credentials = System.Net.CredentialCache.DefaultCredentials
            Dim b() As Byte = Client.DownloadData(URL)

            Dim fs As New FileStream(OutputFile, FileMode.Create)
            fs.Write(b, 0, b.Length)
            fs.Close()

            If File.Exists(OutputFile) = True Then
                ret = True
            End If
        Catch ex As Exception
            ret = False
        End Try

        Return ret
    End Function

#Region "Log"
    Public Function CallAPIUpdateLog(p As Page, pt As Type, LoginHisID As Long, token As String, vAction As String, vModule As String, data As String) As Boolean
        'เมื่อเรียนจบหลักสูตรให้บันทึก Log

        Dim ret As Boolean = False
        Dim info As String = ""
        info = GetStringDataFromURL(p, pt, LoginHisID, GetWebServiceURL() & "api/log", token & "&action=" & vAction & "&module=" & vModule & "&data=" & data)
        If info.Trim <> "" Then
            Dim json As String = info
            Dim ser As JObject = JObject.Parse(json)
            Dim ser_data As List(Of JToken) = ser.Children().ToList

            For Each item As JProperty In ser_data
                Select Case item.Name
                    Case "status"
                        ret = Convert.ToBoolean(item.First)
                        Exit For
                End Select
            Next
        End If

        Return ret
    End Function



    Public Sub UpdateLog(pa As Page, pt As Type, LoginHisID As Long, id As Long, ClassID As Long, Token As String, UserDataCourseFile As DataTable, Username As String)
        Dim tb_user_course_document_id As String = "0"
        Dim file_id As Long = 0
        Dim doc_id As String = "0"
        Dim course_id As String = "0"
        Dim Sql As String
        Sql = " select cdf.id, cdf.document_file_id,cd.document_id, c.course_id "
        Sql += " from TB_USER_COURSE_DOCUMENT_FILE cdf"
        Sql += " inner join TB_USER_COURSE_DOCUMENT cd on cd.id=cdf.tb_user_course_document_id"
        Sql += " inner join TB_USER_COURSE c on c.id=cd.tb_user_course_id"
        Sql += " where cdf.id=@_ID"
        Dim p(1) As SqlParameter
        p(0) = SqlDB.SetBigInt("@_ID", id)
        Dim dtCourseFile As DataTable = SqlDB.ExecuteTable(Sql, p)
        If dtCourseFile.Rows.Count Then
            file_id = dtCourseFile.Rows(0)("document_file_id")
            doc_id = dtCourseFile.Rows(0)("document_id")
            course_id = dtCourseFile.Rows(0)("course_id")
        End If
        dtCourseFile.Dispose()

        Dim strfillter As String = "id >" & id
        UserDataCourseFile.DefaultView.RowFilter = strfillter
        If UserDataCourseFile.DefaultView.Count = 0 Then
            If UpdateCourseHisFinished(course_id, Username) = True Then
                CallAPIUpdateLog(pa, pt, LoginHisID, Token, "complete", "class", "{" & Chr(34) & "class_id" & Chr(34) & ":" & ClassID.ToString & "}")
            End If
        Else
            If UpdateFileHisIsStudy(file_id, Username) = True Then
                CallAPIUpdateLog(pa, pt, LoginHisID, Token, "complete", "document", "{" & Chr(34) & "class_id" & Chr(34) & ":" & ClassID.ToString & "," & Chr(34) & "document_id" & Chr(34) & ":" & doc_id & "}")
            End If
        End If
        UserDataCourseFile.DefaultView.RowFilter = ""
    End Sub

    Private Function UpdateFileHisIsStudy(FileID As Long, Username As String) As Boolean
        'เมื่อเริ่มเรียนที่ไฟล์ไหน ก็ให้ Update ว่าได้เรียนไฟล์นั้นแล้ว
        Dim sql As String = "update TB_USER_COURSE_DOC_FILE_HIS"
        sql += " set is_study='Y', updated_by=@_USERNAME, updated_date=getdate() "
        sql += " where document_file_id=@_FILE_ID"
        sql += " and username=@_USERNAME"

        Dim p(2) As SqlParameter
        p(0) = SqlDB.SetText("@_USERNAME", Username)
        p(1) = SqlDB.SetBigInt("@_FILE_ID", FileID)

        Dim trans As New TransactionDB
        Dim lnq As New TbUserCourseDocFileHisLinqDB
        Dim ret As ExecuteDataInfo = lnq.UpdateBySql(sql, trans.Trans, p)
        If ret.IsSuccess = True Then
            trans.CommitTransaction()
        Else
            trans.RollbackTransaction()
        End If
        lnq = Nothing

        Return ret.IsSuccess
    End Function

    Private Function UpdateCourseHisFinished(CourseID As Long, Username As String) As Boolean
        'เมื่อเรียนจบหลักสูตรแล้วให้ Update IS_FINISHED ว่าเรียนจบแล้ว
        Dim sql As String = "update TB_USER_COURSE_HIS"
        sql += " set is_finished='Y', updated_by=@_USERNAME, updated_date=getdate()"
        sql += " where username=@_USERNAME"
        sql += " and course_id=@_COURSE_ID"

        Dim p(2) As SqlParameter
        p(0) = SqlDB.SetText("@_USERNAME", Username)
        p(1) = SqlDB.SetBigInt("@_COURSE_ID", CourseID)

        Dim trans As New TransactionDB
        Dim lnq As New TbUserCourseHisLinqDB

        Dim ret As ExecuteDataInfo = lnq.UpdateBySql(sql, trans.Trans, p)
        If ret.IsSuccess = True Then
            'Update File ทั้งหมดที่อยู่ในบทเรียนนี้ให้เป็นเรียนแล้ว
            sql = "update TB_USER_COURSE_DOC_FILE_HIS"
            sql += " set is_study='Y', updated_by=@_USERNAME, updated_date=getdate() "
            sql += " where tb_user_course_doc_his_id in (select id from TB_USER_COURSE_DOC_HIS where tb_user_course_his_id in (select id from TB_USER_COURSE_HIS where course_id=@_COURSE_ID and username=@_USERNAME)) "
            sql += " and is_study='N'"

            ReDim p(2)
            p(0) = SqlDB.SetText("@_USERNAME", Username)
            p(1) = SqlDB.SetBigInt("@_COURSE_ID", CourseID)
            ret = lnq.UpdateBySql(sql, trans.Trans, p)

            If ret.IsSuccess = True Then
                trans.CommitTransaction()
            Else
                trans.RollbackTransaction()
            End If
        Else
            trans.RollbackTransaction()
        End If
        lnq = Nothing

        Return ret.IsSuccess
    End Function
#End Region

#Region "User and Class"

    Public Function CreateClass(token As String, stdID As String, CourseID As String, UserID As String) As Long
        Dim ret As Long = 0
        Try
            Dim info As String = ""
            info = GetStringDataFromURL(GetWebServiceURL() & "api/class/create", token & "&course_id=" & CourseID & "&user_id=" & UserID & "&student_id_list=" & stdID)

            Dim json As String = info
            Dim ser As JObject = JObject.Parse(json)
            Dim data As List(Of JToken) = ser.Children().ToList

            If data.Count = 3 Then
                If DirectCast(data(0), JProperty).Value.ToString.ToLower = "true" Then
                    ret = DirectCast(data(2), JProperty).Value
                End If
            End If
        Catch ex As Exception
            ret = 0
        End Try
        Return ret
    End Function

    Public Function GetStudentUser(ByVal Token As String, ByVal UserID As String, CourseID As String) As String()
        Dim ret(3) As String
        Dim info As String = ""
        info = GetStringDataFromURL(GetWebServiceURL() & "api/user/get", Token & "&user_company_id=" & UserID & "&course_id=" & CourseID)
        If info.Trim <> "" Then
            Dim json As String = info
            Dim ser As JObject = JObject.Parse(json)
            Dim data As List(Of JToken) = ser.Children().ToList
            Dim output As String = ""

            For Each item As JProperty In data
                item.CreateReader()
                Select Case item.Name
                    Case "user"
                        For Each comment As JProperty In item.Values
                            Select Case comment.Name
                                Case "id"
                                    ret(0) = comment.Value.ToString
                                Case "firstname"
                                    ret(1) = comment.Value.ToString
                                Case "lastname"
                                    ret(2) = comment.Value.ToString
                                Case "company_id"  'company_id คือ รหัสพนักงาน
                                    ret(3) = comment.Value.ToString
                            End Select
                        Next

                End Select
            Next
        End If
        Return ret
    End Function
#End Region
#Region "Set validate Textbox Property"
    Public Sub SetTextIntKeypress(txt As TextBox)
        txt.Attributes.Add("OnKeyPress", "ChkMinusInt(this,event);")
        txt.Attributes.Add("onKeyDown", "CheckKeyNumber(event);")
    End Sub
    Public Sub SetTextDblKeypress(txt As TextBox)
        txt.Attributes.Add("OnKeyPress", "ChkMinusDbl(this,event);")
        txt.Attributes.Add("onKeyDown", "CheckKeyNumber(event);")
    End Sub
    Public Sub SetTextAreaMaxLength(txt As TextBox, MaxLength As Int16)
        txt.Attributes.Add("onKeyDown", "checkTextAreaMaxLength(this,event,'" & MaxLength & "');")
    End Sub
#End Region

#Region "Testing"
    Public Function GetTesting(UserSessionID As Long) As DataTable
        Dim sql As String = " select * from TB_TESTING where tb_user_session_id=@_USER_SESSION_ID"
        Dim p(1) As SqlParameter
        p(0) = SqlDB.SetBigInt("@_USER_SESSION_ID", UserSessionID)

        Dim dt As DataTable = LinqDB.ConnectDB.SqlDB.ExecuteTable(sql, p)

        Return dt
    End Function

    Public Function GetTestingData(TestID As Long) As TbTestingLinqDB
        Dim lnq As New TbTestingLinqDB
        lnq.GetDataByPK(TestID, Nothing)
        Return lnq
    End Function

    Public Function GetTestQuestion(TestID As Long, QuestionNo As Integer)
        Dim dt As New DataTable
        Dim testLnq As New TbTestingLinqDB
        testLnq.GetDataByPK(TestID, Nothing)
        If testLnq.ID > 0 Then
            Dim p(2) As SqlParameter
            p(0) = SqlDB.SetBigInt("@_TESTING_ID", TestID)
            p(1) = SqlDB.SetInt("@_QUESTION_NO", QuestionNo)
            Dim lnq As New TbTestingQuestionLinqDB
            dt = lnq.GetDataList("tb_testing_id=@_TESTING_ID and question_no=@_QUESTION_NO", "", Nothing, p)
        End If

        Return dt
    End Function

    Public Function GetTestQuestion(TestID As Long)
        Dim p(1) As SqlParameter
        p(0) = SqlDB.SetBigInt("@_TESTING_ID", TestID)
        Dim lnq As New TbTestingQuestionLinqDB
        Dim dt As DataTable = lnq.GetDataList("tb_testing_id=@_TESTING_ID ", "", Nothing, p)
        Return dt
    End Function

    Public Function GetTestQuestion(TestID As Long, trans As TransactionDB)
        Dim p(1) As SqlParameter
        p(0) = SqlDB.SetBigInt("@_TESTING_ID", TestID)
        Dim lnq As New TbTestingQuestionLinqDB
        Dim dt As DataTable = lnq.GetDataList("tb_testing_id=@_TESTING_ID ", "", trans.Trans, p)
        Return dt
    End Function

    Public Function GetTestWritingAnswer(TestID As Long)
        Dim sql As String = "select tq.question_no,ta.answer_text, tq.question_type, tq.weight, ta.time_spent"
        sql += " from TB_TESTING_ANSWER_WRITING ta "
        sql += " inner join TB_TESTING_QUESTION tq on tq.id=ta.tb_testing_question_id "
        sql += " where ta.tb_testing_id=@_TESTING_ID"

        Dim p(1) As SqlParameter
        p(0) = SqlDB.SetBigInt("@_TESTING_ID", TestID)
        Dim dt As DataTable = SqlDB.ExecuteTable(sql, p)
        Return dt
    End Function
    Public Function DeleteTestQuestion(TestID As Long, trans As TransactionDB) As ExecuteDataInfo
        Dim ret As New ExecuteDataInfo
        Dim sql As String = "delete from TB_TESTING_QUESTION where tb_testing_id=@_TESTING_ID"
        Dim p(1) As SqlParameter
        p(0) = SqlDB.SetBigInt("@_TESTING_ID", TestID)

        ret = SqlDB.ExecuteNonQuery(sql, trans.Trans, p)
        Return ret
    End Function

    Public Function SaveTestAnswer(Username As String, trans As TransactionDB, TestID As Long, QuestionID As Long, TimeSpen As Integer, AnswerChoice As Integer, AnswerResult As String) As ExecuteDataInfo
        Dim ret As New ExecuteDataInfo
        Dim lnq As New TbTestingAnswerLinqDB
        lnq.TB_TESTING_ID = TestID
        lnq.TB_TESTING_QUESTION_ID = QuestionID
        lnq.TIME_SPENT = TimeSpen
        lnq.ANSWER_CHOICE = AnswerChoice
        lnq.ANSWER_RESULT = AnswerResult

        ret = lnq.InsertData(Username, trans.Trans)

        Return ret
    End Function
    Public Function SaveTestAnswerWriting(Username As String, trans As TransactionDB, TestID As Long, QuestionID As Long, TimeSpen As Integer, AnswerText As String) As ExecuteDataInfo
        Dim ret As New ExecuteDataInfo
        Dim lnq As New TbTestingAnswerWritingLinqDB
        lnq.TB_TESTING_ID = TestID
        lnq.TB_TESTING_QUESTION_ID = QuestionID
        lnq.TIME_SPENT = TimeSpen
        lnq.ANSWER_TEXT = AnswerText

        ret = lnq.InsertData(Username, trans.Trans)

        Return ret
    End Function

    Public Enum QuestionType
        ABCD = 1
        YesNo = 2
        Writing = 3
        Matching = 4
        Picture = 5
    End Enum
#End Region
#Region "File"
    Public Function GetURLFileExtension(URLFile As String) As String
        Dim ret As String = ""
        Dim Tmp() As String = Split(URLFile, ".")
        If Tmp.Length > 0 Then
            ret = "." & Tmp(Tmp.Length - 1)
        End If

        Return ret
    End Function
#End Region
#Region " Encrypt/Decrypt "

    Public Function EnCripText(ByVal passString As String) As String
        Dim encrypted As String = ""
        Try
            Dim ByteData() As Byte = System.Text.Encoding.UTF8.GetBytes(passString)
            encrypted = Convert.ToBase64String(ByteData)
        Catch ex As Exception
        End Try

        Return encrypted
    End Function

    Public Function DeCripText(ByVal passString As String) As String
        Dim decrypted As String = ""
        Try
            Dim ByteData() As Byte = Convert.FromBase64String(passString)
            decrypted = System.Text.Encoding.UTF8.GetString(ByteData)
        Catch ex As Exception

        End Try

        Return decrypted
    End Function
#End Region
End Module
