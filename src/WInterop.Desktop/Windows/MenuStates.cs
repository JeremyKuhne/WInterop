// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Windows
{
    [Flags]
    public enum MenuStates : uint
    {
        /// <summary>
        /// [MFS_GRAYED]
        /// </summary>
        Grayed = 0x00000003,

        /// <summary>
        /// [MFS_DISABLED]
        /// </summary>
        Disabled = Grayed,

        /// <summary>
        /// [MFS_CHECKED]
        /// </summary>
        Checked = MenuFlags.Checked,

        /// <summary>
        /// [MFS_HILITE]
        /// </summary>
        Hilite = MenuFlags.Hilite,

        /// <summary>
        /// [MFS_ENABLED]
        /// </summary>
        Enabled = MenuFlags.Enabled,

        /// <summary>
        /// [MFS_UNCHECKED]
        /// </summary>
        Unchecked = MenuFlags.Unchecked,

        /// <summary>
        /// [MFS_UNHILITE]
        /// </summary>
        Unhilite = MenuFlags.Unhilite,

        /// <summary>
        /// [MFS_DEFAULT]
        /// </summary>
        Default = MenuFlags.Default
    }
}
