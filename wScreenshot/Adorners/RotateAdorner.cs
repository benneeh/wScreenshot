using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using wScreenshot.DataObject;

namespace wScreenshot.Adorners
{
    public class RotateAdorner : Adorner
    {
        private RotateChrome chrome;
        private VisualCollection visuals;
        private ContentControl _designerItem;

        protected override int VisualChildrenCount
        {
            get
            {
                return this.visuals.Count;
            }
        }

        public RotateAdorner(ContentControl designerItem)
            : base(designerItem)
        {
            SnapsToDevicePixels = true;
            this._designerItem = designerItem;
            var angryRectangle = designerItem.DataContext as AnnoyingRectangle;
            chrome = new RotateChrome();
            chrome.DataContext = angryRectangle;
            visuals = new VisualCollection(this);
            visuals.Add(this.chrome);
        }

        protected override Visual GetVisualChild(int index)
        {
            return visuals[index];
        }

        protected override Size ArrangeOverride(Size arrangeBounds)
        {
            chrome.Arrange(new Rect(new Point(0.0, 0.0), arrangeBounds));
            return arrangeBounds;
        }
    }
}