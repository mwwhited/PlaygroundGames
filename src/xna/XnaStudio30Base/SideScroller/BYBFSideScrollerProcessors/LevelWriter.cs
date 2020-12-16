using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Graphics;
using Microsoft.Xna.Framework.Content.Pipeline.Processors;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;
using BYBFSideScrollerData;

namespace BYBFSideScrollerProcessors
{
    [ContentTypeWriter]
    public class LevelWriter : ContentTypeWriter<Level>
    {
        protected override void Write(ContentWriter output, Level value)
        {
            output.WriteObject(value.Screens);
        }

        public override string GetRuntimeReader(TargetPlatform targetPlatform)
        {
            return typeof(LevelReader).AssemblyQualifiedName;
        }
    }
}
