Imports System.Data.SqlClient
Imports LinqDB.ConnectDB
Public Class frmDisplayHTML
    Inherits System.Web.UI.Page

#Region "Declare & Valiable"
    Public ReadOnly Property UserData As UserProfileData
        Get
            Return Session("UserData")
        End Get
    End Property
    Public ReadOnly Property id As String
        Get
            Return Page.Request.QueryString("id") & ""
        End Get
    End Property
#End Region

#Region "Initail"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("UserData") Is Nothing Then
            Response.Redirect("Default.aspx")
        End If

        If Not Page.IsPostBack Then
            Dim UserData As UserProfileData = DirectCast(Session("UserData"), UserProfileData)
            UpdateLog(Me, Me.GetType, UserData.LoginHistoryID, id, UserData.CurrentClassID, UserData.Token, DirectCast(Session("UserDataCourseFile"), DataTable), UserData.UserName)
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
                myIframe.Attributes.Add("src", dt.Rows(0)("file_url"))
            End If

        End If
        dt.Dispose()
    End Sub

    Private Sub SetContent()
        Try
            Dim dtUserDataCourseFile As DataTable = Session("UserDataCourseFile")
            Dim str As String = ""
            If (dtUserDataCourseFile.Rows.Count > 0) Then
                For i As Int32 = 0 To dtUserDataCourseFile.Rows.Count - 1
                    Dim dr As DataRow = dtUserDataCourseFile.Rows(i)
                    str += " <p> <button class=""btn-block btn btn-larges"" id=" + dr("id").ToString + " onclick=""fselect('" + dr("id").ToString + "','" + dr("file_url").ToString + "','" + dr("rowindex").ToString + "');return false;"" >" + dr("document_title").ToString + "</button></p>"
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
        LogFileBL.LogTrans(UserData.LoginHistoryID, "คลิกปุ่มเอกสารก่อนหน้า")
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
        LogFileBL.LogTrans(UserData.LoginHistoryID, "คลิกปุ่มเอกสารถัดไป")
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
            LogFileBL.LogTrans(UserData.LoginHistoryID, "แสดงบทเรียน " & dr("file_title") & vbNewLine & "URL=" & dr("file_url"))
            url = "frmDisplayImage.aspx?id=" + dr("id").ToString
            Response.Redirect(url)
        ElseIf dr("file_url").ToString.IndexOf(".pdf") <> -1 Then
            LogFileBL.LogTrans(UserData.LoginHistoryID, "แสดงบทเรียน " & dr("file_title") & vbNewLine & "URL=" & dr("file_url"))
            url = "frmDisplayPDF1.aspx?id=" + dr("id").ToString
            Response.Redirect(url)
        ElseIf dr("file_url").ToString.IndexOf(".mp4") <> -1 Then
            LogFileBL.LogTrans(UserData.LoginHistoryID, "แสดงบทเรียน " & dr("file_title") & vbNewLine & "URL=" & dr("file_url"))
            url = "frmDisplayVDO.aspx?id=" + dr("id").ToString
            Response.Redirect(url)
        ElseIf dr("file_url").ToString.IndexOf(".html") <> -1 Then
            LogFileBL.LogTrans(UserData.LoginHistoryID, "แสดงบทเรียน " & dr("file_title") & vbNewLine & "URL=" & dr("file_url"))
            url = "frmDisplayHTML.aspx?id=" + dr("id").ToString
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
        LogFileBL.LogTrans(UserData.LoginHistoryID, "คลิกปุ่ม Home")
        'id = file id

        '?id=" + id + '&title=' + name + "&color=" + color
        Dim dt As DataTable = GetCourseInfoByFileID(id)
        If dt.Rows.Count > 0 Then
            Response.Redirect("frmSelectCourse.aspx?id=" & dt.Rows(0)("department_id") & "&title=" & dt.Rows(0)("department_title") & "&color=" & dt.Rows(0)("function_cover_color").ToString.Replace("#", ""))
        Else
            Response.Redirect("frmSelectFormat.aspx")
        End If
    End Sub

#End Region


End Class