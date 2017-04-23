// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Desktop.Registry.Types
{
    // https://msdn.microsoft.com/en-us/library/windows/hardware/ff554217.aspx
    public struct KEY_VALUE_FULL_INFORMATION
    {
        public uint TitleIndex;
        public RegistryValueType Type;
        public uint DataOffset;
        public uint DataLength;
        public uint NameLength;
        public char Name;
    }
}
