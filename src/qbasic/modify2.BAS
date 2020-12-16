ON ERROR GOTO doneerr
OPEN "input2.txt" FOR INPUT AS #1
OPEN "output2.txt" FOR OUTPUT AS #2

DIM outstr$(0 TO 3)

CLS
item = -1

WRITE #2, "EFCTS", "DESC", "BLDG", "ROOM"

DO UNTIL EOF(1)

        item = item + 1

        LINE INPUT #1, a$

        IF INSTR(a$, " 0000") = 1 THEN

                outstr$(0) = LTRIM$(RTRIM$(STR$(VAL(MID$(a$, 2, 12)))))
                outstr$(1) = LTRIM$(RTRIM$(MID$(a$, 2 + 12 + 1, 40)))
                outstr$(2) = LTRIM$(RTRIM$(MID$(a$, 2 + 12 + 1 + 40 + 28, 4)))
                outstr$(3) = LTRIM$(RTRIM$(MID$(a$, 2 + 12 + 1 + 40 + 28 + 4 + 1, 4)))

               FOR x = 0 TO 3
doagain:
                        b$ = LTRIM$(RTRIM$(outstr$(x)))
                        y = INSTR(outstr$(x), CHR$(34))
                        IF y > 0 THEN

                                outstr$(x) = LTRIM$(RTRIM$(LEFT$(outstr$(x), y - 1) + RIGHT$(outstr$(x), LEN(outstr$(x)) - y)))

                        END IF

                        y = INSTR(outstr$(x), CHR$(34))
                        IF y > 0 THEN GOTO doagain
                        IF b$ <> outstr$(x) THEN PRINT outstr$(x)

                NEXT


                WRITE #2, outstr$(0), outstr$(1), outstr$(2), outstr$(3)

        END IF

LOOP

CLOSE
        PRINT
        PRINT "Number of Items Exported= "; LTRIM$(RTRIM$(STR$(item)))
        PRINT

END

doneerr:
        PRINT
        PRINT "Please place the inventory file in the same directory as this executible"
        PRINT "Please name the file 'input2.txt'"
        PRINT
        END


