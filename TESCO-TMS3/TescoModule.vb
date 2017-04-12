Imports System.Net
Imports System.IO
Imports System.Drawing
Imports LinqDB.ConnectDB
Imports LinqDB.TABLE
Imports System.Data.OleDb
Imports System.Data.SqlClient
Module TescoModule

    Public UserData As New UserProfileData
    Public TempPath As String = "D:\" & "TMS"
    Public FolderCourseDocumentFile As String = TempPath & "\CourseDocumentFile"

    Public Function GetWebServiceURL() As String
        'Return "http://tescolotuslc.com/learningcenterstaging/"
        'Return "https://tescolotuslc.com/learningcenterpreproduction/"

        Dim cf As CfSysconfigLinqDB = GetSysconfig()
        Return cf.WEBSERVICE_URL
    End Function

    Private Function GetSysconfig() As CfSysconfigLinqDB
        Dim lnq As New CfSysconfigLinqDB
        Return lnq.GetDataByPK(1, Nothing)
    End Function

    Private Function CheckInternetConnection(URL As String) As Boolean
        Dim ret = False
        Dim req As WebRequest = WebRequest.Create(URL)
        Dim resp As WebResponse
        Try
            req.Proxy.Credentials = System.Net.CredentialCache.DefaultCredentials
            req.Timeout = 5000
            resp = req.GetResponse
            resp.Close()
            req = Nothing
            ret = True
        Catch ex As Exception
            ret = False
        End Try
        Return ret
    End Function



    Function GetStringDataFromURL(ByVal URL As String, Optional ByVal Parameter As String = "") As String
        Try
            Dim ret As String = ""
            If CheckInternetConnection(GetWebServiceURL() & "api/welcome") = True Then
                Dim request As WebRequest
                request = WebRequest.Create(URL)
                Dim response As WebResponse
                Dim data As Byte() = Encoding.UTF8.GetBytes(Parameter)

                request.Proxy.Credentials = System.Net.CredentialCache.DefaultCredentials
                request.Method = "POST"
                request.ContentType = "application/x-www-form-urlencoded"
                request.ContentLength = data.Length

                Dim stream As Stream = request.GetRequestStream()
                stream.Write(data, 0, data.Length)
                stream.Close()

                response = request.GetResponse()
                Dim sr As New StreamReader(response.GetResponseStream())

                Return sr.ReadToEnd()
            Else
                'MessageBox.Show("Unable to connect Back-End server " & vbCrLf & vbCrLf & "Please check network connection !!", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        Catch ex As Exception
            'MessageBox.Show(ex.Message & vbCrLf & ex.StackTrace, "Attention", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try
        Return ""
    End Function

    Function GetImageFromURL(ByVal URL As String) As Image
        Try
            Dim Client As WebClient = New WebClient
            Client.Proxy.Credentials = System.Net.CredentialCache.DefaultCredentials
            Dim Image As Bitmap = Bitmap.FromStream(New MemoryStream(Client.DownloadData(URL)))
            Return Image
        Catch ex As Exception : End Try
        Dim im As Bitmap
        Return im
    End Function

#Region "File"
    Public Function GetURLFileExtension(URLFile As String) As String
        Dim ret As String = ""
        Dim Tmp() As String = Split(URLFile, ".")
        If Tmp.Length > 0 Then
            ret = "." & Tmp(Tmp.Length - 1)
        End If

        Return ret
    End Function
#End Region
#Region " Encrypt/Decrypt "

    Public Function EnCripText(ByVal passString As String) As String
        Dim encrypted As String = ""
        Try
            Dim ByteData() As Byte = System.Text.Encoding.UTF8.GetBytes(passString)
            encrypted = Convert.ToBase64String(ByteData)
        Catch ex As Exception
        End Try

        Return encrypted
    End Function

    Public Function DeCripText(ByVal passString As String) As String
        Dim decrypted As String = ""
        Try
            Dim ByteData() As Byte = Convert.FromBase64String(passString)
            decrypted = System.Text.Encoding.UTF8.GetString(ByteData)
        Catch ex As Exception

        End Try

        Return decrypted
    End Function
#End Region
End Module
