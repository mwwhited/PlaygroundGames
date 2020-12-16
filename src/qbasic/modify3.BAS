DECLARE FUNCTION REPLACE$ (inputstr$, oldstr$, newstr$)
ON ERROR GOTO doneerr
OPEN "input3.txt" FOR INPUT AS #1
OPEN "output3.txt" FOR OUTPUT AS #2

CLS
item = -1
room = -1


WRITE #2, "bldg", "room", "asset", "qty", "desc", "sn", "model", "cat", "stat", "cond"

DO UNTIL EOF(1)
        LINE INPUT #1, a$
        IF INSTR(LCASE$(a$), " tag number") = 1 THEN
                LINE INPUT #1, b$
                bldg$ = LTRIM$(RTRIM$(MID$(b$, 2, 4)))
                room$ = LTRIM$(RTRIM$(MID$(b$, 7, 4)))
                donext = 0
                DO UNTIL (donext = 1) OR (EOF(1))
                        LINE INPUT #1, c$
                        IF INSTR(LCASE$(c$), " 000") = 1 THEN
                                item = item + 1
                                asset = VAL(MID$(c$, 2, 12))
                                qty = VAL(MID$(c$, 15, 5))
                                desc$ = LTRIM$(RTRIM$(REPLACE$(MID$(c$, 22, 40), CHR$(34), "")))
                                sn$ = LTRIM$(RTRIM$(REPLACE$(MID$(c$, 63, 20), CHR$(34), "")))
                                model$ = LTRIM$(RTRIM$(REPLACE$(MID$(c$, 84, 20), CHR$(34), "")))
                                cat$ = LTRIM$(RTRIM$(MID$(c$, 105, 4)))
                                stat$ = LTRIM$(RTRIM$(REPLACE$(MID$(c$, 110, 2), CHR$(34), "''")))
                                cond$ = LTRIM$(RTRIM$(REPLACE$(MID$(c$, 113, 2), CHR$(34), "''")))
                                WRITE #2, bldg$, room$, asset, qty, desc$, sn$, model$, cat$, stat$, cond$
                        ELSE
                                donext = 1
                        END IF
                LOOP
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
        PRINT "Please name the file 'input3.txt'"
        PRINT
        END


FUNCTION REPLACE$ (inputstr$, oldstr$, newstr$)

        replaceloop = 0
        strout$ = inputstr$

        DO WHILE replaceloop = 0
                replace1 = INSTR(strout$, oldstr$)

                IF replace1 > 0 THEN
                        'PRINT strout$
                        strout$ = LEFT$(strout$, replace1 - 1) + newstr$ + RIGHT$(strout$, LEN(strout$) - replace1)
                        'PRINT strout$
                        'PRINT

                ELSE
                        replaceloop = 1
                END IF

        LOOP

        REPLACE$ = strout$

END FUNCTION

