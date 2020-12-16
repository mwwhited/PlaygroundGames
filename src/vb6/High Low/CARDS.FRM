VERSION 5.00
Begin VB.Form Form1 
   BorderStyle     =   3  'Fixed Dialog
   Caption         =   "High Low"
   ClientHeight    =   2610
   ClientLeft      =   150
   ClientTop       =   150
   ClientWidth     =   8730
   Icon            =   "cards.frx":0000
   LinkTopic       =   "Form1"
   MaxButton       =   0   'False
   MinButton       =   0   'False
   ScaleHeight     =   174
   ScaleMode       =   3  'Pixel
   ScaleWidth      =   582
   ShowInTaskbar   =   0   'False
   StartUpPosition =   2  'CenterScreen
   Begin VB.CommandButton Command4 
      Caption         =   "About"
      Height          =   255
      Left            =   4560
      TabIndex        =   26
      Top             =   2280
      Width           =   855
   End
   Begin VB.Timer Timer1 
      Interval        =   1
      Left            =   600
      Top             =   2280
   End
   Begin VB.Frame play2 
      Caption         =   "Player 2"
      Height          =   615
      Left            =   5280
      TabIndex        =   9
      Top             =   1560
      Width           =   3375
      Begin VB.OptionButton Option4 
         Caption         =   "High"
         Height          =   255
         Left            =   120
         TabIndex        =   12
         Top             =   240
         Width           =   735
      End
      Begin VB.OptionButton Option5 
         Caption         =   "Low"
         Height          =   255
         Left            =   960
         TabIndex        =   11
         Top             =   240
         Width           =   735
      End
      Begin VB.OptionButton Option6 
         Caption         =   "Same"
         Height          =   255
         Left            =   1680
         TabIndex        =   10
         Top             =   240
         Width           =   735
      End
      Begin VB.Label scr2 
         Caption         =   "100"
         BeginProperty Font 
            Name            =   "Comic Sans MS"
            Size            =   14.25
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   375
         Left            =   2520
         TabIndex        =   13
         Top             =   120
         Width           =   615
      End
   End
   Begin VB.CommandButton Command3 
      Caption         =   "Round"
      Height          =   495
      Left            =   3480
      TabIndex        =   7
      Top             =   1680
      Width           =   1215
   End
   Begin VB.CommandButton Command2 
      Caption         =   "Deal"
      Height          =   255
      Left            =   2280
      TabIndex        =   6
      ToolTipText     =   "Deal again"
      Top             =   2280
      Width           =   855
   End
   Begin VB.ComboBox cback 
      Height          =   315
      ItemData        =   "cards.frx":0442
      Left            =   3240
      List            =   "cards.frx":046A
      TabIndex        =   5
      Text            =   "Card backs"
      ToolTipText     =   "Change back of cards"
      Top             =   2280
      Width           =   1215
   End
   Begin VB.CommandButton Command1 
      Caption         =   "Close"
      Height          =   255
      Left            =   5520
      TabIndex        =   4
      ToolTipText     =   "Click here if you wana wimp out"
      Top             =   2280
      Width           =   855
   End
   Begin VB.Frame play1 
      Caption         =   "Player 1"
      Height          =   615
      Left            =   0
      TabIndex        =   0
      Top             =   1560
      Width           =   3375
      Begin VB.OptionButton Option3 
         Caption         =   "Same"
         Height          =   255
         Left            =   1680
         TabIndex        =   3
         Top             =   240
         Width           =   735
      End
      Begin VB.OptionButton Option2 
         Caption         =   "Low"
         Height          =   255
         Left            =   960
         TabIndex        =   2
         Top             =   240
         Width           =   735
      End
      Begin VB.OptionButton Option1 
         Caption         =   "High"
         Height          =   255
         Left            =   120
         TabIndex        =   1
         Top             =   240
         Width           =   735
      End
      Begin VB.Label scr1 
         Caption         =   "100"
         BeginProperty Font 
            Name            =   "Comic Sans MS"
            Size            =   14.25
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   375
         Left            =   2520
         TabIndex        =   8
         Top             =   120
         Width           =   615
      End
   End
   Begin VB.Label value 
      Caption         =   "value"
      Height          =   255
      Left            =   120
      TabIndex        =   25
      Top             =   2280
      Visible         =   0   'False
      Width           =   375
   End
   Begin VB.Label Label1 
      Caption         =   "Press Deal to Start"
      BeginProperty Font 
         Name            =   "Arriba Arriba LET"
         Size            =   48
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   1095
      Left            =   480
      TabIndex        =   24
      Top             =   240
      Width           =   7815
   End
   Begin VB.Label deal 
      Caption         =   "deal"
      Height          =   255
      Left            =   1080
      TabIndex        =   23
      Top             =   2280
      Visible         =   0   'False
      Width           =   375
   End
   Begin VB.Label round 
      Caption         =   "7"
      BeginProperty Font 
         Name            =   "Arial"
         Size            =   18
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   495
      Left            =   4800
      TabIndex        =   22
      Top             =   1680
      Width           =   375
   End
   Begin VB.Label cardblock 
      BorderStyle     =   1  'Fixed Single
      Caption         =   "1234"
      Height          =   1455
      Index           =   7
      Left            =   7560
      TabIndex        =   21
      Top             =   0
      Visible         =   0   'False
      Width           =   1095
   End
   Begin VB.Label cardblock 
      BorderStyle     =   1  'Fixed Single
      Caption         =   "1234"
      Height          =   1455
      Index           =   6
      Left            =   6480
      TabIndex        =   20
      Top             =   0
      Visible         =   0   'False
      Width           =   1095
   End
   Begin VB.Label cardblock 
      BorderStyle     =   1  'Fixed Single
      Caption         =   "1234"
      Height          =   1455
      Index           =   5
      Left            =   5400
      TabIndex        =   19
      Top             =   0
      Visible         =   0   'False
      Width           =   1095
   End
   Begin VB.Label cardblock 
      BorderStyle     =   1  'Fixed Single
      Caption         =   "1234"
      Height          =   1455
      Index           =   4
      Left            =   4320
      TabIndex        =   18
      Top             =   0
      Visible         =   0   'False
      Width           =   1095
   End
   Begin VB.Label cardblock 
      BorderStyle     =   1  'Fixed Single
      Caption         =   "1234"
      Height          =   1455
      Index           =   3
      Left            =   3240
      TabIndex        =   17
      Top             =   0
      Visible         =   0   'False
      Width           =   1095
   End
   Begin VB.Label cardblock 
      BorderStyle     =   1  'Fixed Single
      Caption         =   "1234"
      Height          =   1455
      Index           =   2
      Left            =   2160
      TabIndex        =   16
      Top             =   0
      Visible         =   0   'False
      Width           =   1095
   End
   Begin VB.Label cardblock 
      BorderStyle     =   1  'Fixed Single
      Caption         =   "1234"
      Height          =   1455
      Index           =   1
      Left            =   1080
      TabIndex        =   15
      Top             =   0
      Visible         =   0   'False
      Width           =   1095
   End
   Begin VB.Label cardblock 
      BorderStyle     =   1  'Fixed Single
      Caption         =   "1234"
      Height          =   1455
      Index           =   0
      Left            =   0
      TabIndex        =   14
      Top             =   0
      Visible         =   0   'False
      Width           =   1095
   End
End
Attribute VB_Name = "Form1"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Private Sub Command1_Click()
'MsgBox ("Fine be that way")
cdtTerm
End
End Sub

Private Sub Command2_Click()
'get rid of sign
Label1.Visible = False

'gets card wifth & height
Dim CWidth As Long, CHeight As Long
cdtInit CWidth, CHeight

X = 0
Y = 0


 'suffles and deals cards
For c% = 0 To 7
tryagain:
Randomize Time
i% = Int(Rnd(Time) * 52)
good = 1
For d% = 0 To 7
    If cardblock(d%).Caption = i% Then good = 0
Next
If good = 0 Then GoTo tryagain
cardblock(c%).Caption = i%

cdtDraw Me.hdc, (CWidth + 2) * c%, 0, cback.ListIndex + 54, ordBacks, Me.BackColor
Next
cdtDraw Me.hdc, 0, 0, cardblock(0).Caption, ordFaces, Me.BackColor

Option1 = True
Option4 = True
scr1 = 0
scr2 = 0
round.Caption = 0
deal.Caption = 1

End Sub

Private Sub Command3_Click()

'no deal yet
If deal.Caption = 0 Then
    MsgBox ("Press deal to start a game")
    GoTo donewiththispart
End If

'game over
If round.Caption = 7 Then
    'deal.Caption = 0
    MsgBox ("Game over for another game press deal to quit press close")
    GoTo donewiththispart
End If

'gets card wifth & height
Dim CWidth As Long, CHeight As Long
cdtInit CWidth, CHeight
round.Caption = round.Caption + 1

'Draws a Card
For c% = 0 To 7
If c% > round.Caption Then
    cdtDraw Me.hdc, (CWidth + 2) * c%, 0, cback.ListIndex + 54, ordBacks, Me.BackColor
Else
    cdtDraw Me.hdc, (CWidth + 2) * c%, 0, cardblock(c%).Caption, ordFaces, Me.BackColor
End If
Next

'value of card
value.Caption = Int(cardblock(round.Caption) / 4) + 1

'new card higher then old
If value.Caption > Int(cardblock(round.Caption - 1) / 4) + 1 Then
    If Option1 = True Then scr1 = Val(scr1) + value.Caption
    If Option4 = True Then scr2 = Val(scr2) + value.Caption

'new card lower then old
ElseIf value.Caption < Int(cardblock(round.Caption - 1) / 4) + 1 Then
    If Option2 = True Then scr1 = Val(scr1) + (13 - value.Caption)
    If Option5 = True Then scr2 = Val(scr2) + (13 - value.Caption)

'new card same as old card
Else
    If Option3 = True Then scr1 = Val(scr1) + 12
    If Option6 = True Then scr2 = Val(scr2) + 12

End If

donewiththispart:
End Sub

Private Sub Command4_Click()
MsgBox ("       Made By: Matthew Whited" + Chr$(13) + "for Electronics Two Applied Math Class")
End Sub

Private Sub Form_Load()

cback.ListIndex = 0
Option1 = True
Option4 = True
scr1 = 0
scr2 = 0
round.Caption = 0
deal.Caption = 0
Label1.Visible = True

For c% = 0 To 7
cardblock(c%).Caption = Str$(c%)
Next
End Sub
Private Sub Form_Unload(Cancel As Integer)
Call Command1_Click
End Sub

Private Sub Timer1_Timer()
If deal.Caption = 1 Then
    'gets card wifth & height
    Dim CWidth As Long, CHeight As Long
    cdtInit CWidth, CHeight

    For c% = 0 To 7
    If c% > round.Caption Then
        cdtDraw Me.hdc, (CWidth + 2) * c%, 0, cback.ListIndex + 54, ordBacks, Me.BackColor
    Else
        cdtDraw Me.hdc, (CWidth + 2) * c%, 0, cardblock(c%).Caption, ordFaces, Me.BackColor
    End If
    Next
End If
End Sub
