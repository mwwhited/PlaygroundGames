using Microsoft.Xna.Framework;
using System;

namespace _dAlienGame
{
    public class CameraObject
    {
        private static CameraObject _defaultCamera;
        public static void CreateDefaultCamera(GraphicsDeviceManager graphics)
        {
            _defaultCamera = new CameraObject();
            _defaultCamera.Position = new Vector3(0.0f, 60.0f, 160.0f);
            _defaultCamera.LookAt = new Vector3(0.0f, 50.0f, 0.0f);

            _defaultCamera.View = Matrix.CreateLookAt(
                _defaultCamera.Position,
                _defaultCamera.LookAt,
                Vector3.Up);

            _defaultCamera.Projection = Matrix.CreatePerspectiveFieldOfView(
                MathHelper.ToRadians(45.0f),
                graphics.GraphicsDevice.Viewport.AspectRatio,
                1.0f,
                100000.0f);
        }

        public static CameraObject DefaultCamera
        {
            get
            {
                if (_defaultCamera == null)
                    throw new NullReferenceException("Please run \"CreateDefaultCamera\" before calling this property");
                return _defaultCamera;
            }
        }

        public Vector3 Position = Vector3.Zero;
        public Vector3 LookAt = Vector3.Zero;
        public Matrix Projection;
        public Matrix View;
    }
}
