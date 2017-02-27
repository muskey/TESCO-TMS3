Imports LinqDB.ConnectDB

Namespace GenerateLinqDB
    Public Class Constant

        Partial Public Class FieldName
            Public Const ID As String = "ID"
            Public Const CREATE_BY As String = "created_by"
            Public Const CREATE_DATE As String = "created_date"
            Public Const UPDATE_BY As String = "updated_by"
            Public Const UPDATE_DATE As String = "updated_date"
            Public Const MSREPL_TRAN_VERSION As String = "msrepl_tran_version"
        End Class

        Partial Public Class DatabaseType
            Public Const SQL As String = "SQL"
            Public Const Oracle As String = "Oracle"
        End Class

        Partial Public Class GenerateConstant
            Private _DataSource As String = "แหล่งข้อมูล"
            Private _DatabaseName As String = "ชื่อฐานข้อมูล"
            Private _UserID As String = "ชื่อผู้ใช้ฐานข้อมูล"
            Private _Password As String = "รหัสผ่าน"
            Private _TableName As String = "ชื่อตารางหรือวิว"
            Private _ProjectCode As String = "รหัสโครงการ"
            Private _ClassName As String = "ชื่อคลาส"
            Private _NameSpace As String = "เนมสเปช"

            Private _DataSourceWaterMarkText As String = "ระบุแหล่งข้อมูล"
            Private _DatabaseWaterMarkText As String = "ระบุฐานข้อมูล (สำหรับ SQL Server)"
            Private _UserIDWaterMarkText As String = "ระบุชื่อผู้ใช้ฐานข้อมูล"
            Private _PasswordWaterMarkText As String = "ระบุรหัสผ่าน"
            Private _TableNameWaterMarkText As String = "ระบุชื่อตารางหรือวิว"
            Private _ProjectCodeWaterMarkText As String = "ระบุรหัสโครงการ"
            Private _NameSpaceWaterMarkText As String = "ระบุชื่อเนมสเปช"
            Private _ClassWaterMarkText As String = "ระบุชื่อคลาส"

            Public ReadOnly Property DataSourceRequire() As String
                Get
                    Return Format(MessageResources.MSGEI001, _DataSource)
                End Get
            End Property
            Public ReadOnly Property DatabaseNameRequire() As String
                Get
                    Return Format(MessageResources.MSGEI001, _DatabaseName)
                End Get
            End Property
            Public ReadOnly Property UserIDRequire() As String
                Get
                    Return Format(MessageResources.MSGEI001, _UserID)
                End Get
            End Property
            Public ReadOnly Property PasswordRequire() As String
                Get
                    Return Format(MessageResources.MSGEI001, _Password)
                End Get
            End Property
            Public ReadOnly Property TableNameRequire() As String
                Get
                    Return Format(MessageResources.MSGEI001, _TableName)
                End Get
            End Property
            Public ReadOnly Property ProjectCodeRequire() As String
                Get
                    Return Format(MessageResources.MSGEI001, _ProjectCode)
                End Get
            End Property
            Public ReadOnly Property ClassNameRequire() As String
                Get
                    Return Format(MessageResources.MSGEI001, _ClassName)
                End Get
            End Property
            Public ReadOnly Property NameSpaceRequire() As String
                Get
                    Return Format(MessageResources.MSGEI001, _NameSpace)
                End Get
            End Property
            Public ReadOnly Property DataSourcesWaterMarkText() As String
                Get
                    Return _DataSourceWaterMarkText
                End Get
            End Property
            Public ReadOnly Property DatabaseNameWaterMarkText() As String
                Get
                    Return _DatabaseWaterMarkText
                End Get
            End Property
            Public ReadOnly Property UserIDWaterMarkText() As String
                Get
                    Return _UserIDWaterMarkText
                End Get
            End Property
            Public ReadOnly Property PasswordWaterMarkText() As String
                Get
                    Return _PasswordWaterMarkText
                End Get
            End Property
            Public ReadOnly Property TableNameWaterMarkText() As String
                Get
                    Return _TableNameWaterMarkText
                End Get
            End Property
            Public ReadOnly Property ProjectCodeWaterMarkText() As String
                Get
                    Return _ProjectCodeWaterMarkText
                End Get
            End Property

            Public ReadOnly Property NameSpaceWaterMarkText() As String
                Get
                    Return _NameSpaceWaterMarkText
                End Get
            End Property
            Public ReadOnly Property ClassNameWaterMarkText() As String
                Get
                    Return _ClassWaterMarkText
                End Get
            End Property

        End Class

        Public Shared Function GetFullDate() As String
            Dim month As String = ""
            Select Case DateTime.Now.Month
                Case 1
                    month = "January"
                Case 2
                    month = "Febuary"
                Case 3
                    month = "March"
                Case 4
                    month = "April"
                Case 5
                    month = "May"
                Case 6
                    month = "June"
                Case 7
                    month = "July"
                Case 8
                    month = "August"
                Case 9
                    month = "September"
                Case 10
                    month = "October"
                Case 11
                    month = "November"
                Case 12
                    month = "December"
            End Select
            Return month & ", " & DateTime.Now.Day.ToString() & " " & DateTime.Now.Year.ToString()
        End Function
    End Class

End Namespace

