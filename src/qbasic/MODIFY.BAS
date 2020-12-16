ON ERROR GOTO doneerr
OPEN "input.txt" FOR INPUT AS #1
OPEN "output.txt" FOR OUTPUT AS #2

DIM outstr$(0 TO 4)

CLS

linenum = 0
cntnum = 0
item = -1

WRITE #2, "EFCTS", "DESC", "SERIAL", "MODEL", "LOC"

DO UNTIL EOF(1)

        LINE INPUT #1, a$
        testvar$ = "tag number: "
        IF INSTR(LCASE$(a$), testvar$) = 1 THEN
                IF item > -1 THEN
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
                        WRITE #2, outstr$(0), outstr$(1), outstr$(2), outstr$(3), outstr$(4)
                END IF
                item = item + 1
                outvar = VAL(MID$(a$, INSTR(LCASE$(a$), testvar$) + LEN(testvar$), 12))
                outstr$(0) = RTRIM$(LTRIM$(STR$(outvar)))
                cntnum = 0
        END IF
       
        testvar$ = "first desc.  : "
        IF INSTR(LCASE$(a$), testvar$) <> 0 THEN
                outvar$ = RTRIM$(LTRIM$((MID$(a$, INSTR(LCASE$(a$), testvar$) + LEN(testvar$), 40))))
                outstr$(1) = (outvar$)
        END IF
       
        testvar$ = "serial#    : "
        IF INSTR(LCASE$(a$), testvar$) <> 0 THEN
                outvar$ = RTRIM$(LTRIM$((MID$(a$, INSTR(LCASE$(a$), testvar$) + LEN(testvar$), 18))))
                outstr$(2) = (outvar$)
        END IF

        testvar$ = "model#     : "
        IF INSTR(LCASE$(a$), testvar$) <> 0 THEN
                outvar$ = RTRIM$(LTRIM$((MID$(a$, INSTR(LCASE$(a$), testvar$) + LEN(testvar$), 18))))
                outstr$(3) = (outvar$)
        END IF

        testvar$ = "location : "
        IF INSTR(LCASE$(a$), testvar$) <> 0 THEN
                outvar$ = RTRIM$(LTRIM$((MID$(a$, INSTR(LCASE$(a$), testvar$) + LEN(testvar$), 9))))
                outstr$(4) = (outvar$)
        END IF

        'IF cntnum = 0 THEN PRINT outstr$(0); "|"; outstr$(1); "|"; outstr$(2); "|"; outstr$(3); "|"; outstr$(4); "|"; item

        linenum = 1 + linenum
        cntnum = 1 + cntnum
LOOP

CLOSE
        PRINT
        PRINT "Number of Items Exported= "; LTRIM$(RTRIM$(STR$(item)))
        PRINT

END

doneerr:
        PRINT
        PRINT "Please place the inventory file in the same directory as this executible"
        PRINT "Please name the file 'input.txt'"
        PRINT
        END


