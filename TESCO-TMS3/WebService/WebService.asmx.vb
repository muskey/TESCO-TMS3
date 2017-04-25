Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel
Imports Newtonsoft.Json.Linq

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
' <System.Web.Script.Services.ScriptService()> _
<System.Web.Script.Services.ScriptService()>
<WebService(Namespace:="http://tempuri.org/")>
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)>
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Public Class WebService
    Inherits System.Web.Services.WebService

    <WebMethod()>
    Public Function HelloWorld() As String
        Return "Hello World"
    End Function

    <WebMethod()>
    Public Function LoadCoureseDescByID(id) As String
        Try
            Dim strSql As New StringBuilder
            Dim strdata As String
            Dim sql As String = "select course_desc "
            sql += " from tb_user_course "
            sql += " where id=" & id

            Dim dt As DataTable
            dt = GetSqlDataTable(sql)
            If dt.Rows.Count > 0 Then
                strdata = dt.Rows(0)(0) & ""
            Else
                strdata = ""
            End If
            Return strdata
        Catch ex As Exception
            Return ""
        End Try

    End Function

    <WebMethod()>
    Public Function SetDocumentData(id) As String
        Try


            Dim document_txt As String = ""
            Dim sql As String = " select document_detail from TB_USER_COURSE where id=" & id '& " and bind_document='N'"
            Dim dt As DataTable = GetSqlDataTable(sql)
            If dt.Rows.Count > 0 Then
                sql = "delete from TB_USER_COURSE_DOCUMENT_FILE where tb_user_course_document_id in (select id from TB_USER_COURSE_DOCUMENT where tb_user_course_id=" & id & ") "
                ExecuteSqlNoneQuery(sql)

                sql = " delete from TB_USER_COURSE_DOCUMENT  where tb_user_course_id=" & id
                ExecuteSqlNoneQuery(sql)

                If Convert.IsDBNull(dt.Rows(0)("document_detail")) = False Then
                    document_txt = dt.Rows(0)("document_detail")
                End If
            End If
            dt.Dispose()

            If document_txt.Trim <> "" Then


                Dim document_ser As JObject = JObject.Parse(document_txt)
                Dim document_data As List(Of JToken) = document_ser.Children().ToList
                For Each document_item As JProperty In document_data

                    Dim cdi As Integer = 1
                    For Each document_comment As JObject In document_item.Values
                        document_item.CreateReader()

                        sql = "insert into TB_USER_COURSE_DOCUMENT (document_id,tb_user_course_id,document_title,ms_document_icon_id,document_version,document_type,order_by,user_id)"
                        sql += " values('" & document_comment("id").ToString & "'"
                        sql += ", '" & id & "'"
                        sql += ", '" & document_comment("title").ToString & "'"
                        sql += ", '" & document_comment("icon_id").ToString & "'"
                        sql += ", '" & document_comment("version").ToString & "'"
                        sql += ", '" & document_comment("type").ToString & "'"
                        sql += ", '" & document_comment("order").ToString & "'"
                        sql += ", '" & UserData.UserID & "')"






                        If ExecuteSqlNoneQuery(sql) = True Then
                            Dim docnewid As String = GetSqlDataTable("select max(id) from TB_USER_COURSE_DOCUMENT").Rows(0)(0) & ""
                            Dim doc_id As Object = docnewid 'document_comment("id")
                            Dim file_txt As String = "{""file"":" & document_comment("file").ToString & "}"
                            Dim file_ser As JObject = JObject.Parse(file_txt)
                            Dim file_data As List(Of JToken) = file_ser.Children().ToList
                            For Each file_item As JProperty In file_data

                                Dim cdfi As Integer = 1
                                For Each file_comment As JObject In file_item.Values
                                    file_item.CreateReader()

                                    Dim FileName As String = file_comment("id").ToString & GetURLFileExtension(file_comment("file").ToString)
                                    Dim DocFileID As String = EnCripText(FileName)
                                    Dim DocFileName As String = "null"


                                    sql = "insert into TB_USER_COURSE_DOCUMENT_FILE (document_file_id,tb_user_course_document_id,file_title,file_url,order_by,user_id)"
                                    sql += " values('" & file_comment("id").ToString & "'"
                                    sql += ", '" & doc_id & "'"
                                    sql += ", '" & file_comment("title").ToString & "'"
                                    sql += ", '" & file_comment("file").ToString & "'"
                                    sql += ", '" & file_comment("order").ToString & "'"
                                    sql += ", '" & UserData.UserID & "')"
                                    ExecuteSqlNoneQuery(sql)
                                    cdfi += 1

                                Next
                            Next
                        End If
                        cdi += 1


                    Next
                Next


                ExecuteSqlNoneQuery(sql)

            End If

            Return "True"
        Catch ex As Exception
            Return "False"
        End Try
    End Function

#Region "Convert DataTable To Json"
    Public Function ConvertDataTableToJson(ByVal dt As DataTable) As String
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        serializer.MaxJsonLength = Int32.MaxValue
        Dim rows As New List(Of Dictionary(Of String, Object))
        Dim row As Dictionary(Of String, Object)
        For Each dr As DataRow In dt.Rows
            row = New Dictionary(Of String, Object)
            For Each col As DataColumn In dt.Columns
                row.Add(col.ColumnName, dr(col))
            Next
            rows.Add(row)
        Next
        Return serializer.Serialize(rows)
    End Function
#End Region
End Class