DEFINT A-Z
DECLARE SUB LOGS ()
DECLARE SUB cargo ()
DECLARE SUB savesub ()
DECLARE SUB padd ()
DECLARE SUB TESTCHR (file$, SCE)
DECLARE SUB INTRO ()
DECLARE SUB newgame ()
DECLARE SUB new ()
DECLARE SUB load ()
DECLARE SUB mainbridge ()
DECLARE SUB sickbay ()
DECLARE SUB transporter ()
DECLARE SUB mainengine ()
DECLARE SUB shuttlebay ()
DECLARE SUB holodeck ()
DECLARE SUB weapons ()
DECLARE SUB STARCART ()
DECLARE SUB FORULAS ()
DECLARE SUB turbolift ()
COMMON SHARED skill, rank$, SHIP$, sex$, AGE$, mNAME$, lNAME$, namef$, SHIPNUM$
COMMON SHARED PHASER, CPHASER, PHTOR, CPHTOR, PROBES, CPROBES, CSHUTTLE, SHUTTLE, CDILUM, DILUM
COMMON SHARED CPHASER2, PHASER2, CPHTORQ, PHTORQ, load2$, ful$, WARP, IMPULSE, THRS, STARDATE$
DIM SHARED log$(0 TO 10, 0 TO 10, 0 TO 10), STARDATE$(0 TO 10, 0 TO 10, 0 TO 10)
CALL INTRO
CALL mainbridge

SUB cargo
cargo:
CLS
COLOR 6
SCREEN 0
WIDTH 80, 25
PRINT "CARGO BAY"
PRINT
PRINT " STORED"
PRINT "  PHASER X1       "; CPHASER
PRINT "  PHASER X1a      "; CPHASER2
PRINT "  QUANTUM TORPEDO "; CPHTORQ
PRINT "  PHOTON TORPEDOS "; CPHTOR
PRINT "  PROBES          "; CPROBES
PRINT "  SHUTTLE         "; CSHUTTLE
PRINT "  DILITHUM        "; CDILUM
PRINT
PRINT " IN USE            MAX    AMOUNT"
PRINT "  PHASER X1        1000  "; PHASER
PRINT "  PHASER X1a       100   "; PHASER2
PRINT "  QUANTUM TORPEDO  100   "; PHTORQ
PRINT "  PHOTON TORPEDOS  1000  "; PHTOR
PRINT "  PROBES           400   "; PROBES
PRINT "  SHUTTLE          40    "; SHUTTLE
PRINT "  DILITHUM         4000  "; DILUM
PRINT
PRINT "1) MOVE ITEMS FROM STORAGE TO USE"
PRINT "2) TURBO LIFT"
cargo2:
CAR$ = INKEY$
IF CAR$ < "1" OR CAR$ > "2" GOTO cargo2
IF CAR$ = "1" THEN GOTO CARGOT
IF CAR$ = "2" THEN GOTO CARGOEND
GOTO cargo
CARGOT:
LOCATE 24, 1
PRINT
PRINT "Number of item to move"
PRINT "1) PHASER X1"
PRINT "2) PHASER X1a"
PRINT "3) QUANTUM TORPEDO"
PRINT "4) PHOTON TORPEDOS"
PRINT "5) PROBES"
PRINT "6) SHUTTLE"
PRINT "7) DILITHUM"
PRINT "8) DONE"
cargot2:
cargot2$ = INKEY$
IF cargot2$ < "1" OR cargot2$ > "8" THEN GOTO cargot2
IF cargot2$ = "1" THEN
     IF CPHASER = 0 THEN GOTO CARGOT
     CPHMAX = 1000 - PHASER
     CPHASER = CPHASER - CPHMAX
ELSEIF cargot2$ = "2" THEN
     IF CPHASER2 = 0 THEN GOTO CARGOT
     CPHMAX = 100 - PHASER2
     CPHASER2 = CPHASER2 - CPHMAX
ELSEIF cargot2$ = "3" THEN
     IF CPHTORQ = 0 THEN GOTO CARGOT
     CPHMAX = 100 - PHTORQ
     CPHTORQ = CPHTORQ - CPHMAX
ELSEIF cargot2$ = "4" THEN
     IF CPHTOR = 0 THEN GOTO CARGOT
     CPHMAX = 1000 - PHTOR
     CPHTOR = CPHTOR - CPHMAX
ELSEIF cargot2$ = "5" THEN
     IF CPROBES = 0 THEN GOTO CARGOT
     CPHMAX = 400 - PROBES
     CPROBES = CPROBES - CPHMAX
ELSEIF cargot2$ = "6" THEN
     IF CSHUTTLE = 0 THEN GOTO CARGOT
     CPHMAX = 40 - SHUTTLE
     CSHUTTLE = CSHUTTLE - CPHMAX
ELSEIF cargot2$ = "7" THEN
     IF CDILUM = 0 THEN GOTO CARGOT
     CPHMAX = 4000 - DILUM
     CDILUM = CDILUM - CPHMAX
ELSEIF cargot2$ = "8" THEN
     GOTO cargo
END IF
GOTO CARGOT
CARGOEND:
END SUB

SUB EQUIPMENT
'IF [PART] > [PART MAX] THEN [PARTLEFT=PART MAX-PART] CARGO (PART, PARTLEFT)
IF DILUM > 4000 THEN CDILUM = (CDILUM + 4000) - DILUM
IF SHUTTLE > 40 THEN CSHUTTLE = (CSHUTTLE + 40) - SHUTTLE
IF PROBES > 400 THEN CPROBES = (CPROBES + 400) - PROBES
IF PHTORQ > 100 THEN CPHTORQ = (CPHTORQ + 100) - PHTORQ
IF PHTOR > 1000 THEN CPHTOR = (CPHTOR + 1000) - PHTOR
IF PHASER2 > 100 THEN CPHASER2 = (CPHASER2 + 100) - PHASER2
IF PHASER > 1000 THEN CPHASER = (CPHASER + 1000) - PHASER
END SUB

SUB FORULAS
IF LCASE$(ful$) = "warp" THEN
WARPSPEED:
     IF WARP < 1 THEN IMPULSE = 5
     IF WARP < 1 THEN GOTO IMPULSE
     IF WARP > 9.2 THEN WARP = 9.2
     SPEED = DISTANCE \ (WARP * 10)
GOTO SETSPEED
ELSEIF LCASE$(ful$) = "impulse" THEN
IMPULSE:
     IF IMPULSE > 5 THEN WARP = 1
     IF IMPULSE > 5 THEN GOTO WARPSPEED
     IF IMPULSE < .5 THEN THRS = 10
     IF IMPULSE < .5 THEN GOTO THUSTERS
     SPEED = DISTANCE \ (IMPULSE * 2)
GOTO SETSPEED
ELSEIF LCASE$(ful$) = "thr" THEN
THUSTERS:
     IF THUSTERS > 10 THEN IMPULSE = .5
     IF THUSTERS > 10 THEN GOTO IMPULSE
     IF THUSTERS < .5 THEN THUSTERS = .5
     SPEED = DISTANCE \ THUSTERS
GOTO SETSPEED
END IF
SETSPEED:
FOR DELAY% = 0 TO SPEED: NEXT DELAY%
END SUB

SUB holodeck
load2$ = "HOLO"
holodeck: 'ship viewer
SCREEN 0: WIDTH 80
COLOR 5
PRINT "What class of ship do you want to look at"
COLOR 6: PRINT "FEDERATION": PRINT "1) Excelsior": PRINT "2) Ambassador": PRINT "3) Galaxy"
COLOR 7: PRINT "OTHER": PRINT "4) Borg": PRINT "5) Ferengi Mardauder"
COLOR 8: PRINT "KLINGON": PRINT "6) Battle Cruiser": PRINT "7) Bird of Prey": PRINT "8) Vor'cha"
COLOR 9: PRINT "ROMULAN": PRINT "9) Warbird": PRINT "A) Scout"
ship2:
ship2$ = INKEY$
IF ship2$ = "" THEN GOTO ship2
GOTO ship2
loadfile:
DIM circle1(a, b, c), circle2(a, b, c), RAD(a, b, c), COL2(a, b, c)
DIM line1(d, E, F), line2(d, E, F), line3(d, E, F), line4(d, E, F), LINE5(d, E, F)
DIM fill1(J, K, L), fill2(J, K, L), FILL3(J, K, L)
DIM BOX1(M, n, O), BOX2(M, n, O), BOX3(M, n, O), BOX4(M, n, O), BOX5(M, n, O)
DIM point1(p, q, r), point2(p, q, r), point3(p, q, r)
CLEAR , , 9000
SCREEN TEXTMODE
WIDTH 80
file$ = "": SCE = 0
OPEN file$ FOR INPUT AS #1
a = 0: b = 0: c = 0: d = 0: E = 0: F = 0: J = 0: K = 0: L = 0: M = 0: n = 0: O = 0: p = 0: q = 0: r = 0
conloadc:
INPUT #1, ZZ, YY, rr, cc
IF ZZ = 0 AND YY = 0 AND rr = 0 THEN GOTO lineload
circle1(a, b, c) = ZZ
circle2(a, b, c) = YY
RAD(a, b, c) = rr
COL2(a, b, c) = cc
a = a + 1
IF a = 11 THEN b = b + 1
IF a = 11 THEN a = 0
IF b = 11 THEN c = c + 1
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
BOX1(M, n, O) = ZZ
BOX2(M, n, O) = YY
BOX3(M, n, O) = Z
BOX4(M, n, O) = Y
BOX5(M, n, O) = cc
M = M + 1
IF M = 11 THEN n = n + 1
IF M = 11 THEN M = 0
IF n = 11 THEN O = O + 1
IF n = 11 THEN n = 0
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
a = 0: b = 0: c = 0
checkc:
IF circle1(a, b, c) = 0 THEN GOTO DBOX
GOTO configc
printc:
CIRCLE (ZZ, YY), rr, cc
nextlocatec:
a = a + 1
IF a = 11 THEN b = b + 1
IF a = 11 THEN a = 0
IF b = 11 THEN c = c + 1
IF b = 11 THEN b = 0
GOTO checkc
configc:
ZZ = circle1(a, b, c)
YY = circle2(a, b, c)
rr = RAD(a, b, c)
cc = COL2(a, b, c)
GOTO printc
DBOX:
O = 0: n = 0: M = 0
CHECKDB:
IF BOX1(M, n, O) = 0 THEN GOTO LINES
GOTO CONFIGB
PRINTB:
LINE (YY, ZZ)-(Y, Z), cc, B
nextlocateB:
M = M + 1
IF M = 11 THEN n = n + 1
IF M = 11 THEN M = 0
IF n = 11 THEN O = O + 1
IF n = 11 THEN n = 0
GOTO CHECKDB
CONFIGB:
YY = BOX1(M, n, O)
ZZ = BOX2(M, n, O)
Y = BOX3(M, n, O)
Z = BOX4(M, n, O)
cc = BOX5(M, n, O)
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

END SUB

SUB INTRO
CLS
SCREEN 13
START:
COLOR 42
LOCATE 10, 10: PRINT "STAR TREK ver. 1.00"
COLOR 40
LOCATE 12, 15: PRINT "BY: MATTHEW WHITED"
COLOR 50
LOCATE 23, 10: PRINT "HIT ANY KEY TO CONTINUE"
DUH$ = INKEY$
IF DUH$ <> "" THEN GOTO FINISH
RANDOMIZE TIMER
X% = INT(RND * 320) + 1
DUH$ = INKEY$
IF DUH$ <> "" THEN GOTO FINISH
Y% = INT(RND * 200) + 1
DUH$ = INKEY$
IF DUH$ <> "" THEN GOTO FINISH
C2% = C2% + 1
IF C2% = 7 THEN C2% = 1
FOR c% = 30 TO 45
I = I + 1
IF I = 16 THEN I = 1
IF C2% = 1 THEN PALETTE I, c% * 256
IF C2% = 2 THEN PALETTE I, c% + c% * 256
IF C2% = 3 THEN PALETTE I, c%
IF C2% = 4 THEN PALETTE I, c% * 65536 + c%
IF C2% = 5 THEN PALETTE I, c% * 65536
IF C2% = 6 THEN PALETTE I, c% * 256 + c% * 65536
DUH$ = INKEY$
IF DUH$ <> "" THEN GOTO FINISH
CIRCLE (X%, Y%), c% - 30, I, , , 2
CIRCLE (X%, Y%), c% - 30, I, , , .42
NEXT
GOTO START
FINISH:
CALL newgame
END SUB

SUB load
load2$ = "OLD"
CALL TESTCHR(file$, SCE)
IF file$ = "" THEN GOTO endofsub
IF file$ = "" THEN GOTO endofsub
OPEN file$ FOR INPUT AS #1
INPUT #1, mNAME$, lNAME$, namef$
INPUT #1, AGE$, sex$
INPUT #1, rank$, skill
INPUT #1, SHIP$, SHIPNUM$
INPUT #1, PHASER, CPHASER, PHTOR, CPHTOR, PROBES, CPROBES, CSHUTTLE, SHUTTLE, CDILUM, DILUM
INPUT #1, CPHASER2, PHASER2, CPHTORQ, PHTORQ, STARDATE
a = 0: b = 0: c = 0
LOGLOAD:
INPUT #1, log$, sd$
IF log$ = "" AND sd = 0 THEN GOTO endofsub
log$(a, b, c) = log$
STARDATE$(a, b, c) = sd$
a = a + 1
IF a = 11 THEN b = b + 1
IF a = 11 THEN a = 0
IF b = 11 THEN c = c + 1
IF b = 11 THEN b = 0
GOTO LOGLOAD:
endofsub:
CLOSE #1
END SUB

SUB LOGS
CLS
PRINT "TYPE LOG NOW FOR "; STARDATE$
a = 0: b = 0: c = 0
STARDATE$(a, b, c) = STARDATE$
INPUT "", log$(a, b, c)
a = a + 1
IF a = 11 THEN b = b + 1: a = 0
IF b = 11 THEN c = c + 1: b = 0
END SUB

SUB mainbridge
mainbridge:
CLS
mainbridge2: 'Main Bridge Deck 1
SCREEN 0: WIDTH 80, 25: COLOR 3
LOCATE 1, 1: STARDATE$ = "(" + DATE$ + ", " + TIME$ + ")": PRINT STARDATE$
PRINT "                                                                        "
PRINT "1) OPS"
PRINT "2) CONN"
PRINT "3) Captains Chair"
PRINT "4) Tactical"
PRINT "5) Science Stations"
PRINT "6) Mission OPS"
PRINT "7) Environment"
PRINT "8) Engineering"
PRINT "9) Captains Ready Room(LOGS)"
PRINT "A) Observation Lounge(SAVE)"
PRINT "B) Crew Head"
PRINT "C) Tubro-lift (exit main bridge)"
PRINT "D) Emg. Turbo-lift (to battle bridge)"
MAINB:
MAINB$ = INKEY$
MAINB$ = LCASE$(MAINB$)
IF MAINB$ = "" THEN GOTO MAINB
IF MAINB$ = "c" THEN CALL turbolift: GOTO mainbridge
IF MAINB$ = "a" THEN CALL savesub: GOTO mainbridge
IF MAINB$ = "9" THEN CALL LOGS: GOTO mainbridge
GOTO mainbridge2
ops: 'Operations Management
'full control of ship if need be


END SUB

SUB mainengine

mainengin: 'Main Engineering
PRINT "1) Warp systems"
PRINT "2) Warp System Display"
PRINT "3) Impulse System Display"
PRINT "4) Master System Display"
PRINT "5) Duty Officer Display Station"

END SUB

SUB new
CLS
LOCATE 1, 1
COLOR 9: PRINT "FIRST NAME "; : COLOR 11: INPUT "", namef$
COLOR 9: PRINT "MIDDLE NAME "; : COLOR 11: INPUT "", mNAME$
COLOR 9: PRINT "LAST NAME "; : COLOR 11: INPUT "", lNAME$
COLOR 9: PRINT "AGE "; : COLOR 11: INPUT "", AGE$
COLOR 9
PRINT "SEX(M/F) "
sex:
sex$ = INKEY$
sex$ = UCASE$(sex$)
IF sex$ = "" GOTO sex
IF sex$ = "M" THEN GOTO sexnext
IF sex$ = "F" THEN GOTO sexnext
GOTO sex
sexnext:
LOCATE 5, 10: COLOR 11: PRINT sex$: COLOR 9
PRINT
PRINT "RANK(SKILL)"
PRINT "1) COMMANDER"
PRINT "2) LUTENIT"
PRINT "3) LUT.COMMANDER"
PRINT "4) CAPTIAN"
PRINT "5) ADMRAL"
PRINT "(1-5)"
NUM2:
skill$ = INKEY$
IF skill$ < "1" OR skill$ > "5" GOTO NUM2
IF skill$ = "1" THEN rank$ = "COM": skill = 1
IF skill$ = "2" THEN rank$ = "LUT": skill = 2
IF skill$ = "3" THEN rank$ = "LUT. COM": skill = 3
IF skill$ = "4" THEN rank$ = "CAPT": skill = 4
IF skill$ = "5" THEN rank$ = "AMD": skill = 5
LOCATE 13, 7: COLOR 11: PRINT skill$: COLOR 9
COLOR 9: PRINT "WHAT DO YOU WANT TO NAME YOUR SHIP "; : COLOR 11: INPUT "", SHIP$
COLOR 9: PRINT "SHIP ID NUMBER"; : COLOR 11: PRINT "  NCC "; : INPUT "", SHIPNUM$
SHIPNUM$ = "NCC " + SHIPNUM$
PHASER = 1000: PHTOR = 1000: PROBES = 400: SHUTTLE = 40: DILUM = 4000: PHASER2 = 50: PHTORQ = 50
END SUB

SUB newgame
SCREEN 0: WIDTH 80
CLS
COLOR 2: PRINT "     WELCOME TO STAR TREK ver. 1.00"
PRINT
COLOR 3
PRINT "  1) Start New Game"
PRINT
PRINT "  2) Load Old Game"
PRINT
PRINT "  3) Quit"
num:
num$ = INKEY$
IF num$ > "3" OR num$ < "1" THEN GOTO num
IF num$ = "2" THEN CALL load
IF num$ = "1" THEN CALL new
IF num$ = "3" THEN END
END SUB

SUB padd
CLS
SCREEN 0: WIDTH 80
COLOR 11
PRINT "RANK "; rank$; " NAME "; lNAME$; ", "; namef$; ", "; mNAME$
PRINT "AGE "; AGE$; " SEX "; sex$
PRINT "SHIP "; SHIP$; " "; SHIPNUM$; ""
END SUB

SUB savesub
load2$ = "OLD"
CALL TESTCHR(file$, SCE)
OPEN file$ FOR OUTPUT AS #1
WRITE #1, mNAME$, lNAME$, namef$
WRITE #1, AGE$, sex$
WRITE #1, rank$, skill
WRITE #1, SHIP$, SHIPNUM$
WRITE #1, PHASER, CPHASER, PHTOR, CPHTOR, PROBES, CPROBES, CSHUTTLE, SHUTTLE, CDILUM, DILUM
WRITE #1, CPHASER2, PHASER2, CPHTORQ, PHTORQ, STARDATE
a = 0: b = 0: c = 0
LOGsave:
IF log$(a, b, c) = "" AND sd = 0 THEN GOTO ENDOFSUB2
log$ = log$(a, b, c)
sd$ = STARDATE$(a, b, c)
WRITE #1, log$, sd$
a = a + 1
IF a = 11 THEN b = b + 1
IF a = 11 THEN a = 0
IF b = 11 THEN c = c + 1
IF b = 11 THEN b = 0
GOTO LOGsave:
ENDOFSUB2:
WRITE #1, "", 0
CLOSE #1
END SUB

SUB shuttlebay

shuttlebay: 'shuttle bays 1,2,3
IF bay$ = "1" THEN  'Shuttle bay 1 deck 4
     PRINT "1) EVAC from Enterprize"
     PRINT "2) EVAC to Enterprize"
     PRINT "3) Shuttle to planet"
     PRINT "4) Launch Workbees, and pods"
     PRINT "5) Turbolift (exit shuttle bay 1)"
     INPUT "What would like to do. ", Bay1$
ELSEIF bay$ = "2" THEN  'Shuttle bay 2 deck 13
     PRINT "1) EVAC from Enterprize"
     PRINT "2) EVAC to Enterprize"
     PRINT "3) Shuttle to planet"
     PRINT "4) Convert Shuttle Bay to a Med-Bay"
     PRINT "5) Turbo-lift (exit shuttle bay 2)"
     INPUT "What would you like to do. ", bay2$
ELSEIF bay$ = "3" THEN  'Shuttle bay 3 deck 13
     PRINT "1) EVAC from Enterprize"
     PRINT "2) Change Life support settings"
     PRINT "3) Shuttle to planet"
     PRINT "4) Launch Workbees, and pods"
     PRINT "5) Turbolift (exit shuttle bay 3)"
     INPUT "What would like to do. ", Bay3$
END IF
END SUB

SUB sickbay

sickbay: 'Med Department deck 12

END SUB

SUB STARCART

STARCART:  'STELLER MAPS

END SUB

SUB TESTCHR (file$, SCE) STATIC
CLS
WHAT:
a = 0: b = 0
SHELL "DIR/W"
what2:
IF file2$(a, b) <> "" THEN file2$(a, b) = ""
a = a + 1
IF a = 11 THEN b = b + 1
IF a = 11 THEN a = 0
IF file2$(a, b) = "" THEN GOTO addletter2
GOTO what2
addletter2:
a = 0: b = 0
LOCATE 1, 1: PRINT "                             "
LOCATE 1, 1: PRINT "File name ";
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
IF file2$(a, b) = " " THEN PRINT CHR$(7): GOTO nextletter
IF file2$(a, b) = CHR$(27) THEN file$ = "": GOTO whatever
IF file2$(a, b) = "" THEN GOTO DOWN2
IF file2$(a, b) = CHR$(9) THEN CLS : SHELL: GOTO nextletter
GOTO addletter2
NEXTTEST:
a = 0: b = 0
NEXTTEST3:
IF file2$(a, b) = " " THEN GOTO NEXTTEST2
file$ = file$ + file2$(a, b)
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
IF INSTR(1, file$, ".") = 0 THEN GOTO HUHUH
TESTNEXT:
IF INSTR(1, file$, ",") <> 0 THEN SCE = SCE + 1
IF INSTR(1, file$, "*") <> 0 THEN SCE = SCE + 1
IF INSTR(1, file$, "-") <> 0 THEN SCE = SCE + 1
IF INSTR(1, file$, "+") <> 0 THEN SCE = SCE + 1
IF INSTR(1, file$, "?") <> 0 THEN SCE = SCE + 1
IF INSTR(1, file$, ">") <> 0 THEN SCE = SCE + 1
IF INSTR(1, file$, "<") <> 0 THEN SCE = SCE + 1
IF INSTR(1, file$, "|") <> 0 THEN SCE = SCE + 1
IF INSTR(1, file$, "&") <> 0 THEN SCE = SCE + 1
IF INSTR(1, file$, "%") <> 0 THEN SCE = SCE + 1
IF INSTR(1, file$, "@") <> 0 THEN SCE = SCE + 1
IF INSTR(1, file$, "#") <> 0 THEN SCE = SCE + 1
IF INSTR(1, file$, "$") <> 0 THEN SCE = SCE + 1
IF INSTR(1, file$, "^") <> 0 THEN SCE = SCE + 1
IF INSTR(1, file$, "(") <> 0 THEN SCE = SCE + 1
IF INSTR(1, file$, "=") <> 0 THEN SCE = SCE + 1
IF INSTR(1, file$, ")") <> 0 THEN SCE = SCE + 1
IF INSTR(1, file$, "'") <> 0 THEN SCE = SCE + 1
IF INSTR(1, file$, "{") <> 0 THEN SCE = SCE + 1
IF INSTR(1, file$, "}") <> 0 THEN SCE = SCE + 1
IF INSTR(1, file$, "[") <> 0 THEN SCE = SCE + 1
IF INSTR(1, file$, "]") <> 0 THEN SCE = SCE + 1
IF SCE <> 0 THEN GOTO WHAT
GOTO whatever
HUHUH:
IF load2$ = "OLD" THEN file$ = file$ + ".GAM"
IF load2$ = "HOLO" THEN file$ = file$ + ".PIC"
IF SCE = 0 THEN GOTO TESTNEXT
whatever:
load2$ = ""
END SUB

SUB transporter

transporter: '
     'rooms 4 deck 6
     'rooms 2 deck 14

END SUB

SUB turbolift
turbo2:
SCREEN 0: WIDTH 80, 25: COLOR 5
CLS
PRINT "1) MAIN BRIDGE"
PRINT "2) SICKBAY"
PRINT "3) TRANSPORTER"
PRINT "5) MAIN ENGINEERING"
PRINT "6) SHUTTLE BAY 1"
PRINT "7) SHUTTLE BAY 2"
PRINT "8) SHUTTLE BAY 3"
PRINT "9) HOLODECK"
PRINT "A) TEN FORWARD"
PRINT "B) STELLAR CARTEOGHAPHY"
PRINT "C) CARGO BAY"
PRINT "D) Quit Game"
turbo:
turbo$ = INKEY$
turbo$ = LCASE$(turbo$)
IF turbo$ = "" THEN GOTO turbo
IF turbo$ = "c" THEN CALL cargo
IF turbo$ = "d" THEN END
IF turbo$ = "1" THEN CALL mainbridge
GOTO turbo2
END SUB

SUB weapons

weapon:
PRINT "1) Phasers class X"
PRINT "2) Phasers class X1a"
PRINT "3) Photon Torpedo Quantum"
PRINT "4) Photon Torpedo Standard"

END SUB

