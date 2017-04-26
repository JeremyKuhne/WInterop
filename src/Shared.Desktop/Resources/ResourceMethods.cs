// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;
using WInterop.Gdi.Types;
using WInterop.Modules.Types;
using WInterop.Resources.Types;
using WInterop.Support;
using WInterop.Windows.Types;

namespace WInterop.Resources
{
    public static partial class ResourceMethods
    {
        /// <summary>
        /// Direct usage of Imports isn't recommended. Use the wrappers that do the heavy lifting for you.
        /// </summary>
        public static partial class Imports
        {
            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms647486.aspx
            [DllImport(Libraries.User32, SetLastError = true, ExactSpelling = true)]
            unsafe public static extern int LoadStringW(
                SafeModuleHandle hInstance,
                int uID,
                out char* lpBuffer,
                int nBufferMax);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms648402.aspx
            [DllImport(Libraries.User32, SetLastError = true, ExactSpelling = true)]
            public static extern bool GetCaretPos(
                out POINT lpPoint);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms648405.aspx
            [DllImport(Libraries.User32, SetLastError = true, ExactSpelling = true)]
            public static extern bool SetCaretPos(
                int X,
                int Y);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms648406.aspx
            [DllImport(Libraries.User32, SetLastError = true, ExactSpelling = true)]
            public static extern bool ShowCaret(
                WindowHandle hWnd);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms648403.aspx
            [DllImport(Libraries.User32, SetLastError = true, ExactSpelling = true)]
            public static extern bool HideCaret(
                WindowHandle hWnd);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms648399.aspx
            [DllImport(Libraries.User32, SetLastError = true, ExactSpelling = true)]
            public static extern bool CreateCaret(
                WindowHandle hWnd,
                BitmapHandle hBitmap,
                int nWidth,
                int nHeight);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms648400.aspx
            [DllImport(Libraries.User32, SetLastError = true, ExactSpelling = true)]
            public static extern bool DestroyCaret();

            // uint.MaxValue is INFINITE, or doesn't blink
            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms648401.aspx
            [DllImport(Libraries.User32, SetLastError = true, ExactSpelling = true)]
            public static extern uint GetCaretBlinkTime();

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms648404.aspx
            [DllImport(Libraries.User32, SetLastError = true, ExactSpelling = true)]
            public static extern bool SetCaretBlinkTime(
                uint uMSeconds);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms648072.aspx
            [DllImport(Libraries.User32, SetLastError = true, ExactSpelling = true)]
            public static extern SharedIconHandle LoadIconW(
                SafeModuleHandle hInstance,
                IntPtr lpIconName);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms648063.aspx
            [DllImport(Libraries.User32, SetLastError = true, ExactSpelling = true)]
            public static extern bool DestroyIcon(
                IntPtr hIcon);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms648386.aspx
            [DllImport(Libraries.User32, SetLastError = true, ExactSpelling = true)]
            public static extern bool DestroyCursor(
                IntPtr hCursor);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms648391.aspx
            [DllImport(Libraries.User32, SetLastError = true, ExactSpelling = true)]
            public static extern SharedCursorHandle LoadCursorW(
                SafeModuleHandle hInstance,
                IntPtr lpCursorName);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms648393.aspx
            [DllImport(Libraries.User32, ExactSpelling = true)]
            public static extern IntPtr SetCursor(
                CursorHandle hCursor);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms648396.aspx
            [DllImport(Libraries.User32, ExactSpelling = true)]
            public static extern int ShowCursor(
                bool bShow);
        }

        /// <summary>
        /// Get the specified string resource from the given library.
        /// </summary>
        unsafe public static string LoadString(SafeModuleHandle library, int identifier)
        {
            // A string resource is mapped in with the dll, there is no need to allocate
            // or free a buffer.

            // Passing 0 will give us a read only handle to the string resource
            int result = Imports.LoadStringW(library, identifier, out char* buffer, 0);
            if (result <= 0)
                throw Errors.GetIoExceptionForLastError(identifier.ToString());

            // Null is not included in the result
            return new string(buffer, 0, result);
        }

        public static IconHandle LoadIcon(IconId id)
        {
            IconHandle handle = Imports.LoadIconW(SafeModuleHandle.NullModuleHandle, (IntPtr)id);
            if (handle.IsInvalid)
                throw Errors.GetIoExceptionForLastError();

            return handle;
        }

        public unsafe static IconHandle LoadIcon(string name, SafeModuleHandle module)
        {
            IconHandle handle;
            fixed (char* n = name)
            {
                handle = Imports.LoadIconW(module, (IntPtr)n);
            }

            if (handle.IsInvalid)
                throw Errors.GetIoExceptionForLastError();

            return handle;
        }

        public static CursorHandle LoadCursor(CursorId id)
        {
            CursorHandle handle = Imports.LoadCursorW(SafeModuleHandle.NullModuleHandle, (IntPtr)id);
            if (handle.IsInvalid)
                throw Errors.GetIoExceptionForLastError();

            return handle;
        }

        public static CursorHandle SetCursor(CursorHandle cursor)
        {
            return Imports.SetCursor(cursor);
        }

        public static int ShowCursor(bool show)
        {
            return Imports.ShowCursor(show);
        }
    }
}
