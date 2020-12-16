using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;

namespace testTetris
{
    public delegate void Score(int rowsScored);

    public class Screen
    {
        public event Score CountScore;

        public Screen(int rows, int columns)
        {
            _rows = rows;
            _columns = columns;

            for (int i = 0; i < _rows; i++)
                _lines.Add(NewLine());
        }

        private int _rows;
        private int _columns;

        private List<byte[]> _lines = new List<byte[]>();

        private byte[] NewLine()
        {
            byte[] _newRow = new byte[_columns];
            for (int i = 0; i < _columns; i++)
                _newRow[i] = 0xFF;
            return _newRow;
        }

        private bool FullRow(byte[] line)
        {
            if (line == null)
                return false;

            foreach (byte _pos in line)
                if (_pos == 0xff)
                    return false;

            return true;
        }

        public bool CanLeft(int x, int y, Tetris piece)
        {
            if (x <= 0)
                return false;

            int yp, xp;
            int _leftMost = _columns;
            for (int xi = 0; xi <= 3; xi++)
            {
                for (int yi = 0; yi <= 3 ; yi++)
                {
                    yp = y + yi;
                    xp = x + xi;
                    if (piece.Piece[xi + yi * 4])
                    {
                        if (x < _leftMost)
                            _leftMost = x;

                        if (_lines[yp][xp] != 0xff)
                            return false;
                    }
                }
            }
            if (_leftMost + x < 0)
                return false;

            return true;
        }

        public bool CanRight(int x, int y, Tetris piece)
        {
            if (x >= _columns)
                return false;

            int yp, xp;
            int _rightMost = _columns;
            for (int xi = 0; xi <= 3; xi++)
            {
                for (int yi = 0; yi <= 3; yi++)
                {
                    yp = y + yi;
                    xp = x + xi - 1;
                    if (piece.Piece[xi + yi * 4])
                    {
                        if (x > _rightMost)
                            _rightMost = x;

                        if (_lines[yp][xp] != 0xff)
                            return false;
                    }
                }
            }
            if (_rightMost - x >= _columns)
                return false;

            return true;
        }

        private bool CanDrop(int x, int y, Tetris piece)
        {
            if (y >= _rows)
                return false;

            //Add new empty rows to the top of the screen
            for (int i = 0; i < _lines.Count - _rows; i++)
                _lines.Insert(0, NewLine());

            int yp = 0;
            int xp = 0;

            for (int iy = 0; iy <= 3; iy++)
            {
                for (int ix = 0; ix <= 3; ix++)
                {
                    if (piece.Piece[ix + iy * 4])
                    {
                        yp = y + iy + 1;
                        xp = x + ix;
                        if (yp >= _rows -1)
                            return false;

                        if (_lines.Count > yp ||
                            _lines[yp].Length > xp)
                        {
                            if (_lines[yp][xp] != 0xff)
                                return false;
                        }
                    }
                }
            }
            return true;
/*
Dim x, y, yOff As Integer
For y = 0 To 3
    yOff = y * 4
    For x = 0 To 3
        If (piece(yOff + x) = True) Then
            If (Me.loc.Y + y >= 19) Then
                Return False
            End If
            If (board((Me.loc.Y + y + 1) * 10 + Me.loc.X + x).Visible = True) Then
                Return False
            End If
        End If
    Next
Next
Return True
*/
        }

        public bool CollisionDetect(int x, int y, Tetris piece)
        {
            bool _returnValue = false;

            //Check bounds
            if (y > _columns || x > _rows || y < 0 || x < 0)
                _returnValue = true;

            //check can drop
            _returnValue = !CanDrop(x, y, piece);

            if (_returnValue)
            {
                for (int yi = 0; yi <= 3; yi++)
                {
                    for (int xi = 0; xi <= 3; xi++)
                    {
                        if (piece.Piece[xi + yi * 4])
                        {
                            _lines[y + yi][x + xi] = (byte)piece.Character;
                        }
                    }
                }
            }

            //check completed rows
            int _completedRows = 0;
            for (int i = 0; i < _lines.Count; i++)
            {
                if (FullRow(_lines[i]))
                {
                    _completedRows++;
                    _lines.RemoveAt(i);
                }
            }
            if (_completedRows > 0 && CountScore != null)
                CountScore(_completedRows);

            //Add new empty rows to the top of the screen
            for (int i = 0; i < _lines.Count - _rows; i++)
                _lines.Insert(0, NewLine());

            return _returnValue;
        }

        public ReadOnlyCollection<byte[]> Lines { get { return _lines.AsReadOnly(); } }
    }
}
