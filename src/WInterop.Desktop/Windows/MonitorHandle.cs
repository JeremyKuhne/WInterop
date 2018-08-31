// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using WInterop.Windows.Unsafe;

namespace WInterop.Windows
{
    /// <summary>
    /// Simple struct to encapsulate a monitor handle (HMONITOR).
    /// </summary>
    public readonly struct MonitorHandle
    {
        public HMONITOR HMONITOR { get; }

        public MonitorHandle(HMONITOR hmonitor) => HMONITOR = hmonitor;

        public static implicit operator HMONITOR(MonitorHandle handle) => handle.HMONITOR;
        public static implicit operator MonitorHandle(HMONITOR handle) => new MonitorHandle(handle);

        public override int GetHashCode() => HMONITOR.GetHashCode();

        public override bool Equals(object obj)
        {
            return obj is MonitorHandle other
                ? other.HMONITOR == HMONITOR
                : false;
        }

        public bool Equals(MonitorHandle other) => other.HMONITOR == HMONITOR;

        public static bool operator ==(MonitorHandle a, MonitorHandle b) => a.HMONITOR == b.HMONITOR;

        public static bool operator !=(MonitorHandle a, MonitorHandle b) => a.HMONITOR != b.HMONITOR;

        public bool IsInvalid => HMONITOR.IsInvalid;
    }
}
