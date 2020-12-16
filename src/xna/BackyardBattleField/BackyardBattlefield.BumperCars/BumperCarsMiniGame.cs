using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;
using BackyardBattleField.Common;
using System.Diagnostics;

namespace BackyardBattlefield.BumperCars
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class BumperCarsMiniGame : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Vector3 cameraOffset = new Vector3(0.0f, 100.0f, -100.0f);
        ChaseCamera camera;
        KeyboardState lastKeyboardState;
        GamePadState lastGamePadState;
        SpriteFont spriteFont;

        LivingGameObject rubberDucky;
        LivingGameObject ship;
        LivingGameObject bsd;
        GameObject terrain;

        public BumperCarsMiniGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            camera = new ChaseCamera();

            // Set the camera offsets
            camera.DesiredPositionOffset = new Vector3(0.0f, 2000.0f, 3500.0f);
            camera.LookAtOffset = new Vector3(0.0f, 150.0f, 0.0f);

            // Set camera perspective
            camera.NearPlaneDistance = 10.0f;
            camera.FarPlaneDistance = 100000.0f;
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
            //VideoHelper.SetHighestResolution(this, graphics);
            
            base.Initialize();

            // Set the camera aspect ratio
            // This must be done after the class to base.Initalize() which will
            // initialize the graphics device.
            camera.AspectRatio = (float)graphics.GraphicsDevice.Viewport.Width /
                graphics.GraphicsDevice.Viewport.Height;

            // Perform an inital reset on the camera so that it starts at the resting
            // position. If we don't do this, the camera will start at the origin and
            // race across the world to get behind the chased object.
            // This is performed here because the aspect ratio is needed by Reset.
            //UpdateCameraChaseTarget();
            camera.Reset();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            //CameraObject.CreateDefaultCamera(graphics);

            spriteFont = Content.Load<SpriteFont>("Arial");

            rubberDucky = new LivingGameObject(Content.Load<Model>("Models\\ducky_highres"), 2000.0f);
            rubberDucky.Mass = 0.001f;
            //rubberDucky.DirectionOffset = Vector3.Backward;
            ship = new LivingGameObject(Content.Load<Model>("ship"), 1f);
            bsd = new LivingGameObject(Content.Load<Model>("bsd"), 10f);
            terrain = new GameObject(Content.Load<Model>("Terrian\\Ground"));

            //camera.Target = ship;

            camera.AddObject(rubberDucky);
            camera.AddObject(ship);
            camera.AddObject(bsd);
            camera.AddObject(terrain);

            camera.NextTarget();

            //BoundingSphere duckBounds = rubberDucky.Bounds;
            //while (true)
            //{
            //    ContainmentType boundResult = ContainmentType.Contains;
            //    terrain.Bounds.Contains(ref duckBounds, out boundResult);
            //    if (boundResult == ContainmentType.Contains)
            //        break;
            //    rubberDucky.Position.Y += 1.0f;
            //}

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
            KeyboardState currentKeyboardState = Keyboard.GetState();
            GamePadState currentGamePadState = GamePad.GetState(PlayerIndex.One);

            // Allows the game to exit
            if (currentGamePadState.Buttons.Back == ButtonState.Pressed || currentKeyboardState.IsKeyDown(Keys.Escape))
                this.Exit();

            // Pressing the A button or key toggles the spring behavior on and off
            if (lastKeyboardState.IsKeyUp(Keys.A) && (currentKeyboardState.IsKeyDown(Keys.A)) ||
                (lastGamePadState.Buttons.A == ButtonState.Released && currentGamePadState.Buttons.A == ButtonState.Pressed))
                camera.SpringEnabled = !camera.SpringEnabled;

            if (lastKeyboardState.IsKeyUp(Keys.B) && (currentKeyboardState.IsKeyDown(Keys.B)) ||
                (lastGamePadState.Buttons.B == ButtonState.Released && currentGamePadState.Buttons.B == ButtonState.Pressed))
                camera.NextTarget();

            if (lastKeyboardState.IsKeyUp(Keys.X) && (currentKeyboardState.IsKeyDown(Keys.X)) ||
                (lastGamePadState.Buttons.X == ButtonState.Released && currentGamePadState.Buttons.X == ButtonState.Pressed))
            {
                foreach (GameObject var in camera.Objects)
                    if (var is LivingGameObject)
                        ((LivingGameObject)var).Reset();
                camera.Reset();
            }

            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;

            // TODO: Add your update logic here

            //Vector3 lastPosition = camera.Target.Position;
            //Vector3 lastDirection = camera.Target.Direction;

            (camera.Target as LivingGameObject).Update(
                gameTime, 
                -currentGamePadState.ThumbSticks.Left,
                currentGamePadState.ThumbSticks.Right.X,
                currentGamePadState.Triggers.Left - currentGamePadState.Triggers.Right
                );
            camera.Update(gameTime);

            foreach (GameObject gameObject in camera.Objects)
                if (gameObject is LivingGameObject)
                    (gameObject as LivingGameObject).Update(gameTime, Vector2.Zero, 0f, 0f);

            //BoundingSphere duckBounds = rubberDucky.Bounds;
            //ContainmentType boundResult = ContainmentType.Contains;
            //terrain.Bounds.Contains(ref duckBounds, out boundResult);

            //if (boundResult != ContainmentType.Contains || rubberDucky.Position.Y< 0)
            //{
            //    GamePad.SetVibration(PlayerIndex.One, 1, 1);
            //    rubberDucky.Position = lastPosition;
            //    rubberDucky.Direction = lastDirection;
            //}
            //else
            //    GamePad.SetVibration(PlayerIndex.One, 0, 0);

            //if (rubberDucky.Position != lastPosition)
            //{
            //    Debug.Write(rubberDucky.Position);
            //    Debug.Write(" ");
            //    Debug.WriteLine(rubberDucky.Velocity);
            //}

            lastGamePadState = currentGamePadState;
            lastKeyboardState = currentKeyboardState;

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            graphics.GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            camera.Draw();
            DrawOverlayText();

            base.Draw(gameTime);
        }


        private void DrawOverlayText()
        {
            spriteBatch.Begin(SpriteBlendMode.AlphaBlend, SpriteSortMode.Deferred, SaveStateMode.SaveState);

            string text = "Right Trigger or Spacebar = thrust\n" +
                          "Left Thumb Stick\n" + // or Arrow keys = steer\n" +
                          "A = toggle camera spring (" + (camera.SpringEnabled ? "on" : "off") + ")\n" +
                          string.Format("Target  Position:  {0}\r\n", camera.Target.Position) +
                          string.Format("Target  Direction: {0}\r\n", camera.Target.Direction) +
                          string.Format("Chase   Direction: {0}\r\n", camera.ChasePosition) +
                          string.Format("Chase   Position:  {0}\r\n", camera.ChaseDirection) +
                          string.Format("Desired Position:  {0}\r\n", camera.DesiredPosition) +
                          string.Format("Desired Position:  {0} (offset)\r\n", camera.DesiredPositionOffset) +
                          string.Format("Look at Position:  {0}\r\n", camera.LookAt) +
                          string.Format("Look at Position:  {0} (offset)\r\n", camera.LookAtOffset) +
                          string.Format("Camera  Position:  {0} (offset)\r\n", camera.Position) +
                          string.Format("Camera  Velocity:  {0} (offset)\r\n", camera.Velocity) +
                          "";

            // Draw the string twice to create a drop shadow, first colored black
            // and offset one pixel to the bottom right, then again in white at the
            // intended position. This makes text easier to read over the background.
            spriteBatch.DrawString(spriteFont, text, new Vector2(65, 65), Color.Black);
            spriteBatch.DrawString(spriteFont, text, new Vector2(64, 64), Color.White);

            spriteBatch.End();
        }
    }
}
