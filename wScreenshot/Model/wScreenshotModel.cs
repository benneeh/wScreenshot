using System.ComponentModel;
using System.Windows.Media.Imaging;
using System.Windows.Shell;
using wScreenshot.Helper;

namespace wScreenshot.Model
{
    public class wScreenshotModel : INotifyPropertyChanged
    {
        #region private fields

        private BitmapSource _currentPicture;
        private bool _isSpecialWhiteButtonDown;
        private bool _isUploading;
        private KeyBoardEventHelper _keyBoard;
        private wScreenshotOptionsModel _options;
        private TaskbarItemProgressState _progressState = TaskbarItemProgressState.Error;
        private double _uploadProgress = 1;

        #endregion private fields

        #region public properties

        public KeyBoardEventHelper KeyBoard
        {
            get
            {
                if (_keyBoard == null)
                {
                    _keyBoard = new KeyBoardEventHelper();
                    _keyBoard.PropertyChanged += (s, e) => { RaisePropertyChangedEvent("KeyBoard"); };
                }
                return _keyBoard;
            }
            set
            {
                if (value != _keyBoard)
                {
                    _keyBoard = value;
                    RaisePropertyChangedEvent("KeyBoard");
                }
            }
        }

        public wScreenshotModel This
        {
            get { return this; }
        }

        public double UploadProgress
        {
            get { return _uploadProgress; }
            set
            {
                if (value != _uploadProgress)
                {
                    _uploadProgress = value;
                    RaisePropertyChangedEvent("UploadProgress");
                }
            }
        }

        public bool IsUploading
        {
            get { return _isUploading; }
            set
            {
                if (value != _isUploading)
                {
                    _isUploading = value;
                    RaisePropertyChangedEvent("IsUploading");
                }
            }
        }

        public TaskbarItemProgressState ProgressState
        {
            get { return _progressState; }
            set
            {
                if (value != _progressState)
                {
                    _progressState = value;
                    RaisePropertyChangedEvent("ProgressState");
                }
            }
        }

        public wScreenshotOptionsModel Options
        {
            get
            {
                if (_options == null)
                {
                    _options = new wScreenshotOptionsModel();
                }
                return _options;
            }
            set
            {
                if (value != _options)
                {
                    _options = value;
                    RaisePropertyChangedEvent("Options");
                }
            }
        }

        public BitmapSource CurrentPicture
        {
            get { return _currentPicture; }
            set
            {
                if (value != _currentPicture)
                {
                    _currentPicture = value;
                    RaisePropertyChangedEvent("CurrentPicture");
                }
            }
        }

        public bool IsSpecialWhiteButtonDown
        {
            get { return _isSpecialWhiteButtonDown; }
            set
            {
                if (value != _isSpecialWhiteButtonDown)
                {
                    _isSpecialWhiteButtonDown = value;
                    RaisePropertyChangedEvent("IsSpecialWhiteButtonDown");
                }
            }
        }

        #endregion public properties

        #region Constructors

        #endregion Constructors

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChangedEvent(string Property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(Property));
            }
        }

        #endregion INotifyPropertyChanged
    }
}