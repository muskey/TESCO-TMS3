Public Class MasterPage
    Inherits System.Web.UI.MasterPage


#Region "Declare & Valiable"
    ' Public myUser As User
    Public ReadOnly Property UserData As UserProfileData
        Get
            Return Session("UserData")
        End Get
    End Property
#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("UserData") IsNot Nothing Then
            Me.lblUsername.Text = UserData.FullName & ""
        Else
            Response.Redirect("Default.aspx?rnd=" & DateTime.Now.Millisecond)
        End If
    End Sub

    Private Sub btnTesting_Click(sender As Object, e As EventArgs) Handles btnTesting.Click
        'LogFileBL.LogTrans(UserData.LoginHistoryID, "คลิกเมนู แบบทดสอบ")
        Response.Redirect("frmSelectTestCourse.aspx?rnd=" & DateTime.Now.Millisecond)
    End Sub
    Private Sub btnLogout_Click(sender As Object, e As EventArgs) Handles btnLogout.Click
        'LogFileBL.LogTrans(UserData.LoginHistoryID, "คลิกเมนู ออกจากระบบ")
        Session.Abandon()
        Response.Redirect("~/")
    End Sub
    Private Sub btnCourse_Click(sender As Object, e As EventArgs) Handles btnCourse.Click
        'LogFileBL.LogTrans(UserData.LoginHistoryID, "คลิกเมนู บทเรียน")
        Response.Redirect("frmSelectFormat.aspx?rnd=" & DateTime.Now.Millisecond)
    End Sub


End Class