﻿Imports Newtonsoft.Json.Linq
Imports LinqDB.ConnectDB
Imports LinqDB.TABLE
Imports System.Data.SqlClient

Public Class ConvertCourseENG
    Public Shared Function ConvertCourseData()
        Try
            Dim dLnq As New TbUserDepartmentLinqDB
            Dim dt As DataTable = dLnq.GetDataList("bind_course='N'", "", Nothing, Nothing)
            If dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    Dim UserID As Long = dr("user_id")
                    Dim Username As String = dr("created_by")

                    Dim trans As New TransactionDB
                    Dim ret As New ExecuteDataInfo

                    If (Convert.IsDBNull(dr("course_detail")) = True) Then
                        dLnq = New TbUserDepartmentLinqDB
                        dLnq.GetDataByPK(dr("id"), Nothing)
                        dLnq.BIND_COURSE = "Y"
                        ret = dLnq.UpdateData(Username, trans.Trans)
                        If ret.IsSuccess = True Then
                            trans.CommitTransaction()
                        Else
                            trans.RollbackTransaction()
                        End If
                        Continue For
                    End If



                    Dim document_ser As JObject = JObject.Parse(dr("course_detail"))
                    Dim document_data As List(Of JToken) = document_ser.Children().ToList
                    For Each document_item As JProperty In document_data
                        If document_item.Values.Count = 0 Then
                            ret.IsSuccess = True
                            Continue For
                        End If

                        For Each comment As JObject In document_item.Values
                            document_item.CreateReader()
                            Try
                                Dim lnq As New TbUserCourseLinqDB
                                lnq.TB_USER_DEPARTMENT_ID = dr("id")
                                lnq.USER_ID = dr("user_id")
                                lnq.DEPARTMENT_ID = comment("department_id").ToString
                                lnq.COURSE_ID = comment("id").ToString
                                lnq.COURSE_TITLE = comment("name").ToString
                                lnq.COURSE_DESC = comment("description").ToString
                                lnq.COURSE_TYPE = comment("type").ToString
                                lnq.ICON_URL = comment("icon").ToString
                                lnq.COVER_URL = comment("cover").ToString
                                lnq.IS_DOCUMENT_LOCK = IIf(comment("is_document_lock").ToString.ToLower = "true", "Y", "N")
                                If comment("prerequisite_course_id").ToString.Trim <> "" Then lnq.PREREQUISITE_COURSE_ID = comment("prerequisite_course_id").ToString
                                lnq.IS_FINISHED = IIf(comment("is_finished").ToString.ToLower = "true", "Y", "N")
                                'lnq.DOCUMENT_DETAIL = "{""document"":" & comment("document").ToString & "}"
                                'lnq.BIND_DOCUMENT = "N"

                                ret = lnq.InsertData(Username, trans.Trans)
                                If ret.IsSuccess = True Then
                                    Dim DocumentText As String = "{""document"":" & comment("document").ToString & "}"

                                    ret = ConvertCourseDocumentData(UserID, Username, lnq.ID, DocumentText, trans)
                                    If ret.IsSuccess = False Then
                                        Exit For
                                    End If
                                End If
                            Catch ex As Exception
                                ret.IsSuccess = False
                                ret.ErrorMessage = ex.Message
                                Exit For
                            End Try
                            'ci += 1
                        Next
                        If ret.IsSuccess = False Then
                            Exit For
                        End If
                    Next

                    If ret.IsSuccess = True Then
                        dLnq = New TbUserDepartmentLinqDB
                        dLnq.GetDataByPK(dr("id"), trans.Trans)
                        If dLnq.ID > 0 Then
                            dLnq.BIND_COURSE = "Y"

                            ret = dLnq.UpdateData(Username, trans.Trans)

                            If ret.IsSuccess = True Then
                                trans.CommitTransaction()
                            Else
                                trans.RollbackTransaction()
                            End If
                        Else
                            trans.RollbackTransaction()
                        End If
                    Else
                        trans.RollbackTransaction()
                    End If
                Next
            End If
            dt.Dispose()
            dLnq = Nothing
        Catch ex As Exception

        End Try
    End Function

    Private Shared Function ConvertCourseDocumentData(UserID As Long, Username As String, UserCourseId As String, document_txt As String, trans As TransactionDB) As ExecuteDataInfo
        Dim ret As New ExecuteDataInfo
        Try
            If document_txt.Trim <> "" Then
                Dim document_ser As JObject = JObject.Parse(document_txt)
                Dim document_data As List(Of JToken) = document_ser.Children().ToList
                For Each document_item As JProperty In document_data
                    If document_item.Values.Count = 0 Then
                        ret.IsSuccess = True
                        Continue For
                    End If


                    For Each document_comment As JObject In document_item.Values
                        document_item.CreateReader()

                        Dim cdLnq As New LinqDB.TABLE.TbUserCourseDocumentLinqDB
                        cdLnq.DOCUMENT_ID = Convert.ToInt64(document_comment("id").ToString)
                        cdLnq.TB_USER_COURSE_ID = UserCourseId
                        cdLnq.USER_ID = UserID
                        cdLnq.DOCUMENT_TITLE = document_comment("title").ToString
                        cdLnq.MS_DOCUMENT_ICON_ID = document_comment("icon_id").ToString
                        cdLnq.DOCUMENT_VERSION = document_comment("version").ToString
                        cdLnq.DOCUMENT_TYPE = document_comment("type").ToString
                        cdLnq.ORDER_BY = document_comment("order").ToString

                        ret = cdLnq.InsertData(Username, trans.Trans)
                        If ret.IsSuccess = True Then
                            Dim file_txt As String = "{""file"":" & document_comment("file").ToString & "}"
                            Dim file_ser As JObject = JObject.Parse(file_txt)
                            Dim file_data As List(Of JToken) = file_ser.Children().ToList
                            For Each file_item As JProperty In file_data

                                For Each file_comment As JObject In file_item.Values
                                    file_item.CreateReader()

                                    Dim vUrl As New Uri(file_comment("file"))
                                    Dim vFile As String = vUrl.Segments(vUrl.Segments.Length - 1)

                                    Dim StoryFile As String = file_comment("file").ToString
                                    If vFile.StartsWith("index_lms.html") = True Then
                                        StoryFile = file_comment("file").ToString.Replace("index_lms.html", "story.html")

                                    End If

                                    Dim FileExt As String = GetURLFileExtension(file_comment("file").ToString)
                                    Dim FileName As String = file_comment("id").ToString & FileExt
                                    Dim DocFileID As String = EnCripText(FileName)

                                    Dim cfLnq As New TbUserCourseDocumentFileLinqDB
                                    cfLnq.DOCUMENT_FILE_ID = file_comment("id").ToString
                                    cfLnq.TB_USER_COURSE_DOCUMENT_ID = cdLnq.ID
                                    cfLnq.USER_ID = UserID
                                    cfLnq.FILE_TITLE = file_comment("title").ToString
                                    cfLnq.FILE_URL = StoryFile
                                    cfLnq.ORDER_BY = file_comment("order").ToString
                                    If FileExt.ToLower = ".pdf" Then
                                        cfLnq.IS_CONVERT = "N"
                                    Else
                                        cfLnq.IS_CONVERT = "Z"
                                    End If

                                    ret = cfLnq.InsertData(Username, trans.Trans)
                                    If ret.IsSuccess = False Then
                                        Exit For
                                    End If
                                    cfLnq = Nothing
                                Next
                                If ret.IsSuccess = False Then
                                    Exit For
                                End If
                            Next
                            If ret.IsSuccess = False Then
                                Exit For
                            End If
                        Else
                            Exit For
                        End If
                        cdLnq = Nothing
                    Next
                    'If ret.IsSuccess = False Then
                    '    Exit For
                    'End If
                Next
            End If
        Catch ex As Exception
            ret.IsSuccess = False
            ret.ErrorMessage = ex.Message

        End Try
        Return ret
    End Function


    Private Shared Function GetURLFileExtension(URLFile As String) As String
        Dim ret As String = ""
        Dim Tmp() As String = Split(URLFile, ".")
        If Tmp.Length > 0 Then
            ret = "." & Tmp(Tmp.Length - 1)
        End If

        Return ret
    End Function

#Region " Encrypt/Decrypt "

    Private Shared Function EnCripText(ByVal passString As String) As String
        Dim encrypted As String = ""
        Try
            Dim ByteData() As Byte = System.Text.Encoding.UTF8.GetBytes(passString)
            encrypted = Convert.ToBase64String(ByteData)
        Catch ex As Exception
        End Try

        Return encrypted
    End Function

    Private Shared Function DeCripText(ByVal passString As String) As String
        Dim decrypted As String = ""
        Try
            Dim ByteData() As Byte = Convert.FromBase64String(passString)
            decrypted = System.Text.Encoding.UTF8.GetString(ByteData)
        Catch ex As Exception

        End Try

        Return decrypted
    End Function
#End Region
End Class
