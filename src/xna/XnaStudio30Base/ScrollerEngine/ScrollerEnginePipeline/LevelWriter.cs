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
    public class LevelWriter : ContentTypeWriter<Level>
    {
        protected override void Write(ContentWriter output, Level value)
        {
            output.WriteObject(value.TextureName);
            output.WriteObject(value.GravityWind);
            output.WriteObject(value.DefaultStartPoint);
            output.WriteObject(value.StartPoints);
            output.WriteObject(value.Goals);
            output.WriteObject(value.EnemyStartPoints);
            output.WriteObject(value.BackdropName);
        }

        public override string GetRuntimeReader(TargetPlatform targetPlatform)
        {
            return typeof(Level.LevelReader).AssemblyQualifiedName;
        }
    }
}
