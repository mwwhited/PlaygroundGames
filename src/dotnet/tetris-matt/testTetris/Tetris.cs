using System;
using System.Collections.Generic;
using System.Text;

namespace testTetris
{
    public class Tetris
    {
        public Tetris() { randomPiece(); }

        private int _pieceInt = 0;
        private bool[] _piece = new bool[16];
        private uint[] _color = new uint[]
            {
                0xFF0000,
                0xFFFF00,
                0x00FF00,
                0x00FFFF,
                0x0000FF,
                0xFF00FF,
                0xFFFFFF
            };
        private char[] _char = new char[]
            {
                'X',
                'E',
                'R',
                'O',
                'D',
                'B',
                'N'
            };

        private Random _rand = new Random((int)DateTime.Now.Ticks);

        public int PieceInt { get { return _pieceInt; } }

        public void setPiece(int piece)
        {
            _pieceInt = piece;
            for (int i = 0; i <= 15; i++)
                _piece[i] = false;
            switch (piece)
            {
                case 0:
                    for (int i = 0; i <= 3; i++)
                        _piece[i * 4 + 1] = true;
                    break;

                case 1:
                    for (int i = 0; i <= 2; i++)
                        _piece[i * 4 + 1] = true;
                    _piece[2] = true;
                    break;

                case 2:
                    for (int i = 0; i <= 2; i++)
                        _piece[i * 4 + 2] = true;
                    _piece[1] = true;
                    break;

                case 3:
                    for (int i = 0; i <= 1; i++)
                    {
                        _piece[i * 4 + 1] = true;
                        _piece[i * 4 + 2] = true;
                    }
                    break;

                case 4:
                    for (int i = 0; i <= 2; i++)
                        _piece[i] = true;
                    _piece[5] = true;
                    break;

                case 5:
                    for (int i = 0; i <= 1; i++)
                    {
                        _piece[i * 4 + 1] = true;
                        _piece[(i + 1) * 4 + 2] = true;
                    }
                    break;

                case 6:
                default:
                    for (int i = 0; i <= 1; i++)
                    {
                        _piece[(i + 1) * 4 + 1] = true;
                        _piece[i * 4 + 2] = true;
                    }
                    break;
            }
            shakedown();
        }

        private void shakedown()
        {
            bool[] shakePieces = new bool[16];

            int row = 0;
            int topRow = 0;
            while (row < 4)
            {
                if (
                    _piece[row * 4] ||
                    _piece[row * 4 + 1] ||
                    _piece[row * 4 + 2] ||
                    _piece[row * 4 + 3]
                    )
                {
                    topRow = row;
                    row = 4;
                }
                row++;
            }
            for (row = 0; row <= 3; row++)
            {
                if ((row + topRow) * 4 <= 15)
                {
                    shakePieces[row * 4] = _piece[(row + topRow) * 4];
                    shakePieces[(row * 4) + 1] = _piece[((row + topRow) * 4) + 1];
                    shakePieces[(row * 4) + 2] = _piece[((row + topRow) * 4) + 2];
                    shakePieces[(row * 4) + 3] = _piece[((row + topRow) * 4) + 3];
                }
                else
                {
                    shakePieces[row * 4] = false;
                    shakePieces[(row * 4) + 1] = false;
                    shakePieces[(row * 4) + 2] = false;
                    shakePieces[(row * 4) + 3] = false;
                }
            }

            row = 0;
            topRow = 0;
            while (row < 4)
            {
                if (
                    _piece[row] ||
                    _piece[row + 4] ||
                    _piece[row + 8] ||
                    _piece[row + 12]
                    )
                {
                    topRow = row;
                    row = 4;
                }
                row++;
            }
            for (row = 0; row <= 3; row++)
            {
                if ((row + topRow) * 4 <= 15)
                {
                    shakePieces[row] = _piece[row + topRow];
                    shakePieces[row + 4] = _piece[row + topRow + 4];
                    shakePieces[row + 8] = _piece[row + topRow + 8];
                    shakePieces[row + 12] = _piece[row + topRow + 12];
                }
                else
                {
                    shakePieces[row] = false;
                    shakePieces[row + 4] = false;
                    shakePieces[row + 8] = false;
                    shakePieces[row + 12] = false;
                }
            }

            for (row = 0; row <= 15; row++)
                _piece[row] = shakePieces[row];

        }

        public bool[] Piece { get { return _piece; } }

        public uint Color { get { return _color[PieceInt]; } }

        public char Character { get { return _char[PieceInt]; } }

        public void rotPiece()
        {
            bool[] _rotPieces = new bool[16];
            for (int i = 0; i <= 3; i++)
            {
                _rotPieces[0 + i * 4] = _piece[12 + i];
                _rotPieces[1 + i * 4] = _piece[8 + i];
                _rotPieces[2 + i * 4] = _piece[4 + i];
                _rotPieces[3 + i * 4] = _piece[0 + i];
            }
            for (int i = 0; i <= 15; i++)
                _piece[i] = _rotPieces[i];

            shakedown();
        }

        public void rotPieceOther()
        {
            rotPiece();
            rotPiece();
            rotPiece();
        }

        public void randomPiece()
        {
            setPiece(_rand.Next(0, _color.Length));
        }
    }
}
