using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PongWin
{
    public class SpriteBinder
    {
        public SpriteBinder(Game pongGame)
        {
            _drops = pongGame.Content.Load<Texture2D>("PongTextures");
            _mainLogo = new Sprite(_drops, rLogo);
            _singlePlayer = new Sprite(_drops, rSinglePlayer);
            _singlePlayerHighlighted = new Sprite(_drops, rSinglePlayerH);
            _multiPlayer = new Sprite(_drops, rMultiPlayer);
            _multiPlayerHighlighted = new Sprite(_drops, rMultiPlayerH);
            _exit = new Sprite(_drops, rExit);
            _exitHighlighted = new Sprite(_drops, rExitH);
            _smallBall = new Sprite(_drops, rSmallBall);
            _ball = new Sprite(_drops, rBall);
            _lives = new Sprite(_drops, rLives);
            _redWon = new Sprite(_drops, rRedWon);
            _blueWon = new Sprite(_drops, rBlueWon);
            _redPaddle = new Sprite(_drops, rRedPaddle);
            _bluePaddle = new Sprite(_drops, rBluePaddle);
            _option = new Sprite(_drops, rOption);
            _optionHighlighted = new Sprite(_drops, rOptionH);
        }

        private Texture2D _drops = null;
        private static readonly Rectangle
            rLogo = new Rectangle(181, 3, 455, 138),
            rSinglePlayer = new Rectangle(3, 153, 312, 56),
            rSinglePlayerH = new Rectangle(320, 153, 312, 56),
            rMultiPlayer = new Rectangle(4, 228, 314, 54),
            rMultiPlayerH = new Rectangle(321, 228, 314, 54),
            rExit = new Rectangle(3, 301, 162, 54),
            rExitH = new Rectangle(320, 301, 162, 54),
            rSmallBall = new Rectangle(63, 0, 32, 32),
            rBall = new Rectangle(95, 62, 49, 49),
            rLives = new Rectangle(7, 373, 92, 26),
            rRedWon = new Rectangle(151, 373, 143, 26),
            rBlueWon = new Rectangle(343, 373, 158, 26),
            rRedPaddle = new Rectangle(0, 0, 20, 100),
            rBluePaddle = new Rectangle(33, 0, 20, 100),
            rOption = new Rectangle(3, 445, 300, 58),
            rOptionH = new Rectangle(320, 445, 300, 58)
            ;

        private Sprite _mainLogo;
        public Sprite MainLogo { get { return _mainLogo; } }

        private Sprite _singlePlayer;
        public Sprite SinglePlayer { get { return _singlePlayer; } }

        private Sprite _singlePlayerHighlighted;
        public Sprite SinglePlayerHighlighted { get { return _singlePlayerHighlighted; } }

        private Sprite _multiPlayer;
        public Sprite MultiPlayer { get { return _multiPlayer; } }

        private Sprite _multiPlayerHighlighted;
        public Sprite MultiPlayerHighlighted { get { return _multiPlayerHighlighted; } }

        private Sprite _exit;
        public Sprite Exit { get { return _exit; } }

        private Sprite _exitHighlighted;
        public Sprite ExitHighlighted { get { return _exitHighlighted; } }

        private Sprite _smallBall;
        public Sprite SmallBall { get { return _smallBall; } }

        private Sprite _ball;
        public Sprite Ball { get { return _ball; } }

        private Sprite _lives;
        public Sprite Lives { get { return _lives; } }

        private Sprite _redWon;
        public Sprite RedWon { get { return _redWon; } }

        private Sprite _blueWon;
        public Sprite BlueWon { get { return _blueWon; } }

        private Sprite _redPaddle;
        public Sprite RedPaddle { get { return _redPaddle; } }

        private Sprite _bluePaddle;
        public Sprite BluePaddle { get { return _bluePaddle; } }
        
        private Sprite _option;
        public Sprite Option { get { return _option; } }

        private Sprite _optionHighlighted;
        public Sprite OptionHighlighted { get { return _optionHighlighted; } }
    }
}
