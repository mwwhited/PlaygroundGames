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

namespace JumpGravity
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        GamePadState lastPad;
        Texture2D dude;
        Vector2 dudePosition;
        Vector2 dudeVelocity;
        Vector2 gravityWind = new Vector2(0f, .5f);
        float jumpSpeed = 100;
        float runFactor = 20;

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

            dude = this.Content.Load<Texture2D>("LittleDude");
            dudePosition.Y = int.MaxValue;

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            float speedScale = (gameTime.ElapsedGameTime.Milliseconds * .01f);

            GamePadState currentPad = GamePad.GetState(PlayerIndex.One);

            float runSpeed = runFactor * (currentPad.Triggers.Left + 1);

            // Allows the game to exit
            if (currentPad.Buttons.Back == ButtonState.Pressed)
                this.Exit();

            if (dudePosition.Y >= GraphicsDevice.Viewport.TitleSafeArea.Height - dude.Height)
            {
                dudePosition.X += currentPad.ThumbSticks.Left.X * speedScale * runSpeed;
                if (currentPad.Buttons.A == ButtonState.Pressed &&
                    lastPad.Buttons.A == ButtonState.Released)
                {
                    dudeVelocity.X += currentPad.ThumbSticks.Left.X * runSpeed;
                    dudeVelocity.Y -= jumpSpeed;
                }
            }

            dudeVelocity += gravityWind;
            dudePosition += dudeVelocity * speedScale;

            dudePosition = ClampVector(dudePosition, new Vector2(0, 0),
                new Vector2(
                    GraphicsDevice.Viewport.TitleSafeArea.Width - dude.Width,
                    GraphicsDevice.Viewport.TitleSafeArea.Height - dude.Height
                    ));

            if (dudePosition.X <= 0 ||
                dudePosition.X >= GraphicsDevice.Viewport.TitleSafeArea.Width - dude.Width)
            {
                dudeVelocity.X = 0;
            }

            if (dudePosition.Y <= 0 ||
                dudePosition.Y >= GraphicsDevice.Viewport.TitleSafeArea.Height - dude.Height)
            {
                dudeVelocity.Y = 0;
                dudeVelocity.X = 0;
            }

            if (currentPad.Buttons.B == ButtonState.Pressed && lastPad.Buttons.B == ButtonState.Released)
            {
                Debug.WriteLine(string.Format("gravityWind: {0}", gravityWind));
                Debug.WriteLine(string.Format("dudeVelocity: {0}", dudeVelocity));
                Debug.WriteLine(string.Format("dudePosition: {0}", dudePosition));
            }
            
            lastPad = currentPad;
            
            base.Update(gameTime);
        }

        public Vector2 ClampVector(Vector2 position, Vector2 min, Vector2 max)
        {
            position.X = MathHelper.Clamp(position.X, min.X, max.X);
            position.Y = MathHelper.Clamp(position.Y, min.Y, max.Y);
            return position;
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();

            spriteBatch.Draw(dude, dudePosition, Color.White);

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
