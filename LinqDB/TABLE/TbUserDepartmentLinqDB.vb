Imports System
Imports System.Data 
Imports System.Data.SQLClient
Imports System.Data.Linq.Mapping 
Imports System.Linq 
Imports DB = LinqDB.ConnectDB.SqlDB
Imports LinqDB.ConnectDB

Namespace TABLE
    'Represents a transaction for TB_USER_DEPARTMENT table LinqDB.
    '[Create by  on June, 28 2017]
    Public Class TbUserDepartmentLinqDB
        Public sub TbUserDepartmentLinqDB()

        End Sub 
        ' TB_USER_DEPARTMENT
        Const _tableName As String = "TB_USER_DEPARTMENT"

        'Set Common Property
        Dim _error As String = ""
        Dim _information As String = ""
        Dim _haveData As Boolean = False

        Public ReadOnly Property TableName As String
            Get
                Return _tableName
            End Get
        End Property
        Public ReadOnly Property ErrorMessage As String
            Get
                Return _error
            End Get
        End Property
        Public ReadOnly Property InfoMessage As String
            Get
                Return _information
            End Get
        End Property


        'Generate Field List
        Dim _ID As Long = 0
        Dim _CREATED_BY As  String  = ""
        Dim _CREATED_DATE As  System.Nullable(Of DateTime)  = New DateTime(1,1,1)
        Dim _UPDATED_BY As  String  = ""
        Dim _UPDATED_DATE As  System.Nullable(Of DateTime)  = New DateTime(1,1,1)
        Dim _TB_USER_FUNCTION_ID As Long = 0
        Dim _USER_ID As Long = 0
        Dim _FUNCTION_ID As Long = 0
        Dim _DEPARTMENT_ID As Long = 0
        Dim _DEPARTMENT_TITLE As String = ""
        Dim _DEPARTMENT_COVER_URL As  String  = ""
        Dim _COURSE_DETAIL As  String  = ""
        Dim _BIND_COURSE As Char = "N"

        'Generate Field Property 
        <Column(Storage:="_ID", DbType:="BigInt NOT NULL ",CanBeNull:=false)>  _
        Public Property ID() As Long
            Get
                Return _ID
            End Get
            Set(ByVal value As Long)
               _ID = value
            End Set
        End Property 
        <Column(Storage:="_CREATED_BY", DbType:="VarChar(50)")>  _
        Public Property CREATED_BY() As  String 
            Get
                Return _CREATED_BY
            End Get
            Set(ByVal value As  String )
               _CREATED_BY = value
            End Set
        End Property 
        <Column(Storage:="_CREATED_DATE", DbType:="DateTime")>  _
        Public Property CREATED_DATE() As  System.Nullable(Of DateTime) 
            Get
                Return _CREATED_DATE
            End Get
            Set(ByVal value As  System.Nullable(Of DateTime) )
               _CREATED_DATE = value
            End Set
        End Property 
        <Column(Storage:="_UPDATED_BY", DbType:="VarChar(50)")>  _
        Public Property UPDATED_BY() As  String 
            Get
                Return _UPDATED_BY
            End Get
            Set(ByVal value As  String )
               _UPDATED_BY = value
            End Set
        End Property 
        <Column(Storage:="_UPDATED_DATE", DbType:="DateTime")>  _
        Public Property UPDATED_DATE() As  System.Nullable(Of DateTime) 
            Get
                Return _UPDATED_DATE
            End Get
            Set(ByVal value As  System.Nullable(Of DateTime) )
               _UPDATED_DATE = value
            End Set
        End Property 
        <Column(Storage:="_TB_USER_FUNCTION_ID", DbType:="BigInt NOT NULL ",CanBeNull:=false)>  _
        Public Property TB_USER_FUNCTION_ID() As Long
            Get
                Return _TB_USER_FUNCTION_ID
            End Get
            Set(ByVal value As Long)
               _TB_USER_FUNCTION_ID = value
            End Set
        End Property 
        <Column(Storage:="_USER_ID", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property USER_ID() As Long
            Get
                Return _USER_ID
            End Get
            Set(ByVal value As Long)
               _USER_ID = value
            End Set
        End Property 
        <Column(Storage:="_FUNCTION_ID", DbType:="BigInt NOT NULL ",CanBeNull:=false)>  _
        Public Property FUNCTION_ID() As Long
            Get
                Return _FUNCTION_ID
            End Get
            Set(ByVal value As Long)
               _FUNCTION_ID = value
            End Set
        End Property 
        <Column(Storage:="_DEPARTMENT_ID", DbType:="BigInt NOT NULL ",CanBeNull:=false)>  _
        Public Property DEPARTMENT_ID() As Long
            Get
                Return _DEPARTMENT_ID
            End Get
            Set(ByVal value As Long)
               _DEPARTMENT_ID = value
            End Set
        End Property 
        <Column(Storage:="_DEPARTMENT_TITLE", DbType:="VarChar(255) NOT NULL ",CanBeNull:=false)>  _
        Public Property DEPARTMENT_TITLE() As String
            Get
                Return _DEPARTMENT_TITLE
            End Get
            Set(ByVal value As String)
               _DEPARTMENT_TITLE = value
            End Set
        End Property 
        <Column(Storage:="_DEPARTMENT_COVER_URL", DbType:="VarChar(500)")>  _
        Public Property DEPARTMENT_COVER_URL() As  String 
            Get
                Return _DEPARTMENT_COVER_URL
            End Get
            Set(ByVal value As  String )
               _DEPARTMENT_COVER_URL = value
            End Set
        End Property 
        <Column(Storage:="_COURSE_DETAIL", DbType:="Text")>  _
        Public Property COURSE_DETAIL() As  String 
            Get
                Return _COURSE_DETAIL
            End Get
            Set(ByVal value As  String )
               _COURSE_DETAIL = value
            End Set
        End Property 
        <Column(Storage:="_BIND_COURSE", DbType:="Char(1) NOT NULL ",CanBeNull:=false)>  _
        Public Property BIND_COURSE() As Char
            Get
                Return _BIND_COURSE
            End Get
            Set(ByVal value As Char)
               _BIND_COURSE = value
            End Set
        End Property 


        'Clear All Data
        Private Sub ClearData()
            _ID = 0
            _CREATED_BY = ""
            _CREATED_DATE = New DateTime(1,1,1)
            _UPDATED_BY = ""
            _UPDATED_DATE = New DateTime(1,1,1)
            _TB_USER_FUNCTION_ID = 0
            _USER_ID = 0
            _FUNCTION_ID = 0
            _DEPARTMENT_ID = 0
            _DEPARTMENT_TITLE = ""
            _DEPARTMENT_COVER_URL = ""
            _COURSE_DETAIL = ""
            _BIND_COURSE = "N"
        End Sub

       'Define Public Method 
        'Execute the select statement with the specified condition and return a System.Data.DataTable.
        '/// <param name=whereClause>The condition for execute select statement.</param>
        '/// <param name=orderBy>The fields for sort data.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>The System.Data.DataTable object for specified condition.</returns>
        Public Function GetDataList(whClause As String, orderBy As String, trans As SQLTransaction, cmdParm() As SqlParameter) As DataTable
            Return DB.ExecuteTable(SqlSelect & IIf(whClause = "", "", " WHERE " & whClause) & IIF(orderBy = "", "", " ORDER BY  " & orderBy), trans, cmdParm)
        End Function


        'Execute the select statement with the specified condition and return a System.Data.DataTable.
        '/// <param name=whereClause>The condition for execute select statement.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>The System.Data.DataTable object for specified condition.</returns>

        Public Function GetListBySql(Sql As String, trans As SQLTransaction, cmdParm() As SqlParameter) As DataTable
            Return DB.ExecuteTable(Sql, trans, cmdParm)
        End Function


        '/// Returns an indication whether the current data is inserted into TB_USER_DEPARTMENT table successfully.
        '/// <param name=userID>The current user.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if insert data successfully; otherwise, false.</returns>
        Public Function InsertData(CreatedBy As String,trans As SQLTransaction) As ExecuteDataInfo
            If trans IsNot Nothing Then 
                _created_by = CreatedBy
                _created_date = DateTime.Now
                Return doInsert(trans)
            Else 
                _error = "Transaction Is not null"
                Dim ret As New ExecuteDataInfo
                ret.IsSuccess = False
                ret.SqlStatement = ""
                ret.ErrorMessage = _error
                Return ret
            End If 
        End Function


        '/// Returns an indication whether the current data is updated to TB_USER_DEPARTMENT table successfully.
        '/// <param name=userID>The current user.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if update data successfully; otherwise, false.</returns>
        Public Function UpdateData(UpdatedBy As String,trans As SQLTransaction) As ExecuteDataInfo
            If trans IsNot Nothing Then 
                If _id > 0 Then 
                    _UPDATED_BY = UpdatedBy
                    _UPDATED_DATE = DateTime.Now

                    Return doUpdate("ID = @_ID", trans)
                Else 
                    _error = "No ID Data"
                    Dim ret As New ExecuteDataInfo
                    ret.IsSuccess = False
                    ret.SqlStatement = ""
                    ret.ErrorMessage = _error
                    Return ret
                End If 
            Else 
                _error = "Transaction Is not null"
                Dim ret As New ExecuteDataInfo
                ret.IsSuccess = False
                ret.SqlStatement = ""
                ret.ErrorMessage = _error
                Return ret
            End If 
        End Function


        '/// Returns an indication whether the current data is updated to TB_USER_DEPARTMENT table successfully.
        '/// <returns>true if update data successfully; otherwise, false.</returns>
        Public Function UpdateBySql(Sql As String, trans As SQLTransaction, cmbParm() As SQLParameter) As ExecuteDataInfo
            If trans IsNot Nothing Then 
                Return DB.ExecuteNonQuery(Sql, trans, cmbParm)
            Else 
                _error = "Transaction Is not null"
                Dim ret As New ExecuteDataInfo
                ret.IsSuccess = False
                ret.ErrorMessage = _error
                Return ret
            End If 
        End Function


        '/// Returns an indication whether the current data is deleted from TB_USER_DEPARTMENT table successfully.
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if delete data successfully; otherwise, false.</returns>
        Public Function DeleteByPK(cID As Long, trans As SQLTransaction) As ExecuteDataInfo
            If trans IsNot Nothing Then 
                Dim p(1) As SQLParameter
                p(0) = DB.SetBigInt("@_ID", cID)
                Return doDelete("ID = @_ID", trans, p)
            Else 
                _error = "Transaction Is not null"
                Dim ret As New ExecuteDataInfo
                ret.IsSuccess = False
                ret.ErrorMessage = _error
                Return ret
            End If 
        End Function


        '/// Returns an indication whether the record of TB_USER_DEPARTMENT by specified ID key is retrieved successfully.
        '/// <param name=cID>The ID key.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function ChkDataByPK(cID As Long, trans As SQLTransaction) As Boolean
            Dim p(1) As SQLParameter
            p(0) = DB.SetBigInt("@_ID", cID)
            Return doChkData("ID = @_ID", trans, p)
        End Function


        '/// Returns an indication whether the record and Mapping field to Data Class of TB_USER_DEPARTMENT by specified ID key is retrieved successfully.
        '/// <param name=cID>The ID key.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function GetDataByPK(cID As Long, trans As SQLTransaction) As TbUserDepartmentLinqDB
            Dim p(1) As SQLParameter
            p(0) = DB.SetBigInt("@_ID", cID)
            Return doGetData("ID = @_ID", trans, p)
        End Function


        '/// Returns an indication whether the record of TB_USER_DEPARTMENT by specified condition is retrieved successfully.
        '/// <param name=whText>The condition specify the deleting record(s).</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function ChkDataByWhere(whText As String, trans As SQLTransaction, cmdPara() As SQLParameter) As Boolean
            Return doChkData(whText, trans, cmdPara)
        End Function



        '/// Returns an indication whether the current data is inserted into TB_USER_DEPARTMENT table successfully.
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if insert data successfully; otherwise, false.</returns>
        Private Function doInsert(trans As SQLTransaction) As ExecuteDataInfo
            Dim ret As New ExecuteDataInfo
            If _haveData = False Then
                Try
                    Dim dt as DataTable = DB.ExecuteTable(SqlInsert, trans, SetParameterData())
                    If dt.Rows.Count = 0 Then
                        ret.IsSuccess = False
                        ret.ErrorMessage = DB.ErrorMessage
                    Else
                        _ID = dt.Rows(0)("ID")
                        _haveData = True
                        ret.IsSuccess = True
                        _information = MessageResources.MSGIN001
                        ret.InfoMessage = _information
                    End If
                Catch ex As ApplicationException
                    ret.IsSuccess = false
                    ret.ErrorMessage = ex.Message & "ApplicationException :" & ex.ToString()  
                    ret.SqlStatement = SqlInsert
                Catch ex As Exception
                    ret.IsSuccess = False
                    ret.ErrorMessage = MessageResources.MSGEC101 & " Exception :" & ex.ToString()  
                    ret.SqlStatement = SqlInsert
                End Try
            Else
                ret.IsSuccess = False
                ret.ErrorMessage = MessageResources.MSGEN002  
                ret.SqlStatement = SqlInsert
            End If

            Return ret
        End Function


        '/// Returns an indication whether the current data is updated to TB_USER_DEPARTMENT table successfully.
        '/// <param name=whText>The condition specify the updating record(s).</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if update data successfully; otherwise, false.</returns>
        Private Function doUpdate(whText As String, trans As SQLTransaction) As ExecuteDataInfo
            Dim ret As New ExecuteDataInfo
            Dim tmpWhere As String = " Where " & whText
            If _haveData = True Then
                Dim sql As String = SqlUpdate & tmpWhere
                If whText.Trim() <> ""
                    Try
                        ret = DB.ExecuteNonQuery(sql, trans, SetParameterData())
                        If ret.IsSuccess = False Then
                            _error = DB.ErrorMessage
                        Else
                            _information = MessageResources.MSGIU001
                            ret.InfoMessage = MessageResources.MSGIU001
                        End If
                    Catch ex As ApplicationException
                        ret.IsSuccess = False
                        ret.ErrorMessage = "ApplicationException:" & ex.Message & ex.ToString() 
                        ret.SqlStatement = sql
                    Catch ex As Exception
                        ret.IsSuccess = False
                        ret.ErrorMessage = "Exception:" & MessageResources.MSGEC102 &  ex.ToString() 
                        ret.SqlStatement = sql
                    End Try
                Else
                    ret.IsSuccess = False
                    ret.ErrorMessage = MessageResources.MSGEU003 
                    ret.SqlStatement = sql
                End If
            Else
                ret.IsSuccess = True
            End If

            Return ret
        End Function


        '/// Returns an indication whether the current data is deleted from TB_USER_DEPARTMENT table successfully.
        '/// <param name=whText>The condition specify the deleting record(s).</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if delete data successfully; otherwise, false.</returns>
        Private Function doDelete(whText As String, trans As SQLTransaction, cmdPara() As SqlParameter) As ExecuteDataInfo
             Dim ret As New ExecuteDataInfo
             Dim tmpWhere As String = " Where " & whText
             Dim sql As String = SqlDelete & tmpWhere
             If whText.Trim() <> ""
                 Try
                     ret = DB.ExecuteNonQuery(sql, trans, cmdPara)
                     If ret.IsSuccess = False Then
                         _error = MessageResources.MSGED001
                     Else
                        _information = MessageResources.MSGID001
                        ret.InfoMessage = MessageResources.MSGID001
                     End If
                 Catch ex As ApplicationException
                     _error = "ApplicationException :" & ex.Message & ex.ToString() & "### SQL:" & sql
                     ret.IsSuccess = False
                     ret.ErrorMessage = _error
                     ret.SqlStatement = sql
                 Catch ex As Exception
                     _error =  " Exception :" & MessageResources.MSGEC103 & ex.ToString() & "### SQL: " & sql
                     ret.IsSuccess = False
                     ret.ErrorMessage = _error
                     ret.SqlStatement = sql
                 End Try
             Else
                 _error = MessageResources.MSGED003 & "### SQL: " & sql
                 ret.IsSuccess = False
                 ret.ErrorMessage = _error
                 ret.SqlStatement = sql
             End If

            Return ret
        End Function

        Private Function SetParameterData() As SqlParameter()
            Dim cmbParam(12) As SqlParameter
            cmbParam(0) = New SqlParameter("@_ID", SqlDbType.BigInt)
            cmbParam(0).Value = _ID

            cmbParam(1) = New SqlParameter("@_CREATED_BY", SqlDbType.VarChar)
            If _CREATED_BY.Trim <> "" Then 
                cmbParam(1).Value = _CREATED_BY.Trim
            Else
                cmbParam(1).Value = DBNull.value
            End If

            cmbParam(2) = New SqlParameter("@_CREATED_DATE", SqlDbType.DateTime)
            If _CREATED_DATE.Value.Year > 1 Then 
                cmbParam(2).Value = _CREATED_DATE.Value
            Else
                cmbParam(2).Value = DBNull.value
            End If

            cmbParam(3) = New SqlParameter("@_UPDATED_BY", SqlDbType.VarChar)
            If _UPDATED_BY.Trim <> "" Then 
                cmbParam(3).Value = _UPDATED_BY.Trim
            Else
                cmbParam(3).Value = DBNull.value
            End If

            cmbParam(4) = New SqlParameter("@_UPDATED_DATE", SqlDbType.DateTime)
            If _UPDATED_DATE.Value.Year > 1 Then 
                cmbParam(4).Value = _UPDATED_DATE.Value
            Else
                cmbParam(4).Value = DBNull.value
            End If

            cmbParam(5) = New SqlParameter("@_TB_USER_FUNCTION_ID", SqlDbType.BigInt)
            cmbParam(5).Value = _TB_USER_FUNCTION_ID

            cmbParam(6) = New SqlParameter("@_USER_ID", SqlDbType.Int)
            cmbParam(6).Value = _USER_ID

            cmbParam(7) = New SqlParameter("@_FUNCTION_ID", SqlDbType.BigInt)
            cmbParam(7).Value = _FUNCTION_ID

            cmbParam(8) = New SqlParameter("@_DEPARTMENT_ID", SqlDbType.BigInt)
            cmbParam(8).Value = _DEPARTMENT_ID

            cmbParam(9) = New SqlParameter("@_DEPARTMENT_TITLE", SqlDbType.VarChar)
            cmbParam(9).Value = _DEPARTMENT_TITLE.Trim

            cmbParam(10) = New SqlParameter("@_DEPARTMENT_COVER_URL", SqlDbType.VarChar)
            If _DEPARTMENT_COVER_URL.Trim <> "" Then 
                cmbParam(10).Value = _DEPARTMENT_COVER_URL.Trim
            Else
                cmbParam(10).Value = DBNull.value
            End If

            cmbParam(11) = New SqlParameter("@_COURSE_DETAIL", SqlDbType.Text)
            If _COURSE_DETAIL IsNot Nothing Then 
                cmbParam(11).Value = _COURSE_DETAIL.Trim
            Else
                cmbParam(11).Value = DBNull.value
            End IF

            cmbParam(12) = New SqlParameter("@_BIND_COURSE", SqlDbType.Char)
            cmbParam(12).Value = _BIND_COURSE

            Return cmbParam
        End Function


        '/// Returns an indication whether the record of TB_USER_DEPARTMENT by specified condition is retrieved successfully.
        '/// <param name=whText>The condition specify the deleting record(s).</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Private Function doChkData(whText As String, trans As SQLTransaction, cmdPara() As SQLParameter) As Boolean
            Dim ret As Boolean = True
            Dim tmpWhere As String = " WHERE " & whText
            ClearData()
            _haveData  = False
            If whText.Trim() <> "" Then
                Dim Rdr As SQLDataReader
                Try
                    Rdr = DB.ExecuteReader(SqlSelect() & tmpWhere, trans, cmdPara)
                    If Rdr.Read() Then
                        _haveData = True
                        If Convert.IsDBNull(Rdr("id")) = False Then _id = Convert.ToInt64(Rdr("id"))
                        If Convert.IsDBNull(Rdr("created_by")) = False Then _created_by = Rdr("created_by").ToString()
                        If Convert.IsDBNull(Rdr("created_date")) = False Then _created_date = Convert.ToDateTime(Rdr("created_date"))
                        If Convert.IsDBNull(Rdr("updated_by")) = False Then _updated_by = Rdr("updated_by").ToString()
                        If Convert.IsDBNull(Rdr("updated_date")) = False Then _updated_date = Convert.ToDateTime(Rdr("updated_date"))
                        If Convert.IsDBNull(Rdr("tb_user_function_id")) = False Then _tb_user_function_id = Convert.ToInt64(Rdr("tb_user_function_id"))
                        If Convert.IsDBNull(Rdr("user_id")) = False Then _user_id = Convert.ToInt32(Rdr("user_id"))
                        If Convert.IsDBNull(Rdr("function_id")) = False Then _function_id = Convert.ToInt64(Rdr("function_id"))
                        If Convert.IsDBNull(Rdr("department_id")) = False Then _department_id = Convert.ToInt64(Rdr("department_id"))
                        If Convert.IsDBNull(Rdr("department_title")) = False Then _department_title = Rdr("department_title").ToString()
                        If Convert.IsDBNull(Rdr("department_cover_url")) = False Then _department_cover_url = Rdr("department_cover_url").ToString()
                        If Convert.IsDBNull(Rdr("course_detail")) = False Then _course_detail = Rdr("course_detail").ToString()
                        If Convert.IsDBNull(Rdr("bind_course")) = False Then _bind_course = Rdr("bind_course").ToString()
                    Else
                        ret = False
                        _error = MessageResources.MSGEV002
                    End If

                    Rdr.Close()
                Catch ex As Exception
                    ex.ToString()
                    ret = False
                    _error = MessageResources.MSGEC104 & " #### " & ex.ToString()
                End Try
            Else
                ret = False
                _error = MessageResources.MSGEV001
            End If

            Return ret
        End Function


        '/// Returns an indication whether the record of TB_USER_DEPARTMENT by specified condition is retrieved successfully.
        '/// <param name=whText>The condition specify the deleting record(s).</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Private Function doGetData(whText As String, trans As SQLTransaction, cmdPara() As SQLParameter) As TbUserDepartmentLinqDB
            ClearData()
            _haveData  = False
            If whText.Trim() <> "" Then
                Dim tmpWhere As String = " WHERE " & whText
                Dim Rdr As SQLDataReader
                Try
                    Rdr = DB.ExecuteReader(SqlSelect() & tmpWhere, trans, cmdPara)
                    If Rdr.Read() Then
                        _haveData = True
                        If Convert.IsDBNull(Rdr("id")) = False Then _id = Convert.ToInt64(Rdr("id"))
                        If Convert.IsDBNull(Rdr("created_by")) = False Then _created_by = Rdr("created_by").ToString()
                        If Convert.IsDBNull(Rdr("created_date")) = False Then _created_date = Convert.ToDateTime(Rdr("created_date"))
                        If Convert.IsDBNull(Rdr("updated_by")) = False Then _updated_by = Rdr("updated_by").ToString()
                        If Convert.IsDBNull(Rdr("updated_date")) = False Then _updated_date = Convert.ToDateTime(Rdr("updated_date"))
                        If Convert.IsDBNull(Rdr("tb_user_function_id")) = False Then _tb_user_function_id = Convert.ToInt64(Rdr("tb_user_function_id"))
                        If Convert.IsDBNull(Rdr("user_id")) = False Then _user_id = Convert.ToInt32(Rdr("user_id"))
                        If Convert.IsDBNull(Rdr("function_id")) = False Then _function_id = Convert.ToInt64(Rdr("function_id"))
                        If Convert.IsDBNull(Rdr("department_id")) = False Then _department_id = Convert.ToInt64(Rdr("department_id"))
                        If Convert.IsDBNull(Rdr("department_title")) = False Then _department_title = Rdr("department_title").ToString()
                        If Convert.IsDBNull(Rdr("department_cover_url")) = False Then _department_cover_url = Rdr("department_cover_url").ToString()
                        If Convert.IsDBNull(Rdr("course_detail")) = False Then _course_detail = Rdr("course_detail").ToString()
                        If Convert.IsDBNull(Rdr("bind_course")) = False Then _bind_course = Rdr("bind_course").ToString()
                    Else
                        _error = MessageResources.MSGEV002
                    End If

                    Rdr.Close()
                Catch ex As Exception
                    ex.ToString()
                    _error = MessageResources.MSGEC104 & " #### " & ex.ToString()
                End Try
            Else
                _error = MessageResources.MSGEV001
            End If
            Return Me
        End Function



        ' SQL Statements


        'Get Insert Statement for table TB_USER_DEPARTMENT
        Private ReadOnly Property SqlInsert() As String 
            Get
                Dim Sql As String=""
                Sql += "INSERT INTO " & tableName  & " (CREATED_BY, CREATED_DATE, TB_USER_FUNCTION_ID, USER_ID, FUNCTION_ID, DEPARTMENT_ID, DEPARTMENT_TITLE, DEPARTMENT_COVER_URL, COURSE_DETAIL, BIND_COURSE)"
                Sql += " OUTPUT INSERTED.ID, INSERTED.CREATED_BY, INSERTED.CREATED_DATE, INSERTED.UPDATED_BY, INSERTED.UPDATED_DATE, INSERTED.TB_USER_FUNCTION_ID, INSERTED.USER_ID, INSERTED.FUNCTION_ID, INSERTED.DEPARTMENT_ID, INSERTED.DEPARTMENT_TITLE, INSERTED.DEPARTMENT_COVER_URL, INSERTED.COURSE_DETAIL, INSERTED.BIND_COURSE"
                Sql += " VALUES("
                sql += "@_CREATED_BY" & ", "
                sql += "@_CREATED_DATE" & ", "
                sql += "@_TB_USER_FUNCTION_ID" & ", "
                sql += "@_USER_ID" & ", "
                sql += "@_FUNCTION_ID" & ", "
                sql += "@_DEPARTMENT_ID" & ", "
                sql += "@_DEPARTMENT_TITLE" & ", "
                sql += "@_DEPARTMENT_COVER_URL" & ", "
                sql += "@_COURSE_DETAIL" & ", "
                sql += "@_BIND_COURSE"
                sql += ")"
                Return sql
            End Get
        End Property


        'Get update statement form table TB_USER_DEPARTMENT
        Private ReadOnly Property SqlUpdate() As String
            Get
                Dim Sql As String = ""
                Sql += "UPDATE " & tableName & " SET "
                Sql += "UPDATED_BY = " & "@_UPDATED_BY" & ", "
                Sql += "UPDATED_DATE = " & "@_UPDATED_DATE" & ", "
                Sql += "TB_USER_FUNCTION_ID = " & "@_TB_USER_FUNCTION_ID" & ", "
                Sql += "USER_ID = " & "@_USER_ID" & ", "
                Sql += "FUNCTION_ID = " & "@_FUNCTION_ID" & ", "
                Sql += "DEPARTMENT_ID = " & "@_DEPARTMENT_ID" & ", "
                Sql += "DEPARTMENT_TITLE = " & "@_DEPARTMENT_TITLE" & ", "
                Sql += "DEPARTMENT_COVER_URL = " & "@_DEPARTMENT_COVER_URL" & ", "
                Sql += "COURSE_DETAIL = " & "@_COURSE_DETAIL" & ", "
                Sql += "BIND_COURSE = " & "@_BIND_COURSE" + ""
                Return Sql
            End Get
        End Property


        'Get Delete Record in table TB_USER_DEPARTMENT
        Private ReadOnly Property SqlDelete() As String
            Get
                Dim Sql As String = "DELETE FROM " & tableName
                Return Sql
            End Get
        End Property


        'Get Select Statement for table TB_USER_DEPARTMENT
        Private ReadOnly Property SqlSelect() As String
            Get
                Dim Sql As String = "SELECT ID, CREATED_BY, CREATED_DATE, UPDATED_BY, UPDATED_DATE, TB_USER_FUNCTION_ID, USER_ID, FUNCTION_ID, DEPARTMENT_ID, DEPARTMENT_TITLE, DEPARTMENT_COVER_URL, COURSE_DETAIL, BIND_COURSE FROM " & tableName
                Return Sql
            End Get
        End Property

    End Class
End Namespace
