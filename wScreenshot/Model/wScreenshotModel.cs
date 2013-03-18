using System;
using System.ComponentModel;
using System.Threading;
using System.Windows.Media.Imaging;
using System.Windows.Shell;
using System.Windows.Threading;

namespace wScreenshot.Model
{
    public class wScreenshotModel : INotifyPropertyChanged
    {
        #region private fields

        private double _UploadProgress = 1;
        private TaskbarItemProgressState _ProgressState = TaskbarItemProgressState.Error;
        private BitmapSource _CurrentPicture;
        private bool _IsSpecialWhiteButtonDown;
        private wScreenshotOptionsModel _Options;
        private bool _IsUploading;

        #endregion private fields

        #region public properties

        public wScreenshotModel This
        {
            get
            {
                return this;
            }
        }

        public double UploadProgress
        {
            get
            {
                return _UploadProgress;
            }
            set
            {
                if (value != _UploadProgress)
                {
                    _UploadProgress = value;
                    RaisePropertyChangedEvent("UploadProgress");
                }
            }
        }

        public bool IsUploading
        {
            get
            {
                return _IsUploading;
            }
            set
            {
                if (value != _IsUploading)
                {
                    _IsUploading = value;
                    RaisePropertyChangedEvent("IsUploading");
                }
            }
        }

        public TaskbarItemProgressState ProgressState
        {
            get
            {
                return _ProgressState;
            }
            set
            {
                if (value != _ProgressState)
                {
                    _ProgressState = value;
                    RaisePropertyChangedEvent("ProgressState");
                }
            }
        }

        public wScreenshotOptionsModel Options
        {
            get
            {
                if (_Options == null)
                {
                    _Options = new wScreenshotOptionsModel();
                }
                return _Options;
            }
            set
            {
                if (value != _Options)
                {
                    _Options = value;
                    RaisePropertyChangedEvent("Options");
                }
            }
        }

        public BitmapSource CurrentPicture
        {
            get
            {
                return _CurrentPicture;
            }
            set
            {
                if (value != _CurrentPicture)
                {
                    _CurrentPicture = value;
                    RaisePropertyChangedEvent("CurrentPicture");
                }
            }
        }

        public bool IsSpecialWhiteButtonDown
        {
            get
            {
                return _IsSpecialWhiteButtonDown;
            }
            set
            {
                if (value != _IsSpecialWhiteButtonDown)
                {
                    _IsSpecialWhiteButtonDown = value;
                    RaisePropertyChangedEvent("IsSpecialWhiteButtonDown");
                }
            }
        }

        #endregion public properties

        #region Constructors

        public wScreenshotModel()
        {
        }

        #endregion Constructors

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