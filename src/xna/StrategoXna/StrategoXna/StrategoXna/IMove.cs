using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace StrategoXna
{
    public interface IMove
    {
        bool CanMove { get; }
        Rectangle Position { get; }
        void SetPosition(int x, int y);
    }
}
