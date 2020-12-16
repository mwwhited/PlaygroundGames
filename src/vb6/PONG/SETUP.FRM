VERSION 5.00
Begin VB.Form setup 
   Caption         =   "Win Pong Setup"
   ClientHeight    =   2025
   ClientLeft      =   60
   ClientTop       =   345
   ClientWidth     =   5625
   Icon            =   "setup.frx":0000
   LinkTopic       =   "Form1"
   ScaleHeight     =   2025
   ScaleWidth      =   5625
   StartUpPosition =   3  'Windows Default
   Begin VB.VScrollBar VScroll1 
      Height          =   1575
      Left            =   2400
      Max             =   4
      TabIndex        =   6
      Top             =   360
      Width           =   255
   End
   Begin VB.CheckBox Check1 
      Caption         =   "Debug"
      Height          =   375
      Left            =   240
      TabIndex        =   5
      Top             =   1680
      Width           =   855
   End
   Begin VB.HScrollBar facep 
      Height          =   255
      Left            =   240
      Max             =   24
      TabIndex        =   1
      Top             =   1320
      Width           =   1335
   End
   Begin VB.HScrollBar mscr 
      Height          =   255
      Left            =   240
      Max             =   89
      Min             =   -9
      TabIndex        =   0
      Top             =   480
      Value           =   10
      Width           =   1455
   End
   Begin VB.Label Label4 
      Caption         =   "Change playing field"
      Height          =   255
      Left            =   3360
      TabIndex        =   7
      Top             =   0
      Width           =   1455
   End
   Begin VB.Image Image1 
      Height          =   1620
      Left            =   2760
      Stretch         =   -1  'True
      Top             =   360
      Width           =   2775
   End
   Begin VB.Image puck 
      Height          =   480
      Left            =   1680
      Top             =   1080
      Width           =   480
   End
   Begin VB.Label Label1 
      Caption         =   "Pick your Puck"
      Height          =   255
      Left            =   240
      TabIndex        =   4
      Top             =   960
      Width           =   1095
   End
   Begin VB.Label Label2 
      Caption         =   "Max Score"
      Height          =   255
      Left            =   240
      TabIndex        =   3
      Top             =   120
      Width           =   855
   End
   Begin VB.Label Label3 
      Caption         =   "10"
      Height          =   255
      Left            =   1320
      TabIndex        =   2
      Top             =   120
      Width           =   255
   End
End
Attribute VB_Name = "setup"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False

Private Sub Check1_Click()
dbg = Check1.Value
End Sub

Private Sub facep_Change()
    facen = facep.Value
    puck.Picture = fieldf.Image1(facen).Picture
    fieldf.puck.Picture = setup.puck.Picture
End Sub

Private Sub Form_Activate()
setup.mscr.Value = pscr
setup.Image1.Picture = fieldf.Image2(field3).Picture
setup.puck.Picture = fieldf.puck.Picture
setup.Check1.Value = dbg
setup.Label3.Caption = mscr.Value + 10
End Sub

Private Sub Form_Unload(Cancel As Integer)
fieldf.Timer1.Enabled = True
fieldf.field.Picture = fieldf.Image2(field3).Picture
fieldf.puck.Picture = setup.puck.Picture
pscr = mscr.Value
End Sub


Private Sub mscr_Change()
Label3.Caption = mscr.Value + 10
End Sub

Private Sub VScroll1_Change()
Image1.Picture = fieldf.Image2(VScroll1.Value).Picture
field3 = VScroll1.Value
fieldf.field.Picture = fieldf.Image2(field3).Picture
End Sub
