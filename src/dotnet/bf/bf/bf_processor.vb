Public Class bf_processor

#Region "Details"

    '
    'http://www.muppetlabs.com/~breadbox/bf/
    'The Language
    'A Brainfuck program has an implicit byte pointer, called "the pointer", which is free to move around within an array of 30000 bytes, initially all set to zero. The pointer itself is initialized to point to the beginning of this array. 
    '
    'The Brainfuck programming language consists of eight commands, each of which is represented as a single character. 
    '
    '   >  Increment the pointer. 
    '   <  Decrement the pointer. 
    '   +  Increment the byte at the pointer. 
    '   -  Decrement the byte at the pointer. 
    '   .  Output the byte at the pointer. 
    '   ,  Input a byte and store it in the byte at the pointer. 
    '   [  Jump forward past the matching ] if the byte at the pointer is zero. 
    '   ]  Jump backward to the matching [ unless the byte at the pointer is zero. 
    '
    'http://oopsilon.com/Trainfuck
    'Extention to brainfuck
    '
    'File I/O 
    '   # - Open or close a file. Takes a null-terminated filename string starting at P. If called again, closes the currently open file; no parameter required for close. 
    '   ; - Read a byte from file. Saves the byte to P. 
    '   : - Write a byte to file. Fetches the byte from P, and writes. 
    '   ( - Rewind one byte. Modifies P: 0 if start of file was reached, 1 otherwise. 
    '   ) - Move forward a byte. Modifies P: 0 if end of file was reached, 1 otherwise. 
    'Networking 
    '   % - Connect to an address/port. Takes two parameters: big-endian IPv4 address starting at P, and big-endian TCP port number starting at P+4. If called again, closes currently open socket; no parameters required. 
    '   $ - Listen on an address/port. Takes address and port as detailed for %. If called again, closes currently open socket; no parameters required. 
    '   @ - Accepts an incoming connection. If called again, closes the currently accepted connection. 
    '   ` - Receive a byte from the network stream. Saves the byte to P, or zero if connection was closed. 
    '   ' - Send a byte. Fetches the byte from P, and sends. 
    '
    '   & - Connect to Address:Port of null terminated string starting at pointer
    '

#End Region

#Region "Attributes"

    Private _program As String
    Private _programOffset As Integer = 0
    Private _programLength As Integer = 0

    Private _pointer(29999) As Byte
    Private _index As Integer = 0

    '===============================================

    Private _output(29999) As Byte
    Private _outputOffset As Integer = 0

    Private _input(29999) As Byte
    Private _inputOffset As Integer = 0

    '===============================================

    Private _fileOpen As Boolean = False
    Private _filePointer As Int64 = 0

    Private _fileName As String = ""
    Private _fileStream As IO.FileStream

    '===============================================

    Private _networkConnectSocket As System.Net.Sockets.TcpClient
    Private _networkConnectStream As System.Net.Sockets.NetworkStream
    Private _networkConnectListener As System.Net.Sockets.TcpListener
    Private _networkConnectAddr As String = ""
    Private _networkConnectAddrByte As Byte() = {0, 0, 0, 0}
    Private _networkConnectPort As Integer = 0
    Private _networkConnectOpen As Boolean = False
    Private _networkConnectAccept As Boolean = False
    Private _networkConnectStatus As Byte = _networkStatus.disabled

    Public Enum _networkStatus
        disabled = 0
        client = 1
        server = 2
    End Enum

#End Region

#Region "BrainF*ck Functions"

    Private Sub incrementPointer()
        Dim wrk As Integer = _pointer(_index)
        wrk += 1
        If wrk >= 256 Then wrk = 0
        _pointer(_index) = wrk '+= 1
    End Sub

    Private Sub decrementPointer()
        Dim wrk As Integer = _pointer(_index)
        wrk -= 1
        If wrk < 0 Then wrk = 255
        _pointer(_index) = wrk '-= 1
    End Sub

    Private Sub incrementOffset()
        _index += 1
        If _index >= 30000 Then _index = 0
    End Sub

    Private Sub decrementOffset()
        _index -= 1
        If _index < 0 Then _index = 29999
    End Sub

    Private Sub outputPointer()
        _output(_outputOffset) = _pointer(_index)
        _outputOffset += 1
        If _outputOffset >= 30000 Then _outputOffset = 0
    End Sub

    Private Sub inputPointer()
        _pointer(_index) = _input(_inputOffset)
        _inputOffset += 1
        If _inputOffset >= 30000 Then _inputOffset = 0
    End Sub

    Private Sub loopForward()

        If _pointer(_index) = 0 Then

            Dim loopOffset As Integer = _programOffset
            Dim loopPosition As Integer = 1
            Dim loopCounter As Integer = 1

            Dim doneBool As Boolean = False
            Do Until doneBool

                Dim stepFunction As Char
                stepFunction = Mid(_program, loopOffset + loopPosition, 1)

                Select Case stepFunction

                    Case "["
                        loopCounter += 1

                    Case "]"
                        loopCounter -= 1

                End Select

                loopPosition += 1

                If loopCounter = 0 Then doneBool = True
            Loop

            _programOffset = loopOffset + loopPosition - 1

        Else

            '_programOffset += 1

        End If

    End Sub

    Private Sub loopBackward()

        If _pointer(_index) <> 0 Then

            Dim loopOffset As Integer = _programOffset
            Dim loopPosition As Integer = 1
            Dim loopCounter As Integer = 1

            Dim doneBool As Boolean = False
            Do Until doneBool

                Dim stepFunction As Char
                stepFunction = Mid(_program, loopOffset - loopPosition, 1)

                Select Case stepFunction

                    Case "["
                        loopCounter -= 1

                    Case "]"
                        loopCounter += 1

                End Select


                loopPosition += 1

                If loopCounter = 0 Then doneBool = True

            Loop

            _programOffset = loopOffset - loopPosition + 1

        Else

            '_programOffset += 1

        End If

    End Sub

    Private Sub ResetOutput()

        For x As Integer = 0 To 29999 '_output.Length - 1
            _output(x) = 0
            _pointer(x) = 0
            '_input(x) = 0
        Next

        _programOffset = 0
        _programLength = Len(_program)

        _index = 0

        _outputOffset = 0
        _inputOffset = 0


    End Sub

    Public Sub setProgram(ByVal inProgram As String)
        _program = inProgram
        _programLength = Len(inProgram)
    End Sub

    Public Sub setInput(ByVal inInput As String)

        For x As Integer = 0 To 29999
            _input(x) = 0
        Next

        For x As Integer = 0 To inInput.Length - 1

            _input(x) = Asc(Mid(inInput, x + 1, 1))

        Next

    End Sub

    Public Function getOutput() As String
        Dim outVar As String = ""
        For xPnt As Integer = 0 To _outputOffset - 1
            outVar += Chr(_output(xPnt))
        Next
        Return outVar
    End Function

#End Region

#Region "TrainF*ck Functions File I/O"

    Private Function loadString()

        Dim outVar As String = ""

        Do Until _pointer(_index) = 0
            outVar += Chr(_pointer(_index))
            _index += 1
        Loop

        Return outVar

    End Function

    Private Sub openCloseFile() '#

        If _fileOpen Then
            'Close File
            _fileStream.Close()
            '_fileStream.Dispose()
            _fileOpen = False

        Else
            'Open File

            _fileName = loadString()
            _filePointer = 0

            Try
                If New IO.FileInfo(_fileName).FullName.Length > 0 Then
                    _fileStream = New IO.FileStream(_fileName, IO.FileMode.OpenOrCreate)
                End If
                _fileOpen = True
            Catch ex As Exception
                MsgBox("File open failed" + vbCrLf + ex.Message)
            End Try

        End If
        '_fileOpen = Not _fileOpen

    End Sub

    Private Sub readFile() ';

        If _fileOpen Then
            Dim inByte() As Byte = {0}
            _fileStream.Position = _filePointer
            _fileStream.Read(inByte, 0, 1)
            _pointer(_index) = inByte(0)
        Else
            _filePointer = 0
            _pointer(_index) = 0
        End If

    End Sub

    Private Sub writeFile() ':

        If _fileOpen Then
            Dim outByte() As Byte = {_pointer(_index)}
            _fileStream.Position = _filePointer
            _fileStream.Write(outByte, 0, 1)
        Else
            _filePointer = 0
            _pointer(_index) = 0
        End If

    End Sub

    Private Sub backFile() '(

        If _fileOpen Then
            If _filePointer <= 0 Then
                _filePointer = 0
                _pointer(_index) = 0
            Else
                _filePointer -= 1
                _pointer(_index) = 1
            End If
        Else
            _pointer(_index) = 0
            _filePointer = 0
        End If

    End Sub

    Private Sub forwardFile() ')

        If _fileOpen Then
            If _filePointer >= _fileStream.Length Then
                _filePointer = _fileStream.Length
                _pointer(_index) = 0
            Else
                _filePointer += 1
                _pointer(_index) = 1
            End If
        Else
            _pointer(_index) = 0
            _filePointer = 0
        End If

    End Sub

#End Region

#Region "TrainF*ck Functions Network I/O"

    Private Function loadAddress() As String

        Dim outVar As String = ""
        For wrk As Byte = 0 To 3

            outVar += _pointer(_index).ToString + "."
            _index += 1

        Next
        outVar = outVar.Trim(".")

        Return outVar

    End Function

    Private Function loadAddressByte() As Byte()

        Dim outVar() As Byte = {0, 0, 0, 0}
        For wrk As Byte = 0 To 3

            outVar(wrk) = _pointer(_index)
            _index += 1

        Next

        Return outVar

    End Function

    Private Function loadPort() As Integer

        Dim outVar As Integer = 0
        outVar += _pointer(_index) * &H100
        _index += 1
        outVar += _pointer(_index)
        outVar += 1

        Return outVar

    End Function

    Private Sub networkConnect()

        If _networkConnectOpen Then
            'Close Connection
            _networkConnectSocket.Close()
            _networkConnectOpen = False
            _networkConnectStatus = _networkStatus.disabled

        Else
            'Open Connection
            _networkConnectAddr = loadAddress()
            _networkConnectPort = loadPort()

            Try
                _networkConnectSocket = New System.Net.Sockets.TcpClient
                _networkConnectSocket.Connect(_networkConnectAddr, _networkConnectPort)
                _networkConnectStream = _networkConnectSocket.GetStream
                _networkConnectStatus = _networkStatus.client
                _networkConnectOpen = True

            Catch ex As Exception
                MsgBox("Network Connect Open Failed:" + vbCrLf + ex.Message)
            End Try

        End If

    End Sub

    Private Sub networkConnectHost()

        If _networkConnectOpen Then
            'Close Connection
            _networkConnectSocket.Close()
            _networkConnectOpen = False
            _networkConnectStatus = _networkStatus.disabled

        Else
            _HoldNetworkOut = ""

            'Open Connection
            Dim addrIn As String = loadString() + ":"
            _networkConnectAddr = addrIn.Split(":")(0)
            _networkConnectPort = Val(addrIn.Split(":")(1))

            Try
                _networkConnectSocket = New System.Net.Sockets.TcpClient
                _networkConnectSocket.Connect(_networkConnectAddr, _networkConnectPort)

                _networkConnectSocket.SendBufferSize = 1
                _networkConnectSocket.ReceiveBufferSize = 1

                _networkConnectStream = _networkConnectSocket.GetStream
                _networkConnectStatus = _networkStatus.client
                _networkConnectOpen = True

            Catch ex As Exception
                MsgBox("Network Connect Host Open Failed:" + vbCrLf + ex.Message)
            End Try

        End If

    End Sub

    Private Sub networkListen()

        If _networkConnectOpen Then
            'Close Connection
            _networkConnectSocket.Close()
            _networkConnectOpen = False
            _networkConnectStatus = _networkStatus.disabled

        Else
            'Open Connection
            _networkConnectAddrByte = loadAddressByte()
            _networkConnectPort = loadPort()

            Try
                Dim ipAddress As New System.Net.IPAddress(_networkConnectAddrByte)
                _networkConnectListener = New System.Net.Sockets.TcpListener(ipAddress, _networkConnectPort)
                _networkConnectSocket = _networkConnectListener.AcceptTcpClient
                _networkConnectStream = _networkConnectSocket.GetStream
                _networkConnectStatus = _networkStatus.disabled
                _networkConnectOpen = True

            Catch ex As Exception
                MsgBox("Network Listen Open Failed:" + vbCrLf + ex.Message)
            End Try

        End If

    End Sub

    Private Sub networkAccept()

        If _networkConnectAccept Then
            _networkConnectAccept = False
            _networkConnectListener.Stop()
        Else
            If _networkConnectStatus = _networkStatus.server Then
                _networkConnectAccept = True
                _networkConnectListener.Start()
            End If
        End If

    End Sub

    Private Sub networkRead()

        If _networkConnectOpen And _networkConnectStatus <> _networkStatus.disabled Then

            Dim inByte() As Byte = {0}
            If _networkConnectSocket.Connected And _networkConnectSocket.Available > 0 Then
                If _networkConnectStream.CanRead Then
                    Try
                        _networkConnectStream.Read(inByte, 0, 1)
                    Catch ex As Exception
                    End Try
                End If
            End If
            _pointer(_index) = inByte(0)

        Else
            _pointer(_index) = 0
        End If

    End Sub

    Private _HoldNetworkOut As String = ""
    Private Sub networkWrite()

        If _networkConnectOpen And _networkConnectStatus <> _networkStatus.disabled Then

            Dim outByte() As Byte = {0}
            outByte(0) = _pointer(_index)
            _networkConnectStream.Write(outByte, 0, 1)
            _HoldNetworkOut += Chr(outByte(0))

        End If

    End Sub

#End Region

#Region "Handler"

    Public Sub runProgram()

        ResetOutput()

        If _program <> "" Then
            _programOffset = 0
            Do Until _programOffset > _programLength
                _programOffset += 1

                Dim stepFunction As Char
                stepFunction = Mid(_program, _programOffset, 1)

                Select Case stepFunction

                    Case ">"
                        '>  Increment the pointer.
                        incrementOffset()

                    Case "<"
                        '<  Decrement the pointer. 
                        decrementOffset()

                    Case "+"
                        '+  Increment the byte at the pointer. 
                        incrementPointer()

                    Case "-"
                        '-  Decrement the byte at the pointer. 
                        decrementPointer()

                    Case "."
                        '.  Output the byte at the pointer. 
                        outputPointer()

                    Case ","
                        ',  Input a byte and store it in the byte at the pointer. 
                        'MsgBox("input not supported at this time")
                        inputPointer()

                    Case "["
                        '[  Jump forward past the matching ] if the byte at the pointer is zero. 
                        loopForward()

                    Case "]"
                        ']  Jump backward to the matching [ unless the byte at the pointer is zero.
                        loopBackward()

                    Case "#"
                        '# Open File with name at pointer terminated by Null
                        openCloseFile()

                    Case ";"
                        '; Read Byte
                        readFile()

                    Case ":"
                        ': Write Byte
                        writeFile()

                    Case "("
                        '( Backup File
                        backFile()

                    Case ")"
                        ') Forward File
                        forwardFile()

                    Case "%"
                        '% Connect Network
                        networkConnect()

                    Case "$"
                        '$ Listen Network
                        networkListen()

                    Case "*"
                        '* accept connection
                        networkAccept()

                    Case "`"
                        '` Receive Byte
                        networkRead()

                    Case "'"
                        '' Send Byte
                        networkWrite()

                    Case "&"
                        '& Connect Network Host
                        networkConnectHost()

                End Select

            Loop
        End If

    End Sub

#End Region

End Class
