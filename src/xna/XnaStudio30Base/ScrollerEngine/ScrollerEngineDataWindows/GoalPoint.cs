using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace ScrollerEngineData
{
    public class GoalPoint : GameObject<GoalPoint>
    {
        public Rectangle SavedBounds;
        public string TargetLevelName;

        public override Rectangle Bounds
        {
            get { return SavedBounds; }
            set { SavedBounds = value; }
        }

        public override object Clone()
        {
            return new GoalPoint()
            {
                SavedBounds = this.SavedBounds,
                TargetLevelName = this.TargetLevelName
            };
        }

        public class GoalPointReader : ContentTypeReader<GoalPoint>
        {
            protected override GoalPoint Read(ContentReader input, GoalPoint existingInstance)
            {
                existingInstance = new GoalPoint();

                existingInstance.SavedBounds = input.ReadObject<Rectangle>();
                existingInstance.TargetLevelName = input.ReadObject<string>();

                return existingInstance;
            }
        }
    }
}
