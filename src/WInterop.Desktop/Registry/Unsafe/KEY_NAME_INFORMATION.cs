// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.


using System;

namespace WInterop.Registry.Unsafe
{
    // https://msdn.microsoft.com/en-us/library/windows/hardware/ff553381.aspx
    public struct KEY_NAME_INFORMATION
    {
        public uint NameLength;
        private char _Name;
        public ReadOnlySpan<char> Name => TrailingArray<char>.GetBufferInBytes(ref _Name, NameLength);
    }
}
