using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace wScreenshot.Native
{
    public class Struct
    {
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto, Pack = 4)]
        public struct COMDLG_FILTERSPEC
        {
            [MarshalAs(UnmanagedType.LPWStr)]
            internal string pszName;

            [MarshalAs(UnmanagedType.LPWStr)]
            internal string pszSpec;
        }

        public struct ACTCTX
        {
            public int cbSize;
            public uint dwFlags;
            public string lpSource;
            public ushort wProcessorArchitecture;
            public ushort wLangId;
            public string lpAssemblyDirectory;
            public string lpResourceName;
            public string lpApplicationName;
        }

        public struct BROWSEINFO
        {
            public IntPtr hwndOwner;
            public IntPtr pidlRoot;
            public string pszDisplayName;
            public string lpszTitle;
            public wScreenshot.Native.Enum.BrowseInfoFlags ulFlags;
            public wScreenshot.Native.Shell32.BrowseCallbackProc lpfn;
            public IntPtr lParam;
            public int iImage;
        }

        // Property System structs and consts
        [StructLayout(LayoutKind.Sequential, Pack = 4)]
        public struct PROPERTYKEY
        {
            internal Guid fmtid;
            internal uint pid;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 4)]
        public struct TASKDIALOG_BUTTON
        {
            public int nButtonID;

            [MarshalAs(UnmanagedType.LPWStr)]
            public string pszButtonText;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1049:TypesThatOwnNativeResourcesShouldBeDisposable"), StructLayout(LayoutKind.Sequential, Pack = 4)]
        public struct TASKDIALOGCONFIG
        {
            public uint cbSize;
            public IntPtr hwndParent;
            public IntPtr hInstance;
            public Native.Enum.TaskDialogFlags dwFlags;
            public Native.Enum.TaskDialogCommonButtonFlags dwCommonButtons;

            [MarshalAs(UnmanagedType.LPWStr)]
            public string pszWindowTitle;

            public IntPtr hMainIcon;

            [MarshalAs(UnmanagedType.LPWStr)]
            public string pszMainInstruction;

            [MarshalAs(UnmanagedType.LPWStr)]
            public string pszContent;

            public uint cButtons;

            //[MarshalAs(UnmanagedType.LPArray)]
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2006:UseSafeHandleToEncapsulateNativeResources")]
            public IntPtr pButtons;

            public int nDefaultButton;
            public uint cRadioButtons;

            //[MarshalAs(UnmanagedType.LPArray)]
            public IntPtr pRadioButtons;

            public int nDefaultRadioButton;

            [MarshalAs(UnmanagedType.LPWStr)]
            public string pszVerificationText;

            [MarshalAs(UnmanagedType.LPWStr)]
            public string pszExpandedInformation;

            [MarshalAs(UnmanagedType.LPWStr)]
            public string pszExpandedControlText;

            [MarshalAs(UnmanagedType.LPWStr)]
            public string pszCollapsedControlText;

            public IntPtr hFooterIcon;

            [MarshalAs(UnmanagedType.LPWStr)]
            public string pszFooterText;

            [MarshalAs(UnmanagedType.FunctionPtr)]
            public wScreenshot.Native.Kernel32.TaskDialogCallback pfCallback;

            public IntPtr lpCallbackData;
            public uint cxWidth;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto, Pack = 4)]
        public struct KNOWNFOLDER_DEFINITION
        {
            internal Native.Enum.KF_CATEGORY category;

            [MarshalAs(UnmanagedType.LPWStr)]
            internal string pszName;

            [MarshalAs(UnmanagedType.LPWStr)]
            internal string pszCreator;

            [MarshalAs(UnmanagedType.LPWStr)]
            internal string pszDescription;

            internal Guid fidParent;

            [MarshalAs(UnmanagedType.LPWStr)]
            internal string pszRelativePath;

            [MarshalAs(UnmanagedType.LPWStr)]
            internal string pszParsingName;

            [MarshalAs(UnmanagedType.LPWStr)]
            internal string pszToolTip;

            [MarshalAs(UnmanagedType.LPWStr)]
            internal string pszLocalizedName;

            [MarshalAs(UnmanagedType.LPWStr)]
            internal string pszIcon;

            [MarshalAs(UnmanagedType.LPWStr)]
            internal string pszSecurity;

            internal uint dwAttributes;
            internal Native.Enum.KF_DEFINITION_FLAGS kfdFlags;
            internal Guid ftidType;
        }
    }
}