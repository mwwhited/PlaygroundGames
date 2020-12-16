DEFINT A-Z
DECLARE SUB help ()
DECLARE SUB TESTCHR (FILE$, sec)
DECLARE SUB LOADKEYS ()
'ON ERROR GOTO ERRORTEST2
CURSUR = 30: CURSUR2 = 30
CLS
SCREEN TEXTMODE
PRINT "Welcome to QCADD ver. 3.00"
PRINT "            BY Matthew & Allen"
INPUT "Would you like to draw(y/n)", DRAW$
IF DRAW$ = "N" THEN GOTO done
IF DRAW$ = "n" THEN GOTO done
PRINT "Make sure the NUM LOCK is on"
INPUT "Would you like instructions(Y/N)", INSTR$
IF INSTR$ = "y" THEN GOTO INSTR2
IF INSTR$ = "Y" THEN GOTO INSTR2
HOME1:
SCREEN TEXTMODE
INPUT "Would you like a grid to start with(Y/N)", option$
IF option$ = "Y" THEN GOTO option1
IF option$ = "y" THEN GOTO option1
IF option$ <> "Y" THEN option$ = "n"
GOTO screen1
INSTR2:
CALL help
IF KEYS3$ = "LIST" THEN GOTO KEY2
CLS
GOTO HOME1
option1:
INPUT "What grid size do want(numbers 10-50)"; GRID
IF GRID > 50 THEN GRID = 50
IF GRID < 10 THEN GRID = 10
screen1:
PRINT "Pick a screen"
PRINT "  1) screen 13  res 320x200  with 256 colors"
PRINT "  2) screen 12  res 640x480  with 16 colors"
PRINT "  3) screen 11  res 640x480  with 2 colors"
REASK:
PRINT "(1/2/3)"; : INPUT HI$
IF HI$ = "1" THEN GOSUB SCR13
IF HI$ = "2" THEN GOSUB SCR1A2
IF HI$ = "3" THEN GOSUB SCR11
IF HI$ = "1" OR HI$ = "3" OR HI$ = "2" THEN GOTO cont
GOSUB SCR13
cont:
SCREEN TEXTMODE
INPUT "What is the speed of fast keys(numbers 5-20)"; SPEED
IF SPEED > 20 THEN SPEED = 20
IF SPEED < 5 THEN SPEED = 5
INPUT "Would you like to load a file(Y/N)"; FLOAD$
IF FLOAD$ = "y" THEN GOTO loadfile
IF FLOAD$ = "Y" THEN GOTO loadfile
GOTO first
loadfile:
ERROR$ = "load"
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
SPEED = 5: CURSUR = 30: CURSUR2 = 30: COL = 1
KEY2:
ERROR$ = ""
GRID:
CLS
REDRAW:
SCREEN SCR3
IF VIEW$ = "y" THEN CLS
IF VIEW$ = "Y" THEN CLS
PALSETUP:
S = 0: T = 0: U = 0
PALSETUP2:
IF OLDCOL(S, T, U) = O AND NEWCOLR(S, T, U) = 0 AND NEWCOLB(S, T, U) = 0 AND NEWCOLG(S, T, U) = 0 THEN GOTO setcircle
OLD = OLDCOL(S, T, U)
RED = NEWCOLR(S, T, U)
BLUE = NEWCOLB(S, T, U)
GREEN = NEWCOLG(S, T, U)
PALETTE OLD, RED + (BLUE * 65536) + (GREEN * 256)
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
IF VIEW$ = "y" OR VIEW$ = "Y" OR VIEW$ = "PRINT" THEN GOTO viewy
VIEW$ = "n"
GRID2:
SCREEN SCR3
IF option$ <> "y" THEN GOTO ADDTO
IF option$ <> "Y" THEN GOTO ADDTO
FOR X = 0 TO 640 STEP GRID
FOR I = 0 TO 640 STEP GRID
PSET (X, I): PSET (I, X)
NEXT I
NEXT X
IF GRID > 50 THEN GRID = 50
IF GRID < 10 THEN GRID = 10
ADDTO:
goal = 0
IF CURSUR <= -1 THEN CURSUR = HIGHX
IF CURSUR2 <= -1 THEN CURSUR2 = HIGHY
IF CURSUR >= HIGHX + 1 THEN CURSUR = 0
IF CURSUR2 >= HIGHY + 1 THEN CURSUR2 = 0
' cursur is over cursur2 is down
SCREEN SCR3
IF lin3$ = "on" THEN LINE (LOCN1, LOCN2)-(CURSUR, CURSUR2), COL, B
IF lin2$ = "on" THEN LINE (LOCATION1, LOCATION2)-(CURSUR, CURSUR2), COL
IF cur$ = "cir" OR cur$ = "both" OR cur$ = "" THEN CIRCLE (CURSUR, CURSUR2), 1, COL
IF cur$ = "cir" OR cur$ = "both" OR cur$ = "" THEN CIRCLE (CURSUR, CURSUR2), 5, COL
IF cur$ = "cir" OR cur$ = "both" OR cur$ = "" THEN CIRCLE (CURSUR, CURSUR2), 10, COL
IF cur$ = "line" OR cur$ = "both" THEN LINE (0, CURSUR2)-(640, CURSUR2), COL
IF cur$ = "line" OR cur$ = "both" THEN LINE (CURSUR, 0)-(CURSUR, 480), COL
IF cur$ = "PLUS" THEN LINE (CURSUR + 10, CURSUR2)-(CURSUR - 10, CURSUR2), COL
IF cur$ = "PLUS" THEN LINE (CURSUR, CURSUR2 - 10)-(CURSUR, CURSUR2 + 10), COL
IF ex$ = "ON" THEN RETURN
KEY(1) ON: KEY(2) ON: KEY(3) ON: KEY(5) ON: KEY(6) ON: KEY(4) ON: KEY(7) ON: KEY(25) ON
LOCATE 1, 1: PRINT OBJ$: LOCATE 1, 5: PRINT OBJ2$
GETKEY:
GO$ = INKEY$
GO$ = LCASE$(GO$)
ex$ = ""
ON KEY(1) GOSUB KEYS2
ON KEY(2) GOSUB OPT12
ON KEY(3) GOSUB LOADFILE2
ON KEY(4) GOSUB NEW
ON KEY(5) GOSUB opt13
ON KEY(6) GOSUB done2
ON KEY(7) GOSUB PPRINT
IF GO$ = "" THEN GOTO GETKEY
IF SCR3 = 13 AND GO$ = "y" THEN GOTO GETCOLOR
IF GO$ = "8" THEN GOTO UP
IF GO$ = "6" THEN GOTO left
IF GO$ = "4" THEN GOTO right
IF GO$ = "2" THEN GOTO down
IF GO$ = "7" THEN GOTO ul
IF GO$ = "1" THEN GOTO dl
IF GO$ = "3" THEN GOTO dr
IF GO$ = "9" THEN GOTO ur
IF GO$ = "5" THEN GOTO gridnf
IF GO$ = "q" THEN GOTO ful
IF GO$ = "w" THEN GOTO fu
IF GO$ = "e" THEN GOTO fur
IF GO$ = "a" THEN GOTO fl
IF GO$ = "s" THEN GOTO gridnf
IF GO$ = "d" THEN GOTO fr
IF GO$ = "z" THEN GOTO fdl
IF GO$ = "x" THEN GOTO fd
IF GO$ = "c" THEN GOTO fdr
IF GO$ = "v" THEN GOTO addline
IF GO$ = "r" THEN GOTO addcircle
IF GO$ = "f" THEN GOTO FILL
IF GO$ = CHR$(27) THEN GOTO save
IF GO$ = "n" THEN GOTO SEDHNE
IF GO$ = "h" THEN GOTO HOME
IF GO$ = "b" THEN GOTO change
IF GO$ = "t" THEN GOTO box
IF GO$ = "+" THEN GOTO colup
IF GO$ = "-" THEN GOTO COLDOWN
IF GO$ = "g" THEN GOTO POINTMAKE
IF GO$ = "m" THEN GOTO GETPALETTE
GOTO GETKEY
KEYS2:
ex$ = "ON"
GOTO KEYS
opt13:
ex$ = "ON"
GOTO opt2
OPT12:
ex$ = "ON"
GOTO opt1
LOADFILE2:
ex$ = "ON"
GOTO loadfile
box:
IF LINP2 = 1 THEN GOTO seccor2
LOCN1 = CURSUR: LOCN2 = CURSUR2
LINP2 = 1
lin3$ = "on"
OBJ$ = "BOX"
GOTO GRID
seccor2:
OBJ$ = ""
lin3$ = ""
LOCN3 = CURSUR: LOCN4 = CURSUR2
BOX1(M, N, O) = LOCN1
BOX2(M, N, O) = LOCN2
BOX3(M, N, O) = LOCN3
BOX4(M, N, O) = LOCN4
BOX5(M, N, O) = COL
EMB = EMB + 1
LINP2 = 0
LOCN1 = 0: LOCN2 = 0: LOCN3 = 0: LOCN4 = 0: colb = 0
M = M + 1
IF M = 11 THEN N = N + 1
IF M = 11 THEN M = 0
IF N = 11 THEN O = O + 1
IF N = 11 THEN N = 0
IF O = 11 THEN PRINT "no more boxes"
GOTO GRID
UP:
CURSUR2 = CURSUR2 - 1
GOTO GRID
left:
CURSUR = CURSUR - 1
GOTO GRID
right:
CURSUR = CURSUR + 1
GOTO GRID
down:
CURSUR2 = CURSUR2 + 1
GOTO GRID
ul:
CURSUR2 = CURSUR2 - 1
CURSUR = CURSUR - 1
GOTO GRID
dl:
CURSUR2 = CURSUR2 + 1
CURSUR = CURSUR - 1
GOTO GRID
ur:
CURSUR2 = CURSUR2 - 1
CURSUR = CURSUR + 1
GOTO GRID
dr:
CURSUR2 = CURSUR2 + 1
CURSUR = CURSUR + 1
GOTO GRID
gridnf:
IF option$ = "Y" THEN GOTO off2
IF option$ = "y" THEN GOTO off2
IF option$ = "N" THEN GOTO on2
IF option$ = "n" THEN GOTO on2
on2:
option$ = "y"
IF option$ = "y" GOTO Gridsize
off2:
option$ = "n"
GOTO GRID
Gridsize:
INPUT "What size grid do you want(numbers 10-50)"; GRID
IF GRID > 50 THEN GRID = 50
IF GRID < 10 THEN GRID = 10
GOTO GRID
fu:
CURSUR2 = CURSUR2 - SPEED
GOTO GRID
fl:
CURSUR = CURSUR - SPEED
GOTO GRID
fr:
CURSUR = CURSUR + SPEED
GOTO GRID
fd:
CURSUR2 = CURSUR2 + SPEED
GOTO GRID
ful:
CURSUR2 = CURSUR2 - SPEED
CURSUR = CURSUR - SPEED
GOTO GRID
fdl:
CURSUR2 = CURSUR2 + SPEED
CURSUR = CURSUR - SPEED
GOTO GRID
fur:
CURSUR2 = CURSUR2 - SPEED
CURSUR = CURSUR + SPEED
GOTO GRID
fdr:
CURSUR2 = CURSUR2 + SPEED
CURSUR = CURSUR + SPEED
GOTO GRID
addcircle:
INPUT "radius"; RAD(A, B, C)
circle1(A, B, C) = CURSUR
circle2(A, B, C) = CURSUR2
COL2(A, B, C) = COL
emc = emc + 1
A = A + 1
IF A = 11 THEN B = B + 1
IF A = 11 THEN A = 0
IF B = 11 THEN C = C + 1
IF B = 11 THEN B = 0
IF C = 11 THEN PRINT "no more circles"
GOTO GRID
addline:
IF LINP1 = 1 THEN GOTO seccor
LOCATION1 = CURSUR: LOCATION2 = CURSUR2
LINP1 = 1
lin2$ = "on"
OBJ2$ = "LINE"
GOTO GRID
seccor:
OBJ2$ = ""
lin2$ = ""
location3 = CURSUR: location4 = CURSUR2
line1(D, E, F) = LOCATION1
line2(D, E, F) = LOCATION2
line3(D, E, F) = location3
line4(D, E, F) = location4
LINE5(D, E, F) = COL
LINP1 = 0
eml = eml + 1
LOCATION1 = 0: LOCATION2 = 0: location3 = 0: location4 = 0
D = D + 1
IF D = 11 THEN E = E + 1
IF D = 11 THEN D = 0
IF E = 11 THEN F = F + 1
IF E = 11 THEN E = 0
IF F = 11 THEN PRINT "no more lines"
GOTO GRID
FILL:
fill1(J, K, L) = CURSUR
fill2(J, K, L) = CURSUR2
FILL3(J, K, L) = COL
J = J + 1
emf = emf + 1
IF J = 11 THEN K = K + 1
IF J = 11 THEN J = 0
IF K = 11 THEN L = L + 1
IF K = 11 THEN K = 0
IF L = 11 THEN PRINT "can't fill any more"
GOTO GRID
POINTMAKE:
point1(p, q, R) = CURSUR
point2(p, q, R) = CURSUR2
point3(p, q, R) = COL
p = p + 1
emf = emf + 1
IF p = 11 THEN q = q + 1
IF p = 11 THEN p = 0
IF q = 11 THEN R = R + 1
IF q = 11 THEN q = 0
IF R = 11 THEN PRINT "can't MAKE ANY MORE POINTS"
GOTO GRID
save:
IF VIEW$ = "y" THEN CLS
IF VIEW$ = "Y" THEN CLS
CLS
PRINT "(S)ave, (E)rase a section of drawing,"
PRINT "(C)ompleatly start over,(V)iew, (Q)uit, "
PRINT "(K)ey list, (D)one with options or"
PRINT "(L)oad new drawing, (R)eset controls"
PRINT "(S/E/C/V/Q/K/D/L/R)"
savex:
optex$ = INKEY$
IF optex$ = "" GOTO savex
IF optex$ = "s" THEN GOTO opt1
IF optex$ = "S" THEN GOTO opt1
IF optex$ = "e" THEN GOTO opt2
IF optex$ = "E" THEN GOTO opt2
IF optex$ = "c" THEN GOTO opt3
IF optex$ = "C" THEN GOTO opt3
IF optex$ = "Q" THEN GOTO opt4
IF optex$ = "q" THEN GOTO opt4
IF optex$ = "l" THEN GOTO opt5
IF optex$ = "L" THEN GOTO opt5
IF optex$ = "V" THEN VIEW$ = "y"
IF optex$ = "v" THEN VIEW$ = "y"
IF optex$ = "v" THEN GOTO REDRAW
IF optex$ = "V" THEN GOTO REDRAW
IF optex$ = "D" THEN GOTO GRID
IF optex$ = "d" THEN GOTO GRID
IF optex$ = "k" THEN GOTO KEYS
IF optex$ = "K" THEN GOTO KEYS
IF optex$ = "r" THEN RES$ = "RESET"
IF optex$ = "r" THEN GOTO opt1
IF optex$ = "R" THEN RES$ = "RESET"
IF optex$ = "R" THEN GOTO opt1
GOTO save
opt1:
ERROR$ = "save"
SCREEN 0: WIDTH 80
CLS
IF RES$ = "RESET" THEN INPUT "do you wish to save (y/n)"; sayn$
IF sayn$ = "n" AND RES$ = "RESET" THEN GOTO RES
IF sayn$ = "N" AND RES$ = "RESET" THEN GOTO RES
savfile3:
SCREEN 0: WIDTH 80
savfile2:
IF scrsave$ = "on" THEN FILE$ = "current"
SCE = 0
FILE$ = ""
CALL TESTCHR(FILE$, SCE)
CLS
OPEN FILE$ FOR OUTPUT AS #1
A = 0: B = 0: C = 0: D = 0: E = 0: F = 0: J = 0: K = 0: L = 0: M = 0: N = 0: O = 0
PRINT "SAVING FILE NOW";
consavc:
IF circle1(A, B, C) = 0 THEN GOTO linesav
ZZ = circle1(A, B, C)
YY = circle2(A, B, C)
RR = RAD(A, B, C)
CC = COL2(A, B, C)
WRITE #1, ZZ, YY, RR, CC
PRINT ".";
A = A + 1
IF A = 11 THEN B = B + 1
IF A = 11 THEN A = 0
IF B = 11 THEN C = C + 1
IF B = 11 THEN B = 0
GOTO consavc:
linesav:
WRITE #1, 0, 0, 0, 0
PRINT ".";
consavl:
ZZ = line1(D, E, F)
IF ZZ = 0 THEN GOTO fillsav
YY = line2(D, E, F)
Z = line3(D, E, F)
Y = line4(D, E, F)
CC = LINE5(D, E, F)
WRITE #1, ZZ, YY, Z, Y, CC
PRINT ".";
D = D + 1
IF D = 11 THEN E = E + 1
IF D = 11 THEN D = 0
IF E = 11 THEN F = F + 1
IF E = 11 THEN E = 0
GOTO consavl
fillsav:
WRITE #1, 0, 0, 0, 0, 0
PRINT ".";
consavf:
ZZ = fill1(J, K, L)
IF ZZ = 0 THEN GOTO boxsav
YY = fill2(J, K, L)
CC = FILL3(J, K, L)
WRITE #1, ZZ, YY, CC
PRINT ".";
J = J + 1
IF J = 11 THEN K = K + 1
IF J = 11 THEN J = 0
IF K = 11 THEN L = L + 1
IF K = 11 THEN K = 0
GOTO consavf
boxsav:
WRITE #1, 0, 0, 0
PRINT ".";
consavb:
ZZ = BOX1(M, N, O)
IF ZZ = 0 THEN GOTO POINTsav
YY = BOX2(M, N, O)
Z = BOX3(M, N, O)
Y = BOX4(M, N, O)
CC = BOX5(M, N, O)
WRITE #1, ZZ, YY, Z, Y, CC
PRINT ".";
M = M + 1
IF M = 11 THEN N = N + 1
IF M = 11 THEN M = 0
IF N = 11 THEN O = O + 1
IF N = 11 THEN N = 0
GOTO consavb:
POINTsav:
WRITE #1, 0, 0, 0, 0, 0
PRINT ".";
consavP:
ZZ = point1(p, q, R)
IF ZZ = 0 THEN GOTO PALSAVE
YY = point2(p, q, R)
CC = point3(p, q, R)
WRITE #1, ZZ, YY, CC
PRINT ".";
p = p + 1
IF p = 11 THEN q = q + 1
IF p = 11 THEN p = 0
IF q = 11 THEN R = R + 1
IF q = 11 THEN q = 0
GOTO consavP
PALSAVE:
S = 0: T = 0: U = 0
WRITE #1, 0, 0, 0
PRINT ".";
CONSAVPAL:
ZZ = OLDCOL(S, T, U)
RED = NEWCOLR(S, T, U)
BLUE = NEWCOLB(S, T, U)
GREEN = NEWCOLG(S, T, U)
IF ZZ = 0 AND RED = 0 AND BLUE = 0 AND GREEN = 0 THEN GOTO DONESAV
WRITE #1, ZZ, RED, BLUE, GREEN
PRINT ".";
S = S + 1
IF S = 11 THEN T = T + 1: S = 0
IF T = 11 THEN U = T + 1: T = 0
GOTO CONSAVPAL
DONESAV:
WRITE #1, 0, 0, 0, 0
PRINT ".";
WRITE #1, SCR3
PRINT "."
CLOSE
PRINT "Done with save"
RES:
IF RES$ = "RESET" THEN RUN "SETUP.BAS"
viewd:
INPUT "view (y/n)"; VIEW$
IF VIEW$ = "y" OR VIEW$ = "Y" THEN GOTO REDRAW
GOTO saveopt
viewy:
IF VIEW$ = "PRINT" THEN
    'Calls routine for printing the screen to a local printer.
    DIM A%(2)
    DEF SEG = VARSEG(A%(0))
    FOR I% = 0 TO 2
       READ D%
       POKE VARPTR(A%(0)) + I%, D%
    NEXT I%
    DATA 205, 5, 203  : ' int 5  retf  'Machine-language code
                                       'for printing screen.
    CALL ABSOLUTE(VARPTR(A%(0)))
    DEF SEG
ELSE
viewX:
     GO$ = INKEY$
     IF GO$ = "" THEN GOTO viewX
END IF
saveopt:
SCREEN 0
VIEW$ = "n"
SCREEN SCR3
GOTO GRID
opt2:
INPUT "erase the last (l)ine, (c)ircle, (b)ox,(p)oint, or (f)illed in area"; ERASEW$
IF ERASEW$ = "l" THEN GOTO linegone
IF ERASEW$ = "c" THEN GOTO circlegone
IF ERASEW$ = "f" THEN GOTO fillgone
IF ERASEW$ = "L" THEN GOTO linegone
IF ERASEW$ = "C" THEN GOTO circlegone
IF ERASEW$ = "F" THEN GOTO fillgone
IF ERASEW$ = "A" THEN GOTO KEY2
IF ERASEW$ = "B" OR ERASEW$ = "b" THEN GOTO BOXGONE
IF erawew$ = "P" OR ERASEW$ = "p" THEN GOTO pointgone
GOTO GRID
linegone:
line1(D, E, F) = 0
line2(D, E, F) = 0
line3(D, E, F) = 0
line4(D, E, F) = 0
LINE5(D, E, F) = 0
D = D - 1
IF D < 0 THEN E = E - 1
IF D < 0 THEN D = 11
IF E < 0 THEN F = F - 1
IF E < 0 THEN E = 11
IF D = 0 AND E = 0 AND F = 0 THEN PRINT "no more subtracting lines"
IF D = -1 THEN D = 0
IF E = -1 THEN E = 0
IF F = -1 THEN F = 0
line1(D, E, F) = 0
line2(D, E, F) = 0
line3(D, E, F) = 0
line4(D, E, F) = 0
LINE5(D, E, F) = 0
GOTO GRID
circlegone:
A = A - 1
IF A < 0 THEN B = B - 1
IF A < 0 THEN A = 11
IF B < 0 THEN C = C - 1
IF B < 0 THEN B = 1
IF C < 0 AND A = 0 AND B = 0 THEN PRINT "no more subtracting circles"
IF C = -1 THEN C = 0
IF B = -1 THEN B = 0
IF A = -1 THEN A = 0
circle1(A, B, C) = 0
circle2(A, B, C) = 0
RAD(A, B, C) = 0
COL2(A, B, C) = 0
GOTO GRID
fillgone:
J = J - 1
IF J < 0 THEN K = K - 1
IF J < 0 THEN J = 11
IF K < 0 THEN L = L - 1
IF K < 0 THEN K = 1
IF J < 0 AND K = 0 AND L = 0 THEN PRINT "no more filled areas"
IF J < 0 THEN J = 0
IF K < 0 THEN K = 0
IF L < 0 THEN L = 0
fill1(J, K, L) = 0
fill2(J, K, L) = 0
FILL3(J, K, L) = 0
GOTO GRID
pointgone:
p = p - 1
IF p < 0 THEN q = q - 1
IF p < 0 THEN p = 11
IF q < 0 THEN R = R - 1
IF q < 0 THEN q = 1
IF p < 0 AND q = 0 AND R = 0 THEN PRINT "no more filled areas"
IF p < 0 THEN p = 0
IF q < 0 THEN q = 0
IF R < 0 THEN R = 0
point1(p, q, R) = 0
point2(p, q, R) = 0
point3(p, q, R) = 0
GOTO GRID
BOXGONE:
M = M - 1
IF M < 0 THEN N = N - 1
IF M < 0 THEN M = 11
IF N < 0 THEN O = O - 1
IF N < 0 THEN N = 11
IF M = 0 AND N = 0 AND O = 0 THEN PRINT "no more subtracting lines"
IF M = -1 THEN M = 0
IF N = -1 THEN N = 0
IF O = -1 THEN O = 0
BOX1(M, N, O) = 0
BOX2(M, N, O) = 0
BOX3(M, N, O) = 0
BOX4(M, N, O) = 0
BOX5(M, N, O) = 0
GOTO GRID
opt3:
INPUT "ARE YOU SURE(Y/N)"; DOG$
IF DOG$ = "Y" OR DOG$ = "Y" THEN RUN
GOTO GRID
opt4:
INPUT "ARE YOU SURE YOU WANT TO QUIT(Y/N)"; done$
IF done$ = "Y" THEN GOTO lastch
IF done$ = "y" THEN GOTO lastch
GOTO GETKEY
lastch:
INPUT "Would you like to save before you quit(y/n)"; SQ$
IF SQ$ = "Y" THEN GOTO opt1
IF SQ$ = "Y" THEN GOTO opt1
GOTO done
opt5:
INPUT "ARE YOU SURE(Y/N)"; opt5$
IF opt5$ = "Y" THEN GOTO loadfile
IF opt5$ <> "y" THEN GOTO REDRAW
GOTO loadfile
KEYS:
KEYS3$ = "LIST"
GOTO INSTR2
done2:
INPUT "Are you sure you want to quit(y/n)", yn$
IF yn$ = "y" OR yn$ = "Y" THEN GOTO done3
GOTO GRID
done3:
INPUT "Do you wish to save(y/n)", savy$
IF savy$ = "N" OR savy$ = "n" THEN GOTO done
scrsave$ = "on"
GOSUB savfile3
done:
KEY(0) OFF
END
SEDHNE:
INPUT "WHAT IS THE NEW SPEED OF FAST KEYS(NUMBERS 5-20)"; SPEED
IF SPEED > 20 THEN SPEED = 20
IF SPEED < 5 THEN SPEED = 5
GOTO GRID
HOME:
CURSUR = 30
CURSUR2 = 30
GOTO GRID
change:
IF cur$ = "line" THEN GOTO CCIR
IF cur$ = "cir" THEN GOTO CPLUS
IF cur$ = "PLUS" THEN GOTO CBOTH
IF cur$ = "both" THEN GOTO cline
CCIR:
cur$ = "cir"
GOTO GRID
CBOTH:
cur$ = "both"
GOTO GRID
cline:
cur$ = "line"
GOTO GRID
CPLUS:
cur$ = "PLUS"
GOTO GRID
colup:
COL = COL + 1
IF COL = HIGHCOL + 1 THEN COL = 0
GOTO GRID
COLDOWN:
COL = COL - 1
IF COL = -1 THEN COL = HIGHCOL
GOTO GRID
NEW:
INPUT "Are you sure you want to restart(y/n)", reyn$
IF reyn$ = "y" OR reyn$ = "Y" THEN RUN
ex$ = "ON"
GOTO GRID
SCR13:
SCR3 = 13: HIGHX = 320: HIGHY = 200: HIGHCOL = 255
RETURN
PPRINT:
SHELL "GRAPHICS.COM"
ex$ = "ON"
VIEW$ = "PRINT"
GOTO KEY2
SCR11:
SCR3 = 11: HIGHX = 640: HIGHY = 480: HIGHCOL = 1
RETURN
SCR1A2:
SCR3 = 12: HIGHX = 640: HIGHY = 480: HIGHCOL = 15
RETURN
ERRORTEST2:
SELECT CASE ERR
CASE 57
     '57      Device I/O error
     PRINT "TURN ON PRINTER"
     GOTO ENDOFSUB
CASE 71
     '71      Disk not ready
     PRINT "PLACE DISK IN DRIVE"
     GOTO ENDOFSUB
CASE 52
     '52      Bad file name or number
     IF ERROR$ <> "load" OR ERROR$ <> "save" THEN CALL TESTCHR(FILE$, SCE)
     IF ERROR$ = "load" GOTO loadfile
     IF ERROR$ = "save" GOTO opt1
     GOTO ENDOFSUB
CASE 62
     '62      Input past end of file
     PRINT "THIS FILE HAS AN INCORECT FORMAT"
     GOTO ENDOFSUB
CASE 75
     '75      Path/File access error
     IF ERROR$ <> "load" OR ERROR$ <> "save" THEN CALL TESTCHR(FILE$, SCE)
     IF ERROR$ = "load" GOTO loadfile
     IF ERROR$ = "save" GOTO opt1
     GOTO ENDOFSUB
CASE 64
     '64      Bad file name
     IF ERROR$ <> "load" OR ERROR$ <> "save" THEN CALL TESTCHR(FILE$, SCE)
     IF ERROR$ = "load" GOTO loadfile
     IF ERROR$ = "save" GOTO opt1
     GOTO ENDOFSUB
CASE 53
     '53      File not found
     IF ERROR3$ = "on" THEN GOTO ERRORENDLOAD
     IF ERROR$ <> "load" OR ERROR$ <> "save" THEN CALL TESTCHR(FILE$, SCE)
     IF ERROR$ = "load" GOTO loadfile
     IF ERROR$ = "save" GOTO opt1
     GOTO ENDOFSUB
CASE 76
     '76      Path not found
     IF ERROR$ <> "load" OR ERROR$ <> "save" THEN CALL TESTCHR(FILE$, SCE)
     IF ERROR$ = "load" GOTO loadfile
     IF ERROR$ = "save" GOTO opt1
     GOTO ENDOFSUB
CASE ELSE
     INPUT "What did you do before this error", ERRORWORD$
     ERROR2 = ERR
     GOTO EXTRAERROR
'
' Code    Message                    К  Code    Message
'                                    К
' 1       NEXT without FOR           К  37      Argument-count mismatch
' 2       Syntax error               К  38      Array not defined
' 3       RETURN without GOSUB       К  40      Variable required
' 4       Out of DATA                К  50      FIELD overflow
'                                    К  51      Internal error
' 6       Overflow                   К
' 7       Out of memory              К
' 8       Label not defined          К  54      Bad file mode
' 9       Subscript out of range     К  55      File already open
' 10      Duplicate definition       К  56      FIELD statement active
' 11      Division by zero           К
' 12      Illegal in direct mode     К  58      File already exists
' 13      Type mismatch              К  59      Bad record length
' 14      Out of string space        К  61      Disk full
' 16      String formula too complex К
' 17      Cannot continue            К  63      Bad record number
' 18      Function not defined       К
' 19      No RESUME                  К  67      Too many files
' 20      RESUME without error       К  68      Device unavailable
' 24      Device timeout             К  69      Communication-buffer overflow
' 25      Device fault               К  70      Permission denied
' 26      FOR without NEXT           К
' 27      Out of paper               К  72      Disk-media error
' 29      WHILE without WEND         К  73      Feature unavailable
' 30      WEND without WHILE         К  74      Rename across disks
' 33      Duplicate label            К
' 35      Subprogram not defined     К
EXTRAERROR:
     IF ERROR3$ = "on" THEN CLOSE #2: CLOSE #3
     X = 0: Y = 0: Z = 0
     ERROR3$ = "on"
     OPEN "error.fix" FOR INPUT AS #2
errorload:
errorloadf:
     INPUT #2, ZZ$, YY
     IF ZZ$ = "" AND YY = 0 THEN GOTO ERRORENDLOAD
     ERRORWORD2$(X, Y, Z) = ZZ$
     ERRORNUM(X, Y, Z) = YY
     X = X + 1
     IF X = 11 THEN Y = Y + 1
     IF X = 11 THEN X = 0
     IF Y = 11 THEN Z = Z + 1
     IF Y = 11 THEN Y = 0
     GOTO errorloadf
ERRORENDLOAD:
     CLOSE #2
     X = 0: Y = 0: Z = 0: ERROR3$ = ""
     OPEN "ERROR.FIX" FOR OUTPUT AS #3
ERRORSAVP:
     YY = ERRORNUM(X, Y, Z)
     ZZ$ = ERRORWORD2$(X, Y, Z)
     IF ZZ$ = "" AND YY = 0 THEN GOTO errordonesav
     WRITE #3, ZZ$, YY
     X = X + 1
     IF X = 11 THEN Y = Y + 1
     IF X = 11 THEN X = 0
     IF Y = 11 THEN Z = Z + 1
     IF Y = 11 THEN Y = 0
     GOTO ERRORSAVP
errordonesav:
     WRITE #3, ERRORWORD$, ERROR2
     WRITE #3, "", 0
     CLOSE #3
ENDOFSUB:
END SELECT
GOTO KEY2
GETCOLOR:
CLS
X = 12: Y = 0: COL = 1
SCREEN 13
REDRAW2:
G = 0: H = 0
IF Y = 108 AND X > 252 THEN X = 0: COL = COL - 22
IF X = 0 AND Y = 0 THEN COL = 0
IF Y = 108 AND X < 0 THEN X = 252: COL = COL + 22
IF X < 0 THEN X = 300: COL = COL + 26
IF X > 300 THEN X = 0: COL = COL - 26
IF Y < 0 THEN Y = 108: COL = COL + 260
IF Y > 108 THEN Y = 0: COL = COL - 260
FOR I = 0 TO 255
LINE (G, H)-(G + 10, H + 10), I, BF
LINE (X, Y)-(X + 10, Y + 10), 15, B
LINE (X + 1, Y + 1)-(X + 9, Y + 9), 0
LINE (X + 1, Y + 9)-(X + 9, Y + 1), 0
G = G + 12
IF G >= 12 * 26 THEN
     H = H + 12
     G = 0
END IF
NEXT I
LOCATE 18, 20: PRINT "W": LOCATE 19, 18: PRINT "A   D": LOCATE 20, 20: PRINT "X"
COLOR 3: LOCATE 22, 10: PRINT "Press Enter To Set Color"
GETKEY2:
GO$ = INKEY$
GO$ = UCASE$(GO$)
IF GO$ = "" THEN GOTO GETKEY2
IF GO$ = "W" THEN GOTO UP2
IF GO$ = "X" THEN GOTO DN
IF GO$ = "A" THEN GOTO LT
IF GO$ = "D" THEN GOTO RT
IF GO$ = CHR$(13) THEN GOTO COLOR2
GOTO GETKEY2
UP2:
Y = Y - 12
COL = COL - 26
GOTO REDRAW2
DN:
Y = Y + 12
COL = COL + 26
GOTO REDRAW2
LT:
X = X - 12
COL = COL - 1
GOTO REDRAW2
RT:
X = X + 12
COL = COL + 1
GOTO REDRAW2
COLOR2:
GOTO KEY2

GETPALETTE:
OLDCOL(S, T, U) = COL
CLS
R = 32: B = 32: G = 32
NEXTPAL:
LOCATE 23, 5: PRINT "PRESS ENTER TO SET COLOR"
IF R > 62 THEN R = 63
IF R < 0 THEN R = 0
IF B > 63 THEN B = 63
IF B < 0 THEN B = 0
IF G > 63 THEN G = 63
IF G < 0 THEN G = 0
LOCATE 2, 11: PRINT "RED   "; R: LOCATE 2, 21: PRINT "Q -W =E= R+ T"
     LINE (8, 8)-(8 + R, 16), 4, BF
     LINE (8 + R, 8)-(71, 16), 4, B
IF R < 61 THEN LINE (9 + R, 9)-(70, 15), 0, BF
LOCATE 4, 11: PRINT "BLUE  "; B: LOCATE 4, 21: PRINT "A -S =D= F+ G"
     LINE (8, 24)-(8 + B, 32), 1, BF
     LINE (8 + B, 24)-(71, 32), 1, B
IF B < 61 THEN LINE (9 + B, 25)-(70, 31), 0, BF
LOCATE 6, 11: PRINT "GREEN "; G: LOCATE 6, 21: PRINT "Z -X =C= V+ B"
     LINE (8, 40)-(8 + G, 48), 2, BF
     LINE (8 + G, 40)-(71, 48), 2, B
IF G < 61 THEN LINE (9 + G, 41)-(70, 47), 0, BF
LOCATE 8, 2: PRINT "INVERT  J | BLACK  1 -2 =3= 4+ 5  WHITE"
PALETTE COL, R + (B * 65536) + (G * 256)
LINE (8, 72)-(310, 118), , B
LOCATE 12, 14: COLOR COL: PRINT "лллллллллллллллллл"
LINE (9, 73)-(309, 117), COL, BF
PAL:
PAL$ = INKEY$
PAL$ = UCASE$(PAL$)
IF PAL$ = "" THEN GOTO PAL
IF PAL$ = "J" THEN R = 63 - R: B = 63 - B: G = 63 - G: GOTO NEXTPAL
IF PAL$ = "1" THEN R = 0: B = 0: G = 0: GOTO NEXTPAL
IF PAL$ = "2" THEN R = R - 1: B = B - 1: G = G - 1: GOTO NEXTPAL
IF PAL$ = "3" THEN R = 32: B = 32: G = 32: GOTO NEXTPAL
IF PAL$ = "4" THEN R = R + 1: B = B + 1: G = G + 1: GOTO NEXTPAL
IF PAL$ = "5" THEN R = 64: B = 64: G = 64: GOTO NEXTPAL
IF PAL$ = "Q" THEN R = 0: GOTO NEXTPAL
IF PAL$ = "W" THEN R = R - 1: GOTO NEXTPAL
IF PAL$ = "E" THEN R = 32: GOTO NEXTPAL
IF PAL$ = "R" THEN R = R + 1: GOTO NEXTPAL
IF PAL$ = "T" THEN R = 64: GOTO NEXTPAL
IF PAL$ = "A" THEN B = 0: GOTO NEXTPAL
IF PAL$ = "S" THEN B = B - 1: GOTO NEXTPAL
IF PAL$ = "D" THEN B = 32: GOTO NEXTPAL
IF PAL$ = "F" THEN B = B + 1: GOTO NEXTPAL
IF PAL$ = "G" THEN B = 64: GOTO NEXTPAL
IF PAL$ = "Z" THEN G = 0: GOTO NEXTPAL
IF PAL$ = "X" THEN G = G - 1: GOTO NEXTPAL
IF PAL$ = "C" THEN G = 32: GOTO NEXTPAL
IF PAL$ = "V" THEN G = G + 1: GOTO NEXTPAL
IF PAL$ = "B" THEN G = 64: GOTO NEXTPAL
IF PAL$ = CHR$(13) THEN GOTO DONEPAL
GOTO PAL
DONEPAL:
NEWCOLR(S, T, U) = R
NEWCOLB(S, T, U) = B
NEWCOLG(S, T, U) = G
S = S + 1
IF S = 11 THEN T = T + 1: S = 0
IF T = 11 THEN U = U + 1: T = 0
IF U = 11 THEN PRINT "NO MORE PALETTES CAN BE MADE"
GOTO KEY2

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
PRINT
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
LOCATE 1, 1: PRINT "file name ";
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

