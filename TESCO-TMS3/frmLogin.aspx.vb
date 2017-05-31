'Imports Newtonsoft.Json
Imports System.Data.SqlClient
Imports Newtonsoft.Json.Linq
Imports LinqDB.ConnectDB
Imports LinqDB.TABLE

Public Class frmLogin
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Private Sub txtUsername_TextChanged(sender As Object, e As EventArgs) Handles txtUsername.TextChanged
        'Call Backend เพื่อเช็คว่าเป็นการ Login ครั้งแรก หรือเช็คว่ามีเบอร์โทรหรือไม่มี
    End Sub

    Private Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
        If Login(txtUsername.Text, txtPassword.Text) = True Then
            Session("Username") = txtUsername.Text
            Response.Redirect("frmSelectFormat.aspx?rnd=" & DateTime.Now.Millisecond)
        End If
    End Sub

#Region "Load Data Login"
    Private Function Login(ByVal Username As String, ByVal Password As String) As Boolean
        Dim ret As Boolean = False
        Try
            Dim info As String = ""
            info = GetStringDataFromURL(GetWebServiceURL() & "api/login", "username=" & Username & "&password=" & Password)
            If info.Trim <> "" Then
                Dim json As String = info
                Dim ser As JObject = JObject.Parse(json)
                Dim data As List(Of JToken) = ser.Children().ToList
                Dim output As String = ""

                Dim FirstNameThai As String = ""
                Dim LastNameThai As String = ""
                Dim FirstNameEng As String = ""
                Dim LastNameEng As String = ""
                Dim IsTeacher As String = ""
                Dim FormatData As JToken
                Dim MessageData As JProperty

                Dim UserData = New UserProfileData
                UserData.UserName = Username
                For Each item As JProperty In data
                    item.CreateReader()

                    Select Case item.Name
                        Case "token"
                            UserData.TokenStr = item.First
                            UserData.Token = "token=" & UserData.TokenStr
                        Case "firstname"
                            If item.First.ToString <> "" Then FirstNameEng = item.First.ToString
                        Case "lastname"
                            If item.First.ToString <> "" Then LastNameEng = item.First.ToString
                        Case "firstname_thai"
                            If item.First.ToString <> "" Then FirstNameThai = item.First.ToString
                        Case "lastname_thai"
                            If item.First.ToString <> "" Then LastNameThai = item.First.ToString
                        Case "user_id"
                            UserData.UserID = item.First
                        Case "is_teacher"
                            IsTeacher = item.First.ToString
                        Case "data"

                            ClearUserSession(Username)
                            FormatData = item.First
                        Case "welcome"
                            'BuiltDatableTableUserMessage(item)
                            MessageData = item
                    End Select
                Next

                Dim FirstName As String = ""
                Dim LastName As String = ""
                If FirstNameThai.Trim = "" Then
                    FirstName = FirstNameEng
                Else
                    FirstName = FirstNameThai
                End If

                If LastNameThai.Trim = "" Then
                    LastName = LastNameEng
                Else
                    LastName = LastNameThai
                End If

                UserData.FullName = FirstName & " " & LastName

                Dim re As ExecuteDataInfo = CreateUserSession(UserData.TokenStr, UserData.UserID, Username, FirstNameEng, LastNameEng, FirstNameThai, LastNameThai, IsTeacher, FormatData, MessageData)
                If re.IsSuccess = True Then
                    UserData.UserSessionID = re.NewID
                    Session("UserData") = UserData
                    'GetDatableTableFromTesting(UserData)
                    ret = re.IsSuccess
                End If
            End If
        Catch ex As Exception
            'MessageBox.Show(ex.Message, "Attention", MessageBoxButtons.OK, MessageBoxIcon.Information)
            ScriptManager.RegisterStartupScript(Me, Me.GetType, Guid.NewGuid().ToString(), "alert('" & ex.Message & "')", True)
        End Try

        Return ret
    End Function

    Private Function CreateUserSession(TokenStr As String, UserID As Long, Username As String, FirstNameEng As String, LastNameEng As String, FirstNameThai As String, LastNameThai As String, IsTeacher As String, FormatData As JToken, MessageData As JProperty) As ExecuteDataInfo
        Dim ret As New ExecuteDataInfo
        Try
            Dim lnq As New TbUserSessionLinqDB
            lnq.TOKEN = TokenStr
            lnq.USER_ID = UserID
            lnq.USERNAME = Username
            lnq.FIRST_NAME_ENG = FirstNameEng
            lnq.LAST_NAME_ENG = LastNameEng
            lnq.FIRST_NAME_THAI = FirstNameThai
            lnq.LAST_NAME_THAI = LastNameThai
            lnq.IS_TEACHER = IsTeacher

            Dim trans As New TransactionDB
            ret = lnq.InsertData(Username, trans.Trans)
            If ret.IsSuccess = True Then
                Dim _newID As Long = lnq.ID

                Dim hLnq As New TbLoginHistoryLinqDB
                hLnq.TOKEN = lnq.TOKEN
                hLnq.USERNAME = lnq.USERNAME
                hLnq.FIRST_NAME_ENG = lnq.FIRST_NAME_ENG
                hLnq.LAST_NAME_ENG = lnq.LAST_NAME_ENG
                hLnq.FIRST_NAME_THAI = lnq.FIRST_NAME_THAI
                hLnq.LAST_NAME_THAI = lnq.LAST_NAME_THAI
                hLnq.LOGON_TIME = DateTime.Now
                hLnq.CLIENT_IP = HttpContext.Current.Request.UserHostAddress
                hLnq.CLIENT_BROWSER = HttpContext.Current.Request.Browser.Browser & " Version:" & HttpContext.Current.Request.Browser.Version
                hLnq.SERVER_URL = HttpContext.Current.Request.Url.AbsoluteUri

                ret = hLnq.InsertData(Username, trans.Trans)
                If ret.IsSuccess = True Then
                    ret = BuiltUserFormat(lnq.ID, lnq.USER_ID, Username, FormatData, trans)
                    If ret.IsSuccess = True Then
                        ret = BuiltDatableTableUserMessage(MessageData, lnq.ID, UserID, Username, trans)

                        If ret.IsSuccess = True Then
                            ret.NewID = _newID
                            trans.CommitTransaction()
                        Else
                            trans.RollbackTransaction()
                        End If
                    Else
                        trans.RollbackTransaction()
                    End If
                Else
                    trans.RollbackTransaction()
                End If
            Else
                trans.RollbackTransaction()
            End If
        Catch ex As Exception
            ret.IsSuccess = False
            ret.ErrorMessage = "Exception " & ex.Message
        End Try
        Return ret
    End Function

    Private Sub ClearUserSession(Username As String)

        Dim uLnq As New LinqDB.TABLE.TbUserSessionLinqDB
        uLnq.ChkDataByUSERNAME(Username, Nothing)
        If uLnq.ID > 0 Then
            Dim trans As New TransactionDB
            Dim sql As String = ""
            Dim ret As New ExecuteDataInfo

            sql = "delete from TB_USER_COURSE_DOCUMENT_FILE where user_id=@_USER_ID "
            Dim p(1) As SqlParameter
            p(0) = SqlDB.SetBigInt("@_USER_ID", uLnq.USER_ID)

            ret = SqlDB.ExecuteNonQuery(sql, trans.Trans, p)

            If ret.IsSuccess = True Then
                sql = "delete from TB_USER_COURSE_DOCUMENT where user_id=@_USER_ID"
                ReDim p(1)
                p(0) = SqlDB.SetBigInt("@_USER_ID", uLnq.USER_ID)
                ret = SqlDB.ExecuteNonQuery(sql, trans.Trans, p)

                If ret.IsSuccess = True Then
                    sql = "delete from TB_USER_COURSE where user_id=@_USER_ID"
                    ReDim p(1)
                    p(0) = SqlDB.SetBigInt("@_USER_ID", uLnq.USER_ID)
                    ret = SqlDB.ExecuteNonQuery(sql, trans.Trans, p)

                    If ret.IsSuccess = True Then
                        sql = "delete from TB_USER_DEPARTMENT where user_id=@_USER_ID"
                        ReDim p(1)
                        p(0) = SqlDB.SetBigInt("@_USER_ID", uLnq.USER_ID)
                        ret = SqlDB.ExecuteNonQuery(sql, trans.Trans, p)

                        If ret.IsSuccess = True Then
                            sql = "delete from TB_USER_FUNCTION where user_id=@_USER_ID"
                            ReDim p(1)
                            p(0) = SqlDB.SetBigInt("@_USER_ID", uLnq.USER_ID)
                            ret = SqlDB.ExecuteNonQuery(sql, trans.Trans, p)

                            If ret.IsSuccess = True Then
                                sql = "delete from TB_USER_FORMAT where user_id=@_USER_ID"
                                ReDim p(1)
                                p(0) = SqlDB.SetBigInt("@_USER_ID", uLnq.USER_ID)
                                ret = SqlDB.ExecuteNonQuery(sql, trans.Trans, p)

                                If ret.IsSuccess = True Then
                                    sql = "delete from TB_USER_MESSAGE where user_id=@_USER_ID"
                                    ReDim p(1)
                                    p(0) = SqlDB.SetBigInt("@_USER_ID", uLnq.USER_ID)
                                    ret = SqlDB.ExecuteNonQuery(sql, trans.Trans, p)

                                    If ret.IsSuccess = True Then
                                        sql = "delete from TB_TESTING_ANSWER where tb_testing_id  in ( select id From TB_TESTING  Where tb_user_session_id  In (Select id from TB_USER_SESSION where user_id=@_USER_ID))"
                                        ReDim p(1)
                                        p(0) = SqlDB.SetBigInt("@_USER_ID", uLnq.USER_ID)
                                        ret = SqlDB.ExecuteNonQuery(sql, trans.Trans, p)

                                        If ret.IsSuccess = True Then
                                            sql = "delete from TB_TESTING_QUESTION where tb_testing_id  in ( select id From TB_TESTING  Where tb_user_session_id  In (Select id from TB_USER_SESSION where user_id=@_USER_ID))"
                                            ReDim p(1)
                                            p(0) = SqlDB.SetBigInt("@_USER_ID", uLnq.USER_ID)
                                            ret = SqlDB.ExecuteNonQuery(sql, trans.Trans, p)
                                            If ret.IsSuccess = True Then
                                                sql = "delete From TB_TESTING  Where tb_user_session_id  In (Select id from TB_USER_SESSION where user_id=@_USER_ID)"
                                                ReDim p(1)
                                                p(0) = SqlDB.SetBigInt("@_USER_ID", uLnq.USER_ID)
                                                ret = SqlDB.ExecuteNonQuery(sql, trans.Trans, p)

                                                If ret.IsSuccess = True Then
                                                    sql = "delete from TB_USER_SESSION where user_id=@_USER_ID"
                                                    ReDim p(1)
                                                    p(0) = SqlDB.SetBigInt("@_USER_ID", uLnq.USER_ID)
                                                    ret = SqlDB.ExecuteNonQuery(sql, trans.Trans, p)
                                                End If
                                            End If
                                        End If
                                    End If
                                End If
                            End If
                        End If
                    End If
                End If
            End If

            If ret.IsSuccess = True Then
                trans.CommitTransaction()
            Else
                trans.RollbackTransaction()
            End If
        End If
        uLnq = Nothing
    End Sub

    Private Function BuiltUserFormat(UserSessionID As Long, UserID As Long, Username As String, data2 As JToken, trans As TransactionDB) As ExecuteDataInfo
        Dim ret As New ExecuteDataInfo

        For Each comment As JProperty In data2
            comment.CreateReader()
            Select Case comment.Name
                Case "format"
                    For Each f As JObject In comment.Values
                        If f("id").ToString = "1" Then Continue For   'ถ้าเป็น Format Notset ก็ไม่ต้องให้แสดง

                        Dim lnq As New TbUserFormatLinqDB
                        lnq.TB_USER_SESSION_ID = UserSessionID
                        lnq.USER_ID = UserID
                        lnq.FORMAT_ID = f("id").ToString
                        lnq.FORMAT_TITLE = f("title").ToString

                        ret = lnq.InsertData(Username, trans.Trans)
                        If ret.IsSuccess = True Then
                            Dim jProp As JObject = JObject.Parse("{""function"":" & f("function").ToString & "}")
                            ret = BuiltUserFunction(lnq.ID, lnq.FORMAT_ID, UserID, Username, jProp, trans)
                            If ret.IsSuccess = False Then
                                Exit For
                            End If
                        Else
                            Exit For
                        End If
                    Next
            End Select
        Next
        Return ret
    End Function

    Private Function BuiltUserFunction(UserFormatID As Long, FormatID As Integer, UserID As Long, Username As String, data_ser As JObject, trans As TransactionDB) As ExecuteDataInfo
        Dim ret As New ExecuteDataInfo
        Dim data As List(Of JToken) = data_ser.Children().ToList
        For Each item As JProperty In data
            If item.Values.Count = 0 Then
                ret.IsSuccess = True
                Exit For
            End If
            For Each comment As JObject In item.Value
                comment.CreateReader()

                Dim lnq As New TbUserFunctionLinqDB
                lnq.TB_USER_FORMAT_ID = UserFormatID
                lnq.USER_ID = UserID
                lnq.FORMAT_ID = FormatID
                lnq.FUNCTION_ID = comment("id").ToString
                lnq.FUNCTION_TITLE = comment("title").ToString
                lnq.FUNCTION_COVER_URL = comment("cover").ToString
                lnq.FUNCTION_COVER_COLOR = comment("color").ToString
                lnq.FUNCTION_SUBJECT_TYPE = comment("subject_type").ToString   'main subject / additional subject

                ret = lnq.InsertData(Username, trans.Trans)
                If ret.IsSuccess = True Then
                    Dim jProp As JObject = JObject.Parse("{""department"":" & comment("department").ToString & "}")
                    ret = BuiltFuserDepartment(lnq.ID, lnq.FUNCTION_ID, UserID, Username, jProp, trans)

                    If ret.IsSuccess = False Then
                        Exit For
                    End If
                Else
                    Exit For
                End If
                lnq = Nothing
            Next
        Next
        Return ret
    End Function

    Private Function BuiltFuserDepartment(UserFunctionID As Long, FunctionID As Integer, UserID As Long, Username As String, data_ser As JObject, trans As TransactionDB) As ExecuteDataInfo
        Dim ret As New ExecuteDataInfo
        Dim data As List(Of JToken) = data_ser.Children().ToList

        For Each item As JProperty In data
            If item.Values.Count = 0 Then
                ret.IsSuccess = True
                Exit For
            End If

            For Each desc As JObject In item.Values
                desc.CreateReader()

                Dim lnq As New TbUserDepartmentLinqDB
                lnq.TB_USER_FUNCTION_ID = UserFunctionID
                lnq.FUNCTION_ID = FunctionID
                lnq.USER_ID = UserID
                lnq.DEPARTMENT_ID = desc("id").ToString
                lnq.DEPARTMENT_TITLE = desc("title").ToString
                lnq.DEPARTMENT_COVER_URL = desc("cover").ToString
                ret = lnq.InsertData(Username, trans.Trans)
                If ret.IsSuccess = True Then
                    ret = BindDatableTableFromCourse(lnq.ID, UserID, Username, desc.Last, trans)
                    If ret.IsSuccess = False Then
                        Exit For
                    End If
                Else
                    Exit For
                End If
            Next
        Next
        Return ret
    End Function

    Private Function BindDatableTableFromCourse(UserDepartmentID As Long, UserID As Long, Username As String, data As JProperty, trans As TransactionDB) As ExecuteDataInfo
        Dim ret As New ExecuteDataInfo

        Dim item As JProperty = data
        If item.Values.Count = 0 Then
            ret.IsSuccess = True
            Return ret
        End If

        Dim ci As Integer = 1
        For Each comment As JObject In item.Values
            Try
                Dim lnq As New TbUserCourseLinqDB
                lnq.TB_USER_DEPARTMENT_ID = UserDepartmentID
                lnq.USER_ID = UserID
                lnq.DEPARTMENT_ID = comment("department_id").ToString
                lnq.COURSE_ID = comment("id").ToString
                lnq.COURSE_TITLE = comment("name").ToString
                lnq.COURSE_DESC = comment("description").ToString
                lnq.COURSE_TYPE = comment("type").ToString
                lnq.ICON_URL = comment("icon").ToString
                lnq.COVER_URL = comment("cover").ToString
                lnq.SORT = ci
                lnq.IS_DOCUMENT_LOCK = IIf(comment("is_document_lock").ToString.ToLower = "true", "Y", "N")
                lnq.DOCUMENT_DETAIL = "{""document"":" & comment("document").ToString & "}"
                lnq.BIND_DOCUMENT = "N"

                ret = lnq.InsertData(Username, trans.Trans)
                If ret.IsSuccess = False Then
                    Exit For
                End If
            Catch ex As Exception
                ret.IsSuccess = False
                ret.ErrorMessage = ex.Message
                Exit For
            End Try
            ci += 1
        Next
        'End If

        'Clear Inactive File
        'ถ้ามีไฟล์ที่เคยดาวน์โหลดมาแล้วในเครื่อง และเป็นไฟล์ที่ไม่มีอยู่ในบทเรียนแล้ว ก็ให้ลบไฟล์นั้นออกไปโลด

        'Dim fDt As DataTable = GetSqlDataTable("select * from TB_COURSE_DOCUMENT_FILE")
        'If fDt.Rows.Count > 0 Then
        '    For Each f As String In Directory.GetFiles(FolderCourseDocumentFile)
        '        Dim fInfo As New FileInfo(f)

        '        Dim fFile As String = EnCripText(FolderCourseDocumentFile & "\" & DeCripText(fInfo.Name))
        '        fDt.DefaultView.RowFilter = "file_path='" & fFile & "'"
        '        If fDt.DefaultView.Count = 0 Then
        '            Try
        '                File.SetAttributes(f, FileAttributes.Normal)
        '                File.Delete(f)
        '            Catch ex As Exception

        '            End Try
        '        End If
        '        fDt.DefaultView.RowFilter = ""
        '    Next
        'End If
        'fDt.Dispose()
        Return ret
    End Function

    Private Function BuiltDatableTableUserMessage(data3 As JProperty, UserSessionID As Long, UserID As Long, Username As String, trans As TransactionDB) As ExecuteDataInfo
        Dim ret As New ExecuteDataInfo

        If data3.Values.Count = 0 Then
            ret.IsSuccess = True
            Return ret
        End If

        For Each desc As JObject In data3.Value
            Try
                Dim name As String = desc("name").ToString
                Dim description As String = desc("description").ToString

                Dim lnq As New TbUserMessageLinqDB
                lnq.MESSAGE_NAME = name
                lnq.MESSAGE_DESC = description
                lnq.TB_USER_SESSION_ID = UserSessionID
                lnq.USER_ID = UserID

                ret = lnq.InsertData(Username, trans.Trans)
                If ret.IsSuccess = False Then
                    Exit For
                End If
                lnq = Nothing
            Catch ex As Exception
                ret.IsSuccess = False
                ret.ErrorMessage = ex.Message
                Exit For
            End Try
        Next

        Return ret
    End Function
#End Region

    Private Sub btnForgetPassword_Click(sender As Object, e As EventArgs) Handles btnForgetPassword.Click
        pnlLogin.Visible = False
        pnlRequestOTP.Visible = True
    End Sub

    Private Sub btnSendOTP_Click(sender As Object, e As EventArgs) Handles btnSendOTP.Click
        pnlRequestOTP.Visible = False
        pnlLoginOTP.Visible = True

    End Sub


End Class