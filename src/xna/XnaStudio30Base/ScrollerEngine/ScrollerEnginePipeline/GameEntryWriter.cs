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
    public class GameEntryWriter : ContentTypeWriter<GameEntry>
    {
        protected override void Write(ContentWriter output, GameEntry value)
        {
            output.WriteObject(value.FirstLevel);
            output.WriteObject(value.Levels);
            output.WriteObject(value.AvailableCharacters);
            output.WriteObject(value.AvailableEnemies);
            output.WriteObject(value.DefaultFontName);
        }

        public override string GetRuntimeReader(TargetPlatform targetPlatform)
        {
            return typeof(GameEntry.GameEntryReader).AssemblyQualifiedName;
        }
    }
}
