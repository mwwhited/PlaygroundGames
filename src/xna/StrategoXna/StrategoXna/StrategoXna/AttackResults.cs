using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StrategoXna
{
    public enum AttackResults
    {
        Loss = int.MinValue,
        Tie = 0,
        Win = 1,

        Flag = int.MaxValue,
    }
}
