using System.ComponentModel;

namespace wScreenshot.Interface
{
    public interface IUpload : INotifyPropertyChanged
    {
        string FileName { get; set; }

        bool IsBusy { get; set; }

        bool IsComplete { get; set; }

        long TotalBytes { get; }

        long TotalUploaded { get; }

        decimal Percentage { get; }

        void OnUploadCompleted();

        void OnUploadProgressChanged();

        void OnUploadFailed();
    }
}