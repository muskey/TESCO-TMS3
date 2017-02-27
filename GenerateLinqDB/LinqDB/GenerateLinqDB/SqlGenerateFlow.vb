Imports System
Imports System.Collections.Generic
Imports System.Data
Imports System.Text
Imports LinqDB.GenerateLinqDB

Namespace GenerateLinqDB
    Public Class SqlGenerateFlow
        Inherits Flow.BaseGenerateFlow



        Private ReadOnly Property DALObj() As SqlGenerateDAL
            Get
                If _dal Is Nothing Then
                    _dal = New SqlGenerateDAL()
                End If
                Return _dal
            End Get

        End Property

        Private Sub SetData(ByVal Data As GenerateData)
            DALObj.DataSource = Data.DataSource
            DALObj.DataBaseName = Data.DataBaseName
            DALObj.UserID = Data.UserID
            DALObj.Password = Data.Password
            DALObj.TableName = Data.TableName
            DALObj.DatabaseType = Data.DatabaseType
            DALObj.DatabaseClass = Data.DatabaseClass
            _tableName = DALObj.TableName
            _IsView = DALObj.IsView()
            _databaseType = DALObj.DatabaseType

            _columnTable = DALObj.GetTableColumn()
            _className = Data.ClassName
            _objType = Data.ObjType
            _paradb = Data.ParaDB
            _linqdb = Data.LinqDB
            If _IsView = False Then
                _pkColumnTable = DALObj.GetPKColumn()
                _uniqueColumnTable = DALObj.GetUQColumn()
                _childTableKey = DALObj.GetChildTableKey()
            End If
        End Sub
        Private Sub SetConnDesc(ByVal Data As GenerateData)
            DALObj.DataSource = Data.DataSource
            DALObj.DataBaseName = Data.DataBaseName
            DALObj.UserID = Data.UserID
            DALObj.Password = Data.Password
            DALObj.DatabaseType = Data.DatabaseType
            _databaseType = DALObj.DatabaseType
        End Sub


        Public Function GenerateCodeDAL(ByVal Data As GenerateData) As String
            SetData(Data)
            Return GenerateLinq(Data)
        End Function

        Public Function GenerateCodeData(ByVal Data As GenerateData) As String
            SetData(Data)
            Return GeneratePara(Data)
        End Function

        Public Function GetTableList(ByVal Data As GenerateData) As DataTable
            SetConnDesc(Data)
            Return DALObj.GetTableList()
        End Function

        Public Function GetAllTable(ByVal Data As GenerateData) As DataTable
            SetConnDesc(Data)
            Return DALObj.GetAllTable()
        End Function

        Public Function GetTable(ByVal TableName As String, ByVal data As GenerateData) As DataTable
            SetConnDesc(data)
            Return DALObj.GetTable(TableName)
        End Function

    End Class
End Namespace

