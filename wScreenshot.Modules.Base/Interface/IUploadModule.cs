using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace wScreenshot.Modules.Base.Interface
{
    public interface IUploadModule
    {
        bool IsFileBrowserEnabled { get; }

        bool IsFileDeletable { get; }
    }
}