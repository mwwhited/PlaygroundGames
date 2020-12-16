using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ScrollerEngineData
{
    public class Enemy : MovableGameObject<Enemy>
    {
        public float JumpOdds = .75f;
        public float PowerJumpOdds = .75f;

        //public Vector2 StartPosition;

        public override void LoadContent(ContentManager contentManager)
        {
            base.LoadContent(contentManager);
            //Position = StartPosition;
        }

        public override void Update(GameTime gameTime, GameEntry gameEntry, Level level)
        {
            var run = Randomizer.RangeShort;
            var moveX = Randomizer.Range;

            var jump = Randomizer.Value.NextDouble() > JumpOdds;
            var jumpPower = Randomizer.Value.NextDouble() > PowerJumpOdds;

            Update(gameTime, gameEntry, level, run, moveX, jump, jumpPower);
        }

        public override object Clone()
        {
            var newCharacter = new Enemy();
            newCharacter.TextureName = this.TextureName;
            newCharacter.jumpSpeed = this.jumpSpeed;
            newCharacter.runFactor = this.runFactor;
            newCharacter.superPower = this.superPower;

            newCharacter.JumpOdds = this.JumpOdds;
            newCharacter.PowerJumpOdds = this.PowerJumpOdds;

            newCharacter.Texture = this.Texture;
            newCharacter.RealBounds = this.RealBounds;

            //newCharacter.StartPosition = this.StartPosition;

            return newCharacter;
        }

        public class EnemyReader : ContentTypeReader<Enemy>
        {
            protected override Enemy Read(ContentReader input, Enemy existingInstance)
            {
                existingInstance = new Enemy();

                existingInstance.TextureName = input.ReadObject<string>();
                existingInstance.jumpSpeed = input.ReadObject<float>();
                existingInstance.runFactor = input.ReadObject<float>();
                existingInstance.superPower = input.ReadObject<float>();
                //existingInstance.StartPosition = input.ReadObject<Vector2>();
                existingInstance.JumpOdds = input.ReadObject<float>();
                existingInstance.PowerJumpOdds = input.ReadObject<float>();

                return existingInstance;
            }
        }
    }
}
