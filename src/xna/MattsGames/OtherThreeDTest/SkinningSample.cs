#region File Description
//-----------------------------------------------------------------------------
// SkinningSample.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

#region Using Statements
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SkinnedModel;
using System.Collections.Generic;
#endregion

namespace SkinningSample
{
    /// <summary>
    /// Sample game showing how to display skinned character animation.
    /// </summary>
    public class SkinningSampleGame : Microsoft.Xna.Framework.Game
    {
        //Matrix[] guyTransforms;

        #region Fields

        GraphicsDeviceManager graphics;

        KeyboardState currentKeyboardState = new KeyboardState();
        GamePadState currentGamePadState = new GamePadState();

        Model currentModel;
        AnimationPlayer animationPlayer;

        float cameraArc = 0;
        float cameraRotation = 0;
        float cameraDistance = 100;

        #endregion

        #region Initialization


        public SkinningSampleGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferWidth = 853;
            graphics.PreferredBackBufferHeight = 480;

            graphics.MinimumVertexShaderProfile = ShaderProfile.VS_2_0;
            graphics.MinimumPixelShaderProfile = ShaderProfile.PS_2_0;
        }


        /// <summary>
        /// Load your graphics content.
        /// </summary>
        protected override void LoadContent()
        {
            // Load the model.
            currentModel = Content.Load<Model>("bsd");

            //guyTransforms = new Matrix[currentModel.Bones.Count];
            //currentModel.CopyAbsoluteBoneTransformsTo(guyTransforms);

            // Look up our custom skinning information.
            SkinningData skinningData = currentModel.Tag as SkinningData;

            if (skinningData == null)
                throw new InvalidOperationException
                    ("This model does not contain a SkinningData tag.");

            // Create an animation player, and start decoding an animation clip.
            animationPlayer = new AnimationPlayer(skinningData);

            AnimationClip clip = skinningData.AnimationClips["Take 001"]; //Take 001

            //int frameCount = clip.Keyframes.Count;
            //int frameOffset = 1;
            //int framePart = (frameCount - frameOffset) / 4;

            //Keyframe[] walkArray = new Keyframe[framePart * 2];
            //Array.Copy((clip.Keyframes as List<Keyframe>).ToArray(), frameOffset + framePart, walkArray, 0, framePart * 2);

            //List<Keyframe> walkKeyFrames = new List<Keyframe>();
            //for (int i = 0; i < walkArray.Length; i++)
            //{
            //    walkKeyFrames.Add(walkArray[i]);
            //}

            //TimeSpan newDuration = TimeSpan.FromMilliseconds(clip.Duration.TotalMilliseconds / 2);
            //AnimationClip clip2 = new AnimationClip(newDuration, walkKeyFrames);

            TimeSpan lastSpan = TimeSpan.MinValue;
            int spans = 0;
            foreach (Keyframe keyFrame in clip.Keyframes)
            {
                if (keyFrame.Time != lastSpan)
                {
                    lastSpan = keyFrame.Time;
                    spans++;
                }
            }
            int MillisecondsPerFrame = (int)((float)lastSpan.Milliseconds / (float)spans);

            int spanOffset = 0;
            int durationLength = 0;

            durationLength = 22 * MillisecondsPerFrame;
            List<Keyframe> clipIdle2Walk = new List<Keyframe>();
            foreach (Keyframe keyFrame in clip.Keyframes)
            {
                if (keyFrame.Time.TotalMilliseconds >= spanOffset && keyFrame.Time.TotalMilliseconds <= durationLength + spanOffset)
                {
                    clipIdle2Walk.Add(keyFrame);
                }
            }
            spanOffset += durationLength;
            AnimationClip idle2WalkClip = new AnimationClip(TimeSpan.FromMilliseconds(durationLength * 2), clipIdle2Walk);

            durationLength = 42 * MillisecondsPerFrame;
            List<Keyframe> clipWalk = new List<Keyframe>();
            foreach (Keyframe keyFrame in clip.Keyframes)
            {
                if (keyFrame.Time.TotalMilliseconds >= spanOffset && keyFrame.Time.TotalMilliseconds <= durationLength + spanOffset)
                {
                    clipWalk.Add(keyFrame);
                }
            }
            spanOffset += durationLength;
            AnimationClip walkClip = new AnimationClip(TimeSpan.FromMilliseconds(durationLength * 2), clipWalk);

            durationLength = 22 * MillisecondsPerFrame;
            List<Keyframe> clipWalk2Idle = new List<Keyframe>();
            foreach (Keyframe keyFrame in clip.Keyframes)
            {
                if (keyFrame.Time.TotalMilliseconds >= spanOffset && keyFrame.Time.TotalMilliseconds <= durationLength + spanOffset)
                {
                    clipWalk2Idle.Add(keyFrame);
                }
            }
            AnimationClip walk2IdleClip = new AnimationClip(TimeSpan.FromMilliseconds(durationLength * 2), clipWalk2Idle);


            animationPlayer.StartClip(walkClip);
        }


        #endregion

        #region Update and Draw


        /// <summary>
        /// Allows the game to run logic.
        /// </summary>
        protected override void Update(GameTime gameTime)
        {
            HandleInput();

            UpdateCamera(gameTime);
            
            animationPlayer.Update(gameTime.ElapsedGameTime, true, Matrix.Identity);

            base.Update(gameTime);
        }


        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice device = graphics.GraphicsDevice;

            device.Clear(Color.CornflowerBlue);

            Matrix[] bones = animationPlayer.GetSkinTransforms();

            float aspectRatio = (float)device.Viewport.Width /
                                (float)device.Viewport.Height;

            // Compute camera matrices.
            Matrix view = Matrix.CreateTranslation(0, -40, 0) * 
                          Matrix.CreateRotationY(MathHelper.ToRadians(cameraRotation)) *
                          Matrix.CreateRotationX(MathHelper.ToRadians(cameraArc)) *
                          Matrix.CreateLookAt(new Vector3(0, 0, -cameraDistance), 
                                              new Vector3(0, 0, 0), Vector3.Up);

            Matrix projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, 
                                                                    aspectRatio,
                                                                    1,
                                                                    10000);

            // Render the skinned mesh.
            foreach (ModelMesh mesh in currentModel.Meshes)
            {
                foreach (Effect effect in mesh.Effects)
                {
                    effect.Parameters["Bones"].SetValue(bones);
                    effect.Parameters["View"].SetValue(view);
                    effect.Parameters["Projection"].SetValue(projection);
                }

                mesh.Draw();
            }

            base.Draw(gameTime);
        }

        
        #endregion

        #region Handle Input


        /// <summary>
        /// Handles input for quitting the game.
        /// </summary>
        private void HandleInput()
        {
            currentKeyboardState = Keyboard.GetState();
            currentGamePadState = GamePad.GetState(PlayerIndex.One);

            // Check for exit.
            if (currentKeyboardState.IsKeyDown(Keys.Escape) ||
                currentGamePadState.Buttons.Back == ButtonState.Pressed)
            {
                Exit();
            }
        }


        /// <summary>
        /// Handles camera input.
        /// </summary>
        private void UpdateCamera(GameTime gameTime)
        {
            float time = (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            // Check for input to rotate the camera up and down around the model.
            if (currentKeyboardState.IsKeyDown(Keys.Up) ||
                currentKeyboardState.IsKeyDown(Keys.W))
            {
                cameraArc += time * 0.1f;
            }
            
            if (currentKeyboardState.IsKeyDown(Keys.Down) ||
                currentKeyboardState.IsKeyDown(Keys.S))
            {
                cameraArc -= time * 0.1f;
            }

            cameraArc += currentGamePadState.ThumbSticks.Right.Y * time * 0.25f;

            // Limit the arc movement.
            if (cameraArc > 90.0f)
                cameraArc = 90.0f;
            else if (cameraArc < -90.0f)
                cameraArc = -90.0f;

            // Check for input to rotate the camera around the model.
            if (currentKeyboardState.IsKeyDown(Keys.Right) ||
                currentKeyboardState.IsKeyDown(Keys.D))
            {
                cameraRotation += time * 0.1f;
            }

            if (currentKeyboardState.IsKeyDown(Keys.Left) ||
                currentKeyboardState.IsKeyDown(Keys.A))
            {
                cameraRotation -= time * 0.1f;
            }

            cameraRotation += currentGamePadState.ThumbSticks.Right.X * time * 0.25f;

            // Check for input to zoom camera in and out.
            if (currentKeyboardState.IsKeyDown(Keys.Z))
                cameraDistance += time * 0.25f;

            if (currentKeyboardState.IsKeyDown(Keys.X))
                cameraDistance -= time * 0.25f;

            cameraDistance += currentGamePadState.Triggers.Left * time * 0.5f;
            cameraDistance -= currentGamePadState.Triggers.Right * time * 0.5f;

            // Limit the camera distance.
            if (cameraDistance > 1500.0f)
                cameraDistance = 1500.0f;
            else if (cameraDistance < 100.0f)
                cameraDistance = 100.0f;

            if (currentGamePadState.Buttons.RightStick == ButtonState.Pressed ||
                currentKeyboardState.IsKeyDown(Keys.R))
            {
                cameraArc = 0;
                cameraRotation = 0;
                cameraDistance = 100;
            }
        }


        #endregion
    }


    #region Entry Point

    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    static class Program
    {
        static void Main()
        {
            using (SkinningSampleGame game = new SkinningSampleGame())
            {
                game.Run();
            }
        }
    }

    #endregion
}
