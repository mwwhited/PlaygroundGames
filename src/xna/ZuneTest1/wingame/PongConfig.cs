using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Content;
using System.IO;
using System.Xml.Serialization;
using Microsoft.Xna.Framework.GamerServices;

namespace PongWin
{
    [Serializable]
    public class PongConfig
    {
        public PongConfig() { }

        private float _scalingFactor;
        public float ScalingFactor
        {
            get
            {
                return _scalingFactor;
            }
            set
            {
                _scalingFactor = value;
            }
        }

        private Vector2 _screenOffset;
        public Vector2 ScreenOffset
        {
            get
            {
                return _screenOffset;
            }
            set
            {
                _screenOffset = value;
            }
        }

        public static void Save(StorageDevice storageDevice, PongConfig config)
        {
            config.Save(storageDevice);
        }

        public void Save(StorageDevice storageDevice)
        {
            using (StorageContainer container = storageDevice.OpenContainer("MyPong"))
            {
                string fileName = Path.Combine(container.Path, "PongConfig.xml");
                using (FileStream fileStream = File.Open(fileName, FileMode.Create, FileAccess.Write, FileShare.Write))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(PongConfig));
                    serializer.Serialize(fileStream, this);
                    fileStream.Close();
                }
            }
        }

        public static PongConfig Load(StorageDevice storageDevice)
        {
            PongConfig _returnData = null;
            try
            {
                using (StorageContainer container = storageDevice.OpenContainer("MyPong"))
                {
                    string fileName = Path.Combine(container.Path, "PongConfig.xml");
                    if (File.Exists(fileName))
                    {
                        using (FileStream fileStream = File.Open(fileName, FileMode.Open, FileAccess.Read, FileShare.Read))
                        {
                            XmlSerializer serializer = new XmlSerializer(typeof(PongConfig));
                            _returnData = serializer.Deserialize(fileStream) as PongConfig;
                            fileStream.Close();
                        }
                    }
                }
            }
            catch { }
            return _returnData;

        }

    }
}
