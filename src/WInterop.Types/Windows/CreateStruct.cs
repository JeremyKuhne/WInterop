// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using WInterop.Modules.Types;

namespace WInterop.Windows
{
    // https://msdn.microsoft.com/en-us/library/windows/desktop/ms632603.aspx
    public struct CREATESTRUCT
    {
        public IntPtr lpCreateParams;
        public IntPtr hInstance;
        public IntPtr hMenu;
        public WindowHandle hwndParent;
        public int cy;
        public int cx;
        public int y;
        public int x;
        public WindowStyles style;
        public IntPtr lpszName;
        public IntPtr lpszClass;
        public ExtendedWindowStyles dwExStyle;

        public unsafe string Name => lpszName == IntPtr.Zero ? null : new string((char*)lpszName.ToPointer());
        public unsafe string Class => lpszClass == IntPtr.Zero ? null : new string((char*)lpszClass.ToPointer());
        public ModuleInstance Instance => hInstance;
    }
}
