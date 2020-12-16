using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Diagnostics;

namespace tetrisagain
{
    class Program
    {
        const string ARE_YOU_SURE = "Are you sure? Y/N";
        const string BLANK = "                 ";

        static int _currentScore = 0;

        static Board _board = new Board();
        static ushort _nextPiece = Piece.RandomPiece();
        static ushort _currentPiece = Piece.RandomPiece();
        static int _totalWidth = Console.WindowWidth;
        static int _offset = _totalWidth / 3;
        static int _gameWidth = Board.BOARD_WIDTH;
        static int _totalHeight = Board.BOARD_LENGTH;

        static int _nXpos = (_offset / 2) - (6 / 2);
        static int _nYpos = _totalHeight - 6 - 2;

        static int _xPos = _gameWidth /2;
        static int Xpos
        {
            get { return _xPos; }
            set
            {
                _xPos = value;
                if (_xPos < 0)
                    _xPos = 0;
                else if (_xPos > _gameWidth)
                    _xPos = _gameWidth;
            }
        }

        static int _yPos = 0;
        static int Ypos
        {
            get { return _yPos; }
            set
            {
                _yPos = value;
                if (_yPos < 0)
                    _yPos = 0;
                else if (_yPos > _totalHeight - 1)
                    _yPos = _totalHeight - 1;
            }
        }

        static bool _areYouSure = false;
        static bool _killBit = false;

        static Thread _threadHandle = new Thread(new ThreadStart(CheckTimer));

        static int _gameOver = 0;
        static void CheckTimer()
        {
            while (!_killBit)
            {
                if (_gameOver > 3)
                {
                    Console.WriteLine("Game Over!!!");
                    _threadHandle.Suspend();
                    _killBit = true;
                    Console.ReadLine();
                }

                Thread.Sleep(200);
                //BlankPiece();
                if (_board.CanDrop(Xpos, Ypos, _currentPiece))
                {
                    BlankPiece();

                    _gameOver = 0;
                    Ypos++;

                    DrawBoxNextPiece();
                    DrawBoard();
                    DrawScreen();
                    FillScreen();
                    DrawBoard2();                    
                }
                else
                {
                    _board.MergePiece(Xpos, Ypos, _currentPiece);
                    Ypos = 0;
                    Xpos = _gameWidth / 2;
                    _currentPiece = _nextPiece;
                    _nextPiece = Piece.RandomPiece();
                    _gameOver++;
                }

                DrawScreen();
            }
        }

        static void BlankPiece()
        {
            Console.CursorLeft = Xpos + _offset;
            Console.CursorTop = Ypos;
            Console.Write("    ");
            Console.CursorLeft = Xpos + _offset;
            Console.CursorTop = Ypos + 1;
            Console.Write("    ");
            Console.CursorLeft = Xpos + _offset;
            Console.CursorTop = Ypos + 2;
            Console.Write("    ");
            Console.CursorLeft = Xpos + _offset;
            Console.CursorTop = Ypos + 3;
            Console.Write("    ");
        }

        static void DrawScreen()
        {
            BlankPiece();

            string _out = Convert.ToString(_currentPiece, 2)
                .PadLeft(16, '0')
                .Replace("1", "X")
                .Replace("0", " ");
            Console.CursorLeft = Xpos + _offset;
            Console.CursorTop = Ypos;
            Console.Write(_out.Substring(0, 4));
            Console.CursorLeft = Xpos + _offset;
            Console.CursorTop = Ypos + 1;
            Console.Write(_out.Substring(4, 4));
            Console.CursorLeft = Xpos + _offset;
            Console.CursorTop = Ypos + 2;
            Console.Write(_out.Substring(8, 4));
            Console.CursorLeft = Xpos + _offset;
            Console.CursorTop = Ypos + 3;
            Console.Write(_out.Substring(12, 4));
        }

        static void DrawBoard()
        {
            //Draw board
            for (int i = 0; i < _totalHeight; i++)
            {
                Console.CursorTop = i;
                Console.CursorLeft = _offset - 1;
                Console.Write("|");
            }
        }

        static void DrawBoard2()
        {
            //Draw board
            for (int i = 0; i < _totalHeight; i++)
            {
                Console.CursorTop = i;
                Console.CursorLeft = _offset + _gameWidth;
                Console.Write("|");
            }
        }

        static void DrawBoxNextPiece()
        {
            //Draw next box
            for (int i = 0; i <= 6; i++)
            {
                Console.CursorLeft = i + _nXpos;
                Console.CursorTop = _nYpos;
                if (i == 0 || i == 6)
                    Console.Write('+');
                else
                    Console.Write('-');

                Console.CursorLeft = i + _nXpos;
                Console.CursorTop = _nYpos + 5;
                if (i == 0 || i == 6)
                    Console.Write('+');
                else
                    Console.Write('-');

                if (i > 0 && i < 5)
                {
                    Console.CursorLeft = _nXpos;
                    Console.CursorTop = i + _nYpos;
                    Console.Write('|');

                    Console.CursorLeft = _nXpos + 6;
                    Console.CursorTop = i + _nYpos;
                    Console.Write('|');
                }
            }

            string _out = Convert.ToString(_nextPiece, 2)
                .PadLeft(16, '0')
                .Replace("1", "X")
                .Replace("0", " ");
            Console.CursorLeft = _nXpos + 1;
            Console.CursorTop = _nYpos + 1;
            Console.Write(_out.Substring(0, 4));
            Console.CursorLeft = _nXpos + 1;
            Console.CursorTop = _nYpos + 1 + 1;
            Console.Write(_out.Substring(4, 4));
            Console.CursorLeft = _nXpos + 1;
            Console.CursorTop = _nYpos + 2 + 1;
            Console.Write(_out.Substring(8, 4));
            Console.CursorLeft = _nXpos + 1;
            Console.CursorTop = _nYpos + 3 + 1;
            Console.Write(_out.Substring(12, 4));
        }

        static void FillScreen()
        {
            for (int yi = 0; yi < _board.Lines.Count; yi++)
            {
                ushort _line = _board.Lines[yi];
                for (int xi = 0; xi < 16; xi++)
                {
                    ushort _mask = (ushort)(1 << (15 - xi));

                    if ((_mask & _line) == _mask)
                    {
                        Console.CursorTop = yi;
                        Console.CursorLeft = _offset + xi;
                        Console.Write("W");
                    }
                }
            }
        }

        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            Console.Title = "teXtris";

            Console.WriteLine("Welcome to teXtrix.  Please press any key to start");
            Console.ReadKey();
            Console.Clear();

            //_board.BuildRandomBoard(5);

            _board.Score += new LineScore(_board_Score);
           
            _threadHandle.Start();

            char _charBuff = '\0';
            while (!_killBit)
            {
                BlankPiece();
                if (_areYouSure)
                {
                    switch (_charBuff)
                    {
                        case 'Y':
                        case 'y':
                            _killBit = true;
                            try
                            {
                                _threadHandle.Abort();
                                _threadHandle.Join();
                            }
                            catch (Exception e)
                            {
                                Debug.WriteLine(e.ToString());
                            }
                            continue;

                        case (char)27:
                            break;

                        default:
                            Console.CursorTop = _totalHeight / 2;
                            Console.CursorLeft = (_totalWidth / 2) - (BLANK.Length / 2);
                            Console.Write(BLANK);
                            _killBit = false;
                            _areYouSure = false;
                            _threadHandle.Resume();
                            break;
                    }
                }
                else
                {
                    ushort _rotatePiece;
                    switch (_charBuff)
                    {
                        case 'A':
                        case 'a':
                            if (_board.CanRotateLeft(Xpos, Ypos, _currentPiece))
                                _currentPiece = Piece.RotateLeft(_currentPiece);
                            break;

                        case 'D':
                        case 'd':
                            if (_board.CanRotateRight(Xpos, Ypos, _currentPiece))
                                _currentPiece = Piece.RotateRight(_currentPiece);
                            break;

                        case 'J':
                        case 'j':
                            if (_board.CanLeft(Xpos, Ypos, _currentPiece))
                                Xpos--;
                            break;

                        case 'L':
                        case 'l':
                            if (_board.CanRight(Xpos, Ypos, _currentPiece))
                                Xpos++;
                            break;

                        case 'K':
                        case 'k':
                            while (_board.CanDrop(Xpos, Ypos, _currentPiece))
                                Ypos++;
                            Ypos--;
                            break;

                        case (char)27:
                            _threadHandle.Suspend();
                            _areYouSure = true;
                            continue;

                        default:
                            break;
                    }

                    Console.Clear();

                    BlankPiece();
                    DrawBoxNextPiece();
                    DrawBoard();
                    DrawScreen();
                    FillScreen();
                    DrawBoard2();

                    if (_areYouSure)
                    {
                        Console.CursorTop = _totalHeight / 2;
                        int _cPos = (_totalWidth / 2) - (ARE_YOU_SURE.Length / 2);
                        Console.CursorLeft = _cPos;
                        Console.Write(ARE_YOU_SURE);
                    }
                }
                _charBuff = Console.ReadKey(true).KeyChar;
            }
        }

        static void _board_Score(object sender, LineScoreArgs e)
        {
            Console.CursorTop = 0;
            Console.CursorLeft = _offset + _gameWidth + 10;

            _currentScore += 2 << e.CompletedLines;

            Console.Write(_currentScore);
        }
    }

}
