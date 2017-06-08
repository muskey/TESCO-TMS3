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
        'Return "https://tescolotuslc.com/learningcenterdev/   " Dev
        'Return "http://tescolotuslc.com/learningcenterstaging/"         "Staging"
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



    Function GetStringDataFromURL(ByVal URL As String, Optional ByVal Parameter As String = "") As String
        Try
            Dim ret As String = ""
            If CheckInternetConnection(GetWebServiceURL() & "api/welcome") = True Then
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
            Else
                'MessageBox.Show("Unable to connect Back-End server " & vbCrLf & vbCrLf & "Please check network connection !!", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
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

#Region "Log"
    Public Sub CallAPIUpdateLog(token As String, vAction As String, vModule As String, data As String)
        'เมื่อเรียนจบหลักสูตรให้บันทึก Log
        Dim info As String = ""
        info = GetStringDataFromURL(GetWebServiceURL() & "api/log", token & "&action=" & vAction & "&module=" & vModule & "&data=" & data)
    End Sub



    Public Sub UpdateLog(id As Long, ClassID As Long, Token As String, UserDataCourseFile As DataTable)
        Dim tb_user_course_document_id As String = "0"
        Dim doc_id As String = "0"
        Dim course_id As String = "0"
        Dim Sql As String
        Sql = " select cdf.id, cdf.document_file_id,cd.document_id, c.id course_id "
        Sql += " from TB_USER_COURSE_DOCUMENT_FILE cdf"
        Sql += " inner join TB_USER_COURSE_DOCUMENT cd on cd.id=cdf.tb_user_course_document_id"
        Sql += " inner join TB_USER_COURSE c on c.id=cd.tb_user_course_id"
        Sql += " where cdf.id=@_ID"
        Dim p(1) As SqlParameter
        p(0) = SqlDB.SetBigInt("@_ID", id)
        Dim dtCourseFile As DataTable = SqlDB.ExecuteTable(Sql, p)
        If dtCourseFile.Rows.Count Then
            doc_id = dtCourseFile.Rows(0)("document_id")
            course_id = dtCourseFile.Rows(0)("course_id")
        End If
        dtCourseFile.Dispose()

        Dim strfillter As String = "id >" & id
        UserDataCourseFile.DefaultView.RowFilter = strfillter
        If UserDataCourseFile.DefaultView.Count = 0 Then
            CallAPIUpdateLog(Token, "complete", "class", "{" & Chr(34) & "class_id" & Chr(34) & ":" & ClassID.ToString & "}")
        Else
            CallAPIUpdateLog(Token, "complete", "document", "{" & Chr(34) & "class_id" & Chr(34) & ":" & ClassID.ToString & "," & Chr(34) & "document_id" & Chr(34) & ":" & doc_id & "}")
        End If
        UserDataCourseFile.DefaultView.RowFilter = ""
    End Sub
#End Region

    Public Function CreateClass(token As String, stdID As String, ClientID As String, CourseID As String, UserID As String) As Long
        Dim ret As Long = 0
        Try
            Dim info As String = ""
            info = GetStringDataFromURL(GetWebServiceURL() & "api/class/create", token & "&client_id=" & ClientID & "&course_id=" & CourseID & "&user_id=" & UserID & "&student_id_list=" & stdID)

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
    End Function

    Public Function AddUser(ByVal Token As String, ByVal UserID As String, CourseID As String) As String()
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
                                Case "company_id"
                                    ret(3) = comment.Value.ToString
                            End Select
                        Next

                End Select
            Next
        End If
        Return ret
    End Function

#Region "Testing"
    Public Function GetTesting(UserSessionID As Long) As DataTable
        Dim sql As String = " select * from TB_TESTING where tb_user_session_id=@_USER_SESSION_ID"
        Dim p(1) As SqlParameter
        p(0) = SqlDB.SetBigInt("@_USER_SESSION_ID", UserSessionID)

        Dim dt As DataTable = LinqDB.ConnectDB.SqlDB.ExecuteTable(sql, p)

        Return dt
    End Function

    Public Function GetTestQuestion(TestID As Long, QuestionNo As Integer)
        Dim p(2) As SqlParameter
        p(0) = SqlDB.SetBigInt("@_TESTING_ID", TestID)
        p(1) = SqlDB.SetInt("@_QUESTION_NO", QuestionNo)
        Dim lnq As New TbTestingQuestionLinqDB
        Dim dt As DataTable = lnq.GetDataList("tb_testing_id=@_TESTING_ID and question_no=@_QUESTION_NO", "", Nothing, p)
        Return dt
    End Function

    Public Function GetTestQuestion(TestID As Long)
        Dim p(1) As SqlParameter
        p(0) = SqlDB.SetBigInt("@_TESTING_ID", TestID)
        Dim lnq As New TbTestingQuestionLinqDB
        Dim dt As DataTable = lnq.GetDataList("tb_testing_id=@_TESTING_ID ", "", Nothing, p)
        Return dt
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
