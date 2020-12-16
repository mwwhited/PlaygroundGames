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
    public class StartPointWriter : ContentTypeWriter<StartPoint>
    {
        protected override void Write(ContentWriter output, StartPoint value)
        {
            output.WriteObject(value.SavedBounds);
        }

        public override string GetRuntimeReader(TargetPlatform targetPlatform)
        {
            return typeof(StartPoint.StartPointReader).AssemblyQualifiedName;
        }
    }
}
