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

namespace BackgroundTest
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Texture2D back;
        Texture2D middle;
        Texture2D front;

        Rectangle backPlane;
        Rectangle middlePlane;
        Rectangle frontPlane;

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

            back = this.Content.Load<Texture2D>("BackDrop");
            middle = this.Content.Load<Texture2D>("MiddleDrop");
            front = this.Content.Load<Texture2D>("FrontDrop");

            float backScale = back.Height/this.GraphicsDevice.Viewport.TitleSafeArea.Height;
            float middleScale = middle.Height / this.GraphicsDevice.Viewport.TitleSafeArea.Height;

            float frontScale = front.Width / this.GraphicsDevice.Viewport.TitleSafeArea.Width;

            backPlane = new Rectangle(0, 0,
                (int)(back.Width / backScale),
                this.GraphicsDevice.Viewport.TitleSafeArea.Height);

            middlePlane = new Rectangle(0, 0,
                (int)(middle.Width / middleScale),
                this.GraphicsDevice.Viewport.TitleSafeArea.Height);

            frontPlane = new Rectangle(0, 0,
                this.GraphicsDevice.Viewport.TitleSafeArea.Width,
                (int)(front.Height / frontScale)); //,
                //this.GraphicsDevice.Viewport.TitleSafeArea.Height);


            backPlane.X = (this.GraphicsDevice.Viewport.TitleSafeArea.Width / 2) - (backPlane.Width / 2);
            middlePlane.X = (this.GraphicsDevice.Viewport.TitleSafeArea.Width / 2) - (middlePlane.Width / 2);
            frontPlane.X = (this.GraphicsDevice.Viewport.TitleSafeArea.Width / 2) - (frontPlane.Width / 2);

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
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            backPlane.X += (int)GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.X * 2;
            backPlane.X = (int)MathHelper.Clamp(backPlane.X, 0 - backPlane.Width + this.GraphicsDevice.Viewport.TitleSafeArea.Width, 0);

            if (backPlane.X > 0 - backPlane.Width + this.GraphicsDevice.Viewport.TitleSafeArea.Width && 
                backPlane.X < 0)
                middlePlane.X += (int)GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.X * 3;
            //middlePlane.X = (int)MathHelper.Clamp(middlePlane.X, 0 - middlePlane.Width + this.GraphicsDevice.Viewport.TitleSafeArea.Width, 0);

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();

            spriteBatch.Draw(back, backPlane, null, Color.White);
            spriteBatch.Draw(middle, middlePlane, null, Color.White);
            spriteBatch.Draw(front, frontPlane, null, Color.White);

            spriteBatch.End();            
            base.Draw(gameTime);
        }
    }
}
