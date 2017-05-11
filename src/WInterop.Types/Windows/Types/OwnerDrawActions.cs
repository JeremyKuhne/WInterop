// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Windows.Types
{
    [Flags]
    public enum OwnerDrawActions : uint
    {
        // https://msdn.microsoft.com/en-us/library/windows/desktop/bb775802.aspx

        /// <summary>
        /// (ODA_DRAWENTIRE)
        /// </summary>
        DrawEntire = 0x0001,

        /// <summary>
        /// (ODA_SELECT)
        /// </summary>
        Select = 0x0002,

        /// <summary>
        /// (ODA_FOCUS)
        /// </summary>
        Focut = 0x0004
    }
}
