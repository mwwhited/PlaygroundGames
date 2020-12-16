///////////////////////////////////////////////////////////////////////
// Project: XNA Quake3 Lib
// Author: Aanand Narayanan
// Copyright (c) 2006-2007 All rights reserved
///////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Storage;
using System.IO;

namespace XNAQ3Lib
{
    class Q3BSPShaderManager
    {
        const string noShader = "noshader";
        const string fileExt = ".jpg";
        string textureBasePath = "e:\\quake3\\data\\";
        Texture2D[] diffuseTextures;
        Texture2D nullTexture;
        Q3BSPLightMapManager lightMapManager = null;
        Effect basicEffect = null;

        public bool LoadTextures(Q3BSPTextureData[] textures, GraphicsDevice graphics, ContentManager content)
        {
            string texName;
            int texCount = textures.Length;

            diffuseTextures = new Texture2D[texCount];

            for (int i = 0; i < texCount; i++)
            {
                texName = textures[i].Name.Trim();
                Texture2D thisTexture = null;

                if (noShader != texName)
                {
                    texName = texName.Replace('/', '\\');
                    if (File.Exists(textureBasePath + texName + fileExt))
                    {
                        thisTexture = Texture2D.FromFile(graphics, textureBasePath + texName + fileExt);
                    }
                }
                diffuseTextures[i] = thisTexture;
            }

            basicEffect = content.Load<Effect>("Media\\basicQ3Effect");

            if (null == basicEffect)
            {
                throw (new Exception("basicQ3Effect failed to load. Ensure 'basicQ3Effect.fx' is added to the project."));
            }

            {
                Texture2D defT = new Texture2D(
                    graphics, 
                    2, 
                    2, 
                    1,
                    TextureUsage.AutoGenerateMipMap,
                    //ResourceUsage.Dynamic, 
                    SurfaceFormat.Color //, 
                    //ResourceManagementMode.Manual
                    );
                uint[] ltData = new uint[2 * 2];
                for (int l = 0; l < ltData.Length; l++)
                {
                    ltData[l] = 0xFFFFFFFF;
                }

                defT.SetData<uint>(ltData);
                nullTexture = defT;
            }

            return true;
        }

        public Effect GetEffect(int textureIndex, int lightMapIndex)
        {
            Texture2D tex = null;
            Texture2D ltm = null;

            if (null == basicEffect)
            {
                return null;
            }

            if (0 <= textureIndex && diffuseTextures.Length > textureIndex)
            {
                tex = diffuseTextures[textureIndex];
            }

            if (null != lightMapManager)
            {
                ltm = lightMapManager.GetLightMap(lightMapIndex);
            }

            if (null == tex)
            {
                tex = nullTexture;
            }

            basicEffect.Parameters["DiffuseTexture"].SetValue(tex);
            basicEffect.Parameters["LightMapTexture"].SetValue(ltm);
            if (null == ltm)
            {
                basicEffect.CurrentTechnique = basicEffect.Techniques["TransformAndTextureDiffuse"];
            }
            else
            {
                basicEffect.CurrentTechnique = basicEffect.Techniques["TransformAndTextureDiffuseAndLightMap"];
            }

            return basicEffect;
        }

        public string BasePath
        {
            get
            {
                return textureBasePath;
            }
            set
            {
                textureBasePath = value;
            }
        }

        public Q3BSPLightMapManager LightMapManager
        {
            get
            {
                return lightMapManager;
            }
            set
            {
                lightMapManager = value;
            }
        }
    }
}
