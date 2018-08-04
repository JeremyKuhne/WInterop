// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Drawing;
using WInterop.Errors;
using WInterop.Gdi;
using WInterop.Modules;
using WInterop.Windows.Native;

namespace WInterop.Windows
{
    public static partial class Windows
    {

        public static unsafe CursorHandle LoadCursor(CursorId id)
        {
            HCURSOR handle = Imports.LoadCursorW(ModuleInstance.Null, (char*)(uint)id);
            if (handle.IsInvalid)
                throw Error.GetIoExceptionForLastError();

            return new CursorHandle(handle, ownsHandle: false);
        }

        public static CursorHandle LoadCursorFromFile(string path)
        {
            HCURSOR handle = Imports.LoadCursorFromFileW(path);
            if (handle.IsInvalid)
                throw Error.GetIoExceptionForLastError();

            return new CursorHandle(handle, ownsHandle: false);
        }

        public static CursorHandle SetCursor(CursorHandle cursor)
        {
            return new CursorHandle(Imports.SetCursor(cursor), ownsHandle: false);
        }

        /// <summary>
        /// Replaces the specified system cursor with the given cursor. The cursor will
        /// be destroyed and as such must not be loaded from a resource. Use CopyCursor
        /// on cursors loaded from resources before calling this method.
        /// </summary>
        public static void SetSystemCursor(CursorHandle cursor, SystemCursor id)
        {
            if (!Imports.SetSystemCursor(cursor, id))
                throw Error.GetIoExceptionForLastError();
        }

        public static int ShowCursor(bool show)
        {
            return Imports.ShowCursor(show);
        }

        public static CursorHandle CopyCursor(CursorHandle cursor)
        {
            CursorHandle copy = Imports.CopyCursor(cursor);
            if (copy.IsInvalid)
                throw Error.GetIoExceptionForLastError();

            return copy;
        }

        public static Point GetCursorPosition()
        {
            if (!Imports.GetCursorPos(out Point point))
                throw Error.GetIoExceptionForLastError();

            return point;
        }

        public static Point GetPhysicalCursorPosition()
        {
            if (!Imports.GetPhysicalCursorPos(out Point point))
                throw Error.GetIoExceptionForLastError();

            return point;
        }

        public static void SetCursorPosition(Point point)
        {
            if (!Imports.SetCursorPos(point.X, point.Y))
                throw Error.GetIoExceptionForLastError();
        }

        public static void SetPhysicalCursorPosition(Point point)
        {
            if (!Imports.SetPhysicalCursorPos(point.X, point.Y))
                throw Error.GetIoExceptionForLastError();
        }

        public static void CreateCaret(this in WindowHandle window, BitmapHandle bitmap, Size size)
        {
            if (!Imports.CreateCaret(window, bitmap, size.Width, size.Height))
                throw Error.GetIoExceptionForLastError();
        }

        public static void DestroyCaret()
        {
            if (!Imports.DestroyCaret())
                throw Error.GetIoExceptionForLastError();
        }

        public static void SetCaretPosition(Point point)
        {
            if (!Imports.SetCaretPos(point.X, point.Y))
                throw Error.GetIoExceptionForLastError();
        }

        public static void ShowCaret(this in WindowHandle window)
        {
            if (!Imports.ShowCaret(window))
                throw Error.GetIoExceptionForLastError();
        }

        public static void HideCaret(this in WindowHandle window)
        {
            if (!Imports.HideCaret(window))
                throw Error.GetIoExceptionForLastError();
        }
    }
}
