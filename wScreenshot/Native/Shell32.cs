using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Text;
using wScreenshot.Interop;

namespace wScreenshot.Native
{
    public class Shell32
    {
        public delegate int BrowseCallbackProc(
            IntPtr hwnd, Enum.FolderBrowserDialogMessage msg, IntPtr lParam, IntPtr wParam);

        [DllImport("shell32.dll", CharSet = CharSet.Unicode)]
        public static extern int SHCreateItemFromParsingName([MarshalAs(UnmanagedType.LPWStr)] string pszPath,
            IntPtr pbc, ref Guid riid, [MarshalAs(UnmanagedType.Interface)] out object ppv);

        public static IShellItem CreateItemFromParsingName(string path)
        {
            object item;
            var guid = new Guid("43826d1e-e718-42ee-bc55-a1e261c37bfe"); // IID_IShellItem
            int hr = SHCreateItemFromParsingName(path, IntPtr.Zero, ref guid, out item);
            if (hr != 0)
                throw new Win32Exception(hr);
            return (IShellItem) item;
        }

        [DllImport("shell32.dll", CharSet = CharSet.Unicode)]
        public static extern IntPtr SHBrowseForFolder(ref Struct.BROWSEINFO lpbi);

        [DllImport("shell32.dll", SetLastError = true)]
        public static extern int SHGetSpecialFolderLocation(IntPtr hwndOwner, Environment.SpecialFolder nFolder,
            ref IntPtr ppidl);

        [DllImport("shell32.dll", PreserveSig = false)]
        public static extern IMalloc SHGetMalloc();

        [DllImport("shell32.dll", CharSet = CharSet.Unicode)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SHGetPathFromIDList(IntPtr pidl, StringBuilder pszPath);
    }
}