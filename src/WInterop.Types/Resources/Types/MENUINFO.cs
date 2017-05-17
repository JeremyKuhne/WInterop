// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Resources.Types
{
    // https://msdn.microsoft.com/en-us/library/windows/desktop/ms647575.aspx
    public struct MENUINFO
    {
        public uint cbSize;
        public MenuInfoMembers fMask;
        public MenuStyles dwStyle;
        public uint cyMax;
        public IntPtr hbrBack;
        public uint dwContextHelpID;
        public UIntPtr dwMenuData;
    }
}
