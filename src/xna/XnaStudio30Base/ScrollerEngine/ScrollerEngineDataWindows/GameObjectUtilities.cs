using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace ScrollerEngineData
{
    public static class GameObjectUtilities
    {
        public static void Draw(SpriteBatch spriteBatch, Color color, Texture2D texture, Rectangle bounds)
        {
            spriteBatch.Draw(texture, bounds, color);
        }

        public static Vector2 GetXY(this Rectangle rectangle)
        {
            return new Vector2(rectangle.X, rectangle.Y);
        }

        public static void MoveHere(this Vector2 newXY, ref Rectangle rectangle)
        {
            rectangle.X = (int)newXY.X;
            rectangle.Y = (int)newXY.Y;
        }

        public static bool IsAlphaCollision(
            Rectangle characterBounds, Texture2D characterTexture,
            Rectangle levelBounds, Texture2D levelTexture
            )
        {
            Rectangle intersect;
            Rectangle.Intersect(ref characterBounds, ref levelBounds, out intersect);

            if (characterBounds.X < 0)
            {
                characterBounds.X = characterBounds.Width - intersect.Width;
                characterBounds.Width = intersect.Width;
            }
            else if (characterBounds.X + characterBounds.Width > levelBounds.Width)
            {
                characterBounds.X = 0;
                characterBounds.Width = intersect.Width;
            }
            else
                characterBounds.X = 0;

            if (characterBounds.Y < 0)
            {
                characterBounds.Y = characterBounds.Height - intersect.Height;
                characterBounds.Height = intersect.Height;
            }
            else if (characterBounds.Y + characterBounds.Height > levelBounds.Height)
            {
                characterBounds.Y = 0;
                characterBounds.Height = intersect.Height;
            }
            else
                characterBounds.Y = 0;

            var levelPixels = new Color[intersect.Height * intersect.Width];
            var tinyPixels = new Color[intersect.Height * intersect.Width];

            characterTexture.GetData(0, characterBounds, tinyPixels, 0, tinyPixels.Length);
            levelTexture.GetData(0, intersect, levelPixels, 0, levelPixels.Length);

            for (int i = 0; i < tinyPixels.Length; i++)
            {
                if (Math.Min(tinyPixels[i].A, levelPixels[i].A) < 25)
                    continue;
                return true;
            }

            return false;
        }
    }
}
