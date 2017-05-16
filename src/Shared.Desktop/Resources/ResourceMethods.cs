// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
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
            IconHandle handle = Imports.LoadIconW(SafeModuleHandle.Null, (IntPtr)id);
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
            CursorHandle handle = Imports.LoadCursorW(SafeModuleHandle.Null, (IntPtr)id);
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

        public static void CreateCaret(WindowHandle window, BitmapHandle bitmap, int width, int height)
        {
            if (!Imports.CreateCaret(window, bitmap ?? BitmapHandle.Null, width, height))
                throw Errors.GetIoExceptionForLastError();
        }

        public static void DestroyCaret()
        {
            if (!Imports.DestroyCaret())
                throw Errors.GetIoExceptionForLastError();
        }

        public static void SetCaretPosition(int x, int y)
        {
            if (!Imports.SetCaretPos(x, y))
                throw Errors.GetIoExceptionForLastError();
        }

        public static void ShowCaret(WindowHandle window)
        {
            if (!Imports.ShowCaret(window))
                throw Errors.GetIoExceptionForLastError();
        }

        public static void HideCaret(WindowHandle window)
        {
            if (!Imports.HideCaret(window))
                throw Errors.GetIoExceptionForLastError();
        }
    }
}
