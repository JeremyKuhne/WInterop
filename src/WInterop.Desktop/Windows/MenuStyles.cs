// ------------------------
//    WInterop Framework
// ------------------------

// Copyright [c] Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Windows
{
    [Flags]
    public enum MenuStyles : uint
    {
        /// <summary>
        /// [MNS_AUTODISMISS]
        /// </summary>
        AutoDismiss = 0x10000000,

        /// <summary>
        /// [MNS_CHECKORBMP]
        /// </summary>
        CheckOrBitmap = 0x04000000,

        /// <summary>
        /// [MNS_DRAGDROP]
        /// </summary>
        DragDrop = 0x20000000,

        /// <summary>
        /// [MNS_MODELESS]
        /// </summary>
        ModeLess = 0x40000000,

        /// <summary>
        /// [MNS_NOCHECK]
        /// </summary>
        NoCheck = 0x80000000,

        /// <summary>
        /// [MNS_NOTIFYBYPOS]
        /// </summary>
        NotifyByPosition = 0x08000000
    }
}
