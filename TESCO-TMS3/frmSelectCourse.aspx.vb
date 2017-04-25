Public Class frmSelectCourse
    Inherits System.Web.UI.Page

#Region "Declare & Valiable"
    ' Public myUser As User
    Public ReadOnly Property UserData As UserProfileData
        Get
            Return Session("UserData")
        End Get
    End Property

    Public ReadOnly Property Department_id As String
        Get
            Return Page.Request.QueryString("id") & ""
        End Get
    End Property

    Public ReadOnly Property Department_title As String
        Get
            Return Page.Request.QueryString("title") & ""
        End Get
    End Property

    Public ReadOnly Property color As String
        Get
            Return "#" + Page.Request.QueryString("color")
        End Get
    End Property
#End Region

#Region "Initail"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Me.lblTitle.Text = "<h3>&nbsp>&nbsp<a href=""frmSelectFormat.aspx""><font color=""#019b79"">" + Session("backpathname1") + "&nbsp>&nbsp</font></a><a href=" + Session("backpath2") + "><font color=""#019b79"">" + Session("backpathname2") + "&nbsp>&nbsp</font></a><a href=" + Session("backpath3") + "><font color=""#019b79"">" + Session("backpathname3") + "&nbsp>&nbsp</font></a><font color=""#019b79"">" + Department_title + "</font></h3>"
            'SetDepartment()
            DisplayCourseList()
        End If

    End Sub


    Private Sub DisplayCourseList()
        Dim strURlImage As String = Request.Url.GetLeftPart(UriPartial.Authority) + Request.ApplicationPath & "Assets/PC/icon_course_book.png"
        Dim strMain As String = "<ul class=""tiles"">"
        Dim sql As String = "select * "
        sql += " from tb_user_course "
        sql += " where 1=1 and department_id=" & Department_id
        sql += " order by sort"
        Dim dt As DataTable = GetSqlDataTable(sql)
        For Each dr As DataRowView In dt.DefaultView

            strMain += " <li  onclick=""ShowPopup('" + dr("id").ToString + "','" + dr("course_title").ToString + "','" + dr("course_title").ToString + "');"" id=" + dr("id").ToString + " style=""background-image:url('Assets/PC/icon_course_book.png');background-size: 170px auto;background-repeat: no-repeat;height:170px"">" '+ " style=""background-image:url(" + dr("cover_url").ToString + ")"">"
            strMain += " <a href=""#""><h5 Class=""text-center"">" + dr("course_title").ToString + "</h5></a>"
            strMain += "  </li>"
        Next
        strMain += "</ul>"
        lblMain.Text = strMain
        dt.Dispose()
    End Sub

    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Dim strurl As String = Session("backpath3")
        Response.Redirect(strurl)
    End Sub
#End Region

End Class