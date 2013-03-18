using System.ComponentModel;
using System.Windows.Media;

namespace wScreenshot.Model
{
    public class wScreenshotOptionsModel : INotifyPropertyChanged
    {
        #region private fields

        private Color _DefaultBackgroundColor = Colors.Black;

        #endregion private fields

        #region public properties

        public Color DefaultBackgroundColor
        {
            get
            {
                return _DefaultBackgroundColor;
            }
            set
            {
                if (value != _DefaultBackgroundColor)
                {
                    _DefaultBackgroundColor = value;
                    RaisePropertyChangedEvent("DefaultBackgroundColor");
                }
            }
        }

        #endregion public properties

        #region INotifyPropertyChanged

        private void RaisePropertyChangedEvent(string Property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(Property));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion INotifyPropertyChanged
    }
}