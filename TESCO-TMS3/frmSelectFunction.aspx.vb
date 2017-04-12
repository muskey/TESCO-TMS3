Public Class frmSelectFunction
    Inherits System.Web.UI.Page

#Region "Declare & Valiable"
    ' Public myUser As User
    Public ReadOnly Property UserData As UserProfileData
        Get
            Return Session("UserData")
        End Get
    End Property

    Public ReadOnly Property Format_Id As String
        Get
            Return Page.Request.QueryString("format_id") & ""
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
        If Not Page.IsPostBack Then
            Me.lblTitle.Text = "<h3><font color=""#019b79"">&nbsp; &nbsp; &nbsp;" + formar_title + "</font></h3>"
            SetFuntion()
        End If

    End Sub


    Private Sub SetFuntion()
        Try
            Dim strMain As String = "<ul class=""tiles"">"
            Dim strsub As String = "<ul class=""tiles"">"
            Dim strLink As Int16 = 0
            If (UserData.UserFunction.Rows.Count > 0) Then
                UserData.UserFunction.DefaultView.RowFilter = "format_id='" & Format_Id & "'"
                For Each dr As DataRowView In UserData.UserFunction.DefaultView

                    Dim bgColor As String = ""
                    Dim sql As String = "select id from TB_USER_DEPARTMENT where function_id=@_FUNCTION_ID"
                    'Dim cdt As DataTable = GetSqlDataTable(sql)

                    Dim p(1) As System.Data.SqlClient.SqlParameter
                    p(0) = LinqDB.ConnectDB.SqlDB.SetBigInt("@_FUNCTION_ID", dr("function_id"))
                    Dim cdt As DataTable = LinqDB.ConnectDB.SqlDB.ExecuteTable(sql, p)
                    If cdt.Rows.Count = 0 Then
                        bgColor = "Gray"
                        strLink = 0
                    Else
                        bgColor = dr("function_cover_color").ToString
                        strLink = 1
                    End If

                    If dr("function_subject") = "main subject" Then
                        strMain += " <li  onclick=""fselect('" + dr("function_id").ToString + "','" + strLink.ToString() + "','" + dr("function_title") + "','" + bgColor.Replace("#", "") + "');"" id=" + dr("function_id") + " style=""background-color:" + bgColor + """>"
                        strMain += " <a href=""#""><span><img src=" + dr("function_cover") + " height=""50"" width=""50""></span><h5 Class=""text-center"">" + dr("function_title") + "(" + cdt.Rows.Count.ToString + ")</h5></a>"
                        strMain += "  </li>"
                    ElseIf dr("function_subject") = "additional subject" Then
                        strsub += " <li  onclick=""fselect('" + dr("function_id").ToString + "','" + strLink.ToString() + "','" + dr("function_title") + "','" + bgColor.Replace("#", "") + "');"" id=" + dr("function_id") + " style=""background-color:" + bgColor + """>"
                        strsub += " <a href=""#""><span><img src=" + dr("function_cover") + " height=""50"" width=""50""></span><h5 Class=""text-center"">" + dr("function_title") + "(" + cdt.Rows.Count.ToString + ")</h5></a>"
                        strsub += "  </li>"
                    End If
                Next

            End If
            strMain += "</ul>"
            strsub += "</ul>"
            lblMain.Text = strMain
            lblSub.Text = strsub
        Catch ex As Exception

        End Try
    End Sub
#End Region


End Class