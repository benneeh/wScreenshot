using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using wScreenshot.DataObject;

namespace wScreenshot.Helper.CanvasResizeTool
{
    public class MoveThumb : Thumb
    {
        private RotateTransform _rotateTransform;
        private ContentControl _designerItem;

        public MoveThumb()
        {
            DragStarted += this.MoveThumb_DragStarted;
            DragDelta += this.MoveThumb_DragDelta;
        }

        private void MoveThumb_DragStarted(object sender, DragStartedEventArgs e)
        {
            _designerItem = DataContext as ContentControl;

            if (_designerItem != null)
            {
                _rotateTransform = _designerItem.RenderTransform as RotateTransform;
            }
        }

        private void MoveThumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            if (_designerItem != null)
            {
                var dragDelta = new Point(e.HorizontalChange, e.VerticalChange);

                if (_rotateTransform != null)
                {
                    dragDelta = _rotateTransform.Transform(dragDelta);
                }
                var annoyingRectangle = _designerItem.DataContext as AnnoyingRectangle;
                annoyingRectangle.X += (decimal)dragDelta.X;
                annoyingRectangle.Y += (decimal)dragDelta.Y;

                //Canvas.SetLeft(_designerItem, Canvas.GetLeft(this._designerItem) + dragDelta.X);
                //Canvas.SetTop(_designerItem, Canvas.GetTop(this._designerItem) + dragDelta.Y);
            }
        }
    }
}