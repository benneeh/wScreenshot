using Hardcodet.Wpf.TaskbarNotification;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web.Caching;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using wScreenshot.Adorners;
using wScreenshot.DataObject;

namespace wScreenshot.Helper.CanvasResizeTool
{
    public class RotateThumb : Thumb
    {
        //private RotateTransform _rotateTransform;
        private Vector _startVector;

        private Point _centerPoint;
        private Adorner adorner;
        private ContentControl _designerItem;
        private Canvas _canvas;
        private double _offset;

        public RotateThumb()
        {
            DragDelta += RotateThumb_DragDelta;
            DragStarted += RotateThumb_DragStarted;
            DragCompleted += RotateThumb_DragCompleted;
        }

        private void RotateThumb_DragStarted(object sender, DragStartedEventArgs e)
        {
            _designerItem = DataContext as ContentControl;

            if (_designerItem != null)
            {
                _canvas = _designerItem.FindParentControl<Canvas>();

                var annoyingRectangle = _designerItem.DataContext as AnnoyingRectangle;
                if (_canvas != null)
                {
                    _centerPoint = _designerItem.TranslatePoint(
                        new Point((double)annoyingRectangle.Width * _designerItem.RenderTransformOrigin.X,
                                  (double)annoyingRectangle.Height * _designerItem.RenderTransformOrigin.Y),
                                  _canvas);

                    Point startPoint = Mouse.GetPosition(_canvas);
                    _startVector = Point.Subtract(startPoint, _centerPoint);

                    _offset = annoyingRectangle.Angle;

                    AdornerLayer adornerLayer = AdornerLayer.GetAdornerLayer(this._canvas);
                    if (adornerLayer != null)
                    {
                        adorner = new RotateAdorner(this._designerItem);
                        adornerLayer.Add(this.adorner);
                    }
                }
            }
        }

        private void RotateThumb_DragCompleted(object sender, DragCompletedEventArgs e)
        {
            if (adorner != null)
            {
                AdornerLayer adornerLayer = AdornerLayer.GetAdornerLayer(this._canvas);
                if (adornerLayer != null)
                {
                    adornerLayer.Remove(this.adorner);
                }

                adorner = null;
            }
        }

        private Dictionary<int, List<double>> snapCache = new Dictionary<int, List<double>>();

        private List<double> GetSnapp0rs(int Subs = 32)
        {
            if (!snapCache.ContainsKey(Subs))
            {
                List<double> ret = new List<double>();
                double start = 0;
                for (double i = 0; i < Subs; i++)
                {
                    ret.Add(i * 360.0 / (double)Subs);
                }
                snapCache.Add(Subs, ret);
            }
            return snapCache[Subs];
        }

        private void RotateThumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            if (_designerItem != null && _canvas != null)
            {
                Point currentPoint = Mouse.GetPosition(_canvas);
                Vector deltaVector = Point.Subtract(currentPoint, _centerPoint);
                var annoyingRectangle = (AnnoyingRectangle)_designerItem.DataContext;
                double angle = _offset + Vector.AngleBetween(_startVector, deltaVector);
                while (angle > 360) angle -= 360;
                while (angle < 0) angle += 360;
                RotateTransform rotateTransform = _designerItem.RenderTransform as RotateTransform;
                if (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl))
                {
                    var q = from i in GetSnapp0rs()
                            select new
                            {
                                dif = i - angle,
                                val = i
                            };

                    annoyingRectangle.Angle = q.OrderBy(x => Math.Abs(x.dif)).First().val;
                }
                else
                {
                    annoyingRectangle.Angle = angle;
                }
                _designerItem.InvalidateMeasure();
            }
        }
    }
}