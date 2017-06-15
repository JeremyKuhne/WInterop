// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.FileManagement.Types
{
    /// <summary>
    /// For NT times that are defined as longs (LARGE_INTEGER, etc.).
    /// Do NOT use for FILETIME unless you are POSITIVE it will fall on an
    /// 8 byte boundary.
    /// </summary>
    public struct LongFileTime
    {
        /// <summary>
        /// 100-nanosecond intervals (ticks) since January 1, 1601 (UTC).
        /// </summary>
        public long TicksSince1601;

        public DateTime ToDateTimeUtc() => DateTime.FromFileTimeUtc(TicksSince1601);
    }
}
