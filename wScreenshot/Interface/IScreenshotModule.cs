namespace wScreenshot.Interface
{
    public interface IScreenshotModule
    {
        bool IsBusy { get; }

        bool IsComplete { get; }
        event ScreenshotModuleHandler OnCancel;

        event ScreenshotModuleHandler OnComplete;

        void Initialize(Configuration.wScreenshot wScreenshotConfiguration);

        void Start();
    }
}