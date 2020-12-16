using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using WhitedUS.Controls;

namespace ScrollerEngineData
{
    public class Character : MovableGameObject<Character>
    {
        [ContentSerializerIgnore]
        protected GamePadState lastPad;
        [ContentSerializerIgnore]
        public PlayerIndex PlayerIndex { get; set; }
        [ContentSerializerIgnore]
        public int Points;
        [ContentSerializerIgnore]
        public int? lastEnemyHit = null;

#if WINDOWS
        [ContentSerializerIgnore]
        protected SpeechControlState lastSpeech;
#endif

        protected int? IsCollisionStartPoints(Level level)
        {
            foreach (var item in level.StartPoints)
            {
                var rect = Rectangle.Intersect(Bounds, item.Value.Bounds);
                if (!(rect.Width == 0 && rect.Height == 0))
                    return item.Key;
            }
            return null;
        }

        protected int? IsCollisionGoalPoints(Level level)
        {
            foreach (var item in level.Goals)
            {
                var rect = Rectangle.Intersect(Bounds, item.Value.Bounds);
                if (!(rect.Width == 0 && rect.Height == 0))
                    return item.Key;
            }
            return null;
        }

        protected int? IsCollisionEnemies(Level level)
        {
            foreach (var item in level.Enemies)
            {
                var rect = Rectangle.Intersect(Bounds, item.Value.Bounds);
                if (!(rect.Width == 0 && rect.Height == 0))
                    return item.Key;
            }
            return null;
        }

        public override void Update(GameTime gameTime, GameEntry gameEntry, Level level)
        {
            var currentPad = GamePad.GetState(PlayerIndex);
#if WINDOWS
            var currentSpeech = SpeechControl.GetState(PlayerIndex);
#endif

#if DEBUG
            if (currentPad.Buttons.RightShoulder == ButtonState.Pressed && lastPad.Buttons.RightShoulder == ButtonState.Released)
                level.SendMessage(string.Format("Score: {0}", Points));
#endif

            var run = currentPad.Triggers.Left;
            var moveX = currentPad.ThumbSticks.Left.X;
            var jump = currentPad.Buttons.A == ButtonState.Pressed && lastPad.Buttons.A == ButtonState.Released;
            jump = jump || (currentSpeech.EmulateButtons.A == ButtonState.Pressed && currentSpeech.EmulateButtons.A == ButtonState.Released);

            var jumpPower = currentPad.Buttons.Y == ButtonState.Pressed && lastPad.Buttons.Y == ButtonState.Released;

            Update(gameTime, gameEntry, level, run, moveX, jump, jumpPower);

            lastPad = currentPad;
#if WINDOWS
            lastSpeech = currentSpeech;
#endif

            var enemyHit = IsCollisionEnemies(level);
            var goalHit = IsCollisionGoalPoints(level);

            if (enemyHit.HasValue)
            {
                if (lastEnemyHit != enemyHit.Value)
                {
                    level.SendMessage(string.Format("id:{0} - Enemy Hit", enemyHit));
                    Points -= 50;
                    lastEnemyHit = enemyHit.Value;
                }
            }
            if (goalHit.HasValue)
            {
                level.SendMessage(string.Format("id:{0} - Goal Hit", goalHit));
                Points += 100;
                level.ChangeLevel(level.Goals[goalHit.Value].TargetLevelName);
            }
        }

        public class CharacterReader : ContentTypeReader<Character>
        {
            protected override Character Read(ContentReader input, Character existingInstance)
            {
                existingInstance = new Character();

                existingInstance.TextureName = input.ReadObject<string>();
                existingInstance.jumpSpeed = input.ReadObject<float>();
                existingInstance.runFactor = input.ReadObject<float>();
                existingInstance.superPower = input.ReadObject<float>();

                return existingInstance;
            }
        }
    }
}
