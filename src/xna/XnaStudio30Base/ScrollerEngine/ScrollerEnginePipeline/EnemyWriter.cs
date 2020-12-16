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
    public class EnemyWriter : ContentTypeWriter<Enemy>
    {
        protected override void Write(ContentWriter output, Enemy value)
        {
            output.WriteObject(value.TextureName);
            output.WriteObject(value.jumpSpeed);
            output.WriteObject(value.runFactor);
            output.WriteObject(value.superPower);
            //output.WriteObject(value.StartPosition);
            output.WriteObject(value.JumpOdds);
            output.WriteObject(value.PowerJumpOdds);
        }

        public override string GetRuntimeReader(TargetPlatform targetPlatform)
        {
            return typeof(Enemy.EnemyReader).AssemblyQualifiedName;
        }
    }
}
