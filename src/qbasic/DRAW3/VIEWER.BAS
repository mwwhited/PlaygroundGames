DEFINT A-Z
DECLARE SUB TESTCHR (FILE$, SCE)
loadfile:
CLEAR , , 9000
SCREEN TEXTMODE
WIDTH 80
FILE$ = "": SCE = 0
CALL TESTCHR(FILE$, SCE)
OPEN FILE$ FOR INPUT AS #1
a = 0: b = 0: C = 0: d = 0: E = 0: F = 0: J = 0: K = 0: L = 0: M = 0: N = 0: O = 0: p = 0: q = 0: r = 0
conloadc:
INPUT #1, ZZ, YY, rr, ccc$
cc = ASC(ccc$)
IF ZZ = 0 AND YY = 0 AND rr = 0 THEN GOTO lineload
circle1(a, b, C) = ZZ
circle2(a, b, C) = YY
RAD(a, b, C) = rr
COL2(a, b, C) = cc
a = a + 1
IF a = 11 THEN b = b + 1
IF a = 11 THEN a = 0
IF b = 11 THEN C = C + 1
IF b = 11 THEN b = 0
GOTO conloadc:
lineload:
conloadl:
INPUT #1, ZZ, YY, Z, Y, ccc$
cc = ASC(ccc$)
IF ZZ = 0 AND YY = 0 AND Z = 0 AND Y = 0 THEN GOTO fillload
line1(d, E, F) = ZZ
line2(d, E, F) = YY
line3(d, E, F) = Z
line4(d, E, F) = Y
LINE5(d, E, F) = cc
d = d + 1
IF d = 11 THEN E = E + 1
IF d = 11 THEN d = 0
IF E = 11 THEN F = F + 1
IF E = 11 THEN E = 0
GOTO conloadl
fillload:
conloadf:
INPUT #1, ZZ, YY, ccc$
cc = ASC(ccc$)
IF ZZ = 0 AND YY = 0 THEN GOTO BOXload
fill1(J, K, L) = ZZ
fill2(J, K, L) = YY
FILL3(J, K, L) = cc
J = J + 1
IF J = 11 THEN K = K + 1
IF J = 11 THEN J = 0
IF K = 11 THEN L = L + 1
IF K = 11 THEN K = 0
GOTO conloadf
BOXload:
conloadb:
INPUT #1, ZZ, YY, Z, Y, ccc$
cc = ASC(ccc$)
IF ZZ = 0 AND YY = 0 AND Z = 0 AND Y = 0 THEN GOTO PSETload
BOX1(M, N, O) = ZZ
BOX2(M, N, O) = YY
BOX3(M, N, O) = Z
BOX4(M, N, O) = Y
BOX5(M, N, O) = cc
M = M + 1
IF M = 11 THEN N = N + 1
IF M = 11 THEN M = 0
IF N = 11 THEN O = O + 1
IF N = 11 THEN N = 0
GOTO conloadb
PSETload:
conloaDP:
INPUT #1, ZZ, YY, ccc$
cc = ASC(ccc$)
IF ZZ = 0 AND YY = 0 THEN GOTO DONEload
point1(p, q, r) = ZZ
point2(p, q, r) = YY
point3(p, q, r) = cc
p = p + 1
IF p = 11 THEN q = q + 1
IF p = 11 THEN p = 0
IF q = 11 THEN r = r + 1
IF q = 11 THEN q = 0
GOTO conloaDP
DONEload:
INPUT #1, SCR3
CLOSE
redraW:
SCREEN SCR3
a = 0: b = 0: C = 0
checkc:
IF circle1(a, b, C) = 0 THEN GOTO DBOX
GOTO configc
printc:
CIRCLE (ZZ, YY), rr, cc
nextlocatec:
a = a + 1
IF a = 11 THEN b = b + 1
IF a = 11 THEN a = 0
IF b = 11 THEN C = C + 1
IF b = 11 THEN b = 0
GOTO checkc
configc:
ZZ = circle1(a, b, C)
YY = circle2(a, b, C)
rr = RAD(a, b, C)
cc = COL2(a, b, C)
GOTO printc
DBOX:
O = 0: N = 0: M = 0
CHECKDB:
IF BOX1(M, N, O) = 0 THEN GOTO LINES
GOTO CONFIGB
PRINTB:
LINE (YY, ZZ)-(YY, Z), cc
LINE (YY, ZZ)-(Y, ZZ), cc
LINE (Y, Z)-(Y, ZZ), cc
LINE (Y, Z)-(YY, Z), cc
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
cc = BOX5(M, N, O)
GOTO PRINTB
LINES:
d = 0: E = 0: F = 0
checkf:
IF line1(d, E, F) = 0 THEN GOTO dPOINT
GOTO configf
printf:
LINE (ZZ, YY)-(Z, Y), cc
nextlocatef:
d = d + 1
IF d = 11 THEN E = E + 1
IF d = 11 THEN d = 0
IF E = 11 THEN F = F + 1
IF E = 11 THEN E = 0
GOTO checkf
configf:
ZZ = line1(d, E, F)
YY = line2(d, E, F)
Z = line3(d, E, F)
Y = line4(d, E, F)
cc = LINE5(d, E, F)
GOTO printf
dPOINT:
p = 0: q = 0: r = 0
CHECKP:
IF point1(p, q, r) = 0 THEN GOTO dFILL
GOTO configP
printP:
PSET (ZZ, YY), cc
nextlocated:
p = p + 1
IF p = 11 THEN q = q + 1
IF p = 11 THEN p = 0
IF q = 11 THEN r = r + 1
IF q = 11 THEN q = 0
GOTO CHECKP
configP:
ZZ = point1(p, q, r)
YY = point2(p, q, r)
cc = point3(p, q, r)
GOTO printP
dFILL:
J = 0: K = 0: L = 0
checkd:
IF fill1(J, K, L) = 0 THEN GOTO ADDTO3
GOTO configd
printd:
PAINT (ZZ, YY), cc
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
cc = FILL3(J, K, L)
GOTO printd
ADDTO3:

SUB TESTCHR (FILE$, SCE) STATIC

WHAT:
a = 0: b = 0
what2:
IF file2$(a, b) <> "" THEN file2$(a, b) = ""
a = a + 1
IF a = 11 THEN b = b + 1
IF a = 11 THEN a = 0
IF file2$(a, b) = "" THEN GOTO addletter2
GOTO what2
addletter2:
CLS
a = 0: b = 0
PRINT "file name";
addletter:
PRINT file2$(a, b);
a = a + 1
IF a = 11 THEN b = b + 1
IF a = 11 THEN a = 0
IF file2$(a, b) = "" OR file2$(a, b) = " " THEN GOTO nextletter3
IF a <= 0 AND b <= 0 THEN a = 0 AND b = 0: GOTO nextletter
GOTO addletter
nextletter3:
PRINT "            "
nextletter:
file2$(a, b) = INKEY$
IF file2$(a, b) = "" THEN GOTO nextletter
IF file2$(a, b) = CHR$(13) THEN file2$(a, b) = " ": GOTO NEXTTEST
IF file2$(a, b) = " " THEN PRINT CHR$(7)
IF file2$(a, b) = "" THEN GOTO DOWN2
GOTO addletter2
NEXTTEST:
a = 0: b = 0
NEXTTEST3:
IF file2$(a, b) = " " THEN GOTO NEXTTEST2
FILE$ = FILE$ + file2$(a, b)
a = a + 1
IF a = 11 THEN b = b + 1
IF a = 11 THEN a = 0
GOTO NEXTTEST3
DOWN2:
IF file2$(a, b) = "" THEN file2$(a, b) = ""
a = a - 1
IF a = -1 THEN b = b - 1
IF a = -1 THEN a = 10
IF b = -1 THEN b = 0
file2$(a, b) = ""
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

