Imports System.Collections
Public Class Form1

    Dim grass As Image = Image.FromFile("c:\sprites\grass.ico")
    Dim water As Image = Image.FromFile("c:\sprites\water.ico")
    Dim concret As Image = Image.FromFile("c:\sprites\concret.ico")
    Dim snow As Image = Image.FromFile("c:\sprites\snow.ico")
    Dim sand As Image = Image.FromFile("c:\sprites\sand.ico")
    Dim road As Image = Image.FromFile("c:\sprites\road.ico")
    Dim grassb As Image = Image.FromFile("c:\sprites\grass-block.ico")
    Dim waterb As Image = Image.FromFile("c:\sprites\water-block.ico")
    Dim concretb As Image = Image.FromFile("c:\sprites\concret-block.ico")
    Dim snowb As Image = Image.FromFile("c:\sprites\snow-block.ico")
    Dim sandb As Image = Image.FromFile("c:\sprites\sand-block.ico")
    Dim roadb As Image = Image.FromFile("c:\sprites\road-block.ico")
    Dim xstart As Image = Image.FromFile("c:\sprites\start.ico")
    Dim xstop As Image = Image.FromFile("c:\sprites\stop.ico")
    Dim zstart As Image = Image.FromFile("c:\sprites\start-bad.ico")

    Dim dudeFront As Image = Image.FromFile("c:\sprites\dude-frt.ico")
    Dim dudeBack As Image = Image.FromFile("c:\sprites\dude-bck.ico")
    Dim dudeLeft As Image = Image.FromFile("c:\sprites\dude-lft.ico")
    Dim dudeRight As Image = Image.FromFile("c:\sprites\dude-rt.ico")
    Dim dudeWhere As Point
    Dim dudeStart As Point

    Dim badFront As Image = Image.FromFile("c:\sprites\bad-frt.ico")
    Dim badBack As Image = Image.FromFile("c:\sprites\bad-bck.ico")
    Dim badLeft As Image = Image.FromFile("c:\sprites\bad-lft.ico")
    Dim badRight As Image = Image.FromFile("c:\sprites\bad-rt.ico")
    Dim badWhere As Point
    Dim badStart As Point

    Dim map As System.IO.StreamReader = System.IO.File.OpenText("c:\sprites\map1.txt")
    Dim mapArray(0 To 400) As String
    Dim mapLastLine As Integer
    Dim mapMaxWidth As Integer
    Dim mapLoaded As Boolean = False

    Dim mRand As New Random
    Dim win As Boolean = False
    Dim lose As Boolean = False

    Private Sub Form1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Me.KeyPress

        Dim key As Char
        Dim dudeCurrent As Image
        Dim dudeLast As Point
        Dim dudeX As Integer
        Dim dudeY As Integer
        Dim newStop As String

        dudeLast = dudeWhere
        dudeCurrent = dude.Image
        key = e.KeyChar
        Select Case key
            Case "W", "w"
                dudeWhere.Y -= 32
                dudeCurrent = dudeBack
            Case "S", "s"
                dudeWhere.Y += 32
                dudeCurrent = dudeFront
            Case "A", "a"
                dudeWhere.X -= 32
                dudeCurrent = dudeLeft
            Case "D", "d"
                dudeWhere.X += 32
                dudeCurrent = dudeRight
        End Select

        dudeX = Int(dudeWhere.X / 32) + 1
        dudeY = Int(dudeWhere.Y / 32)
        newStop = Mid(mapArray(dudeY), dudeX, 1)

        Select Case newStop
            Case "w", "W", "r", "n", "s", "g"
                dudeWhere = dudeLast
            Case "x"
                win = True
        End Select

        If dudeWhere.Y < 0 Then dudeWhere.Y = 0
        If dudeWhere.X < 0 Then dudeWhere.X = 0
        If dudeWhere.X > (mapMaxWidth - 1) * 32 Then dudeWhere.X = (mapMaxWidth - 1) * 32
        If dudeWhere.Y > (mapLastLine - 1) * 32 Then dudeWhere.Y = (mapLastLine - 1) * 32
        dudeWhere.X = dudeWhere.X - (dudeWhere.X Mod 32)
        dudeWhere.X = dudeWhere.X - (dudeWhere.X Mod 32)

        dude.Location = dudeWhere
        dude.Image = dudeCurrent

    End Sub

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Dim mapArrayIndex As Integer = 0
        If mapArray Is Nothing Then mapArray(0) = ""

        If Not mapLoaded Then
            Do Until map.EndOfStream
                mapArray(mapArrayIndex) = map.ReadLine
                If InStr(mapArray(mapArrayIndex), "X") <> 0 Then
                    dudeStart.Y = mapArrayIndex * 32
                    dudeStart.X = (InStr(mapArray(mapArrayIndex), "X") - 1) * 32
                End If
                If InStr(mapArray(mapArrayIndex), "Z") <> 0 Then
                    badStart.Y = mapArrayIndex * 32
                    badStart.X = (InStr(mapArray(mapArrayIndex), "Z") - 1) * 32
                End If
                mapArrayIndex += 1
            Loop
            mapLoaded = True
            mapMaxWidth = Len(mapArray(0))
            mapLastLine = mapArrayIndex
        End If

        badGuyAI.Enabled = True

        dudeWhere = dudeStart
        badWhere = badStart

        dude.Location = dudeWhere
        dude.Image = dudeFront
        dude.Parent = Panel1

        badGuy.Location = badWhere
        badGuy.Parent = Panel1
        badGuy.Image = badFront

        Me.Width = mapMaxWidth * 32
        Me.Height = (mapLastLine + 1) * 32
        Me.CenterToScreen()

    End Sub

    Private Sub Panel1_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Panel1.Paint

        Dim mapLine As String
        Dim mapLineNumber As Integer

        Dim mapPiece As Char
        Dim mapPieceNumber As Integer

        Dim place As Point
        Dim activeImage As Image = grass


        mapLineNumber = 0

        For mapLineNumber = 0 To mapLastLine
            mapLine = mapArray(mapLineNumber)
            place.Y = mapLineNumber * 32
            For mapPieceNumber = 0 To Len(mapLine) - 1
                mapPiece = Mid(mapLine, mapPieceNumber + 1, 1)
                place.X = mapPieceNumber * 32
                Select Case mapPiece
                    Case "G"
                        activeImage = grass
                    Case "S"
                        activeImage = sand
                    Case "N"
                        activeImage = snow
                    Case "W"
                        activeImage = water
                    Case "R"
                        activeImage = road
                    Case "C"
                        activeImage = concret
                    Case "g"
                        activeImage = grassb
                    Case "s"
                        activeImage = sandb
                    Case "n"
                        activeImage = snowb
                    Case "w"
                        activeImage = waterb
                    Case "r"
                        activeImage = roadb
                    Case "c"
                        activeImage = concretb
                    Case "X"
                        activeImage = xstart
                    Case "x"
                        activeImage = xstop
                    Case "Z"
                        activeImage = zstart
                End Select
                If Not (activeImage Is Nothing) Then e.Graphics.DrawImage(activeImage, place)
            Next
        Next
    End Sub

    Private Sub PictureBox1_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles dude.Paint, badGuy.Paint
        sender.BackColor = Color.Transparent
        If win Then
            badGuyAI.Enabled = False
            MsgBox("Good Job!")
            win = False
            Call Form1_Load(sender, e)
        End If
        If lose Then
            badGuyAI.Enabled = False
            MsgBox("Loser!")
            lose = False
            Call Form1_Load(sender, e)
        End If

    End Sub


    Private Sub badGuyAI_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles badGuyAI.Tick
        Dim key As Integer
        Dim badCurrent As Image
        Dim badLast As Point
        Dim badX As Integer
        Dim badY As Integer
        Dim newStop As String


        badLast = badWhere
        badCurrent = badGuy.Image
        key = mRand.Next(1, 5)
        Select Case key
            Case 1
                badWhere.Y -= 32
                badCurrent = badBack
            Case 2
                badWhere.Y += 32
                badCurrent = badFront
            Case 3
                badWhere.X -= 32
                badCurrent = badLeft
            Case 4
                badWhere.X += 32
                badCurrent = badRight
        End Select

        badX = Int(badWhere.X / 32) + 1
        badY = Int(badWhere.Y / 32)
        newStop = Mid(mapArray(badY), badX, 1)

        Select Case newStop
            Case "w", "W", "r", "n", "s", "g"
                badWhere = badLast
                'Case "x"
                'win = True
        End Select

        If badWhere.Y < 0 Then badWhere.Y = 0
        If badWhere.X < 0 Then badWhere.X = 0
        If badWhere.X > (mapMaxWidth - 1) * 32 Then badWhere.X = (mapMaxWidth - 1) * 32
        If badWhere.Y > (mapLastLine - 1) * 32 Then badWhere.Y = (mapLastLine - 1) * 32
        badWhere.X = badWhere.X - (badWhere.X Mod 32)
        badWhere.X = badWhere.X - (badWhere.X Mod 32)

        badGuy.Location = badWhere
        badGuy.Image = badCurrent

        If dudeWhere = badWhere Then
            lose = True
        End If


    End Sub
End Class
