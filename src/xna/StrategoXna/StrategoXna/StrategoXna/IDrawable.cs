using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace StrategoXna
{
    public interface IDrawable
    {
        void Draw(Game game, GameTime gameTime, SpriteBatch spriteBatch);
    }
}
