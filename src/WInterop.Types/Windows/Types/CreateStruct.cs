// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using WInterop.Modules.Types;

namespace WInterop.Windows.Types
{
    // https://msdn.microsoft.com/en-us/library/windows/desktop/ms632603.aspx
    public struct CREATESTRUCT
    {
        IntPtr lpCreateParams;
        IntPtr hInstance;
        IntPtr hMenu;
        WindowHandle hwndParent;
        int cy;
        int cx;
        int y;
        int x;
        WindowStyle style;
        IntPtr lpszName;
        IntPtr lpszClass;
        ExtendedWindowStyle dwExStyle;

        public unsafe string Name => lpszName == IntPtr.Zero ? null : new string((char*)lpszName.ToPointer());
        public unsafe string Class => lpszClass == IntPtr.Zero ? null : new string((char*)lpszClass.ToPointer());
        public SafeModuleHandle Instance => hInstance;
    }
}
