Imports System.Data.SqlClient
Imports LinqDB.TABLE
Imports LinqDB.ConnectDB
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

            Dim lnq As New TbUserCourseLinqDB
            lnq.GetDataByPK(UserCourseID, Nothing)
            If lnq.ID > 0 Then
                lblCourseID.Text = lnq.COURSE_ID
                lblCourseName.Text = lnq.COURSE_TITLE
            End If
            lnq = Nothing

            'Default ชื่อคนที่ Login เลย
            txtCheckUser.Text = UserData.UserName
            btnAddUser_Click(Nothing, Nothing)
        End If
    End Sub

    Private Sub btnAddUser_Click(sender As Object, e As EventArgs) Handles btnAddUser.Click
        If txtCheckUser.Text.Trim = "" Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType, Guid.NewGuid().ToString(), "alert('กรุณาใส่รหัสพนักงาน');", True)
            Exit Sub
        End If

        Dim User() As String = GetStudentUser(UserData.Token, txtCheckUser.Text, lblCourseID.Text)
        If User(0) = "" Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType, Guid.NewGuid().ToString(), "alert('ไม่มีรหัสพนักงานนี้');", True)
            Exit Sub
        End If

        Dim dt As DataTable = GetUserList()
        dt.DefaultView.RowFilter = "user_id=" & User(0)
        If dt.DefaultView.Count > 0 Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType, Guid.NewGuid().ToString(), "alert('รหัสประจำตัวนี้ได้มีการเพิ่มเข้าไปแล้ว');", True)
            Exit Sub
        End If
        dt.DefaultView.RowFilter = ""

        Dim dr As DataRow = dt.NewRow
        dr("user_id") = User(0)
        dr("user_fullname") = User(1) & " " & User(2)
        dr("user_code") = User(3)   'รหัสพนักงาน

        dt.Rows.Add(dr)

        rptUserList.DataSource = dt
        rptUserList.DataBind()
        LogFileBL.LogTrans(UserData.LoginHistoryID, "เพิ่มรายชื่อนักเรียน" & User(1) & " " & User(2) & "   Username=" & txtCheckUser.Text)

        txtCheckUser.Text = ""
    End Sub



    Private Sub rptUserList_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles rptUserList.ItemDataBound
        If e.Item.ItemType <> ListItemType.Item And e.Item.ItemType <> ListItemType.AlternatingItem Then Exit Sub

        Dim lblUserID As Label = e.Item.FindControl("lblUserID")
        Dim lblUserCode As Label = e.Item.FindControl("lblUserCode")
        Dim lblUserFullname As Label = e.Item.FindControl("lblUserFullname")

        lblUserID.Text = e.Item.DataItem("user_id")
        lblUserCode.Text = e.Item.DataItem("user_code")
        lblUserFullname.Text = e.Item.DataItem("user_fullname").ToString.Trim

    End Sub

    Private Sub rptUserList_ItemCommand(source As Object, e As RepeaterCommandEventArgs) Handles rptUserList.ItemCommand
        Dim lblUserCode As Label = e.Item.FindControl("lblUserCode")
        Select Case e.CommandName
            Case "DELETE"
                Dim dt As DataTable = GetUserList()
                dt.Rows.RemoveAt(e.Item.ItemIndex)

                rptUserList.DataSource = dt
                rptUserList.DataBind()

                LogFileBL.LogTrans(UserData.LoginHistoryID, "ลบ User " & lblUserCode.Text)
        End Select
    End Sub

    Private Function GetUserList() As DataTable
        Dim dt As New DataTable
        dt.Columns.Add("user_id")
        dt.Columns.Add("user_code")
        dt.Columns.Add("user_fullname")

        For Each itm As RepeaterItem In rptUserList.Items
            Dim lblUserID As Label = itm.FindControl("lblUserID")
            Dim lblUserCode As Label = itm.FindControl("lblUserCode")
            Dim lblUserFullname As Label = itm.FindControl("lblUserFullname")

            Dim dr As DataRow = dt.NewRow

            dr("user_id") = lblUserID.Text
            dr("user_code") = lblUserCode.Text
            dr("user_fullname") = lblUserFullname.Text
            dt.Rows.Add(dr)
        Next

        Return dt
    End Function

    Private Sub btnStart_Click(sender As Object, e As EventArgs) Handles btnStart.Click
        LogFileBL.LogTrans(UserData.LoginHistoryID, "คลิกปุ่มเริ่มเรียน บทเรียน " & lblCourseName.Text)
        Dim dt As DataTable = GetUserList()
        If dt.Rows.Count = 0 Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType, Guid.NewGuid().ToString(), "alert('กรุณาเช็คชื่อผู้เข้าเรียน');", True)
            Exit Sub
        End If

        Dim stdID As String = ""
        For Each drUser As DataRow In dt.Rows
            If stdID = "" Then
                stdID = drUser("user_id")
            Else
                stdID += "," & drUser("user_id")
            End If
        Next

        Dim ClassID As Long = CreateClass(UserData.Token, stdID, lblCourseID.Text, UserData.UserID)
        If ClassID > 0 Then
            LogFileBL.LogTrans(UserData.LoginHistoryID, "สร้าง Class สำเร็จ ClassID= " & ClassID & " บทเรียน" & lblCourseName.Text)

            Dim lnq As New TbUserSessionLinqDB
            lnq.GetDataByPK(UserData.UserSessionID, Nothing)
            If lnq.ID > 0 Then
                lnq.CURRENT_CLASS_ID = ClassID

                Dim trans As New TransactionDB
                If lnq.UpdateData(UserData.UserName, trans.Trans).IsSuccess = True Then
                    trans.CommitTransaction()
                    LogFileBL.LogTrans(UserData.LoginHistoryID, "เริ่มเรียนบทเรียน " & lblCourseName.Text)

                    Response.Redirect("frmDisplayCenter.aspx?id=" + UserCourseID + "&title=" + lblCourseName.Text)
                Else
                    trans.RollbackTransaction()

                    ScriptManager.RegisterStartupScript(Me, Me.GetType, Guid.NewGuid().ToString(), "alert('สร้าง Class ไม่สำเร็จ');", True)
                    LogFileBL.LogError(UserData.LoginHistoryID, lnq.ErrorMessage)
                End If
            End If
            lnq = Nothing
        Else
            ScriptManager.RegisterStartupScript(Me, Me.GetType, Guid.NewGuid().ToString(), "alert('ไม่สามารถติดต่อ Backend Server ได้');", True)
            LogFileBL.LogError(UserData.LoginHistoryID, "สร้าง Class ไม่สำเร็จ")
        End If

    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        LogFileBL.LogTrans(UserData.LoginHistoryID, "คลิกปุ่ม Close")

        'หาข้อมูลก่อนส่งกลับ
        Dim sql As String = ""
        sql += " Select dp.id,dp.department_title , f.function_cover_color " & Environment.NewLine
        sql += " from TB_USER_COURSE c " & Environment.NewLine
        sql += " inner join TB_USER_DEPARTMENT dp on dp.id=c.tb_user_department_id " & Environment.NewLine
        sql += " inner join TB_USER_FUNCTION f on f.id=dp.tb_user_function_id "
        sql += " where c.id=@_USER_COURSE_ID"

        Dim p(1) As SqlParameter
        p(0) = SqlDB.SetBigInt("@_USER_COURSE_ID", UserCourseID)

        Dim dt As DataTable = SqlDB.ExecuteTable(sql, p)
        If dt.Rows.Count > 0 Then
            Response.Redirect("frmSelectCourse.aspx?id=" & dt.Rows(0)("id") & "&title=" & dt.Rows(0)("department_title") & "&color=" & dt.Rows(0)("function_cover_color").ToString.Replace("#", ""))
        Else
            Response.Redirect("frmSelectFormat.aspx?rnd=" & DateTime.Now.Millisecond)
        End If
    End Sub


End Class