Imports System.Data.SqlClient
Imports LinqDB.ConnectDB
Imports System.IO
Imports Newtonsoft.Json.Linq


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
        If Session("UserData") Is Nothing Then
            Response.Redirect("frmLogin.aspx")
        End If

        If Not Page.IsPostBack Then
            Me.txtCoursetID.Text = Course_id
            Me.txtCourseTitle.Text = Course_title
            DisplayCourseDetail()
        End If

    End Sub


    Private Sub DisplayCourseDetail()
        Dim sql As String = "select top 1 course_desc "
        sql += " from tb_user_course "
        sql += " where id=@_ID"
        Dim p(1) As SqlParameter
        p(0) = SqlDB.SetBigInt("@_ID", Course_id)

        Dim dt As DataTable = SqlDB.ExecuteTable(sql, p)
        If dt.Rows.Count > 0 Then
            Me.lblMain.Text = "<h2><font color=""#019b79""><u> " + Course_title + "</u> </font></h2>"
            Me.lblMain.Text += "<h3><font color=""#019b79"">" + dt.Rows(0)("course_desc").ToString + "</font></h3>"
        End If

        dt.Dispose()
    End Sub

#End Region

#Region "Event & Handle"
    Private Sub btnRegisterClick(sender As Object, e As EventArgs) Handles btnRegister.Click
        ScriptManager.RegisterStartupScript(Me, Page.GetType, "Script", "ShowPopup('xxxxxxxxxxxx');", True)

        'If Me.txtCoursetID.Text <> "" Then
        '    BindDocumentData(Me.txtCoursetID.Text)
        '    Response.Redirect("frmDisplayMain.aspx?rnd=" & DateTime.Now.Millisecond & "&id=" & Me.txtCoursetID.Text & "&title=" + Me.txtCourseTitle.Text)
        'End If
    End Sub

    Sub BindDocumentData(CourseID As String)
        Try
            Dim ret As New ExecuteDataInfo
            Dim document_txt As String = ""
            Dim sql As String = " select document_detail from TB_USER_COURSE where id=@_ID"  '& " and bind_document='N'"
            Dim p(1) As SqlParameter
            p(0) = SqlDB.SetBigInt("@_ID", CourseID)

            Dim trans As New LinqDB.ConnectDB.TransactionDB
            Dim dt As DataTable = SqlDB.ExecuteTable(sql, p) ' GetSqlDataTable(sql)
            If dt.Rows.Count > 0 Then
                sql = "delete from TB_USER_COURSE_DOCUMENT_FILE where tb_user_course_document_id in (select id from TB_USER_COURSE_DOCUMENT where tb_user_course_id=@_COURSE_ID) "
                ReDim p(1)
                p(0) = SqlDB.SetBigInt("@_COURSE_ID", CourseID)
                ret = SqlDB.ExecuteNonQuery(sql, trans.Trans, p) ' ExecuteSqlNoneQuery(sql)

                If ret.IsSuccess = True Then
                    sql = " delete from TB_USER_COURSE_DOCUMENT  where tb_user_course_id=" & CourseID
                    ReDim p(1)
                    p(0) = SqlDB.SetBigInt("@_COURSE_ID", CourseID)
                    ret = SqlDB.ExecuteNonQuery(sql, trans.Trans, p)
                End If

                If ret.IsSuccess = True Then
                    If Convert.IsDBNull(dt.Rows(0)("document_detail")) = False Then
                        document_txt = dt.Rows(0)("document_detail")
                    End If
                End If
            End If
            dt.Dispose()

            If ret.IsSuccess = False Then
                trans.RollbackTransaction()
                Exit Sub
            End If

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

                        ret = SqlDB.ExecuteNonQuery(sql, trans.Trans, Nothing)

                        If ret.IsSuccess = True Then
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

                                    sql = "insert into TB_USER_COURSE_DOCUMENT_FILE (id,tb_user_course_document_id,file_title,file_url,order_by,user_id)"
                                    sql += " values('" & file_comment("id").ToString & "'"
                                    sql += ", '" & doc_id & "'"
                                    sql += ", '" & file_comment("title").ToString & "'"
                                    sql += ", '" & file_comment("file").ToString & "'"
                                    sql += ", '" & file_comment("order").ToString & "'"
                                    sql += ", '" & UserData.UserID & "')"
                                    ret = SqlDB.ExecuteNonQuery(sql, trans.Trans, Nothing)

                                    If ret.IsSuccess = False Then
                                        Exit For
                                    End If
                                    cdfi += 1
                                Next
                                If ret.IsSuccess = False Then
                                    Exit For
                                End If
                            Next
                        Else
                            Exit For
                        End If
                        cdi += 1
                    Next

                    If ret.IsSuccess = False Then
                        Exit For
                    End If
                Next

                sql = " update TB_USER_COURSE set bind_document='Y' where id=@_ID" & CourseID
                ReDim p(1)
                p(0) = SqlDB.SetBigInt("@_ID", CourseID)

                ret = SqlDB.ExecuteNonQuery(sql, trans.Trans, p)
                If ret.IsSuccess = True Then
                    trans.CommitTransaction()
                Else
                    trans.RollbackTransaction()
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub
#End Region


End Class