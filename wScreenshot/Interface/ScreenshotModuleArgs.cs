using System;
using System.Drawing;

namespace wScreenshot.Interface
{
    public class ScreenshotModuleArgs : EventArgs
    {
        public ScreenshotModuleArgs(Region ScreenshotRegion)
        {
            IsRegion = true;
            this.ScreenshotRegion = ScreenshotRegion;
        }

        public ScreenshotModuleArgs(Rectangle ScreenshotRectangle)
        {
            this.ScreenshotRectangle = ScreenshotRectangle;
        }

        public bool IsRectangle
        {
            get { return !IsRegion; }
            set { IsRegion = !value; }
        }

        public bool IsRegion { get; set; }

        public RectangleF ScreenshotRectangle { get; set; }

        public Region ScreenshotRegion { get; set; }
    }
}