Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel
Imports Newtonsoft.Json.Linq
Imports LinqDB.ConnectDB
Imports System.Data.SqlClient

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
' <System.Web.Script.Services.ScriptService()> _
<System.Web.Script.Services.ScriptService()>
<WebService(Namespace:="http://tempuri.org/")>
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)>
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Public Class WebService
    Inherits System.Web.Services.WebService

    <WebMethod()>
    Public Function HelloWorld() As String
        Return "Hello World"
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

                                    Dim FileName As String = file_comment("id").ToString & GetURLFileExtension(file_comment("file").ToString)
                                    Dim DocFileID As String = EnCripText(FileName)
                                    'Dim DocFileName As String = "null"

                                    Dim cfLnq As New LinqDB.TABLE.TbUserCourseDocumentFileLinqDB
                                    cfLnq.DOCUMENT_FILE_ID = file_comment("id").ToString
                                    cfLnq.TB_USER_COURSE_DOCUMENT_ID = cdLnq.ID
                                    cfLnq.USER_ID = UserData.UserID
                                    cfLnq.FILE_TITLE = file_comment("title").ToString
                                    cfLnq.FILE_URL = StoryFile
                                    cfLnq.ORDER_BY = file_comment("order").ToString

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
                    trans.CommitTransaction()

                    CreateUserClass(UserSessionID, UserData.UserName, UserData.UserID, CourseID, UserData.Token)

                    Return "True"
                Else
                    trans.RollbackTransaction()
                    Return "False"
                End If
            End If
        Catch ex As Exception
            Return "False"
        End Try
    End Function


#Region "Create Class"
    'Private Function CreateClass(stdID As String) As Long
    '    Dim ret As Long = 0
    '    Try
    '        Dim info As String = ""
    '        info = GetStringDataFromURL(WebServiceURL & "api/class/create", Token & "&client_id=" & myUser.ClientID & "&course_id=" & CourseID & "&user_id=" & myUser.UserID & "&student_id_list=" & stdID)

    '        Dim json As String = info
    '        Dim ser As JObject = JObject.Parse(json)
    '        Dim data As List(Of JToken) = ser.Children().ToList

    '        If data.Count = 3 Then
    '            If DirectCast(data(0), JProperty).Value.ToString.ToLower = "true" Then
    '                ret = DirectCast(data(2), JProperty).Value
    '            End If
    '        End If
    '    Catch ex As Exception
    '        ret = 0
    '    End Try
    '    Return ret
    'End Function



    Private Sub CreateUserClass(UserSessionID As Long, UserName As String, ByVal UserID As String, CourseID As String, Token As String)

        Try
            Dim info As String = GetStringDataFromURL(GetWebServiceURL() & "api/class/create", Token & "&course_id=" & CourseID & "&user_id=" & UserID & "&student_id_list=" & UserID)
            Dim json As String = info
            Dim ser As JObject = JObject.Parse(json)
            Dim data As List(Of JToken) = ser.Children().ToList

            If Data.Count = 3 Then
                If DirectCast(Data(0), JProperty).Value.ToString.ToLower = "true" Then
                    'ret = DirectCast(data(2), JProperty).Value

                    Dim lnq As New LinqDB.TABLE.TbUserSessionLinqDB
                    lnq.GetDataByPK(UserSessionID, Nothing)
                    If lnq.ID > 0 Then
                        lnq.CURRENT_CLASS_ID = Convert.ToInt64(DirectCast(Data(2), JProperty).Value)

                        Dim trans As New LinqDB.ConnectDB.TransactionDB
                        If lnq.UpdateData(UserName, trans.Trans).IsSuccess = True Then
                            trans.CommitTransaction()
                        Else
                            trans.RollbackTransaction()
                        End If
                    End If
                    lnq = Nothing
                End If
            End If
        Catch ex As Exception

        End Try








        'info = GetStringDataFromURL(GetWebServiceURL() & "api/user/get", Token & "&user_company_id=" & UserID & "&course_id=" & CourseID)
        'If info.Trim <> "" Then
        '    Dim json As String = info
        '    Dim ser As JObject = JObject.Parse(json)
        '    Dim data As List(Of JToken) = ser.Children().ToList
        '    Dim output As String = ""

        '    For Each item As JProperty In data
        '        item.CreateReader()
        '        Select Case item.Name
        '            Case "user"
        '                For Each comment As JProperty In item.Values
        '                    Select Case comment.Name
        '                        Case "id"
        '                            UserID = comment.Value.ToString
        '                            Exit For
        '                            'Case "firstname"
        '                            '    ret(1) = comment.Value.ToString
        '                            'Case "lastname"
        '                            '    ret(2) = comment.Value.ToString
        '                            'Case "company_id"
        '                            '    ret(3) = comment.Value.ToString
        '                    End Select
        '                Next
        '        End Select
        '    Next

        '    If UserID.Trim <> "" Then

        '    End If
        'End If
    End Sub

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
End Class