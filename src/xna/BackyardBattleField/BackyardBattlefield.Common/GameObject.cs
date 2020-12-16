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
    public class GameObject
    {
        public GameObject(Model model)
        {
            if (model == null)
                throw new ArgumentNullException("model can not be null");

            _model = model;
        }

        public GameObject(Model model, float scale)
        {
            if (model == null)
                throw new ArgumentNullException("model can not be null");

            _model = model;
            Scale = scale;
        }

        public GameObject(Model model, float scale, Vector3 position)
        {
            if (model == null)
                throw new ArgumentNullException("model can not be null");

            _model = model;
            Scale = scale;
            Position = position;
        }

        public GameObject(Model model, float scale, Vector3 position, Vector3 direction)
        {
            if (model == null)
                throw new ArgumentNullException("model can not be null");

            _model = model;
            Scale = scale;
            Position = position;
            Direction = direction;
        }

        private Model _model;
        public Model Model { get { return _model; } }

        public Vector3 Position = Vector3.Zero;

        /// <summary>
        /// X => Pitch
        /// Y => Yaw
        /// Z => Roll
        /// </summary>
        public Vector3 Direction { get { return _direction + DirectionOffset; } set { _direction = value; } }
        private Vector3 _direction = Vector3.Zero;

        public Vector3 DirectionOffset = Vector3.Zero;

        private float _scale = 1.0f;
        public float Scale { get { return _scale; } set { _scale = value; } }

        public virtual Matrix World
        {
            get
            {
                return Matrix.CreateFromYawPitchRoll(Direction.Y, Direction.X, Direction.Z)
                    * Matrix.CreateScale(Scale)
                    * Matrix.CreateTranslation(Position);
            }
        }

        private ChaseCamera _camera;
        public ChaseCamera Camera { get { return _camera; } set { _camera = value; } }

        public virtual void Draw()
        {
            Draw(_camera);
        }

        public virtual void Draw(ChaseCamera cameraObject)
        {
            if (cameraObject == null)
                throw new ArgumentNullException();

            foreach (ModelMesh mesh in Model.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.EnableDefaultLighting();
                    effect.PreferPerPixelLighting = true;

                    effect.World = World;

                    effect.Projection = cameraObject.Projection;
                    effect.View = cameraObject.View;
                }
                mesh.Draw();
            }
        }
    }
}
