Public Class Form1

    Private board(200) As PictureBox
    'Private bColor(200) As Color
    Private piece(16) As Boolean
    'Private pColor As Color
    Private bdImage As Image
    Private nextPiece(16) As Boolean
    'Private pNColor As Color
    Private dispPiece(16) As PictureBox 'System.Windows.Forms.Panel
    Private dispNext(16) As System.Windows.Forms.PictureBox
    Private loc As Point
    Private imgSize As Integer = 32
    Private paused As Boolean = False

    Private Sub keyboardControls(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown, TextBox1.KeyDown
        Select Case e.KeyCode
            Case Keys.Left
                'If (e.KeyData = Keys.Left) Then
                If (canLeft()) Then
                    Me.loc.X -= 1
                    Me.drawPiece()
                End If
                'End If
            Case Keys.Right
                'If (e.KeyData = Keys.Right) Then
                If (canRight()) Then
                    Me.loc.X += 1
                    Me.drawPiece()
                End If
                'End If
            Case Keys.Down
                'If (e.KeyData = Keys.Down) Then
                Me.Timer1.Interval = 1
                'End If
            Case Keys.Up
                rotPiece()
                Me.drawPiece()
            Case Keys.End
                If paused Then
                    paused = False
                    Timer1.Enabled = True
                Else
                    paused = True
                    Timer1.Enabled = False
                End If
            Case Keys.Space
                Me.fullDrop()
                Me.Timer1.Interval = 1
        End Select
    End Sub

    Private Sub fullDrop()
        While (Me.canDrop())
            Me.loc.Y += 1
        End While
    End Sub

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        imgSize = Smallest(Int((Me.Width / 2) / 10), Int((Me.Height * 0.85) / 20))

        Dim control As PictureBox 'System.Windows.Forms.Panel
        Dim yOff As Integer
        Dim size = New Size(imgSize, imgSize)
        '        Me.pNColor = New Color
        '       Me.pColor = New Color
        loc = New Point

        With Panel1
            .Height = 20 * imgSize + 2
            .Width = 10 * imgSize + 2
        End With

        With Panel2
            .Width = 4 * imgSize
            .Height = 4 * imgSize
            .Left = Me.Width - .Width - imgSize
        End With

        For y As Integer = 0 To 19
            yOff = y * 10
            For x As Integer = 0 To 9
                board(yOff + x) = New PictureBox
                '                bColor(yOff + x) = New Color
                control = board(yOff + x)
                control.Size = size
                control.Location = New Point(x * imgSize, y * imgSize)
                control.BorderStyle = BorderStyle.Fixed3D
                control.BackColor = Color.Gray 'Color.Transparent 'Color.Plum
                control.Visible = False
                Me.Panel1.Controls.Add(control)
            Next
        Next
        'Dim p1, p2 As Panel
        Dim p1 As PictureBox
        Dim p2 As PictureBox
        For y As Integer = 0 To 3
            For x As Integer = 0 To 3
                Me.dispPiece(y * 4 + x) = New PictureBox 'Panel
                Me.dispNext(y * 4 + x) = New System.Windows.Forms.PictureBox
                p1 = Me.dispPiece(y * 4 + x)
                p2 = Me.dispNext(y * 4 + x)
                p1.Visible = False
                p2.Visible = False
                p1.Location = New Point(x * imgSize, y * imgSize)
                p2.Location = New Point(x * imgSize, y * imgSize)
                p1.Size = size
                p2.Size = size
                '                p1.BackColor = Color.Red
                '               p2.BackColor = Color.Red
                p1.BorderStyle = BorderStyle.Fixed3D
                p2.BorderStyle = BorderStyle.Fixed3D
                Me.Panel2.Controls.Add(p2)
                Me.Panel1.Controls.Add(p1)
            Next
        Next
    End Sub

    Private Function getPosition(ByVal x As Integer, ByVal y As Integer) As PictureBox 'System.Windows.Forms.Panel
        Return board(y * 10 + x)
    End Function

    Private Sub clearBoard()
        For x As Integer = 0 To 199
            board(x).Visible = False
        Next
    End Sub

    Private Sub selectPiece(ByVal x As Integer)
        Dim z As Integer
        For z = 0 To 15
            nextPiece(z) = False
        Next
        Select Case (x)
            Case 0 ' straight line
                '                Me.pNColor = Color.Aqua
                For z = 0 To 3
                    nextPiece(z * 4 + 1) = True
                Next
            Case 1 ' backwards 7
                '               Me.pNColor = Color.Red
                For z = 0 To 2
                    nextPiece(z * 4 + 1) = True
                Next
                nextPiece(2) = True
            Case 2 ' 7
                '              Me.pNColor = Color.Green
                nextPiece(1) = True
                For z = 0 To 2
                    nextPiece(z * 4 + 2) = True
                Next
            Case 3 ' square
                '             Me.pNColor = Color.Yellow
                For z = 0 To 1
                    nextPiece(z * 4 + 1) = True
                    nextPiece(z * 4 + 2) = True
                Next
            Case 4 ' T
                '            Me.pNColor = Color.White
                For z = 0 To 2
                    nextPiece(z) = True
                Next
                nextPiece(5) = True
            Case 5 ' offset square 1
                '           Me.pNColor = Color.Black
                For z = 0 To 1
                    nextPiece(z * 4 + 1) = True
                    nextPiece((z + 1) * 4 + 2) = True
                Next
            Case 6 ' offset square 2
                '          Me.pNColor = Color.Brown
                For z = 0 To 1
                    nextPiece((z + 1) * 4 + 1) = True
                    nextPiece(z * 4 + 2) = True
                Next
        End Select
        For z = 0 To 15
            Me.dispNext(z).Image = Me.bdImage
        Next
    End Sub

    Private Sub drawNext()
        Dim x, y As Integer
        For y = 0 To 3
            For x = 0 To 3
                Me.dispNext(y * 4 + x).Visible = Me.nextPiece(y * 4 + x)
                '                Me.dispNext(y * 4 + x).BackColor = Me.pNColor
                Me.dispNext(y * 4 + x).Image = Me.bdImage
            Next
        Next
    End Sub

    Private Sub getPiece()
        Dim x As Integer
        For x = 0 To 15
            Me.piece(x) = Me.nextPiece(x)
            Me.dispPiece(x).Image = Me.dispNext(x).Image
        Next
        Me.shakedown()
        'Me.pColor = Me.pNColor
        'Me.pColor = Color.Transparent
        'Me.bdImage = getBackImage()
        Me.loc.Y = 0
        Me.loc.X = 3
    End Sub

    Private Function getBackImage() As Image

        Dim rand As New System.Random
        Dim pic As Integer
        pic = Int(rand.Next Mod 17)

        Select Case pic
            Case 0
                Return My.Resources.Star_Trek___Jem_Hadar
            Case 1
                Return My.Resources.Star_Trek___Kazon
            Case 2
                Return My.Resources.Star_Trek___Klaestron
            Case 3
                Return My.Resources.Star_Trek___Klingon
            Case 4
                Return My.Resources.Star_Trek___Maquis
            Case 5
                Return My.Resources.Star_Trek___Miradorn
            Case 6
                Return My.Resources.Star_Trek___Reliant
            Case 7
                Return My.Resources.Star_Trek___Romulan
            Case 8
                Return My.Resources.Star_Trek___Romulan_Scout
            Case 9
                Return My.Resources.Star_Trek___Runabout
            Case 10
                Return My.Resources.Star_Trek___Sailship
            Case 11
                Return My.Resources.Star_Trek___Saratoga
            Case 12
                Return My.Resources.Star_Trek___Shuttle
            Case 13
                Return My.Resources.Star_Trek___Stargazer
            Case 14
                Return My.Resources.Star_Trek___Tarellian
            Case 15
                Return My.Resources.Star_Trek___Tholian
            Case 16
                Return My.Resources.Star_Trek___Type_7_Shuttle
            Case Else
                Return My.Resources.Star_Trek___Warbird
        End Select

    End Function

    Private Sub drawPiece()
        Dim x, y, z, yOff As Integer
        For z = 0 To 15
            With Me.dispPiece(z)
                If paused Then
                    .Visible = True
                    If Me.piece(z) Then .BorderStyle = BorderStyle.Fixed3D Else .BorderStyle = BorderStyle.None
                Else
                    .Visible = Me.piece(z)
                    .BorderStyle = BorderStyle.Fixed3D
                End If
                '.BackColor = Me.pColor
                .BackColor = Color.White 'Color.Transparent
                '                .Image = bdImage 'getBackImage()
                .SizeMode = PictureBoxSizeMode.StretchImage
            End With
        Next
        For y = 0 To 3
            yOff = y * 4
            For x = 0 To 3
                Me.dispPiece(yOff + x).Location = New Point((Me.loc.X + x) * imgSize, (Me.loc.Y + y) * imgSize)
            Next
        Next
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim rnd As System.Random = New System.Random
        Me.bdImage = Me.getBackImage
        Me.clearBoard()
        Me.selectPiece(rnd.Next Mod 7)
        Me.getPiece()
        Me.bdImage = Me.getBackImage
        Me.selectPiece(rnd.Next Mod 7)
        Me.drawNext()
        Me.drawPiece()
        Me.Button1.Enabled = False
        Me.Button1.Visible = False
        Me.Timer1.Interval = 200
        Me.Timer1.Enabled = True
    End Sub

    Private Function canDrop() As Boolean
        Dim x, y, yOff As Integer
        For y = 0 To 3
            yOff = y * 4
            For x = 0 To 3
                If (piece(yOff + x) = True) Then
                    If (Me.loc.Y + y >= 19) Then
                        Return False
                    End If
                    If (board((Me.loc.Y + y + 1) * 10 + Me.loc.X + x).Visible = True) Then
                        Return False
                    End If
                End If
            Next
        Next
        Return True
    End Function

    Private Function canLeft()
        Dim x, y, yOff, leftMost As Integer
        leftMost = 10
        For x = 0 To 3
            For y = 0 To 3
                yOff = y * 4
                If (piece(yOff + x) = True) Then
                    If (x < leftMost) Then
                        leftMost = x
                    End If
                    If (Me.board((Me.loc.Y + y) * 10 + Me.loc.X + x - 1).Visible = True) Then
                        Return False
                    End If
                End If
            Next
        Next
        If (leftMost + Me.loc.X <= 0) Then
            Return False
        End If
        Return True
    End Function

    Private Function canRight()
        Dim x, y, yOff, rightMost As Integer
        For x = 0 To 3
            For y = 0 To 3
                yOff = y * 4
                If (piece(yOff + x) = True) Then
                    If (x > rightMost) Then
                        rightMost = x
                    End If
                    If (Me.board((Me.loc.Y + y) * 10 + Me.loc.X + x + 1).Visible = True) Then
                        Return False
                    End If
                End If
            Next
        Next
        If (rightMost + Me.loc.X >= 9) Then
            Return False
        End If
        Return True
    End Function

    Private Function lowerPiece() As Boolean
        If (canDrop()) Then
            Me.loc.Y += 1
            Return True
        Else
            Return False
        End If
    End Function

    Private Function settlePiece() As Boolean
        Dim x, y As Integer
        For y = 0 To 3
            For x = 0 To 3
                If (piece(y * 4 + x)) Then
                    With Me.board((Me.loc.Y + y) * 10 + Me.loc.X + x)
                        .Visible = True
                        .BackColor = Color.White 'Color.Transparent 'Me.pColor
                        .Image = Me.dispPiece(y * 4 + x).Image
                        .SizeMode = PictureBoxSizeMode.StretchImage
                    End With
                End If
            Next
        Next
        If (Me.detectLines()) Then
            Me.destroyLines()
            Return False
        End If
        If (Me.loc.Y = 0) Then
            Me.Button1.Visible = True
            Me.Button1.Enabled = True
            Return True
        End If
        Return False
    End Function

    Private Function destroyLines() As Integer
        Dim x, y, yOff, numDel As Integer
        Dim line As Boolean
        For y = 0 To 19
            yOff = y * 10
            line = True
            For x = 0 To 9
                If (Not (board(yOff + x).Visible)) Then
                    line = False
                End If
            Next
            If (line) Then
                deleteLine(y)
                numDel += 1
            End If
        Next
        Return numDel
    End Function

    Private Sub deleteLine(ByVal pos As Integer)
        Dim x, y, yOff As Integer
        For y = pos To 1 Step -1
            yOff = y * 10
            For x = 0 To 9
                If (board(yOff + x - 10).Visible) Then
                    With board(yOff + x)
                        .Visible = True
                        If .Image Is Nothing Then .Image = bdImage
                    End With
                Else
                    board(yOff + x).Visible = False
                End If
            Next
        Next
        For x = 0 To 9
            board(x).Visible = False
        Next
    End Sub

    Private Function detectLines() As Boolean
        Dim x, y, yOff As Integer
        Dim line As Boolean
        For y = 0 To 19
            yOff = y * 10
            line = True
            For x = 0 To 9
                If (Not (board(yOff + x).Visible)) Then
                    line = False
                End If
            Next
            If (line) Then
                Return True
            End If
        Next
        Return False
    End Function

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Dim rnd As System.Random = New System.Random
        Dim game As Boolean = True
        If (Not (Me.lowerPiece())) Then
            If (Me.settlePiece()) Then
                game = False
                Me.Timer1.Enabled = False
            End If
            Me.bdImage = getBackImage()
            Me.getPiece()
            Me.selectPiece(rnd.Next Mod 7)
            Me.drawNext()
        End If
        Me.drawPiece()
        If (game = True) Then
            Me.Timer1.Interval = 200
            Me.Timer1.Enabled = True
        End If
    End Sub

    Private Function Smallest(ByVal a As Long, ByVal b As Long) As Long
        If a < b Then
            Return a
        Else
            Return b
        End If
    End Function

    Private Function rotPiece()
        Dim rotPieces(15) As Boolean
        Dim rotPartX As Integer = 0
        'Dim rotPartY As Integer = 0
        'Dim curloc As Point

        For rotPartX = 0 To 3
            'rotPartY = rotPartX * 4
            rotPieces(0 + rotPartX * 4) = piece(12 + rotPartX)
            rotPieces(1 + rotPartX * 4) = piece(8 + rotPartX)
            rotPieces(2 + rotPartX * 4) = piece(4 + rotPartX)
            rotPieces(3 + rotPartX * 4) = piece(rotPartX)
        Next

        For rotPartX = 0 To 15
            piece(rotPartX) = rotPieces(rotPartX)
        Next

        Me.shakedown()

        Return True

    End Function

    Private Function shakedown()
        Dim shakePieces(15) As Boolean

        Dim row As Integer = 0
        Dim topRow As Integer = 0
        'shake down rows
        Do Until row >= 4
            If (piece(row * 4) Or piece(row * 4 + 1) Or piece(row * 4 + 2) Or piece(row * 4 + 3)) Then
                topRow = row
                row = 4
            End If
            row += 1
        Loop
        For row = 0 To 3
            If (row + topRow) * 4 <= 15 Then
                shakePieces(row * 4) = piece((row + topRow) * 4)
                shakePieces((row * 4) + 1) = piece(((row + topRow) * 4) + 1)
                shakePieces((row * 4) + 2) = piece(((row + topRow) * 4) + 2)
                shakePieces((row * 4) + 3) = piece(((row + topRow) * 4) + 3)
            Else
                shakePieces(row * 4) = False
                shakePieces((row * 4) + 1) = False
                shakePieces((row * 4) + 2) = False
                shakePieces((row * 4) + 3) = False
            End If
        Next

        'share down cols
        row = 0
        topRow = 0
        Do Until row >= 4
            If (piece(row) Or piece(row + 4) Or piece(row + 8) Or piece(row + 12)) Then
                topRow = row
                row = 4
            End If
            row += 1
        Loop
        For row = 0 To 3
            If (row + topRow) * 4 <= 15 Then
                shakePieces(row) = piece(row + topRow)
                shakePieces(row + 4) = piece(row + topRow + 4)
                shakePieces(row + 8) = piece(row + topRow + 8)
                shakePieces(row + 12) = piece(row + topRow + 12)
            Else
                shakePieces(row) = False
                shakePieces(row + 4) = False
                shakePieces(row + 8) = False
                shakePieces(row + 12) = False
            End If
        Next
        For row = 0 To 15
            piece(row) = shakePieces(row)
        Next
        shakePieces = Nothing

        Return True
    End Function

    Private Sub TextBox1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox1.KeyPress
        e.Handled = True
    End Sub

End Class
