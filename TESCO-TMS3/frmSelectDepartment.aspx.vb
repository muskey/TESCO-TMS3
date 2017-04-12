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
            Me.lblTitle.Text = "<h3><font color=""#019b79"">&nbsp; &nbsp; &nbsp;" + function_title + "</font></h3>"
            DisplayDepartmentList()
        End If

    End Sub


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
                    'Dim cdt As DataTable = GetSqlDataTable(sql)

                    Dim p(1) As System.Data.SqlClient.SqlParameter
                    p(0) = LinqDB.ConnectDB.SqlDB.SetBigInt("@_DEPARTMENT_ID", dr("department_id"))
                    Dim cdt As DataTable = LinqDB.ConnectDB.SqlDB.ExecuteTable(sql, p)
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


#End Region



End Class