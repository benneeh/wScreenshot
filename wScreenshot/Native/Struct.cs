using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace wScreenshot.Native
{
    public class Struct
    {
        public struct ACTCTX
        {
            public int cbSize;
            public uint dwFlags;
            public string lpApplicationName;
            public string lpAssemblyDirectory;
            public string lpResourceName;
            public string lpSource;
            public ushort wLangId;
            public ushort wProcessorArchitecture;
        }

        public struct BROWSEINFO
        {
            public IntPtr hwndOwner;
            public int iImage;
            public IntPtr lParam;
            public Shell32.BrowseCallbackProc lpfn;
            public string lpszTitle;
            public IntPtr pidlRoot;
            public string pszDisplayName;
            public Enum.BrowseInfoFlags ulFlags;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto, Pack = 4)]
        public struct COMDLG_FILTERSPEC
        {
            [MarshalAs(UnmanagedType.LPWStr)] internal string pszName;

            [MarshalAs(UnmanagedType.LPWStr)] internal string pszSpec;
        }

        // Property System structs and consts

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto, Pack = 4)]
        public struct KNOWNFOLDER_DEFINITION
        {
            internal Enum.KF_CATEGORY category;

            [MarshalAs(UnmanagedType.LPWStr)] internal string pszName;

            [MarshalAs(UnmanagedType.LPWStr)] internal string pszCreator;

            [MarshalAs(UnmanagedType.LPWStr)] internal string pszDescription;

            internal Guid fidParent;

            [MarshalAs(UnmanagedType.LPWStr)] internal string pszRelativePath;

            [MarshalAs(UnmanagedType.LPWStr)] internal string pszParsingName;

            [MarshalAs(UnmanagedType.LPWStr)] internal string pszToolTip;

            [MarshalAs(UnmanagedType.LPWStr)] internal string pszLocalizedName;

            [MarshalAs(UnmanagedType.LPWStr)] internal string pszIcon;

            [MarshalAs(UnmanagedType.LPWStr)] internal string pszSecurity;

            internal uint dwAttributes;
            internal Enum.KF_DEFINITION_FLAGS kfdFlags;
            internal Guid ftidType;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 4)]
        public struct PROPERTYKEY
        {
            internal Guid fmtid;
            internal uint pid;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct Rect
        {
            public int left;
            public int top;
            public int right;
            public int bottom;
        }

        [SuppressMessage("Microsoft.Design", "CA1049:TypesThatOwnNativeResourcesShouldBeDisposable"),
         StructLayout(LayoutKind.Sequential, Pack = 4)]
        public struct TASKDIALOGCONFIG
        {
            public uint cbSize;
            public IntPtr hwndParent;
            public IntPtr hInstance;
            public Enum.TaskDialogFlags dwFlags;
            public Enum.TaskDialogCommonButtonFlags dwCommonButtons;

            [MarshalAs(UnmanagedType.LPWStr)] public string pszWindowTitle;

            public IntPtr hMainIcon;

            [MarshalAs(UnmanagedType.LPWStr)] public string pszMainInstruction;

            [MarshalAs(UnmanagedType.LPWStr)] public string pszContent;

            public uint cButtons;

            //[MarshalAs(UnmanagedType.LPArray)]
            [SuppressMessage("Microsoft.Reliability", "CA2006:UseSafeHandleToEncapsulateNativeResources")] public IntPtr
                pButtons;

            public int nDefaultButton;
            public uint cRadioButtons;

            //[MarshalAs(UnmanagedType.LPArray)]
            public IntPtr pRadioButtons;

            public int nDefaultRadioButton;

            [MarshalAs(UnmanagedType.LPWStr)] public string pszVerificationText;

            [MarshalAs(UnmanagedType.LPWStr)] public string pszExpandedInformation;

            [MarshalAs(UnmanagedType.LPWStr)] public string pszExpandedControlText;

            [MarshalAs(UnmanagedType.LPWStr)] public string pszCollapsedControlText;

            public IntPtr hFooterIcon;

            [MarshalAs(UnmanagedType.LPWStr)] public string pszFooterText;

            [MarshalAs(UnmanagedType.FunctionPtr)] public Kernel32.TaskDialogCallback pfCallback;

            public IntPtr lpCallbackData;
            public uint cxWidth;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 4)]
        public struct TASKDIALOG_BUTTON
        {
            public int nButtonID;

            [MarshalAs(UnmanagedType.LPWStr)] public string pszButtonText;
        }
    }
}