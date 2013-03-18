using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace wScreenshot.Hooks
{
    public class MouseHookEventArgs
    {
        private System.Windows.Input.MouseButton? MouseButton;
        public MouseHookEventArgs(System.Windows.Input.MouseButton? MouseButton, int Clicks, int X, int Y, int Delta, bool Handled)
        {
            // TODO: Complete member initialization
            this.MouseButton = MouseButton;
            this.Clicks = Clicks;
            this.X = X;
            this.Y = Y;
            this.Delta = Delta;
            this.Handled = Handled;
        }

        public int Clicks { get; set; }
        public int Delta { get; set; }

        public bool Handled { get; set; }

        public int X { get; set; }
        public int Y { get; set; }
    }
}
