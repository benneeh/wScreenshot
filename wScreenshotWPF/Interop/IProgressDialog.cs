using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using wScreenshot.Dialog;

namespace wScreenshot.Interop
{
#pragma warning disable 3003

    [ComImport]
    [Guid(CLSIDGuid.ProgressDialog)]
    internal class ProgressDialogRCW
    {
    }

    [ComImport,
    Guid(IIDGuid.IProgressDialog),
    CoClass(typeof(ProgressDialogRCW))]
    public interface ProgressDialog : IProgressDialog
    {
    }

    [Flags]
    public enum ProgressDialogFlags : uint
    {
        Normal = 0x00000000,
        Modal = 0x00000001,
        AutoTime = 0x00000002,
        NoTime = 0x00000004,
        NoMinimize = 0x00000008,
        NoProgressBar = 0x00000010,
        MarqueeProgress = 0x00000020,
        NoCancel = 0x00000040
    }

    [ComImport]
    [Guid(IIDGuid.IProgressDialog)]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IProgressDialog
    {
        [PreserveSig]
        void StartProgressDialog(IntPtr hwndParent, [MarshalAs(UnmanagedType.IUnknown)]			object punkEnableModless, ProgressDialogFlags dwFlags, IntPtr pvResevered);

        [PreserveSig]
        void StopProgressDialog();

        [PreserveSig]
        void SetTitle([MarshalAs(UnmanagedType.LPWStr)]			string pwzTitle);

        [PreserveSig]
        void SetAnimation(SafeModuleHandle hInstAnimation, ushort idAnimation);

        [PreserveSig]
        [return: MarshalAs(UnmanagedType.Bool)]
        bool HasUserCancelled();

        [PreserveSig]
        void SetProgress(uint dwCompleted, uint dwTotal);

        [PreserveSig]
        void SetProgress64(ulong ullCompleted, ulong ullTotal);

        [PreserveSig]
        void SetLine(uint dwLineNum, [MarshalAs(UnmanagedType.LPWStr)]			string pwzString, [MarshalAs(UnmanagedType.VariantBool)]		bool fCompactPath, IntPtr pvResevered);

        [PreserveSig]
        void SetCancelMsg([MarshalAs(UnmanagedType.LPWStr)]			string pwzCancelMsg, object pvResevered);

        [PreserveSig]
        void Timer(uint dwTimerAction, object pvResevered);
    }
}