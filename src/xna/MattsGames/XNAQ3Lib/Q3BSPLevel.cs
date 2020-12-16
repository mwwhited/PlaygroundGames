///////////////////////////////////////////////////////////////////////
// Project: XNA Quake3 Lib
// Author: Aanand Narayanan
// Copyright (c) 2006-2007 All rights reserved
///////////////////////////////////////////////////////////////////////
using System;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Content;

namespace XNAQ3Lib
{
    public sealed partial class Q3BSPLevel
    {
        #region Variables
        Q3BSPTextureData[] textureData;
        Plane[] planes;
        Q3BSPNode[] nodes;
        Q3BSPLeaf[] leafs;
        int[] leafFaces;
        int[] leafBrushes;
        Q3BSPModel[] models;
        Q3BSPBrush[] brushes;
        Q3BSPBrushSide[] brushSides;
        Q3BSPVertex[] vertices;
        int[] meshVertices;
        Q3BSPEffect[] effects;
        Q3BSPFace[] faces;
        Q3BSPLightMapData[] lightMapData;
        Q3BSPLightVolume[] lightVolumes;
        Q3BSPVisData visData;
        Q3BSPPatch[] patches;
        Q3BSPLightMapManager lightMapManager;
        Q3BSPShaderManager shaderManager;
        Q3BSPEntityManager entityManager;

        VertexDeclaration vertexDeclaration;

        string levelBasePath = "e:\\quake3\\data\\";
        Q3BSPLogger bspLogger;
        bool levelInitialized = false;
        bool[] facesToDraw;
        #endregion

        public Q3BSPLevel()
        {
            bspLogger = new Q3BSPLogger("c:\\alog.txt");
        }

        public bool InitializeLevel(GraphicsDevice graphics, ContentManager content)
        {
            bool bSuccess = true;


            lightMapManager = new Q3BSPLightMapManager();
            bSuccess = lightMapManager.GenerateLightMaps(lightMapData, graphics);

            if (bSuccess)
            {
                shaderManager = new Q3BSPShaderManager();
                shaderManager.BasePath = levelBasePath;
                bSuccess = shaderManager.LoadTextures(textureData, graphics, content);
            }

            if (bSuccess)
            {
                shaderManager.LightMapManager = lightMapManager;
                vertexDeclaration = new VertexDeclaration(graphics, Q3BSPVertex.VertexElements);
            }

            levelInitialized = bSuccess;

            bspLogger.WriteLine("Level initialized: " + levelInitialized.ToString());
            return levelInitialized;
        }

        public void RenderLevel(Vector3 cameraPosition, Matrix viewMatrix, Matrix projMatrix, GameTime gameTime, GraphicsDevice graphics)
        {
            int cameraLeaf = GetCameraLeaf(cameraPosition);
            int cameraCluster = leafs[cameraLeaf].Cluster;

            if (0 > cameraCluster)
            {
                return;
            }

            ResetFacesToDraw();

            BoundingFrustum frustum = new BoundingFrustum(viewMatrix * projMatrix);
            ArrayList visibleFaces = new ArrayList();
            foreach(Q3BSPLeaf leaf in leafs)
            {
                if (!visData.FastIsClusterVisible(cameraCluster, leaf.Cluster))
                {
                    continue;
                }

                if (!frustum.Intersects(leaf.Bounds))
                {
                    continue;
                }
                
                for (int i = 0; i < leaf.LeafFaceCount; i++)
                {
                    int faceIndex = leafFaces[leaf.StartLeafFace + i];
                    Q3BSPFace face = faces[faceIndex];
                    if (4 != face.FaceType && !facesToDraw[faceIndex])
                    {
                        facesToDraw[faceIndex] = true;
                        visibleFaces.Add(face);
                    }
                }
            }

            if (0 >= visibleFaces.Count)
            {
                return;
            }

            Q3BSPFaceComparer fc = new Q3BSPFaceComparer();
            visibleFaces.Sort(fc);

            graphics.VertexDeclaration = vertexDeclaration;
            Matrix matrixWorldViewProjection = viewMatrix * projMatrix;
            Effect effect;
            foreach (Q3BSPFace face in visibleFaces)
            {
                effect = shaderManager.GetEffect(face.TextureIndex, face.LightMapIndex);
                if (null != effect)
                {
                    effect.Parameters["WorldViewProj"].SetValue(matrixWorldViewProjection);
                    effect.Parameters["WorldView"].SetValue(viewMatrix);
                    if (Q3BSPConstants.faceTypePatch == face.FaceType)
                    {
                        effect.Begin();
                        foreach (EffectPass pass in effect.CurrentTechnique.Passes)
                        {
                            pass.Begin();
                            patches[face.PatchIndex].Draw(graphics);
                            pass.End();
                        }
                        effect.End();
                    }
                    else
                    {
                        RenderFace(face, effect, graphics);
                    }
                }
            }
        }

        private void RenderFace(Q3BSPFace face, Effect effect, GraphicsDevice graphics)
        {
            int[] indices;
            int triCount = face.MeshVertexCount / 3;

            indices = new int[face.MeshVertexCount];
            for (int i = 0; i < face.MeshVertexCount; i++)
            {
                indices[i] = meshVertices[face.StartMeshVertex + i];
            }
            effect.Begin();
            foreach (EffectPass pass in effect.CurrentTechnique.Passes)
            {
                pass.Begin();
                graphics.DrawUserIndexedPrimitives<Q3BSPVertex>(
                    PrimitiveType.TriangleList,
                    vertices,
                    face.StartVertex,
                    face.VertexCount,
                    indices,
                    0,
                    triCount);
                pass.End();
            }
            effect.End();
        }

        private int GetCameraLeaf(Vector3 cameraPosition)
        {
            int currentNode = 0;

            while (0 <= currentNode)
            {
                Plane currentPlane = planes[nodes[currentNode].Plane];
                if (PlaneIntersectionType.Front == ClassifyPoint(currentPlane, cameraPosition))
                {
                    currentNode = nodes[currentNode].Left;
                }
                else
                {
                    currentNode = nodes[currentNode].Right;
                }
            }

            return (~currentNode);
        }

        private PlaneIntersectionType ClassifyPoint(Plane plane, Vector3 pos)
        {
            float e = Vector3.Dot(plane.Normal, pos) - plane.D;

            if (e > Q3BSPConstants.epsilon)
            {
                return PlaneIntersectionType.Front;
            }

            if (e < -Q3BSPConstants.epsilon)
            {
                return PlaneIntersectionType.Back;
            }

            return PlaneIntersectionType.Intersecting;
        }

        private void ResetFacesToDraw()
        {
            for (int i = 0; i < facesToDraw.Length; i++)
            {
                facesToDraw[i] = false;
            }
        }

        #region Properties
        public string BasePath
        {
            get
            {
                return levelBasePath;
            }
            set
            {
                levelBasePath = value;
            }
        }
        #endregion
    }
}
