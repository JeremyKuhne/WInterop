// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;

namespace WInterop.ProcessAndThreads;

// https://msdn.microsoft.com/en-us/library/windows/desktop/ms684874.aspx
[StructLayout(LayoutKind.Sequential)]
public readonly struct ProcessMemoryCountersExtended
{
    private readonly uint cb;
    public readonly uint PageFaultCount;
    public readonly UIntPtr PeakWorkingSetSize;
    public readonly UIntPtr WorkingSetSize;
    public readonly UIntPtr QuotaPeakPagedPoolUsage;
    public readonly UIntPtr QuotaPagedPoolUsage;
    public readonly UIntPtr QuotaPeakNonPagedPoolUsage;
    public readonly UIntPtr QuotaNonPagedPoolUsage;
    public readonly UIntPtr PagefileUsage;
    public readonly UIntPtr PeakPagefileUsage;
    public readonly UIntPtr PrivateUsage;
}