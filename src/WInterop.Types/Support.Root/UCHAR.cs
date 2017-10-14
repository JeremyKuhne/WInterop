// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop
{
    public struct UCHAR
    {
        public byte Value;
        public static implicit operator char(UCHAR c) => (char)c.Value;
    }
}
