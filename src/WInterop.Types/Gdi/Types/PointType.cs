// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Gdi.Types
{
    // https://msdn.microsoft.com/en-us/library/dd162813.aspx
    public enum PointType : byte
    {
        // This is the actual definition, but the only flag so
        // we'll combine and create our own values for ease of use.
        // PT_CLOSEFIGURE     = 0x01,

        PT_LINETO           = 0x02,
        PT_LINETOANDCLOSE   = 0x03,
        PT_BEZIERTO         = 0x04,
        PT_BEZIERTOANDCLOSE = 0x05,
        PT_MOVETO           = 0x06
    }
}
