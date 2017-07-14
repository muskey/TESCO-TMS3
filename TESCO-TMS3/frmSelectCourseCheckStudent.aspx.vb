Public Class frmSelectCourseCheckStudent
    Inherits System.Web.UI.Page
    Protected ReadOnly Property UserData As UserProfileData
        Get
            Return Session("UserData")
        End Get
    End Property

    Public ReadOnly Property UserCourseID As String
        Get
            Return Page.Request.QueryString("user_course_id")
        End Get
    End Property
    Public ReadOnly Property Course_title As String
        Get
            Return Page.Request.QueryString("title") & ""
        End Get
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            ''''ScriptManager.RegisterStartupScript(Me, Me.GetType, Guid.NewGuid().ToString(), "ShowPopup('" & UserCourseID & "')", True)
        End If
    End Sub

End Class