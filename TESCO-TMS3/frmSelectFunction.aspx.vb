Imports System.Data.SqlClient
Imports LinqDB.ConnectDB
Imports System.Web.Services

Public Class frmSelectFunction
    Inherits System.Web.UI.Page

#Region "Declare & Valiable"
    ' Public myUser As User
    Public ReadOnly Property UserData As UserProfileData
        Get
            Return Session("UserData")
        End Get
    End Property

    Public ReadOnly Property User_Format_Id As String
        Get
            Return Page.Request.QueryString("user_format_id") & ""
        End Get
    End Property

    Public ReadOnly Property formar_title As String
        Get
            Return Page.Request.QueryString("formar_title") & ""
        End Get
    End Property


#End Region

#Region "Initail"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("UserData") Is Nothing Then
            Response.Redirect("Default.aspx")
        End If

        If Not Page.IsPostBack Then
            Me.lblTitle.Text = "<h3>&nbsp>&nbsp<a onClick=""return CreateTransLog('" & UserData.LoginHistoryID & "','กลับหน้าจอเลือก Format')"" href=""frmSelectFormat.aspx""><font color=""#019b79"" style=""font-size:30px"">" + Session("backpathname1") + "&nbsp>&nbsp</font></a><font color=""#019b79"" style=""font-size:30px"">" + formar_title + "</font></h3>"
            SetFuntion()

            LogFileBL.LogTrans(UserData.LoginHistoryID, "แสดงหน้าจอเลือก Function")
        End If
    End Sub

    <WebMethod()>
    Public Shared Function SetBackPath(strpath As String, strtitlename As String)
        Dim objp = New Page()
        objp.Session("backpath2") = strpath
        objp.Session("backpathname2") = strtitlename
        Return strpath
    End Function

    Private Sub SetFuntion()
        Try
            'Dim UserData As UserProfileData = Session("UserData")
            Dim strMain As String = "<ul class=""tiles"">"
            Dim strsub As String = "<ul class=""tiles"">"
            Dim strLink As Int16 = 0
            Dim sql As String = " select * from TB_USER_FUNCTION  where tb_user_format_id=@_USER_FORMAT_ID "
            Dim p(1) As SqlParameter
            p(0) = SqlDB.SetText("@_USER_FORMAT_ID", User_Format_Id)

            Dim dt As DataTable = SqlDB.ExecuteTable(sql, p)
            If dt.Rows.Count > 0 Then
                For Each dr As DataRowView In dt.DefaultView

                    Dim bgColor As String = ""
                    sql = "select id from TB_USER_DEPARTMENT where tb_user_function_id=@_USER_FUNCTION_ID"
                    ReDim p(1)
                    p(0) = SqlDB.SetText("@_USER_FUNCTION_ID", dr("id").ToString)
                    Dim cdt As DataTable = SqlDB.ExecuteTable(sql, p)
                    If cdt.Rows.Count = 0 Then
                        bgColor = "Gray"
                        strLink = 0
                    Else
                        bgColor = dr("function_cover_color").ToString
                        strLink = 1
                    End If

                    If dr("function_subject_type") = "m" Then
                        strMain += " <li  onclick=""fselect('" + dr("id").ToString + "','" + strLink.ToString() + "','" + dr("function_title").ToString + "','" + bgColor.Replace("#", "") + "');"" id=" + dr("id").ToString + " style=""background-color:" + bgColor + """>"
                        strMain += " <a href=""#"">"
                        strMain += "    <span>"
                        strMain += "        <img src=" + dr("function_cover_url").ToString + " height=""60"" width=""60"" />"
                        strMain += "    </span>"
                        strMain += "    <span class=""text-center"" style=""font-size:22px;padding-top:25px;"" >" + dr("function_title") + "(" + cdt.Rows.Count.ToString + ")</span>"
                        strMain += " </a>"
                        strMain += " </li>"
                    ElseIf dr("function_subject_type") = "a" Then
                        strsub += " <li  onclick=""fselect('" + dr("id").ToString + "','" + strLink.ToString() + "','" + dr("function_title").ToString + "','" + bgColor.Replace("#", "") + "');"" id=" + dr("id").ToString + " style=""background-color:" + bgColor + """>"
                        strsub += " <a href=""#"">"
                        strsub += "     <span>"
                        strsub += "         <img src=" + dr("function_cover_url").ToString + " height=""60"" width=""60"" />"
                        strsub += "     </span>"
                        strsub += "     <span class=""text-center"" style=""font-size:22px;padding-top:25px;vertical-align:middle;"" >" + dr("function_title") + "(" + cdt.Rows.Count.ToString + ")</span>"
                        strsub += " </a>"
                        strsub += " </li>"
                    End If
                Next

            End If
            dt.Dispose()
            strMain += "</ul>"
            strsub += "</ul>"
            lblMain.Text = strMain
            lblSub.Text = strsub
        Catch ex As Exception
            LogFileBL.LogException(UserData, ex.Message, ex.StackTrace)
        End Try
    End Sub
    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        LogFileBL.LogTrans(UserData.LoginHistoryID, "คลิกปุ่ม Back")
        Response.Redirect("frmSelectFormat.aspx")
    End Sub

#End Region


End Class