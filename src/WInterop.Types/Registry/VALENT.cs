// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Desktop.Registry.DataTypes
{
    // https://msdn.microsoft.com/en-us/library/windows/desktop/ms725490.aspx
    public unsafe struct VALENT
    {
        public char* ve_valuename;
        public uint ve_valuelen;
        public void* ve_valueptr;
        public RegistryValueType ve_type;
    }
}
