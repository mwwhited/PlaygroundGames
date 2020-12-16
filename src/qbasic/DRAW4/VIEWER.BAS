'                         Viewer ver. 4.00 for QCADD ver. 4.00 and Qbasic
'
'                Made by: Matthew Whited
'                 finished April 5, 1996
'                  Share Ware  (feel free to pass on to a friend)
'             
'      Install this program into your basic file for use of the pictures
'               from QCADD ver. 4.00
'
DEFINT A-Z
DECLARE SUB TESTCHR (FILE$, sec)
loadfile:
CLS
CLEAR , , 9000
SCREEN TEXTMODE
WIDTH 80
FILE$ = "": SCE = 0
CALL TESTCHR(FILE$, SCE)
OPEN FILE$ FOR INPUT AS #1
A = 0: B = 0: C = 0: D = 0: E = 0: F = 0: J = 0: K = 0: L = 0: M = 0: N = 0: O = 0: p = 0: q = 0: R = 0
conloadc:
INPUT #1, ZZ, YY, RR, CC
IF CC = 0 AND ZZ = 0 AND RR = 0 AND YY = 0 THEN GOTO LINELOAD
NEXTPART:
circle1(A, B, C) = ZZ
circle2(A, B, C) = YY
RAD(A, B, C) = RR
COL2(A, B, C) = CC
A = A + 1
IF A = 11 THEN B = B + 1
IF A = 11 THEN A = 0
IF B = 11 THEN C = C + 1
IF B = 11 THEN B = 0
GOTO conloadc:
LINELOAD:
conloadl:
INPUT #1, ZZ, YY, Z, Y, CC
IF CC = 0 AND ZZ = 0 AND YY = 0 AND Y = 0 AND Z = 0 THEN GOTO fillload
line1(D, E, F) = ZZ
line2(D, E, F) = YY
line3(D, E, F) = Z
line4(D, E, F) = Y
LINE5(D, E, F) = CC
D = D + 1
IF D = 11 THEN E = E + 1
IF D = 11 THEN D = 0
IF E = 11 THEN F = F + 1
IF E = 11 THEN E = 0
GOTO conloadl
fillload:
conloadf:
INPUT #1, ZZ, YY, CC
IF ZZ = 0 AND YY = 0 AND CC = 0 THEN GOTO BOXload
fill1(J, K, L) = ZZ
fill2(J, K, L) = YY
FILL3(J, K, L) = CC
J = J + 1
IF J = 11 THEN K = K + 1
IF J = 11 THEN J = 0
IF K = 11 THEN L = L + 1
IF K = 11 THEN K = 0
GOTO conloadf
BOXload:
conloadb:
INPUT #1, ZZ, YY, Z, Y, CC
IF CC = 0 AND ZZ = 0 AND YY = 0 AND Y = 0 AND Z = 0 THEN GOTO PSETload
BOX1(M, N, O) = ZZ
BOX2(M, N, O) = YY
BOX3(M, N, O) = Z
BOX4(M, N, O) = Y
BOX5(M, N, O) = CC
M = M + 1
IF M = 11 THEN N = N + 1
IF M = 11 THEN M = 0
IF N = 11 THEN O = O + 1
IF N = 11 THEN N = 0
GOTO conloadb
PSETload:
conloaDP:
INPUT #1, ZZ, YY, CC
IF CC = 0 AND ZZ = 0 AND YY = 0 THEN GOTO PALLOAD
point1(p, q, R) = ZZ
point2(p, q, R) = YY
point3(p, q, R) = CC
p = p + 1
IF p = 11 THEN q = q + 1
IF p = 11 THEN p = 0
IF q = 11 THEN R = R + 1
IF q = 11 THEN q = 0
GOTO conloaDP
PALLOAD:
S = 0: T = 0: U = 0
CONLODPAL:
INPUT #1, ZZ, YY, BL, GR
IF ZZ = 0 AND YY = 0 THEN GOTO DONELOAD
OLDCOL(S, T, U) = ZZ
NEWCOLR(S, T, U) = YY
NEWCOLB(S, T, U) = BL
NEWCOLG(S, T, U) = GR
S = S + 1
IF S = 11 THEN T = T + 1: S = 0
IF T = 11 THEN U = T + 1: T = 0
GOTO CONLODPAL
DONELOAD:
INPUT #1, SCR3
CLOSE
IF SCR3 = 11 THEN GOSUB SCR11
IF SCR3 = 12 THEN GOSUB SCR1A2
IF SCR3 = 13 THEN GOSUB SCR13
IF ex$ = "ON" THEN RETURN
first:
SPEED = 5: cursur = 30: CURSUR2 = 30: COL = 1
KEY2:
ERROR$ = ""
grid:
CLS
REDRAW:
SCREEN SCR3
PALSETUP:
S = 0: T = 0: U = 0
PALSETUP2:
IF OLDCOL(S, T, U) = 0 AND NEWCOLR(S, T, U) = 0 AND NEWCOLB(S, T, U) = 0 AND NEWCOLG(S, T, U) = 0 THEN GOTO setcircle
OLD = OLDCOL(S, T, U)
RED = NEWCOLR(S, T, U)
BLUE = NEWCOLB(S, T, U)
GREEN = NEWCOLG(S, T, U)
PALETTE OLD - 1, RED + (BLUE * 65536) + (GREEN * 256)
S = S + 1
IF S = 11 THEN T = T + 1: S = 0
IF T = 11 THEN U = U + 1: T = 0
GOTO PALSETUP2
setcircle:
A = 0: B = 0: C = 0
checkc:
IF circle1(A, B, C) = 0 THEN GOTO DBOX
GOTO configc
printc:
CIRCLE (ZZ, YY), RR, CC
nextlocatec:
A = A + 1
IF A = 11 THEN B = B + 1
IF A = 11 THEN A = 0
IF B = 11 THEN C = C + 1
IF B = 11 THEN B = 0
GOTO checkc
configc:
ZZ = circle1(A, B, C)
YY = circle2(A, B, C)
RR = RAD(A, B, C)
CC = COL2(A, B, C)
GOTO printc
DBOX:
O = 0: N = 0: M = 0
CHECKDB:
IF BOX1(M, N, O) = 0 THEN GOTO LINES
GOTO CONFIGB
PRINTB:
LINE (YY, ZZ)-(YY, Z), CC
LINE (YY, ZZ)-(Y, ZZ), CC
LINE (Y, Z)-(Y, ZZ), CC
LINE (Y, Z)-(YY, Z), CC
nextlocateB:
M = M + 1
IF M = 11 THEN N = N + 1
IF M = 11 THEN M = 0
IF N = 11 THEN O = O + 1
IF N = 11 THEN N = 0
GOTO CHECKDB
CONFIGB:
YY = BOX1(M, N, O)
ZZ = BOX2(M, N, O)
Y = BOX3(M, N, O)
Z = BOX4(M, N, O)
CC = BOX5(M, N, O)
GOTO PRINTB
LINES:
D = 0: E = 0: F = 0
checkf:
IF line1(D, E, F) = 0 THEN GOTO dPOINT
GOTO configf
printf:
LINE (ZZ, YY)-(Z, Y), CC
nextlocatef:
D = D + 1
IF D = 11 THEN E = E + 1
IF D = 11 THEN D = 0
IF E = 11 THEN F = F + 1
IF E = 11 THEN E = 0
GOTO checkf
configf:
ZZ = line1(D, E, F)
YY = line2(D, E, F)
Z = line3(D, E, F)
Y = line4(D, E, F)
CC = LINE5(D, E, F)
GOTO printf
dPOINT:
p = 0: q = 0: R = 0
CHECKP:
IF point1(p, q, R) = 0 THEN GOTO dFILL
GOTO configP
printP:
PSET (ZZ, YY), CC
nextlocated:
p = p + 1
IF p = 11 THEN q = q + 1
IF p = 11 THEN p = 0
IF q = 11 THEN R = R + 1
IF q = 11 THEN q = 0
GOTO CHECKP
configP:
ZZ = point1(p, q, R)
YY = point2(p, q, R)
CC = point3(p, q, R)
GOTO printP
dFILL:
J = 0: K = 0: L = 0
checkd:
IF fill1(J, K, L) = 0 THEN GOTO ADDTO3
GOTO configd
printd:
PAINT (ZZ, YY), CC
LKnextlocated:
J = J + 1
IF J = 11 THEN K = K + 1
IF J = 11 THEN J = 0
IF K = 11 THEN L = L + 1
IF K = 11 THEN K = 0
GOTO checkd
configd:
ZZ = fill1(J, K, L)
YY = fill2(J, K, L)
CC = FILL3(J, K, L)
GOTO printd
ADDTO3:

DEFSNG A-Z
SUB help
SCREEN 0
WIDTH 80
CLS
PRINT "     SLOW             FAST"
PRINT
PRINT "   7   8   9        Q   W   E"
PRINT "    \  |  /          \  |  /"
PRINT "   4 - 5 - 6        A - S - D"
PRINT "    /  |  \          /  |  \"
PRINT "   1   2   3        Z   X   C"
PRINT
PRINT " SLOW       FAST"
PRINT "  1          Z      moves down and to the left"
PRINT "  2          X      moves down"
PRINT "  3          C      moves down and to the right"
PRINT "  4          A      moves left"
PRINT "  5          S      turns the grid on and off"
PRINT "  6          D      moves right"
PRINT "  7          Q      moves up and to the left"
PRINT "  8          W      moves up"
PRINT "  9          E      moves up and to the right"
PRINT : PRINT "NUMLOCK must be ON": PRINT
INPUT "press enter to continue", Duh4
PRINT
PRINT "To draw a circle move the curser to the location of choice, press (R)"
PRINT "To draw lines move the curser to the first spot and press the (V) then move to the second spot and press the (V)"
PRINT "To fill a area move the curser into the area of choice and press the (F)"
PRINT "To use extra controls press  ESCAPE"
PRINT "To change the speed of the fast keys press the (N)"
PRINT "To make a point move cursur to location and press (G) "
PRINT "To home the cursur at 30, 30 press the (H) key"
PRINT "To change then cursur press the (B) key"
PRINT "To make a box move the cursur to then start point hit (T) then move to end and press it again"
PRINT "To change color up press (+) and to move the color down press the (-)"
PRINT "To use the full screen palette in screen 13 press (Y)"
PRINT "To make an unlisted color press (M) for palette"
PRINT "To change background color use color 0 and go to palette"
PRINT
PRINT "Press F1 for HELP"
PRINT "Press F2 for SAVE"
PRINT "Press F3 for LOAD"
PRINT "Press F4 for NEW"
PRINT "Press F5 for UNDO"
PRINT "Press F6 for Quit"
PRINT "Press F7 for printer"
PRINT
INPUT "Press enter to continue", Duh4

END SUB

DEFINT A-Z
SUB TESTCHR (FILE$, SCE) STATIC
SHELL "DIR/P/W"
WHAT:
A = 0: B = 0
what2:
IF file2$(A, B) <> "" THEN file2$(A, B) = ""
A = A + 1
IF A = 11 THEN B = B + 1
IF A = 11 THEN A = 0
IF file2$(A, B) = "" THEN GOTO addletter2
GOTO what2
addletter2:
A = 0: B = 0
LOCATE 1, 1: PRINT "(ESCAPE to cancel  ENTER to set) file name ";
addletter:
PRINT file2$(A, B);
A = A + 1
IF A = 11 THEN B = B + 1
IF A = 11 THEN A = 0
IF file2$(A, B) = "" OR file2$(A, B) = " " THEN GOTO nextletter3
IF A <= 0 AND B <= 0 THEN A = 0 AND B = 0: GOTO nextletter
GOTO addletter
nextletter3:
PRINT "            "
nextletter:
file2$(A, B) = INKEY$
IF file2$(A, B) = "" THEN GOTO nextletter
IF file2$(A, B) = CHR$(13) THEN file2$(A, B) = " ": GOTO NEXTTEST
IF file2$(A, B) = " " THEN PRINT CHR$(7)
IF file2$(A, B) = "" THEN GOTO DOWN2
GOTO addletter2
NEXTTEST:
A = 0: B = 0
NEXTTEST3:
IF file2$(A, B) = " " THEN GOTO NEXTTEST2
FILE$ = FILE$ + file2$(A, B)
A = A + 1
IF A = 11 THEN B = B + 1
IF A = 11 THEN A = 0
GOTO NEXTTEST3
DOWN2:
IF file2$(A, B) = "" THEN file2$(A, B) = ""
A = A - 1
IF A = -1 THEN B = B - 1
IF A = -1 THEN A = 10
IF B = -1 THEN B = 0
file2$(A, B) = ""
GOTO addletter2
NEXTTEST2:
sec = 0
IF INSTR(1, FILE$, ".") = 0 THEN GOTO HUHUH
TESTNEXT:
IF INSTR(1, FILE$, ",") <> 0 THEN SCE = SCE + 1
IF INSTR(1, FILE$, " ") <> 0 THEN SCE = SCE + 1
IF INSTR(1, FILE$, "*") <> 0 THEN SCE = SCE + 1
IF INSTR(1, FILE$, "-") <> 0 THEN SCE = SCE + 1
IF INSTR(1, FILE$, "+") <> 0 THEN SCE = SCE + 1
IF INSTR(1, FILE$, "?") <> 0 THEN SCE = SCE + 1
IF INSTR(1, FILE$, ">") <> 0 THEN SCE = SCE + 1
IF INSTR(1, FILE$, "<") <> 0 THEN SCE = SCE + 1
IF INSTR(1, FILE$, "|") <> 0 THEN SCE = SCE + 1
IF INSTR(1, FILE$, "&") <> 0 THEN SCE = SCE + 1
IF INSTR(1, FILE$, "%") <> 0 THEN SCE = SCE + 1
IF INSTR(1, FILE$, "@") <> 0 THEN SCE = SCE + 1
IF INSTR(1, FILE$, "#") <> 0 THEN SCE = SCE + 1
IF INSTR(1, FILE$, "$") <> 0 THEN SCE = SCE + 1
IF INSTR(1, FILE$, "^") <> 0 THEN SCE = SCE + 1
IF INSTR(1, FILE$, "(") <> 0 THEN SCE = SCE + 1
IF INSTR(1, FILE$, "=") <> 0 THEN SCE = SCE + 1
IF INSTR(1, FILE$, ")") <> 0 THEN SCE = SCE + 1
IF INSTR(1, FILE$, "'") <> 0 THEN SCE = SCE + 1
IF INSTR(1, FILE$, "{") <> 0 THEN SCE = SCE + 1
IF INSTR(1, FILE$, "}") <> 0 THEN SCE = SCE + 1
IF INSTR(1, FILE$, "[") <> 0 THEN SCE = SCE + 1
IF INSTR(1, FILE$, "]") <> 0 THEN SCE = SCE + 1
IF SCE <> 0 THEN GOTO WHAT
GOTO WHATEVER
HUHUH:
FILE$ = FILE$ + ".PIC"
IF SCE = 0 THEN GOTO TESTNEXT
WHATEVER:

END SUB

