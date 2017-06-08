Imports System.Data.SqlClient
Imports LinqDB.ConnectDB
Public Class frmDisplayCenter
    Inherits System.Web.UI.Page


#Region "Declare & Valiable"
    ' Public myUser As User
    Public ReadOnly Property UserData As UserProfileData
        Get
            Return Session("UserData")
        End Get
    End Property

    Public ReadOnly Property id As String
        Get
            Return Page.Request.QueryString("id") & ""
        End Get
    End Property

    Public ReadOnly Property itle As String
        Get
            Return Page.Request.QueryString("title") & ""
        End Get
    End Property


#End Region

#Region "Initail"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            ' myIframe.Attributes.Add("src", "frmDisplayPDF.aspx?id=0")
            GetData()
        End If
    End Sub

    Private Sub GetData()

        Dim UserCourse = New DataTable
        UserCourse.Columns.Add("id")
        UserCourse.Columns.Add("tb_user_course_id")
        UserCourse.Columns.Add("document_title")
        UserCourse.Columns.Add("ms_document_icon_id")
        UserCourse.Columns.Add("document_version")
        UserCourse.Columns.Add("document_type")
        UserCourse.Columns.Add("order_by")
        UserCourse.Columns.Add("user_id")


        Dim UserCourseFile = New DataTable
        UserCourseFile.Columns.Add("id")
        UserCourseFile.Columns.Add("tb_user_course_document_id")
        UserCourseFile.Columns.Add("file_title")
        UserCourseFile.Columns.Add("file_url")
        UserCourseFile.Columns.Add("order_by")
        UserCourseFile.Columns.Add("user_id")
        UserCourseFile.Columns.Add("rowindex")
        UserCourseFile.Columns.Add("next_id")

        Dim Sql As String
        Sql = " select id,tb_user_course_id,document_title,ms_document_icon_id,document_version,document_type,order_by,user_id from TB_USER_COURSE_DOCUMENT  where tb_user_course_id=@_COURSE_ID"
        Dim p(1) As SqlParameter
        p(0) = SqlDB.SetBigInt("@_COURSE_ID", id)

        Dim dtCourse As DataTable = SqlDB.ExecuteTable(Sql, p)
        For Each item As DataRow In dtCourse.Rows
            Dim drcourse As DataRow = UserCourse.NewRow
            drcourse("id") = item("id").ToString
            drcourse("tb_user_course_id") = item("tb_user_course_id").ToString
            drcourse("document_title") = item("document_title").ToString
            drcourse("ms_document_icon_id") = item("ms_document_icon_id").ToString
            drcourse("document_version") = item("document_version").ToString
            drcourse("document_type") = item("document_type").ToString
            drcourse("order_by") = item("order_by").ToString
            drcourse("user_id") = item("user_id").ToString
            UserCourse.Rows.Add(drcourse)
        Next

        Sql = "select ROW_NUMBER() OVER(ORDER BY tb_user_course_document_id,  order_by ASC) AS rowindex, id,tb_user_course_document_id,file_title,file_url,order_by,user_id from TB_USER_COURSE_DOCUMENT_FILE where tb_user_course_document_id in (select id from TB_USER_COURSE_DOCUMENT where tb_user_course_id=@_COURSE_ID)  and file_title  not like '%.swf%' order by tb_user_course_document_id,  order_by"
        ReDim p(1)
        p(0) = SqlDB.SetBigInt("@_COURSE_ID", id)
        Dim inext As Integer = 1
        Dim dtFile As DataTable = SqlDB.ExecuteTable(Sql, p)
        For Each item As DataRow In dtFile.Rows
            Dim drfile As DataRow = UserCourseFile.NewRow
            drfile("id") = item("id").ToString
            drfile("tb_user_course_document_id") = item("tb_user_course_document_id").ToString
            drfile("file_title") = item("file_title").ToString
            drfile("file_url") = item("file_url").ToString
            drfile("order_by") = item("order_by").ToString
            drfile("user_id") = item("user_id").ToString
            drfile("rowindex") = item("rowindex").ToString
            drfile("next_id") = inext
            UserCourseFile.Rows.Add(drfile)
            inext = inext + 1
        Next

        If UserCourseFile.Rows.Count > 0 Then
            'Set ClassID to Session
            Dim UserData As UserProfileData = DirectCast(Session("UserData"), UserProfileData)
            UserData.GetUserSessionData(UserData.UserSessionID)
            If UserData.CurrentClassID > 0 Then
                Session("UserData") = UserData
            End If


            Session("UserDataCourse") = UserCourse
            Session("UserDataCourseFile") = UserCourseFile

            SetIniPage(UserCourseFile.Rows(0))
        End If
    End Sub

    Private Sub SetIniPage(dr As DataRow)
        Dim url As String
        If dr("file_url").ToString.IndexOf(".png") <> -1 Or dr("file_url").ToString.IndexOf(".jpg") <> -1 Then
            ' myIframe.Attributes.Add("src", "frmDisplayImage.aspx?id=" + dr("id").ToString)
            url = "frmDisplayImage.aspx?id=" + dr("id").ToString
            'ScriptManager.RegisterStartupScript(Me, Page.GetType, "Script", "loadIframe('" + url + "');", True)

            Response.Redirect(url)
        ElseIf dr("file_url").ToString.IndexOf(".pdf") <> -1 Then
            'myIframe.Attributes.Add("src", "frmDisplayPDF.aspx?id=" + dr("id").ToString)
            url = "frmDisplayPDF.aspx?id=" + dr("id").ToString
            'ScriptManager.RegisterStartupScript(Me, Page.GetType, "Script", "loadIframe('" + url + "');", True)
            Response.Redirect(url)
        ElseIf dr("file_url").ToString.IndexOf(".mp4") <> -1 Then
            'myIframe.Attributes.Add("src", "frmDisplayVDO.aspx?id=" + dr("id").ToString)
            url = "frmDisplayVDO.aspx?id=" + dr("id").ToString
            'ScriptManager.RegisterStartupScript(Me, Page.GetType, "Script", "loadIframe('" + url + "');", True)
            Response.Redirect(url)
        ElseIf dr("file_url").ToString.IndexOf(".html") <> -1 Then
            'myIframe.Attributes.Add("src", "frmDisplayHTML.aspx?id=" + dr("id").ToString)
            url = "frmDisplayHTML.aspx?id=" + dr("id").ToString
            ' ScriptManager.RegisterStartupScript(Me, Page.GetType, "Script", "loadIframe('" + url + "');", True)
            Response.Redirect(url)
        End If

    End Sub

#End Region

End Class