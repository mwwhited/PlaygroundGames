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

namespace BackyardBattleField.Common
{
    public class LivingGameObject : GameObject
    {
        public LivingGameObject(Model model)
            : base(model)
        {
            Reset();
        }

        public LivingGameObject(Model model, float scale)
            : base(model, scale)
        {
            Reset();
        }

        public LivingGameObject(Model model, float scale, Vector3 position)
            : base(model, scale, position)
        {
            Reset();
        }

        public LivingGameObject(Model model, float scale, Vector3 position, Vector3 rotation)
            : base(model, scale, position, rotation)
        {
            Reset();
        }

        public LivingGameObject(Model model, float scale, float mass)
            : base(model, scale)
        {
            Mass = mass;
            Reset();
        }

        public LivingGameObject(Model model, float scale, Vector3 position, float mass)
            : base(model, scale, position)
        {
            Mass = mass;
            Reset();
        }

        public LivingGameObject(Model model, float scale, Vector3 position, Vector3 rotation, float mass)
            : base(model, scale, position, rotation)
        {
            Mass = mass;
            Reset();
        }

        public void Reset()
        {
            Position = new Vector3(0, 0, 0);
            Direction = Vector3.Forward;
            Up = Vector3.Up;
            Right = Vector3.Right;
            Velocity = Vector3.Zero;
        }

        public Vector3 Velocity = Vector3.Zero;

        private bool _isAlive;
        public bool IsAlive { get { return _isAlive; } set { _isAlive = value; } }

        public BoundingSphere Bounds
        {
            get
            {
                BoundingSphere mySphere = this.Model.Meshes[0].BoundingSphere;
                mySphere.Center = this.Position;
                mySphere.Radius *= this.Scale;

                return mySphere;
            }
        }

        /// <summary>
        /// Full speed at which ship can rotate; measured in radians per second.
        /// </summary>
        public float RotationRate = 1.5f;

        /// <summary>
        /// Mass of ship.
        /// </summary>
        public float Mass = 1.0f;

        /// <summary>
        /// Maximum force that can be applied along the ship's direction.
        /// </summary>
        public float ThrustForce = 24000.0f;

        /// <summary>
        /// Velocity scalar to approximate drag.
        /// </summary>
        public float DragFactor = 0.97f;

        private Vector3 Up;

        private Vector3 Right;

        public virtual void Update(GameTime gameTime, Vector2 rotationAmount, float roll, float thrustAmount)
        {
            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;

            // Scale rotation amount to radians per second
            rotationAmount = rotationAmount * RotationRate * elapsed;

            //// Correct the X axis steering when the ship is upside down
            if (Up.Y < 0)
                rotationAmount.X = -rotationAmount.X;

            // Create rotation matrix from rotation amount
            Matrix rotationMatrix =
                Matrix.CreateFromAxisAngle(Right, rotationAmount.Y) *
                Matrix.CreateRotationY(rotationAmount.X) *
                Matrix.CreateRotationZ(MathHelper.ToRadians(roll));

            // Rotate orientation vectors
            Direction = Vector3.TransformNormal(Direction, rotationMatrix);

            Up = Vector3.TransformNormal(Up, rotationMatrix);

            // Re-normalize orientation vectors
            // Without this, the matrix transformations may introduce small rounding
            // errors which add up over time and could destabilize the ship.
            Direction.Normalize();
            Up.Normalize();

            // Re-calculate Right
            Right = Vector3.Cross(Direction, Up);

            // The same instability may cause the 3 orientation vectors may
            // also diverge. Either the Up or Direction vector needs to be
            // re-computed with a cross product to ensure orthagonality
            Up = Vector3.Cross(Right, Direction);

            // Calculate force from thrust amount
            Vector3 force = Direction * thrustAmount * ThrustForce;

            // Apply acceleration
            Vector3 acceleration = force / Mass;
            Velocity += acceleration * elapsed;

            // Apply psuedo drag
            Velocity *= DragFactor;

            // Apply velocity
            Position += Velocity * elapsed;
        }

        public override Matrix World
        {
            get
            {
                //Matrix world;
                //Matrix.CreateWorld(ref Position, ref Direction, ref Up, out world);

                //Matrix world = Matrix.CreateFromYawPitchRoll(Direction.Y, Direction.X, Direction.Z)
                //    * Matrix.CreateScale(Scale)
                //    * Matrix.CreateTranslation(Position);

                Matrix world = Matrix.Identity;
                world.Forward = Direction;
                world.Up = Up;
                world.Right = Right;

                world *= Matrix.CreateScale(Scale);

                world.Translation = Position / Scale;

                return world;
            }
        }

        //public override void Draw(ChaseCamera cameraObject)
        //{
        //    if (cameraObject == null)
        //        throw new ArgumentNullException();
        //    //Matrix[] transforms = new Matrix[Model.Bones.Count];
        //    //Model.CopyAbsoluteBoneTransformsTo(transforms);
        //    //Matrix truckScalingMatrix = transforms[0] * Matrix.CreateScale(Scale);
        //    foreach (ModelMesh mesh in Model.Meshes)
        //    {
        //        foreach (BasicEffect effect in mesh.Effects)
        //        {
        //            effect.EnableDefaultLighting();
        //            effect.PreferPerPixelLighting = true;
        //            //effect.World = gameObject.World;
        //            //Matrix world = transforms[mesh.ParentBone.Index] *
        //            //    //Matrix.CreateFromYawPitchRoll(Direction.Y, Direction.X, Direction.Z) *
        //            //    //Matrix.CreateRotationX(MathHelper.ToRadians(Direction.X)) *
        //            //    //Matrix.CreateRotationY(MathHelper.ToRadians(Direction.Y)) *
        //            //    //Matrix.CreateRotationZ(MathHelper.ToRadians(Direction.Z)) *
        //            //    truckScalingMatrix *
        //            //    Matrix.CreateTranslation(Position);
        //            //world.Up = Up;
        //            //world.Right = Right;
        //            //world.Forward = Direction;
        //            //world.Translation = Position;
        //            effect.World = World;
        //            effect.Projection = cameraObject.Projection;
        //            effect.View = cameraObject.View;
        //        }
        //        mesh.Draw();
        //    }
        //}
    }
}
