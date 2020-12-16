using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _dAlienGame
{
    public class LivingGameObject : GameObject
    {
        public LivingGameObject(Model model)
            : base(model)
        {
        }

        public LivingGameObject(Model model, float scale)
            : base(model, scale)
        {
        }

        public LivingGameObject(Model model, float scale, Vector3 position)
            :base (model, scale, position)
        {
        }

        public LivingGameObject(Model model, float scale, Vector3 position, Vector3 rotation)
            : base(model, scale, position, rotation)
        {
        }

        public Vector3 Velocity = Vector3.Zero;

        private bool _isAlive;
        public bool IsAlive { get { return _isAlive; } set { _isAlive = value; } }

        public BoundingSphere Bounds {
            get
            {
                BoundingSphere mySphere = this.Model.Meshes[0].BoundingSphere;
                mySphere.Center = this.Position;
                mySphere.Radius *= this.Scale;

                return mySphere;
            }
        }
    }
}
