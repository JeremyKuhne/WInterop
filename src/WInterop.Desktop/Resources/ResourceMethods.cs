// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Drawing;
using WInterop.Gdi;
using WInterop.Modules.Types;
using WInterop.Resources.Types;
using WInterop.Support;
using WInterop.Windows;

namespace WInterop.Resources
{
    public static partial class ResourceMethods
    {
        /// <summary>
        /// Get the specified string resource from the given library.
        /// </summary>
        public unsafe static string LoadString(ModuleInstance library, int identifier)
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
            IconHandle handle = Imports.LoadIconW(ModuleInstance.Null, (IntPtr)id);
            if (handle.IsInvalid)
                throw Errors.GetIoExceptionForLastError();

            return handle;
        }

        public unsafe static IconHandle LoadIcon(string name, ModuleInstance module)
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

        public static unsafe CursorHandle LoadCursor(CursorId id)
        {
            CursorHandle handle = Imports.LoadCursorW(ModuleInstance.Null, (char*)(IntPtr)id);
            if (handle.IsInvalid)
                throw Errors.GetIoExceptionForLastError();

            return handle;
        }

        public static CursorHandle LoadCursorFromFile(string path)
        {
            CursorHandle handle = Imports.LoadCursorFromFileW(path);
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

        public static Point GetCursorPosition()
        {
            if (!Imports.GetCursorPos(out Point point))
                throw Errors.GetIoExceptionForLastError();

            return point;
        }

        public static Point GetPhysicalCursorPosition()
        {
            if (!Imports.GetPhysicalCursorPos(out Point point))
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
            if (!Imports.CreateCaret(window, bitmap, width, height))
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

        public static MenuHandle CreateMenu()
        {
            MenuHandle menu = Imports.CreateMenu();
            if (menu.IsInvalid)
                throw Errors.GetIoExceptionForLastError();
            return menu;
        }

        public unsafe static void AppendMenu(MenuHandle menu, string text, int id, bool disabled = false, bool @checked = false)
        {
            MenuFlags flags = MenuFlags.String;
            if (disabled) flags |= MenuFlags.Grayed;
            if (@checked) flags |= MenuFlags.Checked;

            fixed (char* c = text)
            {
                if (!Imports.AppendMenuW(menu, flags, (IntPtr)id, (IntPtr)c))
                    throw Errors.GetIoExceptionForLastError();
            }
        }
    }
}
