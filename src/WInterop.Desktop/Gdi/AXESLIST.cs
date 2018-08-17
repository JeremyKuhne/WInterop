// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Gdi
{
    /// <summary>
    /// [AXESLIST]
    /// </summary>
    /// <msdn>https://msdn.microsoft.com/en-us/library/dd183360.aspx</msdn>
    public struct AxesList
    {
        public const uint STAMP_AXESLIST = (0x8000000 + 'a' + ('l' << 8));
        private const int MM_MAX_NUMAXES = 16;

        private uint Reserved;
        public uint NumberOfAxes;

        // Unfortunately the compiler doesn't allow sizeof(AXISINFO) even though it doesn't change
        private unsafe fixed byte _axlAxisInfo[MM_MAX_NUMAXES * (sizeof(int) * 2 + 16 * sizeof(char))];

        public unsafe Span<AxisInfo> AxisInfo
        {
            get
            {
                fixed (void* v = _axlAxisInfo)
                    return new Span<AxisInfo>(v, MM_MAX_NUMAXES);
            }
        }
    }
}
