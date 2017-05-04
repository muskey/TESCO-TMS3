Public Class frmSelectTestCourse
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
            SetTestSubject()
            SetStatistict()
        End If

    End Sub


    Private Sub SetStatistict()
        Try
            Dim str As String = ""
            str += " <div class=""row-fluid"">"
            str += "    <span class=""text-center"" style=""font-size:28px;"">คุณเรียนจบหลักสูตรแล้วทั้งหมด <span style=""color:#019b79"">" + UserData.CourseTotal.ToString + "</span> หลักสูตร</span>"
            str += " </div>"
            str += " <div class=""row-fluid"">"
            str += "    <span class=""text-center"" style=""font-size:22px;"">จากทั้งหมด <span style=""color:#019b79"">" + UserData.CourseTotal.ToString + "</span> หลักสูตร</span>"
            str += " </div>"

            str += " <div class=""row-fluid"">&nbsp;</div>"
            str += " <div class=""row-fluid"">"
            str += "    <span class=""text-center"" style=""font-size:28px;"">คุณได้ทำบททดสอบแล้วทั้งหมด <span style=""color:#019b79"">" + UserData.TestingAttempt.ToString + "</span> หลักสูตร</span>"
            str += " </div>"
            str += " <div class=""row-fluid"">"
            str += "    <span class=""text-center"" style=""font-size:22px;"">จากทั้งหมด <span style=""color:#019b79"">" + UserData.TestingComplete.ToString + "</span> หลักสูตร</span>"
            str += " </div>"

            str += " <div class=""row-fluid"">&nbsp;</div>"
            str += " <div class=""row-fluid"">"
            str += "    <span class=""text-center"" style=""font-size:28px;"">คุณได้ทำบททดสอบผ่านแล้วทั้งหมด <span style=""color:#019b79"">" + UserData.TestingTotal.ToString + "</span> หลักสูตร</span>"
            str += " </div>"
            str += " <div class=""row-fluid"">"
            str += "    <span class=""text-center"" style=""font-size:22px;"">จากทั้งหมด <span style=""color:#019b79"">" + UserData.TestingTotal.ToString + "</span> หลักสูตร</span>"
            str += " </div>"
            lblNEWS.Text = str
        Catch ex As Exception

        End Try
    End Sub

    Private Sub SetTestSubject()
        Try
            Dim dt As DataTable = GetTesting()
            Dim str As String = ""
            If (dt.Rows.Count > 0) Then
                For i As Int32 = 0 To dt.Rows.Count - 1
                    Dim dr As DataRow = dt.Rows(i)
                    str += " <p> <button class=""btn-block btn btn-larges"" id=" + dr("id").ToString + " onclick=""onConfirmTest('" + dr("id").ToString + "','" + dr("title").ToString + "','" + dr("target_percentage").ToString + "','" + dr("question_qty").ToString + "');return false;"" >" + dr("title").ToString + "</button></p>"
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