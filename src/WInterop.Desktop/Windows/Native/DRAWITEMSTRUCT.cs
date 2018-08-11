// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using WInterop.Gdi.Native;

namespace WInterop.Windows.Native
{
    // https://docs.microsoft.com/en-us/windows/desktop/api/winuser/ns-winuser-tagdrawitemstruct
    public struct DRAWITEMSTRUCT
    {
        public OwnerDrawType CtlType;
        public uint CtlID;
        public uint itemID;
        public OwnerDrawActions itemAction;
        public OwnerDrawStates itemState;
        public WindowHandle hwndItem;
        public HDC hDC;
        public RECT rcItem;
        public IntPtr itemData;
    }
}
