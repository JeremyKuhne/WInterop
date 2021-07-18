// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Registry.Native
{
    // https://msdn.microsoft.com/en-us/library/windows/hardware/ff554220.aspx
    public struct KEY_VALUE_PARTIAL_INFORMATION
    {
        public uint TitleIndex;
        public RegistryValueType Type;
        public uint DataLength;
        public char Data;
    }
}