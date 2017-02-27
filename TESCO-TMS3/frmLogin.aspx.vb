'Imports Newtonsoft.Json
Imports System.Data.SqlClient
Imports Newtonsoft.Json.Linq
Imports LinqDB.ConnectDB

Public Class frmLogin
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Private Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
        If Login(txtUsername.Text, txtPassword.Text) = True Then
            Session("Username") = txtUsername.Text
            Response.Redirect("frmMain.aspx?rnd=" & DateTime.Now.Millisecond)
        End If
    End Sub


    Private Function Login(ByVal Username As String, ByVal Password As String) As Boolean
        Dim ret As Boolean = False
        Try
            'Dim Capacity As Double = GetDriveInfoByDriveLetter("C")

            Dim info As String = ""
            info = GetStringDataFromURL(GetWebServiceURL() & "api/login", "username=" & Username & "&password=" & Password)
            If info.Trim <> "" Then
                ClearUserSession(Username)

                Dim json As String = info
                Dim ser As JObject = JObject.Parse(json)
                Dim data As List(Of JToken) = ser.Children().ToList
                Dim output As String = ""

                If data.Count = 12 Then
                    UserData = New UserProfileData
                    UserData.UserID = DirectCast(data(11), JProperty).First
                    UserData.TokenStr = DirectCast(data(9), JProperty).First
                    UserData.Token = "token=" & UserData.TokenStr
                    UserData.FullName = DirectCast(data(2), JProperty).First.ToString & " " & DirectCast(data(3), JProperty).First.ToString

                    Session("UserData") = UserData
                    Dim re As ExecuteDataInfo = CreateUserSession(UserData.TokenStr, UserData.UserID, Username, data)
                    ret = re.IsSuccess
                End If




                For Each item As JProperty In data
                    item.CreateReader()
                    Select Case item.Name
                        Case "token"
                            UserData.TokenStr = item.First
                            UserData.Token = "token=" & UserData.TokenStr
                            ret = True
                        Case "user"
                            For Each comment As JProperty In item.Values
                                UserData.FullName = comment.Value.ToString
                            Next
                        Case "user_id"
                            UserData.UserID = item.First
                        Case "data"
                            'UserData.UserFormat = New DataTable
                            'UserData.UserFormat.Columns.Add("format_id")
                            'UserData.UserFormat.Columns.Add("format_title")

                            'UserData.UserFunction = New DataTable
                            'UserData.UserFunction.Columns.Add("format_id")
                            'UserData.UserFunction.Columns.Add("function_id")
                            'UserData.UserFunction.Columns.Add("function_title")
                            'UserData.UserFunction.Columns.Add("function_cover")
                            'UserData.UserFunction.Columns.Add("function_cover_color")
                            'UserData.UserFunction.Columns.Add("function_subject")

                            'UserData.UserDepartment = New DataTable
                            'UserData.UserDepartment.Columns.Add("department_id")
                            'UserData.UserDepartment.Columns.Add("department_title")
                            'UserData.UserDepartment.Columns.Add("department_cover")
                            'UserData.UserDepartment.Columns.Add("function_id")



                            BuiltUserFormat(item.First)
                        Case "welcome"
                            'BuiltDatableTableUserMessage(item)
                    End Select
                Next
            End If
        Catch ex As Exception
            'MessageBox.Show(ex.Message, "Attention", MessageBoxButtons.OK, MessageBoxIcon.Information)
            ScriptManager.RegisterStartupScript(Me, Me.GetType, Guid.NewGuid().ToString(), "alert('" & ex.Message & "')", True)
        End Try

        Return ret
    End Function

    Private Function CreateUserSession(Token As String, UserID As Long, Username As String, data As List(Of JToken)) As ExecuteDataInfo
        Dim ret As New ExecuteDataInfo
        Try
            Dim lnq As New LinqDB.TABLE.TbUserSessionLinqDB
            lnq.TOKEN = Token
            lnq.USER_ID = UserID
            lnq.USERNAME = Username
            lnq.FIRST_NAME_ENG = DirectCast(data(2), JProperty).First
            lnq.LAST_NAME_ENG = DirectCast(data(3), JProperty).First
            lnq.FIRST_NAME_THAI = DirectCast(data(4), JProperty).First.ToString
            lnq.LAST_NAME_THAI = DirectCast(data(5), JProperty).First.ToString
            lnq.IS_TEACHER = DirectCast(data(6), JProperty).First

            Dim trans As New TransactionDB
            ret = lnq.InsertData(Username, trans.Trans)
            If ret.IsSuccess = True Then
                trans.CommitTransaction()
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

            If ret.IsSuccess = True Then
                trans.CommitTransaction()
            Else
                trans.RollbackTransaction()
            End If
        End If
        uLnq = Nothing


    End Sub

    Private Sub BuiltUserFormat(data2 As JToken)
        For Each comment As JProperty In data2
            comment.CreateReader()
            Select Case comment.Name
                Case "format"
                    For Each f As JObject In comment.Values
                        'Dim dr As DataRow = UserData.UserFormat.NewRow
                        'dr("format_id") = f("id").ToString
                        'dr("format_title") = f("title").ToString
                        'UserData.UserFormat.Rows.Add(dr)

                        Dim jProp As JObject = JObject.Parse("{""function"":" & f("function").ToString & "}")
                        BuiltUserFunction(jProp, f("id").ToString)
                    Next
            End Select
        Next
    End Sub

    Private Sub BuiltUserFunction(data_ser As JObject, FormatID As Integer)
        Dim data As List(Of JToken) = data_ser.Children().ToList
        For Each item As JProperty In data
            For Each comment As JObject In item.Value
                comment.CreateReader()

                'Dim dr As DataRow = UserData.UserFunction.NewRow
                'dr("format_id") = FormatID
                'dr("function_id") = comment("id").ToString
                'dr("function_title") = comment("title").ToString
                'dr("function_cover") = comment("cover").ToString
                'dr("function_cover_color") = comment("color").ToString
                'dr("function_subject") = comment("subject_type").ToString   'main subject / additional subject
                'UserData.UserFunction.Rows.Add(dr)

                Dim jProp As JObject = JObject.Parse("{""department"":" & comment("department").ToString & "}")
                BuiltFuserDepartment(jProp, comment("id").ToString)
            Next
        Next
    End Sub

    Private Sub BuiltFuserDepartment(data_ser As JObject, FunctionID As String)

        Dim data As List(Of JToken) = data_ser.Children().ToList
        For Each item As JProperty In data
            For Each desc As JObject In item.Values
                desc.CreateReader()

                'Dim dr As DataRow = UserData.UserDepartment.NewRow
                'dr("department_id") = desc("id").ToString
                'dr("department_title") = desc("title").ToString
                'dr("department_cover") = desc("cover").ToString
                'dr("function_id") = FunctionID

                'UserData.UserDepartment.Rows.Add(dr)

                BindDatableTableFromCourse(desc.Last)
            Next
        Next

    End Sub

    Private Sub BindDatableTableFromCourse(data As JProperty)
        Dim course_id As Int32 = 0
        Dim doc_id As Int32 = 0

        Dim sql As String = ""
        Dim CourseIconFolder As String = "CourseIcon"
        Dim CourseCoverFolder As String = "CourseCover"

        Dim item As JProperty = data
        Dim ci As Integer = 1
        For Each comment As JObject In item.Values
            Try
                course_id = comment("id")
                'sql = "insert into TB_COURSE (id, department_id,title,description,icon_url,icon_file,cover_url,cover_file,sort,is_document_lock,document_detail,bind_document)"
                'sql += " values('" & course_id & "'"
                'sql += ", '" & comment("department_id").ToString & "'"
                'sql += ", '" & comment("name").ToString & "'"
                'sql += ", '" & comment("description").ToString & "'"
                'sql += ", '" & comment("icon").ToString & "'"
                'sql += ", '" & SaveCourseIcon(CourseIconFolder, comment("icon").ToString, course_id) & "'"
                'sql += ", '" & comment("cover").ToString & "'"
                'sql += ", '" & SaveCourseCover(CourseCoverFolder, comment("cover").ToString, course_id) & "'"
                'sql += ", '" & ci & "' "
                'sql += ", '" & IIf(comment("is_document_lock").ToString.ToLower = "true", "Y", "N") & "'"
                'sql += ", '" & ("{""document"":" & comment("document").ToString & "}").Replace("'", "''") & "','N')"

                'If ExecuteSqlNoneQuery(sql) = False Then
                '    Dim aaa As String = ""
                'End If
            Catch ex As Exception

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
    End Sub
End Class