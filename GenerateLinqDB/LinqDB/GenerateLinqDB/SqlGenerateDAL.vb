Imports System
Imports System.Collections.Generic
Imports System.Configuration
Imports System.Data
Imports System.Data.SqlClient
Imports System.Text
Imports LinqDB.ConnectDB



Namespace GenerateLinqDB
    Public Class SqlGenerateDAL

        Dim _DataSource As String = ""
        Dim _DataBaseName As String = ""
        Dim _UserID As String = ""
        Dim _Password As String = ""
        Dim _TableName As String = ""
        Dim _DatabaseType As String = ""
        Dim _DatabaseClass As String = ""
        Dim _ErrorTableNotFound As String = MessageResources.MSGEC014

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
                Return _DataBaseName
            End Get
            Set(ByVal value As String)
                _DataBaseName = value
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
        Public Property DatabaseType() As String
            Get
                Return _DatabaseType
            End Get
            Set(ByVal value As String)
                _DatabaseType = value
            End Set
        End Property
        Public Property DatabaseClass As String
            Get
                Return _DatabaseClass
            End Get
            Set(ByVal value As String)
                _DatabaseClass = value
            End Set
        End Property

        Private ReadOnly Property ConnectionString() As String
            Get
                Return "Data Source=" + _DataSource + ";Initial Catalog=" + _DataBaseName + ";Persist Security Info=True;User ID=" + _UserID + ";password=" + _Password
            End Get
        End Property

        Public Function GetPKColumn() As DataTable
            Dim dt As New DataTable
            dt.Columns.Add("column_name")
            dt.Columns.Add("constraint_type")
            dt.Columns.Add("type_name")
            dt.Columns.Add("pk_name")

            Try
                Dim conn As SqlConnection = SqlDB.GetConnection(ConnectionString)
                Dim tmpTable As DataTable = SqlDB.ExecuteTable("EXEC SP_PKEYS " & SqlDB.SetString(_TableName), conn)
                For Each dRow As DataRow In tmpTable.Rows
                    Dim dr As DataRow = dt.NewRow
                    dr("column_name") = dRow("column_name").ToString()
                    dr("constraint_type") = "P"
                    dr("pk_name") = dRow("pk_name").ToString()

                    Dim typeTable As DataTable = SqlDB.ExecuteTable("EXEC SP_COLUMNS " & SqlDB.SetString(_TableName), conn)
                    For Each tRow As DataRow In typeTable.Rows
                        If tRow("column_name").ToString() = dRow("column_name").ToString() Then
                            dr("type_name") = tRow("type_name").ToString()
                        End If
                    Next
                    dt.Rows.Add(dr)

                Next

                conn.Close()
                conn.Dispose()

            Catch ex As Exception
            End Try

            Return dt
        End Function
        Public Function GetUQColumn() As DataTable
            Dim dt As New DataTable
            dt.Columns.Add("constraint_keys")
            dt.Columns.Add("constraint_type")
            dt.Columns.Add("constraint_name")

            Dim conn As SqlConnection = SqlDB.GetConnection(ConnectionString)
            Dim sql As String = "exec sp_indexcolumns_managed " & SqlDB.SetString(_DataBaseName) & ",null, " & SqlDB.SetString(_TableName)
            'Dim da As New SqlDataAdapter("EXEC SP_HELPCONSTRAINT " & SqlDB.SetString(_TableName), conn)
            Dim da As New SqlDataAdapter(sql, conn)
            Dim dtIndex As New DataTable
            da.Fill(dtIndex)

            For i As Int16 = dtIndex.Rows.Count - 1 To 0 Step -1
                If dtIndex.Rows(i)("COLUMN_NAME") = "msrepl_tran_version" Then
                    dtIndex.Rows.RemoveAt(i)
                End If
            Next
            For i As Int16 = dtIndex.Rows.Count - 1 To 0 Step -1
                If dtIndex.Rows(i)("COLUMN_NAME") = "rowguid" Then
                    dtIndex.Rows.RemoveAt(i)
                End If
            Next

            Dim sqlPK As String = "exec sp_pkeys " & SqlDB.SetString(_TableName)
            Dim daPK As New SqlDataAdapter(sqlPK, conn)
            Dim dtPK As New DataTable
            daPK.Fill(dtPK)
            Dim pkCol As String = ""
            If dtPK.Rows.Count > 0 Then
                pkCol = dtPK.Rows(0)("column_name")
            End If

            Dim tmpTable As DataTable = dtIndex
            If tmpTable.Rows.Count > 0 Then
                Dim constraintName As String = ""
                For Each dRow As DataRow In tmpTable.Rows
                    If constraintName <> UCase(dRow("CONSTRAINT_NAME").ToString().Trim()) And dRow("column_name") <> pkCol Then

                        constraintName = UCase(dRow("CONSTRAINT_NAME").ToString().Trim())
                        tmpTable.DefaultView.RowFilter = "CONSTRAINT_NAME='" & constraintName & "'"
                        Dim dr As DataRow = dt.NewRow
                        If tmpTable.DefaultView.Count > 1 Then   'ถ้า Constraints นั้นมีฟิลด์ที่เกี่ยวข้องมากกว่า 1 ฟิลด์ ให้อ้างอิงจากชื่อ Constraints
                            Dim constraintKeys As String = ""
                            For i As Integer = 0 To tmpTable.DefaultView.Count - 1
                                If constraintKeys = "" Then
                                    constraintKeys = tmpTable.DefaultView(i).Item("COLUMN_NAME").ToString()
                                Else
                                    constraintKeys += "," & tmpTable.DefaultView(i).Item("COLUMN_NAME").ToString()
                                End If
                            Next

                            dr("constraint_keys") = constraintKeys
                            dr("constraint_type") = "U"
                            dr("constraint_name") = UCase(dRow("CONSTRAINT_NAME").ToString().Trim())
                        Else
                            dr("constraint_keys") = dRow("COLUMN_NAME").ToString()
                            dr("constraint_type") = "U"
                            dr("constraint_name") = dRow("COLUMN_NAME").ToString()
                        End If

                        dt.Rows.Add(dr)
                    End If
                Next
            End If

            conn.Close()
            conn.Dispose()

            Return dt
        End Function

        Public Function GetTableColumn() As DataTable
            Dim sql As String = "EXEC SP_COLUMNS " & SqlDB.SetString(_TableName)
            Dim dt As DataTable = SqlDB.ExecuteTable(sql, SqlDB.GetConnection(ConnectionString))
            dt.DefaultView.Sort = "COLUMN_NAME"
            If dt.Rows.Count = 0 Then
                Throw New ApplicationException(String.Format(_ErrorTableNotFound, _TableName))
            Else
                For i As Int16 = dt.Rows.Count - 1 To 0 Step -1
                    If dt.Rows(i)("COLUMN_NAME") = "msrepl_tran_version" Then
                        dt.Rows.RemoveAt(i)
                    End If
                Next
                For i As Int16 = dt.Rows.Count - 1 To 0 Step -1
                    If dt.Rows(i)("COLUMN_NAME") = "rowguid" Then
                        dt.Rows.RemoveAt(i)
                    End If
                Next

                Return dt
            End If
        End Function

        Public Function GetTableColumn(ByVal tbName As String) As DataTable
            Dim sql As String = "EXEC SP_COLUMNS " & SqlDB.SetString(tbName)
            Dim dt As DataTable = SqlDB.ExecuteTable(sql, SqlDB.GetConnection(ConnectionString))
            dt.DefaultView.Sort = "COLUMN_NAME"
            If dt.Rows.Count = 0 Then
                Throw New ApplicationException(String.Format(_ErrorTableNotFound, _TableName))
            Else
                If dt.Columns.Contains("msrepl_tran_version") = True Then
                    dt.Columns.Remove("msrepl_tran_version")
                End If
                If dt.Columns.Contains("rowguid") = True Then
                    dt.Columns.Remove("rowguid")
                End If
                Return dt
            End If
        End Function

        Public Function GetChildTableKey() As DataTable
            Dim sql As String = "EXEC SP_FKEYS " & SqlDB.SetString(_TableName)
            Dim dt As DataTable = SqlDB.ExecuteTable(sql, SqlDB.GetConnection(ConnectionString))

            If dt.Rows.Count > 0 Then
                dt.DefaultView.Sort = "PKTABLE_NAME, PKCOLUMN_NAME"
            End If

            Return dt.DefaultView.ToTable
        End Function

        Public Function IsView() As Boolean
            Dim sql As String = "EXEC SP_TABLES '" & _TableName & "' "
            Dim dt As DataTable = SqlDB.ExecuteTable(sql, SqlDB.GetConnection(ConnectionString))
            Try
                If dt.Rows.Count = 0 Then
                    Return False
                Else
                    Return IIf(dt.Rows(0)("table_type").ToString() = "VIEW", True, False)
                End If
            Catch ex As Exception

            End Try

        End Function
        Public Function GetTableList() As DataTable
            Dim Sql As String = "EXEC SP_TABLES null,null,'" & _DataBaseName & "'"
            'Dim sql As String = "select * from sysobjects where type='u' and category=0  order by name"

            Dim dt As DataTable = SqlDB.ExecuteTable(Sql, SqlDB.GetConnection(ConnectionString))
            Dim dtList As New DataTable()
            dtList.Columns.Add("TABLE_NAME")
            dtList.Columns.Add("TABLE_VALUE")

            dt.DefaultView.Sort = "TABLE_NAME"

            For Each dr As DataRow In dt.Rows
                If (dr("TABLE_OWNER").ToString() = "dbo") Then
                    Dim drL As DataRow = dtList.NewRow
                    drL("TABLE_NAME") = dr("TABLE_NAME").ToString() & " # " & dr("TABLE_TYPE").ToString()
                    drL("TABLE_VALUE") = dr("TABLE_NAME").ToString()
                    dtList.Rows.Add(drL)
                End If
            Next
            Return dtList
        End Function

        Public Function GetAllTable() As DataTable
            Dim ret As New DataTable
            Try
                Dim Sql As String = "EXEC SP_TABLES null,null,'" & _DataBaseName & "'"
                Dim dt As New DataTable
                dt = SqlDB.ExecuteTable(Sql, SqlDB.GetConnection(ConnectionString))
                dt.DefaultView.RowFilter = "table_owner = 'dbo'"

                ret = dt.DefaultView.ToTable
            Catch ex As Exception
                ret = New DataTable
            End Try

            Return ret
        End Function

        Public Function GetTable(ByVal _TableName As String) As DataTable
            Dim Sql As String = "EXEC SP_TABLES '" & _TableName & "',null,'" & _DataBaseName & "'"
            Dim dt As DataTable = SqlDB.ExecuteTable(Sql, SqlDB.GetConnection(ConnectionString))
            Return dt

        End Function

    End Class
End Namespace

