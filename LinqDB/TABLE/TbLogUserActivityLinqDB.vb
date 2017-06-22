Imports System
Imports System.Data 
Imports System.Data.SQLClient
Imports System.Data.Linq.Mapping 
Imports System.Linq 
Imports DB = LinqDB.ConnectDB.SqlDB
Imports LinqDB.ConnectDB

Namespace TABLE
    'Represents a transaction for TB_LOG_USER_ACTIVITY table LinqDB.
    '[Create by  on June, 19 2017]
    Public Class TbLogUserActivityLinqDB
        Public sub TbLogUserActivityLinqDB()

        End Sub 
        ' TB_LOG_USER_ACTIVITY
        Const _tableName As String = "TB_LOG_USER_ACTIVITY"

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
        Dim _LOG_USER_ACTIVITY_ID As Long = 0
        Dim _CREATED_BY As String = ""
        Dim _CREATED_DATE As DateTime = New DateTime(1,1,1)
        Dim _UPDATED_BY As  String  = ""
        Dim _UPDATED_DATE As  System.Nullable(Of DateTime)  = New DateTime(1,1,1)
        Dim _LOGIN_HISOTRY_ID As Long = 0
        Dim _LOG_TYPE As Char = ""
        Dim _LOG_MESSAGE As String = ""
        Dim _CLASS_NAME As String = ""
        Dim _FUNCTION_NAME As String = ""
        Dim _LINE_NO As Long = 0

        'Generate Field Property 
        <Column(Storage:="_LOG_USER_ACTIVITY_ID", DbType:="BigInt NOT NULL ",CanBeNull:=false)>  _
        Public Property LOG_USER_ACTIVITY_ID() As Long
            Get
                Return _LOG_USER_ACTIVITY_ID
            End Get
            Set(ByVal value As Long)
               _LOG_USER_ACTIVITY_ID = value
            End Set
        End Property 
        <Column(Storage:="_CREATED_BY", DbType:="VarChar(100) NOT NULL ",CanBeNull:=false)>  _
        Public Property CREATED_BY() As String
            Get
                Return _CREATED_BY
            End Get
            Set(ByVal value As String)
               _CREATED_BY = value
            End Set
        End Property 
        <Column(Storage:="_CREATED_DATE", DbType:="DateTime NOT NULL ",CanBeNull:=false)>  _
        Public Property CREATED_DATE() As DateTime
            Get
                Return _CREATED_DATE
            End Get
            Set(ByVal value As DateTime)
               _CREATED_DATE = value
            End Set
        End Property 
        <Column(Storage:="_UPDATED_BY", DbType:="VarChar(100)")>  _
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
        <Column(Storage:="_LOGIN_HISOTRY_ID", DbType:="BigInt NOT NULL ",CanBeNull:=false)>  _
        Public Property LOGIN_HISOTRY_ID() As Long
            Get
                Return _LOGIN_HISOTRY_ID
            End Get
            Set(ByVal value As Long)
               _LOGIN_HISOTRY_ID = value
            End Set
        End Property 
        <Column(Storage:="_LOG_TYPE", DbType:="Char(1) NOT NULL ",CanBeNull:=false)>  _
        Public Property LOG_TYPE() As Char
            Get
                Return _LOG_TYPE
            End Get
            Set(ByVal value As Char)
               _LOG_TYPE = value
            End Set
        End Property 
        <Column(Storage:="_LOG_MESSAGE", DbType:="VarChar(500) NOT NULL ",CanBeNull:=false)>  _
        Public Property LOG_MESSAGE() As String
            Get
                Return _LOG_MESSAGE
            End Get
            Set(ByVal value As String)
               _LOG_MESSAGE = value
            End Set
        End Property 
        <Column(Storage:="_CLASS_NAME", DbType:="VarChar(50) NOT NULL ",CanBeNull:=false)>  _
        Public Property CLASS_NAME() As String
            Get
                Return _CLASS_NAME
            End Get
            Set(ByVal value As String)
               _CLASS_NAME = value
            End Set
        End Property 
        <Column(Storage:="_FUNCTION_NAME", DbType:="VarChar(50) NOT NULL ",CanBeNull:=false)>  _
        Public Property FUNCTION_NAME() As String
            Get
                Return _FUNCTION_NAME
            End Get
            Set(ByVal value As String)
               _FUNCTION_NAME = value
            End Set
        End Property 
        <Column(Storage:="_LINE_NO", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property LINE_NO() As Long
            Get
                Return _LINE_NO
            End Get
            Set(ByVal value As Long)
               _LINE_NO = value
            End Set
        End Property 


        'Clear All Data
        Private Sub ClearData()
            _LOG_USER_ACTIVITY_ID = 0
            _CREATED_BY = ""
            _CREATED_DATE = New DateTime(1,1,1)
            _UPDATED_BY = ""
            _UPDATED_DATE = New DateTime(1,1,1)
            _LOGIN_HISOTRY_ID = 0
            _LOG_TYPE = ""
            _LOG_MESSAGE = ""
            _CLASS_NAME = ""
            _FUNCTION_NAME = ""
            _LINE_NO = 0
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


        '/// Returns an indication whether the current data is inserted into TB_LOG_USER_ACTIVITY table successfully.
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


        '/// Returns an indication whether the current data is updated to TB_LOG_USER_ACTIVITY table successfully.
        '/// <param name=userID>The current user.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if update data successfully; otherwise, false.</returns>
        Public Function UpdateData(UpdatedBy As String,trans As SQLTransaction) As ExecuteDataInfo
            If trans IsNot Nothing Then 
                If _log_user_activity_id > 0 Then 
                    _UPDATED_BY = UpdatedBy
                    _UPDATED_DATE = DateTime.Now

                    Return doUpdate("LOG_USER_ACTIVITY_ID = @_LOG_USER_ACTIVITY_ID", trans)
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


        '/// Returns an indication whether the current data is updated to TB_LOG_USER_ACTIVITY table successfully.
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


        '/// Returns an indication whether the current data is deleted from TB_LOG_USER_ACTIVITY table successfully.
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if delete data successfully; otherwise, false.</returns>
        Public Function DeleteByPK(cLOG_USER_ACTIVITY_ID As Long, trans As SQLTransaction) As ExecuteDataInfo
            If trans IsNot Nothing Then 
                Dim p(1) As SQLParameter
                p(0) = DB.SetBigInt("@_LOG_USER_ACTIVITY_ID", cLOG_USER_ACTIVITY_ID)
                Return doDelete("LOG_USER_ACTIVITY_ID = @_LOG_USER_ACTIVITY_ID", trans, p)
            Else 
                _error = "Transaction Is not null"
                Dim ret As New ExecuteDataInfo
                ret.IsSuccess = False
                ret.ErrorMessage = _error
                Return ret
            End If 
        End Function


        '/// Returns an indication whether the record of TB_LOG_USER_ACTIVITY by specified LOG_USER_ACTIVITY_ID key is retrieved successfully.
        '/// <param name=cLOG_USER_ACTIVITY_ID>The LOG_USER_ACTIVITY_ID key.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function ChkDataByPK(cLOG_USER_ACTIVITY_ID As Long, trans As SQLTransaction) As Boolean
            Dim p(1) As SQLParameter
            p(0) = DB.SetBigInt("@_LOG_USER_ACTIVITY_ID", cLOG_USER_ACTIVITY_ID)
            Return doChkData("LOG_USER_ACTIVITY_ID = @_LOG_USER_ACTIVITY_ID", trans, p)
        End Function


        '/// Returns an indication whether the record and Mapping field to Data Class of TB_LOG_USER_ACTIVITY by specified LOG_USER_ACTIVITY_ID key is retrieved successfully.
        '/// <param name=cLOG_USER_ACTIVITY_ID>The LOG_USER_ACTIVITY_ID key.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function GetDataByPK(cLOG_USER_ACTIVITY_ID As Long, trans As SQLTransaction) As TbLogUserActivityLinqDB
            Dim p(1) As SQLParameter
            p(0) = DB.SetBigInt("@_LOG_USER_ACTIVITY_ID", cLOG_USER_ACTIVITY_ID)
            Return doGetData("LOG_USER_ACTIVITY_ID = @_LOG_USER_ACTIVITY_ID", trans, p)
        End Function


        '/// Returns an indication whether the record of TB_LOG_USER_ACTIVITY by specified LOGIN_HISOTRY_ID key is retrieved successfully.
        '/// <param name=cLOGIN_HISOTRY_ID>The LOGIN_HISOTRY_ID key.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function ChkDataByLOGIN_HISOTRY_ID(cLOGIN_HISOTRY_ID As Long, trans As SQLTransaction) As Boolean
            Dim cmdPara(2)  As SQLParameter
            cmdPara(0) = DB.SetText("@_LOGIN_HISOTRY_ID", cLOGIN_HISOTRY_ID) 
            Return doChkData("LOGIN_HISOTRY_ID = @_LOGIN_HISOTRY_ID", trans, cmdPara)
        End Function

        '/// Returns an duplicate data record of TB_LOG_USER_ACTIVITY by specified LOGIN_HISOTRY_ID key is retrieved successfully.
        '/// <param name=cLOGIN_HISOTRY_ID>The LOGIN_HISOTRY_ID key.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function ChkDuplicateByLOGIN_HISOTRY_ID(cLOGIN_HISOTRY_ID As Long, cLOG_USER_ACTIVITY_ID As Long, trans As SQLTransaction) As Boolean
            Dim cmdPara(2)  As SQLParameter
            cmdPara(0) = DB.SetText("@_LOGIN_HISOTRY_ID", cLOGIN_HISOTRY_ID) 
            cmdPara(1) = DB.SetBigInt("@_LOG_USER_ACTIVITY_ID", cLOG_USER_ACTIVITY_ID) 
            Return doChkData("LOGIN_HISOTRY_ID = @_LOGIN_HISOTRY_ID And LOG_USER_ACTIVITY_ID <> @_LOG_USER_ACTIVITY_ID", trans, cmdPara)
        End Function


        '/// Returns an indication whether the record of TB_LOG_USER_ACTIVITY by specified CLASS_NAME_FUNCTION_NAME key is retrieved successfully.
        '/// <param name=cCLASS_NAME_FUNCTION_NAME>The CLASS_NAME_FUNCTION_NAME key.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function ChkDataByCLASS_NAME_FUNCTION_NAME(cCLASS_NAME As String, cFUNCTION_NAME As String, trans As SQLTransaction) As Boolean
            Dim cmdPara(3)  As SQLParameter
            cmdPara(0) = DB.SetText("@_CLASS_NAME", cCLASS_NAME) 
            cmdPara(1) = DB.SetText("@_FUNCTION_NAME", cFUNCTION_NAME) 
            Return doChkData("CLASS_NAME = @_CLASS_NAME AND FUNCTION_NAME = @_FUNCTION_NAME", trans, cmdPara)
        End Function

        '/// Returns an duplicate data record of TB_LOG_USER_ACTIVITY by specified CLASS_NAME_FUNCTION_NAME key is retrieved successfully.
        '/// <param name=cCLASS_NAME_FUNCTION_NAME>The CLASS_NAME_FUNCTION_NAME key.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function ChkDuplicateByCLASS_NAME_FUNCTION_NAME(cCLASS_NAME As String, cFUNCTION_NAME As String, cLOG_USER_ACTIVITY_ID As Long, trans As SQLTransaction) As Boolean
            Dim cmdPara(3)  As SQLParameter
            cmdPara(0) = DB.SetText("@_CLASS_NAME", cCLASS_NAME) 
            cmdPara(1) = DB.SetText("@_FUNCTION_NAME", cFUNCTION_NAME) 
            cmdPara(2) = DB.SetBigInt("@_LOG_USER_ACTIVITY_ID", cLOG_USER_ACTIVITY_ID) 
            Return doChkData("CLASS_NAME = @_CLASS_NAME AND FUNCTION_NAME = @_FUNCTION_NAME And LOG_USER_ACTIVITY_ID <> @_LOG_USER_ACTIVITY_ID", trans, cmdPara)
        End Function


        '/// Returns an indication whether the record of TB_LOG_USER_ACTIVITY by specified condition is retrieved successfully.
        '/// <param name=whText>The condition specify the deleting record(s).</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function ChkDataByWhere(whText As String, trans As SQLTransaction, cmdPara() As SQLParameter) As Boolean
            Return doChkData(whText, trans, cmdPara)
        End Function



        '/// Returns an indication whether the current data is inserted into TB_LOG_USER_ACTIVITY table successfully.
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
                        _LOG_USER_ACTIVITY_ID = dt.Rows(0)("LOG_USER_ACTIVITY_ID")
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


        '/// Returns an indication whether the current data is updated to TB_LOG_USER_ACTIVITY table successfully.
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


        '/// Returns an indication whether the current data is deleted from TB_LOG_USER_ACTIVITY table successfully.
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
            Dim cmbParam(10) As SqlParameter
            cmbParam(0) = New SqlParameter("@_LOG_USER_ACTIVITY_ID", SqlDbType.BigInt)
            cmbParam(0).Value = _LOG_USER_ACTIVITY_ID

            cmbParam(1) = New SqlParameter("@_CREATED_BY", SqlDbType.VarChar)
            cmbParam(1).Value = _CREATED_BY.Trim

            cmbParam(2) = New SqlParameter("@_CREATED_DATE", SqlDbType.DateTime)
            cmbParam(2).Value = _CREATED_DATE

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

            cmbParam(5) = New SqlParameter("@_LOGIN_HISOTRY_ID", SqlDbType.BigInt)
            cmbParam(5).Value = _LOGIN_HISOTRY_ID

            cmbParam(6) = New SqlParameter("@_LOG_TYPE", SqlDbType.Char)
            cmbParam(6).Value = _LOG_TYPE

            cmbParam(7) = New SqlParameter("@_LOG_MESSAGE", SqlDbType.VarChar)
            cmbParam(7).Value = _LOG_MESSAGE.Trim

            cmbParam(8) = New SqlParameter("@_CLASS_NAME", SqlDbType.VarChar)
            cmbParam(8).Value = _CLASS_NAME.Trim

            cmbParam(9) = New SqlParameter("@_FUNCTION_NAME", SqlDbType.VarChar)
            cmbParam(9).Value = _FUNCTION_NAME.Trim

            cmbParam(10) = New SqlParameter("@_LINE_NO", SqlDbType.Int)
            cmbParam(10).Value = _LINE_NO

            Return cmbParam
        End Function


        '/// Returns an indication whether the record of TB_LOG_USER_ACTIVITY by specified condition is retrieved successfully.
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
                        If Convert.IsDBNull(Rdr("log_user_activity_id")) = False Then _log_user_activity_id = Convert.ToInt64(Rdr("log_user_activity_id"))
                        If Convert.IsDBNull(Rdr("created_by")) = False Then _created_by = Rdr("created_by").ToString()
                        If Convert.IsDBNull(Rdr("created_date")) = False Then _created_date = Convert.ToDateTime(Rdr("created_date"))
                        If Convert.IsDBNull(Rdr("updated_by")) = False Then _updated_by = Rdr("updated_by").ToString()
                        If Convert.IsDBNull(Rdr("updated_date")) = False Then _updated_date = Convert.ToDateTime(Rdr("updated_date"))
                        If Convert.IsDBNull(Rdr("login_hisotry_id")) = False Then _login_hisotry_id = Convert.ToInt64(Rdr("login_hisotry_id"))
                        If Convert.IsDBNull(Rdr("log_type")) = False Then _log_type = Rdr("log_type").ToString()
                        If Convert.IsDBNull(Rdr("log_message")) = False Then _log_message = Rdr("log_message").ToString()
                        If Convert.IsDBNull(Rdr("class_name")) = False Then _class_name = Rdr("class_name").ToString()
                        If Convert.IsDBNull(Rdr("function_name")) = False Then _function_name = Rdr("function_name").ToString()
                        If Convert.IsDBNull(Rdr("line_no")) = False Then _line_no = Convert.ToInt32(Rdr("line_no"))
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


        '/// Returns an indication whether the record of TB_LOG_USER_ACTIVITY by specified condition is retrieved successfully.
        '/// <param name=whText>The condition specify the deleting record(s).</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Private Function doGetData(whText As String, trans As SQLTransaction, cmdPara() As SQLParameter) As TbLogUserActivityLinqDB
            ClearData()
            _haveData  = False
            If whText.Trim() <> "" Then
                Dim tmpWhere As String = " WHERE " & whText
                Dim Rdr As SQLDataReader
                Try
                    Rdr = DB.ExecuteReader(SqlSelect() & tmpWhere, trans, cmdPara)
                    If Rdr.Read() Then
                        _haveData = True
                        If Convert.IsDBNull(Rdr("log_user_activity_id")) = False Then _log_user_activity_id = Convert.ToInt64(Rdr("log_user_activity_id"))
                        If Convert.IsDBNull(Rdr("created_by")) = False Then _created_by = Rdr("created_by").ToString()
                        If Convert.IsDBNull(Rdr("created_date")) = False Then _created_date = Convert.ToDateTime(Rdr("created_date"))
                        If Convert.IsDBNull(Rdr("updated_by")) = False Then _updated_by = Rdr("updated_by").ToString()
                        If Convert.IsDBNull(Rdr("updated_date")) = False Then _updated_date = Convert.ToDateTime(Rdr("updated_date"))
                        If Convert.IsDBNull(Rdr("login_hisotry_id")) = False Then _login_hisotry_id = Convert.ToInt64(Rdr("login_hisotry_id"))
                        If Convert.IsDBNull(Rdr("log_type")) = False Then _log_type = Rdr("log_type").ToString()
                        If Convert.IsDBNull(Rdr("log_message")) = False Then _log_message = Rdr("log_message").ToString()
                        If Convert.IsDBNull(Rdr("class_name")) = False Then _class_name = Rdr("class_name").ToString()
                        If Convert.IsDBNull(Rdr("function_name")) = False Then _function_name = Rdr("function_name").ToString()
                        If Convert.IsDBNull(Rdr("line_no")) = False Then _line_no = Convert.ToInt32(Rdr("line_no"))
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


        'Get Insert Statement for table TB_LOG_USER_ACTIVITY
        Private ReadOnly Property SqlInsert() As String 
            Get
                Dim Sql As String=""
                Sql += "INSERT INTO " & tableName  & " (CREATED_BY, CREATED_DATE, LOGIN_HISOTRY_ID, LOG_TYPE, LOG_MESSAGE, CLASS_NAME, FUNCTION_NAME, LINE_NO)"
                Sql += " OUTPUT INSERTED.LOG_USER_ACTIVITY_ID, INSERTED.CREATED_BY, INSERTED.CREATED_DATE, INSERTED.UPDATED_BY, INSERTED.UPDATED_DATE, INSERTED.LOGIN_HISOTRY_ID, INSERTED.LOG_TYPE, INSERTED.LOG_MESSAGE, INSERTED.CLASS_NAME, INSERTED.FUNCTION_NAME, INSERTED.LINE_NO"
                Sql += " VALUES("
                sql += "@_CREATED_BY" & ", "
                sql += "@_CREATED_DATE" & ", "
                sql += "@_LOGIN_HISOTRY_ID" & ", "
                sql += "@_LOG_TYPE" & ", "
                sql += "@_LOG_MESSAGE" & ", "
                sql += "@_CLASS_NAME" & ", "
                sql += "@_FUNCTION_NAME" & ", "
                sql += "@_LINE_NO"
                sql += ")"
                Return sql
            End Get
        End Property


        'Get update statement form table TB_LOG_USER_ACTIVITY
        Private ReadOnly Property SqlUpdate() As String
            Get
                Dim Sql As String = ""
                Sql += "UPDATE " & tableName & " SET "
                Sql += "UPDATED_BY = " & "@_UPDATED_BY" & ", "
                Sql += "UPDATED_DATE = " & "@_UPDATED_DATE" & ", "
                Sql += "LOGIN_HISOTRY_ID = " & "@_LOGIN_HISOTRY_ID" & ", "
                Sql += "LOG_TYPE = " & "@_LOG_TYPE" & ", "
                Sql += "LOG_MESSAGE = " & "@_LOG_MESSAGE" & ", "
                Sql += "CLASS_NAME = " & "@_CLASS_NAME" & ", "
                Sql += "FUNCTION_NAME = " & "@_FUNCTION_NAME" & ", "
                Sql += "LINE_NO = " & "@_LINE_NO" + ""
                Return Sql
            End Get
        End Property


        'Get Delete Record in table TB_LOG_USER_ACTIVITY
        Private ReadOnly Property SqlDelete() As String
            Get
                Dim Sql As String = "DELETE FROM " & tableName
                Return Sql
            End Get
        End Property


        'Get Select Statement for table TB_LOG_USER_ACTIVITY
        Private ReadOnly Property SqlSelect() As String
            Get
                Dim Sql As String = "SELECT LOG_USER_ACTIVITY_ID, CREATED_BY, CREATED_DATE, UPDATED_BY, UPDATED_DATE, LOGIN_HISOTRY_ID, LOG_TYPE, LOG_MESSAGE, CLASS_NAME, FUNCTION_NAME, LINE_NO FROM " & tableName
                Return Sql
            End Get
        End Property

    End Class
End Namespace
