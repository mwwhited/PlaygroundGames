using System;
using System.Collections.Generic;
using System.Text;

namespace tetrisagain
{
    public class LineScoreArgs
    {
        public LineScoreArgs(int completedLines)
        {
            _completedLines = completedLines;
        }

        private int _completedLines = 0;
        public int CompletedLines { get { return _completedLines; } }
    }
}
