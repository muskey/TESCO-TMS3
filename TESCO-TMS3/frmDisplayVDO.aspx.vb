Imports System.Data.SqlClient
Imports LinqDB.ConnectDB
Public Class frmDisplayVDO
    Inherits System.Web.UI.Page

#Region "Declare & Valiable"
    Public ReadOnly Property id As String
        Get
            Return Page.Request.QueryString("id") & ""
        End Get
    End Property
#End Region

#Region "Initail"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            UpdateLog(id, Session("ClassID"))
            SetContent()
            GetData()
            GetBotton()
        End If
    End Sub

    Private Sub GetData()
        Dim sql As String = " select file_url from TB_USER_COURSE_DOCUMENT_File  where id=@_ID"
        Dim p(1) As SqlParameter
        p(0) = SqlDB.SetBigInt("@_ID", id)

        Dim dt As DataTable = SqlDB.ExecuteTable(sql, p)
        If dt.Rows.Count > 0 Then
            If Convert.IsDBNull(dt.Rows(0)("file_url")) = False Then
                myVideo.Attributes.Add("src", dt.Rows(0)("file_url"))
            End If
        End If
    End Sub

    Private Sub SetContent()
        Try
            Dim dtUserDataCourseFile As DataTable = Session("UserDataCourseFile")
            Dim str As String = ""
            If (dtUserDataCourseFile.Rows.Count > 0) Then
                For i As Int32 = 0 To dtUserDataCourseFile.Rows.Count - 1
                    Dim dr As DataRow = dtUserDataCourseFile.Rows(i)
                    str += " <p> <button class=""btn-block btn btn-larges"" id=" + dr("id").ToString + " onclick=""fselect('" + dr("id").ToString + "','" + dr("file_url").ToString + "','" + dr("rowindex").ToString + "');return false;"" >" + dr("file_title").ToString + "</button></p>"
                Next

            End If
            lblContent.Text = str

            Me.btnCloseContent.Style.Add("visibility", "hidden")
            Me.btnCloseContent.Attributes.Add("onclick", "hidecontent(); return false;")
            Me.btnContent.Attributes.Add("onclick", "ShowPopup(); return false;")

        Catch ex As Exception

        End Try
    End Sub
#End Region

#Region "Event Handle"
    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click

        Dim dtback As DataTable = Session("UserDataCourseFile")
        Dim foundRows() As DataRow
        Dim back_id = Val(id) - 1
        Dim strfillter As String = "id <=" & back_id.ToString
        foundRows = dtback.Select(strfillter)
        Dim i As Integer = foundRows.Length - 1
        If foundRows.Length > 0 Then
            SetIniPage(foundRows(i))
        End If
    End Sub

    Private Sub btnNext_Click(sender As Object, e As EventArgs) Handles btnNext.Click

        Dim dtnext As DataTable = Session("UserDataCourseFile")
        Dim foundRows() As DataRow
        Dim next_id = Val(id) + 1
        Dim strfillter As String = "id >=" & next_id.ToString
        foundRows = dtnext.Select(strfillter)
        If foundRows.Length > 0 Then
            SetIniPage(foundRows(0))
        End If
    End Sub


    Private Sub SetIniPage(dr As DataRow)
        Dim url As String
        If dr("file_url").ToString.IndexOf(".png") <> -1 Or dr("file_url").ToString.IndexOf(".jpg") <> -1 Then
            ' myIframe.Attributes.Add("src", "frmDisplayImage.aspx?id=" + dr("id").ToString)
            url = "frmDisplayImage.aspx?id=" + dr("id").ToString
            'ScriptManager.RegisterStartupScript(Me, Page.GetType, "Script", "loadIframe('" + url + "');", True)

            Response.Redirect(url)
        ElseIf dr("file_url").ToString.IndexOf(".pdf") <> -1 Then
            'myIframe.Attributes.Add("src", "frmDisplayPDF.aspx?id=" + dr("id").ToString)
            url = "frmDisplayPDF.aspx?id=" + dr("id").ToString
            'ScriptManager.RegisterStartupScript(Me, Page.GetType, "Script", "loadIframe('" + url + "');", True)
            Response.Redirect(url)
        ElseIf dr("file_url").ToString.IndexOf(".mp4") <> -1 Then
            'myIframe.Attributes.Add("src", "frmDisplayVDO.aspx?id=" + dr("id").ToString)
            url = "frmDisplayVDO.aspx?id=" + dr("id").ToString
            'ScriptManager.RegisterStartupScript(Me, Page.GetType, "Script", "loadIframe('" + url + "');", True)
            Response.Redirect(url)
        ElseIf dr("file_url").ToString.IndexOf(".html") <> -1 Then
            'myIframe.Attributes.Add("src", "frmDisplayHTML.aspx?id=" + dr("id").ToString)
            url = "frmDisplayHTML.aspx?id=" + dr("id").ToString
            ' ScriptManager.RegisterStartupScript(Me, Page.GetType, "Script", "loadIframe('" + url + "');", True)
            Response.Redirect(url)
        End If

    End Sub

    Public Sub GetBotton()

        Dim dtnext As DataTable = Session("UserDataCourseFile")
        Dim strfillterBack As String = "id <" & id
        Dim strfillterNext As String = "id >" & id
        dtnext.DefaultView.RowFilter = strfillterBack
        If dtnext.DefaultView.Count = 0 Then
            Me.btnBack.Visible = False
        Else
            Me.btnBack.Visible = True
        End If


        dtnext.DefaultView.RowFilter = strfillterNext
        If dtnext.DefaultView.Count = 0 Then
            Me.btnNext.Visible = False
        Else
            Me.btnNext.Visible = True
        End If

    End Sub

    Private Sub btnHome_Click(sender As Object, e As EventArgs) Handles btnHome.Click
        Response.Redirect("frmSelectFormat.aspx")
    End Sub
#End Region
    '#Region "Log"
    '    Private Sub UpdateLog()
    '        Dim tb_user_course_document_id As String = "0"
    '        Dim doc_id As String = "0"
    '        Dim course_id As String = "0"
    '        Dim cassid As Long = 0
    '        Dim Sql As String
    '        Sql = " select * from TB_USER_COURSE_DOCUMENT_FILE  where id=" & id
    '        Dim dtCourseFile As DataTable = GetSqlDataTable(Sql)
    '        If dtCourseFile.Rows.Count Then
    '            doc_id = dtCourseFile.Rows(0)("tb_user_course_document_id")

    '            Sql = " select *  from TB_USER_COURSE_DOCUMENT where id=" & doc_id
    '            Dim dtCourse As DataTable = GetSqlDataTable(Sql)
    '            If dtCourse.Rows.Count > 0 Then
    '                course_id = dtCourse.Rows(0)("tb_user_course_id")

    '            End If
    '        End If

    '        If Not IsNothing(Session("ClassID")) Then
    '            cassid = Session("ClassID")
    '        End If
    '        Dim dtnext As DataTable = Session("UserDataCourseFile")
    '        Dim strfillter As String = "id >" & id
    '        dtnext.DefaultView.RowFilter = strfillter
    '        If dtnext.DefaultView.Count = 0 Then
    '            CallAPIUpdateLog(UserData.Token, 13, "complete", "class", "{" & Chr(34) & "class_id" & Chr(34) & ":" & cassid.ToString & "}")

    '        Else
    '            CallAPIUpdateLog(UserData.Token, 13, "complete", "document", "{" & Chr(34) & "class_id" & Chr(34) & ":" & cassid.ToString & "," & Chr(34) & "document_id" & Chr(34) & ":" & doc_id & "}")

    '        End If
    '    End Sub
    '#End Region


End Class