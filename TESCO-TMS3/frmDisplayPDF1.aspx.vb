Imports System.Drawing
Imports System.IO
Imports System.Data.SqlClient
Imports System.Windows.Forms
Imports LinqDB.ConnectDB


Public Class frmDisplayPDF1
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
    Public ReadOnly Property User_Folder As String
        Get
            Dim UserData As UserProfileData = DirectCast(Session("UserData"), UserProfileData)
            Return UserData.UserID
        End Get
    End Property
#End Region

#Region "Initail"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then

            UpdateLog(Me, Me.GetType, UserData.LoginHistoryID, id, UserData.CurrentClassID, UserData.Token, DirectCast(Session("UserDataCourseFile"), DataTable), UserData.UserName)
            SetContent()
            GetData()
            GetBotton()
        End If
    End Sub

    Private Sub GetData()
        ' Dim UserID As String = "3"
        Dim sql As String = " select file_url from TB_USER_COURSE_DOCUMENT_File  where id=@_ID"
        Dim p(1) As SqlParameter
        p(0) = SqlDB.SetBigInt("@_ID", id)
        Dim dt As DataTable = SqlDB.ExecuteTable(sql, p)
        If dt.Rows.Count > 0 Then

            Dim frompath = dt.Rows(0)("file_url") & ""
            Dim topath = Server.MapPath("~/Document") & "\" & User_Folder
            Dim OutputFile As String = Server.MapPath("~/Document") & "\TempPDF\" & Path.GetFileName(frompath)

            DeleteFilesFromFolder(topath)
            DeleteFilesFromFolder(New FileInfo(OutputFile).Directory.FullName)

            If (Not System.IO.Directory.Exists(topath)) Then
                System.IO.Directory.CreateDirectory(topath)
            End If

            If Directory.Exists(Server.MapPath("~/Document") & "\TempPDF\") = False Then
                Directory.CreateDirectory(Server.MapPath("~/Document") & "\TempPDF\")
            End If

            If GetFileFromURL(frompath, OutputFile) = True Then
                'splitpdf(OutputFile, topath)
            End If


        End If
        dt.Dispose()
    End Sub

    Sub DeleteFilesFromFolder(Folder As String)
        If Directory.Exists(Folder) Then
            For Each _file As String In Directory.GetFiles(Folder)
                File.Delete(_file)
            Next
            For Each _folder As String In Directory.GetDirectories(Folder)
                DeleteFilesFromFolder(_folder)
            Next
        End If
    End Sub


    'Private Sub splitpdf(ByVal frompath As String, ByVal topath As String)
    '    Try
    '        Dim pdf As New PDFConvertor()
    '        Dim CoursePDF As DataTable = pdf.ConvertToDatatable(frompath, topath, Imaging.ImageFormat.Jpeg)
    '        If CoursePDF.Rows.Count = 0 Then
    '            Throw New ArgumentException("Not enough pages in source pdf to split")
    '        End If

    '        Session("CoursePdf") = CoursePDF

    '        Me.ddlPage.DataSource = CoursePDF
    '        Me.ddlPage.DataTextField = "next_id"
    '        Me.ddlPage.DataValueField = "next_id"
    '        Me.ddlPage.DataBind()

    '        If (CoursePDF.Rows.Count > 0) Then
    '            SetInitShow(CoursePDF.Rows(0))
    '            Me.txtPre.Text = 0
    '            Me.txtCurrent.Text = 1
    '            Me.txtNext.Text = IIf(CoursePDF.Rows.Count = 1, 1, 2)
    '            Me.txtMax.Text = CoursePDF.Rows.Count
    '        End If
    '        GetBottonPDF()

    '    Catch ex As Exception
    '        MsgBox(ex.Message, MsgBoxStyle.Information, "splitpdf")
    '        'errormsg = ex.Message

    '    End Try
    'End Sub



    Private Sub SetInitShow(path As DataRow)
        myIframe.Attributes.Add("src", path("pathname").ToString())
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
    Private Sub ddlPage_Click(sender As Object, e As EventArgs) Handles ddlPage.SelectedIndexChanged
        Me.txtCurrent.Text = Val(Me.ddlPage.SelectedValue)
        Dim dtback As DataTable = Session("CoursePdf")
        Dim foundRows() As DataRow
        Dim strfillter As String = "next_id='" & Me.txtCurrent.Text & "'"
        foundRows = dtback.Select(strfillter)

        Dim i As Integer
        For i = 0 To foundRows.GetUpperBound(0)
            SetInitShow(foundRows(i))
        Next i
        Me.txtPre.Text = Val(Me.txtCurrent.Text) - 1
        Me.txtCurrent.Text = Val(Me.txtCurrent.Text)
        Me.txtNext.Text = Val(Me.txtCurrent.Text) + 1

        GetBottonPDF()

        LogFileBL.LogTrans(UserData.LoginHistoryID, "เลือกหน้า " & txtCurrent.Text)
    End Sub

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
            url = "frmDisplayPDF.aspx?id=" + dr("id").ToString
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
        lblPDFPage.ForeColor = Color.White
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
        LogFileBL.LogTrans(UserData.LoginHistoryID, "คลิกปุ่ม Home ")
        Response.Redirect("frmSelectFormat.aspx")
    End Sub

    Private Sub btnPDFBack_Click(sender As Object, e As EventArgs) Handles btnPDFBack.Click

        Dim dtback As DataTable = Session("CoursePdf")
        Dim foundRows() As DataRow
        Dim strfillter As String = "next_id='" & Me.txtPre.Text & "'"
        foundRows = dtback.Select(strfillter)

        Dim i As Integer
        For i = 0 To foundRows.GetUpperBound(0)
            SetInitShow(foundRows(i))
        Next i
        Me.txtPre.Text = Val(Me.txtPre.Text) - 1
        Me.txtCurrent.Text = Val(Me.txtCurrent.Text) - 1
        Me.txtNext.Text = Val(Me.txtNext.Text) - 1


        GetBottonPDF()

        LogFileBL.LogTrans(UserData.LoginHistoryID, "คลิกปุ่ม Previous Page")
    End Sub

    Private Sub btnPDFNext_Click(sender As Object, e As EventArgs) Handles btnPDFNext.Click

        Dim dtnext As DataTable = Session("CoursePdf")
        Dim foundRows() As DataRow
        Dim strfillter As String = "next_id='" & Me.txtNext.Text & "'"
        foundRows = dtnext.Select(strfillter)

        Dim i As Integer
        For i = 0 To foundRows.GetUpperBound(0)
            SetInitShow(foundRows(i))
        Next i


        Me.txtPre.Text = Val(Me.txtPre.Text) + 1
        Me.txtCurrent.Text = Val(Me.txtCurrent.Text) + 1
        Me.txtNext.Text = Val(Me.txtNext.Text) + 1

        GetBottonPDF()

        LogFileBL.LogTrans(UserData.LoginHistoryID, "คลิกปุ่ม Next Page")
    End Sub

    Private Sub GetBottonPDF()
        If Me.txtPre.Text = "0" Then
            Me.btnPDFBack.Visible = False
        Else
            Me.btnPDFBack.Visible = True
        End If

        If Val(Me.txtCurrent.Text) >= Val(Me.txtMax.Text) Then
            Me.btnPDFNext.Visible = False
        Else
            Me.btnPDFNext.Visible = True
        End If

        Me.lblPDFPage.Text = "/" & Me.txtMax.Text
        Me.ddlPage.SelectedIndex = Val(Me.txtCurrent.Text) - 1
    End Sub
#End Region

End Class