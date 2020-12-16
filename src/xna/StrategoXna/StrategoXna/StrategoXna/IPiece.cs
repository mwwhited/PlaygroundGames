using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace StrategoXna
{
    public interface IPiece : IContentManagement, IDrawable, INeedPlayer, IMove
    {
        string TextureName { get; }
        int PieceIndex { get; }

        PieceTypes PieceType { get; }
        AttackResults Attack(IPiece piece);
        int RangeMove { get; }
    }
}
