﻿// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using WInterop.Gdi;

namespace WInterop.Windows;

public struct MenuInformation
{
    public MenuInfoMembers Mask;
    public MenuStyles Style;
    public uint MaxHeight;
    private HBRUSH _background;
    public uint ContextHelpId;
    public UIntPtr MenuData;

    public BrushHandle Background => new(_background, ownsHandle: false);

    public static implicit operator MenuInformation(MENUINFO info)
    {
        return new MenuInformation
        {
            Mask = (MenuInfoMembers)info.fMask,
            Style = (MenuStyles)info.dwStyle,
            MaxHeight = info.cyMax,
            _background = info.hbrBack,
            ContextHelpId = info.dwContextHelpID,
            MenuData = info.dwMenuData
        };
    }

    public static unsafe implicit operator MENUINFO(MenuInformation info)
    {
        return new MENUINFO
        {
            cbSize = (uint)sizeof(MENUINFO),
            fMask = (uint)info.Mask,
            dwStyle = (uint)info.Style,
            cyMax = info.MaxHeight,
            hbrBack = info.Background.Handle,
            dwContextHelpID = info.ContextHelpId,
            dwMenuData = info.MenuData
        };
    }
}