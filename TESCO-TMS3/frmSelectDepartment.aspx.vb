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

    Public ReadOnly Property User_Function_id As String
        Get
            Return Page.Request.QueryString("user_function_id") & ""
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
        If Session("UserData") Is Nothing Then
            Response.Redirect("frmLogin.aspx")
        End If

        If Not Page.IsPostBack Then
            Me.lblTitle.Text = "<h3>&nbsp>&nbsp"
            lblTitle.Text += " <a onClick=""return CreateTransLog('" & UserData.LoginHistoryID & "','กลับหน้าจอเลือก Format')"" href=""frmSelectFormat.aspx""><font color=""#019b79"">" + Session("backpathname1") + "&nbsp>&nbsp</font></a>"
            lblTitle.Text += " <a onClick=""return CreateTransLog('" & UserData.LoginHistoryID & "','กลับหน้าจอเลือก Function')"" href=" + Session("backpath2") + "><font color=""#019b79"">" + Session("backpathname2") + "&nbsp>&nbsp</font></a><font color=""#019b79"">" + function_title + "</font></h3>"
            DisplayDepartmentList()
            LogFileBL.LogTrans(UserData.LoginHistoryID, "แสดงหน้าจอเลือก Department")
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
            Dim UserData As UserProfileData = DirectCast(Session("UserData"), UserProfileData)

            Dim sql As String = "select id, department_id, department_title, department_cover_url, bind_course "
            sql += " from TB_USER_DEPARTMENT "
            sql += " where tb_user_function_id=@_USER_FUNCTION_ID "
            sql += " order by id "
            Dim p(1) As SqlParameter
            p(0) = SqlDB.SetText("@_USER_FUNCTION_ID", User_Function_id)

            Dim dt As DataTable = SqlDB.ExecuteTable(sql, p)
            Dim strMain As String = "<ul class=""tiles"">"
            If dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    Dim bgColor As String = color
                    sql = "select id from TB_USER_COURSE where tb_user_department_id=@_USER_DEPARTMENT_ID"
                    ReDim p(1)
                    p(0) = SqlDB.SetBigInt("@_USER_DEPARTMENT_ID", dr("id"))
                    Dim cdt As DataTable = SqlDB.ExecuteTable(sql, p)
                    If cdt.Rows.Count = 0 Then
                        bgColor = "Gray"
                    End If

                    strMain += " <li  onclick=""fselect('" & dr("id").ToString & "','" & dr("department_title").ToString & "','" & cdt.Rows.Count.ToString & "','" & Page.Request.QueryString("color") & "');"" id=" & dr("id") & " style=""background-color:" & bgColor & """>"
                    strMain += "    <a href=""#"">"
                    strMain += "        <span>"
                    strMain += "            <img src=" + dr("department_cover_url") + " height=""60"" width=""60"">"
                    strMain += "        </span>"
                    strMain += "        <span class=""text-center"" style=""font-size:22px;padding-top:25px;"" >" + dr("department_title") + "(" + cdt.Rows.Count.ToString + ")</span>"
                    strMain += "    </a>"
                    strMain += " </li>"
                Next
            End If
            dt.Dispose()

            strMain += "</ul>"
            lblMain.Text = strMain
        Catch ex As Exception
            LogFileBL.LogException(UserData, ex.Message, ex.StackTrace)
        End Try
    End Sub

    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        LogFileBL.LogTrans(UserData.LoginHistoryID, "คลิกปุ่ม Back")
        Dim strurl As String = Session("backpath2")
        Response.Redirect(strurl)
    End Sub
#End Region



End Class