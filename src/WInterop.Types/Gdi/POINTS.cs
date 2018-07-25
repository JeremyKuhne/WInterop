// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Gdi.Types
{
    // https://msdn.microsoft.com/en-us/library/dd162808.aspx
    public struct POINTS
    {
        public short x;
        public short y;

        public POINTS(short x, short y)
        {
            this.x = x;
            this.y = y;
        }
    }
}
