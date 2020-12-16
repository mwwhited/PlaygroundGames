using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace _dAlienGame
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

        public GameObject(Model model, float scale, Vector3 position, Vector3 rotation)
        {
            if (model == null)
                throw new ArgumentNullException("model can not be null");

            _model = model;
            Scale = scale;
            Position = position;
            Rotation = rotation;
        }

        private Model _model;
        public Model Model { get { return _model; } }

        public Vector3 Position = Vector3.Zero;

        /// <summary>
        /// X => Pitch
        /// Y => Yaw
        /// Z => Roll
        /// </summary>
        public Vector3 Rotation = Vector3.Zero;

        private float _scale = 1.0f;
        public float Scale { get { return _scale; } set { _scale = value; } }

        public Matrix World
        {
            get
            {
                return Matrix.CreateFromYawPitchRoll(Rotation.Y, Rotation.X, Rotation.Z)
                    * Matrix.CreateScale(Scale)
                    * Matrix.CreateTranslation(Position);
            }
        }

        public virtual void Draw()
        {
            Draw(this);
        }

        public static void Draw(GameObject gameObject)
        {
            Draw(gameObject, (CameraObject)null);
        }

        public static void Draw(GameObject gameObject, CameraObject cameraObject)
        {
            if (gameObject == null)
                return;

            if (cameraObject == null)
                cameraObject = CameraObject.DefaultCamera;

            foreach (ModelMesh mesh in gameObject.Model.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.EnableDefaultLighting();
                    effect.PreferPerPixelLighting = true;

                    effect.World = gameObject.World;

                    effect.Projection = cameraObject.Projection;
                    effect.View = cameraObject.View;
                }
                mesh.Draw();
            }
        }
    }
}
