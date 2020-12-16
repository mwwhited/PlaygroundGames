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

namespace XNAQ3Lib
{
    public struct Q3BSPCollisionData
    {
        public float ratio;
        public Vector3 collisionPoint;
        public bool startOutside;
        public bool inSolid;
        public Vector3 startPosition;
        public Vector3 endPosition;
    }

    public sealed partial class Q3BSPLevel
    {
        public Q3BSPCollisionData Trace(Vector3 startPosition, Vector3 endPosition)
        {
            Q3BSPCollisionData cd = new Q3BSPCollisionData();

            cd.startOutside = true;
            cd.inSolid = false;
            cd.ratio = 1.0f;
            cd.startPosition = startPosition;
            cd.endPosition = endPosition;
            cd.collisionPoint = startPosition;

            WalkNode(0, 0.0f, 1.0f, startPosition, endPosition, ref cd);

            if (1.0f == cd.ratio)
            {
                cd.collisionPoint = endPosition;
            }
            else
            {
                cd.collisionPoint = startPosition + cd.ratio * (endPosition - startPosition);
            }

            return cd;
        }

        private void WalkNode(int nodeIndex, float startRatio, float endRatio, Vector3 startPosition, Vector3 endPosition, ref Q3BSPCollisionData cd)
        {
            // Is this a leaf?
            if (0 > nodeIndex)
            {
                Q3BSPLeaf leaf = leafs[-(nodeIndex + 1)];
                for (int i = 0; i < leaf.LeafBrushCount; i++)
                {
                    Q3BSPBrush brush = brushes[leafBrushes[leaf.StartLeafBrush + i]];
                    if (0 < brush.BrushSideCount &&
                        1 == (textureData[brush.TextureIndex].Contents & 1))
                    {
                        CheckBrush(ref brush, ref cd);
                    }
                }

                return;
            }

            // This is a node
            Q3BSPNode thisNode = nodes[nodeIndex];
            Plane thisPlane = planes[thisNode.Plane];
            float startDistance = Vector3.Dot(startPosition, thisPlane.Normal) - thisPlane.D;
            float endDistance = Vector3.Dot(endPosition, thisPlane.Normal) - thisPlane.D;

            if (startDistance >= 0 && endDistance >= 0)
            {
                // Both points are in front
                WalkNode(thisNode.Left, startRatio, endRatio, startPosition, endPosition, ref cd);
            }
            else if (startDistance < 0 && endDistance < 0)
            {
                WalkNode(thisNode.Right, startRatio, endRatio, startPosition, endPosition, ref cd);
            }
            else
            {
                // The line spans the splitting plane
                int side = 0;
                float fraction1 = 0.0f;
                float fraction2 = 0.0f;
                float middleFraction = 0.0f;
                Vector3 middlePosition = new Vector3();

                if (startDistance < endDistance)
                {
                    side = 1;
                    float inverseDistance = 1.0f / (startDistance - endDistance);
                    fraction1 = (startDistance + Q3BSPConstants.epsilon) * inverseDistance;
                    fraction2 = (startDistance + Q3BSPConstants.epsilon) * inverseDistance;
                }
                else if (endDistance < startDistance)
                {
                    side = 0;
                    float inverseDistance = 1.0f / (startDistance - endDistance);
                    fraction1 = (startDistance + Q3BSPConstants.epsilon) * inverseDistance;
                    fraction2 = (startDistance - Q3BSPConstants.epsilon) * inverseDistance;
                }
                else
                {
                    side = 0;
                    fraction1 = 1.0f;
                    fraction2 = 0.0f;
                }

                if (fraction1 < 0.0f) fraction1 = 0.0f;
                else if (fraction1 > 1.0f) fraction1 = 1.0f;
                if (fraction2 < 0.0f) fraction2 = 0.0f;
                else if (fraction2 > 1.0f) fraction2 = 1.0f;

                middleFraction = startRatio + (endRatio - startRatio) * fraction1;
                middlePosition = startPosition + fraction1 * (endPosition - startPosition);

                int side1;
                int side2;
                if (0 == side)
                {
                    side1 = thisNode.Left;
                    side2 = thisNode.Right;
                }
                else
                {
                    side1 = thisNode.Right;
                    side2 = thisNode.Left;
                }

                WalkNode(side1, startRatio, middleFraction, startPosition, middlePosition, ref cd);

                middleFraction = startRatio + (endRatio - startRatio) * fraction2;
                middlePosition = startPosition + fraction2 * (endPosition - startPosition);

                WalkNode(side2, middleFraction, endRatio, middlePosition, endPosition, ref cd);
            }
        }

        private void CheckBrush(ref Q3BSPBrush brush, ref Q3BSPCollisionData cd)
        {
            float startFraction = -1.0f;
            float endFraction = 1.0f;
            bool startsOut = false;
            bool endsOut = false;

            for (int i = 0; i < brush.BrushSideCount; i++)
            {
                Q3BSPBrushSide brushSide = brushSides[brush.StartBrushSide + i];
                Plane plane = planes[brushSide.PlaneIndex];

                float startDistance = Vector3.Dot(cd.startPosition, plane.Normal) - plane.D;
                float endDistance = Vector3.Dot(cd.endPosition, plane.Normal) - plane.D;

                if (startDistance > 0)
                    startsOut = true;
                if (endDistance > 0)
                    endsOut = true;

                if (startDistance > 0 && endDistance > 0)
                {
                    return;
                }

                if (startDistance <= 0 && endDistance <= 0)
                {
                    continue;
                }

                if (startDistance > endDistance)
                {
                    float fraction = (startDistance - Q3BSPConstants.epsilon) / (startDistance - endDistance);
                    if (fraction > startFraction)
                        startFraction = fraction;
                }
                else
                {
                    float fraction = (startDistance + Q3BSPConstants.epsilon) / (startDistance - endDistance);
                    if (fraction < endFraction)
                        endFraction = fraction;
                }
            }

            if (false == startsOut)
            {
                cd.startOutside = false;
                if (false == endsOut)
                    cd.inSolid = true;

                return;
            }

            if (startFraction < endFraction)
            {
                if (startFraction > -1.0f && startFraction < cd.ratio)
                {
                    if (startFraction < 0)
                        startFraction = 0;
                    cd.ratio = startFraction;
                }
            }
        }
    }
}