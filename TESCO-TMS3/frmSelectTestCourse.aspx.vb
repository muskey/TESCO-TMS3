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

            str += " <h4 class=""group inner list-group-item-heading"">คุณเรียนจบหลักสูตรแล้วทั้งหมด <font color=""#019b79"">" + UserData.CourseTotal.ToString + "</font> หลักสูตร</h4>"
            str += " <p class=""group inner list-group-item-text"">จากทั้งหมด <font color=""#019b79"">" + UserData.CourseTotal.ToString + "</font> หลักสูตร</p>"
            str += " <h4 class=""group inner list-group-item-heading"">คุณได้ทำบททดสอบแล้วทั้งหมด <font color=""#019b79"">" + UserData.TestingAttempt.ToString + "</font> หลักสูตร</h4>"
            str += " <p class=""group inner list-group-item-text"">จากทั้งหมด <font color=""#019b79"">" + UserData.TestingComplete.ToString + "</font> หลักสูตร</p>"
            str += " <h4 class=""group inner list-group-item-heading"">คุณได้ทำบททดสอบผ่านแล้วทั้งหมด <font color=""#019b79"">" + UserData.TestingTotal.ToString + "</font> หลักสูตร</h4>"
            str += " <p class=""group inner list-group-item-text"">จากทั้งหมด <font color=""#019b79"">" + UserData.TestingTotal.ToString + "</font> หลักสูตร</p>"
            lblNEWS.Text = str
        Catch ex As Exception

        End Try
    End Sub

    Private Sub SetTestSubject()
        Try

            'Dim sql As String = " select * from TB_TESTING"
            Dim dt As DataTable = UserData.TestSubject 'GetSqlDataTable(sql)
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