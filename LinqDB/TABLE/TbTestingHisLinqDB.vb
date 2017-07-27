Imports System
Imports System.Data 
Imports System.Data.SQLClient
Imports System.Data.Linq.Mapping 
Imports System.Linq 
Imports DB = LinqDB.ConnectDB.SqlDB
Imports LinqDB.ConnectDB

Namespace TABLE
    'Represents a transaction for TB_TESTING_HIS table LinqDB.
    '[Create by  on July, 13 2017]
    Public Class TbTestingHisLinqDB
        Public sub TbTestingHisLinqDB()

        End Sub 
        ' TB_TESTING_HIS
        Const _tableName As String = "TB_TESTING_HIS"

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
        Dim _USER_ID As Long = 0
        Dim _USERNAME As String = ""
        Dim _TEST_ID As Long = 0
        Dim _TEST_TITLE As String = ""
        Dim _TEST_DESC As String = ""
        Dim _TARGET_PERCENTAGE As Long = 0
        Dim _COURSE_ID As Long = 0
        Dim _QUESTION_QTY As Long = 0
        Dim _TEST_SCORE As Long = 0
        Dim _TEST_RESULT As Char = "N"

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
        <Column(Storage:="_USER_ID", DbType:="BigInt NOT NULL ",CanBeNull:=false)>  _
        Public Property USER_ID() As Long
            Get
                Return _USER_ID
            End Get
            Set(ByVal value As Long)
               _USER_ID = value
            End Set
        End Property 
        <Column(Storage:="_USERNAME", DbType:="VarChar(50) NOT NULL ",CanBeNull:=false)>  _
        Public Property USERNAME() As String
            Get
                Return _USERNAME
            End Get
            Set(ByVal value As String)
               _USERNAME = value
            End Set
        End Property 
        <Column(Storage:="_TEST_ID", DbType:="BigInt NOT NULL ",CanBeNull:=false)>  _
        Public Property TEST_ID() As Long
            Get
                Return _TEST_ID
            End Get
            Set(ByVal value As Long)
               _TEST_ID = value
            End Set
        End Property 
        <Column(Storage:="_TEST_TITLE", DbType:="VarChar(255) NOT NULL ",CanBeNull:=false)>  _
        Public Property TEST_TITLE() As String
            Get
                Return _TEST_TITLE
            End Get
            Set(ByVal value As String)
               _TEST_TITLE = value
            End Set
        End Property 
        <Column(Storage:="_TEST_DESC", DbType:="VarChar(255) NOT NULL ",CanBeNull:=false)>  _
        Public Property TEST_DESC() As String
            Get
                Return _TEST_DESC
            End Get
            Set(ByVal value As String)
               _TEST_DESC = value
            End Set
        End Property 
        <Column(Storage:="_TARGET_PERCENTAGE", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property TARGET_PERCENTAGE() As Long
            Get
                Return _TARGET_PERCENTAGE
            End Get
            Set(ByVal value As Long)
               _TARGET_PERCENTAGE = value
            End Set
        End Property 
        <Column(Storage:="_COURSE_ID", DbType:="BigInt NOT NULL ",CanBeNull:=false)>  _
        Public Property COURSE_ID() As Long
            Get
                Return _COURSE_ID
            End Get
            Set(ByVal value As Long)
               _COURSE_ID = value
            End Set
        End Property 
        <Column(Storage:="_QUESTION_QTY", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property QUESTION_QTY() As Long
            Get
                Return _QUESTION_QTY
            End Get
            Set(ByVal value As Long)
               _QUESTION_QTY = value
            End Set
        End Property 
        <Column(Storage:="_TEST_SCORE", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property TEST_SCORE() As Long
            Get
                Return _TEST_SCORE
            End Get
            Set(ByVal value As Long)
               _TEST_SCORE = value
            End Set
        End Property 
        <Column(Storage:="_TEST_RESULT", DbType:="Char(1) NOT NULL ",CanBeNull:=false)>  _
        Public Property TEST_RESULT() As Char
            Get
                Return _TEST_RESULT
            End Get
            Set(ByVal value As Char)
               _TEST_RESULT = value
            End Set
        End Property 


        'Clear All Data
        Private Sub ClearData()
            _ID = 0
            _CREATED_BY = ""
            _CREATED_DATE = New DateTime(1,1,1)
            _UPDATED_BY = ""
            _UPDATED_DATE = New DateTime(1,1,1)
            _USER_ID = 0
            _USERNAME = ""
            _TEST_ID = 0
            _TEST_TITLE = ""
            _TEST_DESC = ""
            _TARGET_PERCENTAGE = 0
            _COURSE_ID = 0
            _QUESTION_QTY = 0
            _TEST_SCORE = 0
            _TEST_RESULT = "N"
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


        '/// Returns an indication whether the current data is inserted into TB_TESTING_HIS table successfully.
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


        '/// Returns an indication whether the current data is updated to TB_TESTING_HIS table successfully.
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


        '/// Returns an indication whether the current data is updated to TB_TESTING_HIS table successfully.
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


        '/// Returns an indication whether the current data is deleted from TB_TESTING_HIS table successfully.
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


        '/// Returns an indication whether the record of TB_TESTING_HIS by specified ID key is retrieved successfully.
        '/// <param name=cID>The ID key.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function ChkDataByPK(cID As Long, trans As SQLTransaction) As Boolean
            Dim p(1) As SQLParameter
            p(0) = DB.SetBigInt("@_ID", cID)
            Return doChkData("ID = @_ID", trans, p)
        End Function


        '/// Returns an indication whether the record and Mapping field to Data Class of TB_TESTING_HIS by specified ID key is retrieved successfully.
        '/// <param name=cID>The ID key.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function GetDataByPK(cID As Long, trans As SQLTransaction) As TbTestingHisLinqDB
            Dim p(1) As SQLParameter
            p(0) = DB.SetBigInt("@_ID", cID)
            Return doGetData("ID = @_ID", trans, p)
        End Function


        '/// Returns an indication whether the record of TB_TESTING_HIS by specified TEST_ID_USER_ID key is retrieved successfully.
        '/// <param name=cTEST_ID_USER_ID>The TEST_ID_USER_ID key.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function ChkDataByTEST_ID_USER_ID(cTEST_ID As Long, cUSER_ID As Long, trans As SQLTransaction) As Boolean
            Dim cmdPara(3)  As SQLParameter
            cmdPara(0) = DB.SetText("@_TEST_ID", cTEST_ID) 
            cmdPara(1) = DB.SetText("@_USER_ID", cUSER_ID) 
            Return doChkData("TEST_ID = @_TEST_ID AND USER_ID = @_USER_ID", trans, cmdPara)
        End Function

        '/// Returns an duplicate data record of TB_TESTING_HIS by specified TEST_ID_USER_ID key is retrieved successfully.
        '/// <param name=cTEST_ID_USER_ID>The TEST_ID_USER_ID key.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function ChkDuplicateByTEST_ID_USER_ID(cTEST_ID As Long, cUSER_ID As Long, cID As Long, trans As SQLTransaction) As Boolean
            Dim cmdPara(3)  As SQLParameter
            cmdPara(0) = DB.SetText("@_TEST_ID", cTEST_ID) 
            cmdPara(1) = DB.SetText("@_USER_ID", cUSER_ID) 
            cmdPara(2) = DB.SetBigInt("@_ID", cID) 
            Return doChkData("TEST_ID = @_TEST_ID AND USER_ID = @_USER_ID And ID <> @_ID", trans, cmdPara)
        End Function


        '/// Returns an indication whether the record of TB_TESTING_HIS by specified condition is retrieved successfully.
        '/// <param name=whText>The condition specify the deleting record(s).</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function ChkDataByWhere(whText As String, trans As SQLTransaction, cmdPara() As SQLParameter) As Boolean
            Return doChkData(whText, trans, cmdPara)
        End Function



        '/// Returns an indication whether the current data is inserted into TB_TESTING_HIS table successfully.
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


        '/// Returns an indication whether the current data is updated to TB_TESTING_HIS table successfully.
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


        '/// Returns an indication whether the current data is deleted from TB_TESTING_HIS table successfully.
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
            Dim cmbParam(14) As SqlParameter
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

            cmbParam(5) = New SqlParameter("@_USER_ID", SqlDbType.BigInt)
            cmbParam(5).Value = _USER_ID

            cmbParam(6) = New SqlParameter("@_USERNAME", SqlDbType.VarChar)
            cmbParam(6).Value = _USERNAME.Trim

            cmbParam(7) = New SqlParameter("@_TEST_ID", SqlDbType.BigInt)
            cmbParam(7).Value = _TEST_ID

            cmbParam(8) = New SqlParameter("@_TEST_TITLE", SqlDbType.VarChar)
            cmbParam(8).Value = _TEST_TITLE.Trim

            cmbParam(9) = New SqlParameter("@_TEST_DESC", SqlDbType.VarChar)
            cmbParam(9).Value = _TEST_DESC.Trim

            cmbParam(10) = New SqlParameter("@_TARGET_PERCENTAGE", SqlDbType.Int)
            cmbParam(10).Value = _TARGET_PERCENTAGE

            cmbParam(11) = New SqlParameter("@_COURSE_ID", SqlDbType.BigInt)
            cmbParam(11).Value = _COURSE_ID

            cmbParam(12) = New SqlParameter("@_QUESTION_QTY", SqlDbType.Int)
            cmbParam(12).Value = _QUESTION_QTY

            cmbParam(13) = New SqlParameter("@_TEST_SCORE", SqlDbType.Int)
            cmbParam(13).Value = _TEST_SCORE

            cmbParam(14) = New SqlParameter("@_TEST_RESULT", SqlDbType.Char)
            cmbParam(14).Value = _TEST_RESULT

            Return cmbParam
        End Function


        '/// Returns an indication whether the record of TB_TESTING_HIS by specified condition is retrieved successfully.
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
                        If Convert.IsDBNull(Rdr("user_id")) = False Then _user_id = Convert.ToInt64(Rdr("user_id"))
                        If Convert.IsDBNull(Rdr("username")) = False Then _username = Rdr("username").ToString()
                        If Convert.IsDBNull(Rdr("test_id")) = False Then _test_id = Convert.ToInt64(Rdr("test_id"))
                        If Convert.IsDBNull(Rdr("test_title")) = False Then _test_title = Rdr("test_title").ToString()
                        If Convert.IsDBNull(Rdr("test_desc")) = False Then _test_desc = Rdr("test_desc").ToString()
                        If Convert.IsDBNull(Rdr("target_percentage")) = False Then _target_percentage = Convert.ToInt32(Rdr("target_percentage"))
                        If Convert.IsDBNull(Rdr("course_id")) = False Then _course_id = Convert.ToInt64(Rdr("course_id"))
                        If Convert.IsDBNull(Rdr("question_qty")) = False Then _question_qty = Convert.ToInt32(Rdr("question_qty"))
                        If Convert.IsDBNull(Rdr("test_score")) = False Then _test_score = Convert.ToInt32(Rdr("test_score"))
                        If Convert.IsDBNull(Rdr("test_result")) = False Then _test_result = Rdr("test_result").ToString()
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


        '/// Returns an indication whether the record of TB_TESTING_HIS by specified condition is retrieved successfully.
        '/// <param name=whText>The condition specify the deleting record(s).</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Private Function doGetData(whText As String, trans As SQLTransaction, cmdPara() As SQLParameter) As TbTestingHisLinqDB
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
                        If Convert.IsDBNull(Rdr("user_id")) = False Then _user_id = Convert.ToInt64(Rdr("user_id"))
                        If Convert.IsDBNull(Rdr("username")) = False Then _username = Rdr("username").ToString()
                        If Convert.IsDBNull(Rdr("test_id")) = False Then _test_id = Convert.ToInt64(Rdr("test_id"))
                        If Convert.IsDBNull(Rdr("test_title")) = False Then _test_title = Rdr("test_title").ToString()
                        If Convert.IsDBNull(Rdr("test_desc")) = False Then _test_desc = Rdr("test_desc").ToString()
                        If Convert.IsDBNull(Rdr("target_percentage")) = False Then _target_percentage = Convert.ToInt32(Rdr("target_percentage"))
                        If Convert.IsDBNull(Rdr("course_id")) = False Then _course_id = Convert.ToInt64(Rdr("course_id"))
                        If Convert.IsDBNull(Rdr("question_qty")) = False Then _question_qty = Convert.ToInt32(Rdr("question_qty"))
                        If Convert.IsDBNull(Rdr("test_score")) = False Then _test_score = Convert.ToInt32(Rdr("test_score"))
                        If Convert.IsDBNull(Rdr("test_result")) = False Then _test_result = Rdr("test_result").ToString()
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


        'Get Insert Statement for table TB_TESTING_HIS
        Private ReadOnly Property SqlInsert() As String 
            Get
                Dim Sql As String=""
                Sql += "INSERT INTO " & tableName  & " (CREATED_BY, CREATED_DATE, USER_ID, USERNAME, TEST_ID, TEST_TITLE, TEST_DESC, TARGET_PERCENTAGE, COURSE_ID, QUESTION_QTY, TEST_SCORE, TEST_RESULT)"
                Sql += " OUTPUT INSERTED.ID, INSERTED.CREATED_BY, INSERTED.CREATED_DATE, INSERTED.UPDATED_BY, INSERTED.UPDATED_DATE, INSERTED.USER_ID, INSERTED.USERNAME, INSERTED.TEST_ID, INSERTED.TEST_TITLE, INSERTED.TEST_DESC, INSERTED.TARGET_PERCENTAGE, INSERTED.COURSE_ID, INSERTED.QUESTION_QTY, INSERTED.TEST_SCORE, INSERTED.TEST_RESULT"
                Sql += " VALUES("
                sql += "@_CREATED_BY" & ", "
                sql += "@_CREATED_DATE" & ", "
                sql += "@_USER_ID" & ", "
                sql += "@_USERNAME" & ", "
                sql += "@_TEST_ID" & ", "
                sql += "@_TEST_TITLE" & ", "
                sql += "@_TEST_DESC" & ", "
                sql += "@_TARGET_PERCENTAGE" & ", "
                sql += "@_COURSE_ID" & ", "
                sql += "@_QUESTION_QTY" & ", "
                sql += "@_TEST_SCORE" & ", "
                sql += "@_TEST_RESULT"
                sql += ")"
                Return sql
            End Get
        End Property


        'Get update statement form table TB_TESTING_HIS
        Private ReadOnly Property SqlUpdate() As String
            Get
                Dim Sql As String = ""
                Sql += "UPDATE " & tableName & " SET "
                Sql += "UPDATED_BY = " & "@_UPDATED_BY" & ", "
                Sql += "UPDATED_DATE = " & "@_UPDATED_DATE" & ", "
                Sql += "USER_ID = " & "@_USER_ID" & ", "
                Sql += "USERNAME = " & "@_USERNAME" & ", "
                Sql += "TEST_ID = " & "@_TEST_ID" & ", "
                Sql += "TEST_TITLE = " & "@_TEST_TITLE" & ", "
                Sql += "TEST_DESC = " & "@_TEST_DESC" & ", "
                Sql += "TARGET_PERCENTAGE = " & "@_TARGET_PERCENTAGE" & ", "
                Sql += "COURSE_ID = " & "@_COURSE_ID" & ", "
                Sql += "QUESTION_QTY = " & "@_QUESTION_QTY" & ", "
                Sql += "TEST_SCORE = " & "@_TEST_SCORE" & ", "
                Sql += "TEST_RESULT = " & "@_TEST_RESULT" + ""
                Return Sql
            End Get
        End Property


        'Get Delete Record in table TB_TESTING_HIS
        Private ReadOnly Property SqlDelete() As String
            Get
                Dim Sql As String = "DELETE FROM " & tableName
                Return Sql
            End Get
        End Property


        'Get Select Statement for table TB_TESTING_HIS
        Private ReadOnly Property SqlSelect() As String
            Get
                Dim Sql As String = "SELECT ID, CREATED_BY, CREATED_DATE, UPDATED_BY, UPDATED_DATE, USER_ID, USERNAME, TEST_ID, TEST_TITLE, TEST_DESC, TARGET_PERCENTAGE, COURSE_ID, QUESTION_QTY, TEST_SCORE, TEST_RESULT FROM " & tableName
                Return Sql
            End Get
        End Property

    End Class
End Namespace
