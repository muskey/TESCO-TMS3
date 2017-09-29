Imports System.Data.SqlClient
Imports LinqDB.ConnectDB

Public Class UserProfileData

    Dim _UserSessionID As Long = 0
    Dim _LoginHistoryID As Long = 0
    Dim _UserID As String = ""
    Dim _UserName As String = ""
    Dim _FullName As String = ""
    Dim _Token As String = ""
    Dim _TokenStr As String = ""
    Dim _CurrentClassID As Long = 0
    Dim _CourseComplete As Integer = 0
    Dim _CourseTotal As Integer = 0
    Dim _TestingAttempt As Integer = 0
    Dim _TestingComplete As Integer = 0
    Dim _TestingTotal As Integer = 0



    Public Property UserSessionID As Long
        Get
            Return _UserSessionID
        End Get
        Set(value As Long)
            _UserSessionID = value
        End Set
    End Property
    Public Property LoginHistoryID As Long
        Get
            Return _LoginHistoryID
        End Get
        Set(value As Long)
            _LoginHistoryID = value
        End Set
    End Property

    Public Property UserID As String
        Get
            Return _UserID.Trim
        End Get
        Set(value As String)
            _UserID = value
        End Set
    End Property
    Public Property UserName As String
        Get
            Return _UserName.Trim
        End Get
        Set(value As String)
            _UserName = value
        End Set
    End Property
    Public Property FullName As String
        Get
            Return _FullName.Trim
        End Get
        Set(value As String)
            _FullName = value
        End Set
    End Property

    Public Property Token As String
        Get
            Return _Token.Trim
        End Get
        Set(value As String)
            _Token = value
        End Set
    End Property
    Public Property TokenStr As String
        Get
            Return _TokenStr.Trim
        End Get
        Set(value As String)
            _TokenStr = value
        End Set
    End Property
    Public Property CurrentClassID As Long
        Get
            Return _CurrentClassID
        End Get
        Set(value As Long)
            _CurrentClassID = value
        End Set
    End Property
    Public Property CourseComplete As Integer
        Get
            Return _CourseComplete
        End Get
        Set(value As Integer)
            _CourseComplete = value
        End Set
    End Property
    Public Property CourseTotal As Integer
        Get
            Return _CourseTotal
        End Get
        Set(value As Integer)
            _CourseTotal = value
        End Set
    End Property
    Public Property TestingAttempt As Integer
        Get
            Return _TestingAttempt
        End Get
        Set(value As Integer)
            _TestingAttempt = value
        End Set
    End Property
    Public Property TestingComplete As Integer
        Get
            Return _TestingComplete
        End Get
        Set(value As Integer)
            _TestingComplete = value
        End Set
    End Property

    Public Property TestingTotal As Integer
        Get
            Return _TestingTotal
        End Get
        Set(value As Integer)
            _TestingTotal = value
        End Set
    End Property

    Public Sub GetUserSessionData(LoginSessionID As Long)
        Dim sql As String = "select u.id,u.token,u.[user_id], u.username, u.first_name_eng,u.last_name_eng, "
        sql += " u.first_name_thai,u.last_name_thai,u.is_teacher, u.current_class_id,l.id login_history_id "
        sql += " from TB_USER_SESSION u "
        sql += " inner join TB_LOGIN_HISTORY l on u.token=l.token "
        sql += " where u.id=@_USER_SESSION_ID"

        Dim p(1) As SqlParameter
        p(0) = SqlDB.SetBigInt("@_USER_SESSION_ID", LoginSessionID)

        Dim dt As DataTable = SqlDB.ExecuteTable(sql, p)

        If dt.Rows.Count > 0 Then
            Dim dr As DataRow = dt.Rows(0)
            _UserSessionID = dr("id")
            _UserName = dr("username")
            _UserID = dr("user_id")
            _LoginHistoryID = dr("login_history_id")

            Dim FirstName As String = ""
            If Convert.IsDBNull(dr("first_name_thai")) = False Then
                FirstName = dr("first_name_thai")
            Else
                If Convert.IsDBNull(dr("first_name_eng")) = False Then FirstName = dr("first_name_eng")
            End If

            Dim LastName As String = ""
            If Convert.IsDBNull(dr("last_name_thai")) = False Then
                LastName = dr("last_name_thai")
            Else
                If Convert.IsDBNull(dr("last_name_eng")) = False Then LastName = dr("last_name_eng")
            End If

            _FullName = FirstName & " " & LastName
            _TokenStr = dr("token")
            _Token = "token=" & dr("token")
            _CurrentClassID = dr("current_class_id")
        End If
        dt.Dispose()

    End Sub

End Class
