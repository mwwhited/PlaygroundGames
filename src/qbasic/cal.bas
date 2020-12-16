'
' Doom.bas port by Matthew Whited
' Origanaly Programmaned By Neotech Computing Soluations
'
'5432(pinout)
'FBLR
'
'98765432
'    FBLR
'
'1 true
'0 false
'
'Binary(pinout) Direction       Decimal
'1000           forward         8
'0100           backward        4
'0010           left            2
'0001           right           1
'1001           Fwd Right       9
'1010           Fwd Left        10
'0101           Back Left       5
'0110           Back Right      6
'
DECLARE SUB caliport ()
DECLARE SUB traveldist ()
DECLARE SUB calitime ()
DECLARE SUB calidist ()
DECLARE SUB testchr (file$, sce%)

DIM direct(0 TO 100), druation(0 TO 100)
COMMON SHARED cali2, cali3, nsect, port
ver$ = "v06.04.1998"
xpart = 0: xmin = 0: xmax = 0: ypart = 0: ymin = 0: ymax = 0

'Command line pram.
linecommand$ = LCASE$(COMMAND$)

'PRINT linecommand$

IF INSTR(1, linecommand$, "/?") <> 0 THEN
        PRINT ""
        PRINT "Remote Control Car Control Program"
        PRINT ver$
        PRINT ""
        PRINT "By: Matthew Whited"
        PRINT ""
        COLOR 15
        PRINT "   CAL";
        COLOR 7
        PRINT " ([/time] [/dist] [/port] | [/?])"
        PRINT ""
        PRINT "         /time   Calibrate time tics per second"
        PRINT "         /dist   Calibrate distance per second"
        PRINT "         /port   Change output port address"
        PRINT "         /?      This help screen"
        PRINT ""
        END
END IF
IF INSTR(1, linecommand$, "/port") <> 0 THEN CALL caliport
IF INSTR(1, linecommand$, "/time") <> 0 THEN CALL calitime
IF INSTR(1, linecommand$, "/dist") <> 0 THEN CALL calidist
                                   
ON ERROR GOTO errorcheck

recheck:

'cal.tim  calibrate time
errorchk$ = "time"
OPEN "cal.tim" FOR INPUT AS #1
INPUT #1, cali2
CLOSE #1

'cal.dis  calibrate distance
errorchk$ = "dist"
OPEN "cal.dis" FOR INPUT AS #1
INPUT #1, cali3
CLOSE #1

'cal.prt  port name
errorchk$ = "port"
OPEN "cal.prt" FOR INPUT AS #1
INPUT #1, cali3
CLOSE #1

'----------------------------------------------------
top2:
SCREEN 0
CLS
PRINT ""
PRINT "Remote Control Car Control Program"
PRINT ver$
PRINT ""
PRINT "By: Matthew Whited"
PRINT "For explore post 891"
PRINT ""
INPUT "Load file (N/Y) ", chkload$
chkload$ = LCASE$(chkload$)
IF chkload$ = "y" THEN GOTO loadprog
top:
INPUT "# of Steps (ó99)"; nsteps
nsteps = ABS(INT(nsteps))
IF nsteps <= 0 THEN PRINT "Sorry must be more then 0 steps": GOTO top
IF nsteps >= 99 THEN nsteps = 99: PRINT "sorry no more than 99 steps": SLEEP 2

FOR fsteps = 1 TO nsteps

CLS
retrystep:

LOCATE 1, 1
PRINT "Step # "; fsteps
PRINT "(F)orward"
PRINT "(B)ackwards"
PRINT "(L)eft"
PRINT "(R)ight"
PRINT "(FL) Forward and Left"
PRINT "(FR) Froward and Right"
PRINT "(BL) Backward and Left"
PRINT "(BR) Backward and Right"
PRINT ""
INPUT "Letter for Direction ", direct$
direct$ = LCASE$(direct$)
IF direct$ = "f" THEN
        dist = 8
ELSEIF direct$ = "b" THEN
        dist = 4
ELSEIF direct$ = "l" THEN
        dist = 2
ELSEIF direct$ = "r" THEN
        dist = 1
ELSEIF direct$ = "fl" THEN
        dist = 10
ELSEIF direct$ = "fr" THEN
        dist = 9
ELSEIF direct$ = "bl" THEN
        dist = 5
ELSEIF direct$ = "br" THEN
        dist = 6
ELSE PRINT "Please Retry Step": GOTO retrystep
END IF
direct(fsteps) = dist
INPUT "Number of seconds to travel ", nsect
IF fsteps < 0 THEN CALL traveldist
duration(fsteps) = nsect

IF direct$ = "f" <> 0 THEN xpart = xpart + nsect
IF direct$ = "b" <> 0 THEN xpart = xpart - nsect
IF direct$ = "fl" <> 0 THEN ypart = ypart + nsect: xpart = xpart + nsect
IF direct$ = "fr" <> 0 THEN ypart = ypart - nsect: xpart = xpart + nsect
IF direct$ = "br" <> 0 THEN ypart = ypart - nsect: xpart = xpart - nsect
IF direct$ = "bl" <> 0 THEN ypart = ypart + nsect: xpart = xpart - nsect
IF xpart > xmax THEN xmax = xpart
IF xpart < xmin THEN xmin = xpart
IF ypart > ymax THEN ymax = ypart
IF ypart < ymin THEN ymin = ypart
NEXT fsteps

'fixed squareness bug

IF xmax < ABS(xmin) THEN xmax = ABS(xmin)
IF xmax > ABS(xmin) THEN xmin = -ABS(xmax)
IF ymax < ABS(ymin) THEN ymax = ABS(ymin)
IF ymax > ABS(ymin) THEN ymin = -ABS(ymax)
IF xmax = xmin THEN xmax = xmax + 1: xmin = xmin - 1
IF ymax = ymin THEN ymax = ymax + 1: ymin = ymin - 1

'-------------------------------------------
runprog:
xpart2 = 0: ypart2 = 0

SCREEN 12
WINDOW SCREEN (xmax, ymax)-(xmin, ymin)
FOR fsteps = 1 TO nsteps
xpart3 = xpart2
ypart3 = ypart2
IF direct(fsteps) = 8 THEN
        'Forward
        xpart2 = xpart2 + duration(fsteps)
ELSEIF direct(fsteps) = 4 THEN
        'Backward
        xpart2 = xpart2 - duration(fsteps)
ELSEIF direct(fsteps) = 9 THEN
        'Forward and Left
        ypart2 = ypart2 + duration(fsteps)
        xpart2 = xpart2 + duration(fsteps)
ELSEIF direct(fsteps) = 10 THEN
        'Forward and Right
        ypart2 = ypart2 - duration(fsteps)
        xpart2 = xpart2 + duration(fsteps)
ELSEIF direct(fsteps) = 5 THEN
        'Backward and Left
        ypart2 = ypart2 - duration(fsteps)
        xpart2 = xpart2 - duration(fsteps)
ELSEIF direct(fsteps) = 6 THEN
        'Backward and Right
        ypart2 = ypart2 + duration(fsteps)
        xpart2 = xpart2 - duration(fsteps)
END IF
LINE (xpart2, ypart2)-(xpart3, ypart3)

IF port = 0 THEN GOTO justtest
FOR tic = 1 TO INT(cali2 * duration(fsteps))
OUT port, direct(fsteps)
NEXT tic
justtest:

NEXT fsteps

xpart4 = 2 * xmax / 640 * 5
ypart4 = 2 * ymax / 480 * 5
CIRCLE (0, 0), xpart4, 2
PAINT (0, 0), 2
CIRCLE (xpart2, ypart2), xpart4, 4
PAINT (xpart2, ypart2), 4
LOCATE 25, 1
PRINT "ESC to continue"
LOCATE 25, 32
PRINT "Ctrl-S to Save"
LOCATE 25, 62
PRINT "Ctrl+Q to continue"

keyloop:
a$ = INKEY$
IF a$ = CHR$(17) THEN END
IF a$ = CHR$(27) THEN GOTO top2
IF a$ = CHR$(19) THEN GOTO saveprog
GOTO keyloop

'-------------------------------------------
saveprog:
SCREEN 0
CALL testchr(file$, sce%)
IF file$ = CHR$(27) THEN PRINT "Save aborted": SLEEP 2: GOTO top2
OPEN file$ FOR OUTPUT AS #1
WRITE #1, nsteps
WRITE #1, xmax, xmin, ymax, ymin
FOR fsteps = 1 TO nsteps
WRITE #1, duration(fsteps), direct(fsteps)
NEXT fsteps
CLOSE #1
PRINT "Done saving file": SLEEP 2: GOTO top2

'-------------------------------------------
loadprog:
SCREEN 0
errorchk$ = "load"
CALL testchr(file$, sce%)
IF file$ = CHR$(27) THEN PRINT "load aborted": SLEEP 2: GOTO top2
PRINT file$
OPEN file$ FOR INPUT AS #1
INPUT #1, nsteps
INPUT #1, xmax, xmin, ymax, ymin
FOR fsteps = 1 TO nsteps
INPUT #1, duration(fsteps), direct(fsteps)
NEXT fsteps
CLOSE #1
PRINT "Done loading file": SLEEP 2: GOTO runprog
'-------------------------------------------

errorcheck:
CLOSE
IF errorchk$ = "time" THEN
        CALL calitime: errorchk$ = ""
ELSEIF errorchk$ = "dist" THEN
        CALL calidist: errorchk$ = ""
ELSEIF errorchk$ = "port" THEN
        CALL caliport: errorchk$ = ""
ELSEIF errorchk$ = "load" THEN
        GOTO loadprog: errorchk$ = ""
ELSE PRINT ERR
END IF
RESUME

SUB calidist

INPUT "time in seconds to go 100 feet ", cali3
'cali3 = inches per seconds
cali3 = cali3 / 100 * 12
OPEN "cal.dis" FOR OUTPUT AS #1
WRITE #1, cali3
CLOSE #1


END SUB

SUB caliport

INPUT "Input port address in hexadecimal (ex. &h2f8) ", port
OPEN "cal.prt" FOR OUTPUT AS #1
WRITE #1, port
CLOSE #1


END SUB

SUB calitime

a$ = TIME$
PRINT a$
        FOR i = 1 TO 1000000
        NEXT i
B$ = TIME$
PRINT B$
REM Var Declar / fix
        beg3$ = MID$(a$, 7, 2)
        fin3$ = MID$(B$, 7, 2)
        beg1$ = MID$(a$, 1, 2)
        fin1$ = MID$(B$, 1, 2)
        beg2$ = MID$(a$, 4, 2)
        fin2$ = MID$(B$, 4, 2)
        valbeg$ = beg1$ + beg2$ + beg3$
        valfin$ = fin1$ + fin2$ + fin3$
times = VAL(valbeg$)
timef = VAL(valfin$)
cal = timef - times

'cali2 is ticks per second
cali2 = 1000000 / cal
OPEN "cal.tim" FOR OUTPUT AS #1
WRITE #1, cali2
CLOSE #1

END SUB

DEFINT A-Z
SUB testchr (file$, sce) STATIC

file$ = ""
SHELL "DIR *.prg /P/W"
tempkey:
keywait$ = INKEY$
IF keywait$ = "" THEN GOTO tempkey
CLS
WHAT:
a = 0: B = 0
what2:
IF file2$(a, B) <> "" THEN file2$(a, B) = ""
a = a + 1
IF a = 11 THEN B = B + 1
IF a = 11 THEN a = 0
IF file2$(a, B) = "" THEN GOTO addletter2
GOTO what2
addletter2:
a = 0: B = 0
LOCATE 1, 1: PRINT "(ESCAPE to cancel  ENTER to set) file name ";
addletter:
PRINT file2$(a, B);
a = a + 1
IF a = 11 THEN B = B + 1
IF a = 11 THEN a = 0
IF file2$(a, B) = "" OR file2$(a, B) = " " THEN GOTO nextletter3
IF a <= 0 AND B <= 0 THEN a = 0 AND B = 0: GOTO nextletter
GOTO addletter
nextletter3:
PRINT "            "
nextletter:
file2$(a, B) = INKEY$
IF file2$(a, B) = "" THEN GOTO nextletter
IF file2$(a, B) = CHR$(13) THEN file2$(a, B) = " ": GOTO NEXTTEST
IF file2$(a, B) = " " THEN PRINT CHR$(7)
IF file2$(a, B) = CHR$(27) THEN GOTO whatever
IF file2$(a, B) = "" THEN GOTO DOWN2
GOTO addletter2
NEXTTEST:
a = 0: B = 0
NEXTTEST3:
IF file2$(a, B) = " " THEN GOTO NEXTTEST2
file$ = file$ + file2$(a, B)
a = a + 1
IF a = 11 THEN B = B + 1
IF a = 11 THEN a = 0
GOTO NEXTTEST3
DOWN2:
IF file2$(a, B) = "" THEN file2$(a, B) = ""
a = a - 1
IF a = -1 THEN B = B - 1
IF a = -1 THEN a = 10
IF B = -1 THEN B = 0
file2$(a, B) = ""
GOTO addletter2
NEXTTEST2:
sec = 0
IF INSTR(1, file$, ".") = 0 THEN GOTO HUHUH
TESTNEXT:
IF INSTR(1, file$, ",") <> 0 THEN sce = sce + 1
IF INSTR(1, file$, " ") <> 0 THEN sce = sce + 1
IF INSTR(1, file$, "*") <> 0 THEN sce = sce + 1
IF INSTR(1, file$, "-") <> 0 THEN sce = sce + 1
IF INSTR(1, file$, "+") <> 0 THEN sce = sce + 1
IF INSTR(1, file$, "?") <> 0 THEN sce = sce + 1
IF INSTR(1, file$, ">") <> 0 THEN sce = sce + 1
IF INSTR(1, file$, "<") <> 0 THEN sce = sce + 1
IF INSTR(1, file$, "|") <> 0 THEN sce = sce + 1
IF INSTR(1, file$, "&") <> 0 THEN sce = sce + 1
IF INSTR(1, file$, "%") <> 0 THEN sce = sce + 1
IF INSTR(1, file$, "@") <> 0 THEN sce = sce + 1
IF INSTR(1, file$, "#") <> 0 THEN sce = sce + 1
IF INSTR(1, file$, "$") <> 0 THEN sce = sce + 1
IF INSTR(1, file$, "^") <> 0 THEN sce = sce + 1
IF INSTR(1, file$, "(") <> 0 THEN sce = sce + 1
IF INSTR(1, file$, "=") <> 0 THEN sce = sce + 1
IF INSTR(1, file$, ")") <> 0 THEN sce = sce + 1
IF INSTR(1, file$, "'") <> 0 THEN sce = sce + 1
IF INSTR(1, file$, "{") <> 0 THEN sce = sce + 1
IF INSTR(1, file$, "}") <> 0 THEN sce = sce + 1
IF INSTR(1, file$, "[") <> 0 THEN sce = sce + 1
IF INSTR(1, file$, "]") <> 0 THEN sce = sce + 1
IF sce <> 0 THEN GOTO WHAT
GOTO whatever
HUHUH:
file$ = file$ + ".PRG"
IF sce = 0 THEN GOTO TESTNEXT
whatever:

END SUB

DEFSNG A-Z
SUB traveldist

'nsect
'cali3
nsect = (nsect * -1) / cali3
                         
END SUB

