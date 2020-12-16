using System;
using System.Collections.Generic;
using System.Text;
using PongWin;
using Microsoft.Xna.Framework;

namespace PongWin.Modes
{
    public class GameScreen
    {
        public GameScreen(Pong currentGame)
        {
            _currentGame = currentGame;

            _topBounds = new Vector2();
            _bottomBounds = Pong.Subtract(
                Pong.StandardDimentions, 
                new Vector2(
                    _currentGame.Sprites.Ball.SourceRectangle.Width,
                    _currentGame.Sprites.Ball.SourceRectangle.Height
                    )
                );

            _redPaddle.X = _currentGame.Sprites.RedPaddle.SourceRectangle.Width;
            _bluePaddle.X = Pong.StandardDimentions.X - (_currentGame.Sprites.BluePaddle.SourceRectangle.Width * 2);
        }

        private Pong _currentGame = null;

        private static float _velocityMultiplier = 2f;

        private static Vector2 _velocity = new Vector2(1000);
        private static Vector2 _position = Pong.Divide(Pong.StandardDimentions, 2);

        private static Vector2 _topBounds = new Vector2();
        private static Vector2 _bottomBounds = new Vector2();

        private static float _redPaddleVelocity = 1000;
        //private static bool _redPaddleDirection = false;
        private static float _bluePaddleVelocity = 1000;
        //private static bool _bluePaddleDirection = false;
        private static Vector2 _redPaddle = Pong.Divide(Pong.StandardDimentions, 2);
        private static Vector2 _bluePaddle = Pong.Divide(Pong.StandardDimentions, 2);

        public Vector2 LastPosition { get { return _position; } }

        public Vector2 RedPaddle { get { return _redPaddle; } }
        public Vector2 BluePaddle { get { return _bluePaddle; } }

        private PaddleDirection _redPaddleDirection;
        public PaddleDirection RedPaddleDirection
        {
            get 
            {
                return _redPaddleDirection; 
            }
            set
            { 
                _redPaddleDirection = value; 
            }
        }

        private PaddleDirection _bluePaddleDirection;
        public PaddleDirection BluePaddleDirection
        {
            get
            {
                return _bluePaddleDirection;
            }
            set 
            { 
                _bluePaddleDirection = value; 
            }
        }

        private GameMode _gameMode;
        public GameMode GameMode { get { return _gameMode; } set { _gameMode = value; } }

        public delegate void HitWall();
        public event HitWall HitWallEvent;

        public delegate void HitPaddle();
        public event HitPaddle HitPaddleEvent;

        public delegate void HitScore(PlayerIndex playerIndexScored);
        public event HitScore HitScoreEvent;

        public Vector2 NextPosition(float scaleSpeed)
        {
            _position = Pong.Add(_position, Pong.Multiply(_velocity, scaleSpeed));

            if (_position.X <= _topBounds.X) //Score
            {
                HitScoreEvent(PlayerIndex.Two);
                _position.X = _topBounds.X;
                _velocity.X *= -1;
            }

            if (_position.Y <= _topBounds.Y)
            {
                HitWallEvent();
                _position.Y = _topBounds.Y;
                _velocity.Y *= -1;
            }

            if (_position.X >= _bottomBounds.X) //Score
            {
                HitScoreEvent(PlayerIndex.One);
                _position.X = _bottomBounds.X;
                _velocity.X *= -1;
            }

            if (_position.Y >= _bottomBounds.Y)
            {
                HitWallEvent();
                _position.Y = _bottomBounds.Y;
                _velocity.Y *= -1;
            }

            _redPaddleVelocity = 1000;
            _redPaddle.Y += scaleSpeed * _redPaddleVelocity * (int)_redPaddleDirection;

            if (_redPaddle.Y < 0)
            {
                _redPaddle.Y = 0;
            }
            else if (_redPaddle.Y + _currentGame.Sprites.RedPaddle.Size.Y > Pong.StandardDimentions.Y)
            {
                _redPaddle.Y = Pong.StandardDimentions.Y - _currentGame.Sprites.RedPaddle.Size.Y;
            }

            if (GameMode == GameMode.SinglePlayer)
            {
                _bluePaddleVelocity = new Random((int)DateTime.Now.Ticks).Next(5, 10) * 100;
                _bluePaddleDirection = ((_bluePaddle.Y + (_currentGame.Sprites.BluePaddle.Size.Y / 2)) < _position.Y) ? 
                    PaddleDirection.Down : 
                    PaddleDirection.Up;
            }
            else
            {
                _bluePaddleVelocity = 1000;
            }
            _bluePaddle.Y += scaleSpeed * _bluePaddleVelocity * (int)_bluePaddleDirection;

            if (_bluePaddle.Y < 0)
            {
                _bluePaddle.Y = 0;
            }
            else if (_bluePaddle.Y + _currentGame.Sprites.BluePaddle.Size.Y > Pong.StandardDimentions.Y)
            {
                _bluePaddle.Y = Pong.StandardDimentions.Y - _currentGame.Sprites.BluePaddle.Size.Y;
            }

            BoundingBox _redPaddleBound = new BoundingBox(
                new Vector3(_redPaddle, 0),
                new Vector3(Pong.Add(_redPaddle, _currentGame.Sprites.RedPaddle.Size), 0)
            );

            BoundingBox _bluePaddleBound = new BoundingBox(
                new Vector3(_bluePaddle, 0),
                new Vector3(Pong.Add(_bluePaddle, _currentGame.Sprites.BluePaddle.Size), 0)
            );

            BoundingBox _ballBounds = new BoundingBox(
                new Vector3(_position, 0),
                new Vector3(Pong.Add(_position, _currentGame.Sprites.Ball.Size), 0)
            );

            if (_ballBounds.Intersects(_redPaddleBound))
            {
                //switch (_redPaddleDirection)
                //{
                //    case PaddleDirection.Up:
                //        if (_velocity.Y <= 0)
                //            _velocity.Y /= _velocityMultiplier;
                //        else
                //            _velocity.Y *= _velocityMultiplier;
                //        break;

                //    case PaddleDirection.Down:
                //        if (_velocity.Y >= 0)
                //            _velocity.Y /= _velocityMultiplier;
                //        else
                //            _velocity.Y *= _velocityMultiplier;
                //        break;
                //}

                HitPaddleEvent();
                _position.X = _redPaddle.X + _currentGame.Sprites.RedPaddle.Size.X;
                _velocity.X *= -1;
            }

            if (_ballBounds.Intersects(_bluePaddleBound))
            {
                //switch (_bluePaddleDirection)
                //{
                //    case PaddleDirection.Up:
                //        if (_velocity.Y <= 0)
                //            _velocity.Y /= _velocityMultiplier;
                //        else
                //            _velocity.Y *= _velocityMultiplier;
                //        break;

                //    case PaddleDirection.Down:
                //        if (_velocity.Y >= 0)
                //            _velocity.Y /= _velocityMultiplier;
                //        else
                //            _velocity.Y *= _velocityMultiplier;
                //        break;
                //}

                HitPaddleEvent();
                _position.X = _bluePaddle.X - _currentGame.Sprites.Ball.Size.X;
                _velocity.X *= -1;
            }

            return _position;
        }
    }
}
