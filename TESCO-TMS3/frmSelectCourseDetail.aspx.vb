Imports System.IO
Imports Newtonsoft.Json.Linq
Imports LinqDB.TABLE
Imports LinqDB.ConnectDB

Public Class frmSelectCourseDetail
    Inherits System.Web.UI.Page

#Region "Declare & Valiable"
    ' Public myUser As User
    Public ReadOnly Property UserData As UserProfileData
        Get
            Return Session("UserData")
        End Get
    End Property

    Public ReadOnly Property Course_id As String
        Get
            Return Page.Request.QueryString("id") & "" '1241
        End Get
    End Property

    Public ReadOnly Property Course_title As String
        Get
            Return Page.Request.QueryString("title") & ""
        End Get
    End Property


#End Region

#Region "Initail"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Me.txtCoursetID.Text = Course_id
            Me.txtCourseTitle.Text = Course_title
            DisplayCourseDetail()
        End If

    End Sub


    Private Sub DisplayCourseDetail()
        Dim sql As String = "select * "
        sql += " from tb_user_course "
        sql += " where id=@_COURSE_ID"

        Dim p(1) As System.Data.SqlClient.SqlParameter
        p(0) = LinqDB.ConnectDB.SqlDB.SetBigInt("@_COURSE_ID", Course_id)
        Dim dt As DataTable = LinqDB.ConnectDB.SqlDB.ExecuteTable(sql, p)
        If (dt.Rows.Count > 0) Then
            Me.lblMain.Text = "<h2><font color=""#019b79""><u> " + Course_title + "</u> </font></h2>"
            Me.lblMain.Text += "<h3><font color=""#019b79"">" + dt.Rows(0)("course_desc").ToString + "</font></h3>"
        End If

        dt.Dispose()
    End Sub

#End Region

#Region "Event & Handle"
    Private Sub btnRegisterClick(sender As Object, e As EventArgs) Handles btnRegister.Click
        If Me.txtCoursetID.Text <> "" Then
            BindDocumentData(Me.txtCoursetID.Text)
            Response.Redirect("frmDisplayMain.aspx?rnd=" & DateTime.Now.Millisecond & "&id=" & Me.txtCoursetID.Text & "&title=" + Me.txtCourseTitle.Text)
        End If
    End Sub

    Sub BindDocumentData(CourseID As String)
        Try
            Dim document_txt As String = ""
            Dim sql As String = " select document_detail from TB_USER_COURSE where id=@_COURSE_ID" '& " and bind_document='N'"
            Dim p(1) As System.Data.SqlClient.SqlParameter
            p(0) = LinqDB.ConnectDB.SqlDB.SetBigInt("@_COURSE_ID", CourseID)
            Dim dt As DataTable = LinqDB.ConnectDB.SqlDB.ExecuteTable(sql, p)


            'Dim dt As DataTable = GetSqlDataTable(sql)
            If dt.Rows.Count > 0 Then
                sql = "delete from TB_USER_COURSE_DOCUMENT_FILE where tb_user_course_document_id in (select id from TB_USER_COURSE_DOCUMENT where tb_user_course_id=" & CourseID & ") "
                LinqDB.ConnectDB.SqlDB.ExecuteNonQuery(sql)

                sql = " delete from TB_USER_COURSE_DOCUMENT  where tb_user_course_id=" & CourseID
                LinqDB.ConnectDB.SqlDB.ExecuteNonQuery(sql)

                If Convert.IsDBNull(dt.Rows(0)("document_detail")) = False Then
                    document_txt = dt.Rows(0)("document_detail")
                End If
            End If
            dt.Dispose()

            If document_txt.Trim <> "" Then

                'Dim document_txt As String = "{""document"":" & comment("document").ToString & "}"
                Dim document_ser As JObject = JObject.Parse(document_txt)
                Dim document_data As List(Of JToken) = document_ser.Children().ToList
                For Each document_item As JProperty In document_data

                    Dim cdi As Integer = 1
                    For Each document_comment As JObject In document_item.Values
                        document_item.CreateReader()

                        sql = "insert into TB_USER_COURSE_DOCUMENT (id,tb_user_course_id,document_title,ms_document_icon_id,document_version,document_type,order_by,user_id)"
                        sql += " values('" & document_comment("id").ToString & "'"
                        sql += ", '" & CourseID & "'"
                        sql += ", '" & document_comment("title").ToString & "'"
                        sql += ", '" & document_comment("icon_id").ToString & "'"
                        sql += ", '" & document_comment("version").ToString & "'"
                        sql += ", '" & document_comment("type").ToString & "'"
                        sql += ", '" & document_comment("order").ToString & "'"
                        sql += ", '" & UserData.UserID & "')"

                        If LinqDB.ConnectDB.SqlDB.ExecuteNonQuery(sql).IsSuccess = True Then
                            Dim doc_id As Object = document_comment("id")
                            Dim file_txt As String = "{""file"":" & document_comment("file").ToString & "}"
                            Dim file_ser As JObject = JObject.Parse(file_txt)
                            Dim file_data As List(Of JToken) = file_ser.Children().ToList
                            For Each file_item As JProperty In file_data

                                Dim cdfi As Integer = 1
                                For Each file_comment As JObject In file_item.Values
                                    file_item.CreateReader()

                                    Dim FileName As String = file_comment("id").ToString & GetURLFileExtension(file_comment("file").ToString)
                                    Dim DocFileID As String = EnCripText(FileName)
                                    Dim DocFileName As String = "null"
                                    'Dim DocFile() As String = Directory.GetFiles(FolderCourseDocumentFile, DocFileID & ".*")
                                    Dim DocFile() As String = Directory.GetFiles(FolderCourseDocumentFile, DocFileID)
                                    If DocFile.Length > 0 Then
                                        DocFileName = "'" & EnCripText(FolderCourseDocumentFile & "\" & FileName) & "'"
                                    End If

                                    'Dim lnq As New TbUserCourseDocumentFileLinqDB


                                    sql = "insert into TB_USER_COURSE_DOCUMENT_FILE (id,tb_user_course_document_id,file_title,file_url,order_by,user_id)"
                                    sql += " values('" & file_comment("id").ToString & "'"
                                    sql += ", '" & doc_id & "'"
                                    sql += ", '" & file_comment("title").ToString & "'"
                                    sql += ", '" & file_comment("file").ToString & "'"
                                    sql += ", '" & file_comment("order").ToString & "'"
                                    sql += ", '" & UserData.UserID & "')"
                                    SqlDB.ExecuteNonQuery(sql)

                                    'Dim drfile As DataRow = UserData.UserCourseFile.NewRow
                                    'drfile("id") = file_comment("id").ToString
                                    'drfile("tb_user_course_document_id") = doc_id
                                    'drfile("file_title") = file_comment("title").ToString
                                    'drfile("file_url") = file_comment("file").ToString
                                    'drfile("order_by") = file_comment("order").ToString
                                    'drfile("user_id") = "3"
                                    'UserData.UserCourseFile.Rows.Add(drfile)


                                    cdfi += 1

                                Next
                            Next
                        End If
                        cdi += 1


                    Next
                Next

                '  sql = " update TB_USER_COURSE set bind_document='Y' where id=" & CourseID
                SqlDB.ExecuteNonQuery(sql)

                'Session("UserData") = UserData

            End If
        Catch ex As Exception

        End Try
    End Sub
#End Region


End Class