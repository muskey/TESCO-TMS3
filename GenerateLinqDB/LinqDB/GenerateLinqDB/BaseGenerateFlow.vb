Imports System
Imports System.Collections.Generic
Imports System.Data
Imports System.Text
Imports LinqDB.GenerateLinqDB

Namespace Flow
    Public Class BaseGenerateFlow
        Private primaryKeyFiels As String = ""
        Private primaryKeyType As String = ""
        Protected _IsView As Boolean = False
        Protected _databaseType As String = ""
        Protected _columnTable As New DataTable
        Protected _pkColumnTable As New DataTable
        Protected _uniqueColumnTable As New DataTable
        Protected _tableName As String = ""
        Protected _className As String = ""
        Protected _objType As String = ""
        Protected _childTableKey As New DataTable
        Protected _paradb As String = ""
        Protected _linqdb As String = ""

        Protected _dal As SqlGenerateDAL

        Public Function GenerateLinq(ByVal Data As GenerateData) As String
            Dim ret As String = ""
            ret += "Imports System"
            ret += vbNewLine & "Imports System.Data "
            ret += vbNewLine & "Imports System.Data." & Data.DatabaseType & "Client"
            ret += vbNewLine & "Imports System.Data.Linq.Mapping "
            ret += vbNewLine & "Imports System.Linq "
            ret += vbNewLine & "Imports DB = " & Data.ObjType & ".ConnectDB." & Data.DatabaseClass
            ret += vbNewLine & "Imports " & Data.ObjType & ".ConnectDB"

            ret += vbNewLine & ""
            ret += vbNewLine & "Namespace " & Data.NameSpaces.Replace(Data.ObjType & ".", "")
            ret += vbNewLine & "    'Represents a transaction for " & Data.TableName & " " & IIf(_IsView = True, "view", "table") & " " & Data.ObjType & "."
            ret += vbNewLine & "    '[Create by " & Data.UserHostName & " on " & Constant.GetFullDate() & "]"
            ret += vbNewLine & "    Public Class " & Data.ClassName & Data.ObjType
            ret += vbNewLine & "        Public sub " & Data.ClassName & Data.ObjType & "()"
            ret += vbNewLine
            ret += vbNewLine & "        End Sub "
            ret += vbNewLine & "        ' " & Data.TableName
            ret += vbNewLine & "        Const _" & IIf(_IsView = True, "view", "table") & "Name As String = " & Chr(34) & Data.TableName & Chr(34)
            'If _IsView = False Then ret += vbNewLine & "        Dim _deletedRow As Int16 = 0"
            ret += vbNewLine & GenerateCommonVariables()
            ret += vbNewLine
            ret += vbNewLine & GenerateFieldVariables()
            ret += vbNewLine
            ret += vbNewLine & GenerateClearVariables()
            ret += vbNewLine
            ret += vbNewLine & GenerateAppendDataMethod()
            ret += vbNewLine & GeneratePrivateMethod()
            ret += vbNewLine & GenerateSQL()
            ret += vbNewLine
            'ret += vbNewLine & GenerateChildTable(False)
            ret += vbNewLine & "    End Class"
            ret += vbNewLine & "End Namespace"

            Return ret
        End Function

        Public Function GeneratePara(ByVal Data As GenerateData) As String
            Dim ret As String = ""
            ret += vbNewLine & "Imports System.Data.Linq "
            ret += vbNewLine & "Imports System.Data.Linq.Mapping "
            ret += vbNewLine & "Imports System.Linq "
            ret += vbNewLine & "Imports System.Linq.Expressions "
            ret += vbNewLine
            ret += vbNewLine & "Namespace " & Data.NameSpaces.Replace("Para.", "")
            ret += vbNewLine & "    'Represents a transaction for " & Data.TableName & " " & IIf(_IsView = True, "view", "table") & " Parameter."
            ret += vbNewLine & "    '[Create by " & Data.UserHostName & " on " & Constant.GetFullDate() & "]"
            ret += vbNewLine
            ret += vbNewLine & "    <Table(Name:=" & Chr(34) & Data.TableName & Chr(34) & ")>  _"
            ret += vbNewLine & "    Public Class " & Data.ClassName & Data.ParaDB
            ret += vbNewLine & GenerateFieldVariables()
            ret += vbNewLine
            'ret += vbNewLine & GenerateChildTable(True)
            ret += vbNewLine & "    End Class"
            ret += vbNewLine & "End Namespace"
            Return ret
        End Function
        
        Private Function GenerateCommonVariables() As String
            Dim ret As String = ""
            ret += vbNewLine & "        'Set Common Property"
            ret += vbNewLine & "        Dim _error As String = " & Chr(34) & Chr(34)
            ret += vbNewLine & "        Dim _information As String = " & Chr(34) & Chr(34)
            ret += vbNewLine & "        Dim _haveData As Boolean = False"
            ret += vbNewLine & ""
            ret += vbNewLine & "        Public ReadOnly Property " & IIf(_IsView = False, "Table", "View") & "Name As String"
            ret += vbNewLine & "            Get"
            ret += vbNewLine & "                Return _" & IIf(_IsView = False, "table", "view") & "Name"
            ret += vbNewLine & "            End Get"
            ret += vbNewLine & "        End Property"
            ret += vbNewLine & "        Public ReadOnly Property ErrorMessage As String"
            ret += vbNewLine & "            Get"
            ret += vbNewLine & "                Return _error"
            ret += vbNewLine & "            End Get"
            ret += vbNewLine & "        End Property"
            ret += vbNewLine & "        Public ReadOnly Property InfoMessage As String"
            ret += vbNewLine & "            Get"
            ret += vbNewLine & "                Return _information"
            ret += vbNewLine & "            End Get"
            ret += vbNewLine & "        End Property"
            'ret += vbNewLine & "        Public ReadOnly Property HaveData As Boolean"
            'ret += vbNewLine & "            Get"
            'ret += vbNewLine & "                Return _haveData"
            'ret += vbNewLine & "            End Get"
            'ret += vbNewLine & "        End Property"

            Return ret
        End Function

        Private Function GenerateFieldVariables() As String
            Dim ret As String = ""
            Dim retField As String = ""
            Dim retProp As String = ""

            retField += vbNewLine & "        'Generate Field List"

            For Each dRow In _columnTable.Rows
                Dim vTypeName As String = UCase(dRow("TYPE_NAME").ToString())
                Dim retFieldType As String = ""
                Dim fieldTypeName As String = ""
                Dim colName As String = "_" & UCase(dRow("COLUMN_NAME").ToString())
                Dim FieldIsNull As String = dRow("NULLABLE").ToString()

                If vTypeName = "VARCHAR" Then
                    retFieldType = IIf(FieldIsNull = "1", GetFieldType(dRow), "String")
                    fieldTypeName = "VarChar(" & dRow("LENGTH") & ")"
                    retField += vbNewLine & "        Dim " & colName & " As " & retFieldType & " = " & Chr(34) & GetFieldDefault(dRow) & Chr(34)
                ElseIf vTypeName = "VARCHAR2" Then
                    retFieldType = IIf(FieldIsNull = "1", GetFieldType(dRow), "String")
                    fieldTypeName = "VarChar(" & dRow("LENGTH") & ")"
                    retField += vbNewLine & "        Dim " & colName & " As " & retFieldType & " = " & Chr(34) & GetFieldDefault(dRow) & Chr(34)
                ElseIf vTypeName = "CHAR" Then
                    retFieldType = IIf(FieldIsNull = "1", GetFieldType(dRow), "Char")
                    fieldTypeName = "Char(" & dRow("LENGTH") & ")"
                    retField += vbNewLine & "        Dim " & colName & " As " & retFieldType & " = " & Chr(34) & GetFieldDefault(dRow) & Chr(34)
                ElseIf vTypeName = "NVARCHAR" Then
                    retFieldType = IIf(FieldIsNull = "1", GetFieldType(dRow), "String")
                    fieldTypeName = "NVarChar(" & dRow("LENGTH") & ")"
                    retField += vbNewLine & "        Dim " & colName & " As " & retFieldType & " = " & Chr(34) & GetFieldDefault(dRow) & Chr(34)
                ElseIf vTypeName = "NCHAR" Then
                    retFieldType = IIf(FieldIsNull = "1", GetFieldType(dRow), "String")
                    fieldTypeName = "NChar(" & dRow("LENGTH") & ")"
                    retField += vbNewLine & "        Dim " & colName & " As " & retFieldType & " = " & Chr(34) & GetFieldDefault(dRow) & Chr(34)
                ElseIf vTypeName = "TEXT" Then
                    retFieldType = IIf(FieldIsNull = "1", GetFieldType(dRow), "String")
                    fieldTypeName = "Text"
                    retField += vbNewLine & "        Dim " & colName & " As " & retFieldType & " = " & Chr(34) & GetFieldDefault(dRow) & Chr(34)
                ElseIf vTypeName = "FLOAT" Then
                    retFieldType = IIf(FieldIsNull = "1", GetFieldType(dRow), "Double")
                    fieldTypeName = "Float"
                    retField += vbNewLine & "        Dim " & colName & " As " & retFieldType & IIf(FieldIsNull = "1", "", " = 0")
                ElseIf vTypeName = "DOUBLE" Then
                    retFieldType = IIf(FieldIsNull = "1", GetFieldType(dRow), "Double")
                    fieldTypeName = "Double"
                    retField += vbNewLine & "        Dim " & colName & " As " & retFieldType & IIf(FieldIsNull = "1", "", " = 0")
                ElseIf vTypeName = "DECIMAL" Then
                    retFieldType = IIf(FieldIsNull = "1", GetFieldType(dRow), "Double")
                    fieldTypeName = "Double"
                    retField += vbNewLine & "        Dim " & colName & " As " & retFieldType & IIf(FieldIsNull = "1", "", " = 0")
                ElseIf vTypeName = "BIGINT" Then
                    retFieldType = IIf(FieldIsNull = "1", GetFieldType(dRow), "Long")
                    fieldTypeName = "BigInt"
                    retField += vbNewLine & "        Dim " & colName & " As " & retFieldType & IIf(FieldIsNull = "1", "", " = 0")
                ElseIf vTypeName = "BIGINT IDENTITY" Then
                    retFieldType = IIf(FieldIsNull = "1", GetFieldType(dRow), "Long")
                    fieldTypeName = "BigInt"
                    retField += vbNewLine & "        Dim " & colName & " As " & retFieldType & IIf(FieldIsNull = "1", "", " = 0")
                ElseIf vTypeName = "INT" Then
                    retFieldType = IIf(FieldIsNull = "1", GetFieldType(dRow), "Long")
                    fieldTypeName = "Int"
                    retField += vbNewLine & "        Dim " & colName & " As " & retFieldType & IIf(FieldIsNull = "1", "", " = 0")
                ElseIf vTypeName = "INT IDENTITY" Then
                    retFieldType = IIf(FieldIsNull = "1", GetFieldType(dRow), "Long")
                    fieldTypeName = "Int"
                    retField += vbNewLine & "        Dim " & colName & " As " & retFieldType & IIf(FieldIsNull = "1", "", " = 0")
                ElseIf vTypeName = "SMALLINT" Then
                    retFieldType = IIf(FieldIsNull = "1", GetFieldType(dRow), "Long")
                    fieldTypeName = "SmallInt"
                    retField += vbNewLine & "        Dim " & colName & " As " & retFieldType & IIf(FieldIsNull = "1", "", " = 0")
                ElseIf vTypeName = "NUMBER" Then
                    retFieldType = IIf(FieldIsNull = "1", GetFieldType(dRow), "Double")
                    fieldTypeName = "Number"
                    retField += vbNewLine & "        Dim " & colName & " As " & retFieldType & IIf(FieldIsNull = "1", "", " = 0")
                ElseIf vTypeName = "NUMBERIC" Then
                    retFieldType = IIf(FieldIsNull = "1", GetFieldType(dRow), "Double")
                    fieldTypeName = "Numberic"
                    retField += vbNewLine & "        Dim " & colName & " As " & retFieldType & IIf(FieldIsNull = "1", "", " = 0")
                ElseIf vTypeName = "DATETIME" Then
                    retFieldType = IIf(FieldIsNull = "1", GetFieldType(dRow), "DateTime")
                    fieldTypeName = "DateTime"
                    retField += vbNewLine & "        Dim " & colName & " As " & retFieldType & " = New DateTime(1,1,1)"
                ElseIf vTypeName = "DATETIME2" Then
                    retFieldType = IIf(FieldIsNull = "1", GetFieldType(dRow), "DateTime")
                    fieldTypeName = "DateTime2"
                    retField += vbNewLine & "        Dim " & colName & " As " & retFieldType & " = New DateTime(1,1,1)"
                ElseIf vTypeName = "DATE" Then
                    retFieldType = IIf(FieldIsNull = "1", GetFieldType(dRow), "Date")
                    fieldTypeName = "Date"
                    retField += vbNewLine & "        Dim " & colName & " As " & retFieldType & " = New DateTime(1,1,1)"
                ElseIf vTypeName = "BIT" Then
                    retFieldType = IIf(FieldIsNull = "1", GetFieldType(dRow), "Boolean")
                    fieldTypeName = "Bit"
                    retField += vbNewLine & "        Dim " & colName & " As " & retFieldType & " = False"
                ElseIf vTypeName = "UNIQUEIDENTIFIER" Then  'uniqueidentifier
                    retFieldType = IIf(FieldIsNull = "1", GetFieldType(dRow), "String")
                    fieldTypeName = "UNIQUEIDENTIFIER"
                    retField += vbNewLine & "        Dim " & colName & " As " & retFieldType & " = " & Chr(34) & Chr(34)
                ElseIf vTypeName = "IMAGE" Then
                    retFieldType = IIf(FieldIsNull = "1", GetFieldType(dRow), "Byte()")
                    fieldTypeName = "IMAGE"
                    retField += vbNewLine & "        Dim " & colName & "() As Byte"
                End If

                retProp += vbNewLine & "        <Column(Storage:=" & Chr(34) & colName & Chr(34)
                retProp += ", DbType:=" & Chr(34) & fieldTypeName
                retProp += IIf(FieldIsNull = "0", " NOT NULL " & Chr(34) & ",CanBeNull:=false", Chr(34))
                If _IsView = False Then
                    GetIsPrimaryKey(dRow("COLUMN_NAME").ToString())
                End If
                retProp += ")>  _"


                'If UCase(dRow("COLUMN_NAME").ToString()) = Constant.FieldName.CREATE_BY Or UCase(dRow("COLUMN_NAME").ToString()) = Constant.FieldName.CREATE_ON Or UCase(dRow("COLUMN_NAME").ToString()) = Constant.FieldName.UPDATE_BY Or UCase(dRow("COLUMN_NAME").ToString()) = Constant.FieldName.UPDATE_ON Then

                'End If


                Select Case UCase(dRow("COLUMN_NAME").ToString())
                    Case Constant.FieldName.ID, Constant.FieldName.CREATE_BY, Constant.FieldName.CREATE_DATE, Constant.FieldName.UPDATE_BY, Constant.FieldName.UPDATE_DATE
                        If InStr(UCase(_objType), _objType) > 0 Then
                            retProp += vbNewLine & "        Public ReadOnly Property " & UCase(dRow("COLUMN_NAME").ToString()) & "() As " & retFieldType
                            retProp += vbNewLine & "            Get"
                            retProp += vbNewLine & "                Return " & colName
                            retProp += vbNewLine & "            End Get"
                            retProp += vbNewLine & "        End Property "
                        Else
                            retProp += vbNewLine & "        Public Property " & UCase(dRow("COLUMN_NAME").ToString()) & "() As " & retFieldType
                            retProp += vbNewLine & "            Get"
                            retProp += vbNewLine & "                Return " & colName
                            retProp += vbNewLine & "            End Get"
                            If vTypeName = "IMAGE" Then
                                retProp += vbNewLine & "            Set(ByVal value() As Byte)"
                            Else
                                retProp += vbNewLine & "            Set(ByVal value As " & retFieldType & ")"
                            End If
                            retProp += vbNewLine & "               " & colName & " = value"
                            retProp += vbNewLine & "            End Set"
                            retProp += vbNewLine & "        End Property "
                        End If
                    Case Else
                        retProp += vbNewLine & "        Public Property " & UCase(dRow("COLUMN_NAME").ToString()) & "() As " & retFieldType
                        retProp += vbNewLine & "            Get"
                        retProp += vbNewLine & "                Return " & colName
                        retProp += vbNewLine & "            End Get"
                        If vTypeName = "IMAGE" Then
                            retProp += vbNewLine & "            Set(ByVal value() As Byte)"
                        Else
                            retProp += vbNewLine & "            Set(ByVal value As " & retFieldType & ")"
                        End If
                        retProp += vbNewLine & "               " & colName & " = value"
                        retProp += vbNewLine & "            End Set"
                        retProp += vbNewLine & "        End Property "
                End Select
            Next

            ret = retField
            ret += vbNewLine
            ret += vbNewLine & "        'Generate Field Property "
            ret += retProp

            Return ret
        End Function

        Private Function GetIsPrimaryKey(ByVal columnName As String) As String
            Dim ret As String = ""
            If _pkColumnTable.Rows.Count > 0 Then
                Dim dRow As DataRow = _pkColumnTable.Rows(0)
                If columnName.ToUpper = dRow("COLUMN_NAME").ToString.ToUpper Then
                    ret = ", IsPrimaryKey:=true"
                End If
            End If

            Return ret
        End Function

        Private Function GetFieldDefault(ByVal dRow As DataRow) As String
            Dim ret As String = ""
            Dim vDfValue As String = ""
            Dim FieldIsNull As String = dRow("NULLABLE").ToString()
            If Convert.IsDBNull(dRow("COLUMN_DEF")) = False Then
                vDfValue = Replace(Replace(dRow("COLUMN_DEF").ToString, "('", ""), "')", "")
            End If
            Dim vTypeName As String = dRow("TYPE_NAME").ToString.ToUpper
            If vTypeName = "VARCHAR" Then
                ret = IIf(vDfValue.Trim = "", "", vDfValue)
            ElseIf vTypeName = "VARCHAR2" Then
                ret = IIf(vDfValue.Trim = "", "", vDfValue)
            ElseIf vTypeName = "CHAR" Then
                ret = IIf(vDfValue.Trim = "", "", vDfValue)
            ElseIf vTypeName = "NVARCHAR" Then
                ret = IIf(vDfValue.Trim = "", "", vDfValue)
            ElseIf vTypeName = "NCHAR" Then
                ret = IIf(vDfValue.Trim = "", "", vDfValue)
            ElseIf vTypeName = "TEXT" Then
                ret = IIf(vDfValue.Trim = "", "", vDfValue)
            ElseIf vTypeName = "FLOAT" Then
                vDfValue = Replace(Replace(vDfValue, "((", ""), "))", "")
                ret = IIf(vDfValue Is Nothing, IIf(FieldIsNull = "1", "Nothing", "0"), vDfValue)
            ElseIf vTypeName = "DOUBLE" Then
                vDfValue = Replace(Replace(vDfValue, "((", ""), "))", "")
                ret = IIf(vDfValue Is Nothing, IIf(FieldIsNull = "1", "Nothing", "0"), vDfValue)
            ElseIf vTypeName = "DECIMAL" Then
                vDfValue = Replace(Replace(vDfValue, "((", ""), "))", "")
                ret = IIf(vDfValue Is Nothing, IIf(FieldIsNull = "1", "Nothing", "0"), vDfValue)
            ElseIf vTypeName = "BIGINT" Then
                vDfValue = Replace(Replace(vDfValue, "((", ""), "))", "")
                ret = IIf(vDfValue Is Nothing, IIf(FieldIsNull = "1", "Nothing", "0"), vDfValue)
            ElseIf vTypeName = "BIGINT IDENTITY" Then
                ret = IIf(vDfValue.Trim = "", IIf(FieldIsNull = "1", "Nothing", "0"), vDfValue)
            ElseIf vTypeName = "INT" Then
                vDfValue = Replace(Replace(vDfValue, "((", ""), "))", "")
                ret = IIf(vDfValue Is Nothing, IIf(FieldIsNull = "1", "Nothing", "0"), vDfValue)
            ElseIf vTypeName = "INT IDENTITY" Then
                ret = IIf(vDfValue.Trim = "", IIf(FieldIsNull = "1", "Nothing", "0"), vDfValue)
            ElseIf vTypeName = "SMALLINT" Then
                vDfValue = Replace(Replace(vDfValue, "((", ""), "))", "")
                ret = IIf(vDfValue Is Nothing, IIf(FieldIsNull = "1", "Nothing", "0"), vDfValue)
            ElseIf vTypeName = "NUMBER" Then
                vDfValue = Replace(Replace(vDfValue, "((", ""), "))", "")
                ret = IIf(vDfValue Is Nothing, IIf(FieldIsNull = "1", "Nothing", "0"), vDfValue)
            ElseIf vTypeName = "NUMBERIC" Then
                vDfValue = Replace(Replace(vDfValue, "((", ""), "))", "")
                ret = IIf(vDfValue Is Nothing, IIf(FieldIsNull = "1", "Nothing", "0"), vDfValue)
            ElseIf vTypeName = "DATETIME" Then
                ret = "DateTime.Now"
            ElseIf vTypeName = "DATETIME2" Then
                ret = "DateTime.Now"
            ElseIf vTypeName = "DATE" Then
                ret = "DateTime.Now"
            ElseIf vTypeName = "BIT" Then
                ret = IIf(vDfValue.Trim = "", "false", vDfValue)
            ElseIf vTypeName = UCase("uniqueidentifier") Then
                ret = vDfValue
            ElseIf vTypeName = "IMAGE" Then
                ret = IIf(vDfValue.Trim = "", "DBNull.Value", vDfValue)
            End If

            Return ret
        End Function

        Private Function GetFieldType(ByVal dRow As DataRow) As String
            Dim ret As String = ""
            If (dRow("NULLABLE").ToString = "1") Then
                Dim vTypeName As String = dRow("TYPE_NAME").ToString.ToUpper
                If vTypeName = "VARCHAR" Then
                    ret = " String "
                ElseIf vTypeName = "VARCHAR2" Then
                    ret = " String "
                ElseIf vTypeName = "CHAR" Then
                    ret = " System.Nullable(Of Char) "
                ElseIf vTypeName = "NVARCHAR" Then
                    ret = " String "
                ElseIf vTypeName = "NCHAR" Then
                    ret = " String "
                ElseIf vTypeName = "TEXT" Then
                    ret = " String "
                ElseIf vTypeName = "FLOAT" Then
                    ret = " System.Nullable(Of Double) "
                ElseIf vTypeName = "DOUBLE" Then
                    ret = " System.Nullable(Of Double) "
                ElseIf vTypeName = "DECIMAL" Then
                    ret = " System.Nullable(Of Double) "
                ElseIf vTypeName = "BIGINT" Then
                    ret = " System.Nullable(Of Long) "
                ElseIf vTypeName = "BIGINT IDENTITY" Then
                    ret = " System.Nullable(Of Long) "
                ElseIf vTypeName = "INT" Then
                    ret = " System.Nullable(Of Long) "
                ElseIf vTypeName = "INT IDENTITY" Then
                    ret = " System.Nullable(Of Long) "
                ElseIf vTypeName = "SMALLINT" Then
                    ret = " System.Nullable(Of Long) "
                ElseIf vTypeName = "NUMBER" Then
                    ret = " System.Nullable(Of Double) "
                ElseIf vTypeName = "NUMBERIC" Then
                    ret = " System.Nullable(Of Double) "
                ElseIf vTypeName = "DATETIME" Then
                    ret = " System.Nullable(Of DateTime) "
                ElseIf vTypeName = "DATETIME2" Then
                    ret = " System.Nullable(Of DateTime) "
                ElseIf vTypeName = "DATE" Then
                    ret = " System.Nullable(Of Date) "
                ElseIf vTypeName = "BIT" Then
                    ret = " System.Nullable(Of Boolean) "
                ElseIf vTypeName = UCase("uniqueidentifier") Then
                    ret = " String "
                ElseIf vTypeName = "IMAGE" Then
                    ret = " Byte() "
                End If
            End If

            Return ret
        End Function

        Private Function GenerateClearVariables() As String
            Dim ret As String = ""
            ret += vbNewLine & "        'Clear All Data"
            ret += vbNewLine & "        Private Sub ClearData()"

            For Each dRow As DataRow In _columnTable.Rows
                Dim vTypeName As String = UCase(dRow("TYPE_NAME").ToString())
                Dim colName As String = "_" & UCase(dRow("COLUMN_NAME").ToString())
                'GetFieldDefault
                If vTypeName = "VARCHAR" Then
                    ret += vbNewLine & "            " & colName & " = " & Chr(34) & GetFieldDefault(dRow) & Chr(34)
                ElseIf vTypeName = "VARCHAR2" Then
                    ret += vbNewLine & "            " & colName & " = " & Chr(34) & GetFieldDefault(dRow) & Chr(34)
                ElseIf vTypeName = "CHAR" Then
                    ret += vbNewLine & "            " & colName & " = " & Chr(34) & GetFieldDefault(dRow) & Chr(34)
                ElseIf vTypeName = "NVARCHAR" Then
                    ret += vbNewLine & "            " & colName & " = " & Chr(34) & GetFieldDefault(dRow) & Chr(34)
                ElseIf vTypeName = "NCHAR" Then
                    ret += vbNewLine & "            " & colName & " = " & Chr(34) & GetFieldDefault(dRow) & Chr(34)
                ElseIf vTypeName = "TEXT" Then
                    ret += vbNewLine & "            " & colName & " = " & Chr(34) & GetFieldDefault(dRow) & Chr(34)
                ElseIf vTypeName = "FLOAT" Then
                    ret += vbNewLine & "            " & colName & " = " & GetFieldDefault(dRow)
                ElseIf vTypeName = "DOUBLE" Then
                    ret += vbNewLine & "            " & colName & " = " & GetFieldDefault(dRow)
                ElseIf vTypeName = "DECIMAL" Then
                    ret += vbNewLine & "            " & colName & " = " & GetFieldDefault(dRow)
                ElseIf vTypeName = "BIGINT" Then
                    ret += vbNewLine & "            " & colName & " = " & GetFieldDefault(dRow)
                ElseIf vTypeName = "BIGINT IDENTITY" Then
                    ret += vbNewLine & "            " & colName & " = " & GetFieldDefault(dRow)
                ElseIf vTypeName = "INT" Then
                    ret += vbNewLine & "            " & colName & " = " & GetFieldDefault(dRow)
                ElseIf vTypeName = "INT IDENTITY" Then
                    ret += vbNewLine & "            " & colName & " = " & GetFieldDefault(dRow)
                ElseIf vTypeName = "SMALLINT" Then
                    ret += vbNewLine & "            " & colName & " = " & GetFieldDefault(dRow)
                ElseIf vTypeName = "NUMBER" Then
                    ret += vbNewLine & "            " & colName & " = " & GetFieldDefault(dRow)
                ElseIf vTypeName = "NUMBERIC" Then
                    ret += vbNewLine & "            " & colName & " = " & GetFieldDefault(dRow)
                ElseIf vTypeName = "DATETIME" Then
                    ret += vbNewLine & "            " & colName & " = New DateTime(1,1,1)"
                ElseIf vTypeName = "DATETIME2" Then
                    ret += vbNewLine & "            " & colName & " = New DateTime(1,1,1)"
                ElseIf vTypeName = "DATE" Then
                    ret += vbNewLine & "            " & colName & " = New DateTime(1,1,1)"
                ElseIf vTypeName = "BIT" Then
                    ret += vbNewLine & "            " & colName & " = False"
                ElseIf vTypeName = UCase("uniqueidentifier") Then
                    ret += vbNewLine & "             " & colName & " = " & Chr(34) & Chr(34)
                ElseIf vTypeName = "IMAGE" Then
                    ret += vbNewLine & "             " & colName & " = Nothing"
                End If
            Next
            ret += vbNewLine & "        End Sub"

            Return ret
        End Function

        Private Function GetStatementParameter(ByVal dataType As String, ByVal columnName As String) As String
            Dim ret As String = ""
            Dim vTypeName As String = UCase(dataType)
            ret = Chr(34) & "@" & columnName & Chr(34)

            Return ret
        End Function

        Private Function GetStatement(ByVal dataType As String, ByVal columnName As String) As String
            Dim ret As String = ""
            Dim vTypeName As String = UCase(dataType)

            If vTypeName = "VARCHAR" Then
                ret = "DB.SetString(" & columnName & ")"
            ElseIf vTypeName = "VARCHAR2" Then
                ret = "DB.SetString(" & columnName & ")"
            ElseIf vTypeName = "CHAR" Then
                ret = "DB.SetString(" & columnName & ")"
            ElseIf vTypeName = "NVARCHAR" Then
                ret = "DB.SetString(" & columnName & ")"
            ElseIf vTypeName = "NCHAR" Then
                ret = "DB.SetString(" & columnName & ")"
            ElseIf vTypeName = "TEXT" Then
                ret = "DB.SetString(" & columnName & ")"
            ElseIf vTypeName = "FLOAT" Then
                ret = "DB.SetDouble(" & columnName & ")"
            ElseIf vTypeName = "DOUBLE" Then
                ret = "DB.SetDouble(" & columnName & ")"
            ElseIf vTypeName = "DECIMAL" Then
                ret = "DB.SetDouble(" & columnName & ")"
            ElseIf vTypeName = "BIGINT" Then
                ret = "DB.SetDouble(" & columnName & ")"
            ElseIf vTypeName = "BIGINT IDENTITY" Then
                ret = "DB.SetDouble(" & columnName & ")"
            ElseIf vTypeName = "INT" Then
                ret = "DB.SetDouble(" & columnName & ")"
            ElseIf vTypeName = "INT IDENTITY" Then
                ret = "DB.SetDouble(" & columnName & ")"
            ElseIf vTypeName = "SMALLINT" Then
                ret = "DB.SetDouble(" & columnName & ")"
            ElseIf vTypeName = "NUMBER" Then
                ret = "DB.SetDouble(" & columnName & ")"
            ElseIf vTypeName = "NUMBERIC" Then
                ret = "DB.SetDouble(" & columnName & ")"
            ElseIf vTypeName = "DATETIME" Then
                ret = "DB.SetDateTime(" + columnName + ")"
            ElseIf vTypeName = "DATETIME2" Then
                ret = "DB.SetDateTime(" + columnName + ")"
            ElseIf vTypeName = "DATE" Then
                ret = "DB.SetDateTime(" + columnName + ")"
            ElseIf vTypeName = "BIT" Then
                ret = "DB.SetString(" & columnName & ")"
            ElseIf vTypeName = UCase("uniqueidentifier") Then
                ret = "DB.SetString(" & columnName & ")"
            ElseIf vTypeName = "IMAGE" Then
                ret = Chr(34) & "@" & columnName & Chr(34)
            End If

            Return ret
        End Function
        Private Function GetDataType(ByVal dataType As String) As String
            Dim ret As String = ""
            Dim vTypeName As String = UCase(dataType)

            If vTypeName = "VARCHAR" Then
                ret = "String"
            ElseIf vTypeName = "VARCHAR2" Then
                ret = "String"
            ElseIf vTypeName = "CHAR" Then
                ret = "String"
            ElseIf vTypeName = "NVARCHAR" Then
                ret = "String"
            ElseIf vTypeName = "NCHAR" Then
                ret = "String"
            ElseIf vTypeName = "TEXT" Then
                ret = "String"
            ElseIf vTypeName = "FLOAT" Then
                ret = "Double"
            ElseIf vTypeName = "DOUBLE" Then
                ret = "Double"
            ElseIf vTypeName = "DECIMAL" Then
                ret = "Double"
            ElseIf vTypeName = "BIGINT" Then
                ret = "Long"
            ElseIf vTypeName = "BIGINT IDENTITY" Then
                ret = "Long"
            ElseIf vTypeName = "INT" Then
                ret = "Integer"
            ElseIf vTypeName = "INT IDENTITY" Then
                ret = "Integer"
            ElseIf vTypeName = "SMALLINT" Then
                ret = "Int16"
            ElseIf vTypeName = "NUMBER" Then
                ret = "Double"
            ElseIf vTypeName = "NUMBERIC" Then
                ret = "Double"
            ElseIf vTypeName = "DATETIME" Then
                ret = "DateTime"
            ElseIf vTypeName = "DATETIME2" Then
                ret = "DateTime"
            ElseIf vTypeName = "DATE" Then
                ret = "DateTime"
            ElseIf vTypeName = "BIT" Then
                ret = "Boolean"
            ElseIf vTypeName = UCase("uniqueidentifier") Then
                ret = "String"
            ElseIf vTypeName = "IMAGE" Then
                ret = "Byte"
            End If

            Return ret
        End Function

        Private Function GenerateSQL() As String
            Dim ret As String = ""
            Dim column As String = ""
            Dim fields As String = ""
            Dim retFld As String = ""
            ret += vbNewLine & "        ' SQL Statements"

            If _IsView = False Then
                For Each dRow As DataRow In _columnTable.Rows
                    'ชื่อฟิลด์ที่จะเก็บข้อมูลเกี่ยวกับการ UPDATE ไม่ต้องกำหนดค่า
                    If dRow("COLUMN_NAME").ToString() <> Constant.FieldName.UPDATE_BY And dRow("COLUMN_NAME").ToString() <> Constant.FieldName.UPDATE_DATE And dRow("COLUMN_NAME").ToString() <> Constant.FieldName.MSREPL_TRAN_VERSION And dRow("TYPE_NAME").ToString().ToUpper.IndexOf("IDENTITY") <= 0 Then
                        column += IIf(column = "", "", ", ") & UCase(dRow("COLUMN_NAME").ToString())
                        fields += IIf(fields = "", "", " & " & Chr(34) & ", " & Chr(34) & vbNewLine) & "                sql += " & GetStatementParameter(dRow("TYPE_NAME").ToString(), "_" & UCase(dRow("COLUMN_NAME").ToString()))
                    End If

                    If retFld.Trim = "" Then
                        retFld = "INSERTED." & UCase(dRow("COLUMN_NAME").ToString())
                    Else
                        retFld += ", INSERTED." & UCase(dRow("COLUMN_NAME").ToString())
                    End If
                Next
                ret += vbNewLine
                ret += vbNewLine
                ret += vbNewLine & "        'Get Insert Statement for table " & _tableName
                ret += vbNewLine & "        Private ReadOnly Property SqlInsert() As String "
                ret += vbNewLine & "            Get"
                ret += vbNewLine & "                Dim Sql As String=" & Chr(34) & Chr(34)
                ret += vbNewLine & "                Sql += " & Chr(34) & "INSERT INTO " & Chr(34) & " & " & IIf(_IsView = True, "view", "table") & "Name " & " & " & Chr(34) & " (" & column & ")" & Chr(34)
                ret += vbNewLine & "                Sql += " & Chr(34) & " OUTPUT " & retFld & Chr(34)
                ret += vbNewLine & "                Sql += " & Chr(34) & " VALUES(" & Chr(34)
                ret += vbNewLine & fields
                ret += vbNewLine & "                sql += " & Chr(34) & ")" & Chr(34)
                ret += vbNewLine & "                Return sql"
                ret += vbNewLine & "            End Get"
                ret += vbNewLine & "        End Property"

                fields = ""
                For Each dRow As DataRow In _columnTable.Rows
                    'ชื่อฟิลด์ที่จะเก็บข้อมูลเกี่ยวกับการ INSERT ไม่ต้องกำหนดค่า
                    If dRow("COLUMN_NAME").ToString() <> Constant.FieldName.CREATE_DATE And dRow("COLUMN_NAME").ToString() <> Constant.FieldName.CREATE_BY And dRow("COLUMN_NAME").ToString() <> primaryKeyFiels And dRow("COLUMN_NAME").ToString() <> Constant.FieldName.MSREPL_TRAN_VERSION And dRow("TYPE_NAME").ToString().ToUpper.IndexOf("IDENTITY") <= 0 Then
                        _pkColumnTable.DefaultView.RowFilter = "column_name = '" & dRow("COLUMN_NAME") & "'"
                        If _pkColumnTable.DefaultView.Count = 0 Then
                            fields += IIf(fields = "", "", " & " & Chr(34) & ", " & Chr(34) & vbNewLine) & "                Sql += " & Chr(34) & UCase(dRow("COLUMN_NAME").ToString()) & " = " & Chr(34) & " & " & GetStatementParameter(dRow("TYPE_NAME").ToString().ToString(), "_" & UCase(dRow("COLUMN_NAME").ToString()))
                        End If
                        _pkColumnTable.DefaultView.RowFilter = ""
                    End If
                Next
                fields += IIf(fields = "", "", " + " & Chr(34) & Chr(34))
                ret += vbNewLine
                ret += vbNewLine
                ret += vbNewLine & "        'Get update statement form table " & _tableName
                ret += vbNewLine & "        Private ReadOnly Property SqlUpdate() As String"
                ret += vbNewLine & "            Get"
                ret += vbNewLine & "                Dim Sql As String = " & Chr(34) & Chr(34)
                ret += vbNewLine & "                Sql += " & Chr(34) & "UPDATE " & Chr(34) & " & " & IIf(_IsView = True, "view", "table") & "Name & " & Chr(34) & " SET " & Chr(34)
                ret += vbNewLine & fields
                ret += vbNewLine & "                Return Sql"
                ret += vbNewLine & "            End Get"
                ret += vbNewLine & "        End Property"

                ret += vbNewLine
                ret += vbNewLine
                ret += vbNewLine & "        'Get Delete Record in table " & _tableName
                ret += vbNewLine & "        Private ReadOnly Property SqlDelete() As String"
                ret += vbNewLine & "            Get"
                ret += vbNewLine & "                Dim Sql As String = " & Chr(34) & "DELETE FROM " & Chr(34) & " & " & IIf(_IsView = True, "view", "table") & "Name"
                ret += vbNewLine & "                Return Sql"
                ret += vbNewLine & "            End Get"
                ret += vbNewLine & "        End Property"
            End If

            ret += vbNewLine
            ret += vbNewLine

            Dim SelectColumn As String = ""
            For Each dRow As DataRow In _columnTable.Rows
                If dRow("COLUMN_NAME").ToString() <> Constant.FieldName.MSREPL_TRAN_VERSION Then
                    SelectColumn += IIf(SelectColumn = "", "", ", ") & UCase(dRow("COLUMN_NAME").ToString())
                End If
            Next
            


            ret += vbNewLine & "        'Get Select Statement for table " & _tableName
            ret += vbNewLine & "        Private ReadOnly Property SqlSelect() As String"
            ret += vbNewLine & "            Get"
            ret += vbNewLine & "                Dim Sql As String = " & Chr(34) & "SELECT " & SelectColumn & " FROM " & Chr(34) & " & " & IIf(_IsView = True, "view", "table") & "Name"
            ret += vbNewLine & "                Return Sql"
            ret += vbNewLine & "            End Get"
            ret += vbNewLine & "        End Property"

            Return ret
        End Function


        Private Function GenerateAppendDataMethod() As String
            Dim ret As String = "       'Define Public Method "
            Dim primaryField As String = ""
            Dim primaryTypeName As String = ""
            Dim orderByType As String = ""

            ret += vbNewLine & "        'Execute the select statement with the specified condition and return a System.Data.DataTable."
            ret += vbNewLine & "        '/// <param name=whereClause>The condition for execute select statement.</param>"
            ret += vbNewLine & "        '/// <param name=orderBy>The fields for sort data.</param>"
            ret += vbNewLine & "        '/// <param name=trans>The System.Data." + _databaseType + "Client." + _databaseType + "Transaction used by this System.Data." + _databaseType + "Client." + _databaseType + "Command.</param>"
            ret += vbNewLine & "        '/// <returns>The System.Data.DataTable object for specified condition.</returns>"
            ret += vbNewLine & "        Public Function GetDataList(whClause As String, orderBy As String, trans As " & _databaseType & "Transaction, cmdParm() As SqlParameter) As DataTable"
            If _databaseType = Constant.DatabaseType.SQL Then
                orderByType = " ORDER BY  " & Chr(34) & " & orderBy"
            ElseIf _databaseType = Constant.DatabaseType.Oracle Then
                orderByType = " ORDER BY  " & Chr(34) & " & DB.SetSortString(orderBy)"
            End If
            ret += vbNewLine & "            Return DB.ExecuteTable(SqlSelect & IIf(whClause = " & Chr(34) & Chr(34) & ", " & Chr(34) & Chr(34) & ", " & Chr(34) & " WHERE " & Chr(34) & " & whClause) & IIF(orderBy = " & Chr(34) & Chr(34) & ", " & Chr(34) & Chr(34) & ", " & Chr(34) & orderByType & "), trans, cmdParm)"
            ret += vbNewLine & "        End Function"
            ret += vbNewLine
            ret += vbNewLine
            ret += vbNewLine & "        'Execute the select statement with the specified condition and return a System.Data.DataTable."
            ret += vbNewLine & "        '/// <param name=whereClause>The condition for execute select statement.</param>"
            ret += vbNewLine & "        '/// <param name=trans>The System.Data." + _databaseType + "Client." + _databaseType + "Transaction used by this System.Data." + _databaseType + "Client." + _databaseType + "Command.</param>"
            ret += vbNewLine & "        '/// <returns>The System.Data.DataTable object for specified condition.</returns>"
            ret += vbNewLine
            ret += vbNewLine & "        Public Function GetListBySql(Sql As String, trans As " & _databaseType & "Transaction, cmdParm() As SqlParameter) As DataTable"
            ret += vbNewLine & "            Return DB.ExecuteTable(Sql, trans, cmdParm)"
            ret += vbNewLine & "        End Function"
            ret += vbNewLine
            ret += vbNewLine


            If _IsView = False Then
                'สำหรับฟิลด์ Primary Key ในแต่ละตาราง มี Primary Key ได้เพียง 1 ฟิลด์
                Dim dRow As DataRow = _pkColumnTable.Rows(0)
                primaryField = dRow("COLUMN_NAME").ToString.ToUpper
                primaryTypeName = dRow("TYPE_NAME").ToString.ToUpper

                ret += vbNewLine & "        '/// Returns an indication whether the current data is inserted into " + _tableName & " " & IIf(_IsView = True, "view", "table") & " successfully."
                ret += vbNewLine & "        '/// <param name=userID>The current user.</param>"
                ret += vbNewLine & "        '/// <param name=trans>The System.Data." & _databaseType & "Client." & _databaseType + "Transaction used by this System.Data." & _databaseType & "Client." & _databaseType & "Command.</param>"
                ret += vbNewLine & "        '/// <returns>true if insert data successfully; otherwise, false.</returns>"
                ret += vbNewLine & "        Public Function InsertData(CreatedBy As String,trans As " & _databaseType & "Transaction) As ExecuteDataInfo"
                ret += vbNewLine & "            If trans IsNot Nothing Then "
                For Each pk As DataRow In _pkColumnTable.Rows
                    'ถ้า Primary Key เป็น Type Autorun
                    If pk("TYPE_NAME").ToString.ToUpper.IndexOf("IDENTITY") <= 0 Then
                        ret += vbNewLine & "                _" & pk("column_name").ToString.ToUpper & " = DB.GetNextID(" & Chr(34) & pk("column_name").ToString.ToUpper & Chr(34) & ",tableName, trans)"
                    End If
                Next
                If ChkHaveColumn(Constant.FieldName.CREATE_BY) = True Then
                    ret += vbNewLine & "                _" & Constant.FieldName.CREATE_BY & " = CreatedBy"
                End If
                If ChkHaveColumn(Constant.FieldName.CREATE_DATE) = True Then
                    ret += vbNewLine & "                _" & Constant.FieldName.CREATE_DATE & " = DateTime.Now"
                End If
                ret += vbNewLine & "                Return doInsert(trans)"
                ret += vbNewLine & "            Else "
                ret += vbNewLine & "                _error = " & Chr(34) & "Transaction Is not null" & Chr(34)
                ret += vbNewLine & "                Dim ret As New ExecuteDataInfo"
                ret += vbNewLine & "                ret.IsSuccess = False"
                ret += vbNewLine & "                ret.SqlStatement = " & Chr(34) & Chr(34)
                ret += vbNewLine & "                ret.ErrorMessage = _error"

                ret += vbNewLine & "                Return ret"
                ret += vbNewLine & "            End If "
                ret += vbNewLine & "        End Function"
                ret += vbNewLine
                ret += vbNewLine

                If _pkColumnTable.Rows.Count > 0 Then
                    ret += vbNewLine & "        '/// Returns an indication whether the current data is updated to " & _tableName & " " & IIf(_IsView = True, "view", "table") & " successfully."
                    ret += vbNewLine & "        '/// <param name=userID>The current user.</param>"
                    ret += vbNewLine & "        '/// <param name=trans>The System.Data." & _databaseType & "Client." & _databaseType & "Transaction used by this System.Data." & _databaseType & "Client." & _databaseType & "Command.</param>"
                    ret += vbNewLine & "        '/// <returns>true if update data successfully; otherwise, false.</returns>"
                    ret += vbNewLine & "        Public Function UpdateData(UpdatedBy As String,trans As " & _databaseType & "Transaction) As ExecuteDataInfo"
                    ret += vbNewLine & "            If trans IsNot Nothing Then "
                    ret += vbNewLine & "                If _" & _pkColumnTable.Rows(0)("COLUMN_NAME") & " > 0 Then "
                    If ChkHaveColumn(Constant.FieldName.UPDATE_BY) = True Then
                        ret += vbNewLine & "                    _" & Constant.FieldName.UPDATE_BY.ToUpper & " = UpdatedBy"
                    End If
                    If ChkHaveColumn(Constant.FieldName.UPDATE_DATE) = True Then
                        ret += vbNewLine & "                    _" & Constant.FieldName.UPDATE_DATE.ToUpper & " = DateTime.Now"
                    End If
                    ret += vbNewLine

                    Dim whUTxt As String = ""
                    For Each pk As DataRow In _pkColumnTable.Rows
                        Dim tmpWh As String = pk("COLUMN_NAME").ToString.ToUpper & " = @_" & pk("COLUMN_NAME").ToString.ToUpper
                        If whUTxt = "" Then
                            whUTxt = tmpWh
                        Else
                            whUTxt += " and " & tmpWh
                        End If
                    Next
                    ret += vbNewLine & "                    Return doUpdate(" & Chr(34) & whUTxt & Chr(34) & ", trans)"
                    ret += vbNewLine & "                Else "
                    ret += vbNewLine & "                    _error = " & Chr(34) & "No ID Data" & Chr(34)
                    ret += vbNewLine & "                    Dim ret As New ExecuteDataInfo"
                    ret += vbNewLine & "                    ret.IsSuccess = False"
                    ret += vbNewLine & "                    ret.SqlStatement = " & Chr(34) & Chr(34)
                    ret += vbNewLine & "                    ret.ErrorMessage = _error"

                    ret += vbNewLine & "                    Return ret"
                    ret += vbNewLine & "                End If "
                    ret += vbNewLine & "            Else "
                    ret += vbNewLine & "                _error = " & Chr(34) & "Transaction Is not null" & Chr(34)
                    ret += vbNewLine & "                Dim ret As New ExecuteDataInfo"
                    ret += vbNewLine & "                ret.IsSuccess = False"
                    ret += vbNewLine & "                ret.SqlStatement = " & Chr(34) & Chr(34)
                    ret += vbNewLine & "                ret.ErrorMessage = _error"

                    ret += vbNewLine & "                Return ret"
                    ret += vbNewLine & "            End If "
                    ret += vbNewLine & "        End Function"
                End If
                ret += vbNewLine
                ret += vbNewLine
                ret += vbNewLine & "        '/// Returns an indication whether the current data is updated to " & _tableName & " " & IIf(_IsView = True, "view", "table") & " successfully."
                ret += vbNewLine & "        '/// <returns>true if update data successfully; otherwise, false.</returns>"
                ret += vbNewLine & "        Public Function UpdateBySql(Sql As String, trans As " & _databaseType & "Transaction, cmbParm() As " & _databaseType & "Parameter) As ExecuteDataInfo"
                ret += vbNewLine & "            If trans IsNot Nothing Then "
                ret += vbNewLine & "                Return DB.ExecuteNonQuery(Sql, trans, cmbParm)"
                ret += vbNewLine & "            Else "
                ret += vbNewLine & "                _error = " & Chr(34) & "Transaction Is not null" & Chr(34)
                ret += vbNewLine & "                Dim ret As New ExecuteDataInfo"
                ret += vbNewLine & "                ret.IsSuccess = False"
                ret += vbNewLine & "                ret.ErrorMessage = _error"
                ret += vbNewLine & "                Return ret"
                ret += vbNewLine & "            End If "
                ret += vbNewLine & "        End Function"
                ret += vbNewLine
                ret += vbNewLine

                Dim cPK As String = "c" & primaryField & " As " & GetDataType(primaryTypeName)
                Dim uPK As String = Chr(34) & primaryField & " = " & "@_" & primaryField & Chr(34)
                ret += vbNewLine & "        '/// Returns an indication whether the current data is deleted from " & _tableName & " " & IIf(_IsView = True, "view", "table") & " successfully."
                ret += vbNewLine & "        '/// <param name=trans>The System.Data." & _databaseType & "Client." & _databaseType & "Transaction used by this System.Data." & _databaseType & "Client." & _databaseType & "Command.</param>"
                ret += vbNewLine & "        '/// <returns>true if delete data successfully; otherwise, false.</returns>"
                ret += vbNewLine & "        Public Function DeleteByPK(" & cPK & ", trans As " + _databaseType + "Transaction) As ExecuteDataInfo"
                ret += vbNewLine & "            If trans IsNot Nothing Then "
                ret += vbNewLine & "                Dim p(1) As " & _databaseType & "Parameter"
                ret += vbNewLine & "                p(0) = DB.SetBigInt(" & Chr(34) & "@_" & primaryField & Chr(34) & ", c" & primaryField.ToUpper & ")"
                ret += vbNewLine & "                Return doDelete(" & uPK & ", trans, p)"
                ret += vbNewLine & "            Else "
                ret += vbNewLine & "                _error = " & Chr(34) & "Transaction Is not null" & Chr(34)
                ret += vbNewLine & "                Dim ret As New ExecuteDataInfo"
                ret += vbNewLine & "                ret.IsSuccess = False"
                ret += vbNewLine & "                ret.ErrorMessage = _error"
                ret += vbNewLine & "                Return ret"
                ret += vbNewLine & "            End If "
                ret += vbNewLine & "        End Function"
                ret += vbNewLine
                ret += vbNewLine
                ret += vbNewLine & "        '/// Returns an indication whether the record of " + _tableName + " by specified " + primaryField + " key is retrieved successfully."
                ret += vbNewLine & "        '/// <param name=c" + primaryField + ">The " + primaryField + " key.</param>"
                ret += vbNewLine & "        '/// <param name=trans>The System.Data." + _databaseType + "Client." + _databaseType + "Transaction used by this System.Data." + _databaseType + "Client." + _databaseType + "Command.</param>"
                ret += vbNewLine & "        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>"
                ret += vbNewLine & "        Public Function ChkDataByPK(" & cPK & ", trans As " & _databaseType & "Transaction) As Boolean"
                ret += vbNewLine & "            Dim p(1) As " & _databaseType & "Parameter"
                ret += vbNewLine & "            p(0) = DB.SetBigInt(" & Chr(34) & "@_" & primaryField & Chr(34) & ", c" & primaryField.ToUpper & ")"
                ret += vbNewLine & "            Return doChkData(" & uPK & ", trans, p)"
                ret += vbNewLine & "        End Function"
                ret += vbNewLine
                ret += vbNewLine
                ret += vbNewLine & "        '/// Returns an indication whether the record and Mapping field to Data Class of " + _tableName + " by specified " + primaryField + " key is retrieved successfully."
                ret += vbNewLine & "        '/// <param name=c" + primaryField + ">The " + primaryField + " key.</param>"
                ret += vbNewLine & "        '/// <param name=trans>The System.Data." + _databaseType + "Client." + _databaseType + "Transaction used by this System.Data." + _databaseType + "Client." + _databaseType + "Command.</param>"
                ret += vbNewLine & "        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>"
                ret += vbNewLine & "        Public Function GetDataByPK(" & cPK & ", trans As " & _databaseType & "Transaction) As " & _className & _objType
                ret += vbNewLine & "            Dim p(1) As " & _databaseType & "Parameter"
                ret += vbNewLine & "            p(0) = DB.SetBigInt(" & Chr(34) & "@_" & primaryField & Chr(34) & ", c" & primaryField.ToUpper & ")"
                ret += vbNewLine & "            Return doGetData(" & uPK & ", trans, p)"
                ret += vbNewLine & "        End Function"
                ret += vbNewLine
                ret += vbNewLine

                For Each rUQ As DataRow In _uniqueColumnTable.Rows
                    'สำหรับฟิลด์ที่กำหนดให้ Unique
                    If rUQ("CONSTRAINT_TYPE") = "U" Then
                        'Dim constraintName As String = rUQ("CONSTRAINT_NAME").ToString()
                        Dim constraintFieldName As String = GetConstraintFieldName(rUQ("CONSTRAINT_KEYS").ToString())
                        Dim constraintFieldDataType As String = GetConstraintFieldDataType(rUQ("CONSTRAINT_KEYS").ToString())
                        Dim assignType As String = GetAssignType(rUQ("CONSTRAINT_KEYS").ToString())

                        Dim iCons As Integer = 0
                        Dim tmpCons As String = ""
                        Dim consParam() As String = Split(rUQ("CONSTRAINT_KEYS").ToString(), ",")
                        If consParam.Length > 0 Then
                            tmpCons += vbNewLine & "            Dim cmdPara(" & (consParam.Length + 1) & ")  As " & _databaseType & "Parameter"

                            For Each consP As String In consParam
                                tmpCons += vbNewLine & "            cmdPara(" & iCons & ") = DB.SetText(" & Chr(34) & "@_" & consP.ToUpper & Chr(34) & ", c" + consP.ToUpper + ") "
                                iCons += 1
                            Next

                            ret += vbNewLine & "        '/// Returns an indication whether the record of " + _tableName + " by specified " + constraintFieldName + " key is retrieved successfully."
                            ret += vbNewLine & "        '/// <param name=c" + constraintFieldName + ">The " + constraintFieldName + " key.</param>"
                            ret += vbNewLine & "        '/// <param name=trans>The System.Data." + _databaseType + "Client." + _databaseType + "Transaction used by this System.Data." + _databaseType + "Client." + _databaseType + "Command.</param>"
                            ret += vbNewLine & "        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>"
                            ret += vbNewLine & "        Public Function ChkDataBy" & constraintFieldName & "(" & constraintFieldDataType & ", trans As " & _databaseType & "Transaction) As Boolean"

                            ret += tmpCons
                            ret += vbNewLine & "            Return doChkData(" & assignType & Chr(34) & ", trans, cmdPara)"
                            ret += vbNewLine & "        End Function"
                            ret += vbNewLine
                            ret += vbNewLine & "        '/// Returns an duplicate data record of " + _tableName + " by specified " + constraintFieldName + " key is retrieved successfully."
                            ret += vbNewLine & "        '/// <param name=c" + constraintFieldName + ">The " + constraintFieldName + " key.</param>"
                            ret += vbNewLine & "        '/// <param name=trans>The System.Data." + _databaseType + "Client." + _databaseType + "Transaction used by this System.Data." + _databaseType + "Client." + _databaseType + "Command.</param>"
                            ret += vbNewLine & "        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>"
                            ret += vbNewLine & "        Public Function ChkDuplicateBy" & constraintFieldName & "(" & constraintFieldDataType & ", c" & primaryField & " As " & GetDataType(dRow("TYPE_NAME").ToString()) & ", trans As " & _databaseType & "Transaction) As Boolean"

                            'กรณีเช็ค Duplicate จะต้อง Where id<>ตัวเองด้วย
                            tmpCons += vbNewLine & "            cmdPara(" & iCons & ") = DB.SetBigInt(" & Chr(34) & "@_" & primaryField & Chr(34) & ", c" & primaryField & ") "
                            ret += tmpCons
                            ret += vbNewLine & "            Return doChkData(" & assignType & " And " & primaryField & " <> @_" & primaryField & Chr(34) & ", trans, cmdPara)"
                            ret += vbNewLine & "        End Function"
                            ret += vbNewLine
                            ret += vbNewLine
                        End If
                    End If
                Next
            End If

            ret += vbNewLine & "        '/// Returns an indication whether the record of " & _tableName & " by specified condition is retrieved successfully."
            ret += vbNewLine & "        '/// <param name=whText>The condition specify the deleting record(s).</param>"
            ret += vbNewLine & "        '/// <param name=trans>The System.Data." & _databaseType & "Client." & _databaseType & "Transaction used by this System.Data." & _databaseType & "Client." & _databaseType & "Command.</param>"
            ret += vbNewLine & "        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>"
            ret += vbNewLine & "        Public Function ChkDataByWhere(whText As String, trans As " & _databaseType & "Transaction, cmdPara() As " & _databaseType & "Parameter) As Boolean"
            ret += vbNewLine & "            Return doChkData(whText, trans, cmdPara)"
            ret += vbNewLine & "        End Function"
            ret += vbNewLine
            ret += vbNewLine

            Return ret
        End Function

        Private Function ChkHaveColumn(ByVal _ColumnName As String) As Boolean
            Dim ret As Boolean = False
            Dim dt As New DataTable
            dt = _columnTable.Copy()

            dt.DefaultView.RowFilter = "COLUMN_NAME = '" & _ColumnName & "'"
            If dt.DefaultView.Count > 0 Then
                ret = True
            End If
            Return ret
        End Function

        Private Function GeneratePrivateMethod() As String
            Dim ret As String = ""

            If _IsView = False Then
                ret += vbNewLine & "        '/// Returns an indication whether the current data is inserted into " & _tableName & " " & IIf(_IsView = True, "view", "table") & " successfully."
                ret += vbNewLine & "        '/// <param name=trans>The System.Data." & _databaseType & "Client." & _databaseType & "Transaction used by this System.Data." & _databaseType & "Client." & _databaseType & "Command.</param>"
                ret += vbNewLine & "        '/// <returns>true if insert data successfully; otherwise, false.</returns>"
                ret += vbNewLine & "        Private Function doInsert(trans As " & _databaseType & "Transaction) As ExecuteDataInfo"
                ret += vbNewLine & "            Dim ret As New ExecuteDataInfo"
                ret += vbNewLine & "            If _haveData = False Then"
                ret += vbNewLine & "                Try"

                'Dim ImpParam As String = CreateImgParam()
                'ret += vbNewLine & ImpParam
                ret += vbNewLine & "                    Dim dt as DataTable = DB.ExecuteTable(SqlInsert, trans, SetParameterData())"
                ret += vbNewLine & "                    If dt.Rows.Count = 0 Then"
                ret += vbNewLine & "                        ret.IsSuccess = False"
                ret += vbNewLine & "                        ret.ErrorMessage = DB.ErrorMessage"
                ret += vbNewLine & "                    Else"
                For Each pk As DataRow In _pkColumnTable.Rows
                    If pk("TYPE_NAME").ToString.ToUpper.IndexOf("IDENTITY") > 0 Then
                        ret += vbNewLine & "                        _" & pk("column_name").ToString.ToUpper & " = dt.Rows(0)(" & Chr(34) & pk("column_name").ToString.ToUpper & Chr(34) & ")"
                    End If
                Next
                ret += vbNewLine & "                        _haveData = True"
                ret += vbNewLine & "                        ret.IsSuccess = True"
                ret += vbNewLine & "                        _information = MessageResources.MSGIN001"
                ret += vbNewLine & "                        ret.InfoMessage = _information"
                ret += vbNewLine & "                    End If"
                ret += vbNewLine & "                Catch ex As ApplicationException"
                ret += vbNewLine & "                    ret.IsSuccess = false"
                ret += vbNewLine & "                    ret.ErrorMessage = ex.Message & " & Chr(34) & "ApplicationException :" & Chr(34) & " & ex.ToString()  "
                ret += vbNewLine & "                    ret.SqlStatement = SqlInsert"
                ret += vbNewLine & "                Catch ex As Exception"
                ret += vbNewLine & "                    ret.IsSuccess = False"
                ret += vbNewLine & "                    ret.ErrorMessage = MessageResources.MSGEC101 & " & Chr(34) & " Exception :" & Chr(34) & " & ex.ToString()  "
                ret += vbNewLine & "                    ret.SqlStatement = SqlInsert"
                ret += vbNewLine & "                End Try"
                ret += vbNewLine & "            Else"
                ret += vbNewLine & "                ret.IsSuccess = False"
                ret += vbNewLine & "                ret.ErrorMessage = MessageResources.MSGEN002  "
                ret += vbNewLine & "                ret.SqlStatement = SqlInsert"
                ret += vbNewLine & "            End If"
                ret += vbNewLine
                ret += vbNewLine & "            Return ret"
                ret += vbNewLine & "        End Function"
                ret += vbNewLine
                ret += vbNewLine
                ret += vbNewLine & "        '/// Returns an indication whether the current data is updated to " & _tableName & " " & IIf(_IsView = True, "view", "table") & " successfully."
                ret += vbNewLine & "        '/// <param name=whText>The condition specify the updating record(s).</param>"
                ret += vbNewLine & "        '/// <param name=trans>The System.Data." & _databaseType & "Client." & _databaseType & "Transaction used by this System.Data." & _databaseType & "Client." & _databaseType & "Command.</param>"
                ret += vbNewLine & "        '/// <returns>true if update data successfully; otherwise, false.</returns>"
                ret += vbNewLine & "        Private Function doUpdate(whText As String, trans As " & _databaseType & "Transaction) As ExecuteDataInfo"
                ret += vbNewLine & "            Dim ret As New ExecuteDataInfo"
                ret += vbNewLine & "            Dim tmpWhere As String = " & Chr(34) & " Where " & Chr(34) & " & whText"
                ret += vbNewLine & "            If _haveData = True Then"
                ret += vbNewLine & "                Dim sql As String = SqlUpdate & tmpWhere"
                ret += vbNewLine & "                If whText.Trim() <> " & Chr(34) & Chr(34)
                ret += vbNewLine & "                    Try"
                ret += vbNewLine & "                        ret = DB.ExecuteNonQuery(sql, trans, SetParameterData())"
                ret += vbNewLine & "                        If ret.IsSuccess = False Then"
                ret += vbNewLine & "                            _error = DB.ErrorMessage"
                ret += vbNewLine & "                        Else"
                ret += vbNewLine & "                            _information = MessageResources.MSGIU001"
                ret += vbNewLine & "                            ret.InfoMessage = MessageResources.MSGIU001"
                ret += vbNewLine & "                        End If"
                ret += vbNewLine & "                    Catch ex As ApplicationException"
                ret += vbNewLine & "                        ret.IsSuccess = False"
                ret += vbNewLine & "                        ret.ErrorMessage = " & Chr(34) & "ApplicationException:" & Chr(34) & " & ex.Message & ex.ToString() "
                ret += vbNewLine & "                        ret.SqlStatement = sql"
                ret += vbNewLine & "                    Catch ex As Exception"
                ret += vbNewLine & "                        ret.IsSuccess = False"
                ret += vbNewLine & "                        ret.ErrorMessage = " & Chr(34) & "Exception:" & Chr(34) & " & MessageResources.MSGEC102 &  ex.ToString() "
                ret += vbNewLine & "                        ret.SqlStatement = sql"
                ret += vbNewLine & "                    End Try"
                ret += vbNewLine & "                Else"
                ret += vbNewLine & "                    ret.IsSuccess = False"
                ret += vbNewLine & "                    ret.ErrorMessage = MessageResources.MSGEU003 "
                ret += vbNewLine & "                    ret.SqlStatement = sql"
                ret += vbNewLine & "                End If"
                ret += vbNewLine & "            Else"
                ret += vbNewLine & "                ret.IsSuccess = True"
                ret += vbNewLine & "            End If"
                ret += vbNewLine
                ret += vbNewLine & "            Return ret"
                ret += vbNewLine & "        End Function"
                ret += vbNewLine
                ret += vbNewLine

                ret += vbNewLine & "        '/// Returns an indication whether the current data is deleted from " & _tableName & " " & IIf(_IsView = True, "view", "table") & " successfully."
                ret += vbNewLine & "        '/// <param name=whText>The condition specify the deleting record(s).</param>"
                ret += vbNewLine & "        '/// <param name=trans>The System.Data." & _databaseType & "Client." & _databaseType & "Transaction used by this System.Data." & _databaseType & "Client." & _databaseType & "Command.</param>"
                ret += vbNewLine & "        '/// <returns>true if delete data successfully; otherwise, false.</returns>"
                ret += vbNewLine & "        Private Function doDelete(whText As String, trans As " & _databaseType & "Transaction, cmdPara() As SqlParameter) As ExecuteDataInfo"
                ret += vbNewLine & "             Dim ret As New ExecuteDataInfo"
                ret += vbNewLine & "             Dim tmpWhere As String = " & Chr(34) & " Where " & Chr(34) & " & whText"
                ret += vbNewLine & "             Dim sql As String = SqlDelete & tmpWhere"
                ret += vbNewLine & "             If whText.Trim() <> " & Chr(34) & Chr(34)
                ret += vbNewLine & "                 Try"
                ret += vbNewLine & "                     ret = DB.ExecuteNonQuery(sql, trans, cmdPara)"
                ret += vbNewLine & "                     If ret.IsSuccess = False Then"
                ret += vbNewLine & "                         _error = MessageResources.MSGED001"
                ret += vbNewLine & "                     Else"
                ret += vbNewLine & "                        _information = MessageResources.MSGID001"
                ret += vbNewLine & "                        ret.InfoMessage = MessageResources.MSGID001"
                ret += vbNewLine & "                     End If"
                ret += vbNewLine & "                 Catch ex As ApplicationException"
                ret += vbNewLine & "                     _error = " & Chr(34) & "ApplicationException :" & Chr(34) & " & ex.Message & ex.ToString() & " & Chr(34) & "### SQL:" & Chr(34) & " & sql"

                ret += vbNewLine & "                     ret.IsSuccess = False"
                ret += vbNewLine & "                     ret.ErrorMessage = _error"
                ret += vbNewLine & "                     ret.SqlStatement = sql"
                ret += vbNewLine & "                 Catch ex As Exception"

                ret += vbNewLine & "                     _error =  " & Chr(34) & " Exception :" & Chr(34) & " & MessageResources.MSGEC103 & ex.ToString() & " & Chr(34) & "### SQL: " & Chr(34) & " & sql"
                ret += vbNewLine & "                     ret.IsSuccess = False"
                ret += vbNewLine & "                     ret.ErrorMessage = _error"
                ret += vbNewLine & "                     ret.SqlStatement = sql"
                ret += vbNewLine & "                 End Try"
                ret += vbNewLine & "             Else"
                ret += vbNewLine & "                 _error = MessageResources.MSGED003 & " & Chr(34) & "### SQL: " & Chr(34) & " & sql"
                ret += vbNewLine & "                 ret.IsSuccess = False"
                ret += vbNewLine & "                 ret.ErrorMessage = _error"
                ret += vbNewLine & "                 ret.SqlStatement = sql"
                ret += vbNewLine & "             End If"
                ret += vbNewLine
                ret += vbNewLine & "            Return ret"
                ret += vbNewLine & "        End Function"
                ret += vbNewLine
                ret += CreateParamVaribles()
                ret += vbNewLine
                ret += vbNewLine
            End If

            ret += vbNewLine & "        '/// Returns an indication whether the record of " & _tableName & " by specified condition is retrieved successfully."
            ret += vbNewLine & "        '/// <param name=whText>The condition specify the deleting record(s).</param>"
            ret += vbNewLine & "        '/// <param name=trans>The System.Data." & _databaseType & "Client." & _databaseType & "Transaction used by this System.Data." & _databaseType & "Client." & _databaseType & "Command.</param>"
            ret += vbNewLine & "        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>"
            ret += vbNewLine & "        Private Function doChkData(whText As String, trans As " & _databaseType & "Transaction, cmdPara() As " & _databaseType & "Parameter) As Boolean"
            ret += vbNewLine & "            Dim ret As Boolean = True"
            ret += vbNewLine & "            Dim tmpWhere As String = " & Chr(34) & " WHERE " & Chr(34) & " & whText"
            ret += vbNewLine & "            ClearData()"
            ret += vbNewLine & "            _haveData  = False"
            ret += vbNewLine & "            If whText.Trim() <> " & Chr(34) & Chr(34) & " Then"
            ret += vbNewLine & "                Dim Rdr As " & _databaseType & "DataReader"
            ret += vbNewLine & "                Try"
            ret += vbNewLine & "                    Rdr = DB.ExecuteReader(SqlSelect() & tmpWhere, trans, cmdPara)"
            ret += vbNewLine & "                    If Rdr.Read() Then"
            ret += vbNewLine & "                        _haveData = True"
            For Each dRow As DataRow In _columnTable.Rows
                Dim vTypeName As String = UCase(dRow("TYPE_NAME").ToString())
                ret += vbNewLine & "                        If Convert.IsDBNull(Rdr(" & Chr(34) & dRow("COLUMN_NAME").ToString() & Chr(34) & ")) = False Then"

                If vTypeName = "VARCHAR" Then
                    ret += " _" & dRow("COLUMN_NAME").ToString() & " = Rdr(" & Chr(34) & dRow("COLUMN_NAME").ToString() & Chr(34) & ").ToString()"
                ElseIf vTypeName = "VARCHAR2" Then
                    ret += " _" & dRow("COLUMN_NAME").ToString() & " = Rdr(" & Chr(34) & dRow("COLUMN_NAME").ToString() & Chr(34) & ").ToString()"
                ElseIf vTypeName = "CHAR" Then
                    ret += " _" & dRow("COLUMN_NAME").ToString() & " = Rdr(" & Chr(34) & dRow("COLUMN_NAME").ToString() & Chr(34) & ").ToString()"
                ElseIf vTypeName = "NVARCHAR" Then
                    ret += " _" & dRow("COLUMN_NAME").ToString() & " = Rdr(" & Chr(34) & dRow("COLUMN_NAME").ToString() & Chr(34) & ").ToString()"
                ElseIf vTypeName = "NCHAR" Then
                    ret += " _" & dRow("COLUMN_NAME").ToString() & " = Rdr(" & Chr(34) & dRow("COLUMN_NAME").ToString() & Chr(34) & ").ToString()"
                ElseIf vTypeName = "TEXT" Then
                    ret += " _" & dRow("COLUMN_NAME").ToString() & " = Rdr(" & Chr(34) & dRow("COLUMN_NAME").ToString() & Chr(34) & ").ToString()"
                ElseIf vTypeName = "FLOAT" Then
                    ret += " _" & dRow("COLUMN_NAME").ToString() & " = Convert.ToDouble(Rdr(" & Chr(34) & dRow("COLUMN_NAME").ToString() & Chr(34) & "))"
                ElseIf vTypeName = "DOUBLE" Then
                    ret += " _" & dRow("COLUMN_NAME").ToString() & " = Convert.ToDouble(Rdr(" & Chr(34) & dRow("COLUMN_NAME").ToString() & Chr(34) & "))"
                ElseIf vTypeName = "DECIMAL" Then
                    ret += " _" & dRow("COLUMN_NAME").ToString() & " = Convert.ToDouble(Rdr(" & Chr(34) & dRow("COLUMN_NAME").ToString() & Chr(34) & "))"
                ElseIf vTypeName = "BIGINT" Then
                    ret += " _" & dRow("COLUMN_NAME").ToString() & " = Convert.ToInt64(Rdr(" & Chr(34) & dRow("COLUMN_NAME").ToString() & Chr(34) & "))"
                ElseIf vTypeName = "BIGINT IDENTITY" Then
                    ret += " _" & dRow("COLUMN_NAME").ToString() & " = Convert.ToInt64(Rdr(" & Chr(34) & dRow("COLUMN_NAME").ToString() & Chr(34) & "))"
                ElseIf vTypeName = "INT" Then
                    ret += " _" & dRow("COLUMN_NAME").ToString() & " = Convert.ToInt32(Rdr(" & Chr(34) & dRow("COLUMN_NAME").ToString() & Chr(34) & "))"
                ElseIf vTypeName = "INT IDENTITY" Then
                    ret += " _" & dRow("COLUMN_NAME").ToString() & " = Convert.ToInt32(Rdr(" & Chr(34) & dRow("COLUMN_NAME").ToString() & Chr(34) & "))"
                ElseIf vTypeName = "SMALLINT" Then
                    ret += " _" & dRow("COLUMN_NAME").ToString() & " = Convert.ToInt16(Rdr(" & Chr(34) & dRow("COLUMN_NAME").ToString() & Chr(34) & "))"
                ElseIf vTypeName = "NUMBER" Then
                    ret += " _" & dRow("COLUMN_NAME").ToString() & " = Convert.ToDouble(Rdr(" & Chr(34) & dRow("COLUMN_NAME").ToString() & Chr(34) & "))"
                ElseIf vTypeName = "NUMBERIC" Then
                    ret += " _" & dRow("COLUMN_NAME").ToString() & " = Convert.ToDouble(Rdr(" & Chr(34) & dRow("COLUMN_NAME").ToString() & Chr(34) & "))"
                ElseIf vTypeName = "DATETIME" Then
                    ret += " _" & dRow("COLUMN_NAME").ToString() & " = Convert.ToDateTime(Rdr(" & Chr(34) & dRow("COLUMN_NAME").ToString() & Chr(34) & "))"
                ElseIf vTypeName = "DATETIME2" Then
                    ret += " _" & dRow("COLUMN_NAME").ToString() & " = Convert.ToDateTime(Rdr(" & Chr(34) & dRow("COLUMN_NAME").ToString() & Chr(34) & "))"
                ElseIf vTypeName = "DATE" Then
                    ret += " _" & dRow("COLUMN_NAME").ToString() & " = Convert.ToDateTime(Rdr(" & Chr(34) & dRow("COLUMN_NAME").ToString() & Chr(34) & "))"
                ElseIf vTypeName = "BIT" Then
                    ret += " _" & dRow("COLUMN_NAME").ToString() & " = Convert.ToBoolean(Rdr(" & Chr(34) & dRow("COLUMN_NAME").ToString() & Chr(34) & "))"
                ElseIf vTypeName = UCase("uniqueidentifier") Then
                    ret += " _" & dRow("COLUMN_NAME").ToString() & " = Rdr(" & Chr(34) & dRow("COLUMN_NAME").ToString() & Chr(34) & ").ToString()"
                ElseIf vTypeName = "IMAGE" Then
                    ret += " _" & dRow("COLUMN_NAME").ToString() & " = CType(Rdr(" & Chr(34) & dRow("COLUMN_NAME") & Chr(34) & "), Byte())"
                End If
            Next
            ret += vbNewLine & "                    Else"
            ret += vbNewLine & "                        ret = False"
            ret += vbNewLine & "                        _error = MessageResources.MSGEV002"
            ret += vbNewLine & "                    End If"
            ret += vbNewLine
            ret += vbNewLine & "                    Rdr.Close()"
            ret += vbNewLine & "                Catch ex As Exception"
            ret += vbNewLine & "                    ex.ToString()"
            ret += vbNewLine & "                    ret = False"
            ret += vbNewLine & "                    _error = MessageResources.MSGEC104 & " & Chr(34) & " #### " & Chr(34) & " & ex.ToString()"
            ret += vbNewLine & "                End Try"
            ret += vbNewLine & "            Else"
            ret += vbNewLine & "                ret = False"
            ret += vbNewLine & "                _error = MessageResources.MSGEV001"
            ret += vbNewLine & "            End If"
            ret += vbNewLine
            ret += vbNewLine & "            Return ret"
            ret += vbNewLine & "        End Function"
            ret += vbNewLine
            ret += vbNewLine
            ret += vbNewLine & "        '/// Returns an indication whether the record of " & _tableName & " by specified condition is retrieved successfully."
            ret += vbNewLine & "        '/// <param name=whText>The condition specify the deleting record(s).</param>"
            ret += vbNewLine & "        '/// <param name=trans>The System.Data." & _databaseType & "Client." & _databaseType & "Transaction used by this System.Data." & _databaseType & "Client." & _databaseType & "Command.</param>"
            ret += vbNewLine & "        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>"
            ret += vbNewLine & "        Private Function doGetData(whText As String, trans As " & _databaseType & "Transaction, cmdPara() As " & _databaseType & "Parameter) As " & _className & _objType
            ret += vbNewLine & "            ClearData()"
            ret += vbNewLine & "            _haveData  = False"
            ret += vbNewLine & "            If whText.Trim() <> " & Chr(34) & Chr(34) & " Then"
            ret += vbNewLine & "                Dim tmpWhere As String = " & Chr(34) & " WHERE " & Chr(34) & " & whText"
            ret += vbNewLine & "                Dim Rdr As " & _databaseType & "DataReader"
            ret += vbNewLine & "                Try"
            ret += vbNewLine & "                    Rdr = DB.ExecuteReader(SqlSelect() & tmpWhere, trans, cmdPara)"
            ret += vbNewLine & "                    If Rdr.Read() Then"
            ret += vbNewLine & "                        _haveData = True"
            For Each dRow As DataRow In _columnTable.Rows
                Dim vTypeName As String = UCase(dRow("TYPE_NAME").ToString())
                ret += vbNewLine & "                        If Convert.IsDBNull(Rdr(" & Chr(34) & dRow("COLUMN_NAME").ToString() & Chr(34) & ")) = False Then"

                If vTypeName = "VARCHAR" Then
                    ret += " _" & dRow("COLUMN_NAME").ToString() & " = Rdr(" & Chr(34) & dRow("COLUMN_NAME").ToString() & Chr(34) & ").ToString()"
                ElseIf vTypeName = "VARCHAR2" Then
                    ret += " _" & dRow("COLUMN_NAME").ToString() & " = Rdr(" & Chr(34) & dRow("COLUMN_NAME").ToString() & Chr(34) & ").ToString()"
                ElseIf vTypeName = "CHAR" Then
                    ret += " _" & dRow("COLUMN_NAME").ToString() & " = Rdr(" & Chr(34) & dRow("COLUMN_NAME").ToString() & Chr(34) & ").ToString()"
                ElseIf vTypeName = "NVARCHAR" Then
                    ret += " _" & dRow("COLUMN_NAME").ToString() & " = Rdr(" & Chr(34) & dRow("COLUMN_NAME").ToString() & Chr(34) & ").ToString()"
                ElseIf vTypeName = "NCHAR" Then
                    ret += " _" & dRow("COLUMN_NAME").ToString() & " = Rdr(" & Chr(34) & dRow("COLUMN_NAME").ToString() & Chr(34) & ").ToString()"
                ElseIf vTypeName = "TEXT" Then
                    ret += " _" & dRow("COLUMN_NAME").ToString() & " = Rdr(" & Chr(34) & dRow("COLUMN_NAME").ToString() & Chr(34) & ").ToString()"
                ElseIf vTypeName = "FLOAT" Then
                    ret += " _" & dRow("COLUMN_NAME").ToString() & " = Convert.ToDouble(Rdr(" & Chr(34) & dRow("COLUMN_NAME").ToString() & Chr(34) & "))"
                ElseIf vTypeName = "DOUBLE" Then
                    ret += " _" & dRow("COLUMN_NAME").ToString() & " = Convert.ToDouble(Rdr(" & Chr(34) & dRow("COLUMN_NAME").ToString() & Chr(34) & "))"
                ElseIf vTypeName = "DECIMAL" Then
                    ret += " _" & dRow("COLUMN_NAME").ToString() & " = Convert.ToDouble(Rdr(" & Chr(34) & dRow("COLUMN_NAME").ToString() & Chr(34) & "))"
                ElseIf vTypeName = "BIGINT" Then
                    ret += " _" & dRow("COLUMN_NAME").ToString() & " = Convert.ToInt64(Rdr(" & Chr(34) & dRow("COLUMN_NAME").ToString() & Chr(34) & "))"
                ElseIf vTypeName = "BIGINT IDENTITY" Then
                    ret += " _" & dRow("COLUMN_NAME").ToString() & " = Convert.ToInt64(Rdr(" & Chr(34) & dRow("COLUMN_NAME").ToString() & Chr(34) & "))"
                ElseIf vTypeName = "INT" Then
                    ret += " _" & dRow("COLUMN_NAME").ToString() & " = Convert.ToInt32(Rdr(" & Chr(34) & dRow("COLUMN_NAME").ToString() & Chr(34) & "))"
                ElseIf vTypeName = "INT IDENTITY" Then
                    ret += " _" & dRow("COLUMN_NAME").ToString() & " = Convert.ToInt32(Rdr(" & Chr(34) & dRow("COLUMN_NAME").ToString() & Chr(34) & "))"
                ElseIf vTypeName = "SMALLINT" Then
                    ret += " _" & dRow("COLUMN_NAME").ToString() & " = Convert.ToInt16(Rdr(" & Chr(34) & dRow("COLUMN_NAME").ToString() & Chr(34) & "))"
                ElseIf vTypeName = "NUMBER" Then
                    ret += " _" & dRow("COLUMN_NAME").ToString() & " = Convert.ToDouble(Rdr(" & Chr(34) & dRow("COLUMN_NAME").ToString() & Chr(34) & "))"
                ElseIf vTypeName = "NUMBERIC" Then
                    ret += " _" & dRow("COLUMN_NAME").ToString() & " = Convert.ToDouble(Rdr(" & Chr(34) & dRow("COLUMN_NAME").ToString() & Chr(34) & "))"
                ElseIf vTypeName = "DATETIME" Then
                    ret += " _" & dRow("COLUMN_NAME").ToString() & " = Convert.ToDateTime(Rdr(" & Chr(34) & dRow("COLUMN_NAME").ToString() & Chr(34) & "))"
                ElseIf vTypeName = "DATETIME2" Then
                    ret += " _" & dRow("COLUMN_NAME").ToString() & " = Convert.ToDateTime(Rdr(" & Chr(34) & dRow("COLUMN_NAME").ToString() & Chr(34) & "))"
                ElseIf vTypeName = "DATE" Then
                    ret += " _" & dRow("COLUMN_NAME").ToString() & " = Convert.ToDateTime(Rdr(" & Chr(34) & dRow("COLUMN_NAME").ToString() & Chr(34) & "))"
                ElseIf vTypeName = "BIT" Then
                    ret += " _" & dRow("COLUMN_NAME").ToString() & " = Convert.ToBoolean(Rdr(" & Chr(34) & dRow("COLUMN_NAME").ToString() & Chr(34) & "))"
                ElseIf vTypeName = UCase("uniqueidentifier") Then
                    ret += " _" & dRow("COLUMN_NAME").ToString() & " = Rdr(" & Chr(34) & dRow("COLUMN_NAME").ToString() & Chr(34) & ").ToString()"
                ElseIf vTypeName = "IMAGE" Then
                    ret += " _" & dRow("COLUMN_NAME").ToString() & " = CType(Rdr(" & Chr(34) & dRow("COLUMN_NAME") & Chr(34) & "), Byte())"
                End If
            Next
            'ret += GetChildTable(False)

            ret += vbNewLine & "                    Else"
            ret += vbNewLine & "                        _error = MessageResources.MSGEV002"
            ret += vbNewLine & "                    End If"
            ret += vbNewLine
            ret += vbNewLine & "                    Rdr.Close()"
            ret += vbNewLine & "                Catch ex As Exception"
            ret += vbNewLine & "                    ex.ToString()"
            ret += vbNewLine & "                    _error = MessageResources.MSGEC104 & " & Chr(34) & " #### " & Chr(34) & " & ex.ToString()"
            ret += vbNewLine & "                End Try"
            ret += vbNewLine & "            Else"
            ret += vbNewLine & "                _error = MessageResources.MSGEV001"
            ret += vbNewLine & "            End If"
            ret += vbNewLine & "            Return Me"
            ret += vbNewLine & "        End Function"
            ret += vbNewLine
            ret += vbNewLine
            
            Return ret
        End Function

        Private Function CreateParamVaribles() As String
            Dim ret As String = ""

            Dim dt As New DataTable
            dt = _columnTable.Copy

            If dt.Rows.Count > 0 Then
                Dim i As Integer = 0

                ret += vbNewLine & "        Private Function SetParameterData() As SqlParameter()"
                ret += vbNewLine & "            Dim cmbParam(@@pCount) As SqlParameter"
                For Each dRow As DataRow In dt.Rows
                    Dim FieldIsNull As String = dRow("NULLABLE").ToString()
                    Dim vColName As String = dRow("COLUMN_NAME").ToString.ToUpper
                    Dim SqlType As String = ""
                    'retFieldType = IIf(FieldIsNull = "1", GetFieldType(dRow), "DateTime")
                    Dim vTypeName As String = dRow("TYPE_NAME").ToString.ToUpper
                    If vTypeName = "VARCHAR" Then
                        ret += vbNewLine & "            cmbParam(" & i & ") = New SqlParameter(" & Chr(34) & "@_" & vColName & Chr(34) & ", SqlDbType.VarChar)"
                        If FieldIsNull = "0" Then  'Not null
                            ret += vbNewLine & "            cmbParam(" & i & ").Value = _" & vColName & ".Trim"
                        Else
                            'Null
                            ret += vbNewLine & "            If _" & vColName & ".Trim <> """" Then "
                            ret += vbNewLine & "                cmbParam(" & i & ").Value = _" & vColName & ".Trim"
                            ret += vbNewLine & "            Else"
                            ret += vbNewLine & "                cmbParam(" & i & ").Value = DBNull.value"
                            ret += vbNewLine & "            End If"
                        End If
                    ElseIf vTypeName = "CHAR" Then
                        ret += vbNewLine & "            cmbParam(" & i & ") = New SqlParameter(" & Chr(34) & "@_" & vColName & Chr(34) & ", SqlDbType.Char)"
                        If FieldIsNull = "0" Then 'Not null
                            ret += vbNewLine & "            cmbParam(" & i & ").Value = _" & vColName
                        Else
                            ret += vbNewLine & "            If _" & vColName & ".Value <> """" Then "
                            ret += vbNewLine & "                cmbParam(" & i & ").Value = _" & vColName & ".Value"
                            ret += vbNewLine & "            Else"
                            ret += vbNewLine & "                cmbParam(" & i & ").Value = DBNull.value"
                            ret += vbNewLine & "            End IF"
                        End If
                    ElseIf vTypeName = "NVARCHAR" Then
                        ret += vbNewLine & "            cmbParam(" & i & ") = New SqlParameter(" & Chr(34) & "@_" & vColName & Chr(34) & ", SqlDbType.NVarChar)"
                        If FieldIsNull = "0" Then 'Not null
                            ret += vbNewLine & "            cmbParam(" & i & ").Value = _" & vColName & ".Trim"
                        Else
                            ret += vbNewLine & "            If _" & vColName & ".Trim <> """" Then "
                            ret += vbNewLine & "                cmbParam(" & i & ").Value = _" & vColName & ".Trim"
                            ret += vbNewLine & "            Else"
                            ret += vbNewLine & "                cmbParam(" & i & ").Value = DBNull.value"
                            ret += vbNewLine & "            End IF"
                        End If
                    ElseIf vTypeName = "NCHAR" Then
                        ret += vbNewLine & "            cmbParam(" & i & ") = New SqlParameter(" & Chr(34) & "@_" & vColName & Chr(34) & ", SqlDbType.NChar)"
                        If FieldIsNull = "0" Then 'Not null
                            ret += vbNewLine & "            cmbParam(" & i & ").Value = _" & vColName
                        Else
                            ret += vbNewLine & "            If _" & vColName & ".Value <> """" Then "
                            ret += vbNewLine & "                cmbParam(" & i & ").Value = _" & vColName & ".Value"
                            ret += vbNewLine & "            Else"
                            ret += vbNewLine & "                cmbParam(" & i & ").Value = DBNull.value"
                            ret += vbNewLine & "            End IF"
                        End If
                    ElseIf vTypeName = "TEXT" Then
                        ret += vbNewLine & "            cmbParam(" & i & ") = New SqlParameter(" & Chr(34) & "@_" & vColName & Chr(34) & ", SqlDbType.Text)"
                        If FieldIsNull = "0" Then 'Not null
                            ret += vbNewLine & "            cmbParam(" & i & ").Value = _" & vColName & ".Trim"
                        Else
                            ret += vbNewLine & "            If _" & vColName & " IsNot Nothing Then "
                            ret += vbNewLine & "                cmbParam(" & i & ").Value = _" & vColName & ".Trim"
                            ret += vbNewLine & "            Else"
                            ret += vbNewLine & "                cmbParam(" & i & ").Value = DBNull.value"
                            ret += vbNewLine & "            End IF"
                        End If
                    ElseIf vTypeName = "FLOAT" Then
                        ret += vbNewLine & "            cmbParam(" & i & ") = New SqlParameter(" & Chr(34) & "@_" & vColName & Chr(34) & ", SqlDbType.Float)"
                        If FieldIsNull = "0" Then 'Not null
                            ret += vbNewLine & "            cmbParam(" & i & ").Value = _" & vColName
                        Else
                            ret += vbNewLine & "            If _" & vColName & " IsNot Nothing Then "
                            ret += vbNewLine & "                cmbParam(" & i & ").Value = _" & vColName & ".Value"
                            ret += vbNewLine & "            Else"
                            ret += vbNewLine & "                cmbParam(" & i & ").Value = DBNull.value"
                            ret += vbNewLine & "            End IF"
                        End If
                    ElseIf vTypeName = "DOUBLE" Then
                        ret += vbNewLine & "            cmbParam(" & i & ") = New SqlParameter(" & Chr(34) & "@_" & vColName & Chr(34) & ", SqlDbType.Decimal)"
                        If FieldIsNull = "0" Then 'Not null
                            ret += vbNewLine & "            cmbParam(" & i & ").Value = _" & vColName
                        Else
                            ret += vbNewLine & "            If _" & vColName & " IsNot Nothing Then "
                            ret += vbNewLine & "                cmbParam(" & i & ").Value = _" & vColName & ".Value"
                            ret += vbNewLine & "            Else"
                            ret += vbNewLine & "                cmbParam(" & i & ").Value = DBNull.value"
                            ret += vbNewLine & "            End IF"
                        End If
                    ElseIf vTypeName = "DECIMAL" Then
                        ret += vbNewLine & "            cmbParam(" & i & ") = New SqlParameter(" & Chr(34) & "@_" & vColName & Chr(34) & ", SqlDbType.Decimal)"
                        If FieldIsNull = "0" Then 'Not null
                            ret += vbNewLine & "            cmbParam(" & i & ").Value = _" & vColName
                        Else
                            ret += vbNewLine & "            If _" & vColName & " IsNot Nothing Then "
                            ret += vbNewLine & "                cmbParam(" & i & ").Value = _" & vColName & ".Value"
                            ret += vbNewLine & "            Else"
                            ret += vbNewLine & "                cmbParam(" & i & ").Value = DBNull.value"
                            ret += vbNewLine & "            End IF"
                        End If
                    ElseIf vTypeName = "BIGINT" Then
                        ret += vbNewLine & "            cmbParam(" & i & ") = New SqlParameter(" & Chr(34) & "@_" & vColName & Chr(34) & ", SqlDbType.BigInt)"
                        If FieldIsNull = "0" Then 'Not null
                            ret += vbNewLine & "            cmbParam(" & i & ").Value = _" & vColName
                        Else
                            ret += vbNewLine & "            If _" & vColName & " IsNot Nothing Then "
                            ret += vbNewLine & "                cmbParam(" & i & ").Value = _" & vColName & ".Value"
                            ret += vbNewLine & "            Else"
                            ret += vbNewLine & "                cmbParam(" & i & ").Value = DBNull.value"
                            ret += vbNewLine & "            End IF"
                        End If
                    ElseIf vTypeName = "BIGINT IDENTITY" Then
                        ret += vbNewLine & "            cmbParam(" & i & ") = New SqlParameter(" & Chr(34) & "@_" & vColName & Chr(34) & ", SqlDbType.BigInt)"
                        If FieldIsNull = "0" Then 'Not null
                            ret += vbNewLine & "            cmbParam(" & i & ").Value = _" & vColName
                        Else
                            ret += vbNewLine & "            If _" & vColName & " IsNot Nothing Then "
                            ret += vbNewLine & "                cmbParam(" & i & ").Value = _" & vColName & ".Value"
                            ret += vbNewLine & "            Else"
                            ret += vbNewLine & "                cmbParam(" & i & ").Value = DBNull.value"
                            ret += vbNewLine & "            End IF"
                        End If
                    ElseIf vTypeName = "INT" Then
                        ret += vbNewLine & "            cmbParam(" & i & ") = New SqlParameter(" & Chr(34) & "@_" & vColName & Chr(34) & ", SqlDbType.Int)"
                        If FieldIsNull = "0" Then 'Not null
                            ret += vbNewLine & "            cmbParam(" & i & ").Value = _" & vColName
                        Else
                            ret += vbNewLine & "            If _" & vColName & " IsNot Nothing Then "
                            ret += vbNewLine & "                cmbParam(" & i & ").Value = _" & vColName & ".Value"
                            ret += vbNewLine & "            Else"
                            ret += vbNewLine & "                cmbParam(" & i & ").Value = DBNull.value"
                            ret += vbNewLine & "            End IF"
                        End If
                    ElseIf vTypeName = "INT IDENTITY" Then
                        ret += vbNewLine & "            cmbParam(" & i & ") = New SqlParameter(" & Chr(34) & "@_" & vColName & Chr(34) & ", SqlDbType.Int)"
                        If FieldIsNull = "0" Then 'Not null
                            ret += vbNewLine & "            cmbParam(" & i & ").Value = _" & vColName
                        Else
                            ret += vbNewLine & "            If _" & vColName & " IsNot Nothing Then "
                            ret += vbNewLine & "                cmbParam(" & i & ").Value = _" & vColName & ".Value"
                            ret += vbNewLine & "            Else"
                            ret += vbNewLine & "                cmbParam(" & i & ").Value = DBNull.value"
                            ret += vbNewLine & "            End IF"
                        End If
                    ElseIf vTypeName = "SMALLINT" Then
                        ret += vbNewLine & "            cmbParam(" & i & ") = New SqlParameter(" & Chr(34) & "@_" & vColName & Chr(34) & ", SqlDbType.SmallInt)"
                        If FieldIsNull = "0" Then 'Not null
                            ret += vbNewLine & "            cmbParam(" & i & ").Value = _" & vColName
                        Else
                            ret += vbNewLine & "            If _" & vColName & " IsNot Nothing Then "
                            ret += vbNewLine & "                cmbParam(" & i & ").Value = _" & vColName & ".Value"
                            ret += vbNewLine & "            Else"
                            ret += vbNewLine & "                cmbParam(" & i & ").Value = DBNull.value"
                            ret += vbNewLine & "            End IF"
                        End If
                    ElseIf vTypeName = "NUMBER" Then
                        ret += vbNewLine & "            cmbParam(" & i & ") = New SqlParameter(" & Chr(34) & "@_" & vColName & Chr(34) & ", SqlDbType.Decimal)"
                        If FieldIsNull = "0" Then 'Not null
                            ret += vbNewLine & "            cmbParam(" & i & ").Value = _" & vColName
                        Else
                            ret += vbNewLine & "            If _" & vColName & " IsNot Nothing Then "
                            ret += vbNewLine & "                cmbParam(" & i & ").Value = _" & vColName & ".Value"
                            ret += vbNewLine & "            Else"
                            ret += vbNewLine & "                cmbParam(" & i & ").Value = DBNull.value"
                            ret += vbNewLine & "            End IF"
                        End If
                    ElseIf vTypeName = "NUMBERIC" Then
                        ret += vbNewLine & "            cmbParam(" & i & ") = New SqlParameter(" & Chr(34) & "@_" & vColName & Chr(34) & ", SqlDbType.Decimal)"
                        If FieldIsNull = "0" Then 'Not null
                            ret += vbNewLine & "            cmbParam(" & i & ").Value = _" & vColName
                        Else
                            ret += vbNewLine & "            If _" & vColName & " IsNot Nothing Then "
                            ret += vbNewLine & "                cmbParam(" & i & ").Value = _" & vColName & ".Value"
                            ret += vbNewLine & "            Else"
                            ret += vbNewLine & "                cmbParam(" & i & ").Value = DBNull.value"
                            ret += vbNewLine & "            End IF"
                        End If
                    ElseIf vTypeName = "DATETIME" Then
                        ret += vbNewLine & "            cmbParam(" & i & ") = New SqlParameter(" & Chr(34) & "@_" & vColName & Chr(34) & ", SqlDbType.DateTime)"
                        If FieldIsNull = "0" Then  'Not null
                            ret += vbNewLine & "            cmbParam(" & i & ").Value = _" & vColName
                        Else
                            ret += vbNewLine & "            If _" & vColName & IIf(FieldIsNull = "1", ".Value", "") & ".Year > 1 Then "
                            ret += vbNewLine & "                cmbParam(" & i & ").Value = _" & vColName & IIf(FieldIsNull = "1", ".Value", "")
                            ret += vbNewLine & "            Else"
                            ret += vbNewLine & "                cmbParam(" & i & ").Value = DBNull.value"
                            ret += vbNewLine & "            End If"
                        End If
                    ElseIf vTypeName = "DATETIME2" Then
                        ret += vbNewLine & "            cmbParam(" & i & ") = New SqlParameter(" & Chr(34) & "@_" & vColName & Chr(34) & ", SqlDbType.DateTime2)"
                        If FieldIsNull = "0" Then  'Not null
                            ret += vbNewLine & "            cmbParam(" & i & ").Value = _" & vColName
                        Else
                            ret += vbNewLine & "            If _" & vColName & IIf(FieldIsNull = "1", ".Value", "") & ".Year > 1 Then "
                            ret += vbNewLine & "                cmbParam(" & i & ").Value = _" & vColName & IIf(FieldIsNull = "1", ".Value", "")
                            ret += vbNewLine & "            Else"
                            ret += vbNewLine & "                cmbParam(" & i & ").Value = DBNull.value"
                            ret += vbNewLine & "            End If"
                        End If
                    ElseIf vTypeName = "DATE" Then
                        ret += vbNewLine & "            cmbParam(" & i & ") = New SqlParameter(" & Chr(34) & "@_" & vColName & Chr(34) & ", SqlDbType.Date)"
                        If FieldIsNull = "0" Then  'Not null
                            ret += vbNewLine & "            cmbParam(" & i & ").Value = _" & vColName
                        Else
                            ret += vbNewLine & "            If _" & vColName & IIf(FieldIsNull = "1", ".Value", "") & ".Year > 1 Then "
                            ret += vbNewLine & "                cmbParam(" & i & ").Value = _" & vColName & IIf(FieldIsNull = "1", ".Value", "")
                            ret += vbNewLine & "            Else"
                            ret += vbNewLine & "                cmbParam(" & i & ").Value = DBNull.value"
                            ret += vbNewLine & "            End If"
                        End If
                    ElseIf vTypeName = "BIT" Then
                        'ret += vbNewLine & "            cmbParam(" & i & ") = New SqlParameter(" & Chr(34) & "@_" & vColName & Chr(34) & ", SqlDbType.Bit)"
                        'ret += vbNewLine & "            If _" & vColName & IIf(FieldIsNull = "1", ".Value", "") & ".Year > 1 Then "
                        'ret += vbNewLine & "                cmbParam(" & i & ").Value = _" & vColName & IIf(FieldIsNull = "1", ".Value", "")
                        'ret += vbNewLine & "            Else"
                        'ret += vbNewLine & "                cmbParam(" & i & ").Value = DBNull.value"
                        'ret += vbNewLine & "            End If"
                    ElseIf vTypeName = UCase("uniqueidentifier") Then
                        'SqlType = "SqlDbType.UniqueIdentifier"
                    ElseIf vTypeName = "IMAGE" Then
                        If FieldIsNull = "0" Then 'Not null
                            ret += vbNewLine & "            cmbParam(" & i & ") = New SqlParameter(" & Chr(34) & "@_" & vColName & Chr(34) & ",SqlDbType.Image, _" & vColName & ".Length)"
                            ret += vbNewLine & "            cmbParam(" & i & ").Value = _" & vColName
                        Else
                            ret += vbNewLine & "            If _" & vColName & " IsNot Nothing Then "
                            ret += vbNewLine & "                cmbParam(" & i & ") = New SqlParameter(" & Chr(34) & "@_" & vColName & Chr(34) & ",SqlDbType.Image, _" & vColName & ".Length)"
                            ret += vbNewLine & "                cmbParam(" & i & ").Value = _" & vColName
                            ret += vbNewLine & "            Else"
                            ret += vbNewLine & "                cmbParam(" & i & ") = New SqlParameter(" & Chr(34) & "@_" & vColName & Chr(34) & ", SqlDbType.Image)"
                            ret += vbNewLine & "                cmbParam(" & i & ").Value = DBNull.value"
                            ret += vbNewLine & "            End If"
                        End If
                    End If
                    ret += vbNewLine
                    i += 1

                Next
                ret += vbNewLine & "            Return cmbParam"
                ret += vbNewLine & "        End Function"
                ret = ret.Replace("@@pCount", (i - 1))


            End If
            Return ret
        End Function


        'Private Function CreateImgParam() As String
        '    Dim ret As String = ""

        '    Dim dt As New DataTable
        '    dt = _columnTable.Copy
        '    dt.DefaultView.RowFilter = "TYPE_NAME = 'image' "

        '    If dt.DefaultView.Count > 0 Then
        '        Dim i As Integer = 0
        '        ret += vbNewLine & "                        Dim cmbParam(@@pCount) As SqlParameter"
        '        For Each dRow As DataRowView In dt.DefaultView
        '            Dim vColName As String = dRow("COLUMN_NAME").ToString.ToUpper
        '            If dRow("TYPE_NAME").ToString.ToUpper = "IMAGE" Then
        '                ret += vbNewLine & "                        If _" & vColName & " IsNot Nothing Then "
        '                ret += vbNewLine & "                            cmbParam(" & i & ") = New SqlParameter(" & Chr(34) & "@_" & vColName & Chr(34) & ", SqlDbType.Image, _" & vColName & ".Length)"
        '                ret += vbNewLine & "                            cmbParam(0).Value = _" & vColName
        '                ret += vbNewLine & "                        Else"
        '                ret += vbNewLine & "                            cmbParam(" & i & ") = New SqlParameter(" & Chr(34) & "@_" & vColName & Chr(34) & ", SqlDbType.Image)"
        '                ret += vbNewLine & "                            cmbParam(0).Value = DBNull.value"
        '                ret += vbNewLine & "                        End If"
        '                i += 1
        '            End If
        '        Next
        '        ret = ret.Replace("@@pCount", (i - 1))
        '    End If
        '    Return ret
        'End Function
        

        Private Function GetAssignType(ByVal constraintKeys As String) As String
            Dim ret As String = ""

            If InStr(constraintKeys, ",") > 0 Then
                Dim paramName() As String = constraintKeys.Split(",")

                For i As Integer = 0 To paramName.Count - 1
                    Dim constraintField As String = UCase(paramName(i).Trim())
                    _columnTable.DefaultView.RowFilter = "COLUMN_NAME='" & constraintField & "'"

                    If _columnTable.DefaultView.Count > 0 Then
                        Dim assType As String = constraintField & " = @_" & constraintField.Trim()
                        If ret = "" Then
                            ret += Chr(34) & assType
                        Else
                            ret += " AND " & assType
                        End If
                    End If
                Next
            Else
                _columnTable.DefaultView.RowFilter = "COLUMN_NAME='" & constraintKeys.Trim() & "'"
                ret = Chr(34) & UCase(constraintKeys) & " = @_" & UCase(constraintKeys.Trim())
            End If
            Return ret
        End Function

        Private Function GetConstraintFieldDataType(ByVal constraintKeys As String) As String
            Dim ret As String = ""
            If InStr(constraintKeys, ",") > 0 Then
                Dim paramName() As String = constraintKeys.Split(",")

                For i As Integer = 0 To paramName.Count - 1
                    Dim constraintField As String = UCase(paramName(i).Trim())
                    _columnTable.DefaultView.RowFilter = "COLUMN_NAME='" & constraintField & "'"

                    If _columnTable.DefaultView.Count > 0 Then

                        If ret = "" Then
                            ret += "c" & constraintField & " As " & GetDataType(_columnTable.DefaultView(0).Item("TYPE_NAME").ToString())
                        Else
                            ret += ", c" & constraintField & " As " & GetDataType(_columnTable.DefaultView(0).Item("TYPE_NAME").ToString())
                        End If
                    End If
                Next
            Else
                _columnTable.DefaultView.RowFilter = "COLUMN_NAME='" & UCase(constraintKeys.Trim()) & "'"
                ret = "c" & UCase(constraintKeys) & " As " & GetDataType(_columnTable.DefaultView(0).Item("TYPE_NAME").ToString())
            End If
            Return ret
        End Function
        Private Function GetChildTable(ByVal IsPara As Boolean) As String
            Dim ret As String = ""
            ret += vbNewLine
            ret += vbNewLine & "                        'Generate Item For Child Table"
            If _childTableKey.Rows.Count > 0 Then
                For i As Integer = 0 To _childTableKey.Rows.Count - 1
                    Dim dRow As DataRow = _childTableKey.Rows(i)
                    Dim fkTableName As String = dRow("FKTABLE_NAME").ToString.ToUpper
                    Dim fkColumnName As String = dRow("FKCOLUMN_NAME").ToString.ToLower
                    Dim fieldType As String = ""
                    For Each Str As String In Split(fkTableName.ToLower, "_")
                        fieldType += Left(Str, 1).ToUpper & Right(Str, Str.Length - 1)
                    Next
                    ret += vbNewLine & "                        'Child Table Name : " & fkTableName & " Column :" & fkColumnName
                    ret += vbNewLine & "                        Dim " & fieldType & "_" & fkColumnName & "Item As New " & fieldType & _objType
                    ret += vbNewLine & "                        _" & fkTableName & "_" & fkColumnName & " = " & fieldType & "_" & fkColumnName & "Item.GetDataList(" & Chr(34) & fkColumnName & " = " & Chr(34) & " & " & IIf(IsPara = False, "_" & Constant.FieldName.ID, "ret.id") & ", " & Chr(34) & Chr(34) & ", Nothing)"
                    If IsPara = True Then
                        ret += vbNewLine & "                        ret.CHILD_" & fkTableName & "_" & fkColumnName & " = _" & fkTableName & "_" & fkColumnName
                    End If
                    ret += vbNewLine
                Next
            End If

            Return ret
        End Function
        Private Function GenerateChildTable(ByVal IsPara As Boolean) As String
            Dim ret As String = ""
            If IsPara = False Then


                If _childTableKey.Rows.Count > 0 Then
                    Dim attr As String = ""
                    Dim prop As String = ""

                    For Each dRow As DataRow In _childTableKey.Rows
                        Dim fkTableName As String = dRow("FKTABLE_NAME").ToString.ToUpper '& "_" & dRow("FKCOLUMN_NAME").ToString.ToLower
                        Dim fkColumnName As String = dRow("FKCOLUMN_NAME").ToString.ToLower
                        attr += vbNewLine & "       Dim _" & fkTableName & "_" & fkColumnName & " As DataTable"

                        prop += vbNewLine & "       Public Property CHILD_" & fkTableName & "_" & fkColumnName & "() As DataTable"
                        prop += vbNewLine & "           Get"

                        Dim fieldType As String = ""
                        For Each Str As String In Split(fkTableName.ToLower, "_")
                            fieldType += Left(Str, 1).ToUpper & Right(Str, Str.Length - 1)
                        Next

                        prop += vbNewLine & "               'Child Table Name : " & fkTableName & " Column :" & fkColumnName
                        prop += vbNewLine & "               Dim trans As New " & _objType & ".ConnectDB.TransactionDB"
                        'prop += vbNewLine & "               trans.CreateTransaction()"
                        prop += vbNewLine & "               Dim " & fieldType & "Item As New " & fieldType & _objType
                        prop += vbNewLine & "               _" & fkTableName & "_" & fkColumnName & " = " & fieldType & "Item.GetDataList(" & Chr(34) & fkColumnName & " = " & Chr(34) & " & " & "_" & Constant.FieldName.ID & ", " & Chr(34) & Chr(34) & ", trans.Trans)"
                        prop += vbNewLine & "               trans.CommitTransaction()"
                        prop += vbNewLine & "               " & fieldType & "Item = Nothing"
                        prop += vbNewLine & "               Return _" & fkTableName & "_" & fkColumnName
                        prop += vbNewLine & "           End Get"
                        prop += vbNewLine & "           Set(ByVal value As DataTable)"
                        prop += vbNewLine & "               _" & fkTableName & "_" & fkColumnName & " = value"
                        prop += vbNewLine & "           End Set"
                        prop += vbNewLine & "       End Property"
                    Next
                    ret += vbNewLine & "            'Define Child Table "
                    ret += vbNewLine & attr
                    ret += vbNewLine & prop
                End If
            End If
            Return ret
        End Function

        Private Function GetConstraintFieldName(ByVal constraintKeys As String) As String
            Dim ret As String = ""
            If InStr(constraintKeys, ",") > 0 Then
                _uniqueColumnTable.DefaultView.RowFilter = "constraint_keys='" & UCase(constraintKeys.Trim()) & "'"
                ret = UCase(Replace(constraintKeys, ",", "_"))
            Else
                ret = UCase(constraintKeys)
            End If
            Return ret
        End Function

    End Class
End Namespace

