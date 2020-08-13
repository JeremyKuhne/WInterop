// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using WInterop.Errors;
using WInterop.Windows.Native;

namespace WInterop.Windows
{
    public static partial class Windows
    {
        public static MenuHandle CreateMenu()
        {
            MenuHandle menu = new MenuHandle(Imports.CreateMenu(), ownsHandle: true);
            if (menu.IsInvalid)
                Error.ThrowLastError();
            return menu;
        }

        public static unsafe void AppendMenu(MenuHandle menu, string text, int id, bool disabled = false, bool @checked = false)
        {
            MenuFlags flags = MenuFlags.String;
            if (disabled) flags |= MenuFlags.Grayed;
            if (@checked) flags |= MenuFlags.Checked;

            fixed (char* c = text)
            {
                Error.ThrowLastErrorIfFalse(
                    Imports.AppendMenuW(menu, flags, (IntPtr)id, (IntPtr)c));
            }
        }
    }
}
