using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace StrategoXna
{
    public interface IPlayer : IContentManagement, IHaveCursor, IDrawable
    {
        PlayerIndex PlayerIndex { get; }
        IEnumerable<IPiece> Pieces { get; }
    }
}
