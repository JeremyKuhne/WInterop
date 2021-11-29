// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Runtime.InteropServices;
using WInterop.SystemInformation;

namespace WInterop.SystemInformation.Native;

[StructLayout(LayoutKind.Sequential)]
public struct PERF_DATA_HEADER
{
    public uint dwTotalSize;
    public uint dwNumCounters;
    public long PerfTimeStamp;
    public long PerfTime100NSec;
    public long PerfFreq;
    public SystemTime SystemTime;
}