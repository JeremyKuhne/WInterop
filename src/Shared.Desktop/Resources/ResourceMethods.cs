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

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms648383.aspx
            [DllImport(Libraries.User32, SetLastError = true, ExactSpelling = true)]
            public static unsafe extern bool ClipCursor(
                RECT* lpRect);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms648384.aspx
            [DllImport(Libraries.User32, SetLastError = true, ExactSpelling = true)]
            public static extern CursorHandle CopyCursor(
                CursorHandle pcur);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms648385.aspx
            [DllImport(Libraries.User32, SetLastError = true, ExactSpelling = true)]
            public static extern CursorHandle CreateCursor(
                SafeModuleHandle hInst,
                int xHotSpot,
                int yHotSpot,
                int nWidth,
                int nHeight,
                byte[] pvANDPlane,
                byte[] pvXORPlane);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms648386.aspx
            [DllImport(Libraries.User32, SetLastError = true, ExactSpelling = true)]
            public static extern bool DestroyCursor(
                IntPtr hCursor);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms648387.aspx
            [DllImport(Libraries.User32, SetLastError = true, ExactSpelling = true)]
            public static extern bool GetClipCursor(
                out RECT lpRect);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms648388.aspx
            [DllImport(Libraries.User32, ExactSpelling = true)]
            public static extern SharedCursorHandle GetCursor();

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms648389.aspx
            [DllImport(Libraries.User32, SetLastError = true, ExactSpelling = true)]
            public static extern bool GetCursorInfo(
                ref CURSORINFO pci);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms648390.aspx
            [DllImport(Libraries.User32, SetLastError = true, ExactSpelling = true)]
            public static extern bool GetCursorPos(
                out POINT lpPoint);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/aa969464.aspx
            [DllImport(Libraries.User32, SetLastError = true, ExactSpelling = true)]
            public static extern bool GetPhysicalCursorPos(
                out POINT lpPoint);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms648391.aspx
            [DllImport(Libraries.User32, SetLastError = true, ExactSpelling = true)]
            public static extern SharedCursorHandle LoadCursorW(
                SafeModuleHandle hInstance,
                IntPtr lpCursorName);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms648392.aspx
            [DllImport(Libraries.User32, SetLastError = true, ExactSpelling = true)]
            public static extern SharedCursorHandle LoadCursorFromFileW(
                IntPtr lpFileName);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms648393.aspx
            [DllImport(Libraries.User32, ExactSpelling = true)]
            public static extern SharedCursorHandle SetCursor(
                CursorHandle hCursor);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms648394.aspx
            [DllImport(Libraries.User32, SetLastError = true, ExactSpelling = true)]
            public static extern bool SetCursorPos(
                int X,
                int Y);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/aa969465.aspx
            [DllImport(Libraries.User32, SetLastError = true, ExactSpelling = true)]
            public static extern bool SetPhysicalCursorPos(
                int X,
                int Y);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms648395.aspx
            [DllImport(Libraries.User32, SetLastError = true, ExactSpelling = true)]
            public static extern bool SetSystemCursor(
                CursorHandle hcur,
                SystemCursor id);

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

        /// <summary>
        /// Replaces the specified system cursor with the given cursor. The cursor will
        /// be destroyed and as such must not be loaded from a resource. Use CopyCursor
        /// on cursors loaded from resources before calling this method.
        /// </summary>
        public static void SetSystemCursor(CursorHandle cursor, SystemCursor id)
        {
            if (!Imports.SetSystemCursor(cursor, id))
                throw Errors.GetIoExceptionForLastError();

            // SetSystemCursor destroys passed in cursors
            cursor.SetHandleAsInvalid();
        }

        public static int ShowCursor(bool show)
        {
            return Imports.ShowCursor(show);
        }

        public static CursorHandle CopyCursor(CursorHandle cursor)
        {
            CursorHandle copy = Imports.CopyCursor(cursor);
            if (copy.IsInvalid)
                throw Errors.GetIoExceptionForLastError();

            return copy;
        }

        public static POINT GetCursorPosition()
        {
            if (!Imports.GetCursorPos(out POINT point))
                throw Errors.GetIoExceptionForLastError();

            return point;
        }

        public static POINT GetPhysicalCursorPosition()
        {
            if (!Imports.GetPhysicalCursorPos(out POINT point))
                throw Errors.GetIoExceptionForLastError();

            return point;
        }

        public static void SetCursorPosition(int x, int y)
        {
            if (!Imports.SetCursorPos(x, y))
                throw Errors.GetIoExceptionForLastError();
        }

        public static void SetPhysicalCursorPosition(int x, int y)
        {
            if (!Imports.SetPhysicalCursorPos(x, y))
                throw Errors.GetIoExceptionForLastError();
        }
    }
}
