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
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;
using System.Diagnostics;
using System.Threading;

using BYBFSideScrollerData;

namespace BYBFSideScroller
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Level level = null;

        const int screenWidth = 20;
        const int screenHeight = 15;

#if ZUNE
        const int cellSize = 16;
#else
        const int cellSize = 32;
#endif

        Vector2 LastPosition = new Vector2(-1f, -1f);
        GamePadState lastGamePadState;
#if !ZUNE
        KeyboardState lastKeyboardState;
#endif

        public Game1()
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

            level = this.Content.Load<Level>("Level001").GetClone();

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        private void ReloadLevel()
        {
            level = this.Content.Load<Level>("Level001").GetClone();
            FindBack();
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            var currentPad = GamePad.GetState(PlayerIndex.One);
            var currentKey = Keyboard.GetState();

            // Allows the game to exit
            if (currentPad.Buttons.Back == ButtonState.Pressed ||
                currentKey.IsKeyDown(Keys.Escape))
                this.Exit();

#if !ZUNE
            if (
                (currentKey.IsKeyDown(Keys.R) && lastKeyboardState.IsKeyUp(Keys.R))||
                (currentPad.Buttons.LeftStick == ButtonState.Pressed && currentPad.Buttons.LeftStick != lastGamePadState.Buttons.LeftStick))
                ReloadLevel();

            if (
                (currentKey.IsKeyDown(Keys.B) && lastKeyboardState.IsKeyUp(Keys.B))||
                (currentPad.Buttons.LeftShoulder == ButtonState.Pressed && currentPad.Buttons.LeftShoulder != lastGamePadState.Buttons.LeftShoulder))
                FindBack();

            if (
                (currentKey.IsKeyDown(Keys.F) && lastKeyboardState.IsKeyUp(Keys.F))||
                (currentPad.Buttons.RightShoulder == ButtonState.Pressed && currentPad.Buttons.RightShoulder != lastGamePadState.Buttons.RightShoulder))
                FindForward();
#endif

            if (LastPosition.X < 0)
                FindBack();


            var nextPosition = LastPosition;

            if (
#if !ZUNE
                (currentKey.IsKeyDown(Keys.Down) && lastKeyboardState.IsKeyUp(Keys.Down)) ||
                (currentPad.Buttons.A == ButtonState.Pressed && currentPad.Buttons.A != lastGamePadState.Buttons.A) ||
                (currentPad.DPad.Down == ButtonState.Pressed && currentPad.DPad.Down != lastGamePadState.DPad.Down)
#else
                (currentPad.DPad.Left == ButtonState.Pressed && currentPad.DPad.Left != lastGamePadState.DPad.Left)
#endif
)
                nextPosition.Y++;

            if (
#if !ZUNE
                (currentKey.IsKeyDown(Keys.Up) && lastKeyboardState.IsKeyUp(Keys.Up)) ||
                (currentPad.Buttons.Y == ButtonState.Pressed && currentPad.Buttons.Y != lastGamePadState.Buttons.Y) ||
                (currentPad.DPad.Up == ButtonState.Pressed && currentPad.DPad.Up != lastGamePadState.DPad.Up)
#else
                (currentPad.DPad.Right == ButtonState.Pressed && currentPad.DPad.Right != lastGamePadState.DPad.Right)
#endif
)
                nextPosition.Y--;

            if (
#if !ZUNE
                (currentKey.IsKeyDown(Keys.Right) && lastKeyboardState.IsKeyUp(Keys.Right)) ||
                (currentPad.Buttons.B == ButtonState.Pressed && currentPad.Buttons.B != lastGamePadState.Buttons.B) ||
                (currentPad.DPad.Right == ButtonState.Pressed && currentPad.DPad.Right != lastGamePadState.DPad.Right)
#else
                (currentPad.DPad.Down == ButtonState.Pressed && currentPad.DPad.Down != lastGamePadState.DPad.Down)
#endif
)
            {
                if (level.CurrentScreen.Cells[(int)nextPosition.X + (int)nextPosition.Y * screenWidth].Type == CellType.Forward)
                {
                    level.CurrentScreenIndex++;
                    FindBack();
                    Debug.WriteLine(string.Format("you are on screen: {0}", level.CurrentScreenIndex));
                    goto bottom;
                }
                else
                    nextPosition.X++;
            }

            if (
#if !ZUNE
                (currentKey.IsKeyDown(Keys.Left) && lastKeyboardState.IsKeyUp(Keys.Left)) ||
                (currentPad.Buttons.X == ButtonState.Pressed && currentPad.Buttons.X != lastGamePadState.Buttons.X) ||
                (currentPad.DPad.Left == ButtonState.Pressed && currentPad.DPad.Left != lastGamePadState.DPad.Left)
#else
                (currentPad.DPad.Up == ButtonState.Pressed && currentPad.DPad.Up != lastGamePadState.DPad.Up)
#endif
            )
            {
                if (level.CurrentScreen.Cells[(int)nextPosition.X + (int)nextPosition.Y * screenWidth].Type == CellType.Back)
                {
                    level.CurrentScreenIndex--;
                    FindForward();
                    Debug.WriteLine(string.Format("you are on screen: {0}", level.CurrentScreenIndex));
                    goto bottom;
                }
                else
                    nextPosition.X--;
            }

            if (nextPosition.X >= screenWidth)
                nextPosition.X = screenWidth - 1;
            if (nextPosition.Y >= screenHeight)
                LastPosition.Y = screenHeight - 1;

            if (nextPosition.X <= 0)
                nextPosition.X = 0;
            if (nextPosition.Y <= 0)
                nextPosition.Y = 0;

#if !ZUNE
            //if (currentPad.Buttons.RightStick == ButtonState.Pressed && currentPad.Buttons.RightStick != lastGamePadState.Buttons.RightStick)
            //    IsCollision();

            nextPosition += currentPad.ThumbSticks.Left * new Vector2(1,-1);
#endif

            if (currentPad.ThumbSticks.Left.X != lastGamePadState.ThumbSticks.Left.X)
            {
                Debug.WriteLine(string.Format("Left Stick: {0}", currentPad.ThumbSticks.Left.X * .9f));
                Debug.WriteLine(string.Format("Position: {0}", nextPosition));
            }
                        
            switch (level.CurrentScreen.Cells[(int)nextPosition.X + (int)nextPosition.Y * screenWidth].Type)
            {
                case CellType.BoxBreakable:
                    if (nextPosition.Y < LastPosition.Y)
                        level.CurrentScreen.Cells[(int)nextPosition.X + (int)nextPosition.Y * screenWidth].Type = CellType.Empty;
                    else
                        goto case CellType.Box;
                    goto case CellType.Empty;

                case CellType.Box:
                    //if (nextPosition.X > LastPosition.X)
                    //    GamePad.SetVibration(PlayerIndex.One, 0f, 1f);
                    //else if (nextPosition.X < LastPosition.X)
                    //    GamePad.SetVibration(PlayerIndex.One, 1f, 0f);
                    //else
                    //    GamePad.SetVibration(PlayerIndex.One, 1f, 1f);

                    nextPosition = LastPosition;
                    break;

                case CellType.Empty:
                case CellType.Back:
                case CellType.Forward:
                default:
                    //GamePad.SetVibration(PlayerIndex.One, 0f, 0f);
                    break;
            }

            LastPosition = nextPosition;
        bottom:
            lastGamePadState = currentPad;
#if !ZUNE
            lastKeyboardState = currentKey;
#endif

            base.Update(gameTime);
        }

        private void FindForward()
        {
            int start = -1;
            for (int i = 0; i < level.CurrentScreen.Cells.Length; i++)
            {
                if (level.CurrentScreen.Cells[i].Type == CellType.Forward)
                    start = i;
            }

            if (start == -1)
                for (int i = 0; i < level.CurrentScreen.Cells.Length; i++)
                {
                    if (level.CurrentScreen.Cells[i].Type == CellType.Empty)
                        start = i;
                }

            if (start == -1)
                throw new InvalidOperationException("No Screen Start Point");
            else
            {
                LastPosition.Y = (int)start / screenWidth;
                LastPosition.X = start % screenWidth;
            }
            Debug.WriteLine(string.Format("you are on screen: {0} (fwd)", level.CurrentScreenIndex));
        }

        private void FindBack()
        {
            int start = -1;
            for (int i = 0; i < level.CurrentScreen.Cells.Length; i++)
            {
                if (level.CurrentScreen.Cells[i].Type == CellType.Back)
                    start = i;
            }

            if (start == -1)
                for (int i = 0; i < level.CurrentScreen.Cells.Length; i++)
                {
                    if (level.CurrentScreen.Cells[i].Type == CellType.Empty)
                        start = i;
                }

            if (start == -1)
                throw new InvalidOperationException("No Screen Start Point");
            else
            {
                LastPosition.Y = (int)start / screenWidth;
                LastPosition.X = start % screenWidth;
            }
            Debug.WriteLine(string.Format("you are on screen: {0} (bck)", level.CurrentScreenIndex));
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            for (int x = 0; x < screenWidth; x++)
            {
                for (int y = 0; y < screenHeight; y++)
                {
                    RenderTile(level.CurrentScreen.Cells[x + y * screenWidth].Type, x, y);
                }
            }

            RenderGuy(LastPosition.X, LastPosition.Y);

            spriteBatch.End();

            base.Draw(gameTime);
        }

#if ZUNE
        private void RenderTile(CellType cellType, int y, int x)
        {
            x = screenHeight - x;
            Texture2D texture = this.Content.Load<Texture2D>(level.CurrentScreen.CellTypeTexture[cellType]);
            spriteBatch.Draw(texture,
                new Vector2(x, y) * cellSize,
                null, Color.White, MathHelper.ToRadians(90), new Vector2(0, 0),
                (float)cellSize / (float)texture.Width,
                SpriteEffects.None, 0f);
        }
#else
        private void RenderTile(CellType cellType, int x, int y)
        {
            x += 2; y += 2;
            Texture2D texture = this.Content.Load<Texture2D>(level.CurrentScreen.CellTypeTexture[cellType]);
            spriteBatch.Draw(texture,
                new Vector2(x, y) * cellSize,
                null, Color.White, MathHelper.ToDegrees(0), new Vector2(0, 0),
                (float)cellSize / (float)texture.Width,
                SpriteEffects.None, 0f);
        }
#endif

#if ZUNE
        private void RenderGuy(float y, float x)
        {
            x = screenHeight - x;
            Texture2D texture = this.Content.Load<Texture2D>("LittleDude");
            spriteBatch.Draw(texture,
                new Vector2(x, y) * cellSize,
                null, Color.White, MathHelper.ToRadians(90), new Vector2(0, 0),
                (float)cellSize / (float)texture.Width,
                SpriteEffects.None, 0f);
        }
#else
        private void RenderGuy(float x, float y)
        {
            x += 2; y += 2;
            Texture2D texture = this.Content.Load<Texture2D>("LittleDude");
            spriteBatch.Draw(texture,
                new Vector2(x, y) * cellSize,
                null, Color.White, MathHelper.ToDegrees(0), new Vector2(0, 0),
                (float)cellSize / (float)texture.Width,
                SpriteEffects.None, 0f);
            //spriteBatch.Draw(texture, new Rectangle(x * cellSize, y * cellSize, cellSize, cellSize), Color.White);
        }
#endif


        private void RenderSprite(Texture2D texture, Vector2 position, Rectangle sourceRectangle, Color hueTone)
        {
            spriteBatch.Draw(
                texture,
                new Rectangle(
                    (int)(position.X),
                    (int)(position.Y),
                    (int)(sourceRectangle.Width),
                    (int)(sourceRectangle.Height)
                ),
                sourceRectangle,
                hueTone
                );
        }

        bool IsCollision()
        {
            int xMin = (int)Math.Floor(LastPosition.X);
            int xMax = (int)Math.Ceiling(LastPosition.X);

            int yMin = (int)Math.Floor(LastPosition.Y);
            int yMax = (int)Math.Ceiling(LastPosition.Y);

            if (xMin < 0 || xMax < 0 || yMin < 0 || yMax < 0 ||
                xMin >= screenWidth || xMax >= screenWidth || yMin >= screenHeight || yMax >= screenHeight)
                return true;

            bool isHit = false;
            for (int x = xMin; x < xMax; x++)
            {
                for (int y = yMin; y < yMax; y++)
                {
                    var cellType = level.CurrentScreen.Cells[x + y * screenWidth].Type;
                    if (cellType != CellType.Box)
                        continue;

                    
                }
            }

            var c0 = level.CurrentScreen.Cells[xMin + yMin * screenWidth].Type;
            var c1 = level.CurrentScreen.Cells[xMin + yMax * screenWidth].Type;
            var c2 = level.CurrentScreen.Cells[yMax + yMin * screenWidth].Type;
            var c3 = level.CurrentScreen.Cells[yMax + yMax * screenWidth].Type;

            Debug.WriteLine(string.Format("Collision Min: {0}", new Vector2(xMin, yMin)));
            Debug.WriteLine(string.Format("Collision Max: {0}", new Vector2(xMax, yMax)));

            return false;
        }

        /// <summary>
        /// Determines if there is overlap of the non-transparent pixels
        /// between two sprites.
        /// </summary>
        /// <param name="rectangleA">Bounding rectangle of the first sprite</param>
        /// <param name="dataA">Pixel data of the first sprite</param>
        /// <param name="rectangleB">Bouding rectangle of the second sprite</param>
        /// <param name="dataB">Pixel data of the second sprite</param>
        /// <returns>True if non-transparent pixels overlap; false otherwise</returns>
        static bool IntersectPixels(Rectangle rectangleA, Color[] dataA,
                                    Rectangle rectangleB, Color[] dataB)
        {
            // Find the bounds of the rectangle intersection
            int top = Math.Max(rectangleA.Top, rectangleB.Top);
            int bottom = Math.Min(rectangleA.Bottom, rectangleB.Bottom);
            int left = Math.Max(rectangleA.Left, rectangleB.Left);
            int right = Math.Min(rectangleA.Right, rectangleB.Right);

            // Check every point within the intersection bounds
            for (int y = top; y < bottom; y++)
            {
                for (int x = left; x < right; x++)
                {
                    // Get the color of both pixels at this point
                    Color colorA = dataA[(x - rectangleA.Left) +
                                         (y - rectangleA.Top) * rectangleA.Width];
                    Color colorB = dataB[(x - rectangleB.Left) +
                                         (y - rectangleB.Top) * rectangleB.Width];

                    // If both pixels are not completely transparent,
                    if (colorA.A != 0 && colorB.A != 0)
                    {
                        // then an intersection has been found
                        return true;
                    }
                }
            }

            // No intersection found
            return false;
        }

    }
}
