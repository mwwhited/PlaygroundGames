using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using System.Threading;

namespace testTetris
{
    class Program
    {
        const string ARE_YOU_SURE = "Are you sure? Y/N";
        const string BLANK = "                 ";

        static Screen _screen; // = new Screen(_gameWidth, _totalHeight);
        static Tetris _nextPiece = new Tetris();
        static Tetris _currentPiece = new Tetris();
        static int _totalWidth = Console.WindowWidth;
        static int _offset = _totalWidth / 3;
        static int _gameWidth = _offset;
        static int _totalHeight = Console.WindowHeight;

        static int _nXpos = (_offset / 2) - (6 / 2);
        static int _nYpos = _totalHeight - 6 - 2;

        static int _xPos = _gameWidth / 2;
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

        static void CheckTimer()
        {
            while (!_killBit)
            {
                Thread.Sleep(100);
                BlankPiece();
                if (!_screen.CollisionDetect(Xpos, Ypos, _currentPiece))
                {
                    Ypos++;
                }
                else
                {
                    Ypos = 0;
                    Xpos = _gameWidth / 2;
                    _currentPiece.setPiece(_nextPiece.PieceInt);
                    _nextPiece.randomPiece();
                }
                DrawScreen();
            }
        }

        static void BlankPiece()
        {
            //Blank piece
            for (int y = 0; y <= 3; y++)
            {
                if ((y + Ypos + 1) > _totalHeight)
                    continue;

                Console.CursorTop = y + Ypos;
                for (int x = 0; x <= 3; x++)
                {
                    if (Xpos + x > _offset - 1)
                        continue;

                    Console.CursorLeft = x + _offset + Xpos;
                    Console.Write(" ");
                }
            }
        }

        static void DrawScreen()
        {
            BlankPiece();


            for (int y = 0; y <= 3; y++)
            {
                if ((y + Ypos + 1) > _totalHeight)
                    continue;

                Console.CursorTop = y + Ypos;
                for (int x = 0; x <= 3; x++)
                {
                    if (Xpos + x > _offset - 1)
                        continue;

                    Console.CursorLeft = x + _offset + Xpos;

                    if (_currentPiece.Piece[x + y * 4])
                        Console.Write(_currentPiece.Character);
                    else
                        Console.Write(" ");
                }
            }

            for (int y = 0; y <= 3; y++)
            {
                Console.CursorTop = y + _nYpos + 1;
                for (int x = 0; x <= 3; x++)
                {
                    Console.CursorLeft = x + _nXpos + 1;

                    if (_nextPiece.Piece[x + y * 4])
                        Console.Write(_nextPiece.Character);
                    else
                        Console.Write(" ");
                }
            }
        }

        static void DrawBoard()
        {
            //Draw board
            for (int i = 0; i < _totalHeight; i++)
            {
                Console.CursorTop = i;
                Console.CursorLeft = _offset - 1;
                Console.Write("|");
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
        }

        static void Main(string[] args)
        {
            _screen = new Screen(_gameWidth, _totalHeight-3);

            Console.CursorVisible = false;
            Console.Title = "teXtris";

            Console.WriteLine("Welcome to teXtrix.  Please press any key to start");
            Console.ReadKey();
            Console.Clear();

            //_threadHandle.Start();

            char _charBuff = '\0';

            //Ypos = _totalHeight = 5;

            DrawBoard();
            DrawBoxNextPiece();

            while (!_killBit)
            {
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
                            catch
                            {
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
                    BlankPiece();

                    switch (_charBuff)
                    {
                        case 'A':
                        case 'a':
                            _currentPiece.rotPiece();
                            break;

                        case 'D':
                        case 'd':
                            _currentPiece.rotPieceOther();
                            break;

                        case 'J':
                        case 'j':
                            if (_screen.CanLeft(Xpos, Ypos, _currentPiece))
                                Xpos--;
                            break;

                        case 'L':
                        case 'l':
                            if (_screen.CanRight(Xpos, Ypos, _currentPiece))
                                Xpos++;
                            break;

                        case 'I':
                        case 'i':
                            Ypos--;
                            break;

                        case 'K':
                        case 'k':
                            Ypos++;
                            break;

                        case (char)27:
                            _threadHandle.Suspend();
                            Console.CursorTop = _totalHeight / 2;
                            int _cPos = (_totalWidth / 2) - (ARE_YOU_SURE.Length / 2);
                            Console.CursorLeft = _cPos;
                            Console.Write(ARE_YOU_SURE);
                            _areYouSure = true;
                            continue;

                        //default:
                        //    _currentPiece.setPiece(_nextPiece.PieceInt);
                        //    _nextPiece.randomPiece();
                        //    break;
                    }

                    bool _hit = _screen.CollisionDetect(Xpos, Ypos, _currentPiece);

                    DrawScreen();

                    Console.CursorLeft = _offset * 2 + _offset / 2;
                    Console.CursorTop = 0;
                    Console.Write(Xpos);
                    Console.CursorTop = 1;
                    Console.Write(Ypos);
                }

                _charBuff = Console.ReadKey(true).KeyChar;
            }
        }
    }
}
