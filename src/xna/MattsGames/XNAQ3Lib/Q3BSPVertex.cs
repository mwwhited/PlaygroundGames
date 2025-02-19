///////////////////////////////////////////////////////////////////////
// Project: XNA Quake3 Lib
// Author: Aanand Narayanan
// Copyright (c) 2006-2007 All rights reserved
///////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace XNAQ3Lib
{
    [Serializable]
    public struct Q3BSPVertex
    {
        private Vector3 position;       // Position
        private Vector3 normal;         // Normal
        private Vector2 textureCoord;   // Diffuse Texture coord
        private Vector2 lightMapCoord;  // Light map coord
        private Color vertexColor;      // Vertex color

        public static readonly VertexElement[] VertexElements =
            new VertexElement[] { 
                new VertexElement(0, 0, VertexElementFormat.Vector3, 
                    VertexElementMethod.Default, VertexElementUsage.Position, 0), 
                new VertexElement(0, sizeof(float) * 3, VertexElementFormat.Vector3, 
                    VertexElementMethod.Default, VertexElementUsage.Normal, 0), 
                new VertexElement(0, sizeof(float) * 6, VertexElementFormat.Vector2, 
                    VertexElementMethod.Default, VertexElementUsage.TextureCoordinate, 0), 
                new VertexElement(0, sizeof(float) * 8, VertexElementFormat.Vector2, 
                    VertexElementMethod.Default, VertexElementUsage.TextureCoordinate, 1), 
                new VertexElement(0, sizeof(float) * 10, VertexElementFormat.Color, 
                    VertexElementMethod.Default, VertexElementUsage.Color, 0),
            };

        public Q3BSPVertex(Vector3 position, Vector3 normal, Vector2 textureCoord, Vector2 lightMapCoord, Color vertexColor)
        {
            this.position = position;
            this.normal = normal;
            this.textureCoord = textureCoord;
            this.lightMapCoord = lightMapCoord;
            this.vertexColor = vertexColor;
        }

        #region Operators
        public static bool operator !=(Q3BSPVertex left,
                                        Q3BSPVertex right)
        {
            return left.GetHashCode() != right.GetHashCode();
        }

        public static bool operator ==(Q3BSPVertex left,
                                        Q3BSPVertex right)
        {
            return left.GetHashCode() == right.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return this == (Q3BSPVertex)obj;
        }

        // Used for patch tesselation only
        public static Q3BSPVertex operator +(Q3BSPVertex left, Q3BSPVertex right)
        {
            return new Q3BSPVertex(
                left.position + right.position,
                left.normal + right.normal,
                left.textureCoord + right.textureCoord,
                left.lightMapCoord + right.lightMapCoord,
                new Color((left.vertexColor.ToVector4() + right.vertexColor.ToVector4())));
        }

        // Used for patch tesselation only
        public static Q3BSPVertex operator *(Q3BSPVertex left, float right) 
        {
            return new Q3BSPVertex(
                left.position*right,
                left.normal*right,
                left.textureCoord*right,
                left.lightMapCoord*right,
                new Color(left.vertexColor.ToVector4()*right));
        }

        #endregion 

        #region Properties
        public Vector3 Position 
        { 
            get { return position; } 
            set { position = value; } 
        }

        public Vector3 Normal 
        { 
            get { return normal; } 
            set { normal = value; } 
        }
        
        public Vector2 TextureCoord 
        { 
            get { return textureCoord; } 
            set { textureCoord = value; } 
        }
        
        public Vector2 LightMapCoord 
        { 
            get { return lightMapCoord; } 
            set { lightMapCoord = value; } 
        }
        
        public Color Color 
        { 
            get { return vertexColor; } 
            set { vertexColor = value; }
        }

        public static int SizeInBytes
        {
            get { return sizeof(float) * 10 + 4; }
        }
        #endregion 

        public override int GetHashCode()
        {
            return position.GetHashCode() |
                normal.GetHashCode() |
                textureCoord.GetHashCode() |
                lightMapCoord.GetHashCode() |
                vertexColor.GetHashCode();
        }

        public override string ToString()
        {
            return string.Format(
                "<{0}><{1}><{2}><{3}><{4}>",
                position.ToString(),
                normal.ToString(),
                textureCoord.ToString(),
                lightMapCoord.ToString(),
                vertexColor.ToString());
        }

        public static Q3BSPVertex FromStream(BinaryReader br)
        {
            Vector3 p = new Vector3();
            p.X = br.ReadSingle() / Q3BSPConstants.scale;
            p.Z = -br.ReadSingle() / Q3BSPConstants.scale;
            p.Y = br.ReadSingle() / Q3BSPConstants.scale;

            Vector2 tc = new Vector2();
            tc.X = br.ReadSingle();
            tc.Y = br.ReadSingle();

            Vector2 lc = new Vector2();
            lc.X = br.ReadSingle();
            lc.Y = br.ReadSingle();

            Vector3 n = new Vector3();
            n.X = br.ReadSingle();
            n.Z = -br.ReadSingle();
            n.Y = br.ReadSingle();

            byte[] vc = new byte[4];
            vc[0] = br.ReadByte();
            vc[1] = br.ReadByte();
            vc[2] = br.ReadByte();
            vc[3] = br.ReadByte();

            return new Q3BSPVertex(
                p, 
                n, 
                tc, 
                lc, 
                new Color(vc[0], vc[1], vc[2], vc[3]));
        }
    }
}
