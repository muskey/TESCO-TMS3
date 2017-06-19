Imports System.Reflection
Imports System.IO
Imports LinqDB.ConnectDB
Imports LinqDB.TABLE


Public Class LogFileBL

    Const AppLogFolder As String = "C:\Application\TESCO-TMS3\log\ErrorLog\"

#Region "User Activity Log"
    'Public Shared Sub CreateHartbeat(TimerName As String)
    '    Dim frame As StackFrame = New StackFrame(1, True)
    '    Dim ClassName As String = frame.GetMethod.ReflectedType.Name
    '    Dim FunctionName As String = frame.GetMethod.Name
    '    Dim LineNo As Integer = frame.GetFileLineNumber

    '    Try
    '        Dim hbPath As String = Application.StartupPath & "\HeartBeat\" & ClassName & "\"
    '        If Directory.Exists(hbPath) = False Then
    '            Directory.CreateDirectory(hbPath)
    '        End If

    '        Dim FileName As String = hbPath & FunctionName & "_Timer_" & TimerName & ".txt"
    '        Dim obj As New StreamWriter(FileName, False)
    '        obj.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"))
    '        obj.Flush()
    '        obj.Close()
    '    Catch ex As Exception
    '        ''### Current Class and Function name
    '        'Dim m As MethodBase = MethodBase.GetCurrentMethod()
    '        'Dim ThisClassName As String = m.ReflectedType.Name
    '        'Dim ThisFunctionName As String = m.Name

    '        CreateTextErrorLog("Exception : " & ex.Message & " " & ex.StackTrace & "&ClassName=" & ClassName & "&FunctionName=" & FunctionName & "&LineNo=" & LineNo & "&TimerName=" & TimerName)
    '    End Try
    'End Sub

    Public Shared Sub LogTrans(UserSession As UserProfileData, LogMsg As String)

        Dim frame As StackFrame = New StackFrame(1, True)
        Dim ClassName As String = frame.GetMethod.ReflectedType.Name
        Dim FunctionName As String = frame.GetMethod.Name
        Dim LineNo As Integer = frame.GetFileLineNumber
        CreateUserActivityLog(UserSession.LoginHistoryID, UserSession.UserName, ClassName, FunctionName, LineNo, LogMsg, AgentLogType.TransLog)
    End Sub

    Public Shared Sub LogTrans(LoginHisID As Long, LogMsg As String)

        Dim frame As StackFrame = New StackFrame(1, True)
        Dim ClassName As String = frame.GetMethod.ReflectedType.Name
        Dim FunctionName As String = frame.GetMethod.Name
        Dim LineNo As Integer = frame.GetFileLineNumber

        Dim lnq As New LinqDB.TABLE.TbLoginHistoryLinqDB
        lnq.GetDataByPK(LoginHisID, Nothing)
        If lnq.ID > 0 Then
            CreateUserActivityLog(LoginHisID, lnq.USERNAME, ClassName, FunctionName, LineNo, LogMsg, AgentLogType.TransLog)
        End If
        lnq = Nothing
    End Sub

    Public Shared Sub LogError(UserSession As UserProfileData, LogMsg As String)
        Dim frame As StackFrame = New StackFrame(1, True)
        Dim ClassName As String = frame.GetMethod.ReflectedType.Name
        Dim FunctionName As String = frame.GetMethod.Name
        Dim LineNo As Integer = frame.GetFileLineNumber

        CreateUserActivityLog(UserSession.LoginHistoryID, UserSession.UserName, ClassName, FunctionName, LineNo, LogMsg, AgentLogType.ErrorLog)
    End Sub

    Public Shared Sub LogException(UserSession As UserProfileData, ExMessage As String, ExStackTrace As String)
        Dim frame As StackFrame = New StackFrame(1, True)
        Dim ClassName As String = frame.GetMethod.ReflectedType.Name
        Dim FunctionName As String = frame.GetMethod.Name
        Dim LineNo As Integer = frame.GetFileLineNumber

        CreateUserActivityLog(UserSession.LoginHistoryID, UserSession.UserName, ClassName, FunctionName, LineNo, "Exception : " & ExMessage & vbNewLine & ExStackTrace, AgentLogType.ExceptionLog)
    End Sub

    Private Enum AgentLogType
        TransLog = 1
        ErrorLog = 2
        ExceptionLog = 3
    End Enum


    Private Shared Sub CreateUserActivityLog(LoginHisID As Long, UserName As String, ClassName As String, FunctionName As String, LineNo As Int16, LogMsg As String, LogType As Int16)
        ''### Current Class and Function name
        'Dim m As MethodBase = MethodBase.GetCurrentMethod()
        'Dim ThisClassName As String = m.ReflectedType.Name
        'Dim ThisFunctionName As String = m.Name

        Try
            Dim lnq As New TbLogUserActivityLinqDB
            lnq.LOGIN_HISOTRY_ID = LoginHisID
            lnq.LOG_TYPE = LogType.ToString
            lnq.LOG_MESSAGE = LogMsg
            lnq.CLASS_NAME = ClassName
            lnq.FUNCTION_NAME = FunctionName
            lnq.LINE_NO = LineNo

            Dim trans As New TransactionDB
            Dim ret As ExecuteDataInfo = lnq.InsertData(UserName, trans.Trans)
            If ret.IsSuccess = True Then
                trans.CommitTransaction()
            Else
                trans.RollbackTransaction()
                CreateTextErrorLog(ret.ErrorMessage & Environment.NewLine & "LoginHisID=" & LoginHisID & "&Username=" & UserName & "&ClassName=" & ClassName & "&FunctionName=" & FunctionName & "&LogType=" & LogType & Environment.NewLine & LogMsg)
            End If
        Catch ex As Exception
            CreateTextErrorLog("Exception : " & ex.Message & " " & ex.StackTrace & Environment.NewLine & "LoginHisID=" & LoginHisID & "&Username=" & UserName & "&ClassName=" & ClassName & "&FunctionName=" & FunctionName & "&LogType=" & LogType & Environment.NewLine & LogMsg)
        End Try
    End Sub


    Private Shared Sub CreateTextErrorLog(LogMsg As String)
        Try
            Dim frame As StackFrame = New StackFrame(1, True)
            Dim ClassName As String = frame.GetMethod.ReflectedType.Name
            Dim FunctionName As String = frame.GetMethod.Name
            Dim LineNo As Integer = frame.GetFileLineNumber

            Dim MY As String = DateTime.Now.ToString("yyyyMM")
            Dim DD As String = DateTime.Now.ToString("dd")
            Dim LogFolder As String = AppLogFolder & MY & "\" & DD & "\"
            If Directory.Exists(LogFolder) = False Then
                Directory.CreateDirectory(LogFolder)
            End If

            Dim FileName As String = LogFolder & ClassName & "_" & DateTime.Now.ToString("yyyyMMddHH") & ".txt"
            Dim obj As New StreamWriter(FileName, True)
            obj.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") & " " & FunctionName & " Line No :" & LineNo & Environment.NewLine & LogMsg & Environment.NewLine & Environment.NewLine)
            obj.Flush()
            obj.Close()
        Catch ex As Exception

        End Try
    End Sub
#End Region




    Public Shared Sub TestTraceFrame()
        '### Current Class and Function name
        Dim m As MethodBase = MethodBase.GetCurrentMethod()
        Dim ThisClassName As String = m.ReflectedType.Name
        Dim ThisFunctionName As String = m.Name



        Dim frame As StackFrame = New StackFrame(1, True)
        Dim CallFromAppName As String = frame.GetMethod.Module.FullyQualifiedName
        Dim CallFromClassName As String = frame.GetMethod.ReflectedType.Name
        Dim CallFromMethod As String = frame.GetMethod.Name
        Dim CallFromFile As String = frame.GetFileName
        Dim CallFromLineNo As String = frame.GetFileLineNumber


        Dim aaa As String = ""
    End Sub
End Class
