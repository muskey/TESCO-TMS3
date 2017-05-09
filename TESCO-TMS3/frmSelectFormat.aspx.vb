Imports System.Data.SqlClient
Imports LinqDB.ConnectDB

Public Class frmSelectFormat
    Inherits System.Web.UI.Page

#Region "Declare & Valiable"
    ' Public myUser As User
    Public ReadOnly Property UserData As UserProfileData
        Get
            Return Session("UserData")
        End Get
    End Property
#End Region

#Region "Initail"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Me.txtFormatID.Style.Add("display", "none")
            Me.txtFormatTitle.Style.Add("display", "none")
            SetFormat()
            SetWelcomeMessage()

        End If

    End Sub

    Private Sub SetWelcomeMessage()
        Try
            Dim sql As String = "select id, message_name, message_desc "
            sql += " from TB_USER_MESSAGE "
            sql += " where tb_user_session_id=@_USER_SESSION_ID"
            sql += " order by id"

            Dim UserData As UserProfileData = DirectCast(Session("UserData"), UserProfileData)
            Dim p(1) As SqlParameter
            p(0) = SqlDB.SetBigInt("@_USER_SESSION_ID", UserData.UserSessionID)
            Dim dt As DataTable = SqlDB.ExecuteTable(sql, p)

            Dim str As String = ""
            If (dt.Rows.Count > 0) Then
                str = "<div class=""box-content"">"
                str += "    <div class=""accordion"" id=""accordion2"">"
                For i As Int32 = 0 To dt.Rows.Count - 1
                    Dim dr As DataRow = dt.Rows(i)
                    'str += " <font color=""#019b79""> <h4 class=""group inner list-group-item-heading"">" + dr("message_name").ToString + " </h4></font>"
                    'str += " <font color=""#FFFFFF""> <p class=""group inner list-group-item-text"">" + dr("message_desc").ToString + " </p></font>"

                    str += "    <div class=""accordion-group"">"
                    str += "        <div class=""accordion-heading"" >"
                    str += "            <a class=""accordion-toggle"" data-toggle=""collapse"" data-parent=""#accordion2"" href=""#collapse" & dr("id") & """ style=""color:#019b79"" >"
                    str += "                " & dr("message_name").ToString
                    str += "            </a>"
                    str += "        </div>"
                    str += "        <div id = ""collapse" & dr("id") & """ class=""accordion-body collapse"">"
                    str += "            <div class=""accordion-inner"">"
                    str += "                " & dr("message_desc").ToString
                    str += "            </div>"
                    str += "        </div>"
                    str += "    </div>"




                    '<div Class="accordion-group">
                    '	<div Class="accordion-heading" >
                    '		<a Class="accordion-toggle" data-toggle="collapse" data-parent="#accordion2" href="#collapseOne" style="color:#019b79" >
                    '			Collapsible Group Item #1
                    '		</a>
                    '	</div>
                    '	<div id = "collapseOne" Class="accordion-body collapse">
                    '		<div Class="accordion-inner">
                    '			Anim pariatur cliche...Lorem ipsum dolore dolor occaecat dolore elit deserunt incididunt ex sed nostrud aute aliquip ut elit sed nisi. 
                    '		</div>
                    '	</div>
                    '</div>
                Next
                str += "    </div>"
                str += "</div>"
            End If
            dt.Dispose()

            lblNEWS.Text = str
        Catch ex As Exception

        End Try
    End Sub

    Private Sub SetFormat()
        Try
            Dim sql As String = "select format_id, format_title "
            sql += " from TB_USER_FORMAT "
            sql += " where tb_user_session_id=@_USER_SESSION_ID"
            sql += " order by format_id "

            Dim UserData As UserProfileData = DirectCast(Session("UserData"), UserProfileData)
            Dim p(1) As SqlParameter
            p(0) = SqlDB.SetBigInt("@_USER_SESSION_ID", UserData.UserSessionID)

            Dim dt As DataTable = SqlDB.ExecuteTable(sql, p)
            Dim str As String = ""
            If dt.Rows.Count Then
                str = "<select  data-plugin=""selectpicker"" class=""select2-success"" onchange=""fselectDDL(this)"" >"
                For i As Int32 = 0 To dt.Rows.Count - 1
                    Dim dr As DataRow = dt.Rows(i)
                    Dim Sel As String = ""
                    If i = 0 Then
                        Sel = "selected='selected'"
                        txtFormatID.Text = dr("format_id")
                        txtFormatTitle.Text = dr("format_title")
                    End If

                    'str += " < p <> Button Class=""btn-block btn btn-larges"" id=" + dr("format_id").ToString + " onclick=""fselect('" + dr("format_id").ToString + "','" + dr("format_title").ToString + "');return false;"" >" + dr("format_title").ToString + "</button></p>"
                    str += "<option value=" & dr("format_id") & " " & Sel & " >" & dr("format_title") & "</option>"
                Next
                str += "</select>"
            End If
            dt.Dispose()
            lblDropdownListFormat.Text = str
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnFormat_Click(sender As Object, e As EventArgs) Handles btnFormat.Click
        If Me.txtFormatID.Text <> "" Then
            Session("backpathname1") = "Format"
            Response.Redirect("frmSelectFunction.aspx?rnd=" & DateTime.Now.Millisecond & "&format_id=" & Me.txtFormatID.Text & "&formar_title=" + Me.txtFormatTitle.Text)
        End If
    End Sub
#End Region


End Class