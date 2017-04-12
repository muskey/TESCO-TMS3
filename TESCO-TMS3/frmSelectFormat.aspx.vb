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
            Dim str As String = ""
            If (UserData.UserMassage.Rows.Count > 0) Then
                For i As Int32 = 0 To UserData.UserMassage.Rows.Count - 1
                    Dim dr As DataRow = UserData.UserMassage.Rows(i)
                    str += " <h4 class=""group inner list-group-item-heading"">" + dr("name").ToString + " </h4>"
                    str += " <p class=""group inner list-group-item-text"">" + dr("description").ToString + " </p>"
                Next

            End If
            lblNEWS.Text = str
        Catch ex As Exception

        End Try
    End Sub

    Private Sub SetFormat()
        Try
            Dim str As String = ""
            If (UserData.UserFormat.Rows.Count > 0) Then
                For i As Int32 = 0 To UserData.UserFormat.Rows.Count - 1
                    Dim dr As DataRow = UserData.UserFormat.Rows(i)
                    str += " <p> <button class=""btn-block btn btn-larges"" id=" + dr("format_id").ToString + " onclick=""fselect('" + dr("format_id").ToString + "','" + dr("format_title").ToString + "');return false;"" >" + dr("format_title").ToString + "</button></p>"
                Next

            End If
            lblBotton.Text = str
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnFormat_Click(sender As Object, e As EventArgs) Handles btnFormat.Click
        If Me.txtFormatID.Text <> "" Then
            Response.Redirect("frmSelectFunction.aspx?rnd=" & DateTime.Now.Millisecond & "&format_id=" & Me.txtFormatID.Text & "&formar_title=" + Me.txtFormatTitle.Text)
        End If
    End Sub
#End Region


End Class