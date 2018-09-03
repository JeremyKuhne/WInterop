// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Communications
{
    // https://msdn.microsoft.com/en-us/library/windows/desktop/aa363214.aspx
    public enum DtrControl : byte
    {
        /// <summary>
        /// [DTR_CONTROL_DISABLE]
        /// </summary>
        Disable = 0x00,

        /// <summary>
        /// [DTR_CONTROL_ENABLE]
        /// </summary>
        Enable = 0x01,

        /// <summary>
        /// [DTR_CONTROL_HANDSHAKE]
        /// </summary>
        Handshake = 0x02
    }
}
