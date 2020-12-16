using System;
using System.Collections.Generic;
using System.Text;

namespace fractals
{
    public class die
    {
        private int _faces;
        private int _lastRoll;
        Random rand = new Random();

        public die(int faces)
        {
            _faces = faces;
        }

        public int roll()
        {
            _lastRoll = Convert.ToInt16(rand.Next(1, _faces + 1));
            return _lastRoll;
        }

        public int getFaces()
        {
            return _faces;
        }
    }
}
