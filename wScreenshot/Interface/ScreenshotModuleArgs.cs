using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace wScreenshot.Interface
{
    public class ScreenshotModuleArgs : EventArgs
    {
        public ScreenshotModuleArgs(Region ScreenshotRegion)
        {
            this.IsRegion = true;
            this.ScreenshotRegion = ScreenshotRegion;
        }

        public ScreenshotModuleArgs(Rectangle ScreenshotRectangle)
        {
            this.ScreenshotRectangle = ScreenshotRectangle;
        }

        public bool IsRectangle
        {
            get
            {
                return !IsRegion;
            }
            set
            {
                IsRegion = !value;
            }
        }

        public bool IsRegion { get; set; }

        public RectangleF ScreenshotRectangle { get; set; }

        public Region ScreenshotRegion { get; set; }
    }
}