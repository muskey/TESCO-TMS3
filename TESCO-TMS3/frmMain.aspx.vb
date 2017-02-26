Public Class frmMain
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        For Each dr As DataRow In UserData.UserFormat.Rows

        Next
    End Sub

End Class