// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Windows;

[Flags]
public enum MenuInfoMembers : uint
{
    /// <summary>
    ///  (MIM_APPLYTOSUBMENUS)
    /// </summary>
    ApplyToSubmenus = 0x80000000,

    /// <summary>
    ///  (MIM_BACKGROUND)
    /// </summary>
    BackGround = 0x00000002,

    /// <summary>
    ///  (MIM_HELPID)
    /// </summary>
    HelpId = 0x00000004,

    /// <summary>
    ///  (MIM_MAXHEIGHT)
    /// </summary>
    MaxHeight = 0x00000001,

    /// <summary>
    ///  (MIM_MENUDATA)
    /// </summary>
    MenuData = 0x00000008,

    /// <summary>
    ///  (MIM_STYLE)
    /// </summary>
    Style = 0x00000010
}