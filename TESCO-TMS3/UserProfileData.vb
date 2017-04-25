Public Class UserProfileData

    Dim _UserSessionID As Long = 0
    Dim _UserID As String = ""
    Dim _UserName As String = ""
    Dim _FullName As String = ""
    Dim _Token As String = ""
    Dim _TokenStr As String = ""
    Dim _CourseComplete As Integer = 0
    Dim _CourseTotal As Integer = 0
    Dim _TestingAttempt As Integer = 0
    Dim _TestingComplete As Integer = 0
    Dim _TestingTotal As Integer = 0
    Dim _UserMassage As DataTable
    Dim _UserFormat As DataTable
    Dim _UserFunction As DataTable
    Dim _UserDepartment As DataTable
    Dim _UserCourse As DataTable
    Dim _UserCourseFile As DataTable
    Dim _TestSubject As DataTable
    Dim _TestQuestion As DataTable


    Public Property UserMassage As DataTable
        Get
            Return _UserMassage
        End Get
        Set(value As DataTable)
            _UserMassage = value
        End Set
    End Property

    Public Property UserFormat As DataTable
        Get
            Return _UserFormat
        End Get
        Set(value As DataTable)
            _UserFormat = value
        End Set
    End Property
    Public Property UserFunction As DataTable
        Get
            Return _UserFunction
        End Get
        Set(value As DataTable)
            _UserFunction = value
        End Set
    End Property
    Public Property UserDepartment As DataTable
        Get
            Return _UserDepartment
        End Get
        Set(value As DataTable)
            _UserDepartment = value
        End Set
    End Property

    Public Property UserCourse As DataTable
        Get
            Return _UserCourse
        End Get
        Set(value As DataTable)
            _UserCourse = value
        End Set
    End Property

    Public Property UserCourseFile As DataTable
        Get
            Return _UserCourseFile
        End Get
        Set(value As DataTable)
            _UserCourseFile = value
        End Set
    End Property

    Public Property TestSubject As DataTable
        Get
            Return _TestSubject
        End Get
        Set(value As DataTable)
            _TestSubject = value
        End Set
    End Property

    Public Property TestQuestion As DataTable
        Get
            Return _TestQuestion
        End Get
        Set(value As DataTable)
            _TestQuestion = value
        End Set
    End Property

    Public Property UserSessionID As Long
        Get
            Return _UserSessionID
        End Get
        Set(value As Long)
            _UserSessionID = value
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
End Class
