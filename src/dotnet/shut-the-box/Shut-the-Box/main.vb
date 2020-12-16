Public Class main

    Private die1 As Die = New Die()
    Private die2 As Die = New Die()
    Private blocks(0 To 9) As Boolean
    Private block As Integer
    Private blockImage(0 To 8) As Drawing.Image
    Private used_pips As Integer
    Private gameOver As Boolean = True

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Me.Text = My.Resources.Title + " - Roll Dice"

        Me.Height = My.Resources.box.Height + 32
        Me.Width = My.Resources.box.Width + 8

        For block = 1 To 9
            blocks(block) = False
        Next

        blockImage(0) = My.Resources.num_1
        blockImage(1) = My.Resources.num_2
        blockImage(2) = My.Resources.num_3
        blockImage(3) = My.Resources.num_4
        blockImage(4) = My.Resources.num_5
        blockImage(5) = My.Resources.num_6
        blockImage(6) = My.Resources.num_7
        blockImage(7) = My.Resources.num_8
        blockImage(8) = My.Resources.num_9

        addendsList.Location = New Point(0, 0)

        roll_dice()
        used_pips = 0
        used_pips = count_pips()
        Me.addendsList.Text = addends()

        With Me.TwoDice
            .Visible = False
            '.Visible = True
            .Checked = True
            .BackColor = Color.Transparent
            .Top = My.Resources.box.Height - .Height
        End With

        gameOver = False
        Me.Invalidate()

    End Sub

    Private Sub Form1_MouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseClick

        Dim score, w, x, y As Integer, pnt As Point


        If Not gameOver Then
            If (e.Y >= (16 + 8)) And (e.Y <= (16 + 8 + 64)) Then
                x = Int((e.X - (16 + 8)) / (64 + 8)) + 1
                If x > 0 And x < 10 Then
                    If Not blocks(x) Then
                        y = (x * (64 + 8)) + 16
                        If e.X < y Then
                            If InStr(addends(), x.ToString) > 0 Then
                                Dim rect As Rectangle = New Rectangle(24 + ((x - 1) * 72), 16 + 8, 64, 64)
                                blocks(x) = True
                                Me.Invalidate(rect)
                                used_pips += x
                            End If
                        End If
                    End If
                End If
            End If

            pnt.X = e.X
            pnt.Y = e.Y

            Me.TwoDice.Visible = Not ((Not blocks(7) Or Not blocks(8) Or Not blocks(9)))

            If count_pips() = 0 Then
                If die1.onClick(pnt) Or die2.onClick(pnt) Then
                    Me.Text = My.Resources.Title + " - Flip"
                    roll_dice()
                Else
                    Me.Text = My.Resources.Title + " - Next Player"
                End If
            End If
            If count_pips() <> 0 Then
                Dim checkStr As String
                checkStr = addends()
                If Trim(checkStr) = "" Then
                    For w = 1 To 9
                        If Not blocks(w) Then score += w
                    Next
                    Me.Text = My.Resources.Title + " - You Lose (" + score.ToString + ")"
                    gameOver = True
                End If
            End If
            Me.addendsList.Text = addends()
        Else
            Form1_Load(sender, e)
        End If

    End Sub

    Private Sub Form1_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint

        Dim Drawing As Drawing.Bitmap = New System.Drawing.Bitmap(My.Resources.box)
        Dim g As Graphics = Graphics.FromImage(Drawing)
        Dim i As Image
        Dim pnt As Point = New Point(0, 0)
        Dim offset As Point = New Point(0, 0)
        Dim rect As Rectangle = New Rectangle(0, 0, 64, 64)

        g.DrawImage(My.Resources.box, 0, 0, My.Resources.box.Width, My.Resources.box.Height)

        For block = 1 To 9
            pnt.Y = 24
            pnt.X = (16 + 8) + ((64 + 8) * (block - 1))

            rect.Location = pnt

            g.DrawImage(My.Resources.block, rect)

            If blocks(block) Then
                i = My.Resources.crown
            Else
                i = blockImage(block - 1)
            End If

            If Not (i Is Nothing) Then
                offset.Y = pnt.Y + (32 - Int(i.Height / 2))
                offset.X = pnt.X + (32 - Int(i.Width / 2))
                g.DrawImage(i, offset.X, offset.Y, i.Width, i.Height)
            End If
        Next

        If Not gameOver Then
            g.DrawImage(die1.getLastImage, die1.Location())
            If die2.getLastRoll <> 0 Then
                g.DrawImage(die2.getLastImage, die2.Location())
            End If
        End If

        e.Graphics.DrawImage(Drawing, 0, 0)
        g.Dispose()

    End Sub

#Region "Functions"

    Private Function count_pips() As Integer
        count_pips = die1.getLastRoll + die2.getLastRoll - used_pips
    End Function

    Private Function roll_dice()

        used_pips = 0
        Dim pnt As Point

        die1.roll()
        Me.Invalidate(die1.Region)
        Do
            pnt.X = Int(Rnd() * 576) + 24
            pnt.Y = Int(Rnd() * 112) + 120
            die1.Location(pnt)
        Loop Until Not die1.checkBounds(die2.Rectangle)
        Me.Invalidate(die1.Region)

        Dim enableDie2 As Boolean

        enableDie2 = (Not blocks(7) Or Not blocks(8) Or Not blocks(9)) Or (Me.TwoDice.Checked)

        If enableDie2 Then

            die2.roll()
            Me.Invalidate(die2.Region)
            Do
                pnt.X = Int(Rnd() * 576) + 24
                pnt.Y = Int(Rnd() * 112) + 120
                die2.Location(pnt)
            Loop Until Not die2.checkBounds(die1.Rectangle)
            Me.Invalidate(die2.Region)
        Else
            die2.setFace(0)
            Me.Invalidate(die2.Region)

        End If


    End Function

#End Region

#Region "UniqueInteger"

    Private Function uniqueInteger(ByVal inVar As String, ByVal newVar As String) As String

        Dim a As Array
        Dim b As String
        Dim outString As String = inVar + " "

        a = Split(newVar)

        For Each b In a
            If InStr(outString, b) <= 0 Then
                outString += b + " "
            End If
        Next

        outString = Trim(outString)
        a = Split(outString)

        Return outString

    End Function

    Public Function uniqueInteger(ByVal inVar As String) As String

        Dim a As Array = Split(inVar)
        Dim b As String
        Dim outstring As String

        For Each b In a
            If InStr(outstring, b) <= 0 Then
                outstring += b + " "
            End If
        Next

        Return outstring

    End Function

#End Region

#Region "Addends"

    Private Function addends() As String

        Dim start As String = count_pips.ToString
        Dim ok As Boolean = False
        Dim last As String = ""

        Do Until ok
            start = uniqueInteger(addends(start))
            If last = start Then ok = True
            last = start
        Loop

        start = Trim(Replace(" " + start, " 0 ", ""))

        Return start

    End Function

    Private Function addends(ByVal sum As Integer) As String
        Dim y, z As Integer
        Dim outstring As String = ""
        For y = 9 To 0 Step -1
            If Not blocks(y) Then
                For z = 9 To y + 1 Step -1
                    If Not blocks(z) Then
                        If y + z = sum Then
                            outstring += y.ToString + " " + z.ToString + " "
                        End If
                    End If
                Next
            End If
        Next

        Return outstring

    End Function

    Private Function addends(ByVal sumList As String) As String
        Dim b, outstring As String
        Dim a As Array
        Dim z As Integer

        a = Split(sumList)

        For Each b In a
            z = Val(b)
            outstring += addends(z)
        Next

        Return outstring

    End Function

#End Region

End Class

Public Class Die

#Region "Attributes"

    Private _Location As Point
    Private Faces As Integer = 1
    Private Face As Integer = 1
    Private dieImage(0 To 5) As Drawing.Image
    Private block As Drawing.Image

#End Region

#Region "Location"

    Public Function Location() As Point
        Location = _Location
    End Function

    Public Function Location(ByVal inPoint As Point)
        _Location = inPoint
    End Function

    Public Function Region() As Region
        Region = New Region(New Rectangle(_Location.X, _Location.Y, 64, 64))
    End Function

    Public Function Rectangle() As Rectangle
        Rectangle = New Rectangle(_Location.X, _Location.Y, 64, 64)
    End Function

#End Region

#Region "Build Die"
    Private Function buildImages()
        dieImage(0) = My.Resources.dice_1
        dieImage(1) = My.Resources.dice_2
        dieImage(2) = My.Resources.dice_3
        dieImage(3) = My.Resources.dice_4
        dieImage(4) = My.Resources.dice_5
        dieImage(5) = My.Resources.dice_6
        block = My.Resources.block
    End Function

    Public Sub New(ByVal NumberFaces As Integer)
        If NumberFaces < 1 Then NumberFaces = 1
        Faces = NumberFaces
        Me.roll()
        Me.buildImages()
    End Sub

    Public Sub New()
        Faces = 6
        Me.roll()
        Me.buildImages()
    End Sub

#End Region

#Region "Last Value"

    Public Function getLastRoll() As Integer
        getLastRoll = Face
    End Function

    Public Function getLastImage() As Drawing.Image
        Dim drawing As Drawing.Bitmap = New System.Drawing.Bitmap(block, 64, 64)
        Dim g As Graphics = Graphics.FromImage(drawing)
        Dim pnt As Point
        Dim workImg As Image = dieImage(getLastRoll() - 1)

        pnt.X = 32 - Int(workImg.Width / 2)
        pnt.Y = 32 - Int(workImg.Height / 2)

        g.DrawImage(workImg, pnt)

        getLastImage = drawing

    End Function

#End Region

#Region "Functions"

    Public Function setFace(ByVal inVal As Integer)
        Face = inVal
    End Function

    Public Function roll() As Integer

        Randomize()
        Rnd()

        Face = Int(Rnd() * Faces) + 1
        roll = Face

    End Function


    Public Function onClick(ByVal e As Point) As Boolean
        Dim pnt As Point
        pnt = New Point(e.X, e.Y)
        If (e.Y >= _Location.Y) And (e.Y < _Location.Y + 64) Then
            If (e.X >= _Location.X) And (e.X < _Location.X + 64) Then
                'Me.roll()
                onClick = True
            Else
                onClick = False
            End If
        End If
    End Function

    Public Function checkBounds(ByVal inRect As Rectangle) As Boolean
        If Math.Abs(Rectangle().Top - inRect.Top) < 64 And _
            Math.Abs(Rectangle().Left - inRect.Left) < 64 Then
            checkBounds = True
        Else
            checkBounds = False
        End If
    End Function



#End Region

End Class