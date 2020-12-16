Imports System.Collections
Imports System.Reflection

Public Class Form1

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Dim asm As Assembly = [Assembly].GetExecutingAssembly()


        grass = Image.FromStream(asm.GetManifestResourceStream("WindowsApplication1.grass.ico"))
        water = Image.FromStream(asm.GetManifestResourceStream("WindowsApplication1.water.ico"))
        concret = Image.FromStream(asm.GetManifestResourceStream("WindowsApplication1.concret.ico"))
        snow = Image.FromStream(asm.GetManifestResourceStream("WindowsApplication1.snow.ico"))
        sand = Image.FromStream(asm.GetManifestResourceStream("WindowsApplication1.sand.ico"))
        road = Image.FromStream(asm.GetManifestResourceStream("WindowsApplication1.road.ico"))
        grassb = Image.FromStream(asm.GetManifestResourceStream("WindowsApplication1.grass-block.ico"))
        waterb = Image.FromStream(asm.GetManifestResourceStream("WindowsApplication1.water-block.ico"))
        concretb = Image.FromStream(asm.GetManifestResourceStream("WindowsApplication1.concret-block.ico"))
        snowb = Image.FromStream(asm.GetManifestResourceStream("WindowsApplication1.snow-block.ico"))
        sandb = Image.FromStream(asm.GetManifestResourceStream("WindowsApplication1.sand-block.ico"))
        roadb = Image.FromStream(asm.GetManifestResourceStream("WindowsApplication1.road-block.ico"))
        xstart = Image.FromStream(asm.GetManifestResourceStream("WindowsApplication1.start.ico"))
        xstop = Image.FromStream(asm.GetManifestResourceStream("WindowsApplication1.stop.ico"))
        zstart = Image.FromStream(asm.GetManifestResourceStream("WindowsApplication1.start-bad.ico"))

        dudeFront = Image.FromStream(asm.GetManifestResourceStream("WindowsApplication1.dude-frt.ico"))
        dudeBack = Image.FromStream(asm.GetManifestResourceStream("WindowsApplication1.dude-bck.ico"))
        dudeLeft = Image.FromStream(asm.GetManifestResourceStream("WindowsApplication1.dude-lft.ico"))
        dudeRight = Image.FromStream(asm.GetManifestResourceStream("WindowsApplication1.dude-rt.ico"))

        badFront = Image.FromStream(asm.GetManifestResourceStream("WindowsApplication1.bad-frt.ico"))
        badBack = Image.FromStream(asm.GetManifestResourceStream("WindowsApplication1.bad-bck.ico"))
        badLeft = Image.FromStream(asm.GetManifestResourceStream("WindowsApplication1.bad-lft.ico"))
        badRight = Image.FromStream(asm.GetManifestResourceStream("WindowsApplication1.bad-rt.ico"))

        map = New IO.StreamReader(asm.GetManifestResourceStream("WindowsApplication1.map1.txt"))

    End Sub

    Dim grass As Image
    Dim water As Image
    Dim concret As Image
    Dim snow As Image
    Dim sand As Image
    Dim road As Image
    Dim grassb As Image
    Dim waterb As Image
    Dim concretb As Image
    Dim snowb As Image
    Dim sandb As Image
    Dim roadb As Image
    Dim xstart As Image
    Dim xstop As Image
    Dim zstart As Image

    Dim dudeFront As Image
    Dim dudeBack As Image
    Dim dudeLeft As Image
    Dim dudeRight As Image
    Dim dudeWhere As Point
    Dim dudeStart As Point

    Dim badFront As Image
    Dim badBack As Image
    Dim badLeft As Image
    Dim badRight As Image

    Dim badWhere As Point
    Dim badStart As Point

    Dim map As System.IO.StreamReader

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
