Imports LinqDB.ConnectDB
Imports System.Data.SqlClient
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
            Me.lblTitle.Text = "<h3><font color=""#019b79"">&nbsp; &nbsp; &nbsp;" + Department_title + "</font></h3>"
            'SetDepartment()
            DisplayCourseList()
        End If

    End Sub


    Private Sub DisplayCourseList()
        Dim strMain As String = "<ul class=""tiles"">"
        Dim sql As String = "select * "
        sql += " from tb_user_course "
        sql += " where 1=1 and department_id=@_DEPARTMENT_ID"
        sql += " order by sort"
        Dim p(1) As SqlParameter
        p(0) = SqlDB.SetBigInt("@_DEPAREMENT_ID", Department_id)
        Dim dt As DataTable = SqlDB.ExecuteTable(sql, p)
        For Each dr As DataRowView In dt.DefaultView

            strMain += " <li  onclick=""fselect('" + dr("id").ToString + "','" + dr("course_title").ToString + "');"" id=" + dr("id").ToString + " style=""background-color:" + color + """>" '+ " style=""background-image:url(" + dr("cover_url").ToString + ")"">"
            strMain += " <a href=""#""><span><img src=" + dr("icon_url").ToString + " height=""50"" width=""50""></span><h5 Class=""text-center"">" + dr("course_title").ToString + "</h5></a>"
            strMain += "  </li>"
        Next
        strMain += "</ul>"
        lblMain.Text = strMain
        dt.Dispose()
    End Sub

#End Region

End Class