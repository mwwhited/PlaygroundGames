using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;
using PongWin.Modes;

namespace PongWin
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Pong : Microsoft.Xna.Framework.Game
    {
        private static readonly Vector2 standardDimentions = new Vector2(1920, 1080);
        private static Vector2 screenCenter = new Vector2();
        private static Vector2 screenOffset = new Vector2();
        private static Vector2 screenOffsetOrigin = new Vector2();
        private static float scalingFactor = 1;
        private static float scalingFactorOrigin = 1;
        private GameState currentState = GameState.TitleScreen;
        private TitleScreen titleScreen = null;
        private GameScreen gameScreen = null;
        private float gameTicksPerSecond = 1;
        private SpriteBinder sprites = null;
        private Texture2D backgroundTexture = null;
        private Texture2D scaleTexture = null;
        private GraphicsDeviceManager graphics = null;
        private SpriteBatch spriteBatch = null;
        private int spacing = 0;
        private int blueLiveOffset = 0;
        private SpriteFont pongFont = null;
#if !ZUNE
        private AudioEngine audioEngine = null;
        private WaveBank waveBank = null;
        //private SoundBank soundBank = null;
#endif
        private MenuMode mainMenuMode = MenuMode.SinglePlayer;
        private OptionMode optionMenuMode = OptionMode.MainMenu;
        private InputState inputState = new InputState();
        private Rectangle fullScreen = new Rectangle(0, 0, (int)standardDimentions.X, (int)standardDimentions.Y);
        private PongConfig pongConfig = null;
        private IAsyncResult storageResult = null;

        private PlayerIndex playerWon = 0;

        private bool startedGame = false;
        private int maxScore = 10;
        private int[] scoreKeeper = new int[2];

        public static Vector2 StandardDimentions { get { return standardDimentions; } }
        public SpriteBinder Sprites { get { return sprites; } }

        public Pong()
        {
            graphics = new GraphicsDeviceManager(this);

#if XBOX360
            Components.Add(new GamerServicesComponent(this));
#else
            Debug.WriteLine("Can't see the XBOX360 define");
#endif

            Content.RootDirectory = "Content";
        }

        #region Operations

        public static Vector2 Subtract(Vector2 left, Vector2 right)
        {
            return new Vector2(
                left.X - right.X,
                left.Y - right.Y 
                );
        }

        public static Vector2 Add(Vector2 left, Vector2 right)
        {
            return new Vector2(
                left.X + right.X,
                left.Y + right.Y
                );
        }

        public static Vector2 Divide(Vector2 left, float right)
        {
            return new Vector2(
                left.X / right,
                left.Y / right
                );
        }

        public static Vector2 Divide(Rectangle left, float right)
        {
            return Divide(new Vector2(left.Width, left.Height), right);
        }

        public static Vector2 Multiply(Vector2 left, float right)
        {
            return new Vector2(
                left.X * right,
                left.Y * right
                );
        }

        #endregion

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            VideoHelper.SetHighestResolution(this, graphics);
            //VideoHelper.InitGraphicsMode(graphics, 1280, 720, false);
#if !ZUNE
            backgroundTexture = Content.Load<Texture2D>("Stars");
            scaleTexture = Content.Load<Texture2D>("ScaleDrop");
#else
            backgroundTexture = Content.Load<Texture2D>("StarsZune");
            scaleTexture = Content.Load<Texture2D>("ScaleDropZune");
#endif
            pongFont = Content.Load<SpriteFont>("Comic Sans MS");
            
            float _xScale = (float)GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width / standardDimentions.X;
            float _yScale = (float)GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height / standardDimentions.Y;

            if (_xScale < _yScale)
                scalingFactor = _xScale;
            else
                scalingFactor = _yScale;

            scalingFactorOrigin = scalingFactor;

            screenCenter.X = standardDimentions.X / 2;
            screenCenter.Y = standardDimentions.Y / 2;

            screenOffset.X = (int)(GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width - (standardDimentions.X * scalingFactor)) / 2;
            screenOffset.Y = (int)(GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height - (standardDimentions.Y * scalingFactor)) / 2;

            screenOffsetOrigin = screenOffset;

            sprites = new SpriteBinder(this);
            titleScreen = new TitleScreen(this);

            spacing = (int)(sprites.Lives.Size.Y / 2);
            blueLiveOffset = (int)(standardDimentions.X - (spacing * 4) - (sprites.Ball.Size.X * 3));

            gameScreen = new GameScreen(this);

            titleScreen.HitPaddleEvent += new TitleScreen.HitPaddle(titleScreen_HitPaddleEvent);
            titleScreen.HitWallEvent += new TitleScreen.HitWall(titleScreen_HitWallEvent);
            titleScreen.HitScoreEvent += new TitleScreen.HitScore(titleScreen_HitScoreEvent);

            gameScreen.HitWallEvent += new GameScreen.HitWall(gameScreen_HitWallEvent);
            gameScreen.HitPaddleEvent += new GameScreen.HitPaddle(gameScreen_HitPaddleEvent);
            gameScreen.HitScoreEvent += new GameScreen.HitScore(gameScreen_HitScoreEvent);

#if !ZUNE
            //audioEngine = new AudioEngine("Content\\PongSounds.xgs");
            //waveBank = new WaveBank(audioEngine, "Content\\Wave Bank.xwb");
            //soundBank = new SoundBank(audioEngine, "Content\\Sound Bank.xsb");
#endif
            ResetGame();

            base.Initialize();
        }

        #region Events

        private void gameScreen_HitScoreEvent(PlayerIndex playerIndexScored)
        {
            scoreKeeper[(int)playerIndexScored]++;

            if (scoreKeeper[(int)playerIndexScored] >= maxScore)
            {
                playerWon = playerIndexScored;
                currentState = GameState.GameOver;
            }            
            titleScreen_HitScoreEvent();
        }

        private void gameScreen_HitPaddleEvent()
        {
            titleScreen_HitPaddleEvent();
        }

        private void gameScreen_HitWallEvent()
        {
            titleScreen_HitWallEvent();
        }

        private void titleScreen_HitScoreEvent()
        {
#if !ZUNE
            //soundBank.PlayCue("ScoreHit");
#endif
        }

        private void titleScreen_HitWallEvent()
        {
#if !ZUNE
            //soundBank.PlayCue("WallHit");
#endif
        }

        private void titleScreen_HitPaddleEvent()
        {
#if !ZUNE
            //soundBank.PlayCue("PaddleHit");
#endif
        }
        #endregion

        private StorageDevice GetStorageDevice()
        {
            storageResult = Guide.BeginShowStorageDeviceSelector(null, null);
            while (!storageResult.IsCompleted)
            {
                this.Tick();
            }
            StorageDevice storageDevice = Guide.EndShowStorageDeviceSelector(storageResult);
            storageResult = null;
            return storageDevice;
        }

        private void ResetGame()
        {
            if (startedGame)
            {
                for (int i = 0; i < scoreKeeper.Length; i++)
                    scoreKeeper[i] = 0;

                startedGame = false;
            }
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            pongConfig = PongConfig.Load(GetStorageDevice());
            if (pongConfig != null)
            {
                scalingFactor = pongConfig.ScalingFactor;
                if (scalingFactor == 0)
                {
                    scalingFactor = scalingFactorOrigin;
                    pongConfig.ScalingFactor = scalingFactorOrigin;
                }

                screenOffset = pongConfig.ScreenOffset;
            }
            else
            {
                pongConfig = new PongConfig();
                currentState = GameState.ScalingScreen;
            }
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            inputState.Update();
            gameTicksPerSecond = this.TargetElapsedTime.Milliseconds / 1000.0f;
                        
            switch (currentState)
            {
                case GameState.SplashScreen:
                    if (inputState.MenuCancel)
                        currentState = GameState.ExitScreen;
                    else
                        currentState = GameState.TitleScreen;

                    break;

                case GameState.TitleScreen:
                    titleScreen.NextPosition(gameTicksPerSecond);

                    if (inputState.MenuSelect || inputState.MenuCancel)
                        currentState = GameState.MainMenu;

                    break;

                case GameState.MainMenu:
                    ResetGame();

                    if (inputState.MenuCancel)
                        currentState = GameState.TitleScreen;

                    if (inputState.MenuUp)
                    {
                        mainMenuMode--;
                        if (mainMenuMode < MenuMode.SinglePlayer)
                            mainMenuMode = MenuMode.SinglePlayer;
                    }
                    if (inputState.MenuDown)
                    {
                        mainMenuMode++;
                        if (mainMenuMode > MenuMode.Exit)
                            mainMenuMode = MenuMode.Exit;
                    }
                    if (inputState.MenuSelect)
                    {
                        switch (mainMenuMode)
                        {
                            case MenuMode.MultiPlayer:
                                gameScreen.GameMode = GameMode.MultiPlayer;
                                currentState = GameState.Running;
                                break;

                            case MenuMode.SinglePlayer:
                                gameScreen.GameMode = GameMode.SinglePlayer;
                                currentState = GameState.Running;
                                break;

                            case MenuMode.Option:
                                currentState = GameState.SettingsMenu;
                                break;

                            case MenuMode.Exit:
                                currentState = GameState.ExitScreen;
                                break;
                        }
                    }
                    break;

                case GameState.SettingsMenu:
                    if (inputState.MenuCancel)
                        currentState = GameState.MainMenu;

                    if (inputState.MenuUp)
                    {
                        optionMenuMode--;
                        if (optionMenuMode < OptionMode.AdjustScreen)
                            optionMenuMode = OptionMode.AdjustScreen;
                    }

                    if (inputState.MenuDown)
                    {
                        optionMenuMode++;
                        if (optionMenuMode > OptionMode.MainMenu)
                            optionMenuMode = OptionMode.MainMenu;
                    }

                    if (inputState.MenuSelect)
                    {
                        switch (optionMenuMode)
                        {
                            case OptionMode.AdjustScreen:
                                currentState = GameState.ScalingScreen;
                                break;
                            case OptionMode.MainMenu:
                                currentState = GameState.MainMenu;
                                break;
                        }
                    }
                    break;
                
                case GameState.ScalingScreen:
                    if (inputState.MenuCancel)
                        currentState = GameState.MainMenu;

                    if (inputState.DirectionUp)
                        screenOffset.Y--;

                    if (inputState.DirectionDown)
                        screenOffset.Y++;

                    if (inputState.DirectionLeft)
                        screenOffset.X--;

                    if (inputState.DirectionRight)
                        screenOffset.X++;

                    if (inputState.DirectionClick)
                        screenOffset = screenOffsetOrigin;

                    if (inputState.OtherDirectionUp)
                        scalingFactor -= .001f;

                    if (inputState.OtherDirectionDown)
                        scalingFactor += .001f;

                    if (inputState.OtherDirectionLeft)
                        scalingFactor -= .01f;

                    if (inputState.OtherDirectionRight)
                        scalingFactor += .01f;

                    if (inputState.OtherDirectionClick)
                        scalingFactor = scalingFactorOrigin;

                    if (inputState.MenuSelect)
                    {
                        pongConfig.ScalingFactor = scalingFactor;
                        pongConfig.ScreenOffset = screenOffset;
                        pongConfig.Save(GetStorageDevice());
                        currentState = GameState.MainMenu;
                    }

                    break;
                
                case GameState.Running:
                    startedGame = true;

                    if (inputState.PauseGame)
                        currentState = GameState.Paused;

                    if (GamePad.GetState(PlayerIndex.One).DPad.Up == ButtonState.Pressed 
#if !ZUNE
                        || Keyboard.GetState().IsKeyDown(Keys.W)
#endif
                        )
                        gameScreen.RedPaddleDirection = PaddleDirection.Up;
                    else if (GamePad.GetState(PlayerIndex.One).DPad.Down == ButtonState.Pressed
#if !ZUNE
                        || Keyboard.GetState().IsKeyDown(Keys.S)
#endif
                        )
                        gameScreen.RedPaddleDirection = PaddleDirection.Down;
                    else
                        gameScreen.RedPaddleDirection = PaddleDirection.Stop;

                    if (GamePad.GetState(PlayerIndex.Two).DPad.Up == ButtonState.Pressed 
#if !ZUNE
                        || Keyboard.GetState().IsKeyDown(Keys.Up)
#endif
                        )
                        gameScreen.BluePaddleDirection = PaddleDirection.Up;
                    else if (GamePad.GetState(PlayerIndex.Two).DPad.Down == ButtonState.Pressed
#if !ZUNE
                        || Keyboard.GetState().IsKeyDown(Keys.Down)
#endif
                        )
                        gameScreen.BluePaddleDirection = PaddleDirection.Down;
                    else
                        gameScreen.BluePaddleDirection = PaddleDirection.Stop;

                    gameScreen.NextPosition(gameTicksPerSecond);

                    break;

                case GameState.GameOver:
                    if (inputState.MenuCancel || inputState.MenuSelect)
                        currentState = GameState.MainMenu;
                    break;

                case GameState.Paused:
                    if (inputState.MenuSelect)
                        currentState = GameState.Running;

                    if (inputState.MenuCancel)
                        currentState = GameState.MainMenu;

                    break;

                case GameState.ExitScreen:
                    if (inputState.MenuCancel)
                        currentState = GameState.MainMenu;

                    if (
                        GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed
#if ZUNE
                        || inputState.MenuSelect
#endif
                        )
                        this.Exit();

                    break;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(
                backgroundTexture,
                new Rectangle(
                0,
                0,
                GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width,
                GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height
                ),
                Color.Black
            );
            
            switch (currentState)
            {
                case GameState.SplashScreen:
                    break;
                case GameState.TitleScreen:
                    TitleScreenMode();
                    break;
                case GameState.MainMenu:
                    MainMenuScreenMode();
                    break;
                case GameState.SettingsMenu:
                    SettingsScreenMode();
                    break;
                case GameState.ScalingScreen:
                    ScalingScreenMode();
                    break;
                case GameState.Running:
                    GameScreenMode();
                    break;
                case GameState.Paused:
                    PausedScreenMode();
                    break;
                case GameState.ExitScreen:
                    ExitScreenMode();
                    break;
                case GameState.GameOver:
                    GameOverScreenMode();
                    break;
            }

            spriteBatch.End();
            base.Draw(gameTime);
        }

        #region Renger Helpers

        private void RenderSprite(Texture2D texture, Vector2 position, Rectangle sourceRectangle, Color hueTone)
        {
            spriteBatch.Draw(
                texture,
                new Rectangle(
                    (int)(position.X * scalingFactor) + (int)screenOffset.X,
                    (int)(position.Y * scalingFactor) + (int)screenOffset.Y,
                    (int)(sourceRectangle.Width * scalingFactor),
                    (int)(sourceRectangle.Height * scalingFactor)
                ),
                sourceRectangle,
                hueTone
                );
        }

        private void RenderString(string input, Vector2 position, Color hueTone)
        {
            spriteBatch.DrawString(
                pongFont, 
                input, 
                new Vector2(
                    (int)(position.X * scalingFactor) + (int)screenOffset.X,
                    (int)(position.Y * scalingFactor) + (int)screenOffset.Y
                ),
                hueTone
                );
        }

        private Vector2 CenterString(string input)
        {
            return Subtract(screenCenter, Divide(pongFont.MeasureString(input), 2));
        }

        private void RenderSprite(Sprite sprite, Vector2 position, Color hueTone)
        {
            RenderSprite(sprite.Texture, position, sprite.SourceRectangle, hueTone);
        }

        private Vector2 CenterSprite(Sprite sprite)
        {
            return Subtract(screenCenter, Divide(sprite.SourceRectangle, 2));
        }

        #endregion

        #region ScreenModes

        private void TitleScreenMode()
        {
            RenderSprite(backgroundTexture, new Vector2(0, 0), new Rectangle(0, 0, (int)standardDimentions.X, (int)standardDimentions.Y), Color.White);

            RenderSprite(sprites.Ball, titleScreen.LastPosition, Color.White);

            RenderSprite(sprites.RedPaddle, titleScreen.RedPaddle, Color.White);
            RenderSprite(sprites.BluePaddle, titleScreen.BluePaddle, Color.White);

            RenderSprite(sprites.MainLogo, CenterSprite(sprites.MainLogo), Color.White);
        }

        private void GameScreenMode()
        {
            RenderSprite(backgroundTexture, new Vector2(0, 0), new Rectangle(0, 0, (int)standardDimentions.X, (int)standardDimentions.Y), Color.White);

            RenderSprite(sprites.Ball, gameScreen.LastPosition, Color.White);

            RenderSprite(sprites.RedPaddle, gameScreen.RedPaddle, Color.White);
            RenderSprite(sprites.BluePaddle, gameScreen.BluePaddle, Color.White);

            const string scoreBoardFormat = "{0} - {1}";
            string scoreBoard = string.Format(scoreBoardFormat, new object[] { scoreKeeper[0], scoreKeeper[1] });
            Vector2 scoreBoardPos = CenterString(scoreBoard);
            scoreBoardPos.Y = spacing;
            RenderString(scoreBoard, scoreBoardPos, Color.Moccasin);
        }

        private void GameOverScreenMode()
        {
            GameScreenMode();

            switch (playerWon)
            {
                case PlayerIndex.One:
                    RenderSprite(sprites.RedWon, CenterSprite(sprites.RedWon), Color.White);
                    break;

                case PlayerIndex.Two:
                    RenderSprite(sprites.BlueWon, CenterSprite(sprites.BlueWon), Color.White);
                    break;
            }
        }

        private void ScalingScreenMode()
        {
            RenderSprite(backgroundTexture, new Vector2(), fullScreen, Color.White);
            RenderSprite(scaleTexture, new Vector2(), fullScreen, Color.White);

            const string CONFIRM = "Adjust screen";
            Vector2 confirmPos = CenterString(CONFIRM);
            RenderString(CONFIRM, confirmPos, Color.Moccasin);
            
        }

        private void MainMenuScreenMode()
        {
            RenderSprite(backgroundTexture, new Vector2(0, 0), new Rectangle(0, 0, (int)standardDimentions.X, (int)standardDimentions.Y), Color.White);

            Vector2 _logoPos = CenterSprite(sprites.MainLogo);
            _logoPos.Y = sprites.MainLogo.Size.Y;
            RenderSprite(sprites.MainLogo, _logoPos, Color.White);


            Vector2 _singlePos = CenterSprite(sprites.SinglePlayer);
            _singlePos.Y = (int)(sprites.SinglePlayer.Size.Y + sprites.MainLogo.Size.Y) * 2;
            RenderSprite(mainMenuMode == MenuMode.SinglePlayer ? sprites.SinglePlayerHighlighted : sprites.SinglePlayer, _singlePos, Color.White);
            
            Vector2 _multiPos = CenterSprite(sprites.MultiPlayer);
            _multiPos.Y = (int)(sprites.MultiPlayer.Size.Y + sprites.SinglePlayer.Size.Y + sprites.MainLogo.Size.Y) * 2;
            RenderSprite(mainMenuMode == MenuMode.MultiPlayer ? sprites.MultiPlayerHighlighted : sprites.MultiPlayer, _multiPos, Color.White);

            Vector2 _optionPos = CenterSprite(sprites.Option);
            _optionPos.Y = (int)(sprites.Option.Size.Y + sprites.MultiPlayer.Size.Y + sprites.SinglePlayer.Size.Y + sprites.MainLogo.Size.Y) * 2;
            RenderSprite(mainMenuMode == MenuMode.Option ? sprites.OptionHighlighted : sprites.Option, _optionPos, Color.White);

            Vector2 _exitPos = CenterSprite(sprites.Exit);
            _exitPos.Y = (int)(sprites.Exit.Size.Y + sprites.Option.Size.Y + sprites.MultiPlayer.Size.Y + sprites.SinglePlayer.Size.Y + sprites.MainLogo.Size.Y) * 2;
            RenderSprite(mainMenuMode == MenuMode.Exit ? sprites.ExitHighlighted : sprites.Exit, _exitPos, Color.White);
        }

        private void SettingsScreenMode()
        {
            RenderSprite(backgroundTexture, new Vector2(0, 0), new Rectangle(0, 0, (int)standardDimentions.X, (int)standardDimentions.Y), Color.White);

            const string SCALING_SCREEN = "Adjust Screen";
            Vector2 scalingOptionPos = CenterString(SCALING_SCREEN);
            scalingOptionPos.Y -= pongFont.MeasureString(SCALING_SCREEN).Y;
            RenderString(SCALING_SCREEN, scalingOptionPos, optionMenuMode == OptionMode.AdjustScreen ? Color.LightCoral : Color.LightBlue);

            const string MAIN_MENU_SCREEN = "Main Menu";
            Vector2 mainMenuOptionPos = CenterString(MAIN_MENU_SCREEN);
            mainMenuOptionPos.Y += pongFont.MeasureString(MAIN_MENU_SCREEN).Y;
            RenderString(MAIN_MENU_SCREEN, mainMenuOptionPos, optionMenuMode == OptionMode.MainMenu ? Color.LightCoral : Color.LightBlue);
        }

        private void ExitScreenMode()
        {
            RenderSprite(backgroundTexture, new Vector2(0, 0), new Rectangle(0, 0, (int)standardDimentions.X, (int)standardDimentions.Y), Color.White);
            const string CONFIRM = "Start to Exit, Select to go back";
            Vector2 confirmPos = CenterString(CONFIRM);
            RenderString(CONFIRM, confirmPos, Color.LawnGreen);
        }

        private void PausedScreenMode()
        {
            RenderSprite(backgroundTexture, new Vector2(0, 0), new Rectangle(0, 0, (int)standardDimentions.X, (int)standardDimentions.Y), Color.White);
            const string CONFIRM = "PAUSED";
            Vector2 confirmPos = CenterString(CONFIRM);
            RenderString(CONFIRM, confirmPos, Color.Moccasin);
        }

        #endregion
    }
}
