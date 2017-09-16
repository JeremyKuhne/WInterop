// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using WInterop.Gdi.Types;

namespace WInterop.Resources.Types
{
    public struct MenuInformation
    {
        public MenuInfoMembers Mask;
        public MenuStyles Style;
        public uint MaxHeight;
        public BrushHandle Background;
        public uint ContextHelpId;
        public UIntPtr MenuData;

        public static implicit operator MenuInformation(MENUINFO info)
        {
            return new MenuInformation
            {
                Mask = info.fMask,
                Style = info.dwStyle,
                MaxHeight = info.cyMax,
                Background = info.hbrBack,
                ContextHelpId = info.dwContextHelpID,
                MenuData = info.dwMenuData
            };
        }

        public unsafe static implicit operator MENUINFO(MenuInformation info)
        {
            return new MENUINFO
            {
                cbSize = (uint)sizeof(MENUINFO),
                fMask = info.Mask,
                dwStyle = info.Style,
                cyMax = info.MaxHeight,
                hbrBack = (IntPtr)info.Background,
                dwContextHelpID = info.ContextHelpId,
                dwMenuData = info.MenuData
            };
        }
    }
}
