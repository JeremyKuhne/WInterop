// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Communications
{
    // https://msdn.microsoft.com/en-us/library/windows/desktop/aa363214.aspx
    public enum RtsControl : byte
    {
        Disable = 0x00,
        Enable = 0x01,
        Handshake = 0x02,
        Toggle = 0x03
    }
}