﻿Public Class UserProfileData

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
