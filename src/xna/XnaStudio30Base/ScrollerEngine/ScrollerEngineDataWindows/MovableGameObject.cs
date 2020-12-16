using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ScrollerEngineData
{
    public abstract class MovableGameObject<T> : ViewableGameObject<T>
        where T : MovableGameObject<T>, new()
    {
        [ContentSerializerIgnore]
        protected Rectangle RealBounds;

        [ContentSerializerIgnore]
        public override Rectangle Bounds
        {
            get
            {
                if (Texture == null)
                    return new Rectangle();

                return new Rectangle(
                    (int)(Position.X), // - Texture.Width / 2
                    (int)(Position.Y), // - Texture.Height / 2
                    Texture.Width,
                    Texture.Height
                    );
            }
            set { }
        }

        [ContentSerializerIgnore]
        public Vector2 Position;

        [ContentSerializerIgnore]
        protected Vector2 Velocity;

        public float jumpSpeed { get; set; } //= 20;
        public float runFactor = 5;
        public float superPower = 2;
        public float highStep = 3;

        protected virtual bool IsCollision(Level level)
        {
            if (this.Bounds.X < 0 - RealBounds.X ||    //Left of the game area
                this.Bounds.X + this.RealBounds.Width + Bounds.Width > level.Bounds.Width ||            //Right of the game area
                this.Bounds.Y < 0 - RealBounds.Y ||   //Above the game area
                this.Bounds.Y + this.Bounds.Height + RealBounds.Height > level.Bounds.Height)             //Below the game area
            {
                return true; //might even want an error
            }

            return GameObjectUtilities.IsAlphaCollision(Bounds, Texture, level.Bounds, level.Texture);
        }

        public override void LoadContent(ContentManager contentManager)
        {
            base.LoadContent(contentManager);

            var allPixels = new Color[Texture.Width * Texture.Height];
            Texture.GetData<Color>(allPixels);

            RealBounds = new Rectangle(Texture.Width, Texture.Height, 0, 0);

            for (int x = 0; x < Texture.Width; x++)
                for (int y = 0; y < Texture.Height; y++)
                    if (allPixels[x + (y * Texture.Width)].A != 0)
                    {
                        RealBounds.X = Math.Min(RealBounds.X, x);
                        RealBounds.Width = Math.Max(RealBounds.Width, x);
                        RealBounds.Y = Math.Min(RealBounds.Y, y);
                        RealBounds.Height = Math.Max(RealBounds.Height, y);
                    }

            RealBounds.Width -= Texture.Width;
            RealBounds.Height -= Texture.Height;
        }

        public virtual void Update(GameTime gameTime, GameEntry gameEntry, Level level,
            float run, float moveX, bool jump, bool jumpPower)
        {
            float speedScale = (gameTime.ElapsedGameTime.Milliseconds * .01f);
            float runSpeed = runFactor * (run + 1);

            var lastPos = Position;

            Velocity += level.GravityWind;
            Position += Velocity * speedScale;

            if (IsCollision(level))
            {
                Position = lastPos;
                Velocity.X = 0;
                Velocity.Y = 0;

                Position.X += moveX * speedScale * runSpeed;
                if (IsCollision(level))
                {
                    Position.Y = (int)Position.Y - highStep;
                    if (IsCollision(level))
                        Position = lastPos;
                    else
                    {
                        while (IsCollision(level))
                            Position.Y += 1;
                    }
                }

                if (jump)
                {
                    Velocity.X += moveX * runSpeed;
                    Velocity.Y -= jumpSpeed;
                }
                else if (jumpPower)
                {
                    Velocity.X += moveX * runSpeed * superPower;
                    Velocity.Y -= jumpSpeed * superPower;
                }
            }
            else
            {
                Position.X += moveX * speedScale;
            }
        }

        public override object Clone()
        {
            var newCharacter = new T();
            newCharacter.TextureName = this.TextureName;
            newCharacter.jumpSpeed = this.jumpSpeed;
            newCharacter.runFactor = this.runFactor;
            newCharacter.superPower = this.superPower;

            newCharacter.Texture = this.Texture;
            newCharacter.RealBounds = this.RealBounds;

            return newCharacter;
        }
    }
}
