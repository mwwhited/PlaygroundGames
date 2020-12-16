using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace StrategoXna
{
    public class Cursor : ICursor
    {
        private string TextureName { get; set; }
        private Texture2D Texture { get; set; }
        public IPlayer Player { get; private set; }
        
        public int X { get; private set; }
        public int Y { get; private set; }
        
        public Rectangle Position
        {
            get
            {
                return new Rectangle(this.X,
                                     this.Y,
                                     Globals.TileSize,
                                     Globals.TileSize
                                     );
            }
        }

        public Cursor(IPlayer player)
        {
            this.TextureName = "Pointer";
            this.Player = player;
        }

        public void Draw(Game game, GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.Texture, this.Position, this.Color);
        }

        public void Load(Game game)
        {
            this.Texture = game.Content.Load<Texture2D>(this.TextureName);
        }

        public void Unload(Game game)
        {
        }

        private Color Color
        {
            get
            {
                switch (this.Player.PlayerIndex)
                {
                    default:
                    case PlayerIndex.One:
                        return Color.Red;

                    case PlayerIndex.Two:
                        return Color.Blue;

                    case PlayerIndex.Three:
                        return Color.Yellow;

                    case PlayerIndex.Four:
                        return Color.Green;
                }
            }
        }

        public bool CanMove
        {
            get { throw new NotImplementedException(); }
        }

        public void SetPosition(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }
    }
}
