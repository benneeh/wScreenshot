using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace wScreenshot.Interface
{
    public interface IScreenshotModule
    {
        event ScreenshotModuleHandler OnCancel;

        event ScreenshotModuleHandler OnComplete;

        bool IsBusy { get; }

        bool IsComplete { get; }

        void Initialize(Configuration.wScreenshot wScreenshotConfiguration);

        void Start();
    }
}