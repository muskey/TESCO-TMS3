﻿Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel
Imports Newtonsoft.Json.Linq
Imports LinqDB.ConnectDB
Imports System.Data.SqlClient


' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
'<System.Web.Script.Services.ScriptService()>
<System.Web.Script.Services.ScriptService()>
<WebService(Namespace:="http://tempuri.org/")>
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)>
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Public Class WebService
    Inherits System.Web.Services.WebService

    '<WebMethod()>
    'Public Function HelloWorld() As String
    '    Return "Hello World"
    'End Function

    <WebMethod()>
    Public Function CreateLogTrans(LoginHistoryID As Long, LogMsg As String) As String
        Dim ret As String = "false"
        'LogFileBL.LogTrans(LoginHistoryID, LogMsg)
        Return "true"
    End Function

    <WebMethod()>
    Public Function LoadCoureseDescByID(id) As String
        Try
            Dim strSql As New StringBuilder
            Dim strdata As String
            Dim sql As String = "select course_desc "
            sql += " from tb_user_course "
            sql += " where id=@_ID"
            Dim p(1) As SqlParameter
            p(0) = SqlDB.SetBigInt("@_ID", id)

            Dim dt As DataTable = SqlDB.ExecuteTable(sql, p)
            If dt.Rows.Count > 0 Then
                strdata = dt.Rows(0)(0) & ""
            Else
                strdata = ""
            End If
            Return strdata
        Catch ex As Exception
            Return ""
        End Try

    End Function

    <WebMethod()>
    Public Function SetDocumentData(id As String, UserSessionID As String) As String
        Try
            Dim trans As New TransactionDB
            Dim ret As New ExecuteDataInfo
            Dim document_txt As String = ""
            Dim CourseID As Long = 0
            Dim sql As String = " select document_detail, course_id from TB_USER_COURSE where id=@_ID"
            Dim p(1) As SqlParameter
            p(0) = SqlDB.SetBigInt("@_ID", id)

            Dim dt As DataTable = SqlDB.ExecuteTable(sql, trans.Trans, p)
            If dt.Rows.Count > 0 Then
                sql = "delete from TB_USER_COURSE_DOCUMENT_FILE where tb_user_course_document_id in (select id from TB_USER_COURSE_DOCUMENT where tb_user_course_id=@_ID) "
                ReDim p(1)
                p(0) = SqlDB.SetBigInt("@_ID", id)
                ret = SqlDB.ExecuteNonQuery(sql, trans.Trans, p)

                If ret.IsSuccess = True Then
                    sql = " delete from TB_USER_COURSE_DOCUMENT  where tb_user_course_id=@_ID"
                    ReDim p(1)
                    p(0) = SqlDB.SetBigInt("@_ID", id)
                    ret = SqlDB.ExecuteNonQuery(sql, trans.Trans, p)

                    If ret.IsSuccess = True Then
                        If Convert.IsDBNull(dt.Rows(0)("document_detail")) = False Then
                            document_txt = dt.Rows(0)("document_detail")
                        End If
                        CourseID = dt.Rows(0)("course_id")
                    End If
                End If
            End If
            dt.Dispose()

            If ret.IsSuccess = False Then
                trans.RollbackTransaction()
                Return "False"
            End If

            Dim UserData As New UserProfileData
            UserData.GetUserSessionData(UserSessionID)

            If document_txt.Trim <> "" Then
                Dim document_ser As JObject = JObject.Parse(document_txt)
                Dim document_data As List(Of JToken) = document_ser.Children().ToList
                For Each document_item As JProperty In document_data

                    Dim cdi As Integer = 1
                    For Each document_comment As JObject In document_item.Values
                        document_item.CreateReader()

                        Dim cdLnq As New LinqDB.TABLE.TbUserCourseDocumentLinqDB
                        cdLnq.DOCUMENT_ID = Convert.ToInt64(document_comment("id").ToString)
                        cdLnq.TB_USER_COURSE_ID = id
                        cdLnq.USER_ID = UserData.UserID
                        cdLnq.DOCUMENT_TITLE = document_comment("title").ToString
                        cdLnq.MS_DOCUMENT_ICON_ID = document_comment("icon_id").ToString
                        cdLnq.DOCUMENT_VERSION = document_comment("version").ToString
                        cdLnq.DOCUMENT_TYPE = document_comment("type").ToString
                        cdLnq.ORDER_BY = document_comment("order").ToString

                        ret = cdLnq.InsertData(UserData.UserName, trans.Trans)
                        If ret.IsSuccess = True Then
                            Dim file_txt As String = "{""file"":" & document_comment("file").ToString & "}"
                            Dim file_ser As JObject = JObject.Parse(file_txt)
                            Dim file_data As List(Of JToken) = file_ser.Children().ToList
                            For Each file_item As JProperty In file_data

                                Dim cdfi As Integer = 1
                                For Each file_comment As JObject In file_item.Values
                                    file_item.CreateReader()

                                    Dim vUrl As New Uri(file_comment("file"))
                                    Dim vFile As String = vUrl.Segments(vUrl.Segments.Length - 1)

                                    Dim StoryFile As String = file_comment("file").ToString
                                    If vFile.StartsWith("index_lms.html") = True Then
                                        StoryFile = file_comment("file").ToString.Replace("index_lms.html", "story.html")

                                    End If

                                    Dim FileExt As String = GetURLFileExtension(file_comment("file").ToString)
                                    Dim FileName As String = file_comment("id").ToString & FileExt
                                    Dim DocFileID As String = EnCripText(FileName)
                                    'Dim DocFileName As String = "null"

                                    Dim cfLnq As New LinqDB.TABLE.TbUserCourseDocumentFileLinqDB
                                    cfLnq.DOCUMENT_FILE_ID = file_comment("id").ToString
                                    cfLnq.TB_USER_COURSE_DOCUMENT_ID = cdLnq.ID
                                    cfLnq.USER_ID = UserData.UserID
                                    cfLnq.FILE_TITLE = file_comment("title").ToString
                                    cfLnq.FILE_URL = StoryFile
                                    cfLnq.ORDER_BY = file_comment("order").ToString
                                    If FileExt.ToLower = ".pdf" Then
                                        cfLnq.IS_CONVERT = "N"
                                    Else
                                        cfLnq.IS_CONVERT = "Z"
                                    End If

                                    ret = cfLnq.InsertData(UserData.UserName, trans.Trans)
                                    If ret.IsSuccess = False Then
                                        trans.RollbackTransaction()
                                        Return "False"
                                    End If
                                    cfLnq = Nothing
                                    cdfi += 1
                                Next
                            Next
                        Else
                            trans.RollbackTransaction()
                            Return "False"
                        End If
                        cdLnq = Nothing
                        cdi += 1
                    Next
                Next

                If ret.IsSuccess = True Then
                    Dim cLnq As New LinqDB.TABLE.TbUserCourseLinqDB
                    cLnq.GetDataByPK(id, trans.Trans)
                    If cLnq.ID > 0 Then
                        cLnq.BIND_DOCUMENT = "Y"

                        ret = cLnq.UpdateData(UserData.UserName, trans.Trans)
                        If ret.IsSuccess = True Then
                            ret = UpdateUserCourseHistory(cLnq.USER_ID, UserData.UserName, cLnq.COURSE_ID, cLnq.COURSE_TITLE, cLnq.COURSE_DESC, cLnq.ID, trans)

                            If ret.IsSuccess = True Then
                                trans.CommitTransaction()
                                Return "True"
                            Else
                                trans.RollbackTransaction()
                                Return "False"
                            End If
                        Else
                            trans.RollbackTransaction()
                            Return "False"
                        End If
                    Else
                        trans.RollbackTransaction()
                        Return "False"
                    End If
                Else
                    trans.RollbackTransaction()
                    Return "False"
                End If
            End If
        Catch ex As Exception
            Return "False"
        End Try
    End Function

    Private Function UpdateUserCourseHistory(UserID As Long, UserName As String, CourseID As Long, CourseTitle As String, CourseDesc As String, UserCourseID As Long, trans As TransactionDB) As ExecuteDataInfo
        Dim ret As New ExecuteDataInfo
        Try
            Dim lnq As New LinqDB.TABLE.TbUserCourseHisLinqDB
            lnq.ChkDataByCOURSE_ID_USER_ID(CourseID, UserID, trans.Trans)

            lnq.USER_ID = UserID
            lnq.USERNAME = UserName
            lnq.COURSE_ID = CourseID
            lnq.COURSE_TITLE = CourseTitle
            lnq.COURSE_DESC = CourseDesc

            If lnq.ID > 0 Then
                ret = lnq.UpdateData(UserName, trans.Trans)
            Else
                lnq.IS_FINISHED = "N"
                ret = lnq.InsertData(UserName, trans.Trans)
            End If

            If ret.IsSuccess = True Then
                Dim dLnq As New LinqDB.TABLE.TbUserCourseDocumentLinqDB
                Dim p(1) As SqlParameter
                p(0) = SqlDB.SetBigInt("@_USER_COURSE_ID", UserCourseID)
                Dim dDt As DataTable = dLnq.GetDataList("tb_user_course_id=@_USER_COURSE_ID", "", trans.Trans, p)
                If dDt.Rows.Count > 0 Then
                    For Each dDr As DataRow In dDt.Rows
                        Dim dhLnq As New LinqDB.TABLE.TbUserCourseDocHisLinqDB
                        dhLnq.ChkDataByDOCUMENT_ID_USER_ID(dDr("document_id"), UserID, trans.Trans)

                        If dhLnq.ID = 0 Then
                            dhLnq.TB_USER_COURSE_HIS_ID = lnq.ID
                            dhLnq.USER_ID = UserID
                            dhLnq.USERNAME = UserName
                            dhLnq.DOCUMENT_ID = dDr("document_id")
                            dhLnq.DOCUMENT_TITLE = dDr("document_title")

                            ret = dhLnq.InsertData(UserName, trans.Trans)
                            If ret.IsSuccess = False Then
                                Exit For
                            End If
                        End If

                        Dim fLnq As New LinqDB.TABLE.TbUserCourseDocumentFileLinqDB
                        ReDim p(1)
                        p(0) = SqlDB.SetBigInt("@_USER_COURSE_DOCUMENT_ID", dDr("id"))
                        Dim fDt As DataTable = fLnq.GetDataList("tb_user_course_document_id=@_USER_COURSE_DOCUMENT_ID", "", trans.Trans, p)
                        If fDt.Rows.Count > 0 Then
                            For Each fDr As DataRow In fDt.Rows
                                Dim fhLnq As New LinqDB.TABLE.TbUserCourseDocFileHisLinqDB
                                fhLnq.ChkDataByDOCUMENT_FILE_ID_USER_ID(fDr("document_file_id"), UserID, trans.Trans)
                                If fhLnq.ID = 0 Then
                                    fhLnq.TB_USER_COURSE_DOC_HIS_ID = dhLnq.ID
                                    fhLnq.USER_ID = UserID
                                    fhLnq.USERNAME = UserName
                                    fhLnq.DOCUMENT_FILE_ID = fDr("document_file_id")
                                    fhLnq.FILE_TITLE = fDr("file_title")
                                    fhLnq.FILE_URL = fDr("file_url")
                                    fhLnq.IS_STUDY = "N"

                                    ret = fhLnq.InsertData(UserName, trans.Trans)
                                    If ret.IsSuccess = False Then
                                        Exit For
                                    End If
                                End If
                                fhLnq = Nothing
                            Next

                            If ret.IsSuccess = False Then
                                Exit For
                            End If
                        End If
                        fDt.Dispose()
                        dhLnq = Nothing
                    Next
                End If
                dDt.Dispose()
            End If

        Catch ex As Exception
            ret.IsSuccess = False
            ret.ErrorMessage = "Exception " & ex.Message & vbNewLine & ex.StackTrace
        End Try
        Return ret
    End Function

#Region "Create Class"
    <WebMethod()>
    Public Function CreateUserClass(id As String, UserSessionID As String) As String

        Dim sql As String = " select  course_id from TB_USER_COURSE where id=@_ID"
        Dim p(1) As SqlParameter
        p(0) = SqlDB.SetBigInt("@_ID", id)

        Dim dt As DataTable = SqlDB.ExecuteTable(sql, p)
        If dt.Rows.Count > 0 Then
            Dim CourseID As Long = dt.Rows(0)("course_id")

            Dim UserData As New UserProfileData
            UserData.GetUserSessionData(UserSessionID)

            'LogFileBL.LogTrans(UserData.LoginHistoryID, "Create UserClass")
            Try
                Dim info As String = GetStringDataFromURL(GetWebServiceURL() & "api/class/create", UserData.LoginHistoryID, UserData.Token & "&course_id=" & CourseID & "&user_id=" & UserData.UserID & "&student_id_list=" & UserData.UserID)
                Dim json As String = info
                Dim ser As JObject = JObject.Parse(json)
                Dim data As List(Of JToken) = ser.Children().ToList

                If data.Count = 3 Then
                    If DirectCast(data(0), JProperty).Value.ToString.ToLower = "true" Then
                        'ret = DirectCast(data(2), JProperty).Value

                        Dim lnq As New LinqDB.TABLE.TbUserSessionLinqDB
                        lnq.GetDataByPK(UserSessionID, Nothing)
                        If lnq.ID > 0 Then
                            lnq.CURRENT_CLASS_ID = Convert.ToInt64(DirectCast(data(2), JProperty).Value)

                            Dim trans As New TransactionDB
                            If lnq.UpdateData(UserData.UserName, trans.Trans).IsSuccess = True Then
                                trans.CommitTransaction()
                                'LogFileBL.LogTrans(UserData.LoginHistoryID, "New UserClass ID=" & lnq.CURRENT_CLASS_ID)
                                Return "True"
                            Else
                                trans.RollbackTransaction()
                                Return "False|" & lnq.ErrorMessage
                            End If
                        End If
                        lnq = Nothing
                    End If
                End If
            Catch ex As Exception
                Return "False|Exception " & ex.Message
            End Try
        End If
    End Function

#End Region

#Region "Convert DataTable To Json"
    Public Function ConvertDataTableToJson(ByVal dt As DataTable) As String
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        serializer.MaxJsonLength = Int32.MaxValue
        Dim rows As New List(Of Dictionary(Of String, Object))
        Dim row As Dictionary(Of String, Object)
        For Each dr As DataRow In dt.Rows
            row = New Dictionary(Of String, Object)
            For Each col As DataColumn In dt.Columns
                row.Add(col.ColumnName, dr(col))
            Next
            rows.Add(row)
        Next
        Return serializer.Serialize(rows)
    End Function
#End Region

    <WebMethod()>
    Public Function GetLoginStatus(UserName As String) As String
        'Dim ret As String = "false#false#"
        'Return ret

        Dim ret As String = ""
        Dim info As String = ""
        info = GetStringDataFromURL(GetWebServiceURL() & "api/GetLoginStatus", 0, "employee_id=" & UserName)

        If info.Trim <> "" Then
            Dim json As String = info
            Dim ser As JObject = JObject.Parse(json)
            Dim data As List(Of JToken) = ser.Children().ToList

            For Each item As JProperty In data
                item.CreateReader()

                Select Case item.Name
                    Case "status"
                        If Convert.ToBoolean(item.First) = False Then
                            Exit For
                        End If
                    Case "is_first_time_login"
                        ret += item.First.ToString.ToLower & "#"
                    'Case "is_telephone_existed"
                    '    ret += item.First.ToString.ToLower & "#"
                    Case "telephone"
                        If item.First.ToString.Trim = "" Then
                            ret += "true#"
                        Else
                            Dim MobileNo As String = item.First.ToString.Trim
                            ret += "false#" & MobileNo.Substring(0, MobileNo.Length - 4) & "XXXX"
                        End If
                End Select
            Next
        End If

        'ret = "is_first_time_login#is_telephone_existed#mobile_no
        Return ret
    End Function


End Class