using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using wScreenshot.Dialog;

namespace wScreenshot.Native
{
    public class Kernel32
    {
        #region LoadLibrary

        [Flags]
        public enum LoadLibraryExFlags : uint
        {
            DontResolveDllReferences = 0x00000001,
            LoadLibraryAsDatafile = 0x00000002,
            LoadWithAlteredSearchPath = 0x00000008,
            LoadIgnoreCodeAuthzLevel = 0x00000010
        }

        [DllImport("kernel32", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern SafeModuleHandle LoadLibraryEx(
            string lpFileName,
            IntPtr hFile,
            LoadLibraryExFlags dwFlags
            );

        [DllImport("kernel32", SetLastError = true), ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool FreeLibrary(IntPtr hModule);

        #endregion LoadLibrary

        #region string Resources

        [DllImport("Kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern uint FormatMessage([MarshalAs(UnmanagedType.U4)] Enum.FormatMessageFlags dwFlags,
            IntPtr lpSource,
            uint dwMessageId, uint dwLanguageId, ref IntPtr lpBuffer,
            uint nSize, string[] Arguments);

        #endregion string Resources

        public delegate uint TaskDialogCallback(
            IntPtr hwnd, uint uNotification, IntPtr wParam, IntPtr lParam, IntPtr dwRefData);

        public const int ACTCTX_FLAG_ASSEMBLY_DIRECTORY_VALID = 0x004;

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern int GetCurrentThreadId();

        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist"),
         DllImport("comctl32.dll", PreserveSig = false)]
        public static extern void TaskDialogIndirect([In] ref Struct.TASKDIALOGCONFIG pTaskConfig, out int pnButton,
            out int pnRadioButton, [MarshalAs(UnmanagedType.Bool)] out bool pfVerificationFlagChecked);

        [DllImport("Kernel32.dll", SetLastError = true)]
        public static extern ActivationContextSafeHandle CreateActCtx(ref Struct.ACTCTX actctx);

        [DllImport("kernel32.dll"), ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
        public static extern void ReleaseActCtx(IntPtr hActCtx);

        [DllImport("Kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool ActivateActCtx(ActivationContextSafeHandle hActCtx, out IntPtr lpCookie);

        [DllImport("Kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool DeactivateActCtx(uint dwFlags, IntPtr lpCookie);
    }
}