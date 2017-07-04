Imports System.Data.SqlClient
Imports LinqDB.ConnectDB
Public Class frmSelectCourse
    Inherits System.Web.UI.Page

#Region "Declare & Valiable"
    ' Public myUser As User
    Protected ReadOnly Property UserData As UserProfileData
        Get
            Return Session("UserData")
        End Get
    End Property

    Public ReadOnly Property User_Department_id As String
        Get
            Return Page.Request.QueryString("id") & ""
        End Get
    End Property

    Public ReadOnly Property Department_title As String
        Get
            Return Page.Request.QueryString("title") & ""
        End Get
    End Property

    Public ReadOnly Property color As String
        Get
            Return "#" + Page.Request.QueryString("color")
        End Get
    End Property
#End Region

#Region "Initail"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Me.lblTitle.Text = "<h3>&nbsp>&nbsp"
            lblTitle.Text += " <a onClick=""return CreateTransLog('" & UserData.LoginHistoryID & "','กลับหน้าจอเลือก Format')"" href=""frmSelectFormat.aspx""><font color=""#019b79"">" + Session("backpathname1") + "&nbsp>&nbsp</font></a>"
            lblTitle.Text += " <a onClick=""return CreateTransLog('" & UserData.LoginHistoryID & "','กลับหน้าจอเลือก Function')"" href=" + Session("backpath2") + "><font color=""#019b79"">" + Session("backpathname2") + "&nbsp>&nbsp</font></a>"
            lblTitle.Text += " <a onClick=""return CreateTransLog('" & UserData.LoginHistoryID & "','กลับหน้าจอเลือก Department')"" href=" + Session("backpath3") + "><font color=""#019b79"">" + Session("backpathname3") + "&nbsp>&nbsp</font></a><font color=""#019b79"">" + Department_title + "</font></h3>"
            'SetDepartment()
            DisplayCourseList()

            LogFileBL.LogTrans(UserData.LoginHistoryID, "แสดงหน้าจอเลือก Course")
        End If

    End Sub


    Private Sub DisplayCourseList()

        Dim strURlImage As String = Request.Url.GetLeftPart(UriPartial.Authority) + Request.ApplicationPath & "Assets/PC/icon_course_book.png"

        Dim sql As String = "select c.*, cr.is_finished pre_course_finish "
        sql += " from tb_user_course c"
        sql += " left join tb_user_course cr on cr.course_id=c.prerequisite_course_id and cr.user_id=@_USER_ID"
        sql += " where 1=1 And c.tb_user_department_id=@_USER_DEPARTMENT_ID"
        sql += " and c.user_id=@_USER_ID"
        sql += " order by c.course_title"
        Dim p(2) As SqlParameter
        p(0) = SqlDB.SetBigInt("@_USER_DEPARTMENT_ID", User_Department_id)
        p(1) = SqlDB.SetBigInt("@_USER_ID", UserData.UserID)
        Dim dt As DataTable = SqlDB.ExecuteTable(sql, p)

        Dim strMain As String = "<ul class=""tiles"">"
        For Each dr As DataRowView In dt.DefaultView
            'ถ้าเป็น Course ที่มี Prerequisite ให้ตรวจสอบว่า Course ที่เป็น Prerequisite นั้นได้เรียนจบแล้วหรือไม่
            Dim IsShow As Boolean = False
            If Convert.ToInt64(dr("prerequisite_course_id")) > 0 Then
                If Convert.IsDBNull(dr("pre_course_finish")) = False Then
                    If dr("pre_course_finish") = "Y" Then
                        IsShow = True
                    End If
                End If
            Else
                IsShow = True
            End If

            If IsShow = True Then
                strMain += " <li  onclick=""ShowPopup('" + dr("id").ToString + "','" + dr("course_title").ToString + "','" + dr("course_title").ToString + "','" & UserData.UserSessionID & "');"" id=" + dr("id").ToString
                strMain += " style=""background-image:url('Assets/PC/icon_course_book.png');background-size: 140px auto;background-repeat: no-repeat;height:150px;margin:8px 12px 0 8px;"">"
                strMain += "    <a href=""#"">"
                strMain += "        <span class=""text-center"" style=""font-size:20px;padding-top:25px;padding-left:15px;padding-right:15px;"" >" + dr("course_title").ToString + "</span>"
                strMain += "    </a>"
                strMain += " </li>"
            Else
                strMain += " <li id=" + dr("id").ToString
                strMain += " style=""background-image:url('Assets/PC/icon_course_book.png');background-size: 140px auto;background-repeat: no-repeat;height:150px;margin:8px 12px 0 8px;"">"
                strMain += "    <a href=""#"" style='cursor:default' >"
                strMain += "        <span class=""text-center"" style=""font-size:20px;padding-top:25px;padding-left:15px;padding-right:15px;"" >" + dr("course_title").ToString + "</span>"
                strMain += "    </a>"
                strMain += " </li>"
            End If


        Next
        strMain += "</ul>"
        lblMain.Text = strMain
        dt.Dispose()
    End Sub

    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        LogFileBL.LogTrans(UserData.LoginHistoryID, "คลิกปุ่ม Back")
        Dim strurl As String = Session("backpath3")
        Response.Redirect(strurl)
    End Sub
#End Region

End Class