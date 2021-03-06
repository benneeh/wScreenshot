﻿using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Shapes;

namespace wScreenshot.Controls.Converter
{
    public class ColorToDegreeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var r = (Rectangle) parameter;
            double x = (double) value + 15;
            while (x < 0) x += r.Width;
            while (x > r.Width) x -= r.Width;

            Color c = GetColorAtPoint(r, new Point(x, .5));

            return c;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        //Calculates the color of a point in a rectangle that is filled
        //with a LinearGradientBrush.
        private Color GetColorAtPoint(Rectangle theRec, Point thePoint)
        {
            //Get properties
            var br = (LinearGradientBrush) theRec.Fill;

            double y3 = thePoint.Y;
            double x3 = thePoint.X;

            double x1 = br.StartPoint.X*theRec.Width;
            double y1 = br.StartPoint.Y*theRec.Height;
            var p1 = new Point(x1, y1); //Starting point

            double x2 = br.EndPoint.X*theRec.Width;
            double y2 = br.EndPoint.Y*theRec.Height;
            var p2 = new Point(x2, y2); //End point

            //Calculate intersecting points
            var p4 = new Point(); //with tangent

            if (y1 == y2) //Horizontal case
            {
                p4 = new Point(x3, y1);
            }

            else if (x1 == x2) //Vertical case
            {
                p4 = new Point(x1, y3);
            }

            else //Diagnonal case
            {
                double m = (y2 - y1)/(x2 - x1);
                double m2 = -1/m;
                double b = y1 - m*x1;
                double c = y3 - m2*x3;

                double x4 = (c - b)/(m - m2);
                double y4 = m*x4 + b;
                p4 = new Point(x4, y4);
            }

            //Calculate distances relative to the vector start
            double d4 = dist(p4, p1, p2);
            double d2 = dist(p2, p1, p2);

            double x = d2 == 0 ? 0 : (d4/d2);

            //Clip the input if before or after the max/min offset values

            double max = br.GradientStops.Max(n => n.Offset);

            if (x > max)
            {
                x = max;
            }

            double min = br.GradientStops.Min(n => n.Offset);

            if (x < min)
            {
                x = min;
            }

            //Find gradient stops that surround the input value
            GradientStop gs0 = br.GradientStops.Where(n => n.Offset <= x).OrderBy(n => n.Offset).Last();
            GradientStop gs1 = br.GradientStops.Where(n => n.Offset >= x).OrderBy(n => n.Offset).First();

            float y = 0f;
            if (gs0.Offset != gs1.Offset)
            {
                y = (float) ((x - gs0.Offset)/(gs1.Offset - gs0.Offset));
            }

            //Interpolate color channels
            var cx = new Color();
            if (br.ColorInterpolationMode == ColorInterpolationMode.ScRgbLinearInterpolation)
            {
                float aVal = (gs1.Color.ScA - gs0.Color.ScA)*y + gs0.Color.ScA;
                float rVal = (gs1.Color.ScR - gs0.Color.ScR)*y + gs0.Color.ScR;
                float gVal = (gs1.Color.ScG - gs0.Color.ScG)*y + gs0.Color.ScG;
                float bVal = (gs1.Color.ScB - gs0.Color.ScB)*y + gs0.Color.ScB;
                cx = Color.FromScRgb(aVal, rVal, gVal, bVal);
            }
            else
            {
                var aVal = (byte) ((gs1.Color.A - gs0.Color.A)*y + gs0.Color.A);
                var rVal = (byte) ((gs1.Color.R - gs0.Color.R)*y + gs0.Color.R);
                var gVal = (byte) ((gs1.Color.G - gs0.Color.G)*y + gs0.Color.G);
                var bVal = (byte) ((gs1.Color.B - gs0.Color.B)*y + gs0.Color.B);
                cx = Color.FromArgb(aVal, rVal, gVal, bVal);
            }

            return cx;
        }

        private double dist(Point px, Point po, Point pf)
        {
            double d = Math.Sqrt((px.Y - po.Y)*(px.Y - po.Y) + (px.X - po.X)*(px.X - po.X));

            if (((px.Y < po.Y) && (pf.Y > po.Y)) ||
                ((px.Y > po.Y) && (pf.Y < po.Y)) ||
                ((px.Y == po.Y) && (px.X < po.X) && (pf.X > po.X)) ||
                ((px.Y == po.Y) && (px.X > po.X) && (pf.X < po.X)))
            {
                d = -d;
            }

            return d;
        }
    }
}