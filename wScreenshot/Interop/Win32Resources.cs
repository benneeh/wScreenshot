// Copyright (c) Sven Groot (Ookii.org) 2006
// See license.txt for details

using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Text;
using wScreenshot.Dialog;
using wScreenshot.Native;
using Enum = wScreenshot.Native.Enum;

namespace wScreenshot.Interop
{
    internal class Win32Resources : IDisposable
    {
        private const int _bufferSize = 500;
        private readonly SafeModuleHandle _moduleHandle;

        public Win32Resources(string module)
        {
            _moduleHandle = Kernel32.LoadLibraryEx(module, IntPtr.Zero,
                Kernel32.LoadLibraryExFlags.LoadLibraryAsDatafile);
            if (_moduleHandle.IsInvalid)
                throw new Win32Exception(Marshal.GetLastWin32Error());
        }

        public string LoadString(uint id)
        {
            CheckDisposed();

            var buffer = new StringBuilder(_bufferSize);
            if (User32.LoadString(_moduleHandle, id, buffer, buffer.Capacity + 1) == 0)
                throw new Win32Exception(Marshal.GetLastWin32Error());
            return buffer.ToString();
        }

        public string FormatString(uint id, params string[] args)
        {
            CheckDisposed();

            IntPtr buffer = IntPtr.Zero;
            string source = LoadString(id);

            // For some reason FORMAT_MESSAGE_FROM_HMODULE doesn't work so we use this way.
            Enum.FormatMessageFlags flags =
                Enum.FormatMessageFlags.FORMAT_MESSAGE_ALLOCATE_BUFFER
                | Enum.FormatMessageFlags.FORMAT_MESSAGE_ARGUMENT_ARRAY
                | Enum.FormatMessageFlags.FORMAT_MESSAGE_FROM_STRING;

            IntPtr sourcePtr = Marshal.StringToHGlobalAuto(source);
            try
            {
                if (Kernel32.FormatMessage(flags, sourcePtr, id, 0, ref buffer, 0, args) == 0)
                    throw new Win32Exception(Marshal.GetLastWin32Error());
            }
            finally
            {
                Marshal.FreeHGlobal(sourcePtr);
            }

            string result = Marshal.PtrToStringAuto(buffer);

            // FreeHGlobal calls LocalFree
            Marshal.FreeHGlobal(buffer);

            return result;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
                _moduleHandle.Dispose();
        }

        private void CheckDisposed()
        {
            if (_moduleHandle.IsClosed)
            {
                throw new ObjectDisposedException("Win32Resources");
            }
        }

        #region IDisposable Members

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion IDisposable Members
    }
}