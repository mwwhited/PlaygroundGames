using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BYBFSideScrollerData;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;
using Microsoft.Xna.Framework;

namespace BYBFSideScrollerProcessors
{
    [ContentTypeWriter]
    public class ScreenWriter : ContentTypeWriter<Screen>
    {
        protected override void Write(ContentWriter output, Screen value)
        {
            output.WriteObject(value.Cells);
            output.WriteObject(value.CellTypeTexture);
        }

        public override string GetRuntimeReader(TargetPlatform targetPlatform)
        {
            return typeof(ScreenReader).AssemblyQualifiedName;
        }
    }
}
