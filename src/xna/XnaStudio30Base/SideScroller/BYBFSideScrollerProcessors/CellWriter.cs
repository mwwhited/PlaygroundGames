using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;
using BYBFSideScrollerData;
using Microsoft.Xna.Framework;

namespace BYBFSideScrollerProcessors
{
    [ContentTypeWriter]
    public class CellWriter : ContentTypeWriter<Cell>
    {
        protected override void Write(ContentWriter output, Cell value)
        {
            output.WriteObject(value.Type);
        }

        public override string GetRuntimeReader(TargetPlatform targetPlatform)
        {
            return typeof(CellReader).AssemblyQualifiedName;
        }
    }
}
