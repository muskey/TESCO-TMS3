Imports System.Data.SqlClient
Imports LinqDB.ConnectDB
Public Class frmDisplayHTML
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
            GetData()
        End If
    End Sub

    Private Sub GetData()
        Dim sql As String = " select * from TB_USER_COURSE_DOCUMENT_File  where id=@_ID"
        Dim p(1) As SqlParameter
        p(0) = SqlDB.SetBigInt("@_ID", id)

        Dim dt As DataTable = SqlDB.ExecuteTable(sql, p)
        If (dt.Rows.Count > 0) Then
            myIframe.Attributes.Add("src", dt.Rows(0)("file_url"))
        End If
    End Sub
#End Region

End Class