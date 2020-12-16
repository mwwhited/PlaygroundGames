VERSION 5.00
Begin VB.Form fieldf 
   BorderStyle     =   1  'Fixed Single
   Caption         =   "Win Pong"
   ClientHeight    =   4980
   ClientLeft      =   45
   ClientTop       =   330
   ClientWidth     =   7545
   Icon            =   "Form1.frx":0000
   LinkTopic       =   "Form1"
   MaxButton       =   0   'False
   MinButton       =   0   'False
   Moveable        =   0   'False
   ScaleHeight     =   332
   ScaleMode       =   3  'Pixel
   ScaleWidth      =   503
   StartUpPosition =   2  'CenterScreen
   Begin VB.CommandButton reset 
      Caption         =   "Click here for a new Game"
      Height          =   255
      Left            =   720
      TabIndex        =   3
      Top             =   360
      Visible         =   0   'False
      Width           =   6135
   End
   Begin VB.Timer Timer1 
      Interval        =   1
      Left            =   240
      Top             =   3720
   End
   Begin VB.Image Image3 
      Height          =   480
      Left            =   4680
      Picture         =   "Form1.frx":0442
      Top             =   4320
      Visible         =   0   'False
      Width           =   480
   End
   Begin VB.Image Image1 
      Height          =   480
      Index           =   24
      Left            =   2520
      Picture         =   "Form1.frx":0884
      Top             =   2760
      Visible         =   0   'False
      Width           =   480
   End
   Begin VB.Image Image1 
      Height          =   480
      Index           =   23
      Left            =   2040
      Picture         =   "Form1.frx":0CC6
      Top             =   2760
      Visible         =   0   'False
      Width           =   480
   End
   Begin VB.Image Image1 
      Height          =   480
      Index           =   22
      Left            =   1560
      Picture         =   "Form1.frx":1108
      Top             =   2760
      Visible         =   0   'False
      Width           =   480
   End
   Begin VB.Image Image1 
      Height          =   480
      Index           =   21
      Left            =   1080
      Picture         =   "Form1.frx":1412
      Top             =   2760
      Visible         =   0   'False
      Width           =   480
   End
   Begin VB.Image Image1 
      Height          =   480
      Index           =   20
      Left            =   600
      Picture         =   "Form1.frx":1854
      Top             =   2760
      Visible         =   0   'False
      Width           =   480
   End
   Begin VB.Image Image1 
      Height          =   480
      Index           =   19
      Left            =   2520
      Picture         =   "Form1.frx":1C96
      Top             =   2280
      Visible         =   0   'False
      Width           =   480
   End
   Begin VB.Image Image1 
      Height          =   480
      Index           =   18
      Left            =   2040
      Picture         =   "Form1.frx":20D8
      Top             =   2280
      Visible         =   0   'False
      Width           =   480
   End
   Begin VB.Image Image1 
      Height          =   480
      Index           =   17
      Left            =   1560
      Picture         =   "Form1.frx":251A
      Top             =   2280
      Visible         =   0   'False
      Width           =   480
   End
   Begin VB.Image Image1 
      Height          =   480
      Index           =   16
      Left            =   1080
      Picture         =   "Form1.frx":295C
      Top             =   2280
      Visible         =   0   'False
      Width           =   480
   End
   Begin VB.Image Image1 
      Height          =   480
      Index           =   15
      Left            =   600
      Picture         =   "Form1.frx":2D9E
      Top             =   2280
      Visible         =   0   'False
      Width           =   480
   End
   Begin VB.Image Image1 
      Height          =   480
      Index           =   14
      Left            =   2520
      Picture         =   "Form1.frx":31E0
      Top             =   1800
      Visible         =   0   'False
      Width           =   480
   End
   Begin VB.Image Image1 
      Height          =   480
      Index           =   13
      Left            =   2040
      Picture         =   "Form1.frx":3622
      Top             =   1800
      Visible         =   0   'False
      Width           =   480
   End
   Begin VB.Image Image1 
      Height          =   480
      Index           =   12
      Left            =   1560
      Picture         =   "Form1.frx":3A64
      Top             =   1800
      Visible         =   0   'False
      Width           =   480
   End
   Begin VB.Image Image1 
      Height          =   480
      Index           =   11
      Left            =   1080
      Picture         =   "Form1.frx":3EA6
      Top             =   1800
      Visible         =   0   'False
      Width           =   480
   End
   Begin VB.Image Image1 
      Height          =   480
      Index           =   10
      Left            =   600
      Picture         =   "Form1.frx":42E8
      Top             =   1800
      Visible         =   0   'False
      Width           =   480
   End
   Begin VB.Image Image1 
      Height          =   480
      Index           =   9
      Left            =   2520
      Picture         =   "Form1.frx":472A
      Top             =   1320
      Visible         =   0   'False
      Width           =   480
   End
   Begin VB.Image Image1 
      Height          =   480
      Index           =   8
      Left            =   2040
      Picture         =   "Form1.frx":4B6C
      Top             =   1320
      Visible         =   0   'False
      Width           =   480
   End
   Begin VB.Image Image1 
      Height          =   480
      Index           =   7
      Left            =   1560
      Picture         =   "Form1.frx":4FAE
      Top             =   1320
      Visible         =   0   'False
      Width           =   480
   End
   Begin VB.Image Image1 
      Height          =   480
      Index           =   6
      Left            =   1080
      Picture         =   "Form1.frx":53F0
      Top             =   1320
      Visible         =   0   'False
      Width           =   480
   End
   Begin VB.Image Image1 
      Height          =   480
      Index           =   5
      Left            =   600
      Picture         =   "Form1.frx":5832
      Top             =   1320
      Visible         =   0   'False
      Width           =   480
   End
   Begin VB.Label fieldc 
      Caption         =   "Label2"
      Height          =   255
      Left            =   1080
      TabIndex        =   5
      Top             =   4560
      Visible         =   0   'False
      Width           =   615
   End
   Begin VB.Image Image2 
      Height          =   495
      Index           =   4
      Left            =   4080
      Picture         =   "Form1.frx":5C74
      Stretch         =   -1  'True
      Top             =   4320
      Visible         =   0   'False
      Width           =   495
   End
   Begin VB.Image Image2 
      Height          =   495
      Index           =   3
      Left            =   3600
      Picture         =   "Form1.frx":124B6
      Stretch         =   -1  'True
      Top             =   4320
      Visible         =   0   'False
      Width           =   495
   End
   Begin VB.Image Image2 
      Height          =   495
      Index           =   2
      Left            =   3120
      Picture         =   "Form1.frx":1ECF8
      Stretch         =   -1  'True
      Top             =   4320
      Visible         =   0   'False
      Width           =   495
   End
   Begin VB.Image Image2 
      Height          =   495
      Index           =   1
      Left            =   2640
      Picture         =   "Form1.frx":83B9A
      Stretch         =   -1  'True
      Top             =   4320
      Visible         =   0   'False
      Width           =   495
   End
   Begin VB.Image Image2 
      Height          =   495
      Index           =   0
      Left            =   2160
      Picture         =   "Form1.frx":E8A3C
      Stretch         =   -1  'True
      Top             =   4320
      Visible         =   0   'False
      Width           =   495
   End
   Begin VB.Image Image1 
      Height          =   480
      Index           =   4
      Left            =   2520
      Picture         =   "Form1.frx":F527E
      Top             =   840
      Visible         =   0   'False
      Width           =   480
   End
   Begin VB.Image Image1 
      Height          =   480
      Index           =   3
      Left            =   2040
      Picture         =   "Form1.frx":F56C0
      Top             =   840
      Visible         =   0   'False
      Width           =   480
   End
   Begin VB.Image Image1 
      Height          =   480
      Index           =   2
      Left            =   1560
      Picture         =   "Form1.frx":F59CA
      Top             =   840
      Visible         =   0   'False
      Width           =   480
   End
   Begin VB.Image Image1 
      Height          =   480
      Index           =   1
      Left            =   1080
      Picture         =   "Form1.frx":F5E0C
      Top             =   840
      Visible         =   0   'False
      Width           =   480
   End
   Begin VB.Label engd 
      Caption         =   "eng"
      Height          =   255
      Left            =   480
      TabIndex        =   2
      Top             =   4560
      Visible         =   0   'False
      Width           =   375
   End
   Begin VB.Shape test4 
      Height          =   135
      Left            =   240
      Top             =   4560
      Visible         =   0   'False
      Width           =   135
   End
   Begin VB.Shape test3 
      Height          =   135
      Left            =   360
      Top             =   4200
      Visible         =   0   'False
      Width           =   135
   End
   Begin VB.Shape test2 
      Height          =   135
      Left            =   360
      Top             =   4440
      Visible         =   0   'False
      Width           =   135
   End
   Begin VB.Shape test1 
      BorderColor     =   &H80000012&
      Height          =   135
      Left            =   240
      Top             =   4320
      Visible         =   0   'False
      Width           =   135
   End
   Begin VB.Image Image1 
      Height          =   480
      Index           =   0
      Left            =   600
      Picture         =   "Form1.frx":F624E
      Top             =   840
      Visible         =   0   'False
      Width           =   480
   End
   Begin VB.Image puck 
      Height          =   480
      Left            =   720
      Top             =   3840
      Width           =   480
   End
   Begin VB.Label scr1 
      BorderStyle     =   1  'Fixed Single
      Caption         =   "99"
      BeginProperty Font 
         Name            =   "Times New Roman"
         Size            =   21.75
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   495
      Left            =   120
      TabIndex        =   1
      Top             =   120
      Width           =   495
   End
   Begin VB.Shape block2 
      BackColor       =   &H0080C0FF&
      BorderColor     =   &H000080FF&
      FillColor       =   &H000040C0&
      FillStyle       =   0  'Solid
      Height          =   1335
      Left            =   7200
      Top             =   1800
      Width           =   135
   End
   Begin VB.Shape block1 
      BackColor       =   &H0080C0FF&
      BorderColor     =   &H000080FF&
      FillColor       =   &H000040C0&
      FillStyle       =   0  'Solid
      Height          =   1335
      Left            =   240
      Top             =   1680
      Width           =   135
   End
   Begin VB.Label scr2 
      BorderStyle     =   1  'Fixed Single
      Caption         =   "99"
      BeginProperty Font 
         Name            =   "Times New Roman"
         Size            =   21.75
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   495
      Left            =   6960
      TabIndex        =   0
      Top             =   120
      Width           =   495
   End
   Begin VB.Label Label1 
      Caption         =   "Press F1 for Help"
      Height          =   255
      Left            =   3240
      TabIndex        =   4
      Top             =   120
      Width           =   1335
   End
   Begin VB.Image field 
      BorderStyle     =   1  'Fixed Single
      Height          =   4200
      Left            =   120
      Stretch         =   -1  'True
      Top             =   720
      Width           =   7365
   End
End
Attribute VB_Name = "fieldf"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Private Sub Form_KeyDown(KeyCode As Integer, Shift As Integer)
'If dbg <> 0 Then MsgBox (KeyCode)
Select Case KeyCode
    Case 38
        u2 = 2
    Case 40
        u2 = 1
    Case 87
        u1 = 2
    Case 88
        u1 = 1
    Case 123
        Timer1.Enabled = False
        setup.Show
    Case 112
        MsgBox ("Left Player uses W and X to move" + Chr$(13) + "Right Player uses Up and Down arrows" + Chr$(13) + "Press F12 to Enter setup")
End Select

End Sub

Private Sub Form_Load()

fieldc.Caption = field3
engp = 0
p1scr = 0
p2scr = 0

fieldf.puck.Picture = fieldf.Image1(facen).Picture
fieldf.field.Picture = fieldf.Image2(field3).Picture

Timer1.Enabled = True

'boundries
    bx1 = field.Top
    bx2 = field.Top + field.Height - puck.Height
    by1 = field.Left
    by2 = field.Left + field.Width - puck.Width
'center of blocks and block bounds
    ctrp1 = block1.Top + (block1.Height / 2)
    ctrp2 = block2.Top + (block2.Height / 2)
    bb1 = bx2 + puck.Height - block1.Height
    bb2 = bx2 + puck.Height - block2.Height
'set center of puck
    puckcx = puck.Width / 2
    puckcy = puck.Height / 2
'set middle of field
    px = ((bx2 - bx1) / 2) + bx1
    py = ((by2 - by1) / 2) + by1
'place puck
    block1.Top = px - (block1.Height / 2)
    block2.Top = px - (block2.Height / 2)
    puck.Top = px - puckcx
    puck.Left = py - puckcy
    
    b1x = block1.Top
    b2x = block2.Top

u1 = 0
u2 = 0

Randomize Time

'remove reset button
reset.Visible = False

'pick random start direction
down = Int(Rnd(Time) * 2)
up = Int(Rnd(Time) * 2)


End Sub

Private Sub Form_Unload(Cancel As Integer)
End
End Sub


Private Sub reset_Click()
Form_Load
End Sub

Private Sub Timer1_Timer()

'set puck skin
    puck.Picture = Image1(facen).Picture

'debug stuff
If dbg = 1 Then
    test1.Visible = True
    test2.Visible = True
    test3.Visible = True
    test4.Visible = True
    engd.Visible = True
    fieldc.Visible = True
Else
    test1.Visible = False
    test2.Visible = False
    test3.Visible = False
    test4.Visible = False
    engd.Visible = False
    fieldc.Visible = False
End If

'set block bounds
    front1 = block1.Left + block1.Width
    front2 = block2.Left - puck.Width
    pt11 = block1.Top - puck.Height
    pt12 = block1.Height + block1.Top
    pt21 = block2.Top - puck.Height
    pt22 = block2.Top + block2.Height
'move puck
    If up = 1 Then px = px + 10
    If up = 0 Then px = px - 10
    If down = 1 Then py = py - 10 - engp
    If down = 0 Then py = py + 10 + engp
'move block1 and block2
    If u1 = 1 Then b1x = b1x + 10
    If u1 = 2 Then b1x = b1x - 10
        If b1x < bx1 Then b1x = bx1
        If b1x > bb1 Then b1x = bb1
    If u2 = 1 Then b2x = b2x + 10
    If u2 = 2 Then b2x = b2x - 10
        If b2x < bx1 Then b2x = bx1
        If b2x > bb2 Then b2x = bb2

'watch the blocks
    If py < front1 And facen = 24 Then puck.Picture = Image3.Picture
    If py > front2 And facen = 24 Then puck.Picture = Image3.Picture
    If py <= front2 And py >= front1 And facen = 24 Then puck.Picture = Image1(facen).Picture
    If px <= pt12 Then
        If px >= pt11 Then
            If py <= front1 Then
                eng = 3
                down = 0
                py = front1
                If up + 1 = u1 Then eng = 1
                If up + 1 <> u1 Then eng = 2
                If u1 = 0 Then eng = 3
            End If
        End If
    End If
    If px <= pt22 Then
        If px >= pt21 Then
            If py >= front2 Then
                If up + 1 = u2 Then eng = 1
                If up + 1 <> u2 Then eng = 2
                If u2 = 0 Then eng = 3
                down = 1
                py = front2
            End If
        End If
    End If
    u2 = 0
    u1 = 0
If eng = 2 Then engp = engp + 5
If eng = 1 Then engp = engp - 5
If eng = 3 Then engp = engp - 1
If engp <= -5 Then engp = -5
If engp >= 60 Then engp = 60
engd.Caption = engp
eng = 0
    
'watch sidelines
    If px < bx1 Then up = 1: px = bx1
    If py < by1 Then down = 0: py = by1: p2scr = p2scr + 1
    If px > bx2 Then up = 0: px = bx2
    If py > by2 Then down = 1: py = by2: p1scr = p1scr + 1

'test bounds
    test1.Top = bx1
    test1.Left = by1
    test1.Height = bx2 - test1.Top
    test1.Width = by2 - test1.Left
    test2.Top = pt11
    test2.Height = pt12 - pt11
    test2.Width = puck.Width
    test2.Left = front1
    test3.Top = pt21
    test3.Height = pt22 - pt21
    test3.Width = puck.Width
    test3.Left = front2 - test3.Width
    test4.Top = bx1
    test4.Height = bx2 - test4.Top
    test4.Left = front1
    test4.Width = front2 - test4.Left
'place puck
    puck.Top = px
    puck.Left = py
'set scores
    scr1.Caption = p1scr
    scr2.Caption = p2scr
'place blocks
    block1.Top = b1x
    block2.Top = b2x

'test for end of game
    check = 1
    If p1scr >= pscr + 10 And check = 1 Then
        MsgBox ("Player 1 Wins")
        Timer1.Enabled = False
        reset.Visible = True
        check = 0
    End If
    If p2scr >= pscr + 10 And check = 1 Then
        MsgBox ("Player 2 Wins")
        Timer1.Enabled = False
        reset.Visible = True
        check = 0
    End If
    check = 0
  
End Sub
