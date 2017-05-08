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

namespace WInterop.Windows.Types
{
    public struct WindowClass
    {
        public ClassStyle Style;
        public WindowProcedure WindowProcedure;
        public int ClassExtraBytes;
        public int WindowExtraBytes;
        public SafeModuleHandle Instance;
        public IconHandle Icon;
        public CursorHandle Cursor;
        public BrushHandle Background;
        [MarshalAs(UnmanagedType.LPWStr)]
        public string MenuName;
        [MarshalAs(UnmanagedType.LPWStr)]
        public string ClassName;

        public unsafe WindowClass(WNDCLASS windowClass)
        {
            Style = windowClass.style;
            WindowProcedure = Marshal.GetDelegateForFunctionPointer<WindowProcedure>(windowClass.lpfnWndProc);
            ClassExtraBytes = windowClass.cbClassExtra;
            WindowExtraBytes = windowClass.cbWndExtra;
            Instance = windowClass.hInstance;
            Icon = windowClass.hIcon;
            Cursor = windowClass.hCursor;
            Background = windowClass.hCursor;
            MenuName = windowClass.lpszMenuName;
            ClassName = windowClass.lpszClassName == IntPtr.Zero
                ? null
                : Atom.IsAtom(windowClass.lpszClassName)
                    ? $"Atom: {windowClass.lpszClassName}"
                    : new string((char*)windowClass.lpszClassName.ToPointer());
        }
    }
}
