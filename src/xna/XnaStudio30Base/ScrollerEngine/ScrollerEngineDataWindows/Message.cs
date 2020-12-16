using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScrollerEngineData
{
    public struct Message
    {
        public DateTime Expiry;
        public string Text;

        public override string ToString()
        {
            return Text;
        }
    }
}
