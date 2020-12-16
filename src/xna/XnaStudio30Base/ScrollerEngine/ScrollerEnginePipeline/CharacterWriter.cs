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
    public class CharacterWriter : ContentTypeWriter<Character>
    {
        protected override void Write(ContentWriter output, Character value)
        {
            output.WriteObject(value.TextureName);
            output.WriteObject(value.jumpSpeed);
            output.WriteObject(value.runFactor);
            output.WriteObject(value.superPower);
        }

        public override string GetRuntimeReader(TargetPlatform targetPlatform)
        {
            return typeof(Character.CharacterReader).AssemblyQualifiedName;
        }
    }
}
