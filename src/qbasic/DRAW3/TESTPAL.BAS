SCREEN 13
R = 32: B = 32: G = 32
LOCATE 2, 11: PRINT "RED   "; R
     LINE (8, 8)-(71, 16), , B
     LINE (R + 8, 8)-(R + 8, 16)
LOCATE 4, 11: PRINT "BLUE  "; B
     LINE (8, 24)-(71, 32), , B
     LINE (B + 8, 24)-(B + 8, 32)
LOCATE 6, 11: PRINT "GREEN "; G
     LINE (8, 40)-(71, 48), , B
     LINE (G + 8, 40)-(G + 8, 48)
'PALETTE 1,

