Imports System.Net
Imports System.IO
Imports System.Drawing
Module TescoModule

    Public UserData As New UserProfileData

    Public Function GetWebServiceURL() As String
        Return "http://tescolotuslc.com/learningcenterstaging/"
        'Return "https://tescolotuslc.com/learningcenterpreproduction/"
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
End Module
