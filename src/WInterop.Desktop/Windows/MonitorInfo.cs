// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Runtime.InteropServices;
using WInterop.Gdi;

namespace WInterop.Windows;

/// <summary>
///  Monitor information. [MONITORINFO]
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public readonly struct MonitorInfo
{
    private const uint MONITORINFOF_PRIMARY = 0x00000001;

    public readonly uint Size;
    public readonly Rect Monitor;
    public readonly Rect Work;
    public readonly uint Flags;

    public static unsafe MonitorInfo Create() => new(sizeof(MonitorInfo));

    internal MonitorInfo(int size)
    {
        this = default;
        Size = (uint)size;
    }

    public bool IsPrimary => (Flags & MONITORINFOF_PRIMARY) != 0;
}