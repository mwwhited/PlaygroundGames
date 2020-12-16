using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System.Diagnostics;

namespace ScrollerEngineData
{
    public abstract class ViewableGameObject<T> : GameObject<T>
    {
        public string TextureName;

        [ContentSerializerIgnore]
        public Texture2D Texture;

        public virtual void LoadContent(ContentManager contentManager)
        {
            Debug.WriteLine(string.Format("{0}: Loading \"{1}\"", this, TextureName));
            Texture = contentManager.Load<Texture2D>(TextureName);
            Bounds = new Rectangle(0, 0, Texture.Width, Texture.Height);
        }

        public virtual void UnloadContent()
        {
        }

        public virtual void Draw(SpriteBatch spriteBatch, Color color)
        {
            GameObjectUtilities.Draw(spriteBatch, color, Texture, Bounds);
        }

        public virtual void Update(GameTime gameTime, GameEntry gameEntry, Level level)
        {
        }
    }
}
