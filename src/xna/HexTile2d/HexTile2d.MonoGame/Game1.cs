using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Linq;

namespace HexTile2d.MonoGame
{
    // https://ostindiegames.wordpress.com/2015/01/29/converting-from-xna-to-monogame/
    public class Game1 : Game
    {
        private readonly string[] _tileSheetNames = { "BorderSheet", "Coastsheet11", "CoastSheet2", "Hillsheet13", "HillSheet2", "ResourceSheet1", "RouteSheet11" };
        private readonly Dictionary<string, Texture2D> _tileSheets = new Dictionary<string, Texture2D>();
        private Rectangle[] _sheetCells =
        {
            new Rectangle(0,0,45,51), new Rectangle(51,0,45,51), new Rectangle(102,0,45,51), new Rectangle(153,0,45,51),
            new Rectangle(0,51,45,51), new Rectangle(51,51,45,51), new Rectangle(102,51,45,51), new Rectangle(153,51,45,51),
            new Rectangle(0,102,45,51), new Rectangle(51,102,45,51), new Rectangle(102,102,45,51), new Rectangle(153,102,45,51),
            new Rectangle(0,153,45,51), new Rectangle(51,153,45,51), new Rectangle(102,153,45,51), new Rectangle(153,153,45,51),
        };
        private int _cellsWide;
        private int _cellsTall;

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);

            //this.SetHighestResolution(graphics);

            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            foreach (var sheetName in _tileSheetNames)
                _tileSheets.Add(sheetName, this.Content.Load<Texture2D>(sheetName));

            _cellsWide = graphics.GraphicsDevice.Viewport.Width / 45;
            _cellsTall = graphics.GraphicsDevice.Viewport.Height / 38;
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
                Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.Escape))
                this.Exit();

            // TODO: Add your update logic here
            var currentGamePad = GamePad.GetState(PlayerIndex.One);
            if (currentGamePad != _lastGamePad)
            {
                if (currentGamePad.Buttons.Y == ButtonState.Pressed)
                    _sheetOffset++;
                if (currentGamePad.Buttons.A == ButtonState.Pressed)
                    _sheetOffset--;
                if (currentGamePad.Buttons.X == ButtonState.Pressed)
                    _cellOffset++;
                if (currentGamePad.Buttons.B == ButtonState.Pressed)
                    _cellOffset--;
            }
            _lastGamePad = currentGamePad;
            var currentKeyboard = Keyboard.GetState(PlayerIndex.One);
            if (currentKeyboard != _lastKeyboard)
            {
                if (currentKeyboard.IsKeyDown(Keys.Left))
                    _sheetOffset++;
                if (currentKeyboard.IsKeyDown(Keys.Right))
                    _sheetOffset--;
                if (currentKeyboard.IsKeyDown(Keys.Up))
                    _cellOffset++;
                if (currentKeyboard.IsKeyDown(Keys.Down))
                    _cellOffset--;
            }
            _lastKeyboard = currentKeyboard;

            base.Update(gameTime);
        }

        KeyboardState _lastKeyboard;
        GamePadState _lastGamePad;
        int _sheetOffset;
        int _cellOffset;

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();

            // TODO: Add your drawing code here
            while (_sheetOffset < 0)
                _sheetOffset += _tileSheetNames.Length;
            while (_cellOffset < 0)
                _cellOffset += _sheetCells.Length;

            var currentSheetIndex = _sheetOffset;
            var currentCellIndex = _cellOffset;

            var cells = from x in Enumerable.Range(0, _cellsWide)
                        from y in Enumerable.Range(0, _cellsTall)
                        let sheetIndex = currentSheetIndex
                        let cellIndex = currentCellIndex + x + y * _cellsTall
                        select new
                        {
                            x,
                            y,
                            sheetIndex,
                            cellIndex,
                        };

            foreach (var cell in cells)
                DrawTile(spriteBatch, cell.sheetIndex, cell.cellIndex, cell.x, cell.y);

            spriteBatch.End();
            base.Draw(gameTime);
        }

        private void DrawTile(SpriteBatch spriteBatch, int currentSheetIndex, int currentCellIndex, int xPos, int yPos)
        {
            var x = xPos * 45 + (yPos % 2 == 0 ? 0 : 22);
            var y = yPos * 38;

            var sheetRect = new Rectangle(x, y, 45, 51);

            currentSheetIndex %= _tileSheetNames.Length;
            currentCellIndex %= _sheetCells.Length;

            var currentSheet = _tileSheets[_tileSheetNames[currentSheetIndex]];
            Rectangle? currentCell = _sheetCells != null ? currentCell = _sheetCells[currentCellIndex] : null;
            spriteBatch.Draw(currentSheet, sheetRect, currentCell, Color.White);
        }
    }
}
