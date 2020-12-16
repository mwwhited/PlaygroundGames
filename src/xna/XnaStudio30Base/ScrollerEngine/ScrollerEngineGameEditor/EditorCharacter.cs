using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ScrollerEngineData;

namespace ScrollerEngineGameEditor
{
    public class EditorCharacter : Character
    {
        Color cursorColor = Color.White;
        MouseState lastMouse;
        KeyboardState lastKeyboard;

        public EditorCharacter()
        {
            TextureName = "Cursor";
        }

        protected int? IsCollisionEnemyStartPoint(Level level)
        {
            foreach (var item in level.EnemyStartPoints)
            {
                var rect = Rectangle.Intersect(Bounds, item.Value.Bounds);
                if (!(rect.Width == 0 && rect.Height == 0))
                    return item.Key;
            }
            return null;
        }
        
        public override void Update(GameTime gameTime, GameEntry gameEntry, Level level)
        {
            var editLevel = level as EditorLevel;

            var currentPad = GamePad.GetState(this.PlayerIndex);
            var currentKeyboard = Keyboard.GetState(PlayerIndex.One);

            float runSpeed = runFactor * (currentPad.Triggers.Left + 1);

            if (this.PlayerIndex == PlayerIndex.One)
            {
                var currentMouse = Mouse.GetState();

                if (currentMouse.X != lastMouse.X || currentMouse.Y != lastMouse.Y)
                    Position = new Vector2(currentMouse.X, currentMouse.Y);

                lastMouse = currentMouse;
            }

            if (currentPad.DPad.Up == ButtonState.Pressed && lastPad.DPad.Up == ButtonState.Released)
                Position.Y--;
            if (currentPad.DPad.Down == ButtonState.Pressed && lastPad.DPad.Down == ButtonState.Released)
                Position.Y++;
            if (currentPad.DPad.Left == ButtonState.Pressed && lastPad.DPad.Left == ButtonState.Released)
                Position.X--;
            if (currentPad.DPad.Right == ButtonState.Pressed && lastPad.DPad.Right == ButtonState.Released)
                Position.X++;

            Position += currentPad.ThumbSticks.Left * new Vector2(1, -1) * runSpeed;

            var selectedStartPoint = IsCollisionStartPoints(level);
            var selectedGoalPoint = IsCollisionGoalPoints(level);
            var selectedEnemy = IsCollisionEnemyStartPoint(level);

            if (selectedStartPoint.HasValue || selectedGoalPoint.HasValue || selectedEnemy.HasValue)
                cursorColor = Color.Blue;
            else
                cursorColor = Color.White;

            if (!IsCollision(level))
            {
                if (currentPad.Buttons.A == ButtonState.Pressed && lastPad.Buttons.A == ButtonState.Released)
                {
                    if (!selectedStartPoint.HasValue)
                        editLevel.AddStartPoint(this.Bounds);
                    else
                        editLevel.DeleteStartPoint(selectedStartPoint.Value);
                }

                if (currentPad.Buttons.B == ButtonState.Pressed && lastPad.Buttons.B == ButtonState.Released)
                {
                    if (!selectedGoalPoint.HasValue)
                        editLevel.AddGoalPoint(this.Bounds);
                    else
                        editLevel.DeleteGoalPoint(selectedGoalPoint.Value);
                }

                if (currentPad.Buttons.X == ButtonState.Pressed && lastPad.Buttons.X == ButtonState.Released)
                {
                    if (!selectedEnemy.HasValue)
                        editLevel.AddEnemy(this.Bounds);
                    else
                        editLevel.DeleteEnemy(selectedEnemy.Value);
                }

                if (currentPad.Buttons.RightShoulder == ButtonState.Pressed && lastPad.Buttons.RightShoulder == ButtonState.Released)
                {
                    level.ChangeLevel("Level001");
                }
                if (currentPad.Buttons.LeftShoulder == ButtonState.Pressed && lastPad.Buttons.LeftShoulder == ButtonState.Released)
                {
                    level.ChangeLevel("Level002");
                }
            }
            else
                cursorColor = Color.Red;

            lastPad = currentPad;
            lastKeyboard = currentKeyboard;
        }

        public override void Draw(SpriteBatch spriteBatch, Color color)
        {
            base.Draw(spriteBatch, cursorColor);
        }

        public static EditorCharacter Upgrade(Character level)
        {
            return level.Upgrade<EditorCharacter>();
        }
    }
}
