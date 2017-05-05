Imports System
Imports System.Data 
Imports System.Data.SQLClient
Imports System.Data.Linq.Mapping 
Imports System.Linq 
Imports DB = LinqDB.ConnectDB.SqlDB
Imports LinqDB.ConnectDB

Namespace TABLE
    'Represents a transaction for TB_TESTING_QUESTION table LinqDB.
    '[Create by  on May, 5 2017]
    Public Class TbTestingQuestionLinqDB
        Public sub TbTestingQuestionLinqDB()

        End Sub 
        ' TB_TESTING_QUESTION
        Const _tableName As String = "TB_TESTING_QUESTION"

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
        Dim _TB_TESTING_ID As Long = 0
        Dim _TEST_ID As Long = 0
        Dim _QUESTION_TITLE As String = ""
        Dim _ICON_URL As  String  = ""
        Dim _CHOICE As String = ""
        Dim _ANSWER As String = ""
        Dim _QUESTION_NO As Long = 0

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
        <Column(Storage:="_TB_TESTING_ID", DbType:="BigInt NOT NULL ",CanBeNull:=false)>  _
        Public Property TB_TESTING_ID() As Long
            Get
                Return _TB_TESTING_ID
            End Get
            Set(ByVal value As Long)
               _TB_TESTING_ID = value
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
        <Column(Storage:="_QUESTION_TITLE", DbType:="VarChar(500) NOT NULL ",CanBeNull:=false)>  _
        Public Property QUESTION_TITLE() As String
            Get
                Return _QUESTION_TITLE
            End Get
            Set(ByVal value As String)
               _QUESTION_TITLE = value
            End Set
        End Property 
        <Column(Storage:="_ICON_URL", DbType:="VarChar(500)")>  _
        Public Property ICON_URL() As  String 
            Get
                Return _ICON_URL
            End Get
            Set(ByVal value As  String )
               _ICON_URL = value
            End Set
        End Property 
        <Column(Storage:="_CHOICE", DbType:="Text NOT NULL ",CanBeNull:=false)>  _
        Public Property CHOICE() As String
            Get
                Return _CHOICE
            End Get
            Set(ByVal value As String)
               _CHOICE = value
            End Set
        End Property 
        <Column(Storage:="_ANSWER", DbType:="VarChar(255) NOT NULL ",CanBeNull:=false)>  _
        Public Property ANSWER() As String
            Get
                Return _ANSWER
            End Get
            Set(ByVal value As String)
               _ANSWER = value
            End Set
        End Property 
        <Column(Storage:="_QUESTION_NO", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property QUESTION_NO() As Long
            Get
                Return _QUESTION_NO
            End Get
            Set(ByVal value As Long)
               _QUESTION_NO = value
            End Set
        End Property 


        'Clear All Data
        Private Sub ClearData()
            _ID = 0
            _CREATED_BY = ""
            _CREATED_DATE = New DateTime(1,1,1)
            _UPDATED_BY = ""
            _UPDATED_DATE = New DateTime(1,1,1)
            _TB_TESTING_ID = 0
            _TEST_ID = 0
            _QUESTION_TITLE = ""
            _ICON_URL = ""
            _CHOICE = ""
            _ANSWER = ""
            _QUESTION_NO = 0
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


        '/// Returns an indication whether the current data is inserted into TB_TESTING_QUESTION table successfully.
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


        '/// Returns an indication whether the current data is updated to TB_TESTING_QUESTION table successfully.
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


        '/// Returns an indication whether the current data is updated to TB_TESTING_QUESTION table successfully.
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


        '/// Returns an indication whether the current data is deleted from TB_TESTING_QUESTION table successfully.
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


        '/// Returns an indication whether the record of TB_TESTING_QUESTION by specified ID key is retrieved successfully.
        '/// <param name=cID>The ID key.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function ChkDataByPK(cID As Long, trans As SQLTransaction) As Boolean
            Dim p(1) As SQLParameter
            p(0) = DB.SetBigInt("@_ID", cID)
            Return doChkData("ID = @_ID", trans, p)
        End Function


        '/// Returns an indication whether the record and Mapping field to Data Class of TB_TESTING_QUESTION by specified ID key is retrieved successfully.
        '/// <param name=cID>The ID key.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function GetDataByPK(cID As Long, trans As SQLTransaction) As TbTestingQuestionLinqDB
            Dim p(1) As SQLParameter
            p(0) = DB.SetBigInt("@_ID", cID)
            Return doGetData("ID = @_ID", trans, p)
        End Function


        '/// Returns an indication whether the record of TB_TESTING_QUESTION by specified QUESTION_TITLE_TB_TESTING_ID key is retrieved successfully.
        '/// <param name=cQUESTION_TITLE_TB_TESTING_ID>The QUESTION_TITLE_TB_TESTING_ID key.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function ChkDataByQUESTION_TITLE_TB_TESTING_ID(cQUESTION_TITLE As String, cTB_TESTING_ID As Long, trans As SQLTransaction) As Boolean
            Dim cmdPara(3)  As SQLParameter
            cmdPara(0) = DB.SetText("@_QUESTION_TITLE", cQUESTION_TITLE) 
            cmdPara(1) = DB.SetText("@_TB_TESTING_ID", cTB_TESTING_ID) 
            Return doChkData("QUESTION_TITLE = @_QUESTION_TITLE AND TB_TESTING_ID = @_TB_TESTING_ID", trans, cmdPara)
        End Function

        '/// Returns an duplicate data record of TB_TESTING_QUESTION by specified QUESTION_TITLE_TB_TESTING_ID key is retrieved successfully.
        '/// <param name=cQUESTION_TITLE_TB_TESTING_ID>The QUESTION_TITLE_TB_TESTING_ID key.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function ChkDuplicateByQUESTION_TITLE_TB_TESTING_ID(cQUESTION_TITLE As String, cTB_TESTING_ID As Long, cID As Long, trans As SQLTransaction) As Boolean
            Dim cmdPara(3)  As SQLParameter
            cmdPara(0) = DB.SetText("@_QUESTION_TITLE", cQUESTION_TITLE) 
            cmdPara(1) = DB.SetText("@_TB_TESTING_ID", cTB_TESTING_ID) 
            cmdPara(2) = DB.SetBigInt("@_ID", cID) 
            Return doChkData("QUESTION_TITLE = @_QUESTION_TITLE AND TB_TESTING_ID = @_TB_TESTING_ID And ID <> @_ID", trans, cmdPara)
        End Function


        '/// Returns an indication whether the record of TB_TESTING_QUESTION by specified condition is retrieved successfully.
        '/// <param name=whText>The condition specify the deleting record(s).</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function ChkDataByWhere(whText As String, trans As SQLTransaction, cmdPara() As SQLParameter) As Boolean
            Return doChkData(whText, trans, cmdPara)
        End Function



        '/// Returns an indication whether the current data is inserted into TB_TESTING_QUESTION table successfully.
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


        '/// Returns an indication whether the current data is updated to TB_TESTING_QUESTION table successfully.
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


        '/// Returns an indication whether the current data is deleted from TB_TESTING_QUESTION table successfully.
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
            Dim cmbParam(11) As SqlParameter
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

            cmbParam(5) = New SqlParameter("@_TB_TESTING_ID", SqlDbType.BigInt)
            cmbParam(5).Value = _TB_TESTING_ID

            cmbParam(6) = New SqlParameter("@_TEST_ID", SqlDbType.BigInt)
            cmbParam(6).Value = _TEST_ID

            cmbParam(7) = New SqlParameter("@_QUESTION_TITLE", SqlDbType.VarChar)
            cmbParam(7).Value = _QUESTION_TITLE.Trim

            cmbParam(8) = New SqlParameter("@_ICON_URL", SqlDbType.VarChar)
            If _ICON_URL.Trim <> "" Then 
                cmbParam(8).Value = _ICON_URL.Trim
            Else
                cmbParam(8).Value = DBNull.value
            End If

            cmbParam(9) = New SqlParameter("@_CHOICE", SqlDbType.Text)
            cmbParam(9).Value = _CHOICE.Trim

            cmbParam(10) = New SqlParameter("@_ANSWER", SqlDbType.VarChar)
            cmbParam(10).Value = _ANSWER.Trim

            cmbParam(11) = New SqlParameter("@_QUESTION_NO", SqlDbType.Int)
            cmbParam(11).Value = _QUESTION_NO

            Return cmbParam
        End Function


        '/// Returns an indication whether the record of TB_TESTING_QUESTION by specified condition is retrieved successfully.
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
                        If Convert.IsDBNull(Rdr("tb_testing_id")) = False Then _tb_testing_id = Convert.ToInt64(Rdr("tb_testing_id"))
                        If Convert.IsDBNull(Rdr("test_id")) = False Then _test_id = Convert.ToInt64(Rdr("test_id"))
                        If Convert.IsDBNull(Rdr("question_title")) = False Then _question_title = Rdr("question_title").ToString()
                        If Convert.IsDBNull(Rdr("icon_url")) = False Then _icon_url = Rdr("icon_url").ToString()
                        If Convert.IsDBNull(Rdr("choice")) = False Then _choice = Rdr("choice").ToString()
                        If Convert.IsDBNull(Rdr("answer")) = False Then _answer = Rdr("answer").ToString()
                        If Convert.IsDBNull(Rdr("question_no")) = False Then _question_no = Convert.ToInt32(Rdr("question_no"))
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


        '/// Returns an indication whether the record of TB_TESTING_QUESTION by specified condition is retrieved successfully.
        '/// <param name=whText>The condition specify the deleting record(s).</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Private Function doGetData(whText As String, trans As SQLTransaction, cmdPara() As SQLParameter) As TbTestingQuestionLinqDB
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
                        If Convert.IsDBNull(Rdr("tb_testing_id")) = False Then _tb_testing_id = Convert.ToInt64(Rdr("tb_testing_id"))
                        If Convert.IsDBNull(Rdr("test_id")) = False Then _test_id = Convert.ToInt64(Rdr("test_id"))
                        If Convert.IsDBNull(Rdr("question_title")) = False Then _question_title = Rdr("question_title").ToString()
                        If Convert.IsDBNull(Rdr("icon_url")) = False Then _icon_url = Rdr("icon_url").ToString()
                        If Convert.IsDBNull(Rdr("choice")) = False Then _choice = Rdr("choice").ToString()
                        If Convert.IsDBNull(Rdr("answer")) = False Then _answer = Rdr("answer").ToString()
                        If Convert.IsDBNull(Rdr("question_no")) = False Then _question_no = Convert.ToInt32(Rdr("question_no"))
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


        'Get Insert Statement for table TB_TESTING_QUESTION
        Private ReadOnly Property SqlInsert() As String 
            Get
                Dim Sql As String=""
                Sql += "INSERT INTO " & tableName  & " (CREATED_BY, CREATED_DATE, TB_TESTING_ID, TEST_ID, QUESTION_TITLE, ICON_URL, CHOICE, ANSWER, QUESTION_NO)"
                Sql += " OUTPUT INSERTED.ID, INSERTED.CREATED_BY, INSERTED.CREATED_DATE, INSERTED.UPDATED_BY, INSERTED.UPDATED_DATE, INSERTED.TB_TESTING_ID, INSERTED.TEST_ID, INSERTED.QUESTION_TITLE, INSERTED.ICON_URL, INSERTED.CHOICE, INSERTED.ANSWER, INSERTED.QUESTION_NO"
                Sql += " VALUES("
                sql += "@_CREATED_BY" & ", "
                sql += "@_CREATED_DATE" & ", "
                sql += "@_TB_TESTING_ID" & ", "
                sql += "@_TEST_ID" & ", "
                sql += "@_QUESTION_TITLE" & ", "
                sql += "@_ICON_URL" & ", "
                sql += "@_CHOICE" & ", "
                sql += "@_ANSWER" & ", "
                sql += "@_QUESTION_NO"
                sql += ")"
                Return sql
            End Get
        End Property


        'Get update statement form table TB_TESTING_QUESTION
        Private ReadOnly Property SqlUpdate() As String
            Get
                Dim Sql As String = ""
                Sql += "UPDATE " & tableName & " SET "
                Sql += "UPDATED_BY = " & "@_UPDATED_BY" & ", "
                Sql += "UPDATED_DATE = " & "@_UPDATED_DATE" & ", "
                Sql += "TB_TESTING_ID = " & "@_TB_TESTING_ID" & ", "
                Sql += "TEST_ID = " & "@_TEST_ID" & ", "
                Sql += "QUESTION_TITLE = " & "@_QUESTION_TITLE" & ", "
                Sql += "ICON_URL = " & "@_ICON_URL" & ", "
                Sql += "CHOICE = " & "@_CHOICE" & ", "
                Sql += "ANSWER = " & "@_ANSWER" & ", "
                Sql += "QUESTION_NO = " & "@_QUESTION_NO" + ""
                Return Sql
            End Get
        End Property


        'Get Delete Record in table TB_TESTING_QUESTION
        Private ReadOnly Property SqlDelete() As String
            Get
                Dim Sql As String = "DELETE FROM " & tableName
                Return Sql
            End Get
        End Property


        'Get Select Statement for table TB_TESTING_QUESTION
        Private ReadOnly Property SqlSelect() As String
            Get
                Dim Sql As String = "SELECT ID, CREATED_BY, CREATED_DATE, UPDATED_BY, UPDATED_DATE, TB_TESTING_ID, TEST_ID, QUESTION_TITLE, ICON_URL, CHOICE, ANSWER, QUESTION_NO FROM " & tableName
                Return Sql
            End Get
        End Property

    End Class
End Namespace
