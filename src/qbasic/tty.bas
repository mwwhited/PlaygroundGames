DECLARE SUB commandline ()
DECLARE SUB splashscreen ()
DECLARE SUB getport ()
DECLARE SUB portsetup ()
DECLARE SUB controlloop ()
DECLARE SUB portandscreen ()
COMMON SHARED X1, X2, Y1, Y2, C1, C2, COMNUM$, ver$
ver$ = "v07.17.98"

        CALL commandline
        ON ERROR GOTO errorcheck
        CALL splashscreen
        CALL getport
        CALL portandscreen
        CALL controlloop
        END
errorcheck:
        SELECT CASE ERR
        CASE 53
                CALL portsetup
        CASE 64
                RUN
        CASE ELSE
                PRINT "An errorcode"; ERR
                END
        END SELECT
RESUME

SUB commandline

command2$ = LCASE$(COMMAND$)
IF INSTR(1, command2$, "/?") THEN
        PRINT
        PRINT "This is TTY " + ver$
        PRINT "   By: Matthew Whited"
        PRINT
        PRINT "Command Line Help"
        PRINT
        PRINT "TTY [/SETUP]"
        PRINT "/SETUP   Setup new port number"
        PRINT
        END
END IF
IF INSTR(1, command2$, "/setup") THEN CALL portsetup

END SUB

SUB controlloop
top:
        A$ = INKEY$
        'Excludes
                'Bell
                IF A$ = CHR$(7) THEN A$ = ""
                'Tab
                IF A$ = CHR$(9) THEN A$ = ""
                'Line Feed
                IF A$ = CHR$(10) THEN A$ = ""
                'Clear Screen
                IF A$ = CHR$(12) THEN A$ = ""
                'Carrage Return
                IF A$ = CHR$(13) THEN A$ = ""
        'F1 Color Up
                IF A$ = CHR$(0) + CHR$(59) THEN
                        C1 = C1 + 1
                        IF C1 = 16 THEN C1 = 1
                        IF C1 = 32 THEN C1 = 17
                        IF COMNUM$ <> "VR" THEN
                                PRINT #1, "Color change"
                                PRINT #1, C1
                        END IF
                        A$ = ""
                END IF
        'F2 Color Down
                IF A$ = CHR$(0) + CHR$(60) THEN
                        C1 = C1 - 1
                        IF C1 = 16 THEN C1 = 31
                        IF C1 = 0 THEN C1 = 15
                        IF COMNUM$ <> "VR" THEN
                                PRINT #1, "Color change"
                                PRINT #1, C1
                        END IF
                        A$ = ""
                END IF
        'F3 Flash Color
                IF A$ = CHR$(0) + CHR$(61) THEN
                A$ = ""
                        IF C1 > 16 THEN
                                C1 = C1 - 16
                        ELSEIF C1 < 16 THEN
                                C1 = C1 + 16
                        END IF
                END IF
        'F12 to Logoff
                IF A$ = CHR$(0) + CHR$(134) THEN
                        IF COMNUM$ <> "VR" THEN PRINT #1, "Logging Off"
                        CLS
                        COLOR 7
                        PRINT "User Logged Off"
                        END
                END IF
        'Block all extra Function Keys
                IF A$ = CHR$(32) THEN A$ = CHR$(176)
                IF A$ <> RIGHT$(A$, 1) THEN A$ = ""
        'See if in dummy mode
                IF COMNUM$ <> "VR" THEN
                        PRINT #1, A$
                        INPUT #1, B$
                END IF
                IF COMNUM$ = "VR" THEN B$ = A$: C2 = C1
        'Host Logging off
                IF B$ = "Logging Off" THEN
                        CLS
                        COLOR 7
                        PRINT "Host Logged Off"
                        END
                END IF
        'Print Current Color in top Corner
                LOCATE 1, 1: COLOR C1: PRINT CHR$(201)
                IF B$ = "Color change" THEN INPUT #1, C2: B$ = ""
        'Convert Space
                IF B$ = CHR$(176) THEN B$ = CHR$(32)
                IF A$ = CHR$(176) THEN A$ = CHR$(32)
        'Print Host
                IF B$ <> "" THEN
                        X1 = X1 + 1
                        IF X1 = 78 THEN Y1 = Y1 + 1: X1 = 0
                        IF Y1 = 9 THEN Y1 = 0: FOR x = 0 TO 77: FOR y = 0 TO 8: LOCATE y + 12, x + 2: PRINT " ": NEXT y, x
                        LOCATE Y1 + 12, X1 + 2: COLOR C2: PRINT B$
                        B$ = ""
                END IF
        'Print User
                IF A$ <> "" THEN
                        X2 = X2 + 1
                        IF X2 = 78 THEN Y2 = Y2 + 1: X2 = 0
                        IF Y2 = 9 THEN Y2 = 0: FOR x = 0 TO 77: FOR y = 0 TO 8: LOCATE y + 2, x + 2: PRINT " ": NEXT y, x
                        LOCATE Y2 + 2, X2 + 2: COLOR C1: PRINT A$
                        A$ = ""
                END IF
GOTO top

END SUB

SUB getport

OPEN "tty.prt" FOR INPUT AS #1
INPUT #1, COMNUM$
CLOSE #1

PRINT "Using Port number "; COMNUM$
IF COMNUM$ = "VR" THEN PRINT "Dummy Mode"

END SUB

SUB portandscreen
 
  SLEEP 2
  'Defaults
        'X1 and Y1 are Host Postion  C1 is Host Color
        X1 = 0: Y1 = 0: C2 = 7
        'X2 and Y2 are User Postion  C2 is User Color
        X2 = 0: Y2 = 0: C1 = 7

  'Open port Com#(comnum$)
  CLOSE
  IF COMNUM$ <> "VR" THEN
  OPEN COMNUM$ + ":9600,n,8,1,rs,cs,ds,cd" FOR RANDOM AS #1
  END IF

  'Screen Setup
  CLS
  COLOR C1
        'Top Line
        PRINT CHR$(201); : FOR N = 1 TO 78: PRINT CHR$(205); : NEXT N: PRINT CHR$(187)
        'Side Lines
        FOR N = 2 TO 20: LOCATE N, 1: PRINT CHR$(186): LOCATE N, 80: PRINT CHR$(186); : NEXT N
        'Bottom Line
        PRINT CHR$(200); : FOR N = 1 TO 78: PRINT CHR$(205); : NEXT N: PRINT CHR$(188)
        'Middle Line
        LOCATE 11, 1: PRINT CHR$(204); : FOR N = 1 TO 78: PRINT CHR$(205); : NEXT N: PRINT CHR$(185)
        LOCATE 22, 3: PRINT "F1   Color Up       F2   Color Down"
        LOCATE 23, 3: PRINT "F3   Flash Toggle   F12  Log off"

END SUB

SUB portsetup
clearkey$ = INKEY$
INPUT "Port(COM#, LPT#,VR)", COMNUM$
COMNUM$ = UCASE$(COMNUM$)
OPEN "tty.prt" FOR OUTPUT AS #1
WRITE #1, COMNUM$
CLOSE #1

END SUB

SUB splashscreen
CLS
SCREEN 13
WIDTH 40, 25
LOCATE 10, 13: COLOR 7: PRINT "Welcome to TTY"
LOCATE 12, 16: PRINT ver$
LOCATE 14, 19: PRINT "By:"
LOCATE 15, 13: COLOR 2: PRINT "M"; : COLOR 4: PRINT "atthew "; : COLOR 2: PRINT "W"; : COLOR 4: PRINT "hited": COLOR 7
SLEEP 5
SCREEN 0
WIDTH 80, 25

END SUB

