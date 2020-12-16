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
    public class CameraObject
    {
        public CameraObject(GraphicsDeviceManager graphics)
        {
            _graphics = graphics;
        }

        private static CameraObject _defaultCamera;
        private static GraphicsDeviceManager _graphics;

        public static void SetDefaultCamera(CameraObject cameraObject)
        {
            _defaultCamera = cameraObject;
        }

        public static void CreateDefaultCamera(GraphicsDeviceManager graphics)
        {
            _defaultCamera = new CameraObject(graphics);
            _defaultCamera.Position = new Vector3(0.0f, 60.0f, 160.0f);
            _defaultCamera.LookAt = new Vector3(0.0f, 50.0f, 0.0f);
        }

        public static void CreateDefaultCamera(GraphicsDeviceManager graphics, Vector3 position, Vector3 lookAt)
        {
            _defaultCamera = new CameraObject(graphics);
            _defaultCamera.Position = position;
            _defaultCamera.LookAt = lookAt;
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

        public Matrix Projection
        {
            get
            {
                return Matrix.CreatePerspectiveFieldOfView(
                    MathHelper.ToRadians(45.0f),
                    _graphics.GraphicsDevice.Viewport.AspectRatio,
                    1.0f,
                    100000.0f
                    );
            }
        }

        public Matrix View
        {
            get
            {
                return Matrix.CreateLookAt(
                    _defaultCamera.Position,
                    _defaultCamera.LookAt,
                    Vector3.Up
                    );
            }
        }
        }
    }
