using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace tetrisagain
{
    public class Board
    {
        public event LineScore Score; //(object sender);

        public const int BOARD_WIDTH = 16;
        public const int BOARD_LENGTH = 20;

        private Random _rand = new Random((int)DateTime.Now.Ticks);
        private List<ushort> _lines = new List<ushort>();

        private bool CollisionDetect(int x, int y, ushort piece)
        {
            int _height = Piece.GetHeight(piece);
            int _width = Piece.GetWidth(piece);

            if (y > Lines.Count - _height || y < 0)
                return false;

            if (x + _width > BOARD_WIDTH || x < 0)
                return false;

            ushort[] _parts = new ushort[]
            {
                (ushort)(((piece & 0xF000) >> 12) >> (4 - _width)),
                (ushort)(((piece & 0x0F00) >> 08) >> (4 - _width)),
                (ushort)(((piece & 0x00F0) >> 04) >> (4 - _width)),
                (ushort)(((piece & 0x000F) >> 00) >> (4 - _width))
            };

            int _lineMaskOffset = BOARD_WIDTH - x - _width;

            List<ushort> _selectedLines = _lines.GetRange(y, _height);
            Debug.WriteLine("");
            for (ushort i = 0; i < _selectedLines.Count; i++)
            {
                ushort _maskedLine = (ushort)((_parts[i]) << _lineMaskOffset);
                Debug.WriteLine(
                    Convert.ToString(_maskedLine, 2).PadLeft(BOARD_WIDTH, '_') +
                    " | " +
                    Convert.ToString(_selectedLines[i], 2).PadLeft(BOARD_WIDTH, '_')
                    );
                ushort _line = (ushort)((_selectedLines[i] ^ _maskedLine) & _maskedLine);

                if (_line != _maskedLine)
                    return false;
            }
            return true;
        }

        private void CountLines()
        {
            if (Score == null)
                return;

            int _completedLines = 0;

            for (int i = 0; i < _lines.Count; i++)
                if (_lines[i] == 0xffff)
                    _lines.RemoveAt(i);

            if (_completedLines > 0)
                Score(this, new LineScoreArgs(_completedLines));
        }

        public List<ushort> Lines
        {
            get
            {
                while (_lines.Count < BOARD_LENGTH)
                    _lines.Insert(0, 0);
                return _lines; //.AsReadOnly();
            }
        }

        public bool CanDrop(int x, int y, ushort piece)
        {
            y++;
            return CollisionDetect(x, y, piece);
        }

        public bool CanRight(int x, int y, ushort piece)
        {
            x++;
            return CollisionDetect(x, y, piece);
        }

        public bool CanLeft(int x, int y, ushort piece)
        {
            x--;
            return CollisionDetect(x, y, piece);
        }

        public bool CanRotateLeft(int x, int y, ushort piece)
        {
            piece = Piece.RotateLeft(piece);
            return CollisionDetect(x, y, piece);
        }

        public bool CanRotateRight(int x, int y, ushort piece)
        {
            piece = Piece.RotateRight(piece);
            return CollisionDetect(x, y, piece);
        }
        
        public void MergePiece(int x, int y, ushort piece)
        {
            int _height = Piece.GetHeight(piece);
            int _width = Piece.GetWidth(piece);

            ushort[] _parts = new ushort[]
            {
                (ushort)(((piece & 0xF000) >> 12) >> (4 - _width)),
                (ushort)(((piece & 0x0F00) >> 08) >> (4 - _width)),
                (ushort)(((piece & 0x00F0) >> 04) >> (4 - _width)),
                (ushort)(((piece & 0x000F) >> 00) >> (4 - _width))
            };

            int _lineMaskOffset = BOARD_WIDTH - x - _width;

            List<ushort> _selectedLines = _lines.GetRange(y, _height);
            Debug.WriteLine("");
            for (ushort i = 0; i < _selectedLines.Count; i++)
            {
                ushort _maskedLine = (ushort)((_parts[i]) << _lineMaskOffset);
                Debug.WriteLine(
                    Convert.ToString(_maskedLine, 2).PadLeft(BOARD_WIDTH, '_') +
                    " | " +
                    Convert.ToString(_selectedLines[i], 2).PadLeft(BOARD_WIDTH, '_') +
                    " | " +
                    Convert.ToString((_maskedLine | _selectedLines[i]), 2).PadLeft(BOARD_WIDTH, '_')
                    );
                ushort _line = (ushort)((_selectedLines[i] ^ _maskedLine) & _maskedLine);

                _lines[y + i] = (ushort)(_maskedLine | _selectedLines[i]);
            }
            CountLines();
        }

        public void BuildRandomBoard(int maxLines)
        {
            int fillLines = Math.Max(Math.Min(Lines.Count, Lines.Count - maxLines), 0);
            for (int i = fillLines; i < Lines.Count; i++)
            {
                Lines[i] = (ushort)_rand.Next(0, ushort.MaxValue);
                Debug.WriteLine(Convert.ToString(Lines[i], 2).PadLeft(BOARD_WIDTH, '0'));
            }
        }
    }        
}
