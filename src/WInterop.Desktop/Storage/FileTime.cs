// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using WInterop.Support;

namespace WInterop.Storage
{
    /// <summary>
    ///  100-nanosecond intervals (ticks) since January 1, 1601 (UTC).
    ///  [FILETIME]
    /// </summary>
    public struct FileTime
    {
        // https://msdn.microsoft.com/en-us/library/windows/desktop/ms724284.aspx
        // https://blogs.msdn.microsoft.com/oldnewthing/20040825-00/?p=38053/
        // https://blogs.msdn.microsoft.com/oldnewthing/20140307-00/?p=1573

        private readonly uint dwLowDateTime;
        private readonly uint dwHighDateTime;

        public FileTime(uint low, uint high)
        {
            dwLowDateTime = low;
            dwHighDateTime = high;
        }

        public ulong FileDateTime => Conversion.HighLowToLong(dwHighDateTime, dwLowDateTime);

        public DateTime ToDateTimeUtc() => DateTime.FromFileTimeUtc((long)FileDateTime);
    }
}