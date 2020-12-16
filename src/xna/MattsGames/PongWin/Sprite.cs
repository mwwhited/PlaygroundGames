using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace PongWin
{
    public class Sprite
    {
        public Sprite(Texture2D texture, Rectangle sourceRect)
        {
            _texture = texture;
            _rectangle = sourceRect;
        }

        private Texture2D _texture = null;
        public Texture2D Texture { get { return _texture; } }

        private Rectangle _rectangle = new Rectangle();
        public Rectangle SourceRectangle { get { return _rectangle; } }

        public Vector2 Size { get { return new Vector2(_rectangle.Width, _rectangle.Height); } }
    }
}
