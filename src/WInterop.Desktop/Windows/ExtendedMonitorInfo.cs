// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace WInterop.Windows;

/// <summary>
///  Extended monitor information. [MONITORINFOEX]
/// </summary>
[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
public unsafe struct ExtendedMonitorInfo
{
    private const int CCHDEVICENAME = 32;

    private readonly MonitorInfo _info;
    private FixedString.Size32 _szDevice;

    public static unsafe ExtendedMonitorInfo Create() => new ExtendedMonitorInfo(sizeof(ExtendedMonitorInfo));

    private ExtendedMonitorInfo(int size)
    {
        _info = new MonitorInfo(size);
    }

    /// <summary>
    ///  The monitor bounds.
    /// </summary>
    public Rectangle Monitor => _info.Monitor;

    /// <summary>
    ///  The work area bounds.
    /// </summary>
    public Rectangle Work => _info.Work;

    public bool IsPrimary => _info.IsPrimary;

    public ReadOnlySpan<char> DeviceName => _szDevice.Buffer.SliceAtNull();
}