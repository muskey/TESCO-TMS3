Imports System
Imports System.Data 
Imports System.Data.SQLClient
Imports System.Data.Linq.Mapping 
Imports System.Linq 
Imports DB = LinqDB.ConnectDB.SqlDB
Imports LinqDB.ConnectDB

Namespace TABLE
    'Represents a transaction for TB_USER_COURSE_DOCUMENT table LinqDB.
    '[Create by  on April, 12 2017]
    Public Class TbUserCourseDocumentLinqDB
        Public sub TbUserCourseDocumentLinqDB()

        End Sub 
        ' TB_USER_COURSE_DOCUMENT
        Const _tableName As String = "TB_USER_COURSE_DOCUMENT"

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
        Dim _TB_USER_COURSE_ID As Long = 0
        Dim _USER_ID As Long = 0
        Dim _DOCUMENT_ID As Long = 0
        Dim _DOCUMENT_TITLE As String = ""
        Dim _MS_DOCUMENT_ICON_ID As Long = 0
        Dim _DOCUMENT_VERSION As  String  = ""
        Dim _DOCUMENT_TYPE As String = ""
        Dim _ORDER_BY As Long = 0

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
        <Column(Storage:="_TB_USER_COURSE_ID", DbType:="BigInt NOT NULL ",CanBeNull:=false)>  _
        Public Property TB_USER_COURSE_ID() As Long
            Get
                Return _TB_USER_COURSE_ID
            End Get
            Set(ByVal value As Long)
               _TB_USER_COURSE_ID = value
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
        <Column(Storage:="_DOCUMENT_ID", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property DOCUMENT_ID() As Long
            Get
                Return _DOCUMENT_ID
            End Get
            Set(ByVal value As Long)
               _DOCUMENT_ID = value
            End Set
        End Property 
        <Column(Storage:="_DOCUMENT_TITLE", DbType:="VarChar(255) NOT NULL ",CanBeNull:=false)>  _
        Public Property DOCUMENT_TITLE() As String
            Get
                Return _DOCUMENT_TITLE
            End Get
            Set(ByVal value As String)
               _DOCUMENT_TITLE = value
            End Set
        End Property 
        <Column(Storage:="_MS_DOCUMENT_ICON_ID", DbType:="BigInt NOT NULL ",CanBeNull:=false)>  _
        Public Property MS_DOCUMENT_ICON_ID() As Long
            Get
                Return _MS_DOCUMENT_ICON_ID
            End Get
            Set(ByVal value As Long)
               _MS_DOCUMENT_ICON_ID = value
            End Set
        End Property 
        <Column(Storage:="_DOCUMENT_VERSION", DbType:="VarChar(50)")>  _
        Public Property DOCUMENT_VERSION() As  String 
            Get
                Return _DOCUMENT_VERSION
            End Get
            Set(ByVal value As  String )
               _DOCUMENT_VERSION = value
            End Set
        End Property 
        <Column(Storage:="_DOCUMENT_TYPE", DbType:="VarChar(50) NOT NULL ",CanBeNull:=false)>  _
        Public Property DOCUMENT_TYPE() As String
            Get
                Return _DOCUMENT_TYPE
            End Get
            Set(ByVal value As String)
               _DOCUMENT_TYPE = value
            End Set
        End Property 
        <Column(Storage:="_ORDER_BY", DbType:="Int NOT NULL ",CanBeNull:=false)>  _
        Public Property ORDER_BY() As Long
            Get
                Return _ORDER_BY
            End Get
            Set(ByVal value As Long)
               _ORDER_BY = value
            End Set
        End Property 


        'Clear All Data
        Private Sub ClearData()
            _ID = 0
            _CREATED_BY = ""
            _CREATED_DATE = New DateTime(1,1,1)
            _UPDATED_BY = ""
            _UPDATED_DATE = New DateTime(1,1,1)
            _TB_USER_COURSE_ID = 0
            _USER_ID = 0
            _DOCUMENT_ID = 0
            _DOCUMENT_TITLE = ""
            _MS_DOCUMENT_ICON_ID = 0
            _DOCUMENT_VERSION = ""
            _DOCUMENT_TYPE = ""
            _ORDER_BY = 0
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


        '/// Returns an indication whether the current data is inserted into TB_USER_COURSE_DOCUMENT table successfully.
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


        '/// Returns an indication whether the current data is updated to TB_USER_COURSE_DOCUMENT table successfully.
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


        '/// Returns an indication whether the current data is updated to TB_USER_COURSE_DOCUMENT table successfully.
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


        '/// Returns an indication whether the current data is deleted from TB_USER_COURSE_DOCUMENT table successfully.
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


        '/// Returns an indication whether the record of TB_USER_COURSE_DOCUMENT by specified ID key is retrieved successfully.
        '/// <param name=cID>The ID key.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function ChkDataByPK(cID As Long, trans As SQLTransaction) As Boolean
            Dim p(1) As SQLParameter
            p(0) = DB.SetBigInt("@_ID", cID)
            Return doChkData("ID = @_ID", trans, p)
        End Function


        '/// Returns an indication whether the record and Mapping field to Data Class of TB_USER_COURSE_DOCUMENT by specified ID key is retrieved successfully.
        '/// <param name=cID>The ID key.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function GetDataByPK(cID As Long, trans As SQLTransaction) As TbUserCourseDocumentLinqDB
            Dim p(1) As SQLParameter
            p(0) = DB.SetBigInt("@_ID", cID)
            Return doGetData("ID = @_ID", trans, p)
        End Function


        '/// Returns an indication whether the record of TB_USER_COURSE_DOCUMENT by specified DOCUMENT_TITLE_TB_USER_COURSE_ID key is retrieved successfully.
        '/// <param name=cDOCUMENT_TITLE_TB_USER_COURSE_ID>The DOCUMENT_TITLE_TB_USER_COURSE_ID key.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function ChkDataByDOCUMENT_TITLE_TB_USER_COURSE_ID(cDOCUMENT_TITLE As String, cTB_USER_COURSE_ID As Long, trans As SQLTransaction) As Boolean
            Dim cmdPara(3)  As SQLParameter
            cmdPara(0) = DB.SetText("@_DOCUMENT_TITLE", cDOCUMENT_TITLE) 
            cmdPara(1) = DB.SetText("@_TB_USER_COURSE_ID", cTB_USER_COURSE_ID) 
            Return doChkData("DOCUMENT_TITLE = @_DOCUMENT_TITLE AND TB_USER_COURSE_ID = @_TB_USER_COURSE_ID", trans, cmdPara)
        End Function

        '/// Returns an duplicate data record of TB_USER_COURSE_DOCUMENT by specified DOCUMENT_TITLE_TB_USER_COURSE_ID key is retrieved successfully.
        '/// <param name=cDOCUMENT_TITLE_TB_USER_COURSE_ID>The DOCUMENT_TITLE_TB_USER_COURSE_ID key.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function ChkDuplicateByDOCUMENT_TITLE_TB_USER_COURSE_ID(cDOCUMENT_TITLE As String, cTB_USER_COURSE_ID As Long, cID As Long, trans As SQLTransaction) As Boolean
            Dim cmdPara(3)  As SQLParameter
            cmdPara(0) = DB.SetText("@_DOCUMENT_TITLE", cDOCUMENT_TITLE) 
            cmdPara(1) = DB.SetText("@_TB_USER_COURSE_ID", cTB_USER_COURSE_ID) 
            cmdPara(2) = DB.SetBigInt("@_ID", cID) 
            Return doChkData("DOCUMENT_TITLE = @_DOCUMENT_TITLE AND TB_USER_COURSE_ID = @_TB_USER_COURSE_ID And ID <> @_ID", trans, cmdPara)
        End Function


        '/// Returns an indication whether the record of TB_USER_COURSE_DOCUMENT by specified condition is retrieved successfully.
        '/// <param name=whText>The condition specify the deleting record(s).</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function ChkDataByWhere(whText As String, trans As SQLTransaction, cmdPara() As SQLParameter) As Boolean
            Return doChkData(whText, trans, cmdPara)
        End Function



        '/// Returns an indication whether the current data is inserted into TB_USER_COURSE_DOCUMENT table successfully.
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


        '/// Returns an indication whether the current data is updated to TB_USER_COURSE_DOCUMENT table successfully.
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


        '/// Returns an indication whether the current data is deleted from TB_USER_COURSE_DOCUMENT table successfully.
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

            cmbParam(5) = New SqlParameter("@_TB_USER_COURSE_ID", SqlDbType.BigInt)
            cmbParam(5).Value = _TB_USER_COURSE_ID

            cmbParam(6) = New SqlParameter("@_USER_ID", SqlDbType.Int)
            cmbParam(6).Value = _USER_ID

            cmbParam(7) = New SqlParameter("@_DOCUMENT_ID", SqlDbType.Int)
            cmbParam(7).Value = _DOCUMENT_ID

            cmbParam(8) = New SqlParameter("@_DOCUMENT_TITLE", SqlDbType.VarChar)
            cmbParam(8).Value = _DOCUMENT_TITLE.Trim

            cmbParam(9) = New SqlParameter("@_MS_DOCUMENT_ICON_ID", SqlDbType.BigInt)
            cmbParam(9).Value = _MS_DOCUMENT_ICON_ID

            cmbParam(10) = New SqlParameter("@_DOCUMENT_VERSION", SqlDbType.VarChar)
            If _DOCUMENT_VERSION.Trim <> "" Then 
                cmbParam(10).Value = _DOCUMENT_VERSION.Trim
            Else
                cmbParam(10).Value = DBNull.value
            End If

            cmbParam(11) = New SqlParameter("@_DOCUMENT_TYPE", SqlDbType.VarChar)
            cmbParam(11).Value = _DOCUMENT_TYPE.Trim

            cmbParam(12) = New SqlParameter("@_ORDER_BY", SqlDbType.Int)
            cmbParam(12).Value = _ORDER_BY

            Return cmbParam
        End Function


        '/// Returns an indication whether the record of TB_USER_COURSE_DOCUMENT by specified condition is retrieved successfully.
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
                        If Convert.IsDBNull(Rdr("tb_user_course_id")) = False Then _tb_user_course_id = Convert.ToInt64(Rdr("tb_user_course_id"))
                        If Convert.IsDBNull(Rdr("user_id")) = False Then _user_id = Convert.ToInt32(Rdr("user_id"))
                        If Convert.IsDBNull(Rdr("document_id")) = False Then _document_id = Convert.ToInt32(Rdr("document_id"))
                        If Convert.IsDBNull(Rdr("document_title")) = False Then _document_title = Rdr("document_title").ToString()
                        If Convert.IsDBNull(Rdr("ms_document_icon_id")) = False Then _ms_document_icon_id = Convert.ToInt64(Rdr("ms_document_icon_id"))
                        If Convert.IsDBNull(Rdr("document_version")) = False Then _document_version = Rdr("document_version").ToString()
                        If Convert.IsDBNull(Rdr("document_type")) = False Then _document_type = Rdr("document_type").ToString()
                        If Convert.IsDBNull(Rdr("order_by")) = False Then _order_by = Convert.ToInt32(Rdr("order_by"))
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


        '/// Returns an indication whether the record of TB_USER_COURSE_DOCUMENT by specified condition is retrieved successfully.
        '/// <param name=whText>The condition specify the deleting record(s).</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Private Function doGetData(whText As String, trans As SQLTransaction, cmdPara() As SQLParameter) As TbUserCourseDocumentLinqDB
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
                        If Convert.IsDBNull(Rdr("tb_user_course_id")) = False Then _tb_user_course_id = Convert.ToInt64(Rdr("tb_user_course_id"))
                        If Convert.IsDBNull(Rdr("user_id")) = False Then _user_id = Convert.ToInt32(Rdr("user_id"))
                        If Convert.IsDBNull(Rdr("document_id")) = False Then _document_id = Convert.ToInt32(Rdr("document_id"))
                        If Convert.IsDBNull(Rdr("document_title")) = False Then _document_title = Rdr("document_title").ToString()
                        If Convert.IsDBNull(Rdr("ms_document_icon_id")) = False Then _ms_document_icon_id = Convert.ToInt64(Rdr("ms_document_icon_id"))
                        If Convert.IsDBNull(Rdr("document_version")) = False Then _document_version = Rdr("document_version").ToString()
                        If Convert.IsDBNull(Rdr("document_type")) = False Then _document_type = Rdr("document_type").ToString()
                        If Convert.IsDBNull(Rdr("order_by")) = False Then _order_by = Convert.ToInt32(Rdr("order_by"))
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


        'Get Insert Statement for table TB_USER_COURSE_DOCUMENT
        Private ReadOnly Property SqlInsert() As String 
            Get
                Dim Sql As String=""
                Sql += "INSERT INTO " & tableName  & " (CREATED_BY, CREATED_DATE, TB_USER_COURSE_ID, USER_ID, DOCUMENT_ID, DOCUMENT_TITLE, MS_DOCUMENT_ICON_ID, DOCUMENT_VERSION, DOCUMENT_TYPE, ORDER_BY)"
                Sql += " OUTPUT INSERTED.ID, INSERTED.CREATED_BY, INSERTED.CREATED_DATE, INSERTED.UPDATED_BY, INSERTED.UPDATED_DATE, INSERTED.TB_USER_COURSE_ID, INSERTED.USER_ID, INSERTED.DOCUMENT_ID, INSERTED.DOCUMENT_TITLE, INSERTED.MS_DOCUMENT_ICON_ID, INSERTED.DOCUMENT_VERSION, INSERTED.DOCUMENT_TYPE, INSERTED.ORDER_BY"
                Sql += " VALUES("
                sql += "@_CREATED_BY" & ", "
                sql += "@_CREATED_DATE" & ", "
                sql += "@_TB_USER_COURSE_ID" & ", "
                sql += "@_USER_ID" & ", "
                sql += "@_DOCUMENT_ID" & ", "
                sql += "@_DOCUMENT_TITLE" & ", "
                sql += "@_MS_DOCUMENT_ICON_ID" & ", "
                sql += "@_DOCUMENT_VERSION" & ", "
                sql += "@_DOCUMENT_TYPE" & ", "
                sql += "@_ORDER_BY"
                sql += ")"
                Return sql
            End Get
        End Property


        'Get update statement form table TB_USER_COURSE_DOCUMENT
        Private ReadOnly Property SqlUpdate() As String
            Get
                Dim Sql As String = ""
                Sql += "UPDATE " & tableName & " SET "
                Sql += "UPDATED_BY = " & "@_UPDATED_BY" & ", "
                Sql += "UPDATED_DATE = " & "@_UPDATED_DATE" & ", "
                Sql += "TB_USER_COURSE_ID = " & "@_TB_USER_COURSE_ID" & ", "
                Sql += "USER_ID = " & "@_USER_ID" & ", "
                Sql += "DOCUMENT_ID = " & "@_DOCUMENT_ID" & ", "
                Sql += "DOCUMENT_TITLE = " & "@_DOCUMENT_TITLE" & ", "
                Sql += "MS_DOCUMENT_ICON_ID = " & "@_MS_DOCUMENT_ICON_ID" & ", "
                Sql += "DOCUMENT_VERSION = " & "@_DOCUMENT_VERSION" & ", "
                Sql += "DOCUMENT_TYPE = " & "@_DOCUMENT_TYPE" & ", "
                Sql += "ORDER_BY = " & "@_ORDER_BY" + ""
                Return Sql
            End Get
        End Property


        'Get Delete Record in table TB_USER_COURSE_DOCUMENT
        Private ReadOnly Property SqlDelete() As String
            Get
                Dim Sql As String = "DELETE FROM " & tableName
                Return Sql
            End Get
        End Property


        'Get Select Statement for table TB_USER_COURSE_DOCUMENT
        Private ReadOnly Property SqlSelect() As String
            Get
                Dim Sql As String = "SELECT ID, CREATED_BY, CREATED_DATE, UPDATED_BY, UPDATED_DATE, TB_USER_COURSE_ID, USER_ID, DOCUMENT_ID, DOCUMENT_TITLE, MS_DOCUMENT_ICON_ID, DOCUMENT_VERSION, DOCUMENT_TYPE, ORDER_BY FROM " & tableName
                Return Sql
            End Get
        End Property

    End Class
End Namespace
