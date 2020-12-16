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
    public class EnemyStartPointWriter : ContentTypeWriter<EnemyStartPoint>
    {
        protected override void Write(ContentWriter output, EnemyStartPoint value)
        {
            output.WriteObject(value.SavedBounds);
            output.WriteObject(value.EnemyName);
        }

        public override string GetRuntimeReader(TargetPlatform targetPlatform)
        {
            return typeof(EnemyStartPoint.EnemyStartPointReader).AssemblyQualifiedName;
        }
    }
}
