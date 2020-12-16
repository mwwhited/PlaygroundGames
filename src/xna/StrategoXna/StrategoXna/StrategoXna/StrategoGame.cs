using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace StrategoXna
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class StrategoGame : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        private readonly Dictionary<PlayerIndex, IPlayer> _players = new Dictionary<PlayerIndex, IPlayer>();
        private GameBoard _gameBoard;
        private MouseState _lastMousePosition;

        private Texture2D _lineX;
        private Texture2D _lineY;

        public StrategoGame()
        {
            graphics = new GraphicsDeviceManager(this);
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

            var player1 = new Player(PlayerIndex.One);
            var player2 = new Player(PlayerIndex.Two);
            this._players.Add(player1.PlayerIndex, player1);
            this._players.Add(player2.PlayerIndex, player2);

            var pieces = this._players.Values.SelectMany(p => p.Pieces);

            this._gameBoard = new GameBoard();

            foreach (var item in pieces.Select((p, i) => new { Piece = p, Index = i }))
            {
                var y = item.Index / Globals.MaxRange;
                var x = item.Index % Globals.MaxRange;

                item.Piece.SetPosition(x, y + (item.Piece.Player.PlayerIndex == PlayerIndex.Two ? 2 : 0));
            }

            this._lineX = new Texture2D(this.graphics.GraphicsDevice, 1, Globals.TileSize * Globals.MaxRange, false, SurfaceFormat.Color);
            this._lineX.SetData<int>(Enumerable.Range(0, this._lineX.Width * this._lineX.Height).Select(i => 0xffffff).ToArray(), 0, this._lineX.Width * this._lineX.Height);
            this._lineY = new Texture2D(this.graphics.GraphicsDevice, Globals.TileSize * Globals.MaxRange, 1, false, SurfaceFormat.Color);
            this._lineY.SetData<int>(Enumerable.Range(0, this._lineY.Width * this._lineY.Height).Select(i => 0xffffff).ToArray(), 0, this._lineY.Width * this._lineY.Height);

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            this.spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            this._gameBoard.Load(this);
            foreach (var player in _players.Values)
                player.Load(this);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
            this._gameBoard.Load(this);
            foreach (var player in _players.Values)
                player.Unload(this);
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
                Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();

            // TODO: Add your update logic here
            var currentMousePosition = Mouse.GetState();
            if (currentMousePosition != _lastMousePosition)
            {
                var player = this._players[PlayerIndex.One];
                player.Cursor.SetPosition(currentMousePosition.X, currentMousePosition.Y);

                var position = ClampToTile(currentMousePosition.X, currentMousePosition.Y);
                var player2 = this._players[PlayerIndex.Two];
                player2.Cursor.SetPosition(position.Item1, position.Item2);
            }

            this._lastMousePosition = currentMousePosition;

            base.Update(gameTime);
        }

        private Tuple<int, int> ClampToTile(int x, int y)
        {
            var tx = (int)MathHelper.Clamp(x / Globals.TileSize, 0f, Globals.MaxRange - 1);
            var ty = (int)MathHelper.Clamp(y / Globals.TileSize, 0f, Globals.MaxRange - 1);

            if (new[] { 4, 5, }.Contains(ty))
                if (new[] { 2, 6, }.Contains(tx))
                    tx--;
                else if (new[] { 3, 7, }.Contains(tx))
                    tx++;

            return Tuple.Create(
                tx * Globals.TileSize + Globals.TileSize / 2,
                ty * Globals.TileSize + Globals.TileSize / 2
                );
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            this.GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            //var offset = Matrix.CreateTranslation(
            //    this.spriteBatch.GraphicsDevice.Viewport.Width / 2 - Globals.MaxRange * Globals.TileSize / 2,
            //    this.spriteBatch.GraphicsDevice.Viewport.Height / 2 - Globals.MaxRange * Globals.TileSize / 2,
            //    0);
            //this.spriteBatch.Begin(
            //    SpriteSortMode.Immediate,
            //    BlendState.AlphaBlend,
            //    null, //SamplerState.LinearClamp,
            //    null, //DepthStencilState.None,
            //    null, //RasterizerState.CullCounterClockwise,
            //    null,
            //    offset
            //    );

            this.spriteBatch.Begin();
            
            foreach (var i in Enumerable.Range(0, Globals.MaxRange))
            {
                this.spriteBatch.Draw(this._lineX, new Rectangle(i * Globals.TileSize, 0, this._lineX.Width, this._lineX.Height), Color.Yellow);
                this.spriteBatch.Draw(this._lineY, new Rectangle(0, i * Globals.TileSize, this._lineY.Width, this._lineY.Height), Color.Green);
            }

            this._gameBoard.Draw(this, gameTime, spriteBatch);

            foreach (var player in this._players.Values)
                player.Draw(this, gameTime, spriteBatch);

            foreach (var player in this._players.Values)
                player.Cursor.Draw(this, gameTime, spriteBatch);

            this.spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
