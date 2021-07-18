// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Windows.Native
{
    // https://msdn.microsoft.com/en-us/library/windows/desktop/ms647578.aspx
    public struct MENUITEMINFO
    {
        public uint cbSize;
        public MenuItemInfoMembers fMask;
        public MenuItemTypes fType;
        public MenuStates fState;
        public uint wID;
        public IntPtr hSubMenu;
        public IntPtr hbmpChecked;
        public IntPtr hbmpUnchecked;
        public UIntPtr dwItemData;
        public IntPtr dwTypeData;
        public uint cch;
        public IntPtr hbmpItem;
    }
}