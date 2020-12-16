using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScrollerEngineData
{
    public class Randomizer
    {
        private static Random rand = new Random((int)DateTime.Now.Ticks);
        public static Random Value { get { return rand; } }

        /// <summary>
        /// will return a random value between -1.0 to 1.0
        /// </summary>
        public static float Range
        {
            get { return (float)(rand.NextDouble() * 2) - 1; }
        }

        /// <summary>
        /// will return a random value between 0.0 to 1.0
        /// </summary>
        public static float RangeShort
        {
            get { return (float)rand.NextDouble(); }
        }
    }
}
