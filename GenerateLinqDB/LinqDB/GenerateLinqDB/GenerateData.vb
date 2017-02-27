Namespace GenerateLinqDB
    Public Class GenerateData
        Dim _DataSource As String = ""
        Dim _DatabaseName As String = ""
        Dim _UserID As String = ""
        Dim _Password As String = ""
        Dim _TableName As String = ""
        Dim _NameSpace As String = ""
        Dim _ClassName As String = ""
        Dim _UserHostName As String = ""
        Dim _ProjectName As String = ""
        Dim _DatabaseType As String = ""
        Dim _DatabaseClass As String = ""
        Dim _Language As String = ""
        Dim _ObjType As String = ""
        Dim _ParaDB As String = ""
        Dim _LinqDB As String = ""


        Public Property DataSource() As String
            Get
                Return _DataSource
            End Get
            Set(ByVal value As String)
                _DataSource = value
            End Set
        End Property
        Public Property DataBaseName() As String
            Get
                Return _DatabaseName
            End Get
            Set(ByVal value As String)
                _DatabaseName = value
            End Set
        End Property
        Public Property UserID() As String
            Get
                Return _UserID
            End Get
            Set(ByVal value As String)
                _UserID = value
            End Set
        End Property
        Public Property Password() As String
            Get
                Return _Password
            End Get
            Set(ByVal value As String)
                _Password = value
            End Set
        End Property
        Public Property TableName() As String
            Get
                Return _TableName
            End Get
            Set(ByVal value As String)
                _TableName = value
            End Set
        End Property
        Public Property NameSpaces() As String
            Get
                Return _NameSpace
            End Get
            Set(ByVal value As String)
                _NameSpace = value
            End Set
        End Property

        Public Property ClassName() As String
            Get
                Return _ClassName
            End Get
            Set(ByVal value As String)
                _ClassName = value
            End Set
        End Property
        Public Property UserHostName() As String
            Get
                Return _UserHostName
            End Get
            Set(ByVal value As String)
                _UserHostName = value
            End Set
        End Property
        Public Property ProjectName() As String
            Get
                Return _ProjectName
            End Get
            Set(ByVal value As String)
                _ProjectName = value
            End Set
        End Property
        Public Property DatabaseType() As String
            Get
                Return _DatabaseType
            End Get
            Set(ByVal value As String)
                _DatabaseType = value
            End Set
        End Property
        Public Property DatabaseClass() As String
            Get
                Return _DatabaseClass
            End Get
            Set(ByVal value As String)
                _DatabaseClass = value
            End Set
        End Property

        Public Property Language() As String
            Get
                Return _Language
            End Get
            Set(ByVal value As String)
                _Language = value
            End Set
        End Property

        Public Property ObjType() As String
            Get
                Return _ObjType
            End Get
            Set(ByVal value As String)
                _ObjType = value
            End Set
        End Property
        Public Property ParaDB() As String
            Get
                Return _ParaDB
            End Get
            Set(ByVal value As String)
                _ParaDB = value
            End Set
        End Property
        Public Property LinqDB() As String
            Get
                Return _LinqDB
            End Get
            Set(ByVal value As String)
                _LinqDB = value
            End Set
        End Property
    End Class
End Namespace

