// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Gdi.Types
{
    // https://msdn.microsoft.com/en-us/library/dd183360.aspx
    public struct AXESLIST
    {
        public const uint STAMP_AXESLIST = (0x8000000 + 'a' + ('l' << 8));
        public const int MM_MAX_NUMAXES = 16;

        public uint axlReserved;
        public uint axlNumAxes;
        private unsafe fixed byte _axlAxisInfo[MM_MAX_NUMAXES * AXISINFO.AxisInfoSize];

        public unsafe Span<AXISINFO> axlAxisInfo
        {
            get
            {
                fixed (void* v = _axlAxisInfo)
                    return new Span<AXISINFO>(v, MM_MAX_NUMAXES);
            }
        }
    }
}
