Imports System.Data.SqlClient
Imports LinqDB.ConnectDB
Public Class frmDisplayMain
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
        If Session("UserData") Is Nothing Then
            Response.Redirect("Default.aspx")
        End If

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
        Sql = " select id,tb_user_course_id,document_title,ms_document_icon_id,document_version,document_type,order_by,user_id from TB_USER_COURSE_DOCUMENT  where tb_user_course_id=" & id

        Dim p(1) As SqlParameter
        p(0) = SqlDB.SetBigInt("@_ID", id)
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

        Sql = "select ROW_NUMBER() OVER(ORDER BY tb_user_course_document_id,  order_by ASC) AS rowindex, "
        Sql += " id,tb_user_course_document_id,file_title,file_url,order_by,user_id "
        Sql += " from TB_USER_COURSE_DOCUMENT_FILE "
        Sql += " where tb_user_course_document_id in (select id from TB_USER_COURSE_DOCUMENT where tb_user_course_id=@_ID)  "
        Sql += " And file_title  Not Like '%.swf%' "
        Sql += " order by tb_user_course_document_id,  order_by"
        ReDim p(1)
        p(0) = SqlDB.SetBigInt("@_ID", id)

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


        Session("UserDataCourse") = UserCourse
        Session("UserDataCourseFile") = UserCourseFile

        If (UserCourseFile.Rows.Count > 0) Then
            SetContent(UserCourseFile)
            SetIniPage(UserCourseFile.Rows(0))
            Me.btnBack.Enabled = False
            Me.txtPre.Text = 0
            Me.txtCurrent.Text = 1
            Me.txtNext.Text = IIf(UserCourseFile.Rows.Count = 1, 1, 2)


            Me.txtMax.Text = UserCourseFile.Rows.Count
            GetBotton()
            Me.btnCloseContent.Style.Add("visibility", "hidden")
            Me.btnCloseContent.Attributes.Add("onclick", "hidecontent(); return false;")
            Me.btnContent.Attributes.Add("onclick", "showcontent(); return false;")
        End If
        'Me.btnCloseContent.Style.Add("visibility", "hidden")
        'Me.btnCloseContent.Attributes.Add("onclick", "hidecontent(); return false;")
        'Me.btnContent.Attributes.Add("onclick", "showcontent(); return false;")
    End Sub

    Private Sub SetContent(dtUserDataCourseFile As DataTable)
        Try
            Dim str As String = ""
            If (dtUserDataCourseFile.Rows.Count > 0) Then
                For i As Int32 = 0 To dtUserDataCourseFile.Rows.Count - 1
                    Dim dr As DataRow = dtUserDataCourseFile.Rows(i)
                    str += " <p> <button class=""btn-block btn btn-larges"" id=" + dr("id").ToString + " onclick=""fselect('" + dr("id").ToString + "','" + dr("file_url").ToString + "','" + dr("rowindex").ToString + "');return false;"" >" + dr("file_title").ToString + "</button></p>"
                Next

            End If
            lblContent.Text = str
        Catch ex As Exception

        End Try
    End Sub

    Private Sub SetIniPage(dr As DataRow)
        Dim url As String
        If dr("file_url").ToString.IndexOf(".png") <> -1 Or dr("file_url").ToString.IndexOf(".jpg") <> -1 Then
            ' myIframe.Attributes.Add("src", "frmDisplayImage.aspx?id=" + dr("id").ToString)
            url = "frmDisplayImage.aspx?id=" + dr("id").ToString
            ScriptManager.RegisterStartupScript(Me, Page.GetType, "Script", "loadIframe('" + url + "');", True)
        ElseIf dr("file_url").ToString.IndexOf(".pdf") <> -1 Then
            'myIframe.Attributes.Add("src", "frmDisplayPDF.aspx?id=" + dr("id").ToString)
            url = "frmDisplayPDF.aspx?id=" + dr("id").ToString
            ScriptManager.RegisterStartupScript(Me, Page.GetType, "Script", "loadIframe('" + url + "');", True)
        ElseIf dr("file_url").ToString.IndexOf(".mp4") <> -1 Then
            'myIframe.Attributes.Add("src", "frmDisplayVDO.aspx?id=" + dr("id").ToString)
            url = "frmDisplayVDO.aspx?id=" + dr("id").ToString
            ScriptManager.RegisterStartupScript(Me, Page.GetType, "Script", "loadIframe('" + url + "');", True)
        ElseIf dr("file_url").ToString.IndexOf(".html") <> -1 Then
            'myIframe.Attributes.Add("src", "frmDisplayHTML.aspx?id=" + dr("id").ToString)
            url = "frmDisplayHTML.aspx?id=" + dr("id").ToString
            ScriptManager.RegisterStartupScript(Me, Page.GetType, "Script", "loadIframe('" + url + "');", True)
        End If

    End Sub

#End Region

#Region "Event Handle"
    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click

        Dim dtback As DataTable = Session("UserDataCourseFile")
            Dim foundRows() As DataRow
            Dim strfillter As String = "next_id=" & Me.txtPre.Text
            foundRows = dtback.Select(strfillter)

            Dim i As Integer
            For i = 0 To foundRows.GetUpperBound(0)
                SetIniPage(foundRows(i))
            Next i
            Me.txtPre.Text = Val(Me.txtPre.Text) - 1
            Me.txtCurrent.Text = Val(Me.txtCurrent.Text) - 1
        Me.txtNext.Text = Val(Me.txtNext.Text) - 1
        GetBotton()
    End Sub

    Private Sub btnNext_Click(sender As Object, e As EventArgs) Handles btnNext.Click

        Dim dtnext As DataTable = Session("UserDataCourseFile")
            Dim foundRows() As DataRow
            Dim strfillter As String = "next_id=" & Me.txtNext.Text
            foundRows = dtnext.Select(strfillter)

            Dim i As Integer
            For i = 0 To foundRows.GetUpperBound(0)
                SetIniPage(foundRows(i))
            Next i

        Me.txtPre.Text = Val(Me.txtPre.Text) + 1
            Me.txtCurrent.Text = Val(Me.txtCurrent.Text) + 1
        Me.txtNext.Text = Val(Me.txtNext.Text) + 1
        GetBotton()
    End Sub

    Public Sub GetBotton()
        If Me.txtPre.Text = "0" Then
            'Me.btnBack.Attributes.Add("disabled", True)
            Me.btnBack.Enabled = False
        Else
            'Me.btnBack.Attributes.Add("disabled", False)
            Me.btnBack.Enabled = True
        End If


        If Val(Me.txtCurrent.Text) >= Val(Me.txtMax.Text) Then
            'Me.btnNext.Attributes.Add("disabled", True)
            Me.btnNext.Enabled = False
        Else
            'Me.btnNext.Attributes.Add("disabled", False)
            Me.btnNext.Enabled = True
        End If
    End Sub

    Private Sub btnHome_Click(sender As Object, e As EventArgs) Handles btnHome.Click
        Response.Redirect("frmSelectFormat.aspx")
    End Sub

#End Region
End Class