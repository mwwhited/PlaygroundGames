///////////////////////////////////////////////////////////////////////
// Project: XNA Quake3 Lib
// Author: Aanand Narayanan
// Copyright (c) 2006-2007 All rights reserved
///////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace XNAQ3Lib
{
    public class Q3BSPLogger
    {
        private StreamWriter sw = null;

        public Q3BSPLogger(string fileName)
        {
            sw = new StreamWriter(fileName, false, Encoding.ASCII);
            sw.WriteLine("Logging started - - - - - - -");
        }

        public void WriteLine(string oneLine)
        {
            if (null != sw)
            {
                sw.WriteLine(oneLine);
            }
        }
    }
}
