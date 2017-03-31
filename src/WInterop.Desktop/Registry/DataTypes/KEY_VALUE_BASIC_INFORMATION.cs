// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Desktop.Registry.DataTypes
{
    public struct KEY_VALUE_BASIC_INFORMATION
    {
        public uint TitleIndex;
        public RegistryValueType Type;
        public uint NameLength;
        public char Name;
    }
}
