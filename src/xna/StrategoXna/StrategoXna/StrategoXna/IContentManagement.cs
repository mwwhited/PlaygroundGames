using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace StrategoXna
{
    public interface IContentManagement
    {
        void Load(Game game);
        void Unload(Game game);
    }
}
