'Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq

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
                Dim json As String = info
                Dim ser As JObject = JObject.Parse(json)
                Dim data As List(Of JToken) = ser.Children().ToList
                Dim output As String = ""

                UserData = New UserProfileData
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
                            UserData.UserFormat = New DataTable
                            UserData.UserFormat.Columns.Add("format_id")
                            UserData.UserFormat.Columns.Add("format_title")

                            UserData.UserFunction = New DataTable
                            UserData.UserFunction.Columns.Add("format_id")
                            UserData.UserFunction.Columns.Add("function_id")
                            UserData.UserFunction.Columns.Add("function_title")
                            UserData.UserFunction.Columns.Add("function_cover")
                            UserData.UserFunction.Columns.Add("function_cover_color")
                            UserData.UserFunction.Columns.Add("function_subject")

                            UserData.UserDepartment = New DataTable
                            UserData.UserDepartment.Columns.Add("department_id")
                            UserData.UserDepartment.Columns.Add("department_title")
                            UserData.UserDepartment.Columns.Add("department_cover")
                            UserData.UserDepartment.Columns.Add("function_id")
                            ClearCoureData()


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

    Private Sub ClearCoureData()
        'Dim sql As String = ""
        'sql = "delete from TB_COURSE_DOCUMENT_FILE  "
        'ExecuteSqlNoneQuery(sql)

        'sql = " delete from TB_COURSE_DOCUMENT "
        'ExecuteSqlNoneQuery(sql)

        'sql = " delete from TB_COURSE "
        'ExecuteSqlNoneQuery(sql)

        'Dim CourseIconFolder As String = "CourseIcon"
        'Dim CourseCoverFolder As String = "CourseCover"
        'DeleteCourseIcon(CourseIconFolder)
        'DeleteCourseCover(CourseCoverFolder)
    End Sub

    Private Sub BuiltUserFormat(data2 As JToken)
        For Each comment As JProperty In data2
            comment.CreateReader()
            Select Case comment.Name
                Case "format"
                    For Each f As JObject In comment.Values
                        Dim dr As DataRow = UserData.UserFormat.NewRow
                        dr("format_id") = f("id").ToString
                        dr("format_title") = f("title").ToString
                        UserData.UserFormat.Rows.Add(dr)

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

                Dim dr As DataRow = UserData.UserFunction.NewRow
                dr("format_id") = FormatID
                dr("function_id") = comment("id").ToString
                dr("function_title") = comment("title").ToString
                dr("function_cover") = comment("cover").ToString
                dr("function_cover_color") = comment("color").ToString
                dr("function_subject") = comment("subject_type").ToString   'main subject / additional subject
                UserData.UserFunction.Rows.Add(dr)

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

                Dim dr As DataRow = UserData.UserDepartment.NewRow
                dr("department_id") = desc("id").ToString
                dr("department_title") = desc("title").ToString
                dr("department_cover") = desc("cover").ToString
                dr("function_id") = FunctionID

                UserData.UserDepartment.Rows.Add(dr)

                'BindDatableTableFromCourse(desc.Last)
            Next
        Next

    End Sub
End Class