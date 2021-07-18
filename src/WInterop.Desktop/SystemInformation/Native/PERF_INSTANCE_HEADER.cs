// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Runtime.InteropServices;

namespace WInterop.SystemInformation.Native
{
    // https://docs.microsoft.com/windows/win32/api/perflib/ns-perflib-perf_instance_header
    [StructLayout(LayoutKind.Sequential)]
    public struct PERF_INSTANCE_HEADER
    {
        public uint Size;
        public uint InstanceId;
    }
}