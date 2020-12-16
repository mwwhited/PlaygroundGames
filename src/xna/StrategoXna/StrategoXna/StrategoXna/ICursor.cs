using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StrategoXna
{
    public interface ICursor : IDrawable, IContentManagement, INeedPlayer, IMove
    {
    }
}
