// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Runtime.InteropServices;

namespace WInterop.Memory;

/// <summary>
///  [PROCESS_MEMORY_COUNTERS_EX]
/// </summary>
/// <docs>https://docs.microsoft.com/windows/win32/api/psapi/ns-psapi-process_memory_counters_ex</docs>
[StructLayout(LayoutKind.Sequential)]
public struct ProcessMemoryCounters
{
    public uint StructSize;
    public uint PageFaultCount;
    public nuint PeakWorkingSetSize;
    public nuint WorkingSetSize;
    public nuint QuotaPeakPagedPoolUsage;
    public nuint QuotaPagedPoolUsage;
    public nuint QuotaPeakNonPagedPoolUsage;
    public nuint QuotaNonPagedPoolUsage;
    public nuint PagefileUsage;
    public nuint PeakPagefileUsage;
    public nuint PrivateUsage;
}