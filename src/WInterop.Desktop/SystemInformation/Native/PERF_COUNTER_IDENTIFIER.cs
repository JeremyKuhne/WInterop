// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;

namespace WInterop.SystemInformation.Native
{
    // https://docs.microsoft.com/windows/win32/api/perflib/ns-perflib-perf_counter_identifier
    [StructLayout(LayoutKind.Sequential)]
    public struct PERF_COUNTER_IDENTIFIER
    {
        public Guid CounterSetGuid;
        public uint Status;

        /// <summary>
        ///  Size of the identifier "block", which is the size of the struct and the optional trailing
        ///  string. Must be a multiple of 8 bytes. (Note that the struct itself is.)
        /// </summary>
        public uint Size;
        public uint CounterId;
        public uint InstanceId;
        public uint Index;
        public uint Reserved;
    }
}
