using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;

namespace BYBFSideScrollerData
{
    public enum CellType : byte
    {
        Empty = 0,

        Back = 1,
        Forward = 2,

        Box = 3,
        BoxBreakable = 4
    }
}
