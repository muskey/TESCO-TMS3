Imports System.Data.SqlClient
Imports LinqDB.ConnectDB
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

        Dim sql As String = "select * "
        sql += " from tb_user_course "
        sql += " where 1=1 and department_id=@_DEPARTMENT_ID"
        sql += " order by sort"
        Dim p(1) As SqlParameter
        p(0) = SqlDB.SetBigInt("@_DEPARTMENT_ID", Department_id)
        Dim dt As DataTable = SqlDB.ExecuteTable(sql, p)

        Dim UserData As UserProfileData = DirectCast(Session("UserData"), UserProfileData)

        Dim strMain As String = "<ul class=""tiles"">"
        For Each dr As DataRowView In dt.DefaultView
            strMain += " <li  onclick=""ShowPopup('" + dr("id").ToString + "','" + dr("course_title").ToString + "','" + dr("course_title").ToString + "','" & UserData.UserSessionID & "');"" id=" + dr("id").ToString
            strMain += " style=""background-image:url('Assets/PC/icon_course_book.png');background-size: 155px auto;background-repeat: no-repeat;height:160px"">"
            strMain += "    <a href=""#"">"
            strMain += "        <span class=""text-center"" style=""font-size:20px;"" >" + dr("course_title").ToString + "</span>"
            strMain += "    </a>"
            strMain += " </li>"
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