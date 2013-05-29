using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using wScreenshot.Annotations;

namespace wScreenshot.DataObject
{
    internal class AnnoyingRectangle : INotifyPropertyChanged
    {
        private decimal _width;
        private decimal _height;
        private decimal _x;
        private decimal _y;

        public decimal Width
        {
            get { return _width; }
            set
            {
                if (value == _width) return;
                _width = value;
                OnPropertyChanged("Width");
            }
        }

        public decimal Height
        {
            get { return _height; }
            set
            {
                if (value == _height) return;
                _height = value;
                OnPropertyChanged("Height");
            }
        }

        public decimal X
        {
            get { return _x; }
            set
            {
                if (value == _x) return;
                _x = value;
                OnPropertyChanged("X");
            }
        }

        public decimal Y
        {
            get { return _y; }
            set
            {
                if (value == _y) return;
                _y = value;
                OnPropertyChanged("Y");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}