using System;

namespace wScreenshot.Native
{
    public class Enum
    {
        [Flags]
        public enum BrowseInfoFlags
        {
            ReturnOnlyFsDirs = 0x00000001,
            DontGoBelowDomain = 0x00000002,
            StatusText = 0x00000004,
            ReturnFsAncestors = 0x00000008,
            EditBox = 0x00000010,
            Validate = 0x00000020,
            NewDialogStyle = 0x00000040,
            UseNewUI = NewDialogStyle | EditBox,
            BrowseIncludeUrls = 0x00000080,
            UaHint = 0x00000100,
            NoNewFolderButton = 0x00000200,
            NoTranslateTargets = 0x00000400,
            BrowseForComputer = 0x00001000,
            BrowseForPrinter = 0x00002000,
            BrowseIncludeFiles = 0x00004000,
            Shareable = 0x00008000,
            BrowseFileJunctions = 0x00010000
        }

        public enum CDCONTROLSTATE
        {
            CDCS_INACTIVE = 0x00000000,
            CDCS_ENABLED = 0x00000001,
            CDCS_VISIBLE = 0x00000002
        }

        public enum FDAP
        {
            FDAP_BOTTOM = 0x00000000,
            FDAP_TOP = 0x00000001,
        }

        public enum FDE_OVERWRITE_RESPONSE
        {
            FDEOR_DEFAULT = 0x00000000,
            FDEOR_ACCEPT = 0x00000001,
            FDEOR_REFUSE = 0x00000002
        }

        public enum FDE_SHAREVIOLATION_RESPONSE
        {
            FDESVR_DEFAULT = 0x00000000,
            FDESVR_ACCEPT = 0x00000001,
            FDESVR_REFUSE = 0x00000002
        }

        public enum FFFP_MODE
        {
            FFFP_EXACTMATCH,
            FFFP_NEARESTPARENTMATCH
        }

        [Flags]
        public enum FOS : uint
        {
            FOS_OVERWRITEPROMPT = 0x00000002,
            FOS_STRICTFILETYPES = 0x00000004,
            FOS_NOCHANGEDIR = 0x00000008,
            FOS_PICKFOLDERS = 0x00000020,
            FOS_FORCEFILESYSTEM = 0x00000040, // Ensure that items returned are filesystem items.
            FOS_ALLNONSTORAGEITEMS = 0x00000080, // Allow choosing items that have no storage.
            FOS_NOVALIDATE = 0x00000100,
            FOS_ALLOWMULTISELECT = 0x00000200,
            FOS_PATHMUSTEXIST = 0x00000800,
            FOS_FILEMUSTEXIST = 0x00001000,
            FOS_CREATEPROMPT = 0x00002000,
            FOS_SHAREAWARE = 0x00004000,
            FOS_NOREADONLYRETURN = 0x00008000,
            FOS_NOTESTFILECREATE = 0x00010000,
            FOS_HIDEMRUPLACES = 0x00020000,
            FOS_HIDEPINNEDPLACES = 0x00040000,
            FOS_NODEREFERENCELINKS = 0x00100000,
            FOS_DONTADDTORECENT = 0x02000000,
            FOS_FORCESHOWHIDDEN = 0x10000000,
            FOS_DEFAULTNOMINIMODE = 0x20000000
        }

        public enum FolderBrowserDialogMessage
        {
            Initialized = 1,
            SelChanged = 2,
            ValidateFailedA = 3,
            ValidateFailedW = 4,
            EnableOk = 0x465,
            SetSelection = 0x467
        }

        [Flags]
        public enum FormatMessageFlags
        {
            FORMAT_MESSAGE_ALLOCATE_BUFFER = 0x00000100,
            FORMAT_MESSAGE_IGNORE_INSERTS = 0x00000200,
            FORMAT_MESSAGE_FROM_STRING = 0x00000400,
            FORMAT_MESSAGE_FROM_HMODULE = 0x00000800,
            FORMAT_MESSAGE_FROM_SYSTEM = 0x00001000,
            FORMAT_MESSAGE_ARGUMENT_ARRAY = 0x00002000
        }

        public enum KF_CATEGORY
        {
            KF_CATEGORY_VIRTUAL = 0x00000001,
            KF_CATEGORY_FIXED = 0x00000002,
            KF_CATEGORY_COMMON = 0x00000003,
            KF_CATEGORY_PERUSER = 0x00000004
        }

        [Flags]
        public enum KF_DEFINITION_FLAGS
        {
            KFDF_PERSONALIZE = 0x00000001,
            KFDF_LOCAL_REDIRECT_ONLY = 0x00000002,
            KFDF_ROAMABLE = 0x00000004,
        }

        public enum SIATTRIBFLAGS
        {
            SIATTRIBFLAGS_AND = 0x00000001, // if multiple items and the attirbutes together.
            SIATTRIBFLAGS_OR = 0x00000002, // if multiple items or the attributes together.
            SIATTRIBFLAGS_APPCOMPAT = 0x00000003,
            // Call GetAttributes directly on the ShellFolder for multiple attributes
        }

        public enum SIGDN : uint
        {
            SIGDN_NORMALDISPLAY = 0x00000000, // SHGDN_NORMAL
            SIGDN_PARENTRELATIVEPARSING = 0x80018001, // SHGDN_INFOLDER | SHGDN_FORPARSING
            SIGDN_DESKTOPABSOLUTEPARSING = 0x80028000, // SHGDN_FORPARSING
            SIGDN_PARENTRELATIVEEDITING = 0x80031001, // SHGDN_INFOLDER | SHGDN_FOREDITING
            SIGDN_DESKTOPABSOLUTEEDITING = 0x8004c000, // SHGDN_FORPARSING | SHGDN_FORADDRESSBAR
            SIGDN_FILESYSPATH = 0x80058000, // SHGDN_FORPARSING
            SIGDN_URL = 0x80068000, // SHGDN_FORPARSING
            SIGDN_PARENTRELATIVEFORADDRESSBAR = 0x8007c001, // SHGDN_INFOLDER | SHGDN_FORPARSING | SHGDN_FORADDRESSBAR
            SIGDN_PARENTRELATIVE = 0x80080001 // SHGDN_INFOLDER
        }

        [Flags]
        public enum TaskDialogCommonButtonFlags
        {
            OkButton = 0x0001, // selected control return value IDOK
            YesButton = 0x0002, // selected control return value IDYES
            NoButton = 0x0004, // selected control return value IDNO
            CancelButton = 0x0008, // selected control return value IDCANCEL
            RetryButton = 0x0010, // selected control return value IDRETRY
            CloseButton = 0x0020 // selected control return value IDCLOSE
        };

        public enum TaskDialogElements
        {
            Content,
            ExpandedInformation,
            Footer,
            MainInstruction
        }

        [Flags]
        public enum TaskDialogFlags
        {
            EnableHyperLinks = 0x0001,
            UseHIconMain = 0x0002,
            UseHIconFooter = 0x0004,
            AllowDialogCancellation = 0x0008,
            UseCommandLinks = 0x0010,
            UseCommandLinksNoIcon = 0x0020,
            ExpandFooterArea = 0x0040,
            ExpandedByDefault = 0x0080,
            VerificationFlagChecked = 0x0100,
            ShowProgressBar = 0x0200,
            ShowMarqueeProgressBar = 0x0400,
            CallbackTimer = 0x0800,
            PositionRelativeToWindow = 0x1000,
            RtlLayout = 0x2000,
            NoDefaultRadioButton = 0x4000,
            CanBeMinimized = 0x8000
        };

        public enum TaskDialogMessages
        {
            NavigatePage = WM_USER + 101,
            ClickButton = WM_USER + 102, // wParam = Button ID
            SetMarqueeProgressBar = WM_USER + 103, // wParam = 0 (nonMarque) wParam != 0 (Marquee)
            SetProgressBarState = WM_USER + 104, // wParam = new progress state
            SetProgressBarRange = WM_USER + 105, // lParam = MAKELPARAM(nMinRange, nMaxRange)
            SetProgressBarPos = WM_USER + 106, // wParam = new position
            SetProgressBarMarquee = WM_USER + 107,
            // wParam = 0 (stop marquee), wParam != 0 (start marquee), lparam = speed (milliseconds between repaints)
            SetElementText = WM_USER + 108,
            // wParam = element (TASKDIALOG_ELEMENTS), lParam = new element text (LPCWSTR)
            ClickRadioButton = WM_USER + 110, // wParam = Radio Button ID
            EnableButton = WM_USER + 111, // lParam = 0 (disable), lParam != 0 (enable), wParam = Button ID
            EnableRadioButton = WM_USER + 112, // lParam = 0 (disable), lParam != 0 (enable), wParam = Radio Button ID
            ClickVerification = WM_USER + 113, // wParam = 0 (unchecked), 1 (checked), lParam = 1 (set key focus)
            UpdateElementText = WM_USER + 114,
            // wParam = element (TASKDIALOG_ELEMENTS), lParam = new element text (LPCWSTR)
            SetButtonElevationRequiredState = WM_USER + 115,
            // wParam = Button ID, lParam = 0 (elevation not required), lParam != 0 (elevation required)
            UpdateIcon = WM_USER + 116
            // wParam = icon element (TASKDIALOG_ICON_ELEMENTS), lParam = new icon (hIcon if TDF_USE_HICON_* was set, PCWSTR otherwise)
        }

        public enum TaskDialogNotifications
        {
            Created = 0,
            Navigated = 1,
            ButtonClicked = 2, // wParam = Button ID
            HyperlinkClicked = 3, // lParam = (LPCWSTR)pszHREF
            Timer = 4, // wParam = Milliseconds since dialog created or timer reset
            Destroyed = 5,
            RadioButtonClicked = 6, // wParam = Radio Button ID
            DialogConstructed = 7,
            VerificationClicked = 8, // wParam = 1 if checkbox checked, 0 if not, lParam is unused and always 0
            Help = 9,
            ExpandoButtonClicked = 10 // wParam = 0 (dialog is now collapsed), wParam != 0 (dialog is now expanded)
        }

        public const int WM_USER = 0x400;
        public const int WM_GETICON = 0x007F;
        public const int WM_SETICON = 0x0080;
        public const int ICON_SMALL = 0;
    }
}