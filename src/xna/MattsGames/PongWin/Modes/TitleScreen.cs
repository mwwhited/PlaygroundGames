using System;
using System.Collections.Generic;
using System.Text;
using PongWin;
using Microsoft.Xna.Framework;

namespace PongWin.Modes
{
    public class TitleScreen
    {
        public TitleScreen(Pong currentGame)
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

        private static Vector2 _velocity = new Vector2(1000);
        private static Vector2 _position = Pong.Divide(Pong.StandardDimentions, 2);

        private static Vector2 _topBounds = new Vector2();
        private static Vector2 _bottomBounds = new Vector2();

        private static float _redPaddleVelocity = 1000;
        private static bool _redPaddleDirection = false;
        private static float _bluePaddleVelocity = 1000;
        private static bool _bluePaddleDirection = false;
        private static Vector2 _redPaddle = Pong.Divide(Pong.StandardDimentions, 2);
        private static Vector2 _bluePaddle = Pong.Divide(Pong.StandardDimentions, 2);

        public Vector2 LastPosition { get { return _position; } }

        public Vector2 RedPaddle { get { return _redPaddle; } }
        public Vector2 BluePaddle { get { return _bluePaddle; } }


        public delegate void HitWall();
        public event HitWall HitWallEvent;

        public delegate void HitPaddle();
        public event HitPaddle HitPaddleEvent;

        public delegate void HitScore();
        public event HitScore HitScoreEvent;

        public Vector2 NextPosition(float scaleSpeed)
        {
            _position = Pong.Add(_position, Pong.Multiply(_velocity, scaleSpeed));

            if (_position.X <= _topBounds.X) //Score
            {
                HitScoreEvent();
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
                HitScoreEvent();
                _position.X = _bottomBounds.X;
                _velocity.X *= -1;
            }

            if (_position.Y >= _bottomBounds.Y)
            {
                HitWallEvent();
                _position.Y = _bottomBounds.Y;
                _velocity.Y *= -1;
            }

            Random _rnd = new Random((int)DateTime.Now.Ticks);

            _redPaddleVelocity = _rnd.Next(5, 10) * 100;
            _redPaddleDirection = (_redPaddle.Y + (_currentGame.Sprites.RedPaddle.Size.Y / 2)) < _position.Y;
            _redPaddle.Y += scaleSpeed * _redPaddleVelocity * (_redPaddleDirection ? 1 : -1);

            if (_redPaddle.Y < 0)
            {
                _redPaddle.Y = 0;
            }
            else if (_redPaddle.Y + _currentGame.Sprites.RedPaddle.Size.Y > Pong.StandardDimentions.Y)
            {
                _redPaddle.Y = Pong.StandardDimentions.Y - _currentGame.Sprites.RedPaddle.Size.Y;
            }

            _bluePaddleVelocity = _rnd.Next(5, 10) * 100;
            _bluePaddleDirection = (_bluePaddle.Y + (_currentGame.Sprites.BluePaddle.Size.Y / 2)) < _position.Y;
            _bluePaddle.Y += scaleSpeed * _bluePaddleVelocity * (_bluePaddleDirection ? 1 : -1);

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
                HitPaddleEvent();
                _position.X = _redPaddle.X + _currentGame.Sprites.RedPaddle.Size.X;
                _velocity.X *= -1;
            }

            if (_ballBounds.Intersects(_bluePaddleBound))
            {
                HitPaddleEvent();
                _position.X = _bluePaddle.X - _currentGame.Sprites.Ball.Size.X;
                _velocity.X *= -1;
            }
            return _position;
        }
    }
}
