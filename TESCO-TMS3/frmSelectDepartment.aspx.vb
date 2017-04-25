Imports System.Web.Services
Imports System.Data.SqlClient
Imports LinqDB.ConnectDB

Public Class frmSelectDepartment
    Inherits System.Web.UI.Page

#Region "Declare & Valiable"
    ' Public myUser As User
    Public ReadOnly Property UserData As UserProfileData
        Get
            Return Session("UserData")
        End Get
    End Property

    Public ReadOnly Property Function_id As String
        Get
            Return Page.Request.QueryString("id") & ""
        End Get
    End Property

    Public ReadOnly Property function_title As String
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
            Me.lblTitle.Text = "<h3>&nbsp>&nbsp<a href=""frmSelectFormat.aspx""><font color=""#019b79"">" + Session("backpathname1") + "&nbsp>&nbsp</font></a><a href=" + Session("backpath2") + "><font color=""#019b79"">" + Session("backpathname2") + "&nbsp>&nbsp</font></a><font color=""#019b79"">" + function_title + "</font></h3>"
            DisplayDepartmentList()
        End If

    End Sub

    <WebMethod()>
    Public Shared Function SetBackPath(strpath As String, strtitlename As String)
        Dim objp = New Page()
        objp.Session("backpath3") = strpath
        objp.Session("backpathname3") = strtitlename
        Return strpath
    End Function

    Private Sub DisplayDepartmentList()
        Try
            Dim strMain As String = "<ul class=""tiles"">"
            If (UserData.UserDepartment.Rows.Count > 0) Then
                UserData.UserDepartment.DefaultView.RowFilter = "function_id=" & Function_id

                Dim dt As New DataTable
                dt = UserData.UserDepartment.DefaultView.ToTable().Copy
                For Each dr As DataRow In dt.Rows
                    Dim bgColor As String = color
                    Dim sql As String = "select id from TB_USER_COURSE where department_id=@_DEPARTMENT_ID"
                    Dim p(1) As SqlParameter
                    p(0) = SqlDB.SetBigInt("@_DEPARTMENT_ID", dr("department_id"))
                    Dim cdt As DataTable = SqlDB.ExecuteTable(sql, p)
                    If cdt.Rows.Count = 0 Then
                        bgColor = "Gray"
                    End If

                    strMain += " <li  onclick=""fselect('" + dr("department_id").ToString + "','" + dr("department_title").ToString + "','" + cdt.Rows.Count.ToString + "','" + Page.Request.QueryString("color") + "');"" id=" + dr("department_id") + " style=""background-color:" + bgColor + """>"
                    strMain += " <a href=""#""><span><img src=" + dr("department_cover") + " height=""50"" width=""50""></span><h5 Class=""text-center"">" + dr("department_title") + "(" + cdt.Rows.Count.ToString + ")</h5></a>"
                    strMain += "  </li>"
                Next
            End If
            strMain += "</ul>"
            lblMain.Text = strMain
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Dim strurl As String = Session("backpath2")
        Response.Redirect(strurl)
    End Sub
#End Region



End Class