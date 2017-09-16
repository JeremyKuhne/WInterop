// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using WInterop.Gdi.Types;

namespace WInterop.Windows.Types
{
    // https://msdn.microsoft.com/en-us/library/windows/desktop/bb775802.aspx
    public struct DRAWITEMSTRUCT
    {
        public OwnerDrawType CtlType;
        public uint CtlID;
        public uint itemID;
        public OwnerDrawActions itemAction;
        public OwnerDrawStates itemState;
        public WindowHandle hwndItem;
        public IntPtr hDC;
        public RECT rcItem;
        public IntPtr itemData;

        public DeviceContext DeviceContext => new DeviceContext(hDC, ownsHandle: false);
    }
}
