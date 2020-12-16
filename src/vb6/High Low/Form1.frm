VERSION 5.00
Begin VB.Form field 
   BorderStyle     =   1  'Fixed Single
   Caption         =   "Pong"
   ClientHeight    =   4875
   ClientLeft      =   45
   ClientTop       =   330
   ClientWidth     =   7545
   LinkTopic       =   "Form1"
   MaxButton       =   0   'False
   MinButton       =   0   'False
   Moveable        =   0   'False
   ScaleHeight     =   325
   ScaleMode       =   3  'Pixel
   ScaleWidth      =   503
   StartUpPosition =   2  'CenterScreen
   Begin VB.Timer Timer1 
      Interval        =   1
      Left            =   3240
      Top             =   240
   End
   Begin VB.Label scr1 
      BorderStyle     =   1  'Fixed Single
      Caption         =   "99"
      BeginProperty Font 
         Name            =   "Orbit-B BT"
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
      Width           =   735
   End
   Begin VB.Shape block2 
      BackColor       =   &H0080C0FF&
      BorderColor     =   &H000080FF&
      FillColor       =   &H000040C0&
      FillStyle       =   0  'Solid
      Height          =   1335
      Left            =   7200
      Top             =   1680
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
   Begin VB.Shape puck 
      BackColor       =   &H00000000&
      BackStyle       =   1  'Opaque
      DrawMode        =   1  'Blackness
      FillColor       =   &H00E0E0E0&
      FillStyle       =   0  'Solid
      Height          =   495
      Left            =   4440
      Shape           =   3  'Circle
      Top             =   3360
      Width           =   495
   End
   Begin VB.Label scr2 
      BorderStyle     =   1  'Fixed Single
      Caption         =   "99"
      BeginProperty Font 
         Name            =   "Orbit-B BT"
         Size            =   21.75
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   495
      Left            =   6720
      TabIndex        =   0
      Top             =   120
      Width           =   735
   End
   Begin VB.Shape field 
      BackColor       =   &H00FFFF00&
      BackStyle       =   1  'Opaque
      Height          =   4095
      Left            =   120
      Top             =   720
      Width           =   7335
   End
End
Attribute VB_Name = "field"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False

Private Sub Form_Load()

bx1 = field.Top
bx2 = field.Top + field.Height
by1 = field.Left
by2 = field.Left + field.Width

puckcx = puck.Width / 2
puckcy = puck.Height / 2

x = ((bx2 - bx1) / 2) + bx1
y = ((by2 - by1) / 2) + by1

puck.Top = x - puckcx
puck.Left = y - puckcy

End Sub

Private Sub Timer1_Timer()
bx1 = field.Top
bx2 = field.Top + field.Height
by1 = field.Left
by2 = field.Left + field.Width
If up = 1 Then x = x + 1
If up = 0 Then x = x - 1
If Left = 1 Then y = y - 1
If Left = 0 Then y = y + 1

If x < bx1 Then up = 0: x = bx1
If y < by1 Then down = 0: x = by1
If x > bx2 Then up = 1: x = bx2
If y > by2 Then down = 1: x = by2

'puck.Top = x
'puck.Left = y

End Sub
