Attribute VB_Name = "CARDS"
Option Explicit
DefInt A-Z

Global Const ordFaces = 0
Global Const ordBacks = 1
Global Const ordInvert = 2

Global Const ordCrossHatch = 53
Global Const ordPlaid = 54
Global Const ordWeave = 55
Global Const ordRobot = 56
Global Const ordRoses = 57
Global Const ordIvyBlack = 58
Global Const ordIvyBlue = 59
Global Const ordFishCyan = 60
Global Const ordFishBlue = 61
Global Const ordShell = 62
Global Const ordCastle = 63
Global Const ordBeach = 64
Global Const ordCardHand = 65
Global Const ordUnused = 66
Global Const ordX = 67
Global Const ordO = 68

Global Const ordClubs = 0
Global Const ordDiamonds = 13
Global Const ordHearts = 26
Global Const ordSpades = 39
    
Declare Function cdtInit Lib "Cards32.Dll" (dx As Long, dy As Long) As Long
Declare Function cdtDrawExt Lib "Cards32.Dll" (ByVal hdc As Long, ByVal X As Long, ByVal Y As Long, ByVal dx As Long, ByVal dy As Long, ByVal ordCard As Long, ByVal iDraw As Long, ByVal clr As Long) As Long
Declare Function cdtDraw Lib "Cards32.Dll" (ByVal hdc As Long, ByVal X As Long, ByVal Y As Long, ByVal iCard As Long, ByVal iDraw As Long, ByVal clr As Long) As Long
Declare Function cdtAnimate Lib "Cards32.Dll" (ByVal hdc As Long, ByVal iCardBack As Long, ByVal X As Long, ByVal Y As Long, ByVal iState As Long) As Long
Declare Function cdtTerm Lib "Cards32.Dll" () As Long

