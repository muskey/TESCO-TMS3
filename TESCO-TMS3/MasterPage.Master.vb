
Public Class MasterPage
    Inherits System.Web.UI.MasterPage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("UserData") IsNot Nothing Then
            Dim UserData As UserProfileData = Session("UserData")
            Me.lblUsername.Text = UserData.FullName & ""
        Else
            Response.Redirect("frmLogin.aspx?rnd=" & DateTime.Now.Millisecond)
        End If
    End Sub

    Private Sub btnTesting_Click(sender As Object, e As EventArgs) Handles btnTesting.Click
        Response.Redirect("frmSelectTestCourse.aspx?rnd=" & DateTime.Now.Millisecond)
    End Sub
    Private Sub btnLogout_Click(sender As Object, e As EventArgs) Handles btnLogout.Click
        Session.Abandon()
        Response.Redirect("frmLogin.aspx?rnd=" & DateTime.Now.Millisecond)
    End Sub
    Private Sub btnCourse_Click(sender As Object, e As EventArgs) Handles btnCourse.Click
        Response.Redirect("frmSelectFormat.aspx?rnd=" & DateTime.Now.Millisecond)
    End Sub



End Class