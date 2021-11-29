// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Windows;

public enum MonitorOption : uint
{
    /// <summary>
    ///  If no intersection, return null. [MONITOR_DEFAULTTONULL]
    /// </summary>
    DefaultToNull = 0x00000000,

    /// <summary>
    ///  If no intersection, default to the primary monitor. [MONITOR_DEFAULTTOPRIMARY]
    /// </summary>
    DefaultToPrimary = 0x00000001,

    /// <summary>
    ///  If no intersection, default to the nearest monitor. [MONITOR_DEFAULTTONEAREST]
    /// </summary>
    DefaultToNearest = 0x00000002
}