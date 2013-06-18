using System.ComponentModel;
using System.Windows.Media;
using wScreenshot.Annotations;

namespace wScreenshot.DataObject
{
    internal class AnnoyingRectangle : INotifyPropertyChanged
    {
        private GradientBrush _Backrground;
        private double _Height;
        private double _Width;
        private double _X;
        private double _Y;

        public double Width
        {
            get { return _Width; }
            set
            {
                if (value == _Width) return;
                _Width = value;
                OnPropertyChanged("Width");
            }
        }

        public double Height
        {
            get { return _Height; }
            set
            {
                if (value == _Height) return;
                _Height = value;
                OnPropertyChanged("Height");
            }
        }

        public double X
        {
            get { return _X; }
            set
            {
                if (value == _X) return;
                _X = value;
                OnPropertyChanged("X");
            }
        }

        public double Y
        {
            get { return _Y; }
            set
            {
                if (value == _Y) return;
                _Y = value;
                OnPropertyChanged("Y");
            }
        }

        public GradientBrush Background
        {
            get { return _Backrground; }
            set
            {
                if (value == _Backrground) return;
                _Backrground = value;
                OnPropertyChanged("Background");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected bool Equals(AnnoyingRectangle other)
        {
            return _Width == other._Width && _Height == other._Height && _X == other._X && _Y == other._Y;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = _Width.GetHashCode();
                hashCode = (hashCode * 397) ^ _Height.GetHashCode();
                hashCode = (hashCode * 397) ^ _X.GetHashCode();
                hashCode = (hashCode * 397) ^ _Y.GetHashCode();
                return hashCode;
            }
        }

        public static bool operator ==(AnnoyingRectangle a1, AnnoyingRectangle a2)
        {
            if (a1 != null)
            {
                return a1.Equals(a2);
            }
            if (a2 != null)
            {
                return false;
            }
            return true;
        }

        public static bool operator !=(AnnoyingRectangle a1, AnnoyingRectangle a2)
        {
            return !(a1 == a2);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (obj.GetType() != GetType()) return false;
            return Equals((AnnoyingRectangle)obj);
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}