using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace StrategoXna
{
    public class GameBoard : IContentManagement, IDrawable
    {
        public GameBoard()
        {
        }

        public void Draw(Game game, GameTime gameTime, SpriteBatch spriteBatch)
        {
            //TODO: Draw Background
        }

        public void Load(Game game)
        {
        }

        public void Unload(Game game)
        {
        }
    }
}
