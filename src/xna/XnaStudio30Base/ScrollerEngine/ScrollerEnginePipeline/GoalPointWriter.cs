using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;
using ScrollerEngineData;

namespace ScrollerEnginePipeline
{
    [ContentTypeWriter]
    public class GoalPointWriter : ContentTypeWriter<GoalPoint>
    {
        protected override void Write(ContentWriter output, GoalPoint value)
        {
            output.WriteObject(value.SavedBounds);
            output.WriteObject(value.TargetLevelName);
        }

        public override string GetRuntimeReader(TargetPlatform targetPlatform)
        {
            return typeof(GoalPoint.GoalPointReader).AssemblyQualifiedName;
        }
    }
}
