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
using Xclna.Xna.Animation;

namespace ThreeDTest
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        
        Model bsdModel;
        Vector3 bsdPosition = new Vector3(0, 0, 1);

        CharacterState bsdState = CharacterState.Idle;
        CharacterState lastState = CharacterState.Idle;

        ModelAnimator bsdAnimator;
        AnimationController[] bsdAnimationControllers = new AnimationController[18];

        Matrix view;
        Matrix[] _boneTransform;
        Vector3 camOffset = new Vector3(0, 15, -20);
        Matrix rotation = Matrix.Identity;
        float currentSpeed = 0;
        float blendFactor = 0;
        const float WALK_SPEED = .115f;
        const float RUN_SPEED = .5f;

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

            bsdModel = Content.Load<Model>("bsd");
            _boneTransform = new Matrix[bsdModel.Bones.Count];
            bsdModel.Root.Transform *= Matrix.CreateRotationY(MathHelper.ToRadians(90));

            bsdAnimator = new ModelAnimator(this, bsdModel);
            Viewport port = graphics.GraphicsDevice.Viewport;
            //view = Matrix.CreateLookAt(new Vector3(0, 500, -1000), Vector3.Zero, Vector3.Up);

            bsdAnimationControllers[(int)CharacterState.Idle] = new AnimationController(this, bsdAnimator.Animations["Idle"]);
            bsdAnimationControllers[(int)CharacterState.IdleToWalk] = new AnimationController(this, bsdAnimator.Animations["IdleToWalk"]);
            bsdAnimationControllers[(int)CharacterState.Walk] = new AnimationController(this, bsdAnimator.Animations["Walk"]);
            bsdAnimationControllers[(int)CharacterState.WalkToIdle] = new AnimationController(this, bsdAnimator.Animations["WalkToIdle"]);
            //bsdAnimationControllers[(int)CharacterState.NodHead] = new AnimationController(this, bsdAnimator.Animations["NodHead"]);
            //bsdAnimationControllers[(int)CharacterState.ShakeHead] = new AnimationController(this, bsdAnimator.Animations["ShakeHead"]);
            //bsdAnimationControllers[(int)CharacterState.Wave] = new AnimationController(this, bsdAnimator.Animations["Wave"]);
            //bsdAnimationControllers[(int)CharacterState.Point] = new AnimationController(this, bsdAnimator.Animations["Point"]);
            //bsdAnimationControllers[(int)CharacterState.PointFinish] = new AnimationController(this, bsdAnimator.Animations["PointFinish"]);
            //bsdAnimationControllers[(int)CharacterState.PistolReady] = new AnimationController(this, bsdAnimator.Animations["PistolReady"]);
            //bsdAnimationControllers[(int)CharacterState.PistolShoot] = new AnimationController(this, bsdAnimator.Animations["PistolShoot"]);
            //bsdAnimationControllers[(int)CharacterState.PistolDone] = new AnimationController(this, bsdAnimator.Animations["PistolDone"]);
            //bsdAnimationControllers[(int)CharacterState.ThrowReady] = new AnimationController(this, bsdAnimator.Animations["ThrowReady"]);
            //bsdAnimationControllers[(int)CharacterState.Throw] = new AnimationController(this, bsdAnimator.Animations["Throw"]);
            //bsdAnimationControllers[(int)CharacterState.ThrowToReady] = new AnimationController(this, bsdAnimator.Animations["ThrowToReady"]);
            //bsdAnimationControllers[(int)CharacterState.ThrowReadyToDone] = new AnimationController(this, bsdAnimator.Animations["ThrowReadyToDone"]);
            //bsdAnimationControllers[(int)CharacterState.ThrowToDone] = new AnimationController(this, bsdAnimator.Animations["ThrowToDone"]);
            //bsdAnimationControllers[(int)CharacterState.DeepBreath] = new AnimationController(this, bsdAnimator.Animations["DeepBreath"]);

            Matrix projection = Matrix.CreatePerspectiveFieldOfView(
                MathHelper.PiOver4, 
                (float)port.Width / port.Height, 
                .1f, 
                100000f
                );

            bsdModel.CopyAbsoluteBoneTransformsTo(_boneTransform);
            foreach (ModelMesh mesh in bsdModel.Meshes)
            {
                foreach (Effect effect in mesh.Effects)
                {
                    if (effect is BasicEffect)
                    {
                        BasicEffect basic = (BasicEffect)effect;
                        basic.View = Matrix.CreateLookAt(
                            new Vector3(1000, 500, 0),
                            new Vector3(0, 150, 0),
                            Vector3.Up);
                        //view;
                        basic.Projection = Matrix.CreatePerspectiveFieldOfView(
                            MathHelper.PiOver4,
                            GraphicsDevice.Viewport.Width / GraphicsDevice.Viewport.Height,
                            10,
                            10000);
                        //projection;
                    }
                    else if (effect is BasicPaletteEffect)
                    {
                        BasicPaletteEffect palette = (BasicPaletteEffect)effect;
                        palette.View = Matrix.CreateLookAt(
                            new Vector3(1000, 500, 0),
                            new Vector3(0, 150, 0),
                            Vector3.Up);
                        //view;

                        palette.Projection = Matrix.CreatePerspectiveFieldOfView(
                            MathHelper.PiOver4,
                            GraphicsDevice.Viewport.Width / GraphicsDevice.Viewport.Height,
                            10,
                            10000);
                            //projection;
                        // enable some lighting
                        palette.EnableDefaultLighting();
                        //palette.DirectionalLight0.Direction = new Vector3(0, 0, 1);
                    }
                }
            }

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
            KeyboardState keyState = Keyboard.GetState();

            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || keyState.IsKeyDown(Keys.Escape))
                this.Exit();

            if (currentSpeed > 0)
            {
                bsdPosition += (-Matrix.CreateTranslation(0, 0, currentSpeed)
                    * rotation).Translation;
            }
            if (keyState.IsKeyDown(Keys.D))
            {
                rotation *=
                    Matrix.CreateFromAxisAngle(Vector3.Up, -MathHelper.Pi / 25.0f);

            }
            if (keyState.IsKeyDown(Keys.A))
            {
                rotation *=
                    Matrix.CreateFromAxisAngle(Vector3.Up, MathHelper.Pi / 25.0f);
            }

            // Add this to the beginning of the Update method
            UpdateState(gameTime);

            //// Add this to the end of the Update method
            //BonePose weapon = bsd.BonePoses["weapon"];
            //weapon.CurrentController = null;
            //weapon.CurrentBlendController = null;
            //weapon.DefaultTransform =
            //    Matrix.CreateRotationX(MathHelper.Pi) *
            //    Matrix.CreateRotationY(MathHelper.Pi) *
            //    Matrix.CreateTranslation(weapon.DefaultTransform.Translation);

            //// Add this to the end of the update method
            //BonePose head = dwarfAnimator.BonePoses["head"];
            //head.CurrentController = nod;
            //head.CurrentBlendController = null;

            // Add this to the Update method
            bsdAnimator.World = rotation * Matrix.CreateTranslation(bsdPosition);
            view = Matrix.CreateLookAt(
                 bsdAnimator.World.Translation + camOffset,
                 bsdAnimator.World.Translation,
                 Vector3.Up);

            // Add this to the Update method
            foreach (ModelMesh mesh in bsdAnimator.Model.Meshes)
                foreach (Effect effect in mesh.Effects)
                    effect.Parameters["View"].SetValue(view);

            //foreach (ModelMesh mesh in ground.Model.Meshes)
            //    foreach (BasicEffect effect in mesh.Effects)
            //        effect.View = view;

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            graphics.GraphicsDevice.Clear(Color.CornflowerBlue);

            bsdModel.CopyAbsoluteBoneTransformsTo(_boneTransform);
            foreach (ModelMesh mesh in bsdModel.Meshes)
            {
                //foreach (BasicPaletteEffect effect in mesh.Effects)
                foreach (Effect effect in mesh.Effects)
                {
                    if (effect is BasicEffect)
                    {
                        BasicEffect _effect = (BasicEffect)effect;
                        //effect.World = _boneTransform[mesh.ParentBone.Index];
                        _effect.View = Matrix.CreateLookAt(new Vector3(1500, 500, 0), new Vector3(0, 0, 0), Vector3.Up);
                        _effect.Projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, GraphicsDevice.Viewport.Width / GraphicsDevice.Viewport.Height, 10, 10000);
                        //effect.EnableDefaultLighting();
                        //effect.DirectionalLight0.Direction = new Vector3(0, 0, 300);
                    }
                    else if (effect is BasicPaletteEffect)
                    {
                        BasicPaletteEffect _effect = (BasicPaletteEffect)effect;
                        //effect.World = _boneTransform[mesh.ParentBone.Index];
                        _effect.View = Matrix.CreateLookAt(new Vector3(1500, 500, 0), new Vector3(0, 0, 0), Vector3.Up);
                        _effect.Projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, GraphicsDevice.Viewport.Width / GraphicsDevice.Viewport.Height, 10, 10000);
                        _effect.EnableDefaultLighting();
                        _effect.DirectionalLight0.Direction = new Vector3(0, 0, 300);
                    }
                }
                mesh.Draw();
            }

            if (lastState != bsdState)
            {
                lastState = bsdState;
                Console.WriteLine("State: " + bsdState.ToString());
            }
            RunController(bsdAnimator, bsdAnimationControllers[(int)bsdState]);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }

        private void RunController(ModelAnimator animator, AnimationController controller)
        {
            foreach (BonePose p in animator.BonePoses)
            {
                p.CurrentController = controller;
                p.CurrentBlendController = null;
            }
        }

        // Add this as a new method
        private void UpdateState(GameTime gameTime)
        {
            // Add this to the UpdateState method
            KeyboardState keyState = Keyboard.GetState();
            BonePoseCollection poses = bsdAnimator.BonePoses;
            if (bsdState == CharacterState.Idle)
            {
                currentSpeed = 0;
                // Add this to the beginning of the if (state=="idle") block in the UpdateState method
                // Remove the old if (keyState.IsKeyDown(Keys.W)) block
                if (keyState.IsKeyDown(Keys.W))
                {
                    blendFactor = 0;
                    bsdState = CharacterState.IdleToWalk;

                }
                // Add this in the if (state=="idle") block of the UpdateState method
                //if (keyState.IsKeyDown(Keys.Space))
                //{
                //    crouch.ElapsedTime = 0;
                //    crouch.IsLooping = false;
                //    //crouch.AnimationEnded += new AnimationEventHandler(crouch_AnimationEnded);
                //    state = "crouchDown";
                //}
                //RunController(bsdAnimator, idle);
            }
            else if (bsdState == CharacterState.Walk)
            {
                currentSpeed = WALK_SPEED;
                // Add this to the beginning of the if (state=="walk") block in the UpdateState method
                // Remove the old if (keyState.IsKeyUp(Keys.W)) block
                if (keyState.IsKeyUp(Keys.W))
                {
                    blendFactor = 0;
                    bsdState = CharacterState.WalkToIdle;
                }
                if (keyState.IsKeyDown(Keys.LeftShift) && keyState.IsKeyDown(Keys.W))
                {
                    blendFactor = 0;
                    bsdState = CharacterState.Walk;
                    bsdAnimationControllers[(int)bsdState].SpeedFactor = 0;
                }
                //RunController(dwarfAnimator, walk);
            }
            // Add this to the UpdateState method
            else if (bsdState == CharacterState.IdleToWalk)
            {
                bsdState = CharacterState.Walk;

                //blendFactor += .1f;
                //currentSpeed = blendFactor * WALK_SPEED;
                //if (blendFactor >= 1)
                //{
                //    blendFactor = 1;
                //    bsdState = CharacterState.Walk;
                //}
                //foreach (BonePose p in poses)
                //{
                //    p.CurrentController = bsdAnimationControllers[(int)CharacterState.Idle];
                //    p.CurrentBlendController = bsdAnimationControllers[(int)CharacterState.Walk];
                //    p.BlendFactor = blendFactor;
                //}
            }
            // Add this to the UpdateState method
            else if (bsdState == CharacterState.WalkToIdle)
            {
                bsdState = CharacterState.Idle;

                //blendFactor += .1f;
                //currentSpeed = (1f - blendFactor) * WALK_SPEED;
                //if (blendFactor >= 1)
                //{
                //    blendFactor = 1;
                //    bsdState = CharacterState.Idle;
                //}
                //foreach (BonePose p in poses)
                //{
                //    p.CurrentController = bsdAnimationControllers[(int)CharacterState.Walk];
                //    p.CurrentBlendController = bsdAnimationControllers[(int)CharacterState.Idle];
                //    p.BlendFactor = blendFactor;
                //}
            }
            //// Add this in the UpdateState method
            //else if (state == "crouchDown")
            //{
            //    RunController(dwarfAnimator, crouch);
            //}
            //else if (state == "stayCrouched")
            //{
            //    // Add this to the if (state == "stayCrouched") block in the UpdateState method
            //    if (keyState.IsKeyDown(Keys.Space))
            //    {
            //        crouch.ElapsedTime = crouch.AnimationSource.Duration;
            //        crouch.SpeedFactor = 0;
            //        state = "standUp";
            //    }
            //    //RunController(dwarfAnimator, stayCrouched);
            //}
            // Add this to the UpdateState method
            //else if (state == "standUp")
            //{
            //    if (crouch.ElapsedTime - gameTime.ElapsedGameTime.Ticks <= 0)
            //    {
            //        crouch.SpeedFactor = 1;
            //        crouch.ElapsedTime = 0;
            //        idle.ElapsedTime = 0;
            //        state = "idle";
            //    }
            //    else
            //        crouch.ElapsedTime -= gameTime.ElapsedGameTime.Ticks;
            //    //RunController(dwarfAnimator, crouch);
            //}
            // Add this to the UpdateState method
            //else if (state == "walkToRun")
            //{
            //    blendFactor += .05f;
            //    if (blendFactor >= 1)
            //    {
            //        blendFactor = 1;
            //        run.SpeedFactor = 1;
            //        state = "run";
            //    }
            //    double factor = (double)walk.ElapsedTime / walk.AnimationSource.Duration;
            //    run.ElapsedTime = (long)(factor * run.AnimationSource.Duration);
            //    currentSpeed = WALK_SPEED + blendFactor * (RUN_SPEED - WALK_SPEED);
            //    foreach (BonePose p in poses)
            //    {
            //        p.CurrentController = walk;
            //        p.CurrentBlendController = run;
            //        p.BlendFactor = blendFactor;
            //    }
            //}
            //else if (state == "run")
            //{
            //    currentSpeed = RUN_SPEED;
            //    if (keyState.IsKeyUp(Keys.LeftShift))
            //    {
            //        blendFactor = 0;
            //        state = "runToWalk";
            //        walk.SpeedFactor = 0;
            //    }
            //    foreach (BonePose p in poses)
            //    {
            //        p.CurrentController = run;
            //        p.CurrentBlendController = null;
            //    }
            //}
            //else if (state == "runToWalk")
            //{
            //    blendFactor += .05f;
            //    if (blendFactor >= 1)
            //    {
            //        blendFactor = 1;

            //        //bsdA .SpeedFactor = 1;
            //        state = "walk";
            //    }
            //    double factor = (double)run.ElapsedTime / run.AnimationSource.Duration;
            //    walk.ElapsedTime = (long)(factor * walk.AnimationSource.Duration);
            //    currentSpeed = WALK_SPEED + (1f - blendFactor) * (RUN_SPEED - WALK_SPEED);
            //    foreach (BonePose p in poses)
            //    {
            //        p.CurrentController = run;
            //        p.CurrentBlendController = walk;
            //        p.BlendFactor = blendFactor;
            //    }
            //}



        }


    }
}
