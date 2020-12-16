using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;
using System.Diagnostics;

namespace AlphaMapTest
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Texture2D level;
        Texture2D tiny;
        Vector2 position;
        Color back = Color.CornflowerBlue;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            level = this.Content.Load<Texture2D>("Level002");
            tiny = this.Content.Load<Texture2D>("TinyDude");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            //alphaLevel.Dispose();
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            var currentPad = GamePad.GetState(PlayerIndex.One);

            // Allows the game to exit
            if (currentPad.Buttons.Back == ButtonState.Pressed)
                this.Exit();

            back = IsCollision() ? Color.Yellow : Color.Green;

            position += currentPad.ThumbSticks.Left * new Vector2(1, -1) * 5;

            position.X = MathHelper.Clamp(position.X, 0, this.GraphicsDevice.Viewport.TitleSafeArea.Width);
            position.Y = MathHelper.Clamp(position.Y, 0, this.GraphicsDevice.Viewport.TitleSafeArea.Height);

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        private Rectangle GetTinysBounds()
        {
            return new Rectangle(
                    (int)(position.X - tiny.Width / 2),
                    (int)(position.Y - tiny.Height / 2),
                    tiny.Width,
                    tiny.Height
                    );
        }

        private bool IsCollision()
        {
            Rectangle tinyBounds = GetTinysBounds();
            Rectangle levelBounds = this.GraphicsDevice.Viewport.TitleSafeArea;

            if (tinyBounds.X + tinyBounds.Width < 0 ||  //Left of the game area
                tinyBounds.X > levelBounds.Width ||     //Right of the game area
                tinyBounds.Y + tinyBounds.Height < 0 || //Above the game area
                tinyBounds.Y > levelBounds.Height)      //Below the game area
                return true; //might even want an error

            Rectangle intersect;
            Rectangle.Intersect(ref tinyBounds, ref levelBounds, out intersect);

            if (tinyBounds.X < 0)
            {
                tinyBounds.X = tinyBounds.Width - intersect.Width;
                tinyBounds.Width = intersect.Width;
            }
            else if (tinyBounds.X + tinyBounds.Width > levelBounds.Width)
                tinyBounds.Width = intersect.Width;
            else
                tinyBounds.X = 0;

            if (tinyBounds.Y < 0)
            {
                tinyBounds.Y = tinyBounds.Height - intersect.Height;
                tinyBounds.Height = intersect.Height;
            }
            else if (tinyBounds.Y + tinyBounds.Height > levelBounds.Height)
                tinyBounds.Height = intersect.Height;
            else
                tinyBounds.Y = 0;


            var levelPixels = new Color[intersect.Height * intersect.Width];
            var tinyPixels = new Color[intersect.Height * intersect.Width];

            tiny.GetData(0, tinyBounds, tinyPixels, 0, tinyPixels.Length);
            level.GetData(0, intersect, levelPixels, 0, levelPixels.Length);

            for (int i = 0; i < tinyPixels.Length; i++)
            {
                if (tinyPixels[i].A < 25 || levelPixels[i].A < 50)
                    continue;

                if (i != lastIntersect)
                {
                    Debug.WriteLine(string.Format("{0}: t:{1} l:{2}", i, tinyPixels[i], levelPixels[i]));
                    Debug.WriteLine(string.Format("intersect: {0}", intersect));
                    Debug.WriteLine(string.Format("tinyBounds: {0}", tinyBounds));
                    Debug.WriteLine(string.Format("levelBounds: {0}", levelBounds));
                }
                lastIntersect = i;
                return true;
            }

            return false;
        }
        int lastIntersect = 0;

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(back);
            spriteBatch.Begin();

            spriteBatch.Draw(tiny, GetTinysBounds(), Color.White);
            spriteBatch.Draw(level, this.GraphicsDevice.Viewport.TitleSafeArea, Color.White);

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
