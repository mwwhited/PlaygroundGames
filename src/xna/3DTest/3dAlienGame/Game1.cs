using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace _dAlienGame
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        const float KEYBOARD_SPEED = 0.05f;
        const float THUMBSTICK_SPEED = 0.05f;
        const int MISSILE_COUNT = 2000;
        const float LAUNCHER_MUZZLE_OFFSET = 25.0f;
        const float MISSILE_POWER = 50.0f;
        const float OUT_OF_BOUNDS = -6000.0f;
        const int ENEMY_SHIP_COUNT = 100;
        const float ENEMY_BOUNDS = 500.0f;

        readonly Vector3 shipMinPosition = new Vector3(-2000.0f, 300.0f, -6000.0f);
        readonly Vector3 shipMaxPosition = new Vector3(2000.0f, 800.0f, -4000.0f);
        const float shipMinVelocity = 0.01f;
        const float shipMaxVelocity = 3.0f;
        
        Random rand = new Random((int)DateTime.Now.Ticks);

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        GameObject terrain;
        GameObject launcherBase;
        GameObject launcherHead;

        LivingGameObject[] missiles;
        LivingGameObject[] enemies;
        
        GamePadState previousGamePadState;
#if !XBOX && !ZUNE
        KeyboardState previousKeyboardState;
#endif

        //AudioEngine audioEngine;
        //SoundBank soundBank;
        //WaveBank waveBank;

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

            // TODO: use this.Content to load your game content here

            //audioEngine = new AudioEngine("Content\\Audio\\3dAlienGameAudio.xgs");
            //waveBank = new WaveBank(audioEngine, "Content\\Audio\\Wave Bank.xwb");
            //soundBank = new SoundBank(audioEngine, "Content\\Audio\\Sound Bank.xsb");

            CameraObject.CreateDefaultCamera(graphics);

            terrain = new GameObject(
                Content.Load<Model>("Models\\terrain")
                );

            launcherBase = new GameObject(
                Content.Load<Model>("Models\\launcher_base"),
                0.2f
                );

            launcherHead = new GameObject(
                Content.Load<Model>("Models\\launcher_head"),
                0.2f,
                launcherBase.Position + new Vector3(0.0f, 20.0f, 0.0f)
                );

            missiles = new LivingGameObject[MISSILE_COUNT];
            for (int i = 0; i < missiles.Length; i++)
                missiles[i] = new LivingGameObject(
                    Content.Load<Model>(
                        "Models\\missile"), 
                        3.0f
                        );

            enemies = new LivingGameObject[ENEMY_SHIP_COUNT];
            for (int i = 0; i < enemies.Length; i++)
                enemies[i] = new LivingGameObject(
                    Content.Load<Model>(
                        "Models\\enemy"), 
                        0.1f, 
                        Vector3.Zero, 
                        new Vector3(
                            0.0f,
                            MathHelper.Pi,
                            0.0f
                            )
                    );

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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed
#if !XBOX && !ZUNE
                || (Keyboard.GetState().IsKeyDown(Keys.Escape))
#endif
                )
                this.Exit();

            // TODO: Add your update logic here
            GamePadState gamePadState = GamePad.GetState(PlayerIndex.One);

            launcherHead.Rotation.Y -= gamePadState.ThumbSticks.Left.X * THUMBSTICK_SPEED;
            launcherHead.Rotation.X += gamePadState.ThumbSticks.Left.Y * THUMBSTICK_SPEED;

            CameraObject.DefaultCamera.Position.Y += gamePadState.ThumbSticks.Right.X * 100.0f; // * THUMBSTICK_SPEED;
            CameraObject.DefaultCamera.Position.Z += gamePadState.ThumbSticks.Right.Y * 100.0f; // * THUMBSTICK_SPEED;

#if !XBOX && !ZUNE
            KeyboardState keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.Left))
                launcherHead.Rotation.Y += KEYBOARD_SPEED;
            if (keyboardState.IsKeyDown(Keys.Right))
                launcherHead.Rotation.Y -= KEYBOARD_SPEED;
            if (keyboardState.IsKeyDown(Keys.Up))
                launcherHead.Rotation.X += KEYBOARD_SPEED;
            if (keyboardState.IsKeyDown(Keys.Down))
                launcherHead.Rotation.X -= KEYBOARD_SPEED;
#endif

            launcherHead.Rotation.Y = MathHelper.Clamp(
                launcherHead.Rotation.Y,
                -MathHelper.PiOver4, MathHelper.PiOver4);

            launcherHead.Rotation.X = MathHelper.Clamp(
                launcherHead.Rotation.X,
                0, MathHelper.PiOver4);

            if ((gamePadState.Buttons.A == ButtonState.Pressed && previousGamePadState.Buttons.A == ButtonState.Released)
#if !XBOX && !ZUNE
                || (keyboardState.IsKeyDown(Keys.Space) && previousKeyboardState.IsKeyUp(Keys.Space))
#endif
                )
                FireMissile();

            if (gamePadState.Triggers.Right > 0.5f)
                FireMissile();

            UpdateMissiles();
            UpdateEnemies();

            //audioEngine.Update();

            previousGamePadState = gamePadState;

#if !XBOX && !ZUNE
            previousKeyboardState = keyboardState;
#endif

            base.Update(gameTime);
        }

        private void UpdateEnemies()
        {
            foreach (LivingGameObject ship in enemies)
            {
                if (ship.IsAlive)
                {
                    ship.Position += ship.Velocity;
                    if (ship.Position.Z > ENEMY_BOUNDS)
                        ship.IsAlive = false;
                }
                else
                {
                    ship.IsAlive = true;
                    ship.Position = new Vector3(
                        MathHelper.Lerp(
                            shipMinPosition.X,
                            shipMaxPosition.X,
                            (float)rand.NextDouble()),
                        MathHelper.Lerp(
                            shipMinPosition.Y,
                            shipMaxPosition.Y,
                            (float)rand.NextDouble()),
                        MathHelper.Lerp(
                            shipMinPosition.Z,
                            shipMaxPosition.Z,
                            (float)rand.NextDouble())
                        );
                    ship.Velocity = new Vector3(
                        0.0f,
                        0.0f,
                        MathHelper.Lerp(
                            shipMinVelocity,
                            shipMaxVelocity,
                            (float)rand.NextDouble())
                        );
                }
            }
        }

        private void FireMissile()
        {
            foreach (LivingGameObject missile in missiles)
            {
                if (!missile.IsAlive)
                {
                    //soundBank.PlayCue("missilelaunch");
                    missile.Velocity = GetMissileMuzzleVelocity();
                    missile.Position = GetMissileMuzzlePosition();
                    missile.Rotation = launcherHead.Rotation;
                    missile.IsAlive = true;
                    break;
                }
            }
        }

        private Vector3 GetMissileMuzzlePosition()
        {
            return launcherHead.Position + (
                Vector3.Normalize(
                    GetMissileMuzzleVelocity()
                    )
                    * LAUNCHER_MUZZLE_OFFSET
                );
        }

        private Vector3 GetMissileMuzzleVelocity()
        {
            return Vector3.Normalize(
                Vector3.Transform(
                    Vector3.Forward,
                    Matrix.CreateFromYawPitchRoll(
                        launcherHead.Rotation.Y,
                        launcherHead.Rotation.X,
                        0
                    )
                )) * MISSILE_POWER;
        }

        private void UpdateMissiles()
        {
            foreach (LivingGameObject missile in missiles)
                if (missile.IsAlive)
                {
                    missile.Position += missile.Velocity;
                    if (missile.Position.Z < OUT_OF_BOUNDS)
                        missile.IsAlive = false;
                    else
                        TestCollision(missile);
                }
        }

        private void TestCollision(LivingGameObject missile)
        {
            foreach (LivingGameObject enemy in enemies)
                if (enemy.IsAlive && enemy.Bounds.Intersects(missile.Bounds))
                {
                   // soundBank.PlayCue("explosion");
                    enemy.IsAlive = false;
                    missile.IsAlive = false;
                }
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            graphics.GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            terrain.Draw();
            launcherBase.Draw();
            launcherHead.Draw();

            foreach (LivingGameObject missile in missiles)
                if (missile.IsAlive)
                    missile.Draw();

            foreach (LivingGameObject enemy in enemies)
                if (enemy.IsAlive)
                    enemy.Draw();

            base.Draw(gameTime);
        }
    }
}
