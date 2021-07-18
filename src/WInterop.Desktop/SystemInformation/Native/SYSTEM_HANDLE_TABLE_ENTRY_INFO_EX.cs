// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;

namespace WInterop.SystemInformation
{
    [StructLayout(LayoutKind.Sequential)]
    public struct SYSTEM_HANDLE_TABLE_ENTRY_INFO_EX
    {
        public IntPtr Object;
        public UIntPtr UniqueProcessId;
        public UIntPtr HandleValue;

        // ACCESS_MASK
        public uint GrantedAccess;
        public ushort CreatorBackTraceIndex;
        public ushort ObjectTypeIndex;
        public uint HandleAttributes;
        public uint Reserved;
    }
}