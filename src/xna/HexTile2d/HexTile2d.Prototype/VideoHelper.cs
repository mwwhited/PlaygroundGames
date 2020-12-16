using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace HexTile2d.Prototype
{
    public static class VideoHelper
    {
        //http://www.ziggyware.com/readarticle.php?article_id=66
        public static bool InitGraphicsMode(this GraphicsDeviceManager graphics, int iWidth, int iHeight, bool bFullScreen)
        {
            // If we aren't using a full screen mode, the height and width of the window can
            // be set to anything equal to or smaller than the actual screen size.
            if (bFullScreen == false)
            {
                if ((iWidth <= GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width)
                && (iHeight <= GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height))
                {
                    graphics.PreferredBackBufferWidth = iWidth;
                    graphics.PreferredBackBufferHeight = iHeight;
                    graphics.IsFullScreen = bFullScreen;
                    graphics.ApplyChanges();
                    return true;
                }
            }
            else
            {
                // If we are using full screen mode, we should check to make sure that the display
                // adapter can handle the video mode we are trying to set. To do this, we will
                // iterate thorugh the display modes supported by the adapter and check them against
                // the mode we want to set.
                foreach (DisplayMode dm in GraphicsAdapter.DefaultAdapter.SupportedDisplayModes)
                {
                    // Check the width and height of each mode against the passed values
                    if ((dm.Width == iWidth) && (dm.Height == iHeight))
                    {
                        // The mode is supported, so set the buffer formats, apply changes and return
                        graphics.PreferredBackBufferWidth = iWidth;
                        graphics.PreferredBackBufferHeight = iHeight;
                        graphics.IsFullScreen = bFullScreen;
                        graphics.ApplyChanges();
                        return true;
                    }
                }
            }
            return false;
        }

        public static void SetHighestResolution(this Game myGame, GraphicsDeviceManager graphics)
        {
            //InitGraphicsMode(graphics, 1280, 720, false);

            if (!InitGraphicsMode(graphics, 1920, 1200, true))
                if (!InitGraphicsMode(graphics, 1920, 1080, true))
                    if (!InitGraphicsMode(graphics, 1280, 1024, true))
                        if (!InitGraphicsMode(graphics, 1280, 720, true))
                            if (!InitGraphicsMode(graphics, 1024, 768, true))
                                if (!InitGraphicsMode(graphics, 800, 600, true))
                                    if (!InitGraphicsMode(graphics, 640, 480, true))
                                        myGame.Exit();
        }
    }
}
