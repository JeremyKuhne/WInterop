// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Utility
{
    using System;
    using System.Runtime.InteropServices.ComTypes;

    public static class Conversion
    {
        public static ulong HighLowToLong(uint high, uint low)
        {
            return ((ulong)high) << 32 | ((ulong)low & 0xFFFFFFFFL);
        }

        public static DateTime FileTimeToDateTime(FILETIME fileTime)
        {
            return DateTime.FromFileTime((((long)fileTime.dwHighDateTime) << 32) + (uint)fileTime.dwLowDateTime);
        }
    }
}
