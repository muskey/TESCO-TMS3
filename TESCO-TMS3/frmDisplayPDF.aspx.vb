
Imports System.IO
Imports System.Data.SqlClient
Imports LinqDB.ConnectDB
'Imports Spire.Pdf
'Imports Spire.Pdf.Graphics

Public Class frmDisplayPDF
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
        If Not Page.IsPostBack Then
            GetData()
        End If
    End Sub

    Private Sub GetData()

        Dim sql As String = " select * from TB_USER_COURSE_DOCUMENT_File  where id=@_ID"
        Dim p(1) As SqlParameter
        p(0) = SqlDB.SetBigInt("@_ID", id)
        Dim dt As DataTable = SqlDB.ExecuteTable(sql, p)
        If (dt.Rows.Count > 0) Then

            Dim frompath = dt.Rows(0)("file_url") & ""
            Dim topath = Server.MapPath("~/Document") & "\" & UserData.UserID

            DeleteFilesFromFolder(topath)

            If (Not System.IO.Directory.Exists(topath)) Then
                System.IO.Directory.CreateDirectory(topath)
            End If

            splitpdf(frompath, topath)
        End If
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


    Private Sub splitpdf(ByVal frompath As String, ByVal topath As String)

        Dim CoursePDF = New DataTable
        CoursePDF.Columns.Add("next_id")
        CoursePDF.Columns.Add("pathName")


        Dim reader As iTextSharp.text.pdf.PdfReader = Nothing
        Dim doc As iTextSharp.text.Document = Nothing
        Dim pdfcpy As iTextSharp.text.pdf.PdfCopy = Nothing
        Dim page As iTextSharp.text.pdf.PdfImportedPage = Nothing
        Dim pagecount As Integer = 0
        Dim outfile As String = String.Empty
        Try
            reader = New iTextSharp.text.pdf.PdfReader(frompath)
            pagecount = reader.NumberOfPages
            If pagecount <= 0 Then
                Throw New ArgumentException("Not enough pages in source pdf to split")
            Else
                Dim currentpage As Integer = 1
                Dim ext As String = Path.GetExtension(frompath)


                For i As Integer = 1 To pagecount
                    outfile = frompath.Replace(".pdf", "") & "_" & i.ToString & ".pdf"
                    doc = New iTextSharp.text.Document(reader.GetPageSizeWithRotation(currentpage))

                    Dim filename = Path.GetFileName(outfile)
                    Dim savepath = topath & "\" & filename
                    pdfcpy = New iTextSharp.text.pdf.PdfCopy(doc, New System.IO.FileStream(savepath, FileMode.Create))
                    doc.Open()
                    page = pdfcpy.GetImportedPage(reader, currentpage)
                    pdfcpy.AddPage(page)
                    currentpage += 1
                    doc.Close()

                    Dim strUrl As String = Request.Url.GetLeftPart(UriPartial.Authority) & Request.ApplicationPath & "Document\3\" & filename
                    Dim drcoursepdf As DataRow = CoursePDF.NewRow
                    drcoursepdf("next_id") = i
                    drcoursepdf("pathname") = strUrl
                    CoursePDF.Rows.Add(drcoursepdf)
                Next
            End If
            reader.Close()
            Session("CoursePdf") = CoursePDF


            If (CoursePDF.Rows.Count > 0) Then
                SetIniPage(CoursePDF.Rows(0))
                Me.txtPre.Text = 0
                Me.txtCurrent.Text = 1
                Me.txtNext.Text = IIf(CoursePDF.Rows.Count = 1, 1, 2)
                Me.txtMax.Text = pagecount
            End If
            GetBotton()

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, "splitpdf")
            'errormsg = ex.Message

        End Try
    End Sub



    Private Sub SetIniPage(path As DataRow)
        myIframe.Attributes.Add("src", path("pathname").ToString())
    End Sub

#End Region

#Region "Event Handle"
    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click

        Dim dtback As DataTable = Session("CoursePdf")
        Dim foundRows() As DataRow
        Dim strfillter As String = "next_id='" & Me.txtPre.Text & "'"
        foundRows = dtback.Select(strfillter)

        Dim i As Integer
        For i = 0 To foundRows.GetUpperBound(0)
            SetIniPage(foundRows(i))
        Next i
        Me.txtPre.Text = Val(Me.txtPre.Text) - 1
        Me.txtCurrent.Text = Val(Me.txtCurrent.Text) - 1
        Me.txtNext.Text = Val(Me.txtNext.Text) - 1


        GetBotton()
    End Sub

    Private Sub btnNext_Click(sender As Object, e As EventArgs) Handles btnNext.Click

        Dim dtnext As DataTable = Session("CoursePdf")
        Dim foundRows() As DataRow
        Dim strfillter As String = "next_id='" & Me.txtNext.Text & "'"
        foundRows = dtnext.Select(strfillter)

        Dim i As Integer
        For i = 0 To foundRows.GetUpperBound(0)
            SetIniPage(foundRows(i))
        Next i


        Me.txtPre.Text = Val(Me.txtPre.Text) + 1
        Me.txtCurrent.Text = Val(Me.txtCurrent.Text) + 1
        Me.txtNext.Text = Val(Me.txtNext.Text) + 1


        GetBotton()
    End Sub

    Private Sub GetBotton()
        If Me.txtPre.Text = "0" Then
            Me.btnBack.Enabled = False
        Else
            Me.btnBack.Enabled = True
        End If

        If Val(Me.txtCurrent.Text) >= Val(Me.txtMax.Text) Then
            Me.btnNext.Enabled = False
        Else
            Me.btnNext.Enabled = True
        End If
    End Sub
#End Region
End Class